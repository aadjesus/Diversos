using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SnippetMaker
{
  class Snippet : ICloneable
  {
    public string Format = "1.0.0";
    public string Title = string.Empty;
    public string Shortcut = string.Empty;
    public string Description = string.Empty;
    public string Author = string.Empty;
    public StringBuilder CodeBuilder = new StringBuilder();
    public SnippetLiteralCollection Literals = new SnippetLiteralCollection();
    public IList<string> Types = new List<string> { "Expansion" };

    public Snippet() { }

    public Snippet(string author, string title, string shortcut, string description)
    {
      Author = author;
      Title = title;
      Shortcut = shortcut;
      Description = description;
    }

    /// <summary>
    /// Returns a <see cref="T:System.Xml.Linq.XElement"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Xml.Linq.XElement"/> that represents the current <see cref="T:SnippetMaker.Snippet"/>.
    /// </returns>
    public XElement ToXElement()
    {
      XElement xe = new XElement("CodeSnippet",
                                 new XAttribute("Format", Format),
                                 new XElement("Header",
                                              new XElement("Title", Title),
                                              new XElement("Shortcut", Shortcut),
                                              new XElement("Description", Description),
                                              new XElement("Author", Author),
                                              new XElement("SnippetTypes",
                                                           Types.Select(t => new XElement("SnippetType", t)).ToArray()
                                                )
                                   ),
                                 new XElement("Snippet",
                                              new XElement("Declarations",
                                                           Literals.Select(l => l.ToXElement())
                                                ),
                                              new XElement("Code",
                                                           new XAttribute("Language", "CSharp"),
                                                           new XCData(CodeBuilder.ToString())
                                                )
                                   )
        );

      return xe;
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:SnippetMaker.Snippet"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:SnippetMaker.Snippet"/>.
    /// </returns>
    public override string ToString()
    {
      return ToXElement().ToString();
    }

    /// <summary>
    /// Deep copy.
    /// </summary>
    /// <returns>Deep copy of object.</returns>
    public object Clone()
    {
      var s = new Snippet(Author, Title, Shortcut, Description)
                {
                  Format = Format,
                  Literals = ((SnippetLiteralCollection) Literals.Clone()),
                  CodeBuilder = new StringBuilder(CodeBuilder.ToString()),
                  Types = new List<string>(Types)
                };
      return s;
    }
  }
}