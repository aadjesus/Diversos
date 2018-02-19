using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using SAS.Shared.AddIns;

namespace EGAddin.CalculateLag
{
	/// <summary>
	/// Windows Form class for the Calculate Lag/Dif custom task
	/// </summary>
	public class LagForm : System.Windows.Forms.Form
	{
		#region Private members
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbColumns;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// the consumer application
		private SAS.Shared.AddIns.ISASTaskConsumer _consumer = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton rbtnLag;
		private System.Windows.Forms.RadioButton rbtnDif;
		private System.Windows.Forms.NumericUpDown txtN;
		private System.Windows.Forms.Panel panelLag;
		private System.Windows.Forms.PictureBox imgExample;
		private System.Windows.Forms.Label lblDesc;

		private Bitmap lagImg=null, difImg=null;

		#endregion

		#region Properties
		/// <summary>
		/// Accessor method to set the interface to the consumer application
		/// </summary>
		public SAS.Shared.AddIns.ISASTaskConsumer Consumer
		{
			set
			{
				_consumer = value;
			}
		}

		private string _lagcolumn="";
		/// <summary>
		/// Column to Lag
		/// </summary>
		public string LagColumn
		{
			get { return _lagcolumn; }
			set { _lagcolumn = value; }
		}

		/// <summary>
		/// Which function to calc: lag or dif
		/// </summary>
		public Lag.eFunction Function
		{
			get 
			{ 
				return rbtnDif.Checked ? Lag.eFunction.Dif : Lag.eFunction.Lag;
			}
			set 
			{ 
				if (value==Lag.eFunction.Dif)
					rbtnDif.Checked = true;
				else rbtnLag.Checked = true;

				UpdateExampleImage();
			}
		}

		public int n 
		{
			get 
			{ 
				return Convert.ToInt32(txtN.Value);
			}
			set 
			{ 
				txtN.Value = value; 
			}
		}

		#endregion

		#region Event handlers
		private void cmbColumns_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			_lagcolumn = cmbColumns.SelectedItem.ToString();
		}

		private void OnFunctionChanged(object sender, System.EventArgs e)
		{
			UpdateExampleImage();
		}
		#endregion

		#region Ctor, Dispose, Initialization
		public LagForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
	
