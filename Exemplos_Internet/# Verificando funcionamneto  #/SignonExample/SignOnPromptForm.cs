using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SAS.Tasks.Toolkit;

namespace Signon
{
    /// <summary>
    /// Form that is used to collect a user and password for this task
    /// </summary>
    public partial class SignOnPromptForm : SAS.Tasks.Toolkit.Controls.TaskForm
    {
        public SignOnPromptForm()
        {
            InitializeComponent();
        }

        #region Properties
        public string User { get; set; }
        public string Password { get; set; }
        #endregion

        // override that is used when prompting to replace results
        protected override void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // ask the main application if it's okay to close
            // used when prompting to replace results
            if (!Consumer.VerifyTaskClosing(this))
                // if it's not okay, then cancel the close operation
                e.Cancel = true;

            base.OnFormClosing(sender, e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // be sure to add validation if necessary
            if (DialogResult.OK == this.DialogResult)
            {
                User = txtUser.Text;
                Password = txtPw.Text;
            }

            base.OnClosing(e);
        }
    }
}
