''' Developed by : Sreenivas Vemulapalli
''' vemvas@yahoo 
''' Date: 08/15/2002
'''
Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Drawing
Imports System.Text
'''
<TypeConverter(GetType(ConnectStringPropertiesConverter)), _
DefaultPropertyAttribute("Application_Name")> _
Public Class ConnectStringProperties
    '''
#Region "Variables"
    Public Enum IntegratedSecurityTypes
        [True]
        [Flase]
        [SSPI]
    End Enum
    Private _Application_Name As String
    Private _Initial_catalog As String
    Private _Data_source As String
    Private _Connection_timeout As Short
    Private _Connection_Lifetime As Short
    Private _Connection_Reset As Boolean
    Private _Current_Language
    Private _Server As String
    Private _Enlist As String
    Private _Database As String
    Private _Integrated_Security As IntegratedSecurityTypes
    Private _Max_PoolSize As Short
    Private _Min_PoolSize As Short
    Private _Network_Library As String = "dbmssocn"
    Private _Packet_Size As Short
    Private _UserID As String
    Private _Password As String
    Private _Persist_Security_Info As Boolean
    Private _Pooling As Boolean
    Private _Workstation_ID As String

    Public Property Application_Name() As String
        Get
            Return _Application_Name
        End Get
        Set(ByVal Value As String)
            _Application_Name = Value
        End Set
    End Property

    Public Property Initial_catalog() As String
        Get
            Return _Initial_catalog
        End Get
        Set(ByVal Value As String)
            _Initial_catalog = Value
        End Set
    End Property
    Public Property Data_source() As String
        Get
            Return _Data_source
        End Get
        Set(ByVal Value As String)
            _Data_source = Value
        End Set
    End Property
    Public Property Connection_timeout() As Short
        Get
            Return _Connection_timeout
        End Get
        Set(ByVal Value As Short)
            _Connection_timeout = Value
        End Set
    End Property
    Public Property Connection_Lifetime() As Short
        Get
            Return _Connection_Lifetime
        End Get
        Set(ByVal Value As Short)
            _Connection_Lifetime = Value
        End Set
    End Property
    Public Property Connection_Reset() As Boolean
        Get
            Return _Connection_Reset
        End Get
        Set(ByVal Value As Boolean)
            _Connection_Reset = Value
        End Set
    End Property
    Public Property Current_Language()
        Get
            Return _Current_Language
        End Get
        Set(ByVal Value)
            _Current_Language = Value
        End Set
    End Property
    Public Property Server() As String
        Get
            Return _Server
        End Get
        Set(ByVal Value As String)
            _Server = Value
        End Set
    End Property
    Public Property Enlist() As String
        Get
            Return _Enlist
        End Get
        Set(ByVal Value As String)
            _Enlist = Value
        End Set
    End Property
    Public Property Database() As String
        Get
            Return _Database
        End Get
        Set(ByVal Value As String)
            _Database = Value
        End Set
    End Property
    Public Property Integrated_Security() As IntegratedSecurityTypes
        Get
            Return _Integrated_Security
        End Get
        Set(ByVal Value As IntegratedSecurityTypes)
            _Integrated_Security = Value
        End Set
    End Property
    Public Property Max_PoolSize() As Short
        Get
            Return _Max_PoolSize
        End Get
        Set(ByVal Value As Short)
            _Max_PoolSize = Value
        End Set
    End Property
    Public Property Min_PoolSize() As Short
        Get
            Return _Min_PoolSize
        End Get
        Set(ByVal Value As Short)
            _Min_PoolSize = Value
        End Set
    End Property
    Public Property Network_Library() As String
        Get
            Return _Network_Library
        End Get
        Set(ByVal Value As String)
            _Network_Library = Value
        End Set
    End Property
    Public Property Packet_Size() As Short
        Get
            Return _Packet_Size
        End Get
        Set(ByVal Value As Short)
            _Packet_Size = Value
        End Set
    End Property
    Public Property UserID() As String
        Get
            Return _UserID
        End Get
        Set(ByVal Value As String)
            _UserID = Value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal Value As String)
            _Password = Value
        End Set
    End Property
    Public Property Persist_Security_Info() As Boolean
        Get
            Return _Persist_Security_Info
        End Get
        Set(ByVal Value As Boolean)
            _Persist_Security_Info = Value
        End Set
    End Property
    Public Property Pooling() As Boolean
        Get
            Return _Pooling
        End Get
        Set(ByVal Value As Boolean)
            _Pooling = Value
        End Set
    End Property
    Public Property Workstation_ID() As String
        Get
            Return _Workstation_ID
        End Get
        Set(ByVal Value As String)
            _Workstation_ID = Value
        End Set
    End Property
#End Region
    '''
#Region "Properties"

#End Region
    '''
End Class
'''
Public Class ConnectStringPropertiesConverter : Inherits ExpandableObjectConverter
    '''
#Region "Variables"

