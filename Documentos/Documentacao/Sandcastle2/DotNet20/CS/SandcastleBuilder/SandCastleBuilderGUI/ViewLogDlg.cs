//=============================================================================
// System  : Sandcastle Help File Builder
// File    : ViewLog.cs
// Author  : Eric Woodruff
// Updated : 09/21/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to view the output of the last build in a
// window so that it is easier to review.
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
// 1.3.0.0  09/12/2006  EFW  Modified to load log text from the log file
//=============================================================================

using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace SandcastleBuilder
{
    /// <summary>
    /// This form is used to view the output from the last build so that it
    /// is easier to review.
    /// </summary>
    public partial class ViewLogDlg : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logFile">The log file to view</param>
        public ViewLogDlg(string logFile)
        {
            InitializeComponent();

            try
            {
                using(StreamReader sr = new StreamReader(logFile))
                {
                    txtOutput.Text = sr.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                MessageBox.Show(String.Format(CultureInfo.CurrentCulture,
                    "An error occurred trying to load the log file '{0}': {1}",
                    logFile, ex.Message), Constants.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
