using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using FGlobus.Componentes.WinForms;
using DevExpress.Utils;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Drawing.Design;
using DevExpress.XtraEditors.Mask;
using System.Drawing;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Controle do tipo XtraEditor.CalcEdit com implementações exclusivas BGM.
    /// </summary>
    [ToolboxItem(true)]
    [Description("Controle do tipo XtraEditor.CalcEdit com implementações exclusivas BGM.")]
    [ToolboxBitmap(typeof(CalcEdit))]
    //[Docking(DockingBehavior.Ask)]
    [Designer(typeof(ControlaComponenteDesigner))]
    [PropertyTab(typeof(BotaoBrowserPropriedades), PropertyTabScope.Document)]
    [DefaultProperty("TipoMascara")]
    //[DefaultEvent("ClickBotao")]
    [InformacoesComponente("Henrique Machado", "1.0.0", "mhtml:http://tfsserver/sites/BGMRodotec/Globus/Desenvolvimento/Manuais/IdentidadeVisualGlb_Htm.mht#_Toc325727336")]
    public partial class CalcEditBGM : CalcEdit
    {
        #region Constructor

        /// <summary>
        /// Construtor default CalcEditBGM em tempo de desenvovimento.
        /// </summary>
        public CalcEditBGM()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Construtor default CalcEditBGM em tempo de execução.
        /// </summary>
        /// <param name="container"></param>
        public CalcEditBGM(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Atributtes

        private DXErrorProvider _dXErrorProvider = new DXErrorProvider();

        private ClassMascaras _classMascaras = new ClassMascaras();
        private decimal _valorMinimo = 0;
        private decimal _valorMaximo = 0;
        private int _casasDecimais = 2;

        #endregion

        #region Properties

        #region TipoMascara

        /// <summary>
        /// (Set\Get) Informa ou retorna o tipo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Tipo de mascara")]
        [Description("Informa ou retorna o tipo.\nNome: TipoMascara")]
        [EditorAttribute(typeof(TipoMascarasEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ClassMascaras TipoMascara
        {
            get
            {
                return _classMascaras;
            }
            set
            {
                _classMascaras = value;
                MascaraCasasDecimais();
            }
        }

        #endregion

        #region CasasDecimais

        /// <summary>
        /// (Set\Get) Informa ou retorna o ValorMaximo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("CasasDecimais")]
        [Description("Informa ou retorna o mínimo.\nNome: CasasDecimais")]
        [DefaultValue(2)]
        public int CasasDecimais
        {
            get
            {
                return this._casasDecimais;
            }
            set
            {
                this._casasDecimais = value;
                MascaraCasasDecimais();
            }
        }

        #endregion

        #region ValorMinimo

        /// <summary>
        /// (Set\Get) Informa ou retorna o ValorMinimo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("ValorMinimo")]
        [Description("Informa ou retorna o mínimo.\nNome: ValorMinimo")]
        public decimal ValorMinimo
        {
            get
            {
                return this._valorMinimo;
            }
            set
            {
                this._valorMinimo = value;
            }
        }

        #endregion

        #region ValorMaximo

        /// <summary>
        /// (Set\Get) Informa ou retorna o ValorMaximo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("ValorMaximo")]
        [Description("Informa ou retorna o máximo.\nNome: ValorMaximo")]
        public decimal ValorMaximo
        {
            get
            {
                return this._valorMaximo;
            }
            set
            {
                this._valorMaximo = value;
            }
        }

        #endregion

        #endregion

        #region Override

        protected override void OnLoaded()
        {
            base.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            base.Properties.Buttons[0].Image = global::FGlobus.Componentes.WinForms.Resources.ImagensBGM.Calculadora;
            base.Properties.Buttons[0].Shortcut = new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.F4));
            base.Properties.Buttons[0].SuperTip = FGlobus.Util.Util.CriarSuperToolTip("Calculadora");

            MascaraCasasDecimais();

            base.OnLoaded();
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            this._dXErrorProvider.ClearErrors();

            if (String.IsNullOrEmpty(this.Text) || String.IsNullOrEmpty(this.EditValue.ToString()))
                this.EditValue = _valorMinimo;

            if (Convert.ToDecimal(this.EditValue) > this._valorMaximo || Convert.ToDecimal(this.EditValue) < this._valorMinimo)
            {
                this._dXErrorProvider.SetError(
                    this,
                    String.Concat("Valor deve estar entre ",
                        this._valorMinimo.ToString(), " e ",
                        this._valorMaximo.ToString()),
                    ErrorType.Critical);
                this.Text = "";
                this.Focus();
            }
            else
                base.OnValidating(e);
        }

        #endregion

        #region Methods

        void MascaraCasasDecimais()
        {
            if (this._classMascaras != null && !String.IsNullOrEmpty(this._classMascaras.Tipo))
            {
                base.Properties.MaxLength = Convert.ToInt32(Math.Truncate(this._valorMaximo));


                MaskProperties _maskProperties = new MaskProperties();

                _maskProperties.UseMaskAsDisplayFormat = true;
                _maskProperties.MaskType = _classMascaras.MaskType;
                
                _maskProperties.EditMask = _classMascaras.Codigo.Equals(1)
                    ? this._classMascaras.EditMask
                    : String.Concat(this._classMascaras.EditMask, this._casasDecimais.ToString());

                base.Properties.Mask.Assign(_maskProperties);
            }
        }

        #endregion
    }
}