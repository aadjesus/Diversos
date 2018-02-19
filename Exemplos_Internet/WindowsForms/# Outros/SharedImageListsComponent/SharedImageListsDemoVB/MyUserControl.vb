Public Class MyUserControl
  Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    MyBase.OnPaint(e)
    If Me.LargeImageList.Images.Count > 0 Then
      e.Graphics.DrawString(Me.Name, Me.Font, SystemBrushes.ControlText, 0, 0)
      Dim sz As Size = System.Windows.Forms.TextRenderer.MeasureText("yY", Me.Font)
      Me.LargeImageList.Draw(e.Graphics, 0, sz.Height + 2, 0)
      e.Graphics.DrawString("LargeImageList.Handle: " & Me.LargeImageList.Handle.ToString("x").ToUpper, Me.Font, SystemBrushes.ControlText, 0, sz.Height + Me.LargeImageList.ImageSize.Height + 4)
    End If
  End Sub
End Class
