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
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Xml;

namespace CriarFavoritos
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        #region Construtor

        public Window1()
        {
            InitializeComponent();

            // 1
            //fullScreenWindow.WindowStartupLocation = WindowStartupLocation.Manual; 
            //Debug.Assert(System.Windows.Forms.SystemInformation.MonitorCount > 1); 
            //System.Drawing.Rectangle workingArea = System.Windows.Forms.Screen.AllScreens[1].WorkingArea; 
            //fullScreenWindow.Left = workingArea.Left; 
            //fullScreenWindow.Top = workingArea.Top; 
            //fullScreenWindow.Width = workingArea.Width; 
            //fullScreenWindow.Height = workingArea.Height; 
            //fullScreenWindow.WindowState = WindowState.Maximized; 
            //fullScreenWindow.WindowStyle = WindowStyle.None; 
            //fullScreenWindow.Topmost = true; 
            //fullScreenWindow.Show();

            // 2
            //private void Window_Loaded(object sender, RoutedEventArgs e) 
            //{            
            //    WindowState = WindowState.Maximized;        
            //}
            //TreeView xxx = new TreeView()
            //xxx.ExpandAll()
        }

        #endregion

        #region Atributos

        private StreamWriter swArqNovo = null;
        private bool achouURL = false;
        private string arqConfig = "Config.xml";
        private DirectoryInfo firstDiretorio;

        #endregion

        #region Metodos

        private void AbrirArqConfig()
        {
            //if (File.Exists(arqConfig))
            //{
            //    XmlDataDocument myXmlDocument = new XmlDataDocument();
            //    myXmlDocument.Load(arqConfig);

            //    foreach (XmlNode item1 in myXmlDocument.SelectNodes("Configuracoes"))
            //        foreach (XmlNode item2 in item1.SelectNodes("diretorio"))
            //        {
            //            DirectoryInfo diretorioSelecionado = new DirectoryInfo(item2.InnerText);

            //            //treeView. SelectedValuePath = diretorioSelecionado.FullName;
            //            //TreeNode node = new TreeNode().FirstNode FindNode(Server.HtmlEncode(ValuePathText.Text));
            //            foreach (var item3 in treeView.Items)
            //            {
            //                new DirectoryInfo(item3).GetDirectories()
            //                foreach (var item4 in )
            //                {

            //                }
            //            }
            //        }
            //}
        }

        private void CriarArqConfig(DirectoryInfo diretorioSelecionado)
        {
            if (!File.Exists(arqConfig))
            {
                FileStream fs = new FileStream(arqConfig, FileMode.Create);
                XmlWriter w = XmlWriter.Create(fs);
                w.WriteStartDocument();
                w.WriteStartElement("Configuracoes");
                w.WriteElementString("diretorio", diretorioSelecionado.FullName);
                w.WriteEndElement();
                w.WriteEndDocument();
                w.Flush();
            }
        }

        private void Arquivos(DirectoryInfo diretorioSelecionado)
        {
            if (!firstDiretorio.Equals(diretorioSelecionado))
                swArqNovo.WriteLine(@"            <TreeViewItem Header=""" + diretorioSelecionado.Name + @""" FontWeight=""Bold"" Foreground=""White"" >");

            foreach (var item in diretorioSelecionado.GetFiles("*.url"))
            {
                StreamReader stream = new StreamReader(item.FullName);
                string linha = null;
                string url = null;
                string icon = null;
                while ((linha = stream.ReadLine()) != null)
                {
                    if (url == null && linha.IndexOf("BASEURL") != -1)
                        url = linha.Replace("BASEURL=", "");

                    if (linha.IndexOf("IconFile") != -1)
                    {
                        icon = linha.Replace("IconFile=", "");
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(url))
                {
                    swArqNovo.WriteLine(@"<Label Content=""" + item.Name.Replace("&", "&amp;").Replace(".url", "") +
                                        @""" DataContext=""" + url.Replace("&", "&amp;") +
                                        (string.IsNullOrEmpty(icon) ? null : @""" Tag=""" + icon.Replace("&", "&amp;")) +
                                        @"""/>");
                    //webBrowser.Source = new Uri(url);                    

                    //webBrowser.Navigated += new NavigatedEventHandler(webBrowser_Navigated);
                    //webBrowser.Refresh();


                    //webBrowser.LoadCompleted += new LoadCompletedEventHandler(webBrowser_LoadCompleted);

                    //mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)webBrowser.Document;
                    //string cookies = doc.cookie; 


                }

                achouURL = true;
            }

            foreach (var item in diretorioSelecionado.GetDirectories())
            {
                Arquivos(item);
                swArqNovo.WriteLine(@"            </TreeViewItem>");
            }
        }

        void webBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            Title = e.Uri.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            firstDiretorio = new DirectoryInfo(((FileSystemInfo)(treeView.SelectedItem)).FullName);
            string arqNovo = firstDiretorio.FullName + @"\Favoritos.xaml";

            if (File.Exists(arqNovo))
                File.Delete(arqNovo);

            swArqNovo = new StreamWriter(arqNovo);

            swArqNovo.WriteLine(@"<Page xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""");
            swArqNovo.WriteLine(@"      xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""");
            swArqNovo.WriteLine(@"      Title=""Principal"">");
            swArqNovo.WriteLine(@"    <Page.Resources>");
            swArqNovo.WriteLine(@"        <Style x:Key=""HyperStyle"" TargetType=""{x:Type Hyperlink}"">");
            swArqNovo.WriteLine(@"            <Setter Property=""Foreground"" Value=""White""/>");
            swArqNovo.WriteLine(@"            <Style.Triggers>");
            swArqNovo.WriteLine(@"                <Trigger Property=""IsMouseOver"" Value=""True"">");
            swArqNovo.WriteLine(@"                    <Setter Property=""Foreground"">");
            swArqNovo.WriteLine(@"                        <Setter.Value>");
            swArqNovo.WriteLine(@"                            <LinearGradientBrush>");
            swArqNovo.WriteLine(@"                                <GradientStop Color=""Orange"" Offset=""0""/>");
            swArqNovo.WriteLine(@"                                <GradientStop Color=""Yellow"" Offset=""0.6""/>");
            swArqNovo.WriteLine(@"                                <GradientStop Color=""Red"" Offset=""1""/>");
            swArqNovo.WriteLine(@"                            </LinearGradientBrush>");
            swArqNovo.WriteLine(@"                        </Setter.Value>");
            swArqNovo.WriteLine(@"                    </Setter>");
            swArqNovo.WriteLine(@"                </Trigger>");
            swArqNovo.WriteLine(@"            </Style.Triggers>");
            swArqNovo.WriteLine(@"        </Style>");
            swArqNovo.WriteLine(@"        <Style TargetType=""{x:Type Label}"">");
            swArqNovo.WriteLine(@"            <Setter Property=""Template"">");
            swArqNovo.WriteLine(@"                <Setter.Value>");
            swArqNovo.WriteLine(@"                    <ControlTemplate TargetType=""{x:Type Label}"">");
            swArqNovo.WriteLine(@"                        <StackPanel Orientation=""Horizontal"">");
            swArqNovo.WriteLine(@"                            <Image Width=""15"" x:Name=""image"" Height=""15"" Margin=""0,0,5,0"" Source=""{Binding Path=Tag, RelativeSource={RelativeSource TemplatedParent}}""/>");
            swArqNovo.WriteLine(@"                            <TextBlock>");
            swArqNovo.WriteLine(@"                                <Hyperlink NavigateUri=""{Binding}""");
            swArqNovo.WriteLine(@"                                           Style=""{DynamicResource HyperStyle}""");
            swArqNovo.WriteLine(@"                                           TextDecorations=""None"">");
            swArqNovo.WriteLine(@"                                    <TextBlock Text=""{TemplateBinding Content}"" />");
            swArqNovo.WriteLine(@"                                </Hyperlink>");
            swArqNovo.WriteLine(@"                            </TextBlock>");
            swArqNovo.WriteLine(@"                        </StackPanel>");
            swArqNovo.WriteLine(@"                        <ControlTemplate.Triggers>");
            swArqNovo.WriteLine(@"                            <Trigger Property=""Tag"" Value=""#vazio#"">");
            swArqNovo.WriteLine(@"                                <Setter TargetName=""image"" Property=""Visibility"" Value=""Collapsed"" />");
            swArqNovo.WriteLine(@"                            </Trigger>");
            swArqNovo.WriteLine(@"                        </ControlTemplate.Triggers>");
            swArqNovo.WriteLine(@"                    </ControlTemplate>");
            swArqNovo.WriteLine(@"                </Setter.Value>");
            swArqNovo.WriteLine(@"            </Setter>");
            swArqNovo.WriteLine(@"        </Style>");
            swArqNovo.WriteLine(@"    </Page.Resources>");
            swArqNovo.WriteLine(@"    <Grid Background=""Black"">");
            swArqNovo.WriteLine(@"        <Grid.RowDefinitions>");
            swArqNovo.WriteLine(@"            <RowDefinition/>");
            swArqNovo.WriteLine(@"            <RowDefinition Height=""20""/>");
            swArqNovo.WriteLine(@"        </Grid.RowDefinitions>");
            swArqNovo.WriteLine(@"        <Border DockPanel.Dock=""Bottom"" CornerRadius=""10,10,0,0"" Margin=""10"" >");
            swArqNovo.WriteLine(@"            <Border.Background>");
            swArqNovo.WriteLine(@"                <LinearGradientBrush EndPoint=""0.484,0.338"" StartPoint=""0.484,0.01"">");
            swArqNovo.WriteLine(@"                    <GradientStop Color=""#FFFF9900"" Offset=""0""/>");
            swArqNovo.WriteLine(@"                    <GradientStop Color=""#FF000000"" Offset=""1""/>");
            swArqNovo.WriteLine(@"                </LinearGradientBrush>");
            swArqNovo.WriteLine(@"            </Border.Background>");
            swArqNovo.WriteLine(@"                <TreeView Background=""Transparent"" BorderThickness=""0"" BorderBrush=""Transparent"" >");

            Arquivos(firstDiretorio);

            swArqNovo.WriteLine(@"                </TreeView>");
            swArqNovo.WriteLine(@"        </Border>");
            swArqNovo.WriteLine(@"        <Label Grid.Row=""2"" Content=""Alessandro A.J"" DataContext=""mailto:aadjesus@hotmail.com?subject="" FontSize=""8"" VerticalAlignment=""Bottom"" />");
            swArqNovo.WriteLine(@"    </Grid>");
            swArqNovo.WriteLine(@"</Page>");

            swArqNovo.Close();

            if (achouURL)
            {
                if (MessageBox.Show("Arquivo " + arqNovo + ", gerado com sucesso.\nDeseja visualizá-lo", "Informação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Windows.Forms.WebBrowser webBrowser = new System.Windows.Forms.WebBrowser();
                    webBrowser.Navigate(new Uri(arqNovo), true);
                }

                CriarArqConfig(firstDiretorio);
            }
            else
                File.Delete(arqNovo);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 aa = new Window2();
            aa.Show();
            //CriarArqConfig(null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AbrirArqConfig();
        }
    }

    public class FileConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is DriveInfo)
                    return new DirectoryInfo(((DriveInfo)value).RootDirectory.Name).GetDirectories();
                if (value is DirectoryInfo)
                    return ((DirectoryInfo)value).GetDirectories();

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}


