Public Class MyOwnClass
    Private mID As Integer
    Private mTitle As String
    Private mDesc As MultiLineString
    Private mExtra As MultiLineString
    Public Sub New(ByVal id As Integer)
        Me.mID = id
    End Sub
    Public ReadOnly Property ID() As Integer
        Get
            Return Me.mID
        End Get
    End Property
    Public Property Title() As String
        Get
            Return Me.mTitle
        End Get
        Set(ByVal Value As String)
            Me.mTitle = Value
        End Set
    End Property
    Public Property Description() As MultiLineString
        Get
            Return Me.mDesc
        End Get
        Set(ByVal Value As MultiLineString)
            Me.mDesc = Value
        End Set
    End Property
    Public Property Extra() As MultiLineString
        Get
            Return Me.mExtra
        End Get
        Set(ByVal Value As MultiLineString)
            Me.mExtra = Value
        End Set
    End Property
End Class