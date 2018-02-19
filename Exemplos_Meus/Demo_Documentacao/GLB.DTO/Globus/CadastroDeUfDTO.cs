using System;
using FGlobus.Comum.DTO;

namespace Globus5.Globus.DTO
{
    /// <summary>
    /// Classe de entidade de banco de dados que representa a tabela relacional BGMCADUF
    /// <remarks>
    /// Mapeamento OR em CadastroDeUf.hbm.xml
    /// </remarks>
    /// </summary>
	[Serializable]
	public class CadastroDeUfDTO : DataTransferObject 
	{
		#region Private Members
			
		private bool _isChanged;
		private bool _isDeleted;
		private string _codigoUf; 
		private string _descricaoUf; 
		private decimal? _aliquotaIcmsInterna; 
		private decimal? _aliquotaIcmsExterna; 
		private decimal? _aliqPrestServico;
        private int? _codigoIbge; 		

		#endregion
		
		#region Default ( Empty ) Class Constructor
		
        /// <summary>
        /// Construtor default.
        /// </summary>
		public CadastroDeUfDTO()
		{
			_codigoUf = null; 
			_descricaoUf = null; 
			_aliquotaIcmsInterna =  null; 
			_aliquotaIcmsExterna =  null; 
			_aliqPrestServico =  null; 
			_codigoIbge =  null; 
		}
		
		#endregion // End of Default ( Empty ) Class Constructor

		#region Public Properties
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>
		public virtual string CodigoUf
		{
			get
			{
				return _codigoUf; 
			}
			set	
			{
				_isChanged |= (_codigoUf != value); 
				_codigoUf = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>
		public virtual string DescricaoUf
		{
			get
			{
				return _descricaoUf; 
			}
			set	
			{
				_isChanged |= (_descricaoUf != value); 
				_descricaoUf = value;
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="decimal"/>.
        /// </summary>
		public virtual decimal? AliquotaIcmsInterna
		{
			get
			{
				return _aliquotaIcmsInterna; 
			}
			set
			{ 
				_isChanged |= (_aliquotaIcmsInterna != value); 
				_aliquotaIcmsInterna = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="decimal"/>.
        /// </summary>
		public virtual decimal? AliquotaIcmsExterna
		{
			get
			{
				return _aliquotaIcmsExterna; 
			}
			set
			{ 
				_isChanged |= (_aliquotaIcmsExterna != value); 
				_aliquotaIcmsExterna = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="decimal"/>.
        /// </summary>
		public virtual decimal? AliqPrestServico
		{
			get
			{
				return _aliqPrestServico; 
			}
			set
			{ 
				_isChanged |= (_aliqPrestServico != value); 
				_aliqPrestServico = value; 
			}
		}

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>
        public virtual int? CodigoIbge
		{
			get
			{
				return _codigoIbge; 
			}
			set
			{ 
				_isChanged |= (_codigoIbge != value); 
				_codigoIbge = value; 
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
			CadastroDeUfDTO castObj = (CadastroDeUfDTO)obj; 
			return ( castObj != null ) &&
				   ( this._codigoUf == castObj.CodigoUf );
		}
		
        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * _codigoUf.GetHashCode();
			return hash; 
		}
		
		#endregion
	}
}
