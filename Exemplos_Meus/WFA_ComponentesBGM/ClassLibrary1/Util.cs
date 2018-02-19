using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.Utils.Controls;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using System.Collections;
using System.Linq;
using System.Data;
using System.ComponentModel;
using System.IO;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Reflection.Emit;

namespace FGlobus.Util
{
    /// <summary>
    /// Classe com métodos úteis
    /// </summary>
    public static class Util
    {
        // #############################################
        #region // ###   M E T O D O S   D E   T E S T E S   ###
        // Definir os metodos sempre como "private" e o nome com "Teste_" e o nome do metodo

        private static void Teste_Exemplo_Invoke()
        {
            try
            {
                Type type = Type.GetType("FGlobus.Util.Util");

                // invoca um metodo
                MethodInfo methodInfo = type.GetMethod("VerificaUsuarioLiberado");
                var retornoMetodo = methodInfo.Invoke(typeof(Util), new object[] { "GLOBUS" });
                if (retornoMetodo == null)
                {
                }

                // invoca uma propriedade
                PropertyInfo propertyInfo = type.GetProperty("ResolucaoMonitor");
                var retornoPropriedade = propertyInfo.GetValue(type, null);
                if (retornoPropriedade == null)
                {
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        // #############################################

        #region Atributos

        /// <summary>
        /// (Get/Set) Retorna ou informa se simula ENTER quando for pressionado seta para baixo em um ButtonEditBGM
        /// que esta associado a um LabelControlBGM do tipo "Default".
        /// </summary>
        public static bool SimulaEnterAposSetaParaBaixo = false;

        /// <summary>
        /// Lista de usuários exclusivos da BgmRodotec.
        /// </summary>
        public enum UsuariosLiberados
        {
            /// <summary>
            /// Usuário GLOBUS
            /// </summary>
            GLOBUS,
            /// <summary>
            /// Usuário MANAGER
            /// </summary>
            MANAGER
        }

        /// <summary>
        /// Versões dos Globus.
        /// </summary>
        public enum eVersaoGlobus
        {
            /// <summary>
            /// Globus4
            /// </summary>
            Globus4,
            /// <summary>
            /// Globus+
            /// </summary>
            GlobusMais
        }

        /// <summary>
        /// Enum de operadores.
        /// </summary>
        public enum eOperador
        {
            /// <summary>
            /// Igual.
            /// </summary>
            Igual,
            /// <summary>
            /// Direferente.
            /// </summary>
            Diferente,
            /// <summary>
            /// Maior ou igual.
            /// </summary>
            MaiorOuIgual,
            /// <summary>
            /// Menor ou igual.
            /// </summary>
            MenorOuIgual,
            /// <summary>
            /// Maior.
            /// </summary>
            Maior,
            /// <summary>
            /// Menor.
            /// </summary>
            Menor
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// (Get) Retorna a data hora do servidor.
        /// </summary>
        /// <returns>Objeto do tipo DateTime.</returns>
        public static DateTime DataHoraServidor
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// (Get) Retorna o nome das namespaces autorizadas para o uso.
        /// </summary>
        /// <remarks>Utilizado no metodo <c>MasterFormCadastro</c> para alertar dos controles visuais que são implementados na tela.
        /// <para>Se o controle não for da namespace <see cref="FGlobus"/> ou <see cref="DevExpress"/> ira aparecer mensagem para ter atenção.</para>
        /// </remarks>
        public static string[] NameSpaceAutorizadas
        {
            get
            {
                return new string[] { "FGlobus", "DevExpress" , "BgmRodotec" , "Globus5" };
            }
        }

        /// <summary>
        /// (Get) Retorna o tamanho da resolução do monitor.
        /// </summary>
        /// <returns>Size.</returns>
        public static Size ResolucaoMonitor
        {
            get
            {
                return new Size(
                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            }
        }

        /// <summary>
        /// (Get) Retorna o nome do usuário do sistema operacional.
        /// </summary>
        /// <returns>string.</returns>
        public static string UsuarioOS
        {
            get
            {
                return System.Environment.UserName.Trim().ToUpper();
            }
        }

        /// <summary>
        /// (Get) Retorna o nome do computador.
        /// </summary>
        /// <returns>string.</returns>
        public static string Estacao
        {
            get
            {
                return System.Environment.MachineName.Trim().ToUpper();
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Verifica se o usuario informado é um usuario liberado.
        /// </summary>
        /// <param name="usuario">Usuario.</param>
        /// <returns>bool</returns>
        public static bool VerificaUsuarioLiberado(string usuario)
        {
            return Enum.GetValues(typeof(UsuariosLiberados))
                .Cast<UsuariosLiberados>()
                .Where(w => w.ToString().Equals(usuario.ToUpper()))
                .Count() > 0;
        }

        /// <summary>
        /// Retorna o tamanho do form conforme a resuloção do monitor.
        /// </summary>
        /// <param name="percentual">Percentual da tela.</param>
        /// <returns>Size.</returns>
        public static Size RetornarTamanhoDoForm(int percentual)
        {
            Size tamanhoForm = new Size(0, 0);
            Size _tamanhoTela = ResolucaoMonitor;

            tamanhoForm.Height = _tamanhoTela.Height - ((_tamanhoTela.Height * percentual) / 100);
            tamanhoForm.Width = _tamanhoTela.Width - ((_tamanhoTela.Width * percentual) / 100);

            return tamanhoForm;
        }

        /// <summary>
        /// Verifica se o campo está vazio.
        /// </summary>
        /// <param name="campo">Objeto do tipo BaseEdit.</param>
        /// <returns>Boolean.</returns>
        public static bool VerificarCampoVazio(BaseEdit campo)
        {
            return campo.EditValue == null || String.IsNullOrEmpty(campo.EditValue.ToString());
        }

        /// <summary>
        /// Posiciona os botões visiveis do panel.
        /// </summary>
        /// <param name="pnlCtrlBotoes">Panel com os botoes.</param>
        public static void PosicionaBotoes(PanelControl pnlCtrlBotoes)
        {
            Size tamanhoBotao = new Size(100, 30); // Tamanho padrão dos botões.

            int qtdeBotoes = 0; // Conta a quantidade de botoes visiveis no panel.
            foreach (Control botao in pnlCtrlBotoes.Controls)
            {
                if (botao is SimpleButton && ((SimpleButton)botao).Visible)
                {
                    qtdeBotoes++;
                }
            }

            // Calcula o espaço entre os botões. 
            int espaco = (int)((pnlCtrlBotoes.Width - (qtdeBotoes * tamanhoBotao.Width)) / (qtdeBotoes + 1));
            // Calcula a altura dos botões.
            int altura = (int)((pnlCtrlBotoes.Height - tamanhoBotao.Height) / 2);
            // Posiciona os botoes e redefine o tamanho.
            int qtde = 0;
            foreach (Control botao in pnlCtrlBotoes.Controls)
            {
                if (botao is SimpleButton && ((SimpleButton)botao).Visible)
                {
                    ((SimpleButton)botao).Size = tamanhoBotao;
                    ((SimpleButton)botao).Left = pnlCtrlBotoes.Width - (++qtde * (espaco + tamanhoBotao.Width));
                    ((SimpleButton)botao).Top = altura;
                }
            }
        }

        /// <summary>
        /// Mostra mensagem de exceçãoo.
        /// </summary>
        /// <param name="aplicacao">Nome aplicação.</param>
        /// <param name="ex">Objeto do tipo Exception.</param>
        [Obsolete("Utilizar a mensagem da Frameworks")]
        public static void MessagemExcecao(string aplicacao, Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                aplicacao,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Converte uma String em Int, se der erro de conversão retorna o default
        /// </summary>
        /// <param name="intString">String que será convertida.</param>
        /// <param name="intDefault">Inteiro default caso ocorra erro de conversão.</param>
        /// <returns>Inteiro.</returns>
        public static int StrToIntDef(string intString, int intDefault)
        {
            int resultado;
            try
            {
                resultado = Convert.ToInt32(intString.Trim());
            }
            catch
            {
                resultado = intDefault;
            }
            return resultado;
        }

        /// <summary>
        /// Limpar as propriedades dos componentes encontrados no controle.
        /// </summary>
        /// <param name="control">Controle.</param>
        public static void LimparControles(Control control)
        {
            // 1º Nome da propriedade.
            // 2º Valor.
            // 3º Identificar se continua procurando.
            object[,] _propriedades = new object[,] {{"SelectedIndex",-1,false,0}
                                                    ,{"Checked",false,false,false}
                                                    ,{"DateTime",null,true,null}
                                                    ,{"Time",null,false,null}
                                                    ,{"EditValue",null,false,null}
                                                    ,{"Text","",true,null}
                                                    };

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
                    else if (!(controle is SimpleButton) &&
                             !(controle is LookUpEditBase) &&
                             !(controle is GridControl) &&
                             !(controle is LabelControl))
                    {
                        for (int i = 0; i < _propriedades.Length / 4; i++)
                        {
                            PropertyInfo propertyInfo = controle.GetType().GetProperty((string)_propriedades[i, 0]);
                            if (propertyInfo != null)
                            {
                                try
                                {
                                    if (((Component)controle).ToString() == "FGlobus.Componentes.WinForms.PedeEmpresaBGM" ||
                                        ((Component)controle).ToString() == "FGlobus.Componentes.WinForms.PedeFilialBGM" ||
                                        ((Component)controle).ToString() == "FGlobus.Componentes.WinForms.PedeGaragemBGM")
                                        propertyInfo.SetValue(controle, _propriedades[i, 3], null);
                                    else
                                        propertyInfo.SetValue(controle, _propriedades[i, 1], null);
                                }
                                catch (Exception)
                                {
                                    propertyInfo.SetValue(controle, _propriedades[i, 3], null);
                                }

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

        /// <summary>
        /// Limpar as propriedades dos componentes encontrados no controle, e foca o controle informado.
        /// </summary>
        /// <param name="control">Controle.</param>
        /// <param name="focaControle">Controle que vai ser focado.</param>
        public static void LimparControles(Control control, Control focaControle)
        {
            LimparControles(control);
            focaControle.Focus();
        }

        /// <summary>
        /// Compara a data informada com a data do servidor.
        /// </summary>
        /// <param name="dataHora">Data hora.</param>
        /// <param name="operador">Tipo de operador para comparar com a data.</param>
        public static void CompararDataComDataServidor(DateTime dataHora, eOperador operador)
        {
            CompararDataComDataServidor(dataHora, operador, false);
        }

        /// <summary>
        /// Compara a data hora informada com a data hora do servidor.
        /// </summary>
        /// <param name="dataHora">Data hora.</param>
        /// <param name="operador">Tipo de operador para comparar com a data.</param>
        /// <param name="hora">Se compara a data junto com a hora.</param>
        public static void CompararDataComDataServidor(DateTime dataHora, eOperador operador, bool hora)
        {
            DateTime dataHoraServidor = DataHoraServidor;
            if (!hora)
            {
                dataHoraServidor = dataHoraServidor.Date;
                dataHora = dataHora.Date;
            }

            
        }

        /// <summary>
        /// Concatena os campos de uma lista em uma lista de string.
        /// </summary>
        /// <param name="lista">Lista</param>
        /// <param name="nomeTabela">Nome da tabela.</param>
        /// <returns>Retorna lista de objeto do tipo string.</returns>
        public static string[] ConcatenarResultadoLinq(object lista, string nomeTabela)
        {
            Type tipoLista = lista.GetType();
            List<string> novaLista = new List<string>();
            if (tipoLista.IsGenericType && (lista as IList) != null)
            {
                // Adiciona na lista nome da tabela quando desconcatenar a lista
                novaLista.Add(":" + Constantes.CARACTER_SEPARACAO + "NomeTabela:" + Constantes.CARACTER_SEPARACAO + nomeTabela);

                // Adiciona na lista, lista como o nome e o tipo de campo
                novaLista.AddRange(
                    tipoLista.GetGenericArguments()[0].GetProperties()
                        .Where(w => w.MemberType.Equals(MemberTypes.Property) || w.MemberType.Equals(MemberTypes.Field))
                        .Select(s => ":" + Constantes.CARACTER_SEPARACAO + s.Name +
                                     ":" + Constantes.CARACTER_SEPARACAO + s.PropertyType.Name));

                // Adiciona na lista campos concatenasdos separandos por "Constantes.CARACTER_SEPARACAO"
                novaLista.AddRange((lista as IList)
                    .Cast<object>()
                    .Select<object, string>(
                    delegate(object p)
                    {
                        return string.Concat(p.GetType().GetProperties()
                            .Select((s, index) => (index.Equals(0) ? "" : Constantes.CARACTER_SEPARACAO) +
                                                  (s.GetValue(p, null) == null ? "" : s.GetValue(p, null).ToString()))
                            .ToArray());

                    }).ToArray());
            }

            return novaLista.ToArray();

        }

        /// <summary>
        /// Concatena os campos de uma lista em uma lista de string.
        /// </summary>
        /// <param name="lista">Lista</param>
        /// <returns>Retorna lista de objeto do tipo string.</returns>
        public static string[] ConcatenarResultadoLinq(object lista)
        {
            return ConcatenarResultadoLinq(lista, "_Retorno_Lista_");
        }

        /// <summary>
        /// Desconcatena uma lista concatenada pelo metodo
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static DataTable DesConcatenarResultadoLinq(string[] lista)
        {
            DataTable tabela = null;
            if (lista != null && lista.Count() > 0)
            {
                try
                {
                    // Retira o nome da tabela da lista
                    string nomeTabela = lista
                        .Where(w => w.IndexOf(":" + Constantes.CARACTER_SEPARACAO + "NomeTabela:" + Constantes.CARACTER_SEPARACAO).Equals(0))
                        .SingleOrDefault();

                    // Retira as colunas da tabela da lista
                    var parametros = lista
                        .Where(w => w.StartsWith(":" + Constantes.CARACTER_SEPARACAO) &&
                                    w.IndexOf(nomeTabela) == -1)
                        .Select(s => new
                        {
                            campo = s.Split(new string[] { ":" + Constantes.CARACTER_SEPARACAO }, StringSplitOptions.None)[1],
                            tipo = s.Split(new string[] { ":" + Constantes.CARACTER_SEPARACAO }, StringSplitOptions.None)[2]
                        })
                        .Select(s => new
                        {
                            campo = s.campo,
                            tipo = Type.GetType("System." + s.tipo)
                        })
                        .ToArray();

                    if (parametros.Count() > 0)
                    {
                        Type tipoLista = lista.GetType();
                        // Cria tabela
                        tabela = new DataTable(nomeTabela.Split(new string[] { ":" + Constantes.CARACTER_SEPARACAO }, StringSplitOptions.None)[2]);
                        // Cria colunas
                        tabela.Columns.AddRange(parametros.Select(s => new DataColumn(s.campo, s.tipo)).ToArray());

                        // Adiciona as informações
                        foreach (var item in lista.Where(w => !w.StartsWith(":" + Constantes.CARACTER_SEPARACAO)))
                        {
                            DataRow dataRow = tabela.NewRow();
                            int i = 0;
                            foreach (var coluna in item.Split(new string[] { Constantes.CARACTER_SEPARACAO }, StringSplitOptions.None))
                                dataRow[i++] = coluna;

                            tabela.Rows.Add(dataRow);
                        };
                    }
                }
                catch (Exception)
                {

                }
            }
            return tabela;
        }

        /// <summary>
        /// Convertes uma lista generica para DataTable. 
        /// </summary>
        /// <param name="lista">Lista de objeto do tipo generica.</param>
        /// <returns>DataTable.</returns>
        public static DataTable ConverteListParaDataTable(IList lista)
        {
            return ConverteListParaDataTable(lista, UsuarioOS);
        }

        /// <summary>
        /// Convertes uma lista generica para DataTable. 
        /// </summary>
        /// <param name="lista">Lista de objeto do tipo generica.</param>
        /// <param name="nomeTabela">Nome da tabela.</param>
        /// <returns>DataTable.</returns>
        public static DataTable ConverteListParaDataTable(IList lista, string nomeTabela)
        {
            DataTable tabela = null;
            Type tipoLista = lista.GetType();
            if (tipoLista.IsGenericType)
            {
                tabela = new DataTable(nomeTabela);

                PropertyInfo[] propriedades = tipoLista.GetGenericArguments().First().GetProperties();
                if (propriedades.Count() > 0)
                {
                    // Cria colunas
                    tabela.Columns.AddRange(propriedades.Select(s => new DataColumn(s.Name, s.PropertyType)).ToArray());

                    PopulaDataTable(ref tabela, lista);
                }
            }
            return tabela;
        }

        /// <summary>
        /// Popula o DataTable informado
        /// </summary>
        /// <param name="tabela">DataTable que vai ser populado.</param>
        /// <param name="lista">Lista de objetos que contenha os mesmos campos do DataTable.</param>
        public static void PopulaDataTable(ref DataTable tabela, IList lista)
        {
            Type tipoLista = lista.GetType();
            if (tipoLista.IsGenericType)
            {
                foreach (var item in lista)
                {
                    DataRow dataRow = tabela.NewRow();
                    foreach (PropertyInfo propriedade in item.GetType().GetProperties())
                        dataRow[propriedade.Name] = propriedade.GetValue(item, null) == null ? DBNull.Value : propriedade.GetValue(item, null);

                    tabela.Rows.Add(dataRow);
                };
            }
        }

        /// <summary>
        /// Retorna o valor int ou null de um objeto.
        /// </summary>
        /// <param name="objeto">Objeto.</param>
        /// <returns>Retorna um objeto int?.</returns>
        public static int? RetornaIntOuNull(object objeto)
        {
            try
            {
                if (string.IsNullOrEmpty(objeto.ToString()))
                    return null;
                else
                    return (int)objeto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna a descrição da proprienade de um enum.
        /// </summary>
        /// <param name="value">Propriedade do enum.</param>
        /// <returns>Retorna umobjeto do tipo string.</returns>
        public static string GetDescricaoEnum(Enum value)
        {
            try
            {
                FieldInfo field = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Converte string para Stream.
        /// </summary>
        /// <param name="estruturaXml">String</param>
        /// <returns>Retorna objeto do tipo Stream.</returns>
        public static Stream ConverteStringEmStream(string estruturaXml)
        {
            try
            {
                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(estruturaXml);
                return new MemoryStream(byteArray);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retornar o valor do Key do arquivo informado.
        /// </summary>
        /// <param name="pathArquivo">Arquivo com a path.</param>
        /// <param name="key">Nome do key.</param>
        /// <returns>string</returns>
        public static string RetornarValorKeyArquivoConfig(string pathArquivo, string key)
        {
            try
            {
                KeyValueConfigurationElement keyValueConfigurationElement = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = pathArquivo }, ConfigurationUserLevel.None).AppSettings.Settings
                    .OfType<KeyValueConfigurationElement>()
                    .Where(w => w.Key.Equals(key))
                    .DefaultIfEmpty(new KeyValueConfigurationElement("", ""))
                    .First();

                return keyValueConfigurationElement.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retornar o valor do Key informado.
        /// </summary>
        /// <param name="key">Nome do key.</param>
        /// <returns>string</returns>
        public static string RetornarValorKeyArquivoConfig(string key)
        {
            return Util.RetornarValorKeyArquivoConfig(AppDomain.CurrentDomain.BaseDirectory + "BgmRodotec.Globus5.Config", key);
        }

        /// <summary>
        /// Retorna objeto contido no assembly informado
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de retorno.</typeparam>
        /// <param name="assemblyName">Nome do assembly name.</param>
        /// <param name="nomeClass">Nome da class.</param>
        /// <returns>Objeto do tipo informado.</returns>
        public static T RetornarClasseDoAssemblyName<T>(string assemblyName, string nomeClass = null)
        {
            int erro = 0;
            string[] listaErros = new string[] 
            {
                "",
                "Arquivo não encontrado ou assembly invalido.",
                "Classe não encontrada no assembly informado.",
                "Não foi possível converter a classe para o tipo informado."
            };
            string pathAssembly = "";
            try
            {
                if (string.IsNullOrEmpty(nomeClass))
                {
                    bool cancelar = false;
                    nomeClass = assemblyName
                        .Reverse()
                        .Aggregate(String.Empty, (a, b) =>
                        {
                            if (b.Equals('.'))
                                cancelar = true;
                            else if (!cancelar)
                                a = b + a;
                            return a;
                        });

                    assemblyName = assemblyName.Replace(String.Concat(".", nomeClass), "");
                }

                erro = 1;
                pathAssembly = String.Concat(AppDomain.CurrentDomain.BaseDirectory, assemblyName, ".DLL");
                Assembly assemblyProjetoPesquisas = Assembly.LoadFile(pathAssembly);

                erro = 2;
                Type type = assemblyProjetoPesquisas.GetExportedTypes()
                    .Where(w => w.Name.Equals(nomeClass))
                    .SingleOrDefault();
                if (type == null)
                    throw new Exception(listaErros[erro]);

                erro = 3;
                ObjectCreateMethod inv = new ObjectCreateMethod(type);
                Object obj = inv.CreateInstance();

                return (T)obj;

                //return (T)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Cria SuperToolTip
        /// </summary>
        /// <param name="titulo">Titulo</param>
        /// <param name="texto">Corpo</param>
        /// <param name="rodape">Rodapé</param>
        /// <param name="separador">Separador</param>
        /// <param name="textoHtml">Tesxto no com formatação html.</param>
        /// <returns>SuperToolTip</returns>
        public static DevExpress.Utils.SuperToolTip CriarSuperToolTip(string titulo, string texto = null, string rodape = null, bool separador = false, bool textoHtml = false)
        {
            DevExpress.Utils.SuperToolTip superToolTip = new DevExpress.Utils.SuperToolTip();
            if (textoHtml)
                superToolTip.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;

            superToolTip.Items.Add(new DevExpress.Utils.ToolTipTitleItem() { Text = titulo });

            if (!String.IsNullOrEmpty(texto))
                superToolTip.Items.Add(new DevExpress.Utils.ToolTipItem() { Text = texto });

            if (separador)
                superToolTip.Items.Add(new DevExpress.Utils.ToolTipSeparatorItem());

            if (!String.IsNullOrEmpty(rodape))
                superToolTip.Items.Add(new DevExpress.Utils.ToolTipTitleItem() { Text = rodape });

            return superToolTip;
        }

        /// <summary>
        /// Retorna a distancia entre as coordenadas
        /// </summary>
        /// <param name="latitudeOrigem">Latitude origem.</param>
        /// <param name="longitudeOrigem">Longitude origem</param>
        /// <param name="latitudeDestino"> Latitude destino</param>
        /// <param name="longitudeDestino">Longitude destino</param>
        /// <returns>double</returns>
        public static double RetornarDistanciaEntreAsCoordenadas(double latitudeOrigem, double longitudeOrigem, double latitudeDestino, double longitudeDestino)
        {
            Func<double, double> delegateRaioPonto =
                delegate(double valor)
                {
                    return valor * (Math.PI / 180);
                };

            Func<double, double, double> delegateRaioEntrePontos =
                delegate(double valor1, double valor2)
                {
                    return delegateRaioPonto(valor2) - delegateRaioPonto(valor1);
                };

            double raio = 6367.0;

            return (raio * 2 * Math.Asin(Math.Min(1,
                Math.Sqrt((
                    Math.Pow(
                        Math.Sin((delegateRaioEntrePontos(latitudeOrigem, latitudeDestino)) / 2.0), 2.0) +
                    Math.Cos(delegateRaioPonto(latitudeOrigem)) *
                    Math.Cos(delegateRaioPonto(latitudeDestino)) *
                    Math.Pow(
                        Math.Sin((delegateRaioEntrePontos(longitudeOrigem, longitudeDestino)) / 2.0), 2.0)))))) * 1000;
        }

        /// <summary>
        /// Insere zeros a esquerda de um número ou string
        /// </summary>
        /// <param name="valor">string, Seu número ou string que deve ser preenchido com zeros a esquerda</param>
        /// <param name="quantidade">int, Total de caracteres que a string de retorno deve possuir</param>
        /// <param name="forcaZerosEmString">bool, se força zeros a esquerda de um valor alfanumérico, só funciona caso o primeiro digito seja um número</param>
        /// <returns>string</returns>
        public static string StrZero(string valor, int quantidade, bool forcaZerosEmString = false)
        {
            if (valor == null || String.IsNullOrEmpty(valor))
                return valor;

            int? valorInteiro;

            try
            {
                valorInteiro = Convert.ToInt32(valor);
            }
            catch (Exception)
            {
                valorInteiro = null;
            }

            if (valorInteiro == null && forcaZerosEmString)
            {
                if (Char.IsDigit(valor, 0))
                    return valor.PadLeft(quantidade, '0');
                else
                    return valor;
            }
            else if (valorInteiro == null)
                return valor;
            else
                return valorInteiro.ToString().PadLeft(quantidade, '0');
        }

        public static string TratamentoCaracteresEspeciais(string campo)
        {
            campo = campo.Replace("&lt;"    , "<" );
            campo = campo.Replace("&gt;"    , ">" );
            campo = campo.Replace("&amp;"   , "&" );
            campo = campo.Replace("&quot;"  , "\"");
            campo = campo.Replace("&#39;"   , "'" );
            campo = campo.Replace("&nbsp;"  , " " );
            campo = campo.Replace("&Ccedil;", "Ç" );
            campo = campo.Replace("&ccedil;", "ç" );
            campo = campo.Replace("&Ntilde;", "Ñ" );
            campo = campo.Replace("&ntilde;", "ñ" );
            campo = campo.Replace("&Yacute;", "Ý" );
            campo = campo.Replace("&yacute;", "ý" );
            campo = campo.Replace("&yuml;"  , "ÿ" );
            campo = campo.Replace("&szlig;" , "ß" );
            campo = campo.Replace("&AElig;" , "Æ" );
            campo = campo.Replace("&Aacute;", "Á" );
            campo = campo.Replace("&Acirc;" , "Â" );
            campo = campo.Replace("&Agrave;", "À" );
            campo = campo.Replace("&Aring;" , "Å" );
            campo = campo.Replace("&Atilde;", "Ã" );
            campo = campo.Replace("&Auml;"  , "Ä" );
            campo = campo.Replace("&aelig;" , "æ" );
            campo = campo.Replace("&aacute;", "á" );
            campo = campo.Replace("&acirc;" , "â" );
            campo = campo.Replace("&agrave;", "à" );
            campo = campo.Replace("&aring;" , "å" );
            campo = campo.Replace("&atilde;", "ã" );
            campo = campo.Replace("&auml;"  , "ä" );
            campo = campo.Replace("&Eacute;", "É" );
            campo = campo.Replace("&Ecirc;" , "Ê" );
            campo = campo.Replace("&Egrave;", "È" );
            campo = campo.Replace("&Euml;"  , "Ë" );
            campo = campo.Replace("&eacute;", "é" );
            campo = campo.Replace("&ecirc;" , "ê" );
            campo = campo.Replace("&egrave;", "è" );
            campo = campo.Replace("&euml;"  , "ë" );
            campo = campo.Replace("&Iacute;", "Í" );
            campo = campo.Replace("&Icirc;" , "Î" );
            campo = campo.Replace("&Igrave;", "Ì" );
            campo = campo.Replace("&Iuml;"  , "Ï" );
            campo = campo.Replace("&iacute;", "í" );
            campo = campo.Replace("&icirc;" , "î" );
            campo = campo.Replace("&igrave;", "ì" );
            campo = campo.Replace("&iuml;"  , "ï" );
            campo = campo.Replace("&Oacute;", "Ó" );
            campo = campo.Replace("&Ocirc;" , "Ô" );
            campo = campo.Replace("&Ograve;", "Ò" );
            campo = campo.Replace("&Oslash;", "­Ø" );
            campo = campo.Replace("&Otilde;", "Õ" );
            campo = campo.Replace("&Ouml;"  , "Ö" );
            campo = campo.Replace("&oacute;", "ó" );
            campo = campo.Replace("&ocirc;" , "ô" );
            campo = campo.Replace("&ograve;", "ò" );
            campo = campo.Replace("&oslash;", "ø" );
            campo = campo.Replace("&otilde;", "õ" );
            campo = campo.Replace("&ouml;"  , "ö" );
            campo = campo.Replace("&Uacute;", "Ú" );
            campo = campo.Replace("&Ucirc;" , "Û" );
            campo = campo.Replace("&Ugrave;", "Ù" );
            campo = campo.Replace("&Uuml;"  , "Ü" );
            campo = campo.Replace("&uacute;", "ú" );
            campo = campo.Replace("&ucirc;" , "û" );
            campo = campo.Replace("&ugrave;", "ù" );
            campo = campo.Replace("&uuml;"  , "ü" );
            campo = campo.Replace("&THORN;" , "­Þ" );
            campo = campo.Replace("&thorn;" , "þ" );
            campo = campo.Replace("&ETH;"   , "­Ð" );
            campo = campo.Replace("&eth;"   , "­ð" );
            campo = campo.Replace("&reg;"   , "­®" );
            campo = campo.Replace("&plusmn;", "±" );
            campo = campo.Replace("&micro;" , "µ" );
            campo = campo.Replace("&para;"  , "¶" );
            campo = campo.Replace("&middot;", "·" );
            campo = campo.Replace("&cent;"  , "¢" );
            campo = campo.Replace("&pound;" , "£" );
            campo = campo.Replace("&yen;"   , "¥" );
            campo = campo.Replace("&frac14;", "¼" );
            campo = campo.Replace("&frac34;", "¾" );
            campo = campo.Replace("&frac12;", "½" );
            campo = campo.Replace("&sup1;"  , "¹" );
            campo = campo.Replace("&sup2;"  , "²" );
            campo = campo.Replace("&sup3;"  , "³" );
            campo = campo.Replace("&iquest;", "¿" );
            campo = campo.Replace("&deg;"   , "°" );
            campo = campo.Replace("&brvbar;", "¦" );
            campo = campo.Replace("&sect;"  , "§" );
            campo = campo.Replace("&lacuo;" , "«" );
            campo = campo.Replace("&racuo;" , "»" ); 
            return campo;
        }

        #endregion
    }


#pragma warning disable 183,184,197,420,465,602,626,657,658,672,684,809,824,1058,1060,1200,1201,1202,1203,1522,1570,1580,1581,1584,1589,1590,1592,1598,1607,1616,1633,1634,1635,1645,1658,1682,1683,1684,1685,1687,1690,1691,1692,1694,1695,1696,1697,1699,1707,1709,1720,1723,1956,1957,2002,2014,2023,2029,3000,3001,3002,3003,3004,3005,3006,3007,3008,3009,3010,3011,3012,3013,3014,3015,3016,3017,3018,3022,3023,3026,3027,5000,108,114,162,164,251,252,253,278,279,280,435,436,437,440,444,458,464,467,469,472,618,652,728,1571,1572,1587,1668,1698,1710,1711,1927,3019,3021,67,105,168,169,219,282,414,419,642,659,660,661,665,675,693,1700,1717,1718,28,78,109,402,422,429,628,649,1573,1591,1610,1712,3024

    public class ObjectCreateMethod
    {
        #region Atributtes

        private delegate object MethodInvoker();
        private MethodInvoker methodHandler = null;

        #endregion

        #region Methods

        public ObjectCreateMethod(Type type)
        {
            CreateMethod(type.GetConstructor(Type.EmptyTypes));
        }

        public ObjectCreateMethod(ConstructorInfo target)
        {
            CreateMethod(target);
        }

        private void CreateMethod(ConstructorInfo target)
        {
            DynamicMethod dynamic = 
                new DynamicMethod(
                    string.Empty,
                    typeof(object),
                    new Type[0],
                    target.DeclaringType);

            ILGenerator il = dynamic.GetILGenerator();
            il.DeclareLocal(target.DeclaringType);

            il.Emit(OpCodes.Newobj, target);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            methodHandler = (MethodInvoker)dynamic.CreateDelegate(typeof(MethodInvoker));
        }

        public object CreateInstance()
        {
            return methodHandler();
        }

        #endregion
    }

#pragma warning restore 183,184,197,420,465,602,626,657,658,672,684,809,824,1058,1060,1200,1201,1202,1203,1522,1570,1580,1581,1584,1589,1590,1592,1598,1607,1616,1633,1634,1635,1645,1658,1682,1683,1684,1685,1687,1690,1691,1692,1694,1695,1696,1697,1699,1707,1709,1720,1723,1956,1957,2002,2014,2023,2029,3000,3001,3002,3003,3004,3005,3006,3007,3008,3009,3010,3011,3012,3013,3014,3015,3016,3017,3018,3022,3023,3026,3027,5000,108,114,162,164,251,252,253,278,279,280,435,436,437,440,444,458,464,467,469,472,618,652,728,1571,1572,1587,1668,1698,1710,1711,1927,3019,3021,67,105,168,169,219,282,414,419,642,659,660,661,665,675,693,1700,1717,1718,28,78,109,402,422,429,628,649,1573,1591,1610,1712,3024

}
