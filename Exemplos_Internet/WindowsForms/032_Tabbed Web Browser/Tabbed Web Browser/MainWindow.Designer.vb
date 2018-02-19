<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim toolbarSeparator1 As System.Windows.Forms.ToolStripSeparator
        Dim newTabButton As System.Windows.Forms.ToolStripButton
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Dim fileMenuItem As System.Windows.Forms.ToolStripMenuItem
        Dim openMenuItem As System.Windows.Forms.ToolStripMenuItem
        Dim fileMenuSeparator1 As System.Windows.Forms.ToolStripSeparator
        Dim exitMenuItem As System.Windows.Forms.ToolStripMenuItem
        Dim viewMenuItem As System.Windows.Forms.ToolStripMenuItem
        Dim historyMenuItem As System.Windows.Forms.ToolStripMenuItem
        Dim toolbarSeparator2 As System.Windows.Forms.ToolStripSeparator
        Dim addressLabel As System.Windows.Forms.ToolStripLabel
        Dim homeButton As System.Windows.Forms.ToolStripButton
        Dim toolsMenuItem As System.Windows.Forms.ToolStripMenuItem
        Dim optionsMenuItem As System.Windows.Forms.ToolStripMenuItem
        Me.newTabMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.closeTabMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.refreshMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.stopMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.backMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.forwardMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.goButton = New System.Windows.Forms.ToolStripButton
        Me.mainMenu = New System.Windows.Forms.MenuStrip
        Me.toolbar = New System.Windows.Forms.ToolStrip
        Me.backButton = New System.Windows.Forms.ToolStripButton
        Me.forwardButton = New System.Windows.Forms.ToolStripButton
        Me.refreshButton = New System.Windows.Forms.ToolStripButton
        Me.stopButton = New System.Windows.Forms.ToolStripButton
        Me.closeTabButton = New System.Windows.Forms.ToolStripButton
        Me.addressCombo = New System.Windows.Forms.ToolStripComboBox
        Me.statusbar = New System.Windows.Forms.StatusStrip
        Me.browserStatusTextLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.pageLoadProgress = New System.Windows.Forms.ToolStripProgressBar
        Me.homeMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tabs = New TabbedWebBrowser.WebBrowserTabControl
        toolbarSeparator1 = New System.Windows.Forms.ToolStripSeparator
        newTabButton = New System.Windows.Forms.ToolStripButton
        fileMenuItem = New System.Windows.Forms.ToolStripMenuItem
        openMenuItem = New System.Windows.Forms.ToolStripMenuItem
        fileMenuSeparator1 = New System.Windows.Forms.ToolStripSeparator
        exitMenuItem = New System.Windows.Forms.ToolStripMenuItem
        viewMenuItem = New System.Windows.Forms.ToolStripMenuItem
        historyMenuItem = New System.Windows.Forms.ToolStripMenuItem
        toolbarSeparator2 = New System.Windows.Forms.ToolStripSeparator
        addressLabel = New System.Windows.Forms.ToolStripLabel
        homeButton = New System.Windows.Forms.ToolStripButton
        toolsMenuItem = New System.Windows.Forms.ToolStripMenuItem
        optionsMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mainMenu.SuspendLayout()
        Me.toolbar.SuspendLayout()
        Me.statusbar.SuspendLayout()
        Me.SuspendLayout()
        '
        'toolbarSeparator1
        '
        toolbarSeparator1.Name = "toolbarSeparator1"
        toolbarSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'newTabButton
        '
        newTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        newTabButton.Image = CType(resources.GetObject("newTabButton.Image"), System.Drawing.Image)
        newTabButton.ImageTransparentColor = System.Drawing.Color.Magenta
        newTabButton.Name = "newTabButton"
        newTabButton.Size = New System.Drawing.Size(23, 22)
        newTabButton.Text = "New Tab"
        AddHandler newTabButton.Click, AddressOf Me.newTabButton_Click
        '
        'fileMenuItem
        '
        fileMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newTabMenuItem, openMenuItem, Me.closeTabMenuItem, fileMenuSeparator1, exitMenuItem})
        fileMenuItem.Name = "fileMenuItem"
        fileMenuItem.Size = New System.Drawing.Size(37, 20)
        fileMenuItem.Text = "&File"
        '
        'newTabMenuItem
        '
        Me.newTabMenuItem.Image = CType(resources.GetObject("newTabMenuItem.Image"), System.Drawing.Image)
        Me.newTabMenuItem.Name = "newTabMenuItem"
        Me.newTabMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.newTabMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.newTabMenuItem.Text = "&New Tab"
        '
        'openMenuItem
        '
        openMenuItem.Enabled = False
        openMenuItem.Name = "openMenuItem"
        openMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        openMenuItem.Size = New System.Drawing.Size(164, 22)
        openMenuItem.Text = "&Open..."
        AddHandler openMenuItem.Click, AddressOf Me.openMenuItem_Click
        '
        'closeTabMenuItem
        '
        Me.closeTabMenuItem.Enabled = False
        Me.closeTabMenuItem.Image = CType(resources.GetObject("closeTabMenuItem.Image"), System.Drawing.Image)
        Me.closeTabMenuItem.Name = "closeTabMenuItem"
        Me.closeTabMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.closeTabMenuItem.Text = "&Close Tab"
        '
        'fileMenuSeparator1
        '
        fileMenuSeparator1.Name = "fileMenuSeparator1"
        fileMenuSeparator1.Size = New System.Drawing.Size(161, 6)
        '
        'exitMenuItem
        '
        exitMenuItem.Image = CType(resources.GetObject("exitMenuItem.Image"), System.Drawing.Image)
        exitMenuItem.Name = "exitMenuItem"
        exitMenuItem.Size = New System.Drawing.Size(164, 22)
        exitMenuItem.Text = "E&xit"
        AddHandler exitMenuItem.Click, AddressOf Me.exitMenuItem_Click
        '
        'viewMenuItem
        '
        viewMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.refreshMenuItem, Me.stopMenuItem})
        viewMenuItem.Name = "viewMenuItem"
        viewMenuItem.Size = New System.Drawing.Size(44, 20)
        viewMenuItem.Text = "&View"
        '
        'refreshMenuItem
        '
        Me.refreshMenuItem.Enabled = False
        Me.refreshMenuItem.Image = CType(resources.GetObject("refreshMenuItem.Image"), System.Drawing.Image)
        Me.refreshMenuItem.Name = "refreshMenuItem"
        Me.refreshMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.refreshMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.refreshMenuItem.Text = "&Refresh"
        '
        'stopMenuItem
        '
        Me.stopMenuItem.Enabled = False
        Me.stopMenuItem.Image = CType(resources.GetObject("stopMenuItem.Image"), System.Drawing.Image)
        Me.stopMenuItem.Name = "stopMenuItem"
        Me.stopMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.stopMenuItem.Text = "&Stop"
        '
        'historyMenuItem
        '
        historyMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.backMenuItem, Me.forwardMenuItem, Me.homeMenuItem})
        historyMenuItem.Name = "historyMenuItem"
        historyMenuItem.Size = New System.Drawing.Size(57, 20)
        historyMenuItem.Text = "Hi&story"
        '
        'backMenuItem
        '
        Me.backMenuItem.Enabled = False
        Me.backMenuItem.Image = CType(resources.GetObject("backMenuItem.Image"), System.Drawing.Image)
        Me.backMenuItem.Name = "backMenuItem"
        Me.backMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Left), System.Windows.Forms.Keys)
        Me.backMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.backMenuItem.Text = "&Back"
        '
        'forwardMenuItem
        '
        Me.forwardMenuItem.Enabled = False
        Me.forwardMenuItem.Image = CType(resources.GetObject("forwardMenuItem.Image"), System.Drawing.Image)
        Me.forwardMenuItem.Name = "forwardMenuItem"
        Me.forwardMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Right), System.Windows.Forms.Keys)
        Me.forwardMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.forwardMenuItem.Text = "For&ward"
        '
        'toolbarSeparator2
        '
        toolbarSeparator2.Name = "toolbarSeparator2"
        toolbarSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'addressLabel
        '
        addressLabel.Name = "addressLabel"
        addressLabel.Size = New System.Drawing.Size(52, 22)
        addressLabel.Text = "Address:"
        '
        'goButton
        '
        Me.goButton.Image = CType(resources.GetObject("goButton.Image"), System.Drawing.Image)
        Me.goButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.goButton.Name = "goButton"
        Me.goButton.Size = New System.Drawing.Size(42, 22)
        Me.goButton.Text = "Go"
        Me.goButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'mainMenu
        '
        Me.mainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {fileMenuItem, viewMenuItem, historyMenuItem, toolsMenuItem})
        Me.mainMenu.Location = New System.Drawing.Point(0, 0)
        Me.mainMenu.Name = "mainMenu"
        Me.mainMenu.Size = New System.Drawing.Size(792, 24)
        Me.mainMenu.TabIndex = 0
        Me.mainMenu.Text = "MenuStrip1"
        '
        'toolbar
        '
        Me.toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.backButton, Me.forwardButton, Me.refreshButton, Me.stopButton, homeButton, toolbarSeparator1, newTabButton, Me.closeTabButton, toolbarSeparator2, addressLabel, Me.addressCombo, Me.goButton})
        Me.toolbar.Location = New System.Drawing.Point(0, 24)
        Me.toolbar.Name = "toolbar"
        Me.toolbar.Size = New System.Drawing.Size(792, 25)
        Me.toolbar.TabIndex = 1
        Me.toolbar.Text = "ToolStrip1"
        '
        'backButton
        '
        Me.backButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.backButton.Enabled = False
        Me.backButton.Image = CType(resources.GetObject("backButton.Image"), System.Drawing.Image)
        Me.backButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.backButton.Name = "backButton"
        Me.backButton.Size = New System.Drawing.Size(23, 22)
        Me.backButton.Text = "Back"
        '
        'forwardButton
        '
        Me.forwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.forwardButton.Enabled = False
        Me.forwardButton.Image = CType(resources.GetObject("forwardButton.Image"), System.Drawing.Image)
        Me.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.forwardButton.Name = "forwardButton"
        Me.forwardButton.Size = New System.Drawing.Size(23, 22)
        Me.forwardButton.Text = "Forward"
        '
        'refreshButton
        '
        Me.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.refreshButton.Enabled = False
        Me.refreshButton.Image = CType(resources.GetObject("refreshButton.Image"), System.Drawing.Image)
        Me.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.refreshButton.Name = "refreshButton"
        Me.refreshButton.Size = New System.Drawing.Size(23, 22)
        Me.refreshButton.Text = "Refresh"
        '
        'stopButton
        '
        Me.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.stopButton.Enabled = False
        Me.stopButton.Image = CType(resources.GetObject("stopButton.Image"), System.Drawing.Image)
        Me.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.stopButton.Name = "stopButton"
        Me.stopButton.Size = New System.Drawing.Size(23, 22)
        Me.stopButton.Text = "Stop"
        '
        'closeTabButton
        '
        Me.closeTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.closeTabButton.Enabled = False
        Me.closeTabButton.Image = CType(resources.GetObject("closeTabButton.Image"), System.Drawing.Image)
        Me.closeTabButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.closeTabButton.Name = "closeTabButton"
        Me.closeTabButton.Size = New System.Drawing.Size(23, 22)
        Me.closeTabButton.Text = "Close Tab"
        '
        'addressCombo
        '
        Me.addressCombo.AutoSize = False
        Me.addressCombo.Name = "addressCombo"
        Me.addressCombo.Size = New System.Drawing.Size(510, 23)
        '
        'statusbar
        '
        Me.statusbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.browserStatusTextLabel, Me.pageLoadProgress})
        Me.statusbar.Location = New System.Drawing.Point(0, 544)
        Me.statusbar.Name = "statusbar"
        Me.statusbar.Size = New System.Drawing.Size(792, 22)
        Me.statusbar.TabIndex = 2
        Me.statusbar.Text = "StatusStrip1"
        '
        'browserStatusTextLabel
        '
        Me.browserStatusTextLabel.Name = "browserStatusTextLabel"
        Me.browserStatusTextLabel.Size = New System.Drawing.Size(777, 17)
        Me.browserStatusTextLabel.Spring = True
        Me.browserStatusTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pageLoadProgress
        '
        Me.pageLoadProgress.Name = "pageLoadProgress"
        Me.pageLoadProgress.Size = New System.Drawing.Size(100, 16)
        Me.pageLoadProgress.Visible = False
        '
        'homeButton
        '
        homeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        homeButton.Image = CType(resources.GetObject("homeButton.Image"), System.Drawing.Image)
        homeButton.ImageTransparentColor = System.Drawing.Color.Magenta
        homeButton.Name = "homeButton"
        homeButton.Size = New System.Drawing.Size(23, 22)
        homeButton.Text = "Home"
        AddHandler homeButton.Click, AddressOf Me.homeButton_Click
        '
        'homeMenuItem
        '
        Me.homeMenuItem.Image = CType(resources.GetObject("homeMenuItem.Image"), System.Drawing.Image)
        Me.homeMenuItem.Name = "homeMenuItem"
        Me.homeMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.homeMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.homeMenuItem.Text = "H&ome"
        '
        'tabs
        '
        Me.tabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabs.Location = New System.Drawing.Point(0, 49)
        Me.tabs.Name = "tabs"
        Me.tabs.SelectedIndex = 0
        Me.tabs.Size = New System.Drawing.Size(792, 495)
        Me.tabs.TabIndex = 3
        '
        'toolsMenuItem
        '
        toolsMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {optionsMenuItem})
        toolsMenuItem.Name = "toolsMenuItem"
        toolsMenuItem.Size = New System.Drawing.Size(48, 20)
        toolsMenuItem.Text = "&Tools"
        '
        'optionsMenuItem
        '
        optionsMenuItem.Image = CType(resources.GetObject("optionsMenuItem.Image"), System.Drawing.Image)
        optionsMenuItem.Name = "optionsMenuItem"
        optionsMenuItem.Size = New System.Drawing.Size(152, 22)
        optionsMenuItem.Text = "&Options..."
        AddHandler optionsMenuItem.Click, AddressOf Me.optionsMenuItem_Click
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.tabs)
        Me.Controls.Add(Me.statusbar)
        Me.Controls.Add(Me.toolbar)
        Me.Controls.Add(Me.mainMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mainMenu
        Me.Name = "MainWindow"
        Me.Text = "Tabbed Web Browser"
        Me.mainMenu.ResumeLayout(False)
        Me.mainMenu.PerformLayout()
        Me.toolbar.ResumeLayout(False)
        Me.toolbar.PerformLayout()
        Me.statusbar.ResumeLayout(False)
        Me.statusbar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents backButton As System.Windows.Forms.ToolStripButton
    Private WithEvents forwardButton As System.Windows.Forms.ToolStripButton
    Private WithEvents refreshButton As System.Windows.Forms.ToolStripButton
    Private WithEvents stopButton As System.Windows.Forms.ToolStripButton
    Private WithEvents closeTabMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents closeTabButton As System.Windows.Forms.ToolStripButton
    Private WithEvents refreshMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents stopMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents backMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents forwardMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents newTabMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents addressCombo As System.Windows.Forms.ToolStripComboBox
    Private WithEvents mainMenu As System.Windows.Forms.MenuStrip
    Private WithEvents toolbar As System.Windows.Forms.ToolStrip
    Private WithEvents statusbar As System.Windows.Forms.StatusStrip
    Private WithEvents tabs As TabbedWebBrowser.WebBrowserTabControl
    Private WithEvents goButton As System.Windows.Forms.ToolStripButton
    Private WithEvents browserStatusTextLabel As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents pageLoadProgress As System.Windows.Forms.ToolStripProgressBar
    Private WithEvents homeMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
