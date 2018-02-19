using System;
using FGlobus.Comum.DTO;

namespace Globus5.GestaoDeFrotaOnLine.DTO
{
    /// <summary>
    /// Classe de entidade de banco de dados que representa a tabela relacional GFO_PARAMETROS
    /// <remarks>
    /// Mapeamento OR em ParametrosDTO.hbm.xml
    /// </remarks>
    /// </summary>
    [Serializable]
    public class ParametrosDTO : DataTransferObject
    {
        #region Private Members

        private bool _isChanged;
        private bool _isDeleted;
        private int _codigoEmpresa;
        private int _codigoFl;
        private int _minutosAtraso;
        private int _minutosAdiantado;
        private int? _ocorrenciaComboio;
        private int? _ocorrenciaAtraso;
        private int? _ocorrenciaAdiantado;
        private int? _ocorrenciaNoHorario;
        private int? _ocorrenciaProgNaoRea;
        private int? _ocorrenciaReaNaoProg;
        private string _keyGoogleMaps;
        private string _idAgenciaGoogleTransit;

        #endregion

        #region Default ( Empty ) Class Constructor

        /// <summary>
        /// Construtor default
        /// </summary>
        public ParametrosDTO()
        {
            _codigoEmpresa = 0;
            _codigoFl = 0;
            _minutosAtraso = 0;
            _minutosAdiantado = 0;
            _ocorrenciaComboio = null;
            _ocorrenciaAtraso = null;
            _ocorrenciaAdiantado = null;
            _ocorrenciaNoHorario = null;
            _ocorrenciaProgNaoRea = null;
            _ocorrenciaReaNaoProg = null;
            _keyGoogleMaps = null;
            _idAgenciaGoogleTransit = null;
        }

        /// <summary>
        /// Construtor com chave primária.
        /// </summary>
        /// <param name="codigoEmpresa">codigoempresa</param>
        /// <param name="codigoFl">codigofl</param>
        public ParametrosDTO(int codigoEmpresa, int codigoFl)
        {
            _codigoEmpresa = codigoEmpresa;
            _codigoFl = codigoFl;
        }

        #endregion // End of Default ( Empty ) Class Constructor

        #region Public Properties

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
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public virtual int MinutosAtraso
        {
            get
            {
                return _minutosAtraso;
            }
            set
            {
                _isChanged |= (_minutosAtraso != value);
                _minutosAtraso = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public virtual int MinutosAdiantado
        {
            get
            {
                return _minutosAdiantado;
            }
            set
            {
                _isChanged |= (_minutosAdiantado != value);
                _minutosAdiantado = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public virtual int? OcorrenciaComboio
        {
            get
            {
                return _ocorrenciaComboio;
            }
            set
            {
                _isChanged |= (_ocorrenciaComboio != value);
                _ocorrenciaComboio = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public virtual int? OcorrenciaAtraso
        {
            get
            {
                return _ocorrenciaAtraso;
            }
            set
            {
                _isChanged |= (_ocorrenciaAtraso != value);
                _ocorrenciaAtraso = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public virtual int? OcorrenciaAdiantado
        {
            get
            {
                return _ocorrenciaAdiantado;
            }
            set
            {
                _isChanged |= (_ocorrenciaAdiantado != value);
                _ocorrenciaAdiantado = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public int? OcorrenciaNoHorario
        {
            get
            {
                return _ocorrenciaNoHorario;
            }
            set
            {
                _isChanged |= (_ocorrenciaNoHorario != value);
                _ocorrenciaNoHorario = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>
        public int? OcorrenciaProgNaoRea
        {
            get
            {
                return _ocorrenciaProgNaoRea;
            }
            set
            {
                _isChanged |= (_ocorrenciaProgNaoRea != value);
                _ocorrenciaProgNaoRea = value;
            }
        }

        /// <summary>
        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
        /// </summary>		
        public int? OcorrenciaReaNaoProg
        {
            get
            {
                return _ocorrenciaReaNaoProg;
            }
            set
            {
                _isChanged |= (_ocorrenciaReaNaoProg != value);
                _ocorrenciaReaNaoProg = value;
            }
        }

        /// <summary>
        /// Chave (key) para acessar a API do Google Maps.
        /// </summary>
        public virtual string KeyGoogleMaps
        {
            get
            {
                return _keyGoogleMaps;
            }
            set
            {
                _isChanged |= (_keyGoogleMaps != value);
                _keyGoogleMaps = value;
            }
        }

        /// <summary>
        /// Identificação agência Google Transit
        /// </summary>
        public virtual string IdAgenciaGoogleTransit
        {
            get
            {
                return _idAgenciaGoogleTransit;
            }
            set
            {
                _isChanged |= (_idAgenciaGoogleTransit != value);
                _idAgenciaGoogleTransit = value;
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
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if ((obj == null) || (obj.GetType() != this.GetType()))
                return false;
            ParametrosDTO castObj = (ParametrosDTO)obj;
            return (castObj != null) &&
                   (this._codigoEmpresa == castObj.CodigoEmpresa) &&
                   (this._codigoFl == castObj.CodigoFl);
        }

        /// <summary>
        /// Retorna o valor do objeto.
        /// </summary>
        /// <returns><see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * _codigoEmpresa.GetHashCode();
            hash = 27 * hash * _codigoFl.GetHashCode();
            return hash;
        }

        #endregion
    }
}
