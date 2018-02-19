using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Globus5.Globus.DTO;
using NHibernate;
using FGlobus.Excecao;
using Globus5.Globus.DAL;

namespace Globus5.Globus.BLL
{
    /// <summary>
    /// Classe reservada aos métodos de regras de negócio relacionada aos relatorios dos módulos do Globus.
    /// </summary>
    public class RelatorioBO
    {
        /// <summary>
        /// Retornar Linha para relatorio
        /// </summary>
        /// <param name="CodIntLinha">Codigo interno da linha.</param>
        /// <returns>Lista de objetos do tipo LinhaDTO</returns>
        public IList<LinhaDTO> RetornarLinhaRelarorio(int CodIntLinha)
        {
            try
            {
                IList<LinhaDTO> operadoraDTO = new LinhaDAO().RetornarLinhaRelarorio(CodIntLinha);
                if (operadoraDTO == null)
                {
                    throw new BOExcecao(BOExcecao.NenhumaInfParaOsDadosInfo);
                }
                return operadoraDTO;
            }
            catch (HibernateException ex)
            {
                throw new DAOExcecao(DAOExcecao.ErroNHibernate, ex.InnerException);
            }
            catch (Excecao)
            {
                throw;
            }
        }

        /// <summary>
        /// Retornar Cadastro De Uf para relatorio
        /// </summary>
        /// <param name="codigoUf">Codigo uf.</param>
        /// <returns>Lista de objetos do tipo CadastroDeUfDTO</returns>
        public IList<CadastroDeUfDTO> RetornarCadastroDeUfRelarorio(string codigoUf)
        {
            try
            {
                IList<CadastroDeUfDTO> operadoraDTO = new CadastroDeUfDAO().RetornarCadastroDeUfRelarorio(codigoUf);
                if (operadoraDTO == null)
                {
                    throw new BOExcecao(BOExcecao.NenhumaInfParaOsDadosInfo);
                }
                return operadoraDTO;
            }
            catch (HibernateException ex)
            {
                throw new DAOExcecao(DAOExcecao.ErroNHibernate, ex.InnerException);
            }
            catch (Excecao)
            {
                throw;
            }
        }
    }
}
