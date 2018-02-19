using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FGlobus.Componentes.WinForms
{
    public partial class TelaCalendario : MasterFormGlobus
    {
        public TelaCalendario()
        {
            InitializeComponent();
            //elementHost1.cale dtNvgtrCalendario.

            DateTime aa = DateTime.Now;

            calendario1.dtNvgtrCalendario.SelectedDates.Add(aa);

            
            int a = 1;
            for (int i = 2; i < 365; i++)
            {
                if ((a++) == 5)
                {
                    calendario1.dtNvgtrCalendario.SelectedDates.Add(aa.AddDays(i));
                    a = 0;
                }


            }



        }
    }
}
