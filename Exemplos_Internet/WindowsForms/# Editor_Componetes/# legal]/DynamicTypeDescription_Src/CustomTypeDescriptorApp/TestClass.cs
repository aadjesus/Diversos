using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using DynamicTypeDescriptor;

using Dyn = DynamicTypeDescriptor;

using Scm = System.ComponentModel;

namespace CustomTypeDescriptorApp
{
    [Dyn.ResourceAttribute("CustomTypeDescriptorApp.Properties.Resources", "Days_")]
    [Scm.Editor(typeof(Dyn.EnumEditor), typeof(UITypeEditor))]
    [Scm.TypeConverter(typeof(Dyn.EnumConverter))]
    [Flags]
    [Dyn.ExpandEnum(true)]
    public enum Days : ulong
    {
        [Scm.Description("Event will not reoccure.")]
        [Dyn.DisplayName("Not Selected")]
        None = 0,

        [Scm.Description("Day of the Moon.")]
        [Dyn.DisplayName("Monday")]
        Mon = 1,

        [Dyn.DisplayName("Tuesday")]
        [Scm.Description("Day of the Mars.")]
        Tue = 2,

        // disable this one, just for the sake of it
        [Dyn.DisplayName("Wednesday")]
        [Scm.Description("Day of the Mercury.")]
        [Scm.ReadOnly(true)]
        Wed = 4,

        [Dyn.DisplayName("Thursday")]
        [Scm.Description("Day of the Jupiter.")]
        Thr = 8,

        [Dyn.DisplayName("Friday")]
        [Scm.Description("Venus's day.")]
        Fri = 16,

        // hide this one, just for the sake of it
        [Dyn.DisplayName("Saturday")]
        [Scm.Description("Day of the Saturn.")]
        [Scm.Browsable(false)]
        Sat = 32,

        // hide this one, just for the sake of it
        [Dyn.DisplayName("Sunday")]
        [Scm.Description("Day of the sun.")]
        [Scm.Browsable(false)]
        Sun = 64,

        [Dyn.DisplayName("Weekdays")]
        [Scm.Description("All days except Saturday and Sunday.")]
        Work = Days.Mon | Days.Tue | Days.Wed | Days.Thr | Days.Fri,

        [Dyn.DisplayName("Weekend")]
        [Scm.Description("Only Saturday and Sunday.")]
        NoWork = Days.Sat | Days.Sun,
    }

    [Dyn.ResourceAttribute("CustomTypeDescriptorApp.Properties.Resources", KeyPrefix = "Placement_")]
    [Scm.Editor(typeof(Dyn.EnumEditor), typeof(UITypeEditor))]
    [Scm.TypeConverter(typeof(Dyn.EnumConverter))]
    [Dyn.ExpandEnum(true)]
    public enum Placement
    {
        [Dyn.DisplayName("Not Selected")]
        [Scm.Description("Placement has not been decided.")]
        None,

        [Dyn.DisplayName("First place")]
        [Scm.Description("Gold medalist")]
        One,

        [Dyn.DisplayName("Second place")]
        [Scm.Description("Silver medalist.")]
        Two,

        [Dyn.DisplayName("Third place")]
        [Scm.Description("Bronz medalist.")]
        Third,
    }

    [Dyn.ResourceAttribute(BaseName = "CustomTypeDescriptorApp.Properties.Resources", KeyPrefix = "TC_")]
    public class TestClass
    {
        private Days m_WeekendWork = Days.Sat;

        public TestClass()
        {
            bool bOk = Dyn.TypeDescriptor.IntallTypeDescriptor(this);
            Dyn.TypeDescriptor td = Dyn.TypeDescriptor.GetTypeDescriptor(this);
            CreateOnTheFlyPropertyE();

            Scm.PropertyDescriptorCollection pdc = td.GetProperties();
            Dyn.PropertyDescriptor pd = null;

            PropG = 1; // initialize
            PropH = 2;  // intialize

            pd = td.GetProperties().Find("PropI", true) as Dyn.PropertyDescriptor;
            PopululateDropDownListFromDatabaseSource(pd);

            pd = td.GetProperties().Find("PropJ", true) as Dyn.PropertyDescriptor;
            PopululateDropDownListFromDatabaseSource(pd);
            pd.Attributes.Add(new Scm.LocalizableAttribute(true), true);
        }

        // enum without flag attribute
        private Placement m_PropertyA = Placement.Two;

        [Dyn.SortID(1, 1)]
        [Scm.DefaultValue(Placement.Two)]
        [Dyn.CategoryResourceKey("Cat1")]
        public Placement PropA
        {
            get
            {
                return m_PropertyA;
            }
            set
            {
                m_PropertyA = value;
            }
        }

        // enum with flag attribute
        private Days m_PropertyB = Days.Mon | Days.Tue;

        [Dyn.SortID(2, 1)]
        [Scm.DefaultValue(Days.Mon | Days.Tue)]
        [Dyn.CategoryResourceKey("Cat1")]
        [Dyn.ExclusiveStandardValues(true)]
        public Days PropB
        {
            get
            {
                return m_PropertyB;
            }
            set
            {
                m_PropertyB = value;
            }
        }

