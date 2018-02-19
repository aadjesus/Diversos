using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GradeTypeSample
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class GradeControl : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GradeControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

		}

		private System.Windows.Forms.Label label1;

		Grade grade = new Grade(100);

		[Category("Acedemics")]
		public Grade Grade 
		{ 
			get 
			{
				return grade;
			}
			set 
			{
				if (value.Value < 0 || value.Value > 100) 
				{
					throw new ArgumentOutOfRangeException("Grades must be between 0 and 100");
				}
				grade = value;
			}
		
		}		

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 96);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// UserControl1
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1});
			this.Name = "UserControl1";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
