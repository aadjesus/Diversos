using System;
using System.Collections.Generic;
using System.Text;
using SAS.Shared.AddIns;
using SAS.Tasks.Toolkit;

namespace Signon
{
    [ClassId("419f2958-3345-49da-b7ed-73f3e7fd14db")]
    [InputRequired(InputResourceType.None)]
    public class SignOnTask : SAS.Tasks.Toolkit.SasTask, SAS.Shared.AddIns.ISASTaskExecution
    {
        #region Constructor and class initialization
        public SignOnTask()
        {
            InitializeComponent();
        }

        // use the Visual Studio Designer to maintain the fields within here
        private void InitializeComponent()
        {
            // 
            // SignOnTask
            // 
            this.GeneratesReportOutput = false;
            this.GeneratesSasCode = false;
            this.RequiresData = false;
            this.TaskCategory = "SAS Example";
            this.TaskDescription = "Sign on to remote system";
            this.TaskName = "Sign on";
        }
        #endregion

        #region Properties
        public string User { get; set; }
        public string Password { get; set; }
        #endregion

        #region Override for Show (show form)
        /// <summary>
        /// This is called when it's time to show the task user interface
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public override ShowResult Show(System.Windows.Forms.IWin32Window Owner)
        {
            // this form is a basic Windows form
            // except that it derives from TaskForm, which 
            // includes some special handlers for the
            // "replace results?" question that EG users 
            // might see
            SignOnPromptForm dlg = new SignOnPromptForm();

            // initialize with a handle to the application (EG)
            dlg.Consumer = Consumer;

            // initialize the form with the current settings, if any
            dlg.User = this.User;
            dlg.Password = this.Password;

            if (dlg.ShowDialog(Owner) == System.Windows.Forms.DialogResult.OK)
            {
                // store the new values from the form
                this.User = dlg.User;
                this.Password = dlg.Password;
                return ShowResult.RunNow;
            }

            return ShowResult.Canceled;
        }
        #endregion

        #region Save and restore settings

        // barebones XML to save some login data
        const string xmlFormat = "<Login User=\"{0}\" Password=\"{1}\"/>";
        public override string GetXmlState()
        {
            return string.Format(xmlFormat, User, Password);
        }

        // load up the XML and retrieve the properties from
        // within it, and use it to populate the
        // current state of the task
        public override void RestoreStateFromXml(string xmlState)
        {
            try
            {
                // NOTE: not nearly enough error checking here!
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(xmlState);
                User = doc.FirstChild.Attributes["User"].Value;
                Password = doc.FirstChild.Attributes["Password"].Value;
            }
            catch (System.Xml.XmlException)
            {
                // could not read this XML for some reason
            }

        }
        
        #endregion

        // The ISASTaskExecution interface gives you explicit control
        // over what happens when the task is run.
        #region ISASTaskExecution Members

        /// <summary>
        /// Cancel - not supported for this task, so return False
        /// </summary>
        /// <returns></returns>
        public bool Cancel()
        {
            return false;
        }

        /// <summary>
        /// No result streams are created.  Only result will be a log file.
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public ISASTaskStream OpenResultStream(int Index)
        {
            // should never happen because we return 0 for ResultCount
            throw new NotImplementedException();
        }

        /// <summary>
        /// No results return, just the log
        /// </summary>
        public int ResultCount
        {
            get { return 0; }
        }

        string theSasLog = "";
        /// <summary>
        /// This is the method that actually runs the work of the task.
        /// It submits the SAS program, hiding the program
        /// and true SAS log from the user.
        /// </summary>
        /// <param name="LogWriter"></param>
        /// <returns></returns>
        public RunStatus Run(ISASTaskTextWriter LogWriter)
        {
            theSasLog = "";
            SAS.Tasks.Toolkit.Helpers.FormattedLogWriter.WriteNormalLine(LogWriter, "Submitting your login information to SAS");
            SubmitSASProgramComplete += new SubmitCompleteHandler(SignOnTask_SubmitSASProgramComplete);

            string theProgram = string.Format("%let user={0};\r\n%let password={1};\r\n", User, Password);
            // will not return until the program has been run
            SubmitSASProgramAndWait(theProgram);

            // will have been populated in the event handler
            LogWriter.WriteText(theSasLog);

            return RunStatus.Success;
        }

        // this event handler will collect the log info
        // you can check the SubmitCompleteEventArgs value for Success status.
        void SignOnTask_SubmitSASProgramComplete(object sender, SubmitCompleteEventArgs args)
        {
            // you're getting the SAS log back in args.Log
            // you can take this opportunity to alter the
            // contents and mask out sensitive data, if needed
            theSasLog = args.Log;

            SubmitSASProgramComplete -= new SubmitCompleteHandler(SignOnTask_SubmitSASProgramComplete);
        }

        #endregion
    }
}
