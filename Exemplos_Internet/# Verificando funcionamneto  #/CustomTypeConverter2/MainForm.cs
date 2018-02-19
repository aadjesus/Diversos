using System;
using System.Windows.Forms;

/*
  Custom Type Converter Sample 2
  http://cyotek.com/blog/creating-a-custom-typeconverter-part-2
*/

namespace CustomTypeConverter2
{
    internal partial class MainForm : Form
    {
        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
