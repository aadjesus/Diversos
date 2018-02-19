''' <summary>
''' The main window for the tabbed web browser application.
''' </summary>
Public Class MainWindow

#Region " Variables "

    ''' <summary>
    ''' The difference in width between the window and the address field in the tool bar.
    ''' </summary>
    Private ReadOnly addressFieldSizeOffset As Integer
    ''' <summary>
    ''' The <b>WebBrowser</b> control on the current tab.
    ''' </summary>
    Private WithEvents currentBrowser As WebBrowser

#End Region 'Variables

#Region " Constructors "

    ''' <summary>
    ''' Creates a new instance of the <see cref="MainWindow" /> class.
    ''' </summary>
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'Get the initial difference between the form width and address field width.
        'This value will be maintained as the window width changes.
        Me.addressFieldSizeOffset = Me.Width - Me.addressCombo.Width
    End Sub

#End Region 'Constructors

#Region " Event Handlers "

    Private Sub MainWindow_Load(ByVal sender As Object, _
                                ByVal e As EventArgs) Handles MyBase.Load
        'Add the default tab.
        Me.tabs.AddTab()
        Me.currentBrowser = Me.tabs.SelectedWebBrowser

        If My.Settings.StartWithHomePage AndAlso _
           Not String.IsNullOrEmpty(My.Settings.HomePage) Then
            'Load the home page.
            Me.currentBrowser.Navigate(My.Settings.HomePage)
        End If
    End Sub

    Private Sub MainWindow_SizeChanged(ByVal sender As Object, _
                                       ByVal e As EventArgs) Handles MyBase.SizeChanged
        If Me.addressFieldSizeOffset > 0 Then
            'Maintain the original width difference between the form and the address
            'without allowing the address field to become less than 100 pixels wide.
            Me.addressCombo.Width = Math.Max(100, Me.Width - Me.addressFieldSizeOffset)
        End If
    End Sub

    Private Sub addressCombo_KeyDown(ByVal sender As Object, _
                                     ByVal e As KeyEventArgs) Handles addressCombo.KeyDown
        If e.KeyCode = Keys.Enter Then
            'Navigate to the current address.
            Me.currentBrowser.Navigate(Me.addressCombo.Text)

            'Prevent the sound that indicates an invalid key press.
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub goButton_Click(ByVal sender As Object, _
                               ByVal e As EventArgs) Handles goButton.Click
        'Navigate to the current address.
        Me.currentBrowser.Navigate(Me.addressCombo.Text)
    End Sub

    Private Sub tabs_SelectedIndexChanged(ByVal sender As Object, _
                                          ByVal e As EventArgs) Handles tabs.SelectedIndexChanged
        'Get the current browser.
        Me.currentBrowser = Me.tabs.SelectedWebBrowser

        Me.DisplayPageTitle()

        'Enable the Close Tab command if and only if there is more than one tab.
        If Me.tabs.TabCount = 1 Then
            Me.closeTabMenuItem.Enabled = False
            Me.closeTabButton.Enabled = False
        Else
            Me.closeTabMenuItem.Enabled = True
            Me.closeTabButton.Enabled = True
        End If

        Dim url As Uri = Me.currentBrowser.Url

        If url Is Nothing Then
            'There is no current address.
            Me.addressCombo.Text = String.Empty

            'The user cannot refresh a page that doesn't exist.
            Me.refreshMenuItem.Enabled = False
            Me.refreshButton.Enabled = False
        Else
            'Display the current page address in the address bar.
            Me.addressCombo.Text = url.ToString()

            'Allow the user to refresh the current page.
            Me.refreshMenuItem.Enabled = True
            Me.refreshButton.Enabled = True
        End If

        'Display the current page's status text in the status bar.
        Me.browserStatusTextLabel.Text = Me.currentBrowser.StatusText

        Dim tab As WebBrowserTabPage = Me.tabs.SelectedWebBrowserTabPage
        Dim lastCurrentProgress As Long = tab.LastCurrentProgress
        Dim lastMaximumProgress As Long = tab.LastMaximumProgress

        If lastMaximumProgress > 0L AndAlso lastCurrentProgress > 0L Then
            'Display the current progress.
            Me.pageLoadProgress.Visible = True
            Me.pageLoadProgress.Value = CInt(Math.Round((100 * lastCurrentProgress / lastMaximumProgress)))
        Else
            'Hide and reset the progress bar.
            Me.pageLoadProgress.Visible = False
            Me.pageLoadProgress.Value = 0
        End If
    End Sub

    Private Sub pageLoadProgress_VisibleChanged(ByVal sender As Object, _
                                                ByVal e As EventArgs) Handles pageLoadProgress.VisibleChanged
        Dim canStop As Boolean = Me.pageLoadProgress.Visible

        'The loading can only be stopped when a page is loading.
        Me.stopMenuItem.Enabled = canStop
        Me.stopButton.Enabled = canStop
    End Sub

#Region " Main Menu Event Handlers "

#Region " File Menu Event Handlers "

    Private Sub newTabMenuItem_Click(ByVal sender As Object, _
                                     ByVal e As EventArgs) Handles newTabMenuItem.Click
        Me.tabs.AddTab()
    End Sub

    Private Sub openMenuItem_Click(ByVal sender As Object, _
                                   ByVal e As EventArgs)
        'TODO: Prompt to open an address in the current tab.
    End Sub

    Private Sub closeTabMenuItem_Click(ByVal sender As Object, _
                                       ByVal e As EventArgs) Handles closeTabMenuItem.Click
        Me.tabs.RemoveCurrentTab()
    End Sub

    Private Sub exitMenuItem_Click(ByVal sender As Object, _
                                   ByVal e As EventArgs)
        Application.Exit()
    End Sub

