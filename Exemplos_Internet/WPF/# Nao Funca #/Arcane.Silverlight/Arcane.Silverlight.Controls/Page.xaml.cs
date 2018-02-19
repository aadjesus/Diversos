using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Browser;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace Arcane.Silverlight.Controls
{
    public partial class Page : UserControl
    {
        BackgroundWorker _worker;
        TreeNode _currentLoadingNode;




        #region Constructors

        public Page()
        {
            InitializeComponent();
            InitializeBackgroundWorkerNode();

            this.Loaded += new RoutedEventHandler(Page_Loaded);
            this.myTreeViewDataBinded.BeforeExpand += new TreeViewCancelEventHandler(myTreeViewDataBinded_BeforeExpand);
        }

        #endregion


        #region Worker

        public void InitializeBackgroundWorkerNode()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_currentLoadingNode.Tag == null)
                return;

            if (_currentLoadingNode.Tag.Equals("Database"))
            {
                _currentLoadingNode.Nodes.Clear();

                SampleNodeData data = new SampleNodeData();
                data.Name = "Tables";
                BitmapImage image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "folder.png"));
                data.NodeImage = image;
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "folderopen.png"));
                data.SelectedNodeImage = image;

                TreeNode node = _currentLoadingNode.Nodes.Add(data);
                node.Nodes = new TreeNodeCollection();
                node.Tag = "Tables";

                data = new SampleNodeData();
                data.Name = "Views";
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "folder.png"));
                data.NodeImage = image;
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "folderopen.png"));
                data.SelectedNodeImage = image;

                _currentLoadingNode.Nodes.Add(data);

                data = new SampleNodeData();
                data.Name = "Synonyms";
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "folder.png"));
                data.NodeImage = image;
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "folderopen.png"));
                data.SelectedNodeImage = image;

                _currentLoadingNode.Nodes.Add(data);

                data = new SampleNodeData();
                data.Name = "Security";
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "folder.png"));
                data.NodeImage = image;
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "folderopen.png"));
                data.SelectedNodeImage = image;

                _currentLoadingNode.Nodes.Add(data);
            }
            else if (_currentLoadingNode.Tag.Equals("Tables"))
            {
                _currentLoadingNode.Nodes.Clear();

                foreach (SampleNodeData data in SampleNodeData.GetTablesNodes())
                    _currentLoadingNode.Nodes.Add(data);
            }
        }


        #endregion

        void myTreeViewDataBinded_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag == null)
                return;
            if (_worker.IsBusy)
                e.Cancel = true;
            else
            {

                SampleNodeData data = new SampleNodeData();
                data.Name = "Loading, pleaze wait";
                BitmapImage image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "loading.png"));
                data.NodeImage = image;
                image = new BitmapImage();
                image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "loading.png"));
                data.SelectedNodeImage = image;
                e.Node.Nodes.Clear();
                e.Node.Nodes.Add(data);

                _currentLoadingNode = e.Node;

                _worker.RunWorkerAsync();
            }
        }


        void Page_Loaded(object sender, RoutedEventArgs e)
        {


            this.myTreeViewDataBinded.Clear();

            SampleNodeData data = new SampleNodeData();
            data.Name = "Adventures works !";
            BitmapImage image = new BitmapImage();
            image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "database.png"));
            data.NodeImage = image;
            image = new BitmapImage();
            image.UriSource = new Uri(HtmlPage.Document.DocumentUri.AbsoluteUri.Replace("Arcane.Silverlight.ControlsTestPage.aspx", "databaseopen.png"));
            data.SelectedNodeImage = image;

            TreeNode node = this.myTreeViewDataBinded.Add(data);
            node.Nodes = new TreeNodeCollection();
            node.Tag = "Database";
            // this.myTreeViewDataBinded.DataContext = SampleNodeData.GetNodes(10);




        }
    }
}
