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

Friend Class TreeListNodeCollectionConverter
    Inherits ExpandableObjectConverter

    Public Overloads Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destType As Type) As Boolean
        'If destType Is GetType(InstanceDescriptor) Then
        '    Return True
        'End If
        If destType Is GetType(String) Then
            Return True
        End If
        Return MyBase.CanConvertTo(context, destType)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destType As Type) As Object
        'If destType Is GetType(InstanceDescriptor) Then
        '    Dim ci As System.Reflection.ConstructorInfo = GetType(TreeListNode).GetConstructor(System.Type.EmptyTypes)
        '    Return New InstanceDescriptor(ci, Nothing, False)
        'End If
        If destType Is GetType(String) Then
            Return CType(value, TreeListNodeCollection).Count & " node(s)"
        End If
        Return MyBase.ConvertTo(context, culture, value, destType)
    End Function
End Class