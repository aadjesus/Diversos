<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MyForm
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
    Me.components = New System.ComponentModel.Container
    Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
    Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New System.Windows.Forms.ListViewItem.ListViewSubItem() {New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, "LargeImageList"), New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, "0", System.Drawing.SystemColors.GrayText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))}, 0)
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.lvcolName = New System.Windows.Forms.ColumnHeader
    Me.lvcolHandle = New System.Windows.Forms.ColumnHeader
    Me.SharedImageLists11 = New WindowsApplication2.SharedImageLists1(Me.components)
    Me.LargeImageList = Me.SharedImageLists11.NewImageList(Me.components, CType(Me.SharedImageLists11.GetSharedImageLists, WindowsApplication2.SharedImageLists1).LargeImageList)
    Me.btnShowInheritedForm = New System.Windows.Forms.Button
    Me.MyUserControl1 = New WindowsApplication2.MyUserControl
    Me.SuspendLayout()
    '
    'ListView1
    '
    Me.ListView1.Activation = System.Windows.Forms.ItemActivation.OneClick
    Me.ListView1.Alignment = System.Windows.Forms.ListViewAlignment.[Default]
    Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvcolName, Me.lvcolHandle})
    ListViewGroup2.Header = "ListViewGroup"
    ListViewGroup2.Name = "ListViewGroup1"
    Me.ListView1.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup2})
    ListViewItem2.Group = ListViewGroup2
    ListViewItem2.IndentCount = 1
    ListViewItem2.StateImageIndex = 0
    ListViewItem2.ToolTipText = "aaa"
    Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem2})
    Me.ListView1.LargeImageList = Me.LargeImageList
    Me.ListView1.Location = New System.Drawing.Point(12, 12)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(266, 162)
    Me.ListView1.TabIndex = 0
    Me.ListView1.TileSize = New System.Drawing.Size(260, 60)
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Tile
    '
    'btnShowInheritedForm
    '
    Me.btnShowInheritedForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnShowInheritedForm.Location = New System.Drawing.Point(346, 110)
    Me.btnShowInheritedForm.Name = "btnShowInheritedForm"
    Me.btnShowInheritedForm.Size = New System.Drawing.Size(133, 64)
    Me.btnShowInheritedForm.TabIndex = 1
    Me.btnShowInheritedForm.Text = "Show Inherited Form"
    Me.btnShowInheritedForm.UseVisualStyleBackColor = True
    '
    'MyUserControl1
    '
    Me.MyUserControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.MyUserControl1.Location = New System.Drawing.Point(285, 12)
    Me.MyUserControl1.Name = "MyUserControl1"
    Me.MyUserControl1.Size = New System.Drawing.Size(194, 92)
    Me.MyUserControl1.TabIndex = 2
    '
    'MyForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(491, 186)
    Me.Controls.Add(Me.MyUserControl1)
    Me.Controls.Add(Me.btnShowInheritedForm)
    Me.Controls.Add(Me.ListView1)
    Me.Name = "MyForm"
    Me.Text = "MyForm"
    Me.ResumeLayout(False)

  End Sub
  Protected Friend WithEvents SharedImageLists11 As WindowsApplication2.SharedImageLists1
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents lvcolName As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvcolHandle As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnShowInheritedForm As System.Windows.Forms.Button
  Friend WithEvents MyUserControl1 As WindowsApplication2.MyUserControl
  Protected Friend WithEvents LargeImageList As System.Windows.Forms.ImageList
End Class
