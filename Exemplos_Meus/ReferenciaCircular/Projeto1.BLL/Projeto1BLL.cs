using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projeto1.IBLL;

namespace Projeto1.BLL
{
    public class Projeto1BLL : IClassIBLL
    {
        public void Add()
        {
            CustomerDal oDal = new CustomerDal();
            oDal.Add(this);
        }

        public Projeto.DTO.ClassDTO RetornarTesteDTO(int codigo)
        {
            return new Projeto.DTO.ClassDTO()
            {
                Codigo = 1,
                Descricao = "Teste"
            };
        }
    }
}
