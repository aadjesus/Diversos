Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Design

#Region "Shape Class"

<Designer(GetType(ShapeDesigner))> _
Public Class Shape
    Inherits Control


    Private _DesignerCenter As Boolean = False

    <Description("The Shape"), _
    Category("Shape")> _
    Public Property DesignerCenter() As Boolean
        Get
            Return _DesignerCenter
        End Get
        Set(ByVal value As Boolean)
            _DesignerCenter = value
        End Set
    End Property

#Region "Shape Properties"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    End Sub

#Region "Corners Expandable Property"

    'Corners Property is defined in the Corners Converter Class
    'to use the ExpandableObjectConverter to simulate the Padding Property

    Private _Corners As CornersProperty = New CornersProperty()

    <Category("Shape"), _
      Description("Get or Set the Corner Radii"), _
      RefreshProperties(RefreshProperties.All), _
      DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
      Public Property Corners() As CornersProperty
        Get
            Return _Corners
        End Get
        Set(ByVal Value As CornersProperty)
            _Corners = Value
            Me.Refresh()
        End Set

    End Property
#End Region 'Corners Expandable Property

    Enum eShape
        Ellipse
        Rectangle
        Triangle
        Diamond
        Heart
        Star
    End Enum

    Private _BrushHatchEasy As HatchStyle = HatchStyle.LargeConfetti

    <Editor(GetType(HatchStyleEditorEasy), GetType(UITypeEditor)), _
    Description("The Shape"), _
    Category("Shape")> _
    Public Property BrushHatchEasy() As HatchStyle
        Get
            Return _BrushHatchEasy
        End Get
        Set(ByVal value As HatchStyle)
            _BrushHatchEasy = value
            Me.Invalidate()
        End Set
    End Property

    Private _BrushHatch As HatchStyle = HatchStyle.LargeConfetti

    <Editor(GetType(HatchStyleEditor), GetType(UITypeEditor)), _
    Description("The Shape"), _
    Category("Shape")> _
    Public Property BrushHatch() As HatchStyle
        Get
            Return _BrushHatch
        End Get
        Set(ByVal value As HatchStyle)
            _BrushHatch = value
            Me.Invalidate()
        End Set
    End Property

    Private _Shape As eShape

    <Editor(GetType(ShapeTypeEditor), GetType(UITypeEditor)), _
    Description("The Shape"), _
    Category("Shape")> _
    Public Property Shape() As eShape
        Get
            Return _Shape
        End Get
        Set(ByVal value As eShape)
            _Shape = value
            Me.Invalidate()
        End Set
    End Property

    Private _FocalPoints As cFocalPoints = New cFocalPoints(0.5, 0.5, 0, 0)

    <Editor(GetType(FocalTypeEditor), GetType(UITypeEditor)), _
    Description("The CenterPoint and FocusScales for the ColorBlend"), _
    Category("Shape"), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public Property FocalPoints() As cFocalPoints
        Get
            Return _FocalPoints
        End Get
        Set(ByVal value As cFocalPoints)
            _FocalPoints = value
            CenterPtTracker.TrackerRectangle = CenterPtTrackerRectangle()
            FocusPtTracker.TrackerRectangle = FocusPtTrackerRectangle()
            Me.Invalidate()
        End Set
    End Property

    Private Function CenterPtTrackerRectangle() As Rectangle
        Try
            Return New Rectangle((Me.FocalPoints.CenterPoint.X * Me.Width) - 5, _
                        (Me.FocalPoints.CenterPoint.Y * Me.Height) - 5, 10, 10)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Function FocusPtTrackerRectangle() As Rectangle
        Try
            Return New Rectangle((Me.FocalPoints.FocusScales.X * Me.Width) - 5, _
                            (Me.FocalPoints.FocusScales.Y * Me.Height) - 5, 10, 10)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private _CenterPtTracker As New DesignerRectTracker

    <Browsable(False)> _
    <Category("Shape")> _
    Public Property CenterPtTracker() As DesignerRectTracker
        Get
            Return _CenterPtTracker
        End Get
        Set(ByVal value As DesignerRectTracker)
            _CenterPtTracker = value
        End Set
    End Property

    Private _FocusPtTracker As New DesignerRectTracker

    <Browsable(False)> _
    <Category("Shape")> _
    Public Property FocusPtTracker() As DesignerRectTracker
        Get
            Return _FocusPtTracker
        End Get
        Set(ByVal value As DesignerRectTracker)
            _FocusPtTracker = value
        End Set
    End Property

    Private _BorderStyle As DashStyle = DashStyle.Solid
    <Category("Shape")> _
    <Description("The line dash style used to draw state borders.")> _
    <Browsable(True)> _
    <Editor(GetType(BorderStyleEditor), GetType(UITypeEditor))> _
    Public Property BorderStyle() As DashStyle
        Get
            Return _BorderStyle
        End Get
        Set(ByVal value As DashStyle)
            _BorderStyle = value
            Me.Invalidate()
        End Set
    End Property

    Private _RadiusInner As Single = 0
    <Editor(GetType(RadiusInnerTypeEditor), GetType(UITypeEditor)), _
    Browsable(True), _
    Description("The Inner Radius for the Star Shape"), _
    Category("Shape")> _
    Public Property RadiusInner() As Single
        Get
            Return _RadiusInner
        End Get
        Set(ByVal value As Single)
            _RadiusInner = value
            Me.Invalidate()
        End Set
    End Property

    Private _BorderShow As Boolean = True
    <Description("Show or not show the Border"), _
    Category("Shape"), _
    DefaultValue(True)> _
    Public Property BorderShow() As Boolean
        Get
            Return _BorderShow
        End Get
        Set(ByVal value As Boolean)
            _BorderShow = value
            Me.Invalidate()
        End Set
    End Property

    Private _BorderWidth As Single = 2
    <Description("The Width of the border around the shape")> _
    <Category("Shape")> _
    <DefaultValue(2)> _
    Public Property BorderWidth() As Single
        Get
            Return _BorderWidth
        End Get
        Set(ByVal value As Single)
            _BorderWidth = value
            Me.Invalidate()
        End Set
    End Property

    Private _ColorFillSolid As Color = Color.White
    <Description("The Solid Color to fill the shape"), _
    Category("Shape")> _
    Public Property ColorFillSolid() As Color
        Get
            Return _ColorFillSolid
        End Get
        Set(ByVal value As Color)
            _ColorFillSolid = value
            Me.Invalidate()
        End Set
    End Property

    Private _ColorFillSolidB As Color = Color.Blue
    <Description("The Solid Color to fill the shape"), _
    Category("Shape")> _
    Public Property ColorFillSolidB() As Color
        Get
            Return _ColorFillSolidB
        End Get
        Set(ByVal value As Color)
            _ColorFillSolidB = value
            Me.Invalidate()
        End Set
    End Property

    Private _ColorFillBlend As cBlendItems = New cBlendItems(New Color() {Color.White, Color.White}, New Single() {0, 1})
    <Description("The ColorBlend to fill the shape"), _
    Category("Shape"), _
    Editor(GetType(BlendTypeEditor), GetType(UITypeEditor))> _
    Public Property ColorFillBlend() As cBlendItems
        Get
            Return _ColorFillBlend
        End Get
        Set(ByVal value As cBlendItems)
            _ColorFillBlend = value
            Me.Invalidate()
        End Set
    End Property

    Enum eFillType
        Solid
        GradientLinear
        GradientPath
        Hatch
    End Enum

    Private _FillType As eFillType = eFillType.Solid
    <Description("The Fill Type to apply to the Shape")> _
    <Category("Shape")> _
    Public Property FillType() As eFillType
        Get
            Return _FillType
        End Get
        Set(ByVal value As eFillType)
            _FillType = value
            Me.Invalidate()
        End Set
    End Property

    Private _FillTypeLinear As LinearGradientMode = LinearGradientMode.Horizontal
    <Description("The Linear Blend type"), _
    Category("Shape")> _
    Public Property FillTypeLinear() As LinearGradientMode
        Get
            Return _FillTypeLinear
        End Get
        Set(ByVal value As LinearGradientMode)
            _FillTypeLinear = value
            Me.Invalidate()
        End Set
    End Property

    Private _BorderColor As Color = Color.Black
    <Description("The Color for the border"), _
    Category("Shape")> _
    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
            Me.Invalidate()
        End Set
    End Property

    Private _RegionClip As Boolean = False
    <Description("Clip off outside area")> _
    <Category("Shape")> _
    <RefreshProperties(RefreshProperties.All)> _
    Public Property RegionClip() As Boolean
        Get
            Return _RegionClip
        End Get
        Set(ByVal value As Boolean)
            _RegionClip = value
            Me.Invalidate()
        End Set
    End Property

