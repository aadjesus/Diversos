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
    public sealed class Checkin : CodeActivity
    {
        // The workspace that is used by the build
        [RequiredArgument]
        public InArgument<Workspace> Workspace { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the input arguments
            Workspace workspace = context.GetValue(this.Workspace);

            // Checks all files in in the workspace that have pending changes
            // The ***NO_CI*** comment ensures that the CI build is not triggered (and that
            // you end in an endless loop)
            workspace.CheckIn(workspace.GetPendingChanges(), "Build Agent", "***NO_CI***", 
                null, null, new PolicyOverrideInfo("Auto checkin", null), 
                CheckinOptions.SuppressEvent);
        }

    }
}
