﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace ScrollControl
{
    /// <summary>
    /// Represents a Panel subclass where all children are layed out in columns. The child
    /// will automatically be placed in a new column if it doesnt fir in the current column,
    /// or if the child uses the Attached DP ColumnBreakBefore
    /// </summary>
    public class ColumnedPanel : Panel
    {
        #region Data
        public static DependencyProperty ColumnBreakBeforeProperty;
        #endregion

        #region Ctor
        /// <summary>
        /// Constructs a new ColumnedPanel object, and serts up the ColumnBreakBefore DP
        /// </summary>
        static ColumnedPanel()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.AffectsArrange = true;
            metadata.AffectsMeasure = true;
            ColumnBreakBeforeProperty = DependencyProperty.RegisterAttached("ColumnBreakBefore",
                typeof(bool), typeof(ColumnedPanel), metadata);
        }
        #endregion

        #region Overrides
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.LayoutTransform = new ScaleTransform(1.1, 1.1, 0.5, 0.5);
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.LayoutTransform = new ScaleTransform(1.0, 1.0, 0.5, 0.5);
        }
        #endregion

        #region DP
        /// <summary>
        /// Can be used to create a new column with the ColumnedPanel
        /// just before an element
        /// </summary>
        public static void SetColumnBreakBefore(UIElement element, Boolean value)
        {
            element.SetValue(ColumnBreakBeforeProperty, value);
        }
        public static Boolean GetColumnBreakBefore(UIElement element)
        {
            return (bool)element.GetValue(ColumnBreakBeforeProperty);
        }
        #endregion

        #region Measure Override
        // From MSDN : When overridden in a derived class, measures the 
        // size in layout required for child elements and determines a
        // size for the FrameworkElement-derived class
        protected override Size MeasureOverride(Size constraint)
        {
            Size currentColumnSize = new Size();
            Size panelSize = new Size();

            foreach (UIElement element in base.InternalChildren)
            {
                element.Measure(constraint);
                Size desiredSize = element.DesiredSize;

                if (GetColumnBreakBefore(element) ||
                    currentColumnSize.Height + desiredSize.Height > constraint.Height)
                {
                    // Switch to a new column (either because the element has requested it
                    // or space has run out).
                    panelSize.Height = Math.Max(currentColumnSize.Height, panelSize.Height);
                    panelSize.Width += currentColumnSize.Width;
                    currentColumnSize = desiredSize;

                    // If the element is too high to fit using the maximum height of the line,
                    // just give it a separate column.
                    if (desiredSize.Height > constraint.Height)
                    {
                        panelSize.Height = Math.Max(desiredSize.Height, panelSize.Height);
                        panelSize.Width += desiredSize.Width;
                        currentColumnSize = new Size();
                    }
                }
                else
                {
                    // Keep adding to the current column.
                    currentColumnSize.Height += desiredSize.Height;

                    // Make sure the line is as wide as its widest element.
                    currentColumnSize.Width = Math.Max(desiredSize.Width, currentColumnSize.Width);
                }
            }

            // Return the size required to fit all elements.
            // Ordinarily, this is the width of the constraint, and the height
            // is based on the size of the elements.
            // However, if an element is higher than the height given to the panel,
            // the desired width will be the height of that column.
            panelSize.Height = Math.Max(currentColumnSize.Height, panelSize.Height);
            panelSize.Width += currentColumnSize.Width;
            return panelSize;
        }
        #endregion

        #region Arrange Override
        //From MSDN : When overridden in a derived class, positions child
        //elements and determines a size for a FrameworkElement derived
        //class.
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            int firstInLine = 0;

            Size currentColumnSize = new Size();

            double accumulatedWidth = 0;

            UIElementCollection elements = base.InternalChildren;
            for (int i = 0; i < elements.Count; i++)
            {

                Size desiredSize = elements[i].DesiredSize;

                if (GetColumnBreakBefore(elements[i]) || currentColumnSize.Height 
                    + desiredSize.Height > arrangeBounds.Height) //need to switch to another column
                {
                    arrangeColumn(accumulatedWidth, currentColumnSize.Width, firstInLine, i, arrangeBounds);

                    accumulatedWidth += currentColumnSize.Width;
                    currentColumnSize = desiredSize;

                    //the element is higher then the constraint - give it a separate column     
                    if (desiredSize.Height > arrangeBounds.Height)                
                    {
                        arrangeColumn(accumulatedWidth, desiredSize.Width, i, ++i, 
                            arrangeBounds);
                        accumulatedWidth += desiredSize.Width;
                        currentColumnSize = new Size();
                    }
                    firstInLine = i;
                }
                else //continue to accumulate a column
                {
                    currentColumnSize.Height += desiredSize.Height;
                    currentColumnSize.Width = Math.Max(desiredSize.Width, currentColumnSize.Width);
                }
            }

            if (firstInLine < elements.Count)
                arrangeColumn(accumulatedWidth, currentColumnSize.Width, 
                    firstInLine, elements.Count, arrangeBounds);

            return arrangeBounds;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Arranges a single column of elements
        /// </summary>
        private void arrangeColumn(double x, double columnWidth, 
            int start, int end, Size arrangeBounds)
        {
            double y = 0;
            double totalChildHeight = 0;
            double widestChildWidth = 0;
            double xOffset = 0;

            UIElementCollection children = InternalChildren;
            UIElement child;

            for (int i = start; i < end; i++)
            {
                child = children[i];
                totalChildHeight += child.DesiredSize.Height;
                if (child.DesiredSize.Width > widestChildWidth)
                    widestChildWidth = child.DesiredSize.Width;
            }

            //work out y start offset within a given column
            y = ((arrangeBounds.Height - totalChildHeight) / 2);


            for (int i = start; i < end; i++)
            {
                child = children[i];
                if (child.DesiredSize.Width < widestChildWidth)
                {
                    xOffset = ((widestChildWidth - child.DesiredSize.Width) / 2);
                }

                child.Arrange(new Rect(x + xOffset, y, child.DesiredSize.Width, columnWidth));
                y += child.DesiredSize.Height;
                xOffset = 0;
            }
        }
        #endregion

    }
}
