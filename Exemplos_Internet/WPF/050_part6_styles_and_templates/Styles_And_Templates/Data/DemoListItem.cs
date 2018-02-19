using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace Styles_And_Templates
{
    /// <summary>
    /// Is used within the <see cref="DemoLauncherWindow">
    /// demo launcher window</see> as individual listBox
    /// items
    /// </summary>
    public class DemoListItem
    {
        #region Public Properties
        public string WindowName { get; set; }
        public string DemoName { get; set; }
        #endregion
    }
}
