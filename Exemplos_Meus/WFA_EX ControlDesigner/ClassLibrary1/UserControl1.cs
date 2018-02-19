using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace ClassLibrary1
{
    [Designer(typeof(myControlDesigner))]
    [ToolboxBitmap(typeof(UserControl1), "AutorizaBGM.ico")]
    [InformacoesComponente("aaa", "bbb", "ccc")]
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        string _teste;

        public string Teste
        {
            get { return _teste; }
            set { _teste = value; }
        }
    }


    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class myControlDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new myControlActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class InformacoesComponenteAttribute : Attribute
    {
        private readonly string _autor;
        private readonly string _versao;
        private readonly string _documentacao;

        public InformacoesComponenteAttribute(string autor, string versao, string documentacao)
        {
            _autor = autor;
            _versao = versao;
            _documentacao = documentacao;
        }

        public InformacoesComponenteAttribute(string autor, string versao)
        {
            _autor = autor;
            _versao = versao;
        }

        public string Autor
        {
            get { return _autor; }
        }

        public string Versao
        {
            get { return _versao; }
        }

        public string Documentacao
        {
            get { return _documentacao; }
        }

    }


    public class myControlActionList : System.ComponentModel.Design.DesignerActionList
    {
        private UserControl1 _controle;
        private InformacoesComponenteAttribute _informacoesAttribute;
        private DesignerActionUIService designerActionUISvc = null;

        public myControlActionList(IComponent component)
            : base(component)
        {
            this._controle = component as UserControl1;

            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(_controle)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        public string Teste
        {
            get
            {
                return _controle.Teste;
            }
            set
            {
                GetPropertyByName("Teste").SetValue(_controle, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            // items.Add(new DesignerActionHeaderItem("Commands"));
            items.Add(new DesignerActionHeaderItem("BGM"));

            items.Add(new DesignerActionPropertyItem("Teste", "Teste.....", "BGM", "Informa ou retorna GridControl que vai ser personalizado."));

            _informacoesAttribute = VerificarInformacoes();

            if (_informacoesAttribute != null)
                items.Add(new DesignerActionMethodItem(this,"AboutComponentes", "Informações", "BGM", "Informações do componente.", false));

            return items;
        }

        private InformacoesComponenteAttribute VerificarInformacoes()
        {
            return _controle.GetType().GetCustomAttributes(typeof(InformacoesComponenteAttribute), true).FirstOrDefault() as InformacoesComponenteAttribute;
        }

        private void AboutComponentes()
        {
            new Informacoes(_controle, _informacoesAttribute).ShowDialog();

        }
    }
}
