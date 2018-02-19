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
using System.Windows.Markup;

namespace WPF_Mysteries
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        #region Ctor
        public Window1()
        {
            InitializeComponent();
            // Allows binding to it's own properties
            this.DataContext = this; 
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Provides a <see cref="List&lt;Person&gt;">List&lt;Person&gt;</see>
        /// objects that are used to Bind to for the 2 contained ListBoxes
        /// within Window1.xaml
        /// </summary>
        public List<Person> GetPeople
        {
            get
            {
                List<Person> people = new List<Person>();
                people.Add(new Person { FirstName = "sam", LastName = "brown" });
                people.Add(new Person { FirstName = "diana", LastName = "ross" });
                return people;
            }

        }
        #endregion

        #region Create Style In Code
        /// <summary>
        /// Creates a a Style in code, 
        /// just to show the difference between code and XAML 
        /// </summary>
        private Style CreateStyleInCode()
        {

            //XAML code is as shown below, nice and simply really

            //<Style TargetType="ListBoxItem">
            //    <Setter Property="TextElement.FontSize" Value="14"/>

            //    <Style.Triggers>
            //        <Trigger Property="IsMouseOver" Value="true">
            //            <Setter Property="Foreground" Value="Black" />
            //            <Setter Property="Background">

            //                <Setter.Value>

            //                    <LinearGradientBrush StartPoint="0,0" EndPoint="0,1">

            //                        <LinearGradientBrush.GradientStops>
            //                            <GradientStop Color="#0E4791" Offset="0"/>
            //                            <GradientStop Color="#468DE2" Offset="1"/>
            //                        </LinearGradientBrush.GradientStops>
            //                    </LinearGradientBrush>

            //                </Setter.Value>
            //            </Setter>
            //            <Setter Property="Cursor" Value="Hand"/>
            //        </Trigger>
            //    </Style.Triggers>

            //</Style>


            //And here is the C# code to achieve the above
            Style styleListBoxItem = new Style(typeof(ListBoxItem));
            styleListBoxItem.Setters.Add(new Setter
            {
                Property=TextElement.FontSizeProperty,
                Value=14.0
            });

            //Trigger
            Trigger triggerIsMouseOver  = 
                new Trigger { Property=ListBoxItem.IsMouseOverProperty, Value=true };

            triggerIsMouseOver.Setters.Add(new Setter(ListBoxItem.ForegroundProperty, Brushes.Black));

            GradientStopCollection gradientStopsLinearBrush = new GradientStopCollection();
            gradientStopsLinearBrush.Add(
                new GradientStop((Color)ColorConverter.ConvertFromString("#0E4791"), 0.0));
            gradientStopsLinearBrush.Add(
                new GradientStop((Color)ColorConverter.ConvertFromString("#468DE2"), 1.0));

            LinearGradientBrush backgroundLinearBrush = 
                new LinearGradientBrush(gradientStopsLinearBrush)
            {
                StartPoint = new Point(0,0),
                EndPoint = new Point(0,1)
            };

            //Trigger setters
            triggerIsMouseOver.Setters.Add(
                new Setter(ListBoxItem.BackgroundProperty, backgroundLinearBrush));
            triggerIsMouseOver.Setters.Add(
                new Setter(ListBoxItem.CursorProperty, Cursors.Hand));
            styleListBoxItem.Triggers.Add(triggerIsMouseOver);

            return styleListBoxItem;


        }

        #endregion

        #region Create Template In Code
        /// <summary>
        /// Creates a a DataTemplate in code, 
        /// just to show the difference between code and XAML 
        /// </summary>
        private DataTemplate CreateDataTemplateInCode()
        {

            //XAML code is as shown below, nice and simply really

                            
            //<DataTemplate DataType="{x:Type local:Person}">

            //    <StackPanel x:Name="spOuter" Orientation="Horizontal" Margin="10">
            //        <Path Name="pathSelected" Fill="White" Stretch="Fill" Stroke="White" Width="15" 
            //            Height="20" Data="M0,0 L 0,10 L 5,5" 
            //            Visibility="Hidden"/>

            //        <StackPanel x:Name="spInner" Orientation="Horizontal">
            //            <Label Content="{Binding FirstName}" Foreground="Black"/>
            //            <Ellipse Fill="Black" Height="5" Width="5" 
            //                     HorizontalAlignment="Center" 
            //                     VerticalAlignment="Center"/>
            //            <Label Content="{Binding LastName}" Foreground="Black"/>
            //        </StackPanel>
                    
            //    </StackPanel>
                
            //    <DataTemplate.Triggers>
            //        <DataTrigger Binding="{Binding RelativeSource={RelativeSource Mode=FindAncestor, 
            //                AncestorType={x:Type ListBoxItem}, AncestorLevel=1}, Path=IsSelected}" Value="True">
            //            <Setter TargetName="pathSelected" Property="Visibility" Value="Visible"  />
            //        </DataTrigger>
            //    </DataTemplate.Triggers>
                
            //</DataTemplate>


            //And here is the C# code to achieve the above
            DataTemplate dataTemplate = new DataTemplate(typeof(Person));

            FrameworkElementFactory spOuterFactory = 
                new FrameworkElementFactory(typeof(StackPanel));
            spOuterFactory.SetValue(
                StackPanel.OrientationProperty, Orientation.Horizontal);
            spOuterFactory.SetValue(
                StackPanel.MarginProperty, new Thickness(10));

            #region Path
            FrameworkElementFactory pathSelectedFactory = 
                new FrameworkElementFactory(typeof(Path), "pathSelected");

            pathSelectedFactory.SetValue(Path.FillProperty, Brushes.Orange);
            pathSelectedFactory.SetValue(Path.StretchProperty, Stretch.Fill); 
            pathSelectedFactory.SetValue(Path.StrokeProperty, Brushes.Orange);
            pathSelectedFactory.SetValue(Path.WidthProperty, 15.0);
            pathSelectedFactory.SetValue(Path.HeightProperty, 20.0);
            pathSelectedFactory.SetValue(Path.VisibilityProperty, Visibility.Hidden);

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures = new PathFigureCollection();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(0, 0);
            pathFigure.Segments = new PathSegmentCollection();
            pathFigure.Segments.Add(new LineSegment() { Point = new Point(0, 10) });
            pathFigure.Segments.Add(new LineSegment() { Point = new Point(5, 5) });
            pathGeometry.Figures.Add(pathFigure);


            pathSelectedFactory.SetValue(Path.DataProperty, pathGeometry);
            spOuterFactory.AppendChild(pathSelectedFactory);
            #endregion

            #region Inner StackPanel
            FrameworkElementFactory spInnerFactory = new FrameworkElementFactory(typeof(StackPanel));
            spInnerFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            //FirstName
            FrameworkElementFactory labelFNFactory = new FrameworkElementFactory(typeof(Label));
            Binding bindingFirstName = new Binding();
            bindingFirstName.Path = new PropertyPath("FirstName");
            labelFNFactory.SetBinding(Label.ContentProperty, bindingFirstName);
            labelFNFactory.SetValue(Label.ForegroundProperty, Brushes.Black);
            spInnerFactory.AppendChild(labelFNFactory);


            //Ellipse
            FrameworkElementFactory ellipseFactory = new FrameworkElementFactory(typeof(Ellipse));
            ellipseFactory.SetValue(Ellipse.FillProperty, Brushes.Black);
            ellipseFactory.SetValue(Ellipse.HeightProperty, 5.0);
            ellipseFactory.SetValue(Ellipse.WidthProperty, 5.0);
            ellipseFactory.SetValue(Ellipse.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            ellipseFactory.SetValue(Ellipse.VerticalAlignmentProperty, VerticalAlignment.Center);
            spInnerFactory.AppendChild(ellipseFactory);


            //LastName
            FrameworkElementFactory labelLNFactory = new FrameworkElementFactory(typeof(Label));
            Binding bindingLastName = new Binding();
            bindingLastName.Path = new PropertyPath("LastName");
            labelLNFactory.SetBinding(Label.ContentProperty, bindingLastName);
            labelLNFactory.SetValue(Label.ForegroundProperty, Brushes.Black);
            spInnerFactory.AppendChild(labelLNFactory);

            //Add to outer StackPanel
            spOuterFactory.AppendChild(spInnerFactory);
            #endregion

            #region DataTrigger
            DataTrigger dataTrigger = new DataTrigger();
            dataTrigger.Binding = new Binding {
                Path = new PropertyPath(ListBoxItem.IsSelectedProperty),
                RelativeSource = 
                    new RelativeSource(RelativeSourceMode.FindAncestor, 
                    typeof(ListBoxItem), 1)
            };
            dataTrigger.Value = true;
            dataTrigger.Setters.Add(
                new Setter(FrameworkElement.VisibilityProperty, 
                    Visibility.Visible, "pathSelected"));

            dataTemplate.Triggers.Add(dataTrigger);
            #endregion

            dataTemplate.VisualTree = spOuterFactory;
            return dataTemplate;
        }

        #endregion



        #region Private Methods
        /// <summary>
        /// This button will demonstrate how to create a DataTemplate and a Style
        /// in code behind. This is done by calling the CreateDataTemplateInCode() 
        /// and CreateStyleInCode() methods of this class.
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            
            //Create the DataTemplate
            lstbox2.ItemTemplate = CreateDataTemplateInCode();

            //Create the Style
            Style style = CreateStyleInCode();
            lstbox2.ItemContainerStyle = style;
        }
        #endregion
    }
}
