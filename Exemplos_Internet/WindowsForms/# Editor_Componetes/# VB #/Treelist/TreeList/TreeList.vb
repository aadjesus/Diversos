Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Collections
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Globalization
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Windows.Forms.Themes
Imports BlueActivity.Controls

<Designer(GetType(TreeListDesigner))> _
Public Class TreeList
    Inherits Control

#Region "VARIABLES"

    Private _AllowPaint As Boolean = True
    Private _NextNodeTop As Integer
    Friend WithEvents VScroll As VScrollBar
    Friend WithEvents HScroll As HScrollBar
    Private WithEvents _Styles As Skybound.VisualStyles.VisualStyleProvider

#End Region

#Region "CONSTRUCTOR"

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.ContainerControl, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.Selectable, True)
        SetStyle(ControlStyles.Opaque, True)
        SetStyle(ControlStyles.UserMouse, True)

        InitializeComponent()

        AddHandler _VScroll.Scroll, AddressOf OnVScroll
        AddHandler _HScroll.Scroll, AddressOf OnHScroll

        _VScroll.Visible = False
        _VScroll.Visible = False

        Controls.Add(_VScroll)
        Controls.Add(_HScroll)

        _Styles.EnableVisualStyles()

    End Sub

#End Region

#Region "PROPERTIES"

    Private WithEvents _Nodes As TreeListNodeCollection
    <Category("Collection"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property Nodes() As TreeListNodeCollection
        Get
            Return _Nodes
        End Get
    End Property

    Private _SelectedBackColor As Color = Color.White
    <Category("Appearance"), DefaultValue(GetType(Color), "White")> _
    Property SelectedBackColor() As Color
        Get
            Return _SelectedBackColor
        End Get
        Set(ByVal Value As Color)
            _SelectedBackColor = Value
            Invalidate()
        End Set
    End Property

    Private _SelectedTextColor As Color = Color.LightSlateGray
    <Category("Appearance"), DefaultValue(GetType(Color), "LightSlateGray")> _
    Property SelectedTextColor() As Color
        Get
            Return _SelectedTextColor
        End Get
        Set(ByVal Value As Color)
            _SelectedTextColor = Value
            Invalidate()
        End Set
    End Property

    Private _SelectedBorderColor As Color = Color.LightSlateGray
    <Category("Appearance"), DefaultValue(GetType(Color), "LightSlateGray")> _
    Property SelectedBorderColor() As Color
        Get
            Return _SelectedBorderColor
        End Get
        Set(ByVal Value As Color)
            _SelectedBorderColor = Value
            Invalidate()
        End Set
    End Property

    Private _BorderColor As Color = SystemColors.ControlDark
    <Category("Appearance"), DefaultValue(GetType(Color), "ControlDark")> _
    Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal Value As Color)
            _BorderColor = Value
            Invalidate()
        End Set
    End Property

    Private _LinesColor As Color = SystemColors.ControlDark
    <Category("Appearance"), DefaultValue(GetType(Color), "ControlDark")> _
    Property LinesColor() As Color
        Get
            Return _LinesColor
        End Get
        Set(ByVal Value As Color)
            _LinesColor = Value
            Invalidate()
        End Set
    End Property

    Private _LinesStyle As Drawing2D.DashStyle = Drawing2D.DashStyle.Dot
    <Category("Appearance"), DefaultValue(GetType(Drawing2D.DashStyle), "Dot")> _
    Property LinesStyle() As Drawing2D.DashStyle
        Get
            Return _LinesStyle
        End Get
        Set(ByVal Value As Drawing2D.DashStyle)
            _LinesStyle = Value
            Invalidate()
        End Set
    End Property

    Private _ShowLines As Boolean = True
    <Category("Behavior"), DefaultValue(True)> _
    Property ShowLines() As Boolean
        Get
            Return _ShowLines
        End Get
        Set(ByVal Value As Boolean)
            _ShowLines = Value
            Invalidate()
        End Set
    End Property

    Private _StylesEnabled As Boolean = True
    <Category("Appearance"), DefaultValue(True)> _
    Property StylesEnabled() As Boolean
        Get
            Return _StylesEnabled
        End Get
        Set(ByVal Value As Boolean)
            _StylesEnabled = Value
            Invalidate()
        End Set
    End Property

    Private _BackColor As Color = SystemColors.Window
    <Category("Appearance"), DefaultValue(GetType(Color), "Window")> _
    Shadows Property BackColor() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal Value As Color)
            _BackColor = Value
            Invalidate()
        End Set
    End Property

    Private _Indent As Integer = 16
    <Category("Layout"), DefaultValue(16)> _
    Property Indent() As Integer
        Get
            Return _Indent
        End Get
        Set(ByVal Value As Integer)
            _Indent = Value
            Invalidate()
        End Set
    End Property

    Private _NodesSpacing As Integer = 2
    <Category("Layout"), DefaultValue(2)> _
    Property NodesSpacing() As Integer
        Get
            Return _NodesSpacing
        End Get
        Set(ByVal Value As Integer)
            _NodesSpacing = Value
            Invalidate()
        End Set
    End Property

    Private _DescriptionFont As Font = New Font("Arial", 8, FontStyle.Italic, GraphicsUnit.Point)
    <Category("Appearance")> _
    Property DescriptionFont() As Font
        Get
            Return _DescriptionFont
        End Get
        Set(ByVal Value As Font)
            If Not Value Is Nothing Then
                _DescriptionFont = Value
            Else
                _DescriptionFont = Control.DefaultFont
            End If
            Invalidate()
        End Set
    End Property

    Private _TextFont As Font = New Font("Arial", 9, FontStyle.Regular, GraphicsUnit.Point)
    <Category("Appearance")> _
    Property TextFont() As Font
        Get
            Return _TextFont
        End Get
        Set(ByVal Value As Font)
            If Not Value Is Nothing Then
                _TextFont = Value
            Else
                _TextFont = Control.DefaultFont
            End If
            Invalidate()
        End Set
    End Property

    Private _IsBusy As Boolean = False
    <Category("Behavior"), DefaultValue(False)> _
    Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Set(ByVal Value As Boolean)
            _IsBusy = Value
            _Value = 0
            Invalidate()
        End Set
    End Property

    Private _Value As Integer = 0
    <Category("Progress"), DefaultValue(0)> _
    Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal Value As Integer)
            _Value = Value
            Invalidate()
        End Set
    End Property

    Private _Maximum As Integer = 100
    <Category("Progress"), DefaultValue(100)> _
    Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal Value As Integer)
            _Maximum = Value
            Invalidate()
        End Set
    End Property

    Private _BusyMessage As String = "Loading..."
    <Category("Behavior"), DefaultValue("Loading...")> _
    Property BusyMessage() As String
        Get
            Return _BusyMessage
        End Get
        Set(ByVal Value As String)
            _BusyMessage = Value
            Invalidate()
        End Set
    End Property

