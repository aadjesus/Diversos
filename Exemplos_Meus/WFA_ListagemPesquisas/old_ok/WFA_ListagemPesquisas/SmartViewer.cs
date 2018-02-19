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
using DevExpress.Utils.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;

namespace WFA_ListagemPesquisas
{
    public partial class SmartViewer : MasterFormGlobus
    {
        #region Contrutor

        /// <summary>
        /// Construtor default
        /// </summary>
        public SmartViewer()
        {
            InitializeComponent();

            string nomePadraoDll = "BgmRodotec.Globus5.Pesquisa";

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

            this.xtrTbCtrlSmartViewer.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.trLstSmartViewer.Nodes.Clear();

            arquivos
                 .ToList()
                 .ForEach(f =>
                 {
                     DataRow dataRow = this.dtTblTblTreeList.NewRow();
                     dataRow["Titulo"] = f.Replace(".dll", "").Split('.').LastOrDefault();
                     dataRow["Arquivo"] = f;
                     this.dtTblTblTreeList.Rows.Add(dataRow);
                 });

            this.trLstSmartViewer.GetCustomSummaryValue += delegate(object senderGetCustomSummaryValue, DevExpress.XtraTreeList.GetCustomSummaryValueEventArgs eGetCustomSummaryValue)
            {
                if (eGetCustomSummaryValue.IsSummaryFooter)
                {
                    Func<bool, int> delegateQtde = delegate(bool nodes)
                    {
                        return dtTblTblTreeList.Rows
                            .OfType<DataRow>()
                            .Where(w => nodes
                                    ? !(w["Pesquisa"] is Type)
                                    : w["Pesquisa"] is Type)
                        .Count();
                    };
                    eGetCustomSummaryValue.CustomValue = String.Concat(delegateQtde(true), " / ", delegateQtde(false));
                }
            };
        }

        #endregion

        #region Metodos Tela

        private void trLstSmartViewer_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DataRowView dataRowView = this.trLstSmartViewer.GetDataRecordByNode(e.Node) as DataRowView;

            if (dataRowView != null)
            {
                XtraTabPage xtraTabPage = this.xtrTbCtrlSmartViewer.TabPages
                    .Where(w => w.Tag is DataRowView && w.Tag.Equals(dataRowView))
                    .FirstOrDefault();

                this.xtrTbCtrlSmartViewer.SelectedTabPage = xtraTabPage == null
                    ? this.xtrTbPgInicial
                    : Convert.ToInt32(dataRowView["Imagem"]).Equals(5)
                        ? this.xtrTbPgAgarde
                        : xtraTabPage;
            }
        }