#End Region

    Private Sub Shape_MouseDown(ByVal sender As Object, _
      ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If DesignMode Then
            'Because of the GetHitTest Override in the
            'Designer Manual Selection is needed
            Dim selectservice As ISelectionService = _
                 CType(GetService(GetType(ISelectionService)), ISelectionService)
            Dim selection As New ArrayList
            selection.Clear()
            selectservice.SetSelectedComponents(selection, SelectionTypes.Replace)
            selection.Add(Me)
            selectservice.SetSelectedComponents(selection, SelectionTypes.Add)

            'FocusPoints Reset
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If Me.CenterPtTracker.IsActive Then
                    Me.FocalPoints = New cFocalPoints( _
                        New PointF(0.5, 0.5), _
                        Me.FocalPoints.FocusScales)
                    Me.Invalidate()
                ElseIf Me.FocusPtTracker.IsActive Then
                    Me.FocalPoints = New cFocalPoints( _
                        Me.FocalPoints.CenterPoint, _
                        New PointF(0, 0))
                    Me.Invalidate()
                End If
            End If
        End If
    End Sub

    Private Sub Shape_MouseMove(ByVal sender As Object, _
      ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If DesignMode Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                If Me.CenterPtTracker.IsActive Then
                    Me.FocalPoints = New cFocalPoints( _
                        New PointF(e.X / Me.Width, e.Y / Me.Height), _
                        Me.FocalPoints.FocusScales)
                    Me.Invalidate()
                ElseIf Me.FocusPtTracker.IsActive Then
                    Me.FocalPoints = New cFocalPoints( _
                        Me.FocalPoints.CenterPoint, _
                        New PointF(e.X / Me.Width, e.Y / Me.Height))
                    Me.Invalidate()
                End If
            End If
        End If
    End Sub

    Private Sub Shape_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim adjWidth As Single = Me.Width - BorderWidth
        Dim adjHeight As Single = Me.Height - BorderWidth

        Dim rect As RectangleF = New RectangleF(BorderWidth / 2, BorderWidth / 2, _
            adjWidth - 1, adjHeight - 1)

        Dim gp As New GraphicsPath
        gp = GetPath(Me.Shape, rect, Me.RadiusInner)

        Select Case Me.FillType
            Case eFillType.Solid
                Using br As Brush = New SolidBrush(ColorFillSolid)
                    e.Graphics.FillPath(br, gp)
                End Using

            Case eFillType.GradientPath
                Using br As PathGradientBrush = New PathGradientBrush(gp)
                    Dim cb As New ColorBlend
                    cb.Colors = Me.ColorFillBlend.iColor
                    cb.Positions = Me.ColorFillBlend.iPoint

                    br.FocusScales = FocalPoints.FocusScales
                    br.CenterPoint = New PointF(adjWidth * FocalPoints.CenterPoint.X, adjHeight * FocalPoints.CenterPoint.Y)
                    br.InterpolationColors = cb

                    e.Graphics.FillPath(br, gp)
                End Using

            Case eFillType.GradientLinear
                Using br As LinearGradientBrush = New LinearGradientBrush(rect, Color.White, Color.White, FillTypeLinear)
                    Dim cb As New ColorBlend
                    cb.Colors = Me.ColorFillBlend.iColor
                    cb.Positions = Me.ColorFillBlend.iPoint

                    br.InterpolationColors = cb

                    e.Graphics.FillPath(br, gp)
                End Using

            Case eFillType.Hatch
                Using br As HatchBrush = New HatchBrush(_BrushHatch, _ColorFillSolid, _ColorFillSolidB)
                    e.Graphics.FillPath(br, gp)
                End Using

        End Select

        If BorderWidth > 0 And BorderShow Then
            Using pn As Pen = New Pen(BorderColor, BorderWidth)
                If BorderWidth > 1 And Shape = eShape.Heart Then pn.LineJoin = LineJoin.Round
                pn.DashStyle = BorderStyle
                e.Graphics.DrawPath(pn, gp)
            End Using
        End If

        Me.Region = Nothing
        If RegionClip Then
            'Because the Region doesn't clipp exactly to the path some tweaking is needed
            rect = New RectangleF((BorderWidth / 2) - 0.5, (BorderWidth / 2) - 0.5, _
                adjWidth, adjHeight)
            If Me.Shape = eShape.Rectangle Then rect = New RectangleF(BorderWidth / 2, BorderWidth / 2, _
                adjWidth, adjHeight)

            Dim mRegion As New Region(GetPath(Me.Shape, rect, Me.RadiusInner))
            Me.Region = mRegion
            mRegion.Dispose()
        End If

        gp.Dispose()
    End Sub

#Region "ShapePaths"

    Public Function GetPath(ByVal Shape As eShape, ByVal rect As RectangleF, Optional ByVal RI As Single = 0.0) As GraphicsPath
        Dim gp As New GraphicsPath

        Select Case Shape
            Case eShape.Rectangle
                gp = GetRoundedRectPath(rect, Corners)

            Case eShape.Ellipse
                gp.AddEllipse(rect)

            Case eShape.Triangle
                Dim pts() As PointF = New PointF() { _
                    New PointF(rect.Width / 2, rect.Y), _
                    New PointF(rect.Width, rect.Y + rect.Height), _
                    New PointF(rect.X, rect.Y + rect.Height)}
                gp.AddPolygon(pts)

            Case eShape.Diamond
                Dim pts() As PointF = New PointF() { _
                    New PointF(rect.Width / 2, rect.Y), _
                    New PointF((rect.X + rect.Width), (rect.Y + rect.Height) / 2), _
                    New PointF(rect.X + (rect.Width / 2), rect.Y + rect.Height), _
                    New PointF(rect.X, (rect.Y + rect.Height) / 2)}
                gp.AddPolygon(pts)

            Case eShape.Star
                gp = DrawStar(0, 0, rect, RI)
            Case eShape.Heart
                gp = DrawHeart(0, 0, rect)

        End Select

        Return gp
        gp.Dispose()
    End Function

    Function DrawStar(ByVal Xc As Single, ByVal Yc As Single, ByVal rect As RectangleF, ByVal RadiusInner As Single) As GraphicsPath
        Dim gp As New GraphicsPath
        Dim xRadiusOuter As Single = (rect.Width / 2)
        Dim yRadiusOuter As Single = (rect.Height / 2)
        Xc += xRadiusOuter
        Yc += yRadiusOuter
        ' RadiusInner and InnerRadius: determines how far from the center the inner vertices of the star are.
        ' RadiusOuter: determines the size of the star.
        ' xc, yc: determine the location of the star.
        Dim sin36 As Single = CType(Math.Sin(36.0 * Math.PI / 180.0), Single)
        Dim sin72 As Single = CType(Math.Sin(72.0 * Math.PI / 180.0), Single)
        Dim cos36 As Single = CType(Math.Cos(36.0 * Math.PI / 180.0), Single)
        Dim cos72 As Single = CType(Math.Cos(72.0 * Math.PI / 180.0), Single)
        Dim xInnerRadius As Single = (xRadiusOuter * cos72 / cos36) + (xRadiusOuter * RadiusInner)
        Dim yInnerRadius As Single = (yRadiusOuter * cos72 / cos36) + (yRadiusOuter * RadiusInner)
        Yc += (yRadiusOuter - (Yc - yRadiusOuter * cos72)) / 4
        Dim pts(9) As PointF
        pts(0) = New PointF(Xc, Yc - yRadiusOuter)
        pts(1) = New PointF(Xc + xInnerRadius * sin36, Yc - yInnerRadius * cos36)
        pts(2) = New PointF(Xc + xRadiusOuter * sin72, Yc - yRadiusOuter * cos72)
        pts(3) = New PointF(Xc + xInnerRadius * sin72, Yc + yInnerRadius * cos72)
        pts(4) = New PointF(Xc + xRadiusOuter * sin36, Yc + yRadiusOuter * cos36)
        pts(5) = New PointF(Xc, Yc + yInnerRadius)
        pts(6) = New PointF(Xc - xRadiusOuter * sin36, Yc + yRadiusOuter * cos36)
        pts(7) = New PointF(Xc - xInnerRadius * sin72, Yc + yInnerRadius * cos72)
        pts(8) = New PointF(Xc - xRadiusOuter * sin72, Yc - yRadiusOuter * cos72)
        pts(9) = New PointF(Xc - xInnerRadius * sin36, Yc - yInnerRadius * cos36)

        gp.AddPolygon(pts)

        Return gp

    End Function

    Function DrawHeart(ByVal Xc As Single, ByVal Yc As Single, ByVal rect As RectangleF) As GraphicsPath
        Xc += rect.X
        Yc += rect.Y
        Dim gp As New GraphicsPath
        Dim RadiusOuter As Single = ((rect.Width - (rect.X / 2)) / 2)
        Dim HeartTopLeftSquare As New Rectangle(Xc, _
           Yc, RadiusOuter, RadiusOuter)
        Dim HeartTopRightSquare As New Rectangle(Xc + _
           RadiusOuter, Yc, RadiusOuter, RadiusOuter)
        gp.AddArc(HeartTopLeftSquare, 135.0F, 210.0F)
        gp.AddArc(HeartTopRightSquare, 180.0F, 210.0F)
        gp.AddLine(Xc + RadiusOuter, Yc + (RadiusOuter * 2), Xc + RadiusOuter, Yc + (RadiusOuter * 2))
        gp.CloseFigure()
        Return gp
    End Function

    Public Function GetRoundedRectPath(ByVal BaseRect As RectangleF, ByVal rCorners As CornersProperty) As GraphicsPath

        Dim ArcRect As RectangleF
        Dim MyPath As New Drawing2D.GraphicsPath()
        If rCorners.All = -1 Then
            With MyPath
                ' top left arc
                If rCorners.UpperLeft = 0 Then
                    .AddLine(BaseRect.X, BaseRect.Y, BaseRect.X, BaseRect.Y)
                Else
                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.UpperLeft * 2, rCorners.UpperLeft * 2))
                    .AddArc(ArcRect, 180, 90)
                End If

                ' top right arc
                If rCorners.UpperRight = 0 Then
                    .AddLine(BaseRect.X + (rCorners.UpperLeft), BaseRect.Y, BaseRect.Right - (rCorners.UpperRight), BaseRect.Top)
                Else
                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.UpperRight * 2, rCorners.UpperRight * 2))
                    ArcRect.X = BaseRect.Right - (rCorners.UpperRight * 2)
                    .AddArc(ArcRect, 270, 90)
                End If

                ' bottom right arc
                If rCorners.LowerRight = 0 Then
                    .AddLine(BaseRect.Right, BaseRect.Top + (rCorners.UpperRight), BaseRect.Right, BaseRect.Bottom - (rCorners.LowerRight))
                Else
                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.LowerRight * 2, rCorners.LowerRight * 2))
                    ArcRect.Y = BaseRect.Bottom - (rCorners.LowerRight * 2)
                    ArcRect.X = BaseRect.Right - (rCorners.LowerRight * 2)
                    .AddArc(ArcRect, 0, 90)
                End If

                ' bottom left arc
                If rCorners.LowerLeft = 0 Then
                    .AddLine(BaseRect.Right - (rCorners.LowerRight), BaseRect.Bottom, BaseRect.X - (rCorners.LowerLeft), BaseRect.Bottom)
                Else
                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.LowerLeft * 2, rCorners.LowerLeft * 2))
                    ArcRect.Y = BaseRect.Bottom - (rCorners.LowerLeft * 2)
                    .AddArc(ArcRect, 90, 90)
                End If

                .CloseFigure()
            End With
        Else
            With MyPath
                If rCorners.All = 0 Then
                    .AddRectangle(BaseRect)
                Else

                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.All * 2, rCorners.All * 2))
                    ' top left arc
                    .AddArc(ArcRect, 180, 90)

                    ' top right arc
                    ArcRect.X = BaseRect.Right - (rCorners.All * 2)
                    .AddArc(ArcRect, 270, 90)

                    ' bottom right arc
                    ArcRect.Y = BaseRect.Bottom - (rCorners.All * 2)
                    .AddArc(ArcRect, 0, 90)

                    ' bottom left arc
                    ArcRect.X = BaseRect.Left
                    .AddArc(ArcRect, 90, 90)

                End If
                .CloseFigure()
            End With
        End If
        Return MyPath
    End Function

