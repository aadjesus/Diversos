using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Collections;
using System.ComponentModel.Design;
using DevExpress.XtraEditors.Design;

namespace Mensagem
{
    public partial class GroupControlBGM : GroupControl
    {
        public GroupControlBGM()
        {
            InitializeComponent();
            Inicializacao();
            this.ControlRemoved += new ControlEventHandler(GroupControlBGM_ControlRemoved);
            this.ControlAdded += new ControlEventHandler(GroupControlBGM_ControlAdded);
        }

        private void GroupControlBGM_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is XtraPanel)
                _listaXtraPanel.Add(e.Control as XtraPanel);
        }

        private void GroupControlBGM_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control is XtraPanel)
                _listaXtraPanel.Remove(e.Control as XtraPanel);
        }

        public GroupControlBGM(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            Inicializacao();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            Inicializacao();
        }

        private void btnEdtMostrarOuOcultar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (_panelInformacao != null)
            {
                _panelInformacao.Visible = e.Button.Kind.Equals(ButtonPredefines.Down);
                e.Button.Kind = _panelInformacao.Visible
                    ? ButtonPredefines.Up
                    : ButtonPredefines.Down;
            }
        }

        private void Inicializacao()
        {
            this.lblCtrlF8IncluirSair.Appearance.ForeColor = this.AppearanceCaption.ForeColor;
            this.btnEdtMostrarOuOcultar.Properties.Buttons[0].Kind = ButtonPredefines.Up;
            this.btnEdtMostrarOuOcultar.Properties.Buttons[0].Appearance.ForeColor = this.lblCtrlF8IncluirSair.Appearance.ForeColor;
            this.pnlCtrlF8IncluirSair.Location = new Point(this.Width - this.pnlCtrlF8IncluirSair.Width - 1, 1);
        }

        private bool _mostrarInformacao = true;
        private List<XtraPanel> _listaXtraPanel = new List<XtraPanel>();
        private XtraPanel _panelInformacao;
        private XtraPanel _panelDetalhes;

        private BaseEdit _focarControleInformacao;
        private BaseEdit _focarControleDetalhes;

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        public BaseEdit FocarControleDetalhes
        {
            get { return _focarControleDetalhes; }
            set
            {
                _focarControleDetalhes = value;
            }
        }

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        public BaseEdit FocarControleInformacao
        {
            get { return _focarControleInformacao; }
            set
            {
                _focarControleInformacao = value;
            }
        }

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [DefaultValue(true)]
        public bool MostrarInformacao
        {
            get { return _mostrarInformacao; }
            set
            {
                if (_panelInformacao != null)
                {
                    this.btnEdtMostrarOuOcultar.PerformClick(this.btnEdtMostrarOuOcultar.Properties.Buttons[0]);
                    _mostrarInformacao = value;
                }
            }
        }

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        public XtraPanel PanelInformacao
        {
            get { return _panelInformacao; }
            set
            {
                _panelInformacao = value;
                if (_panelInformacao != null)
                    _panelInformacao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }
        }

        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        public XtraPanel PanelDetalhes
        {
            get { return _panelDetalhes; }
            set
            {
                _panelDetalhes = value;
                if (_panelDetalhes != null)
                    _panelDetalhes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }
        }

        private void GroupControlBGM_Enter(object sender, EventArgs e)
        {
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (this.btnEdtMostrarOuOcultar != null)
                this.btnEdtMostrarOuOcultar.Enabled = _panelInformacao != null;
        }
    }


}
