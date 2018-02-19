//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : TocEntryCollection.cs
// Author  : Eric Woodruff
// Updated : 09/18/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class used to hold the table of content
// entries for additional content items.
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
// 1.3.0.0  09/17/2006  EFW  Created the code
//=============================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This collection class is used to hold the table of content entries for
    /// additional content items.
    /// </summary>
    public class TocEntryCollection : Collection<TocEntry>
    {
        /// <summary>
        /// This is used to sort the collection
        /// </summary>
        /// <remarks>All top level items and their children are sorted</remarks>
        public void Sort()
        {
            ((List<TocEntry>)this.Items).Sort(
                delegate(TocEntry x, TocEntry y)
                {
                    return Comparer<TocEntry>.Default.Compare(x, y);
                });

            foreach(TocEntry te in this)
                te.Children.Sort();
        }

        /// <summary>
        /// Convert the table of content entry and its children to a string
        /// </summary>
        /// <returns>The entries in HTML 1.x help format</returns>
        public override string ToString()
        {
            return this.ToString(HelpFileFormat.HtmlHelp1x);
        }

        /// <summary>
        /// Convert the table of content entry and its children to a string
        /// in the specified help file format.
        /// </summary>
        /// <param name="format">The help file format to use</param>
        /// <returns>The entries in specified help format</returns>
        /// <exception cref="ArgumentException">This is thrown if the
        /// format is not <b>HtmlHelp1x</b> or <b>HtmlHelp2x</b>.</exception>
        public string ToString(HelpFileFormat format)
        {
            if(format != HelpFileFormat.HtmlHelp1x &&
              format != HelpFileFormat.HtmlHelp2x)
                throw new ArgumentException("The format specified must be " +
                    "HtmlHelp1x or HtmlHelp2x only", "format");

            StringBuilder sb = new StringBuilder(1024);
            this.ConvertToString(format, sb);
            return sb.ToString();
        }

        /// <summary>
        /// This is used to convert the collection to a string and append it
        /// to the specified string builder.
        /// </summary>
        /// <param name="format">The help file format to use</param>
        /// <param name="sb">The string builder to which the information is
        /// appended.</param>
        internal void ConvertToString(HelpFileFormat format, StringBuilder sb)
        {
            foreach(TocEntry te in this)
                te.ConvertToString(format, sb);
        }
    }
}
