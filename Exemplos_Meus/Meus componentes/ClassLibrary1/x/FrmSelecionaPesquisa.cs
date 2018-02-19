using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Meus
{
    public partial class frmSelecionaPesquisaBGM : Form
    {
        public frmSelecionaPesquisaBGM()
        {
            InitializeComponent();
        }

        private Atributo _atributo;
        private string _nomePesquisa;

        public frmSelecionaPesquisaBGM(Atributo atributo)
        {
            InitializeComponent();

            btnOkPesquisa.DialogResult = DialogResult.OK;
            btnCancelarPesquisa.DialogResult = DialogResult.Cancel;
            _atributo = atributo;
        }

        public string NomePesquisa
        {
            get
            {
                return _nomePesquisa;
            }
            set
            {
                _nomePesquisa = value;
            }
        }

        private void FrmSelecionaPesquisa_Load(object sender, EventArgs e)
        {
            string _dirDistribuicaoDlls = @"C:\Globus5\WPF\Distribuicao\";
            string _nomePadraoDll = "WPF.Comum.Pesquisas";
            string[] _dlls = Directory.GetFiles(_dirDistribuicaoDlls, _nomePadraoDll + "*.dll");
            Assembly _assembly;
            Type[] _pesquisas;

            trVwPesquisas.Nodes.Clear();
            TreeNode _projeto = new TreeNode();
            TreeNode _pesquisa = new TreeNode();

            foreach (string dll in _dlls)
            {
                string _nomeProjeto = dll.
                    Replace(_dirDistribuicaoDlls, "").
                    Replace(_nomePadraoDll + ".", "").
                    Replace(".dll", "");

                _projeto = new TreeNode(_nomeProjeto + "  ");
                _projeto.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);

                trVwPesquisas.Nodes.Add(_projeto);

                _assembly = Assembly.LoadFrom(dll);
                _pesquisas = _assembly.GetExportedTypes();

                foreach (Type item in _pesquisas)
                {
                    string _nomePesquisa = item.ToString().
                       Replace("Globus5." + _nomePadraoDll + ".", "");

                    _pesquisa = _projeto.Nodes.Add(_nomePesquisa);


                    _pesquisa.Tag = _nomeProjeto + _nomePesquisa;
                }
                _projeto.ExpandAll();
            }
            _pesquisa.Nodes.Find("XtraForm1", true);
        }

        private void btnOkPesquisa_Click(object sender, EventArgs e)
        {
            if (trVwPesquisas.SelectedNode.Parent != null)
            {
                NomePesquisa = trVwPesquisas.SelectedNode.Text;
                Close();
            }
        }
    }
}
