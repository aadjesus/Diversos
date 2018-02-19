#Region "     imports "

Imports System.ComponentModel
Imports System.ComponentModel.Design

#End Region '      imports 

''' <summary>
''' The root designer class for the SharedImageLists
''' </summary>
''' <remarks>
''' The root designer is the designer that is used for adding the ImageList that are to be shared by the SharedImageList component.
''' By adding the ToolboxItemFilter we can restrict the component to only allow ImageList controls to be added to the design surface.
''' </remarks>
<System.ComponentModel.ToolboxItemFilter("System.Windows.Forms.ImageList", _
System.ComponentModel.ToolboxItemFilterType.Require)> _
Friend Class SharedImageListsRootDesigner
  Inherits System.Windows.Forms.Design.ComponentDocumentDesigner

  Private _oldFilterService As ImageListFilterService

#Region "     Initialize "

  Public Overrides Sub Initialize(ByVal component As System.ComponentModel.IComponent)
    MyBase.Initialize(component)
    Dim isc As IServiceContainer = CType(Me.GetService(GetType(IServiceContainer)), IServiceContainer)
    If isc IsNot Nothing Then
      _oldFilterService = New ImageListFilterService(isc)
    End If
  End Sub

#End Region ' Initialize

#Region "     Verbs "

  ''' <summary>
  ''' Adds a 'Generate ImageList Key Classes' verb.
  ''' </summary>
  ''' <value></value>
  ''' <returns>The 'Generate ImageList Key Classes' generates a single class for each ImageList with a set of constant string values that show the keys in the ImageList</returns>
  ''' <remarks></remarks>
  Public Overrides ReadOnly Property Verbs() As System.ComponentModel.Design.DesignerVerbCollection
    Get
      Dim verb As New DesignerVerb("Generate ImageList Key Classes", AddressOf GenerateKeysClasses)
      MyBase.Verbs.Add(verb)
      Return MyBase.Verbs
    End Get
  End Property

#End Region ' Verbs

#Region "     GenerateKeysClasses "

  ''' <summary>
  ''' A handler for the verb which loops through each ImageList and creates a keys class for it.
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub GenerateKeysClasses(ByVal sender As Object, ByVal e As EventArgs)
    For Each cmp As IComponent In Me.Component.Site.Container.Components
      If TypeOf cmp Is ImageList Then
        GenerateKeysClass(CType(cmp, System.Windows.Forms.ImageList))
      End If
    Next
  End Sub

#End Region ' GenerateKeysClasses

#Region "     GenerateKeysClass "

  ''' <summary>
  ''' Creates a Keys Class object for the imageList provided.
  ''' </summary>
  ''' <param name="imageList"></param>
  ''' <remarks></remarks>
  Private Sub GenerateKeysClass(ByVal imageList As ImageList)
    Dim className As String = Me.Component.Site.Name
    Dim keysClassName As String = imageList.Site.Name & "Keys"

    Dim dte As EnvDTE._DTE = CType(Me.GetService(GetType(EnvDTE._DTE)), EnvDTE._DTE)
    If dte IsNot Nothing AndAlso dte.ActiveDocument IsNot Nothing Then
      Dim pi As EnvDTE.ProjectItem = dte.ActiveDocument.ProjectItem
      If pi.ProjectItems.Count > 0 Then
        Dim fileName As String = System.IO.Path.GetFileNameWithoutExtension(pi.Name) & ".Designer"
        For Each spi As EnvDTE.ProjectItem In pi.ProjectItems
          If Not System.IO.Path.GetFileNameWithoutExtension(spi.Name) = fileName Then Continue For
          pi = spi
          Exit For
        Next
      End If
      If pi IsNot Nothing Then
        Dim closeUndoContext As Boolean

        If Not dte.UndoContext.IsOpen Then
          closeUndoContext = True
          dte.UndoContext.Open("Adding " & imageList.Site.Name & " Keys Class", False)
        End If
        dte.SuppressUI = True
        Try

          For Each ce As EnvDTE.CodeElement In pi.FileCodeModel.CodeElements
            If Not (ce.Kind = EnvDTE.vsCMElement.vsCMElementClass AndAlso ce.Name = className) Then Continue For

            Dim keysClass As EnvDTE.CodeClass = Nothing
            For Each ce2 As EnvDTE.CodeElement In ce.Children
              If Not (ce2.Kind = EnvDTE.vsCMElement.vsCMElementClass AndAlso ce2.Name = keysClassName) Then Continue For
              keysClass = CType(ce2, EnvDTE.CodeClass)
              Exit For
            Next
            Dim ccParent As EnvDTE.CodeClass = CType(ce, EnvDTE.CodeClass)
            If keysClass IsNot Nothing Then
              ccParent.RemoveMember(keysClass)
            End If

            Dim cc As EnvDTE.CodeClass = ccParent.AddClass(keysClassName, ccParent.Members.Count + 1, , , EnvDTE.vsCMAccess.vsCMAccessPublic)

            If cc IsNot Nothing Then
              Dim keys As New System.Collections.Generic.Dictionary(Of String, Object)
              For Each key As String In imageList.Images.Keys
                Dim constName As String = System.IO.Path.GetFileNameWithoutExtension(key)
                constName = System.Text.RegularExpressions.Regex.Replace(constName, "[^A-Za-z0-9]|[ ]", "")
                If key.Length = 0 Then constName = "Image"
                If keys.ContainsKey(key) Then
                  Dim c As Int32 = 1
                  Do While keys.ContainsKey(key & c)
                    c += 1
                  Loop
                  constName &= c
                End If
                keys.Add(constName, Nothing)
                Dim cv As EnvDTE.CodeVariable = cc.AddVariable(constName, EnvDTE.vsCMTypeRef.vsCMTypeRefString, Nothing, EnvDTE.vsCMAccess.vsCMAccessPublic, Nothing)
                cv.IsConstant = True
                cv.InitExpression = """" & key & """"
              Next
            End If
            Exit For
          Next

        Finally
          If closeUndoContext Then
            dte.UndoContext.Close()
          End If
          dte.SuppressUI = False
        End Try

      End If
    End If
  End Sub

