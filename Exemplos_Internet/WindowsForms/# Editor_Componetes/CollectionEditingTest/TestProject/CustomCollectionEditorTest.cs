using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using Test.Items;
using Test.CollectionEditors;
using Test.Collections;
using Test.TypeConverters;
using CustomControls.CollectionEditor;


namespace Test
{
	namespace Items
	{
		#region "Items"

		public class TestItem
		{
			public TestItem(int e, string t)
			{}
		}

		[TypeConverter(typeof(CustomItemConverter))]
		public class CustomItem:SimpleItem
		{
			private SpecialItems _SpecialItems= new SpecialItems();
			private Color _Color=Color.SteelBlue;

			[Category("TestProperties")]
			[DefaultValue(typeof(Color),"SteelBlue")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
			public Color Color
			{
				get{return _Color;}
				set{_Color=value;}
			}
	
			[Category ("Collections")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
			[Editor(typeof(CustomItemCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
			public  SpecialItems SpecialItems
			{
				get{return _SpecialItems;}
			}

			public CustomItem():base()
			{}

			public CustomItem(int Id, string Name,SpecialItem[] specialItems):base(Id,Name)
			{
				this.SpecialItems.AddRange(specialItems);
			}
		
		}
	

		[TypeConverter(typeof(SpecialItemConverter))]
		public class SpecialItem
		{
			private string _Speciality=string.Empty;
			private int _Cod=-1;
			private SpecialItems _SubItems= new SpecialItems();

			[Category("TestProperties")]
			[DefaultValue(typeof(string),"")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
			public string Speciality
			{
				get{return _Speciality;}
				set{_Speciality= value;}
			}

			[Category("TestProperties")]
			[DefaultValue(typeof(int),"-1")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
			public int Cod
			{
				get{return _Cod;}
				set{_Cod= value;}
			}


			[Category ("Collections")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
			[Editor(typeof(CustomItemCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
			public  SpecialItems SubItems
			{
				get{return _SubItems;}
			}

			public SpecialItem()
			{
		
			}


			public SpecialItem(int Cod, string Speciality, SpecialItem[] specialItems)
			{
				this._Cod=Cod;
				this._Speciality= Speciality;
				this._SubItems.AddRange(specialItems);
			}
		}


		#endregion
	}

	namespace Collections
	{
		#region "Collections"

		public class SpecialItems:CollectionBase
		{
			public SpecialItems()
			{}


			public SpecialItem Add(SpecialItem si)
			{
				this.InnerList.Add(si);
				return si;
			}

			public void AddRange(SpecialItem[] si)
			{
				this.InnerList.AddRange(si);
			}		

			public void Remove(SpecialItem si)
			{
				this.InnerList.Remove(si);
			}

			public bool Contains(SpecialItem si)
			{
				return this.InnerList.Contains(si);
		
			}


			public SpecialItem this[int i]
			{
				get{return (SpecialItem)this.InnerList[i];}
				set{this.InnerList[i]=value;}
			}

        
			public SpecialItem[] GetValues()
			{
				SpecialItem[] si= new SpecialItem[this.InnerList.Count];
				this.InnerList.CopyTo(0,si,0,this.InnerList.Count);
				return si;
			}		
		

		}

		#endregion
	}

	namespace CollectionEditors
	{
		#region "CollectionEditors"

		public class CustomCollectionEditorDialog:CustomCollectionEditorForm
		{

			protected override void SetProperties(TItem titem, object reffObject)
			{
				

				if(reffObject is CustomItem)
				{
					CustomItem ci =reffObject as CustomItem;
					titem.SubItems=ci.SpecialItems;
					titem.Text=ci.Color.Name;
					titem.SelectedImageIndex=0;
					titem.ImageIndex=1;
					titem.ForeColor=ci.Color;

				}
				else if(reffObject is SpecialItem)
				{
					SpecialItem si= reffObject as SpecialItem;
					titem.Text= "SpecialItem " + si.GetHashCode().ToString();
					titem.SubItems=si.SubItems;
					titem.ForeColor=Color.Blue;
					titem.SelectedImageIndex=0;
					titem.ImageIndex=2;
				}
				else if(reffObject is SimpleItem_FullTc)
				{
					SimpleItem_FullTc siftc= reffObject as SimpleItem_FullTc;
					titem.Text=siftc.Id.ToString();
					if(siftc.Id>0){titem.ForeColor=Color.SeaGreen;}
					else{titem.ForeColor=Color.DarkRed;}
					titem.SelectedImageIndex=0;
					titem.ImageIndex=3;
				}
				else if(reffObject is SimpleItem_Component)
				{
					SimpleItem_Component sic= reffObject as SimpleItem_Component;
					titem.Text=sic.Name;
					titem.ForeColor=Color.Navy;
					titem.SelectedImageIndex=0;
					titem.ImageIndex=4;
				}
				else if(reffObject is SimpleItem_BasicTc)
				{
					SimpleItem_BasicTc sibtc= reffObject as SimpleItem_BasicTc;
					titem.Text=sibtc.Name;
					titem.ForeColor=Color.DarkSlateGray;
					titem.SelectedImageIndex=0;
					titem.ImageIndex=5;
				}
				else if(reffObject is ComplexItem)
				{
					ComplexItem ci =reffObject as ComplexItem;
					titem.Text=ci.Name;
					titem.ForeColor=Color.Black;
					titem.SelectedImageIndex=0;
					titem.ImageIndex=6;
				}
				else
				{
					// set the default Text 
					base.SetProperties(titem,reffObject);
				}
				
		}

		
			protected override Type[] CreateNewItemTypes(System.Collections.IList coll)
			{
				if(coll is SimpleItems)
				{
					return new Type[]{typeof(CustomItem),typeof(SimpleItem_BasicTc),typeof(SimpleItem_FullTc),typeof(SimpleItem_Component),typeof(ComplexItem)};
				}
				else
				{
					return base.CreateNewItemTypes(coll);
				}
			}

	
			

			protected override CustomControls.Enumerations.EditLevel SetEditLevel(IList collection)
			{
				if(collection is SpecialItems)
				{
					return CustomControls.Enumerations.EditLevel.AddOnly;
				}
				return base.SetEditLevel (collection);
			}


		}


		public class CustomItemCollectionEditor:CustomCollectionEditor
		{
			protected override CustomCollectionEditorForm CreateForm()
			{
				return new CustomCollectionEditorDialog ();
			}

		}

	
		#endregion
	}

	namespace TypeConverters
	{
		#region "TypeConverter"

		internal class SpecialItemConverter:ExpandableObjectConverter
		{
			public override bool CanConvertFrom(ITypeDescriptorContext context,Type t)
			{
				if (t==typeof(string)){return true;}
				return base.CanConvertFrom(context ,t);
			}

			public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) 
			{
				if (destType == typeof(InstanceDescriptor) || destType == typeof(string)) 
				{
					return true;
				}
				return base.CanConvertTo(context, destType);
			}	
		
			public override object  ConvertFrom(ITypeDescriptorContext context,CultureInfo info,object value)
			{
				if (value is string)
				{
					string str=(string)value;
					
					try
					{
						string[] elements=str.Split(new char[] {';',':'});
						return new SpecialItem(int.Parse(elements[1]),elements[3], null);
					}
					catch
					{
						return new SpecialItem();
					}
				
				}
				return base.ConvertFrom(context,info,value);
			}

			public override object  ConvertTo(ITypeDescriptorContext context,CultureInfo info,object value,Type destType )
			{
				if ((destType==typeof(string))&&(value is SpecialItem))
				{
					SpecialItem si=(SpecialItem)value;
					return "Cod : "+ si.Cod.ToString() +";"+" Speciality : " + si.Speciality;
				}
				else if (destType == typeof(InstanceDescriptor)) 
				{
					return new InstanceDescriptor(typeof(SpecialItem).GetConstructor(new Type[]{typeof(int), typeof(string), typeof(SpecialItem[])}), new object[]{((SpecialItem)value).Cod,((SpecialItem)value).Speciality,((SpecialItem)value).SubItems.GetValues()});
			
				}
				return base.ConvertTo(context,info,value,destType);
			}

		}

		internal class CustomItemConverter:ExpandableObjectConverter
		{
	
			public override bool CanConvertFrom(ITypeDescriptorContext context,Type t)
			{
				if (t==typeof(string)){return true;}
				return base.CanConvertFrom(context ,t);
			}

			public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) 
			{
				if (destType == typeof(InstanceDescriptor) || destType == typeof(string)) 
				{
					return true;
				}
				return base.CanConvertTo(context, destType);
			}	
		
			public override object  ConvertFrom(ITypeDescriptorContext context,CultureInfo info,object value)
			{
				if (value is string)
				{
					string str=(string)value;
					
					try
					{
						string[] elements=str.Split(new char[] {';',':'});
						return new CustomItem(int.Parse(elements[1]),elements[3],null);
					}
					catch
					{
						return new CustomItem();
					}
				
				}
				return base.ConvertFrom(context,info,value);
			}

			public override object  ConvertTo(ITypeDescriptorContext context,CultureInfo info,object value,Type destType )
			{
				if ((destType==typeof(string))&&(value is CustomItem))
				{
					CustomItem ci=(CustomItem)value;
					return "Id : "+ ci.Id.ToString() +";"+" Name : " + ci.Name;
				}
				else if (destType == typeof(InstanceDescriptor)) 
				{
					return new InstanceDescriptor(typeof(CustomItem).GetConstructor(new Type[]{typeof(int), typeof(string), typeof(SpecialItem[])}), new object[]{((CustomItem)value).Id,((CustomItem)value).Name,((CustomItem)value).SpecialItems.GetValues()});
			
				}
				return base.ConvertTo(context,info,value,destType);
			}

		
		}

		#endregion
	}
}
