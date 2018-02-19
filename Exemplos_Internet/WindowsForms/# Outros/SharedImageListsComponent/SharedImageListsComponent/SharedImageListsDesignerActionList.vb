#Region "     imports "

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Drawing.Design
Imports System.Reflection
Imports System.Text
Imports System.Windows.Forms
Imports System.ComponentModel.Design.Serialization
Imports System.Windows.Forms.Design

#End Region '      imports 


''' <summary>
''' A class that provides the functionality for displaying the smart tag dialogs.
''' </summary>
''' <remarks>
''' This class is used both for the SharedImageLists and  the shared ImageList components.
''' When the component passed is a shared ImageList then the Smart tab dialog displayed as read-only.
''' </remarks>
Friend Class SharedImageListsDesignerActionList : Inherits DesignerActionList
  Implements IDisposable

  Private uiService As DesignerActionUIService

#Region "     constructors "

  Public Sub New(ByVal component As IComponent)
    MyBase.New(component)
    Me.uiService = CType(MyBase.GetService(GetType(DesignerActionUIService)), DesignerActionUIService)
    Me.AutoShow = True
  End Sub

#End Region ' constructors

#Region "     IsInherited "
  ''' <summary>
  ''' Determines if a component is inherited by checking the IInheritanceService service.
  ''' </summary>
  ''' <param name="component"></param>
  ''' <returns>Returns true if the component is inherited.</returns>
  ''' <remarks></remarks>
  Private Function IsInherited(ByVal component As IComponent) As Boolean
    Dim ihs As IInheritanceService = CType(MyBase.GetService(GetType(IInheritanceService)), IInheritanceService)
    Return ihs IsNot Nothing AndAlso Not ihs.GetInheritanceAttribute(component) Is InheritanceAttribute.NotInherited
  End Function

#End Region ' IsInherited

#Region "     IDisposable Support "

  Private disposedValue As Boolean = False    ' To detect redundant calls
  Protected Overridable Sub Dispose(ByVal disposing As Boolean)
    If Not Me.disposedValue Then
      If disposing Then
        Dim s As ISelectionService = DirectCast(GetService(GetType(ISelectionService)), ISelectionService)
        Dim c As IComponentChangeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)
      End If
    End If
    Me.disposedValue = True
  End Sub

  ' This code added by Visual Basic to correctly implement the disposable pattern.
  Public Sub Dispose() Implements IDisposable.Dispose
    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    Dispose(True)
    GC.SuppressFinalize(Me)
  End Sub

#End Region ' IDisposable Support

#Region "     GetSortedActionItems "

  ''' <summary>
  ''' Overridden to return a collection of DesignerActionItems for use in the smart tag panel.
  ''' </summary>
  ''' <returns>A collection of DesignerActionItems</returns>
  ''' <remarks></remarks>
  Public Overrides Function GetSortedActionItems() As DesignerActionItemCollection
    Dim adding As Boolean = False

    Dim designer As SharedImageListsDesigner = Me.Designer
    If designer Is Nothing Then Return Nothing

    Dim selectedImageListName As String = Nothing
    Dim targetImageList As ImageList = Nothing
    Dim sharedImageList As ImageList = Nothing

    ' Create list to store designer action items
    Dim actionItems As DesignerActionItemCollection = New DesignerActionItemCollection()

    If TypeOf Me.Component Is SharedImageLists Then
      'SharedImageLists

      sharedImageList = Me.SelectedImageList
      If sharedImageList IsNot Nothing Then
        targetImageList = designer.TargetImageListFromSharedImageList(sharedImageList)
        selectedImageListName = designer.GetSharedImageListName(Me.SelectedImageList)
      End If

      actionItems.Add(New DesignerActionHeaderItem("Shared ImageLists", "Design"))
      actionItems.Add(New DesignerActionTextItem("Select a shared ImageList from the list.", "Design"))
      actionItems.Add(New DesignerActionPropertyItem("SelectedImageListName", "Shared ImageLists", "Design", "The shared ImageList to assign."))

      If targetImageList Is Nothing OrElse Not Me.IsInherited(targetImageList) Then
        If Not designer.ImageLists.ContainsValue(sharedImageList) Then
          actionItems.Add(New DesignerActionMethodItem(Me, "OnAddImageList", "Add " & selectedImageListName, "Design", "Add the Shared ImageList to the container.", False))
        Else
          actionItems.Add(New DesignerActionMethodItem(Me, "OnRemoveImageList", "Remove " & selectedImageListName, "Design", "Remove the Shared ImageList from the container.", False))
        End If
      Else
        actionItems.Add(New DesignerActionTextItem(targetImageList.Site.Name & " (Inherited)", "Design"))
      End If

    Else

      'ImageList
      targetImageList = CType(Me.Component, ImageList)
      sharedImageList = Utility.SharedImageList(CType(Me.Component, ImageList))
      selectedImageListName = designer.GetSharedImageListName(sharedImageList)

      actionItems.Add(New DesignerActionHeaderItem("Shared With ImageList", "Design"))
      actionItems.Add(New DesignerActionTextItem(Me.SharedImageLists.Site.Name & "." & selectedImageListName, "Design"))

      If Not Me.IsInherited(targetImageList) Then
        actionItems.Add(New DesignerActionMethodItem(Me, "RemoveImageList", "Remove " & designer.GetSharedImageListName(sharedImageList), "Design", "Remove the Shared ImageList from the container.", False))
      End If

    End If

    If sharedImageList IsNot Nothing Then
      'Adds default ImageList information.
      actionItems.Add(New DesignerActionHeaderItem("Properties", "Appearance"))
      actionItems.Add(New DesignerActionTextItem(PropertyToString(sharedImageList, "ColorDepth"), "Appearance"))
      actionItems.Add(New DesignerActionTextItem(PropertyToString(sharedImageList, "ImageSize"), "Appearance"))
      actionItems.Add(New DesignerActionTextItem(PropertyToString(sharedImageList, "TransparentColor"), "Appearance"))
      actionItems.Add(New DesignerActionTextItem(GetImageCount(sharedImageList), "Appearance"))
    End If

    Return actionItems
  End Function

