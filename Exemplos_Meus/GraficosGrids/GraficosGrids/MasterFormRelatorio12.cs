using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FGlobus.Componentes.WinForms;
using System.IO;
using System.Linq;
using FGlobus.Relatorios;
using FGlobus.Excecao;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Text;

namespace GraficosGrids
{
    /// <summary>
    /// Form de cadastro para a classe MasterFormRelatorio1.
    /// <remarks>
    /// Arquivo criado : 5/10/2010 8:10:12 AM. 
    /// Criado por     : alessandro.augusto.
    /// </remarks>
    /// </summary>
    public partial class MasterFormRelatorio1 : MasterForm
    {
        /// <summary>
        /// Inicializador da classe.
        /// </summary>
        public MasterFormRelatorio1()
        {
            InitializeComponent();

            StreamReader stream = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(Resource1.Informacoeseventos)));
            List<string> arquivo = new List<string>();
            string linha = null;
            while ((linha = stream.ReadLine()) != null)
                arquivo.Add(linha.Replace("#�#", "# #"));

            _dataTable = FGlobus.Util.Util.DesConcatenarResultadoLinq(arquivo.ToArray());
            _dataTable.Columns.Add("CampoBool", typeof(bool));
            _dataTable.Columns.Add("CampoDateTime", typeof(DateTime));

            _dataTable.Rows.Cast<DataRow>()
                .Update(u =>
                {
                    u["CampoBool"] = u.Field<string>("Column13").Equals("MASCULINO");
                    u["CampoDateTime"] = DateTime.Now;
                });

            
            gridControl1.DataSource = _dataTable;
            gridControl1.Refresh();
            this.WindowState = FormWindowState.Maximized;
            
        }

        private DataTable _dataTable;
        private void smpBtnBGMImprimirMFRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                MasterRelatorio12 report = new MasterRelatorio12();

                report.DataSource = _dataTable;

                MasterPreviewRelatorio previewRelatorio = new MasterPreviewRelatorio(report, this, true);
                previewRelatorio.ShowDialog();
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(BOExcecao.NenhumaInfParaOsDadosInfo))
                    MessageBox.Show(BOExcecao.NenhumaInfParaOsDadosInfo.Replace("BGM-FGLOBUS.BO.00032 - ", ""));
            }

        }

        private void smpBtnBGMLimparMFRelatorio_Click(object sender, EventArgs e)
        {
        }

        private void simpleButtonBGM1_Click(object sender, EventArgs e)
        {
            Window1 window1 = new Window1();
            window1.DataSourceGraficos = _dataTable;
            window1.ShowDialog();
        }
       
        private void simpleButtonBGM2_Click(object sender, EventArgs e)
        {

            gridControl1.MainView = ((LayoutView)layoutView1);
            simpleButtonBGM2.Enabled = false;
            simpleButtonBGM3.Enabled = true;
        }

        private void simpleButtonBGM3_Click(object sender, EventArgs e)
        {
            gridControl1.MainView = ((BandedGridView)bandedGridView1);
            simpleButtonBGM3.Enabled = false;
            simpleButtonBGM2.Enabled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Window2 window2 = new Window2();
            window2.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form1 sss = new Form1();
            sss.Show();

        }

    }
}




//1Empresa-2Filial-3Data-4Área-5Condição-6Departamento-7Evento-8Rotina evento-9Tipo evento-10Faixa etária-11Função-12Funcionário-13Sexo-14Motivo desligamento-15Setor-16Tipo folha-17Quantidade-18Referencia-19Valor
//1Empresa-2Filial-3Data-4Área-5Condição-6Departamento-7Evento-8Rotina evento-9Tipo evento-10Faixa etária-11Função-12Funcionário-13Sexo-14Motivo desligamento-15Setor-16Tipo folha-17Quantidade-18Referencia-19Valor