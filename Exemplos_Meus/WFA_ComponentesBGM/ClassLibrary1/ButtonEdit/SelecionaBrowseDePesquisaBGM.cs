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
using FGlobus.Componentes.WinForms;
using System.Globalization;

namespace FGlobus.Componentes.WinForms
{
    [ToolboxItem(false)]
    public partial class SelecionaBrowseDePesquisaBGM : Form
    {
        #region Contrutor

        public SelecionaBrowseDePesquisaBGM(string pesquisaBGM)
        {
            InitializeComponent();
            _pesquisaBGM = pesquisaBGM;
            btnOkPesquisa.DialogResult = DialogResult.OK;
            btnCancelarPesquisa.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Attributes

        private string _pesquisaBGM;

        #endregion

        #region Propriedades

        public String PesquisaBGM
        {
            get
            {
                return _pesquisaBGM;
            }
            set
            {
                _pesquisaBGM = value;
            }
        }

        #endregion

        #region Metodos

        private void FrmSelecionaPesquisa_Load(object sender, EventArgs e)
        {
            string _dirDistribuicaoDlls = @"C:\Globus5\WPF\Distribuicao\";
            string _nomePadraoDll = "BgmRodotec.Globus5.Pesquisas";
            string[] _dlls = Directory.GetFiles(_dirDistribuicaoDlls, _nomePadraoDll + "*.dll");

            Assembly _assembly;
            Type[] _pesquisas;

            trVwPesquisas.Nodes.Clear();
            TreeNode _projeto = new TreeNode("(none)");
            _projeto.ImageIndex = 0;
            _projeto.SelectedImageIndex = 0;
            trVwPesquisas.Nodes.Add(_projeto);

            TreeNode _pesquisa = new TreeNode();
            foreach (string dll in _dlls)
            {
                string _nomeProjeto = dll.
                    Replace(_dirDistribuicaoDlls, "").
                    Replace(_nomePadraoDll + ".", "").
                    Replace(".dll", "");

                _projeto = new TreeNode(_nomeProjeto + "  ");
                _projeto.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                _projeto.ImageIndex = 1;
                _projeto.SelectedImageIndex = 1;
                trVwPesquisas.Nodes.Add(_projeto);

                _assembly = Assembly.LoadFrom(dll);
                _pesquisas = _assembly.GetExportedTypes();

                string module = _assembly.ManifestModule.Name.Replace(".dll", "");
                foreach (Type item in _pesquisas)
                {
                    if (item.BaseType.Name == "MasterPanelBrowseDePesquisa")
                    {
                        string _nomePesquisa = item.ToString().
                           Replace("Globus5." + _nomePadraoDll + ".", "");

                        _pesquisa = _projeto.Nodes.Add(_nomePesquisa);
                        _pesquisa.ImageIndex = 2;
                        _pesquisa.SelectedImageIndex = 2;
                        _pesquisa.Tag = module + "." + _nomePesquisa;
                        if ((_nomePadraoDll + "." + _nomeProjeto + "." + _nomePesquisa) == _pesquisaBGM)
                        {
                            _pesquisa.ForeColor = System.Drawing.Color.Blue;
                            _pesquisa.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                        }
                    }
                }

                _assembly = null;
                _pesquisas = null;

                if (_projeto.Nodes.Count == 0)
                {
                    trVwPesquisas.Nodes.Remove(_projeto);
                }
                _projeto.ExpandAll();
            }
            _dlls = null;

        }

        private void btnOkPesquisa_Click(object sender, EventArgs e)
        {
            if (trVwPesquisas.SelectedNode.Parent != null)
            {
                _pesquisaBGM = trVwPesquisas.SelectedNode.Tag.ToString().Trim();
                Close();
            }
            if (trVwPesquisas.SelectedNode.Text == "(none)")
            {
                _pesquisaBGM = "(none)";
                Close();
            }
        }

        private void btnCancelarPesquisa_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
