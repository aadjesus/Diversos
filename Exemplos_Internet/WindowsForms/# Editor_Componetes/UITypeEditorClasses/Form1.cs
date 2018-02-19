using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Security.Permissions;

namespace WindowsApplication1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
    {
		private System.Windows.Forms.PropertyGrid grid;
        private Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.grid = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.grid.Location = new System.Drawing.Point(15, 102);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(398, 234);
            this.grid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 75);
            this.label1.TabIndex = 2;
            this.label1.Text = "Change the properties of the object in the PropertyGrid below. Properties which a" +
                "re flagged enums can be editing using a check list box.";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(425, 348);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grid);
            this.Name = "Form1";
            this.Text = "FlagEnumEditor Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

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

		private void Form1_Load(object sender, System.EventArgs e)
		{
			grid.SelectedObject = new TestObject();
		
		}
	}

	[Flags]
	enum TestEnum
	{
		None,
        Left=1,
        Right=2,
        Bottom=4,
        Top=8,
        TopLeft = Top | Left,
        BottomRight = Bottom | Right,
        All = Left | Right | Bottom | Top,


	}

	class TestObject
	{
		TestEnum testEnum;
		SecurityPermissionFlag securityPermissionFlag;
        FontStyle fontStyle;

        [Editor(typeof(Utils.FlagEnumUIEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public FontStyle FontStyleProperty
        {
            get
            {
                return fontStyle;
            }
            set
            {
                fontStyle = value;
            }
        }

		[Editor(typeof(Utils.FlagEnumUIEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public SecurityPermissionFlag SecurityPermissionFlagProperty
		{
			get
			{
				return securityPermissionFlag;
			}
			set
			{
				securityPermissionFlag = value;
			}
		}

        [Editor(typeof(Utils.FlagEnumUIEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public TestEnum TestEnumProperty
		{
			get
			{
				return testEnum;
			}
			set
			{
				testEnum = value;
			}
		}
	}
}
