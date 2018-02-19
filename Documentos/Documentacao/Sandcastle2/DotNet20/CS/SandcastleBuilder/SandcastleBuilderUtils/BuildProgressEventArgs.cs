//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuildProgressEventArgs.cs
// Author  : Eric Woodruff
// Updated : 08/10/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the event arguments class for the build progress event
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
// 1.0.0.0  08/04/2006  EFW  Created the code
//=============================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This is a custom event arguments class for the
    /// <see cref="BuildProcess.BuildProgress"/> event.
    /// </summary>
    public class BuildProgressEventArgs : EventArgs
    {
        //=====================================================================
        // Private class members

        private BuildStep step;
        private string message;
        private bool hasCompleted;

        //=====================================================================
        // Properties

        /// <summary>
        /// Get the current build step of the build process
        /// </summary>
        public BuildStep BuildStep
        {
            get { return step; }
            internal set { step = value; }
        }

        /// <summary>
        /// Get the message associated with the progress report
        /// </summary>
        public string Message
        {
            get { return message; }
            internal set { message = value; }
        }

        /// <summary>
        /// Get a flag indicating whether or not the build has completed
        /// </summary>
        public bool HasCompleted
        {
            get { return hasCompleted; }
            internal set { hasCompleted = value; }
        }

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        public BuildProgressEventArgs()
        {
            message = String.Empty;
        }
    }
}
