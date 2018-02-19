Public Class Form1
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents TreeList1 As BlueActivity.Controls.TreeList
    Friend WithEvents TreeListNode1 As BlueActivity.Controls.TreeListNode
    Friend WithEvents TreeListNode2 As BlueActivity.Controls.TreeListNode
    Friend WithEvents TreeListNode3 As BlueActivity.Controls.TreeListNode
    Friend WithEvents TreeListNode4 As BlueActivity.Controls.TreeListNode
    Friend WithEvents TreeListNode5 As BlueActivity.Controls.TreeListNode
    Friend WithEvents TreeListNode6 As BlueActivity.Controls.TreeListNode
    Friend WithEvents TreeListNode7 As BlueActivity.Controls.TreeListNode

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.TreeList1 = New BlueActivity.Controls.TreeList
        Me.TreeListNode1 = New BlueActivity.Controls.TreeListNode
        Me.TreeListNode4 = New BlueActivity.Controls.TreeListNode
        Me.TreeListNode5 = New BlueActivity.Controls.TreeListNode
        Me.TreeListNode2 = New BlueActivity.Controls.TreeListNode
        Me.TreeListNode6 = New BlueActivity.Controls.TreeListNode
        Me.TreeListNode7 = New BlueActivity.Controls.TreeListNode
        Me.TreeListNode3 = New BlueActivity.Controls.TreeListNode
        Me.SuspendLayout()
        '
        'TreeList1
        '
        Me.TreeList1.BackColor = System.Drawing.SystemColors.Control
        Me.TreeList1.BottomMargin = 3
        Me.TreeList1.DescriptionFont = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TreeList1.LeftMargin = 3
        Me.TreeList1.LinesStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.TreeList1.Location = New System.Drawing.Point(10, 10)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.Nodes.Add(Me.TreeListNode1)
        Me.TreeList1.Nodes.Add(Me.TreeListNode2)
        Me.TreeList1.Nodes.Add(Me.TreeListNode3)
        Me.TreeList1.PointedNode = Nothing
        Me.TreeList1.RightMargin = 3
        Me.TreeList1.SelectedBorderColor = System.Drawing.SystemColors.Highlight
        Me.TreeList1.SelectedNode = Nothing
        Me.TreeList1.SelectedTextColor = System.Drawing.SystemColors.Highlight
        Me.TreeList1.Size = New System.Drawing.Size(268, 274)
        Me.TreeList1.SpaceBetweenImageAndText = 2
        Me.TreeList1.SpaceBetweenTextAndDesc = 2
        Me.TreeList1.TabIndex = 0
        Me.TreeList1.TextFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeList1.TopMargin = 3
        '
        'TreeListNode1
        '
        Me.TreeListNode1.Depth = 1
        Me.TreeListNode1.Description = "This is my root node"
        Me.TreeListNode1.Image = CType(resources.GetObject("TreeListNode1.Image"), System.Drawing.Image)
        Me.TreeListNode1.Key = "0ae8ceb8-6828-4749-9fd7-a642085c2706"
        Me.TreeListNode1.Left = 0
        Me.TreeListNode1.Nodes.Add(Me.TreeListNode4)
        Me.TreeListNode1.Nodes.Add(Me.TreeListNode5)
        Me.TreeListNode1.Shown = False
        Me.TreeListNode1.Tag = Nothing
        Me.TreeListNode1.Text = "My root node A"
        Me.TreeListNode1.Top = 3
        Me.TreeListNode1.TreeList = Me.TreeList1
        Me.TreeListNode1.Type = 0
        Me.TreeListNode1.Visible = True
        '
        'TreeListNode4
        '
        Me.TreeListNode4.Depth = 2
        Me.TreeListNode4.Description = "I am a child of A"
        Me.TreeListNode4.Image = CType(resources.GetObject("TreeListNode4.Image"), System.Drawing.Image)
        Me.TreeListNode4.Key = "71394cc6-48b9-4165-97cb-9aa9e0b836c5"
        Me.TreeListNode4.Left = 0
        Me.TreeListNode4.Shown = False
        Me.TreeListNode4.Tag = Nothing
        Me.TreeListNode4.Text = "My child 1"
        Me.TreeListNode4.Top = 42
        Me.TreeListNode4.TreeList = Me.TreeList1
        Me.TreeListNode4.Type = 0
        Me.TreeListNode4.Visible = True
        '
        'TreeListNode5
        '
        Me.TreeListNode5.Depth = 2
        Me.TreeListNode5.Description = Nothing
        Me.TreeListNode5.Image = CType(resources.GetObject("TreeListNode5.Image"), System.Drawing.Image)
        Me.TreeListNode5.Key = "297c55ac-9865-48e5-b9bb-61217e6e8aa4"
        Me.TreeListNode5.Left = 0
        Me.TreeListNode5.Shown = False
        Me.TreeListNode5.Tag = Nothing
        Me.TreeListNode5.Text = "My Child 2"
        Me.TreeListNode5.Top = 81
        Me.TreeListNode5.TreeList = Me.TreeList1
        Me.TreeListNode5.Type = 0
        Me.TreeListNode5.Visible = True
        '
        'TreeListNode2
        '
        Me.TreeListNode2.Depth = 1
        Me.TreeListNode2.Description = Nothing
        Me.TreeListNode2.Image = CType(resources.GetObject("TreeListNode2.Image"), System.Drawing.Image)
        Me.TreeListNode2.Key = "9b5a9c7a-37f1-450c-be33-5ee0a3145914"
        Me.TreeListNode2.Left = 0
        Me.TreeListNode2.Nodes.Add(Me.TreeListNode6)
        Me.TreeListNode2.Nodes.Add(Me.TreeListNode7)
        Me.TreeListNode2.Shown = False
        Me.TreeListNode2.Tag = Nothing
        Me.TreeListNode2.Text = "My root node B"
        Me.TreeListNode2.Top = 105
        Me.TreeListNode2.TreeList = Me.TreeList1
        Me.TreeListNode2.Type = 0
        Me.TreeListNode2.Visible = True
        '
        'TreeListNode6
        '
        Me.TreeListNode6.Depth = 2
        Me.TreeListNode6.Description = Nothing
        Me.TreeListNode6.Image = CType(resources.GetObject("TreeListNode6.Image"), System.Drawing.Image)
        Me.TreeListNode6.Key = "c22d2491-5ecb-4900-aeb7-ba50e3ddbaed"
        Me.TreeListNode6.Left = 0
        Me.TreeListNode6.Shown = False
        Me.TreeListNode6.Tag = Nothing
        Me.TreeListNode6.Text = "My child 1"
        Me.TreeListNode6.Top = 127
        Me.TreeListNode6.TreeList = Me.TreeList1
        Me.TreeListNode6.Type = 0
        Me.TreeListNode6.Visible = True
        '
        'TreeListNode7
        '
        Me.TreeListNode7.Depth = 2
        Me.TreeListNode7.Description = "With a description"
        Me.TreeListNode7.Image = CType(resources.GetObject("TreeListNode7.Image"), System.Drawing.Image)
        Me.TreeListNode7.Key = "06058389-d7a2-4995-a41b-22aba0096c55"
        Me.TreeListNode7.Left = 0
        Me.TreeListNode7.Shown = False
        Me.TreeListNode7.Tag = Nothing
        Me.TreeListNode7.Text = "My child 2"
        Me.TreeListNode7.Top = 151
        Me.TreeListNode7.TreeList = Me.TreeList1
        Me.TreeListNode7.Type = 0
        Me.TreeListNode7.Visible = True
        '
        'TreeListNode3
        '
        Me.TreeListNode3.Depth = 1
        Me.TreeListNode3.Description = "This also has a description"
        Me.TreeListNode3.Image = CType(resources.GetObject("TreeListNode3.Image"), System.Drawing.Image)
        Me.TreeListNode3.Key = "bedf2fee-9fe7-409e-9bf8-8892df50f027"
        Me.TreeListNode3.Left = 0
        Me.TreeListNode3.Shown = False
        Me.TreeListNode3.Tag = Nothing
        Me.TreeListNode3.Text = "My root node C"
        Me.TreeListNode3.Top = 129
        Me.TreeListNode3.TreeList = Me.TreeList1
        Me.TreeListNode3.Type = 0
        Me.TreeListNode3.Visible = True
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(288, 294)
        Me.Controls.Add(Me.TreeList1)
        Me.DockPadding.All = 10
        Me.Name = "Form1"
        Me.Text = "TreeList Control Demo"
        Me.ResumeLayout(False)

    End Sub


    Private Sub TreeList1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TreeList1.Click

    End Sub
End Class
