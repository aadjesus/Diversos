using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FGlobus.Util;
using Globus5.Abastecimento.DAL;

namespace ClassLibrary1.BLL
{
    public class Class1: GenericoBO
    {
        private void Teste_NomeMetodo()
        {
            FGlobus.Comum.ChaveCRUD.Conecta();
            var sss = new BombaDAO().RetornarTodos();
            if (sss == null)
            {
                
            }

            
        }
    }
}
