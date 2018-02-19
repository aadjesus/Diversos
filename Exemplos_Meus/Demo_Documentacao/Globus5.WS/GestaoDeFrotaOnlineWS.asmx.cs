using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using Globus5.GestaoDeFrotaOnLine.DTO;
using Globus5.GestaoDeFrotaOnLine.BLL;

namespace Globus5.WebServices
{
    /// <summary>
    /// WebService do modulo de Gestao de Frotas Online
    /// </summary>
    [WebService(Namespace = "http://bgmrodotec.com.br/globus5/GestaoDeFrotaOnLine")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class GestaoDeFrotaOnLineWS : FGlobus.Comum.WebService.GlobusBaseWS
    {
        #region Cadastro

        #region OperadoraDTO

        /// <summary>
        /// Retornar Operadora
        /// </summary>
        /// <param name="CodOperadora">Codigo operadora.</param>
        /// <returns>Lista de objetos do tipo OperadoraDTO</returns>
        [WebMethod(Description = "")]
        public IList<OperadoraDTO> RetornarOperadora(int CodOperadora)
        {
            try
            {
                return new CadastroBO().RetornarOperadora(CodOperadora);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retorna todas operadora.
        /// </summary>
        /// <returns>Lista de objetos do tipo OperadoraDTO.</returns>
        [WebMethod(Description = "")]
        public IList<OperadoraDTO> RetornarTodosOperadoras()
        {
            try
            {
                return new CadastroBO().RetornarTodosOperadoras();
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retorna Operadora pela chave.
        /// </summary>
        /// <param name="codoperadora">codoperadora</param>
        /// <returns>Objeto do tipo OperadoraDTO.</returns>
        [WebMethod(Description = "")]
        public OperadoraDTO RetornarOperadoraPorChave(int codoperadora)
        {
            try
            {
                return new CadastroBO().RetornarOperadoraPorChave(codoperadora);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Salva ou altera Operadora.
        /// </summary>	
        /// <param name="operadoraDTO">Objeto do tipo OperadoraDTO.</param>
        [WebMethod(Description = "")]
        public void SalvarOuAlterar(OperadoraDTO operadoraDTO)
        {
            try
            {
                new CadastroBO().SalvarOuAlterar(operadoraDTO);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        #endregion

        #region Parametros

        /// <summary>
        /// Retorna todos Parametros.
        /// </summary>
        /// <returns>Lista de objetos do tipo ParametrosDTO.</returns>
        [WebMethod(Description = "")]
        public IList<ParametrosDTO> RetornarTodosParametros()
        {
            try
            {
                return new CadastroBO().RetornarTodosParametros();
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retornar Parametros
        /// </summary>
        /// <param name="CodigoEmpresa">Codigo Empresa.</param>
        /// <returns>Lista de objetos do tipo ParametrosDTO</returns>
        [WebMethod(Description = "")]
        public IList<ParametrosDTO> RetornarParametros(int CodigoEmpresa)
        {
            try
            {
                return new CadastroBO().RetornarParametros(CodigoEmpresa);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retorna Parametros pela chave.
        /// </summary>
        /// <param name="codigoempresa">codigoempresa</param>
        /// <param name="codigofl">codigofl</param>
        /// <returns>Objeto do tipo ParametrosDTO.</returns>
        [WebMethod(Description = "")]
        public ParametrosDTO RetornarParametrosPorChave(int codigoempresa, int codigofl)
        {
            try
            {
                return new CadastroBO().RetornarParametrosPorChave(codigoempresa, codigofl);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }


        #endregion

        #region UsuarioLinha

        /// <summary>
        /// Retorna todos Usuario Linha.
        /// </summary>
        /// <returns>Lista de objetos do tipo UsuarioLinhaDTO.</returns>
        [WebMethod(Description = "")]
        public IList<UsuarioLinhaDTO> RetornarTodosUsuarioLinhas()
        {
            try
            {
                return new CadastroBO().RetornarTodosUsuarioLinhas();
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retornar usuario linha
        /// </summary>
        /// <param name="usuario">Usuario.</param>
        /// <returns>Lista de objetos do tipo UsuarioLinhaDTO</returns>
        [WebMethod(Description = "")]
        public IList<UsuarioLinhaDTO> RetornarUsuarioLinha(string usuario)
        {
            try
            {
                return new CadastroBO().RetornarUsuarioLinha(usuario);
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
        /// Retornar Operadora para relatorio
        /// </summary>
        /// <param name="CodOperadora">Codigo operadora.</param>
        /// <returns>Lista de objetos do tipo OperadoraDTO</returns>
        [WebMethod(Description = "")]
        public IList<OperadoraDTO> RetornarOperadoraRelatorio(int CodOperadora)
        {
            try
            {
                return new RelatorioBO().RetornarOperadoraRelatorio(CodOperadora);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retornar Parametros para relatorio
        /// </summary>
        /// <param name="CodigoEmpresa">Codigo Empresa.</param>
        /// <returns>Lista de objetos do tipo ParametrosDTO</returns>
        [WebMethod(Description = "")]
        public IList<ParametrosDTO> RetornarParametrosRelatorio(int CodigoEmpresa)
        {
            try
            {
                return new RelatorioBO().RetornarParametrosRelatorio(CodigoEmpresa);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        /// <summary>
        /// Retornar usuario linha para relatorio
        /// </summary>
        /// <param name="usuario">Usuario.</param>
        /// <returns>Lista de objetos do tipo UsuarioLinhaDTO</returns>
        [WebMethod(Description = "")]
        public IList<UsuarioLinhaDTO> RetornarUsuarioLinhaRelatorio(string usuario)
        {
            try
            {
                return new RelatorioBO().RetornarUsuarioLinhaRelatorio(usuario);
            }
            catch (Exception ex)
            {
                throw ConverterSoapException(ex); ;
            }
        }

        #endregion
   
    }
}
