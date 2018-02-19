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
Imports System.Collections
Imports System.Collections.Generic
Imports LookOrFeel.Animation

Partial Public Class WindowMgr
    Public Event WindowMaximized As EventHandler

    Private m_StartPoint As Point ' Where did the mouse start off from?
    Private m_OriginalLeft As Double ' What was the element's original x offset?
    Private m_OriginalTop As Double ' What was the element's original y offset?
    Private m_IsDown As Boolean ' Is the mouse down right now?
    Private m_IsDragging As Boolean ' Are we actually dragging the shape around?
    Private m_OriginalElement As UIElement ' What is it that we're dragging?
    Private m_OverlayRect As Rectangle
    Private m_CurrentRow As Integer = 0
    Private m_CurrentCol As Integer = 0
    Private m_WindowList As New List(Of Window)
    Private m_MaxCol As Integer = 3
    Friend m_WindowWidth As Double = 0
    Friend m_WindowHeight As Double = 0
    Private m_numCols As Integer
    Private m_numRows As Integer
    Private m_WindowGap As Integer = 5
    Private m_CurrentDropIndex As Integer
    Private m_movingWindow As Window

    Public Sub AddWindow(Optional ByVal Title As String = "", Optional ByVal controlToAdd As UIElement = Nothing, Optional ByVal winState As WindowState = WindowState.Restore)
        Dim NewWindow As New Window
        NewWindow.Title = Title
        NewWindow.Margin = New Thickness(2)
        NewWindow.Visibility = Windows.Visibility.Visible
        NewWindow._Index = m_WindowList.Count
        NewWindow._Parent = Me
        If Not controlToAdd Is Nothing Then
            NewWindow.AddUIElement(controlToAdd)
        End If
        m_WindowList.Add(NewWindow)
        NormalWindowContainer.Children.Add(NewWindow)
        AddHandler NewWindow.WindowDragStart, AddressOf WindowDragEnter
        UpdLayout()
        If winState = WindowState.Maximized Then
            OnWindowStateChanged(NewWindow, winState)
        End If
        AddHandler NewWindow.WindowStateChanged, AddressOf Me.OnWindowStateChanged
    End Sub

    Private Sub AnimateScaleUp(ByVal img As Window, ByVal toX As Double, ByVal toY As Double, _
                            ByVal toWidth As Double, ByVal toHeight As Double)

        Animator.AnimatePenner(img, Canvas.TopProperty, _
                                       PennerDoubleAnimation.Equations.CubicEaseOut, toY, 500, Nothing)

        Animator.AnimatePenner(img, Canvas.LeftProperty, _
                               PennerDoubleAnimation.Equations.CubicEaseOut, toX, 500, Nothing)

        Animator.AnimatePenner(img, FrameworkElement.HeightProperty, _
                               PennerDoubleAnimation.Equations.CubicEaseOut, toHeight, 500, Nothing)
        Animator.AnimatePenner(img, FrameworkElement.WidthProperty, _
                               PennerDoubleAnimation.Equations.CubicEaseOut, toWidth, 500, Nothing)

    End Sub

    Private Sub UpdLayout(Optional ByVal Animate As Boolean = False)
        If m_WindowList.Count <= 0 Then
            Return
        End If
        Dim len As Integer = m_WindowList.Count
        Dim sqrt As Integer = Math.Floor(Math.Sqrt(len))
        Dim numCols As Integer = Math.Ceiling(len / sqrt)
        Dim numRows As Integer = Math.Ceiling(len / numCols)
        m_numCols = numCols
        m_numRows = numRows
        Dim colNumber As Integer = 0
        Dim rowNumber As Integer = 0
        Dim CurrentWindow As Window
        Dim startPoint As Point
        Dim endPoint As Point
        m_WindowWidth = Math.Round(NormalWindowContainer.ActualWidth / numCols - (m_WindowGap * (numCols - 1)) / numCols)
        m_WindowHeight = Math.Round(NormalWindowContainer.ActualHeight / numRows - (m_WindowGap * (numRows - 1)) / numRows)
        Dim curRow As Integer = 0
        Dim curCol As Integer = 0
        For i As Integer = 0 To m_WindowList.Count - 1
            CurrentWindow = FindWindowAtIndex(i)
            CurrentWindow.Visibility = Windows.Visibility.Visible
            startPoint = New Point
            If Double.IsNaN(Canvas.GetLeft(CurrentWindow)) Then
                startPoint.X = 0
            Else
                startPoint.X = Canvas.GetLeft(CurrentWindow)
            End If
            If Double.IsNaN(Canvas.GetTop(CurrentWindow)) Then
                startPoint.Y = 0
            Else
                startPoint.Y = Canvas.GetTop(CurrentWindow)
            End If
            CurrentWindow.Width = m_WindowWidth - 5
            CurrentWindow.Height = m_WindowHeight

            Canvas.SetZIndex(CurrentWindow, i)
            If Animate Then
                endPoint = New Point((m_WindowWidth * curCol) + curCol * m_WindowGap, _
                                (m_WindowHeight * curRow) + curRow * m_WindowGap)
                AnimateMove(CurrentWindow, startPoint, endPoint)
            Else
                Canvas.SetLeft(CurrentWindow, (m_WindowWidth * curCol) + curCol * m_WindowGap)
                Canvas.SetTop(CurrentWindow, (m_WindowHeight * curRow) + curRow * m_WindowGap)
            End If
            CurrentWindow._WindowState = WindowState.Restore
            CurrentWindow.cmdMaximizeRestore.Content = "M"

            CurrentWindow.UpdateLayout()
            If curCol = (m_numCols - 1) Then
                curCol = 0
                curRow += 1
            Else
                curCol += 1
            End If
        Next
        NormalWindowContainer.UpdateLayout()

    End Sub

    Private Sub WindowDragEnter(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        If NormalWindowContainer Is e.Source Then
            Exit Sub
        End If
        m_StartPoint = e.GetPosition(e.Source)
        If DirectCast(sender, Window).WindowState = WindowState.Maximized Then
            m_IsDown = False
            m_IsDragging = False
            Return
        End If
        m_IsDown = True
        m_OriginalElement = sender
        Canvas.SetZIndex(m_OriginalElement, 100)
        NormalWindowContainer.UpdateLayout()
        m_CurrentDropIndex = DirectCast(m_OriginalElement, Window).Index
    End Sub

    Private Sub NormalWindowContainer_PreviewMouseLeftButtonUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles NormalWindowContainer.PreviewMouseLeftButtonUp
        If m_IsDown And m_IsDragging Then
            DragFinished(False)
            e.Handled = True
            NormalWindowContainer.ReleaseMouseCapture()
        End If
        m_IsDown = False
    End Sub

    Private Sub NormalWindowContainer_PreviewMouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles NormalWindowContainer.PreviewMouseMove
        If Not m_IsDown Then
            Return
        End If
        Dim row As Integer
        Dim col As Integer
        row = Math.DivRem(DirectCast(m_OriginalElement, Window).Index, m_numCols, col)
        If Not m_IsDragging AndAlso ((e.MouseDevice.GetPosition(NormalWindowContainer).X - (col * (m_WindowWidth + m_WindowGap) + m_StartPoint.X)) > 5 _
            OrElse (e.MouseDevice.GetPosition(NormalWindowContainer).Y - (row * (m_WindowWidth + m_WindowGap) + m_StartPoint.Y)) > 10) Then
            DragStarted()
            m_IsDragging = True
            NormalWindowContainer.CaptureMouse()
        End If
        If m_IsDragging Then
            DragMoved(e.GetPosition(NormalWindowContainer))
        End If
    End Sub

    Private Sub Window1_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles Me.PreviewKeyDown
        If e.Key = Input.Key.Escape AndAlso m_IsDragging Then
            DragFinished(True)
        End If
    End Sub

    Private Sub DragStarted()
        m_IsDragging = True
        If Double.IsNaN(Canvas.GetLeft(m_OriginalElement)) Then
            m_OriginalLeft = 0
        Else
            m_OriginalLeft = Canvas.GetLeft(m_OriginalElement)
        End If
        If Double.IsNaN(Canvas.GetTop(m_OriginalElement)) Then
            m_OriginalTop = 0
        Else
            m_OriginalTop = Canvas.GetTop(m_OriginalElement)
        End If
        m_OverlayRect = New Rectangle()
        m_OverlayRect.Height = DirectCast(m_OriginalElement, Window).Height
        m_OverlayRect.Width = DirectCast(m_OriginalElement, Window).Width
        Canvas.SetLeft(m_OverlayRect, Canvas.GetLeft(m_OriginalElement))
        Canvas.SetTop(m_OverlayRect, Canvas.GetTop(m_OriginalElement))
        m_OverlayRect.StrokeThickness = 2
        m_OverlayRect.Stroke = Brushes.Gray
        NormalWindowContainer.Children.Add(m_OverlayRect)
        m_OverlayRect.Visibility = Windows.Visibility.Visible
        Canvas.SetZIndex(m_OverlayRect, 0)
        NormalWindowContainer.CaptureMouse()
    End Sub

    Private Sub DragFinished(ByVal canceled As Boolean)
        NormalWindowContainer.ReleaseMouseCapture()
        System.Windows.Input.Mouse.Capture(Nothing)
        If m_IsDragging Then
            Canvas.SetLeft(m_OriginalElement, Canvas.GetLeft(m_OverlayRect))
            Canvas.SetTop(m_OriginalElement, Canvas.GetTop(m_OverlayRect))
            Canvas.SetZIndex(m_OriginalElement, DirectCast(m_OriginalElement, Window).Index)
            NormalWindowContainer.Children.Remove(m_OverlayRect)
            m_OverlayRect.Visibility = Windows.Visibility.Hidden
            m_OverlayRect = Nothing
        End If
        m_IsDragging = False
        m_IsDown = False
    End Sub

    Private Sub AnimateMove(ByVal windowToMove As Window, ByVal startLoc As Point, ByVal loc As Point)

        Animator.AnimatePenner(windowToMove, Canvas.TopProperty, _
                                       PennerDoubleAnimation.Equations.CubicEaseOut, startLoc.Y, loc.Y, 500, Nothing)

        Animator.AnimatePenner(windowToMove, Canvas.LeftProperty, _
                               PennerDoubleAnimation.Equations.CubicEaseOut, startLoc.X, loc.X, 500, AddressOf windowToMove.OnAnimationComplete)

        Return
    End Sub

    Private Sub HitTestElement(ByVal currentPosition As Point, ByRef TestHitElement As UIElement, ByRef dropIndex As Integer)
        Dim elemPoint As Point
        For Each elem As UIElement In NormalWindowContainer.Children
            elemPoint = elem.TranslatePoint(New Point(0, 0), NormalWindowContainer)
            If elemPoint.X < currentPosition.X AndAlso (elemPoint.X + m_WindowWidth) > currentPosition.X _
                AndAlso elemPoint.Y < currentPosition.Y AndAlso (elemPoint.Y + m_WindowHeight) > currentPosition.Y Then
                TestHitElement = elem
                If elem.GetType().Equals(GetType(Window)) Then
                    dropIndex = DirectCast(TestHitElement, Window).Index
                End If
                If dropIndex <> m_CurrentDropIndex Then
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub DragMoved(ByVal currentPosition As Point)
        Dim TestHitElement As UIElement
        Dim Row As Integer
        Dim Col As Integer
        Dim OrgRow As Integer
        Dim OrgCol As Integer
        Dim dropIndex As Integer
        Dim StartFromLoc As Point
        Dim wid As Double = 0
        Dim hei As Double = 0
        Dim orgIndex As Integer = DirectCast(m_OriginalElement, Window).Index

        Canvas.SetLeft(m_OriginalElement, currentPosition.X - m_StartPoint.X)
        Canvas.SetTop(m_OriginalElement, currentPosition.Y - m_StartPoint.Y)
        NormalWindowContainer.UpdateLayout()
        HitTestElement(currentPosition, TestHitElement, dropIndex)
        If TestHitElement Is Nothing OrElse Not TestHitElement.GetType.Equals(GetType(Window)) Then
            Return
        End If
        If DirectCast(TestHitElement, Window).Index <> DirectCast(m_OriginalElement, Window).Index _
        Then
            If DirectCast(TestHitElement, Window)._IsMoving Then
                Return
            End If
            Row = Math.DivRem(dropIndex, m_numCols, Col)
            OrgRow = Math.DivRem(orgIndex, m_numCols, OrgCol)
            wid = (m_WindowWidth + m_WindowGap) * (Col)
            hei = (m_WindowHeight + m_WindowGap) * (Row)
            StartFromLoc = New Point(Canvas.GetLeft(TestHitElement), Canvas.GetTop(TestHitElement))    '  wid, hei)
            Canvas.SetLeft(m_OverlayRect, StartFromLoc.X)
            Canvas.SetTop(m_OverlayRect, StartFromLoc.Y)
            PushWindows(orgIndex, dropIndex)
            DirectCast(m_OriginalElement, Window).Index = dropIndex
            m_movingWindow = DirectCast(TestHitElement, Window)
            Canvas.SetZIndex(TestHitElement, orgIndex)
            NormalWindowContainer.UpdateLayout()
            m_CurrentDropIndex = dropIndex
        End If
    End Sub

    Private Sub PushWindows(ByVal StartIndex As Integer, ByVal EndIndex As Integer)
        Dim Row As Integer
        Dim Col As Integer
        Dim CurrentWindow As Window
        Dim NextWindow As Window
        Dim wid As Double = 0
        Dim hei As Double = 0
        Dim startLoc As Point
        Dim endLoc As Point
        If StartIndex < EndIndex Then
            CurrentWindow = FindWindowAtIndex(EndIndex)
            For currentIndex As Integer = EndIndex To StartIndex + 1 Step -1

                Row = Math.DivRem(currentIndex, m_numCols, Col)
                wid = (m_WindowWidth + m_WindowGap) * (Col)
                hei = (m_WindowHeight + m_WindowGap) * (Row)
                startLoc = New Point(wid, hei)
                Row = Math.DivRem(currentIndex - 1, m_numCols, Col)
                wid = (m_WindowWidth + m_WindowGap) * (Col)
                hei = (m_WindowHeight + m_WindowGap) * (Row)
                endLoc = New Point(wid, hei)
                CurrentWindow._IsMoving = True
                Canvas.SetLeft(CurrentWindow, endLoc.X)
                Canvas.SetTop(CurrentWindow, endLoc.Y)
                AnimateMove(CurrentWindow, startLoc, endLoc)
                NextWindow = FindWindowAtIndex(currentIndex - 1)
                CurrentWindow.Index -= 1
                CurrentWindow = NextWindow
            Next
        Else
            CurrentWindow = FindWindowAtIndex(EndIndex)
            For currentIndex As Integer = EndIndex To StartIndex - 1

                Row = Math.DivRem(currentIndex, m_numCols, Col)
                wid = (m_WindowWidth + m_WindowGap) * (Col)
                hei = (m_WindowHeight + m_WindowGap) * (Row)
                startLoc = New Point(wid, hei)
                Row = Math.DivRem(currentIndex + 1, m_numCols, Col)
                wid = (m_WindowWidth + m_WindowGap) * (Col)
                hei = (m_WindowHeight + m_WindowGap) * (Row)
                endLoc = New Point(wid, hei)
                CurrentWindow._IsMoving = True
                Canvas.SetLeft(CurrentWindow, endLoc.X)
                Canvas.SetTop(CurrentWindow, endLoc.Y)
                AnimateMove(CurrentWindow, startLoc, endLoc)
                NextWindow = FindWindowAtIndex(currentIndex + 1)
                CurrentWindow.Index += 1
                CurrentWindow = NextWindow
            Next
        End If
    End Sub

    Private Function FindWindowAtIndex(ByVal index As Integer) As Window
        For i As Integer = 0 To Me.m_WindowList.Count - 1
            If m_WindowList(i).Index = index Then
                Return m_WindowList(i)
            End If
        Next
        Return Nothing
    End Function

    Private Sub OnWindowStateChanged(ByVal win As Window, ByVal winState As WindowState)
        Dim row As Integer
        Dim col As Integer
        Dim wid As Double
        Dim hei As Double
        Dim CurrentWindow As Window
        If winState = WindowState.Maximized Then
            For i As Integer = 0 To m_WindowList.Count - 1
                CurrentWindow = DirectCast(m_WindowList(i), Window)
                CurrentWindow.Visibility = Windows.Visibility.Hidden
            Next
            win.Visibility = Windows.Visibility.Visible
            AnimateScaleUp(win, 0, 0, _
                    NormalWindowContainer.ActualWidth - m_WindowGap - 5, NormalWindowContainer.ActualHeight - m_WindowGap)
            win.UpdateLayout()
        ElseIf winState = WindowState.Restore Then
            Canvas.SetZIndex(win, 0)
            row = Math.DivRem(win.Index, m_numCols, col)
            wid = (m_WindowWidth + m_WindowGap) * (col)
            hei = (m_WindowHeight + m_WindowGap) * (row)
            AnimateScaleUp(win, wid, hei, _
                           m_WindowWidth - m_WindowGap - 5, m_WindowHeight - m_WindowGap)
            UpdLayout()
        ElseIf winState = WindowState.Close Then
            NormalWindowContainer.Children.Remove(win)
            win.Visibility = Windows.Visibility.Hidden
            m_WindowList.Remove(win)
            For i As Integer = 0 To m_WindowList.Count - 1
                m_WindowList(i).Index = i
            Next
            UpdLayout(True)
            NormalWindowContainer.UpdateLayout()
        End If
        NormalWindowContainer.UpdateLayout()

    End Sub

    Private Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Private Shared _WinMgr As WindowMgr
    Public Shared Function GetWinMgr() As WindowMgr
        If _WinMgr Is Nothing Then
            _WinMgr = New WindowMgr
        End If
        Return _WinMgr
    End Function

    Private Sub WindowMgr_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
        UpdLayout(True)
    End Sub
End Class
