Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Collections
Imports System.Collections.Generic

Friend Class Utility

#Region "     CopyImageList "

  Public Shared Sub CopyImageList(ByVal target As ImageList, ByVal source As ImageList)
    If Not source.HandleCreated Then
      target.ImageStream = Nothing
      target.ColorDepth = source.ColorDepth
      target.ImageSize = source.ImageSize
      target.TransparentColor = source.TransparentColor
      Return
    End If
    If source.ImageStream Is Nothing Then Return
    Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
    Dim stream As New IO.MemoryStream
    bf.Serialize(stream, source.ImageStream)
    stream.Position = 0
    Dim ils As ImageListStreamer = CType(bf.Deserialize(stream), ImageListStreamer)
    stream.Dispose()
    target.ImageStream = Nothing
    target.ImageStream = ils
  End Sub

#End Region ' CopyImageList

#Region "     SetProperty "

  Shared Sub SetProperty(ByVal component As Component, ByVal propertyName As String, ByVal value As Object)
    ' Get property
    Dim prop As PropertyDescriptor = TypeDescriptor.GetProperties(component)(propertyName)
    If prop Is Nothing Then Return
    ' Set property value
    prop.SetValue(component, value)
  End Sub

#End Region ' SetProperty

#Region "     ToArray "

  Shared Function ToArray(Of T)(ByVal col As ICollection) As T()
    If col.Count = 0 Then Return Nothing
    Dim arr(col.Count - 1) As T
    col.CopyTo(arr, 0)
    Return arr
  End Function

#End Region ' ToArray

#Region "     CodeStatementToString "

  Public Shared Function CodeStatementToString(ByVal codeObject As System.CodeDom.CodeObject) As String
    Dim codeProvider As New Microsoft.VisualBasic.VBCodeProvider
    Dim sb As New System.Text.StringBuilder
    Dim wr As New IO.StringWriter(sb)
    Dim co As New CodeDom.Compiler.CodeGeneratorOptions

    GenerateCodeFromStatement(codeObject, codeProvider, wr, co)

    Return sb.ToString
  End Function

#End Region ' CodeStatementToString

#Region "     CodeStatementsToString "

  Public Shared Function CodeStatementsToString(ByVal statements As System.CodeDom.CodeStatementCollection) As String
    Dim codeProvider As New Microsoft.VisualBasic.VBCodeProvider
    Dim sb As New System.Text.StringBuilder
    Dim wr As New IO.StringWriter(sb)
    Dim co As New CodeDom.Compiler.CodeGeneratorOptions
    For Each statement As CodeDom.CodeObject In statements
      GenerateCodeFromStatement(statement, codeProvider, wr, co)
    Next
    Return sb.ToString
  End Function

#End Region ' CodeStatementsToString

#Region "     GenerateCodeFromStatement "

  Private Shared Sub GenerateCodeFromStatement(ByVal codeObject As System.CodeDom.CodeObject, ByVal codeProvider As Microsoft.VisualBasic.VBCodeProvider, ByVal writer As IO.StringWriter, ByVal codeObtions As CodeDom.Compiler.CodeGeneratorOptions)

    If TypeOf codeObject Is CodeDom.CodeStatement Then
      codeProvider.GenerateCodeFromStatement(CType(codeObject, CodeDom.CodeStatement), writer, codeObtions)
    ElseIf TypeOf codeObject Is CodeDom.CodeExpression Then
      codeProvider.GenerateCodeFromExpression(CType(codeObject, CodeDom.CodeExpression), writer, codeObtions)
    ElseIf TypeOf codeObject Is CodeDom.CodeTypeMember Then
      codeProvider.GenerateCodeFromMember(CType(codeObject, CodeDom.CodeTypeMember), writer, codeObtions)
    ElseIf TypeOf codeObject Is CodeDom.CodeNamespace Then
      codeProvider.GenerateCodeFromNamespace(CType(codeObject, CodeDom.CodeNamespace), writer, codeObtions)
    ElseIf TypeOf codeObject Is CodeDom.CodeTypeDeclaration Then
      codeProvider.GenerateCodeFromType(CType(codeObject, CodeDom.CodeTypeDeclaration), writer, codeObtions)
    ElseIf TypeOf codeObject Is CodeDom.CodeTypeDeclaration Then
      codeProvider.GenerateCodeFromType(CType(codeObject, CodeDom.CodeTypeDeclaration), writer, codeObtions)
    End If

  End Sub

#End Region ' GenerateCodeFromStatement

#Region "     GetSharedImageListValue "

  Private Shared Function GetSharedImageListValue(Of T)(ByVal targetImageList As IComponent, ByVal key As Object) As T
    If targetImageList.Site Is Nothing Then Return Nothing
    If Not TypeOf targetImageList Is ImageList Then Return Nothing
    Dim ids As IDictionaryService = CType(targetImageList.Site.GetService(GetType(IDictionaryService)), IDictionaryService)
    If ids Is Nothing Then Return Nothing
    Return CType(ids.GetValue(key), T)
  End Function

#End Region ' GetSharedImageListValue

#Region "     SharedImageLists "

  Public Shared Function SharedImageLists(ByVal targetImageList As IComponent) As SharedImageLists
    Return GetSharedImageListValue(Of SharedImageLists)(targetImageList, "SharedImageLists")
  End Function

#End Region ' SharedImageLists

#Region "     SharedImageList "

  Public Shared Function SharedImageList(ByVal targetImageList As IComponent) As ImageList
    Return GetSharedImageListValue(Of ImageList)(targetImageList, targetImageList)
  End Function

#End Region ' SharedImageList

#Region "     IsSharedImageList "

  Public Shared Function IsSharedImageList(ByVal sharedImageLists As SharedImageLists, ByVal component As IComponent) As Boolean
    If sharedImageLists Is Nothing Then Return False
    If component Is Nothing Then Throw New ArgumentNullException("component")
    If Not TypeOf component Is ImageList Then Return False
    If IsSharedImageList(component) Then
      Dim des As SharedImageListsDesigner = GetDesigner(sharedImageLists)
      If des IsNot Nothing Then
        Return des.ImageLists.ContainsKey(CType(component, ImageList))
      End If
    End If
    Return GetSharedImageListValue(Of SharedImageLists)(component, "SharedImageLists") Is sharedImageLists
  End Function
 
#End Region ' IsSharedImageList

#Region "     IsSharedImageList "

  Public Shared Function IsSharedImageList(ByVal targetImageList As IComponent) As Boolean
    If Not TypeOf targetImageList Is ImageList Then Return False
    Return GetSharedImageListValue(Of ImageList)(targetImageList, targetImageList) IsNot Nothing
  End Function

#End Region ' IsSharedImageList

#Region "     GetDesigner "

  ''' <summary>
  ''' Gets the SharedImageListsDesigner for the SharedImageList component.
  ''' </summary>
  ''' <param name="component"></param>
  ''' <returns>SharedImageListsDesigner or null if no designer was found.</returns>
  ''' <remarks></remarks>
  Public Shared Function GetDesigner(ByVal component As SharedImageLists) As SharedImageListsDesigner
    If component.Site Is Nothing Then Return Nothing
    Dim idh As IDesignerHost = CType(component.Site.GetService(GetType(IDesignerHost)), IDesignerHost)
    If idh IsNot Nothing Then
      Return TryCast(idh.GetDesigner(component), SharedImageListsDesigner)
    End If
    Return Nothing
  End Function

#End Region ' GetDesigner

End Class
