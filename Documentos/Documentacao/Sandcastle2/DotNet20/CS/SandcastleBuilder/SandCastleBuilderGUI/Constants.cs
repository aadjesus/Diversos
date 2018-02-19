//=============================================================================
// System  : Sandcastle Help File Builder
// File    : Constants.cs
// Author  : Eric Woodruff
// Updated : 08/02/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains various constants for the application.
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
// 1.0.0.0  08/02/2006  EFW  Created the code
//=============================================================================

namespace SandcastleBuilder
{
    //========================================================================

    #region Application-wide constants
    /// <summary>
    /// This class holds a set of constants that define various application
    /// values such as the application name.
    /// </summary>
    public sealed class Constants
    {
        //=====================================================================

        /// <summary>The application name</summary>
        public const string AppName = "Sandcastle Help File Builder";

        // Can't create instances of the class.  You don't need to.
        private Constants()
        {
        }
    }
    #endregion
}
