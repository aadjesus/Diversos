Public Class TabbedWebBrowserContainer

#Region " Variables "

    Private browser As New WebBrowser
    Private _hideTabForSinglePage As Boolean = True

#End Region 'Variables

#Region " Properties "

    Public ReadOnly Property CanGoBack() As Boolean
        Get
            Return Me.browser.CanGoBack
        End Get
    End Property

    Public ReadOnly Property CanGoForward() As Boolean
        Get
            Return Me.browser.CanGoBack
        End Get
    End Property

    Public Property HideTabForSinglePage() As Boolean
        Get
            Return Me._hideTabForSinglePage
        End Get
        Set(ByVal value As Boolean)
            Me._hideTabForSinglePage = value
        End Set
    End Property

    Public ReadOnly Property IsBusy() As Boolean
        Get
            Return Me.browser.IsBusy
        End Get
    End Property

    Public ReadOnly Property TabCount() As Integer
        Get
            Dim value As Integer

            If Me.tabs.Visible Then
                value = Me.tabs.TabPages.Count
            Else
                value = 1
            End If

            Return value
        End Get
    End Property

    Public ReadOnly Property Title() As String
        Get
            Return Me.browser.DocumentTitle
        End Get
    End Property

    Public Property Url() As String
        Get
            Return Me.browser.Url.ToString()
        End Get
        Set(ByVal value As String)
            Me.browser.Navigate(value)
        End Set
    End Property

#End Region 'Properties

#Region " Events "

#End Region 'Events

#Region " Event Handlers "

    Private Sub TabbedWebBrowserContainer_Load(ByVal sender As Object, _
                                               ByVal e As EventArgs) Handles Me.Load
        If Me.HideTabForSinglePage Then
            'Display just a WebBrowser control with no tab.
            Me.tabs.Hide()
            Me.browser.Dock = DockStyle.Fill
            Me.Controls.Add(Me.browser)
        Else
            Me.tabs.TabPages.Add(New WebBrowserTabPage(Me.browser))
        End If
    End Sub

#End Region 'Event Handlers

#Region " Methods "

    Public Overloads Sub AddTab()
        If Not Me.tabs.Visible Then
            'There is only one page open and the tab is hidden so place the first page on a tab first.
            Me.tabs.TabPages.Add(New WebBrowserTabPage(Me.browser))
            Me.tabs.Show()
            Me.browser = Nothing
        End If

        'Add and select a page.
        Me.tabs.TabPages.Add(New WebBrowserTabPage)
        Me.tabs.SelectedIndex = Me.TabCount - 1
    End Sub

    Public Overloads Sub AddTab(ByVal url As String)
        Me.AddTab()
        Me.tabs.SelectedWebBrowser.Navigate(url)
    End Sub

#End Region 'Methods

End Class
