' ==============================================================================
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'
' ©2004 Palo Mraz, LaMarvin. All Rights Reserved.
' ==============================================================================

Public Class MainForm
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
  Private WithEvents LeftPanel As System.Windows.Forms.Panel
  Private WithEvents Label1 As System.Windows.Forms.Label
  Private WithEvents Splitter1 As System.Windows.Forms.Splitter
  Private WithEvents RightPanel As System.Windows.Forms.Panel
  Private WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
  Private WithEvents MyPictureBox1 As LaMarvin.Windows.Forms.MyPictureBox
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.LeftPanel = New System.Windows.Forms.Panel
    Me.Label1 = New System.Windows.Forms.Label
    Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
    Me.MyPictureBox1 = New LaMarvin.Windows.Forms.MyPictureBox
    Me.Splitter1 = New System.Windows.Forms.Splitter
    Me.RightPanel = New System.Windows.Forms.Panel
    Me.LeftPanel.SuspendLayout()
    Me.RightPanel.SuspendLayout()
    Me.SuspendLayout()
    '
    'LeftPanel
    '
    Me.LeftPanel.Controls.Add(Me.Label1)
    Me.LeftPanel.Controls.Add(Me.PropertyGrid1)
    Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left
    Me.LeftPanel.DockPadding.All = 2
    Me.LeftPanel.Location = New System.Drawing.Point(0, 0)
    Me.LeftPanel.Name = "LeftPanel"
    Me.LeftPanel.Size = New System.Drawing.Size(288, 473)
    Me.LeftPanel.TabIndex = 0
    '
    'Label1
    '
    Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
    Me.Label1.Location = New System.Drawing.Point(2, 2)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(284, 38)
    Me.Label1.TabIndex = 4
    Me.Label1.Text = "Edit the Image property in the property grid below in order to see the ResourceIm" & _
    "ageEditor in action:"
    '
    'PropertyGrid1
    '
    Me.PropertyGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PropertyGrid1.CommandsVisibleIfAvailable = True
    Me.PropertyGrid1.LargeButtons = False
    Me.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar
    Me.PropertyGrid1.Location = New System.Drawing.Point(2, 47)
    Me.PropertyGrid1.Name = "PropertyGrid1"
    Me.PropertyGrid1.SelectedObject = Me.MyPictureBox1
    Me.PropertyGrid1.Size = New System.Drawing.Size(284, 424)
    Me.PropertyGrid1.TabIndex = 3
    Me.PropertyGrid1.Text = "PropertyGrid1"
    Me.PropertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window
    Me.PropertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText
    '
    'MyPictureBox1
    '
    Me.MyPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.MyPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.MyPictureBox1.Location = New System.Drawing.Point(0, 0)
    Me.MyPictureBox1.Name = "MyPictureBox1"
    Me.MyPictureBox1.Size = New System.Drawing.Size(321, 473)
    Me.MyPictureBox1.TabIndex = 0
    Me.MyPictureBox1.TabStop = False
    '
    'Splitter1
    '
    Me.Splitter1.Location = New System.Drawing.Point(288, 0)
    Me.Splitter1.Name = "Splitter1"
    Me.Splitter1.Size = New System.Drawing.Size(3, 473)
    Me.Splitter1.TabIndex = 1
    Me.Splitter1.TabStop = False
    '
    'RightPanel
    '
    Me.RightPanel.Controls.Add(Me.MyPictureBox1)
    Me.RightPanel.Dock = System.Windows.Forms.DockStyle.Fill
    Me.RightPanel.Location = New System.Drawing.Point(291, 0)
    Me.RightPanel.Name = "RightPanel"
    Me.RightPanel.Size = New System.Drawing.Size(321, 473)
    Me.RightPanel.TabIndex = 2
    '
    'MainForm
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(612, 473)
    Me.Controls.Add(Me.RightPanel)
    Me.Controls.Add(Me.Splitter1)
    Me.Controls.Add(Me.LeftPanel)
    Me.Name = "MainForm"
    Me.Text = "ResourceImageEditor Sample Application"
    Me.LeftPanel.ResumeLayout(False)
    Me.RightPanel.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region


  Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      ' Do a bit of advertising here: if the user selects the "My Favorite Product" image,
      ' i'll make the pisture box to an "hotspot". If one clicks it, she will be taken to my
      ' products page.
      ' In order to do this, I'll have to find
    Catch ex As Exception
      Trace.WriteLine(ex.ToString())
    End Try
  End Sub
End Class
