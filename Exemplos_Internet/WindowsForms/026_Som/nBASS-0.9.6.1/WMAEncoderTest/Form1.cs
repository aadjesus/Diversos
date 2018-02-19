using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using nBASS;

namespace WMAEncoderTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private WMAEncoder enc;
		private BASS bass;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private Stream stream;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Threading.Thread thread;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			bass = new BASS(-1, 44100, DeviceSetupFlags.Default, this.Handle);
			bass.Start();

			int[] rates = WMAEncoder.GetRates(44100, RateFlags.Default); 
			foreach (int rate in rates)
			{
				this.comboBox1.Items.Add(rate);
			}

		}

		private void Progress8()
		{
			try 
			{
				byte[] buffer = new byte[44100];
				int length = 0;
				do 
				{
					length = stream.GetData(buffer, buffer.Length);
					enc.Write(buffer, length);
					this.progressBar1.Value += length;
				} while (length > 0);
			}
			catch (BASSException ex)
			{
				if (ex.ErrorState == BASSException.Error.NOPLAY) enc.Close();
			}
		}

		private void Progress16()
		{
			try 
			{
				short[] buffer = new short[44100];
				int length = 0;
				do 
				{
					length = stream.GetData(buffer, buffer.Length);
					enc.Write(buffer, length);
					this.progressBar1.Value += length;
				} while (length > 0);
			}
			catch (BASSException)
			{
				enc.Close();
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			try 
			{
				enc.Close();
			} 
			catch (Exception){}
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
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.button3 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "mp3";
			this.openFileDialog1.Filter = "MP3|*.mp3";
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(72, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(280, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Source";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Destination";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox2.Location = new System.Drawing.Point(72, 32);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(120, 20);
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "";
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(8, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Open Source";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(208, 64);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "Encode";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(112, 64);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(88, 21);
			this.comboBox1.TabIndex = 6;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(8, 96);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(400, 23);
			this.progressBar1.TabIndex = 7;
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Location = new System.Drawing.Point(312, 64);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(96, 23);
			this.button3.TabIndex = 8;
			this.button3.Text = "Stop";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(208, 32);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 24);
			this.checkBox1.TabIndex = 9;
			this.checkBox1.Text = "Network";
			// 
			// textBox3
			// 
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox3.Location = new System.Drawing.Point(368, 34);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(40, 20);
			this.textBox3.TabIndex = 10;
			this.textBox3.Text = "42042";
			this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(336, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 23);
			this.label3.TabIndex = 11;
			this.label3.Text = "Port";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(280, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(16, 23);
			this.label4.TabIndex = 13;
			this.label4.Text = "#";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox4
			// 
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox4.Location = new System.Drawing.Point(304, 34);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(32, 20);
			this.textBox4.TabIndex = 12;
			this.textBox4.Text = "3";
			this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkBox2
			// 
			this.checkBox2.Checked = true;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox2.Location = new System.Drawing.Point(360, 8);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(56, 24);
			this.checkBox2.TabIndex = 14;
			this.checkBox2.Text = "16 bit";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 125);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																							 this.checkBox2,
																							 this.label4,
																							 this.textBox4,
																							 this.label3,
																							 this.textBox3,
																							 this.checkBox1,
																							 this.button3,
																							 this.progressBar1,
																							 this.comboBox1,
																							 this.button2,
																							 this.button1,
																							 this.label2,
																							 this.textBox2,
																							 this.label1,
																							 this.textBox1});
			this.Name = "Form1";
			this.Text = "WMAEncoderTest";
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			DialogResult result = this.openFileDialog1.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				this.textBox1.Text = this.openFileDialog1.FileName;
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.progressBar1.Value = 0;
				if (!this.checkBox1.Checked)
					enc = WMAEncoder.OpenEncoderFile(44100, EncoderFlags.Tags, 
						(int) this.comboBox1.SelectedItem, this.textBox2.Text);
				else enc = WMAEncoder.OpenEncoderNetwork(44100, 
						  EncoderFlags.Tags, 
						  (int) this.comboBox1.SelectedItem, 
						  Convert.ToInt32(this.textBox3.Text),
						  Convert.ToInt32(this.textBox4.Text));

				enc.SetTag(WMATag.Author, "leppie Radio restecpe");
				enc.SetTag(WMATag.Copyright,"copyleft");
				enc.SetTag(WMATag.Description,"madass radio show");
				enc.SetTag(WMATag.Title, "the lepShow");
				enc.SetTagDone();

				if (this.checkBox2.Checked)
				{
					stream = bass.LoadStream(false, this.textBox1.Text, 0, 0, StreamFlags.DecodeOnly);
					thread = new System.Threading.Thread( new System.Threading.ThreadStart(Progress16));
				}
				else 
				{
					stream = bass.LoadStream(false, this.textBox1.Text, 0, 0, StreamFlags.DecodeOnly | StreamFlags.EightBits);
					thread = new System.Threading.Thread( new System.Threading.ThreadStart(Progress8));
				}

				this.progressBar1.Maximum = (int)stream.Length;
				thread.Start();

			}
			catch (WMAException ex)
			{
				Console.WriteLine(ex.ErrorDescription);
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			thread.Abort();
			enc.Close();
		}
	}
}
