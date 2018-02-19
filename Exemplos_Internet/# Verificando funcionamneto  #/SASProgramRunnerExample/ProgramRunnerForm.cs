using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SAS.Shared.AddIns;

namespace SAS.Tasks.Examples.SASProgramRunner
{
    public partial class ProgramRunnerForm : SAS.Tasks.Toolkit.Controls.TaskForm
    {
        #region private 
        ProgramRunner model = null;
        #endregion

        #region initialization
        public ProgramRunnerForm(ISASTaskConsumer3 consumer, ProgramRunner model)
        {
            InitializeComponent();

            Consumer = consumer;
            this.model = model;

            // initialize the text editor content
            ctlEditor.EditorText = "proc options; run;";
            ctlLogViewer.EditorText = "";
        }
        #endregion

        #region event handlers for running program

        /// <summary>
        /// When Run is clicked, the program is submitted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            // listen for the event that the program is complete.
            model.SubmitSASProgramComplete += new SAS.Tasks.Toolkit.SubmitCompleteHandler(model_SubmitSASProgramComplete);
            // submit the program
            model.SubmitSASProgram(ctlEditor.EditorText);
        }

        /// <summary>
        /// This event handler is called when the program completes.
        /// NOTE: this event handler will probably be called on a different
        /// thread than the UI thread that was used to submit it.
        /// 
        /// Since we want to update the user interface with the SAS log information,
        /// we need to "marshal" the call back to the UI thread.
        /// To do this, we have to call BeginInvoke() to move the execution path
        /// across a thread boundary.
        /// 
        /// It might seem confusing, but it boils down to this: 
        /// do not make any UI calls in this event handler.  Instead,
        /// use BeginInvoke and a delegate to transfer control back to the
        /// main thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void model_SubmitSASProgramComplete(object sender, SAS.Tasks.Toolkit.SubmitCompleteEventArgs args)
        {
            // release our subscription on the completed event.
            model.SubmitSASProgramComplete -= new SAS.Tasks.Toolkit.SubmitCompleteHandler(model_SubmitSASProgramComplete);

            // invoke the log update using the delegate, passing in the Log as 
            // an argument
            BeginInvoke(new MarshalledLogUpdater(MarshalledLogUpdate), new object[] {args.Log});
        }

        /// <summary>
        /// This is a special delegate descriptor that says
        /// the function call will contain a string argument
        /// </summary>
        /// <param name="log"></param>
        delegate void MarshalledLogUpdater(string log);

        /// <summary>
        /// Updates the log view with the supplied SAS log text.
        /// </summary>
        /// <param name="log"></param>
        void MarshalledLogUpdate(string log)
        {
            ctlLogViewer.EditorText = log;
        }

        #endregion

    }
}