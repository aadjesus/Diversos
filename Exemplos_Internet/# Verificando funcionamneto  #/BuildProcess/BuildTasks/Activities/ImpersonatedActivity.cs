using System.Activities;
using BuildTasks.CustomType;
using BuildTasks.Library;
using Microsoft.TeamFoundation.Build.Client;

namespace BuildTasks.Activities
{
    [BuildActivity(HostEnvironmentOption.Agent)]
    public class CopyFile : CodeActivity
    {

        public InArgument<Credential> Credentials { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            using (Impersonation impersonation = new Impersonation(context.GetValue(this.Credentials)))
            {
                // Insert your activity code over here
            }
        }
    }
}
