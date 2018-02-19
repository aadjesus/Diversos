Imports System
Imports System.Runtime.InteropServices
Imports System.Windows.Interop

Friend Class VistaAeroAPI

#Region " Private Declarations "

    Private Declare Sub DwmExtendFrameIntoClientArea Lib "dwmapi.dll" (ByVal hWnd As IntPtr, ByRef margins As MARGIN_STRUCT)
    Private Declare Function DwmIsCompositionEnabled Lib "dwmapi" (ByRef pfEnabled As Long) As Long

    Private Structure MARGIN_STRUCT

        Dim Left As Integer
        Dim Right As Integer
        Dim Top As Integer
        Dim Bottom As Integer

        Sub New(ByVal intLeft As Integer, ByVal intRight As Integer, ByVal intTop As Integer, ByVal intBottom As Integer)
            Left = intLeft
            Right = intRight
            Top = intTop
            Bottom = intBottom
        End Sub

        Sub New(ByVal t As Thickness)
            Left = CType(t.Left, Integer)
            Right = CType(t.Right, Integer)
            Top = CType(t.Top, Integer)
            Bottom = CType(t.Bottom, Integer)
        End Sub

    End Structure

#End Region

#Region " Methods "

    ''' <summary>
    ''' Makes any WPF window a full Aero Glass window
    ''' </summary>
    ''' <param name="window">Pass the window object.  (Me)</param>
    ''' <returns>Boolan</returns>
    ''' <remarks>Very cool</remarks>
    Friend Shared Function ExtendGlassFrame(ByVal window As Window) As Boolean
        Return ExtendGlassFrame(window, New Thickness(-1))
    End Function

    ''' <summary>
    ''' Makes any WPF window an Aero Glass window
    ''' </summary>
    ''' <param name="window">Pass the window object.  (Me)</param>
    ''' <param name="margin">Set margins to extend the glass into or to make the window all glass pass : New Thickness(-1)</param>
    ''' <returns>Boolan</returns>
    ''' <remarks>Very cool</remarks>
    Friend Shared Function ExtendGlassFrame(ByVal window As Window, ByVal margin As Thickness) As Boolean

        Dim lngReply As Long
        Dim lngResult As Long = DwmIsCompositionEnabled(lngReply)

        If lngResult <> 0 OrElse lngReply <> 1 Then
            Return False
        End If

        Dim hwnd As IntPtr = New WindowInteropHelper(window).Handle

        If hwnd = IntPtr.Zero Then
            Throw New InvalidOperationException("The Window must be shown before extending glass.")
        End If

        window.Background = Brushes.Transparent
        HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent

        Dim margins As New MARGIN_STRUCT(margin)
        DwmExtendFrameIntoClientArea(hwnd, margins)

        Return True
    End Function

    Friend Shared Function IsDwmIsCompositionEnabled() As Boolean

        Dim lngReply As Long
        Dim lngResult As Long = DwmIsCompositionEnabled(lngReply)

        If lngResult <> 0 OrElse lngReply <> 1 Then
            Return False

        Else
            Return True
        End If

    End Function

#End Region

End Class
