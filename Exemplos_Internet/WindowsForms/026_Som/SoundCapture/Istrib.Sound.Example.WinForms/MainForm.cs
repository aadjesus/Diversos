//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  lukasz@istrib.org
//
//  Copyright (C) 2008-2009 Lukasz Kwiecinski. 
//
//  LAME ( LAME Ain't an Mp3 Encoder ) 
//  You must call the fucntion "beVersion" to obtain information  like version 
//  numbers (both of the DLL and encoding engine), release date and URL for 
//  lame_enc's homepage. All this information should be made available to the 
//  user of your product through a dialog box or something similar.
//  You must see all information about LAME project and legal license infos at 
//  http://www.mp3dev.org/  The official LAME site
//
//
//  About Thomson and/or Fraunhofer patents:
//  Any use of this product does not convey a license under the relevant 
//  intellectual property of Thomson and/or Fraunhofer Gesellschaft nor imply 
//  any right to use this product in any finished end user or ready-to-use final 
//  product. An independent license for such use is required. 
//  For details, please visit http://www.mp3licensing.com.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Istrib.Sound.Formats;
using System.IO;

namespace Istrib.Sound.Example.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			fileTxt.Text = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Output.mp3");

            LoadAvailableFormats(PcmSoundFormat.StandardFormats);

            foreach (SoundCaptureDevice device in SoundCaptureDevice.AllAvailable)
            {
                devicesCmb.Items.Add(device);
            }

            devicesCmb.SelectedIndex = 0;

			mp3Rd_CheckedChanged(sender, e);
        }

        private void LoadAvailableFormats(IEnumerable<PcmSoundFormat> formats)
        {
            formatsCmb.Items.Clear();

            foreach (PcmSoundFormat format in formats)
            {
                formatsCmb.Items.Add(format);
            }

            formatsCmb.SelectedIndex = 0;
        }

		private void LoadAvailableBitRates()
		{
			bitRateCmb.Items.Clear();

			foreach (Mp3BitRate bitRate in ((PcmSoundFormat)formatsCmb.SelectedItem).GetCompatibleMp3BitRates())
			{
				bitRateCmb.Items.Add(bitRate);
			}

			if (bitRateCmb.Items.Count > 0)
			{
				bitRateCmb.SelectedIndex = 0;
			}
		}

        private void startBtn_Click(object sender, EventArgs e)
        {
            try
            {
                mp3SoundCapture.CaptureDevice = (SoundCaptureDevice)devicesCmb.SelectedItem;
                mp3SoundCapture.NormalizeVolume = normalizeChk.Checked;
                if (rawPcmRd.Checked)
                {
                    mp3SoundCapture.OutputType = Mp3SoundCapture.Outputs.RawPcm;
                }
                else if (mp3Rd.Checked)
                {
                    mp3SoundCapture.OutputType = Mp3SoundCapture.Outputs.Mp3;
                }
                else
                {
                    mp3SoundCapture.OutputType = Mp3SoundCapture.Outputs.Wav;
                }

                mp3SoundCapture.WaveFormat = (PcmSoundFormat)formatsCmb.SelectedItem;
				mp3SoundCapture.Mp3BitRate = (Mp3BitRate)bitRateCmb.SelectedItem;
                mp3SoundCapture.WaitOnStop = !asyncStopChk.Checked;
                mp3SoundCapture.Start(fileTxt.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Not so easy...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            mp3SoundCapture.Stop();
        }

        private void mp3Rd_CheckedChanged(object sender, EventArgs e)
        {
            if (mp3Rd.Checked)
            {
                LoadAvailableFormats(Mp3SoundFormat.AllSourceFormats);
				formatsCmb.SelectedItem = PcmSoundFormat.Pcm44kHz16bitStereo;
				bitRateCmb.Enabled = true;
				bitRateCmb.SelectedItem = Mp3BitRate.BitRate192;

                fileTxt.Text = Path.Combine(Path.GetDirectoryName(fileTxt.Text),
					Path.GetFileNameWithoutExtension(fileTxt.Text) + ".mp3");
            }
            else
            {
                LoadAvailableFormats(PcmSoundFormat.StandardFormats);
                fileTxt.Text = Path.Combine(Path.GetDirectoryName(fileTxt.Text),
					Path.GetFileNameWithoutExtension(fileTxt.Text) + ".wav");
				bitRateCmb.Enabled = false;
            }
        }

        private void mp3SoundCapture_Starting(object sender, EventArgs e)
        {
            startBtn.Enabled = !(stopBtn.Enabled = true);
            dataAvailableLbl.Visible = false;
        }

        private void mp3SoundCapture_Stopping(object sender, EventArgs e)
        {
            startBtn.Enabled = !(stopBtn.Enabled = false);
        }

        private void mp3SoundCapture_Stopped(object sender, Mp3SoundCapture.StoppedEventArgs e)
        {
            dataAvailableLbl.Text = "Data available in " + e.OutputFileName;
            dataAvailableLbl.Visible = true;
        }

		private void browseBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			saveFileDialog.FileName = fileTxt.Text;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				fileTxt.Text = saveFileDialog.FileName;
			}
		}

		private void formatsCmb_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadAvailableBitRates();
		}
    }
}
