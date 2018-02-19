// -----------------------------------------------------------------------
// <copyright file="ProvideToolboxControlAttribute.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    using Microsoft.VisualStudio.Shell;

    #endregion

    /// <summary>
    ///   This attribute adds a ToolboxControlsInstaller key for the assembly to install toolbox controls from the assembly
    /// </summary>
    /// <remarks>
    ///   For example
    ///   [$(Rootkey)\ToolboxControlsInstaller\$FullAssemblyName$]
    ///   "Codebase"="$path$"
    ///   "WpfControls"="1"
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    [ComVisible(false)]
    public sealed class ProvideToolboxControlAttribute : RegistrationAttribute
    {
        #region Private Constants

        private const string ToolboxControlsInstallerPath = "ToolboxControlsInstaller";

        #endregion

        #region Private Properties

        /// <summary>
        ///   Gets whether the toolbox controls are for WPF.
        /// </summary>
        private bool IsWpfControls
        {
            get;
            set;
        }

        /// <summary>
        ///   Gets the name for the controls
        /// </summary>
        private string Name
        {
            get;
            set;
        }

        #endregion

        #region Public Constructors

        /// <summary>
        ///   Creates a new ProvideToolboxControl attribute to register the assembly for toolbox controls installer
        /// </summary>
        /// <param name="isWpfControls"> </param>
        public ProvideToolboxControlAttribute(string name, bool isWpfControls)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }

            Name = name;
            IsWpfControls = isWpfControls;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Called to register this attribute with the given context.  The context
        ///   contains the location where the registration information should be placed.
        ///   It also contains other information such as the type being registered and path information.
        /// </summary>
        /// <param name="context"> Given context to register in </param>
        public override void Register(RegistrationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            using (Key key = context.CreateKey(String.Format(CultureInfo.InvariantCulture, "{0}\\{1}",
                ToolboxControlsInstallerPath,
                context.ComponentType.Assembly.FullName)))
            {
                key.SetValue(String.Empty, Name);
                key.SetValue("Codebase", context.CodeBase);
                if (IsWpfControls)
                {
                    key.SetValue("WPFControls", "1");
                }
            }
        }

        public override void Unregister(RegistrationContext context)
        {
            if (context != null)
            {
                context.RemoveKey(String.Format(CultureInfo.InvariantCulture, "{0}\\{1}",
                    ToolboxControlsInstallerPath,
                    context.ComponentType.AssemblyQualifiedName));
            }
        }

        #endregion
    }
}