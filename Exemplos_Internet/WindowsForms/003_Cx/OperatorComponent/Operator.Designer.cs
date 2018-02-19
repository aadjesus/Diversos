namespace OperatorComponent
{
	partial class Operator
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
			this.btnPlus = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnEqual = new System.Windows.Forms.Button();
			this.btnDivide = new System.Windows.Forms.Button();
			this.btnMultiply = new System.Windows.Forms.Button();
			this.btnMinus = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnPlus
			// 
			this.btnPlus.Location = new System.Drawing.Point(3, 4);
			this.btnPlus.Name = "btnPlus";
			this.btnPlus.Size = new System.Drawing.Size(33, 23);
			this.btnPlus.TabIndex = 9;
			this.btnPlus.Text = "+";
			this.btnPlus.UseVisualStyleBackColor = true;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(42, 33);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(33, 23);
			this.btnClear.TabIndex = 10;
			this.btnClear.Text = "CLR";
			this.btnClear.UseVisualStyleBackColor = true;
			// 
			// btnEqual
			// 
			this.btnEqual.Location = new System.Drawing.Point(42, 4);
			this.btnEqual.Name = "btnEqual";
			this.btnEqual.Size = new System.Drawing.Size(33, 23);
			this.btnEqual.TabIndex = 11;
			this.btnEqual.Text = "=";
			this.btnEqual.UseVisualStyleBackColor = true;
			// 
			// btnDivide
			// 
			this.btnDivide.Location = new System.Drawing.Point(3, 91);
			this.btnDivide.Name = "btnDivide";
			this.btnDivide.Size = new System.Drawing.Size(33, 23);
			this.btnDivide.TabIndex = 12;
			this.btnDivide.Text = "/";
			this.btnDivide.UseVisualStyleBackColor = true;
			// 
			// btnMultiply
			// 
			this.btnMultiply.Location = new System.Drawing.Point(3, 62);
			this.btnMultiply.Name = "btnMultiply";
			this.btnMultiply.Size = new System.Drawing.Size(33, 23);
			this.btnMultiply.TabIndex = 13;
			this.btnMultiply.Text = "*";
			this.btnMultiply.UseVisualStyleBackColor = true;
			// 
			// btnMinus
			// 
			this.btnMinus.Location = new System.Drawing.Point(3, 33);
			this.btnMinus.Name = "btnMinus";
			this.btnMinus.Size = new System.Drawing.Size(33, 23);
			this.btnMinus.TabIndex = 14;
			this.btnMinus.Text = "-";
			this.btnMinus.UseVisualStyleBackColor = true;
			// 
			// Operator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnMinus);
			this.Controls.Add(this.btnMultiply);
			this.Controls.Add(this.btnDivide);
			this.Controls.Add(this.btnEqual);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnPlus);
			this.Name = "Operator";
			this.Size = new System.Drawing.Size(82, 121);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnPlus;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnEqual;
		private System.Windows.Forms.Button btnDivide;
		private System.Windows.Forms.Button btnMultiply;
		private System.Windows.Forms.Button btnMinus;
	}
}
