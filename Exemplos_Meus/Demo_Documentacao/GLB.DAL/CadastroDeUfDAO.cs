using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Globus5.Globus.DTO;
using FGlobus.Comum.DAL;

namespace Globus5.Globus.DAL
{
    /// <summary>
    /// Classe de acesso a dados da tabela BGM_CADLINHAS
    /// </summary>
    public class CadastroDeUfDAO : DataAccessObject<CadastroDeUfDTO>
    {
        /// <summary>
        /// Retornar Cadastro De Uf
        /// </summary>
        /// <param name="codigoUf">Codigo uf.</param>
        /// <returns>Lista de objetos do tipo CadastroDeUfDTO</returns>
        public IList<CadastroDeUfDTO> RetornarCadastroDeUf(string codigoUf)
        {
            return null;
        }

        /// <summary>
        /// Retornar Cadastro De Uf para relatorio
        /// </summary>
        /// <param name="codigoUf">Codigo uf.</param>
        /// <returns>Lista de objetos do tipo CadastroDeUfDTO</returns>
        public IList<CadastroDeUfDTO> RetornarCadastroDeUfRelarorio(string codigoUf)
        {
            return null;
        }
    }
}
