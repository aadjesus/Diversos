#Region "     imports "

Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Windows.Forms.Design
Imports System.Reflection
Imports System.Collections
Imports System.Collections.Generic

#End Region '      imports 

''' <summary>
''' The designer class that provides all the design time support for the SharedImageLists component.
''' </summary>
''' <remarks></remarks>
Friend Class SharedImageListsDesigner
  Inherits ComponentDesigner
  Implements ITypeDescriptorFilterService

  Private _selectedImageList As String
  Private _imageListFilterService As ITypeDescriptorFilterService

#Region "     Initialize "

  ''' <summary>
  ''' Prepares the designer to view, edit, and design the specified component.
  ''' </summary>
  ''' <param name="component">The component for this designer. this is of type SharedImageList</param>
  ''' <remarks></remarks>
  Public Overrides Sub Initialize(ByVal component As System.ComponentModel.IComponent)
    MyBase.Initialize(component)

    'Hook up the event handlers
    Me.AddEventHandlers()

    For Each il As KeyValuePair(Of ImageList, ImageList) In Me.SharedImageLists.ImageLists
      Me.InitializeImageList(il.Key, il.Value)
    Next

    'Validate the selected ImageList name
    ValidateSelectedImageListName(Me.SelectedImageListName, True)

    'Add the ITypeDescriptorFilterService service
    Dim isc As IServiceContainer = CType(Me.GetService(GetType(IServiceContainer)), IServiceContainer)
    If (Not isc Is Nothing) Then
      Me._imageListFilterService = CType(Me.GetService(GetType(ITypeDescriptorFilterService)), ITypeDescriptorFilterService)
      If (Me._imageListFilterService IsNot Nothing) Then
        isc.RemoveService(GetType(ITypeDescriptorFilterService))
      End If
      isc.AddService(GetType(ITypeDescriptorFilterService), Me)
    End If

    System.ComponentModel.TypeDescriptor.Refresh(GetType(ImageList))

  End Sub

#End Region ' Initialize

#Region "     Dispose "

  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      'Unhook events
      Me.RemoveEventHandlers()
    End If
    MyBase.Dispose(disposing)
  End Sub

#End Region ' Dispose

#Region "     PostFilterProperties "

  ''' <summary>
  ''' Allows a designer to add to the set of properties that it exposes through a System.ComponentModel.TypeDescriptor.
  ''' </summary>
  ''' <param name="properties">The properties for the class of the component. </param>
  ''' <remarks>Note do not remove properties from this collection if possible because this method is called from everywhere
  ''' including invoking properties from the deserialize routines.</remarks>
  Protected Overrides Sub PostFilterProperties(ByVal properties As Collections.IDictionary)
    MyBase.PreFilterProperties(properties)
    Dim keys(properties.Keys.Count - 1) As String
    properties.Keys.CopyTo(keys, 0)
    For i As Int32 = 0 To keys.Length - 1
      Dim pd As PropertyDescriptor = DirectCast(properties(keys(i)), PropertyDescriptor)
      If pd.PropertyType.Equals(GetType(ImageList)) Then
        'Hide the imageLists from the IDE
        'Note do not remove properties from the collection
        'as they are used everywhere including invoking properties when deserialiing.
        properties.Item(keys(i)) = TypeDescriptor.CreateProperty(GetType(SharedImageLists), pd, _
        New Attribute() {New DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden), New BrowsableAttribute(False)})
      End If
    Next

  End Sub

#End Region ' PostFilterProperties

#Region "     ActionLists "
  ''' <summary>
  ''' Overridden to provide our SharedImageList designer with a SmartTags interface for use in the designer.
  ''' </summary>
  ''' <value></value>
  ''' <returns>A DesignerActionListCollection containing a SharedImageListsDesignerActionList class</returns>
  ''' <remarks></remarks>
  Public Overrides ReadOnly Property ActionLists() As System.ComponentModel.Design.DesignerActionListCollection
    Get
      ' Create action list collection
      Dim actionListCol As DesignerActionListCollection = New DesignerActionListCollection()

      ' Add custom action list
      actionListCol.Add(New SharedImageListsDesignerActionList(Me.Component))

      ' Return to the designer action service
      Return actionListCol
    End Get
  End Property

#End Region ' ActionLists

#Region "     ImageLists "
  ''' <summary>
  ''' Returns the collection of ImageLists owned by the SharedImageList Component.
  ''' </summary>
  ''' <value></value>
  ''' <returns>A collection of ImageList objects in a Generic Dictionary(Of ImageList, ImageList)
  ''' the key used is the Target ImageList and the value is the Shared ImageList that's mapped to the target.</returns>
  ''' <remarks></remarks>
  Friend ReadOnly Property ImageLists() As Dictionary(Of ImageList, ImageList)
    Get
      Return Me.SharedImageLists.ImageLists
    End Get
  End Property

