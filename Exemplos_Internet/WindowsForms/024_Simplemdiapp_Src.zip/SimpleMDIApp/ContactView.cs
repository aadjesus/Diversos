using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

namespace SimpleMDIApp
{
    [SmartPart]
    public partial class ContactView : UserControl
    {
        private ContactController controller;

        public ContactView()
        {
            InitializeComponent();
        }

        [CreateNew]
        public ContactController Controller
        {
            set { controller = value; }
        }

        public string ContactName
        {
            get { return edtName.Text == "" ? "Contact- untitled" : edtName.Text; }
        }

        private void edtName_TextChanged(object sender, EventArgs e)
        {
            Parent.Text = "Contact - " + edtName.Text;
        }



 
    }
}
