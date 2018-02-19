using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Snippy
{
	public class LiteralForm : Form
	{
		// Fields
		private Button btnCancel;
		private Button btnOK;
		private CheckBox chbEditable;
		private ComboBox comboBoxDeclarationType;
		private Container components = null;
		private GroupBox groupBox1;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private TextBox txtDefaultValue;
		private TextBox txtFunction;
		private TextBox txtID;
		private TextBox txtTooltip;

		// Methods
		public LiteralForm()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.groupBox1 = new GroupBox();
			this.comboBoxDeclarationType = new ComboBox();
			this.label5 = new Label();
			this.txtFunction = new TextBox();
			this.txtDefaultValue = new TextBox();
			this.txtTooltip = new TextBox();
			this.txtID = new TextBox();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.chbEditable = new CheckBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.comboBoxDeclarationType);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtFunction);
			this.groupBox1.Controls.Add(this.txtDefaultValue);
			this.groupBox1.Controls.Add(this.txtTooltip);
			this.groupBox1.Controls.Add(this.txtID);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.groupBox1.Location = new Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(0x178, 0x9f);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.comboBoxDeclarationType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxDeclarationType.FormattingEnabled = true;
			this.comboBoxDeclarationType.Items.AddRange(new object[] { "Literal", "Object" });
			this.comboBoxDeclarationType.Location = new Point(120, 130);
			this.comboBoxDeclarationType.Name = "comboBoxDeclarationType";
			this.comboBoxDeclarationType.Size = new Size(240, 0x16);
			this.comboBoxDeclarationType.Sorted = true;
			this.comboBoxDeclarationType.TabIndex = 10;
			this.label5.Location = new Point(12, 0x84);
			this.label5.Name = "label5";
			this.label5.Size = new Size(0x68, 20);
			this.label5.TabIndex = 8;
			this.label5.Text = "&Declaration Type:";
			this.txtFunction.Location = new Point(120, 0x65);
			this.txtFunction.Name = "txtFunction";
			this.txtFunction.Size = new Size(240, 0x16);
			this.txtFunction.TabIndex = 7;
			this.txtDefaultValue.Location = new Point(120, 0x49);
			this.txtDefaultValue.Name = "txtDefaultValue";
			this.txtDefaultValue.Size = new Size(240, 0x16);
			this.txtDefaultValue.TabIndex = 5;
			this.txtTooltip.Location = new Point(120, 0x2d);
			this.txtTooltip.Name = "txtTooltip";
			this.txtTooltip.Size = new Size(240, 0x16);
			this.txtTooltip.TabIndex = 3;
			this.txtID.Location = new Point(120, 0x11);
			this.txtID.Name = "txtID";
			this.txtID.Size = new Size(240, 0x16);
			this.txtID.TabIndex = 1;
			this.label4.Location = new Point(12, 0x68);
			this.label4.Name = "label4";
			this.label4.Size = new Size(0x4c, 20);
			this.label4.TabIndex = 6;
			this.label4.Text = "&Function:";
			this.label3.Location = new Point(12, 0x4c);
			this.label3.Name = "label3";
			this.label3.Size = new Size(0x68, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "&Default Value:";
			this.label2.Location = new Point(12, 0x30);
			this.label2.Name = "label2";
			this.label2.Size = new Size(60, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "&ToolTip:";
			this.label1.Location = new Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new Size(0x24, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "&ID:";
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = FlatStyle.System;
			this.btnOK.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnOK.Location = new Point(390, 12);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new Size(0x4b, 0x17);
			this.btnOK.TabIndex = 10;
			this.btnOK.Text = "&OK";
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = FlatStyle.System;
			this.btnCancel.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnCancel.Location = new Point(390, 40);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(0x4b, 0x17);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "&Cancel";
			this.chbEditable.Checked = true;
			this.chbEditable.CheckState = CheckState.Checked;
			this.chbEditable.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chbEditable.Location = new Point(8, 0xad);
			this.chbEditable.Name = "chbEditable";
			this.chbEditable.Size = new Size(360, 0x18);
			this.chbEditable.TabIndex = 8;
			this.chbEditable.Text = "&Editable - Allow users to tab through to this literal";
			base.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new Size(6, 15);
			base.CancelButton = this.btnCancel;
			base.ClientSize = new Size(0x1da, 0xd1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.chbEditable);
			this.Font = new Font("Lucida Sans", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LiteralForm";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Literal Editor - Snippy";
			base.Load += new EventHandler(this.LiteralForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		private void LiteralForm_Load(object sender, EventArgs e)
		{
		}

		// Properties
		public string DefaultValue
		{
			get
			{
				return this.txtDefaultValue.Text;
			}
			set
			{
				this.txtDefaultValue.Text = value;
			}
		}

		public bool Editable
		{
			get
			{
				return this.chbEditable.Checked;
			}
			set
			{
				this.chbEditable.Checked = value;
			}
		}

		public string Function
		{
			get
			{
				return this.txtFunction.Text;
			}
			set
			{
				this.txtFunction.Text = value;
			}
		}

		public string ID
		{
			get
			{
				return this.txtID.Text;
			}
			set
			{
				this.txtID.Text = value;
			}
		}

		public bool IsObject
		{
			get
			{
				return (this.comboBoxDeclarationType.SelectedIndex == 1);
			}
			set
			{
				if (value)
				{
					this.comboBoxDeclarationType.SelectedIndex = 1;
				}
				else
				{
					this.comboBoxDeclarationType.SelectedIndex = 0;
				}
			}
		}

		public string Tooltip
		{
			get
			{
				return this.txtTooltip.Text;
			}
			set
			{
				this.txtTooltip.Text = value;
			}
		}
	}

 

}
