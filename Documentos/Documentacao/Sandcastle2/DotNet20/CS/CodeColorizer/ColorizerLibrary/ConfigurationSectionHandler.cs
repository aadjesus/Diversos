//=============================================================================
// System  : Code Colorizer Library
// File    : CodeColorizer.cs
// Author  : Jonathan de Halleux, (c) 2003
// Updated : 11/17/2006
// Compiler: Microsoft Visual C#
//
// This is used to colorize blocks of code for output as HTML.  The original
// Code Project article by Jonathan can be found at:
// http://www.codeproject.com/csharp/highlightcs.asp.
//
//=============================================================================

using System;
using System.Configuration;
using System.Xml;

namespace ColorizerLibrary
{
	/// <summary>
	/// Configuration handler for code colorizer
	/// </summary>
	/// <remarks>Returns the XmlNode ColorizerLibrary of the Web.config</remarks>
	/// <remarks>Used in CodeColorizer to load its default configuration</remarks>
	public class ConfigurationSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>
		/// Returns the section parameter
		/// </summary>
		/// <param name="parent">The parent</param>
		/// <param name="configContext">The configuration context</param>
		/// <param name="section">The section</param>
		/// <returns>The configuration section</returns>
		public object Create(object parent, object configContext,
          XmlNode section)
		{
			return section;
		}
	}
}