#End Region

#Region "PROPERTIES TO HIDE"

    <Browsable(False)> _
    Shadows Property BackgroundImage() As Image
        Get

        End Get
        Set(ByVal Value As Image)

        End Set
    End Property

    <Browsable(False)> _
    Shadows Property RightToLeft() As Boolean
        Get

        End Get
        Set(ByVal Value As Boolean)

        End Set
    End Property

    <Browsable(False)> _
    Shadows Property Cursor() As Cursor
        Get

        End Get
        Set(ByVal Value As Cursor)

        End Set
    End Property

    <Browsable(False)> _
    Shadows Property Text() As String
        Get

        End Get
        Set(ByVal Value As String)

        End Set
    End Property

    <Browsable(False)> _
    Shadows Property Font() As Font
        Get
            Return TextFont
        End Get
        Set(ByVal Value As Font)
            TextFont = Value
        End Set
    End Property

#End Region

#Region "INTERNAL PROPERTIES"

    Private _TopMargin As Integer = 3
    <Browsable(False)> _
    Property TopMargin() As Integer
        Get
            Return _TopMargin
        End Get
        Set(ByVal Value As Integer)
            _TopMargin = Value
        End Set
    End Property

    Private _LeftMargin As Integer = 3
    <Browsable(False)> _
    Property LeftMargin() As Integer
        Get
            Return _LeftMargin
        End Get
        Set(ByVal Value As Integer)
            _LeftMargin = Value
        End Set
    End Property

    Private _RightMargin As Integer = 3
    <Browsable(False)> _
    Property RightMargin() As Integer
        Get
            Return _RightMargin
        End Get
        Set(ByVal Value As Integer)
            _RightMargin = Value
        End Set
    End Property

    Private _BottomMargin As Integer = 3
    <Browsable(False)> _
    Property BottomMargin() As Integer
        Get
            Return _BottomMargin
        End Get
        Set(ByVal Value As Integer)
            _BottomMargin = Value
        End Set
    End Property

    Private _SpaceBetweenImageAndText As Integer = 2
    <Browsable(False)> _
    Property SpaceBetweenImageAndText() As Integer
        Get
            Return _SpaceBetweenImageAndText
        End Get
        Set(ByVal Value As Integer)
            _SpaceBetweenImageAndText = Value
        End Set
    End Property

    Private _SpaceBetweenTextAndDesc As Integer = 2
    <Browsable(False)> _
    Property SpaceBetweenTextAndDesc() As Integer
        Get
            Return _SpaceBetweenTextAndDesc
        End Get
        Set(ByVal Value As Integer)
            _SpaceBetweenTextAndDesc = Value
        End Set
    End Property

    Private _SelectedNode As TreeListNode = Nothing
    <Browsable(False)> _
    Property SelectedNode() As TreeListNode
        Get
            Return _SelectedNode
        End Get
        Set(ByVal Value As TreeListNode)
            _SelectedNode = Value
        End Set
    End Property

    Private _PointedNode As TreeListNode = Nothing
    <Browsable(False)> _
    Property PointedNode() As TreeListNode
        Get
            Return _PointedNode
        End Get
        Set(ByVal Value As TreeListNode)
            _PointedNode = Value
        End Set
    End Property