#End Region ' GetSortedActionItems

#Region "     PropertyToString "

  ''' <summary>
  ''' Fetches the value of a property 
  ''' </summary>
  ''' <param name="component">The component used get the value of the propertyName from.</param>
  ''' <param name="propertyName">The name of the property on the component.</param>
  ''' <returns>A string in the format "{Property Name} : {The property's string value}</returns>
  ''' <remarks></remarks>
  Public Function PropertyToString(ByVal component As IComponent, ByVal propertyName As String) As String
    Dim stringValue As String = Nothing
    If component IsNot Nothing Then
      Dim propDesc As PropertyDescriptor = TypeDescriptor.GetProperties(component)(propertyName)
      If propDesc IsNot Nothing Then
        Dim value As Object = propDesc.GetValue(component)
        If value IsNot Nothing Then
          stringValue = value.ToString
        End If
      End If
    End If
    Return propertyName & ": " & stringValue
  End Function

#End Region ' PropertyToString

#Region "     GetImageCount "

  ''' <summary>
  ''' Returns the number of images in a shared ImageList.
  ''' </summary>
  ''' <param name="selectedImageList"></param>
  ''' <returns>A string in the format "Images: {Count of Images}"</returns>
  ''' <remarks></remarks>
  Private Function GetImageCount(ByVal selectedImageList As ImageList) As String
    If selectedImageList IsNot Nothing Then
      Return "Images: " & selectedImageList.Images.Count.ToString
    End If
    Return "Images:"
  End Function

#End Region ' GetImageCount

#Region "     SelectedImageListName "

  ''' <summary>
  ''' Used to select a shared ImageList name from a dropdown list
  ''' </summary>
  ''' <value></value>
  ''' <returns>The name of the selected shared ImageList</returns>
  ''' <remarks></remarks>
  <Editor(GetType(SharedImageListSelectorUITypeEditor), GetType(UITypeEditor))> _
  Public Property SelectedImageListName() As String
    Get
      Return Me.Designer.SelectedImageListName
    End Get
    Set(ByVal value As String)
      If Not value = Me.SelectedImageListName Then
        Me.Designer.SelectedImageListName = value
        Me.Refresh()
      End If
    End Set
  End Property

#End Region ' SelectedImageListName

#Region "     SelectedImageList "

  ''' <summary>
  ''' Gets/sets the selected shared ImageList
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function SelectedImageList() As ImageList
    If TypeOf Me.Component Is ImageList Then
      Return CType(Me.Component, ImageList)
    End If

    Dim name As String = Me.SelectedImageListName
    If Not String.IsNullOrEmpty(name) Then
      Dim prop As PropertyInfo = Me.SharedImageLists.GetType.GetProperty(name, Reflection.BindingFlags.Public Or Reflection.BindingFlags.CreateInstance Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.GetField Or Reflection.BindingFlags.NonPublic)
      If prop IsNot Nothing Then
        Return CType(prop.GetValue(Me.SharedImageLists.GetSharedImageLists, Nothing), ImageList)
      End If
    End If

    Return Nothing
  End Function

#End Region ' SelectedImageList

#Region "     Refresh "

  ''' <summary>
  ''' Refreshes the SmartTags panel 
  ''' </summary>
  ''' <remarks>Causes the GetSortedActionItems proc to be recalled.</remarks>
  Private Sub Refresh()
    If Me.uiService IsNot Nothing Then
      Me.uiService.Refresh(Me.Component)
    End If
  End Sub

#End Region ' Refresh

#Region "     OnRemoveImageList "

  ''' <summary>
  ''' The handler for removing an ImageList from the SmartTags panel.
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub OnRemoveImageList()
    Dim imageListTarget As ImageList = Nothing
    Dim sharedImageList As ImageList = Me.SelectedImageList
    'Find the target ImageList using the Me.SelectedImageList
    For Each il As KeyValuePair(Of ImageList, ImageList) In Me.Designer.ImageLists
      If il.Value Is sharedImageList Then imageListTarget = il.Key
    Next
    If imageListTarget IsNot Nothing Then
      Me.Designer.RemoveImageListInternal(imageListTarget)
    End If
    Me.Refresh()
  End Sub

#End Region ' OnRemoveImageList

#Region "     OnAddImageList "

  ''' <summary>
  ''' The handler for adding an new shared ImageList from the SmartTags panel.
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub OnAddImageList()
    Me.Designer.CreateNewTargetImageList()
    Me.Refresh()
  End Sub

#End Region ' OnAddImageList

#Region "     SharedImageLists "

  ''' <summary>
  ''' A helper function for getting the SharedImageLists instance
  ''' regardless of whether our base Component is an ImageList or a SharedImageLists component.
  ''' </summary>
  ''' <returns>A SharedImageLists instance.</returns>
  ''' <remarks></remarks>
  Friend Function SharedImageLists() As SharedImageLists
    If TypeOf Me.Component Is SharedImageLists Then
      Return CType(Me.Component, SharedImageLists)
    Else
      Return Utility.SharedImageLists(CType(Me.Component, ImageList))
    End If
  End Function

#End Region ' SharedImageLists

#Region "     Designer "

  ''' <summary>
  '''  A helper function for getting the SharedImageListsDesigner instance
  ''' </summary>
  ''' <returns>A SharedImageListsDesigner instance.</returns>
  ''' <remarks></remarks>
  Private Function Designer() As SharedImageListsDesigner
    Dim designerHost As IDesignerHost = CType(Me.SharedImageLists.Site.Container, IDesignerHost)
    Return CType(designerHost.GetDesigner(Me.SharedImageLists), SharedImageListsDesigner)
  End Function

#End Region ' Designer

End Class
