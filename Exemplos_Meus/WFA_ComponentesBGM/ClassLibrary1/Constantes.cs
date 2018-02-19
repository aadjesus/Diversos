using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FGlobus.Util
{
    /// <summary>
    /// Constantes reservadas para o sistema.
    /// </summary>
    public sealed class Constantes
    {
        /// <summary>
        /// Constante para ser utilizada na definição de categoria das propriedades dos objetos.
        /// </summary>
        public const string CATEGORIA = "BgmRodotec";

        /// <summary>
        /// Constante com o nome do produto.
        /// </summary>
        public const string NOME_PRODUTO = "Globus+";
        
        /// <summary>
        /// Constante que define cor que não pode ser utilizada.
        /// <para>Cor: Preto.</para>
        /// </summary>
        public const string COR_RESERVADA = "#FF000000";

        /// <summary>
        /// Constante para identificar os valores concatenados em uma string.
        /// </summary>
        public const string CARACTER_SEPARACAO = "# #";

        /// <summary>
        /// Nome default
        /// </summary>
        public const string NONE_DEFAULT = "(none)";

        /// <summary>
        /// Constante que define Checksum de Acesso ao WebService
        /// </summary>
        /// <remarks>Quando alterar o valor mudar no Globus4 vcChecksumAutenticacaoWebService</remarks>
        public const string CHECKSUM_ACESSO_WEBSERVICE = "JEWELRY";

        /// <summary>
        /// Texto de controle interno para mensagem em desenvolvimento.
        /// </summary>
        public const string EM_DESENVOLVIMENTO = "Em desenvolvimento!!!";

        #region Mensagens do SO

        /// <summary>
        /// Barra de titulo do formulario
        /// </summary>
        public const Int32 HTCAPTION = 0x2;

        /// <summary>
        /// Double click na titulo do formulario
        /// </summary>
        public const Int32 WM_NCLBUTTONDBLCLK = 0xA3;

        /// <summary>
        /// Clicou no formulário
        /// </summary>
        public const Int32 WM_SYSCOMMAND = 0x0112;

        /// <summary>
        /// Clicou no botão minimizar do formulario
        /// </summary>
        public const Int32 SC_MINIMIZE = 0xf020;

        #endregion
    }
}
