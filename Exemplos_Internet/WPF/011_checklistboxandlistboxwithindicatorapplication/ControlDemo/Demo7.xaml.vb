Partial Public Class Demo7
    Inherits System.Windows.Controls.UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btnViewCustomControlVersions_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim obj As New CodeVersionDemoWindow
        obj.Owner = App.Current.MainWindow
        obj.Topmost = True
        obj.ShowInTaskbar = True
        obj.WindowStartupLocation = WindowStartupLocation.CenterScreen
        obj.ShowDialog()

    End Sub
End Class