#End Region

#Region "EVENTS"

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        PointedNode = Nothing
        SelectedNode = Nothing
        Nodes.HitTest(e)

        If DesignMode Then
            Dim s As ISelectionService
            s = DirectCast(GetService(GetType(ISelectionService)), ISelectionService)
            Dim a As New ArrayList
            a.Add(SelectedNode)
            s.SetSelectedComponents(a)
        End If
    End Sub

    Friend Sub OnSelectionChanged()
        Dim s As ISelectionService = DirectCast(GetService(GetType(ISelectionService)), ISelectionService)
    End Sub

    Public Event NodePointed(ByVal sender As Object)
    Public Event NodeSelected(ByVal sender As Object)
    Public Event BeforeExpand(ByVal sender As Object)
    Public Event AfterExpand(ByVal sender As Object)
    Public Event BeforeCollapse(ByVal sender As Object)
    Public Event AfterCollapse(ByVal sender As Object)

    Private Sub _Nodes_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Nodes.MouseDown
        If e.Button = MouseButtons.Left Then
            SelectedNode = sender
            RaiseEvent NodeSelected(sender)
            Invalidate()
        End If
        PointedNode = sender
        RaiseEvent NodePointed(sender)
    End Sub

    Private Sub _Nodes_BeforeExpand(ByVal sender As Object) Handles _Nodes.BeforeExpand
        RaiseEvent BeforeExpand(sender)
    End Sub

    Private Sub _Nodes_AfterExpand(ByVal sender As Object) Handles _Nodes.AfterExpand
        RaiseEvent AfterExpand(sender)
    End Sub

    Private Sub _Nodes_AfterCollapse(ByVal sender As Object) Handles _Nodes.AfterCollapse
        RaiseEvent AfterCollapse(sender)
    End Sub

    Private Sub _Nodes_BeforeCollapse(ByVal sender As Object) Handles _Nodes.BeforeCollapse
        RaiseEvent BeforeCollapse(sender)
    End Sub

    Private Sub OnHScroll(ByVal sender As Object, ByVal e As Windows.Forms.ScrollEventArgs)
        Invalidate()
    End Sub

    Private Sub OnVScroll(ByVal sender As Object, ByVal e As Windows.Forms.ScrollEventArgs)
        Invalidate()
    End Sub

