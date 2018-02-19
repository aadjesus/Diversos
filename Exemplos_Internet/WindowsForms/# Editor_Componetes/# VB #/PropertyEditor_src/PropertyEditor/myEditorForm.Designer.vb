<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class myEditorForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
		Me.ButtonCancel = New System.Windows.Forms.Button
		Me.ButtonOK = New System.Windows.Forms.Button
		Me.CheckBox1 = New System.Windows.Forms.CheckBox
		Me.TextBox2 = New System.Windows.Forms.TextBox
		Me.TextBox1 = New System.Windows.Forms.TextBox
		Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar
		Me.SuspendLayout()
		'
		'ButtonCancel
		'
		Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.ButtonCancel.Location = New System.Drawing.Point(134, 42)
		Me.ButtonCancel.Name = "ButtonCancel"
		Me.ButtonCancel.Size = New System.Drawing.Size(58, 25)
		Me.ButtonCancel.TabIndex = 7
		Me.ButtonCancel.Text = "Cancel"
		Me.ButtonCancel.UseVisualStyleBackColor = True
		'
		'ButtonOK
		'
		Me.ButtonOK.Location = New System.Drawing.Point(134, 11)
		Me.ButtonOK.Name = "ButtonOK"
		Me.ButtonOK.Size = New System.Drawing.Size(58, 25)
		Me.ButtonOK.TabIndex = 8
		Me.ButtonOK.Text = "OK"
		Me.ButtonOK.UseVisualStyleBackColor = True
		'
		'CheckBox1
		'
		Me.CheckBox1.AutoSize = True
		Me.CheckBox1.Location = New System.Drawing.Point(14, 76)
		Me.CheckBox1.Name = "CheckBox1"
		Me.CheckBox1.Size = New System.Drawing.Size(100, 17)
		Me.CheckBox1.TabIndex = 6
		Me.CheckBox1.Text = "Some Condition"
		Me.CheckBox1.UseVisualStyleBackColor = True
		'
		'TextBox2
		'
		Me.TextBox2.Location = New System.Drawing.Point(12, 45)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(116, 20)
		Me.TextBox2.TabIndex = 5
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(12, 15)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(116, 20)
		Me.TextBox1.TabIndex = 4
		'
		'MonthCalendar1
		'
		Me.MonthCalendar1.Location = New System.Drawing.Point(12, 107)
		Me.MonthCalendar1.Name = "MonthCalendar1"
		Me.MonthCalendar1.TabIndex = 9
		'
		'myEditorForm
		'
		Me.AcceptButton = Me.ButtonOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoSize = True
		Me.CancelButton = Me.ButtonCancel
		Me.ClientSize = New System.Drawing.Size(205, 280)
		Me.Controls.Add(Me.MonthCalendar1)
		Me.Controls.Add(Me.ButtonCancel)
		Me.Controls.Add(Me.ButtonOK)
		Me.Controls.Add(Me.CheckBox1)
		Me.Controls.Add(Me.TextBox2)
		Me.Controls.Add(Me.TextBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.KeyPreview = True
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "myEditorForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "myEditorForm"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents ButtonCancel As System.Windows.Forms.Button
	Friend WithEvents ButtonOK As System.Windows.Forms.Button
	Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
	Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
	Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
	Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
End Class
