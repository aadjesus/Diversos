Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Collections
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel.Design.Serialization

<TypeConverter(GetType(TreeListNodeConverter)), _
DesignTimeVisible(False), _
ToolboxItem(False), _
Serializable()> _
Public Class TreeListNode
    Inherits Component

#Region "CONSTRUCTOR"

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        If Key = "" Then
            Key = Guid.NewGuid.ToString
        End If
    End Sub

    Public Sub New(ByVal Key As String)
        MyBase.New()
        InitializeComponent()
        Me.Key = Key
    End Sub

#End Region

#Region "PROPERTIES"

    Private _Key As String
    Property Key() As String
        Get
            Return _Key
        End Get
        Set(ByVal Value As String)
            _Key = Value
        End Set
    End Property

    Private _Type As Integer
    Property Type() As Integer
        Get
            Return _Type
        End Get
        Set(ByVal Value As Integer)
            _Type = Value
        End Set
    End Property

    Private _Text As String
    <Category("Appearance")> _
    Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal Value As String)
            _Text = Value
            TreeList.Invalidate()
        End Set
    End Property

    Private _Description As String
    <Category("Appearance")> _
    Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal Value As String)
            _Description = Value
            TreeList.Invalidate()
        End Set
    End Property

    Private _Image As Image
    <Category("Appearance")> _
    Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal Value As Image)
            _Image = Value
            TreeList.Invalidate()
        End Set
    End Property

    Private _Tag As Object
    <Browsable(False)> _
    Property Tag() As Object
        Get
            Return _Tag
        End Get
        Set(ByVal Value As Object)
            _Tag = Value
        End Set
    End Property

    Private _Visible As Boolean = True
    <Category("Behavior")> _
    Property Visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal Value As Boolean)
            _Visible = Value
            TreeList.Invalidate()
        End Set
    End Property

    Private WithEvents _Nodes As TreeListNodeCollection
    <Category("Collection"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property Nodes() As TreeListNodeCollection
        Get
            Return _Nodes
        End Get
    End Property

#End Region

#Region "INTERNAL PROPERTIES"

    Private _TreeList As TreeList = Nothing
    <Browsable(False)> _
    Public Property TreeList() As TreeList
        Get
            Return _TreeList
        End Get
        Set(ByVal Value As TreeList)
            _TreeList = Value
            _Nodes.TreeList = Value
        End Set
    End Property

    Private _Parent As TreeListNodeCollection = Nothing
    <Browsable(False)> _
    Public Property Parent() As TreeListNodeCollection
        Get
            Return _Parent
        End Get
        Set(ByVal Value As TreeListNodeCollection)
            _Parent = Value
        End Set
    End Property

    Private _Depth As Integer = 0
    <Browsable(False)> _
    Property Depth() As Integer
        Get
            Return _Depth
        End Get
        Set(ByVal Value As Integer)
            _Depth = Value
            _Nodes.Depth = Value
        End Set
    End Property

    Private _State As TreeListNodeState = TreeListNodeState.Collapsed
    <Browsable(False)> _
    ReadOnly Property State() As TreeListNodeState
        Get
            If HasChildren Then
                Return _State
            Else
                Return TreeListNodeState.Collapsed
            End If
        End Get
    End Property

    Private _Top As Integer
    <Browsable(False)> _
    Property Top() As Integer
        Get
            Return _Top
            Debug.WriteLine(_Top)
        End Get
        Set(ByVal Value As Integer)
            _Top = Value
        End Set
    End Property

    Private _Left As Integer
    <Browsable(False)> _
    Property Left() As Integer
        Get
            Return _Left
        End Get
        Set(ByVal Value As Integer)
            _Left = Value
        End Set
    End Property

    Private _Shown As Boolean
    <Browsable(False)> _
    Property Shown() As Boolean
        Get
            Return _Shown
        End Get
        Set(ByVal Value As Boolean)
            _Shown = Value
        End Set
    End Property

    <Browsable(False)> _
    ReadOnly Property Bounds() As Rectangle
        Get
            Return GetBounds()
        End Get
    End Property

    <Browsable(False)> _
    ReadOnly Property BoxBounds() As Rectangle
        Get
            Return New Rectangle(Bounds.Left - 12, Bounds.Top + 4, 8, 8)
        End Get
    End Property

    <Browsable(False)> _
    ReadOnly Property BoxShown() As Boolean
        Get
            Return _Nodes.Count > 0
        End Get
    End Property

    <Browsable(False)> _
    ReadOnly Property Height() As Integer
        Get
            Return GetHeight()
        End Get
    End Property

    <Browsable(False)> _
    ReadOnly Property Width() As Integer
        Get
            Return GetWidth()
        End Get
    End Property

    <Browsable(False)> _
     ReadOnly Property HasChildren() As Boolean
        Get
            Return (Nodes.Count > 0)
        End Get
    End Property

#End Region

#Region "EVENTS"

    Public Event MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
    Protected Friend Sub OnMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        RaiseEvent MouseDown(sender, e)
    End Sub

    Private Sub _Nodes_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Nodes.MouseDown
        RaiseEvent MouseDown(sender, e)
    End Sub

    Public Event BeforeExpand(ByVal sender As Object)
    Public Event AfterExpand(ByVal sender As Object)
    Public Event BeforeCollapse(ByVal sender As Object)
    Public Event AfterCollapse(ByVal sender As Object)

#End Region

#Region "FUNCTIONS"

    Private Function GetHeight() As Integer
        Dim _Graphics As Graphics = TreeList.CreateGraphics
        Dim _Height As Integer
        If Description Is Nothing Then
            _Height = CInt(_Graphics.MeasureString(Text, TreeList.TextFont).Height)
        Else
            _Height = CInt(_Graphics.MeasureString(Text, TreeList.TextFont).Height) + TreeList.SpaceBetweenTextAndDesc + CInt(_Graphics.MeasureString(Description, TreeList.DescriptionFont).Height)
        End If
        If Not Image Is Nothing Then
            If Image.Height > _Height Then _Height = Image.Height
        End If
        _Height = _Height + TreeList.TopMargin + TreeList.BottomMargin
        Return _Height
    End Function

    Private Function GetWidth() As Integer
        Dim _Graphics As Graphics = TreeList.CreateGraphics
        Dim _Width As Integer
        If Description Is Nothing Then
            _Width = CInt(_Graphics.MeasureString(Text, TreeList.TextFont).Width)
        Else
            If CInt(_Graphics.MeasureString(Text, TreeList.TextFont).Width) > CInt(_Graphics.MeasureString(Description, TreeList.DescriptionFont).Width) Then
                _Width = CInt(_Graphics.MeasureString(Text, TreeList.TextFont).Width)
            Else
                _Width = CInt(_Graphics.MeasureString(Description, TreeList.DescriptionFont).Width)
            End If
        End If
        If Not Image Is Nothing Then
            _Width = _Width + Image.Width + TreeList.SpaceBetweenImageAndText
        End If
        _Width = _Width + TreeList.RightMargin + TreeList.LeftMargin
        Return _Width
    End Function

    Private Function GetBounds() As Rectangle
        Dim _X As Integer = (Depth * TreeList.Indent) - Left
        Dim _Y As Integer = Top
        Return New Rectangle(_X, _Y, Width, Height)
    End Function

#End Region

#Region "PROCEDURES"

    Private Sub InitializeComponent()
        Me._Nodes = New TreeListNodeCollection(TreeList)
    End Sub

    Public Sub Expand()
        RaiseEvent BeforeExpand(Me)
        _State = TreeListNodeState.Expanded
        TreeList.Invalidate()
        RaiseEvent AfterExpand(Me)
    End Sub

    Public Sub Collapse()
        RaiseEvent BeforeCollapse(Me)
        _State = TreeListNodeState.Collapsed
        TreeList.Invalidate()
        RaiseEvent AfterCollapse(Me)
    End Sub

    Protected Friend Sub HitTest(ByVal e As MouseEventArgs)
        If (e.X >= Bounds.X) AndAlso (e.X <= Bounds.X + Bounds.Width) AndAlso (e.Y >= Bounds.Y) AndAlso (e.Y <= Bounds.Y + Bounds.Height) Then
            OnMouseDown(Me, e)
        Else
            _Nodes.HitTest(e)
        End If

        If BoxShown Then
            If (e.X >= BoxBounds.X) AndAlso (e.X <= BoxBounds.X + BoxBounds.Width) AndAlso (e.Y >= BoxBounds.Y) AndAlso (e.Y <= BoxBounds.Y + BoxBounds.Height) Then
                If _State = TreeListNodeState.Expanded Then
                    Collapse()
                Else
                    Expand()
                End If
            End If
        End If
    End Sub

#End Region

End Class

Public Enum TreeListNodeState
    Expanded
    Collapsed
End Enum



