using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Globus5.Globus.DTO;
using System.Collections.Generic;
using Globus5.Globus.BLL;

namespace Globus5.WebServices
{
    /// <summary>
    /// Summary description for Globus
    /// </summary>
    [WebService(Namespace = "http://bgmrodotec.com.br/globus5/globus")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class GlobusWS : FGlobus.Comum.WebService.GlobusBaseWS
    {
        #region Cadastro

        #region Linha

        /// <summary>
        /// Retornar Linha
        /// </summary>
        /// <param name="CodIntLinha">Codigo interno da linha.</param>
        /// <returns>Lista de objetos do tipo LinhaDAO</returns>
        [WebMethod(Description = "Retornar Linha")]
        public IList<LinhaDTO> RetornarLinha(int CodIntLinha)
        {
            try
            {
                return new CadastroBO().RetornarLinha(CodIntLinha);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retorna todas linhas.
        /// </summary>
        /// <returns>Lista de objetos do tipo LinhaDAO.</returns>
        [WebMethod(Description = "Retorna todas linhas.")]
        public IList<LinhaDTO> RetornarTodosLinhas()
        {
            try
            {
                return new CadastroBO().RetornarTodosLinhas();
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retorna linha pela chave.
        /// </summary>
        /// <param name="codoperadora">codoperadora</param>
        /// <returns>Objeto do tipo LinhaDAO.</returns>
        [WebMethod(Description = "Retorna linha pela chave.")]
        public LinhaDTO RetornarLinhaPorChave(int codoperadora)
        {
            try
            {
                return new CadastroBO().RetornarLinhaPorChave(codoperadora);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Salva ou altera linha.
        /// </summary>	
        /// <param name="linhaDTO">Objeto do tipo LinhaDTO.</param>
        [WebMethod(Description = "Salva ou altera linha.")]
        public void SalvarOuAlterar(LinhaDTO linhaDTO)
        {
            try
            {
                new CadastroBO().SalvarOuAlterar(linhaDTO);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        #endregion

        #region CadastroDeUf

        /// <summary>
        /// Retorna todos cadastro de Uf.
        /// </summary>
        /// <returns>Lista de objetos do tipo CadastroDeUfDTO.</returns>
        [WebMethod(Description = "Retorna todos cadastro de Uf.")]
        public IList<CadastroDeUfDTO> RetornarTodosCadastroDeUf()
        {
            try
            {
                return new CadastroBO().RetornarTodosCadastroDeUf();
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retornar Cadastro De Uf
        /// </summary>
        /// <param name="codigoUf">Codigo uf.</param>
        /// <returns>Lista de objetos do tipo CadastroDeUfDTO</returns>
        [WebMethod(Description = "Retornar Cadastro De Uf")]
        public IList<CadastroDeUfDTO> RetornarCadastroDeUf(string codigoUf)
        {
            try
            {
                return new CadastroBO().RetornarCadastroDeUf(codigoUf);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retorna cadastro de Uf pela chave.
        /// </summary>
        /// <param name="codigoUf">Codigo uf.</param>
        /// <returns>Objeto do tipo CadastroDeUfDTO.</returns>
        [WebMethod(Description = "Retorna cadastro de Uf pela chave.")]
        public CadastroDeUfDTO RetornarCadastroDeUfPorChave(string codigoUf)
        {
            try
            {
                return new CadastroBO().RetornarCadastroDeUfPorChave(codigoUf);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        #endregion

        #endregion

        #region Relatorio

        /// <summary>
        /// Retornar Linha para relatorio
        /// </summary>
        /// <param name="CodIntLinha">Codigo interno da linha.</param>
        /// <returns>Lista de objetos do tipo LinhaDTO</returns>
        [WebMethod(Description = "Retornar Linha para relatorio")]
        public IList<LinhaDTO> RetornarLinhaRelarorio(int CodIntLinha)
        {
            try
            {
                return new RelatorioBO().RetornarLinhaRelarorio(CodIntLinha);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retornar Cadastro De Uf para relatorio
        /// </summary>
        /// <param name="codigoUf">Codigo uf.</param>
        /// <returns>Lista de objetos do tipo CadastroDeUfDTO</returns>
        [WebMethod(Description = "Retornar Cadastro De Uf para relatorio")]
        public IList<CadastroDeUfDTO> RetornarCadastroDeUfRelarorio(string codigoUf)
        {
            try
            {
                return new RelatorioBO().RetornarCadastroDeUfRelarorio(codigoUf);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        #endregion
    }
}
