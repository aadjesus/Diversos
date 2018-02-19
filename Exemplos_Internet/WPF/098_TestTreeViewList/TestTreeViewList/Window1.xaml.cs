using System.Collections.Generic;
using System.Windows;

namespace TestTreeViewList
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            var n0 = this.CreateNode("root");
            var n1 = this.CreateNode("item1");
            var n11 = this.CreateNode("item11");
            var n12 = this.CreateNode("item12");
            var n121 = this.CreateNode("item121");
            var n2 = this.CreateNode("item2");
            var n3 = this.CreateNode("item3");

            n0.Children.Add(n1);
            n0.Children.Add(n2);
            n0.Children.Add(n3);

            n1.Children.Add(n11);
            n1.Children.Add(n12);

            n12.Children.Add(n121);

            n121.IsSelected = true;

            this.DataContext = n0;
        }

        public NodeViewModel CreateNode(string name)
        {
            var node = new NodeViewModel { Name = name };
            node.Children = new List<NodeViewModel>();

            return node;
        }
    }
}
