using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace FileMonitorService
{
    public partial class Monitor : ServiceBase
    {
        private FileSystemWatcher _watcher;


        public Monitor()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            _watcher = new FileSystemWatcher();
            _watcher.Path = @"c:\watched";

            _watcher.Changed += new FileSystemEventHandler(LogFileSystemChanges);
            _watcher.Created += new FileSystemEventHandler(LogFileSystemChanges);
            _watcher.Deleted += new FileSystemEventHandler(LogFileSystemChanges);
            _watcher.Renamed += new RenamedEventHandler(LogFileSystemRenaming);
            _watcher.Error += new ErrorEventHandler(LogBufferError);
            _watcher.EnableRaisingEvents = true;

            LogEvent("Monitoring Started");
        }


        protected override void OnStop()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();

            LogEvent("Monitoring Stopped");
        }


        protected override void OnPause()
        {
            _watcher.EnableRaisingEvents = false;
            LogEvent("Monitoring Paused");
        }


        protected override void OnContinue()
        {
            _watcher.EnableRaisingEvents = true;
            LogEvent("Monitoring Resumed");
        }


        void LogEvent(string message)
        {
            string eventSource = "File Monitor Service";
            EventLog.WriteEntry(eventSource, message);
        }


        void LogFileSystemChanges(object sender, FileSystemEventArgs e)
        {
            string log = string.Format("{0} | {1}", e.FullPath, e.ChangeType);
            LogEvent(log);
        }


        void LogFileSystemRenaming(object sender, RenamedEventArgs e)
        {
            string log = string.Format("{0} | Renamed from {1}", e.FullPath, e.OldName);
            LogEvent(log);
        }


        void LogBufferError(object sender, ErrorEventArgs e)
        {
            LogEvent("Buffer limit exceeded");
        }
    }
}
