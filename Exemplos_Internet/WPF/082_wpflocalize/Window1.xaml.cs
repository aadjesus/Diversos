using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;
using System.ComponentModel;

using WPFLocalize.Cultures;

namespace WPFLocalize
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {
        public Window1()
        {
            //hook up DataChanged event to get notification to make culture-related changes in code
            CultureResources.ResourceProvider.DataChanged += new EventHandler(ResourceProvider_DataChanged);

            //initialise with default culture
            Debug.WriteLine(string.Format("Set culture to default [{0}]:", Properties.Settings.Default.DefaultCulture));
            CultureResources.ChangeCulture(Properties.Settings.Default.DefaultCulture);

            this.InitializeComponent();

            //only attach SelectionChanged event here to avoid the culture being updated twice
            this.cbLanguages.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbLanguages_SelectionChanged);
            cbLanguages.SelectedItem = Properties.Settings.Default.DefaultCulture;
//#if DEBUG
//            // Add an event handler for a left mouse button down for any element in the UI.
//            this.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent, (MouseButtonEventHandler)HandlePreviewLeftBtnDown);
//#endif

        }

        void ResourceProvider_DataChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(string.Format("ObjectDataProvider.DataChanged event. fetching culturename property for new culture [{0}]", Properties.Resources.LabelCultureName));
        }

        //void HandlePreviewLeftBtnDown(object sender, MouseEventArgs e)
        //{
        //    if (Keyboard.PrimaryDevice.Modifiers == ModifierKeys.Control)
        //    {
        //        // IMPORTANT!
        //        // Put a breakpoint on the following line of code.  When the
        //        // debugger stops here, hover the mouse cursor over the
        //        // e.OriginalSource property, and select the menu item called
        //        // "Visual Tree Visualizer (Woodstock)" from the datatip.

        //        object objectToDebug = e.OriginalSource;
        //    }
        //}

        private void cbLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo selected_culture = cbLanguages.SelectedItem as CultureInfo;

            //if not current language
            //could check here whether the culture we want to change to is available in order to provide feedback / action
            if (Properties.Resources.Culture != null && !Properties.Resources.Culture.Equals(selected_culture))
            {
                Debug.WriteLine(string.Format("Change Current Culture to [{0}]", selected_culture));
         
                //save language in settings
                //Properties.Settings.Default.CultureDefault = selected_culture;
                //Properties.Settings.Default.Save();

                //change resources to new culture
                CultureResources.ChangeCulture(selected_culture);

                //could apply a theme tied to this culture if desired
            }
        }
    }
}