#End Region ' ImageLists

#Region "     Add/Remove EventHandlers "

  ''' <summary>
  ''' Adds any event handlers the designer needs
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AddEventHandlers()
    Dim ics As IComponentChangeService = DirectCast(Component.Site.GetService(GetType(IComponentChangeService)), IComponentChangeService)
    If ics IsNot Nothing Then
      AddHandler ics.ComponentAdded, AddressOf OnComponentAdded
      AddHandler ics.ComponentRemoving, AddressOf OnComponentRemoving
      AddHandler ics.ComponentChanged, AddressOf OnComponentChanged
    End If
  End Sub

  ''' <summary>
  ''' Removes any event handlers the designer added in the AddEventHandlers routine
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub RemoveEventHandlers()
    Dim ics As IComponentChangeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)
    If ics IsNot Nothing Then
      RemoveHandler ics.ComponentAdded, AddressOf OnComponentAdded
      RemoveHandler ics.ComponentRemoving, AddressOf OnComponentRemoving
      RemoveHandler ics.ComponentChanged, AddressOf OnComponentChanged
    End If
  End Sub

#End Region ' Add/Remove EventHandlers

#Region "     SharedImageLists "

  ''' <summary>
  ''' Returns the SharedImageLists component for this designer
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private ReadOnly Property SharedImageLists() As SharedImageLists
    Get
      Return CType(Me.Component, SharedImageLists)
    End Get
  End Property

#End Region ' SharedImageLists

