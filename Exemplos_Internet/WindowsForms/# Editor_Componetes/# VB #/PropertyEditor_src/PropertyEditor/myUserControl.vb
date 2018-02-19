Public Class myUserControl

	Public IsCanceled As Boolean = False

	Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
		Me.IsCanceled = False
		Me.Hide()
	End Sub

	Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
		Me.IsCanceled = True
		Me.Hide()
	End Sub

	Private Sub TextBox1_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
		If e.KeyCode = Keys.Escape Then Me.IsCanceled = True
	End Sub

	Private Sub TextBox2_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles TextBox2.PreviewKeyDown
		If e.KeyCode = Keys.Escape Then Me.IsCanceled = True
	End Sub

End Class

