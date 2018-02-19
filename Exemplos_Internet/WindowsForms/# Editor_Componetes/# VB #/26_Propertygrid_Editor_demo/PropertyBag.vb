'*******************************************************************
' *
' * PropertyBag.vb
' * --------------
' * Copyright (C) 2003 Tony Allowatt / Loic Barbou
' * THE SOFTWARE IS PROVIDED BY THE AUTHOR "AS IS", WITHOUT WARRANTY
' * OF ANY KIND, EXPRESS OR IMPLIED. IN NO EVENT SHALL THE AUTHOR BE
' * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY ARISING FROM,
' * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OF THIS
' * SOFTWARE.
' *
' * This file has been translated to VB.NET by an automatic 
' * translator by somebody who replyed on the original article.
' * I edited this translated version a little bit more, but I don't 
' * take any copyright on this, as it's totally Tony's work
' *******************************************************************

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing.Design

Public Class PropertySpec
#Region " Private Vars "
    Private m_attributes() As Attribute
    Private m_category As String
    Private m_defaultValue As Object
    Private m_description As String
    Private editor As String
    Private m_name As String
    Private type As String
    Private typeConverter As String
#End Region
#Region " Constructors "
    Public Sub New(ByVal name As String, ByVal type As String)
        MyClass.New(name, type, Nothing, Nothing, Nothing)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As Type)
        MyClass.New(name, type.AssemblyQualifiedName, Nothing, Nothing, Nothing)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As String, ByVal category As String)
        MyClass.New(name, type, category, Nothing, Nothing)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As Type, ByVal category As String)
        MyClass.New(name, type.AssemblyQualifiedName, category, Nothing, Nothing)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As String, ByVal category As String, ByVal description As String)
        MyClass.New(name, type, category, description, Nothing)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As Type, ByVal category As String, ByVal description As String)
        MyClass.New(name, type.AssemblyQualifiedName, category, description, Nothing)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As String, ByVal category As String, ByVal description As String, ByVal defaultValue As Object)
        Me.m_name = name
        Me.type = type
        Me.m_category = category
        Me.m_description = description
        Me.m_defaultValue = defaultValue
        Me.m_attributes = Nothing
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As Type, ByVal category As String, ByVal description As String, ByVal defaultValue As Object)
        MyClass.New(name, type.AssemblyQualifiedName, category, description, defaultValue)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As String, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As String, ByVal typeConverter As String)
        MyClass.New(name, type, category, description, defaultValue)
        Me.editor = editor
        Me.typeConverter = typeConverter
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As Type, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As String, ByVal typeConverter As String)
        MyClass.New(name, type.AssemblyQualifiedName, category, description, defaultValue, editor, typeConverter)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As String, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As Type, ByVal typeConverter As String)
        MyClass.New(name, type, category, description, defaultValue, editor.AssemblyQualifiedName, typeConverter)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As Type, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As Type, ByVal typeConverter As String)
        MyClass.New(name, type.AssemblyQualifiedName, category, description, defaultValue, editor.AssemblyQualifiedName, typeConverter)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As String, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As String, ByVal typeConverter As Type)
        MyClass.New(name, type, category, description, defaultValue, editor, typeConverter.AssemblyQualifiedName)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As Type, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As String, ByVal typeConverter As Type)
        MyClass.New(name, type.AssemblyQualifiedName, category, description, defaultValue, editor, typeConverter.AssemblyQualifiedName)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As String, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As Type, ByVal typeConverter As Type)
        MyClass.New(name, type, category, description, defaultValue, editor.AssemblyQualifiedName, typeConverter.AssemblyQualifiedName)
    End Sub 'New
    Public Sub New(ByVal name As String, ByVal type As Type, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As Type, ByVal typeConverter As Type)
        MyClass.New(name, type.AssemblyQualifiedName, category, description, defaultValue, editor.AssemblyQualifiedName, typeConverter.AssemblyQualifiedName)
    End Sub 'New
#End Region
#Region " Puplic Properties"
    Public Property Attributes() As Attribute()
        Get
            Return m_attributes
        End Get
        Set(ByVal Value As Attribute())
            m_attributes = Value
        End Set
    End Property
    Public Property Category() As String
        Get
            Return m_category
        End Get
        Set(ByVal Value As String)
            m_category = Value
        End Set
    End Property
    Public Property ConverterTypeName() As String
        Get
            Return typeConverter
        End Get
        Set(ByVal Value As String)
            typeConverter = Value
        End Set
    End Property
    Public Property DefaultValue() As Object
        Get
            Return m_defaultValue
        End Get
        Set(ByVal Value As Object)
            m_defaultValue = Value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return m_description
        End Get
        Set(ByVal Value As String)
            m_description = Value
        End Set
    End Property
    Public Property EditorTypeName() As String
        Get
            Return editor
        End Get
        Set(ByVal Value As String)
            editor = Value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return m_name
        End Get
        Set(ByVal Value As String)
            m_name = Value
        End Set
    End Property
    Public Property TypeName() As String
        Get
            Return type
        End Get
        Set(ByVal Value As String)
            type = Value
        End Set
    End Property