#Region "     GetSharedImageListNames "

  ''' <summary>
  ''' Gets the names of the shared ImageLists available through the GetSharedImageLists call
  ''' </summary>
  ''' <returns>An array of strings containing the names of the ImageLists or null if none were found</returns>
  ''' <remarks></remarks>
  Friend Function GetSharedImageListNames() As String()
    Dim sharedImageLists As SharedImageLists = Me.SharedImageLists.GetSharedImageLists

    If sharedImageLists IsNot Nothing Then

      Const flags As BindingFlags = BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.GetField Or BindingFlags.GetProperty
      Dim aInfo As MemberInfo() = sharedImageLists.GetType.FindMembers(MemberTypes.Field Or MemberTypes.Property, flags, Nothing, Nothing)

      Dim list As New Dictionary(Of String, String)
      For i As Int32 = 0 To aInfo.Length - 1
        Select Case aInfo(i).MemberType
          Case MemberTypes.Property
            If CType(aInfo(i), PropertyInfo).PropertyType.Equals(GetType(ImageList)) Then
              list(aInfo(i).Name) = aInfo(i).Name
            End If
          Case MemberTypes.Field
            If CType(aInfo(i), FieldInfo).FieldType.Equals(GetType(ImageList)) Then
              'if a Property of the same name exists then ignore the field
              If Not list.ContainsKey(aInfo(i).Name) Then
                list(aInfo(i).Name) = aInfo(i).Name
              End If
            End If
        End Select
      Next
      If list.Keys.Count > 0 Then
        Dim names(list.Keys.Count - 1) As String
        list.Keys.CopyTo(names, 0)
        Return names
      End If
    End If

    ReDim GetSharedImageListNames(-1)
    Return GetSharedImageListNames
  End Function

#End Region ' GetSharedImageListNames

#Region "     CreateUniqueName "

  ''' <summary>
  ''' Validates the passed ImageList name and returns a valid version of it. The returned string is appended with a number if the name was not unique.
  ''' </summary>
  ''' <param name="name"></param>
  ''' <returns>a unique valid name for a new target ImageList</returns>
  ''' <remarks></remarks>
  Private Function CreateUniqueName(ByVal name As String) As String
    Dim ncs As INameCreationService = CType(Me.GetService(GetType(INameCreationService)), INameCreationService)
    If ncs Is Nothing Then Return Nothing
    Dim c As Int32 = 1
    If Not ncs.IsValidName(name) Then
      Do While Not ncs.IsValidName(name & c.ToString)
        c += 1
      Loop
      name &= c.ToString
    End If
    Return name
  End Function

#End Region ' CreateUniqueName

#Region "     SelectedImageList "

  ''' <summary>
  ''' Gets the Shared ImageList thats selected in the SmartTags designer
  ''' </summary>
  ''' <returns>A shared ImageList</returns>
  ''' <remarks></remarks>
  Friend Function SelectedImageList() As ImageList
    Dim name As String = Me.SelectedImageListName
    If Not String.IsNullOrEmpty(name) Then
      Dim prop As PropertyInfo = Me.SharedImageLists.GetSharedImageLists.GetType.GetProperty(name, Reflection.BindingFlags.Public Or Reflection.BindingFlags.CreateInstance Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.GetField Or Reflection.BindingFlags.NonPublic)
      If prop IsNot Nothing Then
        Return CType(prop.GetValue(Me.SharedImageLists.GetSharedImageLists, Nothing), ImageList)
      Else
        Dim field As FieldInfo = Me.SharedImageLists.GetSharedImageLists.GetType.GetField(name, Reflection.BindingFlags.Public Or Reflection.BindingFlags.CreateInstance Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.GetField)
        If field IsNot Nothing Then
          Return CType(field.GetValue(Me.SharedImageLists.GetSharedImageLists), ImageList)
        End If
      End If
    End If
    Return Nothing
  End Function

#End Region ' SelectedImageList

#Region "     SelectedImageListName "

  ''' <summary>
  ''' Gets/Sets the name of the currently selected shared ImageList .
  ''' </summary>
  ''' <value>The name of a Shared ImageList To select.</value>
  ''' <returns>The currently selected shared ImageList.</returns>
  ''' <remarks></remarks>
  Friend Property SelectedImageListName() As String
    Get
      Return _selectedImageList
    End Get
    Set(ByVal value As String)
      If ValidateSelectedImageListName(value, False) Then
        _selectedImageList = value
      End If
    End Set
  End Property

#End Region ' SelectedImageListName

#Region "     TargetImageListFromSharedImageList "

  Friend Function TargetImageListFromSharedImageList(ByVal sharedImageList As IComponent) As ImageList
    If sharedImageList.Site Is Nothing Then Return Nothing
    If Not TypeOf sharedImageList Is ImageList Then Return Nothing
    For Each il As Generic.KeyValuePair(Of ImageList, ImageList) In Me.ImageLists
      If il.Value Is sharedImageList Then
        Return il.Key
      End If
    Next
    Return Nothing
  End Function

#End Region ' TargetImageListFromSharedImageList

