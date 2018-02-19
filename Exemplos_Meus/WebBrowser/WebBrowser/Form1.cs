using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace WebBrowserS
{
    //[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        public Form1()
        {
            //InitializeComponent();

            button1.Text = "call script code from client code";
            button1.Dock = DockStyle.Top;
            button1.Click += new EventHandler(button1_Click);
            webBrowser1.Dock = DockStyle.Fill;
            Controls.Add(webBrowser1);
            Controls.Add(button1);
            Load += new EventHandler(Form1_Load);
        }

        private WebBrowser webBrowser1 = new WebBrowser();
        private Button button1 = new Button();

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = this;
            // Uncomment the following line when you are finished debugging.
            //webBrowser1.ScriptErrorsSuppressed = true;

            webBrowser1.DocumentText =
                "<html><head><script>" +
                "function test(message) { alert(message); }" +
                "</script></head><body><button " +
                "onclick=\"window.external.Test('called from script code 1')\">" +
                "call client code from script code</button>" +
                "</body></html>";
        }

        public void Test(String message)
        {
            MessageBox.Show(message, "client code");
            //Form1 xx = new Form1();
            //xx.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("test", new String[] { "called from client code 2" });
        }

    }
}
