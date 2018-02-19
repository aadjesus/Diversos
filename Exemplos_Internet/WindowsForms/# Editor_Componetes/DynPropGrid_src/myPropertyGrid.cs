using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Testproperty
{
	public class myPropertyGrid : PropertyGrid
	{

		private System.ComponentModel.Container components = null;

		public myPropertyGrid()
		{			
			InitializeComponent();			
		}

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

		#region Codice generato da Progettazione componenti
		/// <summary> 
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// UserControl1
			// 
			this.Name = "myPropertyGrid";			

		}
		#endregion		
		
		protected override PropertyTab CreatePropertyTab(Type tabType)
		{			
			myTab t = new myTab();
			return t;
		}			
	}	

	public class myTab : PropertyTab
	{
		public myTab()
		{            
		}

		// get the properties of the selected component
		public override System.ComponentModel.PropertyDescriptorCollection GetProperties(object component, System.Attribute[] attributes)
		{
			PropertyDescriptorCollection properties;
			if(attributes!=null)
				properties=TypeDescriptor.GetProperties(component,attributes);    			
			else
				properties=TypeDescriptor.GetProperties(component);    
            
			//Componet must implement the ICUSTOMCLASS interface.
			ICustomClass bclass=(ICustomClass)component;	

			//The new array of properties, based on the PublicProperties properties of "model"
			PropertyDescriptor[] arrProp = new PropertyDescriptor[bclass.PublicProperties.Count];            							
				
			for (int i=0;i<bclass.PublicProperties.Count;i++)
			{
				//Find the properties in the array of the propertis which neme is in the PubliCProperties
				PropertyDescriptor prop=properties.Find(bclass.PublicProperties[i].Name,true);	
				//Build a new properties
				arrProp[i] = TypeDescriptor.CreateProperty(prop.ComponentType, prop, new CategoryAttribute(bclass.PublicProperties[i].Category));
			}
			return new PropertyDescriptorCollection(arrProp);
		}

		public override System.ComponentModel.PropertyDescriptorCollection GetProperties(object component)
		{                     
			return this.GetProperties(component,null);
		}

		// PropertyTab Name
		public override string TabName
		{
			get
			{
				return "Properties";
			}
		}

		//Image of the property tab (return a blank 16x16 Bitmap)
		public override System.Drawing.Bitmap Bitmap
		{
			get
			{				
				return new Bitmap(16,16);;
			}
		}
	}


}
