using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Controle do tipo xTraEditor.LabelControl com implementações exclusívas BGM.
    /// </summary>    
    [ToolboxItem(true)]
    [Description("Controle do tipo LabelControl com implementações exclusívas BGM.")]
    [ToolboxBitmap(typeof(LabelControlBGM), "LabelControl.LabelControlBGM.ico")]
    //[Docking(DockingBehavior.Ask)]    
    [Designer(typeof(ControlaComponenteDesigner))]
    [PropertyTab(typeof(BotaoBrowserPropriedades), PropertyTabScope.Document)]
    [DefaultProperty("Tipo")]
    //[DefaultEvent("Click")]
    [InformacoesComponente("Alessandro Augusto", "1.0.0", "mhtml:http://tfsserver/sites/BGMRodotec/Globus/Desenvolvimento/Manuais/IdentidadeVisualGlb_Htm.mht#_Toc325727330")]
    public partial class LabelControlBGM : LabelControl
    {
        #region Construtos

        /// <summary>
        /// Construtor default <see cref="LabelControlBGM"/> em tempo de desenvovimento.
        /// </summary>
        public LabelControlBGM()
        {
            InitializeComponent();
            _corDefault = base.Appearance.ForeColor;
            _fontDefault = base.Font;
            _tipo = eTipo.Default;
            _controle = null;
            _controleBotoes = true;
        }

        /// <summary>
        /// Construtor default <see cref="LabelControlBGM"/>, em tempo de execução.
        /// </summary>
        /// <param name="container">Objeto do tipo IContainer.</param>
        public LabelControlBGM(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Tipo do label.
        /// </summary>
        public enum eTipo
        {
            /// <summary>
            /// Default.
            /// </summary>
            Default,
            /// <summary>
            /// Chave.
            /// </summary>
            Chave,
            /// <summary>
            /// Obrigatorio.
            /// </summary>
            Obrigatorio
        }

        private Color _corChave = Color.Blue;
        private Color _corObrigatorio = Color.Black;
        private Font _font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private bool _desabilita = false;
        private bool _controleBotoes = true;

        private Color _corDefault;
        private Font _fontDefault;
        private eTipo _tipo = new eTipo();
        private BaseEdit _controle;
        private ButtonEditBGM _buttonEditBGM;
        private string _telaDeCadastro;

        #endregion

        #region Properties

        /// <summary>
        /// (Set\Get) Informa ou retorna a descrição.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Descrição")]
        [Description("Informa ou retorna a descrição.\nNome: Text")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna o tipo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Tipo")]
        [Description("Informa ou retorna o tipo.\nNome: Tipo")]
        [DefaultValue(LabelControlBGM.eTipo.Default)]
        public eTipo Tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                _tipo = value;

                //Font _font = new System.Drawing.Font(item.Font.Name, item.Font.Size, (item.Font.Bold
                //                ? FontStyle.Bold
                //                : FontStyle.Regular) | FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                if (string.IsNullOrEmpty(_telaDeCadastro))
                {
                    this.Appearance.Font = _tipo.Equals(eTipo.Default)
                        ? _fontDefault
                        : _font;
                    this.Appearance.ForeColor = _tipo.Equals(eTipo.Default)
                        ? _corDefault
                        : _tipo.Equals(eTipo.Chave)
                            ? _corChave
                            : _corObrigatorio;
                }
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna o controle associado.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Controle")]
        [Description("Informa ou retorna o controle associado.\nNome: Controle")]
        [DefaultValue(null)]
        public BaseEdit Controle
        {
            get
            {
                return _controle;
            }
            set
            {
                _controle = value;
                if (_controle is ButtonEditBGM)
                {
                    _buttonEditBGM = _controle as ButtonEditBGM;
                    _buttonEditBGM.LabelControlBGM = this;
                    _buttonEditBGM.Refresh();
                }
                else if (_controle == null)
                {
                    _buttonEditBGM.LabelControlBGM = null;
                    _buttonEditBGM.Refresh();
                    _buttonEditBGM = null;
                    _telaDeCadastro = null;
                }
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna a tela de cadastro associada no ButtonEditBGM.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [Obsolete("Propriedade definida automaticamente, quando o controle associado for um ButtonEditBGM e o mesmo tiver pesquisa associada e na mesma uma tela de cadastro.")]
        public string TelaDeCadastro
        {
            
            get { return _telaDeCadastro; }
            set { _telaDeCadastro = value; }
        }


        /// <summary>
        /// (Set\Get) Informa ou retorna se desabilita o controle associado quando sair do mesmo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Desabilita")]
        [Description("Informa ou retorna se desabilita o controle associado quando sair do mesmo.\nNome: Desabilita")]
        [DefaultValue(false)]
        public bool Desabilita
        {
            get
            {
                return _desabilita;
            }
            set
            {
                _desabilita = _controle == null ? false : value;
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna se utiliza o controle associado na regra que habilitar os botões.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Controlar botões")]
        [Description("Informa ou retorna se utiliza o controle associado na regra que habilitar os botões.\nNome: ControleBotoes")]
        [DefaultValue(true)]
        public bool ControleBotoes
        {
            get
            {
                return _controleBotoes;
            }
            set
            {
                _controleBotoes = value;
            }
        }

        #endregion
    }
}