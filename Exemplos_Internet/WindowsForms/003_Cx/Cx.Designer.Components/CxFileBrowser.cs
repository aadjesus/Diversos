using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;
using Cx.WinForm;

namespace Cx.Designer.Components
{
	[CxComponentName("CxFileBrowser")]
	[CxExplicitEvent("FilenameSet")]
	public partial class CxFileBrowser : UserControl, ICxVisualComponentClass
	{
		[CxComponentProperty]
		public string Label
		{
			get { return lblLabel.Text; }
			set { lblLabel.Text = value; }
		}

		[CxComponentProperty]
		public string Filter { get; set; }

		protected EventHelper filenameSet;

		public CxFileBrowser()
		{
			InitializeComponent();
			filenameSet = EventHelpers.Transform(this, tbFilename, "LostFocus", "Text").To("FilenameSet");
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}

		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = Filter;
			ofd.RestoreDirectory = true;

			try
			{
				ofd.InitialDirectory = Path.GetDirectoryName(tbFilename.Text);
			}
			catch { }

			DialogResult ret = ofd.ShowDialog();

			if (ret == DialogResult.OK)
			{
				tbFilename.Text=ofd.FileName;
				filenameSet.Fire(ofd.FileName);
			}
		}
	}
}
