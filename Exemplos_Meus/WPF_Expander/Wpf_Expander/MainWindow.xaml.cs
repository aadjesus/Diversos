using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Threading;
using System.Reflection;

namespace Wpf_Expander
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AlertaMenuprincipal alertaMenuprincipal = (e.Source as ToggleButton).DataContext as AlertaMenuprincipal;
            if (alertaMenuprincipal != null)
            {
                MessageBox.Show(alertaMenuprincipal.ItemMenu.Caption + "\n" + alertaMenuprincipal.ItemMenu.AssemblyName);

                //AlertaMenuprincipal.Alertas
                //    .Where(w=> w.ItemMenu.Equals(alertaMenuprincipal.ItemMenu))
                //    TiposAlertas.ToList()
                //    .ForEach(f => f.Alertar = true);

                e.Handled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AlertaMenuprincipal.MostrarAlertas(new MasterDockPanel[] 
            { 
                new MasterDockPanel()
                {
                    TipoAlerta = (eTipoAlerta)this.comboBox.SelectedValue,
                    ItemMenu = new ItemMenu()
                    {
                        Caption = "Caption",
                        Descricao = "Descricao" + AlertaMenuprincipal.Alertas.Count.ToString(),
                        AssemblyName = "AssemblyName", 
                        NomeClass = "NomeClass"+ AlertaMenuprincipal.Alertas.Count.ToString(),
                        NomeItemMenu ="NomeItemMenu" + this.comboBox.SelectedValue.ToString()
                    }
                }
            });
        }
        



        
    }

    #region Classes

    /// <summary>
    /// Classe para controlar o tipo de alerta
    /// </summary>
    [ToolboxItem(false)]
    public class TipoAlertaMenuprincipal : DependencyObject, INotifyPropertyChanged
    {
        #region Construtor

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="tipoAlerta">Tipo de alerta</param>
        public TipoAlertaMenuprincipal(eTipoAlerta tipoAlerta, int qtde = 0)
        {
            _tipoAlerta = tipoAlerta;

            TipoAlertaAttribute attributes = tipoAlerta.GetType().GetField(tipoAlerta.ToString()).GetCustomAttributes(typeof(TipoAlertaAttribute), false)
                .FirstOrDefault() as TipoAlertaAttribute;
            _descricao = attributes.Descricao;
            _cor = attributes.Cor;
            _qtde = qtde;
        }

        #endregion

        #region Atributos

        private eTipoAlerta _tipoAlerta;
        private int _qtde;
        private System.Windows.Media.Brush _cor;
        private string _descricao;
        private bool _alertar = true;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Get) Retornar a descrição
        /// </summary>
        public string Descricao { get { return _descricao; } }

        /// <summary>
        /// (Get) Retornar o tipo de alerta
        /// </summary>
        public eTipoAlerta TipoAlerta { get { return _tipoAlerta; } }

        /// <summary>
        /// (Get) Retornar a quantidade
        /// </summary>
        public int Qtde
        {
            get
            {
                return _qtde;
            }
            set
            {
                _qtde = value;
            }
        }

        /// <summary>
        /// (Get) Retornar a cor
        /// </summary>
        public System.Windows.Media.Brush Cor { get { return _cor; } }

        /// <summary>
        /// (Get) Retornar se alerta
        /// </summary>
        public bool Alertar
        {
            //get { return _alertar; }
            //set
            //{
            //    _alertar = value;
            //}

            get { return (bool)GetValue(DayModeProperty); }
            set
            {
                SetValue(DayModeProperty, value);
                OnPropertyChanged("Alertar");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }


        #endregion

        public static readonly DependencyProperty DayModeProperty =
            DependencyProperty.Register("Alertar", typeof(bool), typeof(TipoAlertaMenuprincipal), new UIPropertyMetadata(false));

        public event PropertyChangedEventHandler PropertyChanged;

    }

    /// <summary>
    /// Classe para controlar os alertas do menu pricipal
    /// </summary>
    [ToolboxItem(false)]
    public class AlertaMenuprincipal
    {
        #region Construtor

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="masterDockPanel">Objeto do tipo MasterDockPanel</param>
        public AlertaMenuprincipal(MasterDockPanel masterDockPanel)
        {
            _itemMenu = masterDockPanel.ItemMenu;
            _tipoAlertaMenuprincipal = new TipoAlertaMenuprincipal(masterDockPanel.TipoAlerta);
            _masterDockPanel = masterDockPanel;
        }

        /// <summary>
        /// Construtor default
        /// </summary>
        public AlertaMenuprincipal() { }

        #endregion

        #region Atributos

        private ItemMenu _itemMenu;
        private TipoAlertaMenuprincipal _tipoAlertaMenuprincipal;
        private static ObservableCollection<TipoAlertaMenuprincipal> _listaTipoAlertaMenuprincipal = new ObservableCollection<TipoAlertaMenuprincipal>();
        private static ObservableCollection<AlertaMenuprincipal> _listaAlertaMenuprincipal = new ObservableCollection<AlertaMenuprincipal>();
        private MasterDockPanel _masterDockPanel;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Get) Retornar o tipo de alerta
        /// </summary>
        public TipoAlertaMenuprincipal TipoAlertaMenuprincipal { get { return _tipoAlertaMenuprincipal; } }

        /// <summary>
        /// (Get) Retornar o item de menu
        /// </summary>
        public ItemMenu ItemMenu { get { return _itemMenu; } }

        /// <summary>
        /// (Get) Retornar o MasterDockPanel
        /// </summary>
        public MasterDockPanel MasterDockPanel { get { return _masterDockPanel; } }

        /// <summary>
        /// (Get) Retorna os alertas
        /// </summary> 
        public static ObservableCollection<AlertaMenuprincipal> Alertas
        {
            get
            {
                return AlertaMenuprincipal._listaAlertaMenuprincipal;
            }
        }

        /// <summary>
        /// (Get) Retorna os tipos de alertas
        /// </summary>
        public static ObservableCollection<TipoAlertaMenuprincipal> TiposAlertas
        {
            get
            {
                return AlertaMenuprincipal._listaTipoAlertaMenuprincipal;
            }
        }

        #endregion


        #region Metodos

        /// <summary>
        /// Mostra os alertas
        /// </summary>
        /// <param name="listaMasterDockPanel">Lista de objetos do tipo MasterDockPanel.</param>
        internal static void MostrarAlertas(IEnumerable<MasterDockPanel> listaMasterDockPanel)
        {
            //AlertaMenuprincipal.RetornarAlertas.Clear();
            listaMasterDockPanel
                .Select(s => new AlertaMenuprincipal(s))
                .ToList()
                .ForEach(f =>
                {
                    if (AlertaMenuprincipal.Alertas
                        .Where(w => w.ItemMenu.Equals(f.ItemMenu))
                        .Count().Equals(0))
                        AlertaMenuprincipal.Alertas.Add(f);
                });

            AlertaMenuprincipal.TiposAlertas.Clear();
            AlertaMenuprincipal.Alertas
                .GroupBy(g => g.TipoAlertaMenuprincipal.TipoAlerta)
                .Select(s => new TipoAlertaMenuprincipal(s.Key, s.Count()))
                .ToList()
                .ForEach(f => AlertaMenuprincipal.TiposAlertas.Add(f));
        }

        #endregion
    }

    /// <summary>
    /// Classe para quardar as informações do menu selecionado
    /// </summary>
    public class ItemMenu
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public ItemMenu()
        { }

        /// <summary>
        /// Construtor que popula as propriedades conforme as informações do XML do menu principal.
        /// </summary>
        /// <param name="campos">Lista de objetos do tipo XElement</param>
        /// <param name="itemMenu">Se o item é um item de menu</param>
        public ItemMenu(IEnumerable<System.Xml.Linq.XElement> campos, bool itemMenu = true)
        {

        }

        #endregion

        #region Atributos

        private string _nomeClass;
        private string _assemblyName;
        private string _nomeItemMenu;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Set/Get) Informa ou retorna o nome do menu selecionado.
        /// </summary>
        public string NomeItemMenu
        {
            get { return _nomeItemMenu; }
            set { _nomeItemMenu = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o caption do menu selecionado.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna a descrição do menu selecionado.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna o icone do menu selecionado.
        /// </summary>
        public string Icone { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna o nome da classe que sera aberta.
        /// </summary>
        public string NomeClass
        {
            get { return _nomeClass; }
            set { _nomeClass = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o nome assembly que está a classe informada.
        /// </summary>
        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna a data de inclusão do menu.
        /// </summary>
        public string DataInclusao { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna o usuario que incluiu o menu.
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna o 1º nivel do menu.
        /// </summary>
        public string Nivel1 { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna o 2º nivel do menu.
        /// </summary>
        public string Nivel2 { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna parametro da abertura da tela.
        /// </summary>
        public string Parametros { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna os níveis que foram do acesso do item.
        /// </summary>
        public string Niveis { get; set; }

        /// <summary>
        /// (Set/Get) Informa ou retorna o MenuUsuarioDTO associado ao menu.
        /// </summary>
        public MenuUsuarioDTO MenuUsuario { get; set; }

        #endregion

        #region Operador

        /// <summary>
        /// Operador que verifica se os objetos são iguais pelas propriedades:
        /// AssemblyName, NomeClass, NomeItemMenu
        /// </summary>
        /// <param name="origem">Classes origem</param>
        /// <param name="destino">Classes destino</param>
        /// <returns>bool</returns>
        public static bool operator ==(ItemMenu origem, ItemMenu destino)
        {
            try
            {
                return origem.AssemblyName.Equals(destino.AssemblyName) &&
                       origem.NomeClass.Equals(destino.NomeClass) &&
                       origem.NomeItemMenu.Equals(destino.NomeItemMenu);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Operador que verifica se os objetos são diferentes pelas propriedades:
        /// AssemblyName, NomeClass, NomeItemMenu
        /// </summary>
        /// <param name="origem">Classes origem</param>
        /// <param name="destino">Classes destino</param>
        /// <returns>bool</returns>
        public static bool operator !=(ItemMenu origem, ItemMenu destino)
        {
            return !(origem == destino);
        }

        #endregion

        #region Override

        /// <summary>
        /// Verifica se objeto informado é igual ao objeto atual.
        /// </summary>
        /// <param name="obj">Objeto que vai ser comparado.</param>
        /// <returns><see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;

            return this == (obj as ItemMenu);
        }

        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * _assemblyName.GetHashCode();
            hash = 27 * hash * _nomeClass.GetHashCode();
            hash = 27 * hash * _nomeItemMenu.GetHashCode();
            return hash;
        }

        #endregion
    };

    #endregion

    #region Classe apara tratar os atributos do enum eTipoAlerta

    /// <summary>
    /// Atributo para os tipos de alrtas
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal sealed class TipoAlertaAttribute : Attribute
    {
        #region Construtor

        /// <summary>
        /// Construtor defalt
        /// </summary>
        /// <param name="descricao">Descrição</param>
        /// <param name="cor">Cor</param>
        public TipoAlertaAttribute(string descricao, string cor)
        {
            _descricao = descricao;
            _cor = new System.Windows.Media.BrushConverter().ConvertFrom(cor) as System.Windows.Media.Brush;
        }

        #endregion

        #region Atributos

        private readonly string _descricao;
        private readonly System.Windows.Media.Brush _cor;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Get) Retornar a descrição
        /// </summary>
        public string Descricao
        {
            get { return _descricao; }
        }

        /// <summary>
        /// (Get) Retornar a cor
        /// </summary>
        public System.Windows.Media.Brush Cor
        {
            get { return _cor; }
        }

        #endregion
    }

    #endregion

    #region Enum eTipoAlerta

    /// <summary>
    /// Enum para controlar os tipos de alertas
    /// </summary>
    public enum eTipoAlerta
    {
        /// <summary>
        /// Alerta
        /// </summary>
        [TipoAlerta("Alerta", "Green")]
        Alerta,

        /// <summary>
        /// Notificacao
        /// </summary>
        [TipoAlerta("Notificacao", "Red")]
        Notificacao,

        /// <summary>
        /// Evento
        /// </summary>
        [TipoAlerta("Evento", "Yellow")]
        Evento,

        /// <summary>
        /// Mensagem
        /// </summary>
        [TipoAlerta("Mensagem", "Blue")]
        Mensagem
    }

    #endregion


    public class MenuUsuarioDTO
    {

    }
}
