using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Drawing.Design;
using DevExpress.XtraEditors.Mask;
using DevExpress.Utils;
using System.Drawing;
using DevExpress.XtraEditors.DXErrorProvider;
using FGlobus.Componentes.WinForms.Mascaras;
using System.Windows.Forms;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Controle do tipo XtraEditor.DateEdit com implementações exclusivas BGM.
    /// </summary>    
    [ToolboxItem(true)]
    [Description("Controle do tipo XtraEditor.DateEdit com implementações exclusivas BGM.")]
    [ToolboxBitmap(typeof(DateEdit))]
    //[Docking(DockingBehavior.Ask)]
    [Designer(typeof(ControlaComponenteDesigner))]
    [PropertyTab(typeof(BotaoBrowserPropriedades), PropertyTabScope.Document)]
    [DefaultProperty("TipoMascara")]
    //[DefaultEvent("ClickBotao")]
    [InformacoesComponente("Henrique Machado", "1.0.0", "mhtml:http://tfsserver/sites/BGMRodotec/Globus/Desenvolvimento/Manuais/IdentidadeVisualGlb_Htm.mht#_Toc325727337")]
    public partial class DateEditBGM : DateEdit
    {
        #region Constructor

        public DateEditBGM()
        {
            InitializeComponent();
            
            if ((_classMascaras.Controles != null) && (_classMascaras.Controles.Count() > 0))
                this._mascaraData = _classMascaras.Controles
                    .Where(w => w.Equals(ClassMascaras.TiposControles.DateEdit))
                    .Count() > 0;
        }

        public DateEditBGM(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            
            if ((_classMascaras.Controles != null) && (_classMascaras.Controles.Count() > 0))
                this._mascaraData = _classMascaras.Controles
                    .Where(w => w.Equals(ClassMascaras.TiposControles.DateEdit))
                    .Count() > 0;
        }

        #endregion

        #region Atributtes

        private ClassMascaras _classMascaras = new ClassMascaras();
        private bool _aceitaVazio = false;
        private bool _mascaraData = false;
        private ClassDateEditBGM _dataProgressiva = new ClassDateEditBGM();
        private ClassDateEditBGM _dataRetroativa = new ClassDateEditBGM();
        private MasterForm _masterFormAssociado;
        
        private string _textValidating = String.Empty;
        private object _editValueValidating = null;

        #endregion

        #region Properties

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
                return this._classMascaras;
            }
            set
            {
                this._classMascaras = value;

                ValidaMascara();
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna o tipo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Aceita vazio")]
        [Description("Informa ou retorna se o campo pode ficar vazio.\nNome: AceitaVazio")]
        [DefaultValue(false)]
        public bool AceitaVazio
        {
            get
            {
                return this._aceitaVazio;
            }
            set
            {
                this._aceitaVazio = value;

                ValidaMascara();
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna o tipo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Data progressiva")]
        [Description("Informa ou retorna informações sobre data progressiva.\nNome: DataProgressiva")]
        public ClassDateEditBGM DataProgressiva
        {
            get
            {
                return this._dataProgressiva;
            }
            set
            {
                this._dataProgressiva = value;
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna o tipo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Data retroativa")]
        [Description("Informa ou retorna informações sobre data progressiva.\nNome: DataRetroativa")]
        public ClassDateEditBGM DataRetroativa
        {
            get
            {
                return this._dataRetroativa;
            }
            set
            {
                this._dataRetroativa = value;
            }
        }

        #endregion

        #region Override

        protected override void OnLoaded()
        {
            SuperToolTip _superToolTip = 
                new SuperToolTip();

            _superToolTip.Items.Add(
                new ToolTipTitleItem()
                {
                    Text = "Pressione..."
                });

            _superToolTip.Items.Add(
                new ToolTipItem()
                {
                    Text = String.Concat(
                        "Hoje  ( T )\r\n",
                        "Ontem  ( R )\r\n",
                        "Amanhã  ( Y )\r\n",
                        "Primeiro dia do mês atual  ( PageUp )\r\n",
                        "Último dia do mês atual  ( PageDown )\r\n",
                        "Acresce um dia  ( + )\r\n",
                        "Diminui um dia  ( - )")
                });

            base.SuperTip = _superToolTip;

            base.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            base.Properties.Buttons[0].Image = global::FGlobus.Componentes.WinForms.Resources.ImagensBGM.Calendario;
            base.Properties.Buttons[0].Shortcut = new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.F4));
            base.Properties.Buttons[0].SuperTip = FGlobus.Util.Util.CriarSuperToolTip("Calendário");

            ValidaMascara();

            base.OnLoaded();
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            this.BackColor = Color.White;
            this.Properties.AppearanceDisabled.BackColor = Color.FromArgb(224, 224, 224);

            this.ErrorText = String.Empty;          
        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            DateTime _primeiroDiaDoMes = Convert.ToDateTime("01/" + DateTime.Now.ToString("MM/yyyy"));
            DateTime _ultimoDiaDoMes = _primeiroDiaDoMes.AddMonths(1).AddDays(-1);

            switch (e.KeyValue)
            {
                case 82: // R - Ontem
                    {
                        this.EditValue = DateTime.Now.AddDays(-1);
                        this.SelectAll();
                        
                        break;
                    }
                case 84: // T - Hoje
                    {
                        this.EditValue = DateTime.Now;
                        this.SelectAll();

                        break;
                    }
                case 89: // Y - Amanhã
                    {
                        this.EditValue = DateTime.Now.AddDays(+1);
                        this.SelectAll();

                        break;
                    }
                case 33: // PageUp - Primeiro dia do mês atual
                    {
                        this.EditValue = _primeiroDiaDoMes;
                        this.SelectAll();

                        break;
                    }
                case 34: // PageDown - Último dia do mês atual
                    {
                        this.EditValue = _ultimoDiaDoMes;
                        this.SelectAll();

                        break;
                    }
                case 107: // + - Acresce um dia
                    {
                        if ((this.EditValue == null) || (String.IsNullOrEmpty(this.EditValue.ToString())))
                            this.EditValue = DateTime.Now;

                        this.EditValue = ((DateTime)this.EditValue).AddDays(+1);
                        this.SelectAll();

                        break;
                    }
                case 109: // - - Diminui um dia
                    {
                        if ((this.EditValue == null) || (String.IsNullOrEmpty(this.EditValue.ToString())))
                            this.EditValue = DateTime.Now;

                        this.EditValue = ((DateTime)this.EditValue).AddDays(-1);
                        this.SelectAll();

                        break;
                    }
            }

            base.OnKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if ((String.IsNullOrEmpty(this.Text)) && (this.EditValue == null))
            {
                this.BackColor = Color.White;
                this.Properties.AppearanceDisabled.BackColor = Color.FromArgb(224, 224, 224);
            }

            base.OnTextChanged(e);
        }

        #endregion

        #region Methods

        private void ValidaMascara()
        {
            if (this._classMascaras != null && !String.IsNullOrEmpty(this._classMascaras.Tipo))
            {
                base.Properties.DisplayFormat.FormatString = String.Empty;
                base.Properties.DisplayFormat.FormatType = FormatType.None;

                base.Properties.EditFormat.FormatString = String.Empty;
                base.Properties.EditFormat.FormatType = FormatType.None;


                MaskProperties _maskProperties = new MaskProperties();

                _maskProperties.UseMaskAsDisplayFormat = true;
                _maskProperties.EditMask = this._classMascaras.EditMask;
                _maskProperties.MaskType = this._classMascaras.MaskType;

                base.Properties.Mask.Assign(_maskProperties);
            }
        }

        private bool VerificarDataProgressivaOuRetroativa(eTipoDateEditBGM _eTipoDateEditBGM, ClassDateEditBGM _classDateEditBGM)
        {
            TimeSpan _timeSpan;
            bool _retorno = true;
            
            bool _validacao = _eTipoDateEditBGM.Equals(eTipoDateEditBGM.Progressiva)
                ? ((this.DateTime > DateTime.Now) &&
                    (_classDateEditBGM.Ativo) &&
                    (!_classDateEditBGM.PassarAQuantidadeDeDias.Equals(eDateEditBGM.Permitir)))  // Progressiva
                : ((this.DateTime < DateTime.Now) &&
                    (_classDateEditBGM.Ativo) &&
                    (!_classDateEditBGM.PassarAQuantidadeDeDias.Equals(eDateEditBGM.Permitir)));  // Retroativa


            if (_validacao)
            {
                _timeSpan = _eTipoDateEditBGM.Equals(eTipoDateEditBGM.Progressiva)
                    ? this.DateTime.Date.Subtract(DateTime.Now.Date)
                    : DateTime.Now.Date.Subtract(this.DateTime.Date);

                if (_timeSpan.Days > _classDateEditBGM.QuantidadeDias)
                {
                    this.ErrorText = String.IsNullOrEmpty(_classDateEditBGM.MensagemAviso)
                        ? "Data inválida"
                        : _classDateEditBGM.MensagemAviso;                    

                    switch (_classDateEditBGM.PassarAQuantidadeDeDias)
                    {
                        case eDateEditBGM.Alertar:
                            {
                                this.BackColor = Color.PaleGoldenrod;
                                this.Properties.AppearanceDisabled.BackColor = Color.PaleGoldenrod;
                                
                                break;
                            }
                        case eDateEditBGM.Bloquear:
                            {
                                _retorno = false;

                                this.SelectAll();

                                this.BackColor = Color.LightCoral;
                                this.Properties.AppearanceDisabled.BackColor = Color.LightCoral;
                                
                                break;
                            }
                    }
                }
            }

            return _retorno;
        }

        #endregion
    }
    
}
