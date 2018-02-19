using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using DevExpress.LookAndFeel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace SalvarBrowseDePesquisa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            List<MyStruct> listaMyStruct = new List<MyStruct>();
            for (int i = 0; i < 200; i++)
            {
                listaMyStruct.Add(new MyStruct()
                {
                    Codigo = i,
                    Data = DateTime.Now.AddDays(i),
                    Descricao = i.ToString(),
                    Mes = DateTime.Now.AddDays(i).Month
                });
            }

            gridControl1.DataSource = listaMyStruct.Select(s => new
            {
                s.Codigo,
                s.Data,
                s.Descricao,
                s.Mes
            }).ToArray();
        }

        struct MyStruct
        {
            public int Codigo;
            public int Mes;
            public string Descricao;
            public DateTime Data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _arqXmlBrowseDePesquisa = Path.GetTempPath() + "arqXmlBrowseDePesquisa.xml";


            DevExpress.Utils.OptionsLayoutGrid options = new DevExpress.Utils.OptionsLayoutGrid();
            options.LayoutVersion = Application.ProductVersion;
            options.StoreAllOptions = true;
            gridControl1.DefaultView.SaveLayoutToXml(_arqXmlBrowseDePesquisa, options);

            gridControl1.MainView.SaveLayoutToXml(_arqXmlBrowseDePesquisa + "x");

            XmlDataDocument _xmlDataDoc = new XmlDataDocument();
            _xmlDataDoc.Load(_arqXmlBrowseDePesquisa);
            if (_xmlDataDoc.InnerXml == null)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _arqXmlBrowseDePesquisa = Path.GetTempPath() + "arqXmlBrowseDePesquisa.xml";
            gridControl1.MainView.RestoreLayoutFromXml(_arqXmlBrowseDePesquisa);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink1 = new DevExpress.XtraPrinting.PrintableComponentLink();
            //printableComponentLink1.CreateReportHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink1_CreateReportHeaderArea);
            //printableComponentLink1.Component = gridControl1;

            //dataGridLink1.ShowRibbonPreview(new UserLookAndFeel(gridControl1));

            //printableComponentLink1.

            //PageHeaderFooter phf = printableComponentLink1.PageHeaderFooter as PageHeaderFooter;
            //phf.Header.Content.Clear();
            //phf.Header.Content.AddRange(new string[] { "11111", "2222", "3333" });

            //phf.Header.LineAlignment = BrickAlignment.Center;

            printableComponentLink1.CreateDocument();
            printableComponentLink1.ShowPreview();

            //gridControl1.ShowPrintPreview();
        }


        private void printableComponentLink1_CreateMarginalHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {         
            PageTableBrick table = new PageTableBrick();
            TableRow row = table.Rows.AddRow();
            
            //printableComponentLink1.Margins.
            //printableComponentLink1.Margins.Right
            //    printableComponentLink1.Margins.Left

            TextBrick brick = new TextBrick();
            brick.Rect = new RectangleF(0, 0, 742, 18);            
            brick.ForeColor = Color.LightSteelBlue;
            brick.Text = "xxxxxx12345zzzzz12345aaaaa12345yyyyyyyyy12345xxxxxx12345zzzzz12345aaaaa";
            brick.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            brick.Font = new Font("Nina", 12, FontStyle.Bold);
            brick.BorderWidth = 0;            
            row.Bricks.Add(brick);


            LineBrick brick1 = new LineBrick();
            row = table.Rows.AddRow();
            brick1.Rect = new RectangleF(0, 0, 742, 18);
            brick1.LineDirection = DevExpress.XtraReports.UI.LineDirection.Horizontal;
            brick1.ForeColor = Color.LightSteelBlue;
            brick1.LineWidth = 2;
            brick1.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            brick1.BorderWidth = 0;
            
            row.Bricks.Add(brick1);

            table.UpdateSize();
            e.Graph.DrawBrick(table);           
        }


    }
}
