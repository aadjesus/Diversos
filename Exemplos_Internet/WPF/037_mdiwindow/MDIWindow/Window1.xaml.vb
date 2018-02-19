Imports System
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation

Partial Public Class Window1

    Private _WinMgr As WindowMgr
    Public Sub New()
        MyBase.New()
        Me.InitializeComponent()
        ' Insert code required on object creation below this point.
        Try
            _WinMgr = WindowMgr.GetWinMgr()
            WindowContainer.Content = _WinMgr
            _WinMgr.Visibility = Windows.Visibility.Visible
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub cmdAddWindow_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdAddWindow.Click
        Dim WebPage As New Frame()
        WebPage.Navigate(New Uri("http://www.rediff.com"))
        _WinMgr.AddWindow("New Page", WebPage)
    End Sub

    Private Sub Window_LayoutUpdated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Window.LayoutUpdated
        _WinMgr.Height = WindowContainer.ActualHeight
        _WinMgr.Width = WindowContainer.ActualWidth
    End Sub

    Private Sub Window_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Window.Loaded
        Application.Current.Windows(0).WindowState = Windows.WindowState.Maximized
    End Sub
End Class
