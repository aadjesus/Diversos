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
using System.Xml;
using System.Windows.Markup;
using System.IO;
using System.Windows.Ink;
using System.Threading;
using System.ServiceModel;
using System.Configuration;

namespace DrawMe
{
    public partial class DrawMeWindow : Window
    {
        public static readonly DependencyProperty FillColorProperty =
           DependencyProperty.Register
           ("FillColor", typeof(Color), typeof(DrawMeWindow),
           new PropertyMetadata(Colors.Aquamarine));

        public DrawMeWindow()
        {
            InitializeComponent();
            LoginControl loginCtrl = new LoginControl(this);
            this.loginCanvas.Children.Add(loginCtrl);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            ExitChat();
            base.OnClosing(e);
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SignInMode();

            this.inkCanv.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
            this.inkCanv.DefaultDrawingAttributes.Width = 10;
            this.inkCanv.DefaultDrawingAttributes.Height = 10;
            this.inkCanv.DefaultDrawingAttributes.Color = FillColor;
        }

        private void ShowChatControls(Visibility visibility)
        {
            this.BorderEditingType.Visibility = visibility;
            this.BorderInkCanvas.Visibility = visibility;
            this.BorderUsersList.Visibility = visibility;
        }

        private void SignInMode()
        {
            ApplicationTypeMessage.Text = "Waiting for connection...";
            this.lvUsers.Items.Clear();
            this.inkCanv.Strokes.Clear();
            ShowChatControls(Visibility.Hidden);
            this.loginCanvas.Visibility = Visibility.Visible;
            this.btnLeave.Visibility = Visibility.Hidden;
        }
        
        public void ChatMode()
        {
            this.btnLeave.Visibility = Visibility.Visible;
            loginCanvas.Visibility = Visibility.Hidden;
            ShowChatControls(Visibility.Visible);

            if (App.s_IsServer)
            {
                ApplicationTypeMessage.Text = "Running as server...";
            }
            else
            {
                ApplicationTypeMessage.Text = "Running as client...";
            }
        }

        private void btnLeave_Click(object sender, RoutedEventArgs e)
        {
            ExitChat();
            this.SignInMode();
        }

        public void ServerDisconnected()
        {
            DrawMeServiceClient.Instance = null;
            this.SignInMode();
        }

        #region Send and receive strokes

        public void OnInkStrokesUpdate(byte[] bytesStroke)
        {
            try
            {
                System.IO.MemoryStream memoryStream = new MemoryStream(bytesStroke);
                this.inkCanv.Strokes = new StrokeCollection(memoryStream);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
            }
        }

        private void SaveGesture()
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();

                this.inkCanv.Strokes.Save(memoryStream);
                   
                memoryStream.Flush();

                DrawMeServiceClient.Instance.SendInkStrokes(memoryStream);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
            }
        }

        private void inkCanv_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            SaveGesture();
        }

        private void inkCanv_StrokeErasing(object sender, InkCanvasStrokeErasingEventArgs e)
        {
            SaveGesture();
        }

        private void inkCanv_StrokeErased(object sender, RoutedEventArgs e)
        {
            SaveGesture();
        }

        private void ExitChat()
        {
            if (DrawMeServiceClient.Instance != null)
            {
                DrawMeServiceClient.Instance.Leave();
                DrawMeServiceClient.Instance.Close();
                DrawMeServiceClient.Instance = null;
            }

            if (App.s_IsServer)
            {
                App.StopServer();
            }
        }

        #endregion SendAndReceiveStrokes

        #region Update Chat Status
        public void UpdateUsersList(List<DrawMeObjects.ChatUser> listChatUsers)
        {
            lvUsers.Items.Clear();
            foreach (DrawMeObjects.ChatUser chatUser in listChatUsers)
            {
                lvUsers.Items.Add(chatUser.NickName);
            }
        }

        public void LastUserDraw(DrawMeObjects.ChatUser chatUser)
        {
            this.AnimatedMessage.Text = string.Format("{0} has just drawn. ", chatUser.NickName);
            //this.AnimationStoryBoard.Begin(this, true);
        }
        #endregion Update Chat Status

        #region Update ink

        private void OnSetFill(object sender, RoutedEventArgs e)
        {
            Microsoft.Samples.CustomControls.ColorPickerDialog cPicker = new Microsoft.Samples.CustomControls.ColorPickerDialog();
            cPicker.StartingColor = FillColor;
            cPicker.Owner = this;

            bool? dialogResult = cPicker.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                FillColor = cPicker.SelectedColor;
                this.inkCanv.DefaultDrawingAttributes.Color = FillColor;
            }
        }

        private Color FillColor
        {
            get
            {
                return (Color)GetValue(FillColorProperty);
            }
            set
            {
                SetValue(FillColorProperty, value);
            }
        }

        private void rbInkType_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            this.inkCanv.EditingMode = (InkCanvasEditingMode)rad.Tag;
        }

        #endregion
    }
}
