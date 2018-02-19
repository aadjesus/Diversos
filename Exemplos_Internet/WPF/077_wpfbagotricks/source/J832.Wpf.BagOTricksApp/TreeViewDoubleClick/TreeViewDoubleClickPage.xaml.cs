using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using System.Xml;


namespace Microsoft.Samples.KMoore.WPFSamples.TreeViewDoubleClick
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class TreeViewDoubleClickPage : Page
    {
        /*
        public TreeViewDoubleClickPage()
        {
            InitializeComponent();
        }*/

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            treeView.AddHandler(TreeView.MouseLeftButtonDownEvent, new MouseButtonEventHandler(treeView_MouseLeftButtonDown), true);
        }

        void treeView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TreeViewItem tvi = GetTreeViewItemFromChild(e.OriginalSource as DependencyObject);
                if (tvi != null)
                {
                    XmlNode xNode = tvi.Header as XmlNode;
                    if (xNode != null)
                    {
                        textBox.Text = xNode.OuterXml;
                    }
                    else
                    {
                        textBox.Text = tvi.Header.ToString();
                    }
                }
            }
        }

        static TreeViewItem GetTreeViewItemFromChild(DependencyObject child)
        {
            if (child != null)
            {
                if (child is TreeViewItem)
                {
                    return child as TreeViewItem;
                }
                else if (child is TreeView)
                {
                    return null; //we've walked through the tree. The source of the click is not something else
                }
                else
                {
                    return GetTreeViewItemFromChild(VisualTreeHelper.GetParent(child));
                }
            }
            return null;
        }

    }
}