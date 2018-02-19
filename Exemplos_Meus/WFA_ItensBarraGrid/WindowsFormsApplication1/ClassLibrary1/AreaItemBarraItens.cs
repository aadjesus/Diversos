using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;

namespace ClassLibrary1
{
    [ToolboxItem(false)]
    [Designer(typeof(AreaItemBarraItensDesigner))]
    public partial class AreaItemBarraItens : PanelControl
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public AreaItemBarraItens()
        {
            InitializeComponent();            
        }

        
        #endregion

        #region Propriedades

        /// <summary>
        /// (Set/Get) Informa ou retorna o DockStyle.
        /// </summary>
        [Browsable(false)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o Size.
        /// </summary>
        [Browsable(false)]
        public Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        #endregion

    }

    /// <summary>
    /// Classe para controlar as propriedades visíveis do controle
    /// </summary>
    public class AreaItemBarraItensDesigner : ScrollableControlDesigner
    {
        /// <summary>
        /// Remove as propriedades do controle
        /// </summary>
        /// <param name="controle">Controle</param>
        protected override void PreFilterProperties(System.Collections.IDictionary controle)
        {
            try
            {
                controle.Keys
                    .OfType<string>()
                    .ToList()
                    .ForEach(f =>
                    {
                        if (f != "Dock" && f != "Controls" && f != "Size")
                            controle.Remove(f);
                    });
            }
            catch (Exception)
            {
            }
            base.PreFilterProperties(controle);
        }
    }
}
