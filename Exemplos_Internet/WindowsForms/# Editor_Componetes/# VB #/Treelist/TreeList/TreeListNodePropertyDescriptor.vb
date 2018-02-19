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
Imports System.Text

Public Class TreeListNodePropertyDescriptor
    Inherits PropertyDescriptor

    Private _Collection As TreeListNodeCollection = Nothing
    Private _Index As Integer = -1

    Sub New(ByVal Collection As TreeListNodeCollection, ByVal Index As Integer)
        MyBase.New("#" + Index.ToString(), Nothing)
        _Collection = Collection
        _Index = Index
    End Sub
    Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property ComponentType() As System.Type
        Get
            Return _Collection.GetType
        End Get
    End Property

    Public Overrides Function GetValue(ByVal component As Object) As Object
        Return _Collection(_Index)
    End Function

    Public Overrides ReadOnly Property IsReadOnly() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property PropertyType() As System.Type
        Get
            Return _Collection(_Index).GetType()
        End Get
    End Property

    Public Overrides Sub ResetValue(ByVal component As Object)

    End Sub

    Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)

    End Sub

    Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property Attributes() As System.ComponentModel.AttributeCollection
        Get
            Return New AttributeCollection(Nothing)
        End Get
    End Property

    Public Overrides ReadOnly Property DisplayName() As String
        Get
            Dim _Node As TreeListNode = _Collection(_Index)
            Return _Node.Text
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String
        Get
            Dim _Node As TreeListNode = _Collection(_Index)
            Return _Node.Description
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "#" + _Index.ToString()
        End Get
    End Property
End Class
