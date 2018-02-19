using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SAS.Shared.AddIns;
using SAS.Tasks.Toolkit.Controls;

namespace ProgramManager
{
    /// <summary>
    /// This windows form inherits from the TaskForm class, which
    /// includes a bit of special handling for SAS Enterprise Guide.
    /// </summary>
    public partial class ProgramManagerTaskForm : Form
    {
        // the SAS Enterprise Guide application 
        SAS.Shared.AddIns.ISASTaskConsumer3 Consumer { get; set; }

        public ProgramManagerTaskForm(SAS.Shared.AddIns.ISASTaskConsumer3 consumer)
        {
            InitializeComponent();

            // provide a handle to the SAS Enterprise Guide application
            this.Consumer = consumer;
        }

        // initialize the form with the values from the settings
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // refresh the list with the current project contents
            RefreshContent();
        }

        /// <summary>
        /// Refresh the list of SAS programs that are currently in the project
        /// </summary>
        private void RefreshContent()
        {
            lvPrograms.Items.Clear();
            try
            {
                if (Consumer != null)
                {
                    // ISASProject, ISASProjectProcessFlow, and ISASProjectItem/ISASProjectTask are
                    // all interfaces added in SAS Enterprise Guide 4.2
                    // These are supported only in EG, not in SAS Add-In for Microsoft Office
                    ISASProject project = Consumer.GetProject();
                    if (project != null)
                    {
                        foreach (ISASProjectProcessFlow flow in project.ProcessFlows)
                        {
                            foreach (ISASProjectTask task in flow.GetTasks())
                            {
                                if (task.TaskType == "CodeTask")
                                {
                                    ListViewItem item = new ListViewItem(new string[] { 
                                        ((ISASProjectItem)task).Name, 
                                        task.TaskType, 
                                        flow.Name 
                                    });
                                    lvPrograms.Items.Add(item);
                                }
                            }
                        }
                    }

                }
            }
            catch ( System.Exception )
            {
                MessageBox.Show("Active project has changed or has been closed.  Launch the task again for the new project.",
                    "Cannot communicate with project");
                this.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshContent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SAS program files (*.sas)|*.sas";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dlg.FileName;

                // with a program file selected
                // try to launch the SEGuide.exe process with the /openfile argument,
                // which *should* reuse this active instance of the application
                // to load the program into the active project.

                // get the full path of the main EXE (where SEGuide.exe is running from)
                System.Diagnostics.ProcessModule mod = System.Diagnostics.Process.GetCurrentProcess().MainModule;
                if (mod.FileName.ToLower().Contains("seguide.exe"))
                {
                    // "launch" the program with the /openfile argument
                    System.Diagnostics.Process.Start(mod.FileName, string.Format("/openfile:\"{0}\"", file));

                    // try to refresh, but it might take a moment to update the 
                    // project with the new program, so you might
                    // still need to click the Refresh button
                    RefreshContent();
                }
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
