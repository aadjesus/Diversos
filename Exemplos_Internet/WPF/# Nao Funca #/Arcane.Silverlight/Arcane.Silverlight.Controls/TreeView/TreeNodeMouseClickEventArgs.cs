using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace Arcane.Silverlight.Controls
{

    public class TreeNodeMouseClickEventArgs : MouseEventArgs
    {
          private TreeNode node;

        public TreeNodeMouseClickEventArgs(TreeNode node)
        {
            this.node = node;
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
