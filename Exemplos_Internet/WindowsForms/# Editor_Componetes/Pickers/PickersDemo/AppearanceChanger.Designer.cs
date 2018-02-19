namespace PickersDemo
{
    partial class AppearanceChanger
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonCB = new System.Windows.Forms.RadioButton();
            this.radioButtonECB = new System.Windows.Forms.RadioButton();
            this.radioButtonChkB = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Appearance:";
            // 
            // radioButtonCB
            // 
            this.radioButtonCB.AutoSize = true;
            this.radioButtonCB.Checked = true;
            this.radioButtonCB.Location = new System.Drawing.Point(6, 25);
            this.radioButtonCB.Name = "radioButtonCB";
            this.radioButtonCB.Size = new System.Drawing.Size(76, 17);
            this.radioButtonCB.TabIndex = 1;
            this.radioButtonCB.TabStop = true;
            this.radioButtonCB.Text = "ComboBox";
            this.radioButtonCB.UseVisualStyleBackColor = true;
            this.radioButtonCB.CheckedChanged += new System.EventHandler(this.radioButtonCB_CheckedChanged);
            // 
            // radioButtonECB
            // 
            this.radioButtonECB.AutoSize = true;
            this.radioButtonECB.Location = new System.Drawing.Point(6, 48);
            this.radioButtonECB.Name = "radioButtonECB";
            this.radioButtonECB.Size = new System.Drawing.Size(114, 17);
            this.radioButtonECB.TabIndex = 2;
            this.radioButtonECB.Text = "EditableComboBox";
            this.radioButtonECB.UseVisualStyleBackColor = true;
            this.radioButtonECB.CheckedChanged += new System.EventHandler(this.radioButtonECB_CheckedChanged);
            // 
            // radioButtonChkB
            // 
            this.radioButtonChkB.AutoSize = true;
            this.radioButtonChkB.Location = new System.Drawing.Point(6, 71);
            this.radioButtonChkB.Name = "radioButtonChkB";
            this.radioButtonChkB.Size = new System.Drawing.Size(87, 17);
            this.radioButtonChkB.TabIndex = 3;
            this.radioButtonChkB.Text = "CheckButton";
            this.radioButtonChkB.UseVisualStyleBackColor = true;
            this.radioButtonChkB.CheckedChanged += new System.EventHandler(this.radioButtonChkB_CheckedChanged);
            // 
            // AppearanceChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonChkB);
            this.Controls.Add(this.radioButtonECB);
            this.Controls.Add(this.radioButtonCB);
            this.Controls.Add(this.label1);
            this.Name = "AppearanceChanger";
            this.Size = new System.Drawing.Size(130, 105);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonCB;
        private System.Windows.Forms.RadioButton radioButtonECB;
        private System.Windows.Forms.RadioButton radioButtonChkB;

    }
}
