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
Imports SH = Servitium.Shared

Partial Public Class WindowMgr
    Public Event WindowMaximized As EventHandler

    Private m_StartPoint As Point ' Where did the mouse start off from?
    Private m_OriginalLeft As Double ' What was the element's original x offset?
    Private m_OriginalTop As Double ' What was the element's original y offset?
    Private m_IsDown As Boolean ' Is the mouse down right now?
    Private m_IsDragging As Boolean ' Are we actually dragging the shape around?
    Private m_OriginalElement As UIElement ' What is it that we're dragging?
    Private m_OverlayElement As DropPreviewAdorner
    Private m_OverlayRect As Rectangle
    Private m_CurrentRow As Integer = 0
    Private m_CurrentCol As Integer = 0
    Private m_WindowList As New ArrayList
    Private m_MaxCol As Integer = 3
    Friend m_WindowWidth As Double = 0
    Friend m_WindowHeight As Double = 0
    Private m_numCols As Integer
    Private m_numRows As Integer
    Private m_WindowGap As Integer = 5
    Private m_CurrentDropIndex As Integer
    Private m_pathAnimSB As Storyboard


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

    Private Sub AnimateScaleUp(ByVal img As Window, ByVal toX As Double, ByVal toY As Double)
        Dim ScaleTransform As New ScaleTransform
        img.RenderTransform = ScaleTransform
        Me.RegisterName("ScaleTransform", img)
        Dim tempWidth As Double
        Dim time As Double = 1
        Dim dblAnim As New DoubleAnimationUsingKeyFrames()
        dblAnim.BeginTime = New TimeSpan(0)
        dblAnim.KeyFrames.Add(New SplineDoubleKeyFrame(img.ActualWidth, KeyTime.FromTimeSpan(New TimeSpan(0, 0, 0, 0, 0))))
        tempWidth = img.ActualWidth
        For i As Integer = 20 To 1 Step -1
            tempWidth = SH.WPFtween.WPFpenner.easeInOutElastic(time / i, img.ActualWidth, toX, time)
            If tempWidth > toX + 250 Then tempWidth = toX + 250
            dblAnim.KeyFrames.Add(New SplineDoubleKeyFrame(tempWidth, KeyTime.FromTimeSpan(New TimeSpan(0, 0, 0, 0, CInt((time / i) * 1000)))))
        Next
        dblAnim.KeyFrames.Add(New SplineDoubleKeyFrame(toX, KeyTime.FromTimeSpan(New TimeSpan(0, 0, 0, 1, 0))))

        Dim tempHeight As Double
        Dim dblAnimY As New DoubleAnimationUsingKeyFrames()
        dblAnimY.BeginTime = New TimeSpan(0)
        dblAnimY.KeyFrames.Add(New SplineDoubleKeyFrame(img.ActualHeight, KeyTime.FromTimeSpan(New TimeSpan(0, 0, 0, 0, 0))))
        tempHeight = img.ActualHeight
        For i As Integer = 20 To 1 Step -1
            tempHeight = SH.WPFtween.WPFpenner.easeInOutElastic(time / i, img.ActualHeight, toY, time)
            If tempHeight > toY Then tempHeight = toY
            dblAnimY.KeyFrames.Add(New SplineDoubleKeyFrame(tempWidth, KeyTime.FromTimeSpan(New TimeSpan(0, 0, 0, 0, CInt((time / i) * 1000)))))
        Next
        dblAnimY.KeyFrames.Add(New SplineDoubleKeyFrame(toY, KeyTime.FromTimeSpan(New TimeSpan(0, 0, 0, 1, 0))))

        Storyboard.SetTargetName(dblAnim, "ScaleTransform")
        Storyboard.SetTargetProperty(dblAnim, New PropertyPath(img.WidthProperty))
        Storyboard.SetTargetName(dblAnimY, "ScaleTransform")
        Storyboard.SetTargetProperty(dblAnimY, New PropertyPath(img.HeightProperty))

        Dim sb As New Storyboard()
        'sb.AccelerationRatio = 0.5
        'sb.DecelerationRatio = 0.5
        sb.Children.Add(dblAnim)
        sb.Children.Add(dblAnimY)
        sb.Begin(Me)
        Me.UnregisterName("ScaleTransform")
    End Sub

    Private Sub UpdLayout()
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
            CurrentWindow = DirectCast(m_WindowList(i), Window)
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
            'AnimateScaleUp(CurrentWindow, m_WindowWidth - 5, m_WindowHeight)
            CurrentWindow.Width = m_WindowWidth - 5
            CurrentWindow.Height = m_WindowHeight

            If i > NormalWindowContainer.Children.IndexOf(CurrentWindow) Then
                NormalWindowContainer.Children.Remove(CurrentWindow)
                NormalWindowContainer.Children.Insert(i - 1, CurrentWindow)
            ElseIf i < NormalWindowContainer.Children.IndexOf(CurrentWindow) Then
                NormalWindowContainer.Children.Remove(CurrentWindow)
                NormalWindowContainer.Children.Insert(i, CurrentWindow)
            End If
            Canvas.SetZIndex(CurrentWindow, i)
            'endPoint = New Point((m_WindowWidth * curCol) + curCol * m_WindowGap, (m_WindowHeight * curRow) + curRow * m_WindowGap)
            'If Not startPoint.Equals(endPoint) Then
            '    AnimateMove(CurrentWindow, New Point(DirectCast(CurrentWindow.RenderTransform.Value, Matrix).OffsetX, DirectCast(CurrentWindow.RenderTransform.Value, Matrix).OffsetY), endPoint)
            'End If
            Canvas.SetLeft(CurrentWindow, (m_WindowWidth * curCol) + curCol * m_WindowGap)
            Canvas.SetTop(CurrentWindow, (m_WindowHeight * curRow) + curRow * m_WindowGap)
            CurrentWindow._WindowState = WindowState.Restore
            CurrentWindow.cmdMaximizeRestore.Content = "M"
            'CurrentWindow.cmdMaximizeRestore.Style = DirectCast(FindResource("MaximizeButtonStyle"), Style)
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
        NameScope.SetNameScope(Me, New NameScope())

        Dim TransTransform As New TranslateTransform
        windowToMove.RenderTransform = TransTransform
        Me.RegisterName("TranslateTransform", TransTransform)
        Dim animationPath As New PathGeometry()
        Dim pFigure As New PathFigure()
        pFigure.StartPoint = startLoc 'windowToMove.TranslatePoint(New Point(0, 0), NormalWindowContainer)

        Dim pLineSeg As New PolyLineSegment()
        pLineSeg.Points.Add(loc)
        pFigure.Segments.Add(pLineSeg)
        animationPath.Figures.Add(pFigure)
        Dim translateXAnimation As New DoubleAnimationUsingPath
        translateXAnimation.PathGeometry = animationPath
        translateXAnimation.Duration = TimeSpan.FromSeconds(1)
        translateXAnimation.AccelerationRatio = 1
        translateXAnimation.Source = PathAnimationSource.X
        Storyboard.SetTargetName(translateXAnimation, "TranslateTransform")
        Storyboard.SetTargetProperty(translateXAnimation, New PropertyPath(TranslateTransform.XProperty))

        Dim translateYAnimation As New DoubleAnimationUsingPath()
        translateYAnimation.PathGeometry = animationPath
        translateYAnimation.Duration = TimeSpan.FromSeconds(1)
        translateYAnimation.Source = PathAnimationSource.Y
        translateYAnimation.AccelerationRatio = 1
        Storyboard.SetTargetName(translateYAnimation, "TranslateTransform")
        Storyboard.SetTargetProperty(translateYAnimation, New PropertyPath(TranslateTransform.YProperty))

        Dim pathAnimationStoryboard As New Storyboard
        If startLoc.X <> loc.X Then
            pathAnimationStoryboard.Children.Add(translateXAnimation)
        End If
        If startLoc.Y <> loc.Y Then
            pathAnimationStoryboard.Children.Add(translateYAnimation)
        End If
        m_pathAnimSB = pathAnimationStoryboard
        pathAnimationStoryboard.DecelerationRatio = 0.3
        If pathAnimationStoryboard.Children.Count > 0 Then
            pathAnimationStoryboard.Begin(Me, HandoffBehavior.Compose)
            AddHandler pathAnimationStoryboard.Completed, AddressOf windowToMove.SetMovingToFalse
        End If
        Me.UnregisterName("TranslateTransform")
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
        Dim elemPoint As Point
        Dim dropIndex As Integer
        Dim MoveToLoc As Point
        Dim StartFromLoc As Point
        Dim TempWindow As Window
        Dim wid As Double = 0
        Dim hei As Double = 0
        Dim orgIndex As Integer = DirectCast(m_OriginalElement, Window).Index
        Dim TempElement As Window

        Canvas.SetLeft(m_OriginalElement, currentPosition.X - m_StartPoint.X)
        Canvas.SetTop(m_OriginalElement, currentPosition.Y - m_StartPoint.Y)
        NormalWindowContainer.UpdateLayout()
        HitTestElement(currentPosition, TestHitElement, dropIndex)
        If TestHitElement Is Nothing OrElse Not TestHitElement.GetType.Equals(GetType(Window)) Then
            Return
        End If
        If DirectCast(TestHitElement, Window).Index <> DirectCast(m_OriginalElement, Window).Index _
        Then
            Row = Math.DivRem(dropIndex, m_numCols, Col)
            OrgRow = Math.DivRem(orgIndex, m_numCols, OrgCol)
            DirectCast(m_OriginalElement, Window).Index = dropIndex
            DirectCast(TestHitElement, Window).Index = orgIndex
            'wid = (m_WindowWidth + m_WindowGap) * (Col)
            'hei = (m_WindowHeight + m_WindowGap) * (Row)
            StartFromLoc = New Point(Canvas.GetLeft(TestHitElement), Canvas.GetTop(TestHitElement))    '  wid, hei)
            'AnimateMove(m_OverlayRect, MoveToLoc, StartFromLoc)  'TestHitElement.TranslatePoint(New Point(0, 0), NormalWindowContainer))
            wid = (m_WindowWidth + m_WindowGap) * (OrgCol)
            hei = (m_WindowHeight + m_WindowGap) * (OrgRow)
            MoveToLoc = New Point(wid, hei)
            Canvas.SetLeft(m_OverlayRect, StartFromLoc.X)
            Canvas.SetTop(m_OverlayRect, StartFromLoc.Y)
            DirectCast(TestHitElement, Window)._IsMoving = True
            'AnimateMove(TestHitElement, StartFromLoc, MoveToLoc)  'm_OverlayRect.TranslatePoint(New Point(0, 0), NormalWindowContainer))
            Canvas.SetLeft(TestHitElement, MoveToLoc.X)
            Canvas.SetTop(TestHitElement, MoveToLoc.Y)
            Canvas.SetZIndex(TestHitElement, orgIndex)
            NormalWindowContainer.UpdateLayout()
            m_CurrentDropIndex = dropIndex
        End If

    End Sub

    Private Function GetElementAtIndex(ByVal index As Integer) As Window
        Dim elem As Window = Nothing
        For i As Integer = 0 To m_WindowList.Count - 1
            If DirectCast(m_WindowList(i), Window).Index = index Then
                elem = m_WindowList(i)
                Exit For
            End If
        Next
        Return elem
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
            win.Height = NormalWindowContainer.ActualHeight - m_WindowGap
            win.Width = NormalWindowContainer.ActualWidth - m_WindowGap - 5
            Canvas.SetLeft(win, 0)
            Canvas.SetTop(win, 0)
            'Canvas.SetZIndex(win, m_WindowList.Count - 1)
            'AnimateMove(win, New Point(DirectCast(win.RenderTransform.Value, Matrix).OffsetX, DirectCast(win.RenderTransform.Value, Matrix).OffsetY), New Point(0, 0))
            'AnimateScaleUp(win, NormalWindowContainer.ActualWidth - m_WindowGap - 5, NormalWindowContainer.ActualHeight - m_WindowGap)
            win.UpdateLayout()
        ElseIf winState = WindowState.Restore Then
            Canvas.SetZIndex(win, 0)
            win.Height = m_WindowHeight
            win.Width = m_WindowWidth - 5
            row = Math.DivRem(win.Index, m_numCols, col)
            wid = (m_WindowWidth + m_WindowGap) * (col)
            hei = (m_WindowHeight + m_WindowGap) * (row)
            Canvas.SetLeft(win, wid)
            Canvas.SetTop(win, hei)
            'AnimateScaleUp(win, m_WindowWidth - m_WindowGap - 5, m_WindowHeight - m_WindowGap)
            UpdLayout()
        ElseIf winState = WindowState.Close Then
            NormalWindowContainer.Children.Remove(win)
            win.Visibility = Windows.Visibility.Hidden
            m_WindowList.Remove(win)
            UpdLayout()
            NormalWindowContainer.UpdateLayout()
        End If
        NormalWindowContainer.UpdateLayout()

    End Sub

