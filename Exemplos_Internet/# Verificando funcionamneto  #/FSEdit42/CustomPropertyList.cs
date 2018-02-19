using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace SAS.Tasks.Examples.FSEdit
{
    public class CustomPropertyTable : CustomPropertyList
    {
        private Hashtable customPropertyValues;

        public CustomPropertyTable()
        {
            customPropertyValues = new Hashtable();
        }

        public object this[string key]
        {
            get { return customPropertyValues[key]; }
            set { customPropertyValues[key] = value; }
        }

        protected override void OnGetValue(CustomPropertyEventArgs e)
        {
            e.Value = customPropertyValues[e.CustomProperty.Name];
            base.OnGetValue(e);
        }

        protected override void OnSetValue(CustomPropertyEventArgs e)
        {
            customPropertyValues[e.CustomProperty.Name] = e.Value;
            base.OnSetValue(e);
        }
    }

    public class CustomProperty
    {
        private string name;
        private string type;
        private object defaultValue;

        public CustomProperty(string name, string type, object defaultValue)
        {
            this.name = name;
            this.type = type;
            this.defaultValue = defaultValue;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
    }

    public delegate void CustomPropertyEventHandler(object sender, CustomPropertyEventArgs e);

    public class CustomPropertyEventArgs : EventArgs
    {
        private CustomProperty customProperty;
        private object customProertyValue;

        public CustomPropertyEventArgs(CustomProperty customProperty, object customProertyValue)
        {
            this.customProperty = customProperty;
            this.customProertyValue = customProertyValue;
        }

        public CustomProperty CustomProperty
        {
            get { return customProperty; }
        }

        public object Value
        {
            get { return customProertyValue; }
            set { customProertyValue = value; }
        }
    }

    public class CustomPropertyList : ICustomTypeDescriptor
    {
        private string defaultProperty;
        private CustomPropertyCollection customProperties;

        public CustomPropertyList()
        {
            defaultProperty = null;
            customProperties = new CustomPropertyCollection();
        }

        public class CustomPropertyCollection : IList
        {
            private ArrayList list;

            public CustomPropertyCollection()
            {
                list = new ArrayList();
            }

            public int Count
            {
                get { return list.Count; }
            }

            public bool IsFixedSize
            {
                get { return false; }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public bool IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return null; }
            }

            public CustomProperty this[int index]
            {
                get { return (CustomProperty)list[index]; }
                set { list[index] = value; }
            }

            public int Add(CustomProperty value)
            {
                int index = list.Add(value);

                return index;
            }

            public void AddRange(CustomProperty[] array)
            {
                list.AddRange(array);
            }

            public void Clear()
            {
                list.Clear();
            }

            public bool Contains(CustomProperty item)
            {
                return list.Contains(item);
            }

            public bool Contains(string name)
            {
                foreach (CustomProperty spec in list)
                    if (spec.Name == name)
                        return true;

                return false;
            }

            public void CopyTo(CustomProperty[] array)
            {
                list.CopyTo(array);
            }

            public void CopyTo(CustomProperty[] array, int index)
            {
                list.CopyTo(array, index);
            }

            public IEnumerator GetEnumerator()
            {
                return list.GetEnumerator();
            }

            public int IndexOf(CustomProperty value)
            {
                return list.IndexOf(value);
            }

            public int IndexOf(string name)
            {
                int i = 0;

                foreach (CustomProperty spec in list)
                {
                    if (spec.Name == name)
                        return i;

                    i++;
                }

                return -1;
            }

            public void Insert(int index, CustomProperty value)
            {
                list.Insert(index, value);
            }

            public void Remove(CustomProperty obj)
            {
                list.Remove(obj);
            }

            public void Remove(string name)
            {
                int index = IndexOf(name);
                RemoveAt(index);
            }

            public void RemoveAt(int index)
            {
                list.RemoveAt(index);
            }

            public CustomProperty[] ToArray()
            {
                return (CustomProperty[])list.ToArray(typeof(CustomProperty));
            }


            void ICollection.CopyTo(Array array, int index)
            {
                CopyTo((CustomProperty[])array, index);
            }

            int IList.Add(object value)
            {
                return Add((CustomProperty)value);
            }

            bool IList.Contains(object obj)
            {
                return Contains((CustomProperty)obj);
            }

            object IList.this[int index]
            {
                get
                {
                    return ((CustomPropertyCollection)this)[index];
                }
                set
                {
                    ((CustomPropertyCollection)this)[index] = (CustomProperty)value;
                }
            }

            int IList.IndexOf(object obj)
            {
                return IndexOf((CustomProperty)obj);
            }

            void IList.Insert(int index, object value)
            {
                Insert(index, (CustomProperty)value);
            }

            void IList.Remove(object value)
            {
                Remove((CustomProperty)value);
            }

        }

        private class CustomPropertyDescriptor : PropertyDescriptor
        {
            private CustomPropertyList list;
            private CustomProperty item;

            public CustomPropertyDescriptor(CustomProperty item, CustomPropertyList list, string name, Attribute[] attrs)
                :
                base(name, attrs)
            {
                this.list = list;
                this.item = item;
            }

            public override Type ComponentType
            {
                get { return item.GetType(); }
            }

            public override bool IsReadOnly
            {
                get { return (Attributes.Matches(ReadOnlyAttribute.Yes)); }
            }

            public override Type PropertyType
            {
                get { return Type.GetType(item.Type); }
            }

            public override bool CanResetValue(object component)
            {
                if (item.DefaultValue == null)
                    return false;
                else
                    return !this.GetValue(component).Equals(item.DefaultValue);
            }

            public override object GetValue(object component)
            {
                CustomPropertyEventArgs e = new CustomPropertyEventArgs(item, null);
                list.OnGetValue(e);
                return e.Value;
            }

            public override void ResetValue(object component)
            {
                SetValue(component, item.DefaultValue);
            }

            public override void SetValue(object component, object value)
            {
                CustomPropertyEventArgs e = new CustomPropertyEventArgs(item, value);
                list.OnSetValue(e);
            }

            public override bool ShouldSerializeValue(object component)
            {
                object val = this.GetValue(component);

                if (item.DefaultValue == null && val == null)
                    return false;
                else
                    return !val.Equals(item.DefaultValue);
            }
        }

        public event CustomPropertyEventHandler GetValue;
        public event CustomPropertyEventHandler SetValue;

        protected virtual void OnGetValue(CustomPropertyEventArgs e)
        {
            if (GetValue != null)
                GetValue(this, e);
        }

        protected virtual void OnSetValue(CustomPropertyEventArgs e)
        {
            if (SetValue != null)
                SetValue(this, e);
        }

        public string DefaultProperty
        {
            get { return defaultProperty; }
            set { defaultProperty = value; }
        }

        public CustomPropertyCollection CustomProperties
        {
            get { return customProperties; }
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            CustomProperty customProperty = null;
            if (defaultProperty != null)
            {
                int index = customProperties.IndexOf(defaultProperty);
                customProperty = customProperties[index];
            }

            if (customProperty != null)
                return new CustomPropertyDescriptor(customProperty, this, customProperty.Name, null);
            else
                return null;
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            //Create a list of the properties that are contained to pass on to ICustomTypeDescriptor
            ArrayList properties = new ArrayList();

            foreach (CustomProperty customProperty in customProperties)
            {
                CustomPropertyDescriptor customPropertyDescriptor = new CustomPropertyDescriptor(customProperty,
                    this, customProperty.Name, null);
                properties.Add(customPropertyDescriptor);
            }

            //We need to change the array to a form ICustomDescriptor can use
            PropertyDescriptor[] propArray = (PropertyDescriptor[])properties.ToArray(
                typeof(PropertyDescriptor));
            return new PropertyDescriptorCollection(propArray);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }
}
