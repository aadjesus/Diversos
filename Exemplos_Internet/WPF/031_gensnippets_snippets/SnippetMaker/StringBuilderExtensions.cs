using System.Text;

namespace SnippetMaker
{
  public static class StringBuilderExtensions
  {
    public static void AppendXmlSummary(this StringBuilder sb, string summary)
    {
      sb.AppendLine("/// <summary>");
      sb.AppendLine("/// " + summary);
      sb.AppendLine("/// </summary>");
    }

    public static void AppendXmlValue(this StringBuilder sb, string val)
    {
      sb.AppendLine("/// <value>" + val + "</value>");
    }

    public static void AppendParam(this StringBuilder sb, string name, string text)
    {
      sb.AppendLine(string.Format("/// <param name=\"{0}\">{1}</param>", name, text));
    }

    public static void AppendBetweenCurlyBraces(this StringBuilder sb, string text)
    {
      sb.AppendLine("{");
      sb.AppendLine(text);
      sb.AppendLine("}");
    }
  }
}
