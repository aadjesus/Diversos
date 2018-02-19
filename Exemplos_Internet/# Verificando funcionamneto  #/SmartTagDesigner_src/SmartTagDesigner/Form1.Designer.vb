<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
		Me.XTextBox1 = New SmartTagDesigner.XTextBox
		Me.myText1 = New SmartTagDesigner.XTextBox
		Me.XTooltip1 = New SmartTagDesigner.XTooltip
		Me.SuspendLayout()
		'
		'XTextBox1
		'
		Me.XTextBox1.Location = New System.Drawing.Point(0, 0)
		Me.XTextBox1.myProperty = Nothing
		Me.XTextBox1.Name = "XTextBox1"
		Me.XTextBox1.Size = New System.Drawing.Size(100, 20)
		Me.XTextBox1.TabIndex = 0
		Me.XTextBox1.Text = "saeed"
		'
		'myText1
		'
		Me.myText1.BackColor = System.Drawing.SystemColors.Window
		Me.myText1.ForeColor = System.Drawing.SystemColors.WindowText
		Me.myText1.Location = New System.Drawing.Point(12, 12)
		Me.myText1.myProperty = Nothing
		Me.myText1.Name = "myText1"
		Me.myText1.Size = New System.Drawing.Size(100, 23)
		Me.myText1.TabIndex = 0
		Me.myText1.Text = "Serpooshan"
		'
		'XTooltip1
		'
		Me.XTooltip1.myProperty = Nothing
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(358, 286)
		Me.Controls.Add(Me.myText1)
		Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
		Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.Name = "Form1"
		Me.Text = "Form1"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents XTextBox1 As SmartTagDesigner.XTextBox
	Friend WithEvents myText1 As SmartTagDesigner.XTextBox
	Friend WithEvents XTooltip1 As SmartTagDesigner.XTooltip

End Class
