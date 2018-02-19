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

namespace ClassLibrary1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UsrCtrlTreeViewImagesns : UserControl
    {
        /// <summary>
        /// Contrutor default
        /// </summary>
        public UsrCtrlTreeViewImagesns()
        {
            InitializeComponent();

            this.trVwTela.Items.Clear();


            if (Directory.Exists(DIRETORIO_IMAGENS))
            {
                PopulateTreeView(DIRETORIO_IMAGENS, null);
                this.lblConsultandoImagens.Visibility = Visibility.Hidden;
                this.trVwTela.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Diretório padrão de imagens não encontrado: " + DIRETORIO_IMAGENS);
        }

        /// <summary>
        /// Popula o TreeView da tela conforme os arquivos *.png encontrados.
        /// </summary>
        /// <param name="diretorios">Diretório.</param>
        /// <param name="parentTreeViewItem">TreeViewItem parente do atual</param>
        private void PopulateTreeView(string diretorios, TreeViewItem parentTreeViewItem)
        {
            //		diretorios	"c:\\GlobusMais\\Frameworks\\FGlobus\\Componentes.WinForms\\Resources\\"	string


            Directory.GetDirectories(diretorios)
                .Aggregate(parentTreeViewItem, (retornoDiretorio, diretorio) =>
                {
                    string[] arquivos = Directory.GetFiles(diretorio, "*.PNG");
                    TreeViewItem treeViewItemDiretorio = new TreeViewItem() { Header = System.IO.Path.GetFileName(diretorio) };
                    treeViewItemDiretorio.Tag = System.Windows.Visibility.Collapsed;

                    arquivos.Aggregate(treeViewItemDiretorio, (retornoArquivos, arquivo) =>
                    {
                        // Testa se a imagem é do tipo png
                        BitmapFrame bitmapFrame = BitmapFrame.Create(new Uri(arquivo, UriKind.RelativeOrAbsolute), BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                        if (bitmapFrame != null &&
                            (bitmapFrame.Metadata is BitmapMetadata) &&
                            (bitmapFrame.Metadata as BitmapMetadata).Format.ToUpper() == "PNG")
                        {
                            TreeViewItem treeViewItemarquivo = new TreeViewItem()
                            {
                                Header = System.IO.Path.GetFileName(arquivo),
                                Tag = System.Windows.Visibility.Visible,
                                DataContext = arquivo
                            };
                            retornoArquivos.Items.Add(treeViewItemarquivo);
                        }
                        return retornoArquivos;
                    });

                    if (parentTreeViewItem == null)
                        this.trVwTela.Items.Add(treeViewItemDiretorio);
                    else if (treeViewItemDiretorio.Items.Count > 0)
                        retornoDiretorio.Items.Add(treeViewItemDiretorio);

                    PopulateTreeView(diretorio, treeViewItemDiretorio);
                    return retornoDiretorio;
                });
        }

        private void chckBxSelecionado_Checked(object sender, RoutedEventArgs e)
        {
            _imagens.Remove((sender as CheckBox).DataContext.ToString());
            _imagens.Add((sender as CheckBox).DataContext.ToString());
        }

        private List<string> _imagens = new List<string>();

        internal List<string> Imagens
        {
            get { return _imagens; }
        }

        public const string DIRETORIO_IMAGENS = @"c:\GlobusMais\Frameworks\FGlobus\Componentes.WinForms\Resources\";
    }

    public class FormatConverter : IValueConverter
    {
        #region Convert

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri( value.ToString().Replace("file:///", ""), UriKind.RelativeOrAbsolute));
                    return String.Concat(
                        "Tam: ", GetFileSize(new FileInfo(value.ToString().Replace("file:///", "")).Length),
                        @" \ Dim:", bitmapImage.PixelWidth,
                            " x ", bitmapImage.PixelHeight);
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

        #region IValueConverter Members

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        private static string GetFileSize(long bytes)
        {
            //if (bytes >= 1073741824)
            //    return String.Format("{0:##.##} GB", Decimal.Divide(bytes, 1073741824));
            //else if (bytes >= 1048576)
            //    return String.Format("{0:##.##} MB", Decimal.Divide(bytes, 1048576));
            //else if (bytes >= 1024)
            //    return String.Format("{0:##.##} KB", Decimal.Divide(bytes, 1024));
            //else if (bytes > 0 & bytes < 1024)
            //    return String.Format("{0:##.##} Bytes", bytes);
            //else
            //    return "0 Bytes";

            //string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            //int i;
            //double dblSByte = 0;
            //for (i = 0; (int)(bytes / 1024) > 0; i++, bytes /= 1024)
            //    dblSByte = bytes / 1024.0;

            //return String.Format("{0:0.00} {1}", dblSByte, Suffix[i]);

            const int scale = 1024;
            string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (bytes > max)
                    return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);

                max /= scale;
            }

            return "0 Bytes";
        }
    }

}