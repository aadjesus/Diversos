using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows.Resources;
using System.IO;
using System.Drawing;
using System.Windows.Data;


namespace GridExample {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            grid.DataSource = new List<TestData>() {
                new TestData() { PlainText = "LMA AG", MemoText = "Mercedes-Benz SLK \n 2004 \n Silver", BooleanMember = true, Image = GetImage("/Images/1.png") },
                new TestData() { PlainText = "Western Motors", MemoText ="Rolls-Royce Corniche \n 1975 \n Snowy whight", BooleanMember = false, Image = GetImage("/Images/2.png") },
                new TestData() { PlainText = "Sun car rent", MemoText = "Ford Ranger FX-4\n 1999 \n Red rock", BooleanMember = true, Image = GetImage("/Images/3.png") }
            };
        }

        ImageSource GetImage(string path) {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e) {
            view.ShowPrintPreview(this);
        }
    }
}
