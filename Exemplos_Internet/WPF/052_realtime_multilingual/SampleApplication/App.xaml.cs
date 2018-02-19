/* *************************************************************************************
 *
 * Copyright (c) Rhea NV.
 *
 * This source code is subject to terms and conditions of the Code Project Open License.
 * A copy of the license can be found at http://www.codeproject.com/info/cpol10.aspx.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * ************************************************************************************/

using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows;
using Vidyano.TranslationServices.GoogleTranslate;

namespace SampleApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Note: Optionally you could set the default UI culture here
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-be");
            
            // Note: You might have to set a Proxy here
            // GoogleTranslateExtensions.Proxy = new WebProxy("xxx.xxx.xxx.xxx", 8080);

            base.OnStartup(e);
        }
    }
}