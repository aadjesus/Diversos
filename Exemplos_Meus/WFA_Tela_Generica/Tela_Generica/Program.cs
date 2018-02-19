using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FGlobus.Componentes.WinForms;

namespace Tela_Generica
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            
            MasterFormCadastroTelaGenerica masterFormCadastroTelaGenerica = new MasterFormCadastroTelaGenerica("GFO", "PlanoDePagamentoDTO");

            masterFormCadastroTelaGenerica.ItemMenuSelecionado = new ItemMenu()
            {
                Caption = "Status de orçamento",
                MenuUsuario = new FGlobus.Componentes.WinForms.ws.controle.MenuUsuarioDTO()
                {
                    DireitoAlteracao = true,
                    DireitoExclusao = true,
                    DireitoInclusao = true
                }
            };
            Application.Run(masterFormCadastroTelaGenerica);
            //Application.Run(new Form1());

      //<Propriedade Nome="TipoMensagem">
      //  <Label Descricao="Tipo"
      //         Tipo="Obrigatorio"/>
      //  <Campo Tipo="ComboBox">
      //    <Item Valor="0" Descricao="Recebimento"/>
      //    <Item Valor="1" Descricao="Envio\Recebimento"/>
      //    <Item Valor="2" Descricao="Não validar horário"/>
      //    <Item Valor="3" Descricao="Início de viagem"/>
      //    <Item Valor="4" Descricao="Fim de viagem"/>
      //  </Campo>
      //</Propriedade>

        }
    }
}
