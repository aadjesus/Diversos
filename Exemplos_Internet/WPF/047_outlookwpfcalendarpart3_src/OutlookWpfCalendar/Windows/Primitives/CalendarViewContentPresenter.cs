using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OutlookWpfCalendar.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;

namespace OutlookWpfCalendar.Windows.Primitives
{
    public class CalendarViewContentPresenter : Panel
    {
        private UIElementCollection visualChildren;
        private bool visualChildrenGenerated;
        private List<UIElement> listViewItemVisuals;
        private bool listViewItemVisualsGenerated;

        protected CalendarView CalendarView
        {
            get { return this.ListView.View as CalendarView; }
        }

        protected ListView ListView
        {
            get { return this.TemplatedParent as ListView; }
        }

        internal List<UIElement> ListViewItemVisuals
        {
            get { return listViewItemVisuals; }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int columnCount = this.CalendarView.Periods.Count;
            Size columnSize = new Size(finalSize.Width / columnCount, finalSize.Height);
            double elementX = 0;

            foreach (UIElement element in this.visualChildren)
            {
                element.Arrange(new Rect(new Point(elementX, 0), columnSize));
                elementX = elementX + columnSize.Width;
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.GenerateVisualChildren();
            this.GenerateListViewItemVisuals();

            return constraint;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                if (this.visualChildren == null)
                    return base.VisualChildrenCount;

                return this.visualChildren.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if ((index < 0) || (index >= this.VisualChildrenCount))
                throw new ArgumentOutOfRangeException("index", index, "Index out of range");

            if (this.visualChildren == null)
                return base.GetVisualChild(index);

            return this.visualChildren[index];
        }

        protected void GenerateVisualChildren()
        {
            if (this.visualChildrenGenerated)
                return;

            if (this.visualChildren == null)
                visualChildren = this.CreateUIElementCollection(this);
            else
                visualChildren.Clear();

            foreach (CalendarViewPeriod period in CalendarView.Periods)
                this.visualChildren.Add(new CalendarViewPeriodPresenter() { Period = period, CalendarView = this.CalendarView, ListView = this.ListView });
            
            this.visualChildrenGenerated = true;
        }

        protected void GenerateListViewItemVisuals()
        {
            if (this.listViewItemVisualsGenerated)
                return;

            IItemContainerGenerator generator = ((IItemContainerGenerator)ListView.ItemContainerGenerator).GetItemContainerGeneratorForPanel(this);
            generator.RemoveAll();

            if (this.listViewItemVisuals == null)
                this.listViewItemVisuals = new List<UIElement>();
            else
                this.listViewItemVisuals.Clear();

            using (generator.StartAt(new GeneratorPosition(-1, 0), GeneratorDirection.Forward))
            {
                UIElement element;
                while ((element = generator.GenerateNext() as UIElement) != null)
                {
                    this.listViewItemVisuals.Add(element);
                    generator.PrepareItemContainer(element);
                }

                this.listViewItemVisualsGenerated = true;
            }
        }
    }
}
