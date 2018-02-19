using System;
using DevExpress.XtraReports.UI;

namespace Globus5.WPF.Comum.Relatorios
{
    /// <summary>
    /// Listagem para a classe MasterRelatorio1.
    /// <remarks>
    /// Arquivo criado : 11/3/2010 5:10:52 PM. 
    /// Criado por     : alessandro.augusto.
    /// </remarks>
    /// </summary>
    public partial class LstOcorrencias : FGlobus.Relatorios.MasterRelatorio
    {
        #region Construtor

        /// <summary>
        /// Inicializador da classe.
        /// </summary>
        public LstOcorrencias()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializador da classe, que redefine o campo detalhe.
        /// </summary>
        /// <param name="detalhe">Detalhes do relatorio.</param>
        public LstOcorrencias(string detalhe)
        {
            InitializeComponent();

        }

        #endregion

        private void xrRichText1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string detalhes = this.GetCurrentColumnValue("Detalhes").ToString();
            try
            {
                this.xrRichText1.Lines = detalhes.Split(new string[] { "\n" }, StringSplitOptions.None);
            }
            catch
            {
                this.xrRichText1.Text = detalhes;
            }
        }

        #region Atributes

        #endregion

        #region Metodos


        #endregion
    }
}
