using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;

namespace CustomPropertyGridTab
{
	public partial class FormMain : Form
	{
		static FormMain Instance = null;
		Settings _Settings = null;

		public FormMain()
		{
			Instance = this;
			InitializeComponent();

			_Settings = new Settings(this);
			propertySettings.SelectedObject = _Settings;

			// Add our PropertyTab to the PropertyGrid's PropertyTabs
			propertyGrid.PropertyTabs.AddTabType(typeof(CustomPropertyTab), PropertyTabScope.Global);
			FillTestObjects();
		}

		// Our PropertyTab implementation
		private class CustomPropertyTab : PropertyTab
		{
			public CustomPropertyTab()
			{ }

			public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
			{
				PropertyInfo[] properties = component.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
				List<PropertyDescriptor> filtereds = new List<PropertyDescriptor>();
				PropertyDescriptorCollection propertyDescriptions = TypeDescriptor.GetProperties(component, attributes);

				Type myAttributeType = typeof(OzcanPropertyAttribute);
				List<string> ignoreds = new List<string>();
				for (int i = 0; i < properties.Length; i++)
				{
					if (properties[i].IsDefined(myAttributeType, true))
					{
						filtereds.Add(propertyDescriptions[properties[i].Name]);
					}
					else
					{
						ignoreds.Add(properties[i].Name);
					}
				}

				// now add a custom property descriptor to show hidden properties
				PropertyDescriptorCollection coll = new PropertyDescriptorCollection(filtereds.ToArray());
				if (FormMain.Instance._Settings.ShowHiddenProperties)
					coll.Add(new CustomPropertyDescriptor(ignoreds.ToArray()));
				return coll;
			}

			public override Bitmap Bitmap
			{
				get
				{
					return Properties.Resources.finance_16;
				}
			}

			public override string TabName
			{
				get { return "Ozcan Properties Tab"; }
			}
		}

		// Our custom PropertyDescriptor for the Hidden Properties
		private class CustomPropertyDescriptor : PropertyDescriptor
		{
			string _HiddenPropertyNames = string.Empty;

			public CustomPropertyDescriptor(string[] names) : base("Hidden Properties", new Attribute[0])
			{
				if (names == null)
					return;

				StringBuilder text = new StringBuilder();
				for (int i = 0; i < names.Length; i++)
				{
					if (i != 0)
						text.Append(", ");
					text.Append(names[i]);
				}
				_HiddenPropertyNames = text.ToString();
			}

			public override bool CanResetValue(object component)
			{
				return false;
			}

			public override Type ComponentType
			{
				get { return typeof(string); }
			}

			public override object GetValue(object component)
			{
				return _HiddenPropertyNames;
			}

			public override bool IsReadOnly
			{
				get { return true; }
			}

			public override Type PropertyType
			{
				get { return typeof(string); }
			}

			public override void ResetValue(object component)
			{
				
			}

			public override void SetValue(object component, object value)
			{
				
			}

			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			public override string Category
			{
				get
				{
					return "Hidden Fields";
				}
			}

			public override string Description
			{
				get
				{
					return "Shows the hidden field names\r\nAlthough these properties exists at the selected object's type you cannot see them as editable";
				}
			}
		}

		// Settings class for our example...
		private class Settings
		{
			FormMain MainForm = null;
			bool _ShowDefaultPropertiesTab = true;
			bool _ShowHiddenProperties = true;
			bool _AutoSelectMyTab = true;
			Type predefinedPropertyTab = null;

			public Settings(FormMain mainForm)
			{
				MainForm = mainForm;
			}

			[DefaultValue(true)]
			[Category("Settings")]
			[Description("Gets or sets that is Hidden Fields will be shown or not ?")]
			public bool ShowHiddenProperties
			{
				get { return _ShowHiddenProperties; }
				set {
					if (_ShowHiddenProperties == value)
						return;
					_ShowHiddenProperties = value;
					MainForm.propertyGrid.SelectedObject = null;
					MainForm.propertyGrid.SelectedObject = MainForm.lvwItems.SelectedItem;
				}
			}

			[DefaultValue(true)]
			[Category("Settings")]
			[Description("Gets or sets that is Default Properties TabPage will be visible or not ?")]
			public bool ShowDefaultPropertiesTab
			{
				get { return _ShowDefaultPropertiesTab; }
				set
				{
					if (_ShowDefaultPropertiesTab == value)
						return;
					_ShowDefaultPropertiesTab = value;
					if (value)
					{
						if (predefinedPropertyTab == null)
							return;

						MainForm.propertyGrid.PropertyTabs.AddTabType(predefinedPropertyTab);
					}
					else
					{
						if (predefinedPropertyTab == null)
						{
							IEnumerator en = MainForm.propertyGrid.PropertyTabs.GetEnumerator();
							while (en.MoveNext() && predefinedPropertyTab == null)
							{
								if (en.Current.GetType().Name == "PropertiesTab")
									predefinedPropertyTab = en.Current.GetType();
							}
						}
						if (predefinedPropertyTab != null)
						{
							// first select previous one...
							SendMessage(MainForm.propertyGrid.Handle, PG_CHANGE_TAB, 3, 0);
							MainForm.propertyGrid.PropertyTabs.RemoveTabType(predefinedPropertyTab);
						}
					}
				}
			}

			[DefaultValue(true)]
			[Category("Settings")]
			[Description("Gets or sets that ozcan property tab will select or not whenever you select an object")]
			public bool AutoSelectMyTab
			{
				get { return _AutoSelectMyTab; }
				set {
					if (_AutoSelectMyTab == value)
						return;
					_AutoSelectMyTab = value;

					if (_AutoSelectMyTab && ShowDefaultPropertiesTab)
					{
						SendMessage(MainForm.propertyGrid.Handle, PG_CHANGE_TAB, 4, 0);
					}
				}
			}
		}

		private void FillTestObjects()
		{
			lvwItems.Items.Add(new User());
			lvwItems.Items.Add(new Address());
			lvwItems.Items.Add(new DataItem());

			lvwItems.SelectedItem = lvwItems.Items[0];
		}

		private void lvwItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			propertyGrid.SelectedObject = lvwItems.SelectedItem;

			if (_Settings.AutoSelectMyTab)
			{
				int index = 3;
				if (_Settings.ShowDefaultPropertiesTab)
					index = 4;
				SendMessage(propertyGrid.Handle, PG_CHANGE_TAB, index, 0);
			}
		}

		// windows message which changes active tab of the property grid
		// wParam indicates the button index, for our example wParam is 4 or 3 (according to the ShowDefaultPropertyTab option)
		const int PG_CHANGE_TAB = 0x451;

		[DllImport("user32.dll")]
		static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
	}
}
