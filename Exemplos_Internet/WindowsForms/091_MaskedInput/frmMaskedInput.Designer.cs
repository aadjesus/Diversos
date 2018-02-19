namespace MaskedInput
{
    partial class frmMaskedInput
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
            this.label1 = new System.Windows.Forms.Label();
            this.maskedInput = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radMask5 = new System.Windows.Forms.RadioButton();
            this.radMask4 = new System.Windows.Forms.RadioButton();
            this.radMask3 = new System.Windows.Forms.RadioButton();
            this.radMask2 = new System.Windows.Forms.RadioButton();
            this.radMask1 = new System.Windows.Forms.RadioButton();
            this.chkHideMsk = new System.Windows.Forms.CheckBox();
            this.chkIncludePrompt = new System.Windows.Forms.CheckBox();
            this.chkIncludeLiteral = new System.Windows.Forms.CheckBox();
            this.btnOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.chkBeepErr = new System.Windows.Forms.CheckBox();
            this.txtmskPromptChar = new System.Windows.Forms.MaskedTextBox();
            this.lblMaskCompleted = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Masked Text Box";
            // 
            // maskedInput
            // 
            this.maskedInput.Location = new System.Drawing.Point(146, 14);
            this.maskedInput.Name = "maskedInput";
            this.maskedInput.Size = new System.Drawing.Size(174, 22);
            this.maskedInput.TabIndex = 1;
            this.maskedInput.TextChanged += new System.EventHandler(this.maskedInput_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radMask5);
            this.groupBox1.Controls.Add(this.radMask4);
            this.groupBox1.Controls.Add(this.radMask3);
            this.groupBox1.Controls.Add(this.radMask2);
            this.groupBox1.Controls.Add(this.radMask1);
            this.groupBox1.Location = new System.Drawing.Point(351, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 162);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Masks";
            // 
            // radMask5
            // 
            this.radMask5.AutoSize = true;
            this.radMask5.Location = new System.Drawing.Point(15, 132);
            this.radMask5.Name = "radMask5";
            this.radMask5.Size = new System.Drawing.Size(110, 18);
            this.radMask5.TabIndex = 4;
            this.radMask5.TabStop = true;
            this.radMask5.Text = "LL.LaaaCCCC";
            this.radMask5.UseVisualStyleBackColor = true;
            this.radMask5.CheckedChanged += new System.EventHandler(this.radMask5_CheckedChanged);
            // 
            // radMask4
            // 
            this.radMask4.AutoSize = true;
            this.radMask4.Location = new System.Drawing.Point(15, 104);
            this.radMask4.Name = "radMask4";
            this.radMask4.Size = new System.Drawing.Size(99, 18);
            this.radMask4.TabIndex = 3;
            this.radMask4.TabStop = true;
            this.radMask4.Text = "00 : 00 : 00";
            this.radMask4.UseVisualStyleBackColor = true;
            this.radMask4.CheckedChanged += new System.EventHandler(this.radMask4_CheckedChanged);
            // 
            // radMask3
            // 
            this.radMask3.AutoSize = true;
            this.radMask3.Location = new System.Drawing.Point(15, 76);
            this.radMask3.Name = "radMask3";
            this.radMask3.Size = new System.Drawing.Size(77, 18);
            this.radMask3.TabIndex = 2;
            this.radMask3.TabStop = true;
            this.radMask3.Text = "$99,999";
            this.radMask3.UseVisualStyleBackColor = true;
            this.radMask3.CheckedChanged += new System.EventHandler(this.radMask3_CheckedChanged);
            // 
            // radMask2
            // 
            this.radMask2.AutoSize = true;
            this.radMask2.Location = new System.Drawing.Point(15, 47);
            this.radMask2.Name = "radMask2";
            this.radMask2.Size = new System.Drawing.Size(65, 18);
            this.radMask2.TabIndex = 1;
            this.radMask2.TabStop = true;
            this.radMask2.Text = "Aaaaa";
            this.radMask2.UseVisualStyleBackColor = true;
            this.radMask2.CheckedChanged += new System.EventHandler(this.radMask2_CheckedChanged);
            // 
            // radMask1
            // 
            this.radMask1.AutoSize = true;
            this.radMask1.Location = new System.Drawing.Point(15, 20);
            this.radMask1.Name = "radMask1";
            this.radMask1.Size = new System.Drawing.Size(98, 18);
            this.radMask1.TabIndex = 0;
            this.radMask1.TabStop = true;
            this.radMask1.Text = "#00 - (999)";
            this.radMask1.UseVisualStyleBackColor = true;
            this.radMask1.CheckedChanged += new System.EventHandler(this.radMask1_CheckedChanged);
            // 
            // chkHideMsk
            // 
            this.chkHideMsk.AutoSize = true;
            this.chkHideMsk.Location = new System.Drawing.Point(17, 62);
            this.chkHideMsk.Name = "chkHideMsk";
            this.chkHideMsk.Size = new System.Drawing.Size(152, 18);
            this.chkHideMsk.TabIndex = 5;
            this.chkHideMsk.Text = "Hide Mask on Leave";
            this.chkHideMsk.UseVisualStyleBackColor = true;
            this.chkHideMsk.CheckedChanged += new System.EventHandler(this.chkHideMsk_CheckedChanged);
            // 
            // chkIncludePrompt
            // 
            this.chkIncludePrompt.AutoSize = true;
            this.chkIncludePrompt.Location = new System.Drawing.Point(17, 111);
            this.chkIncludePrompt.Name = "chkIncludePrompt";
            this.chkIncludePrompt.Size = new System.Drawing.Size(121, 18);
            this.chkIncludePrompt.TabIndex = 7;
            this.chkIncludePrompt.Text = "Include Prompt";
            this.chkIncludePrompt.UseVisualStyleBackColor = true;
            this.chkIncludePrompt.CheckedChanged += new System.EventHandler(this.chkIncludePrompt_CheckedChanged);
            // 
            // chkIncludeLiteral
            // 
            this.chkIncludeLiteral.AutoSize = true;
            this.chkIncludeLiteral.Location = new System.Drawing.Point(17, 86);
            this.chkIncludeLiteral.Name = "chkIncludeLiteral";
            this.chkIncludeLiteral.Size = new System.Drawing.Size(115, 18);
            this.chkIncludeLiteral.TabIndex = 8;
            this.chkIncludeLiteral.Text = "Include Literal";
            this.chkIncludeLiteral.UseVisualStyleBackColor = true;
            this.chkIncludeLiteral.CheckedChanged += new System.EventHandler(this.chkIncludeLiteral_CheckedChanged);
            // 
            // btnOutput
            // 
            this.btnOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnOutput.ForeColor = System.Drawing.Color.Black;
            this.btnOutput.Location = new System.Drawing.Point(17, 146);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(114, 23);
            this.btnOutput.TabIndex = 9;
            this.btnOutput.Text = "Show Output";
            this.btnOutput.UseVisualStyleBackColor = false;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(146, 146);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(174, 22);
            this.txtOutput.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "Prompt Char:";
            // 
            // btnSet
            // 
            this.btnSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSet.ForeColor = System.Drawing.Color.Black;
            this.btnSet.Location = new System.Drawing.Point(277, 110);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(37, 23);
            this.btnSet.TabIndex = 13;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = false;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // chkBeepErr
            // 
            this.chkBeepErr.AutoSize = true;
            this.chkBeepErr.Location = new System.Drawing.Point(202, 62);
            this.chkBeepErr.Name = "chkBeepErr";
            this.chkBeepErr.Size = new System.Drawing.Size(113, 18);
            this.chkBeepErr.TabIndex = 14;
            this.chkBeepErr.Text = "Beep on Error";
            this.chkBeepErr.UseVisualStyleBackColor = true;
            this.chkBeepErr.CheckedChanged += new System.EventHandler(this.chkBeepErr_CheckedChanged);
            // 
            // txtmskPromptChar
            // 
            this.txtmskPromptChar.BeepOnError = true;
            this.txtmskPromptChar.Location = new System.Drawing.Point(251, 110);
            this.txtmskPromptChar.Mask = "C";
            this.txtmskPromptChar.Name = "txtmskPromptChar";
            this.txtmskPromptChar.Size = new System.Drawing.Size(21, 22);
            this.txtmskPromptChar.TabIndex = 15;
            // 
            // lblMaskCompleted
            // 
            this.lblMaskCompleted.AutoSize = true;
            this.lblMaskCompleted.BackColor = System.Drawing.Color.Navy;
            this.lblMaskCompleted.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaskCompleted.ForeColor = System.Drawing.Color.PaleGreen;
            this.lblMaskCompleted.Location = new System.Drawing.Point(144, 39);
            this.lblMaskCompleted.Name = "lblMaskCompleted";
            this.lblMaskCompleted.Size = new System.Drawing.Size(100, 12);
            this.lblMaskCompleted.TabIndex = 16;
            this.lblMaskCompleted.Text = " MASK COMPLETED";
            this.lblMaskCompleted.Visible = false;
            // 
            // frmMaskedInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(518, 180);
            this.Controls.Add(this.lblMaskCompleted);
            this.Controls.Add(this.txtmskPromptChar);
            this.Controls.Add(this.chkBeepErr);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.chkIncludeLiteral);
            this.Controls.Add(this.chkIncludePrompt);
            this.Controls.Add(this.chkHideMsk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.maskedInput);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "frmMaskedInput";
            this.Text = "Masked Input Sample";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedInput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radMask4;
        private System.Windows.Forms.RadioButton radMask3;
        private System.Windows.Forms.RadioButton radMask2;
        private System.Windows.Forms.RadioButton radMask1;
        private System.Windows.Forms.CheckBox chkHideMsk;
        private System.Windows.Forms.CheckBox chkIncludePrompt;
        private System.Windows.Forms.CheckBox chkIncludeLiteral;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.RadioButton radMask5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.CheckBox chkBeepErr;
        private System.Windows.Forms.MaskedTextBox txtmskPromptChar;
        private System.Windows.Forms.Label lblMaskCompleted;
    }
}

