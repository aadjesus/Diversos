using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
//using System.Windows.Controls;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;
using FGlobus.Componentes.WinForms;
using FGlobus.Componentes.WinForms.ControleDeComponente;
using FGlobus.Util;

namespace FGlobus.Componentes.WinForms
{
    #region SelecionaTelaEditor

    /// <summary>
    /// Classe para habilitar a edição da classe "SelecionaTela".
    /// </summary>
    internal class SelecionaTelaEditor : UITypeEditor
    {
        #region Override metodos

        /// <summary>
        /// Retorna o tipo de execução do evento
        /// </summary>
        /// <param name="context">Controle que chamou o editor</param>
        /// <returns>UITypeEditorEditStyle</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null)
                return UITypeEditorEditStyle.Modal;

            return base.GetEditStyle(context);
        }

        /// <summary>
        /// Retorna o valor do objeto selecioando
        /// </summary>
        /// <param name="contexto">Controle que chamou o editor</param>
        /// <param name="prestador">Tipo de solicitação do evento.</param>
        /// <param name="valor">Valor do controle selecionado</param>
        /// <returns>object</returns>
        public override object EditValue(ITypeDescriptorContext contexto, IServiceProvider prestador, object valor)
        {
            if (contexto != null && prestador != null)
            {
                IWindowsFormsEditorService service = (IWindowsFormsEditorService)prestador.GetService(typeof(IWindowsFormsEditorService));
                SelecionaTela frmSelecionaTela = new SelecionaTela((System.Windows.Forms.Control)contexto.Instance);
                frmSelecionaTela.ShowDialog();
                if (frmSelecionaTela.DialogResult == DialogResult.OK)
                    valor = frmSelecionaTela.TelaSelecionada;
            }

            return valor;
        }