#End Region

    Private Sub Shape_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        CenterPtTracker.TrackerRectangle = CenterPtTrackerRectangle()
        FocusPtTracker.TrackerRectangle = FocusPtTrackerRectangle()
    End Sub
End Class

#End Region 'Shape Class

#Region "DesignerRectTracker"

Public Class DesignerRectTracker

    Private _TrackerRectangle As New RectangleF
    Public Property TrackerRectangle() As RectangleF
        Get
            Return _TrackerRectangle
        End Get
        Set(ByVal value As RectangleF)
            _TrackerRectangle = value
        End Set
    End Property

    Private _IsActive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property

    Sub New()
        _TrackerRectangle = New Rectangle(0, 0, 10, 10)
        _IsActive = False
    End Sub

End Class

#End Region 'DesignerRectTracker

#Region "Dropdown Editors"

#Region "ShapeTypeEditor - UITypeEditor"

Public Class ShapeTypeEditor
    Inherits UITypeEditor

    Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If Not context Is Nothing Then
            Return UITypeEditorEditStyle.DropDown
        End If
        Return (MyBase.GetEditStyle(context))
    End Function

    Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (Not context Is Nothing) And (Not provider Is Nothing) Then
            ' Access the property browser's UI display service, IWindowsFormsEditorService
            Dim editorService As IWindowsFormsEditorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If Not editorService Is Nothing Then
                ' Create an instance of the UI editor, passing a reference to the editor service
                Dim dropDownEditor As DropdownShapeEditor = New DropdownShapeEditor(editorService)

                ' Pass the UI editor the current property value
                Dim Instance As Shape
                If context.Instance.GetType Is GetType(Shape) Then
                    Instance = CType(context.Instance, Shape)
                Else
                    'This is needed if using in a SmartTag
                    Instance = CType(context.Instance.CurrShape, Shape)
                End If
                dropDownEditor.TheShape.Shape = Instance.Shape
                dropDownEditor.TheShape.FillType = Instance.FillType
                dropDownEditor.TheShape.FillTypeLinear = Instance.FillTypeLinear
                dropDownEditor.TheShape.ColorFillSolid = Instance.ColorFillSolid
                dropDownEditor.TheShape.BorderWidth = Instance.BorderWidth
                dropDownEditor.TheShape.BorderColor = Instance.BorderColor
                dropDownEditor.TheShape.BorderStyle = Instance.BorderStyle
                dropDownEditor.TheShape.RadiusInner = Instance.RadiusInner
                dropDownEditor.TheShape.FocalPoints = New cFocalPoints( _
                            Instance.FocalPoints.CenterPoint, _
                            Instance.FocalPoints.FocusScales)
                dropDownEditor.TheShape.ColorFillBlend = Instance.ColorFillBlend
                dropDownEditor.TheShape.Corners = Instance.Corners

                ' Display the UI editor
                editorService.DropDownControl(dropDownEditor)

                ' Return the new property value from the editor
                Return dropDownEditor.TheShape.Shape
            End If
        End If
        Return MyBase.EditValue(context, provider, value)
    End Function

