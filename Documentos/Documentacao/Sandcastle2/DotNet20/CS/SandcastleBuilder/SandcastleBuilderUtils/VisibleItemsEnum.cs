//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : VisibleItems.cs
// Author  : Eric Woodruff
// Updated : 10/06/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the optional visible
// items in the help file.
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
// 1.3.1.0  10/03/2006  EFW  Created the code
//=============================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This public enumerated type defines the optional visible items in the
    /// help file.
    /// </summary>
    [Flags, Serializable]
    public enum VisibleItems
    {
        /// <summary>None of the optional items are visible.</summary>
        None                             = 0x0000,
        /// <summary>Document attributes.</summary>
        Attributes                       = 0x0001,
        /// <summary>Document explicit interface implementations.</summary>
        ExplicitInterfaceImplementations = 0x0002,
        /// <summary>Document inherited members.</summary>
        InheritedMembers                 = 0x0004,
        /// <summary>Document inherited framework members.  For this to work,
        /// <b>InheritedMembers</b> must also be enabled.</summary>
        InheritedFrameworkMembers        = 0x0008,
        /// <summary>Document internal members.</summary>
        Internals                        = 0x0010,
        /// <summary>Document private members.</summary>
        Privates                         = 0x0020,
        /// <summary>Document protected members.</summary>
        Protected                        = 0x0040,
        /// <summary>Document "protected internal" members as "protected"
        /// only.</summary>
        ProtectedInternalAsProtected     = 0x0080,
        /// <summary>Document protected members of sealed classes.  For this
        /// to work, <b>Protected</b> must also be enabled.</summary>
        SealedProtected                  = 0x0100,
        /// <summary>Document private fields.  For this to work,
        /// <b>Privates</b> must also be enabled.</summary>
        PrivateFields                    = 0x0200
    }
}
