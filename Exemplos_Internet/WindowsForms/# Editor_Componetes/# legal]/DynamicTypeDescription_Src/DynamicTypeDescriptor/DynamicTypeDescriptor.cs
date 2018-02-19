using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms.Design;
using Dyn = DynamicTypeDescriptor;
using Scm = System.ComponentModel;

namespace DynamicTypeDescriptor
{
    public enum SortOrder
    {
        // no custom sorting
        None,

        // sort asscending using the property name or category name
        ByNameAscending,

        // sort descending using the property name or category name
        ByNameDescending,

        // sort asscending using property id or categor id
        ByIdAscending,

        // sort descending using property id or categor id
        ByIdDescending,
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public class ExpandEnumAttribute : Attribute
    {
        public ExpandEnumAttribute(bool expand)
            : base()
        {
            Exapand = expand;
        }

        public bool Exapand
        {
            get;
            set;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExclusiveStandardValuesAttribute : Attribute
    {
        public ExclusiveStandardValuesAttribute(bool exclusive)
            : base()
        {
            Exclusive = exclusive;
        }

        public bool Exclusive
        {
            get;
            set;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ResourceAttribute : Attribute
    {
        public ResourceAttribute()
            : base()
        {
        }

        public ResourceAttribute(string baseString)
            : base()
        {
            BaseName = baseString;
        }

        public ResourceAttribute(string baseString, string keyPrefix)
            : base()
        {
            BaseName = baseString;
            KeyPrefix = keyPrefix;
        }

        public string BaseName
        {
            get;
            set;
        }

        public string KeyPrefix
        {
            get;
            set;
        }

        public string AssemblyFullName
        {
            get;
            set;
        }

        // Use the hash code of the string objects and xor them together.
        public override int GetHashCode()
        {
            return (BaseName.GetHashCode() ^ KeyPrefix.GetHashCode()) ^ AssemblyFullName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ResourceAttribute))
            {
                return false;
            }
            ResourceAttribute other = obj as ResourceAttribute;

            if (String.Compare(this.BaseName, other.BaseName, true) == 0 &&
                String.Compare(this.AssemblyFullName, other.AssemblyFullName, true) == 0)
            {
                return true;
            }

            return false;
        }

        public override bool Match(object obj)
        {
            // Obviously a match.
            if (obj == this)
                return true;

            // Obviously we're not null, so no.
            if (obj == null)
                return false;

            if (obj is ResourceAttribute)

                // Combine the hash codes and see if they're unchanged.
                return (((ResourceAttribute)obj).GetHashCode() & GetHashCode())
                  == GetHashCode();
            else
                return false;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SortIDAttribute : Attribute
    {
        public SortIDAttribute()
            : base()
        {
            PropertyOrder = 0;
            CategoryOrder = 0;
        }

        public SortIDAttribute(int propertyId, int categoryId)
            : base()
        {
            PropertyOrder = propertyId;
            CategoryOrder = categoryId;
        }

        public int PropertyOrder
        {
            get;
            set;
        }

        public int CategoryOrder
        {
            get;
            set;
        }
    }

    public static class AttributeCollectionExtesion
    {
        public static void Add(this System.ComponentModel.AttributeCollection ac, Attribute attribute)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            List<Attribute> listAttr = new List<Attribute>();
            if (arrAttr != null)
            {
                listAttr.AddRange(arrAttr);
            }
            listAttr.Add(attribute);
            fi.SetValue(ac, listAttr.ToArray());
        }

        public static void AddRange(this System.ComponentModel.AttributeCollection ac, Attribute[] attributes)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            List<Attribute> listAttr = new List<Attribute>();
            if (arrAttr != null)
            {
                listAttr.AddRange(arrAttr);
            }
            listAttr.AddRange(attributes);
            fi.SetValue(ac, listAttr.ToArray());
        }

        public static void Add(this System.ComponentModel.AttributeCollection ac, Attribute attribute, bool removeBeforeAdd)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            List<Attribute> listAttr = new List<Attribute>();
            if (arrAttr != null)
            {
                listAttr.AddRange(arrAttr);
            }
            if (removeBeforeAdd)
            {
                listAttr.RemoveAll(a => a.Match(attribute));
            }
            listAttr.Add(attribute);
            fi.SetValue(ac, listAttr.ToArray());
        }

        public static void Add(this System.ComponentModel.AttributeCollection ac, Attribute attribute, Type typeToRemoveBeforeAdd)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            List<Attribute> listAttr = new List<Attribute>();
            if (arrAttr != null)
            {
                listAttr.AddRange(arrAttr);
            }
            if (typeToRemoveBeforeAdd != null)
            {
                listAttr.RemoveAll(a => a.GetType() == typeToRemoveBeforeAdd || a.GetType().IsSubclassOf(typeToRemoveBeforeAdd));
            }
            listAttr.Add(attribute);
            fi.SetValue(ac, listAttr.ToArray());
        }

        public static void Clear(this System.ComponentModel.AttributeCollection ac)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(ac, null);
        }

        public static void Remove(this System.ComponentModel.AttributeCollection ac, Attribute attribute)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            List<Attribute> listAttr = new List<Attribute>();
            if (arrAttr != null)
            {
                listAttr.AddRange(arrAttr);
            }
            listAttr.RemoveAll(a => a.Match(attribute));
            fi.SetValue(ac, listAttr.ToArray());
        }

