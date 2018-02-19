using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Dyn = DynamicTypeDescriptor;
using Scm = System.ComponentModel;

namespace CustomTypeDescriptorApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Dyn.ResourceAttribute ra = new Dyn.ResourceAttribute();
            ra.AssemblyFullName = this.GetType().Assembly.FullName;
            ra.KeyPrefix = "BuiltinBool_";
            ra.BaseName = "CustomTypeDescriptorApp.Properties.Resources";
            Scm.TypeConverterAttribute tca = new TypeConverterAttribute(typeof(Dyn.BooleanConverter));

            Scm.TypeDescriptor.AddAttributes(typeof(Boolean), ra, tca);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            TestClass tc = new TestClass();
            Dyn.TypeDescriptor td = Dyn.TypeDescriptor.GetTypeDescriptor(tc);
            propertyGrid1.Site = td.GetSite();  // this is needed for property state icons
            propertyGrid1.SelectedObject = tc;

            foreach (object o in Enum.GetValues(typeof(PropertySort)))
            {
                this.cboSortByGrid.Items.Add(o);
            }
            foreach (object o in Enum.GetValues(typeof(Dyn.SortOrder)))
            {
                this.cboSortByProperty.Items.Add(o);
            }
            foreach (object o in Enum.GetValues(typeof(Dyn.SortOrder)))
            {
                this.cboSortByCategory.Items.Add(o);
            }

            this.cboSortByGrid.SelectedIndex = (int)PropertySort.Categorized;
            this.cboSortByProperty.SelectedIndex = (int)Dyn.SortOrder.ByIdAscending;
            this.cboSortByCategory.SelectedIndex = (int)Dyn.SortOrder.ByIdAscending;

            this.cboLang.SelectedIndex = 0;

        }

        private void cboSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.PropertySort = (PropertySort)Enum.ToObject(typeof(PropertySort), cboSortByGrid.SelectedItem);
        }

        private void cboSortByProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dyn.TypeDescriptor ctd = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid1.SelectedObject);
            ctd.PropertySortOrder = (Dyn.SortOrder)Enum.ToObject(typeof(Dyn.SortOrder), cboSortByProperty.SelectedItem);
            Scm.TypeDescriptor.Refresh(propertyGrid1.SelectedObject);
        }

        private void cboSortByCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dyn.TypeDescriptor ctd = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid1.SelectedObject);
            ctd.CategorySortOrder = (Dyn.SortOrder)Enum.ToObject(typeof(Dyn.SortOrder), cboSortByCategory.SelectedItem);
            Scm.TypeDescriptor.Refresh(propertyGrid1.SelectedObject);

        }

        private void cboLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboLang.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("da-DK");
                    break;
                case 2:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                    break;
            }
            Scm.TypeDescriptor.Refresh(propertyGrid1.SelectedObject);
        }

        private void ctxReset_Opening(object sender, CancelEventArgs e)
        {
            object owner = GetOwner(propertyGrid1.SelectedGridItem);
            mnuReset.Enabled = propertyGrid1.SelectedGridItem.PropertyDescriptor.CanResetValue(owner);
        }
        private object GetOwner(GridItem gi)
        {
            PropertyDescriptor pdSel = gi.PropertyDescriptor;
            PropertyDescriptor pdParent = null;
            ICustomTypeDescriptor ctd = TypeDescriptor.GetProvider(propertyGrid1.SelectedObject).GetTypeDescriptor(propertyGrid1.SelectedObject);
            if (gi.Parent != null && gi.Parent.PropertyDescriptor != null)
            {
                pdParent = gi.Parent.PropertyDescriptor;
            }

            Object owner = null;
            if (pdParent == null)
            {
                owner = ctd.GetPropertyOwner(pdSel);
            }
            else
            {
                Object ownerParent = ctd.GetPropertyOwner(pdParent);
                owner = pdParent.GetValue(ownerParent);
            }
            Scm.TypeConverter tc = Scm.TypeDescriptor.GetConverter(owner);
            return owner;
        }
        private void mnuReset_Click(object sender, EventArgs e)
        {
            object owner = GetOwner(propertyGrid1.SelectedGridItem);
            propertyGrid1.SelectedGridItem.PropertyDescriptor.ResetValue(owner);

            TypeDescriptor.Refresh(propertyGrid1.SelectedObject);
        }



    }


}