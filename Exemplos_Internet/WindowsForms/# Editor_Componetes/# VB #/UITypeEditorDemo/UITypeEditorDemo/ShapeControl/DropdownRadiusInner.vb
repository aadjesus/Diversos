Imports System.Windows.Forms.Design
Imports System.drawing.Drawing2D
Imports System.ComponentModel

<ToolboxItem(False), ToolboxItemFilter("Prevent", ToolboxItemFilterType.Prevent)> _
Public Class DropdownRadiusInner
    Inherits System.Windows.Forms.UserControl

    Private _editorService As IWindowsFormsEditorService = Nothing

    Public Sub New(ByVal editorService As IWindowsFormsEditorService)
        InitializeComponent()
        _editorService = editorService
    End Sub

    Private Sub butApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butApply.Click
        _editorService.CloseDropDown()
    End Sub

    Private Sub tbarRadiusInner_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarRadiusInner.Scroll
        Me.TheShape.RadiusInner = tbarRadiusInner.Value / 100
        Me.lblValue.Text = Me.TheShape.RadiusInner
    End Sub

    Private Sub llThin_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llThin.LinkClicked
        Me.TheShape.RadiusInner = -0.25
        tbarRadiusInner.Value = -25
        Me.lblValue.Text = Me.TheShape.RadiusInner
    End Sub

    Private Sub llNormal_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llNormal.LinkClicked
        Me.TheShape.RadiusInner = 0
        tbarRadiusInner.Value = 0
        Me.lblValue.Text = Me.TheShape.RadiusInner
    End Sub

    Private Sub llPentagon_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llPentagon.LinkClicked
        Me.TheShape.RadiusInner = 0.43
        tbarRadiusInner.Value = 43
        Me.lblValue.Text = Me.TheShape.RadiusInner
    End Sub

End Class

