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

#End Region '      imports 

''' <summary>
''' A CodeDomSerializer for the SharedImageLists class
''' </summary>
''' <remarks>
''' This class exists solely for the purpose running the code that's in the classes constructors
''' in the constructor we have to replace the default ImageList Serializer (ImageListCodeDomSerializer) with
''' our own. It was not possible to do this any other way, i.e. using IDesignerSerializationProvider.
''' </remarks>
Friend Class SharedImageListsCodeDomSerializer
  Inherits System.ComponentModel.Design.Serialization.CodeDomSerializer

#Region "     constructors "

  Public Sub New()
    'This is the only way to change the DesignerSerializer for the ImageList.
    'Adding SerializationProviders does not work as they get loaded too late to be of any use.
    TypeDescriptor.AddAttributes(GetType(ImageList), New _
       Serialization.DesignerSerializerAttribute(GetType(SharedImageListCodeDomSerializer), _
       GetType(Serialization.CodeDomSerializer)))
  End Sub

#End Region ' constructors

#Region "     Deserialize "

  ''' <summary>
  ''' Deserializes the codeObject's CodeCom expressions back into a SharedImageList instance.
  ''' </summary>
  ''' <param name="manager"></param>
  ''' <param name="codeObject"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function Deserialize(ByVal manager As System.ComponentModel.Design.Serialization.IDesignerSerializationManager, ByVal codeObject As Object) As Object
    Dim baseSerializer As CodeDomSerializer = CType(manager.GetSerializer(GetType(Component), GetType(CodeDomSerializer)), CodeDomSerializer)
    If baseSerializer IsNot Nothing Then
      Return baseSerializer.Deserialize(manager, codeObject)
    End If
    Return Nothing
  End Function

#End Region ' Deserialize

#Region "     Serialize "

  ''' <summary>
  ''' Serializes the SharedImageList value to a series of CodeCom expressions.  
  ''' </summary>
  ''' <param name="manager"></param>
  ''' <param name="value"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function Serialize(ByVal manager As System.ComponentModel.Design.Serialization.IDesignerSerializationManager, ByVal value As Object) As Object
    Dim baseSerializer As CodeDomSerializer = CType(manager.GetSerializer(GetType(Component), GetType(CodeDomSerializer)), CodeDomSerializer)
    If baseSerializer IsNot Nothing Then
      Return baseSerializer.Serialize(manager, value)
    End If
    Return Nothing
  End Function

#End Region ' Serialize

End Class