End Class
#End Region 'ShapeTypeEditor - UITypeEditor

#Region "RadiusInnerTypeEditor - UITypeEditor"

Public Class RadiusInnerTypeEditor
    Inherits UITypeEditor

    Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If Not context Is Nothing Then
            Return UITypeEditorEditStyle.DropDown
        End If
        Return (MyBase.GetEditStyle(context))
    End Function

    Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (Not context Is Nothing) And (Not provider Is Nothing) Then
            ' Access the property browser's UI display service, IWindowsFormsEditorService
            Dim editorService As IWindowsFormsEditorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If Not editorService Is Nothing Then
                ' Create an instance of the UI editor, passing a reference to the editor service
                Dim dropDownEditor As DropdownRadiusInner = New DropdownRadiusInner(editorService)

                ' Pass the UI editor the current property value
                Dim Instance As Shape
                If context.Instance.GetType Is GetType(Shape) Then
                    Instance = CType(context.Instance, Shape)
                Else
                    'This is needed if using in a SmartTag
                    Instance = CType(context.Instance.CurrShape, Shape)
                End If

                With dropDownEditor
                    Dim ratio As Single
                    If Instance.Width > Instance.Height Then
                        .TheShape.Height = .TheShape.Width * (Instance.Height / Instance.Width)
                        .TheShape.Top = (.panShapeHolder.Height - .TheShape.Height) / 2
                        ratio = .TheShape.Height / Instance.Height
                    Else
                        .TheShape.Width = .TheShape.Height * (Instance.Width / Instance.Height)
                        .TheShape.Left = (.panShapeHolder.Width - .TheShape.Width) / 2
                        ratio = .TheShape.Width / Instance.Width
                    End If

                    .lblValue.Text = CType(value, Single)
                    .TheShape.RadiusInner = CType(value, Single)
                    .tbarRadiusInner.Value = CType(value, Single) * 100
                    .TheShape.Shape = Instance.Shape
                    .TheShape.FillType = Instance.FillType
                    .TheShape.FillTypeLinear = Instance.FillTypeLinear
                    .TheShape.ColorFillSolid = Instance.ColorFillSolid
                    .TheShape.BorderWidth = Instance.BorderWidth
                    .TheShape.BorderColor = Instance.BorderColor
                    .TheShape.BorderStyle = Instance.BorderStyle
                    .TheShape.FocalPoints = New cFocalPoints( _
                            Instance.FocalPoints.CenterPoint, _
                            Instance.FocalPoints.FocusScales)
                    .TheShape.ColorFillBlend = Instance.ColorFillBlend
                    .TheShape.Corners = New CornersProperty( _
                                    Instance.Corners.LowerLeft * ratio, _
                                    Instance.Corners.LowerRight * ratio, _
                                    Instance.Corners.UpperLeft * ratio, _
                                    Instance.Corners.UpperRight * ratio)
                End With

                ' Display the UI editor
                editorService.DropDownControl(dropDownEditor)
                ' Return the new property value from the editor
                Return dropDownEditor.TheShape.RadiusInner
            End If
        End If
        Return MyBase.EditValue(context, provider, value)
    End Function

