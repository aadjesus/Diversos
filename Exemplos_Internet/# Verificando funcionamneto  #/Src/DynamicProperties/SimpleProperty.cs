using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicProperties
{
  /// <summary>
  /// Simple dynamic property.
  /// </summary>
  public class SimpleProperty<T> : DynamicProperty<T>
  {
    T _value;

    // constructors...
    public SimpleProperty(T value)
    {
      _value = value;
    }

    // public methods...
    public override TValue GetValue<TValue>(string name)
    {
      throw new InvalidOperationException("Can't get property value from SimpleProperty instance.");
    }
    public override void SetValue<TValue>(string name, TValue value)
    {
      throw new InvalidOperationException("Can't set property value in SimpleProperty instance.");
    }
    public override bool HasProperty(string name)
    {
      return false;
    }
    public override void AddProperty<TValue>(string name, DynamicProperty<TValue> property)
    {
      throw new InvalidOperationException("Can't add child properties to SimpleProperty instance.");
    }
    public override void RemoveProperty(string name)
    {
      throw new InvalidOperationException("Can't remove child properties from SimpleProperty instance.");
    }
    public override DynamicProperty<TValue> GetProperty<TValue>(string name)
    {
      throw new InvalidOperationException("Can't get child properties from SimpleProperty instance.");
    }

    // public properties...
    public override T Value
    {
      get { return _value; }
      set { _value = value; }
    }
  }
}