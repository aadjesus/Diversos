using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DevExpress.XtraEditors.Controls;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Classe para inclusão e controle de itens em DX.ComboBoxEdit
    /// </summary>
    /// <typeparam name="T">O tipo de dado armazenado em value.</typeparam>
    public class ListItemBGM<T>
    {
        private string _text;
        private T _value;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="text">O texto a ser exibido.</param>
        /// <param name="value">Objeto do tipo T para retorno de seleção.</param>
        public ListItemBGM(string text, T value)
        {
            _text = text;
            _value = value;
        }

        /// <summary>
        /// (Get/Set) Informa ou retorna o texto que será exibido na lista.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        /// <summary>
        /// (Get/Set) Informa ou retorna o valor de retorno da lista.
        /// </summary>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Converter para string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// Procura o valor de uma propriedade na lista informada e retorna seu índice.
        /// </summary>
        /// <param name="listaDeItens">A lista de valores do tipo T.</param>
        /// <param name="propertyId">Nome da propriedade de T.</param>
        /// <param name="value">Valor a procurar na lista.</param>
        /// <returns>Inteiro contendo o índice encontrato. Retorna -1 se não encontrado.</returns>
        public static int FindByPropertyValue(IList<T> listaDeItens, string propertyId, object value)
        {
            int retVal = -1;
            if ((listaDeItens.Count > 0) && 
                (!String.IsNullOrEmpty(propertyId)) &&
                (value != null))
            {
                int contador = 0;
                foreach (T item in listaDeItens)
                {
                    PropertyInfo propertyInfo = item.GetType().GetProperty(propertyId);
                    if (propertyInfo != null)
                    {
                        if (propertyInfo.GetValue(item, null).ToString() == value.ToString())
                        {
                            retVal = contador;
                            break;
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
            }
            return retVal;
        }



        /// <summary>
        /// Procura o valor de uma propriedade na lista informada e retorna seu índice.
        /// </summary>
        /// <param name="listaDeItens">ComboBoxItemCollection contendo ListItemBGM<T>.</param>
        /// <param name="propertyId">Nome da propriedade de T.</param>
        /// <param name="value">Valor a procurar na lista.</param>
        /// <returns>Inteiro contendo o índice encontrato. Retorna -1 se não encontrado.</returns>
        public static int FindByPropertyValue(ComboBoxItemCollection listaDeItens, string propertyId, object value)
        {
            int retVal = -1;
            if ((listaDeItens.Count > 0) &&
                (!String.IsNullOrEmpty(propertyId)) && 
                (value != null) &&
                (listaDeItens[0] is ListItemBGM<T>))                
            {
                int contador = 0;
                foreach (ListItemBGM<T> item in listaDeItens)
                {
                    PropertyInfo propertyInfo = item.Value.GetType().GetProperty(propertyId);
                    if (propertyInfo != null)
                    {
                        if (propertyInfo.GetValue(item.Value, null).ToString() == value.ToString())
                        {
                            retVal = contador;
                            break;
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Procura o valor de 'Value' na lista informada e retorna seu índice.
        /// </summary>
        /// <param name="listaDeItens">ComboBoxItemCollection contendo ListItemBGM<T>.</param>
        /// <param name="value">Valor a procurar na lista.</param>
        /// <returns>Inteiro contendo o índice encontrato. Retorna -1 se não encontrado.</returns>
        public static int FindByValue(ComboBoxItemCollection listaDeItens, object value)
        {
            int retVal = -1;
            if ((listaDeItens.Count > 0) &&
                (value != null) &&
                (listaDeItens[0] is ListItemBGM<T>))
            {
                int contador = 0;
                foreach (ListItemBGM<T> item in listaDeItens)
                {
                    if (item.Value.ToString() == value.ToString())
                    {
                        retVal = contador;
                        break;
                    }
                    else
                    {
                        contador++;
                    }
                }
            }
            return retVal;
        }

    }
}
