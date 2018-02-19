//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : ComponentConfigurationEditorDlg.cs
// Author  : Eric Woodruff
// Updated : 01/20/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to edit the component configurations.
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
// 1.3.3.0  11/12/2006  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This is used to edit the component configurations.
    /// </summary>
    /// <remarks>To be editable, the component must be a third party
    /// component with a unique ID in the help file builder's
    /// <b>sandcastle.config</b> file.  In addition, the component must
    /// expose a public <b>ConfigureComponent</b> method that accepts
    /// a string containing the current XML configuration information and
    /// returns a string containing the modified XML configuration
    /// information.</remarks>
    public partial class ComponentConfigurationEditorDlg : Form
    {
        #region Private data members
        private static string sandcastlePath;     // Sandcastle folder

        private string shfbFolder;      // The help file builder folder

        // The current configurations
        private ComponentConfigurationDictionary currentConfigs;

        // The configuration file with the default settings
        private XmlDocument configFile;

        // The bold font for items with configurations
        private Font boldFont;
        #endregion

        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to set or get the Sandcastle installation folder
        /// </summary>
        public static string SandcastlePath
        {
            get { return sandcastlePath; }
            set
            {
                if(String.IsNullOrEmpty(value))
                    sandcastlePath = null;
                else
                    sandcastlePath = value;
            }
        }

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs">The current configurations</param>
        internal ComponentConfigurationEditorDlg(
          ComponentConfigurationDictionary configs)
        {
            string id;

            InitializeComponent();

            // Figure out where Sandcastle is if not done specified
            if(sandcastlePath == null)
            {
                Match m = Regex.Match(Environment.GetEnvironmentVariable("PATH"),
                    @"[A-Z]:\\.[^;]+\\Sandcastle(?=\\Prod)",
                    RegexOptions.IgnoreCase);

                // If not found in the path, search all fixed drives
                if(m.Success)
                    sandcastlePath = m.Value;
                else
                {
                    sandcastlePath = BuildProcess.FindOnFixedDrives(
                        @"\Sandcastle");

                    // If not found there, try the VS 2005 SDK folders
                    if(sandcastlePath.Length == 0)
                    {
                        sandcastlePath = BuildProcess.FindSdkExecutable(
                            "MRefBuilder.exe");

                        if(sandcastlePath.Length != 0)
                            sandcastlePath = sandcastlePath.Substring(0,
                                sandcastlePath.LastIndexOf('\\'));
                        else
                            MessageBox.Show("Unable to locate Sandcastle " +
                                "installation folder.  Please set the " +
                                "SandcastlePath project property.",
                                "Sandcastle Help File Builder",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            boldFont = new Font(this.Font, FontStyle.Bold);

            currentConfigs = configs;

            Assembly asm = Assembly.GetEntryAssembly();
            shfbFolder = Path.GetDirectoryName(asm.Location) + @"\";

            configFile = new XmlDocument();
            configFile.Load(shfbFolder + @"Templates\sandcastle.config");

            // Get all configurable items (those with an ID)
            foreach(XmlNode n in configFile.SelectNodes(
              "//component[string-length(@id) != 0]"))
            {
                id = n.Attributes["id"].Value;

                if(!lbComponents.Items.Contains(id))
                    lbComponents.Items.Add(id);
                else
                    MessageBox.Show("The component with ID '" +
                        id + "' is defined more than once.  Duplicates " +
                        "will be ignored.", "Sandcastle Help File Builder",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(lbComponents.Items.Count != 0)
                lbComponents.SelectedIndex = 0;
            else
            {
                MessageBox.Show("No configurable components found",
                    "Sandcastle Help File Builder", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                btnConfigure.Enabled = false;
            }
        }

        /// <summary>
        /// Close this form
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Try to load the selected component assembly and edit the
        /// specified component.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnConfigure_Click(object sender, EventArgs e)
        {
            string id, assembly, type, currentConfig, newConfig;

            id = (string)lbComponents.SelectedItem;

            XmlNode componentNode = configFile.SelectSingleNode(
                "//component[@id='" + id + "']");
            assembly = componentNode.Attributes["assembly"].Value;
            type = componentNode.Attributes["type"].Value;

            if(!currentConfigs.TryGetValue(id, out currentConfig))
                currentConfig = componentNode.OuterXml;

            // Replace the builder path and any environment variables
            assembly = assembly.Replace("{@SHFBFolder}", shfbFolder);
            assembly = Environment.ExpandEnvironmentVariables(assembly);

            try
            {
                // Change into the component's folder so that it has a
                // better chance of finding all of its dependencies.
                Directory.SetCurrentDirectory(Path.GetDirectoryName(
                    Path.GetFullPath(assembly)));

                // The exception is BuildAssemblerLibrary.dll which is in
                // the Sandcastle installation folder.  We'll have to resolve
                // that one manually.
                AppDomain.CurrentDomain.AssemblyResolve +=
                    new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                // Load the type and get a reference to the static
                // configuration method.
                Assembly asm = Assembly.LoadFrom(assembly);
                Type component = asm.GetType(type);
                MethodInfo mi = null;

                if(component != null)
                    mi = component.GetMethod("ConfigureComponent",
                        BindingFlags.Public | BindingFlags.Static);

                if(component != null && mi != null)
                {
                    // Invoke the method to let it configure the settings
                    newConfig = (string)mi.Invoke(null, new object[] {
                        currentConfig });

                    // Only store it if new or if it changed
                    if(currentConfig != newConfig)
                    {
                        currentConfigs[id] = newConfig;
                        currentConfigs.OnDictionaryChanged(
                            new ListChangedEventArgs(
                                ListChangedType.ItemChanged, -1));

                        lbComponents.Invalidate();
                        lbComponents.Update();
                    }
                }
                else
                    MessageBox.Show(String.Format(CultureInfo.InvariantCulture,
                        "Unable to locate the 'ConfigureComponent' method in " +
                        "component '{0}' in assembly {1}", type, assembly),
                        "Sandcastle Help File Builder", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            catch(IOException ioEx)
            {
                System.Diagnostics.Debug.WriteLine(ioEx.ToString());
                MessageBox.Show(String.Format(CultureInfo.InvariantCulture,
                    "A file access error occurred trying to configure the " +
                    "component '{0}'.  Error: {1}", type, ioEx.Message),
                    "Sandcastle Help File Builder", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show(String.Format(CultureInfo.InvariantCulture,
                    "An error occurred trying to configure the component " +
                    "'{0}'.  Error: {1}", type, ex.Message),
                    "Sandcastle Help File Builder", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This is handled to resolve dependent assemblies and load them
        /// when necessary.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The event arguments</param>
        /// <returns>The loaded assembly</returns>
        private Assembly CurrentDomain_AssemblyResolve(object sender,
          ResolveEventArgs args)
        {
            Assembly asm = null;

            string[] nameInfo = args.Name.Split(new char[] { ',' });
            string resolveName = nameInfo[0];

            // See if it has already been loaded
            foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                if(args.Name == a.FullName)
                {
                    asm = a;
                    break;
                }

            // Is it a Sandcastle component?
            if(asm == null)
            {
                string[] files = Directory.GetFiles(sandcastlePath, "*.dll",
                    SearchOption.AllDirectories);

                foreach(string file in files)
                    if(resolveName == Path.GetFileNameWithoutExtension(file))
                    {
                        asm = Assembly.LoadFile(file);
                        break;
                    }
            }

            if(asm == null)
                throw new BuilderException("Unable to resolve reference to " +
                    args.Name);

            return asm;
        }

        /// <summary>
        /// Reset the configuration of the selected component
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            string id = (string)lbComponents.SelectedItem;

            if(currentConfigs.ContainsKey(id))
            {
                currentConfigs.Remove(id);
                currentConfigs.OnDictionaryChanged(
                    new ListChangedEventArgs(
                        ListChangedType.ItemDeleted, -1));

                lbComponents.Invalidate();
                lbComponents.Update();
            }
        }

        /// <summary>
        /// Show components with a configuration in bold
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event argument</param>
        private void lbComponents_DrawItem(object sender, DrawItemEventArgs e)
        {
            if(e.Index >= 0)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.Bounds;

                string label = (string)lbComponents.Items[e.Index];
                Font font = (currentConfigs.ContainsKey(label)) ? boldFont :
                    e.Font;

                if((e.State & DrawItemState.Selected) == 0)
                {
                    g.FillRectangle(SystemBrushes.Window, r);
                    g.DrawString(label, font, SystemBrushes.WindowText, r);
                }
                else
                {
                    g.FillRectangle(SystemBrushes.Highlight, r);
                    g.DrawString(label, font, SystemBrushes.HighlightText, r);
                }

                if((e.State & DrawItemState.Focus) != 0)
                    e.DrawFocusRectangle();
            }
        }

        /// <summary>
        /// Returns the size of the items in the list box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event argument</param>
        private void lbComponents_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = boldFont.Height + 5;
        }
    }
}
