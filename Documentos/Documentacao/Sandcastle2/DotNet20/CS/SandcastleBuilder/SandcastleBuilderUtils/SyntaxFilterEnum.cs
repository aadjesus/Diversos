//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : SyntaxFilterEnum.cs
// Author  : Eric Woodruff
// Updated : 11/24/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the languages to include
// in the help topic Syntax section.
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
// 1.3.3.0  11/24/2006  EFW  Created the code
//=============================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This public enumerated type defines the languages to include in the
    /// help topic <b>Syntax</b> section.
    /// </summary>
    [Flags, Serializable]
    public enum SyntaxFilters
    {
        /// <summary>Do not include the syntax section.</summary>
        None              = 0x0000,
        /// <summary>Include C# syntax.</summary>
        CSharp            = 0x0001,
        /// <summary>Include VB.NET syntax.</summary>
        VisualBasic       = 0x0002,
        /// <summary>Include C++ syntax.</summary>
        CPlusPlus         = 0x0004,
        /// <summary>Include the J# syntax.</summary>
        JSharp            = 0x0008,
        /// <summary>The standard set (C#, VB.NET, and C++).</summary>
        Standard          = 0x0007,
        /// <summary>Include all languages.</summary>
        All               = 0x000F
    }
}
