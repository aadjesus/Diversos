using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.Skins;

namespace WFA_ComponentDeImagens
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            // Program.EnableComposition();
            InitializeComponent();

            var lista = Enumerable
                .Repeat<int>(0, 50)
                .Select((s, index) => new
                {
                    Codigo = index,
                    Descricao = "Descricao" + index.ToString(),
                    Data = DateTime.Now.AddDays(index)
                })
                .ToArray();

            gridControl1.DataSource = lista;

            pivotGridControl1.DataSource = gridControl1.DataSource;

            chartControl1.DataSource = pivotGridControl1;

            labelControl1.Text = DevExpress.Skins.SkinManager.DefaultSkinName;

            listBoxControl1.Items
                .AddRange(DevExpress.Skins.SkinManager.Default.Skins
                            .OfType<SkinContainer>()
                            //.Where(w => w.SkinName.IndexOf("GlobusMais") != -1)
                            .Select(s => s.SkinName).ToArray());
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = DevExpress.Skins.SkinManager.Default.GetValidSkinName(listBoxControl1.Items[listBoxControl1.SelectedIndex].ToString());
        }
    }
}