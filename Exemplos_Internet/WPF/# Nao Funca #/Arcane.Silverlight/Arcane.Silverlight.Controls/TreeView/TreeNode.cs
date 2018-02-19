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
    [TemplatePart(Name = "Normal Expand State", Type = typeof(Storyboard)),
     TemplatePart(Name = "Normal Collapse State", Type = typeof(Storyboard)),
     TemplatePart(Name = "Selected Collapse State", Type = typeof(Storyboard)),
     TemplatePart(Name = "Selected Expand State", Type = typeof(Storyboard)),
     TemplatePart(Name = "NodeIcon Expand State", Type = typeof(Storyboard)),
     TemplatePart(Name = "NodeIcon Collapse State", Type = typeof(Storyboard)),
     TemplatePart(Name = "MouseOver Collapse State", Type = typeof(Storyboard)),
     TemplatePart(Name = "MouseOver Expand State", Type = typeof(Storyboard)),
     TemplatePart(Name = "RootElement", Type = typeof(FrameworkElement)),
     TemplatePart(Name = "ExpandedNodeIconZone", Type = typeof(FrameworkElement)),
     TemplatePart(Name = "ContentZone", Type = typeof(FrameworkElement)),
     TemplatePart(Name = "NodesPresenter", Type = typeof(FrameworkElement)),
     TemplatePart(Name = "SelectionZone", Type = typeof(FrameworkElement))]
    public class TreeNode : ContentControl
    {

        #region Constants

        private const string ElementRootName = "RootElement";
        private const string ElementSelectionZoneName = "SelectionZone";
        private const string ElementContentZoneName = "ContentZone";
        private const string ElementExpandCollapseZoneName = "ExpandCollapseZone";
        private const string ElementNodesPresenterName = "NodesPresenter";
        private const string StateNodeIconCollapseName = "NodeIcon Collapse State";
        private const string StateNodeIconExpandName = "NodeIcon Expand State";
        private const string StateNormalExpandName = "Normal Expand State";
        private const string StateMouseOverCollapseName = "MouseOver Collapse State";
        private const string StateMouseOverExpandName = "MouseOver Expand State";
        private const string StateSelectedCollapseName = "Selected Collapse State";
        private const string StateSelectedExpandName = "Selected Expand State";
        private const string StateNormalCollapseName = "Normal Collapse State";
        private const double DoubleClickSpan = 400;

        #endregion


        #region Fields

        private Storyboard _stateNormalExpand;
        private Storyboard _stateNormalCollapse;
        private Storyboard _stateNodeIconCollapse;
        private Storyboard _stateNodeIconExpand;
        private Storyboard _stateMouseOverExpand;
        private Storyboard _stateMouseOverCollapse;
        private Storyboard _stateSelectedExpand;
        private Storyboard _stateSelectedCollapse;
        private FrameworkElement _elementFocusVisual;
        private FrameworkElement _elementRoot;
        private FrameworkElement _elementSelectionZone;
        private FrameworkElement _elementExpandCollapseZone;
        private FrameworkElement _elementContentZone;
        private FrameworkElement _elementNodesPresenter;
        private DateTime _lastClickTime;
        private Storyboard _currentState;
        private Storyboard _currentNodeState;

        #endregion


        #region Properties


        public TreeView TreeView
        {
            get
            {
                FrameworkElement element = this;

                while (!(element is TreeView) || (element == null))
                    element = element.Parent as FrameworkElement;

                return element as TreeView;
            }
        }

        private FrameworkElement ElementContentZone
        {

            get
            {
                return this._elementContentZone;
            }
            set
            {
                this._elementContentZone = value;
            }
        }

        private FrameworkElement ElementSelectionZone
        {

            get
            {
                return this._elementSelectionZone;
            }
            set
            {
                this._elementSelectionZone = value;
            }
        }

        private FrameworkElement ElementNodesPresenter
        {

            get
            {
                return this._elementNodesPresenter;
            }
            set
            {
                this._elementNodesPresenter = value;
            }
        }

        private FrameworkElement ElementExpandCollapseZone
        {

            get
            {
                return this._elementExpandCollapseZone;
            }
            set
            {
                this._elementExpandCollapseZone = value;
            }
        }

        private FrameworkElement ElementFocusVisual
        {

            get
            {
                return this._elementFocusVisual;
            }
            set
            {
                this._elementFocusVisual = value;
            }
        }

        private FrameworkElement ElementRoot
        {
            get
            {
                return this._elementRoot;
            }
            set
            {
                this._elementRoot = value;
            }
        }

        public bool IsMouseOver
        {
            get
            {
                return (bool)this.GetValue(IsMouseOverProperty);
            }
            set
            {
                base.SetValue(IsMouseOverProperty, value);
            }
        }

        public bool IsExpanded
        {
            get
            {
                return (bool)this.GetValue(IsExpandedProperty);
            }
            set
            {
                base.SetValue(IsExpandedProperty, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return (bool)this.GetValue(IsSelectedProperty);
            }
            set
            {
                base.SetValue(IsSelectedProperty, value);
            }
        }

        private Storyboard StateNodeIconExpand
        {
            get
            {
                return this._stateNodeIconExpand;
            }
            set
            {
                this._stateNodeIconExpand = value;
            }
        }

        private Storyboard StateNodeIconCollapse
        {
            get
            {
                return this._stateNodeIconCollapse;
            }
            set
            {
                this._stateNodeIconCollapse = value;
            }
        }

        private Storyboard StateMouseOverExpand
        {
            get
            {
                return this._stateMouseOverExpand;
            }
            set
            {
                this._stateMouseOverExpand = value;
            }
        }

        private Storyboard StateMouseOverCollapse
        {
            get
            {
                return this._stateMouseOverCollapse;
            }
            set
            {
                this._stateMouseOverCollapse = value;
            }
        }

        private Storyboard StateNormalExpand
        {
            get
            {
                return this._stateNormalExpand;
            }
            set
            {
                this._stateNormalExpand = value;
            }
        }

        private Storyboard StateNormalCollapse
        {
            get
            {
                return this._stateNormalCollapse;
            }
            set
            {
                this._stateNormalCollapse = value;
            }
        }

        private Storyboard StateSelectedExpand
        {
            get
            {
                return this._stateSelectedExpand;
            }
            set
            {
                this._stateSelectedExpand = value;
            }
        }

        private Storyboard StateSelectedCollapse
        {
            get
            {
                return this._stateSelectedCollapse;
            }
            set
            {
                this._stateSelectedCollapse = value;
            }
        }

        private Storyboard CurrentState
        {
            get
            {
                return this._currentState;
            }
            set
            {
                this._currentState = value;
            }
        }

        private Storyboard CurrentNodeState
        {
            get
            {
                return this._currentNodeState;
            }
            set
            {
                this._currentNodeState = value;
            }
        }


        internal object Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        #endregion


        #region DependencyProperties

        public static DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(TreeNode), new PropertyChangedCallback(TreeNode.OnIsExpandedChanged));
        public static DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(TreeNode), new PropertyChangedCallback(TreeNode.OnIsSelectedChanged));
        public static DependencyProperty IsMouseOverProperty = DependencyProperty.Register("IsMouseOver", typeof(bool), typeof(TreeNode), new PropertyChangedCallback(TreeNode.OnIsMouseOverChanged));

        public static DependencyProperty NodesProperty;

        public TreeNodeCollection Nodes
        {
            get
            {
                return this.GetValue(TreeNode.NodesProperty) as TreeNodeCollection;
            }
            set
            {
                base.SetValue(TreeNode.NodesProperty, value);
            }
        }

        public static void NodesPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             if (e.OldValue == e.NewValue)
                return;
            if (e.NewValue != null)
                (e.NewValue as FrameworkElement).Visibility = Visibility.Collapsed;
            if ((d as TreeNode).ElementNodesPresenter != null)
                ((d as TreeNode).ElementNodesPresenter as ContentPresenter).Content = e.NewValue;
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TreeNode).ChangeVisualState();
        }

        private static void OnIsMouseOverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TreeNode).ChangeVisualState();
        }

        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TreeNode).ChangeVisualState();
        }

        #endregion


        #region Constructors

        static TreeNode()
        {
            TreeNode.NodesProperty = DependencyProperty.Register("Nodes", typeof(TreeNodeCollection), typeof(TreeNode), new PropertyChangedCallback(NodesPropertyChangedCallback));
        }

        /// <summary>
        /// <para>Instanciate a new <see cref="TreeNode"/> object.</para>
        /// </summary>
        public TreeNode()
        {
            base.TabNavigation = KeyboardNavigationMode.Local;
            base.IsTabStop = true;
        }



        #endregion


        #region Public methods


        public void Toggle()
        {
            if (!this.IsExpanded)
            {
                this.Expand();
            }
            else
                Collapse();
        }

        public void Collapse()
        {
            if (this.IsExpanded)
            {
                if (this.TreeView != null)
                {
                    if (this.TreeView.AskCollapsingPermission(this))
                        this.IsExpanded = !this.IsExpanded;
                    this.TreeView.NotifyCollapsed(this);
                }
                else
                    this.IsExpanded = !this.IsExpanded;
            }
        }

        public void Expand()
        {
            if (!this.IsExpanded)
            {
                if (this.TreeView != null)
                {
                    if (this.TreeView.AskExpendingPermission(this))
                        this.IsExpanded = !this.IsExpanded;
                    this.TreeView.NotifyExpanded(this);
                }
                else
                    this.IsExpanded = !this.IsExpanded;
            }
        }


        #endregion


        #region Template Management

        internal void ChangeVisualState()
        {
            {
                Storyboard storyboard = null;
                Storyboard nodestoryboard = null;

                if (this.IsSelected)
                    Console.Out.WriteLine();

                if (this.IsExpanded)
                {
                    if (this.Nodes != null)
                    {
                        if ((this.ElementNodesPresenter as ContentPresenter).Content != null)
                            ((FrameworkElement)(this.ElementNodesPresenter as ContentPresenter).Content).Visibility = Visibility.Visible;// = this.Nodes;
                    }
                    storyboard = (this.IsSelected ? (_stateSelectedExpand ?? _stateNormalExpand) : (this.IsMouseOver ? (_stateMouseOverExpand ?? _stateNormalExpand) : _stateNormalExpand));
                }

                else
                {
                    if ((this.ElementNodesPresenter as ContentPresenter).Content != null)
                        ((FrameworkElement)(this.ElementNodesPresenter as ContentPresenter).Content).Visibility = Visibility.Collapsed;// = this.Nodes;
                    //(this.ElementNodesPresenter as ContentPresenter).Content = null;
                    storyboard = (this.IsSelected ? (_stateSelectedCollapse ?? _stateNormalCollapse) : (this.IsMouseOver ? (_stateMouseOverCollapse ?? _stateNormalCollapse) : _stateNormalCollapse));
                }

                if ((storyboard != null) && (this.CurrentState != storyboard))
                {
                    try
                    {
                        storyboard.Begin();
                        Storyboard currentState = this.CurrentState;
                        this.CurrentState = storyboard;
                        if (currentState != null)
                        {
                            currentState.Stop();
                        }
                    }
                    catch
                    {
                    }
                }

                nodestoryboard = (this.IsExpanded ? this.StateNodeIconExpand : this.StateNormalCollapse);

                if ((nodestoryboard != null) && (this.CurrentNodeState != nodestoryboard))
                {
                    try
                    {
                        nodestoryboard.Begin();
                        Storyboard currentNodeState = this.CurrentNodeState;
                        this.CurrentNodeState = nodestoryboard;
                        if (currentNodeState != null)
                        {
                            currentNodeState.Stop();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ElementRoot = base.GetTemplateChild(TreeNode.ElementRootName) as FrameworkElement;
            if (this.ElementRoot != null)
            {
                this.StateNormalCollapse = this.ElementRoot.Resources[TreeNode.StateNormalCollapseName] as Storyboard;
                this.StateNormalExpand = this.ElementRoot.Resources[TreeNode.StateNormalExpandName] as Storyboard;
                this.StateSelectedCollapse = this.ElementRoot.Resources[TreeNode.StateSelectedCollapseName] as Storyboard;
                this.StateSelectedExpand = this.ElementRoot.Resources[TreeNode.StateSelectedExpandName] as Storyboard;
                this.StateMouseOverCollapse = this.ElementRoot.Resources[TreeNode.StateMouseOverCollapseName] as Storyboard;
                this.StateMouseOverExpand = this.ElementRoot.Resources[TreeNode.StateMouseOverExpandName] as Storyboard;
                this.StateNodeIconCollapse = this.ElementRoot.Resources[TreeNode.StateNodeIconCollapseName] as Storyboard;
                this.StateNodeIconExpand = this.ElementRoot.Resources[TreeNode.StateNodeIconExpandName] as Storyboard;
            }

            this.ElementContentZone = base.GetTemplateChild(TreeNode.ElementContentZoneName) as FrameworkElement;
            this.ElementSelectionZone = base.GetTemplateChild(TreeNode.ElementSelectionZoneName) as FrameworkElement;
            this.ElementExpandCollapseZone = base.GetTemplateChild(TreeNode.ElementExpandCollapseZoneName) as FrameworkElement;
            this.ElementNodesPresenter = base.GetTemplateChild(TreeNode.ElementNodesPresenterName) as FrameworkElement;
 
            if (this.ElementNodesPresenter != null)
            {
                if (this.Nodes != null)
                    this.Nodes.Visibility = Visibility.Collapsed;
                (this.ElementNodesPresenter as ContentPresenter).Content = this.Nodes;
            }
            if (this.ElementExpandCollapseZone != null)
                this.ElementExpandCollapseZone.MouseLeftButtonDown += new MouseButtonEventHandler(ExpandCollapseZone_MouseLeftButtonDown);
            if (this.ElementSelectionZone != null)
            {
                this.ElementSelectionZone.MouseLeftButtonDown += new MouseButtonEventHandler(ElementContentZone_MouseLeftButtonDown);
                this.ElementSelectionZone.MouseEnter += new MouseEventHandler(ElementSelectionZone_MouseEnter);
                this.ElementSelectionZone.MouseLeave += new MouseEventHandler(ElementSelectionZone_MouseLeave);
            }
            this.RefreshChilds();
        }

        void ElementSelectionZone_MouseLeave(object sender, MouseEventArgs e)
        {
            this.IsMouseOver = false;
        }

        void ElementSelectionZone_MouseEnter(object sender, MouseEventArgs e)
        {
            this.IsMouseOver = true;
        }


        /// <summary>
        /// <para>Click on node</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ElementContentZone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((DateTime.Now - this._lastClickTime).TotalMilliseconds <= DoubleClickSpan)
            {
                this.Toggle();
                this.NotifyDoubleClickToTreeView();
            }
            else
                this.NotifyClickToTreeView();
            this._lastClickTime = DateTime.Now;
            this.NotifySelectionToTreeView();
        }

        /// <summary>
        /// Click on expand collapse zone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ExpandCollapseZone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Toggle();
        }

        #endregion


        #region Internal notificaton methods


        internal void NotifySelectionToTreeView()
        {
            if (this.TreeView != null)
                this.TreeView.SelectedNode = this;
        }

        internal void NotifyDoubleClickToTreeView()
        {
            if (this.TreeView != null)
                this.TreeView.NotifyDoubleClicked(this);
        }

        internal void NotifyClickToTreeView()
        {

            if (this.TreeView != null)
                this.TreeView.NotifyClicked(this);
        }

        #endregion





        private object item;




 

        // Properties




        internal void RefreshChilds()
        {
            if (this.Nodes != null)
            {
                if (this.Nodes.Items.Count > 0)
                {
                    if (this.ElementExpandCollapseZone != null)
                        this.ElementExpandCollapseZone.Visibility = Visibility.Visible;

                }
                return;
            }
            if (this.ElementExpandCollapseZone != null)
                this.ElementExpandCollapseZone.Visibility = Visibility.Collapsed;
        }
    }
}