#Region "     GetSharedImageListName "

  ''' <summary>
  ''' Gets the name of the shared ImageList from the class object returned via the SharedImageList.GetSharedImages method
  ''' </summary>
  ''' <param name="sharedImageList"></param>
  ''' <returns>The name of the shared ImageList passed.</returns>
  ''' <remarks></remarks>
  Friend Function GetSharedImageListName(ByVal sharedImageList As ImageList) As String
    If sharedImageList Is Nothing Then Return Nothing
    Dim sharedImageLists As SharedImageLists = Me.SharedImageLists.GetSharedImageLists
    If sharedImageLists Is Nothing Then Return Nothing

    Dim aInfo As MemberInfo() = sharedImageLists.GetType.FindMembers( _
        MemberTypes.Field Or MemberTypes.Property, BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.GetField Or BindingFlags.GetProperty, Nothing, Nothing)

    For i As Int32 = 0 To aInfo.Length - 1
      Select Case aInfo(i).MemberType
        Case MemberTypes.Property
          If CType(aInfo(i), PropertyInfo).PropertyType.Equals(GetType(ImageList)) Then
            If CType(aInfo(i), PropertyInfo).GetValue(sharedImageLists, Nothing) Is sharedImageList Then
              Return aInfo(i).Name
            End If
          End If
        Case MemberTypes.Field
          If CType(aInfo(i), FieldInfo).FieldType.Equals(GetType(ImageList)) Then
            If CType(aInfo(i), FieldInfo).GetValue(sharedImageLists) Is sharedImageList Then
              Return aInfo(i).Name
            End If
          End If
      End Select
    Next
    Return Nothing
  End Function

#End Region ' GetSharedImageListName

#Region "     ValidateSelectedImageListName "

  ''' <summary>
  '''  Validates that the sharedImageListName exists in the inherited SharedImageLists.GetSharedImageLists instance.
  ''' </summary>
  ''' <remarks>
  ''' If the selected ImageList name does not exist then the sharedImageListName returns false
  ''' If whenNullSetToDefault and sharedImageListName is not found then SelectedImageList is set to the first name found.
  ''' </remarks>
  Private Function ValidateSelectedImageListName(ByVal sharedImageListName As String, ByVal whenNullSetToDefault As Boolean) As Boolean
    Dim names As String() = Me.GetSharedImageListNames

    If names IsNot Nothing AndAlso names.Length > 0 Then
      If Not String.IsNullOrEmpty(sharedImageListName) Then
        For i As Int32 = 0 To names.Length - 1
          If String.Compare(sharedImageListName, names(i), True) = 0 Then
            Return True
          End If
        Next
      ElseIf whenNullSetToDefault Then
        Me._selectedImageList = names(0)
        Return False
      End If
    End If

    Return False
  End Function

#End Region ' ValidateSelectedImageListName

#Region "     CreateNewTargetImageList "

  ''' <summary>
  ''' Creates a new target ImageList. This ImageList controlled by the SharedImageLists Component.
  ''' </summary>
  ''' <remarks></remarks>
  Friend Sub CreateNewTargetImageList()
    Dim h As IDesignerHost = DirectCast(GetService(GetType(IDesignerHost)), IDesignerHost)
    Dim c As IComponentChangeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)
    Dim dt As DesignerTransaction
    Dim name As String = CreateUniqueName(Me.SelectedImageListName)

    'Add a new target ImageList to the collection
    dt = h.CreateTransaction("Add Shared ImageList")

    Dim targetImageList As New ImageList

    Me.ImageLists.Add(targetImageList, Me.SelectedImageList)
    h.Container.Add(targetImageList, name)
    'Me.AddImageList(targetImageList, Me.SelectedImageList)

    'Set the default Modifiers value to Protected Friend
    Utility.SetProperty(targetImageList, "Modifiers", System.CodeDom.MemberAttributes.FamilyOrAssembly)

    'Commit the transaction
    dt.Commit()
  End Sub

#End Region ' AddSharedImageListTarget

#Region "     InitializeImageList "

  ''' <summary>
  ''' Initializes a Target ImageList with it's shared Imagelist 
  ''' </summary>
  ''' <param name="targetImageList">The ImageList that will reference the shared ImageList</param>
  ''' <param name="sharedImageList">The Shared ImageList that the target ImageList will reference</param>
  ''' <remarks></remarks>
  Friend Sub InitializeImageList(ByVal targetImageList As ImageList, ByVal sharedImageList As ImageList)

    Dim ids As IDictionaryService = Nothing
    Dim isc As IServiceContainer = TryCast(targetImageList.Site, IServiceContainer)

    If isc IsNot Nothing Then
      ids = CType(isc.GetService(GetType(IDictionaryService)), IDictionaryService)
      If ids IsNot Nothing Then
        ids.SetValue(targetImageList, sharedImageList)
        ids.SetValue("SharedImageLists", Me.SharedImageLists)
      End If

      'Remove the default SmartTags and replace them with our own Smart Tags
      'This stops develpers accidently adding/remiving images from a shared ImageList.
      Dim dcs As DesignerCommandSet = CType(isc.GetService(GetType(DesignerCommandSet)), DesignerCommandSet)
      If dcs IsNot Nothing Then
        dcs.ActionLists.Clear()
        dcs.ActionLists.Add(New SharedImageListsDesignerActionList(targetImageList))
      End If
    End If

    Me.AddImageListPaintHandler(targetImageList)

    'In the IDE we make a copy of the images in the imagelist collection
    'This is because a component cannot be sited on multiple containers at the same time in the IDE
    Utility.CopyImageList(targetImageList, sharedImageList)
  End Sub

#End Region ' InitializeImageList

#Region "     RemoveImageList "

  ''' <summary>
  ''' Removes the controlled ImageList from the SharedImageList Component
  ''' </summary>
  ''' <param name="targetImageList"></param>
  ''' <remarks>This method does not remove the ImageList from the component tray. See the RemoveImageListInternal to remove an ImageList from the component tray</remarks>
  Friend Sub RemoveImageList(ByVal targetImageList As ImageList)
    Dim c As System.ComponentModel.Design.IComponentChangeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)
    Dim ccs As IComponentChangeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)

    If ccs IsNot Nothing Then
      ccs.OnComponentChanging(Me.SharedImageLists, Nothing)
      ccs.OnComponentChanged(Me.SharedImageLists, Nothing, Me.SharedImageLists, Me.SharedImageLists)
    End If

    Me.RemoveImageListPaintHandler(targetImageList)

    Me.ImageLists.Remove(targetImageList)
  End Sub