#End Region

#Region "PROCEDURES"

    Public Sub BeginUpdate()
        _AllowPaint = False
    End Sub

    Public Sub EndUpdate()
        _AllowPaint = True
        Invalidate()
    End Sub

    Private Sub InitializeComponent()
        Me._Styles = New Skybound.VisualStyles.VisualStyleProvider
        Me._VScroll = New VScrollBar
        Me._HScroll = New HScrollBar
        Me._Nodes = New TreeListNodeCollection(Me)
    End Sub

#End Region

#Region "THEMEING"

    Private Function GetThemeData(ByVal Prop As String) As WindowTheme
        Dim _Theme As WindowTheme
        Dim _ThemeInfo As New ThemeInfo
        _Theme = _ThemeInfo(Prop)
        Return _Theme
    End Function

    Private Function GetPartData(ByVal Theme As WindowTheme, ByVal Part As String) As ThemePart
        Dim _Part As ThemePart = Theme.Parts(Part)
        Return _Part
    End Function

    Private Function GetStateData(ByVal Part As ThemePart, ByVal State As String) As ThemePartState
        Dim _State As ThemePartState = Part.States(State)
        Return _State
    End Function

    Private Function GetThemeColor(ByVal Prop As String) As Color
        Dim _Theme As WindowTheme
        Dim _ThemeInfo As New ThemeInfo
        _Theme = _ThemeInfo(Prop)
    End Function

#End Region

