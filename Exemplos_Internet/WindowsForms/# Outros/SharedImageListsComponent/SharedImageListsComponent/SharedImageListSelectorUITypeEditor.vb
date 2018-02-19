Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Windows.Forms.Design
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Collections

Imports System.ComponentModel.Design.Serialization
<DesignerSerializer(GetType(MyUserControlDesignerSerializer), GetType(System.ComponentModel.Design.Serialization.CodeDomSerializer))> _
Public Class MyUserControl
  Inherits UserControl
End Class

Friend Class MyUserControlDesignerSerializer
  Inherits System.ComponentModel.Design.Serialization.CodeDomSerializer

  Private Shared _inIDE As Int32
  Public Shared Function DesignMode() As Boolean
    Return _inIDE = 2
  End Function
  Private Sub InitDesignMode(ByVal serviceProvider As IServiceProvider)
    If Not _inIDE = 0 Then Return
    Dim idh As IDesignerHost = CType(serviceProvider.GetService(GetType(IDesignerHost)), IDesignerHost)
    If idh IsNot Nothing Then
      _inIDE = 2
    End If
  End Sub

  Public Overrides Function Deserialize(ByVal manager As System.ComponentModel.Design.Serialization.IDesignerSerializationManager, ByVal codeObject As Object) As Object
    InitDesignMode(manager)
    Dim baseSerializer As CodeDomSerializer = CType(manager.GetSerializer(GetType(Component), GetType(CodeDomSerializer)), CodeDomSerializer)
    If baseSerializer IsNot Nothing Then
      Return baseSerializer.Deserialize(manager, codeObject)
    End If
    Return Nothing
  End Function
End Class

''' <summary>
''' Provides the smart tag form with it's drop down list of shared ImageLists
''' </summary>
''' <remarks></remarks>
Friend Class SharedImageListSelectorUITypeEditor
  Inherits System.Drawing.Design.UITypeEditor
  Private _editorService As IWindowsFormsEditorService

#Region "     GetEditStyle "

  ''' <summary>
  '''  An Override to let the caller know we are a dropdown editor style'
  ''' </summary>
  ''' <param name="context"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
    Return Drawing.Design.UITypeEditorEditStyle.DropDown
  End Function

#End Region ' GetEditStyle

#Region "     EditValue "

  ''' <summary>
  ''' An override method which fills and displays the drop down listbox.
  ''' </summary>
  ''' <param name="context"></param>
  ''' <param name="provider"></param>
  ''' <param name="value"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object

    If Not (provider Is Nothing) Then
      _editorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
      If _editorService IsNot Nothing Then
        Dim listbox As New ListBox
        Dim sharedImageLists As SharedImageLists = CType(context.Instance, SharedImageListsDesignerActionList).SharedImageLists
        Dim imageLists As New Generic.List(Of String)

        listbox.IntegralHeight = False
        listbox.BorderStyle = BorderStyle.None

        Dim designer As SharedImageListsDesigner = Utility.GetDesigner(sharedImageLists)
        If designer Is Nothing Then Return Nothing

        listbox.Items.AddRange(designer.GetSharedImageListNames)

        If TypeOf value Is String Then
          listbox.SelectedItem = value
        End If
        AddHandler listbox.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        Try
          _editorService.DropDownControl(listbox)
        Finally
          RemoveHandler listbox.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        End Try
        If listbox.SelectedIndex >= 0 Then
          value = CStr(listbox.SelectedItem)
        End If
      End If
    End If

    Return value
  End Function

#End Region ' EditValue

#Region "     ListBox_SelectedIndexChanged "

  ''' <summary>
  '''  Event handler for closing the drop down list box when the selection has changed
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    _editorService.CloseDropDown()
  End Sub

#End Region ' ListBox_SelectedIndexChanged

End Class

