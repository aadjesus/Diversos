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

namespace ImageTooltipInWPF
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            List<Movies> movieList = new List<Movies> 
            {
                new Movies{Name="Ajab Prem Ki Gazhab Kahani",ImageUri="Assets/ajabpremkigazhabkahani.jpg"},
                new Movies{Name="Bolt",ImageUri="Assets/bolt.jpg"},
                new Movies{Name="Brother's Bloom",ImageUri="Assets/brothersbloom.jpg"},
                new Movies{Name="Cloudy With A Chance Of MeatBalls",
                    ImageUri="Assets/cloudywithachanceofmeatballs.jpg"},
                new Movies{Name="How To Lose A Guy In 10 Days",ImageUri="Assets/howtoloseaguyin10days.jpg"},
                new Movies{Name="I am Legend",ImageUri="Assets/iamlegend.jpg"},
                new Movies{Name="Idiocracy",ImageUri="Assets/idiocracy.jpg"},
                new Movies{Name="Knowing",ImageUri="Assets/knowing.jpg"},
                new Movies{Name="Madagascar 2",ImageUri="Assets/madagascar2.jpg"},
                new Movies{Name="Sarkar Raj",ImageUri="Assets/sarkarraj.jpg"},
                new Movies{Name="Taken",ImageUri="Assets/taken.jpg"},
                new Movies{Name="Yes Man",ImageUri="Assets/yesman.jpg"},
            };
            lbMovies.ItemsSource = movieList;
        }
    }

    public class Movies
    {
        public string ImageUri { get; set; }
        public string Name { get; set; }
    }
}
