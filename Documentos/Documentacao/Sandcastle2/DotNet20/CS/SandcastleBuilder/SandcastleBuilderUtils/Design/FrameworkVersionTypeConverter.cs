//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : FrameworkVersionTypeConverter.cs
// Author  : Eric Woodruff
// Updated : 08/10/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type converter that allows you to select a .NET
// Framework version from those currently installed on the system.
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
// 1.0.0.0  08/08/2006  EFW  Created the code
//=============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This type converter allows you to select a .NET Framework version from
    /// those currently installed on the system.
    /// </summary>
    public class FrameworkVersionTypeConverter : StringConverter
    {
        //=====================================================================
        // Private data members

        private static List<string> versions = new List<string>();
        private static StandardValuesCollection standardValues =
            FrameworkVersionTypeConverter.InitializeStandardValues();

        //=====================================================================
        // Properties

        /// <summary>
        /// This read-only property returns the version number of the most
        /// recent copy of the .NET Framework installed on the system.
        /// </summary>
        public static string MostRecentVersion
        {
            get { return versions[versions.Count - 1]; }
        }

        //=====================================================================
        // Methods

        /// <summary>
        /// This is used to get the standard values by searching for the
        /// .NET Framework versions installed on the current system.
        /// </summary>
        private static StandardValuesCollection InitializeStandardValues()
        {
            string dir = Environment.GetFolderPath(
                Environment.SpecialFolder.System) +
                @"\..\Microsoft.NET\Framework";
            string[] dirs = Directory.GetDirectories(dir);

            // Find the available .NET versions on this system
            foreach(string s in dirs)
            {
                dir = s.Substring(s.LastIndexOf(@"\") + 1);

                if(dir[0] == 'v' && Char.IsDigit(dir[1]))
                    versions.Add(dir.Substring(1));
            }

            versions.Sort();
            return new StandardValuesCollection(versions);
        }

        /// <summary>
        /// This is overridden to return the values for the type converter's
        /// dropdown list.
        /// </summary>
        /// <param name="context">The format context object</param>
        /// <returns>Returns the standard values for the type</returns>
        public override StandardValuesCollection GetStandardValues(
          ITypeDescriptorContext context)
        {
            return standardValues;
        }

        /// <summary>
        /// This is overridden to indicate that the values are exclusive
        /// and values outside the list cannot be entered.
        /// </summary>
        /// <param name="context">The format context object</param>
        /// <returns>Always returns true</returns>
        public override bool GetStandardValuesExclusive(
          ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// This is overridden to indicate that standard values are supported
        /// and can be chosen from a list.
        /// </summary>
        /// <param name="context">The format context object</param>
        /// <returns>Always returns true</returns>
        public override bool GetStandardValuesSupported(
          ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// This is used to find out if the specified version of the .NET
        /// Framework is present on the system.
        /// </summary>
        /// <param name="version">The version for which to look</param>
        /// <returns>True if present, false if not found</returns>
        public static bool IsPresent(string version)
        {
            return versions.Contains(version);
        }

        /// <summary>
        /// This is used to get the latest version that starts with the
        /// given value.
        /// </summary>
        /// <param name="version">The version for which to look</param>
        /// <returns>The latest version starting with the specified value or
        /// the most recent version if not found.</returns>
        public static string LatestMatching(string version)
        {
            string latestVersion = FrameworkVersionTypeConverter.MostRecentVersion;

            foreach(string v in versions)
                if(v.StartsWith(version))
                    latestVersion = v;

            return latestVersion;
        }
    }
}
