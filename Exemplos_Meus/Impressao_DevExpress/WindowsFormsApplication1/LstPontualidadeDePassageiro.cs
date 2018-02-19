using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using FGlobus.Relatorios;
using Globus5.WPF.Comum.ws.escala;
using Globus5.WPF.Comum.ws.folha;

namespace Globus5.WPF.Comum.Relatorios
{
    public partial class LstPontualidadeDePassageiro : MasterRelatorio
    {
        #region Contrutor

        public LstPontualidadeDePassageiro()
        {
            InitializeComponent();
        }

        #endregion

        #region Atributes

        public LocaisDTO[] ListaLocais;
        public FuncionarioDTO[] ListaFuncionarios;
        public FGlobus.Componentes.WinForms.ws.controle.FilialDTO[] ListaFiliais;

        #endregion

        private void xrTableCell21_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            try
            {
                FGlobus.Componentes.WinForms.ws.controle.FilialDTO filial = Array.Find(ListaFiliais,
                delegate(FGlobus.Componentes.WinForms.ws.controle.FilialDTO retorno)
                {
                    return retorno.CodigoEmpresa == Convert.ToInt32(this.GetCurrentColumnValue("CodigoEmpresa")) &&
                           retorno.CodigoFl == Convert.ToInt32(this.GetCurrentColumnValue("CodigoFl"));
                });
                e.Result = filial.CodigoFl.ToString("000") + " - " + filial.EmpresaAutorizadaOBJ.NomeFantasiaEmpresa;
            }
            catch
            {
                e.Result = "000 - Filial não localizada";
            }
            e.Handled = true;
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string idaVolta;
            idaVolta = Convert.ToString(this.GetCurrentColumnValue("Sentido")) == "I" ? "Ida" : "Volta";

            try
            {
                LocaisDTO local = Array.Find(ListaLocais,
                delegate(LocaisDTO retorno)
                {
                    return retorno.CodLocalidade == Convert.ToInt32(this.GetCurrentColumnValue("CodLocalidade"));
                });
                this.xrTableCell15.Text = idaVolta + " - " + local.CodLocalidade.ToString() + " - " + local.DescLocalidade;
            }
            catch
            {
                this.xrTableCell15.Text = idaVolta + " - " + "Local não identificado";
            }
        }

        private void xrTblCelMotorista_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                ((XRTableCell)sender).Text = ListaFuncionarios
                    .Where(w => w.CodIntFunc.Equals(this.GetCurrentColumnValue("Motorista")))
                    .SingleOrDefault()
                    .ChapaFuncCodFunc;
            }
            catch
            {
                ((XRTableCell)sender).Text = "";
            }
        }       

    }
}

