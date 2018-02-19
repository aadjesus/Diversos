using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace tabtest
{
	/// <summary>
	/// Our sample PropertyTab.  It is connected to an instance of FunkyButton
	/// and displayes each of its vertices as a different property.
	/// </summary>
	internal class PointsPropertyTab : PropertyTab 
	{
		/// <summary>
		/// The FunkyButton that we are attached to.
		/// </summary>
		internal FunkyButton target;

		public PointsPropertyTab() 
		{
		}

		/// <summary>
		/// This is the tooltip string that will be displayed
		/// for this tab button.
		/// </summary>
  		public override string TabName 
		{
			get 
			{
				return "Funky Button Sample Tab";
			}
		}

		/// <summary>
		/// The Image that will be displayed on the PropertyGrid toolbar.
		/// </summary>
		public override Bitmap Bitmap 
		{
			get
			{
				// force this to 16x16 to work around a Beta2 PropertyGrid
				// issue.
				return new Bitmap(base.Bitmap, new Size(16,16));
			}
		}

		
		/// <summary>
		/// We like FunkyButtons!
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public override bool CanExtend(object o) 
		{
			return o is FunkyButton;
		}

        /// <summary>
		/// Just call the other version...
		/// </summary>
        public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attrs) {
            return GetProperties(null, component, attrs);
        }

		
		/// <summary>
		/// Our main function.  We display the vertices of a FunkyButton as properties by creating
		/// PropertyDescriptors for each one.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="component"></param>
		/// <param name="attrs"></param>
		/// <returns></returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attrs)
		{
			FunkyButton uc = component as FunkyButton;

			// we can get things besides FunkyButtons here.  Since we want our Point types
			// to be expandable, the PropertyGrid will still ask this PropertyTab for the properties of
			// points, so we default to the standard way of getting properties from other types of objects.
			//
			if (uc == null) 
			{
					
				TypeConverter tc = TypeDescriptor.GetConverter(component);
				if (tc != null) 
				{
					return tc.GetProperties(context, component, attrs);
				}
				else 
				{
					return TypeDescriptor.GetProperties(component, attrs);
				}
			}
				
			target = uc;
			// our list of props.
			//
			ArrayList propList = new ArrayList();

			// add the property for our count of vertices
			//
			propList.Add(new NumPointsPropertyDescriptor(this));

			// add a property descriptor for each vertex
			//
			for (int  i = 0; i < ((FunkyButton)component).Points.Count; i++) 
			{
				propList.Add(new VertexPropertyDescriptor(this,i));
			}

			// return the collection of PropertyDescriptors.
			PropertyDescriptor[] props = (PropertyDescriptor[])propList.ToArray(typeof(PropertyDescriptor));				
			return new PropertyDescriptorCollection(props);
		}
	
		/// <summary>
		/// The first custom property descriptor that we'll use.  This one is simply an integer field
		/// that determines the number of vertices we will display.
		/// </summary>
		private class NumPointsPropertyDescriptor : PropertyDescriptor 
		{
			PointsPropertyTab owner;
			public NumPointsPropertyDescriptor(PointsPropertyTab owner) :
				base("NumPoints", new Attribute[]{CategoryAttribute.Data, RefreshPropertiesAttribute.All})
			{
				this.owner = owner;					
			}

			/// <summary>
			/// This is an Integer type property only.
			/// </summary>
			public override Type PropertyType
			{
				get 
				{
					return typeof(int);
				}
			}

			/// <summary>
			/// The type of component the framework expects for this property.  Notice
			/// This returns FunkyButton.  That is because the object that is being browsed
			/// when this property is shown is a FunkyButton.  So we are faking the PropertyGrid
			/// into thinking this is a property on that type, even though it isn't.
			/// </summary>	
			public override Type ComponentType
			{
				get 
				{
					return typeof(FunkyButton);
				}
			}

			/// <summary>
			/// We have to override all the abstract members.
			/// </summary>
			public override bool IsReadOnly {get{return false;}}
		
			/// <summary>
			/// Just get the number of Points we're currently showing.
			/// </summary>
			/// <param name="o"></param>
			/// <returns></returns>
			public override object GetValue(object o) 
			{
				// the object that's passed in is the FunkyButton itself.
				return ((FunkyButton)o).Points.Count;
			}

			public override void SetValue(object o, object value) 
			{
				FunkyButton uc = o as FunkyButton;

				int delta = (int)value - uc.Points.Count;
				bool negative = delta < 0;
				delta = Math.Abs(delta);

				// we need to have at least 3 of these!
				//
				if ((int)value < 3) 
				{
					throw new ArgumentException();
				}

				// Walk through and add or remove vertices
				// as appropriate.
				//
				for (int i = 0; i < delta; i++) 
				{
					if (!negative) 
					{
						uc.Points.Add(new Point(0,0));
					}
					else 
					{
						uc.Points.RemoveAt(uc.Points.Count - 1);
					}
				}				
			}

			/// <summary>
			/// Abstract member override
			/// </summary>
			/// <param name="o"></param>
			public override void ResetValue(object o){}


			/// <summary>
			/// This is not a resettable property.
			/// </summary>
			/// <param name="o"></param>
			/// <returns></returns>
			public override bool CanResetValue(object o) 
			{
				return false;
			}

			/// <summary>
			/// This property doesn't participate in code generation.
			/// </summary>
			/// <param name="o"></param>
			/// <returns></returns>
			public override bool ShouldSerializeValue(object o) 
			{
				return false;
			}
		}
 
		/// <summary>
		/// This PropertyDescriptor handles setting of a given vertex
		/// for the FunkyButton.  It specifies our custom UI editor for Points
		/// as well as handles determining how to draw each vertex.
		/// </summary>
		private class VertexPropertyDescriptor : PropertyDescriptor 
		{
			PointsPropertyTab owner;
			int index;
			System.Drawing.Design.UITypeEditor editor;

			/// <summary>
			/// Initialize our state.
			/// </summary>
			/// <param name="owner">The PropertyTab that created this Property</param>
			/// <param name="index">The vertex this PropertyDescriptor operates on.</param>
			public VertexPropertyDescriptor(PointsPropertyTab owner, int index) :
				base("Vertex " + index, new Attribute[]{CategoryAttribute.Data})
			{
				this.owner = owner;
				this.index = index;
			}
			
			/// <summary>
			/// The type of component the framework expects for this property.  Notice
			/// This returns FunkyButton.  That is because the object that is being browsed
			/// when this property is shown is a FunkyButton.  So we are faking the PropertyGrid
			/// into thinking this is a property on that type, even though it isn't.
			/// </summary>	
			public override Type ComponentType
			{
				get 
				{
					return typeof(FunkyButton);
				}
			}

			/// <summary>
			/// Must override abstract properties.
			/// </summary>
			public override bool IsReadOnly 
			{
				get 
				{
					return false;
				}
			}


			/// <summary>
			/// This property is a Point type property.
			/// </summary>
			public override Type PropertyType 
			{
				get 
				{
					return typeof(Point);
				}
			}

			/// <summary>
			/// This allows us to specify the editor that will be used for this
			/// property.  By default the editor from the Point type would be used,
			/// but we overrride this and specify our own, much cooler, editor.
			/// </summary>
			/// <param name="editorBaseType"></param>
			/// <returns></returns>
			public override object GetEditor(Type editorBaseType) 
			{
				// make sure we're looking for a UITypeEditor.
				//
				if (editorBaseType == typeof(System.Drawing.Design.UITypeEditor)) 
				{
					// create and return one of our editors.
					//
					if (editor == null) 
					{
						editor = new PointUIEditor(owner.target);
					}
					return editor;

				}
				return base.GetEditor(editorBaseType);
			}


			/// <summary>
			/// Gets the value for this vertex
			/// </summary>
			public override object GetValue(object o) 
			{
				// just ask the FunkyButton!
				return ((FunkyButton)o).Points[index];					
			}

			/// <summary>
			/// When the value is set, we just push that value
			/// back up into the buttons Points array.
			/// </summary>
			public override void SetValue(object o, object value) 
			{
				FunkyButton fb = o as FunkyButton;
				IComponentChangeService changeSvc = null;
				PropertyDescriptor prop = null;

				// Undo and code generation work off of the IComponentChangeService, so 
				// we need to notify this service that we're making changes to the object.
				// Normally, when we set properties through a PropertyDescriptor this happens
				// automatically, but since we're modifying the collection directly, we 
				// need to do this manually.
				//
				if (fb != null && fb.Site != null) 
				{
					changeSvc = (IComponentChangeService)fb.Site.GetService(typeof(IComponentChangeService));
					if (changeSvc != null) 
					{
						// notify that we're changing the Points property
						//
						prop = TypeDescriptor.GetProperties(fb)["Points"];
						try 
						{
							changeSvc.OnComponentChanging(fb, prop);
						}
						catch(System.ComponentModel.Design.CheckoutException ex) 
						{
							// if this exception was generated because the checkout of the file
							// was canceled by the user, just return silently.
							//
							if (ex == System.ComponentModel.Design.CheckoutException.Canceled) 
							{
								return;
							}
						}
					}
				}

				// make the value change
				//
				fb.Points[index] = (Point)value;	

				if (changeSvc != null) 
				{
					// finally notify that the property value was actually changed.
					//
					changeSvc.OnComponentChanged(fb, prop, null, null);
				}
					
			}

			/// <summary>
			/// Abstract base members
			/// </summary>			
			public override void ResetValue(object o){}

			public override bool CanResetValue(object o) 
			{
				return false;
			}

			public override bool ShouldSerializeValue(object o) 
			{
				return false;
			}
		}
	}
}
