//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : ContentPlacementEnum.cs
// Author  : Eric Woodruff
// Updated : 09/06/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the placement of the
// additional content items in the table of content if any are defined.
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
// 1.2.0.0  09/06/2006  EFW  Added ContentPlacement
//=============================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This public enumerated type defines the placement of the additional
    /// content items in the table of content if any are defined.
    /// </summary>
    [Serializable]
    public enum ContentPlacement
    {
        /// <summary>Additional content appears above the namespaces</summary>
        AboveNamespaces,
        /// <summary>Additional content appears below the namespaces</summary>
        BelowNamespaces
    }
}
