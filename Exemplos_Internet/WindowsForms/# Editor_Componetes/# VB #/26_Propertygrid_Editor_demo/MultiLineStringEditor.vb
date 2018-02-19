Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms.Design

Public Class MultiLineStringEditor
    Inherits UITypeEditor
    Private service As IWindowsFormsEditorService

    Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (Not context Is Nothing And Not context.Instance Is Nothing And Not provider Is Nothing) Then
            service = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If (Not service Is Nothing) Then
                ' the Edit method gets the value ByRef, 
                ' and changes it to the new value when user clicks ok
                ' I don't know why ... but it took me ages to get this part of the project working 
                ' I didn't find how i could change the property in the propertybag ... 
                ' until i found out that i just needed to change "value" to the new version
                MultiLineStringEditorForm.Edit(value, "Edit " & context.PropertyDescriptor.Name)
            End If
        End If
        Return value
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If (Not context Is Nothing And Not context.Instance Is Nothing) Then
            ' we'll show a modal form
            Return UITypeEditorEditStyle.Modal
        End If
        Return MyBase.GetEditStyle(context)
    End Function
End Class