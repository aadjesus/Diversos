using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using Mvc.Components.Controller;
using Mvc.Components.Model;
using Mvc.Components.Services;

namespace Mvc.Components.Design
{
	/// <summary>
	/// Summary description for ViewMappingsEditorForm.
	/// </summary>
	internal class ViewMappingsEditorForm : System.Windows.Forms.Form
	{
		#region Designer stuff & ctor


		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.Button btnAdd;
		internal System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnlButtons;
		private System.Windows.Forms.StatusBar stStatus;
		private System.Windows.Forms.GroupBox lnStatus;
		internal System.Windows.Forms.ComboBox cbControl;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel pnlGeneral;
		private System.Windows.Forms.ToolTip tpHelp;
		internal System.Windows.Forms.ListBox lstMappings;
		private System.Windows.Forms.ComboBox cbModelProperty;
		private System.Windows.Forms.ComboBox cbModel;
		private System.Windows.Forms.ComboBox cbControlProperty;
		private System.Windows.Forms.Timer tmReady;
		private System.ComponentModel.IContainer components;

		public ViewMappingsEditorForm(IDesigner designer)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			_designer = designer;
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
			this.components = new System.ComponentModel.Container();
			this.lstMappings = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pnlButtons = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.stStatus = new System.Windows.Forms.StatusBar();
			this.lnStatus = new System.Windows.Forms.GroupBox();
			this.pnlGeneral = new System.Windows.Forms.Panel();
			this.cbControl = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.cbModelProperty = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cbModel = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cbControlProperty = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tpHelp = new System.Windows.Forms.ToolTip(this.components);
			this.tmReady = new System.Windows.Forms.Timer(this.components);
			this.pnlButtons.SuspendLayout();
			this.pnlGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstMappings
			// 
			this.lstMappings.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.lstMappings.Location = new System.Drawing.Point(8, 40);
			this.lstMappings.Name = "lstMappings";
			this.lstMappings.Size = new System.Drawing.Size(176, 225);
			this.lstMappings.Sorted = true;
			this.lstMappings.TabIndex = 1;
			this.tpHelp.SetToolTip(this.lstMappings, "List of bindings being edited.");
			this.lstMappings.SelectedIndexChanged += new System.EventHandler(this.lstMappings_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.panel1.BackColor = System.Drawing.Color.Gray;
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(452, 4);
			this.panel1.TabIndex = 2;
			// 
			// pnlButtons
			// 
			this.pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[] {
																																						 this.btnCancel,
																																						 this.btnOK});
			this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlButtons.Location = new System.Drawing.Point(0, 334);
			this.pnlButtons.Name = "pnlButtons";
			this.pnlButtons.Size = new System.Drawing.Size(472, 32);
			this.pnlButtons.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(388, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(308, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Mappings:";
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.btnAdd.Location = new System.Drawing.Point(8, 272);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(84, 24);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "Add";
			this.tpHelp.SetToolTip(this.btnAdd, "Add a new binding.");
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.btnRemove.Location = new System.Drawing.Point(100, 272);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(84, 24);
			this.btnRemove.TabIndex = 6;
			this.btnRemove.Text = "Remove";
			this.tpHelp.SetToolTip(this.btnRemove, "Remove selected binding.");
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox1.Location = new System.Drawing.Point(8, 304);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 4);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(60, 20);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(124, 8);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox3.Location = new System.Drawing.Point(260, 20);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 8);
			this.groupBox3.TabIndex = 10;
			this.groupBox3.TabStop = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(204, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "General:";
			// 
			// stStatus
			// 
			this.stStatus.Location = new System.Drawing.Point(0, 370);
			this.stStatus.Name = "stStatus";
			this.stStatus.Size = new System.Drawing.Size(472, 22);
			this.stStatus.TabIndex = 24;
			this.stStatus.Text = " Ready";
			// 
			// lnStatus
			// 
			this.lnStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lnStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lnStatus.Location = new System.Drawing.Point(0, 366);
			this.lnStatus.Name = "lnStatus";
			this.lnStatus.Size = new System.Drawing.Size(472, 4);
			this.lnStatus.TabIndex = 25;
			this.lnStatus.TabStop = false;
			// 
			// pnlGeneral
			// 
			this.pnlGeneral.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.pnlGeneral.Controls.AddRange(new System.Windows.Forms.Control[] {
																																						 this.cbControl,
																																						 this.label12,
																																						 this.cbModelProperty,
																																						 this.label6,
																																						 this.cbModel,
																																						 this.label5,
																																						 this.cbControlProperty,
																																						 this.label3});
			this.pnlGeneral.Location = new System.Drawing.Point(196, 40);
			this.pnlGeneral.Name = "pnlGeneral";
			this.pnlGeneral.Size = new System.Drawing.Size(266, 220);
			this.pnlGeneral.TabIndex = 26;
			// 
			// cbControl
			// 
			this.cbControl.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.cbControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbControl.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbControl.Location = new System.Drawing.Point(8, 30);
			this.cbControl.Name = "cbControl";
			this.cbControl.Size = new System.Drawing.Size(248, 22);
			this.cbControl.TabIndex = 33;
			this.tpHelp.SetToolTip(this.cbControl, "Control to which this binding applies.");
			this.cbControl.SelectedIndexChanged += new System.EventHandler(this.cbControl_SelectedIndexChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 10);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(44, 16);
			this.label12.TabIndex = 32;
			this.label12.Text = "Control:";
			// 
			// cbModelProperty
			// 
			this.cbModelProperty.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.cbModelProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbModelProperty.Location = new System.Drawing.Point(8, 188);
			this.cbModelProperty.Name = "cbModelProperty";
			this.cbModelProperty.Size = new System.Drawing.Size(156, 21);
			this.cbModelProperty.TabIndex = 31;
			this.tpHelp.SetToolTip(this.cbModelProperty, "Gets/Sets the table field to bind to.");
			this.cbModelProperty.DropDown += new System.EventHandler(this.cbModelProperty_DropDown);
			this.cbModelProperty.SelectedIndexChanged += new System.EventHandler(this.cbModelProperty_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 168);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 16);
			this.label6.TabIndex = 30;
			this.label6.Text = "Model Property:";
			// 
			// cbModel
			// 
			this.cbModel.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.cbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbModel.Location = new System.Drawing.Point(8, 134);
			this.cbModel.Name = "cbModel";
			this.cbModel.Size = new System.Drawing.Size(156, 21);
			this.cbModel.TabIndex = 29;
			this.tpHelp.SetToolTip(this.cbModel, "The table of the appropriate action type (Request/Response).");
			this.cbModel.SelectedIndexChanged += new System.EventHandler(this.cbModel_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 114);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 16);
			this.label5.TabIndex = 28;
			this.label5.Text = "Model:";
			// 
			// cbControlProperty
			// 
			this.cbControlProperty.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.cbControlProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbControlProperty.Location = new System.Drawing.Point(8, 82);
			this.cbControlProperty.Name = "cbControlProperty";
			this.cbControlProperty.Size = new System.Drawing.Size(156, 21);
			this.cbControlProperty.TabIndex = 26;
			this.tpHelp.SetToolTip(this.cbControlProperty, "Action binded to the control.");
			this.cbControlProperty.DropDown += new System.EventHandler(this.cbControlProperty_DropDown);
			this.cbControlProperty.SelectedIndexChanged += new System.EventHandler(this.cbControlProperty_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 16);
			this.label3.TabIndex = 24;
			this.label3.Text = "Control Property:";
			// 
			// tmReady
			// 
			this.tmReady.Interval = 5000;
			// 
			// ViewMappingsEditorForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(472, 392);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.pnlGeneral,
																																	this.groupBox3,
																																	this.label2,
																																	this.groupBox2,
																																	this.groupBox1,
																																	this.btnRemove,
																																	this.btnAdd,
																																	this.label1,
																																	this.pnlButtons,
																																	this.panel1,
																																	this.lstMappings,
																																	this.lnStatus,
																																	this.stStatus});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(480, 400);
			this.Name = "ViewMappingsEditorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "View Mappings Editor";
			this.Load += new System.EventHandler(this.ViewMappingsEditorForm_Load);
			this.pnlButtons.ResumeLayout(false);
			this.pnlGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region Field declarations
		/// <summary>
		/// A reference to the designer for the component. 
		/// </summary>
		IDesigner _designer;

		/// <summary>
		/// Resolves references to controls on the page.
		/// </summary>
		IReferenceService _reference;

		/// <summary>
		/// Resolves type loading at design-time.
		/// </summary>
		ITypeResolutionService _resolution;

		#endregion

		#region Loading and initialization methods

		void ViewMappingsEditorForm_Load(object sender, System.EventArgs e)
		{			
			_reference = (IReferenceService) 
				_designer.Component.Site.GetService(typeof(IReferenceService));
			_resolution = (ITypeResolutionService) 
			_designer.Component.Site.GetService(typeof(ITypeResolutionService));

			InitControls(null);
			//Don't span new threads if you need to get services from the 
			//host inside the pooled method, because GetService must 
			//called from the main application thread!!
			//ThreadPool.QueueUserWorkItem(new WaitCallback(InitControls));	
		}


		/// <summary>
		/// Loads all controls in the page.
		/// </summary>
		void InitControls(object state)
		{
			try
			{
				//We always traverse items because new controls can be added at any time.
				stStatus.Text = " Loading controls... ";

				IAdapterService ad = (IAdapterService) 
					_designer.Component.Site.GetService(typeof(IAdapterService));

				IDesignerHost host = (IDesignerHost)
					_designer.Component.Site.GetService(typeof(IDesignerHost));

				foreach (object control in ad.GetControls())
				{
					cbControl.Items.Add(new ControlEntry(control, _reference.GetName(control)));
				}

				//Load models in the controller.
				foreach (IComponent comp in ((IContainer)_designer.Component).Components)
					if (comp is BaseModel) cbModel.Items.Add(comp);

				stStatus.Text = " Ready";
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}
		}

		/// <summary>
		/// Clears all combos from the form (except cbControl)
		/// </summary>
		void ClearControlDependent()
		{
			cbControlProperty.Items.Clear();
			cbControlProperty.Text = String.Empty;
			cbModel.Items.Clear();
			cbModel.Text = String.Empty;
			cbModelProperty.Items.Clear();
			cbModelProperty.Text = String.Empty;
		}

		#endregion

		#region Add/Remove buttons
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			int pos = lstMappings.Items.Add(new ViewInfo());
			lstMappings.SelectedIndex = pos;
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			lstMappings.Items.Remove(lstMappings.SelectedItem);
		}

		#endregion

		#region Changes tracking
		private void lstMappings_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				ViewInfo view = lstMappings.SelectedItem as ViewInfo;

				if (view == null)
				{
					pnlGeneral.Enabled = false;
					cbControl.SelectedIndex = -1;
					cbControlProperty.SelectedIndex = -1;
					cbModel.SelectedIndex = -1;
					return;
				}

				pnlGeneral.Enabled = true;
				//Reference to control
				if (view.ControlID != String.Empty)
				{
					//EXPLAIN: always go through the IReferenceService
					object ctl = _reference.GetReference(view.ControlID);
					if (ctl == null)
					{
						view.ControlID = String.Empty;
					}
					else
					{
						cbControl.SelectedIndex = cbControl.FindStringExact(new ControlEntry(ctl, _reference.GetName(ctl)).ToString());
					}
				}

				if (view.ControlProperty != String.Empty)
					cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact(view.ControlProperty);
				else
					cbControlProperty.SelectedIndex = -1;

				cbModel.SelectedIndex = cbModel.FindStringExact(view.Model);
				cbModelProperty.SelectedIndex = cbModelProperty.FindStringExact(view.ModelProperty);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}
		}

		private void cbControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (lstMappings.SelectedItem == null)
					return;

				if (cbControl.SelectedItem != null)
					((ViewInfo)lstMappings.SelectedItem).ControlID = 
						((ControlEntry)cbControl.SelectedItem).Name;

				//Cause reloading of control properties.
				cbControlProperty_DropDown(sender, e);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}
		}

		private void cbControlProperty_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (lstMappings.SelectedItem == null)
					return;

				if (cbControlProperty.SelectedItem != null)
					((ViewInfo)lstMappings.SelectedItem).ControlProperty = cbControlProperty.Text;
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}
		}

		private void cbModel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (lstMappings.SelectedItem == null)
					return; 

				if (cbModel.SelectedItem != null)
					((ViewInfo)lstMappings.SelectedItem).Model = cbModel.Text;

				//Cause reloading of model properties.
				cbModelProperty_DropDown(sender, e);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}
		}

		private void cbModelProperty_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (lstMappings.SelectedItem == null)
					return; 

				if (cbModelProperty.SelectedItem != null)
					((ViewInfo)lstMappings.SelectedItem).ModelProperty = cbModelProperty.Text;
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}
		}

		private void cbControlProperty_DropDown(object sender, System.EventArgs e)
		{
			cbControlProperty.Items.Clear();

			try
			{
				if (lstMappings.SelectedItem == null || cbControl.SelectedItem == null)
					return;
				
				//EXPLAIN: why we would have to duplicate code with the existing converters. Context for GetStandardValues in converters.
				//That's why we moved it up to a first-class service.
				IControllerService svc = (IControllerService)
					_designer.Component.Site.GetService(typeof(IControllerService));
				object control = ((ControlEntry) cbControl.SelectedItem).Control;
				cbControlProperty.Items.Clear();
				cbControlProperty.Text = String.Empty;
				cbControlProperty.Items.AddRange(svc.GetControlProperties(control));

				ViewInfo view = (ViewInfo) lstMappings.SelectedItem;

				if (view.ControlProperty != String.Empty)
					cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact(view.ControlProperty);
				else
					cbControlProperty.SelectedIndex = -1;
			}			
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}		
		}

		private void cbModelProperty_DropDown(object sender, System.EventArgs e)
		{
			cbModelProperty.Items.Clear();

			try
			{
				if (lstMappings.SelectedItem == null || cbModel.SelectedItem == null)
					return;

				//EXPLAIN: the same case as with control properties. 
				IControllerService svc = (IControllerService)
					_designer.Component.Site.GetService(typeof(IControllerService));
				BaseModel model = (BaseModel) cbModel.SelectedItem;
				cbModelProperty.Items.Clear();
				cbModelProperty.Text = String.Empty;
				cbModelProperty.Items.AddRange(svc.GetModelProperties(model));

				ViewInfo view = (ViewInfo) lstMappings.SelectedItem;

				if (view.ModelProperty != String.Empty)
					cbModelProperty.SelectedIndex = cbModelProperty.FindStringExact(view.ModelProperty);
				else
					cbModelProperty.SelectedIndex = -1;
			}			
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}		
		}

		#endregion

		#region Status bar notifications
		private void stStatus_TextChanged(object sender, System.EventArgs e)
		{
			tmReady.Enabled = true;
		}

		private void tmReady_Tick(object sender, System.EventArgs e)
		{
			stStatus.Text = " Ready";
			tmReady.Enabled = false;
		}
		#endregion

		#region ControlEntry
		struct ControlEntry
		{
			public object Control;
			public string Name;

			public ControlEntry(object control, string name)
			{
				this.Control = control;
				this.Name = name;
			}
			public override string ToString()
			{
				string id = this.Name.PadRight(20, ' ');
				return id + " [" + Control.GetType().Name + "]";
			}
		}
		#endregion
	}
}
