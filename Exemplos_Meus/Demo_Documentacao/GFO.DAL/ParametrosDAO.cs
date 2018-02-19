using System;
using System.Collections.Generic;
using System.Text;
using Globus5.GestaoDeFrotaOnLine.DTO;
using FGlobus.Comum.DAL;

namespace Globus5.GestaoDeFrotaOnLine.DAL
{
    /// <summary>
    /// Classe de acesso a dados da tabela GFO_PARAMETROS
    /// </summary>
	public class ParametrosDAO : DataAccessObject<ParametrosDTO>
	{
        /// <summary>
        /// Retornar Parametros
        /// </summary>
        /// <param name="CodigoEmpresa">Codigo Empresa.</param>
        /// <returns>Lista de objetos do tipo ParametrosDTO</returns>
        public IList<ParametrosDTO> RetornarParametros(int CodigoEmpresa)
        {
            return null;
        }

        /// <summary>
        /// Retornar Parametros para relatorio
        /// </summary>
        /// <param name="CodigoEmpresa">Codigo Empresa.</param>
        /// <returns>Lista de objetos do tipo ParametrosDTO</returns>
        public IList<ParametrosDTO> RetornarParametrosRelatorio(int CodigoEmpresa)
        {
            return null;
        }

	}
}