#End Region
End Class 'PropertySpec
Public Class PropertySpecEventArgs
    Inherits EventArgs
    Private m_property As PropertySpec
    Private val As Object
    Public Sub New(ByVal [property] As PropertySpec, ByVal val As Object)
        Me.m_property = [property]
        Me.val = val
    End Sub 'New
    Public ReadOnly Property [Property]() As PropertySpec
        Get
            Return m_property
        End Get
    End Property
    Public Property Value() As Object
        Get
            Return val
        End Get
        Set(ByVal Value As Object)
            val = Value
        End Set
    End Property
End Class 'PropertySpecEventArgs

Public Delegate Sub PropertySpecEventHandler(ByVal sender As Object, ByVal e As PropertySpecEventArgs)

Public Class PropertyBag
    Implements ICustomTypeDescriptor 'ToDo: Add Implements Clauses for implementation methods of these interface(s)

    <Serializable()> _
    Public Class PropertySpecCollection
        Implements IList 'ToDo: Add Implements Clauses for implementation methods of these interface(s)
        Private innerArray As ArrayList

        Public Sub New()
            innerArray = New ArrayList
        End Sub 'New
        Public ReadOnly Property Count() As Integer Implements IList.count
            Get
                Return innerArray.Count
            End Get
        End Property
        Public ReadOnly Property IsFixedSize() As Boolean Implements IList.isfixedsize
            Get
                Return False
            End Get
        End Property
        Public ReadOnly Property IsReadOnly() As Boolean Implements IList.isreadonly
            Get
                Return False
            End Get
        End Property
        Public ReadOnly Property IsSynchronized() As Boolean Implements IList.IsSynchronized
            Get
                Return False
            End Get
        End Property
        ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
            Get
                Return Nothing
            End Get
        End Property
        Default Public Property Item(ByVal index As Integer) As Object Implements IList.Item
            Get
                Return CType(innerArray(index), PropertySpec)
            End Get
            Set(ByVal Value As Object)
                innerArray(index) = Value
            End Set
        End Property
        Public Function Add(ByVal value As PropertySpec) As Integer
            Dim index As Integer = innerArray.Add(value)

            Return index
        End Function 'Add
        Public Sub AddRange(ByVal array() As PropertySpec)
            innerArray.AddRange(array)
        End Sub 'AddRange
        Public Sub Clear() Implements IList.clear
            innerArray.Clear()
        End Sub 'Clear
        Public Overloads Function Contains(ByVal item As PropertySpec) As Boolean
            Return innerArray.Contains(item)
        End Function 'Contains
        Public Overloads Function Contains(ByVal name As String) As Boolean
            Dim spec As PropertySpec
            For Each spec In innerArray
                If spec.Name = name Then
                    Return True
                End If
            Next spec
            Return False
        End Function 'Contains

        Public Overloads Sub CopyTo(ByVal array() As PropertySpec)
            innerArray.CopyTo(array)
        End Sub 'CopyTo
        Public Overloads Sub CopyTo(ByVal array() As PropertySpec, ByVal index As Integer)
            innerArray.CopyTo(array, index)
        End Sub 'CopyTo
        Public Function GetEnumerator() As IEnumerator Implements IList.getenumerator
            Return innerArray.GetEnumerator()
        End Function 'GetEnumerator
        Public Overloads Function IndexOf(ByVal value As PropertySpec) As Integer
            Return innerArray.IndexOf(value)
        End Function 'IndexOf
        Public Overloads Function IndexOf(ByVal name As String) As Integer
            Dim i As Integer = 0

            Dim spec As PropertySpec
            For Each spec In innerArray
                If spec.Name = name Then
                    Return i
                End If
                i += 1
            Next spec

            Return -1
        End Function 'IndexOf
        Public Sub Insert(ByVal index As Integer, ByVal value As PropertySpec)
            innerArray.Insert(index, value)
        End Sub 'Insert
        Public Overloads Sub Remove(ByVal obj As PropertySpec)
            innerArray.Remove(obj)
        End Sub 'Remove
        Public Overloads Sub Remove(ByVal name As String)
            Dim index As Integer = IndexOf(name)
            RemoveAt(index)
        End Sub 'Remove
        Public Sub RemoveAt(ByVal index As Integer) Implements IList.removeat
            innerArray.RemoveAt(index)
        End Sub 'RemoveAt
        Public Function ToArray() As PropertySpec()
            Return CType(innerArray.ToArray(GetType(PropertySpec)), PropertySpec())
        End Function 'ToArray
        Overloads Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
            CopyTo(CType(array, PropertySpec()), index)
        End Sub 'ICollection.CopyTo
        Function Add(ByVal value As Object) As Integer Implements IList.add
            Return Add(CType(value, PropertySpec))
        End Function 'IList.Add
        Overloads Function Contains(ByVal obj As Object) As Boolean Implements IList.contains
            Return Contains(CType(obj, PropertySpec))
        End Function 'IList.Contains
        Property IList(ByVal index As Integer) As Object
            Get
                Return CType(Me, PropertySpecCollection)(index)
            End Get
            Set(ByVal Value As Object)
                CType(Me, PropertySpecCollection)(index) = CType(Value, PropertySpec)
            End Set
        End Property
        Overloads Function IndexOf(ByVal obj As Object) As Integer Implements IList.IndexOf
            Return IndexOf(CType(obj, PropertySpec))
        End Function 'IList.IndexOf
        Sub Insert(ByVal index As Integer, ByVal value As Object) Implements IList.insert
            Insert(index, CType(value, PropertySpec))
        End Sub 'IList.Insert
        Overloads Sub Remove(ByVal value As Object) Implements IList.Remove
            Remove(CType(value, PropertySpec))

        End Sub 'IList.Remove
    End Class 'PropertySpecCollection

    Private Class PropertySpecDescriptor
        Inherits PropertyDescriptor

        Private bag As PropertyBag
        Private item As PropertySpec


        Public Sub New(ByVal item As PropertySpec, ByVal bag As PropertyBag, ByVal name As String, ByVal attrs() As Attribute)
            MyBase.New(name, attrs)
            Me.bag = bag
            Me.item = item
        End Sub 'New


        Public Overrides ReadOnly Property ComponentType() As Type
            Get
                Return item.GetType()
            End Get
        End Property

        Public Overrides ReadOnly Property IsReadOnly() As Boolean
            Get
                Return Attributes.Matches(ReadOnlyAttribute.Yes)
            End Get
        End Property

        Public Overrides ReadOnly Property PropertyType() As Type
            Get
                Return Type.GetType(item.TypeName)
            End Get
        End Property

        Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
            If item.DefaultValue Is Nothing Then
                Return False
            Else
                Return Not Me.GetValue(component).Equals(item.DefaultValue)
            End If
        End Function 'CanResetValue

        Public Overrides Function GetValue(ByVal component As Object) As Object
            ' Have the property bag raise an event to get the current value
            ' of the property.
            Dim e As New PropertySpecEventArgs(item, Nothing)
            bag.OnGetValue(e)
            Return e.Value
        End Function 'GetValue


        Public Overrides Sub ResetValue(ByVal component As Object)
            SetValue(component, item.DefaultValue)
        End Sub 'ResetValue


        Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
            ' Have the property bag raise an event to set the current value
            ' of the property.
            Dim e As New PropertySpecEventArgs(item, value)
            bag.OnSetValue(e)
        End Sub 'SetValue


        Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Dim val As Object = Me.GetValue(component)

            If item.DefaultValue Is Nothing And val Is Nothing Then
                Return False
            Else
                Return Not val.Equals(item.DefaultValue)
            End If
        End Function 'ShouldSerializeValue
    End Class 'PropertySpecDescriptor 

    Private m_defaultProperty As String
    Private m_properties As PropertySpecCollection

    Public Sub New()
        m_defaultProperty = Nothing
        m_properties = New PropertySpecCollection
    End Sub 'New
    Public Property DefaultProperty() As String
        Get
            Return m_defaultProperty
        End Get
        Set(ByVal Value As String)
            m_defaultProperty = Value
        End Set
    End Property
    Public ReadOnly Property Properties() As PropertySpecCollection
        Get
            Return m_properties
        End Get
    End Property

    Public Event GetValue As PropertySpecEventHandler
    Public Event SetValue As PropertySpecEventHandler

    Protected Overridable Sub OnGetValue(ByVal e As PropertySpecEventArgs)
        RaiseEvent GetValue(Me, e)
    End Sub 'OnGetValue
    Protected Overridable Sub OnSetValue(ByVal e As PropertySpecEventArgs)
        RaiseEvent SetValue(Me, e)
    End Sub 'OnSetValue
    Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
        Return TypeDescriptor.GetAttributes(Me, True)
    End Function 'ICustomTypeDescriptor.GetAttributes
    Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
        Return TypeDescriptor.GetClassName(Me, True)
    End Function 'ICustomTypeDescriptor.GetClassName
    Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
        Return TypeDescriptor.GetComponentName(Me, True)
    End Function 'ICustomTypeDescriptor.GetComponentName
    Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
        Return TypeDescriptor.GetConverter(Me, True)
    End Function 'ICustomTypeDescriptor.GetConverter
    Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
        Return TypeDescriptor.GetDefaultEvent(Me, True)
    End Function 'ICustomTypeDescriptor.GetDefaultEvent
    Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
        ' This function searches the property list for the property
        ' with the same name as the DefaultProperty specified, and
        ' returns a property descriptor for it. If no property is
        ' found that matches DefaultProperty, a null reference is
        ' returned instead.
        Dim propertySpec As PropertySpec = Nothing
        If Not (DefaultProperty Is Nothing) Then
            Dim index As Integer = Properties.IndexOf(DefaultProperty)
            propertySpec = Properties(index)
        End If

        If Not (propertySpec Is Nothing) Then
            Return New PropertySpecDescriptor(propertySpec, Me, propertySpec.Name, Nothing)
        Else
            Return Nothing
        End If
    End Function 'ICustomTypeDescriptor.GetDefaultProperty
    Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
        Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
    End Function 'ICustomTypeDescriptor.GetEditor
    Overloads Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(Me, True)
    End Function 'ICustomTypeDescriptor.GetEvents
    Overloads Function GetEvents(ByVal attributes() As Attribute) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(Me, attributes, True)
    End Function 'ICustomTypeDescriptor.GetEvents
    Overloads Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Return CType(Me, ICustomTypeDescriptor).GetProperties(New Attribute(0) {})
    End Function 'ICustomTypeDescriptor.GetProperties
    Overloads Function GetProperties(ByVal attributes() As Attribute) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        ' Rather than passing this function on to the default TypeDescriptor,
        ' which would return the actual properties of PropertyBag, I construct
        ' a list here that contains property descriptors for the elements of the
        ' Properties list in the bag.
        Dim props As New ArrayList

        Dim mproperty As PropertySpec
        For Each mproperty In Properties
            Dim attrs As New ArrayList

            ' If a category, description, editor, or type converter are specified
            ' in the PropertySpec, create attributes to define that relationship.
            If Not (mproperty.Category Is Nothing) Then
                attrs.Add(New CategoryAttribute(mproperty.Category))
            End If
            If Not (mproperty.Description Is Nothing) Then
                attrs.Add(New DescriptionAttribute(mproperty.Description))
            End If
            If Not (mproperty.EditorTypeName Is Nothing) Then
                attrs.Add(New EditorAttribute(mproperty.EditorTypeName, GetType(UITypeEditor)))
            End If
            If Not (mproperty.ConverterTypeName Is Nothing) Then
                attrs.Add(New TypeConverterAttribute(mproperty.ConverterTypeName))
            End If
            ' Additionally, append the custom attributes associated with the
            ' PropertySpec, if any.
            If Not (mproperty.Attributes Is Nothing) Then
                attrs.AddRange(mproperty.Attributes)
            End If
            Dim attrArray As Attribute() = CType(attrs.ToArray(GetType(Attribute)), Attribute())

            ' Create a new property descriptor for the property item, and add
            ' it to the list.
            Dim pd As New PropertySpecDescriptor(mproperty, Me, mproperty.Name, attrArray)
            props.Add(pd)
        Next mproperty

        ' Convert the list of PropertyDescriptors to a collection that the
        ' ICustomTypeDescriptor can use, and return it.
        Dim propArray As PropertyDescriptor() = CType(props.ToArray(GetType(PropertyDescriptor)), PropertyDescriptor())
        Return New PropertyDescriptorCollection(propArray)
    End Function 'ICustomTypeDescriptor.GetProperties
    Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
        Return Me
    End Function 'ICustomTypeDescriptor.GetPropertyOwner
End Class 'PropertyBag

Public Class PropertyTable
    Inherits PropertyBag
    Private propValues As Hashtable
    Public Sub New()
        propValues = New Hashtable
    End Sub 'New
    Default Public Property Item(ByVal key As String) As Object
        Get
            Return propValues(key)
        End Get
        Set(ByVal Value As Object)
            propValues(key) = Value
        End Set
    End Property
    Protected Overrides Sub OnGetValue(ByVal e As PropertySpecEventArgs)
        e.Value = propValues(e.Property.Name)
        MyBase.OnGetValue(e)
    End Sub 'OnGetValue
    Protected Overrides Sub OnSetValue(ByVal e As PropertySpecEventArgs)
        propValues(e.Property.Name) = e.Value
        MyBase.OnSetValue(e)
    End Sub 'OnSetValue
End Class 'PropertyTable