using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hardcodet.Wpf.GenericTreeView;
using Hardcodet.Wpf.Samples.ViewModel;
using SampleShop.Products;

namespace Hardcodet.Wpf.Samples
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }


    #region context menu command handling

    /// <summary>
    /// Creates a sub category for the clicked item
    /// and refreshes the tree.
    /// </summary>
    private void AddCategory(object sender, ExecutedRoutedEventArgs e)
    {
      //get the processed item
      ShopCategory parent = GetCommandItem();

      //create a sub category
      string name = ShowInputDialog(null);
      ShopCategory subCategory = new ShopCategory(name, parent);
      parent.SubCategories.Add(subCategory);

      //make sure the parent is expanded
      CategoryTree.TryFindNode(parent).IsExpanded = true;

      //NOTE this would be an alternative to force layout preservation
      //even if the PreserveLayoutOnRefresh property was false:
      //TreeLayout layout = CategoryTree.GetTreeLayout();
      //CategoryTree.Refresh(layout);

      //Important - mark the event as handled
      e.Handled = true;
    }


    /// <summary>
    /// Checks whether it is allowed to delete a category, which is only
    /// allowed for nested categories, but not the root items.
    /// </summary>
    private void EvaluateCanDelete(object sender, CanExecuteRoutedEventArgs e)
    {
      //get the processed item
      ShopCategory item = GetCommandItem();

      e.CanExecute = item.ParentCategory != null;
      e.Handled = true;
    }


    /// <summary>
    /// Deletes the currently processed item. This can be a right-clicked
    /// item (context menu) or the currently selected item, if the user
    /// pressed delete.
    /// </summary>
    private void DeleteCategory(object sender, ExecutedRoutedEventArgs e)
    {
      //get item
      ShopCategory item = GetCommandItem();

      //remove from parent
      item.ParentCategory.SubCategories.Remove(item);

      //mark event as handled
      e.Handled = true;
    }


    /// <summary>
    /// Determines the item that is the source of a given command.
    /// As a command event can be routed from a context menu click
    /// or a short-cut, we have to evaluate both possibilities.
    /// </summary>
    /// <returns></returns>
    private ShopCategory GetCommandItem()
    {
      //get the processed item
      ContextMenu menu = CategoryTree.NodeContextMenu;
      if (menu.IsVisible)
      {
        //a context menu was clicked
        TreeViewItem treeNode = (TreeViewItem) menu.PlacementTarget;
        return (ShopCategory) treeNode.Header;
      }
      else
      {
        //the context menu is closed - the user has pressed a shortcut
        return CategoryTree.SelectedItem;
      }
    }

    #endregion


    #region tree modification

    /// <summary>
    /// Sets or removes a custom root node for the bound
    /// <see cref="ShopCategory"/> items.
    /// </summary>
    private void ToggleRootNode(object sender, RoutedEventArgs e)
    {
      if (CategoryTree.RootNode == null)
      {
        //create a dummy root node
        TreeViewItem rootNode = (TreeViewItem) FindResource("CustomRootNode");
        CategoryTree.RootNode = rootNode;
      }
      else
      {
        //disable artificial root node
        CategoryTree.RootNode = null;
      }
    }


    /// <summary>
    /// Enables / disables the node's context menu.
    /// </summary>
    private void ToggleContextMenu(object sender, RoutedEventArgs e)
    {
      if (CategoryTree.NodeContextMenu == null)
      {
        //the menu is declared as a resource of the window
        ContextMenu menu = (ContextMenu) FindResource("CategoryMenu");
        CategoryTree.NodeContextMenu = menu;
      }
      else
      {
        CategoryTree.NodeContextMenu = null;
      }
    }


    /// <summary>
    /// Sets or resets the style to be applied on the tree's
    /// nodes.
    /// </summary>
    private void ToggleNodeStyle(object sender, RoutedEventArgs e)
    {
      if (CategoryTree.TreeNodeStyle == null)
      {
        Style style = (Style) FindResource("SimpleFolders");
        CategoryTree.TreeNodeStyle = style;
      }
      else
      {
        //setting the style to null does not clear the existing
        //styles (in order to preserve default layout)
        //-> refresh tree
        CategoryTree.TreeNodeStyle = null;
        CategoryTree.Refresh(CategoryTree.GetTreeLayout());
      }
    }


    /// <summary>
    /// Just triggers a refresh of the view models data. The
    /// resulting <see cref="ShopModel.PropertyChanged"/>
    /// event is enough to trigger a refresh of the tree's
    /// items.
    /// </summary>
    private void ReloadData(object sender, RoutedEventArgs e)
    {
      //the shop instance is declared as a resource of the window
      ShopModel model = GetShop();
      model.RefreshData();
    }


    /// <summary>
    /// Copies the layout of one tree to the other.
    /// </summary>
    private void CopyTreeLayout(object sender, RoutedEventArgs e)
    {
      TreeLayout layout = CategoryTree.GetTreeLayout();
      SynchronizedTree.Refresh(layout);
    }

    #endregion


    #region expand / collapse

    private void ExpandAll(object sender, RoutedEventArgs e)
    {
      CategoryTree.ExpandAll();
    }


    private void CollapseAll(object sender, RoutedEventArgs e)
    {
      CategoryTree.CollapseAll();
    }

    #endregion


    #region util

    /// <summary>
    /// Gets the view model to which the trees are bound.
    /// </summary>
    /// <returns>View model.</returns>
    private ShopModel GetShop()
    {
      return (ShopModel) FindResource("Shop");
    }
    
    /// <summary>
    /// Displays an input dialog and returns the entered
    /// value.
    /// </summary>
    private string ShowInputDialog(string defaultValue)
    {
      InputDialog dlg = new InputDialog();
      dlg.CategoryName = defaultValue;
      dlg.Owner = this;
      dlg.ShowDialog();

      return dlg.CategoryName;
    }


    private void ShowAboutDialog(object sender, RoutedEventArgs e)
    {
      AboutDialog dlg = new AboutDialog();
      dlg.Owner = this;
      Shadow.Visibility = Visibility.Visible;
      dlg.ShowDialog();
      Shadow.Visibility = Visibility.Collapsed;
    }

    #endregion


    #region SelectedItemChanged event

    /// <summary>
    /// Handles the tree's <see cref="TreeViewBase{T}.SelectedItemChanged"/>
    /// event and updates the status bar.
    /// </summary>
    private void OnSelectedItemChanged(object sender, RoutedTreeItemEventArgs<ShopCategory> e)
    {
      txtOldItem.Text = String.Format("'{0}'", e.OldItem);
      txtNewItem.Text = String.Format("'{0}'", e.NewItem);
    }

    #endregion


    #region change sort order

    /// <summary>
    /// Sets the sort order of the reference tree.
    /// </summary>
    private void ChangeSortOrder(object sender, RoutedEventArgs e)
    {
      bool asc = (bool)rbAscending.IsChecked;
      string resourceName = asc ? "AscendingNames" : "DescendingNames";

      IEnumerable<SortDescription>sorts = (IEnumerable<SortDescription>) FindResource(resourceName);
      CategoryTree.NodeSortDescriptions =  sorts;
    }

    #endregion


    /// <summary>
    /// Selects a given item on the tree if possible.
    /// </summary>
    private void SelectItem(object sender, RoutedEventArgs e)
    {
      string name = CategoryTree.SelectedItem == null ? null : CategoryTree.SelectedItem.CategoryName;
      name = ShowInputDialog(name);

      //if the model does not contain a matching category, just create
      //a dummy and create an exception
      ShopModel shop = GetShop();
      ShopCategory category = shop.TryFindCategoryByName(name);
      if (category == null)
      {
        category = CreateDummy(name, shop);
      }

      try
      {
        CategoryTree.SelectedItem = category;
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.ToString(), "", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }


    /// <summary>
    /// Creates a random dummy item that is not part of the model's
    /// infrastructure. Using it will create an exception with the
    /// tree.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="shop"></param>
    /// <returns></returns>
    private ShopCategory CreateDummy(string category, ShopModel shop)
    {
      ShopCategory parent = null;
      Random rnd = new Random();
      int level = rnd.Next(0, 3);
      for(int i=0; i<level; i++)
      {
        if (parent == null)
        {
          //select a root item
          int index = rnd.Next(0, shop.Categories.Count);
          parent = shop.Categories[index];
        }
        else
        {
          //select a child item
          if (parent.SubCategories.Count == 0) break;
          int index = rnd.Next(0, parent.SubCategories.Count);
          parent = parent.SubCategories[index];
        }
      }

      return new ShopCategory(category, parent);
    }
  }
}