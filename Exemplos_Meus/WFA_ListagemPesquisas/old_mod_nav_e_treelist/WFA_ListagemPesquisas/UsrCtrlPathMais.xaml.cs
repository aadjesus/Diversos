using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace WFA_ListagemPesquisas
{
    /// <summary>
    /// Interaction logic for UsrCtrl1Mais.xaml
    /// </summary>
    [ToolboxItem(false)]
    public partial class UsrCtrlPathMais : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UsrCtrlPathMais()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Executa o metodo 'Begin' do Storyboard do objeto.
        /// </summary>
        public void IniciaStryBrd()
        {
            ((Storyboard)pathMais.Resources["stryBrdOnLoaded"]).Begin();
        }
    }
}
