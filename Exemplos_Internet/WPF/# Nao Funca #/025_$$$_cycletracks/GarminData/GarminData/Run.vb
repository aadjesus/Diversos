Option Infer On

Imports System.Windows
Imports <xmlns:g="http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2">

Public Class TrackPoint

  Private _time As DateTime

  Public Property Time() As DateTime
    Get
      Return _time
    End Get
    Set(ByVal value As DateTime)
      _time = value
    End Set
  End Property

  Private _position As Point

  Public Property Position() As Point
    Get
      Return _position
    End Get
    Set(ByVal value As Point)
      _position = value
    End Set
  End Property

  Private _heartRate As Integer

  Public Property HeartRate() As Integer
    Get
      Return _heartRate
    End Get
    Set(ByVal value As Integer)
      _heartRate = value
    End Set
  End Property

  Private _distance As Double

  Public Property Distance() As Double
    Get
      Return _distance
    End Get
    Set(ByVal value As Double)
      _distance = value
    End Set
  End Property

  Private _image As Byte()

  Public Property Image() As Byte()
    Get
      Return _image
    End Get
    Set(ByVal value As Byte())
      _image = value
    End Set
  End Property

End Class

Public Class Run

  Private _trackPoints As List(Of TrackPoint)

  Public Property TrackPoints() As List(Of TrackPoint)
    Get
      Return _trackPoints
    End Get
    Set(ByVal value As List(Of TrackPoint))
      _trackPoints = value
    End Set
  End Property

  Private _totalDistance As Double

  Public Property TotalDistance() As Double
    Get
      Return _totalDistance
    End Get
    Set(ByVal value As Double)
      _totalDistance = value
    End Set
  End Property

  ''' <summary>
  ''' Load Garmin document and parse it into trackpoints
  ''' </summary>
  ''' <param name="filename">the name of the file</param>
  ''' <returns>A Run instance</returns>
  ''' <remarks>Uses VB LINQ To XML</remarks>
  Public Shared Function Load(ByVal filename As String) As Run
    Dim doc As XDocument = XDocument.Load(filename)

    Dim query = From tp In doc...<g:Trackpoint> _
                Select New TrackPoint With { _
                  .Time = CType(tp.<g:Time>.Value, DateTime), _
                  .HeartRate = CType(tp.<g:HeartRateBpm>.<g:Value>.Value, Integer), _
                  .Position = New Point() With { _
                    .X = CType(tp.<g:Position>.<g:LatitudeDegrees>.Value, Double), _
                    .Y = CType(tp.<g:Position>.<g:LongitudeDegrees>.Value, Double) _
                    }, _
                  .Distance = CType(tp.<g:DistanceMeters>.Value, Double) _
                }
    Dim run As New Run()
    run.TrackPoints = query.Where(Function(tp) tp.Position.X + tp.Position.Y <> 0) _
                           .OrderBy(Function(tp) tp.Distance).ToList()
    run.TotalDistance = run.TrackPoints.Max(Function(tp) tp.Distance)
    Return run

  End Function

End Class
