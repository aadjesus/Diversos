using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ImportXLS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Shown(object sender, EventArgs e)
        {
            OpenFileDialog arqExcel = new OpenFileDialog();
            arqExcel.InitialDirectory = "c:\\temp";
            arqExcel.Filter = "Microsoft Excel|*.xls";
            arqExcel.Title = "Selecione o Arquivo";

            if (arqExcel.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = new DataSet();

                //string columns = String.Join(",", columnNames.ToArray()); 

                OleDbConnection conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + arqExcel.FileName + ";Extended Properties=Excel 8.0;");
                OleDbDataAdapter da = new OleDbDataAdapter("Select * From [TemposCorretos$B1:I500]", conexao);

                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt.DefaultView;
                conexao.Close();
            }
        }

        //private DataTable LoadXLS(string strFile, String sheetName, String column, String value)
        //{

        //    DataTable dtXLS = new DataTable("XLS");
        //    try
        //    {

        //        string strConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";", strFile);

        //        OleDbConnection SQLConn = new OleDbConnection(strConnectionString);
        //        SQLConn.Open();

        //        OleDbDataAdapter SQLAdapter = new OleDbDataAdapter();

        //        OleDbCommand selectCMD = new OleDbCommand("SELECT * FROM [" + sheetName + "$] WHERE " + column + " = @" + column, SQLConn);
        //        selectCMD.Parameters.Add("@" + column, OleDbType.VarChar).Value = value;
        //        SQLAdapter.SelectCommand = selectCMD;
        //        SQLAdapter.Fill(dtXLS);
        //        SQLConn.Close();

        //    }

        //    catch (Exception e)
        //    {

        //        Console.WriteLine(e.ToString());

        //    }



        //    return dtXLS;
        //}

        
    }
}

