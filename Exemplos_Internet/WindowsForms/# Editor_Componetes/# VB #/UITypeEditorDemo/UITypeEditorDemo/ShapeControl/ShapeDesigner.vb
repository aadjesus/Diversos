Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D

#Region "ShapeDesigner"

Public Class ShapeDesigner
    Inherits ControlDesigner

    Private _Shape As Shape = Nothing
    Private ClipRegion As DesignerVerb = Nothing
    Private _Lists As DesignerActionListCollection
    Private TheBox As Boolean = False

    Public Overrides Sub Initialize(ByVal component As IComponent)
        MyBase.Initialize(component)

        ' Get clock control shortcut reference
        _Shape = CType(component, Shape)
    End Sub

    Protected Overrides Function GetHitTest( _
      ByVal point As System.Drawing.Point) As Boolean
        point = _Shape.PointToClient(point)
        _Shape.CenterPtTracker.IsActive = _
            _Shape.CenterPtTracker.TrackerRectangle.Contains(point)
        _Shape.FocusPtTracker.IsActive = _
            _Shape.FocusPtTracker.TrackerRectangle.Contains(point)
        Return _Shape.CenterPtTracker.IsActive Or _Shape.FocusPtTracker.IsActive
    End Function

    Protected Overrides Sub OnMouseEnter()
        MyBase.OnMouseEnter()
        TheBox = True
        _Shape.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave()
        MyBase.OnMouseLeave()
        TheBox = False
        _Shape.Invalidate()
    End Sub

    Protected Overrides Sub OnPaintAdornments _
      (ByVal pe As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaintAdornments(pe)

        If _Shape.FillType = Shape.eFillType.GradientPath And TheBox Then
            Using g As Graphics = pe.Graphics
                Using pn As Pen = New Pen(Color.Gray, 1)
                    pn.DashStyle = DashStyle.Dot
                    g.FillEllipse( _
                        New SolidBrush(Color.FromArgb(100, 255, 255, 255)), _
                        _Shape.CenterPtTracker.TrackerRectangle)
                    g.DrawEllipse(pn, _Shape.CenterPtTracker.TrackerRectangle)

                    g.FillRectangle( _
                        New SolidBrush(Color.FromArgb(100, 255, 255, 255)), _
                        _Shape.FocusPtTracker.TrackerRectangle)
                    Dim rect As RectangleF = _Shape.FocusPtTracker.TrackerRectangle
                    g.DrawRectangle(pn, rect.X, rect.Y, rect.Width, rect.Height)
                End Using
            End Using
        End If
    End Sub

    'Public Overrides ReadOnly Property SelectionRules() _
    '  As System.Windows.Forms.Design.SelectionRules
    '    Get
    '        Return SelectionRules.LeftSizeable _
    '               Or SelectionRules.RightSizeable _
    '               Or Windows.Forms.Design.SelectionRules.Visible _
    '               Or Windows.Forms.Design.SelectionRules.Moveable
    '    End Get
    'End Property

#Region "ActionLists"

    Public Overrides ReadOnly Property ActionLists() As System.ComponentModel.Design.DesignerActionListCollection
        Get
            If _Lists Is Nothing Then
                _Lists = New DesignerActionListCollection
                _Lists.Add(New ShapeActionList(Me.Component))
            End If
            Return _Lists
        End Get
    End Property

#End Region 'ActionLists

#Region "Verbs"

    Public Overrides ReadOnly Property Verbs() As System.ComponentModel.Design.DesignerVerbCollection
        Get
            Dim myVerbs As DesignerVerbCollection = _
                New DesignerVerbCollection
            ClipRegion = New DesignerVerb(GetVerbText, _
                New EventHandler(AddressOf ClipToRegionClicked))
            myVerbs.Add(ClipRegion)
            Return myVerbs
        End Get
    End Property

    Private Function GetVerbText() As String
        Return "Region Clipping " & IIf(_Shape.RegionClip, "ON", "OFF")
    End Function

    Public Sub ClipToRegionClicked(ByVal sender As Object, ByVal e As EventArgs)
        Me.VerbRegionClip = Not Me.VerbRegionClip
    End Sub

    Public Property VerbRegionClip() As Boolean
        Get
            Return _Shape.RegionClip
        End Get
        Set(ByVal value As Boolean)
            Dim prop As PropertyDescriptor = _
                TypeDescriptor.GetProperties(GetType(Shape)) _
                ("RegionClip")
            Me.RaiseComponentChanging(prop)
            _Shape.RegionClip = value
            Me.RaiseComponentChanged(prop, Not (_Shape.RegionClip), _Shape.RegionClip)
            Dim menuService As IMenuCommandService = CType(Me.GetService(GetType(IMenuCommandService)), IMenuCommandService)
            If Not (menuService Is Nothing) Then
                If menuService.Verbs.IndexOf(ClipRegion) >= 0 Then
                    menuService.Verbs.Remove(ClipRegion)
                    ClipRegion = New DesignerVerb( _
                    GetVerbText, _
                    New EventHandler(AddressOf ClipToRegionClicked))
                    menuService.Verbs.Add(ClipRegion)
                End If
            End If
            _Shape.Refresh()
        End Set
    End Property

#End Region 'Verbs

End Class

#End Region 'ShapeDesigner

#Region "ShapeActionList"

Public Class ShapeActionList
    Inherits DesignerActionList

    Private _ShapeSelector As Shape
    Private _DesignerService As DesignerActionUIService = Nothing

    Public Sub New(ByVal component As IComponent)
        MyBase.New(component)

        ' Save a reference to the control we are designing.
        _ShapeSelector = DirectCast(component, Shape)

        ' Save a reference to the DesignerActionUIService
        _DesignerService = _
            CType(GetService(GetType(DesignerActionUIService)), _
            DesignerActionUIService)

        'Makes the Smart Tags open automatically 
        Me.AutoShow = True
    End Sub

#Region "Smart Tag Items"

#Region "Properties"

    <Editor(GetType(ShapeTypeEditor), GetType(UITypeEditor))> _
    Public Property Shape() As Shape.eShape
        Get
            Return _ShapeSelector.Shape
        End Get
        Set(ByVal value As Shape.eShape)
            SetControlProperty("Shape", value)
            _DesignerService.Refresh(_ShapeSelector)
        End Set
    End Property

    <Editor(GetType(BorderStyleEditor), GetType(UITypeEditor))> _
    Public Property BorderStyle() As DashStyle
        Get
            Return _ShapeSelector.BorderStyle
        End Get
        Set(ByVal value As DashStyle)
            SetControlProperty("BorderStyle", value)
        End Set
    End Property

    <Editor(GetType(RadiusInnerTypeEditor), GetType(UITypeEditor))> _
    Public Property RadiusInner() As Single
        Get
            Return _ShapeSelector.RadiusInner
        End Get
        Set(ByVal value As Single)
            SetControlProperty("RadiusInner", value)
        End Set
    End Property

    <Editor(GetType(BlendTypeEditor), GetType(UITypeEditor))> _
    Public Property ColorFillBlend() As cBlendItems
        Get
            Return _ShapeSelector.ColorFillBlend
        End Get
        Set(ByVal value As cBlendItems)
            SetControlProperty("ColorFillBlend", value)
        End Set
    End Property

    <Editor(GetType(FocalTypeEditor), GetType(UITypeEditor))> _
    Public Property FocalPoints() As cFocalPoints
        Get
            Return _ShapeSelector.FocalPoints
        End Get
        Set(ByVal value As cFocalPoints)
            SetControlProperty("FocalPoints", value)
        End Set
    End Property

    Public Property FillType() As Shape.eFillType
        Get
            Return _ShapeSelector.FillType
        End Get
        Set(ByVal value As Shape.eFillType)
            SetControlProperty("FillType", value)
            _DesignerService.HideUI(_ShapeSelector)
            _DesignerService.ShowUI(_ShapeSelector)
        End Set
    End Property

    Public Property FillTypeLinear() As LinearGradientMode
        Get
            Return _ShapeSelector.FillTypeLinear
        End Get
        Set(ByVal value As LinearGradientMode)
            SetControlProperty("FillTypeLinear", value)
        End Set
    End Property

    <Editor(GetType(HatchStyleEditor), GetType(UITypeEditor))> _
    Public Property BrushHatch() As HatchStyle
        Get
            Return _ShapeSelector.BrushHatch
        End Get
        Set(ByVal value As HatchStyle)
            SetControlProperty("BrushHatch", value)
        End Set
    End Property

    Public Property ColorFillSolid() As Color
        Get
            Return _ShapeSelector.ColorFillSolid
        End Get
        Set(ByVal value As Color)
            SetControlProperty("ColorFillSolid", value)
            _DesignerService.Refresh(_ShapeSelector)
        End Set
    End Property

    Public Property ColorFillSolidB() As Color
        Get
            Return _ShapeSelector.ColorFillSolidB
        End Get
        Set(ByVal value As Color)
            SetControlProperty("ColorFillSolidB", value)
            _DesignerService.Refresh(_ShapeSelector)
        End Set
    End Property

    Public Property BorderColor() As Color
        Get
            Return _ShapeSelector.BorderColor
        End Get
        Set(ByVal value As Color)
            SetControlProperty("BorderColor", value)
        End Set
    End Property

    Public Property BorderShow() As Boolean
        Get
            Return _ShapeSelector.BorderShow
        End Get
        Set(ByVal value As Boolean)
            SetControlProperty("BorderShow", value)
            _DesignerService.Refresh(_ShapeSelector)
        End Set
    End Property

    Public Property BorderWidth() As Integer
        Get
            Return _ShapeSelector.BorderWidth
        End Get
        Set(ByVal value As Integer)
            SetControlProperty("BorderWidth", value)
        End Set
    End Property

    Public ReadOnly Property CurrShape() As Shape
        Get
            Return _ShapeSelector
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub ClipToRegion()
        _ShapeSelector.RegionClip = Not _ShapeSelector.RegionClip
        SetControlProperty("RegionClip", _ShapeSelector.RegionClip)
        'forces the control to refresh
        _DesignerService.Refresh(_ShapeSelector)
    End Sub

    Public Sub AdjustCorners()

        'Create a new Corners Dialog and update the controls on the form
        Dim dlg As dlgCorners = New dlgCorners()

        Dim maxcorner As Integer
        Dim ratio As Single
        If _ShapeSelector.Width > _ShapeSelector.Height Then
            dlg.TheShape.Height = dlg.TheShape.Width * (_ShapeSelector.Height / _ShapeSelector.Width)
            dlg.TheShape.Top = (dlg.panShapeHolder.Height - dlg.TheShape.Height) / 2
            maxcorner = ((dlg.TheShape.Height / 2) - (_ShapeSelector.BorderWidth) * 2)
            ratio = dlg.TheShape.Height / _ShapeSelector.Height
        Else
            dlg.TheShape.Width = dlg.TheShape.Height * (_ShapeSelector.Width / _ShapeSelector.Height)
            dlg.TheShape.Left = (dlg.panShapeHolder.Width - dlg.TheShape.Width) / 2
            maxcorner = ((dlg.TheShape.Width / 2) - (_ShapeSelector.BorderWidth) * 2)
            ratio = dlg.TheShape.Width / _ShapeSelector.Width
        End If

        ' Set current Corners values
        dlg.tbarUpperLeft.Maximum = maxcorner
        dlg.tbarUpperRight.Maximum = maxcorner
        dlg.tbarLowerLeft.Maximum = maxcorner
        dlg.tbarLowerRight.Maximum = maxcorner
        dlg.tbarAll.Maximum = maxcorner
        dlg.tbarUpperLeft.TickFrequency = maxcorner / 2
        dlg.tbarUpperRight.TickFrequency = maxcorner / 2
        dlg.tbarLowerLeft.TickFrequency = maxcorner / 2
        dlg.tbarLowerRight.TickFrequency = maxcorner / 2
        dlg.tbarAll.TickFrequency = maxcorner / 2
        If _ShapeSelector.Corners.All > -1 Then
            dlg.tbarAll.Value = Math.Min((_ShapeSelector.Corners.UpperLeft * ratio), maxcorner)
        End If
        dlg.tbarUpperLeft.Value = Math.Min((_ShapeSelector.Corners.UpperLeft * ratio), maxcorner)
        dlg.tbarUpperRight.Value = Math.Min((_ShapeSelector.Corners.UpperRight * ratio), maxcorner)
        dlg.tbarLowerLeft.Value = Math.Min((_ShapeSelector.Corners.LowerLeft * ratio), maxcorner)
        dlg.tbarLowerRight.Value = Math.Min((_ShapeSelector.Corners.LowerRight * ratio), maxcorner)

        dlg.TheShape.Shape = _ShapeSelector.Shape
        dlg.TheShape.FillType = _ShapeSelector.FillType
        dlg.TheShape.FillTypeLinear = _ShapeSelector.FillTypeLinear
        dlg.TheShape.ColorFillSolid = _ShapeSelector.ColorFillSolid
        dlg.TheShape.BorderWidth = _ShapeSelector.BorderWidth
        dlg.TheShape.BorderColor = _ShapeSelector.BorderColor
        dlg.TheShape.BorderStyle = _ShapeSelector.BorderStyle
        dlg.TheShape.RadiusInner = _ShapeSelector.RadiusInner
        dlg.TheShape.ColorFillBlend = _ShapeSelector.ColorFillBlend
        dlg.TheShape.Corners = New CornersProperty( _
                        _ShapeSelector.Corners.LowerLeft * ratio, _
                        _ShapeSelector.Corners.LowerRight * ratio, _
                        _ShapeSelector.Corners.UpperLeft * ratio, _
                        _ShapeSelector.Corners.UpperRight * ratio)
        dlg.TheShape.FocalPoints = _ShapeSelector.FocalPoints


        ' Update new Corners values if OK button was pressed
        If dlg.ShowDialog() = DialogResult.OK Then
            Dim designerHost As IDesignerHost = CType(Me.Component.Site.GetService(GetType(IDesignerHost)), IDesignerHost)

            If designerHost IsNot Nothing Then
                Dim t As DesignerTransaction = designerHost.CreateTransaction()
                Try
                    SetControlProperty("Corners", New CornersProperty( _
                        dlg.TheShape.Corners.LowerLeft / ratio, _
                        dlg.TheShape.Corners.LowerRight / ratio, _
                        dlg.TheShape.Corners.UpperLeft / ratio, _
                        dlg.TheShape.Corners.UpperRight / ratio))
                    t.Commit()
                Catch
                    t.Cancel()
                End Try
            End If
        End If
        _ShapeSelector.Refresh()

    End Sub

#End Region 'Methods

    ' Set a control property. This method makes Undo/Redo
    ' work properly and marks the form as modified in the IDE.
    Private Sub SetControlProperty(ByVal property_name As String, ByVal value As Object)
        TypeDescriptor.GetProperties(_ShapeSelector) _
            (property_name).SetValue(_ShapeSelector, value)
    End Sub

#End Region ' Smart Tag Items

    ' Return the smart tag action items.
    Public Overrides Function GetSortedActionItems() As System.ComponentModel.Design.DesignerActionItemCollection
        Dim items As New DesignerActionItemCollection()

        ' Section headers.
        items.Add(New DesignerActionHeaderItem("Shape Appearance"))

        ' Property entries.
        items.Add( _
            New DesignerActionPropertyItem( _
                "Shape", _
                "Shape", _
                "Shape Appearance", _
                "The Shape of the Control"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "FillType", _
                "Fill Type", _
                "Shape Appearance", _
                "Fill Solid or Gradient"))
        If _ShapeSelector.FillType = UITypeEditorDemo.Shape.eFillType.GradientLinear Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "FillTypeLinear", _
                    "Linear Fill Type", _
                    "Shape Appearance", _
                    "Gradient Fill Type"))
        End If
        If _ShapeSelector.FillType = UITypeEditorDemo.Shape.eFillType.Hatch Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "BrushHatch", _
                    "Hatch Style", _
                    "Shape Appearance", _
                    "Type of Hatch Style"))
        End If
        If _ShapeSelector.FillType = UITypeEditorDemo.Shape.eFillType.GradientPath _
            Or _ShapeSelector.FillType = UITypeEditorDemo.Shape.eFillType.GradientLinear Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "ColorFillBlend", _
                    "Blend Colors", _
                    "Shape Appearance", _
                    "Color and Position Arrays"))
        End If
        If _ShapeSelector.FillType = UITypeEditorDemo.Shape.eFillType.Solid  Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "ColorFillSolid", _
                    "Solid Fill Color", _
                    "Shape Appearance", _
                    "Solid Fill Color"))
        End If
        If _ShapeSelector.FillType = UITypeEditorDemo.Shape.eFillType.Hatch Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "ColorFillSolid", _
                    "First Hatch Color", _
                    "Shape Appearance", _
                    "First Color for the Hatch Fill"))
            items.Add( _
                New DesignerActionPropertyItem( _
                    "ColorFillSolidB", _
                    "Second Hatch Color", _
                    "Shape Appearance", _
                    "Second Color for the Hatch Fill"))
        End If
        If _ShapeSelector.FillType = UITypeEditorDemo.Shape.eFillType.GradientPath Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "FocalPoints", _
                    "FocalPoints", _
                    "Shape Appearance", _
                    "The color of the shape's Border"))
        End If
        items.Add( _
            New DesignerActionPropertyItem( _
                "BorderShow", _
                "Show Border", _
                "Shape Appearance", _
                "The show or not show the border"))
        If _ShapeSelector.BorderShow Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "BorderStyle", _
                    "Border Style", _
                    "Shape Appearance", _
                    "The line style used to draw state border"))
            items.Add( _
                New DesignerActionPropertyItem( _
                    "BorderColor", _
                    "Border Color", _
                    "Shape Appearance", _
                    "The color of the shape's Border"))
            items.Add( _
                New DesignerActionPropertyItem( _
                    "BorderWidth", _
                    "Border Width", _
                    "Shape Appearance", _
                    "The width of the shape's Border"))
        End If

        If _ShapeSelector.Shape = UITypeEditorDemo.Shape.eShape.Rectangle Then
            items.Add(New DesignerActionHeaderItem("Rectangle Only"))
            items.Add( _
                New DesignerActionMethodItem( _
                      Me, _
                    "AdjustCorners", _
                    "Adjust Corners ", _
                    "Rectangle Only", _
                    "Adjust Corners", _
                    True))
        End If

        If _ShapeSelector.Shape = UITypeEditorDemo.Shape.eShape.Star Then
            items.Add(New DesignerActionHeaderItem("Star Only"))
            items.Add( _
                New DesignerActionPropertyItem( _
                    "RadiusInner", _
                    "Inner Radius", _
                    "Star Only", _
                    "The Inner Radius of the Star Shape"))
        End If

        items.Add(New DesignerActionHeaderItem("Region Clipping"))
        items.Add( _
            New DesignerActionMethodItem( _
                 Me, _
                "ClipToRegion", _
                "Region Clipping " & IIf(_ShapeSelector.RegionClip, "ON", "OFF"), _
                "Region Clipping", _
                "Clip To Region", _
                False))
        items.Add(New DesignerActionHeaderItem("Information"))
        'Add Text Item - I gave it an empty Category to make 
        'it appear at the end with no Header
        items.Add( _
            New DesignerActionTextItem( _
                Space(20) & "Smart Tags Are Cool", _
                ""))

        'Another Text item but with a header
        Dim txt As String = "Width=" & _ShapeSelector.Width & _
         " Height=" & _ShapeSelector.Height
        items.Add( _
            New DesignerActionTextItem( _
                txt, "Information"))

        Return items
    End Function

End Class

#End Region 'ShapeActionList
