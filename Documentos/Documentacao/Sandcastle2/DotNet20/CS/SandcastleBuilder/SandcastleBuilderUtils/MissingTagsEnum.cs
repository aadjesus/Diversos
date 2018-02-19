//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : MissingTagsEnum.cs
// Author  : Eric Woodruff
// Updated : 09/29/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the missing
// documentation tags for which to search.
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
// 1.3.1.0  09/26/2006  EFW  Created the code
//=============================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This public enumerated type defines the missing documentation tags for
    /// which to search.
    /// </summary>
    [Flags, Serializable]
    public enum MissingTags
    {
        /// <summary>Do not search for any missing tags.</summary>
        None              = 0x0000,
        /// <summary>Search for missing &lt;summary&gt; tags.</summary>
        Summary           = 0x0001,
        /// <summary>Search for missing &lt;param&gt; tags.</summary>
        Parameter         = 0x0002,
        /// <summary>Search for missing &lt;returns&gt; tags.</summary>
        Returns           = 0x0004,
        /// <summary>Search for missing &lt;value&gt; tags.</summary>
        Value             = 0x0008,
        /// <summary>Search for missing &lt;remarks&gt; tags.</summary>
        Remarks           = 0x0010,
        /// <summary>Automatically document constructors if they are missing a
        /// &lt;summary&gt; tag.</summary>
        AutoDocumentCtors = 0x0020
    }
}