        public static void Remove(this System.ComponentModel.AttributeCollection ac, Type type)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            List<Attribute> listAttr = new List<Attribute>();
            if (arrAttr != null)
            {
                listAttr.AddRange(arrAttr);
            }
            listAttr.RemoveAll(a => a.GetType() == type);
            fi.SetValue(ac, listAttr.ToArray());
        }

        public static Attribute Get(this System.ComponentModel.AttributeCollection ac, Attribute attribute)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            if (arrAttr == null)
            {
                return null;
            }
            Attribute attrFound = arrAttr.FirstOrDefault(a => a.Match(attribute));
            return attrFound;
        }

        public static List<Attribute> Get(this System.ComponentModel.AttributeCollection ac, params Attribute[] attributes)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);

            if (arrAttr == null)
            {
                return null;
            }
            List<Attribute> listAttr = new List<Attribute>();
            listAttr.AddRange(arrAttr);
            Scm.AttributeCollection ac2 = new Scm.AttributeCollection(attributes);
            List<Attribute> listAttrFound = listAttr.FindAll(a => ac2.Matches(a));
            return listAttrFound;
        }

        public static Attribute Get(this System.ComponentModel.AttributeCollection ac, Type attributeType)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            Attribute attrFound = arrAttr.FirstOrDefault(a => a.GetType() == attributeType);
            return attrFound;
        }

        public static Attribute Get(this System.ComponentModel.AttributeCollection ac, Type attributeType, bool derivedType)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            Attribute attrFound = null;
            if (!derivedType)
            {
                attrFound = arrAttr.FirstOrDefault(a => a.GetType() == attributeType);
            }
            else
            {
                attrFound = arrAttr.FirstOrDefault(a => a.GetType() == attributeType || a.GetType().IsSubclassOf(attributeType));
            }
            return attrFound;
        }

        public static List<Attribute> Get(this System.ComponentModel.AttributeCollection ac, params Type[] attributeTypes)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);

            if (arrAttr == null)
            {
                return null;
            }
            List<Attribute> listAttr = new List<Attribute>();
            listAttr.AddRange(arrAttr);
            List<Attribute> listAttrFound = listAttr.FindAll(a => a.GetType() == attributeTypes.FirstOrDefault(b => b.GetType() == a.GetType()));

            return listAttrFound;
        }

        public static Attribute[] ToArray(this System.ComponentModel.AttributeCollection ac)
        {
            FieldInfo fi = ac.GetType().GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance);
            Attribute[] arrAttr = (Attribute[])fi.GetValue(ac);
            return arrAttr;
        }
    }

    public class StandardValue
    {
        public StandardValue(object value)
        {
            m_Value = value;
            Enabled = true;
            Visible = true;
        }

        public StandardValue(object value, string displayName)
        {
            DisplayName = displayName;
            m_Value = value;
            Enabled = true;
        }

        public StandardValue(object value, string displayName, string description)
        {
            m_Value = value;
            DisplayName = displayName;
            Description = description;
            Enabled = true;
            Visible = true;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        private object m_Value = null;

        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(DisplayName) && (Value != null))
            {
                return Value.ToString();
            }
            return DisplayName;
        }
    }

    public class StandardValuesEditor : UITypeEditor
    {
        private StandardValueEditorUI m_ui = new StandardValueEditorUI();

        public StandardValuesEditor()
        {
        }

        public override bool GetPaintValueSupported(Scm.ITypeDescriptorContext context)
        {
            // let the property browser know we'd like
            // to do custom painting.
            if (context != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
                return (pd.ValueImage != null);
            }
            return base.GetPaintValueSupported(context);
        }

        public override UITypeEditorEditStyle GetEditStyle(Scm.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }

        public override object EditValue(Scm.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                if (editorService == null)
                    return value;

                m_ui.SetData(context, editorService, value);
                editorService.DropDownControl(m_ui);
                value = m_ui.GetValue();
            }
            return value;
        }

        public override void PaintValue(PaintValueEventArgs pe)
        {
            if (pe.Context != null)
            {
                if (pe.Context.PropertyDescriptor != null)
                {
                    if (pe.Context.PropertyDescriptor is Dyn.PropertyDescriptor)
                    {
                        PropertyDescriptor pd = pe.Context.PropertyDescriptor as Dyn.PropertyDescriptor;

                        if (pd.ValueImage != null)
                        {
                            pe.Graphics.DrawImage(pd.ValueImage, pe.Bounds);
                            return;
                        }
                    }
                }
            }
            base.PaintValue(pe);
        }
    }

    public class EnumEditor : UITypeEditor
    {
        private EnumValueEditorUI m_ui = new EnumValueEditorUI();

        public EnumEditor()
        {
        }

        public override bool GetPaintValueSupported(Scm.ITypeDescriptorContext context)
        {
            // let the property browser know we'd like
            // to do custom painting.
            if (context != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
                return (pd.ValueImage != null);
            }
            return base.GetPaintValueSupported(context);
        }

        public override UITypeEditorEditStyle GetEditStyle(Scm.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }

        public override object EditValue(Scm.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                if (editorService == null)
                    return value;

                m_ui.SetData(context, editorService, value);

                editorService.DropDownControl(m_ui);

                value = m_ui.GetValue();
            }

            return value;
        }

        public override void PaintValue(PaintValueEventArgs pe)
        {
            if (pe.Context != null)
            {
                if (pe.Context.PropertyDescriptor != null)
                {
                    if (pe.Context.PropertyDescriptor is Dyn.PropertyDescriptor)
                    {
                        PropertyDescriptor pd = pe.Context.PropertyDescriptor as Dyn.PropertyDescriptor;

                        if (pd.ValueImage != null)
                        {
                            pe.Graphics.DrawImage(pd.ValueImage, pe.Bounds);
                            return;
                        }
                    }
                }
            }
            base.PaintValue(pe);
        }
    }

    public class PropertyValuePaintEditor : UITypeEditor
    {
        public override bool GetPaintValueSupported(Scm.ITypeDescriptorContext context)
        {
            // let the property browser know we'd like
            // to do custom painting.
            if (context != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
                return (pd.ValueImage != null);
            }
            return base.GetPaintValueSupported(context);
        }

        public override UITypeEditorEditStyle GetEditStyle(Scm.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }

        public override void PaintValue(PaintValueEventArgs pe)
        {
            if (pe.Context != null)
            {
                if (pe.Context.PropertyDescriptor != null)
                {
                    if (pe.Context.PropertyDescriptor is Dyn.PropertyDescriptor)
                    {
                        PropertyDescriptor pd = pe.Context.PropertyDescriptor as Dyn.PropertyDescriptor;

                        if (pd.ValueImage != null)
                        {
                            pe.Graphics.DrawImage(pd.ValueImage, pe.Bounds);
                            return;
                        }
                    }
                }
            }
            base.PaintValue(pe);
        }
    }

    public class Int32Converter2 : Scm.Int32Converter
    {
        public override bool CanConvertFrom(Scm.ITypeDescriptorContext context, Type sourceType)
        {
            bool bOk = base.CanConvertFrom(context, sourceType);
            return bOk;
        }

        public override bool CanConvertTo(Scm.ITypeDescriptorContext context, Type t)
        {
            bool bOk = base.CanConvertTo(context, t);
            return bOk;
        }

        public override object ConvertFrom(Scm.ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object obj = base.ConvertFrom(context, culture, value);
            return obj;
        }

        public override object ConvertTo(Scm.ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object obj = base.ConvertTo(context, culture, value, destinationType);
            return obj;
        }

        public override object CreateInstance(Scm.ITypeDescriptorContext context, IDictionary propertyValues)
        {
            object obj = base.CreateInstance(context, propertyValues);
            return obj;
        }

        public override bool Equals(object obj)
        {
            bool bOk = base.Equals(obj);
            return bOk;
        }

        public override bool GetCreateInstanceSupported(Scm.ITypeDescriptorContext context)
        {
            bool bOk = base.GetCreateInstanceSupported(context);
            return bOk;
        }

        public override int GetHashCode()
        {
            int nInt = base.GetHashCode();
            return nInt;
        }

        public override Scm.PropertyDescriptorCollection GetProperties(Scm.ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            Scm.PropertyDescriptorCollection pdc = base.GetProperties(context, value, attributes);
            return pdc;
        }

        public override bool GetPropertiesSupported(Scm.ITypeDescriptorContext context)
        {
            bool bok = base.GetPropertiesSupported(context);
            return bok;
        }

        public override StandardValuesCollection GetStandardValues(Scm.ITypeDescriptorContext context)
        {
            StandardValuesCollection svc = base.GetStandardValues(context);
            return svc;
        }

        public override bool GetStandardValuesExclusive(Scm.ITypeDescriptorContext context)
        {
            bool bOk = base.GetStandardValuesExclusive(context);
            return bOk;
        }

        public override bool GetStandardValuesSupported(Scm.ITypeDescriptorContext context)
        {
            bool bOk = base.GetStandardValuesSupported(context);
            return bOk;
        }

        public override bool IsValid(Scm.ITypeDescriptorContext context, object value)
        {
            bool bOk = base.IsValid(context, value);
            return bOk;
        }

        public override string ToString()
        {
            string s = base.ToString();
            return s;
        }
    }

    public class StandardValuesConverter : Scm.TypeConverter
    {
        static private ulong m_COUNT = 1;
        internal readonly ulong _ID;

        public StandardValuesConverter()
            : base()
        {
            _ID = m_COUNT++;
        }

        public override bool CanConvertFrom(Scm.ITypeDescriptorContext context, Type sourceType)
        {
            if (context != null &&
                context.PropertyDescriptor != null &&
                context.PropertyDescriptor is Dyn.PropertyDescriptor &&
                sourceType == typeof(string))
            {
                return true;
            }

            bool bOk = base.CanConvertFrom(context, sourceType);
            return bOk;
        }

        public override object ConvertFrom(Scm.ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object retObj = null;
            if (context == null || context.PropertyDescriptor == null || !(context.PropertyDescriptor is Dyn.PropertyDescriptor) || value == null)
            {
                retObj = base.ConvertFrom(context, culture, value);
                return retObj;
            }
            PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;

            if (value is string)
            {
                foreach (StandardValue sv in pd.StandardValues)
                {
                    if (String.Compare(value.ToString(), sv.DisplayName, true, culture) == 0 ||
                        String.Compare(value.ToString(), sv.Value.ToString(), true, culture) == 0)
                    {
                        return sv.Value;
                    }
                }
            }

            // try the native converter of the value.
            Scm.TypeConverter tc = Scm.TypeDescriptor.GetConverter(context.PropertyDescriptor.PropertyType);
            Debug.Assert(tc != null);
            retObj = tc.ConvertFrom(context, culture, value);
            return retObj;
        }

        public override bool CanConvertTo(Scm.ITypeDescriptorContext context, Type destinationType)
        {
            if (context != null &&
                context.PropertyDescriptor != null &&
                context.PropertyDescriptor is Dyn.PropertyDescriptor &&
                (destinationType == typeof(string) || destinationType == typeof(StandardValue)))
            {
                return true;
            }

            bool bOk = base.CanConvertTo(context, destinationType);
            return bOk;
        }

        public override object ConvertTo(Scm.ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object retObj = null;

            if (context == null || context.PropertyDescriptor == null || !(context.PropertyDescriptor is Dyn.PropertyDescriptor) || value == null)
            {
                retObj = base.ConvertTo(context, culture, value, destinationType);
                return retObj;
            }
            PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;

            if (value is string)
            {
                if (destinationType == typeof(string))
                {
                    return value;
                }
                else if (destinationType == pd.PropertyType)
                {
                    return ConvertFrom(context, culture, value);
                }
                else if (destinationType == typeof(StandardValue))
                {
                    foreach (StandardValue sv in pd.StandardValues)
                    {
                        if (String.Compare(value.ToString(), sv.DisplayName, true, culture) == 0 ||
                            String.Compare(value.ToString(), sv.Value.ToString(), true, culture) == 0)
                        {
                            return sv;
                        }
                    }
                }
            }
            else if (value.GetType() == pd.PropertyType)
            {
                if (destinationType == typeof(string))
                {
                    foreach (StandardValue sv in pd.StandardValues)
                    {
                        if (sv.Value.Equals(value))
                        {
                            return sv.DisplayName;
                        }
                    }
                }
                else if (destinationType == typeof(StandardValue))
                {
                    foreach (StandardValue sv in pd.StandardValues)
                    {
                        if (sv.Value.Equals(value))
                        {
                            return sv;
                        }
                    }
                }
            }

            // try the native converter of the value.
            Scm.TypeConverter tc = Scm.TypeDescriptor.GetConverter(context.PropertyDescriptor.PropertyType);
            Debug.Assert(tc != null);
            retObj = tc.ConvertTo(context, culture, value, destinationType);
            return retObj;
        }

        public override bool GetStandardValuesSupported(Scm.ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                return (context.PropertyDescriptor as Dyn.PropertyDescriptor).StandardValues.Count > 0;
            }
            return base.GetStandardValuesSupported(context);
        }

        public override bool GetStandardValuesExclusive(Scm.ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null)
            {
                ExclusiveStandardValuesAttribute psfa = (ExclusiveStandardValuesAttribute)context.PropertyDescriptor.Attributes.Get(typeof(ExclusiveStandardValuesAttribute), true);
                if (psfa != null)
                {
                    return psfa.Exclusive;
                }
            }
            return base.GetStandardValuesExclusive(context);
        }

        public override StandardValuesCollection GetStandardValues(Scm.ITypeDescriptorContext context)
        {
            if (context == null || context.PropertyDescriptor == null || !(context.PropertyDescriptor is Dyn.PropertyDescriptor))
            {
                return base.GetStandardValues(context);
            }
            PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
            List<object> list = new List<object>();
            foreach (StandardValue sva in pd.StandardValues)
            {
                if (sva.Visible && sva.Enabled)
                {
                    list.Add(sva.Value);
                }
            }
            StandardValuesCollection svc = new StandardValuesCollection(list);

            return svc;
        }
    }

    public class ExpandableIEnumerationConverter : Scm.TypeConverter
    {
        public ExpandableIEnumerationConverter()
            : base()
        {
        }

        public override bool GetPropertiesSupported(Scm.ITypeDescriptorContext context)
        {
            if (context != null)
            {
                IEnumerable enu = context.PropertyDescriptor.GetValue(context.Instance) as IEnumerable;
                return (enu != null);
            }
            return base.GetPropertiesSupported(context);
        }

        public override Scm.PropertyDescriptorCollection GetProperties(Scm.ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value == null)
            {
                return base.GetProperties(context, value, attributes);
            }
            Scm.PropertyDescriptorCollection pdc = new Scm.PropertyDescriptorCollection(null, false);
            int nIndex = -1;

            IEnumerable en = value as IEnumerable;
            Debug.Assert(en != null);

            if (en != null)
            {
                IEnumerator enu = en.GetEnumerator();
                enu.Reset();
                while (enu.MoveNext())
                {
                    nIndex++;
                    string sPropName = enu.Current.ToString();

                    Scm.IComponent comp = enu.Current as Scm.IComponent;
                    if (comp != null && comp.Site != null && !String.IsNullOrEmpty(comp.Site.Name))
                    {
                        sPropName = comp.Site.Name;
                    }
                    else if (value.GetType().IsArray)
                    {
                        sPropName = "[" + nIndex.ToString() + "]";
                    }
                    pdc.Add(new PropertyDescriptor(value.GetType(), sPropName, enu.Current.GetType(), enu.Current, Scm.TypeDescriptor.GetAttributes(enu.Current).ToArray()));
                }
            }

            return pdc;
        }
    }

    public class EnumConverter : Scm.EnumConverter
    {
        static private ulong m_COUNT = 1;
        internal readonly ulong _ID;

        private List<StandardValue> m_StatandardValues = new List<StandardValue>();
        private int m_LCID = -1;

        public EnumConverter(Type type)
            : base(type)
        {
            _ID = m_COUNT++;
        }

        public override object ConvertFrom(Scm.ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            if (value is string)
            {
                UpdateStringFromResource(context);

                string sInpuValue = value as string;
                string[] arrDispName = sInpuValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                StringBuilder sb = new StringBuilder(1000);
                foreach (string sDispName in arrDispName)
                {
                    string sTrimValue = sDispName.Trim();
                    foreach (StandardValue sv in m_StatandardValues)
                    {
                        if (String.Compare(sv.Value.ToString(), sTrimValue, true) == 0 ||
                            String.Compare(sv.DisplayName, sTrimValue, true) == 0)
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append(",");
                            }
                            sb.Append(sv.Value.ToString());
                        }
                    }
                }  // end of foreach..loop
                return Enum.Parse(this.EnumType, sb.ToString(), true);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(Scm.ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(StandardValue))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(Scm.ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            UpdateStringFromResource(context);

            if (value == null)
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            if (value is string)
            {
                if (destinationType == typeof(string))
                {
                    return value;
                }
                else if (destinationType == this.EnumType)
                {
                    return ConvertFrom(context, culture, value);
                }
                else if (destinationType == typeof(StandardValue))
                {
                    foreach (StandardValue sv in m_StatandardValues)
                    {
                        if (String.Compare(value.ToString(), sv.DisplayName, true, culture) == 0 ||
                            String.Compare(value.ToString(), sv.Value.ToString(), true, culture) == 0)
                        {
                            return sv;
                        }
                    }
                }
            }
            else if (value.GetType() == this.EnumType)
            {
                if (destinationType == typeof(string))
                {
                    string sDelimitedValues = Enum.Format(this.EnumType, value, "G");
                    string[] arrValue = sDelimitedValues.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    StringBuilder sb = new StringBuilder(1000);
                    foreach (string sDispName in arrValue)
                    {
                        string sTrimValue = sDispName.Trim();
                        foreach (StandardValue sv in m_StatandardValues)
                        {
                            if (String.Compare(sv.Value.ToString(), sTrimValue, true) == 0 ||
                                String.Compare(sv.DisplayName, sTrimValue, true) == 0)
                            {
                                if (sb.Length > 0)
                                {
                                    sb.Append(", ");
                                }
                                sb.Append(sv.DisplayName);
                            }
                        }
                    }  // end of foreach..loop
                    return sb.ToString();
                }
                else if (destinationType == typeof(StandardValue))
                {
                    foreach (StandardValue sv in m_StatandardValues)
                    {
                        if (sv.Value.Equals(value))
                        {
                            return sv;
                        }
                    }
                }
                else if (destinationType == this.EnumType)
                {
                    return value;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override StandardValuesCollection GetStandardValues(Scm.ITypeDescriptorContext context)
        {
            UpdateStringFromResource(context);
            List<object> list = new List<object>();
            foreach (StandardValue sva in m_StatandardValues)
            {
                list.Add(sva.Value);
            }
            StandardValuesCollection svc = new StandardValuesCollection(list as ICollection);
            return svc;
        }

        public override bool GetStandardValuesSupported(Scm.ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                return (context.PropertyDescriptor as Dyn.PropertyDescriptor).StandardValues.Count > 0;
            }
            return base.GetStandardValuesSupported(context);
        }

        public override bool GetStandardValuesExclusive(Scm.ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null)
            {
                ExclusiveStandardValuesAttribute psfa = (ExclusiveStandardValuesAttribute)context.PropertyDescriptor.Attributes.Get(typeof(ExclusiveStandardValuesAttribute), true);
                if (psfa != null)
                {
                    return psfa.Exclusive;
                }
            }
            return base.GetStandardValuesExclusive(context);
        }

        public override bool GetPropertiesSupported(Scm.ITypeDescriptorContext context)
        {
            return IsChildPropertySupported(context);
        }

        public override Scm.PropertyDescriptorCollection GetProperties(Scm.ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            UpdateStringFromResource(context);

            Scm.DefaultValueAttribute dva = context.PropertyDescriptor.Attributes.Get(typeof(Scm.DefaultValueAttribute)) as Scm.DefaultValueAttribute;

            Scm.PropertyDescriptorCollection pdc = new Scm.PropertyDescriptorCollection(null, false);
            foreach (StandardValue sv in m_StatandardValues)
            {
                if (sv.Visible)
                {
                    EnumChildPropertyDescriptor epd = new EnumChildPropertyDescriptor(context, sv.Value.ToString(), sv.Value);
                    epd.Attributes.Add(new Scm.ReadOnlyAttribute(!sv.Enabled), true);
                    epd.Attributes.Add(new Scm.DescriptionAttribute(sv.Description), true);
                    epd.Attributes.Add(new Scm.DisplayNameAttribute(sv.DisplayName), true);
                    epd.Attributes.Add(new Scm.BrowsableAttribute(sv.Visible), true);

                    // setup the default value;
                    if (dva != null)
                    {
                        bool bHasBit = EnumUtil.IsBitsOn(dva.Value, sv.Value);
                        epd.DefaultValue = bHasBit;
                    }
                    pdc.Add(epd);
                }
            }
            return pdc;
        }

        private void UpdateStringFromResource(Scm.ITypeDescriptorContext context)
        {
            if (m_StatandardValues.Count > 0 && m_LCID == CultureInfo.CurrentUICulture.LCID)
            {
                return;
            }
            m_LCID = CultureInfo.CurrentUICulture.LCID;
            m_StatandardValues.Clear();
            if (context != null && context.PropertyDescriptor != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                Dyn.PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
                m_StatandardValues.AddRange(pd.StandardValues);
            }
            else
            {
                m_StatandardValues.AddRange(EnumUtil.GetStandardValues(this.EnumType));
            }

            ResourceAttribute ra = null;

            if (context != null && context.PropertyDescriptor != null)
            {
                ra = (ResourceAttribute)context.PropertyDescriptor.Attributes.Get(typeof(ResourceAttribute));
            }
            if (ra == null)
            {
                ra = (ResourceAttribute)Scm.TypeDescriptor.GetAttributes(this.EnumType).Get(typeof(ResourceAttribute));
            }

            if (ra == null)
            {
                return;
            }

            ResourceManager rm = null;

            // construct the resource manager using the resInfo
            try
            {
                if (String.IsNullOrEmpty(ra.BaseName) == false && String.IsNullOrEmpty(ra.AssemblyFullName) == false)
                {
                    rm = new ResourceManager(ra.BaseName, Assembly.ReflectionOnlyLoad(ra.AssemblyFullName));
                }
                else if (String.IsNullOrEmpty(ra.BaseName) == false)
                {
                    rm = new ResourceManager(ra.BaseName, this.EnumType.Assembly);
                }
                else if (String.IsNullOrEmpty(ra.BaseName) == false)
                {
                    rm = new ResourceManager(ra.BaseName, this.EnumType.Assembly);
                }
                else
                {
                    rm = new ResourceManager(this.EnumType);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            // update the display and description string from resource using the resource manager
            foreach (StandardValue sv in m_StatandardValues)
            {
                string keyName = ra.KeyPrefix + sv.Value.ToString() + "_Name";  // display name
                string keyDesc = ra.KeyPrefix + sv.Value.ToString() + "_Desc"; // description
                string dispName = String.Empty;
                string description = String.Empty;
                try
                {
                    dispName = rm.GetString(keyName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (String.IsNullOrEmpty(dispName) == false)
                {
                    sv.DisplayName = dispName;
                }

                try
                {
                    description = rm.GetString(keyDesc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (String.IsNullOrEmpty(description) == false)
                {
                    sv.Description = description;
                }
            }
        }

        private bool IsChildPropertySupported(Scm.ITypeDescriptorContext context)
        {
            ExpandEnumAttribute eea = null;

            if (context != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
                eea = (ExpandEnumAttribute)pd.Attributes.Get(typeof(ExpandEnumAttribute), true);
            }
            else
            {
                //AttributeList al = new AttributeList(Scm.TypeDescriptor.GetAttributes(this.EnumType));
                //eea = (ExpandEnumAttribute)al.FirstOrDefault(a => a is ExpandEnumAttribute);

                eea = (ExpandEnumAttribute)Scm.TypeDescriptor.GetAttributes(this.EnumType).Get(typeof(ExpandableIEnumerationConverter), true);
            }

            if (eea == null)
            {
                return false;
            }
            return eea.Exapand;
        }
    }

    internal class PropertySorter : IComparer
    {
        #region IComparer<PropertyDescriptor> Members

        public int Compare(object x, object y)
        {
            PropertyDescriptor xCpd = x as Dyn.PropertyDescriptor;
            PropertyDescriptor yCpd = y as Dyn.PropertyDescriptor;

            xCpd.AppendCount = 0;
            yCpd.AppendCount = 0;
            int nCompResult = 0;
            switch (m_CategorySortOrder)
            {
                case SortOrder.None:
                    nCompResult = 0;
                    break;

                case SortOrder.ByIdAscending:
                    nCompResult = xCpd.CategoryId.CompareTo(yCpd.CategoryId);
                    break;

                case SortOrder.ByIdDescending:
                    nCompResult = xCpd.CategoryId.CompareTo(yCpd.CategoryId) * -1;
                    break;

                case SortOrder.ByNameAscending:
                    nCompResult = xCpd.Category.CompareTo(yCpd.Category);
                    break;

                case SortOrder.ByNameDescending:
                    nCompResult = xCpd.Category.CompareTo(yCpd.Category) * -1;
                    break;
            }
            if (nCompResult == 0)
            {
                nCompResult = CompareProperty(xCpd, yCpd);
            }
            return nCompResult;
        }

        #endregion IComparer<PropertyDescriptor> Members

        private int CompareProperty(PropertyDescriptor xCpd, PropertyDescriptor yCpd)
        {
            int nCompResult = 0;

            switch (m_PropertySortOrder)
            {
                case SortOrder.None:
                    nCompResult = xCpd._ID.CompareTo(yCpd._ID);
                    break;

                case SortOrder.ByIdAscending:
                    nCompResult = xCpd.PropertyId.CompareTo(yCpd.PropertyId);
                    break;

                case SortOrder.ByIdDescending:
                    nCompResult = xCpd.PropertyId.CompareTo(yCpd.PropertyId) * -1;
                    break;

                case SortOrder.ByNameAscending:
                    nCompResult = xCpd.DisplayName.CompareTo(yCpd.DisplayName);
                    break;

                case SortOrder.ByNameDescending:
                    nCompResult = xCpd.DisplayName.CompareTo(yCpd.DisplayName) * -1;
                    break;
            }
            return nCompResult;
        }

        private SortOrder m_PropertySortOrder = SortOrder.ByNameAscending;

        public SortOrder PropertySortOrder
        {
            get
            {
                return m_PropertySortOrder;
            }
            set
            {
                m_PropertySortOrder = value;
            }
        }

        private SortOrder m_CategorySortOrder = SortOrder.ByNameAscending;

        public SortOrder CategorySortOrder
        {
            get
            {
                return m_CategorySortOrder;
            }
            set
            {
                m_CategorySortOrder = value;
            }
        }
    }

    public class TypeDescriptor : Scm.CustomTypeDescriptor
    {
        private Scm.PropertyDescriptorCollection m_pdc = new Scm.PropertyDescriptorCollection(null, false);
        private object m_instance = null;
        private Hashtable m_hashRM = new Hashtable();

        public TypeDescriptor(Scm.ICustomTypeDescriptor ctd, object instance)
            : base(ctd)
        {
            m_instance = instance;
        }

        public override Scm.PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (m_pdc.Count == 0)
            {
                GetProperties();
            }

            Scm.PropertyDescriptorCollection pdcFilterd = new Scm.PropertyDescriptorCollection(null);
            foreach (Scm.PropertyDescriptor pd in m_pdc)
            {
                if (pd.Attributes.Contains(attributes))
                {
                    pdcFilterd.Add(pd);
                }
            }

            PreProcess(pdcFilterd);
            return pdcFilterd;
        }

        public override Scm.PropertyDescriptorCollection GetProperties()
        {
            if (m_pdc.Count == 0)
            {
                Scm.PropertyDescriptorCollection pdc = null;
                pdc = base.GetProperties();  // this gives us a readonly collection, no good
                foreach (Scm.PropertyDescriptor pd in pdc)
                {
                    if (!(pd is Dyn.PropertyDescriptor))
                    {
                        Dyn.PropertyDescriptor dynPd = null;
                        if (pd.PropertyType.IsEnum)
                        {
                            dynPd = new Dyn.EnumPropertyDescriptor(pd);
                        }
                        else if (pd.PropertyType == typeof(bool))
                        {
                            dynPd = new Dyn.BooleanPropertyDescriptor(pd);
                        }
                        else
                        {
                            dynPd = new Dyn.PropertyDescriptor(pd);
                        }
                        m_pdc.Add(dynPd);
                    }
                    else
                    {
                        m_pdc.Add(pd);
                    }
                }
            }
            return m_pdc;
        }

        private void PreProcess(Scm.PropertyDescriptorCollection pdc)
        {
            if (pdc.Count > 0)
            {
                UpdateStringFromResource(pdc);

                PropertySorter propSorter = new PropertySorter();
                propSorter.CategorySortOrder = this.CategorySortOrder;
                propSorter.PropertySortOrder = this.PropertySortOrder;
                Scm.PropertyDescriptorCollection pdcSorted = pdc.Sort(propSorter);

                UpdateAppendCount(pdcSorted);

                pdc.Clear();
                foreach (Scm.PropertyDescriptor pd in pdcSorted)
                {
                    pdc.Add(pd);
                }
            }
        }

        private void UpdateAppendCount(Scm.PropertyDescriptorCollection pdc)
        {
            if (this.CategorySortOrder == SortOrder.None)
            {
                return;
            }
            int nTabCount = 0;
            if (this.CategorySortOrder == SortOrder.ByNameAscending || this.CategorySortOrder == SortOrder.ByNameDescending)
            {
                string sCatName = null;

                // iterate from last to first
                for (int i = pdc.Count - 1; i >= 0; i--)
                {
                    PropertyDescriptor pd = pdc[i] as Dyn.PropertyDescriptor;
                    if (sCatName == null)
                    {
                        sCatName = pd.Category;
                        pd.AppendCount = nTabCount;
                    }
                    else if (String.Compare(pd.Category, sCatName, true) == 0)
                    {
                        pd.AppendCount = nTabCount;
                    }
                    else
                    {
                        nTabCount++;
                        sCatName = pdc[i].Category;
                        pd.AppendCount = nTabCount;
                    }
                }
            }
            else
            {
                int? nCatID = null;

                // iterate from last to first
                for (int i = pdc.Count - 1; i >= 0; i--)
                {
                    PropertyDescriptor pd = pdc[i] as Dyn.PropertyDescriptor;
                    if (nCatID == null)
                    {
                        nCatID = pd.CategoryId;
                        pd.AppendCount = nTabCount;
                    }
                    if (pd.CategoryId == nCatID)
                    {
                        pd.AppendCount = nTabCount;
                    }
                    else
                    {
                        nTabCount++;
                        nCatID = pd.CategoryId;
                        pd.AppendCount = nTabCount;
                    }
                }
            }
        }

        private void WritePdc(Scm.PropertyDescriptorCollection pdc)
        {
            Console.WriteLine("============================================================================");
            Console.WriteLine("PropSort=" + this.PropertySortOrder.ToString());
            Console.WriteLine("CatSort=" + this.CategorySortOrder.ToString());
            foreach (PropertyDescriptor pd in pdc)
            {
                String s = String.Format("Name={0}; Cat={1}; Tab={2}; CatID={3}; PropID={4}", pd.Name, pd.Category, pd.AppendCount, pd.CategoryId, pd.PropertyId);
                Console.WriteLine(s);
            }
        }

        private SortOrder m_PropertySortOrder = SortOrder.ByIdAscending;

        public SortOrder PropertySortOrder
        {
            get
            {
                return m_PropertySortOrder;
            }
            set
            {
                m_PropertySortOrder = value;
            }
        }

        private Dyn.SortOrder m_CategorySortOrder = Dyn.SortOrder.ByIdAscending;

        public Dyn.SortOrder CategorySortOrder
        {
            get
            {
                return m_CategorySortOrder;
            }
            set
            {
                m_CategorySortOrder = value;
            }
        }

        private void UpdateStringFromResource(Scm.PropertyDescriptorCollection pdc)
        {
            ResourceAttribute ra = (ResourceAttribute)GetAttributes().Get(typeof(ResourceAttribute), true);
            ResourceManager rm = null;
            if (ra == null)
            {
                return;
            }

            try
            {
                if (String.IsNullOrEmpty(ra.BaseName) == false && String.IsNullOrEmpty(ra.AssemblyFullName) == false)
                {
                    rm = new ResourceManager(ra.BaseName, Assembly.ReflectionOnlyLoad(ra.AssemblyFullName));
                }
                else if (String.IsNullOrEmpty(ra.BaseName) == false)
                {
                    rm = new ResourceManager(ra.BaseName, this.m_instance.GetType().Assembly);
                }
                else
                {
                    rm = new ResourceManager(this.m_instance.GetType());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            string sKeyPrefix = (ra != null ? ra.KeyPrefix : "");
            foreach (Dyn.PropertyDescriptor pd in pdc)
            {
                Scm.LocalizableAttribute la = (Scm.LocalizableAttribute)pd.Attributes.Get(typeof(Scm.LocalizableAttribute), true);
                if (la != null && !pd.IsLocalizable)
                {
                    continue;
                }
                if (pd.LCID == CultureInfo.CurrentUICulture.LCID)
                {
                    continue;
                }

                //al = pd.AttributeList;
                string sKey = String.Empty;
                string sResult = String.Empty;

                // first category
                if (!String.IsNullOrEmpty(pd.CategoryResourceKey))
                {
                    sKey = sKeyPrefix + pd.CategoryResourceKey;
                    sResult = String.Empty;

                    try
                    {
                        sResult = rm.GetString(sKey);
                        if (!String.IsNullOrEmpty(sResult))
                        {
                            pd.Attributes.Add(new Scm.CategoryAttribute(sResult), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Key '{0}' does not exist in the resource.", sKey));
                    }
                }

                // now display name
                sKey = sKeyPrefix + pd.Name + "_Name";
                sResult = String.Empty;
                try
                {
                    sResult = rm.GetString(sKey);
                    if (!String.IsNullOrEmpty(sResult))
                    {
                        pd.Attributes.Add(new Scm.DisplayNameAttribute(sResult), typeof(Scm.DisplayNameAttribute));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Key '{0}' does not exist in the resource.", sKey));
                }

                // and now description
                sKey = sKeyPrefix + pd.Name + "_Desc";
                sResult = String.Empty;
                try
                {
                    sResult = rm.GetString(sKey);
                    if (!String.IsNullOrEmpty(sResult))
                    {
                        pd.Attributes.Add(new Scm.DescriptionAttribute(sResult), true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Key '{0}' does not exist in the resource.", sKey));
                }
            }
        }

        private Scm.ISite m_site = null;

        public Scm.ISite GetSite()
        {
            if (m_site == null)
            {
                SimpleSite site = new SimpleSite();
                IPropertyValueUIService service = new PropertyValueUIService();
                service.AddPropertyValueUIHandler(new PropertyValueUIHandler(this.GenericPropertyValueUIHandler));
                site.AddService(service);
                m_site = site;
            }
            return m_site;
        }

        private void GenericPropertyValueUIHandler(Scm.ITypeDescriptorContext context, Scm.PropertyDescriptor propDesc, ArrayList itemList)
        {
            PropertyDescriptor pd = propDesc as Dyn.PropertyDescriptor;
            if (pd != null)
            {
                itemList.AddRange(pd.StateItems as ICollection);
            }
        }

        public void ResetProperties()
        {
            m_pdc.Clear();
            GetProperties();
        }

        static private Hashtable m_HashRef = new Hashtable();

        static public Dyn.TypeDescriptor GetTypeDescriptor(object instance)
        {
            CleanUpRef();
            foreach (DictionaryEntry de in m_HashRef)
            {
                WeakReference wr = de.Key as WeakReference;
                if (wr.IsAlive && instance.Equals(wr.Target))
                {
                    return de.Value as Dyn.TypeDescriptor;
                }
            }
            return null;
        }

        static public bool IntallTypeDescriptor(object instance)
        {
            CleanUpRef();
            foreach (DictionaryEntry de in m_HashRef)
            {
                WeakReference wr = de.Key as WeakReference;
                if (wr.IsAlive && instance.Equals(wr.Target))
                {
                    return false; // because already installed
                }
            }

            //// will have to install the provider and create a new entry in the hash table
            Scm.TypeDescriptionProvider parentProvider = Scm.TypeDescriptor.GetProvider(instance);
            Scm.ICustomTypeDescriptor parentCtd = parentProvider.GetTypeDescriptor(instance);
            Dyn.TypeDescriptor ourCtd = new Dyn.TypeDescriptor(parentCtd, instance);
            Dyn.TypeDescriptionProvider ourProvider = new Dyn.TypeDescriptionProvider(parentProvider, ourCtd);
            Scm.TypeDescriptor.AddProvider(ourProvider, instance);
            WeakReference weakRef = new WeakReference(instance, true);
            m_HashRef.Add(weakRef, ourCtd);
            return true;
        }

        static private void CleanUpRef()
        {
            List<WeakReference> m_DeadList = new List<WeakReference>();
            foreach (WeakReference wr in m_HashRef.Keys)
            {
                if (!wr.IsAlive)
                {
                    m_DeadList.Add(wr);
                }
            }
            foreach (WeakReference wr in m_DeadList)
            {
                m_HashRef.Remove(wr);
            }
        }
    }

    public class StructWrapper : Scm.ICustomTypeDescriptor
    {
        public StructWrapper()
        {
        }

        public StructWrapper(object structObject)
        {
            Debug.Assert(structObject != null);
            Debug.Assert(structObject.GetType().IsValueType);
            m_Struct = structObject;
        }

        private object m_Struct = null;

        [Scm.Browsable(false)]
        public object Struct
        {
            get
            {
                return m_Struct;
            }
            set
            {
                m_Struct = value;
            }
        }

        #region ICustomTypeDescriptor Members

        Scm.AttributeCollection Scm.ICustomTypeDescriptor.GetAttributes()
        {
            return Scm.TypeDescriptor.GetAttributes(this.m_Struct);
        }

        string Scm.ICustomTypeDescriptor.GetClassName()
        {
            return Scm.TypeDescriptor.GetClassName(this.m_Struct);
        }

        string Scm.ICustomTypeDescriptor.GetComponentName()
        {
            return Scm.TypeDescriptor.GetComponentName(this.m_Struct);
        }

        Scm.TypeConverter Scm.ICustomTypeDescriptor.GetConverter()
        {
            return Scm.TypeDescriptor.GetConverter(this.m_Struct);
        }

        Scm.EventDescriptor Scm.ICustomTypeDescriptor.GetDefaultEvent()
        {
            return Scm.TypeDescriptor.GetDefaultEvent(this.m_Struct);
        }

        Scm.PropertyDescriptor Scm.ICustomTypeDescriptor.GetDefaultProperty()
        {
            return Scm.TypeDescriptor.GetDefaultProperty(this.m_Struct);
        }

        object Scm.ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return Scm.TypeDescriptor.GetEditor(this.m_Struct, editorBaseType);
        }

        Scm.EventDescriptorCollection Scm.ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return Scm.TypeDescriptor.GetEvents(this.m_Struct, attributes);
        }

        Scm.EventDescriptorCollection Scm.ICustomTypeDescriptor.GetEvents()
        {
            return Scm.TypeDescriptor.GetEvents(this.m_Struct);
        }

        Scm.PropertyDescriptorCollection Scm.ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return Scm.TypeDescriptor.GetProperties(this.m_Struct, attributes);
        }

        Scm.PropertyDescriptorCollection Scm.ICustomTypeDescriptor.GetProperties()
        {
            return Scm.TypeDescriptor.GetProperties(this.m_Struct);
        }

        object Scm.ICustomTypeDescriptor.GetPropertyOwner(Scm.PropertyDescriptor pd)
        {
            return m_Struct;
        }

        #endregion ICustomTypeDescriptor Members
    }

    internal class TypeDescriptionProvider : Scm.TypeDescriptionProvider
    {
        private Scm.TypeDescriptionProvider m_parent = null;
        private Scm.ICustomTypeDescriptor m_ctd = null;

        public TypeDescriptionProvider()
            : base()
        {
        }

        public TypeDescriptionProvider(Scm.TypeDescriptionProvider parent)
            : base(parent)
        {
            m_parent = parent;
        }

        public TypeDescriptionProvider(Scm.TypeDescriptionProvider parent, Scm.ICustomTypeDescriptor ctd)
            : base(parent)
        {
            m_parent = parent;
            m_ctd = ctd;
        }

        public override Scm.ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return m_ctd;
        }
    }

    public class PropertyDescriptor : Scm.PropertyDescriptor
    {
        private Type m_compType = null;
        private Type m_PropType = null;
        private Scm.PropertyDescriptor m_pd = null;
        private List<PropertyValueUIItem> m_colUIItem = new List<PropertyValueUIItem>();
        static private readonly char m_HiddenChar = '\t';
        static private ulong m_COUNT = 1;
        internal readonly ulong _ID;

        public PropertyDescriptor(Type componentType, string sName, Type propType, object value, params Attribute[] attributes)
            : base(sName, attributes)
        {
            _ID = m_COUNT++;
            m_compType = componentType;
            m_value = value;
            m_PropType = propType;
        }

        public PropertyDescriptor(Scm.PropertyDescriptor pd)
            : base(pd)
        {
            _ID = m_COUNT++;
            m_pd = pd;
        }

        public override Type ComponentType
        {
            get
            {
                if (m_pd != null)
                {
                    return m_pd.ComponentType;
                }
                return m_compType;
            }
        }

        public override Type PropertyType
        {
            get
            {
                if (m_pd != null)
                {
                    return m_pd.PropertyType;
                }
                return m_PropType;
            }
        }

        /// <summary>
        /// Must override abstract properties.
        /// </summary>
        ///

        public override bool IsReadOnly
        {
            get
            {
                Scm.ReadOnlyAttribute attr = (Scm.ReadOnlyAttribute)Attributes.Get(typeof(Scm.ReadOnlyAttribute), true);
                if (attr != null)
                {
                    return attr.IsReadOnly;
                }
                return false;
            }
        }

        public override string Category
        {
            get
            {
                string sOut = base.Category;

                Scm.CategoryAttribute attr = (Scm.CategoryAttribute)Attributes.Get(typeof(Scm.CategoryAttribute), true);
                if (attr != null)
                {
                    sOut = attr.Category;
                }
                sOut = sOut.PadLeft(sOut.Length + m_AppendCount, m_HiddenChar);
                return sOut;
            }
        }

        private Hashtable m_hashEditor = new Hashtable();

        public override object GetEditor(Type editorBaseType)
        {
            if (m_hashEditor[editorBaseType] != null)
            {
                return m_hashEditor[editorBaseType];
            }

            string sEditorBaseType = editorBaseType.AssemblyQualifiedName;
            foreach (Attribute attr in Attributes)
            {
                if (!(attr is Scm.EditorAttribute))
                {
                    continue;
                }

                Scm.EditorAttribute editorAttr = attr as Scm.EditorAttribute;
                if (String.Compare(editorAttr.EditorBaseTypeName, sEditorBaseType, true) == 0)
                {
                    Type editorType = Type.GetType(editorAttr.EditorTypeName);
                    ConstructorInfo ci = editorType.GetConstructor(System.Type.EmptyTypes);
                    if (ci != null)
                    {
                        object editor = Activator.CreateInstance(editorType);
                        m_hashEditor[editorBaseType] = editor;
                    }
                    else
                    {
                        object editor = Activator.CreateInstance(editorType, this.PropertyType);
                        m_hashEditor[editorBaseType] = editor;
                    }
                }
            }
            return m_hashEditor[editorBaseType];
        }

        public override Scm.TypeConverter Converter
        {
            get
            {
                Scm.TypeConverterAttribute attr = (Scm.TypeConverterAttribute)Attributes.Get(typeof(Scm.TypeConverterAttribute), true);
                if (attr == null)
                {
                    return base.Converter;
                }
                Type tcType = Type.GetType(attr.ConverterTypeName);
                Scm.TypeConverter tc = (Scm.TypeConverter)Activator.CreateInstance(tcType);
                return tc;
            }
        }

        internal object DefaultValue
        {
            get
            {
                Scm.DefaultValueAttribute attr = (Scm.DefaultValueAttribute)Attributes.Get(typeof(Scm.DefaultValueAttribute), true);
                if (attr != null)
                {
                    return attr.Value;
                }
                return null;
            }
            set
            {
                Attributes.Add(new Scm.DefaultValueAttribute(value), true);
            }
        }

        internal int PropertyId
        {
            get
            {
                SortIDAttribute rsa = (SortIDAttribute)Attributes.Get(typeof(SortIDAttribute), true);
                if (rsa != null)
                {
                    return rsa.PropertyOrder;
                }
                return 0;
            }
            set
            {
                SortIDAttribute rsa = (SortIDAttribute)Attributes.Get(typeof(SortIDAttribute), true);
                if (rsa == null)
                {
                    rsa = new SortIDAttribute();
                    Attributes.Add(rsa);
                }
                rsa.PropertyOrder = value;
            }
        }

        internal int CategoryId
        {
            get
            {
                SortIDAttribute rsa = (SortIDAttribute)Attributes.Get(typeof(SortIDAttribute), true);

                if (rsa != null)
                {
                    return rsa.CategoryOrder;
                }
                return 0;
            }
            set
            {
                SortIDAttribute rsa = (SortIDAttribute)Attributes.Get(typeof(SortIDAttribute), true);
                if (rsa == null)
                {
                    rsa = new SortIDAttribute();
                    Attributes.Add(rsa);
                }
                rsa.CategoryOrder = value;
            }
        }

        internal string CategoryResourceKey
        {
            get
            {
                CategoryResourceKeyAttribute rsa = (CategoryResourceKeyAttribute)Attributes.Get(typeof(CategoryResourceKeyAttribute), true);
                if (rsa != null)
                {
                    return rsa.ResourceKey;
                }
                return String.Empty;
            }
            set
            {
                CategoryResourceKeyAttribute rsa = (CategoryResourceKeyAttribute)Attributes.Get(typeof(CategoryResourceKeyAttribute), true);
                if (rsa == null)
                {
                    rsa = new CategoryResourceKeyAttribute();
                    Attributes.Add(rsa);
                }
                rsa.ResourceKey = value;
            }
        }

        private int m_AppendCount = 0;

        internal int AppendCount
        {
            get
            {
                return m_AppendCount;
            }
            set
            {
                m_AppendCount = value;
            }
        }

        public override bool DesignTimeOnly
        {
            get
            {
                return false;
            }
        }

        private object m_value = null;

        public override object GetValue(object component)
        {
            Debug.Assert(component != null);
            Debug.Assert(component.GetType() == ComponentType);

            if (m_pd != null)
            {
                return m_pd.GetValue(component);
            }
            return m_value;
        }

        public override void SetValue(object component, object value)
        {
            Debug.Assert(component != null);
            Debug.Assert(component.GetType() == ComponentType);

            Debug.Assert(value != null);
            Debug.Assert(value.GetType() == PropertyType);

            m_value = value;

            if (m_pd != null)
            {
                m_pd.SetValue(component, m_value);
            }
            base.OnValueChanged(component, new EventArgs());
        }

        /// <summary>
        /// Abstract base members
        /// </summary>
        public override void ResetValue(object component)
        {
            Debug.Assert(component != null);
            Debug.Assert(component.GetType() == ComponentType);

            if (m_pd != null)
            {
                m_pd.ResetValue(component);
            }
            else
            {
                SetValue(component, this.DefaultValue);
            }
        }

        public override bool CanResetValue(object component)
        {
            Debug.Assert(component != null);
            Debug.Assert(component.GetType() == ComponentType);

            if (this.DefaultValue == null)
            {
                return false;
            }

            object value = GetValue(component);
            bool bOk = !value.Equals(this.DefaultValue);
            return bOk;
        }

        public override bool ShouldSerializeValue(object component)
        {
            Debug.Assert(component != null);
            Debug.Assert(component.GetType() == ComponentType);

            if (this.DefaultValue == null)
            {
                return true;
            }
            bool bOk = CanResetValue(component);
            return bOk;
        }

        public ICollection<PropertyValueUIItem> StateItems
        {
            get
            {
                return m_colUIItem;
            }
        }

        protected List<StandardValue> m_StatandardValues = new List<StandardValue>();

        public virtual IList<StandardValue> StandardValues
        {
            get
            {
                return m_StatandardValues;
            }
        }

        private Image m_ValueImage = null;

        public Image ValueImage
        {
            get
            {
                return m_ValueImage;
            }
            set
            {
                m_ValueImage = value;
            }
        }

        internal int LCID
        {
            get;
            set;
        }
    }

    public class EnumPropertyDescriptor : PropertyDescriptor
    {
        public EnumPropertyDescriptor(Scm.PropertyDescriptor pd)
            : base(pd)
        {
            Debug.Assert(pd.PropertyType.IsEnum);

            base.m_StatandardValues.Clear();
            StandardValue[] svaArr = EnumUtil.GetStandardValues(this.PropertyType);
            base.m_StatandardValues.AddRange(svaArr);
        }

        public override IList<StandardValue> StandardValues
        {
            get
            {
                return m_StatandardValues.AsReadOnly();
            }
        }

        public override Scm.TypeConverter Converter
        {
            get
            {
                Scm.TypeConverterAttribute attr = (Scm.TypeConverterAttribute)this.Attributes.Get(typeof(Scm.TypeConverterAttribute), true);
                if (attr == null)
                {
                    return base.Converter;
                }
                Type tcType = Type.GetType(attr.ConverterTypeName);
                Scm.TypeConverter tc = (Scm.TypeConverter)Activator.CreateInstance(tcType, this.PropertyType);
                return tc;
            }
        }
    }

    public class EnumChildPropertyDescriptor : Dyn.BooleanPropertyDescriptor
    {
        private Scm.ITypeDescriptorContext m_context = null;
        private object m_enumField = null;  // represent one of the enum field

        public EnumChildPropertyDescriptor(Scm.ITypeDescriptorContext context, string sName, object enumFieldvalue, params Attribute[] attributes)
            : base(enumFieldvalue.GetType(), sName, false, attributes)
        {
            m_context = context;
            m_enumField = enumFieldvalue;
        }

        public override void SetValue(object component, object value)
        {
            Debug.Assert(component != null);
            Debug.Assert(component.GetType() == ComponentType);

            Debug.Assert(value != null);
            Debug.Assert(value.GetType() == PropertyType);

            object enumInstance = m_context.PropertyDescriptor.GetValue(m_context.Instance);
            bool bModified = false;
            if ((bool)value)
            {
                bModified = EnumUtil.TurnOnBits(ref enumInstance, m_enumField);
            }
            else
            {
                bModified = EnumUtil.TurnOffBits(ref enumInstance, m_enumField);
            }

            if (bModified)
            {
                FieldInfo fi = component.GetType().GetField("value__", BindingFlags.Instance | BindingFlags.Public);
                fi.SetValue(component, enumInstance);
                m_context.PropertyDescriptor.SetValue(m_context.Instance, component);
            }
        }

        public override object GetValue(object component)
        {
            Debug.Assert(component != null);
            Debug.Assert(component.GetType() == ComponentType);

            return EnumUtil.IsBitsOn(component, m_enumField);
        }
    }

    internal class EnumUtil
    {
        static public bool IsBitsOn(object enumInstance, object bits)
        {
            Debug.Assert(enumInstance != null);
            Debug.Assert(enumInstance.GetType().IsEnum);
            Debug.Assert(bits != null);
            Debug.Assert(bits.GetType().IsEnum);
            Debug.Assert(enumInstance.GetType() == bits.GetType());

            if (!IsFlag(enumInstance.GetType()))
            {
                return (enumInstance.Equals(bits));
            }

            if (IsZeroDefinend(enumInstance.GetType()))  // special case
            {
                bool isInstanceZero = IsZero(enumInstance);
                bool isBitZero = IsZero(bits);

                if (isInstanceZero && isBitZero)
                {
                    return true;
                }
                else if (isInstanceZero && !isBitZero)
                {
                    return false;
                }
                else if (!isInstanceZero && isBitZero)
                {
                    return false;
                }
            }

            // otherwise (!valueIsZero && !bitsIsZero)

            Type enumDataType = Enum.GetUnderlyingType(enumInstance.GetType());
            if (enumDataType == typeof(Int16))
            {
                Int16 _value = Convert.ToInt16(enumInstance);
                Int16 _bits = Convert.ToInt16(bits);
                return ((_value & _bits) == _bits);
            }
            else if (enumDataType == typeof(UInt16))
            {
                UInt16 _value = Convert.ToUInt16(enumInstance);
                UInt16 _bits = Convert.ToUInt16(bits);
                return ((_value & _bits) == _bits);
            }
            else if (enumDataType == typeof(Int32))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                return ((_value & _bits) == _bits);
            }
            else if (enumDataType == typeof(UInt32))
            {
                UInt32 _value = Convert.ToUInt32(enumInstance);
                UInt32 _bits = Convert.ToUInt32(bits);
                return ((_value & _bits) == _bits);
            }
            else if (enumDataType == typeof(Int64))
            {
                Int64 _value = Convert.ToInt64(enumInstance);
                Int64 _bits = Convert.ToInt64(bits);
                return ((_value & _bits) == _bits);
            }
            else if (enumDataType == typeof(UInt64))
            {
                UInt64 _value = Convert.ToUInt64(enumInstance);
                UInt64 _bits = Convert.ToUInt64(bits);
                return ((_value & _bits) == _bits);
            }
            else if (enumDataType == typeof(SByte))
            {
                SByte _value = Convert.ToSByte(enumInstance);
                SByte _bits = Convert.ToSByte(bits);
                return ((_value & _bits) == _bits);
            }
            else if (enumDataType == typeof(Byte))
            {
                Byte _value = Convert.ToByte(enumInstance);
                Byte _bits = Convert.ToByte(bits);
                return ((_value & _bits) == _bits);
            }
            return false;
        }

        static public bool TurnOffBits(ref object enumInstance, object bits)
        {
            Debug.Assert(enumInstance != null);
            Debug.Assert(enumInstance.GetType().IsEnum);
            Debug.Assert(bits != null);
            Debug.Assert(bits.GetType().IsEnum);
            Debug.Assert(enumInstance.GetType() == bits.GetType());

            if (!IsFlag(enumInstance.GetType()))
            {
                return false;
            }

            if (!IsBitsOn(enumInstance, bits)) // already turned off
            {
                return false;
            }
            if (IsZeroDefinend(enumInstance.GetType()))  // special case
            {
                bool isInstanceZero = IsZero(enumInstance);
                bool isBitZero = IsZero(bits);

                if (isInstanceZero && isBitZero)
                {
                    return false;
                }
                else if (isInstanceZero && !isBitZero)
                {
                    return false;
                }
                else if (!isInstanceZero && isBitZero)
                {
                    return false;
                }
            }
            Type enumType = enumInstance.GetType();
            Type enumDataType = Enum.GetUnderlyingType(enumInstance.GetType());

            if (enumDataType == typeof(Int16))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                _value &= ~(_bits);
                enumInstance = _value;
            }
            else if (enumDataType == typeof(UInt16))
            {
                UInt32 _value = Convert.ToUInt32(enumInstance);
                UInt32 _bits = Convert.ToUInt32(bits);
                _value &= ~(_bits);
                enumInstance = _value;
            }
            else if (enumDataType == typeof(Int32))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                _value &= ~(_bits);
                enumInstance = _value;
            }
            else if (enumDataType == typeof(UInt32))
            {
                UInt32 _value = Convert.ToUInt32(enumInstance);
                UInt32 _bits = Convert.ToUInt32(bits);
                _value &= ~(_bits);
                enumInstance = _value;
            }
            else if (enumDataType == typeof(Int64))
            {
                Int64 _value = Convert.ToInt64(enumInstance);
                Int64 _bits = Convert.ToInt64(bits);
                _value &= ~(_bits);
                enumInstance = _value;
            }
            else if (enumDataType == typeof(UInt64))
            {
                UInt64 _value = Convert.ToUInt64(enumInstance);
                UInt64 _bits = Convert.ToUInt64(bits);
                _value &= ~(_bits);
                enumInstance = _value;
            }
            else if (enumDataType == typeof(SByte))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                _value &= ~(_bits);
                enumInstance = _value;
            }
            else if (enumDataType == typeof(Byte))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                _value &= ~(_bits);
                enumInstance = _value;
            }

            enumInstance = Enum.ToObject(enumType, enumInstance);

            return true;
        }

        static public bool TurnOnBits(ref object enumInstance, object bits)
        {
            Debug.Assert(enumInstance != null);
            Debug.Assert(enumInstance.GetType().IsEnum);
            Debug.Assert(bits != null);
            Debug.Assert(bits.GetType().IsEnum);
            Debug.Assert(enumInstance.GetType() == bits.GetType());

            if (!IsFlag(enumInstance.GetType()))
            {
                if (!enumInstance.Equals(bits))
                {
                    enumInstance = bits;
                    return true;
                }
                return false;
            }

            if (IsBitsOn(enumInstance, bits)) // already turned on
            {
                return false;
            }

            if (IsZeroDefinend(enumInstance.GetType()))  // special case
            {
                bool isInstanceZero = IsZero(enumInstance);
                bool isBitZero = IsZero(bits);

                if (isInstanceZero && isBitZero)
                {
                    return false;
                }
                else if (isInstanceZero && !isBitZero)
                {
                    enumInstance = bits;
                    return true;
                }
                else if (!isInstanceZero && isBitZero)
                {
                    enumInstance = bits;
                    return true;
                }
            }

            Type enumType = enumInstance.GetType();
            Type enumDataType = Enum.GetUnderlyingType(enumInstance.GetType());

            if (enumDataType == typeof(Int16))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                _value |= _bits;
                enumInstance = _value;
            }
            else if (enumDataType == typeof(UInt16))
            {
                UInt32 _value = Convert.ToUInt32(enumInstance);
                UInt32 _bits = Convert.ToUInt32(bits);
                _value |= _bits;
                enumInstance = _value;
            }
            else if (enumDataType == typeof(Int32))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                _value |= _bits;
                enumInstance = _value;
            }
            else if (enumDataType == typeof(UInt32))
            {
                UInt32 _value = Convert.ToUInt32(enumInstance);
                UInt32 _bits = Convert.ToUInt32(bits);
                _value |= _bits;
                enumInstance = _value;
            }
            else if (enumDataType == typeof(Int64))
            {
                Int64 _value = Convert.ToInt64(enumInstance);
                Int64 _bits = Convert.ToInt64(bits);
                _value |= _bits;
                enumInstance = _value;
            }
            else if (enumDataType == typeof(UInt64))
            {
                UInt64 _value = Convert.ToUInt64(enumInstance);
                UInt64 _bits = Convert.ToUInt64(bits);
                _value |= _bits;
                enumInstance = _value;
            }
            else if (enumDataType == typeof(SByte))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                _value |= _bits;
                enumInstance = _value;
            }
            else if (enumDataType == typeof(Byte))
            {
                Int32 _value = Convert.ToInt32(enumInstance);
                Int32 _bits = Convert.ToInt32(bits);
                _value |= _bits;
                enumInstance = _value;
            }
            enumInstance = Enum.ToObject(enumType, enumInstance);
            return true;
        }

        static public bool IsZeroDefinend(Type enumType)
        {
            Debug.Assert(enumType != null);
            Debug.Assert(enumType.IsEnum);
            Debug.Assert(IsFlag(enumType));

            Type enumDataType = Enum.GetUnderlyingType(enumType);

            if (enumDataType == typeof(Int16))
            {
                Int16 zero = 0;
                return Enum.IsDefined(enumType, zero);
            }
            else if (enumDataType == typeof(UInt16))
            {
                UInt16 zero = 0;
                return Enum.IsDefined(enumType, zero);
            }
            else if (enumDataType == typeof(Int32))
            {
                Int32 zero = 0;
                return Enum.IsDefined(enumType, zero);
            }
            else if (enumDataType == typeof(UInt32))
            {
                UInt32 zero = 0;
                return Enum.IsDefined(enumType, zero);
            }
            else if (enumDataType == typeof(Int64))
            {
                Int64 zero = 0;
                return Enum.IsDefined(enumType, zero);
            }
            else if (enumDataType == typeof(UInt64))
            {
                UInt64 zero = 0;
                return Enum.IsDefined(enumType, zero);
            }
            else if (enumDataType == typeof(SByte))
            {
                SByte zero = 0;
                return Enum.IsDefined(enumType, zero);
            }
            else if (enumDataType == typeof(Byte))
            {
                Byte zero = 0;
                return Enum.IsDefined(enumType, zero);
            }

            return false;
        }

        static public bool IsZero(object enumInstance)
        {
            Debug.Assert(enumInstance != null);
            Debug.Assert(enumInstance.GetType().IsEnum);

            if (!IsZeroDefinend(enumInstance.GetType()))
            {
                return false;
            }

            Type enumDataType = Enum.GetUnderlyingType(enumInstance.GetType());

            if (enumDataType == typeof(Int16))
            {
                Int16 zero = 0;
                object objZero = Enum.ToObject(enumInstance.GetType(), zero);
                return objZero.Equals(enumInstance);
            }
            else if (enumDataType == typeof(UInt16))
            {
                UInt16 zero = 0;
                object objZero = Enum.ToObject(enumInstance.GetType(), zero);
                return objZero.Equals(enumInstance);
            }
            else if (enumDataType == typeof(Int32))
            {
                Int32 zero = 0;
                object objZero = Enum.ToObject(enumInstance.GetType(), zero);
                return objZero.Equals(enumInstance);
            }
            else if (enumDataType == typeof(UInt32))
            {
                UInt32 zero = 0;
                object objZero = Enum.ToObject(enumInstance.GetType(), zero);
                return objZero.Equals(enumInstance);
            }
            else if (enumDataType == typeof(Int64))
            {
                Int64 zero = 0;
                object objZero = Enum.ToObject(enumInstance.GetType(), zero);
                return objZero.Equals(enumInstance);
            }
            else if (enumDataType == typeof(UInt64))
            {
                UInt64 zero = 0;
                object objZero = Enum.ToObject(enumInstance.GetType(), zero);
                return objZero.Equals(enumInstance);
            }
            else if (enumDataType == typeof(SByte))
            {
                SByte zero = 0;
                object objZero = Enum.ToObject(enumInstance.GetType(), zero);
                return objZero.Equals(enumInstance);
            }
            else if (enumDataType == typeof(Byte))
            {
                Byte zero = 0;
                object objZero = Enum.ToObject(enumInstance.GetType(), zero);
                return objZero.Equals(enumInstance);
            }

            return false;
        }

        static public bool IsFlag(Type enumType)
        {
            Debug.Assert(enumType != null);
            Debug.Assert(enumType.IsEnum);
            return (enumType.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0);
        }

        static public StandardValue[] GetStandardValues(object enumInstance)
        {
            Debug.Assert(enumInstance != null);
            Debug.Assert(enumInstance.GetType().IsEnum);
            return GetStandardValues(enumInstance.GetType(), BindingFlags.Public | BindingFlags.Instance);
        }

        static public StandardValue[] GetStandardValues(Type enumType)
        {
            Debug.Assert(enumType != null);
            Debug.Assert(enumType.IsEnum);

            return GetStandardValues(enumType, BindingFlags.Public | BindingFlags.Static);
        }

        static private StandardValue[] GetStandardValues(Type enumType, BindingFlags flags)
        {
            ArrayList arrAttr = new ArrayList();
            FieldInfo[] fields = enumType.GetFields(flags);

            foreach (FieldInfo fi in fields)
            {
                StandardValue sv = new StandardValue(Enum.ToObject(enumType, fi.GetValue(null)));

                DisplayNameAttribute[] dna = fi.GetCustomAttributes(typeof(DisplayNameAttribute), false) as DisplayNameAttribute[];
                if (dna != null && dna.Length > 0)
                {
                    sv.DisplayName = dna[0].DisplayName;
                }

                Scm.DescriptionAttribute[] da = fi.GetCustomAttributes(typeof(Scm.DescriptionAttribute), false) as Scm.DescriptionAttribute[];
                if (da != null && da.Length > 0)
                {
                    sv.Description = da[0].Description;
                }

                Scm.BrowsableAttribute[] ba = fi.GetCustomAttributes(typeof(Scm.BrowsableAttribute), false) as Scm.BrowsableAttribute[];
                if (ba != null && ba.Length > 0)
                {
                    sv.Visible = ba[0].Browsable;
                }

                Scm.ReadOnlyAttribute[] roa = fi.GetCustomAttributes(typeof(Scm.ReadOnlyAttribute), false) as Scm.ReadOnlyAttribute[];
                if (roa != null && roa.Length > 0)
                {
                    sv.Enabled = !roa[0].IsReadOnly;
                }
                arrAttr.Add(sv);
            }
            StandardValue[] retAttr = arrAttr.ToArray(typeof(StandardValue)) as StandardValue[];
            return retAttr;
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class DisplayNameAttribute : Scm.DisplayNameAttribute
    {
        public DisplayNameAttribute()
            : base()
        {
        }

        public DisplayNameAttribute(string displayName)
            : base(displayName)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CategoryResourceKeyAttribute : System.Attribute
    {
        public CategoryResourceKeyAttribute()
            : base()
        {
        }

        public CategoryResourceKeyAttribute(string resourceKey)
            : base()
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey
        {
            get;
            set;
        }
    }

    public class BooleanConverter : Scm.BooleanConverter
    {
        static private ulong m_COUNT = 1;
        internal readonly ulong _ID;

        private List<StandardValue> m_StatandardValues = new List<StandardValue>();

        private int m_LCID = -1;

        public BooleanConverter()
            : base()
        {
            _ID = m_COUNT++;
        }

        public override object ConvertFrom(Scm.ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object retOjb = null;
            if (value == null)
            {
                retOjb = base.ConvertFrom(context, culture, value);
                return retOjb;
            }

            if (value is string)
            {
                UpdateStringFromResource(context);

                string sInpuValue = value as string;
                sInpuValue = sInpuValue.Trim();
                foreach (StandardValue sv in m_StatandardValues)
                {
                    if (String.Compare(sv.Value.ToString(), sInpuValue, true) == 0 ||
                        String.Compare(sv.DisplayName, sInpuValue, true) == 0)
                    {
                        return sv.Value;
                    }
                }
            }
            retOjb = base.ConvertFrom(context, culture, value);
            return retOjb;
        }

        public override bool CanConvertTo(Scm.ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(StandardValue))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(Scm.ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object retObj;
            if (value == null)
            {
                retObj = base.ConvertTo(context, culture, value, destinationType);
                return retObj;
            }

            UpdateStringFromResource(context);

            if (value is string)
            {
                if (destinationType == typeof(string))
                {
                    return value;
                }
                else if (destinationType == typeof(StandardValue))
                {
                    foreach (StandardValue sv in m_StatandardValues)
                    {
                        if (String.Compare(value.ToString(), sv.DisplayName, true, culture) == 0 ||
                            String.Compare(value.ToString(), sv.Value.ToString(), true, culture) == 0)
                        {
                            return sv;
                        }
                    }
                }
            }
            else if (value.GetType() == typeof(bool))
            {
                if (destinationType == typeof(string))
                {
                    foreach (StandardValue sv in m_StatandardValues)
                    {
                        if (sv.Value.Equals(value))
                        {
                            return sv.DisplayName;
                        }
                    }
                }
                else if (destinationType == typeof(StandardValue))
                {
                    foreach (StandardValue sv in m_StatandardValues)
                    {
                        if (sv.Value.Equals(value))
                        {
                            return sv;
                        }
                    }
                }
            }
            retObj = base.ConvertTo(context, culture, value, destinationType);
            return retObj;
        }

        public override bool GetStandardValuesSupported(Scm.ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                return (context.PropertyDescriptor as Dyn.PropertyDescriptor).StandardValues.Count > 0;
            }
            return base.GetStandardValuesSupported(context);
        }

        public override bool GetStandardValuesExclusive(Scm.ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null)
            {
                ExclusiveStandardValuesAttribute psfa = (ExclusiveStandardValuesAttribute)context.PropertyDescriptor.Attributes.Get(typeof(ExclusiveStandardValuesAttribute), true);
                if (psfa != null)
                {
                    return psfa.Exclusive;
                }
            }
            return base.GetStandardValuesExclusive(context);
        }

        private void UpdateStringFromResource(Scm.ITypeDescriptorContext context)
        {
            if (m_StatandardValues.Count > 0 && m_LCID == CultureInfo.CurrentUICulture.LCID)
            {
                return;
            }
            m_LCID = CultureInfo.CurrentUICulture.LCID;
            m_StatandardValues.Clear();
            if (context != null && context.PropertyDescriptor != null && context.PropertyDescriptor is Dyn.PropertyDescriptor)
            {
                Dyn.PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
                m_StatandardValues.AddRange(pd.StandardValues);
            }
            else
            {
                m_StatandardValues = new List<StandardValue>();
                m_StatandardValues.Add(new StandardValue(true));
                m_StatandardValues.Add(new StandardValue(false));
            }
            ResourceAttribute ra = null;

            if (context != null && context.PropertyDescriptor != null)
            {
                ra = (ResourceAttribute)context.PropertyDescriptor.Attributes.Get(typeof(ResourceAttribute));
            }
            if (ra == null)
            {
                ra = (ResourceAttribute)Scm.TypeDescriptor.GetAttributes(typeof(bool)).Get(typeof(ResourceAttribute));
            }

            if (ra == null)
            {
                return;
            }

            ResourceManager rm = null;

            // construct the resource manager using the resInfo
            try
            {
                if (String.IsNullOrEmpty(ra.BaseName) == false && String.IsNullOrEmpty(ra.AssemblyFullName) == false)
                {
                    rm = new ResourceManager(ra.BaseName, Assembly.ReflectionOnlyLoad(ra.AssemblyFullName));
                }
                else if (String.IsNullOrEmpty(ra.BaseName) == false)
                {
                    rm = new ResourceManager(ra.BaseName, typeof(bool).Assembly);
                }
                else if (String.IsNullOrEmpty(ra.BaseName) == false)
                {
                    rm = new ResourceManager(ra.BaseName, typeof(bool).Assembly);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            if (rm == null)
            {
                return;
            }

            // update the display and description string from resource using the resource manager
            foreach (StandardValue sv in m_StatandardValues)
            {
                string keyName = ra.KeyPrefix + sv.Value.ToString() + "_Name";  // display name
                string keyDesc = ra.KeyPrefix + sv.Value.ToString() + "_Desc"; // description
                string dispName = String.Empty;
                string description = String.Empty;
                try
                {
                    dispName = rm.GetString(keyName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (String.IsNullOrEmpty(dispName) == false)
                {
                    sv.DisplayName = dispName;
                }

                try
                {
                    description = rm.GetString(keyDesc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (String.IsNullOrEmpty(description) == false)
                {
                    sv.Description = description;
                }
            }
        }
    }

    public class BooleanPropertyDescriptor : Dyn.PropertyDescriptor
    {
        public BooleanPropertyDescriptor(Scm.PropertyDescriptor pd)
            : base(pd)
        {
            Debug.Assert(pd.PropertyType == typeof(bool));

            base.m_StatandardValues.Clear();
            base.m_StatandardValues.Add(new StandardValue(true));
            base.m_StatandardValues.Add(new StandardValue(false));
        }

        public BooleanPropertyDescriptor(Type componentType, string sName, bool value, params Attribute[] attributes)
            : base(componentType, sName, typeof(bool), value, attributes)
        {
            base.m_StatandardValues.Clear();
            base.m_StatandardValues.Add(new StandardValue(true));
            base.m_StatandardValues.Add(new StandardValue(false));
        }

        public override IList<StandardValue> StandardValues
        {
            get
            {
                return base.m_StatandardValues.AsReadOnly();
            }
        }
    }
}