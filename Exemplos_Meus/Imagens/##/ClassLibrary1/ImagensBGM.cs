using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Drawing.Imaging;
using DevExpress.Utils;
using System.IO;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Security.Permissions;
using System.ComponentModel.Design;


namespace ClassLibrary1
{
    //public partial class ImagensBGM : ImageList
    //public partial class ImagensBGM : Component
    //[Designer(typeof(myControlDesigner))]

    //[ToolboxItemFilter("System.Windows.Forms")]
    //[Designer("System.Windows.Forms.Design.ImageListDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    //[DefaultProperty("Images")]
    //[TypeConverter(typeof(ImageListConverter))]
    //[SRDescription("DescriptionImageList")]
    //[System.ComponentModel.Design.Serialization.DesignerSerializer("System.Windows.Forms.Design.ImageListCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    //public partial class ImagensBGM : ImageList

    public partial class ImagensBGM1
    {
        ImageList _imageList;
    }

    public partial class ImagensBGM : ImageCollection
    {
        public ImagensBGM()
        {
            InitializeComponent();
        }

        public ImagensBGM(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }


        private object _imagens = "(Imagens)";

        [DefaultValue("(Imagens)")]
        [EditorAttribute(typeof(SelecionaTelaEditor), typeof(UITypeEditor))]
        public object Imagens
        {
            get
            {
                return _imagens;
            }
        }


    }


    internal class SelecionaTelaEditor : UITypeEditor
    {
        #region Override metodos

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
            if (contexto != null && prestador != null)
            {
                IWindowsFormsEditorService service = (IWindowsFormsEditorService)prestador.GetService(typeof(IWindowsFormsEditorService));
                ListaImagens frmSelecionaTela = new ListaImagens(contexto.Instance as ImagensBGM);
                frmSelecionaTela.StartPosition = FormStartPosition.CenterScreen;
                frmSelecionaTela.ShowDialog();
                if (frmSelecionaTela.DialogResult == DialogResult.OK)
                    valor = frmSelecionaTela.usrCtrlTreeViewImagesns.Imagens;
            }

            return valor;
        }

        #endregion
    }

}

