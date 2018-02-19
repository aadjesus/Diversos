using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using nBASS;

namespace StreamTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.ComponentModel.IContainer components;

		private BASS bass;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.ProgressBar progressBar2;
		private System.Windows.Forms.TrackBar trackBar7;
		private System.Windows.Forms.TrackBar trackBar8;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.Timer timer3;
		private System.Windows.Forms.Timer timer4;
		private System.Windows.Forms.Timer timer5;
		private System.Windows.Forms.Timer timer6;
		private System.Windows.Forms.Timer timer7;
		private System.Windows.Forms.TrackBar trackBar4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.PictureBox pictureBox2;
		private Stream stream;
		private Bitmap bit1;
		private Bitmap bit2;

		public Form1()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint|ControlStyles.DoubleBuffer, true);
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			bit1 = new Bitmap( pictureBox1.Width, pictureBox1.Height);
			bit2 = new Bitmap( pictureBox2.Width, pictureBox2.Height);
			
			bass = new BASS(-1, 44100, DeviceSetupFlags.Latency |	DeviceSetupFlags.LeaveVolume
				| DeviceSetupFlags.ThreeDee, this.Handle);
			bass.Start();
			bass.MasterVolume = trackBar7.Value;
			bass.MusicVolume = trackBar8.Value;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			bass.Stop();
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
			this.components = new System.ComponentModel.Container();
			this.button8 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.trackBar7 = new System.Windows.Forms.TrackBar();
			this.trackBar8 = new System.Windows.Forms.TrackBar();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.timer3 = new System.Windows.Forms.Timer(this.components);
			this.timer4 = new System.Windows.Forms.Timer(this.components);
			this.timer5 = new System.Windows.Forms.Timer(this.components);
			this.timer6 = new System.Windows.Forms.Timer(this.components);
			this.timer7 = new System.Windows.Forms.Timer(this.components);
			this.trackBar4 = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.trackBar7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// button8
			// 
			this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button8.Location = new System.Drawing.Point(85, 29);
			this.button8.Name = "button8";
			this.button8.TabIndex = 16;
			this.button8.Text = "Stop";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button7
			// 
			this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button7.Location = new System.Drawing.Point(164, 5);
			this.button7.Name = "button7";
			this.button7.TabIndex = 15;
			this.button7.Text = "Pause";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button6
			// 
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button6.Location = new System.Drawing.Point(85, 5);
			this.button6.Name = "button6";
			this.button6.TabIndex = 14;
			this.button6.Text = "Resume";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button4
			// 
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.Location = new System.Drawing.Point(164, 29);
			this.button4.Name = "button4";
			this.button4.TabIndex = 12;
			this.button4.Text = "Play";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(4, 29);
			this.button2.Name = "button2";
			this.button2.TabIndex = 10;
			this.button2.Text = "Visual";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(4, 5);
			this.button1.Name = "button1";
			this.button1.TabIndex = 9;
			this.button1.Text = "Open Stream";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(368, 360);
			this.progressBar1.Maximum = 128;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(264, 24);
			this.progressBar1.Step = 4;
			this.progressBar1.TabIndex = 19;
			// 
			// progressBar2
			// 
			this.progressBar2.Location = new System.Drawing.Point(368, 392);
			this.progressBar2.Maximum = 128;
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(264, 24);
			this.progressBar2.Step = 4;
			this.progressBar2.TabIndex = 20;
			// 
			// trackBar7
			// 
			this.trackBar7.LargeChange = 10;
			this.trackBar7.Location = new System.Drawing.Point(56, 304);
			this.trackBar7.Maximum = 100;
			this.trackBar7.Name = "trackBar7";
			this.trackBar7.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar7.Size = new System.Drawing.Size(42, 104);
			this.trackBar7.SmallChange = 5;
			this.trackBar7.TabIndex = 35;
			this.trackBar7.TickFrequency = 10;
			this.trackBar7.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar7.Value = 50;
			this.trackBar7.Scroll += new System.EventHandler(this.trackBar7_Scroll);
			// 
			// trackBar8
			// 
			this.trackBar8.LargeChange = 10;
			this.trackBar8.Location = new System.Drawing.Point(8, 304);
			this.trackBar8.Maximum = 100;
			this.trackBar8.Name = "trackBar8";
			this.trackBar8.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar8.Size = new System.Drawing.Size(42, 104);
			this.trackBar8.SmallChange = 5;
			this.trackBar8.TabIndex = 36;
			this.trackBar8.TickFrequency = 10;
			this.trackBar8.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar8.Value = 50;
			this.trackBar8.Scroll += new System.EventHandler(this.trackBar8_Scroll);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Black;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(8, 56);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(312, 248);
			this.pictureBox1.TabIndex = 37;
			this.pictureBox1.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Interval = 20;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// timer2
			// 
			this.timer2.Interval = 20;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// timer3
			// 
			this.timer3.Interval = 20;
			this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
			// 
			// timer4
			// 
			this.timer4.Interval = 20;
			this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
			// 
			// timer5
			// 
			this.timer5.Interval = 20;
			this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
			// 
			// timer6
			// 
			this.timer6.Interval = 20;
			this.timer6.Tick += new System.EventHandler(this.timer6_Tick);
			// 
			// timer7
			// 
			this.timer7.Interval = 20;
			this.timer7.Tick += new System.EventHandler(this.timer7_Tick);
			// 
			// trackBar4
			// 
			this.trackBar4.BackColor = System.Drawing.SystemColors.Control;
			this.trackBar4.Location = new System.Drawing.Point(104, 312);
			this.trackBar4.Maximum = 240;
			this.trackBar4.Name = "trackBar4";
			this.trackBar4.Size = new System.Drawing.Size(528, 42);
			this.trackBar4.TabIndex = 38;
			this.trackBar4.TickFrequency = 30;
			this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll_1);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 405);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 39;
			this.label1.Text = "MAIN";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(49, 405);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 40;
			this.label2.Text = "STREAM";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(112, 360);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 23);
			this.label3.TabIndex = 41;
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(112, 392);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120, 23);
			this.label4.TabIndex = 42;
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(240, 360);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 23);
			this.label5.TabIndex = 43;
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(232, 384);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Minimum = -100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(136, 42);
			this.trackBar1.TabIndex = 44;
			this.trackBar1.TickFrequency = 20;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// label6
			// 
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(248, 29);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(376, 23);
			this.label6.TabIndex = 45;
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(248, 5);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(376, 23);
			this.label7.TabIndex = 46;
			this.label7.Text = "nBASS Streams/Visuals Test";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Black;
			this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox2.Location = new System.Drawing.Point(320, 56);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(312, 248);
			this.pictureBox2.TabIndex = 47;
			this.pictureBox2.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(635, 423);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																							 this.pictureBox2,
																							 this.label7,
																							 this.label6,
																							 this.trackBar1,
																							 this.label5,
																							 this.label4,
																							 this.label3,
																							 this.label2,
																							 this.label1,
																							 this.trackBar4,
																							 this.pictureBox1,
																							 this.trackBar8,
																							 this.trackBar7,
																							 this.progressBar2,
																							 this.progressBar1,
																							 this.button8,
																							 this.button7,
																							 this.button6,
																							 this.button4,
																							 this.button2,
																							 this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Stream Test";
			((System.ComponentModel.ISupportInitialize)(this.trackBar7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
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
				if (stream != null && stream.ActivityState == State.Playing) 
				{
					stream.Stop();
					stream.Progress -= new BASSProgessHandler( Progress);
					stream.End -= new EventHandler( SongEnd);
				}
				label6.Text = openFileDialog1.FileName;
				//label6.Text = "http://server/test.mp3";
				stream = bass.LoadStream(false, label6.Text, 0, 0, 0);
				//stream = bass.CreateStreamFromURL(label6.Text, 0, StreamFlags.Default, "");
				trackBar4.Value = 0;
				trackBar4.Maximum = Convert.ToInt32(stream.Bytes2Seconds(stream.Length));
				label4.Text = TimeSpan.FromSeconds(stream.Bytes2Seconds(stream.Length)).ToString();
				stream.Progress += new BASSProgessHandler( Progress);
				stream.End += new EventHandler( SongEnd);
			}
		}

		private void SongEnd(object sender, EventArgs e)
		{
			MessageBox.Show(this, "Song has ended");
			ChangeTimer(0);
		}

		private void Progress(ChannelBase channel)
		{
			Stream stream2 = (Stream) channel;
			this.progressBar1.Value = stream2.LeftLevel;
			this.progressBar2.Value = stream2.RightLevel;
			this.label5.Text = (bass.CPUUsage/100).ToString("P2");
			label3.Text = TimeSpan.FromSeconds(stream.Bytes2Seconds(stream.Position)).ToString();
			this.trackBar4.Value = Convert.ToInt32(stream.Bytes2Seconds(stream.Position));
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			ChangeTimer(visual);
			stream.Play(true, StreamPlayFlags.Default);
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			ChangeTimer(0);
			stream.Stop();
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			stream.Pause();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			stream.Resume();
		}

		private void trackBar8_Scroll(object sender, System.EventArgs e)
		{
			bass.StreamVolume = ((TrackBar) sender).Value;
		}

		private void trackBar7_Scroll(object sender, System.EventArgs e)
		{
			bass.MasterVolume = ((TrackBar) sender).Value;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			float[] buffer = new float[2048];
			Pen pen = new Pen(Color.Purple, 2);

			Graphics graph1 = Graphics.FromImage(bit1);
			graph1.Clear(Color.Black);

			Graphics graph2 = Graphics.FromImage(bit2);
			graph2.Clear(Color.Black);

			int y = 0;
			if (stream.ActivityState == State.Playing)
			{
				stream.GetData(buffer, ChannelDataFlags.SFFT2048);

				for (int x = 0; x < pictureBox1.Width; x+=2)
				{
					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox1.Height - 4);
					if (y > pictureBox1.Height)
						y = pictureBox1.Height;
					graph1.DrawLine(pen, x, pictureBox1.Height, x, pictureBox1.Height - y);

					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox2.Height - 4);
					if (y > pictureBox2.Height)
						y = pictureBox2.Height;
					graph2.DrawLine(pen, pictureBox2.Width - x, pictureBox2.Height, pictureBox2.Width - x, pictureBox2.Height - y);
				}
				pictureBox1.Image = bit1;
				pictureBox2.Image = bit2;
			}
		}

		int visual = 1;

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (visual > 7) visual = 0;
			ChangeTimer(visual++);
		}

		void ChangeTimer(int number)
		{
			timer1.Stop();
			timer2.Stop();
			timer3.Stop();
			timer4.Stop();
			timer5.Stop();
			timer6.Stop();
			timer7.Stop();

			switch(number)
			{
				case 1:
					timer1.Start();
					break;
				case 2:
					timer2.Start();
					break;
				case 3:
					timer3.Start();
					break;
				case 4:
					timer4.Start();
					break;
				case 5:
					timer5.Start();
					break;
				case 6:
					timer6.Start();
					break;
				case 7:
					timer7.Start();
					break;
				default:
					pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
					pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
					break;
			}
		}

		private void timer2_Tick(object sender, System.EventArgs e)
		{
			float[] buffer = new float[2048];
			Pen pen = new Pen(Color.Red, 2);

			//Graphics graph1 = Graphics.FromHwnd(pictureBox1.Handle);
			Graphics graph1 = Graphics.FromImage(bit1);
			graph1.Clear(Color.Black);

			//Graphics graph2 = Graphics.FromHwnd(pictureBox2.Handle);
			Graphics graph2 = Graphics.FromImage(bit2);
			graph2.Clear(Color.Black);

			int y = 0;
			if (stream.ActivityState == State.Playing)
			{
				stream.GetData(buffer, ChannelDataFlags.SFFT2048);

				for (int x = 0; x < pictureBox1.Width; x+=4)
				{
					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox1.Height - 4);
					if (y > pictureBox1.Height)
						y = pictureBox1.Height;
					graph1.DrawLine(pen, x, pictureBox1.Height, x, pictureBox1.Height - y);

					y = (int)(Math.Sqrt(buffer[x + 1]) * 3 * pictureBox2.Height - 4);
					if (y > pictureBox2.Height)
						y = pictureBox2.Height;
					graph2.DrawLine(pen, pictureBox2.Width - x, pictureBox2.Height, pictureBox2.Width - x, pictureBox2.Height - y);	
				}
				pictureBox1.Image = bit1;
				pictureBox2.Image = bit2;
			}
		}

		private void timer3_Tick(object sender, System.EventArgs e)
		{
			float[] buffer = new float[2048];
			Pen pen = new Pen(Color.Blue, 2);
			Pen pen2 = new Pen(Color.PaleVioletRed, 2);

			Graphics graph1 = Graphics.FromImage(bit1);
			graph1.Clear(Color.Black);

			Graphics graph2 = Graphics.FromImage(bit2);
			graph2.Clear(Color.Black);

			int y = 0;
			if (stream.ActivityState == State.Playing)
			{
				stream.GetData(buffer, ChannelDataFlags.SFFT2048);

				for (int x = 0; x < pictureBox1.Width; x+=4)
				{
					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox1.Height - 4);
					if (y > pictureBox1.Height)
						y = pictureBox1.Height;
					graph1.DrawEllipse(pen2, x, pictureBox1.Height - y,x, pictureBox1.Height);
					graph1.DrawEllipse(pen, x, pictureBox1.Height - y,x + 5, pictureBox1.Height);

					y = (int)(Math.Sqrt(buffer[x + 1]) * 3 * pictureBox2.Height - 4);
					if (y > pictureBox2.Height)
						y = pictureBox2.Height;
					graph2.DrawEllipse(pen2, pictureBox2.Width - x, pictureBox2.Height - y,-(x), pictureBox2.Height);
					graph2.DrawEllipse(pen, pictureBox2.Width - x, pictureBox2.Height - y,-(x + 5), pictureBox2.Height);
				}
				pictureBox1.Image = bit1;
				pictureBox2.Image = bit2;
			}
		}

		private void timer4_Tick(object sender, System.EventArgs e)
		{
			float[] buffer = new float[2048];
			Pen pen = new Pen(Color.Silver, 2);

			Graphics graph1 = Graphics.FromImage(bit1);
			graph1.Clear(Color.Black);

			Graphics graph2 = Graphics.FromImage(bit2);
			graph2.Clear(Color.Black);

			int y = 0;
			if (stream.ActivityState == State.Playing)
			{
				stream.GetData(buffer, ChannelDataFlags.SFFT2048);

				for (int x = 0; x < pictureBox1.Width; x+=4)
				{
					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox1.Height - 4);
					if (y > pictureBox1.Height)
						y = pictureBox1.Height;
					graph1.DrawEllipse(pen, x, pictureBox1.Height - y,5, pictureBox1.Height);

					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox2.Height - 4);
					if (y > pictureBox2.Height)
						y = pictureBox2.Height;
					graph2.DrawEllipse(pen,pictureBox2.Width - x, pictureBox2.Height - y,-5, pictureBox2.Height);
				}
				pictureBox1.Image = bit1;
				pictureBox2.Image = bit2;
			}
		}

		private void timer5_Tick(object sender, System.EventArgs e)
		{
			float[] buffer = new float[2048];
			Pen pen = new Pen(Color.Green, 2);

			Graphics graph1 = Graphics.FromImage(bit1);
			graph1.Clear(Color.Black);

			Graphics graph2 = Graphics.FromImage(bit2);
			graph2.Clear(Color.Black);

			int y = 0;
			if (stream.ActivityState == State.Playing)
			{
				stream.GetData(buffer, ChannelDataFlags.SFFT2048);

				for (int x = 0; x < pictureBox1.Width; x+=2)
				{
					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox1.Height - 4);
					if (y > pictureBox1.Height)
						y = pictureBox1.Height;
					graph1.DrawEllipse(pen, x, pictureBox1.Height - y, 5, 10);

					y = (int)(Math.Sqrt(buffer[x + 1]) * 3 * pictureBox2.Height - 4);
					if (y > pictureBox2.Height)
						y = pictureBox2.Height;
					graph2.DrawEllipse(pen, pictureBox2.Width - x, pictureBox2.Height - y, -5, 10);
				}
				pictureBox1.Image = bit1;
				pictureBox2.Image = bit2;
			}
		}

		private void timer6_Tick(object sender, System.EventArgs e)
		{
			float[] buffer = new float[2048];
			Pen pen2 = new Pen(Color.Yellow, 2);

			Graphics graph1 = Graphics.FromImage(bit1);
			graph1.Clear(Color.Black);

			Graphics graph2 = Graphics.FromImage(bit2);
			graph2.Clear(Color.Black);

			int y = 0;
			if (stream.ActivityState == State.Playing)
			{
				stream.GetData(buffer, ChannelDataFlags.SFFT2048);

				for (int x = 0; x < pictureBox1.Width; x+=4)
				{
					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox1.Height - 4);
					if (y > pictureBox1.Height)
						y = pictureBox1.Height;
					graph1.DrawLine(pen2, x + 2, pictureBox1.Height - y, x + 2, pictureBox1.Height - y - 2);

					y = (int)(Math.Sqrt(buffer[x+1]) * 3 * pictureBox2.Height - 4);
					if (y > pictureBox2.Height)
						y = pictureBox2.Height;
					graph2.DrawLine(pen2, pictureBox2.Width - (x + 2), pictureBox2.Height - y, pictureBox2.Width - (x + 2), pictureBox2.Height - y - 2);
				}
				pictureBox1.Image = bit1;
				pictureBox2.Image = bit2;
			}
		}

		private void timer7_Tick(object sender, System.EventArgs e)
		{
			float[] buffer = new float[2048];

			Pen pen = new Pen(Color.Red, 2);
			Pen pen2 = new Pen(Color.Yellow, 2);

			Graphics graph1 = Graphics.FromImage(bit1);
			graph1.Clear(Color.Black);

			Graphics graph2 = Graphics.FromImage(bit2);
			graph2.Clear(Color.Black);

			int y = 0;
			if (stream.ActivityState == State.Playing)
			{
				stream.GetData(buffer, ChannelDataFlags.SFFT2048);

				for (int x = 0; x < pictureBox1.Width; x+=4)
				{
					y = (int)(Math.Sqrt(buffer[x]) * 3 * pictureBox1.Height - 4);
					if (y > pictureBox1.Height)
						y = pictureBox1.Height;
					graph1.DrawLine(pen, x + 2, pictureBox1.Height, x + 2, pictureBox1.Height - y);
					graph1.DrawLine(pen2, x + 2, pictureBox1.Height - y, x + 2, pictureBox1.Height - y - 2);
					y = (int)(Math.Sqrt(buffer[x + 1]) * 3 * pictureBox2.Height - 4);
					if (y > pictureBox2.Height)
						y = pictureBox2.Height;
					graph2.DrawLine(pen,pictureBox2.Width - (x + 2), pictureBox2.Height, pictureBox2.Width - (x + 2), pictureBox2.Height - y);
					graph2.DrawLine(pen2,pictureBox2.Width - (x + 2), pictureBox2.Height - y, pictureBox2.Width - (x + 2), pictureBox2.Height - y - 2);
				}
				pictureBox1.Image = bit1;
				pictureBox2.Image = bit2;
			}
		}

		private void trackBar4_Scroll_1(object sender, System.EventArgs e)
		{
			stream.Position = stream.Seconds2Bytes(((TrackBar) sender).Value);
		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			ChannelAttributes chanatt = stream.Attributes;
			chanatt.panning = ((TrackBar)sender).Value;
			stream.Attributes = chanatt;
		}


	}
}