End Class
#End Region 'RadiusInnerTypeEditor - UITypeEditor

#Region "BorderStyleEditor"

Public Class BorderStyleEditor
    Inherits UITypeEditor

    ' Indicate that we display a dropdown.
    Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    ' Edit a line style
    Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        ' Get an IWindowsFormsEditorService object.
        Dim editor_service As IWindowsFormsEditorService = _
            CType(provider.GetService(GetType(IWindowsFormsEditorService)), _
            IWindowsFormsEditorService)
        If editor_service Is Nothing Then
            Return MyBase.EditValue(context, provider, value)
        End If

        ' Convert the value into a BorderStyles value.
        Dim line_style As DashStyle = DirectCast(value, DashStyle)

        ' Make the editing control.
        Dim editor_control As New LineStyleListBox(line_style, editor_service)
        ' Display the editing control.
        editor_service.DropDownControl(editor_control)

        ' Save the new results.
        Return CType(editor_control.SelectedIndex, DashStyle)
    End Function

    ' Indicate that we draw values in the Properties window.
    Public Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    ' Draw a BorderStyles value.
    Public Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        ' Erase the area.
        e.Graphics.FillRectangle(Brushes.White, e.Bounds)

        ' Draw the sample.
        DrawSamplePen(e.Graphics, e.Bounds, Color.Black, DirectCast(e.Value, DashStyle))
    End Sub
End Class

#Region "LineStyleListBox Custom Control"

<ToolboxItem(False)> _
Public Class LineStyleListBox
    Inherits ListBox

    ' The editor service displaying this control.
    Private m_EditorService As IWindowsFormsEditorService

    Public Sub New(ByVal line_style As DashStyle, _
     ByVal editor_service As IWindowsFormsEditorService)
        MyBase.New()

        m_EditorService = editor_service

        ' Make items for each LineStyles value.
        For i As Integer = 0 To 4
            Me.Items.Add(i)
        Next i

        ' Select the current line style.
        Me.SelectedIndex = DirectCast(line_style, Integer)

        Me.DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ItemHeight = 18
    End Sub

    ' When the user selects an item, close the dropdown.
    Private Sub LineStyleListBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        If m_EditorService IsNot Nothing Then
            m_EditorService.CloseDropDown()
        End If
    End Sub

    ' Draw a menu item.
    Private Sub LineStyleListBox_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
        ' Erase the area.
        e.DrawBackground()

        ' Draw the sample.
        DrawSamplePen(e.Graphics, e.Bounds, Color.Black, DirectCast(e.Index, DashStyle))
    End Sub
End Class

#End Region 'LineStyleListBox Custom Control

Friend Module LineStyleEditorStuff
    ' Draw a sample line.
    Public Sub DrawSamplePen(ByVal gr As Graphics, ByVal sample_bounds As Rectangle, ByVal line_color As Color, ByVal line_style As DashStyle)
        Dim y As Integer = sample_bounds.Y + sample_bounds.Height \ 2
        Using line_pen As Pen = New Pen(Color.Black, 2)
            line_pen.DashStyle = line_style
            gr.DrawLine(line_pen, _
                sample_bounds.Left + 1, y, _
                sample_bounds.Right - 1, y)
        End Using
    End Sub
End Module

#End Region 'BorderStyleEditor - UITypeEditor

#Region "HatchStyleEditor"

Public Class HatchStyleEditor
    Inherits UITypeEditor

    ' Indicate that we display a dropdown.
    Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    ' Edit a line style
    Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        ' Get an IWindowsFormsEditorService object.
        Dim editor_service As IWindowsFormsEditorService = _
            CType(provider.GetService(GetType(IWindowsFormsEditorService)), _
            IWindowsFormsEditorService)
        If editor_service Is Nothing Then
            Return MyBase.EditValue(context, provider, value)
        End If

        ' Pass the UI editor the current property value
        Dim Instance As New Shape
        If context.Instance.GetType Is GetType(Shape) Then
            Instance = CType(context.Instance, Shape)
        Else
            Instance = CType(context.Instance, ShapeActionList).CurrShape
        End If

        ' Convert the value into a BorderStyles value.
        Dim hatch_style As HatchStyle = DirectCast(value, HatchStyle)

        ' Make the editing control.
        Dim editor_control As New HatchStyleListBox(hatch_style.ToString, _
            Instance.ColorFillSolid, Instance.ColorFillSolidB, editor_service)
        ' Display the editing control.
        editor_service.DropDownControl(editor_control)

        ' Save the new results.
        Return CType(System.Enum.Parse(GetType(HatchStyle), editor_control.Text, True), HatchStyle)
    End Function

    Public Overrides ReadOnly Property IsDropDownResizable() As Boolean
        Get
            Return MyBase.IsDropDownResizable
        End Get
    End Property

    Private SmartContext As ITypeDescriptorContext 'SmartTag Workaround
    Public Overrides Function GetPaintValueSupported(ByVal context As ITypeDescriptorContext) As Boolean
        SmartContext = context 'store reference for use in PaintValue
        Return True
    End Function

    Public Overrides Sub PaintValue(ByVal e As PaintValueEventArgs)
        Dim hatch As HatchStyle = CType(e.Value, HatchStyle)
        ' Pass the UI editor the current property value
        Dim Instance As New Shape
        'e.context only works properly in the Propertygrid.
        'When comming from the SmartTag e.context becomes null and 
        'will cause a fatal crash of the IDE.
        'So to get around the null value error I captured a reference to the context
        'in the SmartContext variable in the GetPaintValueSupported function 
        If e.Context IsNot Nothing Then
            Instance = CType(e.Context.Instance, Shape)
        Else
            Instance = CType(SmartContext.Instance, ShapeActionList).CurrShape
        End If

        Using br As Brush = New HatchBrush(hatch, Instance.ColorFillSolid, Instance.ColorFillSolidB)
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using

    End Sub
End Class

#Region "HatchStyleListBox Custom Control"

