Imports System
Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.CollectionEditor
Imports System.ComponentModel.Design.Serialization
Imports System.Globalization
Imports System.Collections
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Friend Class TreeListNodeCollectionEditor
    Inherits CollectionEditor

    Private _CollectionForm As CollectionForm

    Sub New(ByVal Type As Type)
        MyBase.New(Type)
    End Sub

    Protected Overrides Function CreateCollectionForm() As System.ComponentModel.Design.CollectionEditor.CollectionForm
        _CollectionForm = MyBase.CreateCollectionForm
        Return _CollectionForm
    End Function

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        If Not _CollectionForm Is Nothing AndAlso _CollectionForm.Visible Then
            Dim _CustomEditor As TreeListNodeCollectionEditor = New TreeListNodeCollectionEditor(CollectionType)
            Return _CustomEditor
        Else
            Return MyBase.EditValue(context, provider, value)
        End If
    End Function
End Class