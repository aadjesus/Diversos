using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Resources;

namespace Test
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox RowNum;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.TextBox textBox3;
		private LJ.Controls.Grid grid1;
		private System.Windows.Forms.Button btn_Fill;
		private System.Windows.Forms.Button button1;
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
			LJ.Controls.Column column1 = new LJ.Controls.Column();
			LJ.Controls.Column column2 = new LJ.Controls.Column();
			LJ.Controls.Column column3 = new LJ.Controls.Column();
			LJ.Controls.Column column4 = new LJ.Controls.Column();
			LJ.Controls.Column column5 = new LJ.Controls.Column();
			LJ.Controls.Column column6 = new LJ.Controls.Column();
			this.button2 = new System.Windows.Forms.Button();
			this.RowNum = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button9 = new System.Windows.Forms.Button();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.grid1 = new LJ.Controls.Grid();
			this.btn_Fill = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(168, 328);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(120, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "MarkRow 5";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// RowNum
			// 
			this.RowNum.Location = new System.Drawing.Point(32, 328);
			this.RowNum.Name = "RowNum";
			this.RowNum.Size = new System.Drawing.Size(120, 22);
			this.RowNum.TabIndex = 4;
			this.RowNum.Text = "1";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(32, 368);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(88, 24);
			this.button3.TabIndex = 5;
			this.button3.Text = "Append Col";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(136, 368);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(88, 24);
			this.button4.TabIndex = 8;
			this.button4.Text = "AppendRow";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(496, 288);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(112, 24);
			this.button5.TabIndex = 9;
			this.button5.Text = "ClearAll";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(248, 368);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(120, 24);
			this.button6.TabIndex = 10;
			this.button6.Text = "Append From DB";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(304, 328);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(88, 24);
			this.button7.TabIndex = 13;
			this.button7.Text = "Insert MarkRow";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(392, 368);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(88, 24);
			this.button8.TabIndex = 14;
			this.button8.Text = "Delete";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(32, 288);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(72, 22);
			this.textBox1.TabIndex = 15;
			this.textBox1.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(112, 288);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(64, 22);
			this.textBox2.TabIndex = 16;
			this.textBox2.Text = "";
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(184, 288);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(104, 24);
			this.button9.TabIndex = 17;
			this.button9.Text = "GetCell";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(296, 288);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(104, 22);
			this.textBox3.TabIndex = 18;
			this.textBox3.Text = "";
			// 
			// grid1
			// 
			this.grid1.BackColor = System.Drawing.SystemColors.Window;
			column1.Image = null;
			column1.Left = 0;
			column1.Text = "Col1";
			column1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			column1.Visible = true;
			column2.Image = null;
			column2.Left = 0;
			column2.Text = "Col2";
			column2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			column2.Visible = true;
			column3.Image = null;
			column3.Left = 0;
			column3.Text = "Col3";
			column3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			column3.Visible = true;
			column4.Image = null;
			column4.Left = 0;
			column4.Text = "Col4";
			column4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			column4.Visible = true;
			column5.Image = null;
			column5.Left = 0;
			column5.Text = "Col5";
			column5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			column5.Visible = true;
			column6.Image = null;
			column6.Left = 0;
			column6.Text = "Col6";
			column6.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			column6.Visible = true;
			this.grid1.Columns.AddRange(new LJ.Controls.Column[] {
																	 column1,
																	 column2,
																	 column3,
																	 column4,
																	 column5,
																	 column6});
			this.grid1.HasGridLine = true;
			this.grid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.grid1.Location = new System.Drawing.Point(24, 16);
			this.grid1.Name = "grid1";
			this.grid1.ReadOnly = false;
			this.grid1.Size = new System.Drawing.Size(592, 232);
			this.grid1.TabIndex = 19;
			this.grid1.Text = "grid1";
			// 
			// btn_Fill
			// 
			this.btn_Fill.Location = new System.Drawing.Point(496, 328);
			this.btn_Fill.Name = "btn_Fill";
			this.btn_Fill.Size = new System.Drawing.Size(112, 24);
			this.btn_Fill.TabIndex = 20;
			this.btn_Fill.Text = "Fill";
			this.btn_Fill.Click += new System.EventHandler(this.btn_Fill_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(496, 368);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 24);
			this.button1.TabIndex = 21;
			this.button1.Text = "Add multi-line";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(632, 416);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btn_Fill);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.RowNum);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
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

		private void Form1_Load(object sender, System.EventArgs e)
		{
			/*for(int i=0;i<1000;i++)
			{
				string[] a=new string[10];
				for(int j=0;j<10;j++)
					a[j]="Row:"+i.ToString()+"Field:"+j.ToString();
				grid1.AppendRow(a); 
			}*/
			for(int i=0;i<10;i++)
				grid1.AppendRow();
		}

		private void grid1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			int index=Convert.ToInt32(RowNum.Text);
			grid1.MarkRow(index);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			LJ.Controls.Column cl=new LJ.Controls.Column();
			cl.Text="New Col";
			grid1.Columns.Add(cl);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
		    //grid1.AppendRow();
			/*
			string[] tmp=new string[5];
			tmp[0]="123\t234\t";
			tmp[1]="123";
			tmp[2]="linjie\txiaotan\thuangqian\txiaowu\t";
			tmp[3]="abc";
			tmp[4]="1\t2\t3\t4\t";
			grid1.AppendRow(tmp);
			*/
				
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			grid1.ClearAll();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			System.Data.SqlClient.SqlConnection cnt=new System.Data.SqlClient.SqlConnection();
			System.Data.SqlClient.SqlDataAdapter sa=new System.Data.SqlClient.SqlDataAdapter();
		
			cnt.ConnectionString="Data Source=192.168.100.1;Initial Catalog=CFP;User id=sa;Password=15033;";
 
			cnt.Open();
			System.Data.DataSet ds=new System.Data.DataSet();


			sa.SelectCommand=new System.Data.SqlClient.SqlCommand("select Top 100 * from FAFR_Main where batchno ='0521'",cnt);

			sa.Fill(ds);

			grid1.Columns.Clear();

            for(int k=0;k<ds.Tables[0].Columns.Count;k++)
			{
				LJ.Controls.Column cl=new LJ.Controls.Column();
				cl.Text = ds.Tables[0].Columns[k].ColumnName;

				grid1.Columns.Add(cl);
			}

			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				string[] tmp=new string[ds.Tables[0].Columns.Count];
				for(int j=0;j<ds.Tables[0].Columns.Count;j++)
				{
					tmp[j]=ds.Tables[0].Rows[i][j].ToString();
				}
				grid1.AppendRow(tmp);
			}
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			int index=grid1.GetMarkRow();
			string[] val=new String[2];
			val[0]="abc";
			val[1]="bce";
			grid1.InsertRow(index,val);
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			int index=grid1.GetMarkRow();
			grid1.RemoveRow(index);
		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			int row=Convert.ToInt32(textBox1.Text);
			int col=Convert.ToInt32(textBox2.Text);

			string val=grid1.GetCell(row,col);
			textBox3.Text=val;
		}

		private void btn_Fill_Click(object sender, System.EventArgs e)
		{
			grid1.ClearAll();
			for(int i=0;i<50;i++)
			{
				string[] tmp=new string[7];
				for(int j=0;j<7;j++)
				{
					tmp[j]="Row:" + i.ToString() + " Col:" + j.ToString();
				}
				grid1.AppendRow(tmp);
			}
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			string[] tmp=new string[4];
			tmp[0]="line 1";
			tmp[1]="line 2";
			tmp[2]="line 3";
			tmp[3]="line 4";

			grid1.SetCell(1,1,tmp);

			string[] tmp1=new string[3];
			tmp1[0]="line 1";
			tmp1[1]="line 2";
			tmp1[2]="line 3";

			grid1.SetCell(1,2,tmp1);
		}
	}
}
