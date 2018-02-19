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

        public List<AlertaMenuprincipal> RetornarAlertas
        {
            get
            {
                List<AlertaMenuprincipal> lista = new List<AlertaMenuprincipal>();
                lista.Add(new AlertaMenuprincipal("1", "xxxxx 1", "xxxxx 1"));
                lista.Add(new AlertaMenuprincipal("2", "xxxxx 2", "xxxxx 2"));
                lista.Add(new AlertaMenuprincipal("3", "xxxxx 3", "xxxxx 3"));
                lista.Add(new AlertaMenuprincipal("4", "xxxxx 4", "xxxxx 4"));
                lista.Add(new AlertaMenuprincipal("5", "xxxxx 5", "xxxxx 5"));
                return lista;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AlertaMenuprincipal alertaMenuprincipal = (e.Source as ToggleButton).DataContext as AlertaMenuprincipal;
            if (alertaMenuprincipal != null)
            {
                MessageBox.Show(alertaMenuprincipal.Titulo + "\n" + alertaMenuprincipal.Dll);
            }
        }
    }


    public class AlertaMenuprincipal
    {
        #region Construtor

        public AlertaMenuprincipal(string titulo, string descricao, string dll)
        {
            _titulo = titulo;
            _descricao = descricao;
            _dll = dll;
        }

        public AlertaMenuprincipal(string titulo, string dll)
        {
            _titulo = titulo;
            _dll = dll;
        }

        #endregion

        #region Atributos

        private string _titulo;
        private string _descricao;
        private string _dll;

        #endregion

        #region override

        public override string ToString()
        {
            return _dll;
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// 
        /// </summary>
        public string Titulo { get { return _titulo; } }

        /// <summary>
        /// 
        /// </summary>
        public string Descricao { get { return _descricao; } }

        /// <summary>
        /// 
        /// </summary>
        public string Dll { get { return _dll; } }

        #endregion

    }
}
