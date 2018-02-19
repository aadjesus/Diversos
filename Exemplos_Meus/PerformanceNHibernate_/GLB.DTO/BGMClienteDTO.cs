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
    public class BGMClienteDTO : DataTransferObject
    {
        #region Private Members

        private bool _isChanged;
        private bool _isDeleted;

        #endregion

        #region Default ( Empty ) Class Constructor

        /// <summary>
        /// Construtor default
        /// </summary>
        public BGMClienteDTO()
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
            BGMClienteDTO castObj = (BGMClienteDTO)obj;
            return (castObj != null) &&
                   (this.CODCLI == castObj.CODCLI);
        }

        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * this.CODCLI.GetHashCode();
            return hash;
        }

        #endregion

        public int CODCLI { get; set; }
        public string NRCLI { get; set; }
        //public string CODTPRECEITA { get; set; }
        //public string UFCOBRANCACLI { get; set; }
        //public int CODOPERFISCAL { get; set; }
        //public string CODIGOUF { get; set; }
        //public int CODCLASSFISC { get; set; }
        //public string RSOCIALCLI { get; set; }
        //public string NFANTASIACLI { get; set; }
        //public string ENDERECOCLI { get; set; }
        //public string BAIRROCLI { get; set; }
        //public string CIDADECLI { get; set; }
        //public string CEPCLI { get; set; }
        //public string TELEFONECLI { get; set; }
        //public string FAXCLI { get; set; }
        //public string HOMEPAGECLI { get; set; }
        //public string EMAILCLI { get; set; }
        //public string TPINSCRICAOCLI { get; set; }
        //public string NRINSCRICAOCLI { get; set; }
        //public string INSCESTADUALCLI { get; set; }
        //public string INSCMUNICIPALCLI { get; set; }
        //public string CONTATOCLI { get; set; }
        //public string OBSERVACAOCLI { get; set; }
        //public string RSCOBRANCACLI { get; set; }
        //public string ENDCOBRANCACLI { get; set; }
        //public string BAIRROCOBRANCACLI { get; set; }
        //public string CIDADECOBRANCACLI { get; set; }
        //public string CEPCOBRANCACLI { get; set; }
        //public string TPINSCCOBRANCACLI { get; set; }
        //public string NRINSCCOBRANCACLI { get; set; }
        //public string INSCESTADUALCOBRANCACLI { get; set; }
        //public string CONDICAOCLI { get; set; }
        //public DateTime DATAULTIMOMOVTOCLI { get; set; }
        //public string CONDRECEBCLI { get; set; }
        //public string TIPOCLI { get; set; }
        //public int CTO_LOCALIDADE { get; set; }
        //public string TIPO_COBRANCA { get; set; }
        //public string TIPO_FATURA { get; set; }
        //public string LOCAL_COLETA { get; set; }
        //public int BANCO_FAT { get; set; }
        //public int AGENCIA_FAT { get; set; }
        //public string CONTA_FAT { get; set; }
        //public int CLIENTE_FAT { get; set; }
        //public string EMITE_A_PRAZO { get; set; }
        //public string VARIA_NF_CTRC { get; set; }
        //public string FRETE_DIRIGIDO { get; set; }
        //public string MICRO_EMPRESA { get; set; }
        //public int PERC_DESC_FAT { get; set; }
        //public string PROTESTAR { get; set; }
        //public DateTime DATA_CADASTRO { get; set; }
        //public DateTime DATA_ALTERACAO { get; set; }
        //public DateTime DATA_INTEGRACAO { get; set; }
        //public int CD_CONTRATO { get; set; }
        //public DateTime DT_CANC_CRED { get; set; }
        //public DateTime DT_REAB_CRED { get; set; }
        //public int DIA_VENCIMENTO { get; set; }
        //public string ICMS_EMBUTIDO { get; set; }
        //public DateTime DATA_ULT_TRANSP { get; set; }
        //public string CONTRIBUINTEICMS { get; set; }
        //public decimal VLRLIMITECOMPRACLI { get; set; }
        //public int COND_PGTO_1 { get; set; }
        //public int COND_PGTO_2 { get; set; }
        //public int COND_PGTO_3 { get; set; }
        //public int COND_PGTO_4 { get; set; }
        //public int COND_PGTO_5 { get; set; }
        //public int COND_PGTO_6 { get; set; }
        //public int VENDEDOR { get; set; }
        //public int NR_ENDERECO { get; set; }
        //public int COD_BAIRRO { get; set; }
        //public string TIPO_VENCTO { get; set; }
        //public int ROTA_ENTREG_COL { get; set; }
        //public int CUBAGEM { get; set; }
        //public string COMPROVA_ENTREG { get; set; }
        //public decimal INDICE_REENTREG { get; set; }
        //public string TIPO_JUROS_MORA { get; set; }
        //public decimal PERCENTUAL_JURO { get; set; }
        //public string FATURA_DOCTO { get; set; }
        //public string SIT_PEDAGIO { get; set; }
        //public string TIPO_BASE_TAB { get; set; }
        //public int CODIGO_EDI { get; set; }
        //public decimal TAXA_ADMINISTRA { get; set; }
        //public string RETER_INSS { get; set; }
        //public int QTDE_FRANQUIA_1 { get; set; }
        //public int QTDE_FRANQUIA_2 { get; set; }
        //public int TIPO_EMPRESA { get; set; }
        //public int SEQ_TABELA_NEG { get; set; }
        //public string SERIE_NF { get; set; }
        //public string SENHA_WEB { get; set; }
        //public string GERA_AVERBACAO { get; set; }
        //public decimal VLCREDITOCGS { get; set; }
        //public string AGENCIADETURISMO { get; set; }
        //public string PARCELAPAGTOSTUR { get; set; }
        //public string REGISTROEMBRATUR { get; set; }
        //public decimal PERCDESCONTOTUR { get; set; }
        //public decimal SALDOCREDORTUR { get; set; }
        //public string TEMDIREITOACORTESIATUR { get; set; }
        //public string TEMDIREITOADESCONTOTUR { get; set; }
        //public int NR_ENDCOBRANCACLI { get; set; }
        //public string COMPLCLI { get; set; }
        //public string COMPLCOBRANCACLI { get; set; }
        //public string CELULARCLI { get; set; }
        //public string TELEFONECOBRANCACLI { get; set; }
        //public string CAIXAPOSTALCLI { get; set; }
        //public string LOCALENTREGACLI { get; set; }
        //public int CODCLI_NOTADEBITO { get; set; }
        //public string NATUREZAJURIDICA { get; set; }
        //public int QTD_PARCELAS { get; set; }
        //public string PERMITEFATURARPEDIDOSTUR { get; set; }
        //public int CODREGIAOTRANSP { get; set; }
        //public int CODIGOZONA { get; set; }
        //public string TIPOFRETE { get; set; }
        //public int COD_CONDPAGTO_COMBUST { get; set; }
        //public int COD_CONDPAGTO_LUBR { get; set; }
        //public int COD_CONDPAGTO_FILTRO { get; set; }
        //public int COD_CONDPAGTO_OUTROS { get; set; }
        //public string CODIGOAUX1 { get; set; }
        //public int CODROTA { get; set; }
        //public string DESCEXIGENCIAS { get; set; }
        //public string CAIXAPOSTAL { get; set; }
        //public string DIREITOCREDITOICMS { get; set; }
        //public int COD_SEQ_AGENCIA { get; set; }
        //public decimal PERC_DESCONTO_MES { get; set; }
        //public int CGS_EMPRESA_RESPCOML { get; set; }
        //public int CGS_FILIAL_RESPCOML { get; set; }
        //public int CGS_GARAGEM_RESPCOML { get; set; }
        //public decimal PERC_MULTA { get; set; }
        //public string INSTRUCAO_PROTESTO { get; set; }
        //public string SIGLA_PAIS { get; set; }
        //public int PRIORIDADE { get; set; }
        //public string TPINSCESTADUALCLI { get; set; }
        //public string PRODUTORRURAL { get; set; }
        //public string PRODRURALCOB { get; set; }
        //public int CODSEGNEG { get; set; }
        //public string VENC_CAPRESENTACAO { get; set; }
        //public int CODAGREGULADO { get; set; }
        //public int CODINSTALACAO { get; set; }
        //public int ATIV_ECONOMICA { get; set; }
        //public string INF_ADICIONAIS { get; set; }
        //public int CODIGO_EMP_HISTO { get; set; }
        //public int CODIGO_FL_HISTO { get; set; }
        //public int CODIGO_HISTO_TUR { get; set; }
        //public string UTILIZA_VEND_DIVISAO { get; set; }
        //public string CONSULTOU_CNPJ_RECEITA { get; set; }
        //public int CODMUNIC { get; set; }
        //public string CLI_OPT_SIMPLES_NACIONAL { get; set; }

    }
}