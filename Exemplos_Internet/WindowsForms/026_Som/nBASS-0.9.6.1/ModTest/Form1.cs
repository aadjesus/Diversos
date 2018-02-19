using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using nBASS;

namespace ModTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private BASS bass;
		private System.Windows.Forms.PropertyGrid propertyGrid2;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.ProgressBar progressBar2;
		private System.Windows.Forms.TrackBar trackBar3;
		private System.Windows.Forms.TrackBar trackBar2;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.TrackBar trackBar4;
		private System.Windows.Forms.TrackBar trackBar5;
		private System.Windows.Forms.TrackBar trackBar6;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.Button button14;
		private System.Windows.Forms.TrackBar trackBar7;
		private System.Windows.Forms.TrackBar trackBar8;
		private Music music;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			bass = new BASS(-1, 44100, DeviceSetupFlags.Latency |	DeviceSetupFlags.LeaveVolume
				| DeviceSetupFlags.ThreeDee, this.Handle);
			bass.Start();
			bass.MasterVolume = trackBar7.Value;
			bass.MusicVolume = trackBar8.Value;

			this.propertyGrid2.SelectedObject = bass;
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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.button8 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.trackBar3 = new System.Windows.Forms.TrackBar();
			this.trackBar2 = new System.Windows.Forms.TrackBar();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.trackBar4 = new System.Windows.Forms.TrackBar();
			this.trackBar5 = new System.Windows.Forms.TrackBar();
			this.trackBar6 = new System.Windows.Forms.TrackBar();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button12 = new System.Windows.Forms.Button();
			this.button13 = new System.Windows.Forms.Button();
			this.button14 = new System.Windows.Forms.Button();
			this.trackBar7 = new System.Windows.Forms.TrackBar();
			this.trackBar8 = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar8)).BeginInit();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(4, 56);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(310, 248);
			this.propertyGrid1.TabIndex = 17;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ToolbarVisible = false;
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
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
			this.button2.Text = "Close";
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(4, 5);
			this.button1.Name = "button1";
			this.button1.TabIndex = 9;
			this.button1.Text = "Open Mod";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// propertyGrid2
			// 
			this.propertyGrid2.CommandsVisibleIfAvailable = true;
			this.propertyGrid2.HelpVisible = false;
			this.propertyGrid2.LargeButtons = false;
			this.propertyGrid2.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid2.Location = new System.Drawing.Point(318, 56);
			this.propertyGrid2.Name = "propertyGrid2";
			this.propertyGrid2.Size = new System.Drawing.Size(314, 248);
			this.propertyGrid2.TabIndex = 18;
			this.propertyGrid2.Text = "propertyGrid2";
			this.propertyGrid2.ToolbarVisible = false;
			this.propertyGrid2.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid2.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(248, 8);
			this.progressBar1.Maximum = 128;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(384, 16);
			this.progressBar1.Step = 4;
			this.progressBar1.TabIndex = 19;
			// 
			// progressBar2
			// 
			this.progressBar2.Location = new System.Drawing.Point(248, 32);
			this.progressBar2.Maximum = 128;
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(384, 16);
			this.progressBar2.Step = 4;
			this.progressBar2.TabIndex = 20;
			// 
			// trackBar3
			// 
			this.trackBar3.Location = new System.Drawing.Point(584, 312);
			this.trackBar3.Minimum = -10;
			this.trackBar3.Name = "trackBar3";
			this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar3.Size = new System.Drawing.Size(42, 104);
			this.trackBar3.TabIndex = 23;
			this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
			// 
			// trackBar2
			// 
			this.trackBar2.Location = new System.Drawing.Point(536, 312);
			this.trackBar2.Minimum = -10;
			this.trackBar2.Name = "trackBar2";
			this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar2.Size = new System.Drawing.Size(42, 104);
			this.trackBar2.TabIndex = 22;
			this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(488, 312);
			this.trackBar1.Minimum = -10;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar1.Size = new System.Drawing.Size(42, 104);
			this.trackBar1.TabIndex = 21;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// trackBar4
			// 
			this.trackBar4.Location = new System.Drawing.Point(104, 312);
			this.trackBar4.Minimum = -10;
			this.trackBar4.Name = "trackBar4";
			this.trackBar4.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar4.Size = new System.Drawing.Size(42, 104);
			this.trackBar4.TabIndex = 26;
			this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll);
			// 
			// trackBar5
			// 
			this.trackBar5.Location = new System.Drawing.Point(56, 312);
			this.trackBar5.Minimum = -10;
			this.trackBar5.Name = "trackBar5";
			this.trackBar5.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar5.Size = new System.Drawing.Size(42, 104);
			this.trackBar5.TabIndex = 25;
			this.trackBar5.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar5.Scroll += new System.EventHandler(this.trackBar5_Scroll);
			// 
			// trackBar6
			// 
			this.trackBar6.Location = new System.Drawing.Point(8, 312);
			this.trackBar6.Minimum = -10;
			this.trackBar6.Name = "trackBar6";
			this.trackBar6.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar6.Size = new System.Drawing.Size(42, 104);
			this.trackBar6.TabIndex = 24;
			this.trackBar6.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar6.Scroll += new System.EventHandler(this.trackBar6_Scroll);
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Location = new System.Drawing.Point(400, 340);
			this.button3.Name = "button3";
			this.button3.TabIndex = 27;
			this.button3.Text = "ConcertHall";
			this.button3.Click += new System.EventHandler(this.button9_Click);
			// 
			// button5
			// 
			this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button5.Location = new System.Drawing.Point(320, 340);
			this.button5.Name = "button5";
			this.button5.TabIndex = 28;
			this.button5.Text = "Cave";
			this.button5.Click += new System.EventHandler(this.button9_Click);
			// 
			// button9
			// 
			this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button9.Location = new System.Drawing.Point(320, 316);
			this.button9.Name = "button9";
			this.button9.TabIndex = 29;
			this.button9.Text = "Generic";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button10
			// 
			this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button10.Location = new System.Drawing.Point(400, 316);
			this.button10.Name = "button10";
			this.button10.TabIndex = 30;
			this.button10.Text = "Room";
			this.button10.Click += new System.EventHandler(this.button9_Click);
			// 
			// button11
			// 
			this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button11.Location = new System.Drawing.Point(400, 364);
			this.button11.Name = "button11";
			this.button11.TabIndex = 34;
			this.button11.Text = "Auditorium";
			this.button11.Click += new System.EventHandler(this.button9_Click);
			// 
			// button12
			// 
			this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button12.Location = new System.Drawing.Point(320, 364);
			this.button12.Name = "button12";
			this.button12.TabIndex = 33;
			this.button12.Text = "Bathroom";
			this.button12.Click += new System.EventHandler(this.button9_Click);
			// 
			// button13
			// 
			this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button13.Location = new System.Drawing.Point(320, 388);
			this.button13.Name = "button13";
			this.button13.TabIndex = 32;
			this.button13.Text = "Alley";
			this.button13.Click += new System.EventHandler(this.button9_Click);
			// 
			// button14
			// 
			this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button14.Location = new System.Drawing.Point(400, 388);
			this.button14.Name = "button14";
			this.button14.TabIndex = 31;
			this.button14.Text = "Drugged";
			this.button14.Click += new System.EventHandler(this.button9_Click);
			// 
			// trackBar7
			// 
			this.trackBar7.LargeChange = 10;
			this.trackBar7.Location = new System.Drawing.Point(272, 312);
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
			this.trackBar8.Location = new System.Drawing.Point(224, 312);
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
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(635, 427);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																							 this.trackBar8,
																							 this.trackBar7,
																							 this.button11,
																							 this.button12,
																							 this.button13,
																							 this.button14,
																							 this.button10,
																							 this.button9,
																							 this.button5,
																							 this.button3,
																							 this.trackBar4,
																							 this.trackBar5,
																							 this.trackBar6,
																							 this.trackBar3,
																							 this.trackBar2,
																							 this.trackBar1,
																							 this.progressBar2,
																							 this.progressBar1,
																							 this.propertyGrid2,
																							 this.propertyGrid1,
																							 this.button8,
																							 this.button7,
																							 this.button6,
																							 this.button4,
																							 this.button2,
																							 this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Mod Test";
			((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar8)).EndInit();
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
				music = bass.LoadMusic(false, openFileDialog1.FileName, 0, 0, 
					MusicFlags.CalculateLength | MusicFlags.ThreeDee | MusicFlags.FX);
