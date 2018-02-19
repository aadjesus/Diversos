using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextBoxWithOpenTypeMenu
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>

    public partial class Page1 : Page
    {

        public Page1()
        {
            InitializeComponent();
        }

        #region OpenType menu

        //Click event handler for the context menu
        void ContextMenuOpen(object o, RoutedEventArgs e)
        {
            FontMenu.Items.Clear();

            //Create the menu item and preview for StylisticAlternate
            MenuItem menuItem1 = new MenuItem();
            menuItem1.Name = "Reset";
            menuItem1.Click += new RoutedEventHandler(ContextMenuClick);

            TextBlock sampleText1 = new TextBlock();
            sampleText1.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "fonts/#Kootenay");
            sampleText1.TextAlignment = TextAlignment.Center;
            sampleText1.FontSize = 26;            
            sampleText1.Foreground = Brushes.White;
            sampleText1.Text = "  Reset  ";
   
            menuItem1.Header = sampleText1;
            FontMenu.Items.Add(menuItem1);

            //Create the menu item and preview for Superscript
            MenuItem menuItem2 = new MenuItem();
            menuItem2.Name = "Superscript";
            menuItem2.Click += new RoutedEventHandler(ContextMenuClick);

            TextBlock sampleText2 = new TextBlock();
            sampleText2.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "fonts/#Pericles");
            sampleText2.FontSize = 36;
            sampleText2.Foreground = Brushes.White;
            sampleText2.Text = "         " + MyRichTextBox.Selection.Text + "         ";
            sampleText2.Typography.Variants = FontVariants.Superscript;

            menuItem2.Header = sampleText2;
            FontMenu.Items.Add(menuItem2);
            
            //Create the menu item and preview for Subscript
          
            MenuItem menuItem3 = new MenuItem();
            menuItem3.Name = "Subscript";
            menuItem3.Click += new RoutedEventHandler(ContextMenuClick);

            TextBlock sampleText3 = new TextBlock();
            sampleText3.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "fonts/#Pericles");
            sampleText3.FontSize = 36;
            sampleText3.Foreground = Brushes.White;
            sampleText3.Text = "         " + MyRichTextBox.Selection.Text + "         ";
            sampleText3.Typography.Variants = FontVariants.Subscript;

            menuItem3.Header = sampleText3;
            FontMenu.Items.Add(menuItem3);


            //Create the menu item and preview for StylisticAlternate
            MenuItem menuItem4 = new MenuItem();
            menuItem4.Name = "StylisticAlternate";
            menuItem4.Click += new RoutedEventHandler(ContextMenuClick);

            TextBlock sampleText4 = new TextBlock();
            sampleText4.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "fonts/#Pericles");
            sampleText4.FontSize = 36;
            sampleText4.Foreground = Brushes.White;
            sampleText4.Text = "         " + MyRichTextBox.Selection.Text + "         ";
            sampleText4.Typography.StylisticAlternates = 1;

            menuItem4.Header = sampleText4;
            FontMenu.Items.Add(menuItem4);
        }

        //To handle clicks in the context menu
        void ContextMenuClick(object o, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)o;
            if (item.Name == "Reset")
            {
                (MyRichTextBox.Selection).ApplyPropertyValue(Typography.StylisticAlternatesProperty, 0);
                (MyRichTextBox.Selection).ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Normal);
            }
            if (item.Name == "Superscript")
                (MyRichTextBox.Selection).ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Superscript);
            if (item.Name == "Subscript")
                (MyRichTextBox.Selection).ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Subscript);
            if (item.Name == "StylisticAlternate")
            {
                (MyRichTextBox.Selection).ApplyPropertyValue(Typography.StylisticAlternatesProperty, 1);
                (MyRichTextBox.Selection).ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Normal);
            }

        }

        #endregion


    }
}