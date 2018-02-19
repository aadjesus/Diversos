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
        Me.XTextBoxA1 = New PropertyEditor.XTextBoxA
        Me.XTextBoxB1 = New PropertyEditor.XTextBoxB
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.SuspendLayout()
        '
        'XTextBoxA1
        '
        Me.XTextBoxA1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.XTextBoxA1.Location = New System.Drawing.Point(18, 66)
        Me.XTextBoxA1.myProperty = "C:"
        Me.XTextBoxA1.Name = "XTextBoxA1"
        Me.XTextBoxA1.Size = New System.Drawing.Size(184, 23)
        Me.XTextBoxA1.TabIndex = 1
        Me.XTextBoxA1.Text = "XTextBoxA1"
        '
        'XTextBoxB1
        '
        Me.XTextBoxB1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.XTextBoxB1.Location = New System.Drawing.Point(18, 192)
        Me.XTextBoxB1.myProperty = "Root2.AAA"
        Me.XTextBoxB1.myProperty2 = "Ali"
        Me.XTextBoxB1.myProperty3 = "1/23/2007 7:30"
        Me.XTextBoxB1.Name = "XTextBoxB1"
        Me.XTextBoxB1.Size = New System.Drawing.Size(184, 23)
        Me.XTextBoxB1.TabIndex = 2
        Me.XTextBoxB1.Text = "XTextBoxB1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(262, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "XTextBoxA: Using a ListBox in Property Editor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(360, 28)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To see it, choose below TextBox (XTextBoxA1) and try to edit " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "'myProperty' in th" & _
            "e properties window."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(415, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "XTextBoxB: Using a TreeView, a UserControl or a Form  in Property Editor"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 153)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(396, 36)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "To see it, choose below TextBox (XTextBoxB1) and try to edit " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "'myProperty', 'myP" & _
            "roperty2', 'myProperty3' in the properties window."
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(5, 97)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(430, 2)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 230)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.XTextBoxB1)
        Me.Controls.Add(Me.XTextBoxA1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Name = "Form1"
        Me.Text = "Form1: Test PropertyEditor - S.Serpooshan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
	Friend WithEvents XTextBoxA1 As PropertyEditor.XTextBoxA
	Friend WithEvents XTextBoxB1 As PropertyEditor.XTextBoxB
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