#Region "DRAWING"

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If Not _AllowPaint Then Exit Sub
        Dim g As Graphics = e.Graphics
        Dim r As Rectangle = ClientRectangle

        _NextNodeTop = 3

        DrawBackground(g, r)

        Try
            If _IsBusy Then
                DrawProgress(g, r)
            Else
                Dim _InsideRect As New Rectangle(r.X, r.Y, r.Width, r.Height)
                _InsideRect.Inflate(-1, -1)
                g.SetClip(_InsideRect)
                DrawNodes(g, _Nodes)
                DrawScrollBars(g, r)
            End If
        Catch ex As Exception
            g.DrawString(ex.Message, New Font("Arial", 8.0!, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 10, 50)
        End Try

    End Sub

    Private Sub DrawBorder(ByVal g As Graphics, ByVal r As Rectangle)
        g.DrawRectangle(New Pen(BorderColor), New Rectangle(r.X, r.Y, r.Width - 1, r.Height - 1))
    End Sub

    Private Sub DrawProgress(ByVal g As Graphics, ByVal r As Rectangle)
        Dim _Rect As New Rectangle(CInt(r.Width / 4), CInt((r.Height - 15) / 2), CInt(r.Width / 2), 15)
        Dim _Width As Integer
        Dim _RectDone As New Rectangle(_Rect.Left + 2, _Rect.Top + 2, GetProgressWidth(_Rect.Width) - 4, _Rect.Height - 4)
        _HScroll.Visible = False
        _VScroll.Visible = False

        If UxTheme.IsAppThemed AndAlso StylesEnabled Then
            Dim _WindowTheme As WindowTheme = GetThemeData("PROGRESS")
            Dim _ThemePart As ThemePart = GetPartData(_WindowTheme, "BAR")
            _ThemePart.DrawBackground(g, _Rect)
            _ThemePart = GetPartData(_WindowTheme, "CHUNK")
            _ThemePart.DrawBackground(g, _RectDone)
        Else
            g.FillRectangle(New SolidBrush(_BackColor), r)
            DrawBorder(g, _Rect)
            g.FillRectangle(New SolidBrush(_SelectedTextColor), _RectDone)
        End If
        g.DrawString(BusyMessage, TextFont, Brushes.Black, CInt((r.Width - g.MeasureString(BusyMessage, TextFont).Width) / 2), _Rect.Top + 20)
    End Sub

    Private Function GetProgressWidth(ByVal OriginalWidth As Integer) As Integer
        If Maximum = 0 Then
            Return 4
        ElseIf Value = 0 Then
            Return 4
        Else
            Return CInt((OriginalWidth / Maximum) * Value)
        End If
    End Function

    Private Sub DrawBackground(ByVal g As Graphics, ByVal r As Rectangle)
        If UxTheme.IsAppThemed AndAlso StylesEnabled Then
            Dim _WindowTheme As WindowTheme = GetThemeData("TREEVIEW")
            Dim _ThemePart As ThemePart = GetPartData(_WindowTheme, "TREEITEM")
            _ThemePart.DrawBackground(g, r)
        Else
            g.FillRectangle(New SolidBrush(_BackColor), r)
            DrawBorder(g, r)
        End If
    End Sub

    Private Sub DrawScrollBars(ByVal g As Graphics, ByVal r As Rectangle)
        Dim HVis As Boolean = _HScroll.Visible
        Dim VVis As Boolean = _VScroll.Visible

        Dim _TotalHeight As Integer
        Dim _TotalWidth As Integer

        _TotalHeight = GetTotalHeight()
        _TotalWidth = GetMaxWidth(Me.Nodes)

        If Nodes.Count > 0 Then
            _VScroll.SmallChange = CInt(_TotalHeight / (Nodes.Count * 10))
            _VScroll.LargeChange = CInt(_TotalHeight / Nodes.Count)
        End If

        _HScroll.SmallChange = Indent
        _HScroll.LargeChange = _TotalWidth / 2

        If _HScroll.Visible Then
            _TotalHeight += _HScroll.Height
        End If

        If _VScroll.Visible Then
            _TotalWidth += _VScroll.Width
        End If

        If _TotalHeight > r.Height Then
            _VScroll.Maximum = _TotalHeight
            If _HScroll.Visible Then
                _VScroll.Height = Height - 2 - _HScroll.Height
            Else
                _VScroll.Height = Height - 2
            End If
            _VScroll.Top = 1
            _VScroll.Left = Width - 1 - _VScroll.Width
            _VScroll.Show()
        Else
            _VScroll.Hide()
            _VScroll.Value = 0
        End If

        If _TotalWidth > r.Width Then
            _HScroll.Maximum = _TotalWidth
            _HScroll.Left = 1
            If _VScroll.Visible Then
                _HScroll.Width = Width - 2 - _VScroll.Width
            Else
                _HScroll.Width = Width - 2
            End If
            _HScroll.Top = Height - 1 - _HScroll.Height
            _HScroll.Show()
        Else
            _HScroll.Hide()
            _HScroll.Value = 0
        End If

        If (HVis <> _HScroll.Visible) OrElse (VVis <> _VScroll.Visible) Then
            Invalidate()
        End If
    End Sub

    Private Sub DrawNodes(ByVal g As Graphics, ByVal nc As TreeListNodeCollection)
        Try
            Dim _Node As TreeListNode
            Dim _SelectedBrush As New SolidBrush(SelectedBackColor)
            Dim _NormalBrush As New SolidBrush(ForeColor)
            Dim _SelectedFontBrush As New SolidBrush(SelectedTextColor)
            Dim _TextHeight As Integer = CInt(g.MeasureString("A", TextFont).Height)
            Dim _NodeInside As Rectangle

            For i As Integer = 0 To nc.Count - 1
                _Node = nc(i)
                If _Node.Visible Then
                    _Node.Top = _NextNodeTop - _VScroll.Value
                    _Node.Left = _HScroll.Value
                    'If a node is selected, draw its selection rectangle
                    If _Node Is SelectedNode Then
                        If UxTheme.IsAppThemed AndAlso StylesEnabled Then
                            Dim _WindowTheme As WindowTheme = GetThemeData("TREEVIEW")
                            Dim _ThemePart As ThemePart = GetPartData(_WindowTheme, "TREEITEM")
                            Dim _ThemeState As ThemePartState = GetStateData(_ThemePart, "SELECTED")
                            _ThemeState.DrawBackground(g, New Rectangle(_Node.Bounds.X, _Node.Bounds.Y, _Node.Bounds.Width + 1, _Node.Bounds.Height + 1))
                            '_SelectedFontBrush = New SolidBrush(_ThemePart.TextColor)
                        Else
                            g.FillRectangle(_SelectedBrush, _Node.Bounds)
                            g.DrawRectangle(New Pen(SelectedBorderColor), _Node.Bounds)
                        End If
                    End If

                    _NodeInside = New Rectangle(_Node.Bounds.X, _Node.Bounds.Y, _Node.Bounds.Width, _Node.Bounds.Height)
                    _NodeInside.Inflate(-1, -1)
                    'g.FillRectangle(Brushes.YellowGreen, _NodeInside)

                    'If there is an image, draws it
                    If Not _Node.Image Is Nothing Then
                        g.DrawImage(_Node.Image, New Rectangle(_Node.Bounds.Left + LeftMargin, _Node.Bounds.Top + TopMargin, _Node.Image.Width, _Node.Image.Height))
                    End If

                    'Draws the text of the node
                    If _Node Is SelectedNode Then
                        If _Node.Image Is Nothing Then
                            g.DrawString(_Node.Text, TextFont, _SelectedFontBrush, _Node.Bounds.Left + LeftMargin, _Node.Bounds.Top + TopMargin)
                            If Not _Node.Description Is Nothing Then
                                g.DrawString(_Node.Description, DescriptionFont, _SelectedFontBrush, _Node.Bounds.Left + LeftMargin, _Node.Bounds.Top + TopMargin + SpaceBetweenTextAndDesc + _TextHeight)
                            End If
                        Else
                            g.DrawString(_Node.Text, TextFont, _SelectedFontBrush, _Node.Bounds.Left + LeftMargin + _Node.Image.Width + SpaceBetweenImageAndText, _Node.Bounds.Top + TopMargin)
                            If Not _Node.Description Is Nothing Then
                                g.DrawString(_Node.Description, DescriptionFont, _SelectedFontBrush, _Node.Bounds.Left + LeftMargin + _Node.Image.Width + SpaceBetweenImageAndText, _Node.Bounds.Top + TopMargin + SpaceBetweenTextAndDesc + _TextHeight)
                            End If
                        End If

                    Else
                        If _Node.Image Is Nothing Then
                            g.DrawString(_Node.Text, TextFont, _NormalBrush, _Node.Bounds.Left + LeftMargin, _Node.Bounds.Top + TopMargin)
                            If Not _Node.Description Is Nothing Then
                                g.DrawString(_Node.Description, DescriptionFont, _NormalBrush, _Node.Bounds.Left + LeftMargin, _Node.Bounds.Top + TopMargin + SpaceBetweenTextAndDesc + _TextHeight)
                            End If
                        Else
                            g.DrawString(_Node.Text, TextFont, _NormalBrush, _Node.Bounds.Left + _Node.Image.Width + LeftMargin + SpaceBetweenImageAndText, _Node.Bounds.Top + TopMargin)
                            If Not _Node.Description Is Nothing Then
                                g.DrawString(_Node.Description, DescriptionFont, _NormalBrush, _Node.Bounds.Left + LeftMargin + _Node.Image.Width + SpaceBetweenImageAndText, _Node.Bounds.Top + TopMargin + SpaceBetweenTextAndDesc + _TextHeight)
                            End If
                        End If
                    End If

                    If ShowLines Then DrawLines(_Node, g)

                    _NextNodeTop = _NextNodeTop + _Node.Bounds.Height + NodesSpacing

                    If _Node.BoxShown Then
                        If _Node.State = TreeListNodeState.Collapsed Then
                            DrawPlus(_Node, g)
                        Else
                            DrawMinus(_Node, g)
                            DrawNodes(g, _Node.Nodes)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Debug.Write(ex.Message & vbCrLf & ex.Source)
        End Try

    End Sub

    Private Sub DrawPlus(ByVal n As TreeListNode, ByVal g As Graphics)
        If UxTheme.IsAppThemed AndAlso StylesEnabled Then
            Dim _WindowTheme As WindowTheme = GetThemeData("TREEVIEW")
            Dim _ThemePart As ThemePart = GetPartData(_WindowTheme, "GLYPH")
            Dim _ThemeState As ThemePartState = GetStateData(_ThemePart, "CLOSED")
            _ThemeState.DrawBackground(g, New Rectangle(n.BoxBounds.X, n.BoxBounds.Y, n.BoxBounds.Width + 2, n.BoxBounds.Height + 2))
        Else
            g.FillRectangle(Brushes.White, n.BoxBounds)
            g.DrawRectangle(SystemPens.ControlDark, n.BoxBounds)
            g.DrawLine(SystemPens.WindowText, n.BoxBounds.Left + 4, n.BoxBounds.Top + 2, n.BoxBounds.Left + 4, n.BoxBounds.Top + 6)
            g.DrawLine(SystemPens.WindowText, n.BoxBounds.Left + 2, n.BoxBounds.Top + 4, n.BoxBounds.Left + 6, n.BoxBounds.Top + 4)
        End If
    End Sub

    Private Sub DrawMinus(ByVal n As TreeListNode, ByVal g As Graphics)
        If UxTheme.IsAppThemed AndAlso StylesEnabled Then
            Dim _WindowTheme As WindowTheme = GetThemeData("TREEVIEW")
            Dim _ThemePart As ThemePart = GetPartData(_WindowTheme, "GLYPH")
            Dim _ThemeState As ThemePartState = GetStateData(_ThemePart, "OPENED")
            _ThemeState.DrawBackground(g, New Rectangle(n.BoxBounds.X, n.BoxBounds.Y, n.BoxBounds.Width + 2, n.BoxBounds.Height + 2))
        Else
            g.FillRectangle(Brushes.White, n.BoxBounds)
            g.DrawRectangle(SystemPens.ControlDark, n.BoxBounds)
            g.DrawLine(SystemPens.WindowText, n.BoxBounds.Left + 2, n.BoxBounds.Top + 4, n.BoxBounds.Left + 6, n.BoxBounds.Top + 4)
        End If
    End Sub

    Private Sub DrawLines(ByVal n As TreeListNode, ByVal g As Graphics)
        Dim _Pen As New Pen(LinesColor, 1)
        _Pen.DashStyle = LinesStyle
        Dim _HalfIndent As Integer = CInt(Indent / 2)


        If n.BoxShown Then
            g.DrawLine(_Pen, n.BoxBounds.Right, n.BoxBounds.Top + 4, n.BoxBounds.Right + 4, n.BoxBounds.Top + 4)
        Else
            g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Top + 4, n.BoxBounds.Right + 4, n.BoxBounds.Top + 4)
        End If

        Select Case n.Parent.IndexOf(n)
            Case 0
                If n.State = TreeListNodeState.Collapsed Then
                    If n.HasChildren Then
                        If n.Parent.Count > 1 Then
                            g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                        Else
                            g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom - 4, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                        End If
                    Else
                        If n.Parent.Count > 1 Then
                            g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom - 4, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                        Else
                            g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom - 4, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                        End If

                    End If
                Else
                    If n.HasChildren Then
                        If n.Parent.Count > 1 Then
                            g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                            g.DrawLine(_Pen, n.BoxBounds.Right - 4 + _Indent, n.Bounds.Bottom, n.BoxBounds.Right - 4 + _Indent, n.Bounds.Bottom + 2 + 8 + 1)
                        Else
                            g.DrawLine(_Pen, n.BoxBounds.Right - 4 + _Indent, n.Bounds.Bottom, n.BoxBounds.Right - 4 + _Indent, n.Bounds.Bottom + 2 + 8 + 1 + NodesSpacing)
                        End If
                    Else
                        g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4)
                    End If
                End If
            Case n.Parent.Count - 1
                If n.State = TreeListNodeState.Collapsed Then
                    If n.HasChildren Then
                    Else
                    End If
                Else
                    If n.HasChildren Then
                        g.DrawLine(_Pen, n.BoxBounds.Right - 4 + _Indent, n.Bounds.Bottom, n.BoxBounds.Right - 4 + _Indent, n.Bounds.Bottom + 2 + 8 + 1)
                    Else
                        g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom - 4, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                    End If
                End If
            Case Else
                If n.State = TreeListNodeState.Collapsed Then
                    If n.HasChildren Then
                        g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom - 4, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                    Else
                        g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom - 4, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                    End If
                Else
                    If n.HasChildren Then
                        g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom - 4, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                        g.DrawLine(_Pen, n.BoxBounds.Right - 4 + _Indent, n.Bounds.Bottom, n.BoxBounds.Right - 4 + _Indent, n.Bounds.Bottom + 2 + 8 + 1)
                    Else
                        g.DrawLine(_Pen, n.BoxBounds.Right - 4, n.BoxBounds.Bottom - 4, n.BoxBounds.Right - 4, n.BoxBounds.Bottom + SumNodesHeight(n) - 4 + NodesSpacing)
                    End If
                End If
        End Select

    End Sub

