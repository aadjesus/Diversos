using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace DemoThreds
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta é a thread principal do programa");

        }
        private void ThreadTarefa()
        {
            int stp;
            int novoValor;
            Random rnd = new Random();

            while (true)
            {
                stp = this.pgbThreads.Step * rnd.Next(-1, 2);
                novoValor = this.pgbThreads.Value + stp;

                if (novoValor > this.pgbThreads.Maximum)
                    novoValor = this.pgbThreads.Maximum;
                else if (novoValor < this.pgbThreads.Minimum)
                    novoValor = this.pgbThreads.Minimum;

                //descomentar essa linha vai causar um erro
                //this.pgbThreads.Value = novoValor;

                //esta linha coontorna o problema do erro da thread
                SetControlPropertyValue(pgbThreads,"value",novoValor); 

                Thread.Sleep(100);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread trd = new Thread(new ThreadStart(this.ThreadTarefa));
            trd.IsBackground = true;
            trd.Start();
        }

        delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] { oControl, propName, propValue });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if (p.Name.ToUpper() == propName.ToUpper())
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }


    }
}
