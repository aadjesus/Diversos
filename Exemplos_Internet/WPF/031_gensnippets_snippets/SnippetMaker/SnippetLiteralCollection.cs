using System;
using System.Collections.ObjectModel;

namespace SnippetMaker
{
  class SnippetLiteralCollection : Collection<SnippetLiteral>, ICloneable
  {
    public void AddLiteral(string nameAndDefault, string toolTip)
    {
      Add(new SnippetLiteral(nameAndDefault, toolTip, nameAndDefault));
    }
    public void AddClassName()
    {
      Add(new SnippetLiteral("ClassName", "Name of class", "ClassName", "ClassName()", false));
    }
    
    public object Clone()
    {
      SnippetLiteralCollection coll = new SnippetLiteralCollection();
      foreach (SnippetLiteral sl in Items)
        coll.Add((SnippetLiteral) sl.Clone());
      return coll;
    }
  }
}