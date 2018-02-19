using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace DragNDrop
{
    static class Program
    {
        #region Variables
        //A prefix used in filename to identify a drag from the application
        //Make it more unique
        internal const string DRAG_SOURCE_PREFIX = "__DragNDrop__Temp__";
        //The item which we drag is being stored here
        //could be a path to a location or a filestream
        internal static object objDragItem;
        //A FileSystemWatcher used to monitor the System's Temp Directory for a drag from the application
        private static FileSystemWatcher tempDirectoryWatcher;
        //A Hashtable to keep multiple FileSystemWatchers 
        public static Hashtable watchers = null;
        #endregion


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Setting the tempDirectoryWatcher to monitor the creation of a file from our application
            tempDirectoryWatcher = new FileSystemWatcher();
            tempDirectoryWatcher.Path = Path.GetTempPath();            
            tempDirectoryWatcher.Filter = string.Format("{0}*.tmp", DRAG_SOURCE_PREFIX);
            tempDirectoryWatcher.NotifyFilter = NotifyFilters.FileName;
            tempDirectoryWatcher.IncludeSubdirectories = false;
            tempDirectoryWatcher.EnableRaisingEvents = true;
            tempDirectoryWatcher.Created += new FileSystemEventHandler(TempDirectoryWatcherCreated);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DragNDrop());
        }

        #region FileSystemWatcher Methods
        /// <summary>
        /// A temp file is created in the temp folder with the prefix wen a drag is initiated
        /// At that point we will add a filesystem watch to all the Logical Drives present.
        /// And will clear the watchers when the drag is completed or canceled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TempDirectoryWatcherCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                CreateFileWatchers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DragNDrop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// This Event is triggered when the temp file created is Dropped into the target folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void FileWatcherCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                OnFileDropPathFound(e.FullPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DragNDrop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Methodd Called when the Dropped Path is found
        /// </summary>
        /// <param name="dropedFilePath"></param>
        private static void OnFileDropPathFound(string dropedFilePath)
        {
            try
            {
                if (dropedFilePath.Trim() != string.Empty && objDragItem != null)
                {
                    string dropPath = dropedFilePath.Substring(0, dropedFilePath.LastIndexOf('\\'));

                    if (File.Exists(dropedFilePath))
                        File.Delete(dropedFilePath);

                    //Use your Download Code here
                    MessageBox.Show(String.Format("{0} dropped to {1}",objDragItem.ToString(),dropPath));                    
                }
                objDragItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DragNDrop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Creating File Watchers for all Logical Drives in the System
        /// </summary>
        public static void CreateFileWatchers()
        {
            try
            {
                if (watchers == null)
                {
                    int i = 0;
                    Hashtable tempWatchers = new Hashtable();
                    FileSystemWatcher watcher;
                    //Adding FileSystemWatchers and adding Created event to it 
                    foreach (string driveName in Directory.GetLogicalDrives())
                    {
                        //this checking may sound absurd to you.
                        //but in OS like Windows 2000,
                        //even if there is no floppy drive present
                        //An "A:/" will be shown in My Computer.
                        //To avoid exceptions like this I've added this check.
                        if (Directory.Exists(driveName))
                        {
                            watcher = new FileSystemWatcher();
                            watcher.Filter = string.Format("{0}*.tmp", DRAG_SOURCE_PREFIX);
                            watcher.NotifyFilter = NotifyFilters.FileName;
                            watcher.Created += new FileSystemEventHandler(FileWatcherCreated);
                            watcher.IncludeSubdirectories = true;
                            watcher.Path = driveName;
                            watcher.EnableRaisingEvents = true;
                            tempWatchers.Add("file_watcher" + i.ToString(), watcher);
                            i = i + 1;
                        }
                    }
                    watchers = tempWatchers;
                    tempWatchers = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DragNDrop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Removes the FileSystemWatchers watching the Logical Drives
        /// </summary>
        public static void ClearFileWatchers()
        {
            try
            {
                if (watchers != null && watchers.Count > 0)
                {
                    for (int i = 0; i < watchers.Count; i++)
                    {
                        ((FileSystemWatcher)watchers["file_watcher" + i.ToString()]).Dispose();
                    }
                    watchers.Clear();
                    watchers = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DragNDrop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}