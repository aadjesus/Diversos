using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using DevExpress.XtraEditors;
using System.Linq.Expressions;
using System.Drawing.Drawing2D;

namespace Sessions_WebService
{
    public partial class MasterFormMensagem : DevExpress.XtraEditors.XtraForm
    {
        #region Construtor

        public MasterFormMensagem()
        {
            InitializeComponent();
        }

        public MasterFormMensagem(IEnumerable<string> mensagem, Mensagem.eTipoMensagem tipoMensagem, string titulo = null)
        {
            InitializeComponent();
            _mensagem = mensagem;
            _titulo = titulo;
            _tipoMensagem = tipoMensagem;

            //ComponentResourceManager resources1 = new ComponentResourceManager(typeof(Form1));            
            //resources1.GetString("xxxxxxxxxxxxxxxxx##")
        }

        #endregion

        #region Atributos

        private bool _mover;
        private Point _point;
        private IEnumerable<string> _mensagem;
        private string _titulo;
        private Mensagem.eTipoMensagem _tipoMensagem;
        private bool _focaBotaoNao = false;

        private const int MINIMO_ALTURA = 40;
        private const int MINIMO_LARGURA = 300;

        #endregion

        #region Metodos

        private void MensagemBGM_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                _mover = true;
                _point = new Point(e.X, e.Y);
            }
            else
                _mover = false;

        }

        private void MensagemBGM_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mover)
            {
                Point pointMoveTo = this.PointToScreen(new Point(e.X, e.Y));
                pointMoveTo.Offset(-_point.X, -_point.Y);
                this.Location = pointMoveTo;
            }

        }

        private void MensagemBGM_MouseUp(object sender, MouseEventArgs e)
        {
            _mover = false;
        }

        private void MensagemBGM_Load(object sender, EventArgs e)
        {
            try
            {
                Size tamanhoMaximo = FGlobus.Util.Util.RetornarTamanhoDoForm(30);

                // Pega o valor da maior linha
                Size tamanhoTextoEmPixels = _mensagem
                    .Where(w => w.Length > 0)
                    .Select(s => TextRenderer.MeasureText(s, this.lblCtrlTextoMensagem.Font))
                    .Aggregate(new Size(), (a, b) => a = (a.Width > b.Width
                                                            ? a
                                                            : b));

                // Retorna o minimo entre, o maximo da tela ou o maximo entre o minimo ou o maior texto
                int larguraTexto = Math.Min(tamanhoMaximo.Width, Math.Max(MINIMO_LARGURA, tamanhoTextoEmPixels.Width));

                // Retorna o minimo entre, o maximo da tela ou o maximo entre o minimo ou a maior qtde de linhas
                int alturaTexto = Math.Min(tamanhoMaximo.Height, Math.Max(MINIMO_ALTURA, pnlCtrlBotoes.Height + (tamanhoTextoEmPixels.Height * _mensagem.Count())));

                this.lblCtrlTextoMensagem.Text = _mensagem.Aggregate((a, b) => a + "\r\n" + b);

                bool scrollBarsVertical = _mensagem.Count() > 47;
                bool scrollBarsHorizontal = tamanhoTextoEmPixels.Width > 1016; // + ou - 170 caracteres, pq depende dos pixels desses caracteres.

                // Define ScrollBars conforme o tamnho da mensagem
                this.lblCtrlTextoMensagem.Properties.ScrollBars = scrollBarsVertical && scrollBarsHorizontal
                    ? ScrollBars.Both
                    : scrollBarsVertical
                        ? ScrollBars.Vertical
                        : scrollBarsHorizontal
                            ? ScrollBars.Horizontal
                            : ScrollBars.None;

                this.lblCtrlTitulo.Text = _titulo;
                this.pnlCtrlTitulo.Visible = !String.IsNullOrEmpty(_titulo);

                int bordaEPadding = this.pnlCtrlTextoMensagem.Padding.All;

                this.Size = new Size(
                    larguraTexto + bordaEPadding + this.pctrEdtImagem.Width + 10,
                    alturaTexto + bordaEPadding + this.pnlCtrlBotoes.Height + (this.pnlCtrlTitulo.Visible ? this.pnlCtrlTitulo.Height : 0));

                this.smplBtnNao.Visible = _tipoMensagem.Equals(Mensagem.eTipoMensagem.Pergunta);
                this.smplBtnSim.Visible = this.smplBtnNao.Visible;
                this.smplBtnOk.Visible = !this.smplBtnSim.Visible;

                this.smplBtnOk.DialogResult = DialogResult.Yes;
                this.smplBtnSim.DialogResult = DialogResult.Yes;
                this.smplBtnNao.DialogResult = DialogResult.No;                

                this.ActiveControl = this.smplBtnOk.Visible
                    ? this.smplBtnOk
                    : this.smplBtnNao;

                //this.pnlCtrlMensagem.Appearance.BackColor2 = Color.Transparent;
                //this.pnlCtrlMensagem.Appearance.BackColor = Color.Transparent;
                //this.pnlCtrlImagem.Appearance.BackColor = Color.Black;

                switch (_tipoMensagem)
                {
                    case Mensagem.eTipoMensagem.Erro:
                        this.pctrEdtImagem.Image = this.imgClctn.Images[0];
                        this.labelControl3.Text = "X";
                        //this.pnlCtrlImagem.Appearance.BackColor2 = Color.Red;
                        break;
                    case Mensagem.eTipoMensagem.Informacao:
                        this.pctrEdtImagem.Image = this.imgClctn.Images[1];
                        this.labelControl3.Text = "i";
                        //this.pnlCtrlImagem.Appearance.BackColor2 = Color.Yellow;
                        break;
                    case Mensagem.eTipoMensagem.Pergunta:
                        this.pctrEdtImagem.Image = this.imgClctn.Images[2];
                        this.labelControl3.Text = "?";
                        //this.pnlCtrlImagem.Appearance.BackColor2 = Color.Blue;                        

                        break;
                    case Mensagem.eTipoMensagem.CaixaDeEntrada:
                        this.pctrEdtImagem.Visible = false;
                        //this.pnlCtrlImagem.Appearance.BackColor2 = SystemColors.Control;
                        break;
                    case Mensagem.eTipoMensagem.Interna:
                        this.pctrEdtImagem.Visible = false;
                        //this.pnlCtrlMensagem.Appearance.BackColor = SystemColors.Control;
                        //this.pnlCtrlImagem.Appearance.BackColor2 = SystemColors.Control;
                        break;
                }

                this.Location = new Point(
                    (Math.Abs(FGlobus.Util.Util.ResolucaoMonitor.Width - this.Width) / 2),
                    (Math.Abs(FGlobus.Util.Util.ResolucaoMonitor.Height - this.Height) / 2));
            }
            catch (Exception ex)
            {
                Mensagem.Informacao.Abrir(ex.Message);

            }


        }

        #endregion

        #region Mover formulario

        //private const int WM_NCHITTEST = 0x84;
        //private const int HTCAPTION = 0x2;
        //private const int HTCLIENT = 0x1;
        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == WM_NCHITTEST && m.Result == new IntPtr(HTCLIENT))
        //    {
        //        m.Result = new IntPtr(HTCAPTION);
        //    }
        //}

        #endregion

        #region Desenha texto

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    // Call base class OnPaint method
        //    base.OnPaint(e);

        //    // Method under System.Drawing.Graphics
        //    e.Graphics.DrawString("Hello Mum!",
        //                           new Font(this.Font.FontFamily,
        //                                    24,
        //                                    this.Font.Style | FontStyle.Bold),
        //                           new SolidBrush(Color.Tomato),
        //                           40,
        //                           40);
        //}

        #endregion

        //public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        //{
        //    if (value.CompareTo(max) > 0)
        //        return max;

        //    if (value.CompareTo(min) < 0)
        //        return min;

        //    return value;
        //}

        private void Teste_NomeMetodo()
        {
            
        }

    }

    

    /// <summary>
    /// Interface para as mensagem
    /// </summary>
    public interface IMensagem
    {
        IMensagem Linha(string textoMensagem);
        IMensagem PularLinha(int qtdeLinhas = 1);
        IMensagem Titulo(string titulo);
        bool Abrir(string textoMensagem = null);
        bool VerificaMensagem(Exception excecao, params string[] mensagens);
    }

    public class Mensagem : IMensagem
    {
        #region Construtor

        public Mensagem()
        {
        }

        public Mensagem(eTipoMensagem tipoMensagem)
        {
            _tipoMensagem = tipoMensagem;
        }

        public Mensagem(string mensagem)
        {
            _linhasMensagem.Add(mensagem);
        }

        public Mensagem(List<string> linhasMensagem)
        {
            _linhasMensagem.AddRange(linhasMensagem);
        }

        #endregion

        #region Atributos

        private List<string> _linhasMensagem = new List<string>();
        private eTipoMensagem _tipoMensagem = eTipoMensagem.Informacao;
        private string _titulo;

        /// <summary>
        /// Estrutura com os tipos de mensagens
        /// </summary>
        public enum eTipoMensagem
        {
            [Description("Erro")]
            Erro,
            [Description("Informação")]
            Informacao,
            [Description("Pergunta")]
            Pergunta,
            [Description("Caixa de entrada")]
            CaixaDeEntrada,
            [Description("Interna")]
            Interna
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Mensagem do tipo informacao
        /// </summary>
        public static Mensagem Informacao
        {
            get
            {
                return new Mensagem(eTipoMensagem.Informacao);
            }
        }

        /// <summary>
        /// Mensagem do tipo Pergunta
        /// </summary>
        public static Mensagem Pergunta
        {
            get
            {
                return new Mensagem(eTipoMensagem.Pergunta);
            }
        }

        /// <summary>
        /// Mensagem do tipo Erro
        /// </summary>
        public static Mensagem Erro
        {
            get
            {
                return new Mensagem(eTipoMensagem.Erro);
            }
        }

        /// <summary>
        /// Mensagem do tipo CaixaDeEntrada
        /// </summary>
        public static Mensagem CaixaDeEntrada
        {
            get
            {
                return new Mensagem(eTipoMensagem.CaixaDeEntrada);
            }
        }

        #endregion

        #region Metodos

        private void QuebraMsgEmLinhas(string textoMensagem)
        {
            string[] lista = textoMensagem.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            if (lista.Count() > 0)
                _linhasMensagem.AddRange(lista);
            else
                _linhasMensagem.Add(textoMensagem);
        }

        #endregion

        #region override

        public override string ToString()
        {
            return FGlobus.Util.Util.GetDescricaoEnum(_tipoMensagem);
        }

        #endregion

        #region IMensagem Members

        public IMensagem Linha(string textoMensagem)
        {
            QuebraMsgEmLinhas(textoMensagem);

            return this;
        }

        public IMensagem PularLinha(int qtdeLinhas = 1)
        {
            Enumerable
                .Repeat<string>("", qtdeLinhas)
                .ToList()
                .ForEach(f => _linhasMensagem.Add(""));

            return this;
        }

        public IMensagem Titulo(string titulo)
        {
            _titulo = titulo
                .Replace("\r\n", "")
                .Replace("\n", "");

            return this;
        }

        public bool Abrir(string textoMensagem = null)
        {
            if (!String.IsNullOrEmpty(textoMensagem))
                QuebraMsgEmLinhas(textoMensagem);

            if (_linhasMensagem.Count.Equals(0))
                new MasterFormMensagem(new string[] { "# ERRO # Não foi informado o texto da mensagem." }, eTipoMensagem.Interna)
                    .ShowDialog();
            else
                new MasterFormMensagem(_linhasMensagem, _tipoMensagem, _titulo)
                    .ShowDialog();

            return true;
        }

        public bool VerificaMensagem(Exception excecao, params string[] mensagens)
        {
            return true;
        }

        #endregion
    }
}
