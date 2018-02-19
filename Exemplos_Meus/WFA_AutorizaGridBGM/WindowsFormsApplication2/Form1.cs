using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            IEnumerable<object> aaa = Enumerable.Range(1, 10)
                .Select((s, index) => new
                {
                    Codigo = index,
                    Descricao = "Descrição" + index.ToString()
                });

            autorizaGridBGM1.PopularGrids<object, int>(aaa, new int[] { 2, 3, 4, 5 });
        }
    }
}


