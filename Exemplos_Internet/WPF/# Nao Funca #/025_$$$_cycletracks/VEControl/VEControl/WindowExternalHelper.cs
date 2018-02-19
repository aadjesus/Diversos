using System.Runtime.InteropServices;
using System.Text;
using System;

namespace VEControl
{
  // Make accessible to the web browser and JavaScript
  [ComVisible(true)]
  public class WindowExternalHelper
  {
    VEMap map;

    public WindowExternalHelper(VEMap map)
    {
      this.map = map;
    }

    public void OnInitMode()
    {
      this.map.RaiseModeInitialized();
    }

    public void OnChangeView()
    {
      this.map.RaiseViewChanged();
    }

    public void OnMapLoaded()
    {
      this.map.RaiseMapLoaded();
    }

    public void ThrowWindowError(string message, string uri, string line)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("MAP WINDOW ERROR\n\n");
      sb.Append("message: ");
      sb.Append(message);
      sb.Append("\n");
      sb.Append("uri: ");
      sb.Append(uri);
      sb.Append("line: ");
      sb.Append("\n");
      sb.Append(line);

      this.map.ThrowException(new Exception(sb.ToString()));
    }

    public void ThrowMapError(string error, string eventName)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("MAP ERROR\n\n");
      sb.Append("error: ");
      sb.Append(error);
      sb.Append("\n");
      sb.Append("eventName: ");
      sb.Append(eventName);

      this.map.ThrowException(new Exception(sb.ToString()));
    }

    public void ThrowVEException(string source, string name, string message)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("MAP VEEXCEPTION\n\n");
      sb.Append("source: ");
      sb.Append(source);
      sb.Append("\n");
      sb.Append("name: ");
      sb.Append(name);
      sb.Append("message: ");
      sb.Append("\n");
      sb.Append(message);

      this.map.ThrowException(new Exception(sb.ToString()));
    }
  }
}
