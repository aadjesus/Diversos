using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.Utils.Controls;
using System.Windows.Forms;

namespace WindowsFormsApplication13
{
    public partial class Xisto : SimpleButton
    {
        public Xisto()
        {
            InitializeComponent();
        }

        public Xisto(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private PanelBase _panelBase;

        [Category("Xisto")]
        public PanelBase PanelBase
        {
            get { return _panelBase; }
            set { _panelBase = value; }
        }

        public enum eTipo
        {
            x = 0,
            x1 = 1
        }

        private eTipo _tipo = new eTipo();

        [Category("Xisto")]
        public eTipo Tipo
        {
            get { return _tipo; }
            set
            {
                _tipo = value;
                if (_tipo == eTipo.x)
                {
                    //MessageBox.Show("1");                    
                }
                else
                {

                    //MessageBox.Show("2");
                    //this._panelBase = null;
                }
            }
        }

        bool _xisto1 = false;

        public string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value + (_xisto1 ? " ok " : "nao");
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (this._panelBase != null)
            {
                this._panelBase.Visible = !this._panelBase.Visible;
                MessageBox.Show("ok");
                _xisto1 = true;
            }
        }


    }
}
