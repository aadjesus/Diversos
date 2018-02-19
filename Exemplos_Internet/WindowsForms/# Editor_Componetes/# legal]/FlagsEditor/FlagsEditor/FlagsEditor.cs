using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace FlagsEditor
{
	internal class FlagsEditor : UITypeEditor
	{
		FlagsEditorControl editor = null;

		public FlagsEditor()
		{ }

		// we tell to the designer host that this editor is a DropDown editor
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			// if value is not an enum than we can not edit it
			if (!(value is Enum))
				throw new Exception("Value doesn't support");

			// try to figure out that is this a Flags enum or not ?
			Type enumType = value.GetType();
			object[] attributes = enumType.GetCustomAttributes(typeof(FlagsAttribute), true);
			if (attributes.Length == 0)
				throw new Exception("Editing enum hasn't got Flags attribute");

			// check the underlying type
			Type type = Enum.GetUnderlyingType(value.GetType());
			if (type != typeof(byte) && type != typeof(sbyte)
				&& type != typeof(short) && type != typeof(ushort)
				&& type != typeof(int) && type != typeof(uint))
				return value;

			if (provider != null)
			{
				// use windows forms editor service to show drop down
				IWindowsFormsEditorService edSvc = provider.GetService(typeof(IWindowsFormsEditorService))
						as IWindowsFormsEditorService;
				if (edSvc == null)
					return value;

				if (editor == null)
					editor = new FlagsEditorControl(this);

				// prepare list
				editor.Begin(edSvc, value);

				// show drop down now
				edSvc.DropDownControl(editor);

				// now we take the result
				value = editor.Value;
				// reset
				editor.End();
			}

			return Convert.ChangeType(value, type);
		}
	}
}
