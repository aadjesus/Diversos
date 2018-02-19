using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicProperties
{
  /// <summary>
  /// Complex dynamic property implementation.
  /// </summary>
  public class ComplexProperty<T> : DynamicProperty<T>
  {
    Dictionary<string, object> _properties;

    // constructors...
    public ComplexProperty()
    {
    }

    // private properties...
    Dictionary<string, object> Properties
    {
      get
      {
        if (_properties == null)
          _properties = new Dictionary<string, object>();
        return _properties;
      }
    }

    // public methods...
    public override TValue GetValue<TValue>(string name)
    {
      DynamicProperty<TValue> property = GetProperty<TValue>(name);
      if (property != null)
        return property.Value;
      return default(TValue);
    }
    public override void SetValue<TValue>(string name, TValue value)
    {
      DynamicProperty<TValue> property = GetProperty<TValue>(name);
      if (property != null)
        property.Value = value;
    }
    public override bool HasProperty(string name)
    {
      return _properties == null ? false : Properties.ContainsKey(name);
    }
    public override void AddProperty<TValue>(string name, DynamicProperty<TValue> property)
    {
      if (HasProperty(name))
        throw new InvalidOperationException(String.Format("Can't add property {0}, because it is already added.", name));

      Properties.Add(name, property);
    }
    public override void RemoveProperty(string name)
    {
      Properties.Remove(name);
    }
    public override DynamicProperty<TValue> GetProperty<TValue>(string name)
    {
      if (!HasProperty(name))
        throw new InvalidOperationException(String.Format("Can't get property {0} value, because it doesn't exist.", name));

      DynamicProperty<TValue> property = Properties[name] as DynamicProperty<TValue>;
      if (property == null)
        throw new InvalidOperationException(String.Format("Invalid type {0} specified for {1} property.", typeof(TValue).FullName, name));

      return property;
    }

    // public properties...
    public override T Value
    {
      get { return default(T); }
      set { }
    }
  }
}