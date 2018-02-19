using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Markup;
using System.Collections.ObjectModel;

namespace WPF_Mysteries
{
    /// <summary>
    /// This demonstrates how to use the ContentPropertyAttribute, which is 
    /// how some of the native Controls such as Grid/Canvas/StackPanel etc etc
    /// know what to do with the content that is added to them in XAML.
    /// Basically the ContentPropertyAttribute, tells the XAML parser, 
    /// what code behind property should be used when the XAML parser 
    /// finds some content.
    /// </summary>
    [ContentProperty("SomeContent")]
    public partial class ContentTestControl : UserControl
    {
        #region Public Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<UIElement> SomeContent { get; set; }
        #endregion

        #region Ctor
        public ContentTestControl()
        {
            InitializeComponent();
            SomeContent = new ObservableCollection<UIElement>();
            // Allows binding to it's own properties
            this.DataContext = this;
        }
        #endregion
    }
}