        private void trLstSmartViewer_DoubleClick(object sender, EventArgs e)
        {
            DataRowView dataRowView = this.trLstSmartViewer.GetDataRecordByNode(this.trLstSmartViewer.FocusedNode) as DataRowView;
            if (dataRowView != null)
            {
                // Se o "Pesquisa" for = Type e que clicou no no sub item
                if (dataRowView["Pesquisa"] is Type)
                {
                    // Procura TabPage
                    XtraTabPage xtraTabPage = this.xtrTbCtrlSmartViewer.TabPages
                        .Where(w => w.Tag != null && w.Tag.Equals(dataRowView))
                        .FirstOrDefault();

                    if (xtraTabPage == null)
                        CriaMetodosBackgroundWorker(dataRowView);
                    else
                        this.xtrTbCtrlSmartViewer.SelectedTabPage = xtraTabPage;
                }

                // Se a "Imagem" for = 0 e que o nivel ainda ñ foi aberto
                if (Convert.ToInt32(dataRowView["Imagem"]).Equals(0))
                {
                    int imagem = 2;
                    Assembly.LoadFrom(dataRowView["Arquivo"].ToString()).GetTypes()
                        .Where(w => w.BaseType.Equals(typeof(MasterPanelBrowseDePesquisa)))
                        .ToList()
                        .ForEach(f2 =>
                        {
                            try
                            {
                                MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = Activator.CreateInstance(f2) as MasterPanelBrowseDePesquisa;
                                if (!masterPanelBrowseDePesquisa.BrowseDePesquisa.Parametros.InformouParametro &&
                                    !String.IsNullOrEmpty(masterPanelBrowseDePesquisa.BrowseDePesquisa.TituloParaRelatorio))
                                {
                                    DataRow dataRow = dtTblTblTreeList.NewRow();
                                    dataRow["ParentTitulo"] = dataRowView["Titulo"];
                                    dataRow["Titulo"] = masterPanelBrowseDePesquisa.BrowseDePesquisa.TituloParaRelatorio;
                                    dataRow["Pesquisa"] = f2;
                                    dataRow["Imagem"] = 3;

                                    this.dtTblTblTreeList.Rows.Add(dataRow);
                                    imagem = 1;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        });

                    dataRowView["Imagem"] = imagem;
                    this.trLstSmartViewer.RefreshDataSource();
                    this.trLstSmartViewer.FocusedNode.ExpandAll();
                }
            }
        }

        private void SmartViewer_Load(object sender, EventArgs e)
        {
            this.spltContCtrlSmartViewer.Panel2.MinSize = this.xtrTbPgAgarde.Width;
        }

        private void trLstSmartViewer_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if (e.Node.GetValue("Imagem") != null)
                e.NodeImageIndex = (int)e.Node.GetValue("Imagem");
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            TreeList treeList = e.SelectedControl as TreeList;
            if (treeList != null)
            {
                TreeListHitInfo treeListHitInfo = treeList.CalcHitInfo(e.ControlMousePosition);
                if (treeListHitInfo.HitInfoType == HitInfoType.Cell)
                {
                    DataRowView dataRowView = this.trLstSmartViewer.GetDataRecordByNode(treeListHitInfo.Node) as DataRowView;
                    if (dataRowView != null)
                        e.Info = new DevExpress.Utils.ToolTipControlInfo(
                            new TreeListCellToolTipInfo(treeListHitInfo.Node, treeListHitInfo.Column, null),
                            this.rpstryItmImgCmbBxImagem.Items.GetItem(dataRowView["Imagem"]).Description);
                }
            }
        }

        private void Teste_NomeMetodo()
        {

        }

        #endregion

        #region Metodos

        private void CriaMetodosBackgroundWorker(DataRowView dtRwVw)
        {
            XtraTabPage xtraTabPageAtual = new XtraTabPage();
            xtraTabPageAtual.Tag = dtRwVw;
            this.xtrTbCtrlSmartViewer.TabPages.Add(xtraTabPageAtual);
            this.xtrTbCtrlSmartViewer.SelectedTabPage = this.xtrTbPgAgarde;

            BackgroundWorker backgroundWorker = new BackgroundWorker();

            #region DoWork

            backgroundWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                XtraTabPage xtraTabPage = e.Argument as XtraTabPage;
                if (xtraTabPage != null)
                {
                    DataRowView dataRowView = xtraTabPage.Tag as DataRowView;
                    if (dataRowView != null)
                    {
                        Type type = dataRowView["Pesquisa"] as Type;
                        dataRowView["Imagem"] = 5;
                        BackgroundWorker backgroundWorkerEmExec = sender as BackgroundWorker;
                        backgroundWorkerEmExec.ReportProgress(1);

                        MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = Activator.CreateInstance(type) as MasterPanelBrowseDePesquisa;
                        masterPanelBrowseDePesquisa.BrowseDePesquisa.OnPesquisar();

                        xtraTabPage.Controls.Add(masterPanelBrowseDePesquisa);

                        e.Result = xtraTabPage;
                    }
                }
            };

            #endregion

            #region RunWorkerCompleted

            backgroundWorker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
            {
                XtraTabPage xtraTabPage = e.Result as XtraTabPage;

                MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = xtraTabPage.Controls
                    .OfType<MasterPanelBrowseDePesquisa>()
                    .FirstOrDefault();

                if (masterPanelBrowseDePesquisa != null)
                {
                    CustomizarGridControlX customizarGridControl = new CustomizarGridControlX();
                    customizarGridControl.GridControl = masterPanelBrowseDePesquisa.BrowseDePesquisa;
                    customizarGridControl.TituloImpressao = masterPanelBrowseDePesquisa.BrowseDePesquisa.TituloParaRelatorio;
                    customizarGridControl.Dock = DockStyle.Left;

                    masterPanelBrowseDePesquisa.Dock = DockStyle.Fill;
                    masterPanelBrowseDePesquisa.BrowseDePesquisa.ButtonEditBGM = new ButtonEditBGM();
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
                                TraducaoDevExpress.LayoutQtde));
                    }

                    DataRowView dataRowView = xtraTabPage.Tag as DataRowView;
                    dataRowView["Imagem"] = masterPanelBrowseDePesquisa.BrowseDePesquisa.MainView.RowCount.Equals(0)
                        ? 6
                        : 4;
                    this.trLstSmartViewer.RefreshDataSource();

                    xtraTabPage.Controls.Add(customizarGridControl);
                    masterPanelBrowseDePesquisa.BringToFront();
                    this.xtrTbCtrlSmartViewer.SelectedTabPage = xtraTabPage;
                }
            };

            #endregion

            #region ProgressChanged

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e)
            {
                this.trLstSmartViewer.RefreshDataSource();
            };

            #endregion

            if (Convert.ToInt32(dtRwVw["Imagem"]).Equals(3))
                backgroundWorker.RunWorkerAsync(xtraTabPageAtual);
        }

        #endregion
    }
}

