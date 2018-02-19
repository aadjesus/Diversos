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
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();

            treeView1.Items.Clear();
            //this.lblDiretorioOrigem.Content = @"c:\Frameworks\FGlobus\Imagens\";
            PopulateTreeView(@"c:\Frameworks\FGlobus\Imagens\", null);
        }

        private void PopulateTreeView(string directoryValue, TreeViewItem parentTreeViewItem)
        {
            Directory.GetDirectories(directoryValue)
                .Aggregate(parentTreeViewItem, (retornoDiretorio, diretorio) =>
                {
                    string[] arquivos = Directory.GetFiles(diretorio, "*.PNG");
                    TreeViewItem treeViewItemDiretorio = new TreeViewItem() { Header = System.IO.Path.GetFileName(diretorio) };
                    treeViewItemDiretorio.Tag = System.Windows.Visibility.Collapsed;

                    arquivos.Aggregate(treeViewItemDiretorio, (retornoArquivos, arquivo) =>
                    {
                        // Testa se a imagem é png
                        BitmapFrame bitmapFrame = BitmapFrame.Create(new Uri(arquivo, UriKind.RelativeOrAbsolute), BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                        if (bitmapFrame != null &&
                            (bitmapFrame.Metadata is BitmapMetadata) &&
                            (bitmapFrame.Metadata as BitmapMetadata).Format.ToUpper() == "PNG")
                        {
                            TreeViewItem treeViewItemarquivo = new TreeViewItem()
                            {
                                Header = System.IO.Path.GetFileName(arquivo),
                                //Header = new CheckBox() { Content = System.IO.Path.GetFileName(arquivo) },
                                Tag = System.Windows.Visibility.Visible,

                                DataContext = arquivo
                            };
                            treeViewItemarquivo.Selected += new RoutedEventHandler(treeViewItemarquivo_Selected);
                            retornoArquivos.Items.Add(treeViewItemarquivo);
                        }
                        return retornoArquivos;
                    });

                    if (parentTreeViewItem == null)
                        treeView1.Items.Add(treeViewItemDiretorio);
                    else if (treeViewItemDiretorio.Items.Count > 0)
                        retornoDiretorio.Items.Add(treeViewItemDiretorio);

                    PopulateTreeView(diretorio, treeViewItemDiretorio);
                    return retornoDiretorio;
                });
        }

        private void treeViewItemarquivo_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
            if (treeViewItem != null && !String.IsNullOrEmpty(treeViewItem.DataContext.ToString()))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(treeViewItem.DataContext.ToString(), UriKind.RelativeOrAbsolute));
                //this.lblValTamanho.Content = GetFileSize(new FileInfo(treeViewItem.DataContext.ToString()).Length);
                //this.lblValDimensoes.Content = String.Concat(bitmapImage.PixelWidth, " x ", bitmapImage.PixelHeight);
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            treeViewItemarquivo_Selected(sender, e);
        }

        private void SelectedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            treeViewItemarquivo_Selected(sender, e);
        }

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
                    BitmapImage bitmapImage = new BitmapImage(new Uri(value.ToString().Replace("file:///", ""), UriKind.RelativeOrAbsolute));
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
