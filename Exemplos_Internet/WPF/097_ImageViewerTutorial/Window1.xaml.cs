using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace ImageViewerTutorial
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();
            DataContext = this;
        }

        public class MyImage
        {
            private ImageSource _image;
            private string _name;

            public MyImage(ImageSource image, string name)
            {
                _image = image;
                _name = name;
            }

            public override string ToString()
            {
                return _name;
            }

            public ImageSource Image { get { return _image; } }
            public string Name { get { return _name; } }
        }

        public List<MyImage> AllImages
        {
            get
            {
                List<MyImage> result = new List<MyImage>();
                string[] aaa = System.IO.Directory.GetFiles(
                    Environment.GetFolderPath(
                    Environment.SpecialFolder.MyPictures));

                foreach (string filename in aaa)
                {
                    try
                    {
                        result.Add(
                            new MyImage(
                            new BitmapImage(
                            new Uri(filename)),
                            System.IO.Path.GetFileNameWithoutExtension(filename)));
                    }
                    catch { }
                }
                return result;
            }
        }

    }
}