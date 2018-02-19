using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using FGlobus.Comum.BLL;
using FGlobus.Util;
using Globus5.Abastecimento.DTO;

namespace BLL
{
    public class CadastroBO : BusinessObject
    {

        public IEnumerable<ClienteDTO> Retornar()
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            return clienteDAO.RetornarTodos();
        }

        public IEnumerable<BGMClienteDTO> Retornar2()
        {
            using (var session = this.Sessao)
            {
                var x1 = "select * from bgm_cliente";
                var x2 = session.CreateSQLQuery(x1);
                if (x2 == null)
                {

                }




            }
            BGMClienteDAO clienteDAO = new BGMClienteDAO();
            return clienteDAO.RetornarTodos();
        }


        private void Teste_NomeMetodo()
        {
            FGlobus.Comum.ChaveCRUD.Conecta("ORA11G64", "SIITESTE");
            var resultado = this.Retornar2();
            if (resultado == null)
            {

            }
        }
    }
}