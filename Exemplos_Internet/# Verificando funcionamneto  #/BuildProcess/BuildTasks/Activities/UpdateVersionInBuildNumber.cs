using System.Activities;
using Microsoft.TeamFoundation.Build.Client;

namespace BuildTasks.Activities
{
    [BuildActivity(HostEnvironmentOption.Controller)]
    public sealed class UpdateVersionInBuildNumber : CodeActivity<string>
    {
        [RequiredArgument]
        public InArgument<string> BuildNumberFormat { get; set; }

        [RequiredArgument]
        public InArgument<string> VersionNumber { get; set; }

        protected override string Execute(CodeActivityContext context)
        {
            string buildNumberFormat = context.GetValue<string>(this.BuildNumberFormat);
            string versionNumber = context.GetValue<string>(this.VersionNumber);

            return buildNumberFormat.Replace("$(Version)", versionNumber);
        }
    }
}
