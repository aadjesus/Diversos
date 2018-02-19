Imports System
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation
Imports System.Windows.Documents
Imports System.Windows.Shapes


Class DropPreviewAdorner
    Inherits Adorner

    ' Adding some basic fields to help us keep track of where we are and what we render
    Private m_Child As Rectangle
    Private m_LeftOffset As Double
    Private m_TopOffset As Double


    ' Methods will come here.
    ' ...


    Public Sub New(ByVal adornedElement As UIElement)
        MyBase.New(adornedElement)

        Dim brush As VisualBrush = New VisualBrush(adornedElement)

        m_Child = New Rectangle()
        m_Child.Width = adornedElement.RenderSize.Width
        m_Child.Height = adornedElement.RenderSize.Height
        m_Child.Fill = brush
    End Sub


    Protected Overrides Function MeasureOverride(ByVal constraint As System.Windows.Size) As System.Windows.Size
        m_Child.Measure(constraint)
        Return m_Child.DesiredSize
    End Function

    Protected Overrides Function ArrangeOverride(ByVal finalSize As System.Windows.Size) As System.Windows.Size
        m_Child.Arrange(New Rect(finalSize))
        Return finalSize
    End Function

    Protected Overrides Function GetVisualChild(ByVal index As Integer) As System.Windows.Media.Visual
        Return m_Child
    End Function

    Protected Overrides ReadOnly Property VisualChildrenCount() As Integer
        Get
            Return 1
        End Get
    End Property

    Public Property LeftOffset() As Double
        Get
            Return m_LeftOffset
        End Get
        Set(ByVal value As Double)
            m_LeftOffset = value
            UpdatePosition()
        End Set
    End Property

    Public Property TopOffset() As Double
        Get
            Return m_TopOffset
        End Get
        Set(ByVal value As Double)
            m_TopOffset = value
            UpdatePosition()
        End Set
    End Property

    Private Sub UpdatePosition()
        Dim adornerLayer As AdornerLayer = Me.Parent
        If Not adornerLayer Is Nothing Then
            adornerLayer.Update(AdornedElement)
        End If
    End Sub


    Public Overrides Function GetDesiredTransform(ByVal transform As System.Windows.Media.GeneralTransform) As System.Windows.Media.GeneralTransform
        Dim result As GeneralTransformGroup = New GeneralTransformGroup()
        result.Children.Add(MyBase.GetDesiredTransform(transform))
        result.Children.Add(New TranslateTransform(LeftOffset, TopOffset))
        Return result
    End Function

End Class
