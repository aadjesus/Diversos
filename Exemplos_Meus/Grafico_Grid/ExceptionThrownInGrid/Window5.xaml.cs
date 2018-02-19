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
using System.Windows.Shapes;
using DevExpress.Wpf.Charts;
using System.Windows.Media.Animation;

namespace ExceptionThrownInGrid
{
    /// <summary>
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        public Window5()
        {
            InitializeComponent();
        }

        private OcorrenciaValidadorDTO criaOcor(int ocor)
        {
            OcorrenciaValidadorDTO ocorrenciaValidadorDTO = new OcorrenciaValidadorDTO();
            ocorrenciaValidadorDTO.Ocorrencia = ocor;
            ocorrenciaValidadorDTO.DescOcorrencia = ocor.ToString("00000");

            return ocorrenciaValidadorDTO;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            List<sDadosControleOperacional> aa = new List<sDadosControleOperacional>();

            for (int i = 1; i <= 10; i++)
            {
                sDadosControleOperacional xx = new sDadosControleOperacional();
                if ("159".IndexOf(i.ToString()) >= 0)
                    xx.OcorrenciaComboio = criaOcor(1);

                xx.OcorrenciaHorario = criaOcor(2);

                if ("27".IndexOf(i.ToString()) >= 0)
                    xx.OcorrenciaMensagem = criaOcor(3);


                if ("279".IndexOf(i.ToString()) >= 0)
                    xx.OcorrenciaReaNaoProg = criaOcor(4);

                if ("1".IndexOf(i.ToString()) >= 0)
                    xx.OcorrenciaProgNaoRea = criaOcor(5);
                aa.Add(xx);
            }

            var categoryCounts = aa
                .GroupBy(a => a.OcorrenciaComboio)
                .Where(a => a.Key.Ocorrencia != 0)
                .Select(a => new
                {
                    DescOcorrencia = (a.Key.Ocorrencia.Equals(1) ? 77 : a.Key.Ocorrencia),
                    QtdeVeiculos = a.Count()
                })
                .Union(aa
                .GroupBy(a => a.OcorrenciaMensagem)
                .Where(a => a.Key.Ocorrencia != 0)
                .Select(a => new
                {
                    DescOcorrencia = a.Key.Ocorrencia,
                    QtdeVeiculos = a.Count()
                }))
                .Union(aa
                .GroupBy(a => a.OcorrenciaHorario)
                .Where(a => a.Key.Ocorrencia != 0)
                .Select(a => new
                {
                    DescOcorrencia = a.Key.Ocorrencia,
                    QtdeVeiculos = a.Count()
                }));



            pieSeries3DQtdeVeic.DataSource = categoryCounts;
        }

        private int mouseDownTime;
        private const int clickDelta = 200;

        private bool IsClick(int mouseUpTime)
        {
            return mouseUpTime - mouseDownTime < clickDelta;
        }

        private void chartControlQtdeVeiculos_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsClick(e.Timestamp))
                return;
            SeriesPoint point = chartControlQtdeVeiculos.HitTest(e.GetPosition(chartControlQtdeVeiculos));
            if (point == null)
                return;
            double distance = PieSeries.GetExplodedDistance(point);
            AnimationTimeline animation = distance > 0 ?
                (AnimationTimeline)TryFindResource("CollapseAnimation") :
                (AnimationTimeline)TryFindResource("ExplodeAnimation");
            point.BeginAnimation(PieSeries.ExplodedDistanceProperty, animation, HandoffBehavior.Compose);
        }

        private void chartControlQtdeVeiculos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDownTime = e.Timestamp;
        }

        private void chartControlQtdeVeiculos_QueryCursor(object sender, QueryCursorEventArgs e)
        {
            SeriesPoint currentPoint = chartControlQtdeVeiculos.HitTest(e.GetPosition(chartControlQtdeVeiculos));
            if (currentPoint != null && e.LeftButton == MouseButtonState.Released)
                e.Cursor = Cursors.Hand;
        }
    }


    public struct sEscalaControleOperacional
    {
        public int CodIntLinha;
        public int CodIntVeiculo;
        public int CodIntMotorista;
        public string CodigoServico;
        public int CodigoHorario;
        public DateTime HoraSaidaProg;
        public DateTime HoraChegadaProg;
        public DateTime HoraSaidaRea;
        public DateTime HoraChegadaRea;
        public int CodLocalidade;
        public DateTime HoraLocalProg;
        public DateTime HoraLocalRea;
        public int CodMensagem;
        public int Velocidade;
        public string Latitude;
        public string Longitude;
        public int QtdePassageiros;
        public int IdControleOperacional;
    }

    public struct OcorrenciaValidadorDTO
    {
        public int Ocorrencia;
        public string DescOcorrencia;
        public int Peso;
        public int Prioridade;
        public int Alerta;
        public string Cor;

    }


    public struct sDadosControleOperacional
    {
        public sEscalaControleOperacional Escala;
        public OcorrenciaValidadorDTO Ocorrencia;
        public OcorrenciaValidadorDTO OcorrenciaHorario;
        public OcorrenciaValidadorDTO OcorrenciaComboio;
        public OcorrenciaValidadorDTO OcorrenciaMensagem;
        public OcorrenciaValidadorDTO OcorrenciaReaNaoProg;
        public OcorrenciaValidadorDTO OcorrenciaProgNaoRea;
        public int IdItinerario;
        public int StatusHora;
    }
}