#End Region ' RemoveImageList

#Region "     RemoveImageListInternal "

  ''' <summary>
  ''' Called to remove a controlled ImageList from the component tray.
  ''' </summary>
  ''' <param name="targetImageList"></param>
  ''' <remarks>The component is removed from our SharedImageLists from the OnComponentRemoving event handler.
  ''' This is so that that the undo/redo engine can serialize the component before we remove it.</remarks>
  Friend Sub RemoveImageListInternal(ByVal targetImageList As ImageList)
    If targetImageList.Site Is Nothing Then Return
    Dim idh As IDesignerHost = CType(targetImageList.Site.GetService(GetType(IDesignerHost)), IDesignerHost)
    If idh IsNot Nothing Then
      idh.DestroyComponent(targetImageList)
    End If
  End Sub

#End Region ' RemoveImageListInternal

#Region "     OnComponentAdded "

  Private Sub OnComponentAdded(ByVal sender As Object, ByVal e As ComponentEventArgs)
    If TypeOf e.Component Is ImageList AndAlso Me.ImageLists.ContainsKey(CType(e.Component, ImageList)) Then
      Dim targetImageList As ImageList = CType(e.Component, ImageList)
      Dim sharedImageList As ImageList = Me.ImageLists(targetImageList)
      If sharedImageList IsNot Nothing Then
        Me.InitializeImageList(targetImageList, sharedImageList)
      End If
    End If
  End Sub

#End Region ' OnComponentAdded

