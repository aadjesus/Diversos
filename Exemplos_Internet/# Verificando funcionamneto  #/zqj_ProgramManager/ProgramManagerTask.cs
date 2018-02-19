using System;
using System.Text;
using SAS.Shared.AddIns;
using SAS.Tasks.Toolkit;

namespace ProgramManager
{
    // unique identifier for this task
    [ClassId("1688e68e-546b-412c-b8fc-a9580bb03a46")]
    // location of the task icon to show in the menu and process flow
    [IconLocation("ProgramManager.task.ico")]
    // does this task require input data? 
    // InputResourceType.Data for data set, or 
    // InputResourceType.None for none required
    [InputRequired(InputResourceType.None)]
    public class ProgramManagerTask : SAS.Tasks.Toolkit.SasTask
    {
        #region private members


        #endregion

        #region Initialization
        public ProgramManagerTask()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // ProgramManagerTask
            // 
            this.RequiresData = false;
            this.TaskCategory = "SAS Examples";
            this.TaskDescription = "Manage the SAS programs in the active project.";
            this.TaskName = "SAS Program Manager";

        }
        #endregion

        #region overrides
        public override bool Initialize()
        {
            return true;
        }
        
        /// <summary>
        /// Show the task user interface
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns>whether to cancel the task, or run now</returns>
        public override ShowResult Show(System.Windows.Forms.IWin32Window Owner)
        {
            ProgramManagerTaskForm dlg = new ProgramManagerTaskForm(this.Consumer);
            dlg.Show(Owner);
            return ShowResult.Canceled;
        }

        #endregion

    }
}
