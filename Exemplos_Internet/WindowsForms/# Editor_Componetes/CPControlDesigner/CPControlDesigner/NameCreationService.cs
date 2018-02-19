/*
 * Erstellt mit SharpDevelop.
 * Benutzer: hmurray
 * Datum: 01.07.2010
 * Zeit: 23:19
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
 
using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CPControlDesigner.Design
{
	/// <summary>
	/// Summary description for NameCreationServiceImpl.
	/// </summary>
	public class NameCreationService : INameCreationService
	{
		private IDesignerHost designerHost;
		public NameCreationService(IDesignerHost host)
		{
			if(host==null)
			{
				throw new ArgumentException("DesignerHost");
			}
			designerHost=host;
		}
		
		#region INameCreationService Members
		/// <summary>
		/// creates a unique name from the given container and dataType
		/// </summary>
		/// <param name="container"></param>
		/// <param name="dataType"></param>
		/// <returns></returns>
		public string CreateName(IContainer container, Type dataType)
		{
			if(container==null)
			{
				throw new ArgumentException("container");
			}
			if(dataType==null)
			{
				throw new ArgumentException("dataType");
			}
			
			string name = string.Empty;
			
            if(dataType == typeof(MyControl))
			{
				// Prompt for the name using a Dialog
				name = PromptName(container, dataType);
			}
			else
			{
				// use the normal way to generate the name
				name = GenerateDefaultName(container, dataType);
			}
			
			return name;
		}
		
		public string GenerateDefaultName(IContainer container, Type dataType)
		{
			// look to see if the container already has this type
			// of component, if it does, then iterate until you
			// find a unique name
			int count = 1;
			string name = dataType.Name + count.ToString();
			if(container.Components[name]!=null)
			{
				for(int i=1; i<container.Components.Count; i++)
				{
					name = dataType.Name + (i+1).ToString();
					if(container.Components[name]==null)
					{
						break;
					}
				}
			}
			
			return name;
		}

		public string PromptName(IContainer container, Type dataType)
		{
			string name = dataType.Name;
			do
			{
				using (EnterControlNameDialog dlg = new EnterControlNameDialog())
				{
					dlg.ControlName = name;
					
					DialogResult dlgRes = dlg.ShowDialog(container as IWin32Window);
					if(dlgRes == DialogResult.OK)
					{
						name = dlg.ControlName;
					}
					else
					{
						name = GenerateDefaultName(container, dataType);
					}
				}
			}while(!IsValidName(name));
			
			return name;
		}
		
		public bool IsValidName(string name)
		{
			ValidateName(name);
			return true;
		}

		public void ValidateName(string name)
		{
			// iterate the comps in the component container and 
			// make sure that the name is not used already
			if(designerHost.Container==null)
			{
				throw new Exception("Null container.");
			}
			// if we have some components
			if(designerHost.Container.Components!=null &&
				designerHost.Container.Components.Count>0)
			{
				foreach(IComponent comp in designerHost.Container.Components)
				{
					if(string.Compare(name,comp.Site.Name)==0)
					{
						throw new Exception("Name alreay in use.");
					}
				}
			}
		}

		#endregion
	}
}
