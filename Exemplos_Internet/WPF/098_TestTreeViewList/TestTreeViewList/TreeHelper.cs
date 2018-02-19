using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestTreeViewList
{
    /// <summary>
    /// Helper methods to go through the logical and the visual tree
    /// </summary>
    public class TreeHelper
    {
        /// <summary>
        /// Find all logical children of a given type
        /// </summary>
        /// <typeparam name="T">Type of the children to find</typeparam>
        /// <param name="obj">Source dependency object</param>
        /// <returns>A list of T objects that are logical children of the source dependency object</returns>
        public static List<T> FindLogicalChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            List<T> matches = new List<T>();
            return FindLogicalChildren(obj, matches);
        }

        /// <summary>
        /// Find all visual children of a given type
        /// </summary>
        /// <typeparam name="T">Type of the children to find</typeparam>
        /// <param name="obj">Source dependency object</param>
        /// <returns>A list of T objects that are visual children of the source dependency object</returns>
        public static List<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            List<T> matches = new List<T>();
            return FindVisualChildren(obj, matches);
        }

        /// <summary>
        /// Determiner whether an element is the logical ancestor of another item
        /// </summary>
        /// <param name="parent">Parent element</param>
        /// <param name="source">Child element</param>
        /// <returns>True if the Child element has the Parent element as its parent in the logical tree</returns>
        public static bool IsLogicalAncestorOf(FrameworkElement parent, FrameworkElement source)
        {
            if(parent == source)
            {
                return true;
            }

            FrameworkElement current = source;
            while(current != null && current.Parent != null)
            {
                if(current.Parent == parent)
                {
                    return true;
                }

                current = current.Parent as FrameworkElement;
            }

            return false;
        }

        /// <summary>
        /// Find the first logical child of a given type
        /// </summary>
        /// <typeparam name="T">Type of the visual child to retrieve</typeparam>
        /// <param name="obj">Source dependency object</param>
        /// <returns>First child that matches the given type</returns>
        public static T FindLogicalChild<T>(DependencyObject obj) where T : DependencyObject
        {
            foreach (object rawChild in LogicalTreeHelper.GetChildren(obj))
            {
                DependencyObject child = rawChild as DependencyObject;
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfchild = FindLogicalChild<T>(child);
                    if (childOfchild != null)
                    {
                        return childOfchild;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Find the first visual child of a given type
        /// </summary>
        /// <typeparam name="T">Type of the visual child to retrieve</typeparam>
        /// <param name="obj">Source dependency object</param>
        /// <returns>First child that matches the given type</returns>
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfchild = FindVisualChild<T>(child);
                    if (childOfchild != null)
                    {
                        return childOfchild;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Walks up the tree starting at the bottomMostVisual, until it finds the first item container for the 
        /// ItemsControl passed as a parameter. In order to make sure it works with any control that derives from 
        /// ItemsControl, this method makes no assumption about the type of that container.
        /// (it will get a ListBoxItem if it's a ListBox, a ListViewItem if it's a ListView...)
        /// </summary>
        /// <param name="itemsControl">Source ItemsControl</param>
        /// <param name="bottomMostVisual">Bottom most Visual object</param>
        /// <returns>Associated ItemContainer</returns>
        public static FrameworkElement GetItemContainer(ItemsControl itemsControl, Visual bottomMostVisual)
        {
            FrameworkElement itemContainer = null;
            if (itemsControl != null && bottomMostVisual != null && itemsControl.Items.Count >= 1)
            {
                var firstContainer = itemsControl.ItemContainerGenerator.ContainerFromIndex(0);
                if (firstContainer != null)
                {
                    Type containerType = firstContainer.GetType();
                    itemContainer = FindVisualAncestor(containerType, bottomMostVisual);
                }
            }

            return itemContainer;
        }

        /// <summary>
        /// Find the first visual ancestor of a given type
        /// </summary>
        /// <param name="ancestorType">Type of the ancestor</param>
        /// <param name="visual">Source visual</param>
        /// <returns>First ancestor that matches the type in the visual tree</returns>
        public static FrameworkElement FindVisualAncestor(Type ancestorType, Visual visual)
        {
            while (visual != null && !ancestorType.IsInstanceOfType(visual))
            {
                visual = (Visual)VisualTreeHelper.GetParent(visual);
            }

            return visual as FrameworkElement;
        }

        /// <summary>
        /// Find the first visual ancestor of a given type
        /// </summary>
        /// <typeparam name="T">Type of the ancestor</typeparam>
        /// <param name="visual">Source visual</param>
        /// <returns>First ancestor that matches the type in the visual tree</returns>
        public static T FindVisualAncestor<T>(Visual visual) where T : class
        {
            while (visual != null && !(visual is T))
            {
                visual = (Visual)VisualTreeHelper.GetParent(visual);
            }

            return visual as T;
        }

        /// <summary>
        /// Find the first logical ancestor of a given type
        /// </summary>
        /// <typeparam name="T">Type of the ancestor</typeparam>
        /// <param name="current">Source dependency object</param>
        /// <returns>First ancestor that matches the type in the logical tree</returns>
        public static T FindLogicalAncestor<T>(DependencyObject current) where T : class
        {
            while (current != null && !(current is T))
            {
                current = LogicalTreeHelper.GetParent(current);
            }

            return current as T;            
        }

        /// <summary>
        /// Find all logical children of a given type
        /// </summary>
        /// <typeparam name="T">Type of the children to find</typeparam>
        /// <param name="obj">Source dependency object</param>
        /// <param name="matches">List of matches</param>
        /// <returns>A list of T objects that are logical children of the source dependency object</returns>
        private static List<T> FindLogicalChildren<T>(DependencyObject obj, List<T> matches) where T : DependencyObject
        {
            foreach (DependencyObject children in LogicalTreeHelper.GetChildren(obj))
            {
                if (children != null && children is T)
                {
                    matches.Add((T)children);
                }

                FindLogicalChildren(children, matches);
            }

            return matches;
        }

        /// <summary>
        /// Find all visual children of a given type
        /// </summary>
        /// <typeparam name="T">Type of the children to find</typeparam>
        /// <param name="obj">Source dependency object</param>
        /// <param name="matches">List of matches</param>
        /// <returns>A list of T objects that are visual children of the source dependency object</returns>
        private static List<T> FindVisualChildren<T>(DependencyObject obj, List<T> matches) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject children = VisualTreeHelper.GetChild(obj, i);
                if (children != null && children is T)
                {
                    matches.Add((T)children);
                }               
                FindVisualChildren(children, matches);
            }

            return matches;
        }
    }
}