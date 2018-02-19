using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FGlobus.Componentes.WinForms.Mascaras
{
    #region Enumeradores

    public enum eDateEditBGM
    {
        Permitir,
        Alertar,
        Bloquear
    }

    public enum eTipoDateEditBGM
    {
        Progressiva,
        Retroativa
    }

    #endregion

    #region Class

    [TypeConverter(typeof(ClassDateEditBGMTypeConverter))]
    public class ClassDateEditBGM
    {

        #region Atributtes

        private bool _regraAtiva = false;
        private string _mensagemAviso = String.Empty;
        private int _quantidadeDias = 0;
        private eDateEditBGM _eDateEditBGM = eDateEditBGM.Permitir;

        #endregion

        #region Properties

        public bool Ativo
        {
            get
            {
                return this._regraAtiva;
            }
            set
            {
                this._regraAtiva = value;
            }
        }

        public string MensagemAviso
        {
            get
            {
                return this._mensagemAviso;
            }
            set
            {
                this._mensagemAviso = value;
            }
        }

        public int QuantidadeDias
        {
            get
            {
                return this._quantidadeDias;
            }
            set
            {
                this._quantidadeDias = value;
            }
        }

        public eDateEditBGM PassarAQuantidadeDeDias
        {
            get
            {
                return this._eDateEditBGM;
            }
            set
            {
                this._eDateEditBGM = value;
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ClassDateEditBGM()
        {
        }

        public override string ToString()
        {
            return
                this._regraAtiva.ToString();
        }
    }

    public delegate void PropertyChangedEventHandler(string propertyname);

    #endregion

    #region TypeConverter

    public class ClassDateEditBGMTypeConverter : TypeConverter
    {
        public ClassDateEditBGMTypeConverter()
        { }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(ClassDateEditBGM));
        }
    }

    #endregion    
}
