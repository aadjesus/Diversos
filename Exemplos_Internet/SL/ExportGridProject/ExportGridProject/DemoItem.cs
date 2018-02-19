using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace ExportGridProject
{
    public class DemoItem
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
        public string Property6 { get; set; }
        public string Property7 { get; set; }
        public string Property8 { get; set; }
        public string Property9 { get; set; }
        public string Property10 { get; set; }
        public string Property11 { get; set; }
        public string Property12 { get; set; }

        public DemoItem(int count)
        {
            this.Property1 = Guid.NewGuid().ToString();
            this.Property2 = "vivaldirow" + count;
            this.Property3 = "vivaldirow" + count;
            this.Property4 = "vivaldirow" + count;
            this.Property5 = "vivaldirow" + count;
            this.Property6 = "vivaldirow" + count;
            this.Property7 = "vivaldirow" + count;
            this.Property8 = "vivaldirow" + count;
            this.Property9 = "vivaldirow" + count;
            this.Property10 = "vivaldirow" + count;
            this.Property11 = "vivaldirow" + count;
            this.Property12 = "vivaldirow" + count;
        }

        public static List<DemoItem> GetDemoItems()
        {
            int itemcount = 10;
            List<DemoItem> demoitems = new List<DemoItem>();
            for (int i = 0; i < itemcount; i++)
            {
                DemoItem item = new DemoItem(i);
                demoitems.Add(item);
            }
            return demoitems;
        }
    }
}
