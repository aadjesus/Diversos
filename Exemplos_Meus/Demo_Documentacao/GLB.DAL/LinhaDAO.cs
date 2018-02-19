using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FGlobus.Comum.DAL;
using Globus5.Globus.DTO;
using NHibernate.Expression;
using NHibernate;
using System.Data;
using Globus5.GestaoDeFrotaOnLine.DTO;

namespace Globus5.Globus.DAL
{
    /// <summary>
    /// Classe de acesso a dados da tabela BGM_CADLINHAS
    /// </summary>
    public class LinhaDAO : DataAccessObject<LinhaDTO>
    {
        /// <summary>
        /// Retornar Linha
        /// </summary>
        /// <param name="CodIntLinha">Codigo interno da linha.</param>
        /// <returns>Lista de objetos do tipo LinhaDTO</returns>
        public IList<LinhaDTO> RetornarLinha(int CodIntLinha)
        {
            return null;
        }

        /// <summary>
        /// Retornar Linha para relatorio
        /// </summary>
        /// <param name="CodIntLinha">Codigo interno da linha.</param>
        /// <returns>Lista de objetos do tipo LinhaDTO</returns>
        public IList<LinhaDTO> RetornarLinhaRelarorio(int CodIntLinha)
        {
            return null;
        }

    }
}
