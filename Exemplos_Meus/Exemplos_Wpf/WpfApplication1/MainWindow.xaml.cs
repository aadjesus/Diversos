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
using DevExpress.Wpf.Editors;
using System.Xml;
using System.Data;
using DevExpress.Wpf.Printing.UI;
using DevExpress.Wpf.Printing;
using DevExpress.Wpf.Charts;
using DevExpress.Wpf.Docking;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _dataRow = (new List<string>() 
            {
                "aaaa", "bbbb", "cccc", "dddd", "aaaseeeaa", "33###", "jjjj" 
            })
            .Select((s,index) => new sGraficos()
            {
                ArgumentDataMember = s,
                ValueDataMember = index+1
               // ValueDataMember = s.Length * 10
            })
            .ToArray();

            chart.Diagram.Series[0].Points.Clear();
            chart.Diagram.Series[0].DataSource = _dataRow
                .Select(s => new
                {
                    s.ArgumentDataMember,
                    s.ValueDataMember
                })
                .OrderByDescending(o => o.ValueDataMember)            
                ;

            uuyuyuy.Tag = 0;
            uuyuyuy.Maximum = chart.Diagram.Series[0].Points.Count;
            uuyuyuy.Minimum = 1;
            uuyuyuy.Value = uuyuyuy.Maximum;
            uuyuyuy.Tag = null;
        }

        private sGraficos[] _dataRow;
        private struct sGraficos
        {
            public string ArgumentDataMember;
            public decimal ValueDataMember;
        }

        private void dtPkrDe_KeyUp(object sender, KeyEventArgs e)
        {
            DateEdit dateEdit = sender as DateEdit;
            if (dateEdit != null)
                (FindName(String.Concat("border", dateEdit.Name.Replace("dtPkr", ""))) as Border).BorderBrush =
                    (System.Windows.Media.Brush)(new System.Windows.Media.BrushConverter().ConvertFrom("Red"));


            if (e.Key == Key.Return)
            {

            }
        }

        private void dtPkrDe_KeyDown(object sender, KeyEventArgs e)
        {
            DateEdit dateEdit = sender as DateEdit;
            if (dateEdit != null)
                (FindName(String.Concat("border", dateEdit.Name.Replace("dtPkr", ""))) as Border).BorderBrush =
                (System.Windows.Media.Brush)(new System.Windows.Media.BrushConverter().ConvertFrom("Transparent"));
        }

        private void dtPkrDe_LostFocus(object sender, RoutedEventArgs e)
        {
            dtPkrDe_KeyDown(sender, null);

        }

        private void dtPkrDe_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            dtPkrDe_KeyUp(sender, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var aaa = cmbBxFase.Items.IndexOf(
                cmbBxFase.Items.Cast<ListBoxItem>().Where(w1 => w1.Content.Equals(cmbBxFase1.Text)).SingleOrDefault()
                )
                ;
            if (aaa == null)
            {

            }


            //var xx = cmbBxFase1.Items
            //        .Cast<ComboBox>()
            //        .Where(w=> w.SelectedIndex = w.Items.Cast<ListBoxItem>().Where(w1=> w1.Content.Equals("")).SingleOrDefault().  )

            //cmbBxFase.SelectedIndex = Convert.ToInt32(
            //    cmbBxFase1.Items
            //        .Cast<ComboBox>()
            //        ListBoxItem
            //        .Where(w => w.Items   Content.Equals(cmbBxFase1.Text))
            //        .SingleOrDefault().TabIndex
            //        );


            if (cmbBxFase.SelectedIndex == 0)
            {

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            XmlDataDocument _xmlDataDoc = new XmlDataDocument();
            _xmlDataDoc.DataSet.ReadXml(@"c:\Users\alessandro.augusto\Documents\Visual Studio\Exemplos_Meus\WpfApplication1\ItinerarioSantos.kml");

            List<string> xxx = new List<string>();
            foreach (DataRelation tabela in _xmlDataDoc.DataSet.Relations)
            {
                //Placemark_LineString
                if (tabela.RelationName.Equals("Document_Placemark"))
                {
                    foreach (DataRow linha in tabela.ChildTable.Rows)
                    {
                        xxx.Add(String.Concat("xxxxxxxxx", linha.Field<string>("description")));

                        //foreach (var item in collection)
                        //{

                        //}
                    }


                    //foreach (DataRow linha in tabela.Rows)
                    //{
                    //    

                    //    foreach (DataTable tabela2 in linha.Table.DataSet.Tables)
                    //    {
                    //        if (tabela2.TableName.Equals("LineString"))
                    //        {
                    //            foreach (DataRow linha2 in tabela2.Rows)
                    //            {
                    //                xxx.Add(String.Concat(tabela2.TableName, "zzzzzzzzz", linha2.Field<string>("coordinates")));
                    //            }
                    //        }

                    //    }
                    //}
                }

            }
            if (xxx == null)
            {

            }

        }

        private void dtPkrDe_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {

                FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;
                TraversalRequest request = new TraversalRequest(focusDirection);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

                if (elementWithFocus != null)
                    elementWithFocus.MoveFocus(request);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {



            //grid1.Children.Remove(chart2);

            var link = new SimpleLink();
            link.Detail = Resources["PrintingTemplate"] as DataTemplate;
            link.DetailCount = 1;
            link.CreateDetail += link_CreateDetail;

            link.CreateDocument(true);

            link.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 10);
            link.Landscape = true;
            //Window aaa = new Window();
            //aaa.Height = 2;
            //aaa.Width = 1;
            //aaa.Opacity = 100;
            //aaa.ShowInTaskbar = false;
            //aaa.Show();            

            // link.ShowPrintPreviewDialog(aaa);

            PrintPreviewWindow aa = new PrintPreviewWindow();
            aa.PrintingSystem = link.PrintingSystem;
            aa.ShowDialog();

            //  ((ContentControl)chart2.Parent).Content = null;

            //  grid1.Children.Add(chart2);
        }

        void link_CreateDetail(object sender, CreateAreaEventArgs e)
        {
            ChartControl xx = chart2;
            EEEEEEEEEEEEE.DataSource = brStckdSeries2DBarra.DataSource;

            xx.Height = 700;
            xx.Width = 1000;
            e.Data = new
            {
                Canvas = xx
            };
        }

        public double GetAngle()
        {
            return this.slider1.Value;
        }

        private void Slider_SizeChanged(object sender, SizeChangedEventArgs e)
        {


        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //this.dddddddddddd.Content =
            //    Math.Round(uuyuyuy.Value).ToString()
            //    ;
            //if (uuyuyuy.Tag == null)
            //{
            //    chart.Diagram.Series[0].DataSource = _dataRow
            //        .OrderByDescending(o => o.ValueDataMember)
            //        .Where((s, index) => (index + 1) <= Math.Round(e.NewValue))
            //        .Select(s => new
            //        {
            //            s.ArgumentDataMember,
            //            s.ValueDataMember
            //        })
            //        .ToArray();
            //}
        }

    }
}

