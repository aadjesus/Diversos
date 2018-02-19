Imports System.ComponentModel

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.PropertyGrid1.SelectedObject = New ClassToEdit
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
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.SuspendLayout()
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.CommandsVisibleIfAvailable = True
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.LargeButtons = False
        Me.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(327, 214)
        Me.PropertyGrid1.TabIndex = 0
        Me.PropertyGrid1.Text = "PropertyGrid1"
        Me.PropertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window
        Me.PropertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(327, 214)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class

Public Class ClassToEdit
    Private _filenameToOpen As String
    Private _filenameToSave As String

    <Editor(GetType(UIFilenameEditor.UIFilenameEditor), _
                GetType(Drawing.Design.UITypeEditor)), _
         UIFilenameEditor.FileDialogFilter("Assembly files (*.dll, *.exe)|*.dll;*.exe|All files (*.*)|*.*"), _
                Description("The filename to open."), Category("Filenames")> _
    Public Property FilenameToOpen() As String
        Get
            Return Me._filenameToOpen
        End Get
        Set(ByVal Value As String)
            Me._filenameToOpen = Value
        End Set
    End Property

    <Editor(GetType(UIFilenameEditor.UIFilenameEditor), _
        GetType(Drawing.Design.UITypeEditor)), UIFilenameEditor.SaveFile(), _
         UIFilenameEditor.FileDialogFilter("Text files (*.txt)|*.txt|All files (*.*)|*.*"), _
        Description("The filename to save."), Category("Filenames")> _
    Public Property FilenameToSave() As String
        Get
            Return Me._filenameToSave
        End Get
        Set(ByVal Value As String)
            Me._filenameToSave = Value
        End Set
    End Property
End Class