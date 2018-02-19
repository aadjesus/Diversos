using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Consultando_IntegracaoWS.Integracao.WS;

namespace Consultando_IntegracaoWS
{
    public partial class Form1 : Form
    {

        private Integracao.WS.IntegracaoWS integracaoWS = new Integracao.WS.IntegracaoWS();
        private Thread _threadDadosPesquisa;
        private sEscalaHorariaProgramada[] escalaHorariaProgramada = null;

        public Form1()
        {
            InitializeComponent();
            _threadDadosPesquisa = new Thread(new ThreadStart(CarregarThreadDadosPesquisa));
        }

        private void CarregarThreadDadosPesquisa()
        {
            escalaHorariaProgramada = integracaoWS.RetornarTabelaHorariaItinerario(dateTimePicker1.Value.Date, textBox1.Text.Split(',').Select(s => Convert.ToInt32(s)).ToArray());
            _threadDadosPesquisa.Abort();
        }

        private void Consultar_RetornarTabelaHorariaItinerario_Click(object sender, EventArgs e)
        {
            try
            {
                integracaoWS.Url = textBox2.Text;

                progressBar1.Visible = true;
                dataGridView1.Enabled = false;
                Consultar_RetornarTabelaHorariaItinerario.Enabled = false;

                timer1.Start();
                _threadDadosPesquisa.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex));
            }
            finally 
            {               
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_threadDadosPesquisa != null && !_threadDadosPesquisa.IsAlive)
            {
                progressBar1.Visible = false;
                dataGridView1.Enabled = true;
                Consultar_RetornarTabelaHorariaItinerario.Enabled = true;

                _threadDadosPesquisa = null;
                CheckForIllegalCrossThreadCalls = true;

                timer1.Stop();

                dataGridView1.DataSource = escalaHorariaProgramada;   
            }           
        }
    }
}

//1052, 1101, 1102, 1103, 1104, 1105, 1106, 1110, 1111, 1112, 1113, 1116, 1117, 1118, 1119, 1120, 1121, 1122, 1198, 1199, 1202, 1204, 1205, 1206, 1207, 1303, 1304, 1305, 1306, 1307, 1308, 1309, 1310, 1311, 1312, 1313, 1314, 1315, 1316, 1317, 1318, 1319, 1504, 1505, 1506, 1507, 1508, 1509, 2201, 2253, 2254, 2255, 2257, 2258, 2264, 2265, 2267, 2268, 2302, 2303, 2304, 2307, 2309, 2310, 4003, 4055, 4402, 4404, 4405, 4514, 4552, 4601, 5003, 5404, 5405, 5406, 5411, 5414, 5713, 5714, 5715, 5718, 5719, 5720, 5723, 5724, 5725, 5726, 5727, 5732, 5733, 5734, 5735, 5737, 5739, 5740, 5741, 5742, 7514, 7515, 7970, 7971, 7972, 7973, 7974, 7976, 7978, 8013, 8014, 8015, 8016, 8017, 8913, 8914, 8915, 8916, 8917, 8918, 9863,12917, 14220, 17621, 22942, 22943, 23450, 23451, 23548, 23550,23551, 23553, 23554, 23555, 23901, 24201, 24203, 24611, 24613,24614, 24714, 24717, 24719, 24721, 24722, 24725, 24728, 24766,24862, 24863, 24866, 25063, 25449, 25465, 25466, 25471, 25473,25475, 25479, 25481, 25483, 25486, 25492, 25495, 25497, 25498,25499, 25501, 25502, 25504, 25506, 25507, 25509, 25510, 25512,25513, 25514, 25516, 25517, 25520, 25521, 25522, 25524, 25527,25528, 25530, 25531, 25532, 25533, 25534, 25535, 25536, 25537,25538, 25539, 25540, 25541, 25542, 25543, 26565, 26566, 26569,26571, 26573, 26578, 26579, 26580, 26581, 26582, 26583, 26584,26585, 26590, 26591, 26593, 26594, 26598, 26599, 26600, 26601,26602, 26603, 26604, 26605, 26606, 26607, 26608, 26609, 26610,26611, 26612, 26613, 26614, 26615, 26665, 26667, 26765, 27069,27070, 27074, 27118, 27470, 27927, 27928, 28212, 28626, 29079,29545, 29569, 30349, 31350