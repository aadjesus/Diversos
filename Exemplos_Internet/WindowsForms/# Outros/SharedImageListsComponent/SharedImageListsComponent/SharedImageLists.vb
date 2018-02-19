Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Collections.Generic
Imports System.Reflection

''' <summary>
''' A component that allows ImageLists work correctly with Visual Inheritance on forms and controls.
''' The component provides both runtime and design time support for Sharing ImageLists.
''' </summary>
''' <remarks></remarks>
<ToolboxBitmap(GetType(SharedImageLists), "ImageList.png")> _
<Designer(GetType(SharedImageListsRootDesigner), GetType(IRootDesigner))> _
<Designer(GetType(SharedImageListsDesigner), GetType(IDesigner))> _
<DesignerSerializer(GetType(SharedImageListsCodeDomSerializer), GetType(CodeDomSerializer))> _
Public Class SharedImageLists
  Inherits System.ComponentModel.Component

  Private _imageLists As Dictionary(Of ImageList, ImageList)
  Private Shared _inIDE As Int32

#Region "     NewImageList "

  ''' <summary>
  ''' Used to map a Shared ImageList to a local 'target' ImageList.
  ''' This method is intended for internal use only.
  ''' </summary>
  ''' <param name="component">The container that the ImageList will be placed on</param>
  ''' <param name="sharedImageList">The shared ImageList to map to the local ImageList</param>
  ''' <returns>
  ''' in DesignMode it returns a new ImageList which contains the cloned images from the sharedImageList
  ''' At runtime the sharedImageList is returned.</returns>
  ''' <remarks>
  ''' In Design mode the ImageLists are not technically shared but are cloned. This is because a component can only be sited to one container at a time.
  ''' At runtime we just return sharedImageList parameterto the target ImageList.
  ''' </remarks>
  <EditorBrowsable(EditorBrowsableState.Never)> _
  Public Function NewImageList(ByVal component As IContainer, ByVal sharedImageList As ImageList) As ImageList

    If SharedImageListCodeDomSerializer.InDesignerMode Then
      Dim imageList As New ImageList

      'Add our imageList to the ImageLists collection
      'These ImageLists are then managed by the SharedImageListDesigner class.
      Me.ImageLists.Add(imageList, sharedImageList)

      If component IsNot Nothing AndAlso component.Components IsNot Nothing Then
        component.Add(imageList)
      End If

      Return imageList
    Else
      'Just return a reference to our Shared ImageList
      Return sharedImageList
    End If
  End Function

#End Region ' NewImageList

#Region "     GetSharedImageLists "

  ''' <summary>
  ''' Gets the SharedImageList component that contains the ImageLists that you want to share.
  ''' </summary>
  ''' <returns>A Shared imageList component.</returns>
  ''' <remarks>Override this method to return your private shared instance your inherited SharedImageLists component.</remarks>
  Public Overridable Function GetSharedImageLists() As SharedImageLists
    Return Me
  End Function

#End Region ' GetSharedImageLists

#Region "     ImageLists "

  ''' <summary>
  '''   Gets a Dictionary of ImageLists, only used in design mode.
  ''' </summary>
  ''' <value></value>
  ''' <returns>Dictionary(Of ImageList, ImageList)</returns>
  ''' <remarks>This cannot be placed inside the SharedImageListsDesigner class because
  ''' we need to add ImageLists from Inherited forms, and when an Inherited form is first loaded
  ''' it has no site so we cannot get access to the SharedImageListsDesigner class
  ''' </remarks>
  Friend ReadOnly Property ImageLists() As Dictionary(Of ImageList, ImageList)
    Get
      If _imageLists Is Nothing Then
        _imageLists = New Dictionary(Of ImageList, ImageList)
      End If
      Return _imageLists
    End Get
  End Property

#End Region ' ImageLists

#Region "     InIDE "
  ''' <summary>
  ''' Helper function to determin if we are running inside the IDE
  ''' </summary>
  ''' <returns>true if running in the IDE (Visual Studio 2005)</returns>
  ''' <remarks>
  ''' Site.Design mode does not work for code that is running as compiled code inside the IDE
  ''' See http://support.microsoft.com/kb/839202/en-us
  ''' This includes inherited and nested components
  ''' </remarks>
  Private Shared Function InIDE() As Boolean
    Debug.WriteLine("InIDE= " & _inIDE & ", LicMan= " & LicenseManager.UsageMode)
    If _inIDE = 0 Then
      If Application.ExecutablePath.EndsWith("\Microsoft Visual Studio 8\Common7\IDE\devenv.exe") Then
        _inIDE = 1
      Else
        _inIDE = 2
      End If
    End If
    Return _inIDE = 1
  End Function

#End Region ' InIDE


End Class
