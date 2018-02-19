using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
//using System.Web.Services.Protocols;
using FGlobus.Util;
using FGlobus.Componentes.WinForms;
using System.Drawing.Design;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Windows.Forms;
using DevExpress.XtraEditors.Mask;
using System.Drawing;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Controle do tipo XtraEditor.TextEdit com implementações exclusívas BGM.
    /// </summary>    
    [ToolboxItem(true)]
    [Description("Controle do tipo XtraEditor.TextEdit com implementações exclusívas BGM.")]
    [ToolboxBitmap(typeof(TextEdit))]
    //[Docking(DockingBehavior.Ask)]
    [Designer(typeof(ControlaComponenteDesigner))]
    [PropertyTab(typeof(BotaoBrowserPropriedades), PropertyTabScope.Document)]
    [DefaultProperty("TipoMascara")]
    //[DefaultEvent("ClickBotao")]
    [InformacoesComponente("Henrique Machado", "1.0.0", "mhtml:http://tfsserver/sites/BGMRodotec/Globus/Desenvolvimento/Manuais/IdentidadeVisualGlb_Htm.mht#_Toc325727335")]
    public partial class TextEditBGM : TextEdit
    {
        #region Constructor

        /// <summary>
        /// Construtor default TextEditBGM em tempo de desenvovimento.
        /// </summary>
        public TextEditBGM()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Construtor default TextEditBGM em tempo de execução.
        /// </summary>
        /// <param name="container"></param>
        public TextEditBGM(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Atributtes

        private DXErrorProvider _dXErrorProvider = new DXErrorProvider();

        private ClassMascaras _classMascaras = new ClassMascaras();
        private int _quantidadeNumeros;

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
                return this._classMascaras;
            }
            set
            {
                this._classMascaras = value;
                ValidaMascara();
            }
        }

        #endregion

        #region QuantidadeNumeros

        /// <summary>
        /// (Set\Get) Informa ou retorna o tipo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("QuantidadeNumeros")]
        [Description("Informa ou retorna o tipo.\nNome: QuantidadeNumeros")]
        [DefaultValue(0)]
        public int QuantidadeNumeros
        {
            get
            {
                return this._quantidadeNumeros;
            }
            set
            {
                this._quantidadeNumeros = value;
                ValidaMascara();
            }
        }

        #endregion

        #endregion

        #region Override

        protected override void OnLoaded()
        {
            ValidaMascara();

            base.OnLoaded();
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            this._dXErrorProvider.ClearErrors();

            if (_classMascaras.Codigo.Equals(23) && !String.IsNullOrEmpty(base.Text))  // E-mail
            {
                if (this.Text.IndexOf("@").Equals(-1))
                {
                    this._dXErrorProvider.SetError(this, "Entre com um e-mail válido", ErrorType.Critical);
                    this.Focus();
                }
            }
            else
                base.OnValidating(e);
        }

        #endregion

        #region Methods

        void ValidaMascara()
        {
            if (this._classMascaras != null && !String.IsNullOrEmpty(this._classMascaras.Tipo))
            {
                base.Properties.PasswordChar = char.MinValue;                
                MaskProperties _maskProperties = new MaskProperties();

                _maskProperties.UseMaskAsDisplayFormat = true;
                _maskProperties.EditMask = this._classMascaras.EditMask;
                _maskProperties.MaskType = this._classMascaras.MaskType;
                _maskProperties.PlaceHolder = '_';


                if (this._classMascaras.Codigo.Equals(23))  // E-mail
                {
                    _maskProperties.PlaceHolder = this._classMascaras.PlaceHolder;
                }
                else if (this._classMascaras.Codigo.Equals(24))  // Senha
                {
                    base.Properties.PasswordChar = this._classMascaras.PasswordChar;
                }
                else if (this._classMascaras.Codigo.Equals(25))  // Número
                {
                    _maskProperties.EditMask = String.Concat("\\d{0,", this._quantidadeNumeros.ToString(), "}");
                    base.Properties.MaxLength = this._quantidadeNumeros;
                }
                else if (this._classMascaras.Codigo.Equals(26)) //Classificador
                {
                    _maskProperties.EditMask = String.Concat("([0-9]|[.]){0,", this._quantidadeNumeros.ToString(), "}");
                    base.Properties.MaxLength = this._quantidadeNumeros;
                }
                else
                    this._quantidadeNumeros = 0;
                

                base.Properties.Mask.Assign(_maskProperties);
            }
        }

        #endregion
    }
}
