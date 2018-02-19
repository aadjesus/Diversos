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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rectangle screenBounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            //
            //Screen.AllScreens
            //    .OfType<Form>()
            //    .Where(w=> w.ac)

            //System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
            //System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height

            using (Bitmap image = new Bitmap(screenBounds.Width, screenBounds.Height))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    Point p = new Point(0, 0);
                    g.CopyFromScreen(p, p, image.Size);
                    Rectangle cursorBounds = new Rectangle(Cursor.Position, Cursor.Current.Size);
                    Cursors.Default.Draw(g, cursorBounds);
                }
                image.Save("c:\\screen.jpg", ImageFormat.Jpeg);
            }



        }

        class Teste
        {
            private int _valor;
            private int _desconto;
            private int _total;

            public int Total
            {
                get { return _total; }
                set { _total = value; }
            }

            public int Desconto
            {
                get { return _desconto; }
                set { _desconto = value; }
            }

            public int Valor
            {
                get { return _valor; }
                set { _valor = value; }
            }

            public static Teste operator +(Teste p1, Teste p2)
            {
                return new Teste()
                {
                    Total = (p1.Valor - p1.Desconto) + (p2.Valor - p2.Desconto)
                };
            }

            public static Teste operator ++(Teste p)
            {
                p.Total = p.Valor - p.Desconto;
                return p;
            }
        }

        private static void XISTO()
        {
            //Teste w = new Teste() { Valor = 10, Desconto = 5 };
            //w++;
            //Console.WriteLine(w.Total);

            //Teste t = w + g;
            //Console.WriteLine(t.Total);
        }

        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            MessageBox.Show("");
        }
    }


}
