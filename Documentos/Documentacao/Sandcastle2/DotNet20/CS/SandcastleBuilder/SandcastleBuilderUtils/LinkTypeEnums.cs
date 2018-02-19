//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : LinkTypeEnums.cs
// Author  : Eric Woodruff
// Updated : 10/02/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated types that define the reference link
// types for project links and SDK links.
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
// 1.1.0.0  08/28/2006  EFW  Created the code
//=============================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This public enumerated type defines the type of links used to reference
    /// other help topics referring to items in the documented assemblies.
    /// </summary>
    [Serializable]
    public enum ProjectLinkType
    {
        /// <summary>No active links.</summary>
        None,
        /// <summary>Local links within the project using anchor tags.  This
        /// is the default.  This type is compatible with HTML 1.x and HTML
        /// 2.x help files.</summary>
        Local,
        /// <summary>MS-Help style links for use within an HTML 2.x help
        /// file.</summary>
        Index
    }

    /// <summary>
    /// This public enumerated type defines the type of links used to reference
    /// other help topics referring to framework (SDK) help topics.
    /// </summary>
    [Serializable]
    public enum SdkLinkType
    {
        /// <summary>No active links.</summary>
        None,
        /// <summary>MS-Help style links for use within an HTML 2.x help
        /// file.</summary>
        Index,
        /// <summary>Links to framework topics on MSDN.  This is the
        /// default.  This type is compatible with HTML 1.x and HTML
        /// 2.x help files.</summary>
        Msdn
    }
}
