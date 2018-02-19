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

namespace ContextMenu.Example01 {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {

        #region private fields
        /// <summary>
        /// Mensagem utilizada para informar qual controle está associado
        /// ao ContextMenu.
        /// </summary>
        private const String StatusMessage = "O controle ao qual o ContextMenu está associado é: ";

        #endregion

        #region Private Properties
        /// <summary>
        /// Buffer para manipulação de strings de mensagens.
        /// </summary>
        private StringBuilder Buffer { get; set; }
        #endregion


        public Window1() {
            InitializeComponent();
        }

        #region Private Behavior
        /// <summary>
        /// Víncula dinamicamente um objeto ContextMenu aos objetos
        /// FrameworkElement passados no parâmetro targets.
        /// Este método de exemplo têm como propósito simular uma regra 
        /// que o sistema pode implementar para criar, associar e ativiar ou não,
        /// itens do menu de contexto.
        /// </summary>
        /// <param name="targest">Objetos derivados de FrameworkElement ao qual
        /// o context menu será vínculado</param>
        private void AttachContextMenuToElements( FrameworkElement[] targets, System.Windows.Controls.ContextMenu source ) {
            //TODO: Simular alguma regra de negócio
            if ( ( targets != null ) && ( source != null ) ) {

                foreach ( FrameworkElement item in targets ) 
                    if ( item.ContextMenu == null ) item.ContextMenu = source;

            }
        }
        #endregion

        

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            // A PlacementTarget é uma DependencyProperty que faz parte do Core
            // da WPF. 
            // Na classe ContextMenu, ela é um campo public static readonly, e representa
            // o objeto ao qual a instância ContextMenu está associada.
            // Quando o ContextMenu é atribuído para algum objeto, como no XAML deste projeto,
            // a propriedade PlacementTarget aponta para este objeto.
            // 
            // A classe FrameworkElement é uma das classes Core a WPF derivada de UIElement,
            // que fornece os recursos para criação, pode exemplo, de estilos e outras características
            // de layout. Os controls visuaies são derivados de FrameworkElement, 
            // por isto o casting no exemplo abaixo.
            FrameworkElement target = (FrameworkElement)( sender as System.Windows.Controls.ContextMenu ).PlacementTarget;

            this.Buffer = new StringBuilder(Window1.StatusMessage);




            /* 
             *  Neste exemplo, estou simulando uma lógica que requer o vínculo
             *  em tempo de execução com outros elementos.
             *  Imagine uma determinada situação em que, baseado em informações contextuais como 
             *  por exemplo, regras de negócio, um menu de contexto pode ou não estar associado
             *  com determinados elementos visuais. Ou que determinadas opções do menu de contexto
             *  não devem estar habilitadas. É possível e indicado criar métodos de suporte para 
             *  realizar as ações, tanto de vínculo do menu de contexto, quanto de disponibilidade dos itens do menu.
             *  Por disponibilidade, compreende-se aspectos como:
             *  - O Item estar adicionado ao menu.
             *  - O Item estar habilitado.
             **/
            this.AttachContextMenuToElements( new FrameworkElement[]{ this.sampleButton1, this.sampleButton2, this.sampleButton3},
                (System.Windows.Controls.ContextMenu)this.FindResource("sharedContextMenu"));
            
            #region Verifica qual é o controle target
            /*
             * Este trecho de código simula a lógica para, basedo no TIPO de objeto associado
             * ao ContextMenu, o sistema realizar alguma ação.             
             **/

            if ( target is Button )
                this.Buffer.Append( ( (Button)target ).Content );

            else if ( target is TextBox )
                this.Buffer.Append( ( (TextBox)target ).Text);

            #endregion

            
            this.result.Text = this.Buffer.ToString();

            this.Buffer.Remove( 0, this.Buffer.Length );
        
        }  
    }
}
