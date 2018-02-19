using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Mask;
using System.ComponentModel;

namespace FGlobus.Componentes.WinForms
{
    #region ClassMascaras

    
    public class ClassMascaras
    {
        #region Constructor

        public int Codigo 
        {
            get
            { 
                return this._codigo;
            }
            set 
            {
                this._codigo = value;
            }
        }

        public string Tipo
        {
            get
            {
                return this._tipo;
            }
            set
            {
                this._tipo = value;
            }
        }

        public string DescricaoTipo
        {
            get
            {
                return this._descricaoTipo;
            }
            set
            {
                this._descricaoTipo = value;
            }
        }

        public string Exemplo
        {
            get
            {
                return this._exemplo;
            }
            set
            {
                this._exemplo = value;
            }
        }

        public enum TiposControles
        {
            CalcEdit,
            DateEdit,
            TextEdit,
            ButtonEdit
        };
        public TiposControles[] Controles
        {
            get
            {
                return this._listaControles;
            }
            set
            {
                this._listaControles = value;
            }
        }

        public string EditMask
        {
            get
            {
                return this._editMask;
            }
            set
            {
                this._editMask = value;
            }
        }

        public MaskType MaskType
        {
            get
            {
                return this._maskType;
            }
            set
            {
                this._maskType = value;
            }
        }

        public char PasswordChar
        {
            get
            {
                return this._passwordChar;
            }
            set
            {
                this._passwordChar = value;
            }
        }

        public char PlaceHolder
        {
            get
            {
                return this._placeHolder;
            }
            set
            {
                this._placeHolder = value;
            }
        }

        #endregion

        #region Atributtes

        private int _codigo = 0;
        private string _tipo = String.Empty;
        private string _descricaoTipo = String.Empty;
        private string _exemplo = String.Empty;
        private TiposControles[] _listaControles = null;
        private string _editMask = string.Empty;
        private MaskType _maskType = MaskType.None;
        private char _passwordChar = ' ';
        private char _placeHolder = char.MinValue;

        #endregion

        #region Override

        public override string ToString()
        {
            return _descricaoTipo;
        }

        #endregion

        #region Set

        internal void SetCodigo(int codigo)
        {
            this._codigo = codigo;
        }

        internal void SetTipo(string tipo)
        {
            this._tipo = tipo;
        }

        internal void SetDescricaoTipo(string descricaoTipo)
        {
            this._descricaoTipo = descricaoTipo;
        }

        internal void SetExemplo(string exemplo)
        {
            this._exemplo = exemplo;
        }

        internal void SetListaControles(TiposControles[] listaControles)
        {
            this._listaControles = listaControles;
        }

        internal void SetEditMask(string editMask)
        {
            this._editMask = editMask;
        }

        internal void SetMaskType(MaskType maskType)
        {
            this._maskType = maskType;
        }

        internal void SetPasswordChar(char passwordChar)
        {
            this._passwordChar = passwordChar;
        }

        internal void SetPlaceHolder(char placeHolder)
        {
            this._placeHolder = placeHolder;
        }
        
        #endregion
    }

    #endregion

    #region ClassMascarasTypeConverter

    public class ClassMascarasTypeConverter : TypeConverter
    {
        public ClassMascarasTypeConverter()
        { }
        
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(ClassMascaras));
        }
    }

    #endregion
}
