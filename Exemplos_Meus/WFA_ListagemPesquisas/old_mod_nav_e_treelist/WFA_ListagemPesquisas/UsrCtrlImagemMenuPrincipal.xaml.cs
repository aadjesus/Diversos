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
using FGlobus.Componentes.WinForms;
using System.Windows.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DevExpress.XtraBars.Docking;
using System.Globalization;
using FGlobus.Componentes.WinForms.ws.controle;
using System.Windows.Controls.Primitives;

namespace WFA_ListagemPesquisas
{
    /// <summary>
    /// Interaction logic for UsrCtrlImagemMenuPrincipal.xaml
    /// </summary>
    [ToolboxItem(false)]
    public partial class UsrCtrlImagemMenuPrincipal : UserControl
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public UsrCtrlImagemMenuPrincipal()
        {
            InitializeComponent();
        }

        #endregion

        #region Atributos

        #endregion

        #region Métodos

        private void CriaSinalDeMaisRandom(double tempo)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(tempo);

            timer.Tick += delegate(object sender, EventArgs e)
            {
                Random random = new Random();
                int randomWidth = random.Next(0, (int)this.ActualWidth);
                int randomHeight = random.Next(0, (int)this.ActualHeight);
                int tamanho = random.Next(5, 50);

                UsrCtrlPathMais usrCtrlPathMais = new UsrCtrlPathMais()
                {
                    Width = tamanho,
                    Height = tamanho,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(randomWidth, randomHeight, 0, 0),
                };
                usrCtrlPathMais.IniciaStryBrd();

                this.LayoutRoot.Children.Add(usrCtrlPathMais);
            };
            timer.Start();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
                CriaSinalDeMaisRandom(2000);
                CriaSinalDeMaisRandom(2500);
                CriaSinalDeMaisRandom(3000);
        }

        private void MensagemItem_DoubleClick(object sender, RoutedEventArgs e)
        {
          
        }

        /// <summary>
        /// <para>Método responsável pelas regras no click do 'listBoxTps'.</para>
        /// <para>Este método foi modificado para 'public' para fazer uma validação no 'MasterMenuPrincipal'.</para>
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void listBoxTps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
        }

        private void usrCtrlImagemMenuPrincipal_LayoutUpdated(object sender, EventArgs e)
        {
           
        }

        private void ToggleButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            

        }

        #endregion
    }
}
