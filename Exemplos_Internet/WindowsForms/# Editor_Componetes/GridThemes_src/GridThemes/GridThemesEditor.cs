using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;
using System.Collections;

namespace UNLV.IAP.GridThemes
{
    // --------------------------------------------------------------
    // GridThemesEditor class
    // --------------------------------------------------------------
    // Defines a custom UITypeEditor for modifying a GridTheme value
    // in the Visual Studio environment; the class defines the editor
    // as a dropdown type, with a listbox for the user interface
    // --------------------------------------------------------------
    public class GridThemesEditor : UITypeEditor
    {
        protected const string kNONE = "(none)";

        // --------------------------------------------------------------
        // ThemesList class
        // --------------------------------------------------------------
        // nested class representing the list that is dropped down in the property window
        // --------------------------------------------------------------
        private class ThemesList : ListBox
        {
            // remember the forms editor service so we can close the dropdown
            // upon a selection
            private IWindowsFormsEditorService _service;

            // constructor
            public ThemesList(IWindowsFormsEditorService service, string curValue)
            {
                _service = service;

                // clear existing items
                this.Items.Clear();

                // get the list of themes from the GridThemesManager
                foreach (string s in GridThemesManager.ThemeTitles)
                {
                    this.Items.Add(s);
                    if (s.ToLower() == curValue.ToLower())
                    {
                        this.SelectedItem = s;
                    }
                }

                this.Sorted = true;
                this.SelectionMode = SelectionMode.One;
                this.SelectedIndexChanged +=new EventHandler(ThemesList_SelectedIndexChanged);

                this.Items.Insert(0, kNONE);
                if (this.SelectedIndex < 0)
                    this.SelectedIndex = 0;
            }

            // close the dropdown on a list selection
            private void  ThemesList_SelectedIndexChanged(object sender, EventArgs e)
            {
                _service.CloseDropDown();
            }

        }

        // --------------------------------------------------------------
        // EditValue method
        // --------------------------------------------------------------
        // Override the UITypeEditor's method to drop down our custom
        // list box
        // --------------------------------------------------------------
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                Object newValue = value;

                // Get the service that will open our dropdown editor
                IWindowsFormsEditorService service = ((IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService)));
                if (service != null)
                {
                    // show the custom ListBox, populated with names of GridThemes
                    ThemesList themes = new ThemesList(service, (string)value);
                    service.DropDownControl(themes);
                    
                    // was a list item selected?
                    if (themes.SelectedIndex >= 0)
                    {
                        // if so, set it as the new value
                        string s = (string)themes.Items[themes.SelectedIndex];
                        if (s == kNONE)
                            newValue = "";
                        else
                            newValue = s;
                    }
                    // if not, revert to the original value
                    else
                        newValue = value;

                    return newValue;
                }
            }

            // if the provider is null, just return the existing value
            return value;
        }

        // --------------------------------------------------------------
        // GetEditStyle method
        // --------------------------------------------------------------
        // Override the UITypeEditor's method to indicate that we're using
        // the DropDown edit style
        // --------------------------------------------------------------
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        // --------------------------------------------------------------
        // IsDropDownResizable method
        // --------------------------------------------------------------
        // Override the UITypeEditor's method to indicate that we'll allow
        // the dropdown to be resized by the developer
        // --------------------------------------------------------------
        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }
    }
}