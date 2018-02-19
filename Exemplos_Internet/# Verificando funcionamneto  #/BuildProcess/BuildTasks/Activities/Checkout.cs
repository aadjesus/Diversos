using System;
using System.Activities;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace BuildTasks.Activities
{
    [BuildActivity(HostEnvironmentOption.Agent)]
    public sealed class Checkout : CodeActivity
    {
        // The file mask of all files for which the buildnumber of the 
        // AssemblyVersion must be increased
        [RequiredArgument]
        public InArgument<string> AssemblyInfoFileMask { get; set; }

        // The workspace that is used by the build
        [RequiredArgument]
        public InArgument<Workspace> Workspace { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the input arguments
            string assemblyInfoFileMask = context.GetValue(this.AssemblyInfoFileMask);
            Workspace workspace = context.GetValue(this.Workspace);

            // Checks all files out in the workspace that apply to the file mask
            // For every workspace folder (mapping)
            foreach (var folder in workspace.Folders)
            {
                // Get the files that apply to the mask on the local system
                foreach (var file in Directory.GetFiles(folder.LocalItem, 
                    assemblyInfoFileMask, SearchOption.AllDirectories))
                {
                    // Check all those file out
                    workspace.PendEdit(file);
                }
            }
        }

    }
}
