//***************************************************************
//This class is used for creating the custom type editor. For creating 
//a custom editor we need to override Editvalue and GetEditStyle methods.					
//***************************************************************

using System;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CustomEditor
{
	/// <summary>
	/// Summary description for WidthEditor.
	/// </summary>
	public class WidthEditor : UITypeEditor
	{
		public WidthEditor()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{

			return UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			//use IWindowsFormsEditorService object to display a control in the dropdown area
		    IWindowsFormsEditorService frmsvr = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if(frmsvr == null)
				return null;
			TrackBar tbr = new TrackBar();
			tbr.Orientation = Orientation.Vertical;
			tbr.Size = new Size(60,120);
			tbr.TickFrequency = 50;
			tbr.SetRange(10,300);
			frmsvr.DropDownControl(tbr);
			return tbr.Value;
						
		}

        

	}
}
