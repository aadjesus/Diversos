using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace NotesListBox
{
    /// <summary>
    /// Hosts an single NotesListBoxControl 
    /// in the AdornerLayer
    /// </summary>
    public class NoteAdorner : Adorner
    {
        #region Data
        private ArrayList logicalChildren;
        private NotesListBoxControl notesListBoxControl;
        private readonly Grid host = new Grid();
        #endregion // Data

        #region Constructor



        public NoteAdorner(FrameworkElement adornedCtrl, ObservableCollection<Note> notes)
            : base(adornedCtrl)
        {


            host.Width = (double)250;
            host.Height = (double)adornedCtrl.ActualHeight;
            host.VerticalAlignment = VerticalAlignment.Stretch;
            host.HorizontalAlignment = HorizontalAlignment.Right;
            host.Margin = new Thickness(0);
            notesListBoxControl = new NotesListBoxControl();
            notesListBoxControl.Notes = notes;
            notesListBoxControl.Margin = new Thickness(0);
            host.Children.Add(notesListBoxControl);
            base.AddLogicalChild(host);
            base.AddVisualChild(host);
        }


        public void ResizeToFillAvailableSpace(System.Windows.Size availableSize)
        {
            host.Width = (double)250;
            host.Height = availableSize.Width;
            notesListBoxControl.ResizeToFillAvailableSpace(host.Height);
        }



        #endregion // Constructor



        #region Measure/Arrange

        /// <summary>
        /// Allows the control to determine how big it wants to be.
        /// </summary>
        /// <param name="constraint">A limiting size for the control.</param>
        protected override Size MeasureOverride(Size constraint)
        {
            return constraint;
        }

        /// <summary>
        /// Positions and sizes the control.
        /// </summary>
        /// <param name="finalSize">The actual size of the control.</param>		
        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect rect = new Rect(new Point(), finalSize);

            host.Arrange(rect);
            return finalSize;
        }

        #endregion // Measure/Arrange

        #region Visual Children

        /// <summary>
        /// Required for the element to be rendered.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        /// <summary>
        /// Required for the element to be rendered.
        /// </summary>
        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException("index");

            return host;
        }

        #endregion // Visual Children

        #region Logical Children

        /// <summary>
        /// Required for the displayed element to inherit property values
        /// from the logical tree, such as FontSize.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (logicalChildren == null)
                {
                    logicalChildren = new ArrayList();
                    logicalChildren.Add(host);
                }

                return logicalChildren.GetEnumerator();
            }
        }

        #endregion // Logical Children
    }
}