//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuilderException.cs
// Author  : Eric Woodruff
// Updated : 08/10/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the exception class for the help file builder
// applications.
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
using System.Runtime.Serialization;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This exception class is thrown by the application if it encounters an
    /// unrecoverable error.
    /// </summary>
    [Serializable]
    public class BuilderException : Exception
    {
        //=====================================================================
        // Methods

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <overloads>There are three overloads for the constructor</overloads>
        public BuilderException()
        {
        }

        /// <summary>
        /// This constructor takes a message string.
        /// </summary>
        /// <param name="message">The exception message</param>
        public BuilderException(string message) :
          base(message)
        {
        }

        /// <summary>
        /// This constructor takes a message string and an inner exception.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception</param>
        public BuilderException(string message, Exception inner) :
          base(message, inner)
        {
        }

        /// <summary>
        /// Deserialization constructor for use with
        /// <see cref="System.Runtime.Serialization.ISerializable"/>.
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context object</param>
        protected BuilderException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
