//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace CustomControls
{
    public enum Highlight { Diffuse, Elliptical }


    public class XSButton : ToggleButton
    {
        // Adopt usefull property defined in a different class, and specify a default value
        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner( typeof( XSButton ) );

        public static readonly DependencyProperty OuterBorderBrushProperty =
            DependencyProperty.Register( "OuterBorderBrush", typeof( Brush ), typeof( XSButton ) );

        public static readonly DependencyProperty OuterBorderThicknessProperty =
            DependencyProperty.Register( "OuterBorderThickness", typeof( Thickness ), typeof( XSButton ) );

        public static readonly DependencyProperty InnerBorderBrushProperty =
           DependencyProperty.Register( "InnerBorderBrush", typeof( Brush ), typeof( XSButton ) );

        public static readonly DependencyProperty InnerBorderThicknessProperty =
            DependencyProperty.Register( "InnerBorderThickness", typeof( Thickness ), typeof( XSButton ) );

        public static readonly DependencyProperty GlowColorProperty =
            DependencyProperty.Register( "GlowColor", typeof( SolidColorBrush ), typeof( XSButton ) );

        public static readonly DependencyProperty HighlightAppearanceProperty =
            DependencyProperty.Register( "HighlightAppearance", typeof( ControlTemplate ), typeof( XSButton ) );

        public static readonly DependencyProperty HighlightMarginProperty =
            DependencyProperty.Register( "HighlightMargin", typeof( Thickness ), typeof( XSButton ) );

        public static readonly DependencyProperty HighlightBrightnessProperty =
            DependencyProperty.Register( "HighlightBrightness", typeof( byte ), typeof( XSButton ) );

        public static readonly DependencyProperty HighlightStyleProperty =
            DependencyProperty.Register( "HighlightStyle", typeof( Highlight ), typeof( XSButton ),
            new FrameworkPropertyMetadata( new PropertyChangedCallback( OnHighlightStyleChanged ) ) );


    #region Properties...

        public Brush GlowColor
        {
            get { return (SolidColorBrush)GetValue( GlowColorProperty ); }
            set { SetValue( GlowColorProperty, value ); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue( CornerRadiusProperty ); }
            set { SetValue( CornerRadiusProperty, value ); }
        }

        public Brush OuterBorderBrush
        {
            get { return (Brush)GetValue( OuterBorderBrushProperty ); }
            set { SetValue( OuterBorderBrushProperty, value ); }
        }

        public Thickness OuterBorderThickness
        {
            get { return (Thickness)GetValue( OuterBorderThicknessProperty ); }
            set { SetValue( OuterBorderThicknessProperty, value ); }
        }

        public Brush InnerBorderBrush
        {
            get { return (Brush)GetValue( InnerBorderBrushProperty ); }
            set { SetValue( InnerBorderBrushProperty, value ); }
        }

        public Thickness InnerBorderThickness
        {
            get { return (Thickness)GetValue( InnerBorderThicknessProperty ); }
            set { SetValue( InnerBorderThicknessProperty, value ); }
        }

        // Force clients to pass enum value to HighlightStyle by hiding this accessor
        internal ControlTemplate HighlightAppearance
        {
            get { return (ControlTemplate)GetValue( HighlightAppearanceProperty ); }
            set { SetValue( HighlightAppearanceProperty, value ); }
        }

        public Thickness HighlightMargin
        {
            get { return (Thickness)GetValue( HighlightMarginProperty ); }
            set { SetValue( HighlightMarginProperty, value ); }
        }

        public byte HighlightBrightness
        {
            get { return (byte)GetValue( HighlightBrightnessProperty ); }
            set { SetValue( HighlightBrightnessProperty, value ); }
        }

        public Highlight HighlightStyle
        {
            get { return (Highlight)GetValue( HighlightStyleProperty ); }
            set { SetValue( HighlightStyleProperty, value ); }
        }

    #endregion (Properties)


        static XSButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( XSButton ), new FrameworkPropertyMetadata( typeof( XSButton ) ) );
        }


        public XSButton()
        {
            // Attempts to set default value in template or via FrameworkPropertyMetadata failed,
            // but everything displays correctly on the client if we do it here. Investigate.
            HighlightStyle = Highlight.Elliptical;
        }


        private static void OnHighlightStyleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            XSButton btn = (XSButton)d;

            Highlight highlight = (Highlight)e.NewValue;

            // Assign style associated with user-selected enum value
            btn.Style = (Style)btn.TryFindResource( new ComponentResourceKey( btn.GetType(), highlight.ToString() ) );
        }
    }
}
