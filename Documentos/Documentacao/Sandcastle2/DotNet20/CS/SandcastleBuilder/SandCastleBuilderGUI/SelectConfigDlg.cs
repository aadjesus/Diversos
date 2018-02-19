//=============================================================================
// System  : Sandcastle Help File Builder
// File    : SelectConfigDlg.cs
// Author  : Eric Woodruff
// Updated : 08/27/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to select the Visual Studio configuration
// from which to load the assembly and XML comment file information for a new
// help file builder project.
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
// 1.1.0.0  08/27/2006  EFW  Created the code
//=============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SandcastleBuilder
{
    /// <summary>
    /// This form is used to select the Visual Studio configuration from which
    /// to load the assembly and XML comment file information for a new help
    /// file builder project.
    /// </summary>
    public partial class SelectConfigDlg : Form
    {
        /// <summary>
        /// This returns the name of the selected configuration
        /// </summary>
        public string ConfigurationName
        {
            get { return (string)lbConfigs.SelectedItem; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="projectConfigs">The list of project configurations
        /// from which to choose.</param>
        public SelectConfigDlg(List<ProjectConfigItem> projectConfigs)
        {
            InitializeComponent();

            foreach(ProjectConfigItem cfg in projectConfigs)
                if(!lbConfigs.Items.Contains(cfg.ConfigurationName))
                    lbConfigs.Items.Add(cfg.ConfigurationName);

            lbConfigs.SelectedIndex = 0;
        }
    }
}
