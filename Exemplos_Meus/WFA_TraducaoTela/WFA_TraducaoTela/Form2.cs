using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace WFA_TraducaoTela
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            AddCulturesToTree();

            //CultureInfo culture = CultureInfo.CurrentCulture;
            //string code = culture.IetfLanguageTag;
            //MessageBox.Show(code);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form1((((CultureInfo)treeCultures.SelectedNode.Tag)).Name).Show();
        }


        public void AddCulturesToTree()
        {
            // get all cultures
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(w => !w.IsNeutralCulture)
                .ToArray();
            TreeNode[] nodes = new TreeNode[cultures.Length];

            int i = 0;
            TreeNode parent = null;
            foreach (CultureInfo ci in cultures)
            {
                nodes[i] = new TreeNode();
                nodes[i].Text = ci.DisplayName;
                nodes[i].Tag = ci;

                if (ci.IsNeutralCulture)
                {
                    // rembember neutral cultures as parent of the
                    // following cultures
                    parent = nodes[i];
                    treeCultures.Nodes.Add(nodes[i]);
                }
                else if (ci.ThreeLetterISOLanguageName ==
                   CultureInfo.InvariantCulture.ThreeLetterISOLanguageName)
                {
                    // invariant cultures don’t have a parent
                    treeCultures.Nodes.Add(nodes[i]);
                }
                else
                {

                    treeCultures.Nodes.Add(nodes[i]);
                    // specific cultures are added to the neutral parent
                    //parent.Nodes.Add(nodes[i]);
                }
                i++;
            }
        }
    }
}
