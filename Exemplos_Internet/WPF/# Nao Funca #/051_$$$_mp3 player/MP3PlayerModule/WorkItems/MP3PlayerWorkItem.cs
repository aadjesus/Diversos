using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Framework.Shell.Constants;
using Framework.MP3PlayerModule.Views;
using System.Windows.Media;
using Framework.MP3PlayerModule.Data;
using System.Data;
using Microsoft.Practices.CompositeUI.EventBroker;
using Framework.Shell.Events;
using IdSharp.Tagging.ID3v2;


namespace Framework.MP3PlayerModule.WorkItems
{
    public class MP3PlayerWorkItem : WorkItem
    {
        private DirectorySearcherWorkItem   directorySearcher;
        private MediaPlayer                 mediaPlayer;
        private bool isPlayingAudio = false;
        private string lastPathSelected;
        private LibraryInformation libraryInformation;        

        public new void Run()
        {
            CreateLibraryInformation();
            CreateViews();
            CreateWorkItems();
            CreateMediaPlayer();
        }

        public DataTable GetBindingSourceDataTable()
        {
            return libraryInformation.DataTableTracks;
        }

        public void PerformSearch(string path)
        {
            directorySearcher.SearchDirectory(path, ref libraryInformation);
        }

        public void ParseTagsAddToLibrary(string path)
        {
            IID3v2 id3 = ID3v2Helper.CreateID3v2(path);
            LibraryInformation.DataTableTracksRow row = GetTrackRow();

            row.Title = id3.Title;
            row.Album = id3.Album;
            row.Artist = id3.Artist;
            row.Year = id3.Year;
            row.Genre = id3.Genre;
            row.Path = path;

            AddTrackRowToLibrary(row);
        }

        public LibraryInformation.DataTableTracksRow GetTrackRow()
        {
            return libraryInformation.DataTableTracks.NewDataTableTracksRow();
        }
        
        public void AddTrackRowToLibrary(LibraryInformation.DataTableTracksRow row)
        {
            libraryInformation.DataTableTracks.AddDataTableTracksRow(row);
        }
        
        private void CreateLibraryInformation()
        {
            libraryInformation = new LibraryInformation();
        }
        
        private void CreateMediaPlayer()
        {
            mediaPlayer = new MediaPlayer();
        }
        
        private void CreateWorkItems()
        {
            directorySearcher =  WorkItems.AddNew<DirectorySearcherWorkItem>();
        }
        
        private void CreateViews()
        {
            MP3PlayerView view = Items.AddNew<MP3PlayerView>();
            Workspaces[WorkspaceNames.MAIN_WORKSPACE].Show(view);
        }

        public void Play(string path)
        {
            if (isPlayingAudio)
                mediaPlayer.Stop();

            if(path != lastPathSelected)
                mediaPlayer.Open(new Uri(path));
            
            mediaPlayer.Play();

            lastPathSelected = path;
            isPlayingAudio = true;
        }

        public void Stop()
        {
            mediaPlayer.Stop();
            isPlayingAudio = false;
        }
    }
}
