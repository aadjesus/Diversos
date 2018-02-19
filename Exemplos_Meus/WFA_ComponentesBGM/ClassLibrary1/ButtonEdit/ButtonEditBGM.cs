using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Collections;
using FGlobus.Util;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Mask;
using DevExpress.Utils;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Controle do tipo ButtonEdit com implementações exclusívas BGM.
    /// </summary>
    [ToolboxItem(true)]
    [Description("Controle do tipo ButtonEdit com implementações exclusívas BGM.")]
    [ToolboxBitmap(typeof(ButtonEditBGM), "ButtonEdit.ButtonEditBGM.ico")]
    //[Docking(DockingBehavior.Ask)]
    [Designer(typeof(ControlaComponenteDesigner))]
    [PropertyTab(typeof(BotaoBrowserPropriedades), PropertyTabScope.Document)]
    [DefaultProperty("BrowseDePesquisa")]
    [DefaultEvent("Click")]
    [InformacoesComponente("Alessandro Augusto", "1.0.0", "mhtml:http://tfsserver/sites/BGMRodotec/Globus/Desenvolvimento/Manuais/IdentidadeVisualGlb_Htm.mht#_Toc325727334")]
    public partial class ButtonEditBGM : ButtonEdit
    {
        #region Construtos

        /// <summary>
        /// Construtor default <see cref="ButtonEditBGM"/> em tempo de desenvovimento.
        /// </summary>
        public ButtonEditBGM()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Construtor default <see cref="ButtonEditBGM"/>, em tempo de execução.
        /// </summary>
        /// <param name="container">Objeto do tipo IContainer.</param>
        public ButtonEditBGM(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        #endregion

        #region Attributes

        private bool _executarPesquisa = true;
        private bool _preencherComZeros = false;
        private bool _segBotaoProximo;
        private string _browseDePesquisa = Constantes.NONE_DEFAULT;
        private object _linhaSelecionada = null;
        private ICollection _resultadoPesquisaDados;
        private int _consulta = 0;
        private LabelControlBGM _labelControlBGM;
        private ClassMascaras _classMascaras = new ClassMascaras();
        private DXErrorProvider _dXErrorProvider = new DXErrorProvider();
        private int _quantidadeNumeros = 0;
        private bool _apresentaBarraDeProgresso = false;
        private string _mensagemBarraDeProgresso = "Aguarde...";

        #endregion

        #region Properties

        /// <summary>
        /// (Set/Get) Informa ou retorna a mascara do controle.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Valor")]
        [Description("Informa ou retorna o valor.\nNome: Text")]
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
        /// (Set/Get) Informa ou retorna a mascara do controle.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Mascara")]
        [Description("Informa ou retorna a mascara.\nNome: Mascara")]
        [Editor("DevExpress.XtraEditors.Design.MaskDesigner, DevExpress.XtraEditors.v9.3.Design", typeof(UITypeEditor))]
        [Browsable(false)]
        [Obsolete("Utilizar a propriedade  Properties.Mask.")]
        public DevExpress.XtraEditors.Mask.MaskProperties Mascara
        {
            get
            {
                return base.Properties.Mask;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o tamnho do campo mascara.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Tamanho do campo")]
        [Description("Informa ou retorna o tamnho do campo mascara.\nNome: TamanhoCampo")]
        [Browsable(false)]
        [Obsolete("Utilizar a propriedade  Properties.MaxLength.")]
        public int TamanhoCampo
        {
            get
            {
                return base.Properties.MaxLength;
            }
            set
            {
                base.Properties.MaxLength = value;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o browse de pesquisa selecionado.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Browse de pesquisa")]
        [Description("Informa ou retorna o browse de pesquisa selecionado.\nNome: BrowseDePesquisa")]
        [EditorAttribute(typeof(SelecionaTelaEditor), typeof(UITypeEditor))]
        [DefaultValue(Constantes.NONE_DEFAULT)]
        public string BrowseDePesquisa
        {
            get
            {
                return _browseDePesquisa;
            }
            set
            {
                _browseDePesquisa = value;
                if (_browseDePesquisa != Constantes.NONE_DEFAULT && this.Properties.Buttons.Count >= 1)
                {
                    this.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    this.Properties.Buttons[0].Image = global::FGlobus.Componentes.WinForms.Resources.ImagensBGM.ButtonEditBGMLupa;
                }
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna se preenche controle com zeros a esquerda.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Preencher com zeros")]
        [Description("Informa ou retorna se preenche controle com zeros a esquerda.\nNome: PreencherComZeros")]
        [DefaultValue(false)]
        public bool PreencherComZeros
        {
            get
            {
                return _preencherComZeros;
            }
            set
            {
                _preencherComZeros = value;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna se adiciona botão para calcula o próximo.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Botão calcular próximo")]
        [Description("Informa ou retorna se adiciona botão para calcular o próximo.\nNome: SegBotaoProximo")]
        [DefaultValue(false)]
        public bool SegBotaoProximo
        {
            get
            {
                return _segBotaoProximo;
            }
            set
            {
                _segBotaoProximo = value;
                string descricaoCaption = "Próximo código (+)";
                EditorButton editorButton = this.Properties.Buttons
                    .Cast<EditorButton>()
                    .Where(w => w.Caption.Equals(descricaoCaption) &&
                                w.ToolTip.Equals(descricaoCaption))
                    .SingleOrDefault();

                if (editorButton != null)
                    editorButton.SuperTip = Util.Util.CriarSuperToolTip(descricaoCaption);
                if (editorButton == null && _segBotaoProximo)
                {
                    this.Properties.Buttons.Add(new EditorButton()
                    {
                        Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus,
                        Caption = descricaoCaption,
                        ToolTip = descricaoCaption,                        
                        Shortcut = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.Add)
                    });
                }
                else if (editorButton != null && !_segBotaoProximo)
                    this.Properties.Buttons.RemoveAt(editorButton.Index);
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o tipo de consulta que vai ser executada.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Consulta")]
        [Description("Informa ou retorna o tipo de consulta que vai ser executada.\nNome: Consulta")]
        [DefaultValue(0)]
        public int Consulta
        {
            get
            {
                return _consulta;
            }
            set
            {
                _consulta = value;
            }
        }

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
        [DisplayName("Quantidade numeros")]
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

        /// <summary>
        /// (Set/Get) Retorna ou informa o LabelControlBGM associado no controle.
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        public LabelControlBGM LabelControlBGM
        {
            get
            {
                return _labelControlBGM;
            }
            set
            {
                _labelControlBGM = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa se executa a pesquisaBGM no click do botão.
        /// </summary>
        [Browsable(false)]
        public bool ExecutarPesquisa
        {
            get
            {
                return _executarPesquisa;
            }
            set
            {
                _executarPesquisa = value;
            }
        }

        /// <summary>
        /// (Get) Retorna o objeto correspondente à linha selecionada na pesquisa.
        /// </summary>
        [Browsable(false)]
        public object LinhaSelecionada
        {
            get
            {
                return _linhaSelecionada;
            }
        }

        /// <summary>
        /// (Get\Set) Guarda o resultado da pesquisa pra não precisar executa la novamente..
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public ICollection ResultadoPesquisaDados
        {
            get
            {
                return _resultadoPesquisaDados;
            }
            set
            {
                _resultadoPesquisaDados = value;
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna se apresenta a Barra de progresso.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Apresenta barra de progresso")]
        [Description("Informa ou retorna se apresenta a Barra de progresso.\nNome: ApresentaBarraDeProgresso")]
        [DefaultValue(false)]
        public bool ApresentaBarraDeProgresso
        {
            get
            {
                return _apresentaBarraDeProgresso;
            }
            set
            {
                _apresentaBarraDeProgresso = value;
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna a mensagem da Barra de progresso.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("Mensagem da barra de progresso")]
        [Description("Informa ou retorna a mensagem da Barra de progresso.\nNome: MensagemBarraDeProgresso")]
        [DefaultValue("Aguarde...")]
        public string MensagemBarraDeProgresso
        {
            get
            {
                return _mensagemBarraDeProgresso;
            }
            set
            {
                _mensagemBarraDeProgresso = value;
            }
        }

        #endregion

        #region Override

        /// <summary>
        /// Sobreposição do método para controlar o browse de pesquisa e o próximo valor.
        /// </summary>
        /// <param name="buttonInfo">Objeto do tipo EditorButtonObjectInfoArgs.</param>
        protected override void OnClickButton(DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs buttonInfo)
        {
            base.OnClickButton(buttonInfo);

            switch (buttonInfo.Button.Index)
            {
                case 0:
                    if (_executarPesquisa && _browseDePesquisa != Constantes.NONE_DEFAULT)
                        this.ShowBrowseDePesquisa(this);
                    break;
                case 1:
                    if (_segBotaoProximo && this.CampoVazio() && CalcularProximo != null)
                    {
                        try
                        {
                            this.Text = CalcularProximo(this).ToString();
                            if (!String.IsNullOrEmpty(this.Text))
                                this.OnValidating(new CancelEventArgs(false));
                        }
                        catch (Exception)
                        {
                            this.Text = "";
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Sobreposição do método para preencher o campo com zeros.
        /// </summary>
        /// <param name="e">Objeto do tipo EventArgs.</param>
        protected override void OnLeave(EventArgs e)
        {
            if (_preencherComZeros && !this.CampoVazio())
            {
                try
                {
                    this.Text = Convert.ToInt32(this.Text.Trim()).ToString().PadLeft(this.Properties.MaxLength, '0');
                }
                catch (Exception)
                {
                }
            }
            base.OnLeave(e);
        }

        /// <summary>
        /// Sobreposição do método para desabilitar o 2º botão quando o campo estiver preenchido.
        /// </summary>
        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();
            if (_segBotaoProximo && this.Properties.Buttons.Count >= 2)
                this.Properties.Buttons[1].Enabled = this.CampoVazio();
        }

        /// <summary>
        /// Sobreposição do método para atribuir true para variável "SimulaEnterAposSetaParaBaixo" quando for pressionado seta para baixo
        /// </summary>
        /// <param name="e">Objeto do tipo KeyEventArgs</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue == 40 &&
                this._labelControlBGM != null &&
                this._labelControlBGM.Tipo == LabelControlBGM.eTipo.Default)
            {
                Util.Util.SimulaEnterAposSetaParaBaixo = true;
            }
            else if (e.KeyValue.Equals(84))
            {
                bool _mascaraData = false;

                if ((_classMascaras.Controles != null) && (_classMascaras.Controles.Count() > 0) && (!_classMascaras.Codigo.Equals(1)))
                    _mascaraData = _classMascaras.Controles
                        .Where(w => w.Equals(ClassMascaras.TiposControles.DateEdit))
                        .Count() > 0;

                if (_mascaraData)
                    this.EditValue = DateTime.Now;
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Sobreposição do método para mostrar a barra de progesso e validar o campo conforme a mascara
        /// </summary>
        /// <param name="e">Objeto do tipo CancelEventArgs</param>
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
            {
                bool _mascaraData = false;

                if ((_classMascaras.Controles != null) && (_classMascaras.Controles.Count() > 0) && (!_classMascaras.Codigo.Equals(1)))
                    _mascaraData = _classMascaras.Controles
                        .Where(w => w.Equals(ClassMascaras.TiposControles.DateEdit))
                        .Count() > 0;

                if (_mascaraData)
                {
                    try
                    {
                        this.ErrorText = String.Empty;

                        Convert.ToDateTime(this.Text);

                        if (String.IsNullOrEmpty(this.Text))
                            this.Text = DateTime.MinValue.ToString();

                        base.OnValidating(e);
                    }
                    catch (Exception)
                    {
                        this.ErrorText = "Data inválida.";
                        this.Focus();
                    }
                }
                else
                    base.OnValidating(e);
            }

        }

        /// <summary>
        /// Sobreposição do método para atualizar os dados da mascara
        /// </summary>
        protected override void OnLoaded()
        {
            ValidaMascara();

            base.OnLoaded();
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Verifica se o campo está vazio.
        /// </summary>
        /// <returns>bool.</returns>
        public bool CampoVazio()
        {
            try
            {
                return this.EditValue == null || String.IsNullOrEmpty(this.EditValue.ToString());
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Compara o valor de uma string.
        /// </summary>
        /// <param name="valor">Valor a ser comparado.</param>
        /// <returns>Boolean.</returns>
        public bool ComparaValor(string valor)
        {
            try
            {
                return this.EditValue.ToString().Trim() == valor;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Compara o valor de um integer.
        /// </summary>
        /// <param name="valor">Valor a ser comparado.</param>
        /// <returns>Boolean.</returns>
        public bool ComparaValor(int valor)
        {
            try
            {
                return Convert.ToInt32(this.EditValue.ToString().Trim()) == valor;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Abre o BrowseDePesquisa associado no controle.
        /// </summary>
        /// <param name="buttonEditBGM">Objeto do tipo ButtonEditBGM.</param>
        public void ShowBrowseDePesquisa(ButtonEditBGM buttonEditBGM)
        {
            
        }

        /// <summary>
        /// Abre o BrowseDePesquisa associado no controle.
        /// </summary>
        public void ShowBrowseDePesquisa()
        {
            this.ShowBrowseDePesquisa(this);
        }

        /// <summary>
        /// Abre o BrowseDePesquisa associado no controle.
        /// </summary>
        /// <param name="buttonEditBGM">Objeto do tipo ButtonEditBGM.</param>
        /// <param name="consulta">Qual consulta que vai ser executada.</param>
        public void ShowBrowseDePesquisa(ButtonEditBGM buttonEditBGM, int consulta)
        {
            int _consultaAtual = this._consulta;
            this._consulta = consulta;
            this.ShowBrowseDePesquisa(buttonEditBGM);
            _linhaSelecionada = null;

            this._consulta = _consultaAtual;
        }

        internal void OnLinhaSelecionada(object objeto, FormClosingEventArgs e)
        {
            _linhaSelecionada = objeto;
            if (Confirmar != null)
                Confirmar(this, e);
        }

        internal void OnCancelar(FormClosingEventArgs e)
        {
            _linhaSelecionada = null;
            if (Cancelar != null)
                Cancelar(this, e);
        }

        private void ValidaMascara()
        {
            if (this._classMascaras != null && !String.IsNullOrEmpty(this._classMascaras.Tipo))
            {
                base.Properties.PasswordChar = char.MinValue;

                base.Properties.DisplayFormat.FormatString = String.Empty;
                base.Properties.DisplayFormat.FormatType = FormatType.None;

                base.Properties.EditFormat.FormatString = String.Empty;
                base.Properties.EditFormat.FormatType = FormatType.None;
                
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

        #region Eventos

        /// <summary>
        /// Evento para calcular o próximo valor do campo, o mesmo é disparado após clicar no 2º botão.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Evento para calcular o próximo valor do campo, o mesmo é disparado após clicar no 2º botão.")]
        [DisplayName("2) Calcular próximo")]
        public event ButtonEditBGMEventHandler CalcularProximo;

        /// <summary>
        /// Evento disparado quando botão CONFIRMAR for pressionado.
        /// </summary>        
        [Description("Evento disparado quando botão CONFIRMAR for pressionado.")]
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("1) Confirmar")]
        public event EventHandler Confirmar;

        /// <summary>
        /// Evento disparado quando botão CANCELAR for pressionado.
        /// </summary>        
        [Description("Evento disparado quando botão CANCELAR for pressionado.")]
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("3) Cancelar")]
        public event EventHandler Cancelar;

        #endregion
    }

    /// <summary>
    /// Delegate para controlar o evento "CalcularProximo"
    /// </summary> 
    /// <param name="sender">Objeto que esta executando o evento.</param>
    /// <returns>Inteito.</returns>
    public delegate int ButtonEditBGMEventHandler(object sender);

    /// <summary>
    /// Delegate para controlar o evento "Parametros"
    /// </summary>
    /// <param name="sender">Objeto que esta executando o evento.</param>
    //public delegate ParametrosBrowseDePesquisaBGM ButtonEditBGMParametrosEventHandler(object sender);
}