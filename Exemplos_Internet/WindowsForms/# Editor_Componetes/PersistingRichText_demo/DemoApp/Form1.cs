using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DemoApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Extended.Windows.Forms.RichTextBoxExtended richTextBoxExtended1;
		private DemoApp.Form1_Polyhedra form1_Polyhedra1;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private Extended.Windows.Forms.RichTextBoxExtended richTextBoxExtended2;
		internal System.Windows.Forms.ImageList ImageList1;
		internal System.Windows.Forms.ToolBar ToolBar1;
		internal System.Windows.Forms.ToolBarButton ToolBarButton1;
		internal System.Windows.Forms.ToolBarButton ToolBarButton2;
		internal System.Windows.Forms.ToolBarButton ToolBarButton3;
		internal System.Windows.Forms.ToolBarButton ToolBarButton4;
		internal System.Windows.Forms.ToolBarButton ToolBarButton5;
		internal System.Windows.Forms.ToolBarButton ToolBarButton6;
		internal System.Windows.Forms.ToolBarButton ToolBarButton7;
		internal System.Windows.Forms.ToolBarButton ToolBarButton8;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.form1_Polyhedra1.ReadXml(@"..\..\polyhedra.xml");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBoxExtended1 = new Extended.Windows.Forms.RichTextBoxExtended();
            this.form1_Polyhedra1 = new DemoApp.Form1_Polyhedra();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBoxExtended2 = new Extended.Windows.Forms.RichTextBoxExtended();
            this.ImageList1 = new System.Windows.Forms.ImageList();
            this.ToolBar1 = new System.Windows.Forms.ToolBar();
            this.ToolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton6 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton7 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton8 = new System.Windows.Forms.ToolBarButton();
            ((System.ComponentModel.ISupportInitialize)(this.form1_Polyhedra1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxExtended1
            // 
            this.richTextBoxExtended1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxExtended1.DataBindings.Add(new System.Windows.Forms.Binding("Rtf", this.form1_Polyhedra1, "Polyhedra.Description", true));
            this.richTextBoxExtended1.Location = new System.Drawing.Point(0, 90);
            this.richTextBoxExtended1.Name = "richTextBoxExtended1";
            this.richTextBoxExtended1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft S" +
    "ans Serif|;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs16\\par\r\n}\r\n";
            this.richTextBoxExtended1.ShowOpen = false;
            this.richTextBoxExtended1.ShowSave = false;
            this.richTextBoxExtended1.Size = new System.Drawing.Size(552, 222);
            this.richTextBoxExtended1.TabIndex = 0;
            // 
            // form1_Polyhedra1
            // 
            this.form1_Polyhedra1.DataSetName = "Form1_Polyhedra";
            this.form1_Polyhedra1.EnforceConstraints = false;
            this.form1_Polyhedra1.Locale = new System.Globalization.CultureInfo("en-IE");
            this.form1_Polyhedra1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataMember = "Polyhedra";
            this.errorProvider1.DataSource = this.form1_Polyhedra1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.form1_Polyhedra1, "Polyhedra.Name", true));
            this.textBox1.Location = new System.Drawing.Point(87, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Description";
            // 
            // richTextBoxExtended2
            // 
            this.richTextBoxExtended2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxExtended2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxExtended2.EditorBackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxExtended2.Location = new System.Drawing.Point(307, 28);
            this.richTextBoxExtended2.Name = "richTextBoxExtended2";
            this.richTextBoxExtended2.ReadOnly = true;
            this.richTextBoxExtended2.Rtf = resources.GetString("richTextBoxExtended2.Rtf");
            this.richTextBoxExtended2.Size = new System.Drawing.Size(238, 62);
            this.richTextBoxExtended2.TabIndex = 5;
            this.richTextBoxExtended2.ToolbarVisible = false;
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            this.ImageList1.Images.SetKeyName(2, "");
            this.ImageList1.Images.SetKeyName(3, "");
            this.ImageList1.Images.SetKeyName(4, "");
            this.ImageList1.Images.SetKeyName(5, "");
            this.ImageList1.Images.SetKeyName(6, "");
            this.ImageList1.Images.SetKeyName(7, "");
            // 
            // ToolBar1
            // 
            this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButton1,
            this.ToolBarButton2,
            this.ToolBarButton3,
            this.ToolBarButton4,
            this.ToolBarButton5,
            this.ToolBarButton6,
            this.ToolBarButton7,
            this.ToolBarButton8});
            this.ToolBar1.DropDownArrows = true;
            this.ToolBar1.ImageList = this.ImageList1;
            this.ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ToolBar1.Name = "ToolBar1";
            this.ToolBar1.ShowToolTips = true;
            this.ToolBar1.Size = new System.Drawing.Size(552, 28);
            this.ToolBar1.TabIndex = 8;
            this.ToolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar1_ButtonClick);
            // 
            // ToolBarButton1
            // 
            this.ToolBarButton1.ImageIndex = 0;
            this.ToolBarButton1.Name = "ToolBarButton1";
            this.ToolBarButton1.ToolTipText = "First";
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.ImageIndex = 1;
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.ToolTipText = "Last";
            // 
            // ToolBarButton3
            // 
            this.ToolBarButton3.ImageIndex = 2;
            this.ToolBarButton3.Name = "ToolBarButton3";
            this.ToolBarButton3.ToolTipText = "Previous";
            // 
            // ToolBarButton4
            // 
            this.ToolBarButton4.ImageIndex = 3;
            this.ToolBarButton4.Name = "ToolBarButton4";
            this.ToolBarButton4.ToolTipText = "Next";
            // 
            // ToolBarButton5
            // 
            this.ToolBarButton5.ImageIndex = 4;
            this.ToolBarButton5.Name = "ToolBarButton5";
            this.ToolBarButton5.ToolTipText = "Add";
            // 
            // ToolBarButton6
            // 
            this.ToolBarButton6.ImageIndex = 5;
            this.ToolBarButton6.Name = "ToolBarButton6";
            this.ToolBarButton6.ToolTipText = "Delete";
            // 
            // ToolBarButton7
            // 
            this.ToolBarButton7.ImageIndex = 6;
            this.ToolBarButton7.Name = "ToolBarButton7";
            this.ToolBarButton7.ToolTipText = "Reload";
            // 
            // ToolBarButton8
            // 
            this.ToolBarButton8.ImageIndex = 7;
            this.ToolBarButton8.Name = "ToolBarButton8";
            this.ToolBarButton8.ToolTipText = "Save";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(552, 312);
            this.Controls.Add(this.ToolBar1);
            this.Controls.Add(this.richTextBoxExtended2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxExtended1);
            this.Name = "Form1";
            this.Text = "Persisting RichText Demo Application";
            ((System.ComponentModel.ISupportInitialize)(this.form1_Polyhedra1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void ToolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			BindingManagerBase bm= this.BindingContext[this.form1_Polyhedra1,"Polyhedra"];
			switch (e.Button.ImageIndex)
			{
				case 0: // First
					if (bm.Count>0)
						bm.Position= 0;
					break;
				case 1: // Last
					if (bm.Count>0)
						bm.Position= bm.Count-1;
					break;
				case 2: // Previous
					if (bm.Position > 0)
						bm.Position = bm.Position - 1;
					break;
				case 3: // Next
                    if (bm.Position < bm.Count)
						bm.Position = bm.Position + 1;
					break;
				case 4: // Add
					bm.AddNew();
					break;
				case 5: // Delete
					if (bm.Position>=0)
						bm.RemoveAt(bm.Position);
					break;
				case 6: // Reload
					this.form1_Polyhedra1.ReadXml(@"..\..\polyhedra.xml");
					break;
				case 7: // Save
					this.form1_Polyhedra1.WriteXml(@"..\..\polyhedra.xml");
					break;

			}
		}
	}
}