#if(TESTFX) //just a test for FX parameters, works :)
				FX fx = music.SetFX(ChannelFX.Chorus);
				FXCHORUS chorus = (FXCHORUS) fx.Parameters;
				chorus.fDepth = 20;
				chorus.fDelay = 5;
				chorus.fWetDryMix = 25;
				fx.Parameters = chorus;
				FXCHORUS chorus2 = (FXCHORUS) fx.Parameters;
#endif
				propertyGrid1.SelectedObject = music;
				
				music.Progress += new BASSProgessHandler( Progress);
			}
		}

		int slowdown = 0;

		private void Progress(ChannelBase channel)
		{
			Music music2 = (Music) channel;
			this.progressBar1.Value = music2.LeftLevel;
			this.progressBar2.Value = music2.RightLevel;
			if (slowdown++%10 == 0)
			{
				this.propertyGrid1.Refresh();
				this.propertyGrid2.Refresh();
			}
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			music.Play(0, MusicFlags.CalculateLength, true);
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			music.Stop();
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			music.Pause();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			music.Resume();
		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			float x = ((float)trackBar1.Value)/100F;

			BASS3DPosition b3d = bass.Position3D;
			b3d.pos.X = x;
			bass.Position3D = b3d;
			
			bass.Apply3D();
		}

		private void trackBar2_Scroll(object sender, System.EventArgs e)
		{
			float y = ((float)trackBar2.Value)/100F;

			BASS3DPosition b3d = bass.Position3D;
			b3d.pos.Y = y;
			bass.Position3D = b3d;
			
			bass.Apply3D();
		}
		

		private void trackBar3_Scroll(object sender, System.EventArgs e)
		{
			float z = ((float)trackBar3.Value)/100F;

			BASS3DPosition b3d = bass.Position3D;
			b3d.pos.Z = z;
			bass.Position3D = b3d;
			
			bass.Apply3D();
		}

		private void trackBar6_Scroll(object sender, System.EventArgs e)
		{
			float x = ((float)trackBar6.Value)/10F;

			Channel3DPosition b3d = music.Position3D;
			b3d.pos.X = x;
			music.Position3D = b3d;
			
			bass.Apply3D();
		}

		private void trackBar5_Scroll(object sender, System.EventArgs e)
		{
			float y = ((float)trackBar5.Value)/10F;

			Channel3DPosition b3d = music.Position3D;
			b3d.pos.Y = y;
			music.Position3D = b3d;
			
			bass.Apply3D();
		}
		

		private void trackBar4_Scroll(object sender, System.EventArgs e)
		{
			float z = ((float)trackBar4.Value)/10F;

			Channel3DPosition b3d = music.Position3D;
			b3d.pos.Z = z;
			music.Position3D = b3d;
			
			bass.Apply3D();
		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			Button button = (Button) sender;
			bass.EAXPreset = (BASSEAXPreset)Enum.Parse(typeof(BASSEAXPreset), button.Text);
		}

		private void trackBar8_Scroll(object sender, System.EventArgs e)
		{
			bass.MusicVolume = ((TrackBar) sender).Value;
		}

		private void trackBar7_Scroll(object sender, System.EventArgs e)
		{
			bass.MasterVolume = ((TrackBar) sender).Value;
		}
	}
}
