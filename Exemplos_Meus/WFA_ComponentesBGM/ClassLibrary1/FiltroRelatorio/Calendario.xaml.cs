using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Interaction logic for Calendario.xaml
    /// </summary>
    public partial class Calendario : UserControl
    {
        public Calendario()
        {
            InitializeComponent();

            //this.dtNvgtrCalendario.DateTime = new DateTime(2012, 01, 01);
            //this.dtNvgtrCalendario.DateTime = dataInicial;
            //Enumerable
            //    .Repeat(dataInicial, dataFinal.Subtract(dataInicial).Days)
            //    .ToList()
            //    .ForEach(f => this.dtNvgtrCalendario.Selection.Add(f.AddDays((this.dtNvgtrCalendario.Selection.Count - 1) + 1)));

        }
    }
}
