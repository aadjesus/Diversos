using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Security.Cryptography;
using System.Xml;


namespace CrypTool
{
    /// <summary>
    /// Interaction logic for DlgMain.xaml
    /// </summary>

    public partial class DlgMain : Window
    {
        private ArrayList _childFormList = new ArrayList();
        System.Windows.Controls.MenuItem[] itemLang;

        public DlgMain()
        {
            InitializeComponent();
            //updateLang();
            //getLangItems();
            //MarkSelectedLang();
            //System.Windows.Forms.Application.EnableVisualStyles();
            //getOpenFileHistoryItems();
            //ShowHowToDialog();
            //ShowExampleFile();
        }
        /// <summary>
        /// Read the choosed language and update the XAML files.
        /// </summary>
        public void updateLang()
        {
            String selLangFullPath = CrypTool.AppLogic.LanguageOptions.getSelLangFullPath();
            XmlDataProvider xmlData = (XmlDataProvider)(this.FindResource("Lang"));
            xmlData.Source = new Uri(selLangFullPath, UriKind.Relative);
        }
        private void CloseDlgMain(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ShowDlgCaesarRot13(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void ShowDlgTextOptions(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void MenuItemNew_OnClick(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void MenuItemOpen_OnClick(object sender, RoutedEventArgs e)
        {
            //...
        }

        private void MenuItemSave_OnClick(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void MenuItemSaveAs_OnClick(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void MenuItemClose_OnClick(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void MenuItemCloseAll_OnClick(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void ShowDlgHash(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void ShowDlgDocPrefs(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void ShowDlgKeySymModern(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void PrintDialog(object sender, RoutedEventArgs e)
        {
            //...            
        }
        private void PrintDialogPreview(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void DocSetup(object sender, RoutedEventArgs e)
        {
            //...
        }
        /// <summary>
        /// Read the language folder, to check which language files are available
        /// </summary>
        private void getLangItems()
        {
            String langName;
            String[] langFiles = CrypTool.AppLogic.LanguageOptions.getLangFiles();

            itemLang = new System.Windows.Controls.MenuItem[langFiles.Length];

            for(int i=0;i<langFiles.Length;i++)
            {
                langName = langFiles[i];
                itemLang[i] = new System.Windows.Controls.MenuItem();
                itemLang[i].Header = langName;
                itemLang[i].Click += SelLang_OnClick;
                MenuItemLanguage.Items.Add(itemLang[i]);
            }
        }
        /// <summary>
        /// Select the language and update the tree view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void SelLang_OnClick(object sender, RoutedEventArgs args)
        {
            //first uncheck all MenuItems
            for (int i = 0; i < itemLang.Length; i++)
                itemLang[i].IsChecked = false;

            //check now the selected MenuItem            
            System.Windows.Controls.MenuItem item = args.Source as System.Windows.Controls.MenuItem;
            item.IsChecked = true;
            CrypTool.AppLogic.LanguageOptions.setLang(item.Header.ToString());
            updateLang();
        }
        void MarkSelectedLang()
        {
            //Only on Load
            String strLang = CrypTool.AppLogic.LanguageOptions.getSelLang();
            for (int i = 0; i < itemLang.Length; i++)
                if (itemLang[i].Header.ToString() == strLang)
                    itemLang[i].IsChecked = true;

        }
        public void setOtherChildFormNonActive()
        {
            //...
        }
        private void getOpenFileHistoryItems()
        {
            //...
        }
        private void openFileHistoryItem(object sender, RoutedEventArgs e)
        {
            //...
        }
        private void ShowHowToDialog()
        {
            CrypTool.AppLogic.StartOptions startOptions = new CrypTool.AppLogic.StartOptions();
            if (startOptions.getShowHowToStartDialog())
            {
                DlgHowTo dlgHowTo = new DlgHowTo();
                dlgHowTo.Show();
            }            
        }
        private void ShowExampleFile()
        {
            //...
        }
        #region Edit-Menu
        private void updateEditMenu()
        {
            //...
        }
        private void doUndo(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void doRedo(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void doCut(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void doCopy(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void doPaste(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void doDelete(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void doSelectAll(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void showKeyDialog(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void showDialogFindReplace(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void findNext(object sender, RoutedEventArgs arg)
        {
            //...
        }
        #endregion

        #region MenuItem-View
        private void showAsText(object sender, RoutedEventArgs arg)
        {
            //...            
        }
        private void showAsHex(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setAlphabetHighLight(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void showEndOfLine(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setWordWrap(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setWhiteSpaces(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setFontArial8(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setFontArial10(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setFontArial12(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setFontCourier8(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setFontCourier10(object sender, RoutedEventArgs arg)
        {
            //...
        }
        private void setFontCourier12(object sender, RoutedEventArgs arg)
        {
            //...
        }
        #endregion
    }
}