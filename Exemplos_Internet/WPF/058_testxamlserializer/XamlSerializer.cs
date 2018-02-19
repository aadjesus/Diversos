using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Markup.Primitives;
using System.Xml;

namespace TestXamlSerializer
{

  //****************************************************************************
  /// <summary>
  ///   Provides methods to read and write XAML.
  /// </summary>
  public class XamlSerializer
  {

    //==========================================================================
    private struct NamespaceDefinition
    {
      public readonly string Namespace;
      public readonly Assembly Assembly;

      public NamespaceDefinition(string ns, Assembly asm)
      {
        Namespace = ns;
        Assembly = asm;
      }
    }


    //==========================================================================
    private readonly XmlDocument Document = new XmlDocument();
    private readonly MarkupObject MarkupObject = null;
    private readonly Dictionary<NamespaceDefinition, string> NamespacePrefixes = new Dictionary<NamespaceDefinition, string>();

    //==========================================================================
    private string NextNamespacePrefix = "a";


    //==========================================================================
    private Type RootType
    {
      get
      {
        return MarkupObject.ObjectType;
      }
    }

    //==========================================================================
    private string RootNamespace
    {
      get
      {
        return RootType.Namespace;
      }
    }

    //==========================================================================
    private Assembly RootAssembly
    {
      get
      {
        return RootType.Assembly;
      }
    }

    //==========================================================================
    private XmlElement RootElement
    {
      get
      {
        return Document.DocumentElement;
      }
    }

    //==========================================================================
    private string GetNamespacePrefix(Type type)
    {
      if((type.Namespace == RootNamespace) && (type.Assembly == RootAssembly))
        return null;

      string assemblyname = type.Assembly.GetName().Name;

      if((type.Assembly == RootAssembly) && (assemblyname == "PresentationFramework"))
        return null;

      if(assemblyname == "PresentationFramework")
        foreach(NamespaceDefinition nsdef in NamespacePrefixes.Keys)
          if(nsdef.Assembly.GetName().Name == assemblyname)
            return NamespacePrefixes[nsdef];

      NamespaceDefinition definition = new NamespaceDefinition(type.Namespace,
                                                               type.Assembly);

      string prefix;
      if(!NamespacePrefixes.TryGetValue(definition, out prefix))
      {
        NamespacePrefixes.Add(definition, prefix = NextNamespacePrefix);

        if(NextNamespacePrefix[NextNamespacePrefix.Length - 1] == 'z')
          NextNamespacePrefix += "a";
        else
          NextNamespacePrefix = NextNamespacePrefix.Substring(0, NextNamespacePrefix.Length - 1) + String.Format("{0}", (char)(NextNamespacePrefix[NextNamespacePrefix.Length - 1] + 1));
        Document.DocumentElement.SetAttribute("xmlns:" + prefix, GetNamespaceUri(type));
      }

      return prefix;
    }

    //==========================================================================
    private string GetNamespaceUri(Type type)
    {
      GetNamespacePrefix(type);

      if(type.Assembly.GetName().Name == "PresentationFramework")
        return "http://schemas.microsoft.com/winfx/2006/xaml/presentation";

      return String.Format("clr-namespace:{0};assembly={1}", type.Namespace, type.Assembly.GetName().Name);
    }

    //==========================================================================
    public XamlSerializer(object instance)
    {
      MarkupObject = MarkupWriter.GetMarkupObjectFor(instance);
    }

    //==========================================================================
    private void SaveObject(XmlElement parent, MarkupObject markupObject)
    {
      XmlElement element = Document.CreateElement(GetNamespacePrefix(markupObject.ObjectType),
                                                  markupObject.ObjectType.Name,
                                                  GetNamespaceUri(markupObject.ObjectType));
      parent.AppendChild(element);

      foreach(MarkupProperty property in markupObject.Properties)
        SaveProperty(element, markupObject, property);
    }

    //==========================================================================
    private XmlElement CreatePropertyElement(MarkupProperty property)
    {
      if(property.IsAttached)
        // If this property is attached it must be a DependencyProperty
        return Document.CreateElement(GetNamespacePrefix(property.DependencyProperty.OwnerType),
                                      property.Name,
                                      GetNamespaceUri(property.DependencyProperty.OwnerType));


      // Use the type of the property's component 
      return Document.CreateElement(GetNamespacePrefix(property.PropertyDescriptor.ComponentType),
                                    property.PropertyDescriptor.ComponentType.Name + "." + property.Name,
                                    GetNamespaceUri(property.PropertyDescriptor.ComponentType));
    }

    //==========================================================================
    private void SaveAttributeProperty(XmlElement parent, MarkupProperty property)
    {
      if(property.IsAttached)
      {
        if(GetNamespacePrefix(property.DependencyProperty.OwnerType) == null)
          parent.SetAttribute(property.Name, property.StringValue);
        else
          // Only use namespace and uri for attached properties not in the...
          // ...root namespace
          parent.SetAttribute(property.Name,
                              GetNamespaceUri(property.DependencyProperty.OwnerType),
                              property.StringValue);
      }
      else
        parent.SetAttribute(property.Name, property.StringValue);
    }

    //==========================================================================
    private void SavePropertyContent(XmlElement parent, MarkupProperty property)
    {
      List<MarkupObject> items = new List<MarkupObject>(property.Items);
      if(items.Count > 0)
        foreach(MarkupObject item in items)
          SaveObject(parent, item);
      else
        parent.InnerText = property.StringValue;
    }


    //==========================================================================
    private void SaveCompositeProperty(XmlElement parent, MarkupProperty property)
    {
      XmlElement element = CreatePropertyElement(property);
      SavePropertyContent(element, property);
      parent.AppendChild(element);
    }