#End Region
    '''
#Region "Properties"
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
                Dim tempProp As String()
                Dim __ConnectStringProperties As New ConnectStringProperties()
                If s.Length > 0 Then
                    tempProp = Split(s, ";")
                    If Not IsNothing(tempProp) AndAlso tempProp.Length > 0 Then
                        Dim i As Short
                        Dim tempValues() As String
                        For i = 0 To tempProp.Length - 1
                            tempValues = Split(tempProp(i), "=")
                            tempValues(0) = LCase(Trim(tempValues(0)))
                            If IsNothing(tempValues(1)) Then tempValues(1) = ""
                            Select Case tempValues(0)
                                Case tempValues(0) = "application name"
                                    __ConnectStringProperties.Application_Name = tempValues(1)
                                Case tempValues(0) = "initial catalog"
                                    __ConnectStringProperties.Initial_catalog = tempValues(1)
                                Case tempValues(0) = "data source"
                                    __ConnectStringProperties.Data_source = tempValues(1)
                                Case tempValues(0) = "connection timeout"
                                    __ConnectStringProperties.Connection_timeout = tempValues(1)
                                Case tempValues(0) = "connection lifetime"
                                    __ConnectStringProperties.Connection_Lifetime = tempValues(1)
                                Case tempValues(0) = "connection reset"
                                    __ConnectStringProperties.Connection_Reset = tempValues(1)
                                Case tempValues(0) = "current language"
                                    __ConnectStringProperties.Current_Language = tempValues(1)
                                Case tempValues(0) = "server"
                                    __ConnectStringProperties.Server = tempValues(1)
                                Case tempValues(0) = "enlist"
                                    __ConnectStringProperties.Enlist = tempValues(1)
                                Case tempValues(0) = "database"
                                    __ConnectStringProperties.Database = tempValues(1)
                                Case tempValues(0) = "integrated security"
                                    __ConnectStringProperties.Integrated_Security = tempValues(1)
                                Case tempValues(0) = "max poolsize"
                                    __ConnectStringProperties.Max_PoolSize = tempValues(1)
                                Case tempValues(0) = "min poolsize"
                                    __ConnectStringProperties.Min_PoolSize = tempValues(1)
                                Case tempValues(0) = "network library"
                                    __ConnectStringProperties.Network_Library = tempValues(1)
                                Case tempValues(0) = "packet size"
                                    __ConnectStringProperties.Packet_Size = tempValues(1)
                                Case tempValues(0) = "userid"
                                    __ConnectStringProperties.UserID = tempValues(1)
                                Case tempValues(0) = "password"
                                    __ConnectStringProperties.Password = tempValues(1)
                                Case tempValues(0) = "persist security info"
                                    __ConnectStringProperties.Persist_Security_Info = tempValues(1)
                                Case tempValues(0) = "pooling"
                                    __ConnectStringProperties.Pooling = tempValues(1)
                                Case tempValues(0) = "workstation id"
                                    __ConnectStringProperties.Workstation_ID = tempValues(1)
                            End Select
                        Next
                    End If
                End If
            Catch ex As Exception
                ' if we got this far, complain that we
                ' couldn't parse the string
                '
                Throw New ArgumentException("Can not convert '" + value + "' to type Person")
            End Try
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is ConnectStringProperties) Then
            Dim __ConnectStringProperties As ConnectStringProperties = CType(value, ConnectStringProperties)
            Dim _ConnectString As New StringBuilder()
            With _ConnectString
                .Append("application name=" & __ConnectStringProperties.Application_Name & ";")
                .Append("initial catalog=" & __ConnectStringProperties.Initial_catalog & ";")
                .Append("data source=" & __ConnectStringProperties.Data_source & ";")
                .Append("connection timeout=" & __ConnectStringProperties.Connection_timeout & ";")
                .Append("connection lifetime=" & __ConnectStringProperties.Connection_Lifetime & ";")
                .Append("connection reset=" & __ConnectStringProperties.Connection_Reset & ";")
                .Append("current language=" & __ConnectStringProperties.Current_Language & ";")
                .Append("server=" & __ConnectStringProperties.Server & ";")
                .Append("enlist=" & __ConnectStringProperties.Enlist & ";")
                .Append("database=" & __ConnectStringProperties.Database & ";")
                .Append("integrated security=" & __ConnectStringProperties.Integrated_Security & ";")
                .Append("max poolsize=" & __ConnectStringProperties.Max_PoolSize & ";")
                .Append("min poolsize=" & __ConnectStringProperties.Min_PoolSize & ";")
                .Append("network library=" & __ConnectStringProperties.Network_Library & ";")
                .Append("packet size=" & __ConnectStringProperties.Packet_Size & ";")
                .Append("userid=" & __ConnectStringProperties.UserID & ";")
                .Append("password=" & __ConnectStringProperties.Password & ";")
                .Append("persist security info=" & __ConnectStringProperties.Persist_Security_Info & ";")
                .Append("pooling=" & __ConnectStringProperties.Pooling & ";")
                .Append("workstation id=" & __ConnectStringProperties.Workstation_ID & ";")
            End With
            Return _ConnectString.ToString
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If (destinationType Is GetType(ConnectStringProperties)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, destinationType)
    End Function
#End Region
    '''
End Class