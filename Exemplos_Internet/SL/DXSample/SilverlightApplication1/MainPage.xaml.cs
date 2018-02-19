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
using System.Windows.Data;
using System.Linq;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        List<A> source = A.Source();
        public MainPage()
        {
            InitializeComponent();
            PagedCollectionView pagedCollectionView = new PagedCollectionView(source);
            pagedCollectionView.PageSize = 30;
            pager.Source = pagedCollectionView;
            grid.DataSource = DevExpress.Xpf.Core.Native.DataBindingHelper.ExtractDataSourceFromCollectionView(pager.Source);
        }

    }
    public class A
    {
        public static List<A> Source()
        {
            List<A> res = new List<A>();
            Random rnd = new Random(199);
            for (int i = 0; i < 1000; i++)
                res.Add(new A()
                {
                    Id = i,
                    Count = rnd.Next(10000),
                    Average = rnd.Next(10000),
                    Name = "Name" + rnd.Next(10000).ToString(),
                    Category = "Category" + rnd.Next(10000).ToString()
                });
            return res;
        }
        public int Id { get; set; }
        public int Count { get; set; }
        public int Average { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }


    }
}
