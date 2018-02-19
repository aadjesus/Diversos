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
using FGlobus.Util;
using System.Globalization;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel.Design;
using System.Collections;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Classe para mostrar as telas de cadastros ou pesquisas.
    /// </summary>
    [ToolboxItem(false)]
    internal partial class SelecionaTela : Form
    {
        #region Contrutor

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="controle">Controle que executou a tela</param>
        public SelecionaTela(Control controle)
        {
            InitializeComponent();

            if (controle is ButtonEditBGM)
            {
                _tipoTela = eTipoTela.Pesquisas;
                _telaSelecionada = (controle as ButtonEditBGM).BrowseDePesquisa;
            }

            this.Text = String.Concat(this.Text.Trim(), " ", _tipoTela.ToString().ToLower().Remove(_tipoTela.ToString().Length - 1));
            this.btnOkTela.DialogResult = DialogResult.OK;
            this.btnCancelarTela.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Attributes

        private enum eTipoTela
        {
            Pesquisas = 0,
            Cadastros = 1
        }

        private eTipoTela _tipoTela;
        private string _telaSelecionada;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Set/Get) Informa ou retorna a tela selecionada
        /// </summary>
        public String TelaSelecionada
        {
            get
            {
                return _telaSelecionada;
            }
            set
            {
                _telaSelecionada = value;
            }
        }

        #endregion

        #region Metodos

        private void SelecionaTela_Load(object sender, EventArgs e)
        {
            string nomePadraoDll = String.Concat("BgmRodotec.Globus5.", _tipoTela.ToString(), "*.dll");

            this.trVwTela.Nodes.Clear();
            this.trVwTela.Nodes.Add(new TreeNode(FGlobus.Util.Constantes.NONE_DEFAULT)
            {
                ImageIndex = 2,
                SelectedImageIndex = 2
            });

            #region Popula TreeView

            #region Delegate para procurar a pastas com as Dlls 
            // Procura nas pastas C:\Globus5\WPF\Distribuicao\, C:\GlobusMais\WPF\Distribuicao\ ou na informada
            
            Func<string, int, string[]> delegateProcurarDiretorio = null;
            delegateProcurarDiretorio = delegate(string diretorio, int item)
            {
                try
                {
                    return Directory.GetFiles(diretorio, nomePadraoDll);
                }
                catch (Exception)
                {
                    string diretorioAux = @"C:\GlobusMais\WPF\Distribuicao\";
                    ++item;
                    if (item.Equals(2))
                    {
                        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                        folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                        folderBrowserDialog.SelectedPath = @"C:\";
                        folderBrowserDialog.ShowDialog();
                        diretorioAux = folderBrowserDialog.SelectedPath;
                    }
                    else if (item.Equals(3))
                        return null;

                    return delegateProcurarDiretorio(diretorioAux, item);
                }
            };

            string[] arquivos = delegateProcurarDiretorio(@"C:\Globus5\WPF\Distribuicao\", 0);

            #endregion


            this.trVwTela.Nodes.AddRange(arquivos
                .OrderBy(o => o)
                .Aggregate(new List<TreeNode>(), (listaModulos, itemModulo) =>
                {
                    Assembly assemblyProjeto = Assembly.LoadFile(itemModulo);
                    try
                    {
                        var telas = assemblyProjeto.GetTypes()
                            .Where(w => w.BaseType.FullName.Equals(typeof(MasterFormCadastro).ToString()) ||
                                        w.BaseType.FullName.Equals(typeof(MasterPanelBrowseDePesquisa).ToString()));

                        if (telas != null && telas.Count() > 0)
                        {
                            // Nivel modulo
                            TreeNode treeNodeModulo = new TreeNode(telas.First().Assembly.GetName().Name.Replace(nomePadraoDll + ".", ""));

                            treeNodeModulo.Nodes.AddRange(telas
                                .Aggregate(new List<TreeNode>(), (listaTelas, type) =>
                                {
                                    // Nivel das telas
                                    TreeNode treeNodeTelas = new TreeNode(type.Name);
                                    treeNodeTelas.ImageIndex = Convert.ToInt16(_tipoTela);

                                    // Se for pesquisa verifica se existe uma tela de cadastro associado na pesquisa.
                                    if (_tipoTela.Equals(eTipoTela.Pesquisas))
                                    {
                                        try
                                        {
                                            //MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = Activator.CreateInstance(type) as MasterPanelBrowseDePesquisa;
                                            //if (masterPanelBrowseDePesquisa != null && !String.IsNullOrEmpty(masterPanelBrowseDePesquisa.BrowseDePesquisa.TelaDeCadastro))
                                            //{
                                            //    treeNodeTelas.ImageIndex = 4;
                                            //    MasterFormCadastro masterFormCadastro = FGlobus.Util.Util.RetornarClasseDoAssemblyName<MasterFormCadastro>(masterPanelBrowseDePesquisa.BrowseDePesquisa.TelaDeCadastro);
                                            //    if (masterFormCadastro != null)
                                            //        treeNodeTelas.ToolTipText = masterFormCadastro.Text;
                                            //}
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                    treeNodeTelas.Tag = String.Concat(type.Assembly.GetName().Name, ".", type.Name);
                                    treeNodeTelas.SelectedImageIndex = treeNodeTelas.ImageIndex;
                                    listaTelas.Add(treeNodeTelas);

                                    type = null;
                                    return listaTelas;
                                })
                                .ToArray());

                            listaModulos.Add(treeNodeModulo);
                        }
                    }
                    catch (Exception)
                    {
                    }

                    assemblyProjeto = null;

                    return listaModulos;
                })
                .ToArray());

            #endregion

            #region Posiciona no ite selecionado

            TreeNode treeNodeSelecionado = null;
            this.trVwTela.Nodes
                 .OfType<TreeNode>()
                 .Where(w => w.Nodes
                             .OfType<TreeNode>()
                             .Where(w1 => w1.Tag != null && w1.Tag.ToString().Equals(_telaSelecionada))
                             .Update(u =>
                             {
                                 u.ForeColor = System.Drawing.Color.Blue;
                                 u.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                                 u.TreeView.Select();
                                 treeNodeSelecionado = u;
                             })
                             .Count() > 0)
                 .DefaultIfEmpty(new TreeNode())
                 .First()
                 .Expand();
            this.trVwTela.SelectedNode = treeNodeSelecionado;

            #endregion
        }

        private void btnOkTela_Click(object sender, EventArgs e)
        {
            try
            {
                _telaSelecionada = this.trVwTela.SelectedNode.Tag.ToString();
            }
            catch (Exception)
            {
                _telaSelecionada = FGlobus.Util.Constantes.NONE_DEFAULT;
            }
            Close();
        }

        private void btnCancelarTela_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tela"></param>
        /// <returns></returns>
        [Obsolete("Não utilizar.")]
        public static object LocalizaTelaSelecionada(string tela)
        {
            return FGlobus.Util.Util.RetornarClasseDoAssemblyName<MasterForm>(tela);
        }

        #endregion
    }
}
