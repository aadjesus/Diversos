using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Test
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button3;
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(160, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show Window";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Hide Window";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(136, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(128, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(176, 40);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(88, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Animation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Alpha Blend (0-255)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Location = new System.Drawing.Point(160, 136);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Exit";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(272, 174);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FloatingWindow Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		InheritedFloatingWindow fw = new InheritedFloatingWindow();

		private void button1_Click(object sender, System.EventArgs e)
		{
			Olvio.Windows.Forms.AnimateMode anim = (Olvio.Windows.Forms.AnimateMode)Enum.Parse(typeof(Olvio.Windows.Forms.AnimateMode), this.comboBox1.Text, true);
			fw.ShowAnimate(fw.Location.X, fw.Location.Y, anim);
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			this.comboBox1.Items.AddRange(Enum.GetNames(typeof(Olvio.Windows.Forms.AnimateMode)));
			this.comboBox1.SelectedIndex = 0;
			this.comboBox1.MaxDropDownItems = 10;
			fw.Location = new Point(100, 100);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Olvio.Windows.Forms.AnimateMode anim = (Olvio.Windows.Forms.AnimateMode)Enum.Parse(typeof(Olvio.Windows.Forms.AnimateMode), this.comboBox1.Text, true);
			fw.HideAnimate(anim);
		}

		private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
		{
			fw.Alpha = (int)this.numericUpDown1.Value;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Application.ExitThread();
		}
	}

	public class InheritedFloatingWindow : Olvio.Windows.Forms.FloatingWindow
	{
		protected override void OnMouseEnter()
		{
			base.OnMouseEnter ();
			this.Text = "Mouse In";
		}

		protected override void OnMouseLeave()
		{
			base.OnMouseLeave ();
			this.Text = "Mouse Out";
		}


		protected override void PerformPaint(PaintEventArgs e)
		{
			base.PerformPaint (e);
			if (!base.Minimized)
			{
				Rectangle r = base.ClientRectangle;
				r.Y+=20;
				r.Height-=20;
				using (SolidBrush brush = new SolidBrush(SystemColors.ControlText))
				{
					using (StringFormat sf = new StringFormat(StringFormat.GenericTypographic))
					{
						sf.Alignment = StringAlignment.Center;
						sf.LineAlignment = StringAlignment.Center;
						e.Graphics.DrawString("This is a custom drawn text", Control.DefaultFont, brush, r, sf);
					}
				}
			}
		}

	}
}
