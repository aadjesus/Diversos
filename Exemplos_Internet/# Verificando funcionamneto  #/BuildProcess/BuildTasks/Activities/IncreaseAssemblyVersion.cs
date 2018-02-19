using System;
using System.Activities;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.Build.Client;

namespace BuildTasks.Activities
{
    [BuildActivity(HostEnvironmentOption.Agent)]
    public sealed class IncreaseAssemblyVersion : CodeActivity
    {
        // The file mask of all files for which the buildnumber of the 
        // AssemblyVersion must be increased
        [RequiredArgument]
        public InArgument<string> AssemblyInfoFileMask { get; set; }
        
        // The SourcesDirectory as initialized in the Build Process Template
        [RequiredArgument]
        public InArgument<string> SourcesDirectory { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the input arguments
            string sourcesDirectory = context.GetValue(this.SourcesDirectory);
            string assemblyInfoFileMask = context.GetValue(this.AssemblyInfoFileMask);
        
            // Enumerate over all version attributes
            foreach (string attribute in new string[] { "AssemblyVersion", "AssemblyFileVersion" })
            {
                // Define the regular expression to find (which is for example 'AssemblyVersion("1.0.0.0")' )
                Regex regex = new Regex(attribute + @"\(""\d+\.\d+\.\d+\.\d+""\)");
                // Get all AssemblyInfo files
                foreach (string file in Directory.EnumerateFiles(sourcesDirectory, assemblyInfoFileMask, SearchOption.AllDirectories))
                {
                    // Read the text from the AssemblyInfo file
                    string text = File.ReadAllText(file);
                    // Search for the first occurance of the version attribute
                    Match match = regex.Match(text);
                    // When found
                    if (match.Success)
                    {
                        // Retrieve the version number
                        string versionNumber = match.Value.Substring(attribute.Length + 2, match.Value.Length - attribute.Length - 4);
                        Version version = new Version(versionNumber);
                        // Increase the build number
                        Version newVersion = new Version(version.Major, version.Minor, version.Build + 1, version.Revision);
                        // Replace the version number 
                        string newText = regex.Replace(text, attribute + "(\"" + newVersion.ToString() + "\")");
                        // Write the new text in the AssemblyInfo file
                        File.WriteAllText(file, newText);
                    }
                }
            }
        }
    }
}
