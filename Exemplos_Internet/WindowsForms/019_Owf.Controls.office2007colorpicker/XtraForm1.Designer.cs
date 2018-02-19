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
            this.components = new System.ComponentModel.Container();
            this.office2007ColorPicker1 = new Owf.Controls.Office2007ColorPicker(this.components);
            this.SuspendLayout();
            // 
            // office2007ColorPicker1
            // 
            this.office2007ColorPicker1.Color = System.Drawing.Color.Black;
            this.office2007ColorPicker1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.office2007ColorPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.office2007ColorPicker1.FormattingEnabled = true;
            this.office2007ColorPicker1.Items.AddRange(new object[] {
            "Color",
            "sasa",
            "asdasdas"});
            this.office2007ColorPicker1.Location = new System.Drawing.Point(13, 13);
            this.office2007ColorPicker1.Name = "office2007ColorPicker1";
            this.office2007ColorPicker1.Size = new System.Drawing.Size(121, 22);
            this.office2007ColorPicker1.TabIndex = 0;
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.office2007ColorPicker1);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            this.ResumeLayout(false);

        }

        #endregion

        private Office2007ColorPicker office2007ColorPicker1;
    }
}