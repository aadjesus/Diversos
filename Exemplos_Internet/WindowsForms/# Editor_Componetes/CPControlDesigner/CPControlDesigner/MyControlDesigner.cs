/*
 * Erstellt mit SharpDevelop.
 * Benutzer: hmurray
 * Datum: 01.07.2010
 * Zeit: 23:19
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
 
using System;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace CPControlDesigner.Design
{
	/// <summary>
	/// Description of NavigationBarDesigner.
	/// </summary>
	[System.Security.Permissions.PermissionSet
	(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	public class MyControlDesigner : System.Windows.Forms.Design.ControlDesigner
	{
		private MyControl control ;
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			
			control = component as MyControl;
		}

        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            PropertyDescriptor colorPropDesc =
                TypeDescriptor.GetProperties(Component)["Name"];

            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            Design.NameCreationService naming = new NameCreationService(host);
            string name = naming.CreateName(host.Container, Component.GetType());

            if (colorPropDesc != null &&
                colorPropDesc.PropertyType == typeof(string) &&
                !colorPropDesc.IsReadOnly &&
                colorPropDesc.IsBrowsable)
            {
                colorPropDesc.SetValue(Component, name);
            }

        }
	}
}