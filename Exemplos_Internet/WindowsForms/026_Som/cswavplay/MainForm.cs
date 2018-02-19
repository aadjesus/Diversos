//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  This material may not be duplicated in whole or in part, except for 
//  personal use, without the express written consent of the author. 
//
//  Email:  ianier@hotmail.com
//
//  Copyright (C) 1999-2003 Ianier Munoz. All Rights Reserved.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace cswavplay
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button PlayButton;
		private System.Windows.Forms.Button StopButton;
		private System.Windows.Forms.OpenFileDialog OpenDlg;
		private System.Windows.Forms.Button OpenButton;

		public MainForm()
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
            this.PlayButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.OpenDlg = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(88, 16);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(72, 24);
            this.PlayButton.TabIndex = 0;
            this.PlayButton.Text = "Play";
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(168, 16);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(72, 24);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(8, 16);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(72, 24);
            this.OpenButton.TabIndex = 2;
            this.OpenButton.Text = "Open";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // OpenDlg
            // 
            this.OpenDlg.DefaultExt = "wav";
            this.OpenDlg.Filter = "WAV files|*.wav";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(250, 55);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.OpenButton,
                                                                          this.StopButton,
                                                                          this.PlayButton});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Low-level audio player";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private WaveLib.WaveOutPlayer m_Player;
        private WaveLib.WaveFormat m_Format;
        private Stream m_AudioStream; 

		private void Filler(IntPtr data, int size)
		{
			byte[] b = new byte[size];
			if (m_AudioStream != null)
			{
				int pos = 0;
				while (pos < size)
				{
					int toget = size - pos;
					int got = m_AudioStream.Read(b, pos, toget);
					if (got < toget)
						m_AudioStream.Position = 0; // loop if the file ends
					pos += got;
				}
			}
			else
			{
				for (int i = 0; i < b.Length; i++)
					b[i] = 0;
			}
			System.Runtime.InteropServices.Marshal.Copy(b, 0, data, size);
		}

		private void Stop()
		{
			if (m_Player != null)
				try
				{
					m_Player.Dispose();
				}
				finally
				{
					m_Player = null;
				}
		}

		private void Play()
		{
			Stop();
			if (m_AudioStream != null)
			{
                m_AudioStream.Position = 0;
				m_Player = new WaveLib.WaveOutPlayer(-1, m_Format, 16384, 3, new WaveLib.BufferFillEventHandler(Filler));
			}
		}

		private void CloseFile()
		{
			Stop();
			if (m_AudioStream != null)
				try
				{
					m_AudioStream.Close();
				}
				finally
				{
					m_AudioStream = null;
				}
		}

		private void OpenFile()
		{
			if (OpenDlg.ShowDialog() == DialogResult.OK)
			{
				CloseFile();
				try
				{
                    WaveLib.WaveStream S = new WaveLib.WaveStream(OpenDlg.FileName);
                    if (S.Length <= 0)
                        throw new Exception("Invalid WAV file");
                    m_Format = S.Format;
                    if (m_Format.wFormatTag != (short)WaveLib.WaveFormats.Pcm && m_Format.wFormatTag != (short)WaveLib.WaveFormats.Float)
						throw new Exception("Olny PCM files are supported");

                    m_AudioStream = S;
				}
				catch(Exception e)
				{
					CloseFile();
					MessageBox.Show(e.Message);
				}
			}
		}

		private void OpenButton_Click(object sender, System.EventArgs e)
		{
			OpenFile();
		}

		private void PlayButton_Click(object sender, System.EventArgs e)
		{
			Play();
		}

		private void StopButton_Click(object sender, System.EventArgs e)
		{
			Stop();
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			CloseFile();
		}
	}
}
