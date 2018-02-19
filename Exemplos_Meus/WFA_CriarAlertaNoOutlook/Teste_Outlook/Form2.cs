using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace Teste_Outlook
{
    // http://www.c-sharpcorner.com/UploadFile/Nimusoft/OutlookwithNET06262007081811AM/OutlookwithNET.aspx
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            dateEditData.DateTime = DateTime.Now;

            int x = 0;
            try
            {

                _outlook = new Microsoft.Office.Interop.Outlook.Application();

                NameSpace olMAPI = _outlook.GetNamespace("MAPI");
                try
                {
                    //if (Process.GetProcessesByName("OUTLOOK").Count() > 0)
                    //    olMAPI.Logon("", "9251XXajic", true, true);

                    //olMAPI.Logon("", "", Missing.Value, Missing.Value);
                    //olMAPI = null;

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(string.Concat(ex));
                }


                x = 1;
                elementHostComboBox.Child = CriaItemsComboBoxWPF();




                x = 2;
                this.imageComboBoxEdit1.Properties.Items
                    .AddRange(Enum.GetValues(typeof(OlImportance))
                    .OfType<OlImportance>()
                    .Select(s => new DevExpress.XtraEditors.Controls.ImageComboBoxItem()
                    {
                        Description = s.ToString(),
                        Value = s
                    })
                    .ToArray());

                x = 3;
                this.imageComboBoxEdit2.Properties.Items
                    .AddRange(Enum.GetValues(typeof(OlBusyStatus))
                    .OfType<OlBusyStatus>()
                    .Select(s => new DevExpress.XtraEditors.Controls.ImageComboBoxItem()
                    {
                        Description = s.ToString(),
                        Value = s
                    })
                    .ToArray());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Concat(x, Environment.NewLine, ex));
            }

        }

        private System.Windows.Controls.ComboBox CriaItemsComboBoxWPF()
        {
            _comboBoxCategoria = new System.Windows.Controls.ComboBox();

            _dictionaryCategoria = _outlook.Session.Categories
                .OfType<Category>()
                .Select(s => new
                {
                    Valor = s,
                    NomeCor = s.Color.ToString().ToUpper().Replace(typeof(OlCategoryColor).Name.ToUpper(), "")
                })
                .Join(typeof(System.Windows.Media.Brushes).GetProperties()
                    .OfType<PropertyInfo>()
                    .Select(s => new
                    {
                        Valor = (System.Windows.Media.Brush)s.GetValue(s, null),
                        NomeCor = s.Name.ToUpper()
                    }),
                    a => a.NomeCor,
                    b => b.NomeCor, (a, b) => new
                    {
                        Valor = a.Valor,
                        Cor = b.Valor
                    })
                .ToDictionary(d => d.Cor, d => d.Valor);

            _comboBoxCategoria.Items.Add(NENHUMA);
            _dictionaryCategoria
                .ToList()
                .ForEach(f => _comboBoxCategoria.Items.Add(
                    new System.Windows.Controls.Label()
                    {
                        Content = f.Value.Name,
                        Background = f.Key,
                        Width = elementHostComboBox.Width - 10,
                        Height = elementHostComboBox.Height
                    }));

            //VerificaSeTemInformacao
            //OlCategoryColor.olCategoryColorNone
            _comboBoxCategoria.SelectedIndex = 0;

            return _comboBoxCategoria;
        }

        private Microsoft.Office.Interop.Outlook.Application _outlook = null;
        private Dictionary<System.Windows.Media.Brush, Category> _dictionaryCategoria = null;
        private System.Windows.Controls.ComboBox _comboBoxCategoria;
        private const string NENHUMA = "Nenhuma";

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MAPIFolder mAPIFolder = null;
            try
            {
                Folders folders = _outlook.Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar).Folders;
                mAPIFolder = folders
                        .OfType<MAPIFolder>()
                        .Where(w => w.Name.Equals("Globus"))
                        .FirstOrDefault();
                if (mAPIFolder == null)
                    mAPIFolder = folders.Add("Globus");

                AppointmentItem newEvent = mAPIFolder.Items.Add(OlItemType.olAppointmentItem) as AppointmentItem;
                newEvent.Subject = this.textEditAssunto.Text;
                newEvent.Body = this.memoEditTexto.Text;
                newEvent.Location = this.textEditLocal.Text;
                newEvent.Start = this.dateEditData.DateTime.Date;
                newEvent.End = this.dateEditData.DateTime.Date;


                //if (comboBoxEdit1.SelectedIndex != -1)
                //    newEvent.Categories = comboBoxEdit1.Text;
                if (_comboBoxCategoria.Text != NENHUMA)
                    newEvent.Categories = _comboBoxCategoria.Text;

                if (this.imageComboBoxEdit1.SelectedIndex != -1)
                    newEvent.Importance = (OlImportance)this.imageComboBoxEdit1.SelectedItem; //Define a prioridade 
                if (this.imageComboBoxEdit2.SelectedIndex != -1)
                    newEvent.BusyStatus = (OlBusyStatus)this.imageComboBoxEdit2.SelectedItem; //Define o status (Ocupado\Dispo..\...)

                //newEvent.AllDayEvent = true; // Define se o evento é para o dia inteiro
                //newEvent.ReminderSet = false; // Define se mostra o lembrete
                //newEvent.ReminderMinutesBeforeStart = 15; // Define quantos minutos antes deve mostrar o lembrete

                newEvent.Save();
                //newEvent.Send();
            }
            catch (COMException ex)
            {
                MessageBox.Show(string.Concat(ex));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Concat(ex));
            }
            finally
            {
                MessageBox.Show("OK");
                try
                {
                    // Seleciona a pasta e posiciona no outlook se o mesmo estiver aberto
                    if (_outlook != null && mAPIFolder != null)
                    {
                        _outlook.ActiveExplorer().SelectFolder(mAPIFolder);
                        _outlook.ActiveExplorer().CurrentFolder.Display();
                    }
                }
                catch (System.Exception)
                {
                }
            }
        }


        private void CreateAppointmentUsingCreateItem()
        {
            Microsoft.Office.Interop.Outlook.Application outlook = new Microsoft.Office.Interop.Outlook.Application();
            AppointmentItem appItem = outlook.CreateItem(OlItemType.olAppointmentItem) as AppointmentItem;
            if (appItem != null)
            {
                appItem.Save();
                appItem.Display(true);
                Marshal.ReleaseComObject(appItem);
            }
        }
    }
}
