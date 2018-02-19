Imports System
Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.CollectionEditor
Imports System.Globalization
Imports System.Collections
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Friend Class TreeListDesigner
    Inherits ControlDesigner

    Private _TreeList As TreeList

    Public Overrides Sub Initialize(ByVal component As System.ComponentModel.IComponent)
        MyBase.Initialize(component)

        'Record instance of control we're designing
        _TreeList = DirectCast(component, TreeList)

        'Hook up events
        Dim s As ISelectionService = DirectCast(GetService(GetType(ISelectionService)), ISelectionService)
        Dim c As IComponentChangeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)
        AddHandler s.SelectionChanged, AddressOf OnSelectionChanged
        AddHandler c.ComponentRemoving, AddressOf OnComponentRemoving
    End Sub

    Private Sub OnSelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        _TreeList.OnSelectionChanged()
    End Sub

    Private Sub OnComponentRemoving(ByVal sender As Object, ByVal e As ComponentEventArgs)
        Dim c As IComponentChangeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)
        Dim n As TreeListNode
        Dim h As IDesignerHost = DirectCast(GetService(GetType(IDesignerHost)), IDesignerHost)
        Dim i As Integer

        'If the user is removing a button
        If TypeOf e.Component Is TreeListNode Then
            n = DirectCast(e.Component, TreeListNode)
            If _TreeList.Nodes.Contains(n) Then
                c.OnComponentChanging(_TreeList, Nothing)
                _TreeList.Nodes.Remove(n)
                c.OnComponentChanged(_TreeList, Nothing, Nothing, Nothing)
                Return
            End If
        End If

        'If the user is removing the control itself
        If e.Component Is _TreeList Then
            For i = _TreeList.Nodes.Count - 1 To 0 Step -1
                n = _TreeList.Nodes(i)
                c.OnComponentChanging(_TreeList, Nothing)
                _TreeList.Nodes.Remove(n)
                h.DestroyComponent(n)
                c.OnComponentChanged(_TreeList, Nothing, Nothing, Nothing)
            Next
        End If
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Dim s As ISelectionService = DirectCast(GetService(GetType(ISelectionService)), ISelectionService)
        Dim c As IComponentChangeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)

        'Unhook events
        RemoveHandler s.SelectionChanged, AddressOf OnSelectionChanged
        RemoveHandler c.ComponentRemoving, AddressOf OnComponentRemoving

        MyBase.Dispose(disposing)
    End Sub

    Public Overrides ReadOnly Property AssociatedComponents() As System.Collections.ICollection
        Get
            Return _TreeList.Nodes
        End Get
    End Property

    Public Overrides ReadOnly Property Verbs() As System.ComponentModel.Design.DesignerVerbCollection
        Get
            Dim v As New DesignerVerbCollection

            'Verb to REDRAW
            'v.Add(New DesignerVerb("Redraw TreeList", AddressOf OnRedraw))

            Return v
        End Get
    End Property

    Private Sub OnRedraw(ByVal sender As Object, ByVal e As EventArgs)
        _TreeList.Invalidate()
    End Sub

    Protected Overrides Function GetHitTest(ByVal point As System.Drawing.Point) As Boolean
        point = _TreeList.PointToClient(point)
        Return Hit(_TreeList.Nodes, point)
    End Function

    Private Function Hit(ByVal Collection As TreeListNodeCollection, ByVal Point As System.drawing.Point) As Boolean
        For Each n As TreeListNode In Collection
            If n.Bounds.Contains(Point) Then Return True
            If n.BoxBounds.Contains(Point) Then Return True
            If n.Nodes.Count > 0 Then
                If Hit(n.Nodes, Point) = True Then Return True
            End If
        Next
        If _TreeList.VScroll.Visible Then
            Dim _VRect As New Rectangle(_TreeList.VScroll.Left, _TreeList.VScroll.Top, _TreeList.VScroll.Width, _TreeList.VScroll.Height)
            If _VRect.Contains(Point) Then Return True
        End If
        If _TreeList.HScroll.Visible Then
            Dim _HRect As New Rectangle(_TreeList.HScroll.Left, _TreeList.HScroll.Top, _TreeList.HScroll.Width, _TreeList.HScroll.Height)
            If _HRect.Contains(Point) Then Return True
        End If

        Return False
    End Function

End Class
