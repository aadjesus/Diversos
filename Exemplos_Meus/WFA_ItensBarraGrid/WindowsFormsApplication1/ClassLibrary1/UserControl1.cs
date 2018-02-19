using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraEditors;

namespace ClassLibrary1
{
    [Designer(typeof(ItemBarraItensDesigner1))]
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SplitContainerControl AreaItem
        {
            get
            {
                return this.splitContainerControl1;
            }
        }

    }

    internal class ItemBarraItensDesigner1 : ParentControlDesigner
    {
        /// <summary>
        /// Inicializar o designer do controle
        /// </summary>
        /// <param name="component">Componente que esta sendo manipulado</param>
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

            if (this.Control is UserControl1)
            {
                this.EnableDesignMode((this.Control as UserControl1).AreaItem, "AreaItem");
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
