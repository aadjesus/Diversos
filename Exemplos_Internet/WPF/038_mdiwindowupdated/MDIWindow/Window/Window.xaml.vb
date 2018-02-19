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

Partial Public Class Window

#Region "Declarations"
    Friend _WindowState As WindowState
    Friend _Index As Integer
    Protected Friend _Parent As WindowMgr
    Private _InitialPos As Point = New Point(0, 0)
    Public _IsMoving As Boolean = False

#Region "Drag information"
    Private _DragStartMousePosition As Point
    Private _DragStartMyPosition As Point
    Private _DragMaxX As Double
    Private _DragMaxY As Double
#End Region

#Region "Events"
    Public Event WindowStateChanged As WindowStateChangeEvent
    Public Event WindowDragStart As Windows.Input.MouseButtonEventHandler
#End Region

#End Region

#Region "Properties"
    Public ReadOnly Property WindowState() As WindowState
        Get
            Return _WindowState
        End Get
    End Property
    Public Property Index() As Integer
        Get
            Return _Index
        End Get
        Set(ByVal value As Integer)
            _Index = value
        End Set
    End Property
    Public Property Title() As String
        Get
            Return lblTitle.Content.ToString()
        End Get
        Set(ByVal value As String)
            lblTitle.Content = value
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New()
        Me.InitializeComponent()
        _WindowState = WindowState.Maximized
    End Sub

#End Region

    Public Class Constants
        Public Const MINIMIZED_HEIGHT As Integer = 22
    End Class

    Private Sub SetPosition(ByVal newIndex As Integer)
        Dim myParent As WrapPanel = DirectCast(Me.Parent, WrapPanel)
        _Index = myParent.Children.IndexOf(Me)
        If newIndex = _Index Then
            Return
        End If
        myParent.Children.RemoveAt(_Index)
        If _Index > newIndex Then
            myParent.Children.Insert(newIndex, Me)
        Else
            myParent.Children.Insert(newIndex - 1, Me)
        End If
        _Index = newIndex
    End Sub

    Private Sub cmdMaximizeRestore_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdMaximizeRestore.Click
        If cmdMaximizeRestore.Content = "M" Then
            RaiseEvent WindowStateChanged(Me, WindowState.Maximized)
            cmdMaximizeRestore.Content = "R"
            'cmdMaximizeRestore.Style = DirectCast(FindResource("MinimizeButtonStyle"), Style)
            Me._WindowState = WindowState.Maximized
        Else
            RaiseEvent WindowStateChanged(Me, WindowState.Restore)
            cmdMaximizeRestore.Content = "M"
            'cmdMaximizeRestore.Style = DirectCast(FindResource("MaximizeButtonStyle"), Style)
            Me._WindowState = WindowState.Restore
        End If
    End Sub

    Public Sub AddUIElement(ByVal obj As UIElement)
        If obj Is Nothing Then
            Return
        End If
        WindowContent.Content = obj
        obj.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub TopBorder_PreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles TopBorder.PreviewMouseLeftButtonDown
        If TopBorder Is e.Source OrElse TopBar Is e.Source OrElse lblTitle Is e.Source Then
            RaiseEvent WindowDragStart(Me, e)
            e.Handled = True
        End If
    End Sub

    Private Sub Window_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
        If Me.ActualHeight < 29 OrElse Me.ActualWidth = 0 Then
            Return
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdClose.Click
        RaiseEvent WindowStateChanged(Me, WindowState.Close)
    End Sub

    Public Sub SetMovingToFalse(ByVal sender As Object, ByVal e As System.EventArgs)
        _IsMoving = False
    End Sub

    Public Sub OnAnimationComplete(ByVal sender As Object, ByVal e As EventArgs)
        Dim at As AnimationClock = sender
        If (Not at Is Nothing) Then
            RemoveHandler at.Completed, AddressOf OnAnimationComplete
        End If
        _IsMoving = False
    End Sub

End Class
