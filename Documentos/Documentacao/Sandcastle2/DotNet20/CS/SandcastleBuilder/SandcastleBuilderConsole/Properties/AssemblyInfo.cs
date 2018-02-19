//=============================================================================
// System  : Sandcastle Help File Builder
// File    : AssemblyInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 03/17/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// Sandcastle Help File Builder attributes.
//
// This code may be used in compiled form in any way you desire.  This file
// may be redistributed unmodified by any means PROVIDING it is not sold for
// profit without the author's written consent, and providing that this notice
// and the author's name and all copyright notices remain intact.
//
// This code is provided "as is" with no warranty either express or implied.
// The author accepts no liability for any damage or loss of business that
// this product may cause.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.0.0.0  08/10/2006  EFW  Created the code
// 1.1.0.0  08/22/2006  EFW  Added support for building HTML help 2.x files
// 1.2.0.0  09/02/2006  EFW  Various additions and updates
// 1.3.0.0  09/08/2006  EFW  Various additions and updates
// 1.3.2.0  10/10/2006  EFW  Various additions and updates
// 1.3.4.0  12/24/2006  EFW  Various additions and updates
//=============================================================================

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

//
// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyProduct("Sandcastle Help File Builder Console")]
[assembly: AssemblyTitle("Sandcastle Help File Builder Console")]
[assembly: AssemblyDescription("A command line builder for configuration " +
    "files used to build HTML help documentation using Sandcastle.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Eric Woodruff")]
[assembly: AssemblyCopyright("Copyright \xA9 2006-2007, Eric Woodruff, All Rights Reserved")]
[assembly: AssemblyTrademark("Eric Woodruff, All Rights Reserved")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]

// No special permissions are required by this assembly
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution = true)]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers
// by using the '*' as shown below:

[assembly: AssemblyVersion("1.4.0.1")]
