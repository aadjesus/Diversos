using System;
using System.Xml.Linq;

namespace SnippetMaker
{
  class SnippetLiteral : ICloneable
  {
    public string ID = string.Empty;
    public string ToolTip = string.Empty;
    public string Default = string.Empty;
    public string Function = string.Empty;
    public bool Editable = true;

    public SnippetLiteral() { }

    public SnippetLiteral(string id, string toolTip, string def) : this(id, toolTip, def, string.Empty, true) { }

    public SnippetLiteral(string id, string toolTip, string def, string function, bool editable)
    {
      ID = id;
      ToolTip = toolTip;
      Default = def;
      Function = function;
      Editable = editable;
    }

    public XElement ToXElement()
    {
      XElement xe = new XElement("Literal",
                                 new XElement("ID", ID),
                                 new XElement("ToolTip", ToolTip),
                                 new XElement("Default", Default));
      if (!Editable) 
        xe.Add(new XAttribute("Editable", false));
      if (!string.IsNullOrEmpty(Function))
        xe.Add(new XElement("Function", Function));
      return xe;
    }

    #region ICloneable Members

    /// <summary>
    /// Deep copy.
    /// </summary>
    /// <returns></returns>
    public object Clone()
    {
      return MemberwiseClone();
    }

    #endregion
  }
}