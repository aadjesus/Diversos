using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

namespace CollectionEditorExamples
{
	public class ExampleComponent : Component
	{
		ArrayListCollection _ArrayListItems = new ArrayListCollection();
		List<Button> _ListItems = new List<Button>();
		List<Control> _MultipleItems = new List<Control>();

		public ExampleComponent()
		{ }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Contains only Control objects in an ArrayList")]
		[Category("Behaviour")]
		public ArrayListCollection ArrayListItems
		{
			get { return _ArrayListItems; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Contains only Button objects in a List<Button> collection")]
		[Category("Behaviour")]
		public List<Button> ListItems
		{
			get { return _ListItems; }
		}

		// custom editor attribute
		[Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Contains ListView, Button and CheckBox items in a List<Control> collection")]
		[Category("Behaviour")]
		public List<Control> MultipleItems
		{
			get { return _MultipleItems; }
		}

		public class ArrayListCollection : ArrayList
		{
			// CollectionEditor searches collection type's Index property and finds its property type.
			// It uses this type to add new items
			public new Control this[int index]
			{
				get { return base[index] as Control; }
			}
		}

		// Custom collection editor.
		// by using this editor we can add ListView, Button and 
		// CheckBoxes to the same collection
		public class CustomCollectionEditor : CollectionEditor
		{
			public CustomCollectionEditor() : base(typeof(List<Control>))
			{ 
			
			}

			// override this method if you have to do custom initializing
			// operations. For example, if you have to use a constructor which
			// takes arguments you can create your own items here ...
			// or may be you have to do some initializing operations ...
			protected override object CreateInstance(Type itemType)
			{
				if (itemType == typeof(CheckBox))
				{
					CheckBox checkbox = new CheckBox();
					checkbox.Text = "My Text, hede";
					return checkbox;
				}

				Control control = base.CreateInstance(itemType) as Control;
				control.Text = "Other, hede";
				return control;
			}

			// if you want to use a custom collection editor form, 
			// you have to override this method and return your form here
			protected override CollectionEditor.CollectionForm CreateCollectionForm()
			{
				return base.CreateCollectionForm();
			}

			// here you can return a text which will be appeared in the list
			// for the given item
			protected override string GetDisplayText(object value)
			{
				Control control = value as Control;
				return string.Format("{0} - {1}", control.GetType().Name, control.Text);
			}

			// we allow 3 types can be added to our collection
			protected override Type[] CreateNewItemTypes()
			{
				return new Type[] { typeof(ListView), typeof(Button), typeof(CheckBox)};
			}
		}
	}
}
