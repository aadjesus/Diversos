Public Class myEditorForm

	Public IsCanceled As Boolean = False

	Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
		Me.IsCanceled = False
		Me.Hide()
	End Sub

	Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
		Me.IsCanceled = True
		Me.Hide()
	End Sub

	Public Sub UpdateTextBox1()
		TextBox1.Text = MonthCalendar1.SelectionStart.ToShortDateString & " " & TextBox2.Text
	End Sub

	Private Sub MonthCalendar1_DateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateChanged
		UpdateTextBox1()
	End Sub

	Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
		UpdateTextBox1()
	End Sub

End Class