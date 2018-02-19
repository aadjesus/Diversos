namespace Cx.Designer.Components
{
	partial class CxComboBox
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
			this.cbComboBox = new System.Windows.Forms.ComboBox();
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
			// cbComboBox
			// 
			this.cbComboBox.FormattingEnabled = true;
			this.cbComboBox.Location = new System.Drawing.Point(0, 17);
			this.cbComboBox.Name = "cbComboBox";
			this.cbComboBox.Size = new System.Drawing.Size(147, 21);
			this.cbComboBox.TabIndex = 1;
			// 
			// CxComboBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cbComboBox);
			this.Controls.Add(this.lblLabel);
			this.Name = "CxComboBox";
			this.Size = new System.Drawing.Size(150, 41);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblLabel;
		private System.Windows.Forms.ComboBox cbComboBox;
	}
}
