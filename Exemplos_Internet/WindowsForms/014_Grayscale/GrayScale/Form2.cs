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
    public partial class Form2 : Form
    {
        private Bitmap DrawingArea;  // Area to draw on.

        private Button btnCircle;
        private Button btnLine;
        private Button btnOK;


        private System.Random rnd;
        private Pen myPen;

        public Form2()
        {
            InitializeComponent();
            rnd = new System.Random();
            myPen = new Pen(Color.Blue);
        }

        private void btnLine_Click(object sender, System.EventArgs e)
        {
            Graphics oGraphics;
            oGraphics = Graphics.FromImage(DrawingArea);

            myPen.Color = Color.Blue;

            for (int x = 1; x < 50; x++)
            {
                oGraphics.DrawLine(
                         myPen,
                        (int)rnd.Next(0, this.Width),
                        (int)rnd.Next(0, this.Height),
                        (int)rnd.Next(0, this.Width),
                        (int)rnd.Next(0, this.Height));
            }
            oGraphics.Dispose();

            this.Invalidate();
        }


        private void btnCircle_Click(object sender, System.EventArgs e)
        {
            Graphics oGraphics;
            oGraphics = Graphics.FromImage(DrawingArea);

            // get a radius for circle - up to 1/2 the width of form
            int radius = rnd.Next(0, (this.Width / 2));

            for (int x = 1; x < 50; x++)
            {
                myPen.Color = Color.FromArgb(
                   (rnd.Next(0, 255)),
                   (rnd.Next(0, 255)),
                   (rnd.Next(0, 255)));

                oGraphics.DrawEllipse(
                   myPen,
                   rnd.Next(0, this.Width),
                   rnd.Next(0, this.Height),
                   radius, radius);
            }
            oGraphics.Dispose();

            this.Invalidate();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void frmGraphics_Load(object sender, System.EventArgs e)
        {
            DrawingArea = new Bitmap(
                    this.ClientRectangle.Width,
                    this.ClientRectangle.Height,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            InitializeDrawingArea();
        }

        private void InitializeDrawingArea()
        {
            Graphics oGraphics;
            oGraphics = Graphics.FromImage(DrawingArea);

            myPen.Color = Color.AliceBlue;

            for (int x = 0; x < this.Width; x++)
            {
                oGraphics.DrawLine(myPen, x, 0, x, this.Height);
            }
            oGraphics.Dispose();

            this.Invalidate();
        }

        private void frmGraphics_Closed(object sender, System.EventArgs e)
        {
            DrawingArea.Dispose();
        }

        private void frmGraphics_Paint(object sender,
                                        System.Windows.Forms.PaintEventArgs e)
        {
            Graphics oGraphics;

            oGraphics = e.Graphics;

            oGraphics.DrawImage(DrawingArea,
                                 0, 0,
                                 DrawingArea.Width,
                                 DrawingArea.Height);
            oGraphics.Dispose();
        }
    }
}
