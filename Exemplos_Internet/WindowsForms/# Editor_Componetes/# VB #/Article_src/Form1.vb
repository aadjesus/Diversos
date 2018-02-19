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
    Friend WithEvents UserControl11 As TestObjects.UserControl1
    Friend WithEvents btnNew As TestObjects.PushButton
    Friend WithEvents btnOpen As TestObjects.PushButton
    Friend WithEvents btnSave As TestObjects.PushButton
    Friend WithEvents ToggleButton1 As TestObjects.ToggleButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.UserControl11 = New TestObjects.UserControl1
        Me.btnNew = New TestObjects.PushButton
        Me.btnOpen = New TestObjects.PushButton
        Me.btnSave = New TestObjects.PushButton
        Me.ToggleButton1 = New TestObjects.ToggleButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'UserControl11
        '
        Me.UserControl11.BackColor = System.Drawing.Color.Red
        Me.UserControl11.Buttons.AddRange(New Object() {Me.btnNew, Me.btnOpen, Me.btnSave, New TestObjects.ButtonSeperator, New TestObjects.PlaceHolder(30), New TestObjects.ButtonSeperator, Me.ToggleButton1})
        Me.UserControl11.ImageList = Me.ImageList1
        Me.UserControl11.Location = New System.Drawing.Point(112, 32)
        Me.UserControl11.Name = "UserControl11"
        Me.UserControl11.Size = New System.Drawing.Size(64, 32)
        Me.UserControl11.TabIndex = 0
        Me.UserControl11.Text = "UserControl11"
        '
        'btnNew
        '
        Me.btnNew.ButtonType = TestObjects.ButtonBase.ButtonTypes.PushButton
        Me.btnNew.ImageIndex = 0
        Me.btnNew.Value = Nothing
        Me.btnNew.Width = Nothing
        '
        'btnOpen
        '
        Me.btnOpen.ButtonType = TestObjects.ButtonBase.ButtonTypes.PushButton
        Me.btnOpen.ImageIndex = 1
        Me.btnOpen.Value = Nothing
        Me.btnOpen.Width = Nothing
        '
        'btnSave
        '
        Me.btnSave.ButtonType = TestObjects.ButtonBase.ButtonTypes.PushButton
        Me.btnSave.ImageIndex = 2
        Me.btnSave.Value = Nothing
        Me.btnSave.Width = Nothing
        '
        'ToggleButton1
        '
        Me.ToggleButton1.ButtonType = TestObjects.ButtonBase.ButtonTypes.ToggleButton
        Me.ToggleButton1.Value = False
        Me.ToggleButton1.Width = Nothing
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Red
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.UserControl11)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class