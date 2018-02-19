using System;
using FGlobus.Comum.DTO;

namespace Globus5.Abastecimento.DTO
{
    /// <summary>
    /// Classe de entidade de banco de dados que representa a tabela relacional ABA_CADBOMBA
    /// <remarks>
    /// Mapeamento OR em BombaDTO.hbm.xml
    /// </remarks>
    /// </summary>
	[Serializable]
	public class BombaDTO : DataTransferObject 
	{
		#region Private Members
			
		private bool _isChanged;
		private bool _isDeleted;
		private int _codigo; 
		private string _descricao; 
		private int _codigoGaragem; 
		private string _codigoTipoDeOleo; 
		private int _viradaDoHodometro; 
		private string _codigoBombaNoArquivoCTF;
        private string _terminalGtFrota;
        private string _bombaGtFrota;
        private int _codigoEmpresaGtFrota;
        private int _codigoFilialGtFrota;

		#endregion
		
		#region Default ( Empty ) Class Constructor
		
        /// <summary>
        /// Construtor default
        /// </summary>
		public BombaDTO()
		{
			_codigo = 0; 
			_descricao = null; 
			_codigoGaragem = 0; 
			_codigoTipoDeOleo = null; 
			_viradaDoHodometro = 0; 
			_codigoBombaNoArquivoCTF = null;
            _terminalGtFrota = null;
            _bombaGtFrota = null;
            _codigoEmpresaGtFrota = 0;
            _codigoFilialGtFrota = 0; 
		}
		
		
		#endregion // End of Default ( Empty ) Class Constructor

		#region Public Properties
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int Codigo
		{
			get
			{
				return _codigo; 
			}
			set
			{ 
				_isChanged |= (_codigo != value); 
				_codigo = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string Descricao
		{
			get
			{
				return _descricao; 
			}
			set	
			{
				_isChanged |= (_descricao != value); 
				_descricao = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int CodigoGaragem
		{
			get
			{
				return _codigoGaragem; 
			}
			set
			{ 
				_isChanged |= (_codigoGaragem != value); 
				_codigoGaragem = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string CodigoTipoOleo
		{
			get
			{
				return _codigoTipoDeOleo; 
			}
			set	
			{
				_isChanged |= (_codigoTipoDeOleo != value); 
				_codigoTipoDeOleo = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int ViradaHodometroBomba
		{
			get
			{
				return _viradaDoHodometro; 
			}
			set
			{ 
				_isChanged |= (_viradaDoHodometro != value); 
				_viradaDoHodometro = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
        public virtual string CodigoBombaNoArquivoCTF
        {
            get
            {
                return _codigoBombaNoArquivoCTF;
            }
            set
            {
                _isChanged |= (_codigoBombaNoArquivoCTF != value);
                _codigoBombaNoArquivoCTF = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
        public virtual string TerminalGtFrota
        {
            get
            {
                return _terminalGtFrota;
            }
            set
            {
                _isChanged |= (_terminalGtFrota != value);
                _terminalGtFrota = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
        public virtual string BombaGtFrota
        {
            get
            {
                return _bombaGtFrota;
            }
            set
            {
                _isChanged |= (_bombaGtFrota != value);
                _bombaGtFrota = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public virtual int CodigoEmpresaGtFrota
        {
            get
            {
                return _codigoEmpresaGtFrota;
            }
            set
            {
                _isChanged |= (_codigoEmpresaGtFrota != value);
                _codigoEmpresaGtFrota = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public virtual int CodigoFilialGtFrota
        {
            get
            {
                return _codigoFilialGtFrota;
            }
            set
            {
                _isChanged |= (_codigoFilialGtFrota != value);
                _codigoFilialGtFrota = value;
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
			BombaDTO castObj = (BombaDTO)obj; 
			return ( castObj != null ) &&
				   ( this._codigo == castObj.Codigo );
		}
		
        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * _codigo.GetHashCode();
			return hash; 
		}
		
		#endregion
	}
}
