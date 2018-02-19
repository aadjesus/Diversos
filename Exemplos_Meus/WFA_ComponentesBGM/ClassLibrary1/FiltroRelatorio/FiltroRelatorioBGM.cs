using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using FGlobus.Util;
using System.Drawing.Design;
using DevExpress.XtraEditors.Mask;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace FGlobus.Componentes.WinForms
{

    [Designer(typeof(ControlaComponenteDesigner1))]
    public partial class FiltroRelatorioBGM : UserControl
    {
        #region Construtor

        /// <summary>
        /// Construtor dafault
        /// </summary>
        public FiltroRelatorioBGM()
        {
            InitializeComponent();
        }

        #endregion

        #region Atributos

        private string _browserDePesquisa;
        private string _titulo;
        private string _colunaRetorno;
        private object _valorFinal;
        private object _valorInicial;
        private eTipo _tipo = eTipo.AlfaNumerico;
        private bool _informouTipo;
        private ClassMascaras _classMascaras = new ClassMascaras();
        private decimal _valorMaximo = 0;
        private decimal _valorMinimo = 0;
        private int _casasDecimais = 2;

        /// <summary>
        /// Tipo de campo do filtro.
        /// </summary>
        public enum eTipo
        {
            /// <summary>
            /// Data
            /// </summary>
            Data,
            /// <summary>
            /// String
            /// </summary>
            AlfaNumerico,
            /// <summary>
            /// Numerico
            /// </summary>
            Numerico
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public object ValorInicial
        {
            get { return _valorInicial; }
            set { _valorInicial = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public object ValorFinal
        {
            get { return _valorFinal; }
            set { _valorFinal = value; }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna a titulo.
        /// </summary>
        [Description("Informa ou retorna o titulo.\nNome: Titulo")]
        [DisplayName("1) Titulo")]
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
                this.groupControl1.Text = _titulo;
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna a Tipo.
        /// </summary>
        [Description("Informa ou retorna a Tipo.\nNome: Tipo")]
        [DisplayName("2) Tipo")]
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [DefaultValue(eTipo.AlfaNumerico)]
        [RefreshProperties(RefreshProperties.All)]
        public eTipo Tipo
        {
            get
            {
                _informouTipo = true;
                return _tipo;
            }
            set
            {
                _tipo = value;
                _informouTipo = true;
                AtualizarLayOut();
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna o tipo da mascara.
        /// </summary>
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("3) Tipo de mascara")]
        [Description("Informa ou retorna o tipo da mascara.\nNome: ClassMascaras")]
        [EditorAttribute(typeof(TipoMascarasEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        public ClassMascaras ClassMascaras
        {
            get { return _classMascaras; }
            set
            {
                _classMascaras = value;
                ValidaMascara();
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o browse de pesquisa selecionado.
        /// </summary>
        /// 
        [Category(Util.Constantes.CATEGORIA)]
        [DisplayName("4) Browser de pesquisa")]
        [Description("Informa ou retorna o browse de pesquisa selecionado.\nNome: BrowseDePesquisa")]
        [EditorAttribute(typeof(SelecionaTelaEditor), typeof(UITypeEditor))]
        [DefaultValue(Constantes.NONE_DEFAULT)]
        [RefreshProperties(RefreshProperties.All)]
        public string BrowserDePesquisa
        {
            get { return _browserDePesquisa; }
            set
            {

                _browserDePesquisa = value;
                AtualizarLayOut();
            }
        }

        /// <summary>
        /// (Set\Get) Informa ou retorna a coluna de retorno.
        /// </summary>
        [Description("Informa ou retorna a coluna de retorno.\nNome: ColunaRetorno")]
        [DisplayName("5) Coluna retorno")]
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        public string ColunaRetorno
        {
            get { return _colunaRetorno; }
            set { _colunaRetorno = value; }
        }

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [DefaultValue(2)]
        public int CasasDecimais
        {
            get { return _casasDecimais; }
            set { _casasDecimais = value; }
        }

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [DefaultValue(0)]
        public int TamanhoCampo
        {
            get
            {
                return this.btEdtBGMInicial.Properties.MaxLength;
            }
            set
            {
                this.btEdtBGMInicial.Properties.MaxLength = value;
            }
        }

        #endregion

        #region Metodos controle

        private void FiltroRelatorioBGM_Load(object sender, EventArgs e)
        {
            try
            {
                _informouTipo = true;
                AtualizarLayOut();
                if (DesignMode && String.IsNullOrEmpty(_titulo) && this.ToString().IndexOf(' ') != -1)
                    this.Titulo = this.ToString().Substring(0, this.ToString().IndexOf(' '));
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex));
            }

        }

        /// <summary>
        /// Redefine os limites do controle, pra bloquear o redimensionamento da altura.
        /// </summary>
        /// <param name="x">Limite esquerdo</param>
        /// <param name="y">Limite topo</param>
        /// <param name="width">Limite largura</param>
        /// <param name="height">Limite altura</param>
        /// <param name="specified">Objeto do tipo BoundsSpecified</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, base.MinimumSize.Height, specified);
        }

        #endregion

        #region Metodos

        private void AtualizarLayOut()
        {
            try
            {
                if (_informouTipo)
                {
                    this.smplBtnSelecaoAleatoria.Visible = !String.IsNullOrEmpty(_browserDePesquisa) ||
                                                           _tipo.Equals(eTipo.Data);

                    this.btEdtBGMInicial.Visible = _tipo.Equals(eTipo.AlfaNumerico);
                    this.btEdtBGMFinal.Visible = this.btEdtBGMInicial.Visible;
                    this.btEdtBGMInicial.Properties.Buttons[0].Visible = this.smplBtnSelecaoAleatoria.Visible;
                    this.btEdtBGMFinal.Properties.Buttons[0].Visible = this.smplBtnSelecaoAleatoria.Visible;

                    this.btEdtBGMInicial.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    this.btEdtBGMInicial.Properties.Buttons[0].Image = global::FGlobus.Componentes.WinForms.Resources.ImagensBGM.ButtonEditBGMLupa;
                    this.btEdtBGMFinal.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    this.btEdtBGMFinal.Properties.Buttons[0].Image = global::FGlobus.Componentes.WinForms.Resources.ImagensBGM.ButtonEditBGMLupa;

                    this.clcEdtBGMInicial.Visible = _tipo.Equals(eTipo.Numerico);
                    this.clcEdtBGMFinal.Visible = this.clcEdtBGMInicial.Visible;

                    this.dtEdtBGMInicial.Visible = _tipo.Equals(eTipo.Data);
                    this.dtEdtBGMFinal.Visible = this.dtEdtBGMInicial.Visible;

                    this.btEdtBGMInicial.Dock = DockStyle.Fill;
                    this.btEdtBGMFinal.Dock = DockStyle.Fill;

                    this.clcEdtBGMInicial.Dock = DockStyle.Fill;
                    this.clcEdtBGMFinal.Dock = DockStyle.Fill;

                    this.dtEdtBGMInicial.Dock = DockStyle.Fill;
                    this.dtEdtBGMFinal.Dock = DockStyle.Fill;

                    this.tblLytPnlFiltroRelatorioBGM.ColumnStyles.OfType<ColumnStyle>().LastOrDefault().Width = this.smplBtnSelecaoAleatoria.Visible
                        ? 30
                        : 0;

                    ValidaMascara();

                    this.dtEdtBGMInicial.EditValue = DateTime.Now;
                    this.dtEdtBGMFinal.EditValue = DateTime.Now;

                    this.clcEdtBGMInicial.EditValue = 0;
                    this.clcEdtBGMFinal.EditValue = 9;

                    this.btEdtBGMInicial.EditValue = "!";
                    this.btEdtBGMFinal.EditValue = "ZZ";

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(String.Concat(ex));
            }
        }

        private void ValidaMascara()
        {
            if (_classMascaras != null && !String.IsNullOrEmpty(_classMascaras.Tipo))
            {
                Func<ButtonEdit, bool> delegateAtualiza =
                    delegate(ButtonEdit buttonEdit)
                    {
                        MaskProperties maskProperties = new MaskProperties();
                        maskProperties.UseMaskAsDisplayFormat = true;
                        maskProperties.EditMask = _classMascaras.EditMask;
                        maskProperties.MaskType = _classMascaras.MaskType;

                        switch (_tipo)
                        {
                            case eTipo.Data:
                                buttonEdit.Properties.DisplayFormat.FormatString = String.Empty;
                                buttonEdit.Properties.DisplayFormat.FormatType = FormatType.None;
                                buttonEdit.Properties.EditFormat.FormatString = String.Empty;
                                buttonEdit.Properties.EditFormat.FormatType = FormatType.None;
                                break;
                            case eTipo.AlfaNumerico:
                                maskProperties.PlaceHolder = '_';
                                buttonEdit.Properties.PasswordChar = char.MinValue;

                                switch (_classMascaras.Codigo)
                                {
                                    case 23:
                                        maskProperties.PlaceHolder = _classMascaras.PlaceHolder;
                                        break;
                                    case 24:
                                        buttonEdit.Properties.PasswordChar = _classMascaras.PasswordChar;
                                        break;
                                    case 25:
                                        maskProperties.EditMask = String.Concat("\\d{0,", this.btEdtBGMInicial.Properties.MaxLength, "}");
                                        break;
                                    case 26:
                                        maskProperties.EditMask = String.Concat("([0-9]|[.]){0,", this.btEdtBGMInicial.Properties.MaxLength, "}");
                                        break;
                                    default:
                                        this.btEdtBGMInicial.Properties.MaxLength = 0;
                                        break;
                                }
                                break;
                            case eTipo.Numerico:
                                maskProperties.EditMask = _classMascaras.Codigo.Equals(1)
                                    ? _classMascaras.EditMask
                                    : String.Concat(_classMascaras.EditMask, _casasDecimais);

                                break;
                        }

                        buttonEdit.Properties.Mask.Assign(maskProperties);

                        return true;
                    };

                switch (_tipo)
                {
                    case eTipo.Data:
                        delegateAtualiza(this.dtEdtBGMInicial);
                        delegateAtualiza(this.dtEdtBGMFinal);
                        break;
                    case eTipo.AlfaNumerico:
                        delegateAtualiza(this.btEdtBGMInicial);
                        delegateAtualiza(this.btEdtBGMFinal);
                        break;
                    case eTipo.Numerico:
                        delegateAtualiza(this.clcEdtBGMInicial);
                        delegateAtualiza(this.clcEdtBGMFinal);
                        break;
                }
            }

        }

        #endregion

        private void smplBtnSelecaoAleatoria_Click(object sender, EventArgs e)
        {
            // new TelaCalendario(this.dtEdtBGMInicial.DateTime, this.dtEdtBGMFinal.DateTime).ShowDialog(this);
            new TelaCalendario().ShowDialog(this);
        }
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    internal class ControlaComponenteDesigner1 : ControlDesigner
    {
        private FiltroRelatorioBGM _controle;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            _controle = this.Control as FiltroRelatorioBGM;
        }

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            try
            {
                base.PreFilterProperties(properties);

                PropertyDescriptor selectProp = (PropertyDescriptor)properties["ColunaRetorno"];
                properties["ColunaRetorno"] = TypeDescriptor.CreateProperty(
                    selectProp.ComponentType,
                    selectProp,
                    String.IsNullOrEmpty(_controle.BrowserDePesquisa)
                        ? BrowsableAttribute.No
                        : BrowsableAttribute.Yes);

                selectProp = (PropertyDescriptor)properties["TamanhoCampo"];
                properties["TamanhoCampo"] = TypeDescriptor.CreateProperty(
                    selectProp.ComponentType,
                    selectProp,
                    _controle.Tipo.Equals(FiltroRelatorioBGM.eTipo.Data)
                        ? BrowsableAttribute.No
                        : BrowsableAttribute.Yes);

                selectProp = (PropertyDescriptor)properties["CasasDecimais"];
                properties["CasasDecimais"] = TypeDescriptor.CreateProperty(
                    selectProp.ComponentType,
                    selectProp,
                    _controle.Tipo.Equals(FiltroRelatorioBGM.eTipo.Numerico)
                        ? BrowsableAttribute.Yes
                        : BrowsableAttribute.No);
            }
            catch (Exception)
            {
            }
        }

        #region Criar propriedade


        //public Color TestColor
        //{
        //    get
        //    { return this.intcolor; }
        //    set
        //    { this.intcolor = value; }
        //}
        //private Color intcolor = Color.Azure;
        //protected override void PreFilterProperties(System.Collections.IDictionary properties)
        //{
        //    base.PreFilterProperties(properties);
        //    properties["TestColor"] = TypeDescriptor.CreateProperty(
        //        typeof(ControlaComponenteDesigner1),
        //        "TestColor",
        //        typeof(System.Drawing.Color),
        //        DesignOnlyAttribute.Yes);
        //}      


        #endregion

        #region Mostrar bordar em volta do componente

        //private bool mouseover;
        //protected override void OnMouseEnter()
        //{
        //    this.mouseover = true;
        //}
        //protected override void OnMouseLeave()
        //{
        //    this.mouseover = false;
        //}
        //protected override void OnPaintAdornments(PaintEventArgs pe)
        //{
        //    if (this.mouseover)
        //    {
        //        pe.Graphics.DrawRectangle(
        //            new Pen(new SolidBrush(Color.Red), 1),
        //            0,
        //            0,
        //            this.Control.Size.Width - 1,
        //            this.Control.Size.Height - 1);
        //    }
        //}

        #endregion
    }
}

