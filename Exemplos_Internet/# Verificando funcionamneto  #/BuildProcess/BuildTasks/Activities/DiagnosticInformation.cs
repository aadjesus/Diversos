using System.Activities;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Workflow.Activities;

namespace BuildTasks.Activities
{

    [BuildActivity(HostEnvironmentOption.All)]
    public sealed class DiagnosticInformation : CodeActivity
    {
        public InArgument<string> TextIn { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string textIn = context.GetValue(this.TextIn);

            context.TrackBuildMessage("Message: " + textIn);
            context.TrackBuildMessage("Message (High importance): " + textIn, BuildMessageImportance.High);
            context.TrackBuildMessage("Message (Normal importance): " + textIn, BuildMessageImportance.Normal);
            context.TrackBuildMessage("Message (Low importance): " + textIn, BuildMessageImportance.Low);
            context.TrackBuildError(textIn);
            context.TrackBuildWarning(textIn);
        }
    }
}
