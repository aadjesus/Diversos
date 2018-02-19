using System.Windows;
using System.Windows.Input;
using Onyx;
using Onyx.ComponentModel;
using Onyx.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System;
using System.Windows.Threading;

namespace FlipTile3D
{
    /// <summary>
    /// The <em>View Model</em> for the <see cref="MainWindow"/> <em>View Element</em>.
    /// This uses the Oynx MVVM framework. 
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        #region Data
        private List<DiscipleInfo> discipleInfos = new List<DiscipleInfo>();
        private Boolean xmlFileOpened = false;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="view">The view to associate with this instance.</param>
        public MainWindowViewModel(View view)
            : base(view)
        {
            this.CommandBindings.Add(new CommandBinding(OpenDisciplesXMLFileCommand,
                this.OpenDisciplesXMLFileExecuted,
                new CanExecuteRoutedEventHandler(this.OpenDisciplesXMLFileCanExecute)));
        }
        #endregion

        #region Commands
        /// <summary>
        /// The OpenDisciplesXMLFile routed command.
        /// </summary>
        private static readonly RoutedUICommand cmdOpenDisciplesXMLFile =
            new System.Windows.Input.RoutedUICommand(
                "Open Disciples XML File",
                "OpenDisciplesXMLFileCommand",
                typeof(MainWindowViewModel));

        /// <summary>
        /// Gets the OpenDisciplesXMLFile command.
        /// </summary>
        /// <value>The OpenDisciplesXMLFile command.</value>
        public static RoutedUICommand OpenDisciplesXMLFileCommand
        {
            get { return cmdOpenDisciplesXMLFile; }
        }


        /// <summary>
        /// Determines if the OpenDisciplesXMLFileCommand can Execute
        /// </summary>
        private void OpenDisciplesXMLFileCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !xmlFileOpened;
        }

        /// <summary>
        /// Handles the execution of the <see cref="MainWindow.OpenDisciplesXMLFile"/> command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OpenDisciplesXMLFileExecuted(object sender, RoutedEventArgs args)
        {

            var openFileDialogService = this.View.GetService<ICommonDialogProvider>().CreateOpenFileDialog();
            openFileDialogService.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            openFileDialogService.Filter = "XML files (*.xml)|*.xml;|All files (*.*)|*.*";


            if (openFileDialogService.ShowDialog() == true)
            {
                string s = openFileDialogService.FileName;

                XElement root = XElement.Load(openFileDialogService.FileName);

                try
                {
                    DiscipleInfos = (from disc in root.Descendants("Disciple")
                                     select new DiscipleInfo
                                     {
                                         ImageUrl = disc.Element("ImageUrl").Value,
                                         BlogUrl = disc.Element("BlogUrl").Value
                                     }).ToList();

                    if (DiscipleInfos.Count > 0)
                    {
                        xmlFileOpened = true;
                        (App.Current as App).AreDisciplesOpen = true;
                    }
                }
                catch
                {
                }

            }
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Occurs when the associated View is loaded
        /// </summary>
        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            //run messagebox on Background give view some time for view to render properly
            base.View.ViewElement.Dispatcher.BeginInvoke((Action)delegate
            {
                var messageBoxService = this.View.GetService<IDisplayMessage>();
                messageBoxService.ShowMessage("Welcome to a small Onyx Demo.\r\n\r\n " +
                    "Please pick the Disciples.xml file from the debug directory","Onyx demo",
                    MessageBoxButton.OK,MessageBoxImage.Information);

            }, DispatcherPriority.Background);
        }
        #endregion

        #region Public Properties
        public List<DiscipleInfo> DiscipleInfos
        {
            get { return discipleInfos; }
            set
            {
                this.discipleInfos = value;
                OnPropertyChanged(() => DiscipleInfos);
            }
        }
        #endregion
    }
}
