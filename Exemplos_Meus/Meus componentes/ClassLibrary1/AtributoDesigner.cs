using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;


namespace Meus
{
    public class AtributoDesigner : ComponentDesigner
    {
        Atributo atributo;

        public AtributoDesigner()
        {
        }

        public override void Initialize(IComponent component)
        {
            atributo = (Atributo)component;
            base.Initialize(component);

            Verbs.Add(new DesignerVerb("Selecionar pesquisa", new EventHandler(SelecionaPesquisa)));
        }

        private void SelecionaPesquisa(object sender, EventArgs e)
        {
            using (frmSelecionaPesquisaBGM frmSelecionaPesquisa = new frmSelecionaPesquisaBGM(atributo))
            {
                if (frmSelecionaPesquisa.ShowDialog() == DialogResult.OK)
                {
                    atributo.SelecionaPesquisa = frmSelecionaPesquisa.NomePesquisa;                    
                }
            }
        }
    }

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
                    frmSelecionaPesquisa.NomePesquisa = (string)valor;
                    if (editorService.ShowDialog(frmSelecionaPesquisa) == DialogResult.OK) 
                    {
                        MessageBox.Show("1");
                        ///atributo.SelecionaPesquisa = frmSelecionaPesquisa.NomePesquisa;
                        return frmSelecionaPesquisa.NomePesquisa; 
                    } 
                }
            }
            return base.EditValue(contexto, prestador, valor);
        }
    }
}


