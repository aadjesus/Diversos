using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ClassLibrary1
{
    public partial class Informacoes : Form
    {

        public Informacoes(Control controle, Attribute attribute)
        {
            InitializeComponent();

            InformacoesComponenteAttribute informacoesAttribute = attribute as InformacoesComponenteAttribute;

            this.pnlCtrlAutor.Visible = !String.IsNullOrWhiteSpace(informacoesAttribute.Autor);
            this.pnlCtrlVersao.Visible = !String.IsNullOrWhiteSpace(informacoesAttribute.Versao);
            this.pnlCtrlDocumentacao.Visible = !String.IsNullOrWhiteSpace(informacoesAttribute.Documentacao);


            this.lblNomeControle.Text = controle.Name;
            this.lblAutor.Text = informacoesAttribute.Autor;
            this.lblVersao.Text = informacoesAttribute.Versao;
            this.lnkLblDocumentacao.Text = informacoesAttribute.Documentacao;

            ToolboxBitmapAttribute toolboxBitmapAttribute = controle.GetType().GetCustomAttributes(typeof(ToolboxBitmapAttribute), true).FirstOrDefault() as ToolboxBitmapAttribute;
            if (toolboxBitmapAttribute == null)
                this.pcturBxImagem.Visible = false;
            else
                this.pcturBxImagem.Image = toolboxBitmapAttribute.GetImage(controle);

        }
    }
}
