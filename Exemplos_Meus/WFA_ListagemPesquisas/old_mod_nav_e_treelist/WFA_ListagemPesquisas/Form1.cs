using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraNavBar;
using System.Reflection;
using FGlobus.Util;
using FGlobus.Componentes.WinForms;
using DevExpress.XtraGrid.Views.Grid;
using System.Threading;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;

namespace WFA_ListagemPesquisas
{
    public partial class Form1 : MasterFormGlobus
    {
        public Form1()
        {
            InitializeComponent();

            string nomePadraoDll = "BgmRodotec.Globus5.Pesquisa";

            this.pnlCtrlNavegacao.Dock = DockStyle.Fill;
            this.panelControl1.Dock = DockStyle.Fill;

            radioGroup1.SelectedIndex = 0;
            this.elmntHstLogoGlobus.Child = new UsrCtrlImagemMenuPrincipal();

            #region Delegate para procurar a pastas com as Dlls
            // Procura nas pastas C:\Globus5\WPF\Distribuicao\, C:\GlobusMais\WPF\Distribuicao\ ou na informada

            Func<string, int, string[]> delegateProcurarDiretorio = null;
            delegateProcurarDiretorio = delegate(string diretorio, int item)
            {
                try
                {
                    return Directory.GetFiles(diretorio, nomePadraoDll + "*.dll");
                    //return Directory.GetFiles(diretorio, nomePadraoDll);
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

            xtrTbPgInicial.Controls.Remove(tblLytPnlAguarde);

            this.xtrTbCtrlPesquisas.TabPages.Clear();
            this.treeList1.Nodes.Clear();

            this.navBarControl1.Items.Clear();
            this.navBarControl1.Groups.Clear();
            this.navBarControl1.Groups.AddRange(arquivos
                .Aggregate(new List<NavBarGroup>(), (listaModulos, itemModulo) =>
                {
                    Assembly assemblyProjeto = Assembly.LoadFrom(itemModulo);
                    IEnumerable<Type> telas = assemblyProjeto.GetTypes()
                        .Where(w => w.BaseType.Equals(typeof(MasterPanelBrowseDePesquisa)))
                        ;

                    bool criaNode = true;
                    int parentID = 0;
                    NavBarItem[] listaNavBarItem = telas
                        .Aggregate(new List<NavBarItem>(), (listaTelas, type) =>
                        {
                            MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = Activator.CreateInstance(type) as MasterPanelBrowseDePesquisa;
                            if (!masterPanelBrowseDePesquisa.BrowseDePesquisa.Parametros.InformouParametro
                                && !String.IsNullOrEmpty(masterPanelBrowseDePesquisa.BrowseDePesquisa.TituloParaRelatorio)
                                )
                            {
                                masterPanelBrowseDePesquisa.Tag = type.FullName;
                                NavBarItem navBarItem = new NavBarItem();
                                navBarItem.Caption = masterPanelBrowseDePesquisa.BrowseDePesquisa.TituloParaRelatorio;
                                //navBarItem.Caption = masterPanelBrowseDePesquisa.BrowseDePesquisa.Caption;
                                navBarItem.Tag = masterPanelBrowseDePesquisa;
                                navBarItem.LinkClicked += new NavBarLinkEventHandler(navBarItem_LinkClicked);

                                DataRow dataRow = this.dtTblTblTreeList.NewRow();
                                if (criaNode)
                                {
                                    dataRow["ParentID"] = -1;
                                    dataRow["Titulo"] = itemModulo.Replace(".dll", "").Split('.').LastOrDefault();
                                    criaNode = false;
                                    this.dtTblTblTreeList.Rows.Add(dataRow);
                                    parentID = (int)dataRow["ID"];
                                }

                                dataRow = dtTblTblTreeList.NewRow();
                                dataRow["ParentID"] = parentID;
                                dataRow["Titulo"] = masterPanelBrowseDePesquisa.BrowseDePesquisa.TituloParaRelatorio;
                                dataRow["Pesquisa"] = masterPanelBrowseDePesquisa;
                                dataRow["Mostrou"] = false;

                                this.dtTblTblTreeList.Rows.Add(dataRow);
                                listaTelas.Add(navBarItem);
                            }
                            return listaTelas;
                        })
                        .ToArray();

                    if (listaNavBarItem.Count() > 0)
                    {
                        this.navBarControl1.Items.AddRange(listaNavBarItem);

                        NavBarGroup navBarGroup = new NavBarGroup(itemModulo.Replace(".dll", "").Split('.').LastOrDefault());
                        navBarGroup.ItemLinks.AddRange(listaNavBarItem
                            .Select(s => new NavBarItemLink(s))
                            .ToArray());

                        listaModulos.Add(navBarGroup);
                    }

                    return listaModulos;
                })
                .ToArray());

            treeList1.ExpandAll();
        }

        private void navBarItem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = e.Link.Item.Tag as MasterPanelBrowseDePesquisa;
            this.Text = this.Tag.ToString() + " - " + e.Link.Item.Caption;

            // Altera o ultimo item consultado "Bold e Red" para "Bold e Black"
            this.navBarControl1.Items
                .OfType<NavBarItem>()
                .Where(w => w.Appearance.ForeColor.Equals(Color.Red))
                .Update(u => u.Appearance.ForeColor = Color.Black);

            if (masterPanelBrowseDePesquisa != null)
            {
                if (!this.xtrTbCtrlPesquisas.Visible)
                {
                    this.xtrTbCtrlPesquisas.Visible = true;
                    this.elmntHstLogoGlobus.Child = null;
                }

                XtraTabPage xtraTabPage = this.xtrTbCtrlPesquisas.TabPages
                    .Where(w => w.Tag == masterPanelBrowseDePesquisa.Tag)
                    .FirstOrDefault();

                if (xtraTabPage == null)
                {
                    navBarControl1.Enabled = false;
                    xtraTabPage = new XtraTabPage();

                    xtraTabPage.Tag = masterPanelBrowseDePesquisa.Tag;
                    this.xtrTbCtrlPesquisas.TabPages.Add(xtraTabPage);

                    masterPanelBrowseDePesquisa.Dock = DockStyle.Fill;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.ButtonEditBGM = new ButtonEditBGM();

                    masterPanelBrowseDePesquisa.Visible = false;
                    xtraTabPage.Controls.Add(masterPanelBrowseDePesquisa);

                    xtraTabPage.Controls.Add(this.tblLytPnlAguarde);

                    CustomizarGridControlX customizarGridControl = new CustomizarGridControlX();
                    customizarGridControl.GridControl = masterPanelBrowseDePesquisa.BrowseDePesquisa;
                    customizarGridControl.TituloImpressao = masterPanelBrowseDePesquisa.BrowseDePesquisa.TituloParaRelatorio;
                    customizarGridControl.Dock = DockStyle.Left;
                    xtraTabPage.Controls.Add(customizarGridControl);

                    masterPanelBrowseDePesquisa.BrowseDePesquisa.OnPesquisar();

                    masterPanelBrowseDePesquisa.BringToFront();

                    masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.OptionsView.ShowFooter = true;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.UseEmbeddedNavigator = true;

                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.Append.Visible = false;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.Edit.Visible = false;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.Remove.Visible = false;

                    GridColumn gridColumn = masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.Columns
                        .OfType<GridColumn>()
                        .FirstOrDefault();

                    if (gridColumn != null)
                    {
                        if (gridColumn.SortOrder == DevExpress.Data.ColumnSortOrder.None)
                            gridColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

                        masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.OptionsBehavior.AutoExpandAllGroups = true;
                        masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.GroupSummary.Add(
                            new DevExpress.XtraGrid.GridGroupSummaryItem(
                                DevExpress.Data.SummaryItemType.Count,
                                gridColumn.FieldName,
                                gridColumn,
                                TraducaoDevExpress.LayoutQtde)
                            );
                    }
                    masterPanelBrowseDePesquisa.Visible = true;
                    xtraTabPage.Controls.Remove(tblLytPnlAguarde);

                }

                e.Link.Item.Appearance.Font = new Font(e.Link.Item.Appearance.Font, FontStyle.Bold);
                e.Link.Item.Appearance.ForeColor = Color.Red;
                this.xtrTbCtrlPesquisas.SelectedTabPage = xtraTabPage;
                navBarControl1.Enabled = true;

            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelControl1.Visible = radioGroup1.SelectedIndex == 0;
            pnlCtrlNavegacao.Visible = radioGroup1.SelectedIndex == 1;
        }


        private void rpstryItmBtnEdtTitulo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Dictionary<string, object> dictionary = RetornarTabPage(this.treeList1.FocusedNode);

            DataRowView dataRowView = dictionary["DataRow"] as DataRowView;
            MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = dictionary["Pesquisa"] as MasterPanelBrowseDePesquisa;

            if (masterPanelBrowseDePesquisa != null)
            {
                if (!this.xtrTbCtrlPesquisas.Visible)
                {
                    this.xtrTbCtrlPesquisas.Visible = true;
                    this.elmntHstLogoGlobus.Child = null;
                }

                XtraTabPage xtraTabPage = dictionary["XtraTabPage"] as XtraTabPage;

                if (xtraTabPage == null)
                {
                    this.treeList1.Enabled = false;
                    xtraTabPage = new XtraTabPage();

                    xtraTabPage.Tag = masterPanelBrowseDePesquisa.Tag;
                    this.xtrTbCtrlPesquisas.TabPages.Add(xtraTabPage);

                    masterPanelBrowseDePesquisa.Dock = DockStyle.Fill;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.ButtonEditBGM = new ButtonEditBGM();

                    masterPanelBrowseDePesquisa.Visible = false;
                    xtraTabPage.Controls.Add(masterPanelBrowseDePesquisa);

                    xtraTabPage.Controls.Add(this.tblLytPnlAguarde);

                    CustomizarGridControlX customizarGridControl = new CustomizarGridControlX();
                    customizarGridControl.GridControl = masterPanelBrowseDePesquisa.BrowseDePesquisa;
                    customizarGridControl.TituloImpressao = masterPanelBrowseDePesquisa.BrowseDePesquisa.TituloParaRelatorio;
                    customizarGridControl.Dock = DockStyle.Left;
                    xtraTabPage.Controls.Add(customizarGridControl);

                    masterPanelBrowseDePesquisa.BrowseDePesquisa.OnPesquisar();

                    masterPanelBrowseDePesquisa.BringToFront();

                    masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.OptionsView.ShowFooter = true;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.UseEmbeddedNavigator = true;

                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.Append.Visible = false;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.Edit.Visible = false;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.EmbeddedNavigator.Buttons.Remove.Visible = false;

                    GridColumn gridColumn = masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.Columns
                        .OfType<GridColumn>()
                        .FirstOrDefault();

                    if (gridColumn != null)
                    {
                        if (gridColumn.SortOrder == DevExpress.Data.ColumnSortOrder.None)
                            gridColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

                        masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.OptionsBehavior.AutoExpandAllGroups = true;
                        masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.GroupSummary.Add(
                            new DevExpress.XtraGrid.GridGroupSummaryItem(
                                DevExpress.Data.SummaryItemType.Count,
                                gridColumn.FieldName,
                                gridColumn,
                                TraducaoDevExpress.LayoutQtde)
                            );
                    }

                    dataRowView["Mostrou"] = true;
                    this.treeList1.RefreshDataSource();

                    masterPanelBrowseDePesquisa.Visible = true;
                    xtraTabPage.Controls.Remove(tblLytPnlAguarde);

                    this.treeList1.Enabled = true;
                }

                this.xtrTbCtrlPesquisas.SelectedTabPage = xtraTabPage;
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            this.xtrTbCtrlPesquisas.SelectedTabPage = RetornarTabPage(e.Node)["XtraTabPage"] as XtraTabPage;
        }

        private Dictionary<string, object> RetornarTabPage(TreeListNode treeListNode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            DataRowView dataRowView = treeList1.GetDataRecordByNode(treeListNode) as DataRowView;
            XtraTabPage xtraTabPage = null;
            MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = null;
            
            if (dataRowView != null)
                masterPanelBrowseDePesquisa = dataRowView["Pesquisa"] as MasterPanelBrowseDePesquisa;
                
            if (masterPanelBrowseDePesquisa != null)
            {
                this.Text = this.Tag.ToString() + " - " + dataRowView["Titulo"];

                xtraTabPage = this.xtrTbCtrlPesquisas.TabPages
                    .Where(w => w.Tag == masterPanelBrowseDePesquisa.Tag)
                    .FirstOrDefault();

                if (xtraTabPage == null)
                {
                    this.xtrTbCtrlPesquisas.Visible = false;
                    this.elmntHstLogoGlobus.Child = new UsrCtrlImagemMenuPrincipal();
                }
                else
                {
                    this.xtrTbCtrlPesquisas.Visible = true;
                    this.elmntHstLogoGlobus.Child = null;
                }
            }

            dictionary.Add("DataRow", dataRowView);
            dictionary.Add("XtraTabPage", xtraTabPage);
            dictionary.Add("Pesquisa", masterPanelBrowseDePesquisa);

            return dictionary;
        }


    }


}

