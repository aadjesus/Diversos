Imports System.ComponentModel
Imports System.Drawing.Design

' this is the custom type we'll want to edit in the propertygrid
' it's just a class with 1 property, a string, but we want a multiline editor for it
Public Class MultiLineString
    Private mString As String
    Private mS As String

    Public Sub New()

    End Sub
    Public Sub New(ByVal s As String)
        mString = s
    End Sub
    Public Property Value() As String
        Get
            Return mString
        End Get
        Set(ByVal Value As String)
            mString = Value
        End Set
    End Property
    Public Overrides Function ToString() As String
        Return IIf(mString = "", "", "<...>")
    End Function
End Class
