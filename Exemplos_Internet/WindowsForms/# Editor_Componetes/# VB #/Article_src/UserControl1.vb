Imports System.ComponentModel
Imports System.ComponentModel.Design ' Custom Designers Require this NameSpace
Imports System.ComponentModel.Design.Serialization ' Instance Descriptors
Imports System.Globalization  ' This name space is required for 
Imports System.Drawing.Design ' Designers Are in this NameSpace, You Also need to a reference to System.Design.Dll


<DesignerAttribute(GetType(UserControl1Designer))> Public Class UserControl1
    Inherits System.Windows.Forms.Control

#Region " Declarations"
    ' This is an example Control Providing a collection with Different Objects in a collection
    Private WithEvents m_Buttons As New Buttons
    Event SomeEvent()
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.BackColor = Color.Red
    End Sub

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

#Region " Properties"
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Editor(GetType(ButtonCollectionEditor), GetType(UITypeEditor))> Public ReadOnly Property Buttons() As Buttons
        Get
            Return m_Buttons
        End Get
    End Property
    Private m_ImageList As ImageList
    Public Property ImageList() As ImageList
        Get
            Return m_ImageList
        End Get
        Set(ByVal Value As ImageList)
            m_ImageList = Value
            mm_ImageList = Value
        End Set
    End Property
#End Region

End Class

#Region " Classes"

#Region " ButtonBase"

' Base Class For Button Object
' We Inherit from Component, DesignTimeVisible(False) Attribute prevents your objects created by your component or Control to appear in the component tray of the designer.
<DesignTimeVisible(False)> _
Public Class ButtonBase
    Inherits System.ComponentModel.Component

#Region " Constructors"

    Public Sub New()
    End Sub

    'Overloaded Constructor
    Public Sub New(ByVal Type As ButtonTypes, ByVal Width As Integer)
        m_ButtonType = Type
        m_Width = Width
    End Sub

#End Region

#Region " Declarations"
    ' We need the following Enum to differentiate between different objects
    Public Enum ButtonTypes
        PushButton = 0
        GroupButton = 1
        ToggleButton = 2
        PlaceHolder = 3
        Seperator = 4
    End Enum
    'Memory Variables For Properties
    Private m_ButtonType As ButtonTypes
    Private m_ImageIndex As Integer = -1
    Private m_Value As Boolean
    Private m_Collection As Buttons
    Private m_Width As Integer
#End Region

#Region " Properties"
    'This property is used to give a reference to the collection in which the object is.
    'By means of this object variable we can raise an event through the collection when one of the properties is changed
    Public WriteOnly Property Collection() As Buttons
        Set(ByVal Value As Buttons)
            m_Collection = Value
        End Set
    End Property
    'A PropertyChanged Method will be called in property sets
    Private Sub PropertyChanged()
        'Check if the collection is a valid object if not during design time you and up with a message 'Object is not set to an Instance' But Your Program Works
        'By the way I am looking forward for the IsNot operator in VS 2005, because every time I forget the Not Operator and need to navigate back
        If Not m_Collection Is Nothing Then
            m_Collection.RaisePropertyChangedEvent()
        End If
    End Sub
    'We give a browsable(False) attribute to Button type, because this property will be set in inherited objects Constructor( Sub New )
    <Browsable(False)> Public Property ButtonType() As ButtonTypes
        Get
            Return m_ButtonType
        End Get
        Set(ByVal Value As ButtonTypes)
            m_ButtonType = Value
        End Set
    End Property
    <DefaultValue(-1), TypeConverter(GetType(EImageIndexConverter)), _
    Editor(GetType(EImageIndexEditor), GetType(UITypeEditor))> _
    Public Property ImageIndex() As Integer
        Get
            Return m_ImageIndex
        End Get
        Set(ByVal Value As Integer)
            m_ImageIndex = Value
            PropertyChanged()
        End Set
    End Property
    Public Property Value() As Boolean
        Get
            Return m_Value
        End Get
        Set(ByVal Value As Boolean)
            m_Value = Value
        End Set
    End Property
    Public ReadOnly Property Width() As Integer
        Get
            Return m_Width
        End Get
    End Property
#End Region
End Class

#End Region

#Region " PushButton"

'We Make PushButton Serializable and assign a type converter to control the sserialization of the object. For Code Clearity it is advisable to have the Converter as a nested class 

