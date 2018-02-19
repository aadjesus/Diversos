// This class create a custom designer for PhoneNumbercontrol.
//This custom designer is used for changing the look and feel 
//of the control.

using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;


namespace Phone_Number_Control
{
	/// <summary>
	/// Summary description for PhoneNumberControlDesigner.
	/// </summary>
	public class PhoneNumberControlDesigner :ControlDesigner
	{

		public PhoneNumberControlDesigner()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region UserDefinedVariables
		private DesignerVerbCollection actions;
		public override DesignerVerbCollection Verbs
		{
			get
			{
				if(actions == null)
				{
					actions = new DesignerVerbCollection();
					actions.Add(new DesignerVerb("Look and Feel", new EventHandler(ChangeDisplay)));
				}
				return actions;
			}
		}

		#endregion

		#region EventHandlers
		void ChangeDisplay(object sender, EventArgs e)
		{
			//Control.BackColor = Color.Chocolate;
			Formater formt = new Formater();
			formt.ShowDialog();
			Control.BackColor = formt.Style.BackGroudColor;
			Control.Font = new Font("Microsoft Sans Serif",8.25f,formt.Style.ForeStyle);
		}
		#endregion
	}
}
