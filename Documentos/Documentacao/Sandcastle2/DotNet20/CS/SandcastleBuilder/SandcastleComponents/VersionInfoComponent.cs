//=============================================================================
// System  : Sandcastle Help File Builder Components
// File    : VersionInfoComponent.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 02/17/2007
// Note    : Copyright 2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a build component that is used to obtain version
// information for each topic so that it can be placed in the footer by the
// PostTransformComponent.
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
// 1.4.0.0  02/17/2007  EFW  Created the code
//=============================================================================

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.Ddue.Tools;

namespace SandcastleBuilder.Components
{
    /// <summary>
    /// This build component is is used to obtain version information for each
    /// topic so that it can be placed in the footer by the
    /// <see cref="PostTransformComponent"/>.
    /// </summary>
    /// <remarks>The <see cref="PostTransformComponent"/> adds the version
    /// information to the topic after it has been transformed into HTML.
    /// We need to get the version information here though as the reference
    /// information is lost once it has been transformed.</remarks>
    /// <example>
    /// <code lang="xml" title="Example configuration">
    /// &lt;!-- Version information component configuration.  This must
    ///      appear before the TransformComponent.  See also:
    ///      PostTransformComponent --&gt;
    /// &lt;component type="SandcastleBuilder.Components.VersionInfoComponent"
    ///   assembly="C:\SandcastleBuilder\SandcastleBuilder.Components.dll"&gt;
    ///     &lt;!-- Reflection information file for version info (required) --&gt;
    ///     &lt;reflectionFile filename="reflection.xml" /&gt;
    /// &lt;/component&gt;
    /// </code>
    /// </example>
    public class VersionInfoComponent : BuildComponent
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private static string itemVersion;
        private Dictionary<string, string> assemblyVersions;

        #endregion

        #region Properties

        /// <summary>
        /// This is used by the <see cref="PostTransformComponent"/> to get the
        /// version for the current topic.
        /// </summary>
        public static string ItemVersion
        {
            get { return itemVersion; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assembler">A reference to the build assembler.</param>
        /// <param name="configuration">The configuration information</param>
        /// <remarks>See the <see cref="VersionInfoComponent"/> class topic
        /// for an example of the configuration</remarks>
        /// <exception cref="ConfigurationErrorsException">This is thrown if
        /// an error is detected in the configuration.</exception>
        public VersionInfoComponent(BuildAssembler assembler,
          XPathNavigator configuration) : base(assembler, configuration)
        {
            XmlDocument assemblies;
            XmlNodeList asmList;
            XPathNavigator nav;

            string reflectionFilename, line;
            StringBuilder sb = new StringBuilder(1024);

            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);

            base.WriteMessage(MessageLevel.Info, String.Format(
                CultureInfo.InvariantCulture,
                "\r\n    [{0}, version {1}]\r\n    Version Information " +
                "Component. {2}\r\n    http://www.codeplex.com/SHFB",
                fvi.ProductName, fvi.ProductVersion, fvi.LegalCopyright));

            // The reflectionFile element is required.  The file must exist if
            // specified.  This is a hack to get version information into
            // each topic.  Note that it requires a modification to the
            // reference_content.xml file for each style.
            nav = configuration.SelectSingleNode("reflectionFile");
            if(nav == null)
                throw new ConfigurationErrorsException(
                    "The <reflectionFile> element is required for the " +
                    "VersionInfoComponent");

            reflectionFilename = nav.GetAttribute("filename", String.Empty);

            if(String.IsNullOrEmpty(reflectionFilename))
                throw new ConfigurationErrorsException("A filename is " +
                    "required in the <reflectionFile> element");

            if(!File.Exists(reflectionFilename))
                throw new ConfigurationErrorsException(
                    "The reflection file '" + reflectionFilename +
                    "' must exist");

            assemblyVersions = new Dictionary<string, string>();

            // Load the part of the reflection info file that contains the
            // assembly definitions.  The file itself can be huge so we
            // will use a stream reader to extract the assembly info and
            // load that into the XML document.
            using(StreamReader sr = new StreamReader(reflectionFilename))
            {
                do
                {
                    line = sr.ReadLine();
                    sb.Append(line);

                } while(!line.Contains("/assemblies"));
            }

            sb.Append("</reflection>");

            assemblies = new XmlDocument();
            assemblies.LoadXml(sb.ToString());

            asmList = assemblies.SelectNodes("//assemblies/assembly");

            foreach(XmlNode node in asmList)
                assemblyVersions.Add(node.Attributes["name"].Value,
                    node.Attributes["version"].Value);
        }
        #endregion

        #region Apply the component
        /// <summary>
        /// This is implemented to add the missing documentation tags
        /// </summary>
        /// <param name="document">The XML document with which to work.</param>
        /// <param name="key">The key (member name) of the item being
        /// documented.</param>
        public override void Apply(XmlDocument document, string key)
        {
            XmlNode assembly;

            itemVersion = null;

            // Project and namespace items don't have version info
            if(key[0] != 'R' && key[0] != 'N')
            {
                assembly = document.SelectSingleNode(
                    "//reference/containers/library/@assembly");

                if(assembly == null || !assemblyVersions.TryGetValue(
                  assembly.Value, out itemVersion))
                {
                    base.WriteMessage(MessageLevel.Warn, "Unable to find " +
                        "version information for " + key);
                    itemVersion = "?.?.?.?";
                }
            }
        }
        #endregion
    }
}
