using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.DXErrorProvider;


namespace GraficosGrids
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBoxEdit1.Properties.Items.AddRange(Enum.GetValues(typeof(CompareControlOperator)));
            comboBoxEdit2.Properties.Items.AddRange(Enum.GetValues(typeof(ErrorType)));

            notEqualsValidationRule.ErrorText = "Valor incorreto";
            notEqualsValidationRule.Control = buttonEdit1;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MasterFormRelatorio1 xx = new MasterFormRelatorio1();
            xx.ShowDialog();
        }

        CompareAgainstControlValidationRule notEqualsValidationRule = new CompareAgainstControlValidationRule();
        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            notEqualsValidationRule.CompareControlOperator = (CompareControlOperator)comboBoxEdit1.SelectedItem;
            dxValidationProvider1.SetValidationRule(buttonEdit2, notEqualsValidationRule);
        }

        private void comboBoxEdit2_EditValueChanged(object sender, EventArgs e)
        {
            notEqualsValidationRule.ErrorType = (ErrorType)comboBoxEdit2.SelectedItem;
            dxValidationProvider1.SetValidationRule(buttonEdit2, notEqualsValidationRule);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Specify the type of records stored in the BindingSource.
            //this.DataBindings.Add(new Binding()

            bindingSource1.DataSource = typeof(MyRecord);
            // Create an empty MyRecord and add it to the BindingSource.
            MyRecord rec = new MyRecord();
            rec.FirstName = "";
            rec.LastName = "";
            bindingSource1.Add(rec);

            // Bind the text editors to the MyRecord.FirstName and MyRecord.LastName properties.
            textEdit1.DataBindings.Add(new Binding("EditValue", this.bindingSource1, "FirstName", true));
            textEdit2.DataBindings.Add(new Binding("EditValue", this.bindingSource1, "LastName", true));

            // Bind the DXErrorProvider to the data source.
            dxErrorProvider1.DataSource = bindingSource1;
            // Specify the container of controls (textEdit1 and textEdit2) 
            // which are monitored for errors.
            dxErrorProvider1.ContainerControl = this;


            DXErrorProvider.GetErrorIcon += new GetErrorIconEventHandler(DXErrorProvider_GetErrorIcon);

            icon = Image.FromFile(@"c:\Globus5\WPF\Sistemas\GerenciadordeProcessos\Resources\caminhaoamarelo_16x16.png");

        }

        Image icon;
        void DXErrorProvider_GetErrorIcon(GetErrorIconEventArgs e)
        {
            if (e.ErrorType == ErrorType.User1)
                e.ErrorIcon = icon;
        }

    }

    public class MyRecord : IDXDataErrorInfo
    {
        private string firstName;
        private string lastName;
        public MyRecord() { }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        // Implements the IDXDataErrorInfo.GetPropertyError method.
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == "FirstName" && FirstName == "" ||
                propertyName == "LastName" && LastName == "")
            {
                info.ErrorText = String.Format("xx '{0}' xx", propertyName);
            }
            else
            {
                try
                {
                    if (propertyName == "FirstName")
                        if (Convert.ToInt16(this.FirstName) > Convert.ToInt16(this.LastName))
                        {
                            info.ErrorText = String.Format("zz '{0}' zz ", propertyName);
                        }
                }
                catch (Exception)
                {
                }
            }
        }

        // IDXDataErrorInfo.GetError method
        public void GetError(ErrorInfo info)
        {
        }
    }
}
