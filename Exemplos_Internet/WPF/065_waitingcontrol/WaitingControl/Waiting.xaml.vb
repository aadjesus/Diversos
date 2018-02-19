Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Partial Public Class Waiting

    Public Shared ReadOnly BeforeWaitStartEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("BeforeWaitStart", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly WaitStartEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("WaitStart", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly AfterWaitStartEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("AfterWaitStart", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly BeforeWaitPauseEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("BeforeWaitPause", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly WaitPauseEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("WaitPause", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly AfterWaitPauseEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("AfterWaitPause", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly BeforeWaitResumeEvent As RoutedEvent = _
            EventManager.RegisterRoutedEvent("BeforeWaitResume", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly WaitResumeEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("WaitResume", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly AfterWaitResumeEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("AfterWaitResume", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly BeforeWaitEndEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("BeforeWaitEnd", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly WaitEndEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("WaitEnd", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly AfterWaitEndEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("AfterWaitEnd", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Public Shared ReadOnly StateChangeEvent As RoutedEvent = _
        EventManager.RegisterRoutedEvent("OnStateChange", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Waiting))

    Private _stryBrd As Storyboard
    Private _ctlState As ControlState
    Private _throwInvalidException As Boolean

#Region " Constructors "

    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
        Me.State = ControlState.Ready
        Me.ThrowsErrorOnInvalidOperation = False
    End Sub

#End Region

#Region " Custom Events "

    Public Custom Event BeforeWaitStart As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(BeforeWaitStartEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(BeforeWaitStartEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event WaitStarted As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(WaitStartEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(WaitStartEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event AfterWaitStart As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(AfterWaitStartEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(AfterWaitStartEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event BeforeWaitPause As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(BeforeWaitPauseEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(BeforeWaitPauseEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event WaitPaused As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(WaitPauseEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(WaitPauseEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event AfterWaitPause As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(AfterWaitPauseEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(AfterWaitPauseEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event BeforeWaitResume As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(BeforeWaitResumeEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(BeforeWaitResumeEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event WaitResumed As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(WaitResumeEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(WaitResumeEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event AfterWaitResume As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(AfterWaitResumeEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(AfterWaitResumeEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event BeforeWaitEnd As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(BeforeWaitEndEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(BeforeWaitEndEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event WaitEnd As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(WaitEndEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(WaitEndEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event AfterWaitEnd As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(AfterWaitEndEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(AfterWaitEndEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Custom Event StateChanged As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(StateChangeEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(StateChangeEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event
#End Region

#Region " Event Raising Methods "

    Private Sub RaiseBeforeWaitStartEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.BeforeWaitStartEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseWaitStartEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.WaitStartEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseAfterWaitStartEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.AfterWaitStartEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseBeforeWaitPauseEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.BeforeWaitPauseEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseWaitPauseEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.WaitPauseEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseAfterWaitPauseEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.AfterWaitPauseEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseBeforeWaitResumeEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.BeforeWaitResumeEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseWaitResumeEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.WaitResumeEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseAfterWaitResumeEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.AfterWaitResumeEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseBeforeWaitEndEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.BeforeWaitEndEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseWaitEndEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.WaitEndEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseAfterWaitEndEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.AfterWaitEndEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub

    Private Sub RaiseStateChangeEvent()
        Dim newEventArgs As New RoutedEventArgs(Waiting.StateChangeEvent)
        MyBase.RaiseEvent(newEventArgs)
    End Sub
#End Region

#Region " Public Methods "

    Public Sub Start()
        Try
            Me.RaiseBeforeWaitStartEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured before starting the wait animation: {0}", ex.Message))
        End Try

        Try
            Me.RaiseWaitStartEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured while starting the wait animation: {0}", ex.Message))
        End Try

        Try
            Me.RaiseAfterWaitStartEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured after starting the wait animation: {0}", ex.Message))
        End Try
    End Sub

    Public Sub [End]()
        Try
            Me.RaiseBeforeWaitEndEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured before ending the wait animation: {0}", ex.Message))
        End Try

        Try
            Me.RaiseWaitEndEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured while ending the wait animation: {0}", ex.Message))
        End Try

        Try
            Me.RaiseAfterWaitEndEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured after ending the wait animation: {0}", ex.Message))
        End Try
    End Sub

    Public Sub Pause()
        Try
            Me.RaiseBeforeWaitPauseEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured before Pausing the wait animation: {0}", ex.Message))
        End Try

        Try
            Me.RaiseWaitPauseEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured while Pausing the wait animation: {0}", ex.Message))
        End Try

        Try
            Me.RaiseAfterWaitPauseEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured after Pausing the wait animation: {0}", ex.Message))
        End Try
    End Sub

    Public Sub [Resume]()
        Try
            Me.RaiseBeforeWaitResumeEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured before Resuming the wait animation: {0}", ex.Message))
        End Try

        Try
            Me.RaiseWaitResumeEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured while Resuming the wait animation: {0}", ex.Message))
        End Try

        Try
            Me.RaiseAfterWaitResumeEvent()
        Catch ex As Exception
            Throw New Exception(String.Format("Error occured after Resuming the wait animation: {0}", ex.Message))
        End Try
    End Sub

#End Region

#Region " Public Properties "

    <ComponentModel.Description("Gets / Sets the Storyboard for the control animation")> _
    Public Property StoryBoard() As Storyboard
        Get
            If _stryBrd Is Nothing Then
                _stryBrd = Me.Resources.Item("Spin")
            End If

            Return _stryBrd
        End Get
        Set(ByVal value As Storyboard)
            _stryBrd = value
        End Set
    End Property

    <ComponentModel.Description("Gets the State of the control")> _
    Public Property State() As ControlState
        Get
            Return _ctlState
        End Get
        Private Set(ByVal value As ControlState)
            _ctlState = value
            Me.RaiseStateChangeEvent()
        End Set
    End Property

    <ComponentModel.Description("Gets / Sets if the control will throw an exception on invalid operation; Default value is fals")> _
    Public Property ThrowsErrorOnInvalidOperation() As Boolean
        Get
            Return _throwInvalidException
        End Get
        Set(ByVal value As Boolean)
            _throwInvalidException = value
        End Set
    End Property
#End Region

#Region " Events "

    Private Sub Waiting_WaitStarted(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.WaitStarted
        Try
            If Me.ThrowsErrorOnInvalidOperation Then
                If Me.State <> ControlState.Ready And _
                    Me.State <> ControlState.Stopped Then
                    Throw New InvalidOperationException("Control can be started only when it is ready / stopped")
                End If
            End If

            Me.StoryBoard.Begin(Me.Content, True)
            Me.State = ControlState.Running
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Waiting_WaitEnd(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.WaitEnd
        Try
            If Me.ThrowsErrorOnInvalidOperation Then
                If Me.State <> ControlState.Running And _
                    Me.State <> ControlState.Resumed And _
                    Me.State <> ControlState.Paused Then
                    Throw New InvalidOperationException("Control can be stopped only when it is running / paused / resumed")
                End If
            End If

            Me.StoryBoard.Stop(Me.Content)
            Me.State = ControlState.Stopped
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Waiting_WaitPaused(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.WaitPaused
        Try
            If Me.ThrowsErrorOnInvalidOperation Then
                If Me.State <> ControlState.Running And _
                    Me.State <> ControlState.Resumed Then
                    Throw New InvalidOperationException("Control can be paused only when it is running / resumed")
                End If
            End If

            Me.StoryBoard.Pause(Me.Content)
            Me.State = ControlState.Paused
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Waiting_WaitResumed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.WaitResumed
        Try
            If Me.ThrowsErrorOnInvalidOperation Then
                If Me.State <> ControlState.Paused Then
                    Throw New InvalidOperationException("Control can be resumed only when it is paused")
                End If
            End If

            Me.StoryBoard.Resume(Me.Content)
            Me.State = ControlState.Resumed
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

#End Region

#Region "Enums "

    Public Enum ControlState
        Running = 0
        Stopped = 1
        Paused = 2
        Resumed = 3
        Ready = 4
    End Enum

#End Region

End Class