#Region "     OnComponentChanged "

  ''' <summary>
  ''' The OnComponentChanged evnet handler is required so that Inherited ImageLists are correctly added to the
  ''' SharedImageList Component when they are sited onto the designer. (Which is different from non inherited components.)
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub OnComponentChanged(ByVal sender As Object, ByVal e As ComponentChangedEventArgs)
    If e.Component Is Me.SharedImageLists AndAlso Not e.Member Is Nothing AndAlso e.Member.Name = "Modifiers" Then
      For Each il As ImageList In Me.ImageLists.Keys
        Utility.SetProperty(il, "Modifiers", e.NewValue)
      Next
    End If
  End Sub

#End Region ' OnComponentChanged

#Region "     OnComponentRemoving "

  ''' <summary>
  ''' This event handler used to remove the controlled ImageList components from the component tray when
  ''' they are deleted.
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub OnComponentRemoving(ByVal sender As Object, ByVal e As ComponentEventArgs)

    'If the user is removing the control via Cut or Delete then remove it from our SharedImageLists collection.
    If Utility.IsSharedImageList(Me.SharedImageLists, e.Component) Then
      Me.RemoveImageList(CType(e.Component, ImageList))
    End If

    If e.Component Is Me.SharedImageLists Then
      Dim dicSils As Dictionary(Of ImageList, ImageList) = Me.ImageLists
      If dicSils IsNot Nothing AndAlso dicSils.Count > 0 Then
        Dim keys() As ImageList = Utility.ToArray(Of ImageList)(dicSils.Keys)
        Dim h As IDesignerHost = CType(Me.GetService(GetType(IDesignerHost)), IDesignerHost)

        'remove the event handlers here so that we do not recive the 
        'ImageList removing events in here.
        Me.RemoveEventHandlers()

        For i As Int32 = keys.Length - 1 To 0 Step -1
          Me.RemoveImageListInternal(keys(i))
          keys(i) = Nothing
        Next

        dicSils = Nothing
      End If
    End If
  End Sub

#End Region ' OnComponentRemoving