#End Region 'File Menu Event Handlers

#Region " View Menu Event Handlers "

    Private Sub refreshMenuItem_Click(ByVal sender As Object, _
                                      ByVal e As EventArgs) Handles refreshMenuItem.Click
        Me.currentBrowser.Refresh()

        Me.pageLoadProgress.Visible = True
    End Sub

    Private Sub stopMenuItem_Click(ByVal sender As Object, _
                                   ByVal e As EventArgs) Handles stopMenuItem.Click
        Me.currentBrowser.Stop()
    End Sub

#End Region 'View Menu Event Handlers

#Region " History Menu Event Handlers "

    Private Sub backMenuItem_Click(ByVal sender As Object, _
                                   ByVal e As EventArgs) Handles backMenuItem.Click
        Me.currentBrowser.GoBack()
    End Sub

    Private Sub forwardMenuItem_Click(ByVal sender As Object, _
                                      ByVal e As EventArgs) Handles forwardMenuItem.Click
        Me.currentBrowser.GoForward()
    End Sub

    Private Sub homeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles homeMenuItem.Click
        Me.currentBrowser.Navigate(My.Settings.HomePage)
    End Sub

#End Region 'History Menu Event Handlers

#Region " Tools Menu Event Handlers "

    Private Sub optionsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Using dialogue As New OptionsDialogue(Me.currentBrowser.Url.ToString())
            dialogue.ShowDialog()
        End Using
    End Sub

#End Region 'Tools Menu Event Handlers

#End Region 'Main Menu Event Handlers

#Region " Toolbar Event Handlers "

    Private Sub backButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles backButton.Click
        Me.backMenuItem.PerformClick()
    End Sub

    Private Sub forwardButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles forwardButton.Click
        Me.forwardMenuItem.PerformClick()
    End Sub

    Private Sub refreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles refreshButton.Click
        Me.refreshMenuItem.PerformClick()
    End Sub

    Private Sub stopButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles stopButton.Click
        Me.stopMenuItem.PerformClick()
    End Sub

    Private Sub homeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.homeMenuItem.PerformClick()
    End Sub

    Private Sub newTabButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.newTabMenuItem.PerformClick()
    End Sub

    Private Sub closeTabButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles closeTabButton.Click
        Me.closeTabMenuItem.PerformClick()
    End Sub

#End Region 'Toolbar Event Handlers

#Region " Web Browser Event Handlers "

    Private Sub currentBrowser_StatusTextChanged(ByVal sender As Object, _
                                                 ByVal e As EventArgs) Handles currentBrowser.StatusTextChanged
        'Display the current page's status text in the status bar.
        Me.browserStatusTextLabel.Text = Me.currentBrowser.StatusText
    End Sub

    Private Sub currentBrowser_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles currentBrowser.Navigated
        'Display the progress bar.
        Me.pageLoadProgress.Visible = True

        'Display the current page address in the address bar.
        Me.addressCombo.Text = Me.currentBrowser.Url.ToString()
    End Sub

    Private Sub currentBrowser_CanGoBackChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles currentBrowser.CanGoBackChanged
        Dim canGoBack As Boolean = Me.currentBrowser.CanGoBack

        Me.backButton.Enabled = canGoBack
        Me.backMenuItem.Enabled = canGoBack
    End Sub

    Private Sub currentBrowser_CanGoForwardChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles currentBrowser.CanGoForwardChanged
        Dim canGoForward As Boolean = Me.currentBrowser.CanGoForward

        Me.forwardButton.Enabled = canGoForward
        Me.forwardMenuItem.Enabled = canGoForward
    End Sub

    Private Sub currentBrowser_ProgressChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles currentBrowser.ProgressChanged
        Me.pageLoadProgress.Visible = True

        If e.MaximumProgress > 0L AndAlso e.CurrentProgress > 0L Then
            'Display the current progress.
            Me.pageLoadProgress.Value = CInt(Math.Round((100 * e.CurrentProgress / e.MaximumProgress)))
        ElseIf e.MaximumProgress = 0L AndAlso e.CurrentProgress = 0L Then
            'Hide and reset the progress bar.
            Me.pageLoadProgress.Visible = False
            Me.pageLoadProgress.Value = 0
        End If
    End Sub

    Private Sub currentBrowser_DocumentTitleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles currentBrowser.DocumentTitleChanged
        Me.DisplayPageTitle()
    End Sub

    Private Sub currentBrowser_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles currentBrowser.DocumentCompleted
        Me.refreshMenuItem.Enabled = True
        Me.refreshButton.Enabled = True
    End Sub

#End Region 'Web Browser Event Handlers

#End Region 'Event Handlers

#Region " Methods "

    ''' <summary>
    ''' Displays the current page title in the title bar.
    ''' </summary>
    ''' <remarks>
    ''' Only the application name is displayed if no document is loaded.
    ''' </remarks>
    Private Sub DisplayPageTitle()
        Dim documentTitle As String = Me.currentBrowser.DocumentTitle

        If documentTitle = String.Empty Then
            Me.Text = Application.ProductName
        Else
            'Display the current page title in the window's title bar.
            Me.Text = String.Format("{0} - {1}", _
                                    documentTitle, _
                                    Application.ProductName)
        End If
    End Sub

#End Region 'Methods

End Class