<Serializable(), TypeConverter(GetType(PushButton.PushButtonConverter))> _
Public Class PushButton
    Inherits ButtonBase

    Event Click As EventHandler
    'Constructors
    Public Sub New()
        MyBase.New()
        Me.ButtonType = ButtonBase.ButtonTypes.PushButton
    End Sub
    'We Dont Want a Push Button to Expose a Value Property so we shadow it with browsable(False) atributr
    <Browsable(False)> _
    Shadows Property Value()
        Get
            'No Code is Required for Get and Set
        End Get
        Set(ByVal Value)

        End Set
    End Property
    <Browsable(False)> Shadows Property Width()
        Get

        End Get
        Set(ByVal Value)

        End Set
    End Property
    Friend Sub OnClick()
        RaiseEvent Click(Me, New EventArgs)
    End Sub
    'Now Exciting Part of Code Serialization
    Friend Class PushButtonConverter
        Inherits TypeConverter
        Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
            'What we are saying to the serializor, if the seriazor asks for an InstanceDescriptor, we can handle it
            If destinationType Is GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destinationType)
        End Function
        Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
            If destinationType Is GetType(InstanceDescriptor) Then
                'PushButton object does not have a constructor with parameters so we just return the Sub New Constructor
                'First paramater return the Constructor, Second must be Nothing because Constructor does not have any parameters, and Third parameter basically tell the serializor definition is not complete and properties will be defined afterwards. This is required because we want to see the generated Code as follows

                ' Friend WithEvents PushButton1 as PushButton

                ' In InitializeComponent

                ' Me.PushButton1 = new PushButton

                ' ...   .AddRange(new Object(),{me.PushButton1, ....  other Buttons .... })

                ' Me.PushBotton1.ImageIndex = 0
                ' Other Properties follows

                Return New InstanceDescriptor(GetType(PushButton).GetConstructor(New Type() {}), Nothing, False)
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function
    End Class

End Class
#End Region

#Region " ToggleButton"
<Serializable(), TypeConverter(GetType(ToggleButton.ToggleButtonConverter))> _
Public Class ToggleButton
    Inherits ButtonBase
    Event ValueChanged As EventHandler
    Public Sub New()
        MyBase.New()
        Me.ButtonType = ButtonBase.ButtonTypes.ToggleButton
    End Sub
    <Browsable(False)> Shadows Property Width()
        Get

        End Get
        Set(ByVal Value)

        End Set
    End Property
    Friend Sub OnValueChanged()
        RaiseEvent ValueChanged(Me, New EventArgs)
    End Sub
    Friend Class ToggleButtonConverter
        Inherits TypeConverter
        Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
            'What we are saying to the serializor, if the seriazor asks for an InstanceDescriptor, we can handle it
            If destinationType Is GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destinationType)
        End Function
        Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
            If destinationType Is GetType(InstanceDescriptor) Then
                Return New InstanceDescriptor(GetType(ToggleButton).GetConstructor(New Type() {}), Nothing, False)
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function
    End Class
End Class
#End Region

#Region " ButtonSeperator"
<Serializable(), TypeConverter(GetType(ButtonSeperator.ButtonSeperatorConverter))> _
Public Class ButtonSeperator
    Private m_Text As String
    Public Sub New()
        m_Text = "Seperator"
    End Sub
    Public ReadOnly Property Text() As String
        Get
            Return m_Text
        End Get
    End Property
    Friend Class ButtonSeperatorConverter
        Inherits TypeConverter
        Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
            If destinationType Is GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destinationType)
        End Function
        Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
            If destinationType Is GetType(InstanceDescriptor) Then
                Return New InstanceDescriptor(GetType(ButtonSeperator).GetConstructor(New Type() {}), Nothing, True)
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function
    End Class
End Class
#End Region

#Region " PlaceHolder"
<Serializable(), TypeConverter(GetType(PlaceHolder.PlaceHolderConverter))> _
Public Class PlaceHolder
    Private m_Width As Integer
    Public Sub New()
    End Sub
    Public Sub New(ByVal Width As Integer)
        m_Width = Width
    End Sub
    Public Property Width() As Integer
        Get
            Return m_Width
        End Get
        Set(ByVal Value As Integer)
            m_Width = Value
        End Set
    End Property
    Friend Class PlaceHolderConverter
        Inherits TypeConverter
        Public Overloads Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destType As Type) As Boolean
            If destType Is GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destType)
        End Function
        Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destType As Type)
            If destType Is GetType(InstanceDescriptor) Then
                Dim MyObject As PlaceHolder = CType(value, PlaceHolder)
                'The PlaceHolder Object has an overloaded constructor in which Width is set. So we tell the serializer to use this constructor when creating the instance of the object
                ' This is achieved by passing the types of variables in the argument list, in this case Integer.
                'We dont want to see a place holder in class dropdown so we return true as third parameter to tell the serializer to define the object during AddRange method
                Return New InstanceDescriptor(GetType(PlaceHolder).GetConstructor(New Type() {GetType(Integer)}), New Object() {MyObject.Width}, True)
            End If
            Return MyBase.ConvertTo(context, culture, value, destType)
        End Function
    End Class
End Class
#End Region