        // boolean as enum
        private bool m_PropertyC = true;

        [Scm.DefaultValue(true)]
        [Dyn.SortID(3, 1)]
        [Scm.Editor(typeof(Dyn.StandardValuesEditor), typeof(UITypeEditor))]
        [Scm.TypeConverter(typeof(Dyn.BooleanConverter))]
        [Dyn.CategoryResourceKey("Cat1")]
        [Dyn.Resource("CustomTypeDescriptorApp.Properties.Resources",
          KeyPrefix = "PropC_",
          AssemblyFullName = "CustomTypeDescriptorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
        public bool PropC
        {
            get
            {
                return m_PropertyC;
            }
            set
            {
                m_PropertyC = value;
            }
        }

        private System.Windows.Forms.FormStartPosition m_PropertyD = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;

        [Dyn.SortID(4, 1)]
        [Scm.Editor(typeof(Dyn.EnumEditor), typeof(UITypeEditor))]
        [Scm.TypeConverter(typeof(Dyn.EnumConverter))]
        [Dyn.Resource("CustomTypeDescriptorApp.Properties.Resources",
          KeyPrefix = "BuiltInEnum_",
          AssemblyFullName = "CustomTypeDescriptorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
        [Scm.DefaultValue(FormStartPosition.WindowsDefaultLocation)]
        [Dyn.CategoryResourceKey("Cat1")]
        public System.Windows.Forms.FormStartPosition PropD
        {
            get
            {
                return m_PropertyD;
            }
            set
            {
                m_PropertyD = value;
            }
        }

        // on-the-fly
        private void CreateOnTheFlyPropertyE()
        {
            Dyn.PropertyDescriptor pd = new Dyn.PropertyDescriptor(this.GetType(),
                            "PropE", typeof(int), 5,
                            new Scm.BrowsableAttribute(true),
                            new Scm.LocalizableAttribute(true),
                            new Scm.DefaultValueAttribute(5),
                            new Scm.DisplayNameAttribute("PropertyE"),
                            new Scm.DescriptionAttribute("Description of PropertyE"),
                            new Scm.CategoryAttribute("On-the-fly property"),
                            new Dyn.SortIDAttribute(5, 2));
            pd.AddValueChanged(this, new EventHandler(this.OnPropertyEChanged));

            Dyn.TypeDescriptor td = Dyn.TypeDescriptor.GetTypeDescriptor(this);
            td.GetProperties().Add(pd);
        }

        // disable-property
        private int m_PropertyF = 80;

        [Dyn.SortID(6, 2)]
        [Scm.DefaultValue(80)]
        [Scm.ReadOnly(true)]
        [Dyn.CategoryResourceKey("Cat2")]
        public int PropF
        {
            get
            {
                return m_PropertyF;
            }
            set
            {
                m_PropertyF = value;
            }
        }

        // value image
        private int m_PropertyG = 1;

        [Dyn.SortID(7, 3)]
        [Scm.Editor(typeof(Dyn.PropertyValuePaintEditor), typeof(UITypeEditor))]
        [Scm.DefaultValue(1)]
        [Dyn.CategoryResourceKey("Cat3")]
        public int PropG
        {
            get
            {
                return m_PropertyG;
            }
            set
            {
                m_PropertyG = value;
                if (m_PropertyG < 0)
                {
                    m_PropertyG = 1;
                }
                if (m_PropertyG > 3)
                {
                    m_PropertyG = 3;
                }

                Dyn.TypeDescriptor td = Dyn.TypeDescriptor.GetTypeDescriptor(this);
                Dyn.PropertyDescriptor pd = td.GetProperties().Find("PropG", true) as Dyn.PropertyDescriptor;

                pd.ValueImage = null;
                switch (m_PropertyG)
                {
                    case 1:
                        pd.ValueImage = CustomTypeDescriptorApp.Properties.Resources.HappyFace;
                        break;

                    case 2:
                        pd.ValueImage = CustomTypeDescriptorApp.Properties.Resources.OkFace;
                        break;

                    case 3:
                        pd.ValueImage = CustomTypeDescriptorApp.Properties.Resources.UnhappyFace;
                        break;
                }
                Scm.TypeDescriptor.Refresh(this);
            }
        }

        // state image
        private int m_PropertyH = 2;

        [Dyn.SortID(8, 3)]
        [Scm.DefaultValue(2)]
        [Dyn.CategoryResourceKey("Cat3")]
        public int PropH
        {
            get
            {
                return m_PropertyH;
            }
            set
            {
                m_PropertyH = value;
                if (m_PropertyH < 1)
                {
                    m_PropertyH = 1;
                }
                if (m_PropertyH > 3)
                {
                    m_PropertyH = 3;
                }
                Dyn.TypeDescriptor td = Dyn.TypeDescriptor.GetTypeDescriptor(this);

                Dyn.PropertyDescriptor pd = td.GetProperties().Find("PropH", true) as Dyn.PropertyDescriptor;
                pd.StateItems.Clear();
                for (int i = 0; i < m_PropertyH; i++)
                {
                    PropertyValueUIItem pvui = new PropertyValueUIItem(CustomTypeDescriptorApp.Properties.Resources.ErrorState1,
                                                    this.UIItemClicked, "Index " + (i + 1).ToString() + ". Double-click the icon.");

                    pd.StateItems.Add(pvui);
                }
                Scm.TypeDescriptor.Refresh(this);
            }
        }

        // standard value exclusive
        private int m_PropertyI = 101;

        [Dyn.SortID(9, 4)]
        [Scm.Editor(typeof(Dyn.StandardValuesEditor), typeof(UITypeEditor))]
        [Scm.TypeConverter(typeof(Dyn.StandardValuesConverter))]
        [Scm.DefaultValue(101)]
        [Dyn.CategoryResourceKey("Cat4")]
        public int PropI
        {
            get
            {
                return m_PropertyI;
            }
            set
            {
                m_PropertyI = value;
            }
        }

        // standard value exclusive
        private int m_PropertyJ = 101;

        [Dyn.ExclusiveStandardValues(true)]
        [Dyn.SortID(10, 4)]
        [Scm.Editor(typeof(Dyn.StandardValuesEditor), typeof(UITypeEditor))]
        [Scm.TypeConverter(typeof(Dyn.StandardValuesConverter))]
        [Scm.DefaultValue(101)]
        [Dyn.CategoryResourceKey("Cat4")]
        public int PropJ
        {
            get
            {
                return m_PropertyJ;
            }
            set
            {
                m_PropertyJ = value;
            }
        }

        private List<Size> m_PropK = new List<Size>();

        [Dyn.SortID(11, 5)]
        [Scm.TypeConverter(typeof(Dyn.ExpandableIEnumerationConverter))]
        [Scm.EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Dyn.CategoryResourceKey("Cat5")]
        public List<Size> PropK
        {
            get
            {
                if (m_PropK.Count == 0)
                {
                    m_PropK.Add(new Size(1, 2));
                    m_PropK.Add(new Size(3, 4));
                    m_PropK.Add(new Size(5, 6));
                }
                return m_PropK;
            }
            set
            {
                m_PropK = value;
            }
        }

        private int[] m_PropL = new int[] { 2, 4, 6, 8 };

        [Dyn.SortID(13, 5)]
        [Scm.TypeConverter(typeof(Dyn.ExpandableIEnumerationConverter))]
        [Scm.EditorAttribute(typeof(System.ComponentModel.Design.ArrayEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Dyn.CategoryResourceKey("Cat5")]
        public int[] PropL
        {
            get
            {
                return m_PropL;
            }
            set
            {
                m_PropL = value;
            }
        }

        private Collection<Point> m_PropM = new Collection<Point>();

        [Dyn.SortID(14, 5)]
        [Scm.TypeConverter(typeof(Dyn.ExpandableIEnumerationConverter))]
        [Scm.EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Dyn.CategoryResourceKey("Cat5")]
        public Collection<Point> PropM
        {
            get
            {
                if (m_PropM.Count == 0)
                {
                    m_PropM.Add(new Point(11, 12));
                    m_PropM.Add(new Point(13, 14));
                    m_PropM.Add(new Point(15, 16));
                }
                return m_PropM;
            }
            set
            {
                m_PropM = value;
            }
        }

        private void OnPropertyEChanged(object sender, EventArgs e)
        {
            Dyn.PropertyDescriptor pd = Dyn.TypeDescriptor.GetTypeDescriptor(sender).GetProperties().Find("PropE", true) as Dyn.PropertyDescriptor;
            m_WeekendWork = (Days)Enum.ToObject(typeof(Days), pd.GetValue(sender));
        }

        private void UIItemClicked(Scm.ITypeDescriptorContext context, Scm.PropertyDescriptor propDesc, PropertyValueUIItem item)
        {
            StringBuilder sb = new StringBuilder();
            Dyn.PropertyDescriptor pd = propDesc as Dyn.PropertyDescriptor;
            sb.AppendLine("Prop state icon clicked for property '" + pd.DisplayName + "'.");
            sb.AppendLine("Tool tip:");
            sb.AppendLine(item.ToolTip);
            MessageBox.Show(sb.ToString());
        }

        private void PopululateDropDownListFromDatabaseSource(Dyn.PropertyDescriptor pd)
        {
            // we actually don't have any data source in this sample.
            // we will simply add some hard coded data here.
            // we will make  customer names and id.

            pd.StandardValues.Clear();
            string[] arrNames = { "Adam", "Brian", "Russel", "Jones", "Jakob" };
            for (int i = 101; i < 106; i++)
            {
                Dyn.StandardValue sv = new Dyn.StandardValue(i, arrNames[i - 101]);
                sv.Description = "Description of " + sv.DisplayName + ".";
                pd.StandardValues.Add(sv);
            }
        }
    }
}