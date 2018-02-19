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
using System.Timers;
using System.Windows.Threading;

//using System.Threading;
//using System.Timers;

namespace Exemplo_Thread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private Timer _timerTela = new Timer();
        private bool _fimThread;
        private System.Threading.Thread _threadConsulta;

        private DispatcherTimer _timerTela = new DispatcherTimer();


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //_timerTela.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            //_timerTela.Enabled = true;

            _timerTela.Interval = new TimeSpan(0, 0, 1);
            _timerTela.Tick += new EventHandler(timer1_Tick);
            _timerTela.Start();

            _threadConsulta = new System.Threading.Thread(new System.Threading.ThreadStart(Consultar));
            _threadConsulta.SetApartmentState(System.Threading.ApartmentState.STA);
            _threadConsulta.Start();
            _fimThread = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_fimThread)
            {
                _timerTela.Stop();
                Window1 window1 = new Window1();
                window1.ShowDialog();
            }

        }


        private void Consultar()
        {
            System.Threading.Thread.BeginCriticalRegion();
            System.Threading.Thread.Sleep(5000);
            _fimThread = true;
            
            System.Threading.Thread.EndCriticalRegion();
        }
    }
}
