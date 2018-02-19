using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Testproperty
{
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Data.DataView dataView1;
		private System.Windows.Forms.Button button3;
		private Class1 c=new Class1();
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;		
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private Testproperty.myPropertyGrid PropertyGrid1;

		private System.ComponentModel.Container components = null;

		public Form1()
		{			
			InitializeComponent();
		}

		/// <summary>
		/// Pulire le risorse in uso.
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

		#region Codice generato da Progettazione Windows Form
		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataView1 = new System.Data.DataView();
			this.button3 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.PropertyGrid1 = new Testproperty.myPropertyGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataView1)).BeginInit();
			this.SuspendLayout();
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(240, 116);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(92, 24);
			this.button3.TabIndex = 4;
			this.button3.Text = "value";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(244, 28);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(148, 16);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "test";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(244, 52);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(156, 16);
			this.checkBox2.TabIndex = 6;
			this.checkBox2.Text = "test2";
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(244, 80);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(156, 12);
			this.checkBox3.TabIndex = 7;
			this.checkBox3.Text = "test3";
			this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(240, 144);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(168, 92);
			this.textBox1.TabIndex = 9;
			this.textBox1.Text = "textBox1";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(240, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 12);
			this.label1.TabIndex = 10;
			this.label1.Text = "Properties to show";
			// 
			// PropertyGrid1
			// 
			this.PropertyGrid1.CommandsVisibleIfAvailable = true;
			this.PropertyGrid1.LargeButtons = false;
			this.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.PropertyGrid1.Location = new System.Drawing.Point(4, 4);
			this.PropertyGrid1.Name = "PropertyGrid1";
			this.PropertyGrid1.Size = new System.Drawing.Size(228, 268);
			this.PropertyGrid1.TabIndex = 11;
			this.PropertyGrid1.Text = "myPropertyGrid1";
			this.PropertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.PropertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(436, 281);
			this.Controls.Add(this.PropertyGrid1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button3);
			this.Name = "Form1";
			this.Text = "Form1";
			
			((System.ComponentModel.ISupportInitialize)(this.dataView1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Il punto di ingresso principale dell'applicazione.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button2_Click(object sender, System.EventArgs e)
		{			
			PropertyGrid1.SelectedObject=c;			
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			textBox1.Text = "test : " + c.test.ToString()+"\r\n";
			textBox1.Text += "test2 : " + c.test2.ToString()+"\r\n";
			textBox1.Text += "test3 : " + c.test3.ToString()+"\r\n";
		}

		private void propertyGrid1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBox1.Checked==true)
			{
				c.PublicProperties.Add(new myProperty("test","Generali"));
			}
			else
			{
				c.PublicProperties.Remove("test");
			}
			PropertyGrid1.SelectedObject=c;
		}

		private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBox2.Checked==true)
			{
				c.PublicProperties.Add(new myProperty("test2","Generali"));
			}
			else
			{
				c.PublicProperties.Remove("test2");
			}
			PropertyGrid1.SelectedObject=c;
		}

		private void checkBox3_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBox3.Checked==true)
			{
				c.PublicProperties.Add(new myProperty("test3","Prova"));
			}
			else
			{
				c.PublicProperties.Remove("test3");
			}
			PropertyGrid1.SelectedObject=c;
		}
	}
}
