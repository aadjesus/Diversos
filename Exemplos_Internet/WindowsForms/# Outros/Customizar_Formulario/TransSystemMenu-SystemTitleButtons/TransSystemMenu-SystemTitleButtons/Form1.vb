Public Class Form1
    Inherits System.Windows.Forms.Form

#Region "Sub Main"
    Public Shared Sub Main()
        'you hook your program at the beginning
        TransMenuLib.Hook(Process.GetCurrentProcess)
        Application.EnableVisualStyles()
        Application.DoEvents()
        Application.Run(New Form1)
        'and unhook your program at the end
        TransMenuLib.Unhook()
    End Sub
#End Region


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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents stylelist As System.Windows.Forms.ListBox
    Friend WithEvents b4imagelist As System.Windows.Forms.ImageList
    Friend WithEvents b1imagelist As System.Windows.Forms.ImageList
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents TransMenu As TransSystemMenu_SystemTitleButtons.TransMenuLib
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.TransMenu = New TransSystemMenu_SystemTitleButtons.TransMenuLib(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.stylelist = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.b4imagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.b1imagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.Label1, "You just clicked a control using the WinTrayButton Help feature.")
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.HelpProvider1.SetShowHelp(Me.Label1, True)
        Me.Label1.Size = New System.Drawing.Size(176, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Current Button Index: "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.Button1, "You just clicked a control using the WinTrayButton Help feature.")
        Me.Button1.Location = New System.Drawing.Point(16, 48)
        Me.Button1.Name = "Button1"
        Me.HelpProvider1.SetShowHelp(Me.Button1, True)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Move Left"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.Button2, "You just clicked a control using the WinTrayButton Help feature.")
        Me.Button2.Location = New System.Drawing.Point(96, 48)
        Me.Button2.Name = "Button2"
        Me.HelpProvider1.SetShowHelp(Me.Button2, True)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Move Right"
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.CheckBox1, "You just clicked a control using the WinTrayButton Help feature.")
        Me.CheckBox1.Location = New System.Drawing.Point(16, 152)
        Me.CheckBox1.Name = "CheckBox1"
        Me.HelpProvider1.SetShowHelp(Me.CheckBox1, True)
        Me.CheckBox1.Size = New System.Drawing.Size(96, 16)
        Me.CheckBox1.TabIndex = 3
        Me.CheckBox1.Text = "Enabled"
        '
        'stylelist
        '
        Me.stylelist.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.HelpProvider1.SetHelpString(Me.stylelist, "You just clicked a control using the WinTrayButton Help feature.")
        Me.stylelist.Location = New System.Drawing.Point(232, 48)
        Me.stylelist.Name = "stylelist"
        Me.HelpProvider1.SetShowHelp(Me.stylelist, True)
        Me.stylelist.Size = New System.Drawing.Size(168, 69)
        Me.stylelist.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.Label3, "You just clicked a control using the WinTrayButton Help feature.")
        Me.Label3.Location = New System.Drawing.Point(240, 32)
        Me.Label3.Name = "Label3"
        Me.HelpProvider1.SetShowHelp(Me.Label3, True)
        Me.Label3.Size = New System.Drawing.Size(100, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Form Border Style:"
        '
        'b4imagelist
        '
        Me.b4imagelist.ImageSize = New System.Drawing.Size(16, 16)
        Me.b4imagelist.ImageStream = CType(resources.GetObject("b4imagelist.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.b4imagelist.TransparentColor = System.Drawing.Color.Transparent
        '
        'b1imagelist
        '
        Me.b1imagelist.ImageSize = New System.Drawing.Size(16, 16)
        Me.b1imagelist.ImageStream = CType(resources.GetObject("b1imagelist.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.b1imagelist.TransparentColor = System.Drawing.Color.Transparent
        '
        'CheckBox2
        '
        Me.CheckBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.CheckBox2, "You just clicked a control using the WinTrayButton Help feature.")
        Me.CheckBox2.Location = New System.Drawing.Point(16, 136)
        Me.CheckBox2.Name = "CheckBox2"
        Me.HelpProvider1.SetShowHelp(Me.CheckBox2, True)
        Me.CheckBox2.Size = New System.Drawing.Size(96, 16)
        Me.CheckBox2.TabIndex = 7
        Me.CheckBox2.Text = "DrawImageOnly"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.GroupBox1, "You just clicked a control using the WinTrayButton Help feature.")
        Me.GroupBox1.Location = New System.Drawing.Point(16, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.HelpProvider1.SetShowHelp(Me.GroupBox1, True)
        Me.GroupBox1.Size = New System.Drawing.Size(152, 56)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ImageList"
        '
        'RadioButton2
        '
        Me.RadioButton2.Checked = True
        Me.RadioButton2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.RadioButton2.Location = New System.Drawing.Point(8, 32)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(104, 16)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "4 Images"
        '
        'RadioButton1
        '
        Me.RadioButton1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.RadioButton1.Location = New System.Drawing.Point(8, 16)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(104, 16)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.Text = "1 Image"
        '
        'CheckBox3
        '
        Me.CheckBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.CheckBox3, "You just clicked a control using the WinTrayButton Help feature.")
        Me.CheckBox3.Location = New System.Drawing.Point(16, 168)
        Me.CheckBox3.Name = "CheckBox3"
        Me.HelpProvider1.SetShowHelp(Me.CheckBox3, True)
        Me.CheckBox3.Size = New System.Drawing.Size(96, 16)
        Me.CheckBox3.TabIndex = 9
        Me.CheckBox3.Text = "IsHelpButton"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.HelpProvider1.SetHelpString(Me.TextBox1, "You just clicked a control using the WinTrayButton Help feature.")
        Me.TextBox1.Location = New System.Drawing.Point(240, 152)
        Me.TextBox1.Name = "TextBox1"
        Me.HelpProvider1.SetShowHelp(Me.TextBox1, True)
        Me.TextBox1.Size = New System.Drawing.Size(160, 20)
        Me.TextBox1.TabIndex = 10
        Me.TextBox1.Text = ""
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.Label4, "You just clicked a control using the WinTrayButton Help feature.")
        Me.Label4.Location = New System.Drawing.Point(240, 136)
        Me.Label4.Name = "Label4"
        Me.HelpProvider1.SetShowHelp(Me.Label4, True)
        Me.Label4.Size = New System.Drawing.Size(100, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Button Menu Text:"
        '
        'Label2
        '
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.HelpProvider1.SetHelpString(Me.Label2, "You just clicked a control using the WinTrayButton Help feature.")
        Me.Label2.Location = New System.Drawing.Point(232, 8)
        Me.Label2.Name = "Label2"
        Me.HelpProvider1.SetShowHelp(Me.Label2, True)
        Me.Label2.Size = New System.Drawing.Size(176, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Current Button State:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(416, 190)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.stylelist)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TransSystemMenu-SystemTitleButtons"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'we can add this in to the 'Windows Form Designer generated code'
    'but this is not editable through the editor, it has to be done
    'manually. So this is here to remind you of the object and events
#Region "Wintraybtn events"
    Private Sub wintraybtn_MinTrayBtnClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles wintraybtn.MinTrayBtnClicked
        MessageBox.Show("You just clicked a tray button")
    End Sub

    Private Sub wintraybtn_MinTrayBtnStateChanged(ByVal sender As Object, ByVal st As WinTrayButton.state) Handles wintraybtn.MinTrayBtnStateChanged
        Me.Label2.Text = "Button state is now: " + st.ToString
    End Sub

    Private Sub wintraybtn_MinTrayBtnIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles wintraybtn.MinTrayBtnIndexChange
        Me.Label1.Text = "Current Button Index: " + wintraybtn.ButtonTrayIndex.ToString
    End Sub

#End Region
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.wintraybtn.ButtonTrayIndex = Me.wintraybtn.ButtonTrayIndex + 1
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.wintraybtn.ButtonTrayIndex = Me.wintraybtn.ButtonTrayIndex - 1
    End Sub

    Private Sub CheckBox1_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckStateChanged
        If CheckBox1.Checked = False Then
            Me.wintraybtn.ButtonEnabled = False
        ElseIf CheckBox1.Checked = True Then
            Me.wintraybtn.ButtonEnabled = True
        End If
    End Sub
    Friend WithEvents wintraybtn As WinTrayButton = New WinTrayButton(Me)

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        wintraybtn.ButtonImageList = b4imagelist
        wintraybtn.ButtonMenuText = "Tray Button Menu"
        TextBox1.Text = wintraybtn.ButtonMenuText
        Me.Label1.Text = "Current Button Index: " + wintraybtn.ButtonTrayIndex.ToString
        stylelist.Items.Add(FormBorderStyle.Fixed3D)
        stylelist.Items.Add(FormBorderStyle.FixedDialog)
        stylelist.Items.Add(FormBorderStyle.FixedSingle)
        stylelist.Items.Add(FormBorderStyle.FixedToolWindow)
        stylelist.Items.Add(FormBorderStyle.None)
        stylelist.Items.Add(FormBorderStyle.Sizable)
        stylelist.Items.Add(FormBorderStyle.SizableToolWindow)
    End Sub

    Private Sub stylelist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles stylelist.Click
        If stylelist.SelectedIndex <> -1 Then
            Me.FormBorderStyle = CType(stylelist.Items.Item(stylelist.SelectedIndex), FormBorderStyle)
        End If
    End Sub

    Private Sub CheckBox2_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckStateChanged
        If CheckBox2.Checked = False Then
            Me.wintraybtn.ButtonDrawImageOnly = False
        ElseIf CheckBox2.Checked = True Then
            Me.wintraybtn.ButtonDrawImageOnly = True
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            wintraybtn.ButtonImageList = b1imagelist
        Else
            wintraybtn.ButtonImageList = b4imagelist
        End If
    End Sub

    Private Sub CheckBox3_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckStateChanged
        If CheckBox3.Checked = False Then
            Me.wintraybtn.ButtonIsHelpButton = False
        ElseIf CheckBox3.Checked = True Then
            Me.wintraybtn.ButtonIsHelpButton = True
        End If
    End Sub

    Private Sub textbox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        wintraybtn.ButtonMenuText = Me.TextBox1.Text
    End Sub
End Class
