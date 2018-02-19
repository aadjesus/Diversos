using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication13
{
    public partial class Xisto2 : Xisto
    {
        public Xisto2()
        {
            InitializeComponent();
        }

        public Xisto2(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            MessageBox.Show("nao");
        }
    }
}
