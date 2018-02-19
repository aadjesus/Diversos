using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Arcane.Silverlight.Controls
{
    public class TreeViewEventArgs : EventArgs
    {
        private TreeViewAction action;
        private TreeNode node;

        public TreeViewEventArgs(TreeNode node)
        {
            this.node = node;
        }

        public TreeViewEventArgs(TreeNode node, TreeViewAction action)
        {
            this.node = node;
            this.action = action;
        }

        public TreeViewAction Action
        {
            get
            {
                return this.action;
            }
        }

        public TreeNode Node
        {
            get
            {
                return this.node;
            }
        }
    }


}
