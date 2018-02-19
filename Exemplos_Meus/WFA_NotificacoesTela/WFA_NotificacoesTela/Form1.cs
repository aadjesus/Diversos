using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;

namespace WFA_NotificacoesTela
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

            imageComboBoxEdit1.Properties.Items
                .AddRange(
            this.Controls
                .OfType<BaseEdit>()
                .Select(s => new ImageComboBoxItem(s.Name, s))
                .ToArray());
        }

        private void imageComboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (imageComboBoxEdit1.SelectedItem != null)
            {
                panelControl1.Controls.Clear();

                BaseEdit baseEdit1 = (imageComboBoxEdit1.SelectedItem as ImageComboBoxItem).Value as BaseEdit;

                BaseEdit baseEdit2 = Activator.CreateInstance(baseEdit1.GetType()) as BaseEdit;
                baseEdit2.EditValue = baseEdit1.EditValue;


                //BaseEdit baseEdit2 = Copy(baseEdit1) as BaseEdit;
                //baseEdit2.Parent = null;
                //baseEdit2.Location = new Point(0, 0);

                

                //DevExpress.XtraEditors

                panelControl1.Controls.Add(baseEdit2);
                panelControl1.Refresh();
                //panelControl1.Controls.Add(baseEdit2);
            }

        }


        private BaseEdit CloneControls(BaseEdit o)
        {
            Type type = o.GetType();
            PropertyInfo[] properties = type.GetProperties();
            BaseEdit retObject = type.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null) as BaseEdit;
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.CanWrite )
                {
                    propertyInfo.SetValue(retObject, propertyInfo.GetValue(o, null), null);
                }
            }
            return retObject;
        } 

        public Control Copy(Control ctrlSource)
        {
            Type t = ctrlSource.GetType();
            Control ctrlDest = (Control)t.InvokeMember("", BindingFlags.CreateInstance, null, null, null);


            foreach (PropertyInfo prop in t.GetProperties())
            {
                if (prop.CanWrite)
                {
                        prop.SetValue(ctrlDest, prop.GetValue(ctrlSource, null), null);
                }
            }



            return ctrlDest;
        }

      


    }


}
