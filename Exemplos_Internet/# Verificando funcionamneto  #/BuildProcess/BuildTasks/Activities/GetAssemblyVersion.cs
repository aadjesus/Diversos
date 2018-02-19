using System;
using System.Activities;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Workflow.Activities;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace BuildTasks.Activities
{
    [BuildActivity(HostEnvironmentOption.Controller)]
    public sealed class GetAssemblyVersion : CodeActivity<string>
    {
        [RequiredArgument]
        public InArgument<string> AssemblyInfoFileMask { get; set; }

        [RequiredArgument]
        public InArgument<IBuildDetail> BuildDetail { get; set; }

        protected override string Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the input arguments
            string assemblyInfoFileMask = context.GetValue(this.AssemblyInfoFileMask);
            IBuildDetail buildDetail = context.GetValue(this.BuildDetail);

            var workspace = buildDetail.BuildDefinition.Workspace;
            var vc = buildDetail.BuildServer.TeamProjectCollection.GetService<VersionControlServer>();

            string attribute = "AssemblyFileVersion";

            // Define the regular expression to find (which is for example 'AssemblyFileVersion("1.0.0.0")' )
            Regex regex = new Regex(attribute + @"\(""\d+\.\d+\.\d+\.\d+""\)");

            // For every workspace folder (mapping)
            foreach (var folder in workspace.Mappings)
            {
                // Get all files (recursively) that apply to the file mask
                ItemSet itemSet = vc.GetItems(folder.ServerItem + "//" + assemblyInfoFileMask, RecursionType.Full);
                foreach (Item item in itemSet.Items)
                {
                    context.TrackBuildMessage(string.Format("Download {0}", item.ServerItem));
                    
                    // Download the file
                    string localFile = Path.GetTempFileName();
                    item.DownloadFile(localFile);

                    // Read the text from the AssemblyInfo file
                    string text = File.ReadAllText(localFile);
                    // Search for the first occurrence of the version attribute
                    Match match = regex.Match(text);
                    // When found
                    if (match.Success)
                    {
                        // Retrieve the version number
                        string versionNumber = match.Value.Substring(attribute.Length + 2, match.Value.Length - attribute.Length - 4);
                        Version version = new Version(versionNumber);
                        // Increase the build number -> this will be the new version number for the build
                        Version newVersion = new Version(version.Major, version.Minor, version.Build + 1, version.Revision);

                        context.TrackBuildMessage(string.Format("Version found {0}", newVersion));

                        return newVersion.ToString();
                    }
                }
            }

            return "No version found";
        }
    }
}