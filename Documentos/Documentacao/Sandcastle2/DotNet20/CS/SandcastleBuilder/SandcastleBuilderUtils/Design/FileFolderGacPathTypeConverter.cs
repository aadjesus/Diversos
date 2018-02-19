//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : FileFolderGacPathTypeConverter.cs
// Author  : Eric Woodruff
// Updated : 01/18/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type converter used to convert FileFolderGacPath
// objects to and from strings.
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
// 1.3.4.0  12/31/2006  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.Globalization;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This type converter is used to convert a FileFolderGacPath object to
    /// and from a string so that it can be edited in a
    /// <see cref="System.Windows.Forms.PropertyGrid" />.
    /// </summary>
    internal class FileFolderGacPathTypeConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Convert the specified object to the specified type
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <param name="culture">Culture-specific information</param>
        /// <param name="value">The object to convert</param>
        /// <param name="destinationType">The type to which to convert</param>
        /// <returns>The converted object</returns>
        public override object ConvertTo(ITypeDescriptorContext context,
          CultureInfo culture, object value, Type destinationType)
        {
            FileFolderGacPath ffgp = value as FileFolderGacPath;

            if(destinationType == typeof(string) && ffgp != null)
                return ffgp.PersistablePath;

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// This returns whether this converter can convert an object of the
        /// given type to a <see cref="FileFolderGacPath"/> object using the specified
        /// context.
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <param name="sourceType">The type from which to convert</param>
        /// <returns>True if the conversion can be performed or false if not.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context,
          Type sourceType)
        {
            if(sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// This converts the given object to a <see cref="FileFolderGacPath"/>
        /// object using the specified context and culture information.
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <param name="culture">Culture-specific information</param>
        /// <param name="value">The object to convert</param>
        public override object ConvertFrom(ITypeDescriptorContext context,
          CultureInfo culture, object value)
        {
            if(value is string)
                return new FileFolderGacPath((string)value);

            return base.ConvertFrom(context, culture, value);
        }
    }
}
