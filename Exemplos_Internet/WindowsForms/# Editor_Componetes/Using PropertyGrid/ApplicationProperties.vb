''' Developed by : Sreenivas Vemulapalli
''' vemvas@yahoo 
''' Date: 08/15/2002
'''
Imports System.ComponentModel
'''
Public Class ApplicationProperties
    Private _Title As String
    Private _ApplicationIcon As String
    Private _ApplicationSize As Size
    Private _ApplicationLocation As Point
    Private _ApplicationCursor As System.Windows.Forms.Cursor
    Private _ApplicationFont As System.Drawing.Font
    Private _ApplicationFontColor As System.Drawing.Color
    Private _EmailAddress As String
    Private _EmailServer As String
    Private _SQLConnectStringProperties As New ConnectStringProperties()
    Private _Client
    Private _MaximizeOnStartup As Boolean
    Private _ShowTips As Boolean
    '''
     

    <CategoryAttribute("Design"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DesignOnly(False), _
    DescriptionAttribute("")> _
    Public Property ApplicationSize() As Size
        Get
            Return _ApplicationSize
        End Get
        Set(ByVal Value As Size)
            _ApplicationSize = Value
        End Set
    End Property
    Public Property ApplicationLocation() As Point
        Get
            Return _ApplicationLocation
        End Get
        Set(ByVal Value As Point)
            _ApplicationLocation = Value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal Value As String)
            _Title = Value
        End Set
    End Property
    Public Property ApplicationCursor() As System.Windows.Forms.Cursor
        Get
            Return _ApplicationCursor
        End Get
        Set(ByVal Value As System.Windows.Forms.Cursor)
            _ApplicationCursor = Value
        End Set
    End Property
    Public Property ApplicationIcon() As String
        Get
            Return _ApplicationIcon
        End Get
        Set(ByVal Value As String)
            _ApplicationIcon = Value
        End Set
    End Property
    Public Property ApplicationFont() As System.Drawing.Font
        Get
            Return _ApplicationFont
        End Get
        Set(ByVal Value As System.Drawing.Font)
            _ApplicationFont = Value
        End Set
    End Property
    Public Property ApplicationFontColor() As System.Drawing.Color
        Get
            Return _ApplicationFontColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            _ApplicationFontColor = Value
        End Set
    End Property
    Public Property EmailAddress() As String
        Get
            Return _EmailAddress
        End Get
        Set(ByVal Value As String)
            _EmailAddress = Value
        End Set
    End Property
    Public Property EmailServer() As String
        Get
            Return _EmailServer
        End Get
        Set(ByVal Value As String)
            _EmailServer = Value
        End Set
    End Property
    Public Property SQLConnectStringProperties() As ConnectStringProperties
        Get
            Return _SQLConnectStringProperties
        End Get
        Set(ByVal Value As ConnectStringProperties)
            _SQLConnectStringProperties = Value
        End Set
    End Property
    Public Property Client()
        Get
            Return _Client
        End Get
        Set(ByVal Value)
            _Client = Value
        End Set
    End Property
    Public Property MaximizeOnStartup() As Boolean
        Get
            Return _MaximizeOnStartup
        End Get
        Set(ByVal Value As Boolean)
            _MaximizeOnStartup = Value
        End Set
    End Property
    Public Property ShowTips() As Boolean
        Get
            Return _ShowTips
        End Get
        Set(ByVal Value As Boolean)
            _ShowTips = Value
        End Set
    End Property
End Class
