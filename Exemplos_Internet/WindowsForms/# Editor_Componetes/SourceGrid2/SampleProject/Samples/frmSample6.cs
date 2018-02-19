using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample6.
	/// </summary>
	public class frmSample6 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btLoad;
		private System.Windows.Forms.TextBox txtCols;
		private System.Windows.Forms.TextBox txtRows;
		private SampleProject.Extensions.GridArray gridArray1;
		private System.Windows.Forms.ComboBox cbArrayType;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample6()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btLoad = new System.Windows.Forms.Button();
			this.txtCols = new System.Windows.Forms.TextBox();
			this.txtRows = new System.Windows.Forms.TextBox();
			this.gridArray1 = new SampleProject.Extensions.GridArray();
			this.cbArrayType = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 23);
			this.label2.TabIndex = 14;
			this.label2.Text = "Rows:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(132, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 13;
			this.label1.Text = "Columns:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btLoad
			// 
			this.btLoad.Location = new System.Drawing.Point(348, 28);
			this.btLoad.Name = "btLoad";
			this.btLoad.TabIndex = 12;
			this.btLoad.Text = "Load";
			this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
			// 
			// txtCols
			// 
			this.txtCols.Location = new System.Drawing.Point(192, 28);
			this.txtCols.Name = "txtCols";
			this.txtCols.Size = new System.Drawing.Size(72, 20);
			this.txtCols.TabIndex = 11;
			this.txtCols.Text = "1000";
			// 
			// txtRows
			// 
			this.txtRows.Location = new System.Drawing.Point(56, 28);
			this.txtRows.Name = "txtRows";
			this.txtRows.Size = new System.Drawing.Size(72, 20);
			this.txtRows.TabIndex = 10;
			this.txtRows.Text = "1000";
			// 
			// gridArray1
			// 
			this.gridArray1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridArray1.AutoSizeMinHeight = 10;
			this.gridArray1.AutoSizeMinWidth = 10;
			this.gridArray1.AutoStretchColumnsToFitWidth = false;
			this.gridArray1.AutoStretchRowsToFitHeight = false;
			this.gridArray1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gridArray1.ContextMenuStyle = ((SourceGrid2.ContextMenuStyle)((SourceGrid2.ContextMenuStyle.ClearSelection | SourceGrid2.ContextMenuStyle.CopyPasteSelection)));
			this.gridArray1.EnableEdit = true;
			this.gridArray1.GridToolTipActive = true;
			this.gridArray1.Location = new System.Drawing.Point(8, 56);
			this.gridArray1.Name = "gridArray1";
			this.gridArray1.Size = new System.Drawing.Size(480, 404);
			this.gridArray1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.gridArray1.TabIndex = 15;
			// 
			// cbArrayType
			// 
			this.cbArrayType.Location = new System.Drawing.Point(124, 4);
			this.cbArrayType.Name = "cbArrayType";
			this.cbArrayType.Size = new System.Drawing.Size(144, 21);
			this.cbArrayType.TabIndex = 16;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 16);
			this.label3.TabIndex = 17;
			this.label3.Text = "ArrayType:";
			// 
			// frmSample6
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 466);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbArrayType);
			this.Controls.Add(this.gridArray1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btLoad);
			this.Controls.Add(this.txtCols);
			this.Controls.Add(this.txtRows);
			this.Name = "frmSample6";
			this.Text = "Virtual Grid Array";
			this.Load += new System.EventHandler(this.frmSample6_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btLoad_Click(object sender, System.EventArgs e)
		{
			try
			{
				Type l_Type;
				if (cbArrayType.SelectedItem is Type)
					l_Type = (Type)cbArrayType.SelectedItem;
				else
					l_Type = Type.GetType(cbArrayType.SelectedText,true);

				System.Array l_Array = Array.CreateInstance(l_Type, int.Parse(txtRows.Text), int.Parse(txtCols.Text));
				gridArray1.LoadDataSource(l_Array);
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this,err, "Error");
			}
		}

		private void frmSample6_Load(object sender, System.EventArgs e)
		{
			cbArrayType.Items.Add(typeof(string));
			cbArrayType.Items.Add(typeof(int));
			cbArrayType.Items.Add(typeof(long));
			cbArrayType.Items.Add(typeof(DateTime));
			cbArrayType.Items.Add(typeof(double));
			cbArrayType.Items.Add(typeof(float));
			cbArrayType.Items.Add(typeof(Int64));
			cbArrayType.Items.Add(typeof(AnchorStyles));
			//cbArrayType.Items.Add(typeof(ContentAlignment)); //this enum cannot be used because doesn't have a 0 Value (required if you not initializa the array)
			cbArrayType.Items.Add(typeof(Rectangle));
			cbArrayType.SelectedIndex = 0;
		}
	}
}
