namespace TestDesigner
{
    partial class InheritedForm1
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
            this.SuspendLayout();
            // 
            // demoTreeView1
            // 
            this.demoTreeView1.LineColor = System.Drawing.Color.Black;
            this.demoTreeView1.Location = new System.Drawing.Point(71, 70);
            // 
            // InheritedForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(292, 262);
            this.Name = "InheritedForm1";
            this.Controls.SetChildIndex(this.demoTreeView1, 0);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
