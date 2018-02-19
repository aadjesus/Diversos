using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Framework.Shell.Constants;
using Framework.Shell.Events;
using System.ComponentModel;
using System.Diagnostics;
using Framework.MP3PlayerModule.Data;
using IdSharp.Tagging.ID3v2;
using System.Threading;

namespace Framework.MP3PlayerModule.WorkItems
{
    public class DirectorySearcherWorkItem : WorkItem
    {


        #region Variables

        private BackgroundWorker workerSearcher;
        private LibraryInformation libraryInformation;
        #endregion

        #region Events

        [EventPublication(EventNames.FILE_FOUND, PublicationScope.Global)]
        public event System.EventHandler<EventArgs<string>> FileFound;


        #endregion


        #region Methods

        public new void Run()
        {
        }

        public void SearchDirectory(string searchPath, ref LibraryInformation libraryInformation)
        {
            this.libraryInformation = libraryInformation;
            workerSearcher = new BackgroundWorker();
            workerSearcher.DoWork += new DoWorkEventHandler(_workerSearcher_DoWork);
            workerSearcher.RunWorkerAsync(searchPath);
        }

        void _workerSearcher_DoWork(object sender, DoWorkEventArgs e)
        {
            string searchPath = e.Argument as string;
            BackgroundWorkerSearchDirectory(searchPath);
        }

        public void ParseTagsAddToLibrary(string path)
        {
            IID3v2 id3 = ID3v2Helper.CreateID3v2(path);
            LibraryInformation.DataTableTracksRow row = libraryInformation.DataTableTracks.NewDataTableTracksRow();

            row.Title = id3.Title;
            row.Album = id3.Album;
            row.Artist = id3.Artist;
            row.Year = id3.Year;
            row.Genre = id3.Genre;
            row.Path = path;

            libraryInformation.DataTableTracks.AddDataTableTracksRow(row);
            Thread.Sleep(600);
        }

        public void BackgroundWorkerSearchDirectory(string searchPath)
        {
            FileInfo[] mp3FileList;
            DirectoryInfo directoryInformation;

           
            try
            {
                directoryInformation = new DirectoryInfo(searchPath);
                mp3FileList = directoryInformation.GetFiles("*.mp3");

                foreach (FileInfo mp3Info in mp3FileList)
                {
                    FileFound(this, new EventArgs<string>(mp3Info.FullName));
                    ParseTagsAddToLibrary(mp3Info.FullName);
                }

                foreach (string directory in Directory.GetDirectories(searchPath))
                {
                    BackgroundWorkerSearchDirectory(directory);
                }
            }
            catch (Exception e) {}
        }

        #endregion
    }
}
