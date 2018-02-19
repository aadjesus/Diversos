Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Design

Public Class dlgFocalPoints
    Private fpp As New cFocalPoints

    Private Sub dlgEditSelected_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.DialogResult = Windows.Forms.DialogResult.Cancel
        If e.KeyCode = Keys.Enter Then Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub tbarFocalX_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles tbarFocalX.MouseDown, tbarFocalY.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            sender.Value = 0
            UpdateFocusScales()
        End If
    End Sub

    Private Sub tbarFocalY_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarFocalY.Scroll, tbarFocalX.Scroll
        UpdateFocusScales()
    End Sub

    Sub UpdateFocusScales()
        TheShape.FocalPoints.FocusPtX = tbarFocalX.Value / 1000
        TheShape.FocalPoints.FocusPtY = tbarFocalY.Value / 1000
        TheShape.Invalidate()
        lblFx.Text = TheShape.FocalPoints.FocusScales.X
        lblFy.Text = TheShape.FocalPoints.FocusScales.Y
    End Sub

    Private Sub TheShape_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles TheShape.MouseDown, TheShape.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            TheShape.FocalPoints.CenterPtX = e.X / TheShape.Width
            TheShape.FocalPoints.CenterPtY = e.Y / TheShape.Height
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            TheShape.FocalPoints.CenterPtX = 0.5
            TheShape.FocalPoints.CenterPtY = 0.5
        End If
        TheShape.Invalidate()
        UpdateCenterLabels(TheShape.FocalPoints.CenterPoint.X, TheShape.FocalPoints.CenterPoint.Y)
    End Sub

    Sub UpdateCenterLabels(ByVal cx As Double, ByVal cy As Double)
        lblCx.Text = "Center X=" & Math.Round(cx, 4)
        lblCy.Text = "Center Y=" & Math.Round(cy, 4)
    End Sub

    Private Sub dlgFocalPoints_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        fpp = New cFocalPoints(TheShape.FocalPoints.CenterPoint.X, TheShape.FocalPoints.CenterPoint.Y, TheShape.FocalPoints.FocusScales.X, TheShape.FocalPoints.FocusScales.Y)
        tbarFocalX.Value = fpp.FocusScales.X * 1000
        tbarFocalY.Value = fpp.FocusScales.Y * 1000
        UpdateCenterLabels(fpp.CenterPoint.X, fpp.CenterPoint.Y)
        lblFx.Text = fpp.FocusScales.X
        lblFy.Text = fpp.FocusScales.Y
    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        TheShape.FocalPoints = New cFocalPoints(fpp.CenterPoint.X, fpp.CenterPoint.Y, fpp.FocusScales.X, fpp.FocusScales.Y)
    End Sub
End Class
