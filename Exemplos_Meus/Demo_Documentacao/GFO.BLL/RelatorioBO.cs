using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Globus5.Globus.DTO;
using NHibernate;
using FGlobus.Excecao;
using Globus5.GestaoDeFrotaOnLine.DTO;
using Globus5.GestaoDeFrotaOnLine.DAL;

namespace Globus5.GestaoDeFrotaOnLine.BLL
{
    /// <summary>
    /// Classe reservada aos métodos de regras de negócio relacionada aos relatorios dos módulos do Gestao de Frota OnLine.
    /// </summary>
    public class RelatorioBO
    {
        /// <summary>
        /// Retornar Operadora para relatorio
        /// </summary>
        /// <param name="CodOperadora">Codigo operadora.</param>
        /// <returns>Lista de objetos do tipo OperadoraDTO</returns>
        public IList<OperadoraDTO> RetornarOperadoraRelatorio(int CodOperadora)
        {
            try
            {
                IList<OperadoraDTO> operadoraDTO = new OperadoraDAO().RetornarOperadoraRelatorio(CodOperadora);
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
        /// Retornar Parametros para relatorio
        /// </summary>
        /// <param name="CodigoEmpresa">Codigo Empresa.</param>
        /// <returns>Lista de objetos do tipo ParametrosDTO</returns>
        public IList<ParametrosDTO> RetornarParametrosRelatorio(int CodigoEmpresa)
        {
            try
            {
                IList<ParametrosDTO> operadoraDTO = new ParametrosDAO().RetornarParametrosRelatorio(CodigoEmpresa);
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
        /// Retornar usuario linha para relatorio
        /// </summary>
        /// <param name="usuario">Usuario.</param>
        /// <returns>Lista de objetos do tipo UsuarioLinhaDTO</returns>
        public IList<UsuarioLinhaDTO> RetornarUsuarioLinhaRelatorio(string usuario)
        {
            try
            {
                IList<UsuarioLinhaDTO> operadoraDTO = new UsuarioLinhaDAO().RetornarUsuarioLinhaRelatorio(usuario);
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
