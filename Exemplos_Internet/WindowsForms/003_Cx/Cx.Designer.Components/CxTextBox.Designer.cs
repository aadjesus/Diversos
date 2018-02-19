namespace Cx.Designer.Components
{
	partial class CxTextBox
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
			this.lblLabel = new System.Windows.Forms.Label();
			this.tbText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lblLabel
			// 
			this.lblLabel.AutoSize = true;
			this.lblLabel.Location = new System.Drawing.Point(0, 0);
			this.lblLabel.Name = "lblLabel";
			this.lblLabel.Size = new System.Drawing.Size(35, 13);
			this.lblLabel.TabIndex = 0;
			this.lblLabel.Text = "label1";
			// 
			// tbText
			// 
			this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbText.Location = new System.Drawing.Point(0, 17);
			this.tbText.Name = "tbText";
			this.tbText.Size = new System.Drawing.Size(253, 20);
			this.tbText.TabIndex = 1;
			// 
			// CxTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tbText);
			this.Controls.Add(this.lblLabel);
			this.Name = "CxTextBox";
			this.Size = new System.Drawing.Size(253, 39);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblLabel;
		private System.Windows.Forms.TextBox tbText;
	}
}
