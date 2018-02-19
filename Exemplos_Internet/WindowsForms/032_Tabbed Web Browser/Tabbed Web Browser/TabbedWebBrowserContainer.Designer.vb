<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TabbedWebBrowserContainer
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.tabs = New TabbedWebBrowser.WebBrowserTabControl
        Me.SuspendLayout()
        '
        'tabs
        '
        Me.tabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabs.Location = New System.Drawing.Point(0, 0)
        Me.tabs.Margin = New System.Windows.Forms.Padding(0)
        Me.tabs.Name = "tabs"
        Me.tabs.Padding = New System.Drawing.Point(0, 0)
        Me.tabs.SelectedIndex = 0
        Me.tabs.SelectedTab = Nothing
        Me.tabs.Size = New System.Drawing.Size(150, 150)
        Me.tabs.TabIndex = 0
        '
        'TabbedWebBrowserContainer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tabs)
        Me.Name = "TabbedWebBrowserContainer"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tabs As TabbedWebBrowser.WebBrowserTabControl

End Class
