using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using FGlobus.Componentes.WinForms;
using FGlobus.Util;

namespace SerealizarMSG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pathImagem = new OpenFileDialog();

            pathImagem.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            pathImagem.Title = "Selecione o Arquivo";

            if (pathImagem.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imageList1.Images.Clear();
                    richTextBox1.Clear();

                    imageList1.Images.Add(Image.FromFile(pathImagem.FileName));
                    Image image = imageList1.Images[0];

                    pictureBox1.Image = image;

                    richTextBox1.Text = ImageToBase64(pictureBox1.Image, ImageFormat.Png);

                    Clipboard.SetText(richTextBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Concat(ex));
                }
            }
        }

        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        private enum eTipoTela
        {
            Pesquisas = 0,
            Cadastros = 1
        }

        public const string _none = "(none)";
        eTipoTela _tipoTela = eTipoTela.Pesquisas;
        string _telaSelecionada = "BgmRodotec.Globus5.Pesquisas.GestaoDeFrotaOnLine.OcorrenciaValidador111";

    }
}
