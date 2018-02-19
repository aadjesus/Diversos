using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CsharpButtonControl
{
    
    public class Button_CSharp : Button 
    {
        public Button_CSharp()
        {
            this.LoadButtonControl();

        }
        private void LoadButtonControl()
        {
           
            this.Content = "C#Code";

            //-----------------------------------------------GlassBackgroundFill-------------------------------------------------------------

            LinearGradientBrush gradient_GlassBackgroundFill = new LinearGradientBrush();
            gradient_GlassBackgroundFill.StartPoint = new Point(0, 0);
            gradient_GlassBackgroundFill.EndPoint = new Point(0, 1);

            GradientStop color_BackgroundFill = new GradientStop();
            color_BackgroundFill.Offset = 0;
            color_BackgroundFill.Color = (Color)ColorConverter.ConvertFromString("#ff4b58");

            GradientStop color2_BackgroundFill = new GradientStop();
            color2_BackgroundFill.Offset = 1;
            color2_BackgroundFill.Color = (Color)ColorConverter.ConvertFromString("#a91e28");

            gradient_GlassBackgroundFill.GradientStops.Add(color_BackgroundFill);
            gradient_GlassBackgroundFill.GradientStops.Add(color2_BackgroundFill);

            //--------------------------------------------------HoverGlassBackgroundFill-------------------------------------------------------------

            RadialGradientBrush gradient_HoverGlassBackgroundFill = new RadialGradientBrush();
            gradient_HoverGlassBackgroundFill.Center = new Point(0.5, 0.5);

            GradientStop color_HoverGlassBackgroundFill = new GradientStop();
            color_HoverGlassBackgroundFill.Offset = 0;
            color_HoverGlassBackgroundFill.Color = (Color)ColorConverter.ConvertFromString("#a91e28");

            GradientStop color2_HoverGlassBackgroundFill = new GradientStop();
            color2_HoverGlassBackgroundFill.Offset = 1;
            color2_HoverGlassBackgroundFill.Color = (Color)ColorConverter.ConvertFromString("#ff4b58");

            gradient_HoverGlassBackgroundFill.GradientStops.Add(color_HoverGlassBackgroundFill);
            gradient_HoverGlassBackgroundFill.GradientStops.Add(color2_HoverGlassBackgroundFill);


            //-------------------------------------------HighlightFill------------------------------------------------------------

            LinearGradientBrush highlightFill = new LinearGradientBrush();
            highlightFill.StartPoint = new Point(0, 0);
            highlightFill.EndPoint = new Point(0, 1);

            GradientStop color_highlightFill = new GradientStop();
            color_highlightFill.Offset = 0;
            color_highlightFill.Color = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");

            GradientStop color2_highlightFill = new GradientStop();
            color2_highlightFill.Offset = 1;
            color2_highlightFill.Color = (Color)ColorConverter.ConvertFromString("#00000000");

            highlightFill.GradientStops.Add(color_highlightFill);
            highlightFill.GradientStops.Add(color2_highlightFill);

            //-----------------------------------------------------BorderStroke-------------------------------------------------------
            LinearGradientBrush borderStroke = new LinearGradientBrush();
            borderStroke.StartPoint = new Point(0, 0);
            borderStroke.EndPoint = new Point(0, 1);

            GradientStop color_borderStroke = new GradientStop();
            color_borderStroke.Offset = 0;
            color_borderStroke.Color = (Color)ColorConverter.ConvertFromString("#c7c7c7");

            GradientStop color2_borderStroke = new GradientStop();
            color2_borderStroke.Offset = 1;
            color2_borderStroke.Color = (Color)ColorConverter.ConvertFromString("#b0b0b0");

            borderStroke.GradientStops.Add(color_borderStroke);
            borderStroke.GradientStops.Add(color2_borderStroke);


            //--------------------------------------------------------------------------------------------------

            ControlTemplate template = new ControlTemplate(typeof(Button));

            FrameworkElementFactory grid = new FrameworkElementFactory(typeof(Grid));
            grid.SetValue(Grid.RenderTransformOriginProperty, new Point(0.5, 0.5));

            //-------------------------------------------------------------------------------------------------------

            FrameworkElementFactory ele_BorderStock = new FrameworkElementFactory(typeof(Border));
            ele_BorderStock.Name = "Outline";
            ele_BorderStock.SetValue(Border.CornerRadiusProperty, new CornerRadius(9.0));
            ele_BorderStock.SetValue(Border.BackgroundProperty, Brushes.Red);
            ele_BorderStock.SetValue(Border.BorderBrushProperty, borderStroke);
            ele_BorderStock.SetValue(Border.BorderThicknessProperty, new Thickness(1));


            FrameworkElementFactory ele_GlassBackground = new FrameworkElementFactory(typeof(Rectangle));
            ele_GlassBackground.Name = "GlassBackground";
            ele_GlassBackground.SetValue(Rectangle.MarginProperty, new Thickness(1, 1, 2, 2));
            ele_GlassBackground.SetValue(Rectangle.RadiusXProperty, 9.0);
            ele_GlassBackground.SetValue(Rectangle.RadiusYProperty, 9.0);
            ele_GlassBackground.SetValue(Rectangle.FillProperty, gradient_GlassBackgroundFill);

            FrameworkElementFactory ele_Highlight = new FrameworkElementFactory(typeof(Rectangle));
            ele_Highlight.SetValue(Rectangle.MarginProperty, new Thickness(1, 1, 2, 2));
            ele_Highlight.SetValue(Rectangle.RadiusXProperty, 9.0);
            ele_Highlight.SetValue(Rectangle.RadiusYProperty, 9.0);
            ele_Highlight.SetValue(Rectangle.OpacityProperty, 1.0);
            ele_Highlight.SetValue(Rectangle.FillProperty, highlightFill);

            FrameworkElementFactory ContentPresenterAll = new FrameworkElementFactory(typeof(ContentPresenter));
            ContentPresenterAll.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            ContentPresenterAll.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            grid.AppendChild(ele_BorderStock);
            grid.AppendChild(ele_GlassBackground);
            grid.AppendChild(ele_Highlight);
            grid.AppendChild(ContentPresenterAll);

            //------------------------------------------------ControlTemplate.Triggers.IsMouseOver---------------------------------------------------------

            Trigger tg_IsMouseOver = new Trigger();
            tg_IsMouseOver.Property = IsMouseOverProperty;
            tg_IsMouseOver.Value = true;

            Setter str_IsMouseOver = new Setter();
            str_IsMouseOver.TargetName = "GlassBackground";
            str_IsMouseOver.Property = Rectangle.FillProperty;
            str_IsMouseOver.Value = gradient_HoverGlassBackgroundFill;
            tg_IsMouseOver.Setters.Add(str_IsMouseOver);

            template.Triggers.Add(tg_IsMouseOver);

            //------------------------------------------------ControlTemplate.Triggers.IsPressed---------------------------------------------------------


            Trigger tg_IsPressed = new Trigger();
            tg_IsPressed.Property = Button.IsPressedProperty;
            tg_IsPressed.Value = true;

            Setter str_IsPressed = new Setter();
            str_IsPressed.TargetName = "GlassBackground";
            str_IsPressed.Property = Rectangle.FillProperty;
            str_IsPressed.Value = Brushes.Red;
            tg_IsPressed.Setters.Add(str_IsPressed);

            template.Triggers.Add(tg_IsPressed);
            //--------------------------------------------------------------------------------------------------------------------

            template.VisualTree = grid;

            Style newStyle = new Style();
            newStyle.TargetType = typeof(Button);

            Setter str_margin = new Setter(Button.MarginProperty, new Thickness(1.0));
            Setter str_snapsToDevicePixes = new Setter(Button.SnapsToDevicePixelsProperty, true);
            Setter str_OverridesDefaultStyle = new Setter(Button.OverridesDefaultStyleProperty, true);
            Setter str_minHeight = new Setter(Button.MinHeightProperty, 16.0);
            Setter str_minWidth = new Setter(Button.MinWidthProperty, 16.0);
            Setter str_fontFamiy = new Setter(Button.FontFamilyProperty, new FontFamily("Verdana"));
            FontSizeConverter myFontSizeConverter = new FontSizeConverter();
            Setter str_fontSize = new Setter(Button.FontSizeProperty, (Double)myFontSizeConverter.ConvertFromString("11px"));
            Setter str_foreground = new Setter(Button.ForegroundProperty, Brushes.White);
            Setter str_ModifiedTemplate = new Setter(TemplateProperty, template);

            newStyle.Setters.Add(str_margin);
            newStyle.Setters.Add(str_snapsToDevicePixes);
            newStyle.Setters.Add(str_OverridesDefaultStyle);
            newStyle.Setters.Add(str_minHeight);
            newStyle.Setters.Add(str_minWidth);
            newStyle.Setters.Add(str_fontFamiy);
            newStyle.Setters.Add(str_fontSize);
            newStyle.Setters.Add(str_foreground);
            newStyle.Setters.Add(str_ModifiedTemplate);

            this.Style = newStyle;
        }

       
    }
}
