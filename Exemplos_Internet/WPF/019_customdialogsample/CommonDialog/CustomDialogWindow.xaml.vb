Partial Public Class CustomDialogWindow
    Inherits System.Windows.Window

#Region " Private Declarations "

    Private _bolAeroGlassEnabled As Boolean = False
    Private _enumCustomDialogResult As CustomDialogResults = CustomDialogResults.None
    Private _intButtonsDisabledDelay As Integer
    Private _objButtonDelayTimer As System.Windows.Forms.Timer

#End Region

#Region " Public Properties "

    Public ReadOnly Property CustomDialogResult() As CustomDialogResults
        Get
            Return _enumCustomDialogResult
        End Get
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal intButtonsDisabledDelay As Integer)
        InitializeComponent()

        If System.Environment.OSVersion.Version.Major < 6 Then
            Me.AllowsTransparency = True
            _bolAeroGlassEnabled = False

        Else
            _bolAeroGlassEnabled = True
        End If

        _intButtonsDisabledDelay = intButtonsDisabledDelay

    End Sub

#End Region

#Region " Methods "

    Protected Overrides Sub OnSourceInitialized(ByVal e As System.EventArgs)
        MyBase.OnSourceInitialized(e)

        If _bolAeroGlassEnabled = False Then
            'no aero glass
            Me.borderCustomDialog.Background = System.Windows.SystemColors.ActiveCaptionBrush
            Me.tbCaption.Foreground = System.Windows.SystemColors.ActiveCaptionTextBrush
            Me.borderCustomDialog.CornerRadius = New CornerRadius(10, 10, 0, 0)
            Me.borderCustomDialog.Padding = New Thickness(4, 0, 4, 4)
            Me.borderCustomDialog.BorderThickness = New Thickness(0, 0, 1, 1)
            Me.borderCustomDialog.BorderBrush = System.Windows.Media.Brushes.Black

        Else

            'aero glass
            If VistaAeroAPI.ExtendGlassFrame(Me, New Thickness(0, 25, 0, 0)) = False Then
                'aero didn't work make window without glass
                Me.borderCustomDialog.Background = System.Windows.SystemColors.ActiveCaptionBrush
                Me.tbCaption.Foreground = System.Windows.SystemColors.ActiveCaptionTextBrush
                Me.borderCustomDialog.Padding = New Thickness(4, 0, 4, 4)
                Me.borderCustomDialog.BorderThickness = New Thickness(0, 0, 1, 1)
                Me.borderCustomDialog.BorderBrush = System.Windows.Media.Brushes.Black

                _bolAeroGlassEnabled = False
            End If

        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnCancel.Click
        _enumCustomDialogResult = CustomDialogResults.Cancel
        Me.DialogResult = True
    End Sub

    Private Sub btnNo_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnNo.Click
        _enumCustomDialogResult = CustomDialogResults.No
        Me.DialogResult = True
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnOK.Click
        _enumCustomDialogResult = CustomDialogResults.OK
        Me.DialogResult = True
    End Sub

    Private Sub btnYes_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnYes.Click
        _enumCustomDialogResult = CustomDialogResults.Yes
        Me.DialogResult = True
    End Sub

    Private Sub CustomDialogWindow_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing

        'this prevents ALT-F4 from closing the dialog box
        If Me.DialogResult.HasValue AndAlso Me.DialogResult.Value = True Then
            e.Cancel = False

        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub CustomDialogWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Me.tbAdditionalDetailsText.Visibility = Windows.Visibility.Collapsed

        If Me.ResizeMode <> Windows.ResizeMode.NoResize Then
            'this work around is necessary when glass is enabled and the window style is None which removes the chrome because the resize mode MUST be set to CanResize or else glass won't display
            Me.MinHeight = Me.ActualHeight
            Me.MaxHeight = Me.ActualHeight

            Me.MinWidth = Me.ActualWidth
            Me.MaxWidth = Me.ActualWidth
        End If

        If _intButtonsDisabledDelay > 0 Then
            Me.pbDisabledButtonsProgressBar.Maximum = _intButtonsDisabledDelay
            Me.pbDisabledButtonsProgressBar.IsIndeterminate = False

            Dim objDuration As New Duration(TimeSpan.FromSeconds(_intButtonsDisabledDelay))
            Dim objDoubleAnimation As New System.Windows.Media.Animation.DoubleAnimation(_intButtonsDisabledDelay, objDuration)
            Me.pbDisabledButtonsProgressBar.BeginAnimation(ProgressBar.ValueProperty, objDoubleAnimation)
            btnCancel.IsEnabled = False
            btnNo.IsEnabled = False
            btnOK.IsEnabled = False
            btnYes.IsEnabled = False
            _objButtonDelayTimer = New System.Windows.Forms.Timer
            AddHandler _objButtonDelayTimer.Tick, AddressOf OnTimedEvent
            _objButtonDelayTimer.Interval = _intButtonsDisabledDelay * 1000
            _objButtonDelayTimer.Start()

        Else
            Me.pbDisabledButtonsProgressBar.Visibility = Windows.Visibility.Collapsed
        End If

    End Sub

    Private Sub expAdditionalDetails_Collapsed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles expAdditionalDetails.Collapsed
        Me.expAdditionalDetails.Header = "See Details"
        Me.tbAdditionalDetailsText.Visibility = Windows.Visibility.Collapsed
        Me.UpdateLayout()

        If Me.ResizeMode <> Windows.ResizeMode.NoResize Then
            Me.MaxHeight = Me.ActualHeight
        End If

    End Sub

    Private Sub expAdditionalDetails_Expanded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles expAdditionalDetails.Expanded

        If Me.ResizeMode <> Windows.ResizeMode.NoResize Then
            Me.MaxHeight = Double.PositiveInfinity
        End If

        Me.expAdditionalDetails.Header = "Hide Details"
        Me.tbAdditionalDetailsText.Visibility = Windows.Visibility.Visible
        Me.UpdateLayout()

        If Me.ResizeMode <> Windows.ResizeMode.NoResize Then
            Me.MaxHeight = Me.ActualHeight
        End If

    End Sub

    Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
        _objButtonDelayTimer.Stop()
        _objButtonDelayTimer.Dispose()
        _objButtonDelayTimer = Nothing
        btnCancel.IsEnabled = True
        btnNo.IsEnabled = True
        btnOK.IsEnabled = True
        btnYes.IsEnabled = True
        Me.pbDisabledButtonsProgressBar.Visibility = Windows.Visibility.Collapsed
    End Sub

    Private Sub tbCaption_MouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles tbCaption.MouseLeftButtonDown
        DragMove()
    End Sub

#End Region

End Class
