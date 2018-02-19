' Interaction logic for DemoIntro.xaml
Partial Public Class DemoIntro
    Inherits System.Windows.Controls.UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub hlViewKSBlog_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles hlViewKSBlog.Click
        System.Diagnostics.Process.Start("http://karlshifflett.wordpress.com/")
    End Sub

    Private Sub hlToJSArticle_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles hlToJSArticle.Click
        System.Diagnostics.Process.Start("http://joshsmithonwpf.wordpress.com/2007/10/13/an-article-about-the-wpf-thought-process/")
    End Sub
End Class
