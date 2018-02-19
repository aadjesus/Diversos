//=============================================================================
// System  : Sandcastle Help File Builder
// File    : ProjectConfigItem.cs
// Author  : Eric Woodruff
// Updated : 08/25/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the class used to represent a Visual Studio project
// configuration.
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
// 1.0.0.0  08/12/2006  EFW  Created the code
// 1.1.0.0  08/25/2006  EFW  Added support for Visual Studio projects
//=============================================================================

using System;

namespace SandcastleBuilder
{
    /// <summary>
    /// This represents a Visual Studio project configuration
    /// </summary>
    /// <remarks>This is used to select the documentation assemblies when
    /// creating a new project from a Visual Studio solution or project
    /// file.</remarks>
    public class ProjectConfigItem
    {
        //=====================================================================
        // Private data members

        string configName, assemblyFile, commentsFile;

        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to get the configuration name
        /// </summary>
        public string ConfigurationName
        {
            get { return configName; }
        }

        /// <summary>
        /// This is used to get the assembly filename
        /// </summary>
        public string AssemblyFile
        {
            get { return assemblyFile; }
        }

        /// <summary>
        /// This is used to get the XML comments filename
        /// </summary>
        public string XmlCommentsFile
        {
            get { return commentsFile; }
        }

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">The configuration name</param>
        /// <param name="assembly">The assembly filename</param>
        /// <param name="comments">The comments filename</param>
        public ProjectConfigItem(string configuration, string assembly,
           string comments)
        {
            configName = configuration;
            assemblyFile = assembly;
            commentsFile = comments;
        }
    }
}
