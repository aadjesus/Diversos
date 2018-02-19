using System;
using FGlobus.Comum.DTO;

namespace Globus5.GestaoDeFrotaOnLine.DTO
{
    /// <summary>
    /// Classe de entidade de banco de dados que representa a tabela relacional GFO_USUARIO_LINHA
    /// <remarks>
    /// Mapeamento OR em UsuarioLinhaDTO.hbm.xml
    /// </remarks>
    /// </summary>
	[Serializable]
	public class UsuarioLinhaDTO : DataTransferObject 
	{
		#region Private Members
			
		private bool _isChanged;
		private bool _isDeleted;
		private int _codIntLinha; 
		private string _usuario; 		

		#endregion
		
		#region Default ( Empty ) Class Constructor
		
        /// <summary>
        /// Construtor default
        /// </summary>
		public UsuarioLinhaDTO()
		{
			_codIntLinha = 0; 
			_usuario = null; 
		}
				
		/// <summary>
       	/// Construtor com chave primária.
       	/// </summary>
		/// <param name="codintlinha">codintlinha</param>
		/// <param name="usuario">usuario</param>
		public UsuarioLinhaDTO(int codintlinha, string usuario)
		{
			_codIntLinha = codintlinha;
			_usuario = usuario;
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
		public virtual string Usuario
		{
			get
			{
				return _usuario; 
			}
			set	
			{
				_isChanged |= (_usuario != value); 
				_usuario = value;
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
			UsuarioLinhaDTO castObj = (UsuarioLinhaDTO)obj; 
			return ( castObj != null ) &&
				   ( this._codIntLinha == castObj.CodIntLinha ) &&
				   ( this._usuario == castObj.Usuario );
		}
		
        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * _codIntLinha.GetHashCode();
			hash = 27 * hash * _usuario.GetHashCode();
			return hash; 
		}
		
		#endregion
	}
}
