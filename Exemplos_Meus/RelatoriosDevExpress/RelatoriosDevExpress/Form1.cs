using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Globus5.WPF.Comum.ws.gestaoDeFrotaOnline;
using FGlobus.Relatorios;

namespace Globus5.WPF.Comum.Relatorios
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Iif([Tempo] == '01/01/0001 00:00:00',' ',[Tempo])

        private void smpBtnLstTempoNoTrecho_Click(object sender, EventArgs e)
        {
            try
            {
                Globus5.WPF.Comum.WebServices.GestaoDeFrotaOnLineWSApp.ConectaUsuarioWap();

                sTempoNoTrecho[] tempoNoTrecho =
                    Globus5.WPF.Comum.WebServices.GestaoDeFrotaOnLineWSApp.RetornarTempoNoTrecho(
                                   new DateTime(2011, 1, 6),
                                   new DateTime(2011, 1, 6, 23, 59, 59),
                                   "A",
                                   4127,
                                   9574,
                                   new int[] { 26605 },
                                   new int[] { 8809, 8834, 8897, 6612, 8810, 8819, 8818, 8809, 8834, 8897, 6566, 6612, 8810, 8819, 8818, 8823, 8811, 8809, 8834, 8897, 6612, 8818, 8823, 8811, 8834, 8897, 6612, 8823, 8811, 6642, 8834, 8897, 6612, 8810, 8811, 6642, 8834, 8897, 6612, 8810, 8818, 8811, 8911, 8834, 8897, 8823, 8911, 8811, 8834 });

                LstTempoNoTrecho report = new LstTempoNoTrecho();
               
                report.Veiculos = Globus5.WPF.Comum.WebServices.FrotaWSApp.RetornarVeiculosPorCodigoVeic(tempoNoTrecho
                    .Select(s => s.CodIntVeiculo)
                    .ToArray());

                report.Linhas = WebServices.GlobusWSApp.RetornarLinhasPermitidasParaUsuario("BGMRODOTEC");
                report.Filiais = Globus5.WPF.Comum.WebServices.ControleWSApp.RetornarFiliaisPorEmpresa(8);

                report.DataSource = tempoNoTrecho;

                MasterPreviewRelatorio previewRelatorio = new MasterPreviewRelatorio(report);
                previewRelatorio.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}