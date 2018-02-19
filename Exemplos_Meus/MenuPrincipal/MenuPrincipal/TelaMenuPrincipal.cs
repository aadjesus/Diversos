using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Xml;

namespace MenuPrincipal
{
    public partial class TelaMenuPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public TelaMenuPrincipal()
        {
            InitializeComponent();
            
            XmlDataDocument _xmlDataDoc = new XmlDataDocument();

            _xmlDataDoc.DataSet.ReadXml(@"c:\Users\alessandro.augusto\Documents\Visual Studio\Exemplos_Meus\MenuPrincipal\MenuPrincipal\MenuPrincipal.Config");

            gridControl1.DataSource = _xmlDataDoc.DataSet;
            gridControl1.DataMember = "MenuPrincipal";
            //Cadastro

        }
    }
}