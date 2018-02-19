//-----------------------------------------
// Slider3D.cs (c) 2007 by Charles Petzold
//-----------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.Slider3D
{
    public partial class Slider3D : Slider
    {
        // Constructor.
        public Slider3D()
        {
            InitializeComponent();

            // Default Orientation is Horizontal, so load that template.
            Template = 
                (ControlTemplate)FindResource("templateHorizontalSlider3D");
        }

        // Get-only Angle property and dependency property.
        static readonly DependencyPropertyKey AngleKey =
            DependencyProperty.RegisterReadOnly("Angle",
                typeof(double),
                typeof(Slider3D),
                new PropertyMetadata(-25.0));

        public static readonly DependencyProperty AngleProperty =
            AngleKey.DependencyProperty;

        public double Angle
        {
            protected set { SetValue(AngleKey, value); }
            get { return (double)GetValue(AngleProperty); }
        }

        // Static constructor.
        static Slider3D()
        {
            OrientationProperty.OverrideMetadata(typeof(Slider3D),
                new FrameworkPropertyMetadata(PropertyChanged));

            ValueProperty.OverrideMetadata(typeof(Slider3D),
                new FrameworkPropertyMetadata(PropertyChanged));

            MinimumProperty.OverrideMetadata(typeof(Slider3D),
                new FrameworkPropertyMetadata(PropertyChanged));

            MaximumProperty.OverrideMetadata(typeof(Slider3D),
                new FrameworkPropertyMetadata(PropertyChanged));

            IsDirectionReversedProperty.OverrideMetadata(typeof(Slider3D),
                new FrameworkPropertyMetadata(PropertyChanged));
        }

        // PropertyChanged event handlers.
        static void PropertyChanged(DependencyObject obj,
                                    DependencyPropertyChangedEventArgs args)
        {
            (obj as Slider3D).PropertyChanged(args);
        }

        void PropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.Property == OrientationProperty)
            {
                Template = (ControlTemplate)FindResource(
                    Orientation == Orientation.Horizontal ?
                        "templateHorizontalSlider3D" : 
                        "templateVerticalSlider3D");
            }
            else
            {
                Angle = (IsDirectionReversed ? -1 : 1) *
                            (50 * (Value - Minimum) / (Maximum - Minimum) - 25);
            }
        }
    }
}
