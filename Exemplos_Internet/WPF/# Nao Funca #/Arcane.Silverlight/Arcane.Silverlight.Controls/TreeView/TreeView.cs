using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Windows.Data;
using Arcane.Silverlight.Controls.Properties;

namespace Arcane.Silverlight.Controls
{

    /// <summary>
    /// <para>Tree view.</para>
    /// </summary>
    [TemplatePart(Name = "ScrollViewerElement", Type = typeof(ScrollViewer))]
    public class TreeView : TreeNodeCollection, ITreeView
    {

        #region Fields

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private ScrollViewer _elementScrollViewer;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TreeViewCancelEventHandler _onBeforeSelect;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TreeViewEventHandler _onAfterSelect;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TreeViewCancelEventHandler _onBeforeCollapse;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TreeViewCancelEventHandler _onBeforeExpand;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TreeViewEventHandler _onAfterCollapse;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TreeViewEventHandler _onAfterExpand;
        private TreeNodeMouseClickEventHandler _onNodeMouseClick;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TreeNodeMouseClickEventHandler _onNodeMouseDoubleClick;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TreeNodeMouseHoverEventHandler _onNodeMouseHover;

        #endregion


        #region Properties

        public event TreeNodeMouseClickEventHandler NodeMouseClick
        {
            add
            {
                this._onNodeMouseClick = (TreeNodeMouseClickEventHandler)Delegate.Combine(this._onNodeMouseClick, value);
            }
            remove
            {
                this._onNodeMouseClick = (TreeNodeMouseClickEventHandler)Delegate.Remove(this._onNodeMouseClick, value);
            }
        }

        public event TreeNodeMouseClickEventHandler NodeMouseDoubleClick
        {
            add
            {
                this._onNodeMouseDoubleClick = (TreeNodeMouseClickEventHandler)Delegate.Combine(this._onNodeMouseDoubleClick, value);
            }
            remove
            {
                this._onNodeMouseDoubleClick = (TreeNodeMouseClickEventHandler)Delegate.Remove(this._onNodeMouseDoubleClick, value);
            }
        }

        public event TreeNodeMouseHoverEventHandler NodeMouseHover
        {
            add
            {
                this._onNodeMouseHover = (TreeNodeMouseHoverEventHandler)Delegate.Combine(this._onNodeMouseHover, value);
            }
            remove
            {
                this._onNodeMouseHover = (TreeNodeMouseHoverEventHandler)Delegate.Remove(this._onNodeMouseHover, value);
            }
        }
    
        public event TreeViewCancelEventHandler BeforeCollapse
        {
            add
            {
                this._onBeforeCollapse = (TreeViewCancelEventHandler)Delegate.Combine(this._onBeforeCollapse, value);
            }
            remove
            {
                this._onBeforeCollapse = (TreeViewCancelEventHandler)Delegate.Remove(this._onBeforeCollapse, value);
            }
        }

        public event TreeViewCancelEventHandler BeforeExpand
        {
            add
            {
                this._onBeforeExpand = (TreeViewCancelEventHandler)Delegate.Combine(this._onBeforeExpand, value);
            }
            remove
            {
                this._onBeforeExpand = (TreeViewCancelEventHandler)Delegate.Remove(this._onBeforeExpand, value);
            }
        }

        public event TreeViewCancelEventHandler BeforeSelect
        {
            add
            {
                this._onBeforeSelect = (TreeViewCancelEventHandler)Delegate.Combine(this._onBeforeSelect, value);
            }
            remove
            {
                this._onBeforeSelect = (TreeViewCancelEventHandler)Delegate.Remove(this._onBeforeSelect, value);
            }
        }

        public event TreeViewEventHandler AfterCollapse
        {
            add
            {
                this._onAfterCollapse = (TreeViewEventHandler)Delegate.Combine(this._onAfterCollapse, value);
            }
            remove
            {
                this._onAfterCollapse = (TreeViewEventHandler)Delegate.Remove(this._onAfterCollapse, value);
            }
        }

        public event TreeViewEventHandler AfterExpand
        {
            add
            {
                this._onAfterExpand = (TreeViewEventHandler)Delegate.Combine(this._onAfterExpand, value);
            }
            remove
            {
                this._onAfterExpand = (TreeViewEventHandler)Delegate.Remove(this._onAfterExpand, value);
            }
        }

        public event TreeViewEventHandler AfterSelect
        {
            add
            {
                this._onAfterSelect = (TreeViewEventHandler)Delegate.Combine(this._onAfterSelect, value);
            }
            remove
            {
                this._onAfterSelect = (TreeViewEventHandler)Delegate.Remove(this._onAfterSelect, value);
            }
        }


        internal ScrollViewer ElementScrollViewer
        {
            get
            {
                return this._elementScrollViewer;
            }
            set
            {
                this._elementScrollViewer = value;
            }
        }

        #endregion


        #region DependencyProperties


        public static DependencyProperty NodeTemplateProperty;
        public static DependencyProperty SelectedNodeProperty;
        public static DependencyProperty BackgroundProperty;

        public Brush Background
        {
            get
            {
                return this.GetValue(BackgroundProperty) as Brush;
            }
            set
            {
                base.SetValue(BackgroundProperty, value);
            }
        }

        public TreeNode SelectedNode
        {
            get
            {
                return this.GetValue(SelectedNodeProperty) as TreeNode;
            }
            set
            {
                base.SetValue(SelectedNodeProperty, value);
            }
        }

        public DataTemplate NodeTemplate
        {
            get
            {
                return this.GetValue(NodeTemplateProperty) as DataTemplate;
            }
            set
            {
                base.SetValue(NodeTemplateProperty, value);
            }
        }

