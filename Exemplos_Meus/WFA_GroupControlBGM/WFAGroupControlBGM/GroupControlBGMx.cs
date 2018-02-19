using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Mensagem
{
    //[DesignerAttribute(typeof(System.Windows.Forms.Design.ScrollableControlDesigner))]
    public partial class GroupControlBGMx : GroupControl
    {
        public GroupControlBGMx()
        {
            InitializeComponent();
            Inicializacao();
            
            ControlAdded += new ControlEventHandler(Handler_ControlAdded);

        }

        public GroupControlBGMx(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            Inicializacao();
        }

        private void Handler_ControlAdded(object theSender, ControlEventArgs theArgs)
        {
            try
            {
                theArgs.Control.BringToFront();
            }
            catch (Exception)
            {
            }
        }

        private void Inicializacao()
        {
            this.lblCtrlF8IncluirSair.Appearance.ForeColor = this.AppearanceCaption.ForeColor;
            this.btnEdtMostrarOuOcultar.Properties.Buttons[0].Kind = ButtonPredefines.Up;
            this.btnEdtMostrarOuOcultar.Properties.Buttons[0].Appearance.ForeColor = this.lblCtrlF8IncluirSair.Appearance.ForeColor;
            this.pnlCtrlF8IncluirSair.Location = new Point(this.Width - this.pnlCtrlF8IncluirSair.Width - 1, 1);
            this.btnEdtAcoesInformacao.Location = new Point(this.pnlCtrlInformacao.Width - this.btnEdtAcoesInformacao.Width - 1, this.pnlCtrlInformacao.Height - this.btnEdtAcoesInformacao.Height - 1);
        }

        private int _alturaInfromacao = 50;
        private bool _mostrarInformacao = true;

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [DefaultValue(true)]
        public bool MostrarInformacao
        {
            get { return _mostrarInformacao; }
            set
            {
                this.btnEdtMostrarOuOcultar.PerformClick(this.btnEdtMostrarOuOcultar.Properties.Buttons[0]);
                _mostrarInformacao = value;
            }
        }

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [DefaultValue(50)]
        internal int AlturaInfromacaoMaxima
        {
            get
            {
                int altura = this.pnlCtrlInformacao.Height + this.pnlCtrlDetalhes.Height;
                return altura - (altura * 20 / 100);
            }
        }

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Editor(typeof(WidthEditor), typeof(UITypeEditor))]
        public int AlturaInfromacao
        {
            get
            {
                return _alturaInfromacao;
            }
            set
            {
                if (value < 50)
                    value = 50;
                else if (value > this.AlturaInfromacaoMaxima)
                    value = this.AlturaInfromacaoMaxima;

                _alturaInfromacao = value;
                this.pnlCtrlInformacao.Height = _alturaInfromacao;
            }
        }

        private void btnEdtMostrarOuOcultar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.pnlCtrlInformacao.Visible = e.Button.Kind.Equals(ButtonPredefines.Down);
            e.Button.Kind = this.pnlCtrlInformacao.Visible
                ? ButtonPredefines.Up
                : ButtonPredefines.Down;
        }
    }
    

    public class WidthEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService iWindowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (iWindowsFormsEditorService == null)
                return null;

            GroupControlBGMx groupControlBGM = context.Instance as GroupControlBGMx;
            try
            {
                TrackBar trackBar = new TrackBar()
                {
                    Minimum = 50,
                    Maximum = groupControlBGM.AlturaInfromacaoMaxima,
                    Orientation = Orientation.Vertical,
                    Size = new Size(0, 200),
                    TickFrequency = 1,
                    Value = (int)value
                };
                iWindowsFormsEditorService.DropDownControl(trackBar);

                return trackBar.Value;
            }
            catch (Exception)
            {
                return 50;
            }
        }
    }

}
