//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : NamingMethodEnum.cs
// Author  : Eric Woodruff
// Updated : 11/03/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the naming method to
// use for the help topic filenames.
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
// 1.3.2.0  11/03/2006  EFW  Created the code
//=============================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This public enumerated type defines the naming method to use for the
    /// help topic filenames.
    /// </summary>
    [Serializable]
    public enum NamingMethod
    {
        /// <summary>Use the default GUID file naming method.</summary>
        Guid,
        /// <summary>Use the member name without parameters as the
        /// filename.  The characters ":", ".", "#", and "`" in the name are
        /// replaced with an underscore (i.e. T:CustomType becomes
        /// T_CustomType, M:CustomType.#ctor becomes M_CustomType__ctor,
        /// P:CustomType.Property becomes P:CustomType_Property, etc).
        /// Duplicate names will have an incrementing value appended to the
        /// end of the name (i.e. M_CustomType_Method,
        /// M_CustomType.Method_1, M_CustomType_Method_2, etc).</summary>
        MemberName,
        /// <summary>Use the hashed member name without parameters as the
        /// filename.  No character replacements are made for this option
        /// and the <b>GetHashCode</b> method is used to generate the hash
        /// value and it is formatted as a hex value.  This is useful for
        /// extremely long type names that cause the filename to exceed the
        /// maximum length when the full path is included.  Duplicate names
        /// will have an incrementing value appended to the name prior to
        /// creating the hash value as needed.</summary>
        HashedMemberName
    }
}
