namespace Owf.Controls
{
    partial class XtraForm1
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
            this.a1Panel1 = new Owf.Controls.A1Panel();
            this.SuspendLayout();
            // 
            // a1Panel1
            // 
            this.a1Panel1.BorderColor = System.Drawing.Color.Gray;
            this.a1Panel1.GradientEndColor = System.Drawing.Color.Gray;
            this.a1Panel1.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel1.Image = null;
            this.a1Panel1.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel1.Location = new System.Drawing.Point(13, 48);
            this.a1Panel1.Name = "a1Panel1";
            this.a1Panel1.Size = new System.Drawing.Size(200, 100);
            this.a1Panel1.TabIndex = 0;
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.a1Panel1);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            this.ResumeLayout(false);

        }

        #endregion

        private A1Panel a1Panel1;
    }
}