			System.IO.Stream	resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CalculateLag.lag.jpg");
			if (resStream != null)
			{
				lagImg = new Bitmap(resStream);
			}
			resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CalculateLag.dif.jpg");
			if (resStream != null)
			{
				difImg = new Bitmap(resStream);
			}		
		}

		private void UpdateExampleImage()
		{
			if (rbtnLag.Checked && lagImg!=null)
			{
				imgExample.Image = lagImg;
				lblDesc.Text = "The Lag function populates a new column with the value of the input column as it appears in an earlier row.";
			}
			else if (difImg!=null)
			{
				imgExample.Image = difImg;
				lblDesc.Text = "The Dif function calculates the difference between the value in the current row and a previous row.";
			}
			imgExample.Width = imgExample.Image.Width;
			imgExample.Height = imgExample.Image.Height;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);

			if (_consumer==null)
				throw new NullReferenceException("Consumer property must be set.");

			ISASTaskData d = _consumer.ActiveData;
			ISASTaskDataAccessor a = d.Accessor;
			a.Open();
			for (int i = 0; i<a.ColumnCount; i++)
			{
				ISASTaskDataColumn dc = a.ColumnInfoByIndex(i);
				if (dc.Group == VariableGroup.Numeric || 
					dc.Group == VariableGroup.Date || 
					dc.Group == VariableGroup.Currency ||
					dc.Group == VariableGroup.Time) 
					cmbColumns.Items.Add(dc.Name);
			}
			a.Close();

			// cannot continue if there are no numeric columns
			if (cmbColumns.Items.Count == 0)
			{
				MessageBox.Show(this,"Input data does not contain any numeric columns.","Cannot compute LAG");
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}

			// no selection yet -- set the first column as selected
			if (_lagcolumn.Length==0)
				cmbColumns.SelectedIndex = 0;
			else
				cmbColumns.Text = _lagcolumn;

			UpdateExampleImage();

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

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LagForm));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbColumns = new System.Windows.Forms.ComboBox();
			this.rbtnLag = new System.Windows.Forms.RadioButton();
			this.rbtnDif = new System.Windows.Forms.RadioButton();
			this.txtN = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panelLag = new System.Windows.Forms.Panel();
			this.imgExample = new System.Windows.Forms.PictureBox();
			this.lblDesc = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.txtN)).BeginInit();
			this.panelLag.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(134, 316);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 7;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(222, 316);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(8, 192);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(296, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Select a column to lag/dif:";
			// 
			// cmbColumns
			// 
			this.cmbColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColumns.Location = new System.Drawing.Point(8, 216);
			this.cmbColumns.Name = "cmbColumns";
			this.cmbColumns.Size = new System.Drawing.Size(296, 21);
			this.cmbColumns.TabIndex = 4;
			this.cmbColumns.SelectedIndexChanged += new System.EventHandler(this.cmbColumns_SelectedIndexChanged);
			// 
			// rbtnLag
			// 
			this.rbtnLag.Location = new System.Drawing.Point(16, 40);
			this.rbtnLag.Name = "rbtnLag";
			this.rbtnLag.Size = new System.Drawing.Size(56, 16);
			this.rbtnLag.TabIndex = 1;
			this.rbtnLag.Text = "Lag";
			this.rbtnLag.CheckedChanged += new System.EventHandler(this.OnFunctionChanged);
			// 
			// rbtnDif
			// 
			this.rbtnDif.Location = new System.Drawing.Point(80, 40);
			this.rbtnDif.Name = "rbtnDif";
			this.rbtnDif.Size = new System.Drawing.Size(56, 16);
			this.rbtnDif.TabIndex = 2;
			this.rbtnDif.Text = "Dif";
			this.rbtnDif.CheckedChanged += new System.EventHandler(this.OnFunctionChanged);
			// 
			// txtN
			// 
			this.txtN.Location = new System.Drawing.Point(16, 280);
			this.txtN.Maximum = new System.Decimal(new int[] {
																 999,
																 0,
																 0,
																 0});
			this.txtN.Minimum = new System.Decimal(new int[] {
																 1,
																 0,
																 0,
																 0});
			this.txtN.Name = "txtN";
			this.txtN.Size = new System.Drawing.Size(56, 20);
			this.txtN.TabIndex = 6;
			this.txtN.Value = new System.Decimal(new int[] {
															   1,
															   0,
															   0,
															   0});
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(8, 256);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(296, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Number of rows by which to lag/dif:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(240, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Select value to calculate:";
			// 
			// panelLag
			// 
			this.panelLag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panelLag.Controls.Add(this.lblDesc);
			this.panelLag.Controls.Add(this.imgExample);
			this.panelLag.Location = new System.Drawing.Point(8, 64);
			this.panelLag.Name = "panelLag";
			this.panelLag.Size = new System.Drawing.Size(296, 112);
			this.panelLag.TabIndex = 9;
			// 
			// imgExample
			// 
			this.imgExample.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.imgExample.Dock = System.Windows.Forms.DockStyle.Left;
			this.imgExample.Location = new System.Drawing.Point(0, 0);
			this.imgExample.Name = "imgExample";
			this.imgExample.Size = new System.Drawing.Size(168, 112);
			this.imgExample.TabIndex = 0;
			this.imgExample.TabStop = false;
			// 
			// lblDesc
			// 
			this.lblDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblDesc.Location = new System.Drawing.Point(168, 0);
			this.lblDesc.Name = "lblDesc";
			this.lblDesc.Size = new System.Drawing.Size(128, 112);
			this.lblDesc.TabIndex = 1;
			this.lblDesc.Text = "description of function here";
			// 
			// LagForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 350);
			this.Controls.Add(this.panelLag);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtN);
			this.Controls.Add(this.rbtnDif);
			this.Controls.Add(this.rbtnLag);
			this.Controls.Add(this.cmbColumns);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LagForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Calculate Lag and Dif";
			((System.ComponentModel.ISupportInitialize)(this.txtN)).EndInit();
			this.panelLag.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}
