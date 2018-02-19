#Region "     imports "

Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.CodeDom
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.Windows.Forms.ComponentModel
Imports System.Reflection
Imports System.Globalization

#End Region '      imports 

''' <summary>
''' This class provides support for the Serializing of Shared ImageLists 
''' </summary>
''' <remarks>
''' This class is used to override the default ImageListCodeDomSerializer so that shared ImageLists
''' get serialized. Normal ImageLists are passed through to the standard Serializer
''' This class is provided to the CodeDom serialize manager via the IDesignerSerializationProvider interface
''' that is implemented in the SharedImageListDesigner class.
''' </remarks>
Friend Class SharedImageListCodeDomSerializer
  Inherits System.ComponentModel.Design.Serialization.CodeDomSerializer

  Private Shared _inDesignerMode As Int32

#Region "     InDesignerMode "

  ''' <summary>
  ''' Deturmins we the code running is running inside a designer or at runtime.
  ''' Unlike th DesignMode property of ISite this method works for inherited and nested components.
  ''' andlso works when compoents are used as Visual Studio Addins.
  ''' </summary>
  ''' <returns>returns true if we are running inside a designer</returns>
  ''' <remarks></remarks>
  Public Shared Function InDesignerMode() As Boolean
    Return _inDesignerMode = 2
  End Function

#End Region ' InDesignerMode

