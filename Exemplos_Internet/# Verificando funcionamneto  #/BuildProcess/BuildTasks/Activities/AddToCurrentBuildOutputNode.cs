using System.Activities;
using Microsoft.TeamFoundation.Build.Client;
using System;
using Microsoft.TeamFoundation.Build.Workflow.Services;

namespace BuildTasks.Activities
{

    [BuildActivity(HostEnvironmentOption.All)]
    public sealed class AddToCurrentBuildOutputNode : CodeActivity
    {
        [RequiredArgument]
        public InArgument<IBuildDetail> BuildDetail { get; set; }
 
        [RequiredArgument]
        public InArgument<string> DisplayText { get; set; }
 
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the input arguments            
            IBuildDetail buildDetail = context.GetValue(this.BuildDetail);
            string displayText = context.GetValue(this.DisplayText);

            // Get the current build output node
            IActivityTracking currentTracking = context.GetExtension<IBuildLoggingExtension>().GetActivityTracking(context);

            // Add a new node
            IBuildInformationNode childNode = currentTracking.Node.Children.CreateNode();
            childNode.Type = currentTracking.Node.Type;
            childNode.Fields.Add("DisplayText", "This text is displayed.");

            // Add a build step
            IBuildStep buildStep = childNode.Children.AddBuildStep("Custom Build Step", "This is my custom build step");
            buildStep.FinishTime = DateTime.Now.AddSeconds(10);
            buildStep.Status = BuildStepStatus.Succeeded;

            // Add an hyperlink
            childNode.Children.AddExternalLink("My link", new Uri("http://www.ewaldhofman.nl"));

            childNode.Save();
        }
    }
}
