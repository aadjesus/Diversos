using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projeto.DTO;

namespace Projeto1.IBLL
{
    public interface IClassIBLL
    {
        ClassDTO RetornarTesteDTO(int codigo);
    }
}
