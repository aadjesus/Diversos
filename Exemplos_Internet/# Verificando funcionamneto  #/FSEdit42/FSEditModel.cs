using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SAS.Shared.AddIns;
using SAS.Tasks.Toolkit;

namespace SAS.Tasks.Examples.FSEdit
{
    [ClassId("2630792A-A521-462e-B6AB-96AFC9EA202E")]
    [InputRequired(InputResourceType.Data)]
    [Version(4.2)]
    [IconLocation("SAS.Tasks.Examples.FSEdit.FSEditIcon.ico")]

    public class FSEditModel : SAS.Tasks.Toolkit.SasTask
    {
        public FSEditModel()
        {

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSEditModel));
            // 
            // FSEditModel
            // 
            this.GeneratesReportOutput = false;
            this.GeneratesSasCode = false;
            this.ProcsUsed = "SQL";
            this.ProductsRequired = "base";
            resources.ApplyResources(this, "$this");

        }

        public override ShowResult Show(System.Windows.Forms.IWin32Window Owner)
        {
            string server = "";
            SAS.Workspace ws = null;
            try
            {
                server = Consumer.AssignedServer;
                ws = Consumer.Workspace(server) as SAS.Workspace;
            }
            catch (Exception ex)
            {
                throw new System.Exception("ISASTaskConsumer.Workspace is not usable!", ex);
            }

            // Show the default form for this custom task
            if (server.Length > 0 && ws != null)
            {
                FSEditForm dlg = new FSEditForm(Consumer, ws.UniqueIdentifier);
                dlg.Text = "FSEdit";
                dlg.ShowDialog(Owner);
            }
            return ShowResult.Canceled;
        }
    }
}
