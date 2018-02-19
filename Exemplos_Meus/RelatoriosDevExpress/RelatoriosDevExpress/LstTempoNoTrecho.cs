using FGlobus.Relatorios;
using Globus5.WPF.Comum.ws.globus;
using FGlobus.Componentes.WinForms.ws.controle;
using Globus5.WPF.Comum.ws.frota;
using System;
using System.Linq;
using DevExpress.XtraReports.UI;
using Globus5.WPF.Comum.ws.gestaoDeFrotaOnline;

namespace Globus5.WPF.Comum.Relatorios
{
    /// <summary>
    /// Listagem para a classe LstTempoNoTrecho.
    /// <remarks>
    /// Arquivo criado : 11/18/2009 4:11:04 PM. 
    /// Criado por     : alessandro.augusto.
    /// </remarks>
    /// </summary>
    public partial class LstTempoNoTrecho : MasterRelatorio
    {
        #region Construtor

        public LstTempoNoTrecho()
        {
            InitializeComponent();
        }

        #endregion

        #region Atributos
        
        public sLinhasPermitidasParaUsuario[] Linhas;
        public FilialDTO[] Filiais;
        public VeiculoDTO[] Veiculos;
        private double qtde = 0;
        private double totalDuracao = 0;

        #endregion

        #region Metodos

        private void xrTblCelFilial_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            int codigoFl = Convert.ToInt32(this.GetCurrentColumnValue("Filial"));
            try
            {
                e.Result =
                    codigoFl.ToString("000") + " - " +
                    (string)(from a in this.Filiais
                             where a.CodigoFl.Equals(codigoFl)
                             select a.EmpresaAutorizadaOBJ.NomeFantasiaEmpresa).First();
            }
            catch
            {
                e.Result = codigoFl.ToString() + " - Filial não localizada";
            }
            e.Handled = true;
        }

        private void xrTblCelLinha_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            try
            {
                foreach (var item in from a in this.Linhas
                                     where a.CodIntLinha.Equals(this.GetCurrentColumnValue("CodIntLinha"))
                                     select new { a.CodigoLinha, a.NomeLinha })
                {
                    e.Result = item.CodigoLinha + "-" + item.NomeLinha;
                }
            }
            catch
            {
                e.Result = "Linha não localizada";
            }
            e.Handled = true;

        }

        private void xrTblCelFaixaHDuracao_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            try
            {
                double media = totalDuracao / qtde;

                e.Result = string.Format("{0:HH:mm}", new DateTime().Add(TimeSpan.FromMilliseconds(media)));

                qtde = 0;
                totalDuracao = 0;
            }
            catch (Exception)
            {
                e.Result = "";
            }
            e.Handled = true;
        }

        private void LstTempoNoTrecho_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.GetCurrentColumnValue("calcFieldTempo").ToString().Trim()))
            //{
            //    qtde += 1;
            //    totalDuracao += Convert.ToDateTime(this.GetCurrentColumnValue("calcFieldTempo")).TimeOfDay.TotalMilliseconds;
            //}
            DateTime tempo = Convert.ToDateTime(this.GetCurrentColumnValue("Tempo"));
            if (tempo > DateTime.MinValue)
            {
                qtde += 1;
                totalDuracao += tempo.TimeOfDay.TotalMilliseconds;
            }
        }

        #endregion

        private void LstTempoNoTrecho_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // Atualiza a compo sentido com o prefixo do veiculo
            foreach (var item in Veiculos)
                ((sTempoNoTrecho[])this.DataSource)
                    .Where(w => w.CodIntVeiculo.Equals(item.CodigoVeic))
                    .Update(u => u.Sentido = item.PrefixoVeic);
        }
    }
}