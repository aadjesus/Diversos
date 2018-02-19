using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System;
using System.Windows.Forms;

namespace BuildTasks.CustomType
{

    public class CredentialEditor : UITypeEditor
    {

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (editorService != null)
                {
                    Credential credential = value as Credential;

                    using (CredentialDialog dialog = new CredentialDialog())
                    {
                        dialog.Domain = credential.Domain;
                        dialog.UserName = credential.UserName;
                        dialog.Password = credential.Password;

                        if (editorService.ShowDialog(dialog) == DialogResult.OK)
                        {
                            credential.Domain = dialog.Domain;
                            credential.UserName = dialog.UserName;
                            credential.Password = dialog.Password;
                        }
                    }
                }

            }

            return value;
            
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
