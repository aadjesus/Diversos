using System;

namespace ShellControlsExample
{
  class TreeView : System.Windows.Forms.TreeView
  {
    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);

      if (!this.DesignMode && Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
        NativeMethods.SetWindowTheme(this.Handle, "explorer", null);
    }
  }
}
