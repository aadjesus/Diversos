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


namespace Test
{
	#region "Test Control"

	public class TestControl:CustomControls.Win32Controls.PushButton,ISupportUniqueName
	{

		#region "Variables"

		private SimpleItem _SimpleItem= new SimpleItem();
		private SimpleItem_BasicTc _SimpleItem_BasicTc= new SimpleItem_BasicTc();
		private SimpleItem_FullTc _SimpleItem_FullTc= new SimpleItem_FullTc();
		private SimpleItem_Component _SimpleItem_Component= new SimpleItem_Component();
		private ComplexItem _ComplexItem= new ComplexItem();

		private SimpleItems _SimpleItems= new SimpleItems();
		private ComplexItems _ComplexItems=new ComplexItems();
		private SimpleItems _CustomItems= new SimpleItems();
			
		#endregion

		#region "Properties"

		
		[Category("Member Persisting")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SimpleItem SimpleItem
		{
			get{return _SimpleItem;}
		}

		
		[Category("Member Persisting")]
		public SimpleItem_BasicTc  SimpleItem_BasicTc
		{
			get{return _SimpleItem_BasicTc;}
			set{_SimpleItem_BasicTc= value;}
		}

		
		[Category("Member Persisting")]
		public SimpleItem_FullTc SimpleItem_FullTc
		{
			get{return _SimpleItem_FullTc;}
			set{_SimpleItem_FullTc=value;}
		}

		
		[Category("Member Persisting")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SimpleItem_Component SimpleItem_Component
		{
			get{return _SimpleItem_Component;}
		}


		[Category("Member Persisting")]
		public ComplexItem ComplexItem
		{
			get{return _ComplexItem;}
			set{_ComplexItem=value;}
		}

		
		
		[Category ("Collections")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(SimpleItem_CollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
		public SimpleItems SimpleItems
		{
			get{return _SimpleItems;}
		}

		
		[Category ("Collections")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ComplexItemCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
		public ComplexItems ComplexItems
		{
			get{return _ComplexItems;}
		}

		[Category ("Collections")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(CustomItemCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
		public SimpleItems CustomItems
		{
			get{return _CustomItems;}
		}

		
		#endregion

		#region "Constructors"

		public TestControl()
		{
			SetStyle(ControlStyles.ResizeRedraw,true);
		}
		
		#endregion

		#region "Overrides"

		protected override Size DefaultSize
		{
			get
			{
				return new Size(100,100);
			}
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
//			base.OnPaintBackground (pevent);
//			ControlPaint.DrawBorder3D(pevent.Graphics,this.DisplayRectangle,Border3DStyle.Etched);
//			SizeF textSize=pevent.Graphics.MeasureString(Text,Font);
//			int y=Math.Max(1,(int)(Height-textSize.Height))/2;
//			int x=Math.Max(1,(int)(Width-textSize.Width))/2;
//		
//			RectangleF rect = new RectangleF(x,y,Width,textSize.Height);
//			pevent.Graphics.DrawString(Text,Font, new SolidBrush(ForeColor),rect);
		}

		#endregion

		#region "Implementation"

		public string GetUniqueName()
		{
			return GetName("ComplexItem",this.ComplexItems.Count+1);
		}


		public string GetName(string name,int index)
		{
			foreach(ComplexItem ci in this.ComplexItems )
			{
				if(ci.Name==name+index.ToString()){return GetName(name, index+1);}
			}
			return name+index.ToString();
		}


		public bool ShouldSerializeSimpleItem_FullTc()
		{
			return (SimpleItem_FullTc.Id!=-1 || SimpleItem_FullTc.Name!="SimpleItem FullTC");
		}


		public void ResetSimpleItem_FullTc()
		{
			this.SimpleItem_FullTc= new SimpleItem_FullTc();
		}


		public bool ShouldSerializeSimpleItem_BasicTc()
		{
			return (SimpleItem_BasicTc.Id!=-1 || SimpleItem_BasicTc.Name!="SimpleItem BasicTC");
		}


		public void ResetSimpleItem_BasicTc()
		{
			this.SimpleItem_BasicTc= new SimpleItem_BasicTc();
		}


		public bool ShouldSerializeSimpleItem_Component()
		{
			return (SimpleItem_Component.Id!=-1 || SimpleItem_Component.Name!="SimpleItem Comp");
		}


		public void ResetSimpleItem_Component()
		{
			this.SimpleItem_Component.Id=-1;
			this.SimpleItem_Component.Name="SimpleItem Comp";
		}


		public bool ShouldSerializeComplexItem()
		{
			return (ComplexItem.Id!=-1 || ComplexItem.Name!="ComplexItem" || ComplexItem.ComplexItems.Count>0);
		}


		public void ResetComplexItem()
		{
			this.ComplexItem= new ComplexItem();
		}



		#endregion
	}


	#endregion	

	namespace Items
	{
		

		public class SimpleItem
		{

			// A verry simple Item
			private string _Name="SimpleItem";
			private int _Id=-1;

			[Category("TestProperties")] // Take this out, and you will soon have problems with serialization;
			[DefaultValue(typeof(string),"")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
			public string Name
			{
				get{return _Name;}
				set{_Name= value;}
			}

			[Category("TestProperties")]
			[DefaultValue(typeof(int),"-1")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
			public int Id
			{
				get{return _Id;}
				set{_Id= value;}
			}

			public SimpleItem()
			{
		
			}


			public SimpleItem(int Id, string Name)
			{
				this._Id=Id;
				this._Name= Name;
			}


		}


		[TypeConverter(typeof(SimpleItemFullConverter))]
		public class SimpleItem_FullTc:SimpleItem
		{
			// A class that inherits from SimpleItem, and has an custom 'full initialization' TypeConverter

			public SimpleItem_FullTc():base(-1,"SimpleItem FullTC")
			{}

			public SimpleItem_FullTc(int Id, string Name):base(Id,Name)
			{
			}
		}


		[TypeConverter(typeof(SimpleItemBasicConverter))]
		public class SimpleItem_BasicTc:SimpleItem
		{
			 //A class that inherits from SimpleItem, and has an custom 'basic' TypeConverter

			public SimpleItem_BasicTc():base(-1,"SimpleItem BasicTC")
			{}

		
		}
//		If you don't want to appear in the Component Tray uncoment the next line.	
//		[DesignTimeVisible(false)]
		public class SimpleItem_Component:SimpleItem,IComponent
		{
			//A class that inherits from SimpleItem and implements IComponent

			public event EventHandler Disposed;  //Implement IComponent.Disposed
			private ISite _curISBNSite;


			public virtual void Dispose() //Implement IComponent.Dispose()
			{    
				//There is nothing to clean.
				if(Disposed != null)
				{
					Disposed(this,EventArgs.Empty);
				}
			}

			[Browsable(false)]
			public virtual ISite Site //implements IComponent.Site
			{
				get
				{
					return _curISBNSite;
				}
				set
				{
					_curISBNSite = value;
				}
			}


			public SimpleItem_Component():base(-1,"SimpleItem Comp")
			{
				// IComponent also requires a simple constructor...
			}
		


		}
	

		[TypeConverter(typeof(ComplexItemConverter))]
		public class ComplexItem:SimpleItem,ISupportUniqueName
		{
			private ComplexItems _ComplexItems= new ComplexItems();
	
			[Category ("Collections")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
			[Editor(typeof(ComplexItemCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
			public virtual ComplexItems ComplexItems
			{
				get{return _ComplexItems;}
			}

	
			public ComplexItem():base(-1,"ComplexItem")
			{}
		
	
			public ComplexItem(int Id, string Name,ComplexItem[] complexItems):base(Id,Name)
			{
				this.ComplexItems.AddRange(complexItems);
			}


			public string GetUniqueName()
			{
				return GetName("SubItem",this.ComplexItems.Count+1);
			}


			public string GetName(string name,int index)
			{
				foreach(ComplexItem ci in this.ComplexItems )
				{
					if(ci.Name==name+index.ToString()){return GetName(name, index+1);}
				}
				return name+index.ToString();
			}
		}


	

	
	}


	namespace Collections
	{
		

		public class SimpleItems:CollectionBase
		{
			public SimpleItems()
			{}


			public SimpleItem Add(SimpleItem si)
			{
				this.InnerList.Add(si);
				return si;
			}

		

			public void Remove(SimpleItem si)
			{
				this.InnerList.Remove(si);
			}

			public bool Contains(SimpleItem si)
			{
				return this.InnerList.Contains(si);
		
			}


			public SimpleItem this[int i]
			{
				get{return (SimpleItem)this.InnerList[i];}
				set{this.InnerList[i]=value;}
			}

			public void AddRange(SimpleItem[] si)
			{
				this.InnerList.AddRange(si);
			}

				
			public SimpleItem[] GetValues()
			{
				SimpleItem[] si= new SimpleItem[this.InnerList.Count];
				this.InnerList.CopyTo(0,si,0,this.InnerList.Count);
				return si;
			}
			protected override void OnInsert(int index, object value)
			{
				base.OnInsert (index, value);
			}

		
		}



		public class ComplexItems:CollectionBase
		{
			public ComplexItems()
			{}


			public ComplexItem Add(ComplexItem ci)
			{
				this.InnerList.Add(ci);
				return ci;
			}

			public void AddRange(ComplexItem[] ci)
			{
				this.InnerList.AddRange(ci);
			}		

			public void Remove(ComplexItem ci)
			{
				this.InnerList.Remove(ci);
			}

			public bool Contains(ComplexItem ci)
			{
				return this.InnerList.Contains(ci);
		
			}


			public ComplexItem this[int i]
			{
				get{return (ComplexItem)this.InnerList[i];}
				set{this.InnerList[i]=value;}
			}

		

				
			public ComplexItem[] GetValues()
			{
				//It is used by the ComplexItemConverter
				ComplexItem[] ci= new ComplexItem[this.InnerList.Count];
				this.InnerList.CopyTo(0,ci,0,this.InnerList.Count);
				return ci;
			}

		
		}


	
	}


	namespace TypeConverters
	{
		

		internal class SimpleItemFullConverter:ExpandableObjectConverter
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
						return new SimpleItem_FullTc(int.Parse(elements[1]),elements[3]);
					}
					catch
					{
						return new SimpleItem_FullTc();
					}
				
				}
				return base.ConvertFrom(context,info,value);
			}

			public override object  ConvertTo(ITypeDescriptorContext context,CultureInfo info,object value,Type destType )
			{
				if (destType==typeof(string))
				{
					SimpleItem_FullTc si=(SimpleItem_FullTc)value;
					return "Id : "+ si.Id.ToString() +";"+" Name : " + si.Name;
				}
				else if (destType == typeof(InstanceDescriptor)) 
				{
					return new InstanceDescriptor(typeof(SimpleItem_FullTc).GetConstructor(new Type[]{typeof(int), typeof(string)}), new object[]{((SimpleItem_FullTc)value).Id,((SimpleItem_FullTc)value).Name},true);
				}
				return base.ConvertTo(context,info,value,destType);
			}

		
		}


		internal class SimpleItemBasicConverter:ExpandableObjectConverter
		{
	
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) 
			{
				if (destType == typeof(InstanceDescriptor)) 
				{
					return true;
				}
				return base.CanConvertTo(context, destType);
			}	
		
		
			public override object  ConvertTo(ITypeDescriptorContext context,CultureInfo info,object value,Type destType )
			{
				if (destType == typeof(InstanceDescriptor)) 
				{
					return new InstanceDescriptor(typeof(SimpleItem_BasicTc).GetConstructor(new Type[0]), null,false);
				}
				return base.ConvertTo(context,info,value,destType);
			}

		
		}


		internal class ComplexItemConverter:ExpandableObjectConverter
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
						return new ComplexItem(int.Parse(elements[1]),elements[3],null);
					}
					catch
					{
						return new ComplexItem();
					}
				
				}
				return base.ConvertFrom(context,info,value);
			}

			
			public override object  ConvertTo(ITypeDescriptorContext context,CultureInfo info,object value,Type destType )
			{
				if ((destType==typeof(string)))
				{
					ComplexItem ci=(ComplexItem)value;
					return "Id : "+ ci.Id.ToString() +";"+" Name : " + ci.Name;
				}
				else if (destType == typeof(InstanceDescriptor)) 
				{
					return new InstanceDescriptor(typeof(ComplexItem).GetConstructor(new Type[]{typeof(int), typeof(string), typeof(ComplexItem[])}), new object[]{((ComplexItem)value).Id,((ComplexItem)value).Name,((ComplexItem)value).ComplexItems.GetValues()},true);
			
				}
				return base.ConvertTo(context,info,value,destType);
			}

		
		}


	
	}


	namespace CollectionEditors
	{
		

		public class SimpleItem_CollectionEditor:System.ComponentModel.Design.CollectionEditor
		{
			// A Collection Editor for all the classes that inherits from SimpleItem

			private Type[] types; 

			public SimpleItem_CollectionEditor (Type type):base(type )
			{
				types = new Type[]{typeof(SimpleItem),typeof(SimpleItem_BasicTc),typeof(SimpleItem_FullTc),typeof(SimpleItem_Component),typeof(ComplexItem)  };
			
			} 


			protected override Type[] CreateNewItemTypes() 
			{
				return types;  
			} 
		}


		public class ComplexItemCollectionEditor:System.ComponentModel.Design.CollectionEditor
		{
		
			private CollectionForm collectionForm; 

			public ComplexItemCollectionEditor (Type type):base(type ){}

	
			public override object EditValue( 	ITypeDescriptorContext context, IServiceProvider provider,object value) 
 
			{ 
 
				if(this.collectionForm != null && this.collectionForm.Visible) 
 
				{ 
 	
					ComplexItemCollectionEditor editor = new ComplexItemCollectionEditor(this.CollectionType); 
					return editor.EditValue(context, provider, value); 
				} 
 
				else return base.EditValue(context, provider, value); 
 
			} 
 
 
			protected override CollectionForm CreateCollectionForm() 
 
			{  	
				this.collectionForm = base.CreateCollectionForm(); 
				return this.collectionForm; 
			} 
 

			protected override object CreateInstance(Type ItemType)
			{
				/* you can create the new instance yourself 
				 * ComplexItem ci=new ComplexItem(2,"ComplexItem",null);
				 * we know for sure that the itemType it will always be ComplexItem
				 *but this time let it to do the job... 
				 */
				ComplexItem ci=(ComplexItem)base.CreateInstance(ItemType);

				if ( this.Context.Instance!=null)
				{
					if (this.Context.Instance is ISupportUniqueName)
					{
						ci.Name=((ISupportUniqueName)this.Context.Instance).GetUniqueName();
					}
					else{ci.Name="ComplexItem";}
			
				}

				return ci;

			}
		}


		
	}

	
	#region "Interfaces"

	public interface ISupportUniqueName
	{
		string GetUniqueName();
	}

	#endregion
}
