using System.Windows.Forms;
using BuildTasks.Library;
using System;

namespace BuildTasks.CustomType
{
    public partial class CredentialDialog : Form
    {
        public CredentialDialog()
        {
            InitializeComponent();
        }

        public string Domain
        {
            get
            {
                return DomainTextbox.Text;
            }
            set
            {
                DomainTextbox.Text = value;
            }
        }
        public string Password
        {
            get
            {
                return PasswordMaskedTextbox.Text;
            }
            set
            {
                PasswordMaskedTextbox.Text = value;
            }
        }

        public string UserName
        {
            get
            {
                return UserNameTextbox.Text;
            }
            set
            {
                UserNameTextbox.Text = value;
            }
        }

        private void TestButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                Credential credential = new Credential() { Domain = Domain, UserName = UserName, Password = Password };
                using (Impersonation impersonation = new Impersonation(credential)) { }

                MessageBox.Show(this, "Credentials zijn correct", "Test succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(this, ex.Message, "Test failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
