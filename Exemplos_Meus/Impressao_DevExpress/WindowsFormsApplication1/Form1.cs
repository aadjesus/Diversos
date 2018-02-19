using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FGlobus.Relatorios;
using Globus5.WPF.Comum.Relatorios;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //WebServices.GestaoDeFrotaOnLineWS web = new GestaoDeFrotaOnLineWS();
            //web.ConectaUsuarioWap();

            //FichaDeCondutaDTO fichaDeCondutaDTO = web.RetornarFichaDeCondutaPorChave(18);

            //string xxx = "Levo ao conhecimento que foi observado, através do sistema de monitoramento do GFO, que o operador acima citado, saiu do ponto [:Mensagem local atual] [:Ocorrência local atual], prejudicando assim, a frequência da linha.\n"
            //        + "\n\n\n\n\n\n\n\n[:Mensagem local atual]\n[:Ocorrência local atual]\n[:Programado local atual]\n[:Realizado local atual]"
            //        + "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"
            //        + "[:Apontador]                               Chapa: [:Chapa apontador]";


            //LstOcorrencias lstOcorrencia = new LstOcorrencias("");
            //List<string> xisto = new List<string>();
            //xisto.Add("aaaa");
            //lstOcorrencia.DataSource = xisto
            //    .Select(s => new
            //    {
            //        Data = fichaDeCondutaDTO.Data.ToShortDateString(),
            //        Trafego = true,
            //        Comercial = false,
            //        Horario = fichaDeCondutaDTO.Data.ToString("HH:mm"),
            //        Linha = "LINHA",
            //        Sentido = "SENTIDO",
            //        Onibus = "ONIBUS",
            //        Chapa = "CHAPA",
            //        Nome = "NOME",
            //        Cargo = "Motorista",

            //        CodOcorrencia = "COD.OCOR",
            //        Natureza = "DESC.OCOR",

            //        PontoFinal = "PONTO.FINAL",
            //        Detalhes = fichaDeCondutaDTO.Observacao,
            //        AvaliacaoDisciplinar = true,
            //        UmaOrientacao = false,
            //        UmaAdvertencia = false,
            //        UmaSuspensao = false
            //    })
            //    .ToArray();

            //MasterPreviewRelatorio previewRelatorio = new MasterPreviewRelatorio(lstOcorrencia);
            //previewRelatorio.ShowDialog();

        }
    }
}
