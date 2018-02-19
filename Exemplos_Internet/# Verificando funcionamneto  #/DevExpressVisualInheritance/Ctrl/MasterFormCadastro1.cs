using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FGlobus.Componentes.WinForms;
using System.Web.Services.Protocols;
using Globus5.WPF.Comum;

namespace Globus5.WPF.Comum.Cadastros
{
    /// <summary>
    /// Form de cadastro para a classe MasterFormCadastro1.
    /// <remarks>
    /// Arquivo criado : 2/16/2011 2:45:46 PM. 
    /// Criado por     : alessandro.augusto.
    /// </remarks>
    /// </summary>
    public partial class MasterFormCadastro1 : FGlobus.Componentes.WinForms.MasterFormCadastro
    {
        #region Construtor

        /// <summary>
        /// Inicializador da classe.
        /// </summary>
        public MasterFormCadastro1()
        {
            InitializeComponent();
        }

        #endregion

        #region Metodos

        private void MasterFormCadastro1_HabilitarBotoes(object sender)
        {
        }

        private void smpBtnBGMGravarMFCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                smpBtnBGMLimparMFCadastro.PerformClick();
            }
            catch (SoapException ex)
            {
                Util.MessagemExcecao(ex);
            }
        }

        private void smpBtnBGMLimparMFCadastro_Click(object sender, EventArgs e)
        {
        }

        private void smpBtnBGMExcluirMFCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                smpBtnBGMLimparMFCadastro.PerformClick();
            }
            catch (SoapException ex)
            {
                Util.MessagemExcecao(ex);
            }
        }

        #endregion
    }
}