#Region " Buttons Collection"
' A Strong typed collection for different type of buttons
' Serializable attribute is required to generate code for your objects defined during design time 
<Serializable()> _
Public Class Buttons
    Inherits CollectionBase
    'Event To Notify Parent when a property is changed during Design or Run Time so the control can Paint itself
    Event PropertyChaged()
    'Provide Add and AddRange Methods and Item(Indexer)
    Public Function Add(ByVal Item As Object) As Object
        If Not TypeOf Item Is ButtonSeperator And Not TypeOf Item Is PlaceHolder Then
            CType(Item, ButtonBase).Collection = Me
        End If
        list.Add(Item)
        Return Item
    End Function
    Public Sub AddRange(ByVal Items() As Object)
        Dim Item As Object
        For Each Item In Items
            If Not TypeOf Item Is ButtonSeperator And Not TypeOf Item Is PlaceHolder Then
                CType(Item, ButtonBase).Collection = Me
            End If
            list.Add(Item)
        Next
    End Sub
    Default Public ReadOnly Property Item(ByVal Index As Integer) As ButtonBase
        Get
            If TypeOf list(Index) Is PushButton Then
                Return CType(list(Index), ButtonBase)
            End If
            If TypeOf list(Index) Is ToggleButton Then
                Return CType(list(Index), ButtonBase)
            End If
            If TypeOf list(Index) Is PlaceHolder Then
                Return New ButtonBase(ButtonBase.ButtonTypes.PlaceHolder, CType(List(Index), PlaceHolder).Width)
            End If
            If TypeOf List(Index) Is ButtonSeperator Then
                Return New ButtonBase(ButtonBase.ButtonTypes.Seperator, 0)
            End If
        End Get
    End Property
    Friend Sub RaisePropertyChangedEvent()
        RaiseEvent PropertyChaged()
    End Sub
End Class
#End Region

#Region " ButtonCollectionEditor"
Friend Class ButtonCollectionEditor
    Inherits System.ComponentModel.Design.CollectionEditor
    Private Types() As System.Type
    Sub New(ByVal type As System.Type)
        MyBase.New(type)
        Types = New System.Type() {GetType(PushButton), GetType(ButtonSeperator), GetType(PlaceHolder), GetType(ToggleButton)}
    End Sub
    Protected Overrides Function CreateNewItemTypes() As System.Type()
        Return Types
    End Function
End Class
#End Region

#Region " EImageIndexConverter"
Friend Class EImageIndexConverter
    Inherits ImageIndexConverter

    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            If value <> "(none)" And value <> vbNullString Then
                Try
                    Return CInt(value)
                Catch ex As Exception
                    Return -1
                End Try
            Else
                Return -1
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If TypeOf value Is Integer Then
            If value <> -1 Then
                Return CStr(value)
            Else
                Return "(none)"
            End If
        Else
            Return -1
        End If
    End Function

    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Dim Ar As New ArrayList
        Ar.Add(-1)
        Dim m_imagel As ImageList
        m_imagel = mm_ImageList
        If mm_ImageList Is Nothing Then
            m_imagel = Nothing
        Else
            m_imagel = mm_ImageList
        End If
        If Not m_imagel Is Nothing Then
            For i As Integer = 0 To m_imagel.Images.Count - 1
                Ar.Add(i)
            Next
        End If
        Return New StandardValuesCollection(Ar)
    End Function
    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        If context.Instance Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
#End Region

#Region " EImageIndexEditor"
Friend Class EImageIndexEditor
    Inherits UITypeEditor

    Public Overloads Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overloads Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        Dim m_imageIdx As Integer
        m_imageIdx = CInt(e.Value)
        Dim m_imagel As ImageList
        If mm_ImageList Is Nothing Then
            m_imagel = Nothing
        Else
            m_imagel = mm_ImageList
        End If
        If Not m_imagel Is Nothing Then
            If m_imageIdx >= 0 And m_imageIdx < m_imagel.Images.Count Then
                e.Graphics.DrawImage(m_imagel.Images(CInt(e.Value)), e.Bounds)
            End If
        End If
    End Sub
End Class
#End Region

#Region " ControlDesigner"
Public Class UserControl1Designer
    Inherits System.Windows.Forms.Design.ControlDesigner
    Public Overrides Sub Initialize(ByVal component As System.ComponentModel.IComponent)
        MyBase.Initialize(component)
        Dim ss As ISelectionService = CType(GetService(GetType(ISelectionService)), ISelectionService)
        If Not (ss Is Nothing) Then
            AddHandler ss.SelectionChanged, AddressOf OnSelectionChanged
        End If

    End Sub
    Private Sub OnSelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ss As ISelectionService = CType(sender, ISelectionService)
        If Not ss Is Nothing Then
            If TypeOf ss.PrimarySelection Is UserControl1 Then
                mm_ImageList = CType(ss.PrimarySelection, UserControl1).ImageList
            End If
        End If
    End Sub
End Class
#End Region

#End Region