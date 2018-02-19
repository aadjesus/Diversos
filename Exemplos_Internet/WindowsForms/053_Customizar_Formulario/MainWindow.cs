using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BorderOverride
{
    public partial class MainWindow : Form
    {
        private const int WM_NCPAINT = 0x0085;
        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int WM_NCLBUTTONUP = 0x00A2;
        private const int WM_NCHITTEST = 0x0084;
        private const int WM_NCCALCSIZE = 0x0083;
        private const int WM_NCACTIVATE = 0x0086;
        private const int WM_CLOSE = 0x0010;

        private const int HTCATION = 2;
        private const int HTCLOSE = 20;
        private const int HTMINBUTTON = 8;
        private const int HTMAXBUTTON = 9;

        private Rectangle m_WindowRect;
        private Rectangle m_CloseButton;
        private Rectangle m_MaximizeButton;
        private Rectangle m_MinimizeButton;

        [DllImport( "user32.dll", SetLastError=true )]
        private static extern IntPtr GetWindowDC( IntPtr handleWindow );

        [DllImport( "User32.dll", SetLastError=true )]
        static extern int SendMessage( IntPtr handleWindow, int message, IntPtr wParam, IntPtr lParam );

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void WndProc( ref Message m )
        {
            switch( m.Msg )
            {
                /* This message is sent when we need to redraw the
                 * Non Client Area of the Window */
                case WM_NCPAINT:                        
                    DrawNonClientArea( m.HWnd );
                    DrawNonClientButtons( m.HWnd );
                    break;

                /* This message is sent when the Window State is changed
                 * (Forground/Background) */
                case WM_NCACTIVATE:                     
                    break;

                /* This message is sent when the Left Mouse button is pressed
                 * inside the Non Client Area. We have to determine if we are above
                 * the Close, Maximize or Minimize Button */
                case WM_NCLBUTTONDOWN:
                    switch( CheckButtons( GetMousePosition( m.LParam )))
                    {
                        case WindowButtons.CloseButton:
                            SendMessage( m.HWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero );
                            break;

                        case WindowButtons.MaximizeButton:
                            if( this.WindowState == FormWindowState.Maximized )
                            {
                                this.WindowState = FormWindowState.Normal;
                            }
                            else
                            {
                                this.WindowState = FormWindowState.Maximized;
                            }
                            break;

                        case WindowButtons.MinimizeButton:
                            this.WindowState = FormWindowState.Minimized;
                            break;

                        default:
                            break;
                    }

                    int msgResult = SendMessage( m.HWnd, WM_NCHITTEST, m.WParam, m.LParam );
                    if( msgResult != HTCLOSE && msgResult != HTMAXBUTTON && msgResult != HTMINBUTTON )
                    {
                        base.WndProc( ref m );
                    }
                    break;

                /* Unknown Message, needed to not allow the redraw of the System Buttons */
                case 0xAE:
                    break;

                /* This message is sent every time the mouse moves over the Titlebar */
                case WM_NCHITTEST:
                    base.WndProc( ref m );
                    break;

                /* This message is sent if we need to recalculate the size of the Non Client Area */
                case WM_NCCALCSIZE:
                    RecalcNonClientArea( m );
                    base.WndProc( ref m );
                    break;

                default:
                    Console.Out.WriteLine( m.ToString() );
                    base.WndProc( ref m );
                    break;
            }
        }

        private Point GetMousePosition( IntPtr mousePos )
        {
            int x = LowWord(( int ) mousePos ) - this.Location.X;
            int y = HighWord(( int ) mousePos ) - this.Location.Y;

            return( new Point( x, y ));
        }

        private static int HighWord( int number )
        {
            if(( number & 0x80000000 ) == 0x80000000 )
                return ( number >> 16 );
            else
                return ( number >> 16 ) & 0xffff;
        }

        private static int LowWord( int number )
        {
            return number & 0xffff;
        }

        private void RecalcNonClientArea( Message m )
        {
            if( m.WParam.ToInt32() == 1 )
            {
                m_WindowRect = ( Rectangle ) Marshal.PtrToStructure( m.LParam, typeof( Rectangle ) );
                Console.Out.WriteLine( "Recalc: {0}", m_WindowRect.ToString() );

                int buttonSize = 16;
                m_CloseButton = new Rectangle( new Point( Bounds.Width - buttonSize - 5, (( SystemInformation.CaptionHeight - buttonSize ) / 2 ) + 1 ),
                                               new Size( buttonSize, buttonSize ));

                m_MaximizeButton = new Rectangle( new Point( Bounds.Width - ( buttonSize * 2 ) - 10, (( SystemInformation.CaptionHeight - buttonSize ) / 2 ) + 1 ),
                                                  new Size( buttonSize, buttonSize ));

                m_MinimizeButton = new Rectangle( new Point( Bounds.Width - ( buttonSize * 3 ) - 15, (( SystemInformation.CaptionHeight - buttonSize ) / 2 ) + 1 ),
                                                  new Size( buttonSize, buttonSize ));
            }
        }

        private WindowButtons CheckButtons( Point mousePosition )
        {
            if( mousePosition.X > m_CloseButton.X && mousePosition.X < m_CloseButton.X + m_CloseButton.Size.Width &&
                mousePosition.Y > m_CloseButton.Y && mousePosition.Y < m_CloseButton.Y + m_CloseButton.Size.Height )
            {
                return WindowButtons.CloseButton;
            }
            else if( mousePosition.X > m_MaximizeButton.X && mousePosition.X < m_MaximizeButton.X + m_MaximizeButton.Size.Width &&
                     mousePosition.Y > m_MaximizeButton.Y && mousePosition.Y < m_MaximizeButton.Y + m_MaximizeButton.Size.Height )
            {
                return WindowButtons.MaximizeButton;
            }
            else if( mousePosition.X > m_MinimizeButton.X && mousePosition.X < m_MinimizeButton.X + m_MinimizeButton.Size.Width &&
                     mousePosition.Y > m_MinimizeButton.Y && mousePosition.Y < m_MinimizeButton.Y + m_MinimizeButton.Size.Height )
            {
                return WindowButtons.MinimizeButton;
            }
            else
            {
                return WindowButtons.NoButton;
            }
        }

        private void DrawNonClientButtons( IntPtr handleWindow )
        {
            int captionHeight = SystemInformation.CaptionHeight;

            Graphics gr = Graphics.FromHdc( GetWindowDC( handleWindow ));
            
            // Drawing the Close Button
            Pen closePen = new Pen( Brushes.Red, 1.0f );
            gr.DrawRectangle( closePen, m_CloseButton );
            gr.DrawLine( closePen, m_CloseButton.Location, new Point( m_CloseButton.Location.X + m_CloseButton.Size.Width, m_CloseButton.Location.Y + m_CloseButton.Size.Height ) );
            gr.DrawLine( closePen, new Point( m_CloseButton.Location.X + m_CloseButton.Size.Width, m_CloseButton.Location.Y ),
                                  new Point( m_CloseButton.Location.X, m_CloseButton.Location.Y + m_CloseButton.Size.Height ) );

            // Drawing the Maximize Button
            Pen maxPen = new Pen( Brushes.Green, 1.0f );
            gr.DrawRectangle( maxPen, m_MaximizeButton );
            gr.DrawLine( maxPen, new Point( m_MaximizeButton.Location.X + 2, m_MaximizeButton.Location.Y + 2 ),
                                 new Point( m_MaximizeButton.Location.X + m_MaximizeButton.Width - 2, m_MaximizeButton.Location.Y + 2 ));

            // Drawing the Minimize Button
            Pen minPen = new Pen( Brushes.Green, 1.0f );
            gr.DrawRectangle( minPen, m_MinimizeButton );
            gr.DrawLine( minPen, new Point( m_MinimizeButton.Location.X + 2, m_MinimizeButton.Location.Y + m_MinimizeButton.Size.Height - 2 ),
                                 new Point( m_MinimizeButton.Location.X + m_MinimizeButton.Width - 2, m_MaximizeButton.Location.Y + m_MinimizeButton.Size.Height - 2 ) );

            gr.Dispose();
        }

        private void DrawNonClientArea( IntPtr handleWindow )
        {
            int captionHeight = SystemInformation.CaptionHeight;
            int borderWidth = SystemInformation.Border3DSize.Width + ( SystemInformation.BorderSize.Width * 2 );

            Graphics gr = Graphics.FromHdc( GetWindowDC( handleWindow ) );

            // Draw the Borders
            SolidBrush brush = new SolidBrush( Color.FromArgb( 64, 64, 64 ));
            gr.FillRectangle( brush, new Rectangle( 0, 0, m_WindowRect.Width, captionHeight ));
            gr.FillRectangle( brush, new Rectangle( new Point( 0, captionHeight ), new Size( Bounds.Width, borderWidth )));
            gr.FillRectangle( brush, new Rectangle( new Point( 0, captionHeight + borderWidth ), new Size( borderWidth, Bounds.Height )));
            gr.FillRectangle( brush, new Rectangle( new Point( 0, Bounds.Height - borderWidth ), new Size( Bounds.Width, borderWidth )));
            gr.FillRectangle( brush, new Rectangle( new Point( Bounds.Width - borderWidth, captionHeight ), new Size( borderWidth, Bounds.Width - captionHeight )));

            // Draw the Window Title
            Font titlebarFont = new Font( FontFamily.GenericSerif, 12.0f, ( FontStyle.Italic | FontStyle.Bold ) );
            float textHeight = gr.MeasureString( this.Text, titlebarFont ).Height;
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gr.DrawString( this.Text, new Font( FontFamily.GenericSansSerif, 12.0f, ( FontStyle.Italic | FontStyle.Bold ) ), Brushes.White, new PointF( ( float ) borderWidth, ( captionHeight / 2 - textHeight / 2 ) +2 ) );

            gr.Dispose();            

        }

        private enum WindowButtons
        {
            CloseButton,
            MaximizeButton,
            MinimizeButton,
            NoButton
        }
    }
}