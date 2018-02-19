Namespace Treeview_Rearrange

    Public Class Form1
        Inherits System.Windows.Forms.Form
        Private NodeCount As Integer
        Private FolderCount As Integer
        Private NodeMap As String
        Private Const MAPSIZE As Integer = 128
        Private NewNodeMap As New System.Text.StringBuilder(MAPSIZE)


#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            Me.treeView1.ImageList = New ImageList
            Me.treeView1.ImageList.Images.Add(System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("Treeview_Rearrange.folder.png")))
            Me.treeView1.ImageList.Images.Add(System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("Treeview_Rearrange.node.png")))

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
        Friend WithEvents treeView1 As System.Windows.Forms.TreeView
        Friend WithEvents btnEnable As System.Windows.Forms.Button
        Friend WithEvents btnAddFolder As System.Windows.Forms.Button
        Friend WithEvents btnAddNode As System.Windows.Forms.Button
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.treeView1 = New System.Windows.Forms.TreeView
            Me.btnEnable = New System.Windows.Forms.Button
            Me.btnAddFolder = New System.Windows.Forms.Button
            Me.btnAddNode = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'treeView1
            '
            Me.treeView1.AllowDrop = True
            Me.treeView1.ImageIndex = -1
            Me.treeView1.Location = New System.Drawing.Point(8, 8)
            Me.treeView1.Name = "treeView1"
            Me.treeView1.SelectedImageIndex = -1
            Me.treeView1.Size = New System.Drawing.Size(216, 224)
            Me.treeView1.TabIndex = 1
            '
            'btnEnable
            '
            Me.btnEnable.Location = New System.Drawing.Point(8, 272)
            Me.btnEnable.Name = "btnEnable"
            Me.btnEnable.Size = New System.Drawing.Size(216, 23)
            Me.btnEnable.TabIndex = 6
            Me.btnEnable.Text = "Enable drag drop rearrange support"
            '
            'btnAddFolder
            '
            Me.btnAddFolder.Location = New System.Drawing.Point(120, 240)
            Me.btnAddFolder.Name = "btnAddFolder"
            Me.btnAddFolder.Size = New System.Drawing.Size(104, 23)
            Me.btnAddFolder.TabIndex = 5
            Me.btnAddFolder.Text = "Add Folder"
            '
            'btnAddNode
            '
            Me.btnAddNode.Location = New System.Drawing.Point(8, 240)
            Me.btnAddNode.Name = "btnAddNode"
            Me.btnAddNode.Size = New System.Drawing.Size(104, 23)
            Me.btnAddNode.TabIndex = 4
            Me.btnAddNode.Text = "Add Node"
            '
            'Form1
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(232, 298)
            Me.Controls.Add(Me.btnEnable)
            Me.Controls.Add(Me.btnAddFolder)
            Me.Controls.Add(Me.btnAddNode)
            Me.Controls.Add(Me.treeView1)
            Me.Name = "Form1"
            Me.Text = "Treeview Rearrange"
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Sub btnAddNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNode.Click
            System.Threading.Interlocked.Increment(Me.NodeCount)
            Me.treeView1.Nodes.Add(New TreeNode("Node #" + Me.NodeCount.ToString, 1, 1))
        End Sub
        Private Sub btnAddFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFolder.Click
            System.Threading.Interlocked.Increment(Me.FolderCount)
            Me.treeView1.Nodes.Add(New TreeNode("Folder #" + Me.FolderCount.ToString, 0, 0))
        End Sub
        Private Sub btnEnable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnable.Click
            AddHandler Me.treeView1.MouseDown, AddressOf Me.treeView1_MouseDown
            AddHandler Me.treeView1.DragOver, AddressOf Me.treeView1_DragOver
            AddHandler Me.treeView1.DragEnter, AddressOf Me.treeView1_DragEnter
            AddHandler Me.treeView1.ItemDrag, AddressOf Me.treeView1_ItemDrag
            AddHandler Me.treeView1.DragDrop, AddressOf Me.treeView1_DragDrop
            Me.btnEnable.Enabled = False
        End Sub

        Private Sub treeView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            Me.treeView1.SelectedNode = Me.treeView1.GetNodeAt(e.X, e.Y)
        End Sub
        Private Sub treeView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs)
            DoDragDrop(e.Item, DragDropEffects.Move)
        End Sub
        Private Sub treeView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
            e.Effect = DragDropEffects.Move
        End Sub
        Private Sub treeView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) AndAlso Not (Me.NodeMap = "") Then
                Dim MovingNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
                Dim NodeIndexes As String() = Me.NodeMap.Split("|"c)
                Dim InsertCollection As TreeNodeCollection = Me.treeView1.Nodes
                Dim i As Integer = 0
                While i < NodeIndexes.Length - 1
                    InsertCollection = InsertCollection(Int32.Parse(NodeIndexes(i))).Nodes
                    System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
                End While
                If Not (InsertCollection Is Nothing) Then
                    InsertCollection.Insert(Int32.Parse(NodeIndexes(NodeIndexes.Length - 1)), CType(MovingNode.Clone, TreeNode))
                    Me.treeView1.SelectedNode = InsertCollection(Int32.Parse(NodeIndexes(NodeIndexes.Length - 1)))
                    MovingNode.Remove()
                End If
            End If
        End Sub
        Private Sub treeView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
            Dim NodeOver As TreeNode = Me.treeView1.GetNodeAt(Me.treeView1.PointToClient(Cursor.Position))
            Dim NodeMoving As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
            If Not (NodeOver Is Nothing) AndAlso (Not (NodeOver Is NodeMoving) OrElse (Not (NodeOver.Parent Is Nothing) AndAlso NodeOver.Index = (NodeOver.Parent.Nodes.Count - 1))) Then
                Dim OffsetY As Integer = Me.treeView1.PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top
                Dim NodeOverImageWidth As Integer = Me.treeView1.ImageList.Images(NodeOver.ImageIndex).Size.Width + 8
                Dim g As Graphics = Me.treeView1.CreateGraphics
                If NodeOver.ImageIndex = 1 Then
                    If OffsetY < (NodeOver.Bounds.Height / 2) Then
                        Dim tnParadox As TreeNode = NodeOver
                        While Not (tnParadox.Parent Is Nothing)
                            If tnParadox.Parent Is NodeMoving Then
                                Me.NodeMap = ""
                                Return
                            End If
                            tnParadox = tnParadox.Parent
                        End While
                        SetNewNodeMap(NodeOver, False)  ' ERROR!  Not sure why
                        If SetMapsEqual() = True Then
                            Return
                        End If
                        Me.Refresh()
                        Me.DrawLeafTopPlaceholders(NodeOver)
                    Else
                        Dim tnParadox As TreeNode = NodeOver
                        While Not (tnParadox.Parent Is Nothing)
                            If tnParadox.Parent Is NodeMoving Then
                                Me.NodeMap = ""
                                Return
                            End If
                            tnParadox = tnParadox.Parent
                        End While
                        Dim ParentDragDrop As TreeNode = Nothing
                        If Not (NodeOver.Parent Is Nothing) AndAlso NodeOver.Index = (NodeOver.Parent.Nodes.Count - 1) Then
                            Dim XPos As Integer = Me.treeView1.PointToClient(Cursor.Position).X
                            If XPos < NodeOver.Bounds.Left Then
                                ParentDragDrop = NodeOver.Parent
                                If XPos < (ParentDragDrop.Bounds.Left - Me.treeView1.ImageList.Images(ParentDragDrop.ImageIndex).Size.Width) Then
                                    If Not (ParentDragDrop.Parent Is Nothing) Then
                                        ParentDragDrop = ParentDragDrop.Parent
                                    End If
                                End If
                            End If
                        End If
                        SetNewNodeMap(Microsoft.VisualBasic.IIf(Not (ParentDragDrop Is Nothing), ParentDragDrop, NodeOver), True)
                        If SetMapsEqual() = True Then
                            Return
                        End If
                        Me.Refresh()
                        DrawLeafBottomPlaceholders(NodeOver, ParentDragDrop)
                    End If
                Else
                    If OffsetY < (NodeOver.Bounds.Height / 3) Then
                        Dim tnParadox As TreeNode = NodeOver
                        While Not (tnParadox.Parent Is Nothing)
                            If tnParadox.Parent Is NodeMoving Then
                                Me.NodeMap = ""
                                Return
                            End If
                            tnParadox = tnParadox.Parent
                        End While
                        SetNewNodeMap(NodeOver, False)
                        If SetMapsEqual() = True Then
                            Return
                        End If
                        Me.Refresh()
                        Me.DrawFolderTopPlaceholders(NodeOver)
                    Else
                        If (Not (NodeOver.Parent Is Nothing) AndAlso NodeOver.Index = 0) AndAlso (OffsetY > (NodeOver.Bounds.Height - (NodeOver.Bounds.Height / 3))) Then
                            Dim tnParadox As TreeNode = NodeOver
                            While Not (tnParadox.Parent Is Nothing)
                                If tnParadox.Parent Is NodeMoving Then
                                    Me.NodeMap = ""
                                    Return
                                End If
                                tnParadox = tnParadox.Parent
                            End While
                            SetNewNodeMap(NodeOver, True)
                            If SetMapsEqual() = True Then
                                Return
                            End If
                            Me.Refresh()
                            DrawFolderTopPlaceholders(NodeOver)
                        Else
                            If NodeOver.Nodes.Count > 0 Then
                                NodeOver.Expand()
                            Else
                                If NodeMoving Is NodeOver Then
                                    Return
                                End If
                                Dim tnParadox As TreeNode = NodeOver
                                While Not (tnParadox.Parent Is Nothing)
                                    If tnParadox.Parent Is NodeMoving Then
                                        Me.NodeMap = ""
                                        Return
                                    End If
                                    tnParadox = tnParadox.Parent
                                End While
                                SetNewNodeMap(NodeOver, False)
                                NewNodeMap = NewNodeMap.Insert(NewNodeMap.Length, "|0")
                                If SetMapsEqual() = True Then
                                    Return
                                End If
                                Me.Refresh()
                                DrawAddToFolderPlaceholder(NodeOver)
                            End If
                        End If
                    End If
                End If
            End If
        End Sub
        Private Sub DrawLeafTopPlaceholders(ByVal NodeOver As TreeNode)
            Dim g As Graphics = Me.treeView1.CreateGraphics
            Dim NodeOverImageWidth As Integer = Me.treeView1.ImageList.Images(NodeOver.ImageIndex).Size.Width + 8
            Dim LeftPos As Integer = NodeOver.Bounds.Left - NodeOverImageWidth
            Dim RightPos As Integer = Me.treeView1.Width - 4
            Dim LeftTriangle As Point() = New Point(4) {New Point(LeftPos, NodeOver.Bounds.Top - 4), New Point(LeftPos, NodeOver.Bounds.Top + 4), New Point(LeftPos + 4, NodeOver.Bounds.Y), New Point(LeftPos + 4, NodeOver.Bounds.Top - 1), New Point(LeftPos, NodeOver.Bounds.Top - 5)}
            Dim RightTriangle As Point() = New Point(4) {New Point(RightPos, NodeOver.Bounds.Top - 4), New Point(RightPos, NodeOver.Bounds.Top + 4), New Point(RightPos - 4, NodeOver.Bounds.Y), New Point(RightPos - 4, NodeOver.Bounds.Top - 1), New Point(RightPos, NodeOver.Bounds.Top - 5)}
            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle)
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle)
            g.DrawLine(New System.Drawing.Pen(Color.Black, 2), New Point(LeftPos, NodeOver.Bounds.Top), New Point(RightPos, NodeOver.Bounds.Top))
        End Sub
        Private Sub DrawLeafBottomPlaceholders(ByVal NodeOver As TreeNode, ByVal ParentDragDrop As TreeNode)
            Dim g As Graphics = Me.treeView1.CreateGraphics
            Dim NodeOverImageWidth As Integer = Me.treeView1.ImageList.Images(NodeOver.ImageIndex).Size.Width + 8
            Dim LeftPos As Integer
            Dim RightPos As Integer
            If Not (ParentDragDrop Is Nothing) Then
                LeftPos = ParentDragDrop.Bounds.Left - (Me.treeView1.ImageList.Images(ParentDragDrop.ImageIndex).Size.Width + 8)
            Else
                LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth
            End If
            RightPos = Me.treeView1.Width - 4
            Dim LeftTriangle As Point() = New Point(4) {New Point(LeftPos, NodeOver.Bounds.Bottom - 4), New Point(LeftPos, NodeOver.Bounds.Bottom + 4), New Point(LeftPos + 4, NodeOver.Bounds.Bottom), New Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1), New Point(LeftPos, NodeOver.Bounds.Bottom - 5)}
            Dim RightTriangle As Point() = New Point(4) {New Point(RightPos, NodeOver.Bounds.Bottom - 4), New Point(RightPos, NodeOver.Bounds.Bottom + 4), New Point(RightPos - 4, NodeOver.Bounds.Bottom), New Point(RightPos - 4, NodeOver.Bounds.Bottom - 1), New Point(RightPos, NodeOver.Bounds.Bottom - 5)}
            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle)
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle)
            g.DrawLine(New System.Drawing.Pen(Color.Black, 2), New Point(LeftPos, NodeOver.Bounds.Bottom), New Point(RightPos, NodeOver.Bounds.Bottom))
        End Sub
        Private Sub DrawFolderTopPlaceholders(ByVal NodeOver As TreeNode)
            Dim g As Graphics = Me.treeView1.CreateGraphics
            Dim NodeOverImageWidth As Integer = Me.treeView1.ImageList.Images(NodeOver.ImageIndex).Size.Width + 8
            Dim LeftPos As Integer
            Dim RightPos As Integer
            LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth
            RightPos = Me.treeView1.Width - 4
            Dim LeftTriangle As Point() = New Point(4) {New Point(LeftPos, NodeOver.Bounds.Top - 4), New Point(LeftPos, NodeOver.Bounds.Top + 4), New Point(LeftPos + 4, NodeOver.Bounds.Y), New Point(LeftPos + 4, NodeOver.Bounds.Top - 1), New Point(LeftPos, NodeOver.Bounds.Top - 5)}
            Dim RightTriangle As Point() = New Point(4) {New Point(RightPos, NodeOver.Bounds.Top - 4), New Point(RightPos, NodeOver.Bounds.Top + 4), New Point(RightPos - 4, NodeOver.Bounds.Y), New Point(RightPos - 4, NodeOver.Bounds.Top - 1), New Point(RightPos, NodeOver.Bounds.Top - 5)}
            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle)
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle)
            g.DrawLine(New System.Drawing.Pen(Color.Black, 2), New Point(LeftPos, NodeOver.Bounds.Top), New Point(RightPos, NodeOver.Bounds.Top))
        End Sub
        Private Sub DrawAddToFolderPlaceholder(ByVal NodeOver As TreeNode)
            Dim g As Graphics = Me.treeView1.CreateGraphics
            Dim RightPos As Integer = NodeOver.Bounds.Right + 6
            Dim RightTriangle As Point() = New Point(4) {New Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4), New Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4), New Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)), New Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1), New Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)}
            Me.Refresh()
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle)
        End Sub
        Private Sub SetNewNodeMap(ByVal tnNode As TreeNode, ByVal boolBelowNode As Boolean)
            NewNodeMap.Length = 0
            If boolBelowNode Then
                NewNodeMap.Insert(0, CType(tnNode.Index, Integer) + 1)
            Else
                NewNodeMap.Insert(0, CType(tnNode.Index, Integer))
            End If
            Dim tnCurNode As TreeNode = tnNode
            While Not (tnCurNode.Parent Is Nothing)
                tnCurNode = tnCurNode.Parent
                If NewNodeMap.Length = 0 AndAlso boolBelowNode = True Then
                    NewNodeMap.Insert(0, (tnCurNode.Index + 1) + "|")
                Else
                    NewNodeMap.Insert(0, tnCurNode.Index + "|")
                End If
            End While
        End Sub
        Private Function SetMapsEqual() As Boolean
            If Me.NewNodeMap.ToString = Me.NodeMap Then
                Return True
            Else
                Me.NodeMap = Me.NewNodeMap.ToString
                Return False
            End If
        End Function
    End Class
End Namespace
