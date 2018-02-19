Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Design

Public Class dlgCorners
    Private cnrs As CornersProperty = New CornersProperty()

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        TheShape.Corners.UpperLeft = cnrs.UpperLeft
        TheShape.Corners.UpperRight = cnrs.UpperRight
        TheShape.Corners.LowerLeft = cnrs.LowerLeft
        TheShape.Corners.LowerRight = cnrs.LowerRight

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub tbarUpperLeft_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarUpperLeft.Scroll
        TheShape.Corners.UpperLeft = CShort(tbarUpperLeft.Value)
        TheShape.Invalidate()
    End Sub

    Private Sub tbarUpperRight_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarUpperRight.Scroll
        TheShape.Corners.UpperRight = CShort(tbarUpperRight.Value)
        TheShape.Invalidate()
    End Sub

    Private Sub tbarLowerLeft_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarLowerLeft.Scroll
        TheShape.Corners.LowerLeft = CShort(tbarLowerLeft.Value)
        TheShape.Invalidate()
    End Sub

    Private Sub tbarLowerRight_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarLowerRight.Scroll
        TheShape.Corners.LowerRight = CShort(tbarLowerRight.Value)
        TheShape.Invalidate()
    End Sub

    Private Sub tbarAll_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarAll.Scroll
        tbarUpperLeft.Value = tbarAll.Value
        tbarUpperRight.Value = tbarAll.Value
        tbarLowerLeft.Value = tbarAll.Value
        tbarLowerRight.Value = tbarAll.Value
        TheShape.Corners.UpperLeft = CShort(tbarAll.Value)
        TheShape.Corners.UpperRight = CShort(tbarAll.Value)
        TheShape.Corners.LowerLeft = CShort(tbarAll.Value)
        TheShape.Corners.LowerRight = CShort(tbarAll.Value)
        TheShape.Invalidate()

    End Sub

    Private Sub dlgCorners_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cnrs.UpperLeft = TheShape.Corners.UpperLeft
        cnrs.UpperRight = TheShape.Corners.UpperRight
        cnrs.LowerLeft = TheShape.Corners.LowerLeft
        cnrs.LowerRight = TheShape.Corners.LowerRight
    End Sub

    Private Sub HSBarSample_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HSBarSample.Scroll
        TheShape.Value = HSBarSample.Value
    End Sub

End Class
