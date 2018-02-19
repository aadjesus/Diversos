using System;
using FGlobus.Comum.DTO;

namespace Globus5.GestaoDeFrotaOnLine.DTO
{
    /// <summary>
    /// Classe de entidade de banco de dados que representa a tabela relacional GFO_OPERADORA
    /// <remarks>
    /// Mapeamento OR em OperadoraDTO.hbm.xml
    /// </remarks>
    /// </summary>
	[Serializable]
	public class OperadoraDTO : DataTransferObject 
	{
		#region Private Members
			
		private bool _isChanged;
		private bool _isDeleted;
		private int _codOperadora; 
		private string _descOperadora; 		

		#endregion
		
		#region Default ( Empty ) Class Constructor
		
        /// <summary>
        /// Construtor default
        /// </summary>
		public OperadoraDTO()
		{
			_codOperadora = 0; 
			_descOperadora = null; 
		}
		
		
		#endregion // End of Default ( Empty ) Class Constructor

		#region Public Properties
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
		public virtual int CodOperadora
		{
			get
			{
				return _codOperadora; 
			}
			set
			{ 
				_isChanged |= (_codOperadora != value); 
				_codOperadora = value; 
			}
		}
			
        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
        /// </summary>		
		public virtual string DescOperadora
		{
			get
			{
				return _descOperadora; 
			}
			set	
			{
				_isChanged |= (_descOperadora != value); 
				_descOperadora = value;
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
			OperadoraDTO castObj = (OperadoraDTO)obj; 
			return ( castObj != null ) &&
				   ( this._codOperadora == castObj.CodOperadora );
		}
		
        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * _codOperadora.GetHashCode();
			return hash; 
		}
		
		#endregion
	}
}
