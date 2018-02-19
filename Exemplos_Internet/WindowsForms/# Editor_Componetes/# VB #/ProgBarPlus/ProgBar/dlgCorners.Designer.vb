<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgCorners
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim CBlendItems2 As ProgBar.cBlendItems = New ProgBar.cBlendItems
        Dim CFocalPoints2 As ProgBar.cFocalPoints = New ProgBar.cFocalPoints
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCorners))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.panShapeHolder = New System.Windows.Forms.Panel
        Me.tbarUpperLeft = New System.Windows.Forms.TrackBar
        Me.tbarUpperRight = New System.Windows.Forms.TrackBar
        Me.tbarLowerLeft = New System.Windows.Forms.TrackBar
        Me.tbarLowerRight = New System.Windows.Forms.TrackBar
        Me.tbarAll = New System.Windows.Forms.TrackBar
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.HSBarSample = New System.Windows.Forms.HScrollBar
        Me.TheShape = New ProgBar.ProgBarPlus
        Me.TableLayoutPanel1.SuspendLayout()
        Me.panShapeHolder.SuspendLayout()
        CType(Me.tbarUpperLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarUpperRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarLowerLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarLowerRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(185, 302)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'panShapeHolder
        '
        Me.panShapeHolder.Controls.Add(Me.TheShape)
        Me.panShapeHolder.Location = New System.Drawing.Point(71, 54)
        Me.panShapeHolder.Name = "panShapeHolder"
        Me.panShapeHolder.Size = New System.Drawing.Size(200, 200)
        Me.panShapeHolder.TabIndex = 5
        '
        'tbarUpperLeft
        '
        Me.tbarUpperLeft.Location = New System.Drawing.Point(71, 12)
        Me.tbarUpperLeft.Maximum = 50
        Me.tbarUpperLeft.Name = "tbarUpperLeft"
        Me.tbarUpperLeft.Size = New System.Drawing.Size(125, 45)
        Me.tbarUpperLeft.TabIndex = 6
        Me.tbarUpperLeft.TickFrequency = 50
        '
        'tbarUpperRight
        '
        Me.tbarUpperRight.Location = New System.Drawing.Point(289, 54)
        Me.tbarUpperRight.Maximum = 100
        Me.tbarUpperRight.Name = "tbarUpperRight"
        Me.tbarUpperRight.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbarUpperRight.Size = New System.Drawing.Size(45, 125)
        Me.tbarUpperRight.TabIndex = 7
        Me.tbarUpperRight.TickFrequency = 50
        Me.tbarUpperRight.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        '
        'tbarLowerLeft
        '
        Me.tbarLowerLeft.Location = New System.Drawing.Point(12, 129)
        Me.tbarLowerLeft.Maximum = 50
        Me.tbarLowerLeft.Name = "tbarLowerLeft"
        Me.tbarLowerLeft.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbarLowerLeft.Size = New System.Drawing.Size(45, 125)
        Me.tbarLowerLeft.TabIndex = 8
        Me.tbarLowerLeft.TickFrequency = 50
        '
        'tbarLowerRight
        '
        Me.tbarLowerRight.Location = New System.Drawing.Point(146, 265)
        Me.tbarLowerRight.Maximum = 50
        Me.tbarLowerRight.Name = "tbarLowerRight"
        Me.tbarLowerRight.Size = New System.Drawing.Size(125, 45)
        Me.tbarLowerRight.TabIndex = 9
        Me.tbarLowerRight.TickFrequency = 50
        Me.tbarLowerRight.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        '
        'tbarAll
        '
        Me.tbarAll.Location = New System.Drawing.Point(13, 300)
        Me.tbarAll.Maximum = 50
        Me.tbarAll.Name = "tbarAll"
        Me.tbarAll.Size = New System.Drawing.Size(125, 45)
        Me.tbarAll.TabIndex = 6
        Me.tbarAll.TickFrequency = 50
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "UpperLeft"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(274, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "UpperRight"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(88, 271)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "LowerRight"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "LowerLeft"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 284)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(18, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "All"
        '
        'HSBarSample
        '
        Me.HSBarSample.Location = New System.Drawing.Point(55, 253)
        Me.HSBarSample.Maximum = 109
        Me.HSBarSample.Name = "HSBarSample"
        Me.HSBarSample.Size = New System.Drawing.Size(233, 13)
        Me.HSBarSample.TabIndex = 11
        Me.HSBarSample.Value = 100
        '
        'TheShape
        '
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.White, System.Drawing.Color.White}
        CBlendItems2.iPoint = New Single() {0.0!, 1.0!}
        Me.TheShape.BarColorBlend = CBlendItems2
        Me.TheShape.BarColorSolid = System.Drawing.Color.Blue
        Me.TheShape.BarColorSolidB = System.Drawing.Color.White
        Me.TheShape.BarLength = ProgBar.ProgBarPlus.eBarLength.Full
        Me.TheShape.BarLengthValue = CType(25, Short)
        Me.TheShape.BarPadding = New System.Windows.Forms.Padding(0)
        Me.TheShape.BarStyleFill = ProgBar.ProgBarPlus.eBarStyle.Solid
        Me.TheShape.BarStyleHatch = System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard
        Me.TheShape.BarStyleLinear = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.TheShape.BarStyleTexture = Nothing
        Me.TheShape.BarStyleWrapMode = System.Drawing.Drawing2D.WrapMode.Clamp
        Me.TheShape.BarType = ProgBar.ProgBarPlus.eBarType.Bar
        Me.TheShape.BorderWidth = CType(1, Short)
        Me.TheShape.Corners.All = CType(0, Short)
        Me.TheShape.Corners.LowerLeft = CType(0, Short)
        Me.TheShape.Corners.LowerRight = CType(0, Short)
        Me.TheShape.Corners.UpperLeft = CType(0, Short)
        Me.TheShape.Corners.UpperRight = CType(0, Short)
        Me.TheShape.CornersApply = ProgBar.ProgBarPlus.eCornersApply.Both
        Me.TheShape.CylonInterval = CType(1, Short)
        Me.TheShape.CylonMove = 5.0!
        Me.TheShape.FillDirection = ProgBar.ProgBarPlus.eFillDirection.Up_Right
        CFocalPoints2.CenterPoint = CType(resources.GetObject("CFocalPoints2.CenterPoint"), System.Drawing.PointF)
        CFocalPoints2.FocusScales = CType(resources.GetObject("CFocalPoints2.FocusScales"), System.Drawing.PointF)
        Me.TheShape.FocalPoints = CFocalPoints2
        Me.TheShape.Location = New System.Drawing.Point(0, 0)
        Me.TheShape.Name = "TheShape"
        Me.TheShape.Orientation = ProgBar.ProgBarPlus.eOrientation.Horizontal
        Me.TheShape.Shape = ProgBar.ProgBarPlus.eShape.Rectangle
        Me.TheShape.ShapeTextFont = New System.Drawing.Font("Arial Black", 30.0!)
        Me.TheShape.ShapeTextRotate = ProgBar.ProgBarPlus.eRotateText.None
        Me.TheShape.Size = New System.Drawing.Size(200, 200)
        Me.TheShape.TabIndex = 0
        Me.TheShape.TextAlignment = System.Drawing.StringAlignment.Center
        Me.TheShape.TextAlignmentVert = System.Drawing.StringAlignment.Center
        Me.TheShape.TextPlacement = ProgBar.ProgBarPlus.eTextPlacement.OverBar
        Me.TheShape.TextRotate = ProgBar.ProgBarPlus.eRotateText.None
        Me.TheShape.TextShow = ProgBar.ProgBarPlus.eTextShow.None
        Me.TheShape.Value = 100
        '
        'dlgCorners
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(340, 339)
        Me.Controls.Add(Me.HSBarSample)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbarUpperRight)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.panShapeHolder)
        Me.Controls.Add(Me.tbarLowerRight)
        Me.Controls.Add(Me.tbarLowerLeft)
        Me.Controls.Add(Me.tbarAll)
        Me.Controls.Add(Me.tbarUpperLeft)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgCorners"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Corner Adjustment"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.panShapeHolder.ResumeLayout(False)
        CType(Me.tbarUpperLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarUpperRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarLowerLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarLowerRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents panShapeHolder As System.Windows.Forms.Panel
    Friend WithEvents tbarUpperLeft As System.Windows.Forms.TrackBar
    Friend WithEvents tbarUpperRight As System.Windows.Forms.TrackBar
    Friend WithEvents tbarLowerLeft As System.Windows.Forms.TrackBar
    Friend WithEvents tbarLowerRight As System.Windows.Forms.TrackBar
    Friend WithEvents tbarAll As System.Windows.Forms.TrackBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TheShape As ProgBar.ProgBarPlus
    Friend WithEvents HSBarSample As System.Windows.Forms.HScrollBar

End Class
