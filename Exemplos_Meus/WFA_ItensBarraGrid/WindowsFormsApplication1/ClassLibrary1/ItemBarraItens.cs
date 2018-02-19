using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Reflection;
using FGlobus.Componentes.WinForms;

namespace ClassLibrary1
{
    [ToolboxBitmap(typeof(ButtonEditBGM), "ItemBarraItens.ico")]
    [Designer(typeof(ItemBarraItensDesigner))]
    public partial class ItemBarraItens : UserControl
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public ItemBarraItens()
        {
            InitializeComponent();
            areaItemBarraItens1_Paint(null, null);
            chckEdtF8EntrarOuSair_CheckedChanged(null, null);
        }

        #endregion

        #region Atributos

        private Control _primeiroControleItem;
        private Control _primeiroControleItens;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Set/Get) Informa ou retorna o titulo do grupo.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Informa ou retorna o titulo da grupo.\nNome: Titulo")]
        [DisplayName("1) Titulo")]
        [Localizable(true)]
        [DefaultValue("Item")]
        public String Titulo
        {
            get
            {
                return this.grpCtrlItemBarraItens.Text;
            }
            set
            {
                this.grpCtrlItemBarraItens.Text = value;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o primeiro controle do item.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Informa ou retorna o primeiro controle do item.\nNome: PrimeiroControleItem")]
        [DisplayName("2) Primeiro controle item")]
        [DefaultValue(null)]
        [EditorAttribute(typeof(SelecionaTelaEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Control PrimeiroControleItem
        {
            get { return _primeiroControleItem; }
            set { _primeiroControleItem = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o primeiro controle do itens.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Informa ou retorna o primeiro controle do itens.\nNome: PrimeiroControleItens")]
        [DisplayName("3) Primeiro controle itens")]
        [DefaultValue(null)]
        [EditorAttribute(typeof(SelecionaTelaEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Control PrimeiroControleItens
        {
            get
            {
                return _primeiroControleItens;
            }
            set
            {
                _primeiroControleItens = value;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna se mostra o item.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Informa ou retorna o primeiro controle do itens.\nNome: MostrarItem")]
        [DisplayName("4) Mostrar item")]
        [DefaultValue(true)]
        public bool MostrarItem
        {
            get
            {
                return this.chckEdtF8EntrarOuSair.Checked;
            }
            set
            {
                this.chckEdtF8EntrarOuSair.Checked = value;
            }
        }

        /// <summary>
        /// (Get) Retorna a AreaItem
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AreaItemBarraItens AreaItem
        {
            get
            {
                return this.areaItemBarraItens1;
            }
        }

        /// <summary>
        /// (Get) Retorna a AreaItens
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AreaItemBarraItens AreaItens
        {
            get
            {
                return this.areaItemBarraItens2;
            }
        }

        #endregion

        #region Metodos

        private void areaItemBarraItens1_Paint(object sender, PaintEventArgs e)
        {
            this.btnEdtIncluirOuAlterar.Top = this.areaItemBarraItens1.Height - this.btnEdtIncluirOuAlterar.Height - 10;
            this.btnEdtIncluirOuAlterar.Left = this.areaItemBarraItens1.Width - this.btnEdtIncluirOuAlterar.Width - 5;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (((Keys)keyData).Equals(Keys.F8))
                this.chckEdtF8EntrarOuSair.Checked = !this.chckEdtF8EntrarOuSair.Checked;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnEdtEntrarOuSair_Click(object sender, EventArgs e)
        {

            if (this.ClickBotao != null)
                this.ClickBotao(this);
        }

        private void chckEdtF8EntrarOuSair_CheckedChanged(object sender, EventArgs e)
        {
            this.areaItemBarraItens1.Visible = this.chckEdtF8EntrarOuSair.Checked;
            this.chckEdtF8EntrarOuSair.Text = this.chckEdtF8EntrarOuSair.Checked
                ? this.chckEdtF8EntrarOuSair.Properties.DisplayValueUnchecked
                : this.chckEdtF8EntrarOuSair.Properties.DisplayValueChecked;
            if (_primeiroControleItem != null)
                _primeiroControleItem.Focus();
        }

        private void areaItemBarraItens2_Paint(object sender, PaintEventArgs e)
        {
            this.lnShpSeparacao.X1 = 0;
            this.lnShpSeparacao.X2 = this.areaItemBarraItens2.Width;
            this.lnShpSeparacao.Y1 = 0;
            this.lnShpSeparacao.Y2 = 0;
            this.lnShpSeparacao.Visible = DesignMode;
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Executa o clique do botão incluir ou alterar
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Executa o clique do botão incluir ou alterar")]
        public event ItemBarraItensEventHandler ClickBotao;

        #endregion      
    }

    /// <summary>
    /// Delegate para controlar o evento clique do botão incluir ou alterar
    /// </summary>
    /// <param name="sender">Objeto que executou o clique</param>
    public delegate void ItemBarraItensEventHandler(object sender);

    /// <summary>
    /// Classe para controlar o designer dos controles que estão na tela
    /// </summary>
    internal class SelecionaTelaEditor : UITypeEditor
    {
        #region Atributos

        private ListBox _listBox;
        private IWindowsFormsEditorService _windowsFormsEditorService;

        #endregion

        #region Override metodos

        /// <summary>
        /// Retorna o tipo de execução do evento
        /// </summary>
        /// <param name="contexto">Controle que chamou o editor</param>
        /// <returns>UITypeEditorEditStyle</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext contexto)
        {
            if (contexto != null)
                return UITypeEditorEditStyle.DropDown;

            return base.GetEditStyle(contexto);
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
                    ItemBarraItens itemBarraItens = contexto.Instance as ItemBarraItens;
                    _listBox = new ListBox();
                    _listBox.BorderStyle = BorderStyle.None;
                    _listBox.KeyDown += new KeyEventHandler(listBox_KeyDown);
                    _listBox.Leave += new EventHandler(listBox_Leave);
                    _listBox.DoubleClick += new EventHandler(listBox_Leave);
                    _listBox.Click += new EventHandler(listBox_Leave);

                    _listBox.Items.Add(new ListItemBGM<Control>("(none)", new Control()));
                    _listBox.Items.AddRange(
                        (contexto.PropertyDescriptor.Name.Equals("PrimeiroControleItem")
                            ? itemBarraItens.AreaItem
                            : itemBarraItens.AreaItens).Controls
                        .OfType<Control>()
                        .Where(w => w.Tag == null || w.Tag.ToString() != "#ItemBarraItens#")
                        .Select(s => new ListItemBGM<Control>(s.Name, s))
                        .ToArray());

                    _windowsFormsEditorService.DropDownControl(_listBox);
                    if (_listBox.SelectedItem != null)
                    {
                        Control controle = (_listBox.SelectedItem as ListItemBGM<Control>).Value;
                        if (controle.Name.Equals("(none)"))
                        {
                            contexto.PropertyDescriptor.ResetValue(null);
                            return null;
                        }
                        else
                            return controle;
                    }
                }
                return valor;

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

    /// <summary>
    /// Classe para controlar o designer de edição do controle
    /// </summary>
    internal class ItemBarraItensDesigner : ParentControlDesigner
    {
        /// <summary>
        /// Inicializar o designer do controle
        /// </summary>
        /// <param name="component">Componente que esta sendo manipulado</param>
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

            if (this.Control is ItemBarraItens)
            {
                this.EnableDesignMode((this.Control as ItemBarraItens).AreaItem, "AreaItem");
                this.EnableDesignMode((this.Control as ItemBarraItens).AreaItens, "AreaItens");
            }
        }

        /// <summary>
        /// Verificar se o componente e um control 
        /// </summary>
        /// <param name="control">Componente que esta sendo manipulado</param>
        /// <returns>bool</returns>
        public override bool CanParent(System.Windows.Forms.Control control)
        {
            return control is System.Windows.Forms.Control;
        }

        /// <summary>
        /// Verificar se o componente e um control 
        /// </summary>
        /// <param name="controlDesigner">Componente que esta sendo manipulado</param>
        /// <returns>bool</returns>
        public override bool CanParent(System.Windows.Forms.Design.ControlDesigner controlDesigner)
        {
            return controlDesigner != null && 
                controlDesigner.Control is System.Windows.Forms.Control;
        }
    }

}
