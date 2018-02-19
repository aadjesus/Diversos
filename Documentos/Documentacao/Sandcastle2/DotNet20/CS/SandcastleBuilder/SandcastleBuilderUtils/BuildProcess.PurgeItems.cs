//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuildProcess.PurgeItems.cs
// Author  : Eric Woodruff
// Updated : 01/21/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the code used to purge duplicate topics and other items
// from the Sandcastle reflection.org file based on the project's Visibility
// category properties.
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
// 1.0.0.0  08/05/2006  EFW  Created the code
// 1.3.1.1  10/05/2006  EFW  Implemented the Visibility category properties
// 1.3.3.1  12/11/2006  EFW  Added support for <exclude/>
//=============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace SandcastleBuilder.Utils
{
    partial class BuildProcess
    {
        #region Private data members

        // A list of members that need to be excluded from the reflection
        // information based on the visibility settings.
        private Dictionary<string, string> excludedMembers;

        private static Regex reExcludeElementEntry = new Regex(
            "<element api=\"([^\n\"]+?)\">.*?</element>|" +
            "<element api=\"([^\n\"]+?)\" />",
            RegexOptions.Singleline);

        private MatchEvaluator excludeElementEval;
        #endregion

        /// <summary>
        /// Purge duplicate topics from the reflection information file.
        /// </summary>
        /// <remarks>This is used to purge duplicate topics from the
        /// Sandcastle reflection.org file when building a help file from
        /// multiple assemblies that share common code that is compiled into
        /// each of them.  This prevents duplicate topics from being created
        /// in the help file table of contents and the namespace
        /// documentation.
        /// <p/>This problem was submitted as a bug report for the July CTP
        /// and, once fixed, this code can be removed as can the related
        /// project option.</remarks>
        protected void PurgeDuplicateTopics()
        {
            XmlNodeList elements;
            XmlNode node;
            int idx = 0, dupCount = 0;

            // The reflection file can contain tens of thousands of entries
            // for large assemblies.  Dictionary<K, T> is much faster at
            // lookups than List<T>.
            Dictionary<string, string> existingEntries =
                new Dictionary<string, string>();

            // There are much fewer namespaces so List<T> is okay for this
            List<string> namespaceEntries = new List<string>();

            this.ReportProgress(BuildStep.PurgeDuplicateTopics,
                "-------------------------------\r\n" +
                "Removing duplicate topics from reflection information file");

            try
            {
                // Find the API node list
                elements = reflectionInfo.SelectNodes("//apis/*");

                for(idx = 0; idx < elements.Count; idx++)
                {
                    // If we've already seen it, delete it.  We'll
                    // only keep the first reference regardless of
                    // its location.
                    if(existingEntries.ContainsKey(elements[idx].Attributes[
                      "id"].Value))
                    {
                        this.ReportProgress("Deleting duplicate topic '{0}'",
                            elements[idx].Attributes["id"].Value);
                        elements[idx].ParentNode.RemoveChild(elements[idx]);
                        dupCount++;

                        // Make a note of the name space as we'll have to
                        // remove the dups from it's elements too.
                        node = elements[idx].SelectSingleNode(
                            "containers/namespace");

                        if(!namespaceEntries.Contains(node.Attributes["api"].Value))
                            namespaceEntries.Add(node.Attributes["api"].Value);
                    }
                    else
                        existingEntries.Add(elements[idx].Attributes[
                            "id"].Value, null);
                }

                this.ReportProgress("    {0} duplicate topics removed",
                    dupCount);

                if(namespaceEntries.Count != 0)
                {
                    this.ReportProgress("Cleaning up namespace elements");

                    // Now remove duplicates from the namespace API elements
                    foreach(string ns in namespaceEntries)
                    {
                        elements = reflectionInfo.SelectNodes(
                            "//apis/api[@id='" + ns + "']/elements/*");

                        existingEntries.Clear();
                        dupCount = 0;

                        for(idx = 0; idx < elements.Count; idx++)
                            if(existingEntries.ContainsKey(
                              elements[idx].Attributes["api"].Value))
                            {
                                this.ReportProgress("Deleting duplicate " +
                                    "namespace entry '{0}'",
                                    elements[idx].Attributes["api"].Value);

                                elements[idx].ParentNode.RemoveChild(
                                    elements[idx]);
                                dupCount++;
                            }
                            else
                                existingEntries.Add(elements[idx].Attributes[
                                    "api"].Value, null);

                        this.ReportProgress(
                            "    {0} duplicates removed from {1}",
                            dupCount, ns);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new BuilderException("Error removing duplicate nodes: " +
                    ex.Message, ex);
            }
        }

        /// <summary>
        /// Apply the project's Visibility category properties to the
        /// reflection information file.
        /// </summary>
        /// <remarks>This is used to remove entries from the reflection
        /// information file so that it does not appear in the help file.
        /// See the <b>Document*</b> properties in the
        /// <see cref="SandcastleProject"/> class for information on the
        /// items removed.</remarks>
        protected void ApplyVisibilityProperties()
        {
            XmlNodeList apis;
            XmlNode api;
            MatchCollection matches;
            string xpath;

            this.ReportProgress(BuildStep.ApplyVisibilityProperties,
                "-------------------------------\r\n" +
                "Applying visibility properties to reflection information file");

            // The reflection file can contain tens of thousands of entries
            // for large assemblies.  Removing the elements at the time they
            // were found proved to be too slow for such large files.  As such,
            // we will get a list of all api and element entries to be
            // removed and we'll remove them using regular expressions and a
            // match evaluator at the end of this method.
            excludedMembers = new Dictionary<string, string>();

            try
            {
                // Remove members with an <exclude/> tag
                this.RemoveExcludedMembers(reflectionInfo);

                // Apply the visibility properties
                if(!project.DocumentAttributes)
                    this.RemoveAttributes(reflectionInfo);

                if(!project.DocumentExplicitInterfaceImplementations)
                    this.RemoveExplicitInterfaceImplementations(reflectionInfo);

                // Removal of inherited members implies Framework members too
                if(!project.DocumentInheritedMembers ||
                  !project.DocumentInheritedFrameworkMembers)
                    this.RemoveInheritedMembers(reflectionInfo,
                        project.DocumentInheritedMembers);

                // If both privates and internals are not shown, they won't be
                // there as the /interal+ parameter will not have been used.
                // If privates are omitted, private fields are omitted by
                // default too.
                if(project.DocumentPrivates || project.DocumentInternals)
                {
                    if(!project.DocumentPrivates)
                    {
                        // Remove all private members excluding EII entries
                        // which are controlled by the Document EII property.
                        this.RemoveMembers(reflectionInfo, "//apis/api[" +
                            "memberdata/@visibility='private' and (" +
                            "not(proceduredata) or " +
                            "proceduredata/@virtual='false')]",
                            "private members");
                    }
                    else
                    {
                        if(!project.DocumentPrivateFields)
                        {
                            this.RemoveMembers(reflectionInfo,
                                "//apis/api[apidata/@subgroup='field' and " +
                                "memberdata/@visibility='private']",
                                "private fields");

                            // This will get rid of a load of base framework
                            // class fields too.
                            this.RemoveMembers(reflectionInfo,
                                "//apis/api/elements/element[starts-with(" +
                                "@api, 'F:System.') or starts-with(@api, " +
                                "'F:Microsoft.')]", "private framework fields");
                        }
                        else
                        {
                            // Remove backing fields that correspond to events.
                            // These can never be documented and should not
                            // show up.
                            this.RemoveMembers(reflectionInfo,
                                "//apis/api[apidata/@subgroup='field' and " +
                                "//apis/api/@id=concat('E', substring(@id,2))]",
                                "event backer fields");
                        }

                        if(!project.DocumentInternals)
                            this.RemoveMembers(reflectionInfo,
                                "//apis/api[memberdata/@visibility='assembly' " +
                                "or memberdata/@visibility='family and " +
                                "assembly']", "internal members");
                    }
                }

                if(!project.DocumentProtected)
                {
                    xpath = "//apis/api[memberdata/@visibility='family'";

                    // If DocumentInternals is false, remove protected internal
                    // members as well. If not, leave them alone.
                    if(!project.DocumentInternals)
                        xpath += " or memberdata/@visibility='family or " +
                            "assembly'";

                    xpath += "]";

                    this.RemoveMembers(reflectionInfo, xpath,
                        "protected members");
                }
                else
                {
                    if(project.DocumentProtectedInternalAsProtected)
                        this.ModifyProtectedInternalVisibility(reflectionInfo);

                    if(!project.DocumentSealedProtected)
                        this.RemoveSealedProtected(reflectionInfo);
                }

                // Now remove unwanted members if any were found
                if(excludedMembers.Count != 0)
                {
                    this.ReportProgress("    Removing previously noted " +
                        "unwanted APIs and elements");
                    apis = reflectionInfo.SelectNodes("//apis/api");

                    for(int idx = 0; idx < apis.Count; idx++)
                    {
                        api = apis[idx];

                        if(!excludedMembers.ContainsKey(api.Attributes["id"].Value))
                        {
                            // An XPath sub-query for the <element> entries is
                            // way to slow especially on very large files so
                            // we'll use a regular expression and a match
                            // evaluator to get rid of unwanted <element>
                            // entries.
                            matches = reExcludeElementEntry.Matches(api.InnerXml);

                            // However, just doing a straight replace consumes
                            // a large amount of memory.  As such, check the
                            // results for those that contain an unwanted
                            // element and only do it when really necessary.
                            foreach(Match m in matches)
                                if(excludedMembers.ContainsKey(m.Groups[1].Value) ||
                                  excludedMembers.ContainsKey(m.Groups[2].Value))
                                {
                                    api.InnerXml = reExcludeElementEntry.Replace(
                                        api.InnerXml, excludeElementEval);
                                    break;
                                }
                        }
                        else
                            api.ParentNode.RemoveChild(api);
                    }

                    excludedMembers = null;
                }
            }
            catch(BuilderException )
            {
                // If it's from one of the other methods, just rethrow it
                throw;
            }
            catch(Exception ex)
            {
                throw new BuilderException(
                    "Error applying Visibility properties: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Remove attribute information
        /// </summary>
        /// <param name="sourceFile">The document from which to remove the
        /// attributes.</param>
        private void RemoveAttributes(XmlDocument sourceFile)
        {
            XmlNodeList elements;
            XmlNode typeNode, parent;
            string attrName;
            int idx;

            try
            {
                // Find all of the attribute nodes
                elements = sourceFile.SelectNodes(
                    "//apis/api/attributes/attribute");

                for(idx = 0; idx < elements.Count; idx++)
                {
                    typeNode = elements[idx].SelectSingleNode("type");

                    if(typeNode != null)
                        attrName = typeNode.Attributes["api"].Value;
                    else
                        attrName = String.Empty;

                    // FlagsAttribute, ObsoleteAttribute, and
                    // SerializableAttribute are always kept as they provide
                    // useful information.  SerializableAttribute is an
                    // attribute in the apis/api/typedata node rather than
                    // an actual attribute entry so we can ignore it here.
                    if(!attrName.EndsWith("FlagsAttribute") &&
                      !attrName.EndsWith("ObsoleteAttribute"))
                    {
                        parent = elements[idx].ParentNode;
                        parent.RemoveChild(elements[idx]);

                        // Remove the parent if it is empty
                        if(parent.ChildNodes.Count == 0)
                            parent.ParentNode.RemoveChild(parent);
                    }
                }

                this.ReportProgress("    {0} attribute nodes removed",
                    elements.Count);
            }
            catch(Exception ex)
            {
                throw new BuilderException("Error removing attributes nodes: " +
                    ex.Message, ex);
            }
        }

        /// <summary>
        /// Remove explicit interface implementation information
        /// </summary>
        /// <param name="sourceFile">The document from which to remove the
        /// implementations.</param>
        private void RemoveExplicitInterfaceImplementations(
          XmlDocument sourceFile)
        {
            XmlNodeList elements;
            int idx;

            try
            {
                // Find all explicit implementations in the documented assemblies
                elements = sourceFile.SelectNodes("//apis/api[" +
                    "memberdata/@visibility='private' and " +
                    "proceduredata/@virtual='true']");

                for(idx = 0; idx < elements.Count; idx++)
                    elements[idx].ParentNode.RemoveChild(elements[idx]);

                this.ReportProgress("    {0} explicit interface " +
                    "implementations removed", elements.Count);

                // Get rid of the matching element entries and those for base
                // classes too.  This ensures they don't show up in the
                // "class members" help page.
                elements = sourceFile.SelectNodes("//apis/api/elements/" +
                    "element[contains(@api, '#') and " +
                    "not(contains(@api, '.#ctor')) and " +
                    "not(contains(@api, '.#cctor'))]");

                for(idx = 0; idx < elements.Count; idx++)
                    elements[idx].ParentNode.RemoveChild(elements[idx]);

                this.ReportProgress("    {0} local and base class EII " +
                    "elements removed", elements.Count);
            }
            catch(Exception ex)
            {
                throw new BuilderException("Error removing EII nodes: " +
                    ex.Message, ex);
            }
        }

        /// <summary>
        /// Remove inherited members information from each type's element list
        /// </summary>
        /// <param name="sourceFile">The document from which to remove the
        /// inherited members.</param>
        /// <param name="frameworkOnly">True to only remove framework members
        /// or false to remove all inherited members.</param>
        private void RemoveInheritedMembers(XmlDocument sourceFile,
          bool frameworkOnly)
        {
            XmlNodeList types, elements;
            int typeIdx, elementIdx, removed = 0;
            string typeName;

            try
            {
                // Find all types
                types = sourceFile.SelectNodes(
                    "//apis/api[starts-with(@id, 'T:')]");

                for(typeIdx = 0; typeIdx < types.Count; typeIdx++)
                {
                    // Only remove inherited Framework members?
                    if(frameworkOnly)
                        elements = types[typeIdx].SelectNodes(
                            "elements/element[starts-with(substring-after(" +
                            "@api,':'),'System.') or starts-with(" +
                            "substring-after(@api,':'),'Microsoft.')]");
                    else
                    {
                        // Get rid of any member not starting with this
                        // entry's type.
                        typeName = types[typeIdx].Attributes[
                            "id"].Value.Substring(2);
                        elements = types[typeIdx].SelectNodes(
                            "elements/element[not(starts-with(" +
                            "substring-after(@api,':'),'" + typeName + "'))]");
                    }

                    for(elementIdx = 0; elementIdx < elements.Count; elementIdx++)
                    {
                        elements[elementIdx].ParentNode.RemoveChild(
                            elements[elementIdx]);
                        removed++;
                    }
                }

                this.ReportProgress("    {0} inherited member elements removed",
                    removed);
            }
            catch(Exception ex)
            {
                throw new BuilderException("Error removing inherited members: " +
                    ex.Message, ex);
            }
        }

        /// <summary>
        /// Remove member information matching the specified XPath query.
        /// </summary>
        /// <param name="sourceFile">The document from which to remove the
        /// members.</param>
        /// <param name="xpath">The XPath query used to find the members.</param>
        /// <param name="memberDesc">A description of the members removed.</param>
        /// <returns>The number of members to be removed</returns>
        /// <remarks>Actual removal of the members is deferred.  On very large
        /// files, the XPath queries took to long when removing the
        /// &lt;elemen&gt; members.</remarks>
        private int RemoveMembers(XmlDocument sourceFile, string xpath,
          string memberDesc)
        {
            XmlNodeList apis;
            int count = 0;
            string id;

            try
            {
                // Find all matching members
                apis = sourceFile.SelectNodes(xpath);

                foreach(XmlNode node in apis)
                {
                    id = node.Attributes["id"].Value;
 
                    if(id != null && !excludedMembers.ContainsKey(id))
                    {
                        excludedMembers.Add(id, null);
                        count++;
                    }
                }

                if(!String.IsNullOrEmpty(memberDesc))
                    this.ReportProgress("    {0} {1} noted for removal", count,
                        memberDesc);
            }
            catch(Exception ex)
            {
                throw new BuilderException(String.Format(
                    CultureInfo.CurrentCulture, "Error removing {0}: {1}",
                    memberDesc, ex.Message), ex);
            }

            return count;
        }

        /// <summary>
        /// Change the visibility of "protected internal" members to
        /// "protected".
        /// </summary>
        /// <param name="sourceFile">The document in which to make the
        /// change.</param>
        private void ModifyProtectedInternalVisibility(XmlDocument sourceFile)
        {
            XmlNodeList members;

            try
            {
                // Find all matching members
                members = sourceFile.SelectNodes("//apis/api/memberdata[" +
                    "@visibility='family or assembly']");

                foreach(XmlNode n in members)
                    n.Attributes["visibility"].Value = "family";

                this.ReportProgress("    {0} 'protected internal' members " +
                    "changed to 'protected'", members.Count);
            }
            catch(Exception ex)
            {
                throw new BuilderException(
                    String.Format(CultureInfo.CurrentCulture,
                    "Error changed protected internal to protected: {0}",
                    ex.Message), ex);
            }
        }

        /// <summary>
        /// Remove protected members from sealed classes.
        /// </summary>
        /// <param name="sourceFile">The document from which to remove the
        /// members</param>
        private void RemoveSealedProtected(XmlDocument sourceFile)
        {
            XmlNodeList elements;
            XmlNode member;
            int elementIdx, removed = 0;
            string elementType, memberType;

            try
            {
                // Find all matching members
                elements = sourceFile.SelectNodes("//apis/api[" +
                    "apidata/@subgroup !='enumeration' and " +
                    "typedata/@sealed = 'true']/elements/*");

                for(elementIdx = 0; elementIdx < elements.Count; elementIdx++)
                {
                    member = sourceFile.SelectSingleNode("//apis/api[@id='" +
                        elements[elementIdx].Attributes["api"].Value +
                        "' and (memberdata/@visibility='family' or " +
                        "memberdata/@visibility='family or assembly')]");

                    if(member != null)
                    {
                        // If the type matches, delete the member too
                        elementType = elements[
                            elementIdx].ParentNode.ParentNode.Attributes[
                            "id"].Value.Substring(2);
                        memberType = member.Attributes["id"].Value.Substring(2);
                        memberType = memberType.Substring(0,
                            memberType.LastIndexOf('.'));

                        if(memberType == elementType)
                            member.ParentNode.RemoveChild(member);

                        elements[elementIdx].ParentNode.RemoveChild(
                            elements[elementIdx]);
                        removed++;
                    }
                }

                this.ReportProgress("    {0} protected members removed from " +
                    "sealed classes", removed);
            }
            catch(Exception ex)
            {
                throw new BuilderException(
                    String.Format(CultureInfo.CurrentCulture,
                    "Error removing protected members from sealed classes: {0}",
                    ex.Message), ex);
            }
        }

        /// <summary>
        /// This is used to remove members from the reflection information file
        /// that have an <b>&lt;exclude&gt;</b> tag in their comments.
        /// </summary>
        /// <param name="sourceFile">The document in which to make the
        /// change.</param>
        /// <remarks>This method may eventually go away in favor of using the
        /// namespace ripping feature of MRefBuilder.  However, as of the
        /// December 2006 CTP, that feature is currently broken and is
        /// unusable.  As such, we'll do it the hard way.</remarks>
        private void RemoveExcludedMembers(XmlDocument sourceFile)
        {
            XmlNodeList excludedMembers;
            string name;

            foreach(XmlCommentsFile comments in commentsFiles)
            {
                excludedMembers = comments.Comments.SelectNodes("//exclude/..");

                foreach(XmlNode member in excludedMembers)
                {
                    // It should appear at the same level as <summary> so that
                    // we can find the member name in the parent node.
                    if(member.Attributes["name"] == null)
                    {
                        this.ReportProgress("    Incorrect placement of " +
                            "<exclude/> tag.  Unable to locate member name.");
                        continue;
                    }

                    name = member.Attributes["name"].Value;

                    // Namespace exclusions are handled later
                    if(name[0] != 'N')
                        if(name[0] == 'T')
                            this.RemoveType(sourceFile, name);
                        else
                            if(this.RemoveMembers(sourceFile,
                              "//apis/api[@id='" + name + "']", null) != 0)
                                this.ReportProgress("    Excluded member {0} " +
                                    "noted for removal", name);
                }
            }
        }

        /// <summary>
        /// Remove all elements for a type
        /// </summary>
        /// <param name="sourceFile">The document in which to make the
        /// change.</param>
        /// <param name="id">The ID of the element to remove</param>
        private void RemoveType(XmlDocument sourceFile, string id)
        {
            XmlNodeList members;
            string apiName;
            int count = 0;

            // Remove an entire type
            members = sourceFile.SelectNodes("//apis/api[@id='" + id +
                "']/elements/*");

            foreach(XmlNode member in members)
            {
                apiName = member.Attributes["api"].Value;

                // Remove an entire type from a namespace?
                if(apiName[0] == 'T')
                    this.RemoveType(sourceFile, apiName);

                count += this.RemoveMembers(sourceFile, "//apis/api[@id='" +
                    apiName + "']", null);
            }

            // And finally, remove the type itself
            count += this.RemoveMembers(sourceFile, "//apis/api[@id='" +
                id + "']", null);

            if(count != 0)
                this.ReportProgress("    Excluded type {0} and all of its " +
                    "members noted for removal", id);
        }

        /// <summary>
        /// This is used as the match evaluator for the regular expression
        /// that finds the &lt;element&gt; entries to remove from the
        /// reflection information file.
        /// </summary>
        /// <param name="m">The match found</param>
        /// <returns>The string with which to replace the match</returns>
        /// <remarks>The removals are done this way as it proved to be a very
        /// slow process to remove the child elements at the time they
        /// were found with an XPath query on very large files.</remarks>
        private string OnExcludeElement(Match m)
        {
            if(excludedMembers.ContainsKey(m.Groups[1].Value) ||
              excludedMembers.ContainsKey(m.Groups[2].Value))
                return String.Empty;

            return m.Value;
        }
    }
}
