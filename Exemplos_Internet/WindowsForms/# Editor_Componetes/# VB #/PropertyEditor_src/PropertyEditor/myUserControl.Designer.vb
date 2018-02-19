<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class myUserControl
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
		Me.TextBox1 = New System.Windows.Forms.TextBox
		Me.TextBox2 = New System.Windows.Forms.TextBox
		Me.ButtonOK = New System.Windows.Forms.Button
		Me.ButtonCancel = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(10, 11)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(124, 20)
		Me.TextBox1.TabIndex = 0
		'
		'TextBox2
		'
		Me.TextBox2.Location = New System.Drawing.Point(10, 41)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(123, 20)
		Me.TextBox2.TabIndex = 1
		'
		'ButtonOK
		'
		Me.ButtonOK.Location = New System.Drawing.Point(149, 8)
		Me.ButtonOK.Name = "ButtonOK"
		Me.ButtonOK.Size = New System.Drawing.Size(58, 25)
		Me.ButtonOK.TabIndex = 3
		Me.ButtonOK.Text = "OK"
		Me.ButtonOK.UseVisualStyleBackColor = True
		'
		'ButtonCancel
		'
		Me.ButtonCancel.Location = New System.Drawing.Point(149, 39)
		Me.ButtonCancel.Name = "ButtonCancel"
		Me.ButtonCancel.Size = New System.Drawing.Size(58, 25)
		Me.ButtonCancel.TabIndex = 3
		Me.ButtonCancel.Text = "Cancel"
		Me.ButtonCancel.UseVisualStyleBackColor = True
		'
		'myUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoSize = True
		Me.Controls.Add(Me.ButtonCancel)
		Me.Controls.Add(Me.ButtonOK)
		Me.Controls.Add(Me.TextBox2)
		Me.Controls.Add(Me.TextBox1)
		Me.Name = "myUserControl"
		Me.Size = New System.Drawing.Size(220, 74)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
	Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
	Friend WithEvents ButtonOK As System.Windows.Forms.Button
	Friend WithEvents ButtonCancel As System.Windows.Forms.Button

End Class
