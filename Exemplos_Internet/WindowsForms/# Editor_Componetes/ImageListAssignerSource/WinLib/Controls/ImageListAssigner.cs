using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using WinLib.Controls.Form;

namespace WinLib.Controls {
	public class ImageListAssigner : System.ComponentModel.Component {
		private System.ComponentModel.Container components = null;
		private const string Identity = "ImageList Assigner";
		private ImageList FDestination = null;
		private ImageListAssignerSelector FSelector;

		[DefaultValue(null)]
		public ImageList Destination {
			get { return FDestination; }
			set { FDestination = value; }
		}

		[Description("Double-Click to show Assigner"), DefaultValue(true)]
		public bool ShowDesigner {
			get { return true; }
			set { ShowDesignerForm(); }
		}

		private void ShowDesignerForm() {
			FSelector.Assigner = this;
			FSelector.ShowDialog();
		}

		private void ImageListInit() {
			FSelector = new ImageListAssignerSelector();
		}

		public ImageListAssigner(System.ComponentModel.IContainer container) {
			container.Add(this);
			InitializeComponent();
			ImageListInit();
			if (!DesignMode) {
				MessageBox.Show("The ImageListAssigner not needed in run-time.\nThe Assigner should be run in design mode only.",Identity);
			}
		}

		public ImageListAssigner() {
			InitializeComponent();
			ImageListInit();
		}

		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
