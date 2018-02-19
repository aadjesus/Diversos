using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WFA_VerificaAcessoRemoto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //System.Type            

            //SystemInformation.TerminalServicesManager().CurrentSession.ClientName.
            

            //Process[] myProcesses = Process.GetProcessesByName("notepad", System.Environment.MachineName);
            //if (myProcesses == null)
            //{
                
            //}

            var xx = typeof(System.Environment).GetProperties();
            listBox1.Items.AddRange(xx
                .Select(s => 
                    (s is IComparable) 
                        ? "xx"
                        : String.Concat(s.Name, ":", s.GetValue(xx, null)))
                .ToArray()
                );

            listBox1.Items.Add(String.Concat("aaa :", SystemInformation.TerminalServerSession));


        }
    }
}
