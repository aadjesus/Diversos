// -----------------------------------------------------------------------
// <copyright file="SampleWpfControl.xaml.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System.Windows.Controls;

    #endregion

    /// <summary>
    ///   Control used for demonstrating Visual Studio Toolbox integration with VSIX package.
    /// </summary>
    [ProvideToolboxControl("SampleWpfControl", true)]
    public partial class SampleWpfControl : UserControl
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="SampleWpfControl" /> class.
        /// </summary>
        public SampleWpfControl()
        {
            InitializeComponent();
        }
    }
}