    //==========================================================================
    private void SaveBindingProperty(XmlElement parent, MarkupProperty property, Binding binding)
    {
      // Bound properties are always serialized with an element so...
      // ...MarkupWriter.GetMarkupObjectFor() can be used to save the Binding
      XmlElement element = CreatePropertyElement(property);
      SaveObject(element, MarkupWriter.GetMarkupObjectFor(binding));
      parent.AppendChild(element);
    }

    //==========================================================================
    private void SaveProperty(XmlElement parent, MarkupObject markupObject, MarkupProperty property)
    {
      Binding binding = null;

      if(property.DependencyProperty != null)
        // If this property is a DependencyProperty markupObject.Instance... 
        // ...must be a DepedencyObject
        binding = BindingOperations.GetBinding(markupObject.Instance as DependencyObject,
                                               property.DependencyProperty);

      if(binding != null)
        SaveBindingProperty(parent, property, binding);
      else
      {
        string content_property_name = null;

        // Get the content property of markupObject.Instance...
        object[] attributes = property.PropertyDescriptor.ComponentType.GetCustomAttributes(typeof(ContentPropertyAttribute), true);
        if(attributes.Length > 0)
        {
          ContentPropertyAttribute attribute = attributes[attributes.Length - 1] as ContentPropertyAttribute;
          content_property_name = attribute.Name;
        }

        if(content_property_name == property.Name)
          // Only store the property's content
          SavePropertyContent(parent, property);

        else if(property.IsComposite)
          // Save property using its own element
          SaveCompositeProperty(parent, property);

        else
          // Save as attribute
          SaveAttributeProperty(parent, property);
      }
    }

    //==========================================================================
    private void CreateDocument()
    {
      Document.AppendChild(Document.CreateXmlDeclaration("1.0", "utf-8", null));
      Document.AppendChild(Document.CreateElement(MarkupObject.ObjectType.Name, GetNamespaceUri(MarkupObject.ObjectType)));

      // Save all properties if the element
      foreach(MarkupProperty property in MarkupObject.Properties)
        SaveProperty(Document.DocumentElement, MarkupObject, property);
    }

    //==========================================================================
    private static XmlDocument CreateDocument(object instance)
    {
      XamlSerializer xaml_writer = new XamlSerializer(instance);
      xaml_writer.CreateDocument();
      return xaml_writer.Document;
    }

    //==========================================================================
    /// <summary>
    ///   Saves the provided object as XAML into the specified 
    ///   <see cref="XmlWriter"/>.
    /// </summary>
    /// <param name="instance">
    ///   The <see cref="Object"/> to save.
    /// </param>
    /// <param name="writer">
    ///   The <see cref="XmlWriter"/> to use for writing the XAML.
    /// </param>
    public static void Save(object instance, XmlWriter writer)
    {
      CreateDocument(instance).Save(writer);
    }

    //==========================================================================
    /// <summary>
    ///   Saves the provided object as XAML into the specified 
    ///   <see cref="TextWriter"/>.
    /// </summary>
    /// <param name="instance">
    ///   The <see cref="Object"/> to save.
    /// </param>
    /// <param name="writer">
    ///   The <see cref="TextWriter"/> to use for writing the XAML.
    /// </param>
    public static void Save(object instance, TextWriter writer)
    {
      CreateDocument(instance).Save(writer);
    }

    //==========================================================================
    /// <summary>
    ///   Saves the provided object as XAML into the specified 
    ///   <see cref="Stream"/>.
    /// </summary>
    /// <param name="instance">
    ///   The <see cref="Object"/> to save.
    /// </param>
    /// <param name="stream">
    ///   The <see cref="Stream"/> to use for writing the XAML.
    /// </param>
    public static void Save(object instance, Stream stream)
    {
      CreateDocument(instance).Save(stream);
    }

    //==========================================================================
    /// <summary>
    ///   Saves the provided object as XAML into the specified 
    ///   <see cref="string"/>.
    /// </summary>
    /// <param name="instance">
    ///   The <see cref="Object"/> to save.
    /// </param>
    /// <returns>
    ///   A <see cref="string"/> containing the XAML.
    /// </returns>
    public static string Save(object instance)
    {
      using(MemoryStream stream = new MemoryStream())
      {
        CreateDocument(instance).Save(stream);
        return Encoding.UTF8.GetString(stream.GetBuffer());
      }
    }

    //==========================================================================
    /// <summary>
    ///   Reads the XAML in the specified <see cref="Stream"/> and returns 
    ///   the root object.
    /// </summary>
    /// <param name="stream">
    ///   The <see cref="Stream"/> providing the XAML.
    /// </param>
    /// <returns>
    ///   The root object of the read XAML.
    /// </returns>
    public static object Load(Stream stream)
    {
      return XamlReader.Load(stream);
    }

    //==========================================================================
    /// <summary>
    ///   Reads the XAML in the specified <see cref="XmlReader"/> and returns 
    ///   the root object.
    /// </summary>
    /// <param name="reader">
    ///   The <see cref="XmlReader"/> providing the XAML.
    /// </param>
    /// <returns>
    ///   The root object of the read XAML.
    /// </returns>
    public static object Load(XmlReader reader)
    {
      return XamlReader.Load(reader);
    }

    //==========================================================================
    /// <summary>
    ///   Reads the XAML in the specified <see cref="string"/> and returns 
    ///   the root object.
    /// </summary>
    /// <param name="value">
    ///   The <see cref="string"/> providing the XAML.
    /// </param>
    /// <returns>
    ///   The root object of the read XAML.
    /// </returns>
    public static object Load(string value)
    {
      using(MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(value)))
        return XamlReader.Load(stream);
    }

  } // class XamlSerializer

}
