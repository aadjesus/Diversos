Partial Public Class CodeVersionDemoWindow
    Inherits System.Windows.Window

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Window1_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Me.lbDemoOne.SelectedIndex = -1
        Me.lbDemoTwo.SelectedIndex = -1
        Me.lbDemoFour.SelectedIndex = -1
        Me.lbDemoFive.SelectedIndex = -1
    End Sub

    Private Sub hlToJSArticle_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles hlToJSArticle.Click
        System.Diagnostics.Process.Start("http://joshsmithonwpf.wordpress.com/2007/10/13/an-article-about-the-wpf-thought-process/")
    End Sub
End Class
