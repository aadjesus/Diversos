using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DevExpress.XtraEditors.Controls;
using System.Reflection;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using ClassLibrary1;
using System.Windows.Forms;

namespace FGlobus.Util
{
    public static class ExtensaoDeMetodos
    {
        private static string RemoverCaracterEspecialOuAcentuacao(string valor, bool acentuacao = false, String subistituirPor = null)
        {
            if (String.IsNullOrEmpty(valor))
                return valor;
            else
                return valor
                    .Select(s => s.ToString())
                    .Aggregate(new StringBuilder(), (a, b) =>
                    {
                        if (acentuacao)
                        {
                            string caracter = b.Normalize(NormalizationForm.FormD);
                            if (CharUnicodeInfo.GetUnicodeCategory(caracter[0]) != UnicodeCategory.NonSpacingMark)
                                a.Append(caracter[0]);
                        }
                        else
                        {
                            if ((b[0] >= '0' && b[0] <= '9') ||
                                (b[0] >= 'A' && b[0] <= 'Z') ||
                                (b[0] >= 'a' && b[0] <= 'z'))
                                a.Append(b);
                            else if (!String.IsNullOrEmpty(subistituirPor))
                                a.Append(subistituirPor);
                        }

                        return a;
                    }).ToString();
        }

        /// <summary>
        /// Remove os caracteres especiais.
        /// </summary>
        /// <param name="valor">String.</param>
        /// <param name="subistituirPor">Substitui caracter especial encontrado pelo informado.</param>
        /// <returns>String.</returns>
        public static string RemoverCaracterEspecial(this string valor, String subistituirPor = null)
        {
            return RemoverCaracterEspecialOuAcentuacao(valor, false, subistituirPor);
        }

    }


    public static class Constantes
    {
        public const string NONE_DEFAULT = "";
        public const string CATEGORIA = "BgmRodotec";

    }

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


    internal class ControlaComponenteEditor : UITypeEditor
    {
        #region Atributos

        private System.Windows.Forms.ListBox _listBox;
        private IWindowsFormsEditorService _windowsFormsEditorService;

        #endregion

        #region Override metodos

        /// <summary>
        /// Retorna o tipo de execução do evento
        /// </summary>
        /// <param name="controle">Controle que chamou o editor</param>
        /// <returns>UITypeEditorEditStyle</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext controle)
        {
            //if (controle != null)
            //{
            //    if (controle.Instance is AutorizaGridBGM)
            //        return UITypeEditorEditStyle.DropDown;
            //    else
            //        return UITypeEditorEditStyle.Modal;

            //}

            return base.GetEditStyle(controle);
        }

        /// <summary>
        /// Retorna o valor do objeto selecioando
        /// </summary>
        /// <param name="contexto">Controle que chamou o editor</param>
        /// <param name="provider">Tipo de solicitação do evento.</param>
        /// <param name="valor">Valor do controle selecionado</param>
        /// <returns>object</returns>
        public override object EditValue(ITypeDescriptorContext contexto, IServiceProvider provider, object valor)
        {
            if (contexto != null && provider != null)
            {
                _windowsFormsEditorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

                if (_windowsFormsEditorService != null)
                {
                    return RetornarAutorizaGridBGM(contexto, valor);
                }

                return valor;
            }

            return valor;
        }

        #endregion

        #region ItemBarraItens

        private object RetornarAutorizaGridBGM(ITypeDescriptorContext contexto, object valor)
        {
            AutorizaGridBGMx2 itemBarraItens = contexto.Instance as AutorizaGridBGMx2;
            CheckedListBox listBox = new CheckedListBox();
            listBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            listBox.KeyDown += new KeyEventHandler(listBox_KeyDown);
            listBox.Leave += new EventHandler(listBox_Leave);
            listBox.DoubleClick += new EventHandler(listBox_Leave);
            listBox.Click += new EventHandler(listBox_Leave);


            listBox.Items.AddRange(itemBarraItens.Colunas
                .OfType<ClassLibrary1.AutorizaGridBGMx2.ColunaAutorizaGridBGM>()
                .Select(s => new ListItemBGM<ClassLibrary1.AutorizaGridBGMx2.ColunaAutorizaGridBGM>(s.Titulo, s))
                .ToArray());

            _windowsFormsEditorService.DropDownControl(listBox);
            if (listBox.SelectedItem != null)
            {
                
                return (listBox.SelectedItem as ListItemBGM<ClassLibrary1.AutorizaGridBGMx2.ColunaAutorizaGridBGM>).Text;
            }

            return valor;
        }

        private void listBox_Leave(object sender, EventArgs e)
        {
            if (_windowsFormsEditorService != null)
                _windowsFormsEditorService.CloseDropDown();
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_windowsFormsEditorService != null &&
                e != null &&
                e.Control &&
                e.KeyCode.Equals(Keys.Return))
                _windowsFormsEditorService.CloseDropDown();
        }

        #endregion

    }


}