#End Region ' GenerateKeysClass

#Region "     PostFilterProperties "

  ''' <summary>
  ''' Prevents inherited ImageLists from showing in the properties window
  ''' </summary>
  ''' <param name="properties"></param>
  ''' <remarks></remarks>
  Protected Overrides Sub PostFilterProperties(ByVal properties As System.Collections.IDictionary)
    MyBase.PostFilterProperties(properties)
    If Component Is Me.Component Then
      Dim keys(properties.Keys.Count - 1) As String
      properties.Keys.CopyTo(keys, 0)
      For i As Int32 = 0 To keys.Length - 1
        Dim pd As PropertyDescriptor = DirectCast(properties(keys(i)), PropertyDescriptor)
        If pd.PropertyType.Equals(GetType(ImageList)) Then
          'Hide the imageLists from the IDE
          'Note do not remove properties from the collection
          'as they are used everywhere including invoking properties when deserializing.
          properties.Item(keys(i)) = TypeDescriptor.CreateProperty(GetType(SharedImageLists), pd, _
          New Attribute() {New DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden), New BrowsableAttribute(False)})
        End If
      Next
    End If
  End Sub

#End Region ' PostFilterProperties

#Region "     ImageListFilterService (class) "

  Private Class ImageListFilterService
    Implements ITypeDescriptorFilterService

    Private _imageListFilterService As ITypeDescriptorFilterService

#Region "     constructors "

    ''' <summary>
    ''' Create an instance of the class and adds the ITypeDescriptorFilterService to the serviceContainer provided.
    ''' </summary>
    ''' <param name="serviceContainer"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal serviceContainer As IServiceContainer)
      If (Not serviceContainer Is Nothing) Then
        Me._imageListFilterService = CType(serviceContainer.GetService(GetType(ITypeDescriptorFilterService)), ITypeDescriptorFilterService)
        If (Me._imageListFilterService IsNot Nothing) Then
          serviceContainer.RemoveService(GetType(ITypeDescriptorFilterService))
        End If
        serviceContainer.AddService(GetType(ITypeDescriptorFilterService), Me)
      End If
    End Sub

#End Region ' constructors

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
      If TypeOf component Is ImageList Then
        If attributes.Contains(GetType(InheritanceAttribute)) Then
          attributes(GetType(InheritanceAttribute)) = InheritanceAttribute.InheritedReadOnly
        End If
      End If
      Return True
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
    ''' The method then filters out the unwanted properties from any of our ImageList's that are controlled by the
    ''' SharedImageLists component. Other properties are made read only. 
    ''' </summary>
    ''' <param name="component"></param>
    ''' <param name="properties"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FilterProperties(ByVal component As System.ComponentModel.IComponent, ByVal properties As System.Collections.IDictionary) As Boolean Implements System.ComponentModel.Design.ITypeDescriptorFilterService.FilterProperties
      Dim cache As Boolean = False
      If (Not Me._imageListFilterService Is Nothing) Then
        cache = Me._imageListFilterService.FilterProperties(component, properties)
      End If
      Return cache
    End Function

#End Region ' ITypeDescriptorFilterService

  End Class

#End Region ' ImageListFilterService (class)

End Class
