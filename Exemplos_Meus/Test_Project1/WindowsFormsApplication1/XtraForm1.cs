using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace WindowsFormsApplication1
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
            gridControl1.DataSource = Enumerable
                .Repeat(0, 50)
                .Select((s, index) => new
                {
                    Codigo = index,
                    Descricao = "Descricao ---- " + index.ToString(),
                    Data = DateTime.Now.AddDays(index)
                })
                .ToArray();
        }
    }
}