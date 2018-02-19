using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Globus5.GestaoDeFrotaOnLine.DTO;
using NHibernate;
using Globus5.GestaoDeFrotaOnLine.DAL;
using FGlobus.Excecao;

namespace Globus5.GestaoDeFrotaOnLine.BLL
{
    /// <summary>
    /// Classe reservada aos métodos de regras de negócio relacionada aos cadastros dos módulos do Gestao de Frota OnLine.
    /// </summary>
    public class CadastroBO : FGlobus.Util.GenericoBO
    {
        #region OperadoraDTO

        /// <summary>
        /// Retornar Operadora
        /// </summary>
        /// <param name="CodOperadora">Codigo operadora.</param>
        /// <returns>Lista de objetos do tipo OperadoraDTO</returns>
        public IList<OperadoraDTO> RetornarOperadora(int CodOperadora)
        {
            try
            {
                return new OperadoraDAO().RetornarOperadora(CodOperadora);
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
        /// Retorna todas operadora.
        /// </summary>
        /// <returns>Lista de objetos do tipo OperadoraDTO.</returns>
        public IList<OperadoraDTO> RetornarTodosOperadoras()
        {
            try
            {
                return new OperadoraDAO().RetornarTodos();
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
        /// Retorna Operadora pela chave.
        /// </summary>
        /// <param name="codoperadora">codoperadora</param>
        /// <returns>Objeto do tipo OperadoraDTO.</returns>
        public OperadoraDTO RetornarOperadoraPorChave(int codoperadora)
        {
            try
            {
                OperadoraDTO operadoraDTO = new OperadoraDAO().ConsultarPorChave(codoperadora);
                if (operadoraDTO == null)
                {
                    throw new BOExcecao(BOExcecao.CodigoInexistente);
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
        /// Salva ou altera Operadora.
        /// </summary>	
        /// <param name="operadoraDTO">Objeto do tipo OperadoraDTO.</param>
        public void SalvarOuAlterar(OperadoraDTO operadoraDTO)
        {
            try
            {
                new OperadoraDAO().Salvar(operadoraDTO);
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

        #endregion

        #region Parametros

        /// <summary>
        /// Retorna todos Parametros.
        /// </summary>
        /// <returns>Lista de objetos do tipo ParametrosDTO.</returns>
        public IList<ParametrosDTO> RetornarTodosParametros()
        {
            try
            {              
                return new ParametrosDAO().RetornarTodos();
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
        /// Retornar Parametros
        /// </summary>
        /// <param name="CodigoEmpresa">Codigo Empresa.</param>
        /// <returns>Lista de objetos do tipo ParametrosDTO</returns>
        public IList<ParametrosDTO> RetornarParametros(int CodigoEmpresa)
        {
            try
            {
                return new ParametrosDAO().RetornarParametros(CodigoEmpresa);
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
        /// Retorna Parametros pela chave.
        /// </summary>
        /// <param name="codigoempresa">codigoempresa</param>
        /// <param name="codigofl">codigofl</param>
        /// <returns>Objeto do tipo ParametrosDTO.</returns>
        public ParametrosDTO RetornarParametrosPorChave(int codigoempresa, int codigofl)
        {
            try
            {
                ParametrosDTO parametrosDTO = new ParametrosDAO().ConsultarPorChave(codigoempresa);
                if (parametrosDTO == null)
                {
                    throw new BOExcecao(BOExcecao.CodigoInexistente);
                }
                return parametrosDTO;
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


        #endregion

        #region UsuarioLinha

        /// <summary>
        /// Retorna todos Usuario Linha.
        /// </summary>
        /// <returns>Lista de objetos do tipo UsuarioLinhaDTO.</returns>
        public IList<UsuarioLinhaDTO> RetornarTodosUsuarioLinhas()
        {
            try
            {
                return new UsuarioLinhaDAO().RetornarTodos();
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
        /// Retornar usuario linha
        /// </summary>
        /// <param name="usuario">Usuario.</param>
        /// <returns>Lista de objetos do tipo UsuarioLinhaDTO</returns>
        public IList<UsuarioLinhaDTO> RetornarUsuarioLinha(string usuario)
        {
            try
            {
                return new UsuarioLinhaDAO().RetornarUsuarioLinha(usuario);
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
        #endregion
    }
}
