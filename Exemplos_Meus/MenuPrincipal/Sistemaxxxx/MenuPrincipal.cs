using DevExpress.XtraBars.Ribbon;
using Globus5.WPF.Comum;
using FGlobus.Componentes.WinForms;
using DevExpress.XtraBars;
using System;
using System.Windows.Forms;

namespace Globus5.WPF.Comum
{
    public partial class MenuPrincipal : RibbonForm
    {
        #region Construtor

        public MenuPrincipal()
        {
            InitializeComponent();
        }

        #endregion

        private void MenuPrincipal_Shown(object sender, System.EventArgs e)
        {
            this.Text = FGlobus.Util.Constantes.NOME_PRODUTO + " - " +
                        Inicializacao.Aplicacao + " - " +
                        Inicializacao.Concedente.NomeConcedente;

            this.brSbItmUsuarioMFMnPrncpl.Caption = String.Concat("Olá ", Inicializacao.FuncionarioSacDTO == null
                    ? Inicializacao.UsuarioDTO == null || String.IsNullOrEmpty(Inicializacao.UsuarioDTO.NomeUsuario)
                        ? Inicializacao.Usuario
                        : Inicializacao.UsuarioDTO.NomeUsuario
                    : ""//Inicializacao.FuncionarioSacDTO.NomeAbreviado.ConverterPrimeiraLetraParaMaiuscula()
                    );

            this.brStcItmVersao.Caption = "Versão: " + Inicializacao.DataHora;
            this.brStcItmVersao.Visibility = Inicializacao.AmbienteBGMRodotec
                ? BarItemVisibility.Always
                : BarItemVisibility.Never;

            this.rbnFavoritos.Ribbon.Minimized = true;

            //PopularFavoritos();
        }

        private void MenuPrincipal_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            for (int formsCount = 0; formsCount < Application.OpenForms.Count; formsCount++)
            {
                if (Application.OpenForms[formsCount] != this)
                {
                    try
                    {
                        Application.OpenForms[formsCount].Close();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void brBtnItmCalculadora_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void brBtnItmGlobusReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Globus5.WPF.Comum.Util.AbrirGlobusReport();
        }

        private void brBtnItmTrocarUsuario_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Mensagem.Pergunta(Mensagem.Perguntas.DesejaEfetuarLogoff))
            {
                Inicializacao.Usuario = string.Empty;

                this.Visible = false;
                Login login = new Login();
                login.Sistema = WebServices.ControleWSApp.RetornarSistemaPorSigla(Inicializacao.Sistema).NomeDoSistema;
                login.ShowDialog();

                //PopularFavoritos();
                this.brSbItmUsuarioMFMnPrncpl.Caption = "Olá " + Inicializacao.UsuarioDTO.NomeUsuario;
                this.Visible = true;
            }
        }

        private void brBtnItmCadastrarNovaSenha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TrocaSenhaUsuario trocaSenhaUsuario = new TrocaSenhaUsuario();
            trocaSenhaUsuario.AbrirForm(e.Item);
        }

        private void brBtnItmSaida_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Mensagem.Pergunta("Deseja sair do módulo ?"))
                this.Close();
        }

        private void brBtnItmAgenda_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void brBttnItmBgmRodotec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void brBttnItmAjuda_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void brBttnItmSobre_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}