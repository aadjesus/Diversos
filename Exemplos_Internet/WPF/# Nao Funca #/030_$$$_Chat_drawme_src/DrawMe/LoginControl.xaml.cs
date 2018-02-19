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
using System.ServiceModel;
using System.Threading;

namespace DrawMe
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        private DrawMeWindow m_mainWindow;
        public LoginControl(DrawMeWindow mainWindow)
        {
            m_mainWindow = mainWindow;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.chatTypeServer.IsChecked = true;
        }

        private void chatTypeServer_Checked(object sender, RoutedEventArgs e)
        {
            this.serverPanel.IsEnabled = false;
        }

        private void chatTypeClient_Checked(object sender, RoutedEventArgs e)
        {
            this.serverPanel.IsEnabled = true;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            EndpointAddress serverAddress;
            if (this.chatTypeServer.IsChecked == true)
            {
                DrawMe.App.s_IsServer = true;
                serverAddress = new EndpointAddress("net.tcp://localhost:8000/DrawMeService/service");
            }
            else
            {
                DrawMe.App.StopServer();
                DrawMe.App.s_IsServer = false;
                if (txtServer.Text.Length == 0)
                {
                    MessageBox.Show("Please enter server name");
                    return;
                }
                serverAddress = new EndpointAddress(string.Format("net.tcp://{0}:8000/DrawMeService/service", txtServer.Text));
            }

            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("Please enter username");
                return;
            }

            if (DrawMeServiceClient.Instance == null)
            {
                if (App.s_IsServer)
                {
                    DrawMe.App.StartServer();
                }

                try
                {
                    ClientCallBack.Instance = new ClientCallBack(SynchronizationContext.Current, m_mainWindow);
                    DrawMeServiceClient.Instance = new DrawMeServiceClient
                                                    (
                                                        new DrawMeObjects.ChatUser
                                                        (
                                                            txtUserName.Text,
                                                            System.Environment.UserName,
                                                            System.Environment.MachineName,
                                                            System.Diagnostics.Process.GetCurrentProcess().Id,
                                                            App.s_IsServer
                                                        ),
                                                        new InstanceContext(ClientCallBack.Instance),
                                                        "DrawMeClientTcpBinding",
                                                        serverAddress
                                                    );
                    DrawMeServiceClient.Instance.Open();
                }
                catch (System.Exception ex)
                {
                    DrawMe.App.StopServer();
                    DrawMeServiceClient.Instance = null;
                    MessageBox.Show(string.Format("Failed to connect to chat server, {0}", ex.Message),this.m_mainWindow.Title);
                    return;
                }
            }

            if (DrawMeServiceClient.Instance.IsUserNameTaken(DrawMeServiceClient.Instance.ChatUser.NickName))
            {
                DrawMeServiceClient.Instance = null;
                MessageBox.Show("Username is already in use");
                return;
            }

            if (DrawMeServiceClient.Instance.Join() == false)
            {
                MessageBox.Show("Failed to join chat room");
                DrawMeServiceClient.Instance = null;
                DrawMe.App.StopServer();
                return;
            }

            this.m_mainWindow.ChatMode();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.m_mainWindow.Close();
        }

    }
}