#Region "     InitDesignMode "

  ''' <summary>
  ''' Initailizer that caches a value for the shared DesignMode mothod above
  ''' </summary>
  ''' <param name="serviceProvider"></param>
  ''' <remarks>
  ''' Beacuse we need to detect for inherited components before they are sited, the DesignMode property is no use.
  ''' As a workaround we know that the Serializer is always called before are component is created and sited we 
  ''' can use it to signal that we are running in in designer mode.
  ''' </remarks>
  Private Sub InitDesignMode(ByVal serviceProvider As IServiceProvider)
    If Not _inDesignerMode = 0 Then Return
    Dim idh As IDesignerHost = CType(serviceProvider.GetService(GetType(IDesignerHost)), IDesignerHost)
    If idh IsNot Nothing Then
      _inDesignerMode = 2
    End If
  End Sub

#End Region ' InitDesignMode

#Region "     Deserialize "

  ''' <summary>
  ''' Deserializes CodeDom Objects to an ImageList instance.
  ''' </summary>
  ''' <param name="manager"></param>
  ''' <param name="codeObject"></param>
  ''' <returns></returns>
  ''' <remarks>
  ''' It appears that because the the ImageList control is created from inside the NewImageList method call
  ''' the CodeDomSerializer does not know how to set the name of this component
  ''' So this requires bit of extra code that walks the passed CodeDom model to
  ''' verify that we are processing a SharedImageList statement and set the ImageList component name manually
  ''' </remarks>
  Public Overrides Function Deserialize(ByVal manager As System.ComponentModel.Design.Serialization.IDesignerSerializationManager, ByVal codeObject As Object) As Object
    Dim instance As Object = Nothing
    InitDesignMode(manager)
    ' Bug fix or The variable 'LargeImageList' is either undeclared or was never assigned error.
    ' The Default deserializer appears to have an issue where it throws the above error for the following Call
    ' CType(MySharedImageList1.GetSharedImageLists),MySharedImageList1).LargeImageList
    ' The problem appears to be when the Desterilize is looking at the LargeImageList variable it's 
    ' actually thinking it's a property (which it's converted to when compiled) but in the IDE it's a field.
    ' So to fix the problem we have deserialize the expression manually.

    For Each cc As CodeObject In CType(codeObject, CodeStatementCollection)
      Dim codeAssign As CodeAssignStatement = TryCast(cc, CodeAssignStatement)
      If codeAssign Is Nothing Then Continue For

      Dim methodI As CodeMethodInvokeExpression = TryCast(codeAssign.Right, CodeMethodInvokeExpression)
      If methodI Is Nothing OrElse methodI.Parameters.Count <> 2 Then Continue For

      Dim sharedImageLists As Object = MyBase.DeserializeExpression(manager, _
          MyBase.GetTargetComponentName(Nothing, methodI.Method.TargetObject, Nothing), methodI.Method.TargetObject)
      If sharedImageLists Is Nothing Then Continue For

      If Not GetType(SharedImageLists).IsAssignableFrom(sharedImageLists.GetType) Then Continue For

      ' If we are here we can be pretty sure we have the correct statement.
      Dim name As String = Nothing
      Dim propRef As CodePropertyReferenceExpression = TryCast(methodI.Parameters(1), CodePropertyReferenceExpression)
      Dim fieldRef As CodeFieldReferenceExpression = Nothing
      Dim propInfo As PropertyInfo
      Dim fieldInfo As FieldInfo

      If propRef Is Nothing Then
        fieldRef = TryCast(methodI.Parameters(1), CodeFieldReferenceExpression)
      End If

      If propRef IsNot Nothing Then
        name = propRef.PropertyName
        ' Check that we actualy do have a property and not a field
        propInfo = sharedImageLists.GetType.GetProperty(name, BindingFlags.Instance Or BindingFlags.Public)
        If propInfo Is Nothing Then
          fieldInfo = sharedImageLists.GetType.GetField(name, BindingFlags.Instance Or BindingFlags.Public)
          If fieldInfo IsNot Nothing Then
            ' Change the property expression to a field expression
            methodI.Parameters(1) = New CodeFieldReferenceExpression(propRef.TargetObject, name)
          End If
        End If
      ElseIf fieldRef IsNot Nothing Then
        ' Check that we actualy do have a field and not a property
        name = fieldRef.FieldName
        fieldInfo = sharedImageLists.GetType.GetField(name, BindingFlags.Instance Or BindingFlags.Public)
        If fieldInfo Is Nothing Then
          propInfo = sharedImageLists.GetType.GetProperty(name, BindingFlags.Instance Or BindingFlags.Public)
          If propInfo IsNot Nothing Then
            'change the field expression to a field expression
            methodI.Parameters(1) = New CodePropertyReferenceExpression(fieldRef.TargetObject, name)
          End If
        End If
      End If

      If propRef IsNot Nothing OrElse fieldRef IsNot Nothing Then
        instance = CType(MyBase.Deserialize(manager, codeObject), ImageList)

        ' After the instance has been created we now need to make sure it's named correctly.
        If TypeOf instance Is ImageList Then
          If Not CType(instance, ImageList).Site.Name = name Then
            Dim obj As Object = manager.GetInstance(name)
            If obj Is Nothing Then
              CType(instance, ImageList).Site.Name = name
              manager.SetName(instance, name)
            End If
          End If
        End If
      End If
    Next

    If instance Is Nothing Then
      Dim ilSerializer As New ImageListCodeDomSerializer
      instance = ilSerializer.Deserialize(manager, codeObject)
    End If
    Return instance
  End Function

#End Region ' Deserialize

#Region "     Serialize "

  ''' <summary>
  ''' Serialize an ImageList to CodeDom Objects
  ''' </summary>
  ''' <param name="manager"></param>
  ''' <param name="value"></param>
  ''' <returns>If its a SharedImageList then it returns a CodeStatementCollection, otherwise it returns whatever the base serializer returns</returns>
  ''' <remarks>
  ''' If the ImageList is a SharedImageList then the BaseSerializer is called and takes care of serializing the ImageList as normal
  ''' The CodeStatementCollection returned is walked to find the ImageList's assigned CodeObjectCreateExpression. 
  ''' This CodeObjectCreateExpression is then replaced with our SharedImageList.NewImageList CodeMethodInvokeExpression to create the ImageList.
  ''' Note that Shared ImageList properties are set to be ReadOnly in the designer using a ITypeDescriptorFilterService interface,
  ''' so no properties of the Shared ImageList are actually serialized to code.
  ''' </remarks>
  Public Overrides Function Serialize(ByVal manager As IDesignerSerializationManager, ByVal value As Object) As Object
    If Utility.IsSharedImageList(CType(value, IComponent)) Then

      Dim baseSerializer As CodeDomSerializer = CType(manager.GetSerializer(GetType(ImageList).BaseType, GetType(CodeDomSerializer)), CodeDomSerializer)
      Dim obj As Object = baseSerializer.Serialize(manager, value)
      Dim statements As CodeStatementCollection = TryCast(obj, CodeStatementCollection)

      If statements IsNot Nothing Then

        'Walk the codedom statements looking for the correct ImageList CodeAssignStatement
        'that we need to change
        Dim sharedImageList As ImageList = Utility.SharedImageList(CType(value, IComponent))
        Dim sharedImageLists As SharedImageLists = Utility.SharedImageLists(CType(value, IComponent))

        If sharedImageList IsNot Nothing AndAlso sharedImageLists IsNot Nothing Then
          For Each co As CodeObject In statements
            Dim codeAssignExp As CodeAssignStatement = TryCast(co, CodeAssignStatement)
            If codeAssignExp IsNot Nothing Then
              Dim codeCreateExp As CodeObjectCreateExpression = TryCast(codeAssignExp.Right, CodeObjectCreateExpression)
              If codeCreateExp IsNot Nothing Then
                If codeCreateExp.CreateType.BaseType = "System.Windows.Forms.ImageList" Then
                  'If we are here then we have found the correct ImageList assign expression that we need to change
                  Dim codeExp As CodeExpression = SerializeImageList(manager, sharedImageLists, CType(value, ImageList), sharedImageList)
                  If codeExp IsNot Nothing Then
                    codeAssignExp.Right = codeExp
                    Return obj
                  End If
                  Exit For
                End If
              End If
            End If
          Next
        End If
      End If
    End If

    ' The ImageList statments are for a standard ImageList so use the default ImageListCodeDomSerializer
    Return (New ImageListCodeDomSerializer).Serialize(manager, value)

  End Function

