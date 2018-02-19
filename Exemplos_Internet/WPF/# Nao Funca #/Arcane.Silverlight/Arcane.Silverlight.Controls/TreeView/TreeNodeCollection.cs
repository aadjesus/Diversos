
#region Usings

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

#endregion

namespace Arcane.Silverlight.Controls
{

    /// <summary>
    /// <para>Defines a node collection.</para>
    /// </summary>
    [TemplatePart(Name="ItemsHost", Type=typeof(FrameworkElement))]
    public class TreeNodeCollection : ItemsControl
    {

        #region Fields

        private Dictionary<object, TreeNode> _objectToTreeNode;
        private IList _itemsSourceAsList;


        #endregion


        #region Properties

        // Events
        public event SelectionChangedEventHandler SelectionChanged;


        public TreeView TreeView
        {
            get
            {
                FrameworkElement element = this;

                while (!(element is TreeView) && (element != null))
                    element = element.Parent as FrameworkElement;

                return element as TreeView;
            }
        }

        public TreeNode TreeNode
        {
            get
            {
                FrameworkElement element = this;

                while ((!(element is TreeNode) && (element != null)))
                    element = element.Parent as FrameworkElement;

                return element as TreeNode;
            }
        }

        private IDictionary<object, TreeNode> ObjectToTreeNode
        {
            get
            {
                if (this._objectToTreeNode == null)
                {
                    this._objectToTreeNode = new Dictionary<object, TreeNode>();
                }
                return this._objectToTreeNode;
            }
        }

        #endregion

        
        #region DependencyProperties


        #region ItemsHostProperty

        public static readonly DependencyProperty ItemsHostProperty;

        /// <summary>
        /// <para>Gets treeview 's list of item.</para>
        /// </summary>
        public new Panel ItemsHost
        {
            get
            {
                return (Panel)this.GetValue(TreeNodeCollection.ItemsHostProperty);
            }
        }

        public static void ItemsHostPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion


        #region ItemContainerStyleProperty

        private static DependencyProperty ItemContainerStyleProperty;

        public Style ItemContainerStyle
        {
            get
            {
                return (Style)this.TreeView.GetValue(ItemContainerStyleProperty);
            }
            set
            {
                this.TreeView.SetValue(ItemContainerStyleProperty, value);
            }
        }
       

