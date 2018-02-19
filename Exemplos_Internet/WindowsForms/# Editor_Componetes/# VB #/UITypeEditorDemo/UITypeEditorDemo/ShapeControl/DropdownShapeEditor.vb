Imports System.Windows.Forms.Design
Imports System.drawing.Drawing2D
Imports System.ComponentModel

<ToolboxItem(False), ToolboxItemFilter("Prevent", ToolboxItemFilterType.Prevent)> _
Public Class DropdownShapeEditor
    Inherits System.Windows.Forms.UserControl

    Private _editorService As IWindowsFormsEditorService = Nothing

    Public Sub New(ByVal editorService As IWindowsFormsEditorService)
        InitializeComponent()
        _editorService = editorService
    End Sub

    Private _TheShape As New Shape
    Public Property TheShape() As Shape
        Get
            Return _TheShape
        End Get
        Set(ByVal value As Shape)
            _TheShape = value
            Me.Invalidate()
        End Set
    End Property

    Private Sub Shape_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles sCircle.MouseEnter, sRectangle.MouseEnter, sTriangle.MouseEnter, sDiamond.MouseEnter, sHeart.MouseEnter, sStar.MouseEnter

        sender.FillType = Me.TheShape.FillType
        sender.FillTypeLinear = Me.TheShape.FillTypeLinear
        sender.ColorFillSolid = Me.TheShape.ColorFillSolid
        sender.BorderWidth = Me.TheShape.BorderWidth
        sender.BorderColor = Me.TheShape.BorderColor
        sender.BorderStyle = Me.TheShape.BorderStyle
        sender.RadiusInner = Me.TheShape.RadiusInner
        sender.FocalPoints = Me.TheShape.FocalPoints
        sender.ColorFillBlend = Me.TheShape.ColorFillBlend

    End Sub

    Private Sub Shape_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles sCircle.MouseLeave, sRectangle.MouseLeave, sTriangle.MouseLeave, sDiamond.MouseLeave, sHeart.MouseLeave, sStar.MouseLeave

        sender.RadiusInner = 0
        sender.ColorFillSolid = SystemColors.Control
        sender.BorderWidth = 1
        sender.BorderColor = Color.Black
        sender.BorderStyle = DashStyle.Solid
        sender.FillType = UITypeEditorDemo.Shape.eFillType.Solid
    End Sub

    Private Sub sCircle_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles sCircle.Click, sRectangle.Click, sTriangle.Click, sDiamond.Click, sHeart.Click, sStar.Click

        Me.TheShape.Shape = sender.Shape

        _editorService.CloseDropDown()

    End Sub


End Class
