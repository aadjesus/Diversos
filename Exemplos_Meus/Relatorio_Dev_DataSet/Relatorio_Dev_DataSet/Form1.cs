using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Relatorio_Dev_DataSet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Random valor1 = new Random();
            Random valor2 = new Random();
            Random valor3 = new Random();
            for (int i = 1; i <= 10; i++)
            {
                DataRow dataRow1 = dataSet1.Tabela1.NewRow();
                dataRow1["Int32"] = valor1.Next(i,200);
                dataRow1["String"] = string.Format("Descrição:{0}", dataRow1["ID"]);
                dataRow1["DateTime"] = DateTime.Now.AddDays(valor1.Next(1, 30));
                dataRow1["Decimal"] = valor1.NextDouble() ;
                dataRow1["Boolean"] = (Convert.ToInt32(dataRow1["Int32"]) % 2).Equals(0);

                dataSet1.Tabela1.Rows.Add(dataRow1);

                for (int h = 1; h <= valor2.Next(1, i + 5); h++)
                {
                    DataRow dataRow2 = dataSet1.Tabela2.NewRow();
                    dataRow2["IDTab1"] = dataRow1["ID"];
                    dataRow2["Int32"] = valor2.Next(h, 200);
                    dataRow2["String"] = string.Format("Descrição:{0}", dataRow2["ID"]);
                    dataRow2["DateTime"] = DateTime.Now.AddDays(valor2.Next(1, 30));
                    dataRow2["Decimal"] = valor2.NextDouble();
                    dataRow2["Boolean"] = (Convert.ToInt32(dataRow2["Int32"]) % 2).Equals(0);
                    dataSet1.Tabela2.Rows.Add(dataRow2);

                    for (int j = 1; j <= valor3.Next(1, i + 5); j++)
                    {
                        DataRow dataRow3 = dataSet1.Tabela3.NewRow();
                        dataRow3 = dataSet1.Tabela3.NewRow();
                        dataRow3["IDTab1"] = dataRow1["ID"];
                        dataRow3["IDTab2"] = dataRow2["ID"];
                        dataRow3["Int32"] = valor3.Next(j, 200);
                        dataRow3["String"] = string.Format("Descrição:{0}", dataRow3["ID"]);
                        dataRow3["DateTime"] = DateTime.Now.AddDays(valor3.Next(1, 30));
                        dataRow3["Decimal"] = valor3.NextDouble();
                        dataRow3["Boolean"] = (Convert.ToInt32(dataRow3["Int32"]) % 2).Equals(0);
                        dataSet1.Tabela3.Rows.Add(dataRow3);
                    }
                }

            }

            xtraReport1.DataSource = dataSet1;
            xtraReport1.DataMember = "dataTable1";

        }

        XtraReport1 xtraReport1 = new XtraReport1();

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            xtraReport1.ShowPreview();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            xtraReport1.ShowDesigner();
        }
    }
}