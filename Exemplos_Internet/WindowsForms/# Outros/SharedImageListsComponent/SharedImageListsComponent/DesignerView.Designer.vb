<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DesignerView
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.ListBox1 = New System.Windows.Forms.ListBox
    Me.ListBox2 = New System.Windows.Forms.ListBox
    Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
    Me.AddNewImageListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.MenuStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'ListBox1
    '
    Me.ListBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListBox1.FormattingEnabled = True
    Me.ListBox1.IntegralHeight = False
    Me.ListBox1.ItemHeight = 16
    Me.ListBox1.Location = New System.Drawing.Point(10, 41)
    Me.ListBox1.Margin = New System.Windows.Forms.Padding(10)
    Me.ListBox1.Name = "ListBox1"
    Me.ListBox1.Size = New System.Drawing.Size(132, 389)
    Me.ListBox1.TabIndex = 0
    '
    'ListBox2
    '
    Me.ListBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListBox2.FormattingEnabled = True
    Me.ListBox2.IntegralHeight = False
    Me.ListBox2.ItemHeight = 16
    Me.ListBox2.Location = New System.Drawing.Point(151, 41)
    Me.ListBox2.Name = "ListBox2"
    Me.ListBox2.Size = New System.Drawing.Size(179, 389)
    Me.ListBox2.TabIndex = 1
    '
    'MenuStrip1
    '
    Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewImageListToolStripMenuItem})
    Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip1.Name = "MenuStrip1"
    Me.MenuStrip1.Size = New System.Drawing.Size(527, 26)
    Me.MenuStrip1.TabIndex = 3
    Me.MenuStrip1.Text = "MenuStrip1"
    '
    'AddNewImageListToolStripMenuItem
    '
    Me.AddNewImageListToolStripMenuItem.Name = "AddNewImageListToolStripMenuItem"
    Me.AddNewImageListToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
    Me.AddNewImageListToolStripMenuItem.Text = "Add New ImageList"
    '
    'DesignerView
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.ListBox2)
    Me.Controls.Add(Me.ListBox1)
    Me.Controls.Add(Me.MenuStrip1)
    Me.Name = "DesignerView"
    Me.Size = New System.Drawing.Size(527, 440)
    Me.MenuStrip1.ResumeLayout(False)
    Me.MenuStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
  Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
  Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
  Friend WithEvents AddNewImageListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
