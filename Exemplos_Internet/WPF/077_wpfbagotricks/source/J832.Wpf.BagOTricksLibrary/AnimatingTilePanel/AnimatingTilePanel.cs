using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using J832.Common;
using J832.Wpf;

namespace Microsoft.Samples.KMoore.WPFSamples.AnimatingTilePanel
{
    public class AnimatingTilePanel : Panel
    {
        public AnimatingTilePanel()
        {
            m_listener.Rendering += compositionTarget_Rendering;
            m_listener.WireParentLoadedUnloaded(this);
        }

        #region public properties

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public static double GetItemWidth(DependencyObject element)
        {
            Util.RequireNotNull(element, "element");
            return (double)element.GetValue(ItemWidthProperty);
        }

        public static void SetItemWidth(DependencyObject element, double itemWidth)
        {
            Util.RequireNotNull(element, "element");
            element.SetValue(ItemWidthProperty, itemWidth);
        }

        public static readonly DependencyProperty ItemWidthProperty =
            CreateDoubleDP("ItemWidth", 50, FrameworkPropertyMetadataOptions.AffectsMeasure, 0, double.PositiveInfinity, true);

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public static double GetItemHeight(DependencyObject element)
        {
            Util.RequireNotNull(element, "element");
            return (double)element.GetValue(ItemHeightProperty);
        }

        public static void SetItemHeight(DependencyObject element, double itemHeight)
        {
            Util.RequireNotNull(element, "element");
            element.SetValue(ItemHeightProperty, itemHeight);
        }

        public static readonly DependencyProperty ItemHeightProperty =
            CreateDoubleDP("ItemHeight", 50, FrameworkPropertyMetadataOptions.AffectsMeasure, 0, double.PositiveInfinity, true);

        public double Dampening
        {
            get { return (double)GetValue(DampeningProperty); }
            set { SetValue(DampeningProperty, value); }
        }

        public static readonly DependencyProperty DampeningProperty =
            CreateDoubleDP("Dampening", 0.2, FrameworkPropertyMetadataOptions.None, 0, 1, false);

        public double Attraction
        {
            get { return (double)GetValue(AttractionProperty); }
            set { SetValue(AttractionProperty, value); }
        }

        public static readonly DependencyProperty AttractionProperty =
            CreateDoubleDP("Attraction", 2, FrameworkPropertyMetadataOptions.None, 0, double.PositiveInfinity, false);

        public bool AnimatesNewItem
        {
            get { return (bool)GetValue(AnimatesNewItemProperty); }
            set { SetValue(AnimatesNewItemProperty, value); }
        }

        public static readonly DependencyProperty AnimatesNewItemProperty =
            DependencyProperty.Register("AnimatesNewItem", typeof(bool),
                typeof(AnimatingTilePanel), new FrameworkPropertyMetadata(false));

        public double Variation
        {
            get { return (double)GetValue(VariationProperty); }
            set { SetValue(VariationProperty, value); }
        }

        public static readonly DependencyProperty VariationProperty =
            CreateDoubleDP("Variation", 1, FrameworkPropertyMetadataOptions.None, 0, true, 1, true, false);

        #endregion

        #region protected override

