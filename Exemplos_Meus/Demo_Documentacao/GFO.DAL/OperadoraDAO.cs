using System;
using System.Collections.Generic;
using System.Text;
using Globus5.GestaoDeFrotaOnLine.DTO;
using FGlobus.Comum.DAL;

namespace Globus5.GestaoDeFrotaOnLine.DAL
{
    /// <summary>
    /// Classe de acesso a dados da tabela GFO_OPERADORA
    /// </summary>
	public class OperadoraDAO : DataAccessObject<OperadoraDTO>
	{
        /// <summary>
        /// Retornar Operadora
        /// </summary>
        /// <param name="CodOperadora">Codigo operadora.</param>
        /// <returns>Lista de objetos do tipo OperadoraDTO</returns>
        public IList<OperadoraDTO> RetornarOperadora(int CodOperadora)
        {
            return null;
        }

        /// <summary>
        /// Retornar Operadora para relatorio
        /// </summary>
        /// <param name="CodOperadora">Codigo operadora.</param>
        /// <returns>Lista de objetos do tipo OperadoraDTO</returns>
        public IList<OperadoraDTO> RetornarOperadoraRelatorio(int CodOperadora)
        {
            return null;
        }

    }
}
