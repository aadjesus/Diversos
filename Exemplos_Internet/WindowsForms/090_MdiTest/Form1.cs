using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.XtraBars.Ribbon;
namespace MdiTest
{
    public partial class Form1 : XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void opToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            RibbonForm1 frm1 = new RibbonForm1();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void opToolStripMenuItem2_Click(object sender, System.EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.MdiParent = this;
            frm2.Show();
        }
    }
}
