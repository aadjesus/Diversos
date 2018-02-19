using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication17
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MyModel();
        }
    }

    public class MyModel
    {

        public string ThemeName { get; set; }
        public List<String> Themes { get; set; }

        public MyModel()
        {
            Themes = new List<string>();
            Themes.Add("Gray");
            Themes.Add("Blue");
            Themes.Add("Black");

            ThemeName = "Blue";
        }
    }
}