#End Region

#Region "HELPER FUNCTIONS"

    Private Function SumNodesHeight(ByVal n As TreeListNode) As Integer
        Dim _Height As Integer
        If n.State = TreeListNodeState.Collapsed Then
            _Height = n.Height
        Else
            For Each _TreeListNode As TreeListNode In n.Nodes
                _Height = _Height + SumNodesHeight(_TreeListNode) + NodesSpacing
            Next
            _Height = _Height + n.Height
        End If
        Return _Height
    End Function

    Private Function GetMaxDepth(ByVal c As TreeListNodeCollection, Optional ByVal max As Integer = 0) As Integer
        For Each _n As TreeListNode In c
            If _n.Nodes.Count > 0 Then
                Dim _Max As Integer = GetMaxDepth(_n.Nodes, _n.Depth)
                If _Max > max Then Return _Max
            End If
        Next
    End Function

    Private Function GetTotalHeight() As Integer
        Dim _Height As Integer
        For Each _TreeListNode As TreeListNode In Me.Nodes
            _Height = _Height + SumNodesHeight(_TreeListNode)
        Next
        Return _Height
    End Function

    Private Function GetMaxWidth(ByVal c As TreeListNodeCollection) As Integer
        Dim _Width As Integer
        For Each _TreeListNode As TreeListNode In c
            If _TreeListNode.Width > _Width Then _Width = _TreeListNode.Width + (_TreeListNode.Depth * Indent)
            If _TreeListNode.HasChildren Then
                If GetMaxWidth(_TreeListNode.Nodes) > _Width Then _Width = GetMaxWidth(_TreeListNode.Nodes)
            End If
        Next
        Return _Width
    End Function

    Protected Function MouseInRect(ByVal e As MouseEventArgs, ByVal r As Rectangle) As Boolean
        If ((e.X >= r.Left AndAlso e.X <= r.Right) AndAlso (e.Y >= r.Top AndAlso e.Y <= r.Bottom)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function ClipText(ByVal text As String, ByVal g As Graphics, ByVal ToWidth As Integer) As String
        Dim _Temp As String
        If g.MeasureString(text, Font).Width <= ToWidth Then Return text
        For i As Integer = (text.Length - 1) To 0 Step -1
            _Temp = text.Substring(0, i) & "..."
            If g.MeasureString(_Temp, Font).Width <= ToWidth Then Return _Temp
        Next
    End Function

    Public Function GetByKey(ByVal Key As String, ByVal Collection As TreeListNodeCollection) As TreeListNode
        Dim _NodeFound As TreeListNode
        For Each _TreeListNode As TreeListNode In Collection
            If _TreeListNode.Key = Key Then
                _NodeFound = _TreeListNode
            Else
                If _TreeListNode.Nodes.Count > 0 Then
                    _NodeFound = GetByKey(Key, _TreeListNode.Nodes)
                End If
            End If
            Return _NodeFound
        Next
    End Function

    Public Function GetByName(ByVal Name As String, ByVal Collection As TreeListNodeCollection) As TreeListNode
        Dim _NodeFound As TreeListNode
        For Each _TreeListNode As TreeListNode In Collection
            If _TreeListNode.Text = Name Then
                _NodeFound = _TreeListNode
            Else
                If _TreeListNode.Nodes.Count > 0 Then
                    _NodeFound = GetByName(Name, _TreeListNode.Nodes)
                End If
            End If
            Return _NodeFound
        Next
    End Function

#End Region

End Class
