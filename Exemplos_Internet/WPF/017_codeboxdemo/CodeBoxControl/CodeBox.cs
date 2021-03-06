﻿using System;
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
using System.ComponentModel;
using System.Globalization;
using CodeBoxControl.Decorations;
using System.Diagnostics;
using System.Reflection;

namespace CodeBoxControl
{
    /// <summary>
    ///  A control to view or edit styled text
    /// </summary>
 public partial class CodeBox:TextBox
     {
     bool mScrollingEventEnabled ;
     public CodeBox()
     {
         
         this.TextChanged += new TextChangedEventHandler(txtTest_TextChanged);
         this.Foreground = new SolidColorBrush(Colors.Transparent);
         this.Background = new SolidColorBrush(Colors.Transparent);
         InitializeComponent();
     }

     public static DependencyProperty BaseForegroundProperty = DependencyProperty.Register("BaseForeground", typeof(Brush), typeof(CodeBox),
new FrameworkPropertyMetadata( new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.AffectsRender));

     public Brush BaseForeground
     {
         get { return (Brush)GetValue(BaseForegroundProperty); }
         set { SetValue(BaseForegroundProperty, value); }
     }
  

     void txtTest_TextChanged(object sender, TextChangedEventArgs e)
     {
         this.InvalidateVisual();
     }


     private List<Decoration> mDecorations = new List<Decoration>();
     /// <summary>
     /// List of the Decorative attributes assigned to the text
     /// </summary>
     public List<Decoration> Decorations
     {
         get { return mDecorations; }
         set { mDecorations = value; }
     }

    
        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);
            if (this.Text != "")
            {
                EnsureScrolling();
                FormattedText formattedText = new FormattedText(
                  this.Text,
                   CultureInfo.GetCultureInfo("en-us"),
                   FlowDirection.LeftToRight,
                   new Typeface(this.FontFamily.Source),
                   this.FontSize,
                   BaseForeground);  //Text that matches the textbox's
                double leftMargin = 4.0 + this.BorderThickness.Left;
                double topMargin = 2 + this.BorderThickness.Top;
                formattedText.MaxTextWidth = this.ViewportWidth; // space for scrollbar
                formattedText.MaxTextHeight = Math.Max(this.ActualHeight + this.VerticalOffset,0); //Adjust for scrolling
                drawingContext.PushClip(new RectangleGeometry(new Rect(0, 0, this.ActualWidth, this.ActualHeight)));//restrict text to textbox
                 
                //Background hilight
                foreach (Decoration dec in mDecorations)
                {
                    if (dec.DecorationType == EDecorationType.Hilight)
                    {
                        List<Pair> ranges = dec.Ranges(this.Text);
                        foreach (Pair p in ranges)
                        {
                            Geometry geom = formattedText.BuildHighlightGeometry(new Point(leftMargin, topMargin - this.VerticalOffset), p.Start, p.Length);
                            if (geom != null)
                            {
                                drawingContext.DrawGeometry(dec.Brush, null, geom);
                            }
                        }
                    }
                }

                //Underline
                foreach (Decoration dec in mDecorations)
                {
                    if (dec.DecorationType == EDecorationType.Underline)
                    {
                        List<Pair> ranges = dec.Ranges(this.Text);
                        foreach (Pair p in ranges)
                        {
                            Geometry geom = formattedText.BuildHighlightGeometry(new Point(leftMargin, topMargin - this.VerticalOffset), p.Start, p.Length);
                            if (geom != null)
                            {
                                StackedRectangleGeometryHelper srgh = new StackedRectangleGeometryHelper(geom);
                               
                                foreach (Geometry g  in srgh.BottomEdgeRectangleGeometries ())
                                {
                                    drawingContext.DrawGeometry(dec.Brush, null, g);
                                }
                            }
                        }
                    }
                }


                    //Strikethrough
                    foreach (Decoration dec in mDecorations)
                    {
                        if (dec.DecorationType == EDecorationType.Strikethrough)
                        {
                            List<Pair> ranges = dec.Ranges(this.Text);
                            foreach (Pair p in ranges)
                            {
                                Geometry geom = formattedText.BuildHighlightGeometry(new Point(leftMargin, topMargin - this.VerticalOffset), p.Start, p.Length);
                                if (geom != null)
                                {
                                    StackedRectangleGeometryHelper srgh = new StackedRectangleGeometryHelper(geom);

                                    foreach (Geometry g in srgh.CenterLineRectangleGeometries())
                                    {
                                        drawingContext.DrawGeometry(dec.Brush, null, g);
                                    }
                                }
                            } 
                        }
                    }


                //TextColor
                foreach (Decoration dec in mDecorations)
                {
                    if (dec.DecorationType == EDecorationType.TextColor)
                    {
                        List<Pair> ranges = dec.Ranges(this.Text);
                        foreach (Pair p in ranges)
                        {
                            formattedText.SetForegroundBrush(dec.Brush, p.Start, p.Length);
                        }
                    }
                }
                drawingContext.DrawText(formattedText, new Point(leftMargin, topMargin - this.VerticalOffset));
            }
        }

        private void EnsureScrolling()
        {
            if (!mScrollingEventEnabled)
            {
                 DependencyObject dp = VisualTreeHelper.GetChild(this, 0);
                  ScrollViewer sv = VisualTreeHelper.GetChild(dp, 0) as ScrollViewer;
                sv.ScrollChanged += new ScrollChangedEventHandler(ScrollChanged);
                mScrollingEventEnabled = true;
            }
        }

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            this.InvalidateVisual();
        }
    }
}

