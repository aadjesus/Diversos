using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.Utils.Controls;
using DevExpress.XtraTab;
using FGlobus.Componentes.WinForms;
using System.IO;

namespace WindowsFormsApplication13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void xisto1_Click(object sender, EventArgs e)
        {
            xisto1.Text = "1111";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            LimparControles(groupControl1, dateEdit1);
            
           //autorizaBGM1.ItemsDestino
        }

        public static void LimparControles(Control control)
        {
            // 1º Nome da propriedade
            // 2º Valor 
            // 3º Identificar se continua procurando.
            object[,] _propriedades = new object[,] {{"SelectedIndex",-1,false}
                                                    ,{"Checked",false,false}
                                                    ,{"DateTime",null,true}
                                                    ,{"EditValue",null,false}
                                                    ,{"Text","",true}};

            //Control[] _naoLimparControle = new Control[] {SimpleButton,LookUpEdit,LabelControl };

            foreach (Control controle in control.Controls)
            {
                if (controle.Name != null)
                {
                    if (controle is PanelBase ||
                        controle is XtraTabControl ||
                        controle is Panel ||
                        controle is XtraPanel ||
                        controle is XtraScrollableControl)
                    {
                        LimparControles(controle);
                    }
                    else if (!(controle is BaseButton) && !(controle is LookUpEditBase) && !(controle is LabelControl))
                    {
                        if (controle is ListBoxControl)
                        {
                            ((ListBoxControl)controle).Items.Clear();
                            
                        }
                        else
                            for (int i = 0; i < _propriedades.Length / 3; i++)
                            {
                                PropertyInfo propertyInfo = controle.GetType().GetProperty((string)_propriedades[i, 0]);
                                if (propertyInfo != null)
                                {
                                    propertyInfo.SetValue(controle, _propriedades[i, 1], null);
                                    if (!(bool)_propriedades[i, 2])
                                    {
                                        break;
                                    }
                                }
                            }
                    }
                }
            }
        }

        public static void LimparControles(Control control, Control focaControle)
        {
            LimparControles(control);
            focaControle.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string xx = string.Format("{0:d/M/yyyy}", new DateTime(1, 1, 1)) +" "+ tmEdtHoraFinal.Text;
            MessageBox.Show(Convert.ToDateTime(xx).ToString());
        }

        private void simpleButtonBGM1_Click(object sender, EventArgs e)
        {
            TelasDecadastro();
        }

        private eTipoTela _tipoTela;
        private enum eTipoTela
        {
            Pesquisas = 0,
            Cadastros = 1
        }

        private string[] RetornaDll(eTipoTela tipoTela, ref string nomePadraoDll)
        {
            nomePadraoDll = "BgmRodotec.Globus5." + _tipoTela.ToString();
            return Directory.GetFiles(@"C:\Globus5\WPF\Distribuicao\", nomePadraoDll + "*.dll");
        }

        Type[] _telasCadastro = new Type[0];
        private void TelasDecadastro()
        {
            string _nomePadraoDll = "";
            Assembly _assembly;
            Type[] _telas;
            foreach (string dll in RetornaDll(eTipoTela.Cadastros, ref _nomePadraoDll))
            {
                _assembly = Assembly.LoadFrom(dll);
                _telas = _assembly.GetExportedTypes();
                foreach (Type item in _telas)
                {
                    int indice = _telasCadastro.Length + 1;

                    Array.Resize(ref _telasCadastro, indice);
                    _telasCadastro[indice-1] = item;
                }

            }
        }

        private void xisto21_Click(object sender, EventArgs e)
        {

        }

    }
}
