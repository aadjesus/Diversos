namespace Istrib.Sound.Example.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.formatsCmb = new System.Windows.Forms.ComboBox();
			this.devicesCmb = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.startBtn = new System.Windows.Forms.Button();
			this.fileTxt = new System.Windows.Forms.TextBox();
			this.fileLbl = new System.Windows.Forms.Label();
			this.stopBtn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mp3Rd = new System.Windows.Forms.RadioButton();
			this.riffRd = new System.Windows.Forms.RadioButton();
			this.rawPcmRd = new System.Windows.Forms.RadioButton();
			this.normalizeChk = new System.Windows.Forms.CheckBox();
			this.mp3SoundCapture = new Istrib.Sound.Mp3SoundCapture(this.components);
			this.bitRateCmb = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.asyncStopChk = new System.Windows.Forms.CheckBox();
			this.dataAvailableLbl = new System.Windows.Forms.Label();
			this.browseBtn = new System.Windows.Forms.LinkLabel();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 130);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sampling";
			// 
			// formatsCmb
			// 
			this.formatsCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.formatsCmb.FormattingEnabled = true;
			this.formatsCmb.Location = new System.Drawing.Point(83, 127);
			this.formatsCmb.Name = "formatsCmb";
			this.formatsCmb.Size = new System.Drawing.Size(178, 21);
			this.formatsCmb.TabIndex = 3;
			this.formatsCmb.SelectedIndexChanged += new System.EventHandler(this.formatsCmb_SelectedIndexChanged);
			// 
			// devicesCmb
			// 
			this.devicesCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.devicesCmb.FormattingEnabled = true;
			this.devicesCmb.Location = new System.Drawing.Point(83, 100);
			this.devicesCmb.Name = "devicesCmb";
			this.devicesCmb.Size = new System.Drawing.Size(178, 21);
			this.devicesCmb.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 103);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Device";
			// 
			// startBtn
			// 
			this.startBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.startBtn.Location = new System.Drawing.Point(107, 247);
			this.startBtn.Name = "startBtn";
			this.startBtn.Size = new System.Drawing.Size(75, 23);
			this.startBtn.TabIndex = 7;
			this.startBtn.Text = "Start";
			this.startBtn.UseVisualStyleBackColor = true;
			this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
			// 
			// fileTxt
			// 
			this.fileTxt.Location = new System.Drawing.Point(83, 182);
			this.fileTxt.Name = "fileTxt";
			this.fileTxt.Size = new System.Drawing.Size(161, 20);
			this.fileTxt.TabIndex = 5;
			this.fileTxt.Text = "c:\\Output.mp3";
			// 
			// fileLbl
			// 
			this.fileLbl.AutoSize = true;
			this.fileLbl.Location = new System.Drawing.Point(12, 185);
			this.fileLbl.Name = "fileLbl";
			this.fileLbl.Size = new System.Drawing.Size(58, 13);
			this.fileLbl.TabIndex = 6;
			this.fileLbl.Text = "Output File";
			// 
			// stopBtn
			// 
			this.stopBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.stopBtn.Enabled = false;
			this.stopBtn.Location = new System.Drawing.Point(188, 247);
			this.stopBtn.Name = "stopBtn";
			this.stopBtn.Size = new System.Drawing.Size(75, 23);
			this.stopBtn.TabIndex = 8;
			this.stopBtn.Text = "Stop";
			this.stopBtn.UseVisualStyleBackColor = true;
			this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.mp3Rd);
			this.groupBox1.Controls.Add(this.riffRd);
			this.groupBox1.Controls.Add(this.rawPcmRd);
			this.groupBox1.Location = new System.Drawing.Point(15, 7);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(246, 87);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Format";
			// 
			// mp3Rd
			// 
			this.mp3Rd.AutoSize = true;
			this.mp3Rd.Checked = true;
			this.mp3Rd.Location = new System.Drawing.Point(7, 66);
			this.mp3Rd.Name = "mp3Rd";
			this.mp3Rd.Size = new System.Drawing.Size(47, 17);
			this.mp3Rd.TabIndex = 2;
			this.mp3Rd.TabStop = true;
			this.mp3Rd.Text = "MP3";
			this.mp3Rd.UseVisualStyleBackColor = true;
			this.mp3Rd.CheckedChanged += new System.EventHandler(this.mp3Rd_CheckedChanged);
			// 
			// riffRd
			// 
			this.riffRd.AutoSize = true;
			this.riffRd.Location = new System.Drawing.Point(7, 43);
			this.riffRd.Name = "riffRd";
			this.riffRd.Size = new System.Drawing.Size(118, 17);
			this.riffRd.TabIndex = 1;
			this.riffRd.Text = "WAV (RIFF header)";
			this.riffRd.UseVisualStyleBackColor = true;
			// 
			// rawPcmRd
			// 
			this.rawPcmRd.AutoSize = true;
			this.rawPcmRd.Location = new System.Drawing.Point(7, 20);
			this.rawPcmRd.Name = "rawPcmRd";
			this.rawPcmRd.Size = new System.Drawing.Size(180, 17);
			this.rawPcmRd.TabIndex = 0;
			this.rawPcmRd.Text = "Raw PCM (WAV without header)";
			this.rawPcmRd.UseVisualStyleBackColor = true;
			// 
			// normalizeChk
			// 
			this.normalizeChk.AutoSize = true;
			this.normalizeChk.Checked = true;
			this.normalizeChk.CheckState = System.Windows.Forms.CheckState.Checked;
			this.normalizeChk.Location = new System.Drawing.Point(15, 208);
			this.normalizeChk.Name = "normalizeChk";
			this.normalizeChk.Size = new System.Drawing.Size(72, 17);
			this.normalizeChk.TabIndex = 6;
			this.normalizeChk.Text = "Normalize";
			this.normalizeChk.UseVisualStyleBackColor = true;
			// 
			// mp3SoundCapture
			// 
			this.mp3SoundCapture.NormalizeVolume = false;
			this.mp3SoundCapture.OutputType = Istrib.Sound.Mp3SoundCapture.Outputs.Wav;
			this.mp3SoundCapture.UseSynchronizationContext = true;
			this.mp3SoundCapture.WaitOnStop = true;
			this.mp3SoundCapture.Starting += new System.EventHandler(this.mp3SoundCapture_Starting);
			this.mp3SoundCapture.Stopped += new System.EventHandler<Istrib.Sound.Mp3SoundCapture.StoppedEventArgs>(this.mp3SoundCapture_Stopped);
			this.mp3SoundCapture.Stopping += new System.EventHandler(this.mp3SoundCapture_Stopping);
			// 
			// bitRateCmb
			// 
			this.bitRateCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.bitRateCmb.Enabled = false;
			this.bitRateCmb.FormattingEnabled = true;
			this.bitRateCmb.Location = new System.Drawing.Point(83, 155);
			this.bitRateCmb.Name = "bitRateCmb";
			this.bitRateCmb.Size = new System.Drawing.Size(178, 21);
			this.bitRateCmb.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 158);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "MP3 Bitrate";
			// 
			// asyncStopChk
			// 
			this.asyncStopChk.AutoSize = true;
			this.asyncStopChk.Location = new System.Drawing.Point(93, 208);
			this.asyncStopChk.Name = "asyncStopChk";
			this.asyncStopChk.Size = new System.Drawing.Size(80, 17);
			this.asyncStopChk.TabIndex = 12;
			this.asyncStopChk.Text = "Async Stop";
			this.asyncStopChk.UseVisualStyleBackColor = true;
			// 
			// dataAvailableLbl
			// 
			this.dataAvailableLbl.AutoSize = true;
			this.dataAvailableLbl.Location = new System.Drawing.Point(12, 228);
			this.dataAvailableLbl.Name = "dataAvailableLbl";
			this.dataAvailableLbl.Size = new System.Drawing.Size(90, 13);
			this.dataAvailableLbl.TabIndex = 13;
			this.dataAvailableLbl.Text = "Data Available in ";
			this.dataAvailableLbl.Visible = false;
			// 
			// browseBtn
			// 
			this.browseBtn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.browseBtn.Location = new System.Drawing.Point(243, 182);
			this.browseBtn.Name = "browseBtn";
			this.browseBtn.Size = new System.Drawing.Size(18, 20);
			this.browseBtn.TabIndex = 14;
			this.browseBtn.TabStop = true;
			this.browseBtn.Text = "...";
			this.browseBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.browseBtn_LinkClicked);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "WAV|*.wav|MP3|*.mp3";
			this.saveFileDialog.Title = "Save output As...";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 279);
			this.Controls.Add(this.browseBtn);
			this.Controls.Add(this.dataAvailableLbl);
			this.Controls.Add(this.asyncStopChk);
			this.Controls.Add(this.bitRateCmb);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.normalizeChk);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.stopBtn);
			this.Controls.Add(this.fileLbl);
			this.Controls.Add(this.fileTxt);
			this.Controls.Add(this.startBtn);
			this.Controls.Add(this.devicesCmb);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.formatsCmb);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "MP3 Sound Capture";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox formatsCmb;
        private System.Windows.Forms.ComboBox devicesCmb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox fileTxt;
        private System.Windows.Forms.Label fileLbl;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton riffRd;
        private System.Windows.Forms.RadioButton rawPcmRd;
        private System.Windows.Forms.CheckBox normalizeChk;
        private System.Windows.Forms.RadioButton mp3Rd;
        private Mp3SoundCapture mp3SoundCapture;
		private System.Windows.Forms.ComboBox bitRateCmb;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox asyncStopChk;
        private System.Windows.Forms.Label dataAvailableLbl;
		private System.Windows.Forms.LinkLabel browseBtn;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

