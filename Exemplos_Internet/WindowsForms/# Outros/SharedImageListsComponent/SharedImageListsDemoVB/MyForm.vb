Public Class MyForm

  Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
    MyBase.OnLoad(e)
    If Me.ListView1.Items.Count > 0 AndAlso Me.ListView1.Items(0).SubItems.Count > 0 Then
      Me.ListView1.Items(0).SubItems(1).Text = "Handle: " & Me.ListView1.LargeImageList.Handle.ToString("x").ToUpper
    End If
  End Sub

  Private Sub btnShowInheritedForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowInheritedForm.Click
    Dim frm As New MyInheritedForm
    frm.Show()
  End Sub
End Class