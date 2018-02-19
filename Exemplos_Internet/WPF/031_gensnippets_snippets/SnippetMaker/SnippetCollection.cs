using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace SnippetMaker
{
  class SnippetCollection : Collection<Snippet>
  {
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
      sb.AppendLine("<CodeSnippets xmlns=\"http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet\">");
      foreach (Snippet s in Items)
        sb.Append(s.ToString());
      sb.AppendLine("</CodeSnippets>");
      return sb.ToString();
    }

    public void Save(string nameOfSnippet)
    {
      Save(nameOfSnippet, false);
    }
    public void Save(string nameOfSnippet, bool deleteInstead)
    {
      try
      {
        string filename = string.Format(@"C:\Users\Dmitri\Projects\Snippets\{0}.snippet", nameOfSnippet);
        File.Delete(filename);
        if (!deleteInstead)
          File.WriteAllText(filename, ToString());
        
        // not process installer
        string toInstall = string.Format(@"C:\Users\Dmitri\Projects\CodePlex\Installer\{0}.snippet", nameOfSnippet);
        File.Delete(toInstall);
        File.WriteAllText(toInstall, ToString());
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.Message);
      }
    }
  }
}