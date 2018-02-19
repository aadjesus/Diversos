using System;
using System.Collections.Generic;
using System.Text;
using Globus5.GestaoDeFrotaOnLine.DTO;
using FGlobus.Comum.DAL;
using NHibernate;
using NHibernate.Expression;

namespace Globus5.GestaoDeFrotaOnLine.DAL
{
    /// <summary>
    /// Classe de acesso a dados da tabela GFO_USUARIO_LINHA
    /// </summary>
	public class UsuarioLinhaDAO : DataAccessObject<UsuarioLinhaDTO>
	{
        /// <summary>
        /// Retornar usuario linha
        /// </summary>
        /// <param name="usuario">Usuario.</param>
        /// <returns>Lista de objetos do tipo UsuarioLinhaDTO</returns>
        public IList<UsuarioLinhaDTO> RetornarUsuarioLinha(string usuario)
        {
            return null;
        }

        /// <summary>
        /// Retornar usuario linha para relatorio
        /// </summary>
        /// <param name="usuario">Usuario.</param>
        /// <returns>Lista de objetos do tipo UsuarioLinhaDTO</returns>
        public IList<UsuarioLinhaDTO> RetornarUsuarioLinhaRelatorio(string usuario)
        {
            return null;
        }

	}
}
