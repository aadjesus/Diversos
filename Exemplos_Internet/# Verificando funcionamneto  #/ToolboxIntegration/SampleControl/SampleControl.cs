// -----------------------------------------------------------------------
// <copyright file="SampleControl.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    #endregion

    /// <summary>
    /// Control used for demonstrating Visual Studio Toolbox integration.
    /// </summary>
    [ToolboxBitmap(typeof(SampleControl), "Resources.SampleControl.bmp")]
    [ToolboxItem(true)]
    public partial class SampleControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleControl" /> class.
        /// </summary>
        public SampleControl()
        {
            InitializeComponent();
        }
    }
}