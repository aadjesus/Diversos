using System.Activities;
using Microsoft.TeamFoundation.Build.Client;
using System;

namespace BuildTasks.Activities
{

    [BuildActivity(HostEnvironmentOption.All)]
    public sealed class AddHyperlinkToBuildOutput : CodeActivity
    {
        [RequiredArgument]
        public InArgument<IBuildDetail> BuildDetail { get; set; }
 
        [RequiredArgument]
        public InArgument<string> DisplayText { get; set; }
 
        [RequiredArgument]
        public InArgument<string> Url { get; set; }
 
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the input arguments            
            IBuildDetail buildDetail = context.GetValue(this.BuildDetail);
            string displayText = context.GetValue(this.DisplayText);
            var url = context.GetValue(this.Url);

            // Add the hyperlink
            buildDetail.Information.AddExternalLink(displayText, new Uri(url));
            buildDetail.Information.Save();
        }
    }
}
