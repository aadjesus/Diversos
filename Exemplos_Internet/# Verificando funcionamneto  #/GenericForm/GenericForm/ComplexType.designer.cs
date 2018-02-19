namespace GenericForm
{
    partial class ComplexType
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
            this.panel1 = new ObjectPanel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Instance = null;
            this.panel1.Location = new System.Drawing.Point(12, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 69);
            this.panel1.TabIndex = 0;
            // 
            // SoapComplexType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "SoapComplexType";
            this.Size = new System.Drawing.Size(482, 150);
            this.Load += new System.EventHandler(this.SoapComplexType_Load);
            this.Leave += new System.EventHandler(this.SoapComplexType_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ObjectPanel panel1;

        public ObjectPanel Panel1
        {
            get { return panel1; }
            set { panel1 = value; }
        }

    }
}
