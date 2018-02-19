namespace TestDisableWindowManagement
{
	partial class TestDisableWindowManagement
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
			this.checkBoxTaskBar = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// checkBoxTaskBar
			// 
			this.checkBoxTaskBar.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxTaskBar.AutoSize = true;
			this.checkBoxTaskBar.Location = new System.Drawing.Point(13, 34);
			this.checkBoxTaskBar.Name = "checkBoxTaskBar";
			this.checkBoxTaskBar.Size = new System.Drawing.Size(60, 23);
			this.checkBoxTaskBar.TabIndex = 0;
			this.checkBoxTaskBar.Text = "Task Bar";
			this.checkBoxTaskBar.UseVisualStyleBackColor = true;
			this.checkBoxTaskBar.CheckedChanged += new System.EventHandler(this.checkBoxTaskBar_CheckedChanged);
			// 
			// TestDisableWindowManagement
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.checkBoxTaskBar);
			this.Name = "TestDisableWindowManagement";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxTaskBar;
	}
}

