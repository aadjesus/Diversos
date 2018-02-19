using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WFA_Testando_o_Test
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Teste_NomeMetodo()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i.ToString());
            }

        }
    }
}
