using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CustomTreeViewLayout
{
    public partial class Window1 : System.Windows.Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        void OnLoaded( object sender, RoutedEventArgs e )
        {
            // Prevent the TreeView from responding to the keyboard.
            // Since there's no sane way to set a TreeView's SelectedItem,
            // we can't customize the keyboard navigation logic. :(
            this.tree.PreviewKeyDown += delegate( object obj, KeyEventArgs args ) { args.Handled = true; };

            // Put some data into the TreeView.
            this.PopulateTreeView();
        }

        void PopulateTreeView()
        {
            Node root = new Node( "Big Daddy Root" );

            int branches = 0;
            int subBranches = 0;

            for( int i = 0; i < 2; ++i )
            {
                Node child = new Node( "Branch " + ++branches );
                root.ChildNodes.Add( child );

                for( int j = 0; j < 3; ++j )
                {
                    Node gchild = new Node( "Sub-Branch " + ++subBranches );
                    child.ChildNodes.Add( gchild );

                    for( int k = 0; k < 2; ++k )
                        gchild.ChildNodes.Add( new Node( "Leaf" ) );
                }
            }

            // Create a dummy node so that we can bind the TreeView
            // it's ChildNodes collection.
            Node dummy = new Node();
            dummy.ChildNodes.Add( root );

            this.tree.ItemsSource = dummy.ChildNodes;
        }
    }
}