namespace GrayScale
{
    partial class Form2
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
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCircle
            // 
            this.btnCircle.Location = new System.Drawing.Point(8, 32);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(40, 40);
            this.btnCircle.TabIndex = 0;
            this.btnCircle.Text = "Circ";
            this.btnCircle.Click += new System.EventHandler(
                                           this.btnCircle_Click);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(8, 88);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(40, 40);
            this.btnLine.TabIndex = 1;
            this.btnLine.Text = "Line";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(296, 296);
            this.btnOK.Name = "btnOK";
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmGraphics
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(376, 334);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.btnCircle);
            //this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Name = "frmGraphics";
            this.Text = "Drawing";
            this.Load += new System.EventHandler(this.frmGraphics_Load);
            this.Closed += new System.EventHandler(this.frmGraphics_Closed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(
                                       this.frmGraphics_Paint);
            this.ResumeLayout(false);

        }

        #endregion
    }
}