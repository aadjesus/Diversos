using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WCF_Client_SvcUtil.Controls
{
    public partial class WaitControl : UserControl
    {
        public WaitControl()
        {
            InitializeComponent();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return label3.Text;
            }
            set
            {
                label3.Text = value;
            }
        }
    }
}
