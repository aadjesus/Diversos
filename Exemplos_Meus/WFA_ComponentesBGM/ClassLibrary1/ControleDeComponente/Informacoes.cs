using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Classe do formulario de informaçõe do componente
    /// </summary>
    internal partial class Informacoes : Form
    {
        #region Construtor
        
        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="controle">Controle que acionou a tela.</param>
        /// <param name="attribute">Abrituto de informações</param>
        public Informacoes(Control controle, Attribute attribute)
        {
            InitializeComponent();

            InformacoesComponenteAttribute informacoesAttribute = attribute as InformacoesComponenteAttribute;

            this.pnlCtrlAutor.Visible = !String.IsNullOrWhiteSpace(informacoesAttribute.Autor);
            this.pnlCtrlVersao.Visible = !String.IsNullOrWhiteSpace(informacoesAttribute.Versao);
            this.pnlCtrlDocumentacao.Visible = !String.IsNullOrWhiteSpace(informacoesAttribute.Documentacao);

            this.lblNomeControle.Text = controle.GetType().Name;
            this.lblAutor.Text = informacoesAttribute.Autor;
            this.lblVersao.Text = informacoesAttribute.Versao;
            this.lnkLblDocumentacao.Text = informacoesAttribute.Documentacao;

            ToolboxBitmapAttribute toolboxBitmapAttribute = controle.GetType().GetCustomAttributes(typeof(ToolboxBitmapAttribute), true).FirstOrDefault() as ToolboxBitmapAttribute;
            if (toolboxBitmapAttribute == null)
                this.pcturBxImagem.Visible = false;
            else
                this.pcturBxImagem.Image = toolboxBitmapAttribute.GetImage(controle,true);
        }

        #endregion

        #region Metodos
        
        private void lnkLblDocumentacao_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
