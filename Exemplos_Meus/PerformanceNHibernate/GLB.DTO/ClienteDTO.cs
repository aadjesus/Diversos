using System;
using FGlobus.Comum.DTO;

namespace Globus5.Abastecimento.DTO
{
    /// <summary>
    /// Classe de entidade de banco de dados que representa a tabela relacional ABA_ABASTECIMENTOCARRO
    /// <remarks>
    /// Mapeamento OR em AbastecimentoCarroDTO.hbm.xml
    /// Arquivo criado : 26/10/2011 13:25:51.
    /// Criado por     : cleber.santos.
    /// </remarks>
    /// </summary>
    [Serializable]
    public class ClienteDTO : DataTransferObject
    {
        #region Private Members

        private bool _isChanged;
        private bool _isDeleted;

        #endregion

        #region Default ( Empty ) Class Constructor

        /// <summary>
        /// Construtor default
        /// </summary>
        public ClienteDTO()
        {
        }

        #endregion // End of Default ( Empty ) Class Constructor

        #region Public Properties

        /// <summary>
        /// (Get) Retorna um valor indicando se a classe foi alterada.
        /// </summary>
        /// <value>
        /// <c>true</c> Se alguma propriedade foi alterada, caso contrario, <c>false</c>.
        /// </value>
        public virtual bool IsChanged
        {
            get
            {
                return _isChanged;
            }
        }

        /// <summary>
        /// (Get) Retorna um valor indicando se o registro foi marcado para delecao.
        /// </summary>
        /// <value>
        /// <c>true</c> Se o registro foi marcado para delecao, caso contrario, <c>false</c>.
        /// </value>
        public virtual bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Marca <see cref="IsChanged"/> e <see cref="IsDeleted"/> para <c>true</c>.
        /// </summary>
        public virtual void MarkAsDeleted()
        {
            _isDeleted = true;
            _isChanged = true;
        }

        #endregion

        #region Equals And HashCode Overrides

        /// <summary>
        /// Verifica se os objetos sao iguais.
        /// </summary>
        /// <param name="obj">Objeto que vai ser comparado.</param>
        /// <returns><see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if ((obj == null) || (obj.GetType() != this.GetType()))
                return false;
            ClienteDTO castObj = (ClienteDTO)obj;
            return (castObj != null) &&
                   (this.CODCLIENTE == castObj.CODCLIENTE);
        }

        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * this.CODCLIENTE.GetHashCode();
            return hash;
        }

        #endregion

        public int CODCLIENTE { get; set; }
        public string CODGRUPOEMP { get; set; }
        public string CODTIPOEMPRESA { get; set; }
        public string CODUFCOBRANCA { get; set; }
        public string RAZAOSOCIALCLIENTE { get; set; }
        public string CODUFENDERECO { get; set; }
        public string NOMEFANTASIACLIENTE { get; set; }
        public string CNPJCLIENTE { get; set; }
        public string IESTADUALCLIENTE { get; set; }
        public string ATIVOCLIENTE { get; set; }
        public int CODIGOGLOBUSCLIENTE { get; set; }
        public int QUANTFUNCIONARIOS { get; set; }
        public int QUANTCARROS { get; set; }
        public string RUACOBRANCA { get; set; }
        public string RUACLIENTE { get; set; }
        public string NUMEROCOBRANCA { get; set; }
        public string NUMEROCLIENTE { get; set; }
        public string COMPLEMENTOCOBRANCA { get; set; }
        public string COMPLCLIENTE { get; set; }
        public string BAIRROCOBRANCA { get; set; }
        public string BAIRROCLIENTE { get; set; }
        public string CIDADECOBRANCA { get; set; }
        public string CIDADECLIENTE { get; set; }
        public string CEPCLIENTE { get; set; }
        public string CEPCOBRANCA { get; set; }
        public string CAIXAPOSTALCOBRANCA { get; set; }
        public string CAIXAPOSTALCLIENTE { get; set; }
        public int DISTANCIACLIENTE { get; set; }
        public string EMAILCLIENTE { get; set; }
        public int WORKCLIENTE { get; set; }
        public string HOMEPAGECLIENTE { get; set; }
        public int QTDESERVERCLIENTE { get; set; }
        public string SERVERCLIENTE { get; set; }
        public string FONE1CLIENTE { get; set; }
        public string ORACLECLIENTE { get; set; }
        public string FONE2CLIENTE { get; set; }
        public string ESTACIONACLIENTE { get; set; }
        public string FAX1CLIENTE { get; set; }
        public int TEMPOCLIENTE { get; set; }
        public string FAX2CLIENTE { get; set; }
        public string MSGCLIENTE { get; set; }
        public string OBSCLIENTE { get; set; }
        public string CRITICOCLIENTE { get; set; }
        public DateTime ULTIMOEMAILENVIADO { get; set; }
        public string EMAILCHAMADOSCLIENTE { get; set; }
        public string EMAILRELATVISITASCLIENTE { get; set; }
        public string EMAILSASABERTOSCLIENTE { get; set; }
        public string EMAILSASREPROVADOSCLIENTE { get; set; }
        public string EMAILSASAPROVADOSCLIENTE { get; set; }
        public string EMAILSASCONCLUIDOSCLIENTE { get; set; }
        public string EMAILAGENDAMENTOSCLIENTE { get; set; }
        public string CODLIENTEOLD { get; set; }
        public string ATENDIDOPORTERCEIRO { get; set; }
        public string CATEGORIACLIENTE { get; set; }
        public string SENHAWEB { get; set; }
        public string VISUALIZASIM { get; set; }
        public string VISUALIZASAS { get; set; }
        public int CODREGIAO { get; set; }
        public int QTDELICENCAS { get; set; }
        public string LIGACAOPREVENTIVA { get; set; }
        public string EMIMPLANTACAO { get; set; }
        public int CODLOCAL { get; set; }
        public int INDICADORCLIENTE { get; set; }
        public string CLASSIFICACLIENTE { get; set; }
        public DateTime DATALIMITEUSO { get; set; }
        public DateTime TERMINOIMPLANTACAO { get; set; }
        public string FORMASDEACESSO { get; set; }
        public string ARQCONCEDENTE { get; set; }
        public DateTime DATAENVIOKIT { get; set; }
        public string OBSERVACAOKIT { get; set; }
        public string EMPRESA_CONCEDENTE { get; set; }
        public string AMBIENTE_GLOBUS_MAIS { get; set; }
        public int FATOR_SLA { get; set; }
        public DateTime DTINSTALWEBSERVICE { get; set; }
        public string GLOBUS_CLOUD { get; set; }
        public string WEBCONCEDENTE { get; set; }
        public DateTime DT_COMUNICADO_VENDA { get; set; }
        public DateTime DT_RECEB_CONTRATO { get; set; }
        public int NUMERO_MYTICKET { get; set; }
        public string NUMERO_RA { get; set; }
    }
}