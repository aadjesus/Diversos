using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Microsoft.TeamFoundation.Build.Client;

namespace BuildTasks.Activities
{

    [BuildActivity(HostEnvironmentOption.All)]
    public sealed class DoExcitingThings : CodeActivity
    {
        // Define the activity arguments of type string
        public InArgument<string> TextIn { get; set; }
        public OutArgument<string> TextOut { get; set; }
        public InOutArgument<string> TextInOut { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string textIn = context.GetValue(this.TextIn);
            string textInOut = context.GetValue(this.TextInOut);

            // Set the values
            context.SetValue<string>(this.TextOut, "This is the out argument with the value " + textIn);
            context.SetValue<string>(this.TextInOut, "This is the in/out argument with the value " + textInOut);
        }
    }
}
