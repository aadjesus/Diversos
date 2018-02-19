using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Drawing.Design;

namespace FlagsEditor
{
	public class ExampleComponent : Component
	{
		FileAttributes _FlagsAttribute = FileAttributes.Archive;
		FileAttributes _NonFlagsAttribute = FileAttributes.Archive;

		// set editor of this property to our FlagsEditor
		[Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
		[Description("You can select more than one value for this property")]
		[Category("Behaviour")]
		[DefaultValue(FileAttributes.Archive)]
		public FileAttributes FlagsAttribute
		{
			get { return _FlagsAttribute; }
			set { _FlagsAttribute = value; }
		}

		// do not set any editor, so PropertyGrid will use default 
		// enum editor for this property, which does not allow
		// select more than one value from the list
		[Description("You cannot select more than one value for this property")]
		[Category("Behaviour")]
		[DefaultValue(FileAttributes.Archive)]
		public FileAttributes NonFlagsAttribute
		{
			get { return _NonFlagsAttribute; }
			set { _NonFlagsAttribute = value; }
		}
	}
}
