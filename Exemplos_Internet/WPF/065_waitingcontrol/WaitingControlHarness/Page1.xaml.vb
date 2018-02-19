Imports System.Windows
Imports System

Partial Public Class Page1

    Private Sub StartCtl_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles StartCtl.Click
        Try
            Me.WaitingCtl.Start()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Waiting Ctl", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub StopCtl_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles StopCtl.Click
        Try
            Me.WaitingCtl.End()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Waiting Ctl", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub PauseCtl_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PauseCtl.Click
        Try
            Me.WaitingCtl.Pause()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Waiting Ctl", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub ResumeCtl_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles ResumeCtl.Click
        Try
            Me.WaitingCtl.Resume()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Waiting Ctl", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub WaitingCtl_BeforeWaitStart(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.BeforeWaitStart

    End Sub

    Private Sub WaitingCtl_OnWaitStart(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.WaitStarted

    End Sub

    Private Sub WaitingCtl_AfterWaitStart(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.AfterWaitStart

    End Sub

    Private Sub WaitingCtl_BeforeWaitEnd(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.BeforeWaitEnd

    End Sub

    Private Sub WaitingCtl_WaitEnd(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.WaitEnd

    End Sub

    Private Sub WaitingCtl_AfterWaitEnd(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.AfterWaitEnd

    End Sub

    Private Sub WaitingCtl_BeforeWaitPause(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.BeforeWaitPause

    End Sub

    Private Sub WaitingCtl_WaitPause(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.WaitPaused

    End Sub

    Private Sub WaitingCtl_AfterWaitPause(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.AfterWaitPause

    End Sub

    Private Sub WaitingCtl_BeforeWaitResume(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.BeforeWaitResume

    End Sub

    Private Sub WaitingCtl_WaitResume(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.WaitResumed

    End Sub

    Private Sub WaitingCtl_AfterWaitResume(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.AfterWaitResume

    End Sub

    Private Sub WaitingCtl_StateChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles WaitingCtl.StateChanged
        Try
            MessageBox.Show(System.Enum.GetName(GetType(WaitingControl.Waiting.ControlState), Me.WaitingCtl.State))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Waiting Ctl", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub Page1_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        
    End Sub

    Private Sub ErrorCtl_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles ErrorCtl.Click
        Try
            Me.WaitingCtl.ThrowsErrorOnInvalidOperation = Me.ErrorCtl.IsChecked
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Waiting Ctl", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub
End Class
