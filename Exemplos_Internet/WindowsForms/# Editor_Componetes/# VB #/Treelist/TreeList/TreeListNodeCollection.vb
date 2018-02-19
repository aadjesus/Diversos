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

<TypeConverter(GetType(TreeListNodeCollectionConverter)), _
Editor(GetType(TreeListNodeCollectionEditor), GetType(UITypeEditor)), _
DesignTimeVisible(False), _
ToolboxItem(False), _
Serializable()> _
Public Class TreeListNodeCollection
    Inherits CollectionBase
    Implements ICustomTypeDescriptor

#Region "CONSTRUCTOR"

    Sub New()

    End Sub

    Friend Sub New(ByVal TreeList As TreeList)
        MyBase.New()
        Me.TreeList = TreeList
    End Sub

#End Region

#Region "PROPERTIES"

    Private _TreeList As TreeList = Nothing
    <Browsable(False)> _
    Public Property TreeList() As TreeList
        Get
            Return _TreeList
        End Get
        Set(ByVal Value As TreeList)
            _TreeList = Value
            For Each _Node As TreeListNode In List
                _Node.TreeList = Value
            Next
        End Set
    End Property

    Default ReadOnly Property Item(ByVal Index As Integer) As TreeListNode
        Get
            Return DirectCast(List(Index), TreeListNode)
        End Get
    End Property

    Private _Depth As Integer = 0
    <Browsable(False)> _
    Property Depth() As Integer
        Get
            Return _Depth
        End Get
        Set(ByVal Value As Integer)
            _Depth = Value
            For Each _Node As TreeListNode In List
                _Node.Depth = Value + 1
            Next
        End Set
    End Property

#End Region

#Region "FUNCTIONS"

    Public Function Add(ByVal value As TreeListNode) As TreeListNode
        Dim _Index As Integer
        value.Parent = Me
        value.TreeList = TreeList
        value.Depth = Depth + 1
        List.Add(value)
        AddHandler value.MouseDown, AddressOf OnMouseDown
        AddHandler value.BeforeExpand, AddressOf OnBeforeExpand
        AddHandler value.AfterExpand, AddressOf OnAfterExpand
        AddHandler value.BeforeCollapse, AddressOf OnBeforeCollapse
        AddHandler value.AfterCollapse, AddressOf OnAfterCollapse
        Return value
    End Function

    Public Function Contains(ByVal value As TreeListNode) As Boolean
        Return List.Contains(value)
    End Function

    Public Sub Remove(ByVal value As TreeListNode)
        List.Remove(value)
        value.TreeList = Nothing
        TreeList.Invalidate()
    End Sub

    Public Function IndexOf(ByVal value As TreeListNode) As Integer
        Return List.IndexOf(value)
    End Function

#End Region

#Region "PROCEDURES"

    Public Sub Insert(ByVal index As Integer, ByVal value As TreeListNode)
        List.Insert(index, value)
    End Sub

    Public Shadows Sub Clear()
        MyBase.Clear()
    End Sub

    Protected Friend Sub HitTest(ByVal e As MouseEventArgs)
        For i As Integer = 0 To Count - 1
            Item(i).HitTest(e)
        Next
    End Sub

    Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
        CType(value, TreeListNode).Parent = Me
        CType(value, TreeListNode).Depth = Depth + 1
        CType(value, TreeListNode).TreeList = TreeList
    End Sub

#End Region

#Region "EVENTS"

    Public Event BeforeExpand(ByVal sender As Object)
    Protected Friend Sub OnBeforeExpand(ByVal sender As Object)
        RaiseEvent BeforeExpand(sender)
    End Sub

    Public Event AfterExpand(ByVal sender As Object)
    Protected Friend Sub OnAfterExpand(ByVal sender As Object)
        RaiseEvent AfterExpand(sender)
    End Sub

    Public Event BeforeCollapse(ByVal sender As Object)
    Protected Friend Sub OnBeforeCollapse(ByVal sender As Object)
        RaiseEvent BeforeCollapse(sender)
    End Sub

    Public Event AfterCollapse(ByVal sender As Object)
    Protected Friend Sub OnAfterCollapse(ByVal sender As Object)
        RaiseEvent AfterCollapse(sender)
    End Sub

    Public Event MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
    Protected Friend Sub OnMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        RaiseEvent MouseDown(sender, e)
    End Sub

#End Region

#Region "ICUSTOMTYPEDESCRIPTOR"

    Public Function GetAttributes() As System.ComponentModel.AttributeCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetAttributes
        Return TypeDescriptor.GetAttributes(Me, True)
    End Function

    Public Function GetClassName() As String Implements System.ComponentModel.ICustomTypeDescriptor.GetClassName
        Return TypeDescriptor.GetClassName(Me, True)
    End Function

    Public Function GetComponentName() As String Implements System.ComponentModel.ICustomTypeDescriptor.GetComponentName
        Return TypeDescriptor.GetComponentName(Me, True)
    End Function

    Public Function GetConverter() As System.ComponentModel.TypeConverter Implements System.ComponentModel.ICustomTypeDescriptor.GetConverter
        Return TypeDescriptor.GetConverter(Me, True)
    End Function

    Public Function GetDefaultEvent() As System.ComponentModel.EventDescriptor Implements System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent
        Return TypeDescriptor.GetDefaultEvent(Me, True)
    End Function

    Public Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor Implements System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty
        Return TypeDescriptor.GetDefaultProperty(Me, True)
    End Function

    Public Function GetEditor(ByVal editorBaseType As System.Type) As Object Implements System.ComponentModel.ICustomTypeDescriptor.GetEditor
        Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
    End Function

    Public Overloads Function GetEvents() As System.ComponentModel.EventDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(Me, True)
    End Function

    Public Overloads Function GetEvents(ByVal attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(Me, attributes, True)
    End Function

    Public Overloads Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetProperties
        Application.DoEvents()
    End Function

    Public Overloads Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetProperties
        Dim _Pds As New PropertyDescriptorCollection(Nothing)
        For _Index As Integer = 0 To List.Count - 1
            Dim _Pd As New TreeListNodePropertyDescriptor(Me, _Index)
            _Pds.Add(_Pd)
        Next
        Return _Pds
    End Function

    Public Function GetPropertyOwner(ByVal pd As System.ComponentModel.PropertyDescriptor) As Object Implements System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner
        Return Me
    End Function

#End Region

End Class