        #endregion
    }

    #endregion

    #region TipoMascarasEditor

    /// <summary>
    /// Classe para habilitar a edição da classe "TipoMascaras".
    /// </summary>
    internal class TipoMascarasEditor : UITypeEditor
    {
        #region Override metodos

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext controle)
        {
            if (controle != null)
            {
              //  MessageBox.Show("1");
                if (controle.PropertyDescriptor.PropertyType.Equals(typeof(ClassMascaras)))
                    return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(controle);
        }

        public override object EditValue(ITypeDescriptorContext _context, IServiceProvider _provider, object _value)
        {
            IWindowsFormsEditorService _iWindowsFormsEditorService =
                (IWindowsFormsEditorService)_provider.GetService(typeof(IWindowsFormsEditorService));

           // MessageBox.Show("2");
            //_value.GetType()  ==  typeof(FGlobus.Componentes.WinForms.ClassMascaras)
            if (_iWindowsFormsEditorService == null)
                return null;

            TipoMascaras _tipoMascaras = new TipoMascaras((System.Windows.Forms.Control)_context.Instance, _iWindowsFormsEditorService);
            _iWindowsFormsEditorService.DropDownControl(_tipoMascaras);

            return _tipoMascaras.MascaraSelecionada as ClassMascaras;
        }

        #endregion
    }

    #endregion

    /// <summary>
    /// Classe comum para mostrar botão no browser de propriedades com as propriedades da BGMRodotec.
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    internal class BotaoBrowserPropriedades : PropertyTab
    {
        #region Construtor

        /// <summary>
        /// Construtor padrao
        /// </summary>
        public BotaoBrowserPropriedades()
        {
        }

        #endregion

        #region Override metodos

        /// <summary>
        /// Retorna as propriedades do componente informado
        /// </summary>
        /// <param name="component">Componente</param>
        /// <param name="attributes">Atributos</param>
        /// <returns>PropertyDescriptorCollection</returns>
        public override PropertyDescriptorCollection GetProperties(object component, System.Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(
                TypeDescriptor.GetProperties(component, attributes)
                    .OfType<PropertyDescriptor>()
                    .Where(w => w.Category.Equals(FGlobus.Util.Constantes.CATEGORIA))
                    .Aggregate(new List<PropertyDescriptor>(), (lista, item) =>
                    {
                        lista.Add(TypeDescriptor.CreateProperty(
                                   item.ComponentType,
                                   item,
                                   item.Attributes
                                        .Cast<Attribute>()
                                        .ToArray()));
                        return lista;
                    })
                    .ToArray());
        }

        /// <summary>
        /// Retorna as propriedades do componente informado
        /// </summary>
        /// <param name="component">Componente</param>
        /// <returns>PropertyDescriptorCollection</returns>
        public override PropertyDescriptorCollection GetProperties(object component)
        {
            return this.GetProperties(component, null);
        }

        /// <summary>
        /// Define o nome da tab
        /// </summary>
        public override string TabName
        {
            get
            {
                return String.Concat("Propriedades ", FGlobus.Util.Constantes.CATEGORIA);
            }
        }

        // <summary>
        // Define o icone da tab
        // </summary>
        public override System.Drawing.Bitmap Bitmap
        {
            get
            {
                return Resources.ImagensBGM.Icone_Globus_2008.ToBitmap();
            }
        }

        /// <summary>
        /// Define se mostra a tab para o componente selecionado
        /// </summary>
        /// <param name="objeto">Componente</param>
        /// <returns>bool</returns>
        public override bool CanExtend(object objeto)
        {
            //GC.SuppressFinalize(this);
            //(objeto as System.Windows.Forms.Control).Invalidate();
            return objeto.GetType().Namespace.Equals("FGlobus.Componentes.WinForms");
            //return !System.Runtime.InteropServices.Marshal.IsComObject(objeto);
        }

        #endregion
    }

    /// <summary>
    /// Classe para controlar o designer dos controles que estão na tela
    /// </summary>
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
            if (controle != null)
            {
                //if (controle.Instance is ItemBarraItensBGM)
                //    return UITypeEditorEditStyle.DropDown;
                //else
                return UITypeEditorEditStyle.Modal;

            }

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
                    //if (contexto.Instance is ItemBarraItensBGM)
                    //    return RetornarItemBarraItens(contexto, valor);
                    //else if (contexto.Instance is ButtonEditBGM ||
                    //         contexto.Instance is BrowseDePesquisaBGM)
                    return RetornarSelecionaTela(contexto, valor);
                }

                return valor;
            }

            return valor;
        }

        #endregion



        #region SelecionaTela

        private object RetornarSelecionaTela(ITypeDescriptorContext contexto, object valor)
        {
            SelecionaTela frmSelecionaTela = new SelecionaTela((System.Windows.Forms.Control)contexto.Instance);
            frmSelecionaTela.ShowDialog();
            if (frmSelecionaTela.DialogResult == DialogResult.OK)
                valor = frmSelecionaTela.TelaSelecionada;

            return valor;
        }

        #endregion
    }

    /// <summary>
    /// Classe para controlar o designer do controle
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    internal class ControlaComponenteDesigner : ControlDesigner
    {
        #region Atributos

        private DesignerActionListCollection _designerActionListCollection;
        private List<PropriedadesEEventos> _listaPropriedadesEEventos;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Get) Retorna lista com as propriedades principais do componente
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_designerActionListCollection == null)
                {
                    _designerActionListCollection = new DesignerActionListCollection();
                    _designerActionListCollection.Add(new ControlaPrincipaisPropriedadesDesigner(this.Component));
                }
                return _designerActionListCollection;
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Inicializar o designer do controle
        /// </summary>
        /// <param name="component">Componente que esta sendo manipulado</param>
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);


            if (_listaPropriedadesEEventos == null)
                _listaPropriedadesEEventos = new List<PropriedadesEEventos>();

            _listaPropriedadesEEventos.Add(new PropriedadesEEventos(FGlobus.Util.Constantes.CATEGORIA));
            _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Layout"));
            _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Design"));
            _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Data"));
            _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Behavior"));
            _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Tooltip"));
            _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Events", tipo: PropriedadesEEventos.eTipo.Evento));

            if (component is LabelControlBGM)
            {
                //_listaPropriedadesEEventos.Add(new PropriedadesEEventos("Appearance", tipo: PropriedadesEEventos.eTipo.Propriedade));
            }
            else if (component is ButtonEditBGM ||
                     component is CalcEditBGM ||
                     component is DateEditBGM ||
                     component is TextEditBGM)
            {
                _listaPropriedadesEEventos.Add(new PropriedadesEEventos(nome: "Properties"));

                _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Action"));
                _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Focus"));
                _listaPropriedadesEEventos.Add(new PropriedadesEEventos("Key"));
            }
        }

        ///// <summary>
        ///// Verificar se o componente e um control 
        ///// </summary>
        ///// <param name="control">Componente que esta sendo manipulado</param>
        ///// <returns>bool</returns>
        //public override bool CanParent(System.Windows.Forms.Control control)
        //{
        //    return control is System.Windows.Forms.Control;
        //}

        ///// <summary>
        ///// Verificar se o componente e um control 
        ///// </summary>
        ///// <param name="controlDesigner">Componente que esta sendo manipulado</param>
        ///// <returns>bool</returns>
        //public override bool CanParent(System.Windows.Forms.Design.ControlDesigner controlDesigner)
        //{
        //    return controlDesigner != null &&
        //        controlDesigner.Control is System.Windows.Forms.Control;
        //}

        /// <summary>
        /// Procura a propriedade ou evento 
        /// </summary>
        /// <param name="memberDescriptor">MemberDescriptor</param>
        /// <returns>bool</returns>
        private bool ProcurarPropriedadeOuEvento(MemberDescriptor memberDescriptor)
        {
            // Verifica se o objeto não esta nulo, se esta visivel e nao existir na lista
            return memberDescriptor != null &&
                memberDescriptor.IsBrowsable &&
                _listaPropriedadesEEventos
                    .Where(w => w.Tipo != ((memberDescriptor is PropertyDescriptor)
                                                ? PropriedadesEEventos.eTipo.Evento
                                                : PropriedadesEEventos.eTipo.Propriedade) &&
                                (w.Nome.Equals(memberDescriptor.Name) ||
                                 w.Categoria.Equals(memberDescriptor.Category)))
                    .Count().Equals(0);
        }

        /// <summary>
        /// Remove as propriedades do controle
        /// </summary>
        /// <param name="propriedades">Propriedades do componente</param>
        protected override void PreFilterProperties(System.Collections.IDictionary propriedades)
        {
            try
            {
                // Remove as propriedades
                //propriedades.Keys
                //    .OfType<string>()
                //    .Where(w => ProcurarPropriedadeOuEvento(propriedades[w] as PropertyDescriptor))
                //    .ToList()
                //    .ForEach(f => propriedades.Remove(f));

                string[] copiaPropriedades = new string[propriedades.Keys.Count];
                propriedades.Keys.CopyTo(copiaPropriedades, 0);
                copiaPropriedades
                    .Where(w => ProcurarPropriedadeOuEvento(propriedades[w] as PropertyDescriptor))
                    .ToList()
                    .ForEach(f =>
                    {
                        PropertyDescriptor propriedade = propriedades[f] as PropertyDescriptor;
                        propriedades[f] = TypeDescriptor.CreateProperty(propriedade.ComponentType, propriedade, BrowsableAttribute.No);
                    });
            }
            catch (Exception)
            {
            }
            base.PreFilterProperties(propriedades);
        }

        /// <summary>
        /// Remove os eventos do controle
        /// </summary>
        /// <param name="eventos">Eventos do componente</param>
        protected override void PreFilterEvents(System.Collections.IDictionary eventos)
        {
            try
            {
                // Remove
                //eventos.Keys
                //    .OfType<string>()
                //    .Where(w => ProcurarPropriedadeOuEvento(eventos[w] as EventDescriptor))
                //    .ToList()
                //    .ForEach(f => eventos.Remove(f));

                string[] copiaEventos = new string[eventos.Keys.Count];
                eventos.Keys.CopyTo(copiaEventos, 0);
                copiaEventos
                    .Where(w => ProcurarPropriedadeOuEvento(eventos[w] as EventDescriptor))
                    .ToList()
                    .ForEach(f =>
                    {
                        EventDescriptor evento = eventos[f] as EventDescriptor;
                        eventos[f] = TypeDescriptor.CreateEvent(evento.ComponentType, evento, BrowsableAttribute.No);
                    });
            }
            catch (Exception)
            {
            }

            base.PreFilterEvents(eventos);
        }

        #endregion
    }

    /// <summary>
    /// Classe para controlar as opções principais propriedades do componente
    /// </summary>
    internal class ControlaPrincipaisPropriedadesDesigner : DesignerActionList
    {
        #region Construtor

        /// <summary>
        /// Construtor padrao
        /// </summary>
        /// <param name="component">Componente</param>
        public ControlaPrincipaisPropriedadesDesigner(IComponent component)
            : base(component)
        {
            _controle = component as System.Windows.Forms.Control;

            _designerActionUIService = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        #endregion

        #region Atributos

        private DesignerActionUIService _designerActionUIService = null;
        private readonly System.Windows.Forms.Control _controle;

        #endregion

        #region Metodos

        private PropertyDescriptor RetornarPropriedade(String propName)
        {
            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(_controle)[propName];
            if (propertyDescriptor == null)
                throw new ArgumentException("Propriedade não encontrada.", propName);
            else
                return propertyDescriptor;
        }

        /// <summary>
        /// Retorna as principais propriedades
        /// </summary>
        /// <returns>DesignerActionItemCollection</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            items.Add(new DesignerActionHeaderItem(FGlobus.Util.Constantes.CATEGORIA));

            // Retorna as propriedades da classe"ControlaPrincipaisPropriedadesDesigner" que estão com o atributo = "DescriptionAttribute" e o valor = "ControlaPrincipaisPropriedadesDesigner"
            List<PropertyInfo> listaPropriedades = this.GetType().GetProperties()
                .Where(w => w.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                .Where(w1 => (w1 as DescriptionAttribute).Description.Equals("ControlaPrincipaisPropriedadesDesigner"))
                                .Count() > 0)
                .OrderBy(o => o.Name)
                .ToList();

            // Cria os itens conforme as propriedades encontradas
            listaPropriedades
                .ForEach(f =>
                {
                    Dictionary<string, string> dictionary = RetornarValorAtributos(f.Name);
                    if (dictionary.Count > 0)
                        items.Add(new DesignerActionPropertyItem(
                            f.Name,
                            // Split, para remover a numeração caso exista.
                            dictionary["DisplayNameAttribute"].Split(')').LastOrDefault(),
                            FGlobus.Util.Constantes.CATEGORIA,
                            // Split, para remover o nome caso exista.
                            dictionary["DescriptionAttribute"].Split(new string[] { "\nNome:" }, StringSplitOptions.None).FirstOrDefault()));

                });

            items.Add(new DesignerActionMethodItem(this, "InformacoesComponente", "Informações", FGlobus.Util.Constantes.CATEGORIA, "Informações do componente.", true));

            return items;
        }

        private Dictionary<string, string> RetornarValorAtributos(string nomePropriedade)
        {
            try
            {
                // Procura os atributos DisplayName e Description na propriedade informada
                return _controle.GetType()
                    .GetProperty(nomePropriedade)
                    .GetCustomAttributes(true)
                    .Aggregate(new Dictionary<string, string>(), (retorno, item) =>
                    {
                        if (item.GetType().Equals(typeof(DisplayNameAttribute)))
                            retorno.Add("DisplayNameAttribute", (item as DisplayNameAttribute).DisplayName);
                        else
                            if (item.GetType().Equals(typeof(DescriptionAttribute)))
                                retorno.Add("DescriptionAttribute", (item as DescriptionAttribute).Description);

                        return retorno;
                    });
            }
            catch (Exception)
            {
                return new Dictionary<string, string>();
            }
        }

        private void InformacoesComponente()
        {
            var informacoesComponenteAttribute = _controle.GetType().GetCustomAttributes(typeof(InformacoesComponenteAttribute), true);

            if (informacoesComponenteAttribute.Count().Equals(0))
                informacoesComponenteAttribute = new object[] { new InformacoesComponenteAttribute() };

            new Informacoes(_controle, informacoesComponenteAttribute.FirstOrDefault() as InformacoesComponenteAttribute)
                .ShowDialog();
        }

        #endregion
    }

    /// <summary>
    /// Classe para controlar as propriedades ou eventos visiveis dos controles
    /// </summary>
    internal class PropriedadesEEventos
    {
        #region Construtor

        /// <summary>
        /// Construtor dafault
        /// </summary>
        public PropriedadesEEventos()
        {
            _categoria = "";
            _nome = "";
            _tipo = eTipo.Ambos;

        }

        /// <summary>
        /// Construtor padrao para definir a categoria, nome e o tipo
        /// </summary>
        /// <param name="categoria">Nome da categoria</param>
        /// <param name="nome">Nome da propriedade ou evento</param>
        /// <param name="tipo">Tipo</param>
        public PropriedadesEEventos(string categoria = "", string nome = "", eTipo tipo = eTipo.Ambos)
        {
            _categoria = categoria;
            _nome = nome;
            _tipo = tipo;
        }

        #endregion

        #region Atributos

        private string _categoria;
        private string _nome;
        private eTipo _tipo = eTipo.Ambos;

        /// <summary>
        /// Enum para controlar o tipo de objeto
        /// </summary>
        public enum eTipo
        {
            /// <summary>
            /// Propriedade
            /// </summary>
            Propriedade,
            /// <summary>
            /// Evento
            /// </summary>
            Evento,
            /// <summary>
            /// Ambos
            /// </summary>
            Ambos
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// (Set/Get) Informa ou retorna a categoria.
        /// </summary>
        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o nome da propriedade ou evento
        /// </summary>
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna tipo
        /// </summary>
        public eTipo Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        #endregion
    }

    /// <summary>
    /// Classe para controlar quem desenvolveu o componente
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal sealed class InformacoesComponenteAttribute : Attribute
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public InformacoesComponenteAttribute()
        {
            _autor = "";
            _versao = "";
            _documentacao = "";
        }

        /// <summary>
        /// Construtor padrao
        /// </summary>
        /// <param name="autor">Autor</param>
        /// <param name="versao">Versão</param>
        /// <param name="documentacao">Documentação</param>
        public InformacoesComponenteAttribute(string autor, string versao, string documentacao)
        {
            _autor = autor;
            _versao = versao;
            _documentacao = documentacao;
        }

        /// <summary>
        /// Construtor padrao
        /// </summary>
        /// <param name="autor">Autor</param>
        /// <param name="versao">Versão</param>
        public InformacoesComponenteAttribute(string autor, string versao)
        {
            _autor = autor;
            _versao = versao;
        }

        #endregion

        #region Atributos

        private readonly string _autor;
        private readonly string _versao;
        private readonly string _documentacao;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Get) Retorna o autor
        /// </summary>
        public string Autor
        {
            get { return _autor; }
        }

        /// <summary>
        /// (Get) Retorna a versão
        /// </summary>
        public string Versao
        {
            get { return _versao; }
        }

        /// <summary>
        /// (Get) Retorna a documentação
        /// </summary>
        public string Documentacao
        {
            get { return _documentacao; }
        }

        #endregion
    }
}


