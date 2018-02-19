''' Developed by : Sreenivas Vemulapalli
''' vemvas@yahoo 
''' Date: 08/15/2002
'''
Public Class frmPropertyGrid
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(12, 118)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(228, 24)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Properties using System Resources"
        '
        'Label13
        '
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(12, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(176, 24)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Simple Properties"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(216, 48)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Click on the Link below to load the properties"
        '
        'Label1
        '
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(16, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 24)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Application Settings"
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.CommandsVisibleIfAvailable = True
        Me.PropertyGrid1.LargeButtons = False
        Me.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.PropertyGrid1.Location = New System.Drawing.Point(248, 14)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(288, 472)
        Me.PropertyGrid1.TabIndex = 16
        Me.PropertyGrid1.Text = "PropertyGrid1"
        Me.PropertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window
        Me.PropertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText
        '
        'frmPropertyGrid
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(544, 549)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label14, Me.Label13, Me.Label5, Me.Label1, Me.PropertyGrid1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmPropertyGrid"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Using PropertyGrid"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmPropertyGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click
        PropertyGrid1.SelectedObject = New SimpleProperties()
        PropertyGrid1.Refresh()
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click
        PropertyGrid1.SelectedObject = New SystemResourceOptions()
        PropertyGrid1.Refresh()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        PropertyGrid1.SelectedObject = New ApplicationProperties()
        PropertyGrid1.Refresh()
    End Sub
End Class