<ToolboxItem(False)> _
Public Class HatchStyleListBox
    Inherits ListBox

    ' The editor service displaying this control.
    Private m_EditorService As IWindowsFormsEditorService

    Public Sub New(ByVal hatch_style As String, _
      ByVal ColorFore As Color, _
      ByVal ColorBack As Color, _
      ByVal editor_service As IWindowsFormsEditorService)
        MyBase.New()

        m_EditorService = editor_service
        ' Make items for each LineStyles value.
        Me.Items.Clear()
        Dim hatchNames As String() = System.Enum.GetNames(GetType(HatchStyle))
        Array.Sort(hatchNames)
        For Each hs As String In hatchNames
            Me.Items.Add(hs)
        Next
        Me.SelectedIndex = Me.FindStringExact(hatch_style)
        Me.ColorFore = ColorFore
        Me.ColorBack = ColorBack
        Me.DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ItemHeight = 21
        Me.Height = 200
        Me.Width = 200
    End Sub

    Private _ColorFore As Color
    Public Property ColorFore() As Color
        Get
            Return _ColorFore
        End Get
        Set(ByVal value As Color)
            _ColorFore = value
        End Set
    End Property

    Private _ColorBack As Color
    Public Property ColorBack() As Color
        Get
            Return _ColorBack
        End Get
        Set(ByVal value As Color)
            _ColorBack = value
        End Set
    End Property

    ' When the user selects an item, close the dropdown.
    Private Sub HatchStyleListBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        If m_EditorService IsNot Nothing Then
            m_EditorService.CloseDropDown()
        End If
    End Sub

    ' Draw a menu item.
    Private Sub HatchStyleListBox_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
        e.DrawBackground()
        If e.Index <> -1 And Me.Items.Count > 0 Then
            Dim g As Graphics = e.Graphics
            Dim sample As Rectangle = e.Bounds
            Dim sampletext As Rectangle = e.Bounds

            sample.Width = 40
            sample.Inflate(0, -3)
            sampletext.Width = sampletext.Width - sample.Width - 2
            sampletext.X = sample.Right + 2

            Dim displayText As String = Me.Items(e.Index).ToString()
            Dim hs As HatchStyle = CType(System.Enum.Parse(GetType(HatchStyle), displayText, True), HatchStyle)
            Dim hb As HatchBrush = New HatchBrush(hs, ColorFore, ColorBack)

            Dim sf As StringFormat = New StringFormat()
            sf.Alignment = StringAlignment.Near
            sf.LineAlignment = StringAlignment.Center
            sf.FormatFlags = StringFormatFlags.NoWrap
            If (e.State And DrawItemState.Focus) = 0 Then
                g.FillRectangle(New SolidBrush(SystemColors.Window), sampletext)
                g.DrawString(displayText, Me.Font, New SolidBrush(SystemColors.WindowText), sampletext, sf)
            Else
                g.FillRectangle(New SolidBrush(SystemColors.Highlight), sampletext)
                g.DrawString(displayText, Me.Font, New SolidBrush(SystemColors.HighlightText), sampletext, sf)
            End If
            g.FillRectangle(hb, sample)
            g.DrawRectangle(New Pen(Color.Black, 1), sample)
        End If
        e.DrawFocusRectangle()

    End Sub
End Class

#End Region 'HatchStyleListBox Custom Control

#End Region 'HatchStyleEditor

#Region "HatchStyleEditorEasy"

Public Class HatchStyleEditorEasy
    Inherits UITypeEditor

    Public Overrides Function GetPaintValueSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Sub PaintValue(ByVal e As PaintValueEventArgs)
        Dim hatch As HatchStyle = CType(e.Value, HatchStyle)
        Using br As Brush = New HatchBrush(hatch, SystemColors.WindowText, SystemColors.Window)
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using
    End Sub
End Class

#End Region 'HatchStyleEditorEasy

#Region "BlendTypeEditor - UITypeEditor"

#Region "cBlendItems"

Public Class cBlendItems

    Sub New()

    End Sub

    Sub New(ByVal Color As Color(), ByVal Pt As Single())
        iColor = Color
        iPoint = Pt
    End Sub

    Private _iColor As Color()
    <Description("The Color for the Point"), _
       Category("Appearance")> _
    Public Property iColor() As Color()
        Get
            Return _iColor
        End Get
        Set(ByVal value As Color())
            _iColor = value
        End Set
    End Property

    Private _iPoint As Single()
    <Description("The Color for the Point"), _
       Category("Appearance")> _
    Public Property iPoint() As Single()
        Get
            Return _iPoint
        End Get
        Set(ByVal value As Single())
            _iPoint = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return "BlendItems"
    End Function

End Class

#End Region 'cBlendItems

Public Class BlendTypeEditor
    Inherits UITypeEditor

    Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If Not context Is Nothing Then
            Return UITypeEditorEditStyle.DropDown
        End If
        Return (MyBase.GetEditStyle(context))
    End Function

    Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (Not context Is Nothing) And (Not provider Is Nothing) Then
            ' Access the property browser's UI display service, IWindowsFormsEditorService
            Dim editorService As IWindowsFormsEditorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If Not editorService Is Nothing Then
                ' Create an instance of the UI editor, passing a reference to the editor service
                Dim dropDownEditor As DropdownColorBlender = New DropdownColorBlender(editorService)

                ' Pass the UI editor the current property value
                Dim Instance As Shape
                If context.Instance.GetType Is GetType(Shape) Then
                    Instance = CType(context.Instance, Shape)
                Else
                    'This is needed if using in a SmartTag
                    Instance = CType(context.Instance.CurrShape, Shape)
                End If

                With dropDownEditor
                    Dim ratio As Single
                    If Instance.Width > Instance.Height Then
                        .TheShape.Height = .TheShape.Width * (Instance.Height / Instance.Width)
                        .TheShape.Top = (.panShapeHolder.Height - .TheShape.Height) / 2
                        ratio = .TheShape.Height / Instance.Height
                    Else
                        .TheShape.Width = .TheShape.Height * (Instance.Width / Instance.Height)
                        .TheShape.Left = (.panShapeHolder.Width - .TheShape.Width) / 2
                        ratio = .TheShape.Width / Instance.Width
                    End If

                    ' Set current Corners values

                    .TheShape.Shape = Instance.Shape
                    .TheShape.FillType = Instance.FillType
                    .TheShape.FillTypeLinear = Instance.FillTypeLinear
                    .TheShape.ColorFillSolid = Instance.ColorFillSolid
                    .TheShape.BorderWidth = Instance.BorderWidth
                    .TheShape.BorderColor = Instance.BorderColor
                    .TheShape.BorderStyle = Instance.BorderStyle
                    .TheShape.RadiusInner = Instance.RadiusInner
                    .TheShape.FocalPoints = New cFocalPoints( _
                            Instance.FocalPoints.CenterPoint, _
                            Instance.FocalPoints.FocusScales)
                    .TheShape.ColorFillBlend = Instance.ColorFillBlend
                    .TheShape.Corners = New CornersProperty( _
                                    Instance.Corners.LowerLeft * ratio, _
                                    Instance.Corners.LowerRight * ratio, _
                                    Instance.Corners.UpperLeft * ratio, _
                                    Instance.Corners.UpperRight * ratio)
                    .LoadABlend(Instance.ColorFillBlend)
                End With
                ' Display the UI editor
                editorService.DropDownControl(dropDownEditor)
                ' Return the new property value from the editor
                Return dropDownEditor.TheShape.ColorFillBlend
            End If
        End If
        Return MyBase.EditValue(context, provider, value)
    End Function

    ' Indicate that we draw values in the Properties window.
    Public Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    ' Draw a BorderStyles value.
    Public Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        ' Erase the area.
        e.Graphics.FillRectangle(Brushes.White, e.Bounds)

        ' Draw the sample.
        Dim cblnd As cBlendItems = DirectCast(e.Value, cBlendItems)
        Using br As LinearGradientBrush = New LinearGradientBrush(e.Bounds, Color.Black, Color.Black, LinearGradientMode.Horizontal)
            Dim cb As New ColorBlend
            cb.Colors = cblnd.iColor
            cb.Positions = cblnd.iPoint
            br.InterpolationColors = cb
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using
    End Sub
