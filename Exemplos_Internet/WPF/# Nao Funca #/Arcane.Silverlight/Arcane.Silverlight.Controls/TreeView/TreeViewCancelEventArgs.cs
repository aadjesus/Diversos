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
    public class TreeViewCancelEventArgs : CancelEventArgs
    {
        // Fields
        private TreeViewAction action;
        private TreeNode node;

        // Methods
        public TreeViewCancelEventArgs(TreeNode node, bool cancel, TreeViewAction action)
            : base(cancel)
        {
            this.node = node;
            this.action = action;
        }

        // Properties
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
