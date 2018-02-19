using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;
using DevExpress.Wpf.Grid;
using System.Data;
using DevExpress.Wpf.Data;
using System.Windows.Controls;
using DevExpress.Wpf.Charts;
using System.Windows.Media;

namespace ExceptionThrownInGrid
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataTable dtTblLinhas = new DataTable();
        public Window1()
        {
            InitializeComponent();


            DataColumn CodigoEmpresa = new DataColumn("CodigoEmpresa", typeof(Int32));
            DataColumn CodigoFl = new DataColumn("CodigoFl", typeof(Int32));
            DataColumn CodigoGa = new DataColumn("CodigoGa", typeof(Int32));
            DataColumn CodIntLinha = new DataColumn("Linha", typeof(string));
            DataColumn Qtde = new DataColumn("Qtde", typeof(Int32));

            dtTblLinhas.Columns.AddRange(new System.Data.DataColumn[] {
            CodigoEmpresa,
            CodigoFl,
            CodigoGa,
            CodIntLinha,
            Qtde});

            for (int i = 1; i < 20; i++)
            {
                DataRow linha = dtTblLinhas.NewRow();
                linha["CodigoEmpresa"] =  i < 10 ? 2 : 1 ;
                linha["CodigoFl"] = i < 15 ? 2 : 1;
                linha["CodigoGa"] = i < 5 ? 3 : i < 10 ? 2 : i < 15 ? 4 : 5;
                linha["Linha"] = (i + 3).ToString("00000");
                linha["Qtde"] = i;

                dtTblLinhas.Rows.Add(linha);
            }

            grid.DataContext = dtTblLinhas;



        }

        //private Controller _controller;

        private void TableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (grid1.SelectedRows.Count > 0) // i there a selected Row?
            {
                // ((DataRowView)(grid1.FocusedRow)).Row.ItemArray[2]
                //int[] aa = grid1.GetSelectedRowsHandles();

                //grid1.FocusedRowData.CellData[3].Value
                //MessageBox.Show(((DataRowView)(grid1.FocusedRow)).Row["Linha"].ToString()); //<<<<<<<<<<<<<<<<< ERROR
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split(new string[] { "\\" }, StringSplitOptions.None)[1]);
            MessageBox.Show(DateTime.Now.ToShortDateString());

            DataTable dtTblLinhas = new DataTable();
            DataColumn dtClmQtde = new DataColumn("Qualitative", typeof(decimal));
            DataColumn dtClmOcorrencia = new DataColumn("Status", typeof(string));

            dtTblLinhas.Columns.AddRange(new System.Data.DataColumn[] {
            dtClmQtde,
            dtClmOcorrencia});

            for (int i = 1; i < 10; i++)
            {
                DataRow linha = dtTblLinhas.NewRow();
                linha["Qualitative"] = (10 / i * 2);
                linha["Status"] = i.ToString("00000");
                dtTblLinhas.Rows.Add(linha);
            }

            Window3 window3 = new Window3();
            window3.xisto1010.DataSource = dtTblLinhas;
            window3.gridControl1.DataContext = dtTblLinhas;

            window3.ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4();
            window4.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Window5 window5 = new Window5();
            window5.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Form1 form1 = new Form1(dtTblLinhas);

            form1.Show();
        }

    }

    public class ViewModel
    {
        private readonly ObservableCollection<Person> m_DataSource = new ObservableCollection<Person>();

        public ObservableCollection<Person> DataSource
        {
            get { return m_DataSource; }
        }
    }

    public class TestData
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
    }

}