End Class
#End Region 'BlendTypeEditor - UITypeEditor

#End Region 'Dropdown Editors

#Region "Modal Form Editors"

#Region "FocalTypeEditor"

Public Class FocalTypeEditor
    Inherits UITypeEditor

    ' Indicate that we display a modal dialog.
    Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.Modal
    End Function

    ' Edit a Selected value.
    Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
        ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        ' Get the editor service.
        Dim editor_service As IWindowsFormsEditorService = _
            CType(provider.GetService(GetType(IWindowsFormsEditorService)), _
                IWindowsFormsEditorService)
        If editor_service Is Nothing Then Return value

        Dim Instance As Shape
        If context.Instance.GetType Is GetType(Shape) Then
            Instance = CType(context.Instance, Shape)
        Else
            'This is needed if using in a SmartTag
            Instance = CType(context.Instance.CurrShape, Shape)
        End If

        Dim dlg As dlgFocalPoints = New dlgFocalPoints

        ' Prepare the editing dialog.
        With dlg
            Dim ratio As Single
            If Instance.Width > Instance.Height Then
                .TheShape.Height = .TheShape.Width * (Instance.Height / Instance.Width)
                .TheShape.Top = (.panShapeHolder.Height - .TheShape.Height) / 2
                ratio = .TheShape.Height / Instance.Height
            Else
                .TheShape.Width = .TheShape.Height * (Instance.Width / Instance.Height)
                .TheShape.Left = (.panShapeHolder.Width - .TheShape.Width) / 2
                ratio = .TheShape.Width / Instance.Width
            End If

            .TheShape.Shape = Instance.Shape
            .TheShape.FillType = Instance.FillType
            .TheShape.FillTypeLinear = Instance.FillTypeLinear
            .TheShape.ColorFillSolid = Instance.ColorFillSolid
            .TheShape.BorderWidth = Instance.BorderWidth
            .TheShape.BorderColor = Instance.BorderColor
            .TheShape.BorderStyle = Instance.BorderStyle
            .TheShape.RadiusInner = Instance.RadiusInner
            .TheShape.ColorFillBlend = Instance.ColorFillBlend
            .TheShape.Corners = New CornersProperty( _
                            Instance.Corners.LowerLeft * ratio, _
                            Instance.Corners.LowerRight * ratio, _
                            Instance.Corners.UpperLeft * ratio, _
                            Instance.Corners.UpperRight * ratio)
            .TheShape.FocalPoints = New cFocalPoints( _
                            Instance.FocalPoints.CenterPoint, _
                            Instance.FocalPoints.FocusScales)
        End With

        ' Display the dialog.
        editor_service.ShowDialog(dlg)
        Instance.Refresh()
        ' Return the new value.
        Return dlg.TheShape.FocalPoints
    End Function
End Class

#End Region 'FocalTypeEditor

#End Region 'Modal Form Editors

#Region "Expandable Focal Points Property Classes"

#Region "cFocalPoints"

<TypeConverter(GetType(FocalPointsConverter))> _
Public Class cFocalPoints

    Private _CenterPtX As Single = 0.5
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0.5)> _
        Public Property CenterPtX() As Single
        Get
            Return _CenterPtX
        End Get
        Set(ByVal value As Single)
            If value < 0 Then value = 0
            If value > 1 Then value = 1
            _CenterPtX = value
        End Set
    End Property

    Private _CenterPtY As Single = 0.5
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0.5)> _
    Public Property CenterPtY() As Single
        Get
            Return _CenterPtY
        End Get
        Set(ByVal value As Single)
            If value < 0 Then value = 0
            If value > 1 Then value = 1
            _CenterPtY = value
        End Set
    End Property

    Private _FocusPtX As Single = 0
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property FocusPtX() As Single
        Get
            Return _FocusPtX
        End Get
        Set(ByVal value As Single)
            If value < 0 Then value = 0
            If value > 1 Then value = 1
            _FocusPtX = value
        End Set
    End Property

    Private _FocusPtY As Single = 0
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property FocusPtY() As Single
        Get
            Return _FocusPtY
        End Get
        Set(ByVal value As Single)
            If value < 0 Then value = 0
            If value > 1 Then value = 1
            _FocusPtY = value
        End Set
    End Property

    Public Function CenterPoint() As PointF
        Return New PointF(Me.CenterPtX, Me.CenterPtY)
    End Function

    Public Function FocusScales() As PointF
        Return New PointF(Me.FocusPtX, Me.FocusPtY)
    End Function

    Sub New()
        Me.CenterPtX = 0.5
        Me.CenterPtY = 0.5
        Me.FocusPtX = 0
        Me.FocusPtY = 0
    End Sub

    Sub New(ByVal Cx As Single, ByVal Cy As Single, ByVal Fx As Single, ByVal Fy As Single)
        Me.CenterPtX = Cx
        Me.CenterPtY = Cy
        Me.FocusPtX = Fx
        Me.FocusPtY = Fy
    End Sub

    Sub New(ByVal ptC As PointF, ByVal ptF As PointF)
        Me.CenterPtX = ptC.X
        Me.CenterPtY = ptC.Y
        Me.FocusPtX = ptF.X
        Me.FocusPtY = ptF.Y
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}, {3}", _CenterPtX, _CenterPtY, _FocusPtX, _FocusPtY)
    End Function

End Class

#End Region 'cFocalPoints

#Region "FocalPointsConverter"

