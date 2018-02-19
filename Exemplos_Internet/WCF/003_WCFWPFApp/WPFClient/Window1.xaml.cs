using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using WPFClient.SVC;
using System.Collections;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;

using System.Net;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window, SVC.IChatCallback
    {
        //SVC holds references to the proxy and cotracts..
        SVC.ChatClient proxy = null;
        SVC.Client receiver = null;
        SVC.Client localClient = null;

        //Client will create this folder when loading
        string rcvFilesPath = @"C:/WCF_Received_Files/";

        //When the communication object turns to fault state it will
        //require another thread to invoke a fault event
        private delegate void FaultedInvoker();

        //This will hold each online client with a listBoxItem to quickly handle adding
        //and removing clients when they join or leave
        Dictionary<ListBoxItem, SVC.Client> OnlineClients = new Dictionary<ListBoxItem, Client>();


        public Window1()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window1_Loaded);
            chatListBoxNames.SelectionChanged += new SelectionChangedEventHandler(chatListBoxNames_SelectionChanged);
            chatTxtBoxType.KeyDown += new KeyEventHandler(chatTxtBoxType_KeyDown);
            chatTxtBoxType.KeyUp += new KeyEventHandler(chatTxtBoxType_KeyUp);
        }


        //Service might be disconnected or stopped for any reason,
        //so we have to handle the state of the communication object,
        //the communication object will fire an event for each transitioning
        //from a state to another, notice that when a connection state goes
        //from opening to opened or from opened to closing state.. it can't go
        //back so, if it is closed or faulted you have to set the proxy = null;
        //to be able to create a proxy again and open a connection
        //..
        //I have made a method called HandleProxy() to handle the state
        //of the connection, so in each event like opened, closed or faulted
        //we will call this method, and it will switch on the connection state
        //and apply a suitable reaction.
        //..
        //Because this events will need to be invoked on another thread
        //you can do like so in WPF applications (I've got this idea from
        //Sacha Barber's greate article on WCF WPF Application)

        void InnerDuplexChannel_Closed(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }

        void InnerDuplexChannel_Opened(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }

        void InnerDuplexChannel_Faulted(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }

        #region Private Methods

        /// <summary>
        /// This is the most method I like, it helps us alot
        /// We may can't know when a connection is lost in 
        /// of network failure or service stopped.
        /// And also to maintain performance client doesnt know
        /// that the connection will be lost when hitting the 
        /// disconnect button, but when a session is terminated
        /// this method will be called, and it will handle everything.
        /// </summary>
        private void HandleProxy()
        {
            if (proxy != null)
            {
                switch (this.proxy.State)
                {
                    case CommunicationState.Closed:
                        proxy = null;
                        chatListBoxMsgs.Items.Clear();
                        chatListBoxNames.Items.Clear();
                        loginLabelStatus.Content = "Disconnected";
                        ShowChat(false);
                        ShowLogin(true);
                        loginButtonConnect.IsEnabled = true;
                        break;
                    case CommunicationState.Closing:
                        break;
                    case CommunicationState.Created:
                        break;
                    case CommunicationState.Faulted:
                        proxy.Abort();
                        proxy = null;
                        chatListBoxMsgs.Items.Clear();
                        chatListBoxNames.Items.Clear();
                        ShowChat(false);
                        ShowLogin(true);
                        loginLabelStatus.Content = "Disconnected";
                        loginButtonConnect.IsEnabled = true;
                        break;
                    case CommunicationState.Opened:
                        ShowLogin(false);
                        ShowChat(true);

                        chatLabelCurrentStatus.Content = "online";
                        chatLabelCurrentUName.Content = this.localClient.Name;

                        Dictionary<int, Image> images = GetImages();
                        Image img = images[loginComboBoxImgs.SelectedIndex];
                        chatCurrentImage.Source = img.Source;
                        break;
                    case CommunicationState.Opening:
                        break;
                    default:
                        break;
                }
            }

        }


        
        /// <summary>
        /// This is the second important method, which creates 
        /// the proxy, subscribe to connection state events
        /// and open a connection with the service
        /// </summary>
        private void Connect()
        {
            if (proxy == null)
            {
                try
                {
                    this.localClient = new SVC.Client();
                    this.localClient.Name = loginTxtBoxUName.Text.ToString();
                    this.localClient.AvatarID = loginComboBoxImgs.SelectedIndex;
                    InstanceContext context = new InstanceContext(this);
                    proxy = new SVC.ChatClient(context);

                    //As the address in the configuration file is set to localhost
                    //we want to change it so we can call a service in internal 
                    //network, or over internet
                    string servicePath = proxy.Endpoint.ListenUri.AbsolutePath;
                    string serviceListenPort = proxy.Endpoint.Address.Uri.Port.ToString();

                    proxy.Endpoint.Address = new EndpointAddress("net.tcp://" + loginTxtBoxIP.Text.ToString() + ":" + serviceListenPort + servicePath);


                    proxy.Open();

                    proxy.InnerDuplexChannel.Faulted += new EventHandler(InnerDuplexChannel_Faulted);
                    proxy.InnerDuplexChannel.Opened += new EventHandler(InnerDuplexChannel_Opened);
                    proxy.InnerDuplexChannel.Closed += new EventHandler(InnerDuplexChannel_Closed);
                    proxy.ConnectAsync(this.localClient);
                    proxy.ConnectCompleted += new EventHandler<ConnectCompletedEventArgs>(proxy_ConnectCompleted);
                }
                catch (Exception ex)
                {
                    loginTxtBoxUName.Text = ex.Message.ToString();
                    loginLabelStatus.Content = "Offline";
                    loginButtonConnect.IsEnabled = true;
                }
            }
            else
            {
                HandleProxy();
            }
        }

        private void Send()
        {
            if (proxy != null && chatTxtBoxType.Text != "")
            {
                if (proxy.State == CommunicationState.Faulted)
                {
                    HandleProxy();
                }
                else
                {
                    //Create message, assign its properties
                    SVC.Message msg = new WPFClient.SVC.Message();
                    msg.Sender = this.localClient.Name;
                    msg.Content = chatTxtBoxType.Text.ToString();

                    //If whisper mode is checked and an item is
                    //selected in the list box of clients, it will
                    //arrange a client object called receiver
                    //to whisper
                    if ((bool)chatCheckBoxWhisper.IsChecked)
                    {
                        if (this.receiver != null)
                        {
                            proxy.WhisperAsync(msg, this.receiver);
                            chatTxtBoxType.Text = "";
                            chatTxtBoxType.Focus();
                        }
                    }

                    else
                    {
                        proxy.SayAsync(msg);
                        chatTxtBoxType.Text = "";
                        chatTxtBoxType.Focus();
                    }
                    //Tell the service to tell back all clients that this client
                    //has just finished typing..
                    proxy.IsWritingAsync(null);
                }
            }
        }

        
        /// <summary>
        /// This method to enable us scrolling the list box of messages
        /// when a new message comes from the service..
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private ScrollViewer FindVisualChild(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is ScrollViewer)
                {
                    return (ScrollViewer)child;
                }
                else
                {
                    ScrollViewer childOfChild = FindVisualChild(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

 
        /// <summary>
        /// This is an important method which is called whenever
        /// a message comes from the service, a client joins or
        /// leaves, to return a ready item to be added in the
        /// list box (either the one for messages or the one for
        /// clients).
        /// </summary>
        /// <param name="imgID"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private ListBoxItem MakeItem(int imgID, string text)
        {
            ListBoxItem item = new ListBoxItem();
            Dictionary<int, Image> images = GetImages();
            Image img = images[imgID];
            img.Height = 70;
            img.Width = 60;
            item.Content = img;

            TextBlock txtblock = new TextBlock();
            txtblock.Text = text;
            txtblock.VerticalAlignment = VerticalAlignment.Center;

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Children.Add(item);
            panel.Children.Add(txtblock);

            ListBoxItem bigItem = new ListBoxItem();
            bigItem.Content = panel;

            return bigItem;
        }




        /// <summary>
        /// This method is not used, I just put it here to help
        /// you in case you want to make a rich text box and enable
        /// emoticons for example.
        /// Just add a richTextBox control and set
        /// richTextBox.Document = MakeDocument(imgid, text);
        /// </summary>
        /// <param name="imgID"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private FlowDocument MakeDocument(int imgID, string text)
        {
            Dictionary<int, Image> images = GetImages();
            Image img = images[imgID];
            img.Height = 70;
            img.Width = 60;


            Block imgBlock = new BlockUIContainer(img);
            Block txtBlock = new Paragraph(new Run(text));


            FlowDocument doc = new FlowDocument();
            doc.Blocks.Add(imgBlock);

            doc.Blocks.Add(txtBlock);

            doc.FlowDirection = FlowDirection.LeftToRight;
            return doc;
        }

        /// <summary>
        /// A method to retreive avatars as stream objects
        /// and get an objects of type Image from the stream,
        /// to return a dictionary of images and an ID for each
        /// image.
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Image> GetImages()
        {
            List<Stream> picsStrm = new List<Stream>();

            Assembly asmb = Assembly.GetExecutingAssembly();
            string[] picNames = asmb.GetManifestResourceNames();

            foreach (string s in picNames)
            {
                if (s.EndsWith(".png"))
                {
                    Stream strm = asmb.GetManifestResourceStream(s);
                    if (strm != null)
                    {
                        picsStrm.Add(strm);
                    }
                }
            }


            Dictionary<int, Image> images = new Dictionary<int, Image>();

            int i = 0;

            foreach (Stream strm in picsStrm)
            {

                PngBitmapDecoder decoder = new PngBitmapDecoder(strm,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                BitmapSource bitmap = decoder.Frames[0] as BitmapSource;
                Image img = new Image();
                img.Source = bitmap;
                img.Stretch = Stretch.UniformToFill;

                images.Add(i, img);
                i++;

                strm.Close();
            }
            return images;
        }

        /// <summary>
        /// Show or hide login controls depends on the parameter
        /// </summary>
        /// <param name="show"></param>
        private void ShowLogin(bool show)
        {
            if (show)
            {
                loginButtonConnect.Visibility = Visibility.Visible;
                loginComboBoxImgs.Visibility = Visibility.Visible;
                loginLabelIP.Visibility = Visibility.Visible;
                loginLabelStatus.Visibility = Visibility.Visible;
                loginLabelTitle.Visibility = Visibility.Visible;
                loginLabelUName.Visibility = Visibility.Visible;
                loginPolyLine.Visibility = Visibility.Visible;
                loginTxtBoxIP.Visibility = Visibility.Visible;
                loginTxtBoxUName.Visibility = Visibility.Visible;
            }
            else
            {
                loginButtonConnect.Visibility = Visibility.Collapsed;
                loginComboBoxImgs.Visibility = Visibility.Collapsed;
                loginLabelIP.Visibility = Visibility.Collapsed;
                loginLabelStatus.Visibility = Visibility.Collapsed;
                loginLabelTitle.Visibility = Visibility.Collapsed;
                loginLabelUName.Visibility = Visibility.Collapsed;
                loginPolyLine.Visibility = Visibility.Collapsed;
                loginTxtBoxIP.Visibility = Visibility.Collapsed;
                loginTxtBoxUName.Visibility = Visibility.Collapsed;
            }
        }


        /// <summary>
        /// Show or hide chat controls depends on the parameter
        /// </summary>
        /// <param name="show"></param>
        private void ShowChat(bool show)
        {
            if (show)
            {
                chatButtonDisconnect.Visibility = Visibility.Visible;
                chatButtonSend.Visibility = Visibility.Visible;
                chatCheckBoxWhisper.Visibility = Visibility.Visible;
                chatCurrentImage.Visibility = Visibility.Visible;
                chatLabelCurrentStatus.Visibility = Visibility.Visible;
                chatLabelCurrentUName.Visibility = Visibility.Visible;
                chatListBoxMsgs.Visibility = Visibility.Visible;
                chatListBoxNames.Visibility = Visibility.Visible;
                chatTxtBoxType.Visibility = Visibility.Visible;
                chatLabelWritingMsg.Visibility = Visibility.Visible;
                chatLabelSendFileStatus.Visibility = Visibility.Visible;
                chatButtonOpenReceived.Visibility = Visibility.Visible;
                chatButtonSendFile.Visibility = Visibility.Visible;
            }
            else
            {
                chatButtonDisconnect.Visibility = Visibility.Collapsed;
                chatButtonSend.Visibility = Visibility.Collapsed;
                chatCheckBoxWhisper.Visibility = Visibility.Collapsed;
                chatCurrentImage.Visibility = Visibility.Collapsed;
                chatLabelCurrentStatus.Visibility = Visibility.Collapsed;
                chatLabelCurrentUName.Visibility = Visibility.Collapsed;
                chatListBoxMsgs.Visibility = Visibility.Collapsed;
                chatListBoxNames.Visibility = Visibility.Collapsed;
                chatTxtBoxType.Visibility = Visibility.Collapsed;
                chatLabelWritingMsg.Visibility = Visibility.Collapsed;
                chatLabelSendFileStatus.Visibility = Visibility.Collapsed;
                chatButtonOpenReceived.Visibility = Visibility.Collapsed;
                chatButtonSendFile.Visibility = Visibility.Collapsed;
            }
        }

        #endregion


        #region UI_Events

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            //Create a folder named WCF_Received_Files in C directory
            DirectoryInfo dir = new DirectoryInfo(rcvFilesPath);
            dir.Create();

            Dictionary<int, Image> images = GetImages();
            //Populate images in the login comboBoc control
            foreach (Image img in images.Values)
            {
                ListBoxItem item = new ListBoxItem();
                item.Width = 90;
                item.Height = 90;
                item.Content = img;

                loginComboBoxImgs.Items.Add(item);
            }
            loginComboBoxImgs.SelectedIndex = 0;

            ShowChat(false);
            ShowLogin(true);

        }

        private void chatButtonOpenReceived_Click(object sender, RoutedEventArgs e)
        {
            //Open WCF_Received_Files folder in windows explorer
            System.Diagnostics.Process.Start(rcvFilesPath);
        }

        private void chatButtonSendFile_Click(object sender, RoutedEventArgs e)
        {
            if (this.receiver != null)
            {
                Stream strm = null;
                try
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Multiselect = false;

                    if (fileDialog.ShowDialog() == DialogResult.HasValue)
                    {
                        return;
                    }

                    strm = fileDialog.OpenFile();
                    if (strm != null)
                    {
                        byte[] buffer = new byte[(int)strm.Length];

                        int i = strm.Read(buffer, 0, buffer.Length);

                        if (i > 0)
                        {
                            SVC.FileMessage fMsg = new FileMessage();
                            fMsg.FileName = fileDialog.SafeFileName;
                            fMsg.Sender = this.localClient.Name;
                            fMsg.Data = buffer;
                            proxy.SendFileAsync(fMsg, this.receiver);
                            proxy.SendFileCompleted += new EventHandler<SendFileCompletedEventArgs>(proxy_SendFileCompleted);
                            chatLabelSendFileStatus.Content = "Sending...";
                        }

                    }
                }
                catch (Exception ex)
                {
                    chatTxtBoxType.Text = ex.Message.ToString();
                }
                finally
                {
                    if (strm != null)
                    {
                        strm.Close();
                    }
                }
            }

        }

        void proxy_SendFileCompleted(object sender, SendFileCompletedEventArgs e)
        {
            chatLabelSendFileStatus.Content = "File Sent";
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (proxy != null)
            {
                if (proxy.State == CommunicationState.Opened)
                {
                    proxy.Disconnect(this.localClient);
                    //dont set proxy.Close(); because isTerminating = true on Disconnect()
                    //and this by default will call HandleProxy() to take care of this.
                }
                else
                {
                    HandleProxy();
                }
            }
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            loginButtonConnect.IsEnabled = false;
            loginLabelStatus.Content = "Connecting..";
            proxy = null;
            Connect();

        }

        void proxy_ConnectCompleted(object sender, ConnectCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                loginLabelStatus.Foreground = new SolidColorBrush(Colors.Red);
                loginTxtBoxUName.Text = e.Error.Message.ToString();
                loginButtonConnect.IsEnabled = true;
            }
            else if (e.Result)
            {
                HandleProxy();
            }
            else if (!e.Result)
            {
                loginLabelStatus.Content = "Name found";
                loginButtonConnect.IsEnabled = true;
            }


        }

        private void chatButtonSend_Click(object sender, RoutedEventArgs e)
        {
            Send();
        }

        private void chatButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            if (proxy != null)
            {
                if (proxy.State == CommunicationState.Faulted)
                {
                    HandleProxy();
                }
                else
                {
                    proxy.DisconnectAsync(this.localClient);
                }
            }
        }


        void chatTxtBoxType_KeyUp(object sender, KeyEventArgs e)
        {
            if (proxy != null)
            {
                if (proxy.State == CommunicationState.Faulted)
                {
                    HandleProxy();
                }
                else
                {
                    if (chatTxtBoxType.Text.Length < 1)
                    {
                        proxy.IsWritingAsync(null);
                    }
                }
            }
        }

        void chatTxtBoxType_KeyDown(object sender, KeyEventArgs e)
        {
            if (proxy != null)
            {
                if (proxy.State == CommunicationState.Faulted)
                {
                    HandleProxy();
                }
                else
                {
                    if (e.Key == Key.Enter)
                    {
                        Send();
                    }
                    else if (chatTxtBoxType.Text.Length < 1)
                    {
                        proxy.IsWritingAsync(this.localClient);
                    }
                }
            }
        }

        void chatListBoxNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If user select an online client, make a client object
            //to be the receiver if the user wants to whisper him.
            ListBoxItem item = chatListBoxNames.SelectedItem as ListBoxItem;
            if (item != null)
            {
                this.receiver = this.OnlineClients[item];
            }
        }


        #endregion

        #region IChatCallback Members

        public void RefreshClients(List<WPFClient.SVC.Client> clients)
        {
            chatListBoxNames.Items.Clear();
            OnlineClients.Clear();
            foreach (SVC.Client c in clients)
            {
                ListBoxItem item = MakeItem(c.AvatarID, c.Name);
                chatListBoxNames.Items.Add(item);
                OnlineClients.Add(item, c);
            }
        }

        public void Receive(WPFClient.SVC.Message msg)
        {
            foreach (SVC.Client c in this.OnlineClients.Values)
            {
                if (c.Name == msg.Sender)
                {
                    ListBoxItem item = MakeItem(c.AvatarID, msg.Sender + " : " + msg.Content);
                    chatListBoxMsgs.Items.Add(item);
                }
            }
            ScrollViewer sv = FindVisualChild(chatListBoxMsgs);
            sv.LineDown();
        }

        public void ReceiveWhisper(WPFClient.SVC.Message msg, WPFClient.SVC.Client receiver)
        {
            foreach (SVC.Client c in this.OnlineClients.Values)
            {
                if (c.Name == msg.Sender)
                {
                    ListBoxItem item = MakeItem(c.AvatarID,
                        msg.Sender + " whispers " + receiver.Name + " : " + msg.Content);
                    chatListBoxMsgs.Items.Add(item);
                }
            }
            ScrollViewer sv = FindVisualChild(chatListBoxMsgs);
            sv.LineDown();
        }

        public void IsWritingCallback(WPFClient.SVC.Client client)
        {
            if (client == null)
            {
                chatLabelWritingMsg.Content = "";
            }
            else
            {
                chatLabelWritingMsg.Content += client.Name + " is writing a message.., ";
            }
        }

        public void ReceiverFile(WPFClient.SVC.FileMessage fileMsg, WPFClient.SVC.Client receiver)
        {
            try
            {
                FileStream fileStrm = new FileStream(rcvFilesPath + fileMsg.FileName, FileMode.Create, FileAccess.ReadWrite);
                fileStrm.Write(fileMsg.Data, 0, fileMsg.Data.Length);
                chatLabelSendFileStatus.Content = "Received file, " + fileMsg.FileName;
            }
            catch (Exception ex)
            {
                chatLabelSendFileStatus.Content = ex.Message.ToString();
            }
        }

        public void UserJoin(WPFClient.SVC.Client client)
        {
            ListBoxItem item = MakeItem(client.AvatarID,
                "------------ " + client.Name + " joined chat ------------");
            chatListBoxMsgs.Items.Add(item);
            ScrollViewer sv = FindVisualChild(chatListBoxMsgs);
            sv.LineDown();
        }

        public void UserLeave(WPFClient.SVC.Client client)
        {
            ListBoxItem item = MakeItem(client.AvatarID,
                "------------ " + client.Name + " left chat ------------");
            chatListBoxMsgs.Items.Add(item);
            ScrollViewer sv = FindVisualChild(chatListBoxMsgs);
            sv.LineDown();
        }

        #region Async

        public IAsyncResult BeginUserLeave(WPFClient.SVC.Client client, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndUserLeave(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginUserJoin(WPFClient.SVC.Client client, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndUserJoin(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceiverFile(WPFClient.SVC.FileMessage fileMsg, WPFClient.SVC.Client receiver, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndReceiverFile(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginIsWritingCallback(WPFClient.SVC.Client client, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndIsWritingCallback(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceiveWhisper(WPFClient.SVC.Message msg, WPFClient.SVC.Client receiver, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndReceiveWhisper(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceive(WPFClient.SVC.Message msg, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndReceive(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginRefreshClients(List<WPFClient.SVC.Client> clients, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndRefreshClients(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion


    }
}
