using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors.Settings;
using System.ComponentModel;
using DevExpress.Data.Browsing;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.DataNodes;
using DevExpress.XtraEditors.DXErrorProvider;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.Xpf.Core;

namespace SilverlightApplicationImpressao
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            try
            {

                gridControl.AutoPopulateColumns = true;

                List<MyClass> persons = new List<MyClass>();
                persons.Add(new MyClass("John", "", "123 Home Lane, Homesville", "(555)956-15-47", "none"));
                persons.Add(new MyClass("Henry", "McAllister", "436 1st Ave, Cleveland", "(555)941-24-32", "@hotbox.com"));
                persons.Add(new MyClass("Frank", "Frankson", "349 Graphic Design L, Newman", "(555)155-05-02", "none"));
                persons.Add(new MyClass("Freddy", "Krueger", "Elm Street", "", "none"));
                persons.Add(new MyClass("Leticia", "Ford", "93900 Carter Lane, Cartersville", "(555)776-15-66", "none"));
                persons.Add(new MyClass("Karen", "Holmes", "", "(555)342-25-74", "none"));
                persons.Add(new MyClass("Roger", "Michelson", "3920 Michelson Dr., Bridgeford", "(555)954-51-88", "none"));
                gridControl.DataSource = persons;

                PrintableControlLink link = new PrintableControlLink(gridControl.View as IPrintableControl);
                link.ExportServiceUri = "../ReportService.svc";
                LinkPreviewModel model = new LinkPreviewModel(link);
                documentPreview.Model = model;
                link.CreateDocument(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void buttonShowDialog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XLS Files | *.XLS";
                saveFileDialog.DefaultExt = "XLS";
                if (saveFileDialog.ShowDialog() ?? false)
                {
                    PrintableControlLink link = new PrintableControlLink(gridControl.View as IPrintableControl);
                    link.ExportServiceUri = "../ReportService.svc";
                    link.CreateDocument(false);

                    XlsExportOptions xlsExportOptions = new XlsExportOptions();


                    link.ExportToXls(saveFileDialog.OpenFile(), new XlsExportOptions(), true);
                }
            }
            catch (Exception)
            {

            }

        }

        private void buttonDXWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DocumentPreview preview = new DocumentPreview();
                PrintableControlLink link = new PrintableControlLink(gridControl.View as IPrintableControl);
                link.ExportServiceUri = "../ReportService.svc";
                LinkPreviewModel model = new LinkPreviewModel(link);
                preview.Model = model;

                link.CreateDocument(true);
                DXDialog dlg = new DXDialog();
                dlg.Content = preview;
                dlg.Height = 640;
                dlg.Left = 150;
                dlg.Top = 150;
                dlg.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    #region MyRegion

    public class MyClass : object, IDXDataErrorInfo
    {
        public MyClass(string nome, string sobreNome, string endereco, string telefone, string email)
        {
            this.Nome = nome;
            this.SobreNome = sobreNome;
            this.Endereco = endereco;
            this.Telefone = telefone;
            this.Email = email;
        }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        bool IsStringEmpty(string str)
        {
            return (str != null && str.Trim().Length == 0);
        }
        bool IsEmailCorrect(string email)
        {
            return email == null || (email.IndexOf("@") >= 1 && email.Length > email.IndexOf("@") + 1);
        }
        void SetErrorInfo(ErrorInfo info, string errorText, ErrorType errorType)
        {
            info.ErrorText = errorText;
            info.ErrorType = errorType;
        }

        #region IDXDataErrorInfo Members

        void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
        {
            switch (propertyName)
            {
                case "Nome":
                case "SobreNome":
                    if (IsStringEmpty(propertyName == "Nome" ? Nome : SobreNome))
                    {
                        SetErrorInfo(info, propertyName + " campo não pode ser vazio", ErrorType.Critical);
                    }
                    break;
                case "Endereco":
                    if (IsStringEmpty(Endereco))
                    {
                        SetErrorInfo(info, "Endereço não foi inserido", ErrorType.Information);
                    }
                    break;
                case "Email":
                    if (IsStringEmpty(Email))
                    {
                        SetErrorInfo(info, "Email não foi inserido", ErrorType.Information);
                    }
                    else if (Email != "none" && !IsEmailCorrect(Email))
                    {
                        SetErrorInfo(info, "Endereço de e-mail errado", ErrorType.Warning);
                    }
                    break;
            }
        }
        void IDXDataErrorInfo.GetError(ErrorInfo info)
        {
            if (IsStringEmpty(Telefone) && (Email == "none" || !IsEmailCorrect(Email)))
                SetErrorInfo(info, "De qualquer número de telefone ou e-mail deve ser especificado", ErrorType.Information);
        }
        #endregion

    }

    #endregion

}
