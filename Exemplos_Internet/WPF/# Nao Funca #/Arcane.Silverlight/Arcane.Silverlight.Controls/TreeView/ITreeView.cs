using System;
namespace Arcane.Silverlight.Controls
{
    public interface ITreeView
    {
        event Arcane.Silverlight.Controls.TreeViewEventHandler AfterCollapse;
        event Arcane.Silverlight.Controls.TreeViewEventHandler AfterExpand;
        event Arcane.Silverlight.Controls.TreeViewEventHandler AfterSelect;
        event Arcane.Silverlight.Controls.TreeViewCancelEventHandler BeforeCollapse;
        event Arcane.Silverlight.Controls.TreeViewCancelEventHandler BeforeExpand;
        event Arcane.Silverlight.Controls.TreeViewCancelEventHandler BeforeSelect;
        event Arcane.Silverlight.Controls.TreeNodeMouseClickEventHandler NodeMouseClick;
        event Arcane.Silverlight.Controls.TreeNodeMouseClickEventHandler NodeMouseDoubleClick;
        event Arcane.Silverlight.Controls.TreeNodeMouseHoverEventHandler NodeMouseHover;
        System.Windows.DataTemplate NodeTemplate { get; set; }
        Arcane.Silverlight.Controls.TreeNode SelectedNode { get; set; }
    }
}
