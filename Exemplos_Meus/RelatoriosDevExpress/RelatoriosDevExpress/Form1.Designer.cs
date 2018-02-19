namespace Globus5.WPF.Comum.Relatorios
{
    partial class Form1
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
            this.smpBtnLstTempoNoTrecho = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // smpBtnLstTempoNoTrecho
            // 
            this.smpBtnLstTempoNoTrecho.Location = new System.Drawing.Point(12, 31);
            this.smpBtnLstTempoNoTrecho.Name = "smpBtnLstTempoNoTrecho";
            this.smpBtnLstTempoNoTrecho.Size = new System.Drawing.Size(196, 23);
            this.smpBtnLstTempoNoTrecho.TabIndex = 2;
            this.smpBtnLstTempoNoTrecho.Text = "LstTempoNoTrecho";
            this.smpBtnLstTempoNoTrecho.Click += new System.EventHandler(this.smpBtnLstTempoNoTrecho_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 419);
            this.Controls.Add(this.smpBtnLstTempoNoTrecho);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton smpBtnLstTempoNoTrecho;
    }
}