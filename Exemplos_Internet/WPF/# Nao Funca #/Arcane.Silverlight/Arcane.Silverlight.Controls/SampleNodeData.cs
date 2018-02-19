using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows.Browser;

namespace Arcane.Silverlight.Controls
{
    public class SampleNodeData
    {

        public string Name
        {
            get;
            set;
        }

        public string VideoUri
        {
            get;
            set;
        }

        public BitmapImage SelectedNodeImage
        {
            get;
            set;
        }

        public BitmapImage NodeImage
        {
            get;
            set;
        }


        private static Random random = new Random();
        private static List<string> ImagesNames = new List<string>(new string[]{"doc", "folder", "gif", "pdf"});

        public static List<SampleNodeData> GetNodes(int number)
        {
            List<SampleNodeData> nodes = new List<SampleNodeData>();

            for (int i = 0; i < number; i++)
            {
                SampleNodeData node = new SampleNodeData();
                BitmapImage image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", string.Concat(ImagesNames[random.Next(0, ImagesNames.Count)], ".png")));
                node.NodeImage = image;
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", string.Concat(ImagesNames[random.Next(0, ImagesNames.Count)], ".png")));
                node.SelectedNodeImage = image;

                node.Name = Guid.NewGuid().ToString();
                nodes.Add(node);
            }

            return nodes;
        }

        public static List<SampleNodeData> GetTablesNodes()
        {
            List<SampleNodeData> nodes = new List<SampleNodeData>();

            for (int i = 0; i < 10; i++)
            {
                SampleNodeData node = new SampleNodeData();
                BitmapImage image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "table.png"));
                node.NodeImage = image;
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "table.png"));
                node.SelectedNodeImage = image;

                node.Name = string.Concat("Table ", i);
                nodes.Add(node);
            }

            return nodes;
        }

        public static List<SampleNodeData> GetVideoNodes()
        {
            List<SampleNodeData> nodes = new List<SampleNodeData>();

            for (int i = 0; i < 10; i++)
            {
                SampleNodeData node = new SampleNodeData();
                node.VideoUri = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "bear.wmv")).AbsoluteUri;
                node.Name = string.Concat("Video ", i);
                nodes.Add(node);
            }

            return nodes;
        }

    }
}