Friend Class FocalPointsConverter : Inherits ExpandableObjectConverter

    Public Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function CreateInstance( _
      ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal propertyValues As System.Collections.IDictionary) As Object

        Dim fPt As New cFocalPoints
        fPt.CenterPtX = CType(propertyValues("CenterPtX"), Single)
        fPt.CenterPtY = CType(propertyValues("CenterPtY"), Single)
        fPt.FocusPtX = CType(propertyValues("FocusPtX"), Single)
        fPt.FocusPtY = CType(propertyValues("FocusPtY"), Single)

        Return fPt
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim s As String = CType(value, String)
                Dim FocalPointsParts(4) As String
                FocalPointsParts = Split(s, ",")
                If Not IsNothing(FocalPointsParts) Then
                    If IsNothing(FocalPointsParts(0)) Then FocalPointsParts(0) = "0.5"
                    If IsNothing(FocalPointsParts(1)) Then FocalPointsParts(1) = "0.5"
                    If IsNothing(FocalPointsParts(2)) Then FocalPointsParts(2) = "0"
                    If IsNothing(FocalPointsParts(3)) Then FocalPointsParts(3) = "0"
                    Return New cFocalPoints(CSng(FocalPointsParts(0).Trim), _
                                            CSng(FocalPointsParts(1).Trim), _
                                            CSng(FocalPointsParts(2).Trim), _
                                            CSng(FocalPointsParts(3).Trim))
                End If
            Catch ex As Exception
                Throw New ArgumentException("Can not convert '" & CStr(value) & "' to type Corners")
            End Try
        Else
            Return New CornersProperty()
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, _
      ByVal value As Object, ByVal destinationType As System.Type) As Object

        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is cFocalPoints) Then
            Dim _FocalPoints As cFocalPoints = CType(value, cFocalPoints)

            ' build the string as "UpperLeft,UpperRight,LowerLeft,LowerRight" 
            Return String.Format("{0}, {1}, {2}, {3}", _FocalPoints.CenterPtX, _FocalPoints.CenterPtY, _FocalPoints.FocusPtX, _FocalPoints.FocusPtY)
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)

    End Function

End Class 'CornerConverter Code

#End Region 'FocalPointsConverter

#End Region 'Expandable Focal Points Property Class

#Region "Expandable Border Corners Property Class"

#Region "CornersProperty"

<TypeConverter(GetType(CornerConverter))> _
Public Class CornersProperty

    Private _All As Short = -1
    Private _UpperLeft As Short = 0
    Private _UpperRight As Short = 0
    Private _LowerLeft As Short = 0
    Private _LowerRight As Short = 0

    Public Sub New(ByVal LowerLeft As Short, ByVal LowerRight As Short, _
      ByVal UpperLeft As Short, ByVal UpperRight As Short)
        Me.LowerLeft = LowerLeft
        Me.LowerRight = LowerRight
        Me.UpperLeft = UpperLeft
        Me.UpperRight = UpperRight
    End Sub

    Public Sub New(ByVal All As Short)
        Me.All = All
    End Sub

    Public Sub New()
        Me.LowerLeft = 0
        Me.LowerRight = 0
        Me.UpperLeft = 0
        Me.UpperRight = 0
    End Sub

    Private Sub CheckForAll(ByVal val As Short)
        If val = LowerLeft AndAlso _
           val = LowerRight AndAlso _
           val = UpperLeft AndAlso _
           val = UpperRight Then
            If _All <> val Then _All = val
        Else
            If All <> -1 Then All = -1
        End If
    End Sub

    <DescriptionAttribute("Set the Radius of the All four Corners the same")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(-1)> _
    Public Property All() As Short
        Get
            Return _All
        End Get
        Set(ByVal Value As Short)
            _All = Value
            If Value > -1 Then
                Me._LowerLeft = Value
                Me._LowerRight = Value
                Me._UpperLeft = Value
                Me._UpperRight = Value
            End If
        End Set

    End Property

    <DescriptionAttribute("Set the Radius of the Upper Left Corner")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property UpperLeft() As Short
        Get
            Return _UpperLeft
        End Get
        Set(ByVal Value As Short)
            _UpperLeft = Value

            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Upper Right Corner")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property UpperRight() As Short
        Get
            Return _UpperRight
        End Get
        Set(ByVal Value As Short)
            _UpperRight = Value
            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Lower Left Corner")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property LowerLeft() As Short
        Get
            Return _LowerLeft
        End Get
        Set(ByVal Value As Short)
            _LowerLeft = Value
            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Lower Right Corner")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property LowerRight() As Short
        Get
            Return _LowerRight
        End Get
        Set(ByVal Value As Short)
            _LowerRight = Value
            CheckForAll(Value)
        End Set
    End Property

End Class 'Corners Properties

#End Region 'CornersProperty

#Region "CornerConverter"

Friend Class CornerConverter : Inherits ExpandableObjectConverter

    Public Overrides Function GetCreateInstanceSupported( _
      ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean

        Return True

    End Function

    Public Overrides Function CreateInstance( _
      ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal propertyValues As System.Collections.IDictionary) As Object

        Dim crn As New CornersProperty
        Dim AL As Short = CType(propertyValues("All"), Short)
        Dim LL As Short = CType(propertyValues("LowerLeft"), Short)
        Dim LR As Short = CType(propertyValues("LowerRight"), Short)
        Dim UL As Short = CType(propertyValues("UpperLeft"), Short)
        Dim UR As Short = CType(propertyValues("UpperRight"), Short)


        Dim oAll As Short = CType(CType(context.Instance, Shape).Corners, _
            CornersProperty).All

        If oAll <> AL And AL > -1 Then
            crn.All = AL
        Else
            crn.LowerLeft = LL
            crn.LowerRight = LR
            crn.UpperLeft = UL
            crn.UpperRight = UR

        End If

        Return crn

    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal sourceType As System.Type) As Boolean

        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim s As String = CType(value, String)
                Dim cornerParts(4) As String
                cornerParts = Split(s, ",")
                If Not IsNothing(cornerParts) Then
                    If IsNothing(cornerParts(0)) Then cornerParts(0) = "0"
                    If IsNothing(cornerParts(1)) Then cornerParts(1) = "0"
                    If IsNothing(cornerParts(2)) Then cornerParts(2) = "0"
                    If IsNothing(cornerParts(3)) Then cornerParts(3) = "0"
                    Return New CornersProperty(CShort(cornerParts(0).Trim), _
                                               CShort(cornerParts(1).Trim), _
                                               CShort(cornerParts(2).Trim), _
                                               CShort(cornerParts(3).Trim))
                End If
            Catch ex As Exception
                Throw New ArgumentException("Can not convert '" & CStr(value) & "' to type Corners")
            End Try
        Else
            Return New CornersProperty()
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, _
      ByVal value As Object, ByVal destinationType As System.Type) As Object

        Dim _Corners As CornersProperty = CType(value, CornersProperty)
        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is CornersProperty) Then
            ' build the string as "UpperLeft, UpperRight, LowerLeft, LowerRight" 
            Return String.Format("{0}, {1}, {2}, {3}", _
                _Corners.LowerLeft, _
                _Corners.LowerRight, _
                _Corners.UpperLeft, _
                _Corners.UpperRight)
        Else
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End If

    End Function

End Class 'CornerConverter Code

#End Region 'CornerConverter

#End Region 'Expandable Border Corners Property Class
