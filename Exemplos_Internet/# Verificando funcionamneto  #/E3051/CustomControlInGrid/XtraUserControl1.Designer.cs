namespace CustomControlInGrid
{
    partial class XtraUserControl1
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
            this.childControl1 = new CustomControlInGrid.ChildControl();
            this.SuspendLayout();
            // 
            // childControl1
            // 
            this.childControl1.EditValue = null;
            this.childControl1.Location = new System.Drawing.Point(20, 29);
            this.childControl1.Name = "childControl1";
            this.childControl1.Size = new System.Drawing.Size(181, 22);
            this.childControl1.TabIndex = 1;
            // 
            // XtraUserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.childControl1);
            this.Name = "XtraUserControl1";
            this.Size = new System.Drawing.Size(334, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private ChildControl childControl1;

    }
}