        public static void NodeTemplatePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public static void SelectedNodePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion


        #region Constructors

        static TreeView()
        {
            TreeView.SelectedNodeProperty = DependencyProperty.Register("SelectedNode", typeof(TreeNode), typeof(TreeView), new PropertyChangedCallback(SelectedNodePropertyChangedCallback));
            TreeView.NodeTemplateProperty = DependencyProperty.Register("NodeTemplate", typeof(DataTemplate), typeof(TreeView), new PropertyChangedCallback(NodeTemplatePropertyChangedCallback));
            TreeView.BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(TreeView), null);
         }

        /// <summary>
        /// <para>Instanciate a new <see cref="TreeView"/>.</para>
        /// </summary>
        public TreeView()
        {
            ScrollViewer.SetHorizontalScrollBarVisibility(this, ScrollBarVisibility.Visible);
            ScrollViewer.SetVerticalScrollBarVisibility(this, ScrollBarVisibility.Auto);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ElementScrollViewer = base.GetTemplateChild("ScrollViewerElement") as ScrollViewer;
            if (this.ElementScrollViewer != null)
            {
                this.ElementScrollViewer.HorizontalScrollBarVisibility = ScrollViewer.GetHorizontalScrollBarVisibility(this);
                this.ElementScrollViewer.VerticalScrollBarVisibility = ScrollViewer.GetVerticalScrollBarVisibility(this);
            }
        }

        #endregion


        #region Events overriding

        protected internal virtual void OnAfterCollapse(TreeViewEventArgs e)
        {
            if (this._onAfterCollapse != null)
            {
                this._onAfterCollapse(this, e);
            }
        }

        protected virtual void OnAfterExpand(TreeViewEventArgs e)
        {
            if (this._onAfterExpand != null)
            {
                this._onAfterExpand(this, e);
            }
        }

        protected virtual void OnAfterSelect(TreeViewEventArgs e)
        {
            if (this._onAfterSelect != null)
            {
                this._onAfterSelect(this, e);
            }
        }

        protected internal virtual void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            if (this._onBeforeCollapse != null)
            {
                this._onBeforeCollapse(this, e);
            }
        }

        protected virtual void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            if (this._onBeforeExpand != null)
            {
                this._onBeforeExpand(this, e);
            }
        }

        protected virtual void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            if (this._onBeforeSelect != null)
            {
                this._onBeforeSelect(this, e);
            }
        }

        protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (this._onNodeMouseClick != null)
            {
                this._onNodeMouseClick(this, e);
            }
        }

        protected virtual void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            if (this._onNodeMouseDoubleClick != null)
            {
                this._onNodeMouseDoubleClick(this, e);
            }
        }

        protected virtual void OnNodeMouseHover(TreeNodeMouseHoverEventArgs e)
        {
            if (this._onNodeMouseHover != null)
            {
                this._onNodeMouseHover(this, e);
            }
        }

        #endregion


        #region Internal Access Methods

        internal void NotifyClicked(TreeNode node)
        {
            this.ComputeNodeSelection(node);
            TreeNodeMouseClickEventArgs tnmcea = new TreeNodeMouseClickEventArgs(node);
            this.OnNodeMouseClick(tnmcea);
        }

        internal void NotifyDoubleClicked(TreeNode node)
        {
            this.ComputeNodeSelection(node);
            TreeNodeMouseClickEventArgs tnmcea = new TreeNodeMouseClickEventArgs(node);
            this.OnNodeMouseDoubleClick(tnmcea);
        }

        internal void NotifyOvered(TreeNode node)
        {
            TreeNodeMouseHoverEventArgs tnmoea = new TreeNodeMouseHoverEventArgs(node);
            this.OnNodeMouseHover(tnmoea);
        }

        internal void NotifyExpanded(TreeNode node)
        {
            TreeViewEventArgs tvea = new TreeViewEventArgs(node);
            this.OnAfterExpand(tvea);
        }

        internal void NotifyCollapsed(TreeNode node)
        {
            TreeViewEventArgs tvea = new TreeViewEventArgs(node);
            this.OnAfterCollapse(tvea);
        }

        internal bool AskExpendingPermission(TreeNode node)
        {
            TreeViewCancelEventArgs tvcea = new TreeViewCancelEventArgs(node, false, TreeViewAction.Unknown);
            this.OnBeforeExpand(tvcea);
            return !tvcea.Cancel;
        }

        internal bool AskCollapsingPermission(TreeNode node)
        {
            TreeViewCancelEventArgs tvcea = new TreeViewCancelEventArgs(node, false, TreeViewAction.Unknown);
            this.OnBeforeCollapse(tvcea);
            return !tvcea.Cancel;
        }

        #endregion


        #region Private mehods

        private void ComputeNodeSelection(TreeNode node)
        {
            //always action = unknown
            if (node != null)
            {
                TreeViewCancelEventArgs tvcea = new TreeViewCancelEventArgs(node, false, TreeViewAction.Unknown);
                this.OnBeforeSelect(tvcea);

                if (tvcea.Cancel)
                    return;
            }

            if (this.SelectedNode != null)
            {
                this.SelectedNode.IsSelected = false;
            }

            if (node != null)
            {
                node.IsSelected = true;
            }

            this.SelectedNode = node;

            if (node != null)
            {
                TreeViewEventArgs tvea = new TreeViewEventArgs(node);
                this.OnAfterSelect(tvea);
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject nodeElement, object bindedValue)
        {
            base.PrepareContainerForItemOverride(nodeElement, bindedValue);
        }

        #endregion

    }
}