#End Region ' Serialize

#Region "     SerializeImageList "

  ''' <summary>
  ''' Creates a CodeMethodInvokeExpression for creating a shared ImageList component.
  ''' </summary>
  ''' <param name="manager">The IDesignerSerializationManager provided by the Serialize override.</param>
  ''' <param name="sharedImageLists">The SharedImageLists component that manages the Shared ImageLists creation and provides designer support.</param>
  ''' <param name="targetImageList">The ImageList that will to be used locally to reference the shared ImageList</param>
  ''' <param name="sharedImageList">The ImageList is to be shared.</param>
  ''' <returns>
  ''' The expression returned is used to create a Shared ImageList object in the form
  ''' Me.ImageList1 = Me.SharedImageLists1.NewImageList(components, CType(Me.SharedImageLists1.GetSharedImageLists, MySharedImageLists).SmallImageList)
  ''' </returns>
  ''' <remarks></remarks>
  Private Function SerializeImageList(ByVal manager As IDesignerSerializationManager, ByVal sharedImageLists As SharedImageLists, ByVal targetImageList As ImageList, ByVal sharedImageList As ImageList) As CodeExpression
    Dim designer As SharedImageListsDesigner = Utility.GetDesigner(sharedImageLists)
    If designer Is Nothing Then Return Nothing

    Dim sharedImageListName As String = designer.GetSharedImageListName(sharedImageList)
    If String.IsNullOrEmpty(sharedImageListName) Then Return Nothing

    'The statement we need to construct
    'Me.SmallImageList1 = SharedImageLists1.NewImageList(components, CType(SharedImageLists1.GetSharedImageLists, MySharedImageLists).ImageList1)

    Dim containerExp As CodeExpression = MyBase.GetExpression(manager, sharedImageLists.Container) ' Components
    Dim sharedImageListsExp As CodeExpression = MyBase.GetExpression(manager, sharedImageLists)    ' SharedImageLists (may return null)
    Dim targetImageListExp As CodeExpression = MyBase.GetExpression(manager, targetImageList)      ' Target ImageList (may return null)
    Dim sharedImageListExp As New CodePropertyReferenceExpression(Nothing, sharedImageListName)    ' Shared ImageList

    If sharedImageListsExp Is Nothing Then
      sharedImageListsExp = MyBase.SerializeToExpression(manager, sharedImageLists)
    End If
    If containerExp Is Nothing Then
      containerExp = MyBase.SerializeToExpression(manager, sharedImageLists.Container)
    End If
    Dim getSharedImageListsMthd As New CodeMethodInvokeExpression(sharedImageListsExp, "GetSharedImageLists")
    Dim castTo As New CodeCastExpression(sharedImageLists.GetType, getSharedImageListsMthd)
    sharedImageListExp.TargetObject = castTo

    Return New CodeMethodInvokeExpression(sharedImageListsExp, "NewImageList", containerExp, sharedImageListExp)
  End Function

#End Region ' SerializeImageList

End Class
