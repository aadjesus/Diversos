''' Developed by : Sreenivas Vemulapalli
''' vemvas@yahoo 
''' Date: 08/15/2002
'''
Imports System.ComponentModel
'''
<DefaultPropertyAttribute("Title"), _
DescriptionAttribute("Appliation Properties")> _
Public Class SimpleProperties
    Private _Title As String
    Private _Show As Boolean
    Private _Number As Short
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