        protected override Size MeasureOverride(Size availableSize)
        {
            onPreApplyTemplate();

            Size theChildSize = new Size(this.ItemWidth, this.ItemHeight);

            object foo = DependencyPropertyHelper.GetValueSource(this, ItemHeightProperty);

            foreach (UIElement child in Children)
            {
                child.Measure(theChildSize);
            }

            int childrenPerRow;

            // Figure out how many children fit on each row
            if (availableSize.Width == Double.PositiveInfinity)
            {
                childrenPerRow = this.Children.Count;
            }
            else
            {
                childrenPerRow = Math.Max(1, (int)Math.Floor(availableSize.Width / this.ItemWidth));
            }

            // Calculate the width and height this results in
            double width = childrenPerRow * this.ItemWidth;
            double height = this.ItemHeight * (Math.Floor((double)this.Children.Count / childrenPerRow) + 1);
            height = (height.IsRational()) ? height : 0;
            return new Size(width, height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            m_listener.StartListening();

            // Calculate how many children fit on each row
            int childrenPerRow = Math.Max(1, (int)Math.Floor(finalSize.Width / this.ItemWidth));

            bool animateNewItem = AnimatesNewItem && m_hasArranged;
            m_hasArranged = true;

            Size theChildSize = new Size(this.ItemWidth, this.ItemHeight);
            for (int i = 0; i < this.Children.Count; i++)
            {
                UIElement child = this.Children[i];

                // Figure out where the child goes
                Point newOffset = calculateChildOffset(i, childrenPerRow, 
                    this.ItemWidth, this.ItemHeight, 
                    finalSize.Width, this.Children.Count);

                AnimatingTilePanelData data = (AnimatingTilePanelData)child.GetValue(DataProperty);
                if (data == null)
                {
                    data = new AnimatingTilePanelData();
                    child.SetValue(DataProperty, data);
                }

                //set the location attached DP
                data.ChildTarget = newOffset;

                if (data.IsNew) // first time I've seen this...
                {
                    data.IsNew = false;

                    if (animateNewItem)
                    {
                        newOffset.X -= theChildSize.Width;
                    }

                    data.ChildLocation = newOffset;
                    child.Arrange(new Rect(newOffset, theChildSize));
                }
                else
                {
                    Point currentOffset = data.ChildLocation;
                    // Position the child and set its size
                    child.Arrange(new Rect(currentOffset, theChildSize));
                }
            }
            return finalSize;
        }

        #endregion

        #region Implementation

        #region private methods

        private void bindToParentItemsControl(DependencyProperty property, DependencyObject source)
        {
            if (DependencyPropertyHelper.GetValueSource(this, property).BaseValueSource == BaseValueSource.Default)
            {
                Binding binding = new Binding();
                binding.Source = source;
                binding.Path = new PropertyPath(property);
                base.SetBinding(property, binding);
            }
        }

        private void onPreApplyTemplate()
        {
            if (!m_appliedTemplate)
            {
                m_appliedTemplate = true;

                DependencyObject source = base.TemplatedParent;
                if (source is ItemsPresenter)
                {
                    source = LookForItemsControl((ItemsPresenter)source);
                }

                if (source != null)
                {
                    bindToParentItemsControl(ItemHeightProperty, source);
                    bindToParentItemsControl(ItemWidthProperty, source);
                }
            }
        }

        private static ItemsControl LookForItemsControl(FrameworkElement element)
        {
            if (element == null)
            {
                return null;
            }
            else if (element is ItemsControl)
            {
                return (ItemsControl)element;
            }
            else
            {
                FrameworkElement parent = VisualTreeHelper.GetParent(element) as FrameworkElement;
                return LookForItemsControl(parent);
            }
        }

        private void compositionTarget_Rendering(object sender, EventArgs e)
        {
            double dampening = this.Dampening;
            double attractionFactor = this.Attraction * .01;
            double variation = this.Variation;

            bool shouldChange = false;
            for (int i = 0; i < Children.Count; i++)
            {
                shouldChange = updateElement(
                    (AnimatingTilePanelData)Children[i].GetValue(DataProperty),
                    dampening,
                    attractionFactor,
                    variation) || shouldChange;
            }

            if (shouldChange)
            {
                InvalidateArrange();
            }
            else
            {
                m_listener.StopListening();
            }
        }

        private static bool updateElement(AnimatingTilePanelData data, double dampening, double attractionFactor, double variation)
        {
            Debug.Assert(dampening > 0 && dampening < 1);
            Debug.Assert(attractionFactor > 0 && !double.IsInfinity(attractionFactor));

            attractionFactor *= 1 + (variation * data.Random - .5);

            Point newLocation;
            Vector newVelocity;

            bool anythingChanged =
                WpfUtil.Animate(data.ChildLocation, data.Velocity, data.ChildTarget,
                    attractionFactor, dampening, c_terminalVelocity, c_diff, c_diff,
                    out newLocation, out newVelocity);

            data.ChildLocation = newLocation;
            data.Velocity = newVelocity;

            return anythingChanged;
        }

        // Given a child index, child size and children per row, figure out where the child goes
        private static Point calculateChildOffset(
            int index,
            int childrenPerRow,
            double itemWidth,
            double itemHeight,
            double panelWidth,
            int totalChildren)
        {
            double fudge = 0;
            if (totalChildren > childrenPerRow)
            {
                fudge = (panelWidth - childrenPerRow * itemWidth) / childrenPerRow;
                Debug.Assert(fudge >= 0);
            }

            int row = index / childrenPerRow;
            int column = index % childrenPerRow;
            return new Point(.5 * fudge + column * (itemWidth + fudge), row * itemHeight);
        }

        private static DependencyProperty CreateDoubleDP(
            string name,
            double defaultValue,
            FrameworkPropertyMetadataOptions metadataOptions,
            double minValue,
            double maxValue,
            bool attached)
        {
            return CreateDoubleDP(name, defaultValue, metadataOptions, minValue, false, maxValue, false, attached);
        }

        private static DependencyProperty CreateDoubleDP(
            string name,
            double defaultValue,
            FrameworkPropertyMetadataOptions metadataOptions,
            double minValue,
            bool includeMin,
            double maxValue,
            bool includeMax,
            bool attached)
        {
            if (double.IsNaN(minValue))
            {
                Util.RequireArgument(double.IsNaN(maxValue), "maxValue", "If minValue is NaN, then maxValue must be.");

                if (attached)
                {
                    return DependencyProperty.RegisterAttached(
                        name,
                        typeof(double),
                        typeof(AnimatingTilePanel),
                        new FrameworkPropertyMetadata(defaultValue, metadataOptions));
                }
                else
                {
                    return DependencyProperty.Register(
                        name,
                        typeof(double),
                        typeof(AnimatingTilePanel),
                        new FrameworkPropertyMetadata(defaultValue, metadataOptions));
                }
            }
            else
            {
                Util.RequireArgument(!double.IsNaN(maxValue), "maxValue");
                Util.RequireArgument(maxValue >= minValue, "maxValue");

                ValidateValueCallback validateValueCallback = delegate(object objValue)
                {
                    double value = (double)objValue;

                    if (includeMin)
                    {
                        if (value < minValue)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (value <= minValue)
                        {
                            return false;
                        }
                    }
                    if (includeMax)
                    {
                        if (value > maxValue)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (value >= maxValue)
                        {
                            return false;
                        }
                    }

                    return true;
                };

                if (attached)
                {
                    return DependencyProperty.RegisterAttached(
                        name,
                        typeof(double),
                        typeof(AnimatingTilePanel),
                        new FrameworkPropertyMetadata(defaultValue, metadataOptions), validateValueCallback);
                }
                else
                {
                    return DependencyProperty.Register(
                        name,
                        typeof(double),
                        typeof(AnimatingTilePanel),
                        new FrameworkPropertyMetadata(defaultValue, metadataOptions), validateValueCallback);
                }
            }
        }

        #endregion

        private bool m_appliedTemplate;
        private bool m_hasArranged; // Used to make sure items are not arranged on the initial load

        private readonly CompositionTargetRenderingListener m_listener = new CompositionTargetRenderingListener();

        private static readonly DependencyProperty DataProperty =
            DependencyProperty.RegisterAttached("Data", typeof(AnimatingTilePanelData), typeof(AnimatingTilePanel));

        private const double c_diff = 0.1;
        private const double c_terminalVelocity = 10000;

        private class AnimatingTilePanelData
        {
            public Point ChildTarget;
            public Point ChildLocation;
            public Vector Velocity = new Vector(0, 0);
            public bool IsNew = true;
            public readonly double Random = s_random.NextDouble();

            private static readonly Random s_random = new Random();
        }

        #endregion

    } //*** class AnimatingTilePanel

}