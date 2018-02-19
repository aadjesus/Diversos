using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnviarSMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebBrowser webBrowser;
        List<string> lista = new List<string>();
        bool fim = false;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                webBrowser = new WebBrowser();
                webBrowser.Navigate(new Uri("http://aguiamay.com.br/human/Default.aspx?id=ss7&from=4444444444&to=sss28413&msg=PO 4008&account=" + i.ToString() + "&dataHoraReceb=13/1/sdsd2010 19:00"));
                //webBrowser.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);
                webBrowser.Navigating += new WebBrowserNavigatingEventHandler(xx_Navigating);
                fim = i == 99;
            }
        }

        void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {


        }


        private void xx_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!string.IsNullOrEmpty(webBrowser.Document.Body.OuterText))
                lista.Add(webBrowser.Document.Body.OuterText);

            if (fim && lista.Count > 0)
            {
                MessageBox.Show(lista.Count.ToString());
            }
        }
    }
}
