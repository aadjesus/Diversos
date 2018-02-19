using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _left = this.Left;
            _top = this.Top;


        }

        private bool dragging;
        private Point pointClicked;

        protected override void OnMouseDown(MouseEventArgs e)
        {

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
        }

        private int _left = 0;
        private int _top = 0;

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowPos
        {
            public IntPtr Hwnd;
            public IntPtr HwndInsertAfter;
            public int X;
            public int Y;
            public int CX;
            public int CY;
            public int Flags;
        }


        protected override void WndProc(ref Message mensagem)
        {
            const int WM_WINDOWPOSCHANGING = 0x46;

            switch (mensagem.Msg)
            {
                case WM_WINDOWPOSCHANGING:
                    {
                        if (this.WindowState == FormWindowState.Normal)
                        {

                            WindowPos newPosition = new WindowPos();
                            newPosition = (WindowPos)Marshal.PtrToStructure(mensagem.LParam, typeof(WindowPos));
                            newPosition.X = this.Location.X;
                            newPosition.Y = this.Location.Y;

                            Marshal.StructureToPtr(newPosition, mensagem.LParam, true);
                        }

                        break;
                    }
            }

            base.WndProc(ref mensagem);
        }
        protected override void OnLocationChanged(EventArgs e)
        {

            //this.Left = 0;
            //this.Top = 0;

            base.OnLocationChanged(e);
        }

        public struct Padding
        {
            // No padding
            public static readonly Padding Empty;
            private int p;

            // Constructors
            //Padding(int all);
            //Padding(int left, int top, int right, int bottom);


            // Properties (Properties window and code)
            int All { get; set; } // Get/Set all padding edges
            int Bottom { get; set; } // Get/Set bottom padding edge only
            int Left { get; set; } // Get/Set left padding edge only
            int Right { get; set; } // Get/Set right padding edge only
            int Top { get; set; } // Get/Set top padding edge only

            // Properties (Code only)
            //int Horizontal { get; } // Sum of Left and Right padding
            //int Vertical { get; } // Sum of Top and Bottom padding
            //Size Size { get; } // Horizontal + Vertical
        }



    }
}
