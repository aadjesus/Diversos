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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Friend WithEvents wintraybtn As WinTrayButton = New WinTrayButton(Me)

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        wintraybtn.ButtonImageList = ImageList1
        wintraybtn.ButtonMenuText = "Tray Button Menu"

    End Sub

    Private Sub wintraybtn_MinTrayBtnClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles wintraybtn.MinTrayBtnClicked
        MessageBox.Show("You just clicked a tray button")
    End Sub

    Private Sub wintraybtn_MinTrayBtnIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles wintraybtn.MinTrayBtnIndexChange
        Me.Text = "Current Button Index: " + wintraybtn.ButtonTrayIndex.ToString
    End Sub

    Private Sub wintraybtn_MinTrayBtnStateChanged(ByVal sender As Object, ByVal st As WinTrayButton.state) Handles wintraybtn.MinTrayBtnStateChanged
        Me.Text = "Button state is now: " + st.ToString
    End Sub
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.DoEvents()
        Application.Run(New Form1)
    End Sub
End Class
