' ==============================================================================
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'
' ©2004 Palo Mraz, LaMarvin. All Rights Reserved.
' ==============================================================================

Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design

Namespace LaMarvin.Windows.Forms.Design

  '<summary>
  ' This is a type editor that allows to choose images from either an assembly's manifest
  ' resources or from the file system.
  ' In order for an image to be recognized by this editor, the image file should be included
  ' into project and marked as "Embedded Resource" in the image's properties window.
  '</summary>
  Public Class ResourceImageEditor
    Inherits ImageEditor

    ' This is a table of file extensions that are supposed to be image files.
    Private Shared _ImageFileExtensions() As String = {".bmp", ".jpg", ".gif", ".png"}

    ' This is the assembly with the image resources in it. Initially, it is our own assembly.
    ' The user can change the assembly by setting the ResourceAssembly property to any valid
    ' System.Reflection.Assembly reference.
    Private _ResourceAssembly As System.Reflection.Assembly = GetType(ResourceImageEditor).Assembly

    ' These members are initialized in the EditValue method in order to be available in the
    ' ListBox event handler methods. For example to invoke the ancestor's EditValue when the user chooses
    ' the "Browse..." button.
    Private _EditorService As IWindowsFormsEditorService
    Private _Context As ITypeDescriptorContext
    Private _Provider As IServiceProvider
    Private _Value As Object
    Private _BrowseItemIndex As Integer
    Private _NoneItemIndex As Integer


    '<summary>
    ' The assembly containing the image resources.
    '</summary>
    Public Property ResourceAssembly() As System.Reflection.Assembly
      Get
        Return Me._ResourceAssembly
      End Get
      Set(ByVal Value As System.Reflection.Assembly)
        If Value Is Nothing Then
          Throw New ArgumentNullException("Value")
        End If
        Me._ResourceAssembly = Value
      End Set
    End Property


    '<summary>
    ' Alway returns the DropDown style.
    '</summary>
    Public Overloads Overrides Function GetEditStyle( _
      ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle

      Return UITypeEditorEditStyle.DropDown
    End Function


    '<summary>
    ' Displays our drop-down list control with names of the image resources from our assembly
    ' along with the special "Browse..." and "None" items.
    '</summary>
    Public Overloads Overrides Function EditValue( _
      ByVal context As ITypeDescriptorContext, _
      ByVal provider As System.IServiceProvider, _
      ByVal value As Object) As Object

      If context Is Nothing Then
        Return value
      End If

      ' Store the context information for use in the drop-down ListBox event handlers.
      Me._EditorService = DirectCast(context.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
      If Me._EditorService Is Nothing Then
        Exit Function
      End If

      Me._Context = context
      Me._Provider = provider
      Me._Value = value

      ' Create and fill the listbox with the names of the available image resources.
      ' Note: we want to have the resource names sorted, but the two special items should
      ' be always on the bottom of the list. Therefore, we can't just set the ListBox.Sorted
      ' to True, because the "(none)" string will likely end up at the top of the list.
      ' System.Collections.Specialized.StringCollection = Me.GetImageResourceNames()
      Dim SortedNames As ArrayList = Me.GetImageResourceNames()
      SortedNames.Sort(New CaseInsensitiveComparer)

      Dim List As New EnterKeyListBox      
      For Each S As String In SortedNames
        List.Items.Add(S)
      Next

      ' Add our special items.
      Me._BrowseItemIndex = List.Items.Add("Browse...")
      Me._NoneItemIndex = List.Items.Add("(none)")

      ' This was used to "highlight" the drop-down list when making the screenshot :-)
      'List.BackColor = Color.OrangeRed
      'List.ForeColor = Color.White
      'List.Font = New Font(List.Font, FontStyle.Bold)

      ' Wire the ListBox events and display the drop-down UI. The selection process
      ' is then handled inside the event handler methods.
      AddHandler List.Click, AddressOf Me.OnClickInList
      AddHandler List.EnterPressed, AddressOf Me.OnEnterPressedInList
      _EditorService.DropDownControl(List)

      Return Me._Value
    End Function


    Private Sub OnClickInList( _
      ByVal sender As Object, _
      ByVal e As EventArgs)

      Me.ProcessSelectedListItem(DirectCast(sender, ListBox))
    End Sub


    Private Sub OnEnterPressedInList(ByVal sender As Object, ByVal e As EventArgs)
      ' We have to accept anything that is selected in the list box.
      Me.ProcessSelectedListItem(DirectCast(sender, ListBox))
    End Sub


    '<summary>
    ' Handles the user selection in the drop-down list taking into account
    ' our special "Browse" and "None" items.
    '</summary>
    Private Sub ProcessSelectedListItem(ByVal list As ListBox)
      ' When the user selects Browse, we'll display the OpenFile dialog by calling
      ' the ancestor's EditValue method (we know for sure it will show the dialog).
      ' If the user selects None, we'll set the value to Nothing.
      ' Otherwise we will load the image from the resource.

      If list.SelectedIndex = Me._BrowseItemIndex Then
        Me._Value = MyBase.EditValue(Me._Context, Me._Provider, Me._Value)
      ElseIf list.SelectedIndex = Me._NoneItemIndex Then
        Me._Value = Nothing
      ElseIf list.SelectedIndex >= 0 Then
        Me._Value = Me.LoadResourceImage(CStr(list.SelectedItem))
      End If

      Me._EditorService.CloseDropDown()
    End Sub


    '<summary>
    ' Loads the specified image resource.
    '</summary>
    Private Function LoadResourceImage(ByVal resourceName As String) As Image
      Debug.Assert(Not resourceName Is Nothing)

      Dim ImageStream As System.IO.Stream = _
        Me.ResourceAssembly.GetManifestResourceStream(resourceName)
      Return System.Drawing.Bitmap.FromStream(ImageStream)
    End Function


    '<summary>
    ' Retrieves the names of all the image resources in the ResourceAssembly assembly.
    '</summary>
    Private Function GetImageResourceNames() As ArrayList
      Dim ImageResourceNames As New ArrayList
      For Each Name As String In Me.ResourceAssembly.GetManifestResourceNames()
        If Me.IsResourceImageResource(Name) Then
          ImageResourceNames.Add(Name)
        End If
      Next
      Return ImageResourceNames
    End Function


    '<summary>
    ' Checks to see if the resourceName contains an image file extension.
    '</summary>
    Private Function IsResourceImageResource(ByVal resourceName As String) As Boolean
      Debug.Assert(Not resourceName Is Nothing)

      ' Get the index of the last dot in the name.
      Dim LastDotIndex As Integer = resourceName.LastIndexOf("."c)
      If LastDotIndex < 0 Then
        Return False
      End If

      ' Extract the extension and look it up in our static table. If it's there,
      ' we're assuming that the resource is, in fact, an image resource.
      Dim Ext As String = resourceName.Substring(LastDotIndex).ToLower()
      Return (System.Array.IndexOf(ResourceImageEditor._ImageFileExtensions, Ext) >= 0)
    End Function


    '<summary>
    ' This ListBox descendant intercepts the ENTER key and generates the 
    ' distinguished EnterPressed event.
    '</summary>
    Private Class EnterKeyListBox
      Inherits ListBox

      Public Event EnterPressed As EventHandler

      Protected Overrides Function ProcessDialogKey(ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
          ' Note: keyData is a bitmask, so the following IF statement is evaluated to true
          ' only if just the ENTER key is pressed, without any modifiers (Shift, Alt, Ctrl...).
          If keyData = System.Windows.Forms.Keys.Return Then
            RaiseEvent EnterPressed(Me, EventArgs.Empty)
            Return True ' True means we've processed the key
          Else
            Return MyBase.ProcessDialogKey(keyData)
          End If
        Catch ex As Exception
          Trace.WriteLine(ex.ToString())
        End Try
      End Function

    End Class

  End Class

End Namespace