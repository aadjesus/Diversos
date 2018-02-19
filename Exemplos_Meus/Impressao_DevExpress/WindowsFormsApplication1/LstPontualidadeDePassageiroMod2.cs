using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using FGlobus.Relatorios;
using Globus5.WPF.Comum.ws.escala;
using System.Linq;
using Globus5.WPF.Comum.ws.folha;

namespace Globus5.WPF.Comum.Relatorios
{
    public partial class LstPontualidadeDePassageiroMod2 : MasterRelatorio
    {
        #region Contrutor

        public LstPontualidadeDePassageiroMod2()
        {
            InitializeComponent();

/*
            foreach (var item in this.Controls)
            {
                if (item == null)
                {

                }

            }

            GroupHeaderBand[] grpHdBands = this.Bands.Cast<object>().ToArray()
                            .Where(a => a.GetType().Equals(typeof(GroupHeaderBand)))
                            .Select(a => a).Cast<GroupHeaderBand>().ToArray();
            foreach (var item in grpHdBands)
            {
                //(((XRTable)((new System.Linq.SystemCore_EnumerableDebugView(item)).Items[0]))).Controls
                //                (XRTable)item[0]


                XRTable[] xRTable = item.Controls.Cast<object>().ToArray()
                            .Where(a => a.GetType().Equals(typeof(XRTable)))
                            .Select(a => a).Cast<XRTable>().ToArray();
                // XRTableRow 
                // XRTableCell 
                //xRTable[0].Controls[0].Controls[0].Font.Underline
                //xRTable[0].Rows[0].Cells[0].Name

                //XRTableCellCollection   
                //xRTable.Cast<XRTable>().ToArray()

                //                'DevExpress.XtraReports.UI.XRTableRowCollection' no tipo 'DevExpress.XtraReports.UI.XRTableRow'.
                string a1 = xRTable.ToString();
                if (a1 == null)
                {

                }
                //(xRTable1.Cast<XRTableRowCollection>().ToArray()[0].LastRow).Cells[0].Name
                var xRTable1 = xRTable.Cast<XRTable>()
                            .Select(a => a.Rows)

                            ;

                foreach (var item1 in xRTable1)
                {
                    if (item1 == null)
                    {

                    }
                }

                //XRTableRowCollection[] xRTableRow = xRTable.Where(a => a.Rows).ToArray();
                //XRTableCell[] xRTableCell = xRTableRow.ToArray().Where(a => a.Cells).ToArray();


                //xRTable[0].Controls[0].Controls[0].Name

                //item.Controls
                if (xRTable == null)
                {

                }


            }
 */
        }

        #endregion

        #region Atributes

        public LocaisDTO[] ListaLocais;
        public FGlobus.Componentes.WinForms.ws.controle.FilialDTO[] ListaFiliais;
        public FuncionarioDTO[] ListaFuncionarios;

        #endregion

        #region Metodos

        private void xrTblCelGrpHdFilial_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            try
            {
                e.Result = Convert.ToInt32(this.GetCurrentColumnValue("CodigoFl")).ToString("000") + " - " +
                    ListaFiliais.Where(a => a.CodigoEmpresa.Equals(Convert.ToInt32(this.GetCurrentColumnValue("CodigoEmpresa"))) &&
                                       a.CodigoFl.Equals(Convert.ToInt32(this.GetCurrentColumnValue("CodigoFl"))))
                           .Select(a => a.EmpresaAutorizadaOBJ.NomeFantasiaEmpresa).SingleOrDefault();
            }
            catch
            {
                e.Result = "000 - Filial não localizada";
            }
            e.Handled = true;

        }

        private void xrTblCelDtLocal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                ((XRTableCell)sender).Text = ListaLocais
                    .Where(a => a.CodLocalidade.Equals(this.GetCurrentColumnValue("CodLocalidade")))
                    .Select(a => a.CodLocalidade + " - " + (a.CodLocalidade.ToString().Length + 3 + a.DescLocalidade.Length > 22 ? a.DescLocalidadeabrev : a.DescLocalidade)).SingleOrDefault();
            }
            catch
            {
                ((XRTableCell)sender).Text = "";
            }

        }


        #endregion

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

