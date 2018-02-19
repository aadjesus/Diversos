using System;
using FGlobus.Comum.DTO;

namespace Globus5.Globus.DTO
{
    /// <summary>
    /// Classe de entidade de banco de dados que representa a tabela relacional BGM_CADLINHAS
    /// <remarks>
    /// Mapeamento OR em LinhaDTO.hbm.xml
    /// </remarks>
    /// </summary>
	[Serializable]
	public class LinhaDTO : DataTransferObject 
	{
		#region Private Members
			
		private bool _isChanged;
		private bool _isDeleted;
		private int _codIntLinha; 
		private string _codigoOrgConc; 
		private int? _codLocalTerminalSec; 
		private int _codigoEmpresa;
        private int? _codigoGrupoLinha; 
		private int _codigoGa; 
		private string _codigoLinha; 
		private string _nroFicialLinha; 
		private string _nomeLinha; 
		private string _nomeAbrevLinha; 
		private string _loteLinha; 
		private decimal _comissaoMotLinha; 
		private decimal _comissaoCobLinha; 
		private string _tipoDeServico; 
		private DateTime _dataInicOperacaoLinha; 
		private string _flgRespGuia; 
		private int? _codigoRegiao; 
		private int? _origemLinha; 
		private int? _destinoLinha; 
		private int? _codAgenciaIda; 
		private int? _codAgenciaVolta; 
		private int _categoria; 
		private string _flgMunicInterest; 
		private int _ordem; 
		private string _flgPaese; 
		private string _flgCircular; 
		private string _logAlter; 
		private int _codigoFl; 
		private string _naoCriticaCircular; 
		private string _codigoImportacao; 
		private string _flgLinhaPrincipal; 
		private string _digitaSecao; 
		private string _tipoCircular; 
		private string _digitaQtd; 
		private int? _codIntlinhaComp; 
		private int _tempoMinViagem; 
		private string _codigoLinhaMin; 
		private string _digitaOrigemDestino; 
		private int? _codGrupolinhaPvt;
        private int _intervaloComboio;

        
        private int _fatorRenovacao;

        

		#endregion
		
		#region Default ( Empty ) Class Constructor
		
        /// <summary>
        /// Construtor default
        /// </summary>
		public LinhaDTO()
		{
			_codIntLinha = 0; 
			_codigoOrgConc = null;
            _codLocalTerminalSec = null; 
			_codigoEmpresa = 0; 
			_codigoGrupoLinha = 0; 
			_codigoGa = 0; 
			_codigoLinha = null; 
			_nroFicialLinha = null; 
			_nomeLinha = null; 
			_nomeAbrevLinha = null; 
			_loteLinha = null; 
			_comissaoMotLinha = 0; 
			_comissaoCobLinha = 0; 
			_tipoDeServico = null; 
			_dataInicOperacaoLinha = DateTime.MinValue; 
			_flgRespGuia = null; 
			_codigoRegiao = 0; 
			_origemLinha = 0; 
			_destinoLinha = 0; 
			_codAgenciaIda = 0; 
			_codAgenciaVolta = 0; 
			_categoria = 0; 
			_flgMunicInterest = null; 
			_ordem = 0; 
			_flgPaese = null; 
			_flgCircular = null; 
			_logAlter = null; 
			_codigoFl = 0; 
			_naoCriticaCircular = null; 
			_codigoImportacao = null; 
			_flgLinhaPrincipal = null; 
			_digitaSecao = null; 
			_tipoCircular = null; 
			_digitaQtd = null; 
			_codIntlinhaComp = 0; 
			_tempoMinViagem = 0; 
			_codigoLinhaMin = null; 
			_digitaOrigemDestino = null; 
			_codGrupolinhaPvt = 0;
            _intervaloComboio = 0;
            _fatorRenovacao = 0;
		}
		
		
		#endregion // End of Default ( Empty ) Class Constructor