        private static void OnItemContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TreeNodeCollection).OnItemContainerStyleChanged((Style)e.OldValue, (Style)e.NewValue);
        }

        #endregion


        #region ItemsProperty

        public static readonly DependencyProperty ItemsProperty;

        /// <summary>
        /// <para>Gets carousel's list of item.</para>
        /// </summary>
        public new ObservableCollection<object> Items
        {
            get
            {
                return (ObservableCollection<object>)this.GetValue(TreeNodeCollection.ItemsProperty);
            }
        }

        public static void ItemsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           /* INotifyCollectionChanged newValue;
            TreeNodeCollection control = d as TreeNodeCollection;


            if ((e.OldValue != null) && (e.OldValue is INotifyCollectionChanged))
            {
                newValue = e.OldValue as INotifyCollectionChanged;
                newValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(control.OnCollectionChanged);
            }
            control.ClearVisualChildren();

            newValue = e.NewValue as INotifyCollectionChanged;
            if (newValue != null)
            {
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(control.OnCollectionChanged);
            }*/
        }

        #endregion


        #endregion


        #region Constructors

        static TreeNodeCollection()
        {
            TreeNodeCollection.ItemContainerStyleProperty = DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(TreeNodeCollection), new PropertyChangedCallback(TreeNodeCollection.OnItemContainerStyleChanged));
            TreeNodeCollection.ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<object>), typeof(TreeNodeCollection), new PropertyChangedCallback(ItemsPropertyChangedCallback));
            TreeNodeCollection.ItemsHostProperty = DependencyProperty.Register("ItemsHost", typeof(Panel), typeof(TreeNodeCollection), null);
        }

        /// <summary>
        /// <para>Instanciate a new <see cref="NodesCollection"/>.</para>
        /// </summary>
        public TreeNodeCollection()
        {
           // this.ItemsSource = new ObservableCollection<object>();
            ObservableCollection<object> observables = new ObservableCollection<object>();
            observables.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
            this.SetValue(TreeNodeCollection.ItemsProperty, observables);

            StackPanel panel = new StackPanel();
            base.SetValue(TreeNodeCollection.ItemsHostProperty, panel);

            KeyEventHandler handler = null;
            base.TabNavigation = KeyboardNavigationMode.Once;
            base.IsTabStop = false;
            if (handler == null)
            {
                handler = delegate(object sender, KeyEventArgs e)
                {
                    this.OnKeyDown(e);
                };
            }
            base.KeyDown += handler;
        }

        #endregion


        #region Public methods

        public void Clear()
        {
            (this.Items as ObservableCollection<object>).Clear();
        }

        public TreeNode Add(object data)
        {
            (this.Items as ObservableCollection<object>).Add(data);

            return GetTreeNodeForObject(data);
        }

        public List<TreeNode> Add(params object[] datas)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (object @object in datas)
            {
                (this.Items as ObservableCollection<object>).Add(@object);
                nodes.Add(GetTreeNodeForObject(@object));
            }
            return nodes;
        }

        public TreeNode Add(TreeNode node)
        {
            return null;
        }

        public TreeNode GetTreeNodeForObject(object value)
        {
            TreeNode item = value as TreeNode;
            if (item == null)
            {
                this.ObjectToTreeNode.TryGetValue(value, out item);
            }
            return item;
        }

        public TreeNode FindNode(string name)
        {
            foreach (Object o in this.Items)
            {
                FrameworkElement element = o as FrameworkElement;
                TreeNode node = element as TreeNode;

                if (node == null)
                    continue;

                if (element.Name.Equals(name))
                    return node;
                else
                {
                    if (node != null)
                    {
                        if (node.Nodes != null)
                            return (element as TreeNode).Nodes.FindNode(name);
                    }
                }
            }
            return null;
        }

        #endregion


        #region Template, style and keyboards


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.GetTemplateChild("ItemsHost") != null)
                (this.GetTemplateChild("ItemsHost") as Panel).Children.Add(this.ItemsHost);
        }

        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            if (!e.Handled)
            {
                bool flag = false;
                int num = -1;
                switch (e.Key)
                {
                    case Key.Enter:
                        if (Key.Enter != e.Key)
                        {

                        }
                        break;

                    case Key.PageUp:

                        break;

                    case Key.PageDown:

                        break;

                    case Key.End:

                        break;

                    case Key.Home:

                        break;

                    case Key.Left:

                        break;

                    case Key.Up:

                        break;

                    case Key.Right:

                        break;

                    case Key.Down:

                        //this.ElementScrollViewerScrollInDirection(Key.Down);

                        break;
                }

                if (flag)
                {
                    e.Handled = true;
                }
            }
        }

        protected virtual void OnItemContainerStyleChanged(Style oldItemContainerStyle, Style newItemContainerStyle)
        {
            foreach (object obj2 in this.Items)
            {
                TreeNode treeNodeItemForObject = this.GetTreeNodeForObject(obj2);
                if ((treeNodeItemForObject != null) && ((treeNodeItemForObject.Style == null) || (oldItemContainerStyle == treeNodeItemForObject.Style)))
                {
                    if (treeNodeItemForObject.Style != null)
                    {
                        throw new NotSupportedException(Resource.Treeview_OnItemContainerStyleChanged_CanNotSetStyle);
                    }
                    treeNodeItemForObject.Style = newItemContainerStyle;
                }
            }
        }



        #endregion


        #region Collection of item management

        private void ClearVisualChildren(IList items)
        {
            if (((items != null) && (this.ItemsHost != null)) && (this.ItemsHost.Children != null))
            {
                UIElementCollection children = this.ItemsHost.Children;
                int count = this.ItemsHost.Children.Count;
                while (0 < count)
                {
                    count--;
                    object item = (count < items.Count) ? items[count] : null;
                    this.ClearContainerForItemOverride(children[count], item);
                }
                this.ItemsHost.Children.Clear();
            }
        }



        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (((e.NewItems != null) && (e.NewItems.Count != 1)) || ((e.OldItems != null) && (e.OldItems.Count != 1)))
            {
                throw new NotSupportedException("");
            }
            UIElementCollection elements = (this.ItemsHost == null) ? null : this.ItemsHost.Children;


            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.UpdateContainerForItem(e.NewStartingIndex);

                    return;

                case NotifyCollectionChangedAction.Remove:

                    return;

                case NotifyCollectionChangedAction.Replace:

                    return;

                case NotifyCollectionChangedAction.Reset:
                    this.ClearVisualChildren(this.GetItems());
                    break;

                default:
                    return;
            }
        }

        private void UpdateContainerForItem(int index)
        {
            IList items = this.GetItems();
            if ((items != null) && (items.Count > index))
            {
                object item = items[index];
                if (item == null)
                {
                    this.UpdateVisualChild(index, null);
                }
                else
                {
                    UIElement itemElement = null;
                    if (this.IsItemItsOwnContainerOverride(item))
                    {
                        itemElement = item as UIElement;
                        if (itemElement == null)
                        {
                            throw new NotSupportedException("ItemsControl.Items must be a derivative of type UIElement");
                        }
                        if ((this._itemsSourceAsList != null) && (this.ItemTemplate != null))
                        {
                            throw new NotSupportedException("ItemsControl.Items must not be a UIElement type when an ItemTemplate is set");
                        }
                    }
                    else
                    {
                        itemElement = this.GetContainerForItemOverride() as UIElement;
                        if (itemElement == null)
                        {
                            throw new NotSupportedException("ItemsControl.GetContainerForItemOverride must return a derivative of UIElement");
                        }
                    }
                    this.UpdateVisualChild(index, itemElement);
                    FrameworkElement element2 = itemElement as FrameworkElement;
                    if (element2 != null)
                    {
                        element2.DataContext = item;
                    }
                    this.PrepareContainerForItemOverride(itemElement, item);
                }
            }
        }



        protected override void ClearContainerForItemOverride(DependencyObject nodeElement, object bindedValue)
        {
            base.ClearContainerForItemOverride(nodeElement, bindedValue);
            TreeNode node = nodeElement as TreeNode;
            if (bindedValue == null)
            {
                bindedValue = (node.Item == null) ? node : node.Item;
            }

            node.IsSelected = false;
            if (node != bindedValue)
            {
                this.ObjectToTreeNode.Remove(bindedValue);
            }

            if (this.TreeNode != null)
                this.TreeNode.RefreshChilds();
        }

  
        private void UpdateVisualChild(int index, UIElement itemElement)
        {
            UIElementCollection children = this.ItemsHost.Children;
            for (int i = children.Count; i < index; i++)
            {
                Rectangle rectangle;
                rectangle = new Rectangle
                {
                    Height = Width = 0.0
                };
                children.Add(rectangle);
            }
            if (itemElement == null)
            {
                Rectangle rectangle2;
                itemElement = new Rectangle { Height = Width = 0.0 };
            }
            if (index < this.ItemsHost.Children.Count)
            {
                children.Insert(index, itemElement);
            }
            else
            {
                children.Add(itemElement);
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            TreeNode item = new TreeNode();

            if (this.ItemContainerStyle != null)
            {
                item.Style = this.ItemContainerStyle;
            }

            return item;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is TreeNode)
                return true;
            return false;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject nodeElement, object bindedValue)
        {
            base.PrepareContainerForItemOverride(nodeElement, bindedValue);
            TreeNode node = nodeElement as TreeNode;
            this.ObjectToTreeNode.Add(bindedValue, node);
            bool flag = true;
            if (node != bindedValue)
            {
                if (this.TreeView.ItemTemplate != null)
                {
                    node.ContentTemplate = this.TreeView.ItemTemplate;
                }
                node.Item = bindedValue;
                if (flag)
                {
                    node.Content = bindedValue;
                }
                this.ObjectToTreeNode[bindedValue] = node;
            }
            if ((this.ItemContainerStyle != null) && (node.Style == null))
            {
                node.Style = this.ItemContainerStyle;
            }

            if (this.TreeNode != null)
                this.TreeNode.RefreshChilds();
        }

        private IList GetItems()
        {
            return (this._itemsSourceAsList ?? (this.GetValue(ItemsProperty) as IList));
        }
        #endregion

    }
}
