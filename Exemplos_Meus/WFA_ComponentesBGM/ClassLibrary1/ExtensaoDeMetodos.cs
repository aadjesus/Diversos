using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using System.Reflection;
using System.Data;
using System.Globalization;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;

#region Diversos

namespace FGlobus.Util
{
    /// <summary>
    /// Classe estático reservada para criação de "Extension Methods".
    /// </summary>
    public static class ExtensaoDeMetodos
    {
        #region SoapHttpClientProtocol

        /// <summary>
        /// Consulta por chave.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto DataTransferObject.</typeparam>
        /// <param name="valor">Objeto que esta sendo executado.</param>
        /// <param name="chave">Chave com o valor da pesquisa.</param>
        /// <returns>Objetos do tipo informado.</returns>
        public static T GenericoConsultaPorChave<T>(this SoapHttpClientProtocol valor, object chave)
        {
            try
            {
                return (T)RetornaInvokeMember<T>(valor, "GenericoConsultaPorChave", chave, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retornar lista com todos os objetos.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto DataTransferObject.</typeparam>
        /// <param name="valor">Objeto que está executando o metodo.</param>
        /// <returns>Lista de objetos do tipo informado.</returns>
        public static T[] GenericoRetornarTodos<T>(this SoapHttpClientProtocol valor)
        {
            try
            {
                return ((object[])RetornaInvokeMember<T>(valor, "GenericoRetornarTodos", null, null))
                    .Select(s => (T)s)
                    .ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta básica por condições.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto DataTransferObject.</typeparam>
        /// <param name="valor">Objeto que está executando o metodo.</param>
        /// <param name="condicoes">Lista de objeto do tipo "sCondicaoAdicionalCriteria" com as condições da consulta.</param>        
        /// <returns>Lista de objetos do tipo informado.</returns>
        public static T[] GenericoConsultaBasica<T>(this SoapHttpClientProtocol valor, object[] condicoes)
        {
            try
            {
                return ((object[])RetornaInvokeMember<T>(valor, "GenericoConsultaBasicaPorCondicoes", condicoes, null))
                    .Select(s => (T)s)
                    .ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta básica por condição.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto DataTransferObject.</typeparam>
        /// <param name="valor">Objeto que está executando o metodo.</param>
        /// <param name="condicao">Objeto do tipo "sCondicaoAdicionalCriteria" com a condição da consulta.</param>        
        /// <returns>Lista de objetos do tipo informado.</returns>
        public static T[] GenericoConsultaBasica<T>(this SoapHttpClientProtocol valor, object condicao)
        {
            try
            {
                return ((object[])RetornaInvokeMember<T>(valor, "GenericoConsultaBasicaPorCondicao", condicao, null))
                    .Select(s => (T)s)
                    .ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta básica por condições dos campos informados.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto DataTransferObject.</typeparam>
        /// <param name="valor">Objeto que está executando o metodo.</param>
        /// <param name="condicoes">Lista de objeto do tipo "sCondicaoAdicionalCriteria" com as condições da consulta.</param>        
        /// <param name="campos">Lista de string com o nome dos campos</param>
        /// <returns>Objeto do tipo DataTable.</returns>
        public static DataTable GenericoConsultaBasicaDosCampos<T>(this SoapHttpClientProtocol valor, object[] condicoes, string[] campos)
        {
            try
            {
                return (DataTable)RetornaInvokeMember<T>(valor, "GenericoConsultaBasicaPorCondicoesDosCampos", condicoes, campos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta básica por condição dos campos informados.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto DataTransferObject.</typeparam>
        /// <param name="valor">Objeto que está executando o metodo.</param>
        /// <param name="condicao">Objeto do tipo "sCondicaoAdicionalCriteria" com a condição da consulta.</param>        
        /// <param name="campos">Lista de string com o nome dos campos</param>
        /// <returns>Objeto do tipo DataTable.</returns>
        public static DataTable GenericoConsultaBasicaDosCampos<T>(this SoapHttpClientProtocol valor, object condicao, string[] campos)
        {
            try
            {
                return (DataTable)RetornaInvokeMember<T>(valor, "GenericoConsultaBasicaPorCondicaoDosCampos", condicao, campos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static object RetornaInvokeMember<T>(SoapHttpClientProtocol valor, string nomeMetodo, object parametro, string[] campos)
        {
            try
            {
                if (!((typeof(T)).UnderlyingSystemType).BaseType.Name.Equals("DataTransferObject"))
                    throw new Exception("# ERRO # O tipo informado não encontrado é do tipo DataTransferObject.");

                if (nomeMetodo.IndexOf("GenericoConsutaBasicaPorCondic") > -1 &&
                    parametro.GetType().Name.IndexOf("sCondicaoAdicionalCriteria") == -1)
                    throw new Exception("# ERRO # A lista de condições informada não é do tipo sCondicaoAdicionalCriteria.");

                Assembly assembly = valor.GetType().Assembly;
                Type type = assembly.GetType(valor.GetType().FullName);

                object activator = Activator.CreateInstance(type);

                List<object> parametros = new List<object>();
                parametros.Add("ValorChaveCRUD");
                parametros.Add(typeof(T).Name);
                if (parametro != null)
                    parametros.Add(parametro);

                if (campos != null)
                    parametros.Add(campos);

                return type.InvokeMember(
                    nomeMetodo,
                    BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                    null,
                    activator,
                    parametros.ToArray());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Linq

        #region Update

        /// <summary>
        /// Delegate para controlar uma expressão linq
        /// </summary>
        /// <typeparam name="TArg0">Tipo de objeto"Generico".</typeparam>
        /// <param name="element">Parametro.</param>
        public delegate void Func<TArg0>(TArg0 element);

        /// <summary>
        /// Atualiza os campos informados na lista.
        /// </summary>
        /// <typeparam name="TSource">Tipo de lista.</typeparam>
        /// <param name="source">Lista.</param>
        /// <param name="update">Campos.</param>
        /// <returns>Retorna a lista atualizada.</returns>
        public static IEnumerable<TSource> Update<TSource>(this IEnumerable<TSource> source, Func<TSource> update)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (update == null) throw new ArgumentNullException("update");
            if (typeof(TSource).IsValueType)
                throw new NotSupportedException("Elementos de tipo de valor não são suportados por atualização.");

            foreach (TSource element in source)
                update(element);
            return source;
        }

        #endregion

        #endregion

        #region ### Métodos antigos ### - String

        ///// <summary>
        ///// Utilizar o método 'ConverteCor' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        ///// </summary>
        ///// <param name="valor">String</param>
        ///// <returns>Objeto do tipo System.Windows.Media.Brush</returns>
        //[Obsolete("Utilizar o método 'ConverteCor' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        //public static System.Windows.Media.Brush ConverteCor(this string valor)
        //{
        //    return FGlobus.Util.ExtensaoString.ExtensaoString.ConverteCor(valor);
        //}

        /// <summary>
        /// Utilizar o método 'ConverteImage' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        /// </summary>
        /// <param name="valor">String</param>
        /// <returns>Objeto do tipo System.Drawing.Image</returns>
        [Obsolete("Utilizar o método 'ConverteImage' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        public static System.Drawing.Image ConverteImage(this string valor)
        {
            return FGlobus.Util.ExtensaoString.ExtensaoString.ConverteImage(valor);
        }

        /// <summary>
        /// Utilizar o método 'ConverterPrimeiraLetraParaMaiuscula' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        /// </summary>
        /// <param name="valor">String</param>
        /// <param name="todas">Todas palavras da string</param>
        /// <returns>Objeto do tipo string.</returns>
        [Obsolete("Utilizar o método 'ConverterPrimeiraLetraParaMaiuscula' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        public static string ConverterPrimeiraLetraParaMaiuscula(this string valor, bool todas = true)
        {
            return FGlobus.Util.ExtensaoString.ExtensaoString.ConverterPrimeiraLetraParaMaiuscula(valor, todas);
        }

        /// <summary>
        /// Utilizar o método 'RemoverCaracterEspecial' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        /// </summary>
        /// <param name="valor">String.</param>
        /// <param name="subistituirPor">Substitui caracter especial encontrado pelo informado.</param>
        /// <returns>String.</returns>
        [Obsolete("Utilizar o método 'RemoverCaracterEspecial' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        public static string RemoverCaracterEspecial(this string valor, String subistituirPor = null)
        {
            return FGlobus.Util.ExtensaoString.ExtensaoString.RemoverCaracterEspecial(valor, subistituirPor);
        }

        /// <summary>
        /// Utilizar o método 'RemoverAcentuacao' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        /// </summary>
        /// <param name="valor">String.</param>
        /// <returns>String.</returns>
        [Obsolete("Utilizar o método 'RemoverAcentuacao' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        public static string RemoverAcentuacao(this string valor)
        {
            return FGlobus.Util.ExtensaoString.ExtensaoString.RemoverAcentuacao(valor);
        }

        /// <summary>
        /// Utilizar o método 'Concatenar' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        /// </summary>
        /// <param name="valor">String</param>
        /// <param name="caracaterSeparacao">Caracater de separação.</param>
        /// <param name="valores">Lista de objeto dos tipo object</param>
        /// <returns>String.</returns>
        [Obsolete("Utilizar o método 'Concatenar' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        public static string Concatenar(this string valor, char caracaterSeparacao, params object[] valores)
        {
            return FGlobus.Util.ExtensaoString.ExtensaoString.Concatenar(valor, caracaterSeparacao, valores);
        }

        /// <summary>
        /// Utilizar o método 'Concatenar' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        /// </summary>
        /// <param name="valor">String</param>
        /// <param name="valores">Lista de objeto dos tipo object</param>
        /// <returns>String.</returns>
        [Obsolete("Utilizar o método 'Concatenar' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        public static string Concatenar(this string valor, params object[] valores)
        {
            return FGlobus.Util.ExtensaoString.ExtensaoString.Concatenar(valor, valores);
        }

        /// <summary>
        /// Utilizar o método 'RemoverNaoNumericos' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        /// </summary>
        /// <param name="valor">String.</param>
        /// <returns>String.</returns>
        [Obsolete("Utilizar o método 'RemoverNaoNumericos' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        public static string RemoverNaoNumericos(this string valor)
        {
            return FGlobus.Util.ExtensaoString.ExtensaoString.RemoverNaoNumericos(valor);
        }

        /// <summary>
        /// Utilizar o método 'RemoverNaoNumericos' que esta na namespace 'FGlobus.Util.ExtensaoString'.
        /// <para>Observação: Se não existir nenhum numero, retorna Zero</para>
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de retorno</typeparam>
        /// <param name="valor">String.</param>
        /// <returns>Objeto do tipo informado.</returns>
        [Obsolete("Utilizar o método 'RemoverNaoNumericos' que esta na namespace 'FGlobus.Util.ExtensaoString'.")]
        public static T RemoverNaoNumericos<T>(this string valor) where T : IComparable
        {
            return FGlobus.Util.ExtensaoString.ExtensaoString.RemoverNaoNumericos<T>(valor);
        }

        #endregion

        #region ### Métodos antigos ### - DateTime

        /// <summary>
        /// Utilizar o método 'RetornarUltimaHoraDoDia' que esta na namespace 'FGlobus.Util.ExtensionsDateTime'.
        /// </summary>
        /// <param name="valor">DateTime</param>
        /// <returns>DateTime</returns>
        [Obsolete("Utilizar o método 'RetornarUltimaHoraDoDia' que esta na namespace 'FGlobus.Util.ExtensionsDateTime'.")]
        public static DateTime RetornarUltimaHoraDoDia(this DateTime valor)
        {
            return FGlobus.Util.ExtensaoDateTime.ExtensaoDateTime.RetornarUltimaHoraDoDia(valor);
        }

        #endregion

        #region ### Métodos antigos ### - DevExpress.XtraEditors

        /// <summary>
        /// Utilizar o método 'LimparControles' que esta na namespace 'FGlobus.Util.ExtensaoScrollableControl'.
        /// </summary>
        /// <param name="valor">ScrollableControl.</param>
        [Obsolete("Utilizar o método 'LimparControles' que esta na namespace 'FGlobus.Util.ExtensaoScrollableControl'.")]
        public static void LimparControles(this ScrollableControl valor)
        {
            FGlobus.Util.ExtensaoWindowsForms.ExtensaoWindowsForms.LimparControles(valor);
        }

        /// <summary>
        /// Utilizar o método 'LimparControles' que esta na namespace 'FGlobus.Util.ExtensaoXtraTabControl'.
        /// </summary>
        /// <param name="valor">ScrollableControl.</param>
        [Obsolete("Utilizar o método 'LimparControles' que esta na namespace 'FGlobus.Util.ExtensaoXtraTabControl'.")]
        public static void LimparControles(DevExpress.XtraTab.XtraTabControl valor)
        {
            FGlobus.Util.ExtensaoWindowsForms.ExtensaoWindowsForms.LimparControles(valor);
        }

        #endregion

        #region ### Métodos antigos ### - Exception

        /// <summary>
        /// Utilizar o método 'MostrarExcecao' que esta na namespace 'FGlobus.Util.ExtensaoException'.
        /// </summary>
        /// <param name="valor">Exception</param>
        [Obsolete("Utilizar o método 'MostrarExcecao' que esta na namespace 'FGlobus.Util.ExtensaoException'.")]
        public static void MostrarMensagem(this Exception valor)
        {
            FGlobus.Util.ExtensaoException.ExtensaoException.MostrarExcecao(valor);
        }

        #endregion

        #region ### Métodos antigos ### - Enum

        /// <summary>
        /// Utilizar o método 'RetornarDefaultValue' que esta na namespace 'FGlobus.Util.ExtensionsEnum'.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        [Obsolete("Utilizar o método 'RetornarDefaultValue' que esta na namespace 'FGlobus.Util.ExtensionsEnum'.")]
        public static string RetornarDefaultValue(this Enum valor)
        {
            return FGlobus.Util.ExtensaoEnum.ExtensaoEnum.RetornarDefaultValue(valor);
        }

        /// <summary>
        /// Utilizar o método 'RetornarDescricao' que esta na namespace 'FGlobus.Util.ExtensionsEnum'.
        /// </summary>
        /// <param name="valor">Enum</param>
        /// <returns>String.</returns>
        [Obsolete("Utilizar o método 'RetornarDescricao' que esta na namespace 'FGlobus.Util.ExtensionsEnum'.")]
        public static string RetornarDescricao(this Enum valor)
        {
            return FGlobus.Util.ExtensaoEnum.ExtensaoEnum.RetornarDescricao(valor);
        }

        #endregion
    }
}

#endregion

#region Enum

namespace FGlobus.Util.ExtensaoEnum
{
    /// <summary>
    /// Classe estática reservada para criação de "Extension Methods" voltada à classe Enum.
    /// </summary>
    public static class ExtensaoEnum
    {
        /// <summary>
        /// Retorna o valor do Enum quando o mesmo estiver o atributo DefaultValue
        /// <para>Parametro: <param name="valor">Enum</param></para>
        /// <para>Retorno: <returns>string</returns></para>
        /// <example>
        /// <para>Exemplo:</para>
        /// Este exemplo mostra como utilizar o atributo "<see cref="System.ComponentModel.DefaultValueAttribute"/>" em um enumerador.
        /// <code>
        /// <para>public enum eEnumerador</para>
        /// <para>{</para>
        /// <para>        [System.ComponentModel.DefaultValue("Teste")]</para>
        /// <para>        Valor1,</para>
        /// <para>        [System.ComponentModel.DefaultValue("Teste")]</para>
        /// <para>        Valor2</para>
        /// <para>}</para>
        /// </code>
        /// </example>
        /// </summary>
        public static string RetornarDefaultValue(this Enum valor)
        {
            try
            {
                return (valor.GetType().GetField(valor.ToString()).GetCustomAttributes(typeof(System.ComponentModel.DefaultValueAttribute), false)
                    .FirstOrDefault() as System.ComponentModel.DefaultValueAttribute).Value.ToString();
            }
            catch (Exception)
            {
                return valor.ToString();
            }
        }

        /// <summary>
        /// Retorna descrição do Enum pelo atributo XmlEnum.
        /// </summary>
        /// <param name="valor">Enum</param>
        /// <returns>String.</returns>
        public static string RetornarDescricao(this Enum valor)
        {
            try
            {
                return (valor.GetType().GetField(valor.ToString())
                                   .GetCustomAttributes(typeof(XmlEnumAttribute), true)
                                   .First() as XmlEnumAttribute).Name;
            }
            catch (Exception)
            {
                return valor.ToString();
            }
        }
    }
}

#endregion

#region String

namespace FGlobus.Util.ExtensaoString
{
    /// <summary>
    /// Classe estática reservada para criação de "Extension Methods" voltada à classe String.
    /// </summary>
    public static class ExtensaoString
    {

        private static string RemoverCaracterEspecialOuAcentuacao(string valor, bool acentuacao = false, String subistituirPor = null)
        {
            if (String.IsNullOrEmpty(valor))
                return valor;
            else
                return valor
                    .Select(s => s.ToString())
                    .Aggregate(new StringBuilder(), (a, b) =>
                    {
                        if (acentuacao)
                        {
                            string caracter = b.Normalize(NormalizationForm.FormD);
                            if (CharUnicodeInfo.GetUnicodeCategory(caracter[0]) != UnicodeCategory.NonSpacingMark)
                                a.Append(caracter[0]);
                        }
                        else
                        {
                            if ((b[0] >= '0' && b[0] <= '9') ||
                                (b[0] >= 'A' && b[0] <= 'Z') ||
                                (b[0] >= 'a' && b[0] <= 'z'))
                                a.Append(b);
                            else if (!String.IsNullOrEmpty(subistituirPor))
                                a.Append(subistituirPor);
                        }

                        return a;
                    }).ToString();
        }

        ///// <summary>
        ///// Converte string de cor em System.Windows.Media.Brush.
        ///// </summary>
        ///// <param name="valor">String</param>
        ///// <returns>Objeto do tipo System.Windows.Media.Brush</returns>
        //public static System.Windows.Media.Brush ConverteCor(this string valor)
        //{
        //    object cor = null;
        //    try
        //    {
        //        cor = new System.Windows.Media.BrushConverter().ConvertFrom(valor);
        //    }
        //    catch (Exception)
        //    {
        //        try
        //        {
        //            string[] _cor = valor.Split(new char[] { ';' }, 3);
        //            cor = new System.Windows.Media.BrushConverter()
        //                    .ConvertFrom(System.Windows.Media.Color.FromRgb(
        //                        Convert.ToByte(_cor[0]),
        //                        Convert.ToByte(_cor[1]),
        //                        Convert.ToByte(_cor[2])).ToString());
        //        }
        //        catch (Exception)
        //        {
        //            cor = new System.Windows.Media.BrushConverter().ConvertFrom(Constantes.COR_RESERVADA);
        //        }
        //    }
        //    return (System.Windows.Media.Brush)cor;
        //}

        /// <summary>
        /// Converte string com byte[] em System.Drawing.Image.
        /// </summary>
        /// <param name="valor">String</param>
        /// <returns>Objeto do tipo System.Drawing.Image</returns>
        public static System.Drawing.Image ConverteImage(this string valor)
        {
            if (!String.IsNullOrEmpty(valor))
            {
                try
                {
                    byte[] array = Convert.FromBase64String(valor);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(array));
                    return image;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Converte a primeira letra de cada palavra para maiuscula.
        /// </summary>
        /// <param name="valor">String</param>
        /// <param name="todas">Todas palavras da string</param>
        /// <returns>Objeto do tipo string.</returns>
        public static string ConverterPrimeiraLetraParaMaiuscula(this string valor, bool todas = true)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;
            else
                return valor.ToLower()
                    .Split(new string[] { todas ? " " : "" }, StringSplitOptions.None)
                    .Aggregate(string.Empty, (a, b) => a + (String.IsNullOrEmpty(a) ? "" : " ") +
                                                     b.Aggregate(string.Empty, (a1, b1) => a1 + (String.IsNullOrEmpty(a1) ? Char.ToUpper(b1) : b1)));
        }

        /// <summary>
        /// Remove os caracteres especiais.
        /// </summary>
        /// <param name="valor">String.</param>
        /// <param name="subistituirPor">Substitui caracter especial encontrado pelo informado.</param>
        /// <returns>String.</returns>
        public static string RemoverCaracterEspecial(this string valor, String subistituirPor = null)
        {
            return RemoverCaracterEspecialOuAcentuacao(valor, false, subistituirPor);
        }

        /// <summary>
        /// Remove acentuação dos caracteres.
        /// </summary>
        /// <param name="valor">String.</param>
        /// <returns>String.</returns>
        public static string RemoverAcentuacao(this string valor)
        {
            return RemoverCaracterEspecialOuAcentuacao(valor, true);
        }

        /// <summary>
        /// Concatena uma lista de objetos em um texto separando o pelo caracter de separação informado.
        /// </summary>
        /// <param name="valor">String</param>
        /// <param name="caracaterSeparacao">Caracater de separação.</param>
        /// <param name="valores">Lista de objeto dos tipo object</param>
        /// <returns>String.</returns>
        public static string Concatenar(this string valor, char caracaterSeparacao, params object[] valores)
        {
            List<object> novaLista = new List<object>();
            novaLista.Add(valor);
            novaLista.AddRange(valores);

            return novaLista.Aggregate(String.Empty, (a, b) =>
                String.Concat(a,
                (String.IsNullOrEmpty(a)
                    ? ""
                    : valores.First() == null
                        ? ""
                        : String.Concat(" ", caracaterSeparacao, " ")),
                b));

        }

        /// <summary>
        /// Concatena uma lista de objetos em um texto separando o por ' - '.
        /// </summary>
        /// <param name="valor">String</param>
        /// <param name="valores">Lista de objeto dos tipo object</param>
        /// <returns>String.</returns>
        public static string Concatenar(this string valor, params object[] valores)
        {
            return valor.Concatenar('-', valores);
        }

        /// <summary>
        /// Remove os caracteres não numéricos da variável.
        /// </summary>
        /// <param name="valor">String.</param>
        /// <returns>String.</returns>
        public static string RemoverNaoNumericos(this string valor)
        {
            try
            {
                if (String.IsNullOrEmpty(valor))
                    return valor;
                else
                    return valor.ToCharArray()
                        .Where(w => w >= 48 && w <= 57)
                        .Aggregate("", (a, b) => String.Concat(a, b));
            }
            catch (Exception ex)
            {
                throw new Exception("# ERRO #" + ex.Message);
            }
        }

        /// <summary>
        /// Remove os caracteres não numéricos da variável e converte para o tipo informado.
        /// <para>Observação: Se não existir nenhum numero, retorna Zero</para>
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de retorno</typeparam>
        /// <param name="valor">String.</param>
        /// <returns>Objeto do tipo informado.</returns>
        public static T RemoverNaoNumericos<T>(this string valor) where T : IComparable
        {
            string retorno = valor.RemoverNaoNumericos();
            if (String.IsNullOrEmpty(retorno))
                retorno = "0";

            return (T)Convert.ChangeType(retorno, typeof(T));
        }

    }
}

#endregion

#region DateTime

namespace FGlobus.Util.ExtensaoDateTime
{
    /// <summary>
    /// Classe estática reservada para criação de "Extension Methods" voltada à classe DateTime.
    /// </summary>
    public static class ExtensaoDateTime
    {
        /// <summary>
        /// Retorna a ultima hora do dia
        /// </summary>
        /// <param name="valor">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime RetornarUltimaHoraDoDia(this DateTime valor)
        {
            return valor.Date.AddDays(1).AddTicks(-1);
        }
    }
}

#endregion

#region Exception

namespace FGlobus.Util.ExtensaoException
{
    /// <summary>
    /// Classe estática reservada para criação de "Extension Methods" voltada à classe Exception.
    /// </summary>
    public static class ExtensaoException
    {
        /// <summary>
        /// Mostrar MessageBox com o valor da Exception
        /// </summary>
        /// <param name="valor">Exception</param>
        public static void MostrarExcecao(this Exception valor)
        {
            MessageBox.Show(String.Concat(valor), "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

#endregion

#region Web

namespace FGlobus.Util.Web
{
    /// <summary>
    /// Classe estática reservada para criação de "Extension Methods" voltadas à Web.
    /// </summary>
    public static class ExtensaoDeMetodosWeb
    {
        /// <summary>
        /// Retorna qualquer lista no formato Json.
        /// </summary>
        /// <param name="lista">Qualquer tipo de lista.</param>
        /// <returns>Lista no formato Json.</returns>
        public static string RetornarJson(this IEnumerable lista)
        {
            //System.Web.Script.Serialization.JavaScriptSerializer serializacaoJson =
            //        new System.Web.Script.Serialization.JavaScriptSerializer();
            //return serializacaoJson.Serialize(lista);
            return "";
        }
    }
}

#endregion

#region ExtensaoWindowsForms

namespace FGlobus.Util.ExtensaoWindowsForms
{
    /// <summary>
    /// Classe estática reservada para criação de "Extension Methods" das classes dos controles WindowsForms.
    /// </summary>
    public static class ExtensaoWindowsForms
    {
        /// <summary>
        /// Limpa os controles do controle informado, do tipo container.
        /// </summary>
        /// <param name="valor">ScrollableControl.</param>
        public static void LimparControles(this ScrollableControl valor)
        {
            // 0º Nome da propriedade.
            // 1º Valor.
            // 2º Segundo valor caso o 1º de erro
            // 3º Identificar se continua procurando.
            List<object> _propriedades = new List<object>();
            _propriedades.Add(new object[] { "SelectedIndex", -1 });
            _propriedades.Add(new object[] { "Checked", false });
            _propriedades.Add(new object[] { "DateTime", null });
            _propriedades.Add(new object[] { "Time", null });
            _propriedades.Add(new object[] { "EditValue", null });
            _propriedades.Add(new object[] { "Text", null });

            if (valor.Controls.Count > 0)
            {
                valor.Controls
                    .OfType<Control>()
                    .ToList()
                    .ForEach(f =>
                    {
                        ScrollableControl scrollableControl = f as ScrollableControl;
                        if (scrollableControl != null && scrollableControl.Controls.Count > 0)
                            scrollableControl.LimparControles();
                        else
                        {
                            try
                            {
                                _propriedades
                                    .OfType<object>()
                                    .ToList()
                                    .ForEach(f1 =>
                                    {
                                        PropertyInfo propertyInfo = f.GetType().GetProperty(((object[])(f1))[0].ToString());
                                        propertyInfo.SetValue(f, ((object[])(f1))[1], null);
                                    });
                            }
                            catch (Exception)
                            {
                            }
                        }
                    });
            }
        }

        /// <summary>
        /// Limpa os controles das XtraTabPage
        /// </summary>
        /// <param name="valor">ScrollableControl.</param>
        public static void LimparControles(DevExpress.XtraTab.XtraTabControl valor)
        {
            valor.TabPages
                .OfType<DevExpress.XtraTab.XtraTabPage>()
                .ToList()
                .ForEach(f => f.LimparControles());
        }

    }
}

#endregion