#Region "     ITypeDescriptorFilterService implementation "

  ''' <summary>
  ''' Implemented to call the default ITypeDescriptorFilterService.FilterAttributes method
  ''' </summary>
  ''' <param name="component"></param>
  ''' <param name="attributes"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FilterAttributes(ByVal component As System.ComponentModel.IComponent, ByVal attributes As System.Collections.IDictionary) As Boolean Implements System.ComponentModel.Design.ITypeDescriptorFilterService.FilterAttributes
    If (Not Me._imageListFilterService Is Nothing) Then
      Me._imageListFilterService.FilterAttributes(component, attributes)
    End If
    Return False
  End Function

  ''' <summary>
  ''' Implemented to call the default ITypeDescriptorFilterService.FilterEvents method
  ''' </summary>
  ''' <param name="component"></param>
  ''' <param name="events"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FilterEvents(ByVal component As System.ComponentModel.IComponent, ByVal events As System.Collections.IDictionary) As Boolean Implements System.ComponentModel.Design.ITypeDescriptorFilterService.FilterEvents
    If (Not Me._imageListFilterService Is Nothing) Then
      Return Me._imageListFilterService.FilterEvents(component, events)
    End If
    Return True
  End Function

  ''' <summary>
  ''' Implemented to first call the default ITypeDescriptorFilterService.FilterEvents FilterProperties method.
  ''' The method then filters out the nnwanted properties from anyof our ImageList's that are controled by the
  ''' SharedImageLists component. Other properties are made read only. 
  ''' </summary>
  ''' <param name="component"></param>
  ''' <param name="properties"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FilterProperties(ByVal component As System.ComponentModel.IComponent, ByVal properties As System.Collections.IDictionary) As Boolean Implements System.ComponentModel.Design.ITypeDescriptorFilterService.FilterProperties
    Dim cache As Boolean = True
    If (Not Me._imageListFilterService Is Nothing) Then
      cache = Me._imageListFilterService.FilterProperties(component, properties)
    End If
    If TypeOf component Is ImageList Then
      If Utility.IsSharedImageList(Me.SharedImageLists, component) Then
        Dim propsCopy() As PropertyDescriptor = Utility.ToArray(Of PropertyDescriptor)(properties.Values)

        Dim pd As PropertyDescriptor
        Dim keys() As String = Utility.ToArray(Of String)(properties.Keys)
        For i As Int32 = 0 To keys.Length - 1
          pd = DirectCast(properties(keys(i)), PropertyDescriptor)
          Select Case keys(i)
            Case "Modifiers"
              properties(keys(i)) = TypeDescriptor.CreateProperty(pd.ComponentType, pd, _
                New Attribute() {ReadOnlyAttribute.Yes})
            Case "Images"
              properties(keys(i)) = TypeDescriptor.CreateProperty(pd.ComponentType, pd, _
                New Attribute() {BrowsableAttribute.No, ReadOnlyAttribute.Yes})
            Case "ImageStream"
              properties(keys(i)) = TypeDescriptor.CreateProperty(pd.ComponentType, pd, _
                New Attribute() {DesignerSerializationVisibilityAttribute.Hidden, ReadOnlyAttribute.Yes})
            Case Else
              If pd.IsBrowsable AndAlso Not pd.DesignTimeOnly Then
                properties(keys(i)) = TypeDescriptor.CreateProperty(pd.ComponentType, pd, _
                New Attribute() {DesignerSerializationVisibilityAttribute.Hidden, ReadOnlyAttribute.Yes})
              End If
          End Select
        Next
      End If
      cache = False
    Else
      cache = True
    End If
    Return cache
  End Function

#End Region ' ITypeDescriptorFilterService

#Region "     ImageList component tray paint handler implementation "

  ''' <summary>
  ''' Finds our component on the ComponentTray and adds a paint handler to the Control
  ''' so the ImageLists that are controlled by the SharedImageList component show up different
  ''' </summary>
  ''' <param name="imageList">The ImageList component to add the handler to.</param>
  ''' <remarks></remarks>
  Friend Sub AddImageListPaintHandler(ByVal imageList As ImageList)

    Dim t As ComponentTray = CType(Me.GetService(GetType(ComponentTray)), ComponentTray)
    If t Is Nothing Then Return
    Dim name As String = imageList.ToString
    For Each cc As Control In t.Controls
      If cc.ToString = "ComponentTray: " & name Then
        AddHandler cc.Paint, AddressOf OnPaint
      End If
    Next
  End Sub
  ''' <summary>
  ''' Removes our paint handler from the component tray control
  ''' </summary>
  ''' <param name="imageList">The ImageList component to remove the handler from.</param>
  ''' <remarks></remarks>
  Friend Sub RemoveImageListPaintHandler(ByVal imageList As ImageList)
    Dim t As ComponentTray = CType(Component.Site.GetService(GetType(ComponentTray)), ComponentTray)
    If t Is Nothing Then Return
    Dim name As String = imageList.ToString
    For Each cc As Control In t.Controls
      If cc.ToString = "ComponentTray: " & name Then
        AddHandler cc.Paint, AddressOf OnPaint
      End If
    Next
  End Sub

  ''' <summary>
  ''' Paints our hand image onto the component tray's control.
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e">The ImageList component to remove the handler from.</param>
  ''' <remarks></remarks>
  Private Sub OnPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    Dim stream As IO.Stream = GetType(SharedImageLists).Module.Assembly.GetManifestResourceStream(GetType(SharedImageLists), "share.png")
    If (Not stream Is Nothing) Then
      Dim bmp As New Bitmap(stream)
      e.Graphics.DrawImageUnscaled(bmp, 0, CType(sender, Control).ClientSize.Height - 16)
    End If
  End Sub

#End Region ' ImageList component tray paint handler implementation

End Class

