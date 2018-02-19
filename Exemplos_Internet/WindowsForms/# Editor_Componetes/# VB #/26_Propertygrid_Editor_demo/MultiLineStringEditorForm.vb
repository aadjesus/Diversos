Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class MultiLineStringEditorForm
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
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(392, 112)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.Text = "Input"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(232, 120)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "&Cancel"
        '
        'btnOk
        '
        Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOk.Location = New System.Drawing.Point(312, 120)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(72, 24)
        Me.btnOk.TabIndex = 3
        Me.btnOk.Text = "&Ok"
        '
        'MultiLineStringEditorForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(392, 149)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.TextBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MultiLineStringEditorForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.ResumeLayout(False)

    End Sub

#End Region
    ' i put the form declaration & initialisation here
    ' then i can have 2 methods to call & open up the editor
    Private Shared Form As New MultiLineStringEditorForm

    Public Shared Sub Edit(ByRef InputValue As MultiLineString)
        ' set the textbox text to the inputvalues' string-representation
        Form.MultiLineString = InputValue.Value
        ' if user hit "ok" then change the InputValue's value to the new string
        If Form.ShowDialog() = Form.DialogResult.OK Then InputValue.Value = Form.MultiLineString
    End Sub

    Public Shared Sub Edit(ByRef InputValue As MultiLineString, ByVal Title As String)
        Form.Text = Title
        Edit(InputValue)
    End Sub

    Public Property MultiLineString() As String
        Get
            Return CType(TextBox1.Text, String)
        End Get
        Set(ByVal Value As String)
            TextBox1.Text = Value
        End Set
    End Property
End Class