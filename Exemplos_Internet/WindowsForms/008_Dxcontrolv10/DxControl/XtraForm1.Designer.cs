namespace DxControl
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
            this.direct3d1 = new Gosub.Direct3d();
            this.SuspendLayout();
            // 
            // direct3d1
            // 
            this.direct3d1.DxAutoResize = false;
            this.direct3d1.DxBackBufferCount = 2;
            this.direct3d1.DxFullScreen = false;
            this.direct3d1.DxSimulateFullScreen = false;
            this.direct3d1.Location = new System.Drawing.Point(13, 13);
            this.direct3d1.Name = "direct3d1";
            this.direct3d1.Size = new System.Drawing.Size(150, 150);
            this.direct3d1.TabIndex = 0;
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.direct3d1);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            this.ResumeLayout(false);

        }

        #endregion

        private Gosub.Direct3d direct3d1;
    }
}