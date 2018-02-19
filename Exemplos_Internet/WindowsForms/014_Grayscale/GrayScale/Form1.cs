using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace GrayScale
{
    public class Form1 : System.Windows.Forms.Form
    {
        OpenFileDialog open = new OpenFileDialog();

        private System.Windows.Forms.PictureBox pb_color;
        private System.Windows.Forms.Button btn_convert;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.PictureBox pb_grayscale;
        private Button button1;
        private Button button2;
        private Button button3;
        private ListBox listBox1;
        private Button button4;
        private Button button5;
        private Button button6;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb_color = new System.Windows.Forms.PictureBox();
            this.pb_grayscale = new System.Windows.Forms.PictureBox();
            this.btn_convert = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_grayscale)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_color
            // 
            this.pb_color.Location = new System.Drawing.Point(0, 29);
            this.pb_color.Name = "pb_color";
            this.pb_color.Size = new System.Drawing.Size(232, 232);
            this.pb_color.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_color.TabIndex = 0;
            this.pb_color.TabStop = false;
            // 
            // pb_grayscale
            // 
            this.pb_grayscale.Location = new System.Drawing.Point(319, 29);
            this.pb_grayscale.Name = "pb_grayscale";
            this.pb_grayscale.Size = new System.Drawing.Size(248, 232);
            this.pb_grayscale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_grayscale.TabIndex = 1;
            this.pb_grayscale.TabStop = false;
            // 
            // btn_convert
            // 
            this.btn_convert.ForeColor = System.Drawing.Color.Black;
            this.btn_convert.Location = new System.Drawing.Point(238, 29);
            this.btn_convert.Name = "btn_convert";
            this.btn_convert.Size = new System.Drawing.Size(75, 56);
            this.btn_convert.TabIndex = 2;
            this.btn_convert.Text = "Convert";
            this.btn_convert.Click += new System.EventHandler(this.btn_convert_Click);
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(0, 0);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(232, 23);
            this.btn_load.TabIndex = 3;
            this.btn_load.Text = "Load Image";
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(312, 0);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(248, 23);
            this.btn_save.TabIndex = 4;
            this.btn_save.Text = "Save Image";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 296);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(319, 267);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(400, 267);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(168, 147);
            this.listBox1.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(24, 325);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(24, 354);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(24, 391);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 11;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(580, 423);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_convert);
            this.Controls.Add(this.pb_grayscale);
            this.Controls.Add(this.pb_color);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pb_color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_grayscale)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void btn_load_Click(object sender, System.EventArgs e)
        {
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pb_color.Image = new Bitmap(open.FileName);
                btn_convert.Enabled = true;
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            btn_convert.Enabled = false;
            btn_save.Enabled = false;
        }

        private void btn_convert_Click(object sender, System.EventArgs e)
        {
            Bitmap grays = (Bitmap)pb_color.Image;
            int width = grays.Size.Width;
            int height = grays.Size.Height;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Color col;
                    col = grays.GetPixel(i, j);

                    grays.SetPixel(i, j, Color.FromArgb((col.R + col.G + col.B) / 3, (col.R + col.G + col.B) / 3, (col.R + col.G + col.B) / 3));
                }
            }

            pb_grayscale.Image = grays;
            btn_save.Enabled = true;
        }

        private void btn_save_Click(object sender, System.EventArgs e)
        {
            Bitmap b = (Bitmap)pb_grayscale.Image;
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (save.ShowDialog() == DialogResult.OK)
            {
                b.Save(save.FileName);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form1 = new Form();
            form1.Load += new EventHandler(form1_Load);
            form1.Paint += new PaintEventHandler(form1_Paint);
            form1.WindowState = FormWindowState.Maximized;
            form1.ShowDialog();
        }

        private void form1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(open.FileName);
            Color c;

            e.Graphics.DrawImage(bmp, 10, 30);

            for (int x = 0; x < bmp.Width - 1; x++)
            {
                for (int y = 0; y < bmp.Height - 1; y++)
                {
                    c = bmp.GetPixel(x, y);
                    c = Color.FromArgb(c.ToArgb() + 100);
                    bmp.SetPixel(x, y, c);
                }
            }
            e.Graphics.DrawImage(bmp, 150, 30);
        }

        private void form1_Load(object sender, EventArgs e)
        {
            ColorPalette cp;
            String s;
            Bitmap bmp = new Bitmap(open.FileName);

            cp = bmp.Palette;

            foreach (Color c in cp.Entries)
            {
                s = c.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form2 = new Form();
            form2.Load += new EventHandler(form2_Load);
            form2.Paint += new PaintEventHandler(form2_Paint);
            form2.WindowState = FormWindowState.Maximized;
            form2.ShowDialog();

        }

        private void form2_Paint(object sender, PaintEventArgs e)
        {
            Bitmap BMP = new Bitmap(open.FileName);
            Point Pt = new Point(20, 20);

            //   BMP.SetPixel(15,20,Color.Black);
            BMP.MakeTransparent(BMP.GetPixel(15, 25));

            e.Graphics.DrawImage(BMP, Pt);
            e.Graphics.DrawLine(new Pen(Brushes.Red, 30), 60, 60, 200, 60);
        }

        private void form2_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Image sample = new Bitmap(open.FileName);
            MemoryStream buf = new MemoryStream();
            sample.Save(buf, ImageFormat.Bmp);
            byte[] currentImage = buf.GetBuffer();

            int[] stats = new int[3];
            for (int i = 0; i < currentImage.Length; )
            {
                for (int j = 0; j < 3; j++)
                {
                    stats[j] += currentImage[i];
                    ++i;
                }
            }

            listBox1.Items.Clear();
            listBox1.Items.Add("Blue: " + stats[0]);
            listBox1.Items.Add("Green: " + stats[1]);
            listBox1.Items.Add("Red: " + stats[2]);
            if ((stats[0] > stats[1]) && (stats[0] > stats[2]))
                listBox1.Items.Add("This is a cold picture.");
            if ((stats[1] > stats[0]) && (stats[1] > stats[2]))
                listBox1.Items.Add("This is a summer picture.");
            if ((stats[2] > stats[0]) && (stats[2] > stats[1]))
                listBox1.Items.Add("This is a fiery picture.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap myBitmap = new Bitmap(open.FileName);
            Graphics g = Graphics.FromImage(myBitmap);
            g.DrawString("My\nText", new Font("Tahoma", 40), Brushes.White, new PointF(0, 0));

            Pen blackPen = new Pen(Color.Black, 3);
            float x = 0.0F;
            float y = 0.0F;
            float width = 200.0F;
            float height = 100.0F;
            g.DrawEllipse(blackPen, x, y, width, height);

            pb_color.Image = myBitmap;

            SolidBrush solidColorBrush = new SolidBrush(Color.Red);
            Pen coloredPen = new Pen(solidColorBrush);

            g.DrawPie(coloredPen, 240, 30, 75, 100, 0, 270);


            Pen thickRedPen = new Pen(Color.Red, 10);
            Rectangle drawArea2 = new Rectangle(200, 200, 50, 10);
            Rectangle drawArea3 = new Rectangle(190, 210, 70, 10);
            Rectangle drawArea4 = new Rectangle(180, 220, 90, 10);
            //Rectangle drawArea2 = new Rectangle(80, 30, 65, 100);            
            
            g.DrawRectangle(thickRedPen, drawArea2);
            g.DrawRectangle(thickRedPen, drawArea3);
            g.DrawRectangle(thickRedPen, drawArea4);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form2 = new Form3();
            form2.ShowDialog();
        }

    }

}
