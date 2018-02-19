using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using FGlobus.Excecao;
using System.Data;

namespace FGlobus.Relatorios
{
    /// <summary>
    /// Master form para TODOS relatorios
    /// </summary>
    public partial class MasterRelatorio : DevExpress.XtraReports.UI.XtraReport
    {
        #region Construtores

        public MasterRelatorio()
        {
            InitializeComponent();
        }

        #endregion

        #region Attributes

        private string _nomeRelatorio = "Informe o nome do relatório";
        private bool _modoGerencial = false;
        private bool _imprimeLogo = false;
        private bool _paisagem = false;
        private Image _logoCliente = null;
        private List<string> _parametros = new List<string>();
        private bool _first = true;
        private bool _verificaDataSource = false;
        private bool _imprimeRodape = true;

        #endregion

        #region Override

        public object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;

                if (_verificaDataSource)
                {
                    bool bataTable = base.DataSource is DataTable;
                    if (base.DataSource == null ||
                        ((bataTable && ((DataTable)base.DataSource).Rows.Count == 0) ||
                         (!bataTable && ((object[])base.DataSource).Length == 0)))
                    {
                        throw new BOExcecao(BOExcecao.NenhumaInfParaOsDadosInfo);
                    }
                }
                _verificaDataSource = true;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// (Get/Set) Informa ou retorna o nome do relatório.
        /// </summary>
        [Category(FGlobus.Util.Util.Categoria), Browsable(true)]
        [Description("(Get/Set) Informa ou retorna o nome do relatório.")]
        public string NomeRelatorio
        {
            get
            {
                return _nomeRelatorio;
            }
            set
            {
                _nomeRelatorio = value;
                this.mstRelNomeGerencial.Text = _nomeRelatorio;
                this.mstRelNomeOperacional.Text = _nomeRelatorio;
            }
        }

        [Category(FGlobus.Util.Util.Categoria), Browsable(false)]
        public List<string> Parametros
        {
            get
            {
                return _parametros;
            }
            set
            {
                _parametros = value;
            }
        }

        [Category(FGlobus.Util.Util.Categoria), Browsable(false)]
        public bool ModoGerencial
        {
            get
            {
                return _modoGerencial;
            }
            set
            {
                _modoGerencial = value;
            }
        }

        [Category(FGlobus.Util.Util.Categoria), Browsable(false)]
        public bool ImprimeLogo
        {
            get
            {
                return _imprimeLogo;
            }
            set
            {
                _imprimeLogo = value;
            }
        }

        [Category(FGlobus.Util.Util.Categoria), Browsable(false)]
        public bool Paisagem
        {
            get
            {
                return _paisagem;
            }
            set
            {
                _paisagem = value;
            }
        }

        [Category(FGlobus.Util.Util.Categoria), Browsable(false)]
        public Image LogoCliente
        {
            get
            {
                return _logoCliente;
            }
            set
            {
                _logoCliente = value;
            }
        }

        /// <summary>
        /// (Get/Set) Informa ou retorna se imprime o rodapé da página.
        /// </summary>
        [Category(FGlobus.Util.Util.Categoria), Browsable(true)]
        [Description("(Get/Set) Informa ou retorna se imprime o rodapé da página.")]
        public bool ImprimeRodape
        {
            get { return _imprimeRodape; }
            set
            {
                _imprimeRodape = value;
                this.mstRelPageFooter.Visible = _imprimeRodape;
                this.mstRelBottomMargin.Visible = _imprimeRodape;
            }
        }

        #endregion

        #region Metodos

        protected override void BeforeReportPrint()
        {
            base.BeforeReportPrint();

            if (_first)
            {
                _first = false;
                LeOsGroupHeaderDoRelatorio();
                PopularCampos();
            }
        }

        private void celulas_PreviewDoubleClick(object sender, PreviewMouseEventArgs e)
        {
            // Identifica a band q a celula esta localizada
            GroupHeaderBand grpHdBandSelec = (GroupHeaderBand)((((XRTableCell)sender).Parent.Parent.Parent));

            // Cria lista com as bandas que o Level seja menor que o Level da band selecionado
            GroupBand[] grpHdBands = this.Bands
                .OfType<GroupBand>()
                .Where(w => w.Level < grpHdBandSelec.Level)
                .OrderByDescending(o => o.Level)
                .ToArray();

            if (grpHdBands.Length > 0)
            {
                for (int i = 0; i < grpHdBands.Length; i++)
                    grpHdBands[i].Visible = i == 0 ? !grpHdBands[i].Visible : grpHdBands[0].Visible;
                this.Detail.Visible = grpHdBands[0].Visible;
            }
            else
                this.Detail.Visible = !this.Detail.Visible;

            this.CreateDocument();
        }

        private void PopularCampos()
        {
            this.mstRelNomeGerencial.Text = _nomeRelatorio;
            this.mstRelNomeOperacional.Text = _nomeRelatorio;
            this.mstRelTopMargin.Visible = _modoGerencial;
            this.mstRelPageHeaderOperacional.Visible = !this.mstRelTopMargin.Visible;
            /*
            Parametros.Add("Linha 1");
            Parametros.Add("Linha 2");
            Parametros.Add("Linha 3");
            */
            this.mstRelParam.Lines = _parametros.ToArray();
            this.mstRelParamGerencial.Lines = this.mstRelParam.Lines;

            if (this.mstRelTopMargin.Visible && _parametros.Count.Equals(0))
            {
                this.mstRelTopMargin.HeightF = this.mstRelTopMargin.HeightF - this.mstRelParamGerencial.HeightF;
                this.mstRelParamGerencial.Visible = false;
            }

            if (this.mstRelPageHeaderOperacional.Visible && _parametros.Count.Equals(0))
            {
                this.mstRelPageHeaderOperacional.HeightF = this.mstRelPageHeaderOperacional.HeightF - this.mstRelParam.HeightF;
                this.mstRelParam.Visible = false;
            }

            this.mstRelLogoBgm.Visible = _imprimeLogo;
            this.mstRelLogoCliente.Visible = _imprimeLogo && _logoCliente != null;
            if (this.mstRelLogoCliente.Visible)
                this.mstRelLogoCliente.Image = _logoCliente;

        }

        /// <summary>
        /// Inclui no "GroupHeaderBand" Duplo click para abrir e fecar o nivel, e altera a fonte para negrito;
        /// </summary>
        private void LeOsGroupHeaderDoRelatorio()
        {
            // Le as "Bands" do relatório separando somente os "GroupHeaderBand"
            foreach (var grupoHd in this.Bands.OfType<GroupHeaderBand>())
            {
                try
                {
                    // Cria lista com as celulas da banda
                    var celulasTabela = grupoHd.Controls
                        .OfType<XRTable>()
                        .First()
                        .OfType<XRTableRow>()
                        .First()
                        .OfType<XRTableCell>();

                    // Verifica se existe alguma célula com a fonte Sublinhada
                    if (celulasTabela.Where(w => w.Font.Underline).Count() > 0)
                        foreach (var celula in celulasTabela)
                        {
                            celula.PreviewDoubleClick += new PreviewMouseEventHandler(celulas_PreviewDoubleClick);
                            celula.Font = new Font("Arial", celula.Font.Size, celula.Font.Style | FontStyle.Bold);
                        }
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion
    }
}
