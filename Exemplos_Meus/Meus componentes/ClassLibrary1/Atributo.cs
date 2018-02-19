using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing;
using System.Collections;
using System.Windows.Forms.Design;
using System.Reflection;

namespace Meus
{
    // [Designer(typeof(AtributoDesigner))]
    public partial class Atributo : Component
    {
        public Atributo()
        {
            InitializeComponent();
        }

        public Atributo(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private string _selecionaPesquisa = "(none)";

        private sPesquisa _pesquisa = new sPesquisa();

        [EditorAttribute(typeof(DigitalTimeFormatEditor), typeof(UITypeEditor))]
        public sPesquisa Pesquisax
        {
            get { return _pesquisa; }
            set
            {
                sPesquisa x = new sPesquisa();
                MessageBox.Show("3");
                _pesquisa = x;
            }
        }

        //[DefaultValue("(none)")]
        //[EditorAttribute(typeof(DigitalTimeFormatEditor), typeof(UITypeEditor))]
        //public string SelecionaPesquisa
        //{
        //    get
        //    {
        //        return _selecionaPesquisa;
        //    }
        //    set
        //    {
        //        _selecionaPesquisa = value;
        //    }
        //}

    }

    //public class AtributoDesigner : ComponentDesigner
    //{
    //    Atributo atributo;

    //    public AtributoDesigner()
    //    {
    //    }

    //    public override void Initialize(IComponent component)
    //    {
    //        atributo = (Atributo)component;
    //        base.Initialize(component);

    //        Verbs.Add(new DesignerVerb("Selecionar pesquisa", new EventHandler(SelecionaPesquisa)));
    //    }

    //    private void SelecionaPesquisa(object sender, EventArgs e)
    //    {
    //        using (frmSelecionaPesquisaBGM frmSelecionaPesquisa = new frmSelecionaPesquisaBGM(atributo))
    //        {
    //            if (frmSelecionaPesquisa.ShowDialog() == DialogResult.OK)
    //            {
    //                atributo.SelecionaPesquisa = frmSelecionaPesquisa.NomePesquisa;
    //            }
    //        }
    //    }
    //}

    public class DigitalTimeFormatEditor : UITypeEditor
    {
        Atributo atributo;
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                return UITypeEditorEditStyle.Modal;
            }
            return base.GetEditStyle(context);
        }

        public override object EditValue(ITypeDescriptorContext contexto, IServiceProvider prestador, object valor)
        {
            if ((contexto != null) && (prestador != null))
            {
                IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)prestador.GetService(typeof(IWindowsFormsEditorService));

                if (editorService != null)
                {
                    frmSelecionaPesquisaBGM frmSelecionaPesquisa = new frmSelecionaPesquisaBGM(atributo);
                    //frmSelecionaPesquisa.NomePesquisa = (string)valor;
                    if (editorService.ShowDialog(frmSelecionaPesquisa) == DialogResult.OK)
                    {
                        //valor = frmSelecionaPesquisa.NomePesquisa;
                        sPesquisa sPesquisax = new sPesquisa();
                        MessageBox.Show("1.0");
                        sPesquisax.NomePesquisa = frmSelecionaPesquisa.NomePesquisa;
                        MessageBox.Show("1.1");
                        return valor = (object)sPesquisax.NomePesquisa;
                        MessageBox.Show("1");
                    }
                }
            }

            return base.EditValue(contexto, prestador, valor);
        }
    }

    public struct sPesquisa
    {
        public Assembly NomeAssembly;
        public string NomeProjeto;
        public string NomePesquisa;
    }


}

