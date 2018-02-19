//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : HelpFileFormat.cs
// Author  : Eric Woodruff
// Updated : 02/16/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the help file format.
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
// 1.3.0.0  09/02/2006  EFW  Added HelpFileFormat.Website support
// 1.4.0.0  02/16/2007  EFW  Added 1x + 2x and 1x + 2x + website combos
//=============================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This public enumerated type defines the type(s) of help file generated
    /// </summary>
    [Serializable, Flags]
    public enum HelpFileFormat
    {
        /// <summary>HTML Help 1.x format built with HHC.EXE</summary>
        HtmlHelp1x            = 0x0001,
        /// <summary>HTML Help 2.x format built with HXCOMP.EXE</summary>
        HtmlHelp2x            = 0x0002,
        /// <summary>A website with basic frame set viewer page</summary>
        Website               = 0x0004,

        /// <summary>An HTML Help 1.x and an HTML Help 2.x file</summary>
        Help1xAndHelp2x       = 0x0003,
        /// <summary>An HTML Help 1.x file and website</summary>
        Help1xAndWebsite      = 0x0005,
        /// <summary>An HTML Help 2.x file and website</summary>
        Help2xAndWebsite      = 0x0006,
        /// <summary>An HTML Help 1.x file, HTML Help 2.x file, and a
        /// website</summary>
        Help1xAnd2xAndWebsite = 0x0007
    }
}
