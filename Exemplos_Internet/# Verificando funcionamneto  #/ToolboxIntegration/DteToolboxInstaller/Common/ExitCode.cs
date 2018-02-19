namespace ComponentOwl.ToolboxIntegration
{
    /// <summary>
    ///   Exit codes for the application.
    /// </summary>
    public enum ExitCode
    {
        /// <summary>
        ///   Application ended with success.
        /// </summary>
        Success = 0,

        /// <summary>
        ///   Cannot proceed because an instance of Visual Studio is running.
        /// </summary>
        VisualStudioRunning = 1,

        /// <summary>
        ///   Failed to install/uninstall component.
        /// </summary>
        Fail = 2
    }
}