using System;
using DevExpress.XtraReports.UI;

namespace Globus5.WPF.Comum.Relatorios
{
    /// <summary>
    /// Listagem para a classe LstTabelaDoMotorista1.
    /// <remarks>
    /// Arquivo criado : 4/28/2010 4:08:50 PM. 
    /// Criado por     : alessandro.augusto.
    /// </remarks>
    /// </summary>
    public partial class LstTabelaDoMotorista : FGlobus.Relatorios.MasterRelatorio
    {
        #region Construtor

        /// <summary>
        /// Inicializador da classe.
        /// </summary>
        public LstTabelaDoMotorista()
        {
            InitializeComponent();
        }

        #endregion

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell18.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
            xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
        }

        private void GrpFtDetLinha_AfterPrint(object sender, EventArgs e)
        {
            xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTableCell9.Borders = xrTableCell13.Borders;
            xrTableCell18.Borders = xrTableCell13.Borders;
        }

        #region Atributes

        #endregion

        #region Metodos

        #endregion
    }
}
