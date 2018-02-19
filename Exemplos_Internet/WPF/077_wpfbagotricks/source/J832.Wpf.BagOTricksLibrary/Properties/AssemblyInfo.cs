using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

#region General information
[assembly: AssemblyCompany("http://j832.com/")]

#if (DEBUG)
[assembly: AssemblyConfiguration("Debug Build")]
#else
[assembly: AssemblyConfiguration("Release Build")]
#endif

#endregion

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: ThemeInfo(ResourceDictionaryLocation.SourceAssembly, ResourceDictionaryLocation.SourceAssembly)]