using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ClassLibrary1
{
    [ToolboxItem(false)]
    internal partial class ListaImagens : Form
    {
        public ListaImagens(ImagensBGM controle)
        {
            InitializeComponent();
            
            _controle = controle;
            this.btnOkTela.DialogResult = DialogResult.OK;
            this.btnCancelarTela.DialogResult = DialogResult.Cancel;
        }

        private void btnOkTela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListaImagens_FormClosing(object sender, FormClosingEventArgs e)
        {
           //usrCtrlTreeViewImagesns.Imagens
        }

        private ImagensBGM _controle;

    }
}
