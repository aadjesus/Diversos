//=============================================================================
// System  : Sandcastle Help File Builder Components
// File    : CodeBlockConfigDlg.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 03/09/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a form that is used to configure the settings for the
// Code Block Component.
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
// 1.3.3.0  11/24/2006  EFW  Created the code
// 1.4.0.0  02/12/2007  EFW  Added code block language filter option, default
//                           title option, and "Copy" image URL.
//=============================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace SandcastleBuilder.Components
{
    /// <summary>
    /// This form is used to configure the settings for the
    /// <see cref="CodeBlockComponent"/>.
    /// </summary>
    internal partial class CodeBlockConfigDlg : Form
    {
        private static string[] languages = { "none", "cs", "vbnet", "cpp",
            "c", "jscript", "vbscript", "xml" };

        private XmlDocument config;     // The configuration

        /// <summary>
        /// This is used to return the configuration information
        /// </summary>
        public string Configuration
        {
            get { return config.OuterXml; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentConfig">The current XML configuration
        /// XML fragment</param>
        public CodeBlockConfigDlg(string currentConfig)
        {
            XmlNode component, node;
            XmlAttribute attr;
            bool itemChecked;
            int tabSize;

            InitializeComponent();

            cboLanguage.SelectedIndex = 1;
            lnkCodePlexSHFB.Links[0].LinkData = "http://www.codeplex.com/SHFB";

            // Load the current settings
            config = new XmlDocument();
            config.LoadXml(currentConfig);

            component = config.SelectSingleNode("//component");
            node = component.SelectSingleNode("//basePath");

            if(node != null)
                txtBasePath.Text = node.Attributes["value"].Value;

            node = component.SelectSingleNode("//languageFilter");
            if(node != null)
            {
                attr = node.Attributes["value"];
                if(attr != null && Boolean.TryParse(attr.Value, out itemChecked))
                    chkLanguageFilter.Checked = itemChecked;
            }

            node = component.SelectSingleNode("//colorizer");
            txtSyntaxFile.Text = node.Attributes["syntaxFile"].Value;
            txtStyleFile.Text = node.Attributes["styleFile"].Value;
            txtCopyImageUrl.Text = node.Attributes["copyImageUrl"].Value;

            attr = node.Attributes["language"];
            if(attr != null)
                for(int i = 0; i < languages.Length; i++)
                    if(attr.Value == languages[i])
                    {
                        cboLanguage.SelectedIndex = i;
                        break;
                    }

            attr = node.Attributes["tabSize"];
            if(attr != null && Int32.TryParse(attr.Value, out tabSize))
                udcTabSize.Value = tabSize;

            attr = node.Attributes["numberLines"];
            if(attr != null && Boolean.TryParse(attr.Value, out itemChecked))
                chkNumberLines.Checked = itemChecked;

            attr = node.Attributes["outlining"];
            if(attr != null && Boolean.TryParse(attr.Value, out itemChecked))
                chkOutlining.Checked = itemChecked;

            attr = node.Attributes["defaultTitle"];
            if(attr != null && Boolean.TryParse(attr.Value, out itemChecked))
                chkDefaultTitle.Checked = itemChecked;
        }

        /// <summary>
        /// Close without saving
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Go to the CodePlex home page of the Sandcastle Help File Builder
        /// project.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void lnkCodePlexSHFB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start((string)e.Link.LinkData);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show("Unable to launch link target.  " +
                    "Reason: " + ex.Message, "Sandcastle Help File Builder",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Validate the configuration and save it
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            XmlAttribute attr;
            XmlNode component, node;
            bool isValid = true;

            txtBasePath.Text = txtBasePath.Text.Trim();
            txtSyntaxFile.Text = txtSyntaxFile.Text.Trim();
            txtStyleFile.Text = txtStyleFile.Text.Trim();
            txtCopyImageUrl.Text = txtCopyImageUrl.Text.Trim();
            epErrors.Clear();

            if(txtSyntaxFile.Text.Length == 0)
            {
                epErrors.SetError(txtSyntaxFile,
                    "The syntax filename is required");
                isValid = false;
            }

            if(txtStyleFile.Text.Length == 0)
            {
                epErrors.SetError(txtStyleFile,
                    "The XSLT style filename is required");
                isValid = false;
            }

            if(txtCopyImageUrl.Text.Length == 0)
            {
                epErrors.SetError(txtCopyImageUrl,
                    "The \"Copy\" image URL is required");
                isValid = false;
            }

            if(!isValid)
                return;

            // Store the changes
            component = config.SelectSingleNode("//component");
            node = component.SelectSingleNode("//basePath");
            if(node == null)
            {
                node = config.CreateNode(XmlNodeType.Element,
                    "basePath", null);
                component.AppendChild(node);

                attr = config.CreateAttribute("value");
                node.Attributes.Append(attr);
            }
            else
                attr = node.Attributes["value"];

            attr.Value = txtBasePath.Text;

            node = component.SelectSingleNode("//languageFilter");
            if(node == null)
            {
                node = config.CreateNode(XmlNodeType.Element,
                    "languageFilter", null);
                component.AppendChild(node);

                attr = config.CreateAttribute("value");
                node.Attributes.Append(attr);
            }
            else
                attr = node.Attributes["value"];

            attr.Value = chkLanguageFilter.Checked.ToString().ToLower(
                CultureInfo.InvariantCulture);

            node = component.SelectSingleNode("//colorizer");
            node.Attributes["syntaxFile"].Value = txtSyntaxFile.Text;
            node.Attributes["styleFile"].Value = txtStyleFile.Text;
            node.Attributes["copyImageUrl"].Value = txtCopyImageUrl.Text;

            attr = node.Attributes["language"];
            if(attr == null)
            {
                attr = config.CreateAttribute("language");
                node.Attributes.Append(attr);
            }

            attr.Value = languages[cboLanguage.SelectedIndex];

            attr = node.Attributes["tabSize"];
            if(attr == null)
            {
                attr = config.CreateAttribute("tabSize");
                node.Attributes.Append(attr);
            }

            attr.Value = udcTabSize.Value.ToString(
                CultureInfo.InvariantCulture);

            attr = node.Attributes["numberLines"];
            if(attr == null)
            {
                attr = config.CreateAttribute("numberLines");
                node.Attributes.Append(attr);
            }

            attr.Value = chkNumberLines.Checked.ToString().ToLower(
                CultureInfo.InvariantCulture);

            attr = node.Attributes["outlining"];
            if(attr == null)
            {
                attr = config.CreateAttribute("outlining");
                node.Attributes.Append(attr);
            }

            attr.Value = chkOutlining.Checked.ToString().ToLower(
                CultureInfo.InvariantCulture);

            attr = node.Attributes["defaultTitle"];
            if(attr == null)
            {
                attr = config.CreateAttribute("defaultTitle");
                node.Attributes.Append(attr);
            }

            attr.Value = chkDefaultTitle.Checked.ToString().ToLower(
                CultureInfo.InvariantCulture);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Select the base source folder
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select the base source folder";
                dlg.SelectedPath = Directory.GetCurrentDirectory();

                // If selected, set the new folder
                if(dlg.ShowDialog() == DialogResult.OK)
                    txtBasePath.Text = dlg.SelectedPath + @"\";
            }
        }

        /// <summary>
        /// Select the syntax for style file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void SelectFile_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            TextBox t;

            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                if(b == btnSelectSyntax)
                {
                    t = txtSyntaxFile;
                    dlg.Title = "Select the language syntax file";
                    dlg.Filter = "XML files (*.xml)|*.xml|" +
                        "All Files (*.*)|*.*";
                }
                else
                {
                    t = txtStyleFile;
                    dlg.Title = "Select the XSLT style transformation file";
                    dlg.Filter = "XSL files (*.xsl)|*.xsl|" +
                        "All Files (*.*)|*.*";
                }

                dlg.InitialDirectory = Directory.GetCurrentDirectory();

                // If selected, set the filename
                if(dlg.ShowDialog() == DialogResult.OK)
                    t.Text = dlg.FileName;
            }
        }
    }
}
