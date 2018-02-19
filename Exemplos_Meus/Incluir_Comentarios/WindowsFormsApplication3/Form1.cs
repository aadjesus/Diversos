using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string diretorio = @"C:\Frameworks\FGlobus\Seguranca.DTO\";

        private void Form1_Load(object sender, EventArgs e)
        {
            string padrao = "*DTO.cs";
            string[] arquivos = Directory.GetFiles(diretorio, padrao);

          

            //foreach (Control controle in panelControl1.Controls)
            //{
            //    string[] _nameSpace = new string[] { "FGlobus", "DevExpress" };
            //    Type _typeControle = controle.GetType();
            //    bool valida = false;

            //    foreach (string nameSpace in _nameSpace)
            //        if (_typeControle.Namespace.IndexOf(nameSpace) != -1)
            //        {
            //            valida = true;
            //            break;
            //        }

            //    if (!valida && DesignMode)
            //    {
            //        MessageBox.Show("Namespace (" + _typeControle.Namespace + ") do controle (" + controle.Name + ") não esta na lista de permitidas para o uso verificar.");
            //    }
            //}

            foreach (string arquivo in arquivos)
            {
                listBoxControl1.Items.Add(arquivo.Substring(36));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string arquivo in listBoxControl1.Items)
            {
                StreamReader stream = new StreamReader(diretorio + arquivo);
                string arqnovo = diretorio + arquivo + ".new";

                if (File.Exists(arqnovo))
                {
                    File.Delete(arqnovo);
                }

                StreamWriter sw = new StreamWriter(arqnovo);

                string linha = null;
                bool achou1 = false;
                bool achou2 = false;
                bool achou3 = false;
                while ((linha = stream.ReadLine()) != null)
                {
                    if (linha.IndexOf("#region Public Properties") != -1)
                    {
                        achou1 = true;
                    }

                    if (linha.IndexOf("#region Public Functions") != -1)
                    {
                        achou2 = true;
                        achou1 = false;
                    }
                    if (linha.IndexOf("#region Equals And HashCode Overrides") != -1)
                    {
                        achou1 = false;
                        achou2 = false;
                        achou3 = true;
                    }

                    if (achou1 && linha.IndexOf("public virtual") != -1)
                    {
                        string tipo = linha.Replace("public virtual", "").Trim();
                        tipo = tipo.Substring(0, tipo.IndexOf(" ")).Trim().Replace("?", "");

                        sw.WriteLine("        /// <summary>");
                        sw.WriteLine("        /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref=\"" + tipo + "\"/>.");
                        sw.WriteLine("        /// </summary>");
                    }

                    if (achou2 && linha.IndexOf("public virtual void MarkAsDeleted()") != -1)
                    {
                        sw.WriteLine("        /// <summary>");
                        sw.WriteLine("        /// Marca <see cref=\"IsChanged\"/> e <see cref=\"IsDeleted\"/> para <c>true</c>.");
                        sw.WriteLine("        /// </summary>");
                    }

                    if (achou3 && linha.IndexOf("public override bool Equals") != -1)
                    {
                        sw.WriteLine("        /// <summary>");
                        sw.WriteLine("        /// Verifica se os objetos sao iguais.");
                        sw.WriteLine("        /// </summary>");
                        sw.WriteLine("        /// <param name=\"obj\">Objeto que vai ser comparado.</param>");
                        sw.WriteLine("        /// <returns><see cref=\"bool\"/>.</returns>");
                    }

                    if (achou3 && linha.IndexOf("public override int GetHashCode") != -1)
                    {
                        sw.WriteLine("        /// <summary>");
                        sw.WriteLine("        /// Retorna o valor do objeto.");
                        sw.WriteLine("        /// </summary>");
                        sw.WriteLine("        /// <returns><see cref=\"int\"/>.</returns>");

                        listBoxControl2.Items.Add(arquivo);
                    }



                    sw.WriteLine(linha);
                }
                sw.Close();
                stream.Close();
                File.Delete(diretorio + arquivo);
                File.Move(arqnovo, diretorio + arquivo);

            }

        }

        private void listBoxControl2_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void panelControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            //string[] _nameSpace = new string[] { "FGlobus", "DevExpress" };
            //Type _typeControle = e.Control.GetType();
            //bool valida = false;

            //foreach (string nameSpace in _nameSpace)
            //    if (_typeControle.Namespace.IndexOf(nameSpace) != -1)
            //    {
            //        valida = true;
            //        break;
            //    }

            //if (!valida && DesignMode)
            //{
            //    MessageBox.Show("Namespace (" + _typeControle.Namespace + ") do controle (" + e.Control.Name + ") não esta na lista de permitidas para o uso verificar.");
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }


    }
}
