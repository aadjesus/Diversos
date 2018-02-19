using System;
using System.Collections.Generic;
using System.Text;
using Framework.Shell.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using Framework.Shell.Constants;
using Framework.Shell.Events;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using Framework.MP3PlayerModule.Data;

namespace Framework.MP3PlayerModule.Views
{
    public class MP3PlayerViewPresenter : Presenter<IMP3PlayerView>
    {
        private WorkItems.MP3PlayerWorkItem workItem
        {
            get { return base.WorkItem as WorkItems.MP3PlayerWorkItem; }
        }

        [EventSubscription(EventNames.FILE_FOUND, ThreadOption.UserInterface)]
        public void OnFileFound(object sender, EventArgs<string> eventArgs)
        {
            View.RefreshListView();
        }

        public void Browse()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                workItem.PerformSearch(folderBrowser.SelectedPath);
                View.SetTrackListBinding(workItem.GetBindingSourceDataTable());
            }
        }

        public void Stop()
        {
            workItem.Stop();
        }

        public void Play(string path)
        {
            workItem.Play(path);
        }
    }
}
