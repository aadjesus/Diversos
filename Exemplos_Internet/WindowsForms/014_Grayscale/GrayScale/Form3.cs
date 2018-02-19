using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GrayScale
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(400, 400);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 1, 15));
            }

            //b.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Bitmap b2 = new Bitmap(b);

            //using (Graphics g2 = Graphics.FromImage(b2))
            //{
            //    g2.DrawRectangle(new Pen(Color.White, 7.2f), 200, 100, 150, 100);
            //}

            using (Graphics g3 = this.panel1.CreateGraphics())
            {
                g3.DrawImage(b2, 0, 0);
            }


            //Bitmap b = new Bitmap(300, 400);
            //using (Graphics g = Graphics.FromImage(b))
            //{
            //    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 300, 400));
            //}

            //b.RotateFlip(RotateFlipType.Rotate90FlipNone);

            //using (Graphics g2 = Graphics.FromImage(b))
            //{
            //    g2.DrawRectangle(new Pen(Color.White, 7.2f), 200, 100, 150, 100);
            //}

            //using (Graphics g3 = this.panel1.CreateGraphics())
            //{
            //    g3.DrawImage(b, 0, 0);
            //}
        }
    }
}