#Region "Useful code"
    'If currentPosition.X - m_StartPoint.X < 0 OrElse currentPosition.Y - m_StartPoint.Y < 0 Then
    '    Return
    'End If
    'If (currentPosition.X + (m_WindowWidth - m_StartPoint.X)) > NormalWindowContainer.ActualWidth OrElse (currentPosition.Y + (m_WindowHeight - m_StartPoint.Y)) > NormalWindowContainer.ActualHeight Then
    '    Return
    'End If

    'Else
    '    For i As Integer = 0 To m_WindowList.Count - 1
    '        If i > orgIndex And i <= dropIndex Then
    '            TempElement = GetElementAtIndex(i)
    '            DirectCast(TempElement, Window).Index = orgIndex
    '            Row = Math.DivRem(i, m_numCols, Col)
    '            OrgRow = Math.DivRem(i - 1, m_numCols, OrgCol)
    '            wid = (m_WindowWidth + m_WindowGap) * (Col)
    '            hei = (m_WindowHeight + m_WindowGap) * (Row)
    '            StartFromLoc = New Point(wid, hei)
    '            'AnimateMove(m_OverlayRect, MoveToLoc, StartFromLoc)  'TestHitElement.TranslatePoint(New Point(0, 0), NormalWindowContainer))
    '            wid = (m_WindowWidth + m_WindowGap) * (OrgCol)
    '            hei = (m_WindowHeight + m_WindowGap) * (OrgRow)
    '            MoveToLoc = New Point(wid, hei)
    '            'AnimateMove(TestHitElement, StartFromLoc, MoveToLoc)  'm_OverlayRect.TranslatePoint(New Point(0, 0), NormalWindowContainer))
    '            Canvas.SetLeft(TempElement, MoveToLoc.X)
    '            Canvas.SetTop(TempElement, MoveToLoc.Y)
    '            NormalWindowContainer.UpdateLayout()
    '        End If
    '    Next
    '    Row = Math.DivRem(dropIndex, m_numCols, Col)
    '    wid = (m_WindowWidth + m_WindowGap) * (Col)
    '    hei = (m_WindowHeight + m_WindowGap) * (Row)
    '    StartFromLoc = New Point(wid, hei)
    '    'AnimateMove(m_OverlayRect, MoveToLoc, StartFromLoc)  'TestHitElement.TranslatePoint(New Point(0, 0), NormalWindowContainer))
    '    Canvas.SetLeft(m_OverlayRect, StartFromLoc.X)
    '    Canvas.SetTop(m_OverlayRect, StartFromLoc.Y)
    '    NormalWindowContainer.UpdateLayout()

    'End If

    ' NormaliWindowContainer.UpdateLayout()

#End Region

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Shared _WinMgr As WindowMgr
    Public Shared Function GetWinMgr() As WindowMgr
        If _WinMgr Is Nothing Then
            _WinMgr = New WindowMgr
        End If
        Return _WinMgr
    End Function

End Class
