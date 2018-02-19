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
    /// Classe reservada aos métodos de regras de negócio relacionada aos cadastros dos módulos do Globus.
    /// </summary>
    public class CadastroBO : FGlobus.Util.GenericoBO
    {
        #region Linha

        /// <summary>
        /// Retornar Linha
        /// </summary>
        /// <param name="CodIntLinha">Codigo interno da linha.</param>
        /// <returns>Lista de objetos do tipo LinhaDAO</returns>
        public IList<LinhaDTO> RetornarLinha(int CodIntLinha)
        {
            try
            {
                return new LinhaDAO().RetornarLinha(CodIntLinha);
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
        /// Retorna todas linhas.
        /// </summary>
        /// <returns>Lista de objetos do tipo LinhaDAO.</returns>
        public IList<LinhaDTO> RetornarTodosLinhas()
        {
            try
            {
                return new LinhaDAO().RetornarTodos();
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
        /// Retorna linha pela chave.
        /// </summary>
        /// <param name="codoperadora">codoperadora</param>
        /// <returns>Objeto do tipo LinhaDAO.</returns>
        public LinhaDTO RetornarLinhaPorChave(int codoperadora)
        {
            try
            {
                LinhaDTO operadoraDTO = new LinhaDAO().ConsultarPorChave(codoperadora);
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
        /// Salva ou altera linha.
        /// </summary>	
        /// <param name="linhaDTO">Objeto do tipo LinhaDTO.</param>
        public void SalvarOuAlterar(LinhaDTO linhaDTO)
        {
            try
            {
                new LinhaDAO().Salvar(linhaDTO);
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

        #region CadastroDeUf

        /// <summary>
        /// Retorna todos cadastro de Uf.
        /// </summary>
        /// <returns>Lista de objetos do tipo CadastroDeUfDTO.</returns>
        public IList<CadastroDeUfDTO> RetornarTodosCadastroDeUf()
        {
            try
            {
                return new CadastroDeUfDAO().RetornarTodos();
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
        /// Retornar Cadastro De Uf
        /// </summary>
        /// <param name="codigoUf">Codigo uf.</param>
        /// <returns>Lista de objetos do tipo CadastroDeUfDTO</returns>
        public IList<CadastroDeUfDTO> RetornarCadastroDeUf(string codigoUf)
        {
            try
            {
                return new CadastroDeUfDAO().RetornarCadastroDeUf(codigoUf);
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
        /// Retorna cadastro de Uf pela chave.
        /// </summary>
        /// <param name="codigoUf">Codigo uf.</param>
        /// <returns>Objeto do tipo CadastroDeUfDTO.</returns>
        public CadastroDeUfDTO RetornarCadastroDeUfPorChave(string codigoUf)
        {
            try
            {
                CadastroDeUfDTO CadastroDeUfDTO = new CadastroDeUfDAO().ConsultarPorChave(codigoUf);
                if (CadastroDeUfDTO == null)
                {
                    throw new BOExcecao(BOExcecao.CodigoInexistente);
                }
                return CadastroDeUfDTO;
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
