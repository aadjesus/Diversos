using System;
using System.Collections.Generic;
using System.Text;
using SAS.Shared.AddIns;
using SAS.Tasks.Toolkit;

namespace SAS.Tasks.Examples.SASProgramRunner
{
    // a unique identifier for this task, formatted as a GUID
    [ClassId("09360de7-7ed6-41eb-bfa9-66a9a2b2e9aa")]
    // the version of the task
    [Version(4.2)]
    // What does this task require for input? Typical types are Data and None.
    [InputRequired(InputResourceType.None)]
    // IconLocation is the namespace-qualified name of the icon
    // it is built into this assembly as an Embedded Resource.
    [IconLocation("SAS.Tasks.Examples.SASProgramRunner.SASProgramRunner.ico")]
    public class ProgramRunner : SAS.Tasks.Toolkit.SasTask
    {
        #region Initialization
        public ProgramRunner()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramRunner));
            // 
            // ProgramRunner
            // 
            this.RequiresData = false;
            resources.ApplyResources(this, "$this");

        }
        #endregion

        #region Override for Show
        public override ShowResult Show(System.Windows.Forms.IWin32Window Owner)
        {
            ProgramRunnerForm dlg = new ProgramRunnerForm(Consumer, this);
            dlg.ShowDialog();

            // always return Canceled.  Results are not kept in the project.
            return ShowResult.Canceled;
        }
        #endregion

    }
}
