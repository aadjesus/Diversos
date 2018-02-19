using System.Collections.ObjectModel;
using System;
using System.Xml.Linq;
using System.Xml;
using System.Text;
namespace SnippetMaker
{
  partial class Program
  {
    private void makeLTest()
    {
      
    }
  }

  internal class Variable
  {
    internal string Name { get; set; }
    internal string Expression { get; set; }
    internal string InitialRange { get; set; }
    internal XElement AsElement()
    {
      return new XElement("Variable",
        new XAttribute("name", Name),
        new XAttribute("expression", Expression),
        new XAttribute("initialRange", InitialRange));
    }
  }

  internal class Variables : Collection<Variable>
  {
    internal XElement AsElement()
    {
      XElement result = new XElement("Variables");
      foreach (Variable v in Items)
        result.Add(v);
      return result;
    }
  }

  internal class LiveTempates : Collection<LiveTempate>
  {
    private const string family = "Live Templates";
    public override string ToString()
    {
      var doc = new XDocument(
        new XElement("TemplatesExport",
          new XAttribute("family", family)));
      var sb = new StringBuilder();
      using (var w = XmlWriter.Create(sb))
      {
        if (w != null) doc.WriteTo(w);
      }
      return sb.ToString();
    }
  }

  internal class LiveTempate
  {
    private Guid guid = Guid.NewGuid();
    internal string Text { get; set; }
    internal string Shortcut { get; set; }
    internal string Description { get; set; }
    private const bool reformat = true;
    private const bool shortenQualifiedReferences = true;
    internal Variables Variables { get; set; }
    internal LiveTempate()
    {
      Variables = new Variables();
    }
    internal XElement AsElement()
    {
      return new XElement("Template",
        new XAttribute("uid", guid.ToString()),
        new XAttribute("text", Text),
        new XAttribute("shortcut", Shortcut),
        new XAttribute("description", Description),
        new XAttribute("reformat", reformat),
        new XAttribute("shortenQualifiedReferences", shortenQualifiedReferences),
        
        Variables.AsElement());
    }
  }
}