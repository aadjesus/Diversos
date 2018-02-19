using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Imagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.GetType().GetProperties
            //SimpleButton 
            //simpleButton1.ImageList
            //button1.ImageList
            //iconAnimator1.Start();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            using (Bitmap image = new Bitmap(screenBounds.Width, screenBounds.Height))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    Point p = new Point(0, 0);
                    g.CopyFromScreen(p, p, image.Size);
                    Rectangle cursorBounds = new Rectangle(Cursor.Position, Cursor.Current.Size);
                    Cursors.Default.Draw(g, cursorBounds);
                }
                image.Save("c:\\temp\\screen.jpg", ImageFormat.Jpeg);
            }
        }

        private void simpleButtonBGM1_Click(object sender, EventArgs e)
        {

            if (Enum.GetValues(typeof(FGlobus.Componentes.WinForms.SimpleButtonBGM.eTipoBotao))
                .OfType<FGlobus.Componentes.WinForms.SimpleButtonBGM.eTipoBotao>()
                .Aggregate((a, b) =>
                {
                    MessageBox.Show(b.ToString());
                    return a;
                }) == null)
            {

            }

        }


    }


}








