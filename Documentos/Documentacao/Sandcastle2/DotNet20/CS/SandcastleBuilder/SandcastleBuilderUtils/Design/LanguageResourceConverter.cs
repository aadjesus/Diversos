//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : LanguageResourceConverter.cs
// Author  : Eric Woodruff
// Updated : 09/15/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type converter that allows you to select a culture
// from a list representing a set of available language resource folders.
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
// 1.3.0.0  09/15/2006  EFW  Created the code
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This type converter allows you to select a culture from a list
    /// representing a set of available language resource folders.
    /// </summary>
    public class LanguageResourceConverter : CultureInfoConverter
    {
        private bool initialized;   // Initialized flag

        /// <summary>
        /// This is used to compare two culture info objects by display name
        /// </summary>
        private class CultureInfoComparer : IComparer
        {
            /// <summary>
            /// Compare two items
            /// </summary>
            /// <param name="x">The first item to compare</param>
            /// <param name="y">The second item to compare</param>
            /// <returns>-1 if item 1 is less than item 2, 0 if they are equal,
            /// or 1 if item 1 is greater than item 2.</returns>
            public int Compare(object x, object y)
            {
                if(x == null)
                    return (y == null) ? 0 : -1;

                if(y == null)
                    return 1;

                return CultureInfo.CurrentCulture.CompareInfo.Compare(
                    ((CultureInfo)x).DisplayName,
                    ((CultureInfo)y).DisplayName,
                    CompareOptions.StringSort);
            }
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
            if(!initialized)
            {
                string dir = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location) +
                    @"\LanguageResources";
                string[] dirs = Directory.GetDirectories(dir);
                int idx = 0;

                CultureInfo[] ci = new CultureInfo[dirs.Length];

                // Find the available language resources
                foreach(string s in dirs)
                {
                    dir = s.Substring(s.LastIndexOf(@"\") + 1);
                    ci[idx++] = new CultureInfo(dir);
                }

                Array.Sort(ci, new CultureInfoComparer());

                StandardValuesCollection svc = new StandardValuesCollection(ci);

                // Use reflection to set the base class's values field to
                // our limited array.
                FieldInfo fi = typeof(CultureInfoConverter).GetField(
                    "values", BindingFlags.NonPublic | BindingFlags.Instance);
                fi.SetValue(this, svc);
                initialized = true;
            }

            return base.GetStandardValues(context);
        }
    }
}
