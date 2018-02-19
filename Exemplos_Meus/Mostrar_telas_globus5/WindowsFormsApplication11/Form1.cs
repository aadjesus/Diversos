using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using FGlobus.Componentes.WinForms;

namespace WindowsFormsApplication11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private enum eTipoTela
        {
            Pesquisas = 0,
            Cadastros = 1
        }
        private eTipoTela _tipoTela ;

        public const string _none = "(none)";

        public struct sTelaDeCadastro
        {
            public object Tela;
            public object BrowseDePesquisa;
            public string TelaDeCadastro;
            public string Caption;
            public bool AchouTela;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _tipoTela = comboBox1.Text.Trim() == "Pesquisas" ? eTipoTela.Pesquisas : eTipoTela.Cadastros;
            string _dirDistribuicaoDlls = @"C:\Globus5\WPF\Distribuicao\";
            string _nomePadraoDll = "BgmRodotec.Globus5." + _tipoTela.ToString();
            string[] _dlls = Directory.GetFiles(_dirDistribuicaoDlls, _nomePadraoDll + "*.dll");

            Assembly _assembly;
            Type[] _telas;

            trVwTela.Nodes.Clear();
            TreeNode _projeto = new TreeNode();            
            _projeto.Text = _none;

            _projeto.ImageIndex = 2;
            _projeto.SelectedImageIndex = 2;
            trVwTela.Nodes.Add(_projeto);

            TreeNode _tela = new TreeNode();
            foreach (string dll in _dlls)
            {
                string _nomeProjeto = RetornarNomeTela(dll.Replace(".dll", ""));

                _projeto = new TreeNode();
                _projeto.Text = _nomeProjeto.PadRight(_nomeProjeto.Length + 10)  ;
                _projeto.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                _projeto.ImageIndex = 3;
                _projeto.SelectedImageIndex = 3;
                trVwTela.Nodes.Add(_projeto);

                _assembly = Assembly.LoadFrom(dll);
                _telas = _assembly.GetExportedTypes();

                string module = _assembly.ManifestModule.Name.Replace(".dll", "");
                foreach (Type item in _telas)
                {
                    if (item.BaseType.Name == "MasterPanelBrowseDePesquisa" ||
                        item.BaseType.Name == "MasterFormCadastro")
                    {
                        string _nomeTela = RetornarNomeTela(item.ToString());

                        _tela = _projeto.Nodes.Add(_nomeTela);
                        if (_tipoTela == eTipoTela.Pesquisas)
                        {
                            sTelaDeCadastro telaDeCadastro = TrazDadosDaTelaDeCadastro(item);
                            _tela.ImageIndex = telaDeCadastro.AchouTela ? 4 : Convert.ToInt16(_tipoTela);
                            _tela.ToolTipText = telaDeCadastro.Caption;
                        }
                        else
                        {
                            _tela.ImageIndex = Convert.ToInt16(_tipoTela);
                        }


                        _tela.SelectedImageIndex = _tela.ImageIndex;
                        _tela.Tag = module + "." + _nomeTela;

                        if ((_nomePadraoDll + "." + _nomeProjeto + "." + _nomeTela) == _telaSelecionada)
                        {
                            _tela.ForeColor = System.Drawing.Color.Blue;
                            _tela.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                        }
                    }
                }

                _assembly = null;
                _telas = null;

                if (_projeto.Nodes.Count == 0)
                {
                    trVwTela.Nodes.Remove(_projeto);
                }
                _projeto.ExpandAll();
            }
            _dlls = null;
        }

        private void button1_Click2(object sender, EventArgs e)
        {
            string _tipoTela1 = comboBox1.Text;
            string _telaSelecionada;

            string _dirDistribuicaoDlls = @"C:\Globus5\WPF\Distribuicao\";
            string _nomePadraoDll = "BgmRodotec.Globus5." + _tipoTela1.ToString();
            string[] _dlls = Directory.GetFiles(_dirDistribuicaoDlls, _nomePadraoDll + "*.dll");

            Assembly _assembly;
            Type[] _telas;

            trVwTela.Nodes.Clear();
            TreeNode _projeto = new TreeNode("(none)");
            _projeto.ImageIndex = 0;
            _projeto.SelectedImageIndex = 0;
            trVwTela.Nodes.Add(_projeto);

            TreeNode _tela = new TreeNode();
            foreach (string dll in _dlls)
            {
                string _nomeProjeto = RetornarNomeTela(dll.Replace(".dll", ""));

                _projeto = new TreeNode(_nomeProjeto + "  ");
                _projeto.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                _projeto.ImageIndex = 1;
                _projeto.SelectedImageIndex = 1;
                trVwTela.Nodes.Add(_projeto);

                _assembly = Assembly.LoadFrom(dll);
                _telas = _assembly.GetTypes();// GetExportedTypes();

                string module = _assembly.ManifestModule.Name.Replace(".dll", "");
                foreach (Type item in _telas)
                {
                    if (item.BaseType.Name == "MasterPanelBrowseDePesquisa" ||
                        item.BaseType.Name == "MasterFormCadastro")
                    {
                        string _nomeTela = RetornarNomeTela(item.ToString());
                        try
                        {

                            object objeto = Activator.CreateInstance(item);
                            object objeto2 = objeto.GetType().GetProperty("BrowseDePesquisa").GetValue(objeto, null);
                            object objeto3 = objeto2.GetType().GetProperty("TelaDeCadastro").GetValue(objeto2, null);

                            //MasterPanelBrowseDePesquisa masterPanelBrowseDePesquisa = new MasterPanelBrowseDePesquisa();
                            if (objeto3 != null)
                            {
                                //masterPanelBrowseDePesquisa.BrowseDePesquisa.TelaDeCadastro = (string)objeto3;
                                _nomeTela += "*";
                            }



                            //foreach (PropertyInfo item2 in item.GetProperty("BrowseDePesquisa").GetType().GetProperties())
                            //{
                            //    foreach (PropertyInfo item3 in masterPanelBrowseDePesquisa.GetType().GetProperty("BrowseDePesquisa").GetType().GetProperties())
                            //    {
                            //        if (item2.Equals(item3))
                            //        {
                            //            item3.SetValue(item3, item2.GetValue(item2,null), null);                                        
                            //            break;
                            //        }
                            //    }
                            //}

                            //_nomeTela += (!string.IsNullOrEmpty(masterPanelBrowseDePesquisa.BrowseDePesquisa.TelaDeCadastro) && masterPanelBrowseDePesquisa.BrowseDePesquisa.TelaDeCadastro != SelecionaTela._none ? "*" : "");



                            //PropertyInfo x = objeto.GetType().GetProperty("BrowseDePesquisa");
                            //object xx = x.GetValue(objeto, null).GetType();

                            //object objeto = Activator.CreateInstance(item);
                            //PropertyInfo x = objeto.GetType().GetProperty("TelaDeCadastro");
                            //object objeto2 = x.GetValue(objeto, null);

                            //if (objeto2 != null && (bool)objeto2)
                            //{
                            //    _nomeTela += "*";
                            //}



                            //_nomeTela += (!string.IsNullOrEmpty(masterPanelBrowseDePesquisa.BrowseDePesquisa.TelaDeCadastro) && masterPanelBrowseDePesquisa.BrowseDePesquisa.TelaDeCadastro != SelecionaTela._none ? "*" : "");

                            //    //item.AssemblyQualifiedName item.FullName

                            //    //object masterPanelBrowseDePesquisa = Activator.CreateInstance(item);


                            //    object calcInstance = Activator.CreateInstance(item);
                            //    //PropertyInfo numberPropertyInfo = calcInstance.GetType().GetProperty("BrowseDePesquisa");


                            //    Type calcType = calcInstance.GetType().GetProperty("BrowseDePesquisa").GetType();
                            //    object calcInstance2 = Activator.CreateInstance(calcType);

                            //    //PropertyInfo numberPropertyInfo = calcType.GetProperty("Number");


                            //    //object calcInstance2 = Activator.CreateInstance(numberPropertyInfo.PropertyType );

                            //    //PropertyInfo numberPropertyInfo2 = calcInstance2.GetType().GetProperty("TelaDeCadastro");

                            //    //object value = numberPropertyInfo2.GetValue(calcInstance2, null);
                            //    //if (value != null)
                            //    //{
                            //    //    _nomeTela += "*";
                            //    //}

                            //    //PropertyInfo[] propertyInfos;
                            //    //propertyInfos = item.GetProperties(BindingFlags.GetProperty);

                            //    //object value = numberPropertyInfo.GetValue(calcInstance, null);

                            //    //string piValue = (string)item.InvokeMember("TelaDeCadastro",
                            //    //    BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public,
                            //    //    null, null, null);

                            //    //PropertyInfo numberPropertyInfo2 = value.GetType().GetProperty("TelaDeCadastro");

                            //    //Type xx = numberPropertyInfo2.GetType();

                            //    //string value2 = (string)xx.ToString();
                            //    //MessageBox.Show("1");

                            //    ////numberPropertyInfo.SetValue(calcInstance, 10.0, null);

                            //    //PropertyInfo piPropertyInfo = item.GetProperty("BrowseDePesquisa.TelaDeCadastro");

                            //    //double piValue = (double)piPropertyInfo.GetValue(null, null);

                            //    //item.InvokeMember("Clear",
                            //    //    BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                            //    //    null, calcInstance, null);

                            //    //item.InvokeMember("DoClear",
                            //    //    BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
                            //    //    null, calcInstance, null);

                            //    //double value = (double)item.InvokeMember("Add",
                            //    //    BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                            //    //    null, calcInstance, new object[] { 20.0 });

                            //    //double piValue = (double)item.InvokeMember("GetPi",
                            //    //    BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public,
                            //    //    null, null, null);

                            //    //double value = (double)item.InvokeMember("_number",
                            //    //    BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic,
                            //    //    null, calcInstance, null);



                            //    //System.Runtime.Remoting.ObjectHandle hdlSample;
                            //    //MasterPanelBrowseDePesquisa myExtenderInterface;

                            //    //hdlSample = Activator.CreateInstanceFrom(dll, item.FullName);

                            //    //myExtenderInterface = (MasterPanelBrowseDePesquisa)hdlSample.Unwrap();
                            //    //MessageBox.Show(myExtenderInterface.BrowseDePesquisa.TelaDeCadastro);


                            //    //Type typ = Type.GetTypeFromProgID("MyDLLx.clsMyClass");
                            //    //Activator.CreateInstance(typ).mNoShow("Test");

                            //    //_nomeTela += (!string.IsNullOrEmpty(myExtenderInterface.BrowseDePesquisa.TelaDeCadastro) && myExtenderInterface.BrowseDePesquisa.TelaDeCadastro != SelecionaTela._none ? "*" : "");
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                            break;
                        }
                        _tela = _projeto.Nodes.Add(_nomeTela);
                        _tela.ImageIndex = 2;
                        _tela.SelectedImageIndex = 2;
                        _tela.Tag = module + "." + _nomeTela;

                        //if ((_nomePadraoDll + "." + _nomeProjeto + "." + _nomeTela) == _telaSelecionada)
                        //{
                        //    _tela.ForeColor = System.Drawing.Color.Blue;
                        //    _tela.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                        //}
                    }
                }

                _assembly = null;
                _telas = null;

                if (_projeto.Nodes.Count == 0)
                {
                    trVwTela.Nodes.Remove(_projeto);
                }
                _projeto.ExpandAll();
            }
            _dlls = null;


        }

        private static string RetornarNomeTela(string nome)
        {
            string retorno = "";
            for (int i = nome.Length - 1; i > 0; i--)
            {
                if (nome[i].ToString().IndexOf(".") != -1)
                {
                    break;
                }
                retorno = nome[i].ToString() + retorno;
            }

            return retorno;
        }

        internal static sTelaDeCadastro TrazDadosDaTelaDeCadastro(Type tela)
        {
            sTelaDeCadastro telaDeCadastro = new sTelaDeCadastro();
            telaDeCadastro.AchouTela = false;
            try
            {
                telaDeCadastro.Tela = Activator.CreateInstance(tela);
                telaDeCadastro.BrowseDePesquisa = telaDeCadastro.Tela.GetType().GetProperty("BrowseDePesquisa").GetValue(telaDeCadastro.Tela, null);
                object _telaDeCadastro = telaDeCadastro.BrowseDePesquisa.GetType().GetProperty("TelaDeCadastro").GetValue(telaDeCadastro.BrowseDePesquisa, null);
                if (_telaDeCadastro != null)
                {
                    telaDeCadastro.TelaDeCadastro = (string)_telaDeCadastro;
                    try
                    {
                        object masterFormCadastro = LocalizaTelaSelecionada(telaDeCadastro.TelaDeCadastro);
                        telaDeCadastro.Caption = ((string)masterFormCadastro.GetType().GetProperty("Text").GetValue(masterFormCadastro, null)) + " (Associado no BrowseDePesquisa)";
                        telaDeCadastro.AchouTela = true;
                    }
                    catch (Exception)
                    {
                        telaDeCadastro.AchouTela = false;
                        throw;
                    }
                }
            }
            catch (Exception)
            {
            }
            return telaDeCadastro;
        }

        internal static object LocalizaTelaSelecionada(string _telaSelecionada)
        {
            if (_telaSelecionada != _none)
            {
                try
                {
                    string pesquisa = RetornarNomeTela(_telaSelecionada);
                    string projeto = _telaSelecionada.Replace("." + pesquisa, "");

                    Assembly assemblyProjetoPesquisas = Assembly.Load(projeto);


                    foreach (Module item in assemblyProjetoPesquisas.GetLoadedModules())
                    {
                        Type[] telas = item.GetTypes();
                        foreach (Type tela in telas)
                        {
                            if (tela.Module.ToString().Replace(".dll", "") + tela.Name == projeto + pesquisa)
                            {
                                return Activator.CreateInstance(tela);
                            }
                        }                       
                    }

                    //Type[] telas = assemblyProjetoPesquisas.GetExportedTypes();
                    //foreach (Type tela in telas)
                    //{
                    //    if (tela.Module.ToString().Replace(".dll", "") + tela.Name == projeto + pesquisa)
                    //    {
                    //        return Activator.CreateInstance(tela);
                    //    }
                    //}
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possivel abrir a tela associada ao controle.");
                }
            }
            return null;
        }

        private string _telaSelecionada;

        private void button2_Click(object sender, EventArgs e)
        {
            _telaSelecionada = trVwTela.SelectedNode.Tag.ToString().Trim();
            MessageBox.Show(_telaSelecionada);
        }


        private static void XISTO()
        {

            List<sTeste> lista = new List<sTeste>();
            for (int i = 1; i < 100; i++)
            {
                sTeste item = new sTeste();
                item.Codigo = i;
                item.Descricao = i.ToString();
                lista.Add(item);
            }

            var newLista = lista
                .AsParallel()
                .WithDegreeOfParallelism(1)
                //.WithDegreeOfParallelism(1)
                .GroupBy(g => g.Codigo % 2)
                .Select(s => new
                {
                    Valor = s.Key,
                    Qtde = s.Count()
                });
            if (newLista == null)
            {

            }

        }

        private struct sTeste
        {
            public int Codigo;
            public string Descricao;
        }

    }
}
