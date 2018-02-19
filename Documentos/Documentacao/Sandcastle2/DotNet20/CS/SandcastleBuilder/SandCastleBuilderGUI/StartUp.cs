//=============================================================================
// System  : Sandcastle Help File Builder
// File    : StartUp.cs
// Author  : Eric Woodruff
// Updated : 08/10/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This application provides a GUI that is used to edit configuration files
// that can be used to build HTML documentation help files using Sandcastle.
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

using System;
using System.Windows.Forms;

using SandcastleBuilder.Properties;

namespace SandcastleBuilder
{
    /// <summary>
    /// This class contains the main entry point and other start up code.
    /// </summary>
    public sealed class StartUp
    {
        /// <summary>
        /// This class cannot be instantiated
        /// </summary>
        private StartUp()
        {
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        [STAThread]
        static void Main(string[] args)
        {
            string projectToLoad = (args.Length == 0) ? null : args[0];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Bring forward user preferences after a version update
            if(!Settings.Default.SettingsUpgraded)
            {
                Settings.Default.Upgrade();
                Settings.Default.SettingsUpgraded = true;
                Settings.Default.Save();
            }

            Application.Run(new MainForm(projectToLoad));
        }
    }
}
