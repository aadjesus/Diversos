' ==============================================================================
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'
' © 2003 LaMarvin. All Rights Reserved.
'
' FMI: http://www.vbinfozine.com/a_default.shtml
' ==============================================================================

Imports LaMarvin.Windows.Forms

Public Class Form1
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
  Private WithEvents TopLeftColor As LaMarvin.Windows.Forms.ColorPicker
  Private WithEvents BottomRightColor As LaMarvin.Windows.Forms.ColorPicker
  Private WithEvents ArticleLink As System.Windows.Forms.LinkLabel
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.TopLeftColor = New LaMarvin.Windows.Forms.ColorPicker
    Me.BottomRightColor = New LaMarvin.Windows.Forms.ColorPicker
    Me.ArticleLink = New System.Windows.Forms.LinkLabel
    Me.SuspendLayout()
    '
    'TopLeftColor
    '
    Me.TopLeftColor.Color = System.Drawing.Color.DarkBlue
    Me.TopLeftColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.TopLeftColor.ForeColor = System.Drawing.Color.Cyan
    Me.TopLeftColor.Location = New System.Drawing.Point(16, 16)
    Me.TopLeftColor.Name = "TopLeftColor"
    Me.TopLeftColor.Size = New System.Drawing.Size(104, 48)
    Me.TopLeftColor.TabIndex = 0
    Me.TopLeftColor.Text = "ColorPicker1"
    '
    'BottomRightColor
    '
    Me.BottomRightColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.BottomRightColor.Color = System.Drawing.Color.Orange
    Me.BottomRightColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.BottomRightColor.Location = New System.Drawing.Point(376, 320)
    Me.BottomRightColor.Name = "BottomRightColor"
    Me.BottomRightColor.Size = New System.Drawing.Size(104, 48)
    Me.BottomRightColor.TabIndex = 1
    Me.BottomRightColor.Text = "ColorPicker1"
    '
    'ArticleLink
    '
    Me.ArticleLink.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ArticleLink.BackColor = System.Drawing.Color.Transparent
    Me.ArticleLink.Location = New System.Drawing.Point(8, 360)
    Me.ArticleLink.Name = "ArticleLink"
    Me.ArticleLink.Size = New System.Drawing.Size(256, 23)
    Me.ArticleLink.TabIndex = 2
    Me.ArticleLink.TabStop = True
    Me.ArticleLink.Text = "See the accompanying article at VBInfoZine"
    '
    'Form1
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(496, 381)
    Me.Controls.Add(Me.ArticleLink)
    Me.Controls.Add(Me.BottomRightColor)
    Me.Controls.Add(Me.TopLeftColor)
    Me.Name = "Form1"
    Me.Text = "ColorPickerTest"
    Me.ResumeLayout(False)

  End Sub

#End Region


	Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Try
      Me.ResizeRedraw = True
		Catch ex As Exception
			MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
		End Try
	End Sub


  Private Sub ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TopLeftColor.ColorChanged, BottomRightColor.ColorChanged
    Me.Refresh()
  End Sub


  Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
    Dim B As Brush
    Try
      B = New Drawing2D.LinearGradientBrush(Me.ClientRectangle, Me.TopLeftColor.Color, Me.BottomRightColor.Color, Drawing2D.LinearGradientMode.ForwardDiagonal)
      e.Graphics.FillRectangle(B, Me.ClientRectangle)
    Catch
    Finally
      If Not B Is Nothing Then
        B.Dispose()
      End If
    End Try
  End Sub

  Private Sub ArticleLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles ArticleLink.LinkClicked
    Try
      Dim StartInfo As New System.Diagnostics.ProcessStartInfo
      StartInfo.FileName = "http://www.vbinfozine.com/a_colorpicker.shtml"
      StartInfo.UseShellExecute = True
      System.Diagnostics.Process.Start(StartInfo)
    Catch ex As Exception
      MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    End Try
  End Sub
End Class
