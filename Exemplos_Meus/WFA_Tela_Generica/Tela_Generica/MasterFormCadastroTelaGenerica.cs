using System;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using FGlobus.Componentes.WinForms;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using System.Data;
using Globus5.WPF.Comum;
using Globus5.WPF.Comum.Pesquisas;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using FGlobus.Util.ExtensaoEnum;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Form de cadastro para a classe TelaGenerica.
    /// <remarks>
    /// Arquivo criado : 7/5/2012 10:06:35 AM. 
    /// Criado por     : alessandro.augusto.
    /// </remarks>
    /// </summary>
    public partial class MasterFormCadastroTelaGenerica : FGlobus.Componentes.WinForms.MasterFormCadastro
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public MasterFormCadastroTelaGenerica()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="modulo">Sigla do modulo</param>
        /// <param name="nomeDTO">Nome do DTO</param>
        public MasterFormCadastroTelaGenerica(string modulo, string nomeDTO)
        {
            InitializeComponent();

            _modulo = modulo;
            _nomeDTO = nomeDTO;

            if (!DesignMode)
                PupularTela();
        }

        #endregion

        #region Atributos

        private string _nomeDTO;
        private string _modulo;
        private string _namespaceDTO;
        private string _propriedadeWebService;
        private object _dTO;
        private object _instancia;
        private PropertyInfo _propertyInfo;
        private MasterPanelBrowseDePesquisaTelaGenerica _masterPanelBrowseDePesquisa;

        #endregion

        #region Metodos controle

        private void TelaGenerica_HabilitarBotoes(object sender)
        {
            object objeto = this.objVwTelaGenerica.RetornarValorObjeto();
            bool achouRegistro = false;

            this.smpBtnBGMGravarMFCadastro.AchouRegistro =
                objeto != null &&
                bool.TryParse(objeto.GetType().GetProperty("AchouRegistro").GetValue(objeto, null).ToString(), out achouRegistro);
        }

        private void smpBtnBGMGravarMFCadastro_Click(object sender, EventArgs e)
        {
            object objeto = this.objVwTelaGenerica.RetornarValorObjeto();
            bool criarObjeto = objeto == null;
            if (criarObjeto)
                objeto = Activator.CreateInstance(this.objVwTelaGenerica.Type);

            this.tblLytPnlTelaGenerica.Controls
                .OfType<TextEdit>()
                .ToList()
                .ForEach(f =>
                {
                    Binding binding = f.DataBindings
                        .OfType<Binding>()
                        .FirstOrDefault();
                    binding.WriteValue();
                    PropertyInfo propertyInfo = objeto.GetType().GetProperty(f.Tag.ToString());

                    object valorObjeto = f.GetType().GetProperty(binding.PropertyName).GetValue(f, null);

                    propertyInfo.SetValue(
                        objeto,
                        Convert.ChangeType(
                            valorObjeto,
                            f.RetornarTipo()), null);
                });

            if (criarObjeto)
                this.objVwTelaGenerica.DataSource = new object[] { objeto };

            ExecutarMetodosGenericoWS("GenericoSalvarOuAlterar", new object[] { this.objVwTelaGenerica.RetornarValorObjeto() });

            this.smpBtnBGMLimparMFCadastro.PerformClick();
        }

        private void smpBtnBGMLimparMFCadastro_Click(object sender, EventArgs e)
        {
            this.objVwTelaGenerica.Clear();
        }

        private void smpBtnBGMExcluirMFCadastro_Click(object sender, EventArgs e)
        {
            ExecutarMetodosGenericoWS("GenericoApagar", new object[] { this.objVwTelaGenerica.RetornarValorObjeto() });
        }

        private void MasterFormCadastroTelaGenerica_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Metodos

        private void PupularTela()
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            #region Difine valores conforme o XML

            XDocument xDocumentTelasGenericas = XDocument.Load(new StreamReader(
                assembly.GetManifestResourceStream(assembly.GetManifestResourceNames()
                    .Where(w => w.IndexOf("TelasGenericas.xml") > 0)
                    .SingleOrDefault())));

            Func<XElement, string, string, bool> delegateValida =
                delegate(XElement xElement, string nomePropriedade, string valor)
                {
                    return xElement.Attribute(nomePropriedade) != null &&
                           xElement.Attribute(nomePropriedade).Value.Equals(valor);
                };

            XElement xElementModulo = xDocumentTelasGenericas.Descendants("Modulo")
                .Where(w => delegateValida(w, "Nome", _modulo) &&
                            w.Descendants("DTO")
                                .Where(w1 => delegateValida(w1, "Nome", _nomeDTO))
                                .Count() > 0)
                .FirstOrDefault();

            ValidarValor(xElementModulo.RetornarValor<string>("Nome", ""), "Nome", "Modulo");

            _namespaceDTO = xElementModulo.RetornarValor<string>("NamespaceDTO", "");
            _propriedadeWebService = xElementModulo.RetornarValor<string>("PropriedadeWebService", "");

            XElement xElementDTO = xElementModulo.Descendants("DTO")
                .Where(w => delegateValida(w, "Nome", _nomeDTO))
                .FirstOrDefault();

            ValidarValor(xElementDTO.RetornarValor<string>("Nome", ""), "Nome", "Modulo");

            #endregion

            this.objVwTelaGenerica.Type = typeof(WebServices).Assembly.GetType(String.Concat(_namespaceDTO, ".", _nomeDTO));
            if (this.objVwTelaGenerica.Type == null)
                throw new Exception(String.Concat("DTO: ", _nomeDTO, " não encontrado na namespace: ", _namespaceDTO));

            _propertyInfo = typeof(WebServices).GetProperty(_propriedadeWebService);
            _instancia = Activator.CreateInstance(_propertyInfo.PropertyType);

            _masterPanelBrowseDePesquisa = new MasterPanelBrowseDePesquisaTelaGenerica();
            _masterPanelBrowseDePesquisa.objVwMFBrowseDePesquisa = this.objVwTelaGenerica;
            _masterPanelBrowseDePesquisa.BrowseDePesquisa.Pesquisar += new BrowseDePesquisaEventHandler(BrowseDePesquisa_Pesquisar);

            #region Cria campos conformas as propriedades do XML

            IEnumerable<XElement> xElementPropriedades = xElementDTO.Descendants("Propriedade");

            IEnumerable<int[]> listaLinhasColunas = xElementPropriedades
                .Select(s => new int[] 
                {
                    s.RetornarValor<int>("Linha", 0),
                    s.RetornarValor<int>("Coluna", 0)
                });

            //this.tblLytPnlTelaGenerica.RowCount = listaLinhasColunas.Max(m => m[0]);
            //this.tblLytPnlTelaGenerica.ColumnCount = listaLinhasColunas.Max(m => m[1]);

            RowStyle rowStyle = this.tblLytPnlTelaGenerica.RowStyles.OfType<RowStyle>().FirstOrDefault();
            rowStyle.SizeType = SizeType.Absolute;
            rowStyle.Height = 22;

            xElementPropriedades
                .Select((s, index) => new { s, index })
                .ToList()
                .ForEach(f =>
                {
                    #region Identifica os atributos do XML

                    string nomeProp = f.s.RetornarValor<string>("Nome", "");
                    ValidarValor(nomeProp, "Nome", "Propriedade");

                    bool calculaProximoProp = f.s.RetornarValor<bool>("CalculaProximo", false);
                    bool pesquisaProp = f.s.RetornarValor<bool>("Pesquisa", false);

                    int linha = f.s.RetornarValor<int>("Linha", 0);
                    int coluna = f.s.RetornarValor<int>("Coluna", 0);

                    XElement xElementLabel = f.s.Descendants("Label").FirstOrDefault();
                    string descicaoLabel = xElementLabel.RetornarValor<string>("Descricao", "");
                    LabelControlBGM.eTipo tipoLabel = xElementLabel.RetornarValor<string>("Tipo", "Default").RetornarEnum<LabelControlBGM.eTipo>();

                    XElement xElementCampo = f.s.Descendants("Campo").FirstOrDefault();
                    int tipoMascaraCampo = xElementCampo.RetornarValor<int>("TipoMascara", 1);
                    int tamanhoCampo = xElementCampo.RetornarValor<int>("Tamanho", 0);
                    string tipoCampo = xElementCampo.RetornarValor<string>("Tipo", "");
                    ValidarValor(tipoCampo, "Tipo", "Campo");
                    ValidarValor((tamanhoCampo.Equals(0) && (tipoCampo.Equals("Button") || tipoCampo.Equals("Calc") || tipoCampo.Equals("Text"))
                        ? null
                        : "X"), "Tamanho", "Campo");

                    bool dockFillCampo = xElementCampo.RetornarValor<bool>("DockFill", false);
                    bool preencherComZerosCampo = xElementCampo.RetornarValor<bool>("PreencherComZeros", false);
                    CharacterCasing tipoCaracterCampo = xElementCampo.RetornarValor<string>("TipoCaracter", "Normal").RetornarEnum<CharacterCasing>();
                    int casasDecimaisCampo = xElementCampo.RetornarValor<int>("CasasDecimais", 0);
                    int valorMinimoCampo = xElementCampo.RetornarValor<int>("ValorMinimo", 0);
                    int valorMaximoCampo = xElementCampo.RetornarValor<int>("ValorMaximo", 0);

                    bool dataProgressivaAtivoCampo = xElementCampo.RetornarValor<bool>("DataProgressivaAtivo", false);
                    string dataProgressivaMensagemCampo = xElementCampo.RetornarValor<string>("DataProgressivaMensagem", "");
                    string dataProgressivaPassarAQuantidadeDeDiasCampo = xElementCampo.RetornarValor<string>("DataProgressivaPassarAQuantidadeDeDias", "Permitir");
                    int dataProgressivaQuantidadeDiasCampo = xElementCampo.RetornarValor<int>("DataProgressivaQuantidadeDias", 0);

                    bool dataRetroativaAtivoCampo = xElementCampo.RetornarValor<bool>("DataRetroativaAtivo", false);
                    string dataRetroativaMensagemCampo = xElementCampo.RetornarValor<string>("DataRetroativaMensagem", "");
                    string dataRetroativaPassarAQuantidadeDeDiasCampo = xElementCampo.RetornarValor<string>("DataRetroativaPassarAQuantidadeDeDias", "Permitir");
                    int dataRetroativaQuantidadeDiasCampo = xElementCampo.RetornarValor<int>("DataRetroativaQuantidadeDias", 0);

                    ImageComboBoxItem[] listaImageComboBoxItemCampo = xElementCampo.Descendants("Item")
                        .Select(s => new ImageComboBoxItem()
                        {
                            Description = s.RetornarValor<string>("Descricao", ""),
                            Value = Convert.ChangeType(s.RetornarValor<string>("Valor", ""), this.objVwTelaGenerica.Columns[nomeProp].Type)
                        })
                        .ToArray();

                    #endregion

                    #region LabelControlBGM

                    LabelControlBGM labelControlBGM = null;
                    if (tipoCampo != "Check")
                    {
                        ValidarValor(descicaoLabel, "Descricao", "Label");
                        labelControlBGM = new LabelControlBGM()
                        {
                            Text = descicaoLabel,
                            Tipo = tipoLabel,
                            Dock = DockStyle.Bottom
                        };
                        this.tblLytPnlTelaGenerica.Controls.Add(labelControlBGM);
                    }

                    #endregion

                    #region Cria coluna do grid

                    GridColumn gridColumn = new GridColumn();
                    gridColumn.Caption = descicaoLabel;
                    gridColumn.FieldName = nomeProp;
                    gridColumn.Visible = true;
                    gridColumn.VisibleIndex = _masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.Columns.Count;
                    gridColumn.OptionsColumn.AllowEdit = false;
                    gridColumn.OptionsColumn.ReadOnly = true;
                    _masterPanelBrowseDePesquisa.BrowseDePesquisa.Grid.Columns.Add(gridColumn);

                    #endregion

                    string propertyNameBinding = "EditValue";
                    BaseEdit baseEdit = null;
                    switch (tipoCampo)
                    {
                        #region ButtonEditBGM
                        case "Button":
                            {
                                ButtonEditBGM buttonEditBGM = new ButtonEditBGM();
                                buttonEditBGM.BrowseDePesquisa = "x";
                                buttonEditBGM.ExecutarPesquisa = false;
                                buttonEditBGM.SegBotaoProximo = calculaProximoProp;
                                buttonEditBGM.PreencherComZeros = preencherComZerosCampo;
                                buttonEditBGM.QuantidadeNumeros = tamanhoCampo;
                                buttonEditBGM.Properties.MaxLength = tamanhoCampo;
                                buttonEditBGM.TipoMascara = new ClassMascaras(tipoMascaraCampo);
                                buttonEditBGM.Properties.CharacterCasing = tipoCaracterCampo;

                                buttonEditBGM.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(buttonEditBGM_ButtonClick);
                                buttonEditBGM.Confirmar += new EventHandler(buttonEditBGM_Confirmar);
                                buttonEditBGM.CalcularProximo += new ButtonEditBGMEventHandler(buttonEditBGM_CalcularProximo);
                                buttonEditBGM.Validating += new System.ComponentModel.CancelEventHandler(buttonEditBGM_Validating);

                                labelControlBGM.Desabilita = true;
                                labelControlBGM.Controle = buttonEditBGM;
                                baseEdit = buttonEditBGM;
                                this.tblLytPnlTelaGenerica.Controls.Add(buttonEditBGM);
                            }
                            break;
                        #endregion

                        #region CalcEditBGM
                        case "Calc":
                            {
                                CalcEditBGM calcEditBGM = new CalcEditBGM();
                                calcEditBGM.CasasDecimais = casasDecimaisCampo;
                                calcEditBGM.ValorMinimo = valorMinimoCampo;
                                calcEditBGM.ValorMaximo = valorMaximoCampo;
                                calcEditBGM.TipoMascara = new ClassMascaras(tipoMascaraCampo);
                                calcEditBGM.Properties.MaxLength = tamanhoCampo;

                                labelControlBGM.Controle = calcEditBGM;
                                baseEdit = calcEditBGM;
                                this.tblLytPnlTelaGenerica.Controls.Add(calcEditBGM);
                            }
                            break;
                        #endregion

                        #region DateEditBGM
                        case "Date":
                            {
                                #region Delegate ClassDateEditBGM

                                Func<bool, string, string, int, ClassDateEditBGM> delegateClassDateEditBGM =
                                    delegate(bool ativo, string mensagemAviso, string passarAQuantidadeDeDias, int quantidadeDias)
                                    {
                                        return new ClassDateEditBGM()
                                        {
                                            Ativo = ativo,
                                            MensagemAviso = mensagemAviso,
                                            PassarAQuantidadeDeDias = passarAQuantidadeDeDias.Equals("Permitir")
                                                ? FGlobus.Componentes.WinForms.eDateEditBGM.Permitir
                                                : passarAQuantidadeDeDias.Equals("Bloquear")
                                                    ? FGlobus.Componentes.WinForms.eDateEditBGM.Bloquear
                                                    : FGlobus.Componentes.WinForms.eDateEditBGM.Alertar,
                                            QuantidadeDias = quantidadeDias
                                        };
                                    };

                                #endregion

                                DateEditBGM dateEditBGM = new DateEditBGM();
                                dateEditBGM.TipoMascara = new ClassMascaras(tipoMascaraCampo);
                                dateEditBGM.DataProgressiva = delegateClassDateEditBGM(
                                    dataProgressivaAtivoCampo,
                                    dataProgressivaMensagemCampo,
                                    dataProgressivaPassarAQuantidadeDeDiasCampo,
                                    dataProgressivaQuantidadeDiasCampo);

                                dateEditBGM.DataRetroativa = delegateClassDateEditBGM(
                                    dataRetroativaAtivoCampo,
                                    dataRetroativaMensagemCampo,
                                    dataRetroativaPassarAQuantidadeDeDiasCampo,
                                    dataRetroativaQuantidadeDiasCampo);

                                labelControlBGM.Controle = dateEditBGM;
                                baseEdit = dateEditBGM;
                                tamanhoCampo = dateEditBGM.Properties.Mask.EditMask.Length;

                                this.tblLytPnlTelaGenerica.Controls.Add(dateEditBGM);
                            }
                            break;
                        #endregion

                        #region TextEditBGM
                        case "Text":
                            {
                                TextEditBGM textEditBGM = new TextEditBGM();
                                textEditBGM.QuantidadeNumeros = tamanhoCampo;
                                textEditBGM.TipoMascara = new ClassMascaras(tipoMascaraCampo);
                                textEditBGM.Properties.MaxLength = tamanhoCampo;
                                textEditBGM.Properties.CharacterCasing = tipoCaracterCampo;

                                labelControlBGM.Controle = textEditBGM;
                                baseEdit = textEditBGM;
                                this.tblLytPnlTelaGenerica.Controls.Add(textEditBGM);
                            }
                            break;
                        #endregion

                        #region CheckEdit
                        case "Check":
                            {
                                CheckEdit checkEdit = new CheckEdit();
                                checkEdit.Text = descicaoLabel;

                                baseEdit = checkEdit;
                                this.tblLytPnlTelaGenerica.Controls.Add(checkEdit);
                            }
                            break;
                        #endregion

                        #region ComboBoxEdit
                        case "ComboBox":
                            {
                                RepositoryItemImageComboBox repositoryItemImageComboBox = new RepositoryItemImageComboBox();
                                repositoryItemImageComboBox.Items.AddRange(listaImageComboBoxItemCampo);

                                gridColumn.ColumnEdit = repositoryItemImageComboBox;
                                _masterPanelBrowseDePesquisa.BrowseDePesquisa.RepositoryItems.Add(repositoryItemImageComboBox);

                                ImageComboBoxEdit imageComboBoxEdit = new ImageComboBoxEdit();
                                imageComboBoxEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                                imageComboBoxEdit.Properties.Items.AddRange(listaImageComboBoxItemCampo);
                                tamanhoCampo = listaImageComboBoxItemCampo
                                    .Max(m => m.Description.Length);

                                // propertyNameBinding = "SelectedItem";
                                imageComboBoxEdit.SelectedIndex = 0;
                                labelControlBGM.Controle = imageComboBoxEdit;
                                baseEdit = imageComboBoxEdit;

                                this.tblLytPnlTelaGenerica.Controls.Add(imageComboBoxEdit);

                            }
                            break;
                        #endregion

                        default:
                            throw new Exception(String.Concat("Tipo de campo não encontrado: ", tipoCampo, "\n",
                                "Os tipos disponíveis são:", "\n",
                                "       Button", "\n",
                                "       Calc", "\n",
                                "       Date", "\n",
                                "       Text", "\n",
                                "       Check", "\n",
                                "       ComboBox"));
                    }

                    this.tblLytPnlTelaGenerica.RowStyles.Insert(0, new RowStyle(rowStyle.SizeType, rowStyle.Height));

                    DefinirPropriedadesComuns(
                        baseEdit,
                        propertyNameBinding,
                        nomeProp,
                        tamanhoCampo,
                        dockFillCampo,
                        f.index,
                        gridColumn);
                });

            #endregion

            this.Height = 70 + this.pnlCtrlBotoesMFCadastro.Height + (this.tblLytPnlTelaGenerica.Controls.Count * 22);
        }

        private void DefinirPropriedadesComuns(BaseEdit baseEdit, string propertyNameBinding, string nomeProp, int tamanhoCampo, bool dockFill, int tabIndex, GridColumn gridColumn)
        {
            if (baseEdit is TextEdit)
            {
                TextEdit textEdit = baseEdit as TextEdit;
                if (dockFill)
                    textEdit.Dock = DockStyle.Fill;
                else
                {
                    ButtonEdit buttonEdit = textEdit as ButtonEdit;
                    int tamanhoBotoes = 0;
                    if (buttonEdit != null)
                        tamanhoBotoes = buttonEdit.Properties.Buttons.Count * 16;

                    baseEdit.Width = DefinirTamanhoDoCampo(nomeProp, textEdit, tamanhoCampo) + tamanhoBotoes;
                }
                gridColumn.Width = DefinirTamanhoDoCampo(nomeProp, textEdit, tamanhoCampo);
            }

            //baseEdit.Dock = DockStyle.Top;
            baseEdit.TabIndex = tabIndex;
            baseEdit.Tag = nomeProp;            
            baseEdit.DataBindings.Add(new Binding(propertyNameBinding, this.objVwTelaGenerica, nomeProp, true));
        }

        private int DefinirTamanhoDoCampo(string nomeProp, TextEdit textEdit, int tamanhoCampo)
        {
            // Retornar o tamanho absoluto do caracter
            // Conforme o tipo da coluna
            string caracter = this.objVwTelaGenerica.Columns[nomeProp].Type.Equals(typeof(string))
                    ? "O"
                    : "0";

            return (int)(Graphics.FromHwnd(textEdit.Handle).MeasureString(caracter, textEdit.Font).Width * tamanhoCampo);
        }

        private int buttonEditBGM_CalcularProximo(object sender)
        {
            object retorno = ExecutarMetodosGenericoWS(
                "GenericoRetornarProximoValorPropriedade",
                new object[] 
                {
                    Activator.CreateInstance(this.objVwTelaGenerica.Type),
                    (sender as ButtonEditBGM).Tag.ToString()
                });

            return (int)retorno;
        }

        private void buttonEditBGM_Confirmar(object sender, EventArgs e)
        {
            this.objVwTelaGenerica.DataSource = new object[] 
            {
                _masterPanelBrowseDePesquisa.LinhaSelecionada
            };
        }

        private void buttonEditBGM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    _masterPanelBrowseDePesquisa.ShowBrowseDePesquisa();
                    buttonEditBGM_Confirmar(sender, null);
                    break;
                case 1:
                    break;
            }
        }

        private System.Collections.ICollection BrowseDePesquisa_Pesquisar()
        {
            var retorno = (ExecutarMetodosGenericoWS(
                "GenericoRetornarTodos",
                new object[]
                {
                    "ValorChaveCRUD",
                    _nomeDTO
                }) as object[])
                .Select(s => Convert.ChangeType(s, this.objVwTelaGenerica.Type))
                .ToArray();

            this.objVwTelaGenericaGeral.DataSource = retorno;

            return retorno;
        }

        private void buttonEditBGM_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SentidoNavegacao == eSentidoNavegacao.Fore)
            {
                ButtonEditBGM buttonEditBGM = sender as ButtonEditBGM;
                if (buttonEditBGM.CampoVazio())
                {
                    _masterPanelBrowseDePesquisa.ShowBrowseDePesquisa();
                    buttonEditBGM_Confirmar(sender, null);
                }
                else
                {
                    try
                    {
                        object objeto = this.objVwTelaGenerica.RetornarValorObjeto();

                        try
                        {
                            // Pesquisa o valor na lista geral
                            object objetoListaGeral = this.objVwTelaGenericaGeral.DataSource
                                .OfType<object>()
                                .Where(w => w.GetType().GetProperty(buttonEditBGM.Tag.ToString()).GetValue(w, null).ToString() == buttonEditBGM.EditValue.ToString())
                                .FirstOrDefault();

                            if (objetoListaGeral != null)
                            {
                                objeto = objetoListaGeral;
                                this.objVwTelaGenerica.DataSource = new object[] { objetoListaGeral };
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        object valorObjeto = null;
                        if (objeto != null)
                            valorObjeto = objeto.GetType().GetProperty(buttonEditBGM.Tag.ToString()).GetValue(objeto, null);

                        if (objeto == null || !buttonEditBGM.ComparaValor(valorObjeto.ToString()))
                            this.objVwTelaGenerica.DataSource = new object[] 
                            { 
                                ExecutarMetodosGenericoWS(
                                    "GenericoConsultaPorChave", 
                                    new object[]
                                    {
                                        "ValorChaveCRUD",
                                        _nomeDTO,
                                        Convert.ChangeType(buttonEditBGM.Text, buttonEditBGM.RetornarTipo())
                                    }) 
                            };
                    }
                    catch (Exception ex)
                    {
                        Mensagem.VerificaMensagem(
                            new SoapException(ex.InnerException.Message, System.Xml.XmlQualifiedName.Empty),
                            FGlobus.Excecao.BOExcecao.CodigoInexistente);
                    }
                }

                if (buttonEditBGM.CampoVazio())
                {
                    buttonEditBGM.Text = "";
                    buttonEditBGM.Focus();
                }
                else
                    this.AtualizaSimpleButtonBGM();
            }
        }

        private void ValidarValor(string valor, string nomeAtributo, string nomePropriedade)
        {
            if (String.IsNullOrEmpty(valor))
                throw new Exception("Atributo '" + nomeAtributo + "' não encontrado na propriedade '" + nomePropriedade + "'.");
        }

        private object ExecutarMetodosGenericoWS(string nomeMetodo, IEnumerable<object> parametros)
        {
            try
            {
                return _propertyInfo.PropertyType.InvokeMember(
                    nomeMetodo,
                    BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                    null,
                    _instancia,
                    parametros.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    internal static class Funcoes
    {
        /// <summary>
        /// Retorna valor do atributo
        /// </summary>
        /// <typeparam name="T">Tipo de retorno</typeparam>
        /// <param name="valor">Objeto</param>
        /// <param name="nomePropriedade">Nome da propriedade</param>
        /// <param name="valorDefault">Valor default</param>
        /// <returns>Retorna valor conforme o tipo informado</returns>
        public static T RetornarValor<T>(this XElement valor, string nomePropriedade, object valorDefault = null)
        {
            XAttribute xAttribute = valor.Attribute(nomePropriedade);
            if (xAttribute == null)
                xAttribute = new XAttribute(valor.Name, valorDefault);

            try
            {
                return (T)Convert.ChangeType(xAttribute.Value, typeof(T));
            }
            catch (Exception)
            {
                return (T)Convert.ChangeType(valorDefault, typeof(T));
            }
        }

        /// <summary>
        /// Retorna o tipo do coluna do do controle informado
        /// </summary>
        /// <param name="valor">Valor</param>
        /// <returns>Type</returns>
        public static Type RetornarTipo(this TextEdit valor)
        {
            return (valor.DataBindings
                .OfType<Binding>()
                .FirstOrDefault().DataSource as ObjectView).Columns[valor.Tag.ToString()].Type;
        }

        /// <summary>
        /// Retorna o valor do primeiro item o ObjectView informado
        /// </summary>
        /// <param name="valor">Valor</param>
        /// <returns>object</returns>
        public static object RetornarValorObjeto(this ObjectView valor)
        {
            object objeto = valor.DataSource
                .OfType<object>()
                .FirstOrDefault();

            if (objeto == null)
                return null;
            else
                return Convert.ChangeType(objeto, valor.Type);

        }

    }


}