		#region Public Properties
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int CodIntLinha
		{
			get
			{
				return _codIntLinha; 
			}
			set
			{ 
				_isChanged |= (_codIntLinha != value); 
				_codIntLinha = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string CodigoOrgConc
		{
			get
			{
				return _codigoOrgConc; 
			}
			set	
			{
				_isChanged |= (_codigoOrgConc != value); 
				_codigoOrgConc = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int? CodLocalTerminalSec
		{
			get
			{
				return _codLocalTerminalSec; 
			}
			set
			{ 
				_isChanged |= (_codLocalTerminalSec != value); 
				_codLocalTerminalSec = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int CodigoEmpresa
		{
			get
			{
				return _codigoEmpresa; 
			}
			set
			{ 
				_isChanged |= (_codigoEmpresa != value); 
				_codigoEmpresa = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int? CodigoGrupoLinha
		{
			get
			{
				return _codigoGrupoLinha; 
			}
			set
			{ 
				_isChanged |= (_codigoGrupoLinha != value); 
				_codigoGrupoLinha = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int CodigoGa
		{
			get
			{
				return _codigoGa; 
			}
			set
			{ 
				_isChanged |= (_codigoGa != value); 
				_codigoGa = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string CodigoLinha
		{
			get
			{
				return _codigoLinha; 
			}
			set	
			{
				_isChanged |= (_codigoLinha != value); 
				_codigoLinha = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string NroFicialLinha
		{
			get
			{
				return _nroFicialLinha; 
			}
			set	
			{
				_isChanged |= (_nroFicialLinha != value); 
				_nroFicialLinha = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string NomeLinha
		{
			get
			{
				return _nomeLinha; 
			}
			set	
			{
				_isChanged |= (_nomeLinha != value); 
				_nomeLinha = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string NomeAbreviado
		{
			get
			{
				return _nomeAbrevLinha; 
			}
			set	
			{
				_isChanged |= (_nomeAbrevLinha != value); 
				_nomeAbrevLinha = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string LoteLinha
		{
			get
			{
				return _loteLinha; 
			}
			set	
			{
				_isChanged |= (_loteLinha != value); 
				_loteLinha = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="decimal"/>.
        /// </summary>		
		public virtual decimal ComissaoMotLinha
		{
			get
			{
				return _comissaoMotLinha; 
			}
			set
			{ 
				_isChanged |= (_comissaoMotLinha != value); 
				_comissaoMotLinha = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="decimal"/>.
        /// </summary>		
		public virtual decimal ComissaoCobLinha
		{
			get
			{
				return _comissaoCobLinha; 
			}
			set
			{ 
				_isChanged |= (_comissaoCobLinha != value); 
				_comissaoCobLinha = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string TipoDeServico
		{
			get
			{
				return _tipoDeServico; 
			}
			set	
			{
				_isChanged |= (_tipoDeServico != value); 
				_tipoDeServico = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="DateTime"/>.
        /// </summary>		
        public virtual DateTime DataInicOperacaoLinha
		{
			get
			{
				return _dataInicOperacaoLinha; 
			}
			set
			{ 
				_isChanged |= (_dataInicOperacaoLinha != value); 
				_dataInicOperacaoLinha = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string FlgRespGuia
		{
			get
			{
				return _flgRespGuia; 
			}
			set	
			{
				_isChanged |= (_flgRespGuia != value); 
				_flgRespGuia = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public virtual int? CodigoRegiao
		{ 
			get
			{
				return _codigoRegiao; 
			}
			set
			{ 
				_isChanged |= (_codigoRegiao != value); 
				_codigoRegiao = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int? OrigemLinha
		{
			get
			{
				return _origemLinha; 
			}
			set
			{ 
				_isChanged |= (_origemLinha != value); 
				_origemLinha = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int? DestinoLinha
		{
			get
			{
				return _destinoLinha; 
			}
			set
			{ 
				_isChanged |= (_destinoLinha != value); 
				_destinoLinha = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int? CodAgenciaIda
		{
			get
			{
				return _codAgenciaIda; 
			}
			set
			{ 
				_isChanged |= (_codAgenciaIda != value); 
				_codAgenciaIda = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int? CodAgenciaVolta
		{
			get
			{
				return _codAgenciaVolta; 
			}
			set
			{ 
				_isChanged |= (_codAgenciaVolta != value); 
				_codAgenciaVolta = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int Categoria
		{
			get
			{
				return _categoria; 
			}
			set
			{ 
				_isChanged |= (_categoria != value); 
				_categoria = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string FlgMunicInterest
		{
			get
			{
				return _flgMunicInterest; 
			}
			set	
			{
				_isChanged |= (_flgMunicInterest != value); 
				_flgMunicInterest = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int Ordem
		{
			get
			{
				return _ordem; 
			}
			set
			{ 
				_isChanged |= (_ordem != value); 
				_ordem = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string FlgPaese
		{
			get
			{
				return _flgPaese; 
			}
			set	
			{
				_isChanged |= (_flgPaese != value); 
				_flgPaese = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string FlgCircular
		{
			get
			{
				return _flgCircular; 
			}
			set	
			{
				_isChanged |= (_flgCircular != value); 
				_flgCircular = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string LogAlter
		{
			get
			{
				return _logAlter; 
			}
			set	
			{
				_isChanged |= (_logAlter != value); 
				_logAlter = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int CodigoFl
		{
			get
			{
				return _codigoFl; 
			}
			set
			{ 
				_isChanged |= (_codigoFl != value); 
				_codigoFl = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string NaoCriticaCircular
		{
			get
			{
				return _naoCriticaCircular; 
			}
			set	
			{
				_isChanged |= (_naoCriticaCircular != value); 
				_naoCriticaCircular = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string CodigoImportacao
		{
			get
			{
				return _codigoImportacao; 
			}
			set	
			{
				_isChanged |= (_codigoImportacao != value); 
				_codigoImportacao = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
        public virtual string FlgLinhaPrincipal
		{
			get
			{
				return _flgLinhaPrincipal; 
			}
			set	
			{
				_isChanged |= (_flgLinhaPrincipal != value); 
				_flgLinhaPrincipal = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string DigitaSecao
		{
			get
			{
				return _digitaSecao; 
			}
			set	
			{
				_isChanged |= (_digitaSecao != value); 
				_digitaSecao = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string TipoCircular
		{
			get
			{
				return _tipoCircular; 
			}
			set	
			{
				_isChanged |= (_tipoCircular != value); 
				_tipoCircular = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string DigitaQtd
		{
			get
			{
				return _digitaQtd; 
			}
			set	
			{
				_isChanged |= (_digitaQtd != value); 
				_digitaQtd = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int? CodIntLinhaComp
		{
			get
			{
				return _codIntlinhaComp; 
			}
			set
			{ 
				_isChanged |= (_codIntlinhaComp != value); 
				_codIntlinhaComp = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int TempoMinViagem
		{
			get
			{
				return _tempoMinViagem; 
			}
			set
			{ 
				_isChanged |= (_tempoMinViagem != value); 
				_tempoMinViagem = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string CodigoLinhaMin
		{
			get
			{
				return _codigoLinhaMin; 
			}
			set	
			{
				_isChanged |= (_codigoLinhaMin != value); 
				_codigoLinhaMin = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string DigitaOrigemDestino
		{
			get
			{
				return _digitaOrigemDestino; 
			}
			set	
			{
				_isChanged |= (_digitaOrigemDestino != value); 
				_digitaOrigemDestino = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int? CodGrupoLinhaPvt
		{
			get
			{
				return _codGrupolinhaPvt; 
			}
			set
			{ 
				_isChanged |= (_codGrupolinhaPvt != value); 
				_codGrupolinhaPvt = value; 
			}
		}

        /// <summary>
        /// 
        /// </summary>
        public int IntervaloComboio
        {
            get
            {
                return _intervaloComboio;
            }
            set
            {
                _isChanged |= (_intervaloComboio != value);
                _intervaloComboio = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int FatorRenovacao
        {
            get
            {
                return _fatorRenovacao;
            }
            set
            {
                _isChanged |= (_fatorRenovacao != value);
                _fatorRenovacao = value;
            }
        }
			
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
		public override bool Equals( object obj )
		{
			if( this == obj ) 
				return true;
			if( ( obj == null ) || ( obj.GetType() != this.GetType() ) ) 
				return false;
			LinhaDTO castObj = (LinhaDTO)obj; 
			return ( castObj != null ) &&
				   ( this._codIntLinha == castObj.CodIntLinha );
		}
		
        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * _codIntLinha.GetHashCode();
			return hash; 
		}
		
		#endregion
	}
}
