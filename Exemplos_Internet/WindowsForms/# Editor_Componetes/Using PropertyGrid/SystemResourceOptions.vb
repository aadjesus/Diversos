''' Developed by : Sreenivas Vemulapalli
''' vemvas@yahoo 
''' Date: 08/15/2002
'''
Imports System.ComponentModel

'''
Public Class SystemResourceOptions
    '''
    Private _Title As String
    Private _Show As Boolean
    Private _Number As Short
    Private _ApplicationSize As Size
    Private _ApplicationLocation As Point
    Private _ApplicationFont As System.Drawing.Font
    Private _FontColor As System.Drawing.Color
    Private _ApplicationIcon As System.Drawing.Icon
    Private _ApplicationCursor As System.Windows.Forms.Cursor
    Private _ApplicationLangugae As System.Globalization.CultureInfo
    Private _State As String
    Private _Version As ApplicationVersion = New ApplicationVersion()
    '''
    <CategoryAttribute("Custom List"), DefaultValueAttribute(""), _
    DescriptionAttribute("Set the version")> _
    Public Property Version() As ApplicationVersion
        Get
            Return _Version
        End Get
        Set(ByVal Value As ApplicationVersion)
            Me._Version = Value
        End Set
    End Property
    '''
    <TypeConverter(GetType(StatesList)), _
    CategoryAttribute("Custom List"), DefaultValueAttribute(""), _
    DescriptionAttribute("Select a state from the list")> _
    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal Value As String)
            _State = Value
        End Set
    End Property

    <CategoryAttribute("Design"), DefaultValueAttribute(""), _
    DescriptionAttribute("Enter Application size")> _
    Public Property ApplicationSize() As Size
        Get
            Return _ApplicationSize
        End Get
        Set(ByVal Value As Size)
            _ApplicationSize = Value
        End Set
    End Property
    <CategoryAttribute("Design"), DefaultValueAttribute(""), _
    DescriptionAttribute("Enter Application Location")> _
    Public Property ApplicationLocation() As Point
        Get
            Return ApplicationLocation
        End Get
        Set(ByVal Value As Point)
            _ApplicationLocation = Value
        End Set
    End Property
    <CategoryAttribute("Design"), DefaultValueAttribute(""), _
    DescriptionAttribute("Select font for the application")> _
    Public Property ApplicationFont() As System.Drawing.Font
        Get
            Return _ApplicationFont
        End Get
        Set(ByVal Value As System.Drawing.Font)
            _ApplicationFont = Value
        End Set
    End Property
    <CategoryAttribute("Design"), DefaultValueAttribute(""), _
    DescriptionAttribute("Select font color")> _
    Public Property FontColor() As System.Drawing.Color
        Get
            Return _FontColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            _FontColor = Value
        End Set
    End Property
    <CategoryAttribute("Design"), DefaultValueAttribute(""), _
    DescriptionAttribute("Select icon for application")> _
        Public Property ApplicationIcon() As System.Drawing.Icon
        Get
            Return _ApplicationIcon
        End Get
        Set(ByVal Value As System.Drawing.Icon)
            _ApplicationIcon = Value
        End Set
    End Property
    <CategoryAttribute("Design"), DefaultValueAttribute(""), _
    DescriptionAttribute("Select application's cursor")> _
    Public Property ApplicationCursor() As System.Windows.Forms.Cursor
        Get
            Return _ApplicationCursor
        End Get
        Set(ByVal Value As System.Windows.Forms.Cursor)
            _ApplicationCursor = Value
        End Set
    End Property
    <CategoryAttribute("Design"), DefaultValueAttribute(""), _
    DescriptionAttribute("Select User language")> _
    Public Property ApplicationLangugae() As System.Globalization.CultureInfo
        Get
            Return _ApplicationLangugae
        End Get
        Set(ByVal Value As System.Globalization.CultureInfo)
            _ApplicationLangugae = Value
        End Set
    End Property
    <CategoryAttribute("Application"), _
       Browsable(True), _
       [ReadOnly](False), _
       BindableAttribute(False), _
       DefaultValueAttribute(""), _
       DesignOnly(False), _
       DescriptionAttribute("Enter Title for the application")> _
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal Value As String)
            _Title = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Application"), _
       Browsable(True), _
       [ReadOnly](False), _
       BindableAttribute(False), _
       DefaultValueAttribute("True"), _
       DesignOnly(False), _
       DescriptionAttribute("Show option")> _
    Public Property Show() As Boolean
        Get
            Return _Show
        End Get
        Set(ByVal Value As Boolean)
            _Show = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Application"), _
       Browsable(True), _
       [ReadOnly](False), _
       BindableAttribute(False), _
       DefaultValueAttribute("0"), _
       DesignOnly(False), _
       DescriptionAttribute("Enter a number")> _
    Public Property Number() As Short
        Get
            Return _Number
        End Get
        Set(ByVal Value As Short)
            _Number = Value
        End Set
    End Property
    '''
End Class
