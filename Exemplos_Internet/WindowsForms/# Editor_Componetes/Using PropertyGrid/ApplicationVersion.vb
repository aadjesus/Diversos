''' Developed by : Sreenivas Vemulapalli
''' vemvas@yahoo 
''' Date: 08/15/2002
'''
Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
'''
<TypeConverter(GetType(ApplicationVersionConverter))> _
Public Class ApplicationVersion
    '''
    Private _Major As Short
    Private _Minor As Short
    Private _Build As Short
    Private _Private As Short
    '''
    <DescriptionAttribute("Set the major part of version number")> _
        Public Property Major() As Short
        Get
            Return _Major
        End Get
        Set(ByVal Value As Short)
            _Major = Value
        End Set
    End Property
    '''
    <DescriptionAttribute("Set the minor part of version number")> _
    Public Property Minor() As Short
        Get
            Return _Minor
        End Get
        Set(ByVal Value As Short)
            Me._Minor = Value
        End Set
    End Property
    '''
    <DescriptionAttribute("Set the build part of version number")> _
    Public Property Build() As Short
        Get
            Return _Build
        End Get
        Set(ByVal Value As Short)
            Me._Build = Value
        End Set
    End Property
    '''
    <DescriptionAttribute("Set the private part of version number")> _
    Public Property [Private]() As Short
        Get
            Return _Private
        End Get
        Set(ByVal Value As Short)
            Me._Private = Value
        End Set
    End Property
    '''
End Class

Friend Class ApplicationVersionConverter : Inherits ExpandableObjectConverter
    '''
    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim s As String = CType(value, String)
                Dim versionParts() As String
                Dim VersionString As String = ""
                versionParts = Split(s, ".")
                If Not IsNothing(versionParts) Then
                    Dim _ApplicationVersion As ApplicationVersion = New ApplicationVersion()
                    If Not IsNothing(versionParts(0)) Then _ApplicationVersion.Major = versionParts(0)
                    If Not IsNothing(versionParts(1)) Then _ApplicationVersion.Minor = versionParts(1)
                    If Not IsNothing(versionParts(2)) Then _ApplicationVersion.Build = versionParts(2)
                    If Not IsNothing(versionParts(3)) Then _ApplicationVersion.Private = versionParts(3)
                End If
            Catch ex As Exception
                Throw New ArgumentException("Can not convert '" + value + "' to type ApplicationVersion")
            End Try
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is ApplicationVersion) Then
            Dim _ApplicationVersion As ApplicationVersion = CType(value, ApplicationVersion)
            ' build the string as "Major.Minor.Build.Private" 
            Return _ApplicationVersion.Major & "." & _ApplicationVersion.Minor & "." & _ApplicationVersion.Build & "." & _ApplicationVersion.Private
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If (destinationType Is GetType(ApplicationVersion)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, destinationType)
    End Function
End Class

