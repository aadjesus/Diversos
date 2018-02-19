using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyTreeView
{
    public partial class NewNode : Form
    {
        
#region Local Variables

        private string mNewNodeName;
        private string mNewNodeText;
        private string mNewNodeTag;


#endregion


        /// <summary>
        /// Default constructor
        /// </summary>
        public NewNode()
        {
            InitializeComponent();
        }



#region Class Properties

        public string NewNodeName
        {
            get
            {
                return mNewNodeName;
            }
            set
            {
                mNewNodeName = value;
            }
        }

        public string NewNodeText
        {
            get
            {
                return mNewNodeText;
            }
            set
            {
                mNewNodeText = value;
            }
        }


        public string NewNodeTag
        {
            get
            {
                return mNewNodeTag;
            }
            set
            {
                mNewNodeTag = value;
            }
        }

        
#endregion



        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (txtNewNodeName.Text != string.Empty)
            {
                NewNodeName = txtNewNodeName.Text;
            }
            else
            {
                MessageBox.Show("Name the node.");
                return;
            }

            if (txtNewNodeText.Text != string.Empty)
            {
                NewNodeText = txtNewNodeText.Text;
            }
            else
            {
                MessageBox.Show("Provide the new node's text");
                return;
            }

            if (txtTag.Text != string.Empty)
            {
                NewNodeTag = txtTag.Text;
            }
            else
            {
                MessageBox.Show("Provide the new node's text");
                return;
            }

            this.Close();
        }



    }
}