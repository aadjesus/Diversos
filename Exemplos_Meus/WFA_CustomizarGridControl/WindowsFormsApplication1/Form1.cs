using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.Properties;
using System.Reflection;
using System.IO;
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class Form1 : FGlobus.Componentes.WinForms.MasterFormGlobus
    {
        public struct sTeste
        {
            public int Codigo;
            public string Descricao;
            public int Segundo;
            public int Minuto;
            public int Hora;
            public string Ok;

            public double Valor;
            public double Qtde;

            public Image Imagem;

        }

        public Form1()
        {
            InitializeComponent();

            int imagem = 0;
            gridControl1.DataSource = Enumerable
                .Repeat(1, 100)
                .Select((s, index) => index)
                .Aggregate(new List<sTeste>(), (a, b) =>
                {
                    sTeste teste = new sTeste()
                    {
                        Codigo = b,
                        Descricao = "Descrição_" + b.ToString(),
                        Hora = DateTime.Now.AddHours(b).Hour,
                        Minuto = DateTime.Now.AddMinutes(b).Minute,
                        Segundo = DateTime.Now.AddSeconds(b).Second,
                        Ok = (DateTime.Now.AddHours(b).Hour % 2).Equals(0) ? "S" : "N",
                        Valor = b / 20,
                        Qtde = b * (b / 20),
                        Imagem = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApplication1.Resources.Imagem" + imagem.ToString() + ".jpg"))
                    };

                    imagem += 1;

                    if (imagem.Equals(10))
                        imagem = 0;
                    a.Add(teste);

                    return a;
                })
                .Select(s => new
                {
                    s.Codigo,
                    s.Descricao,
                    s.Hora,
                    s.Minuto,
                    s.Segundo,
                    s.Ok,
                    s.Imagem,
                    s.Valor,
                    s.Qtde
                })
                .ToArray()
                ;

        }
    }
}
