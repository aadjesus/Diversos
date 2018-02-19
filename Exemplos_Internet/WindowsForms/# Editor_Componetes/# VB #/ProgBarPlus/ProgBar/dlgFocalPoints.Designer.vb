<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgFocalPoints
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
        Dim CBlendItems1 As ProgBar.cBlendItems = New ProgBar.cBlendItems
        Dim CFocalPoints1 As ProgBar.cFocalPoints = New ProgBar.cFocalPoints
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgFocalPoints))
        Me.butApply = New System.Windows.Forms.Button
        Me.tbarFocalX = New System.Windows.Forms.TrackBar
        Me.tbarFocalY = New System.Windows.Forms.TrackBar
        Me.panShapeHolder = New System.Windows.Forms.Panel
        Me.TheShape = New ProgBar.ProgBarPlus
        Me.lblFy = New System.Windows.Forms.Label
        Me.lblFx = New System.Windows.Forms.Label
        Me.lblCx = New System.Windows.Forms.Label
        Me.lblCy = New System.Windows.Forms.Label
        Me.butCancel = New System.Windows.Forms.Button
        CType(Me.tbarFocalX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarFocalY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panShapeHolder.SuspendLayout()
        Me.SuspendLayout()
        '
        'butApply
        '
        Me.butApply.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.butApply.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butApply.Location = New System.Drawing.Point(106, 301)
        Me.butApply.Name = "butApply"
        Me.butApply.Size = New System.Drawing.Size(67, 23)
        Me.butApply.TabIndex = 0
        Me.butApply.Text = "Apply"
        '
        'tbarFocalX
        '
        Me.tbarFocalX.LargeChange = 10
        Me.tbarFocalX.Location = New System.Drawing.Point(12, 218)
        Me.tbarFocalX.Maximum = 1000
        Me.tbarFocalX.Name = "tbarFocalX"
        Me.tbarFocalX.Size = New System.Drawing.Size(200, 45)
        Me.tbarFocalX.TabIndex = 3
        Me.tbarFocalX.TickFrequency = 50
        Me.tbarFocalX.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        Me.tbarFocalX.Value = 500
        '
        'tbarFocalY
        '
        Me.tbarFocalY.Location = New System.Drawing.Point(218, 12)
        Me.tbarFocalY.Maximum = 1000
        Me.tbarFocalY.Name = "tbarFocalY"
        Me.tbarFocalY.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbarFocalY.Size = New System.Drawing.Size(45, 200)
        Me.tbarFocalY.TabIndex = 3
        Me.tbarFocalY.TickFrequency = 50
        Me.tbarFocalY.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        Me.tbarFocalY.Value = 500
        '
        'panShapeHolder
        '
        Me.panShapeHolder.Controls.Add(Me.TheShape)
        Me.panShapeHolder.Location = New System.Drawing.Point(12, 12)
        Me.panShapeHolder.Name = "panShapeHolder"
        Me.panShapeHolder.Size = New System.Drawing.Size(200, 200)
        Me.panShapeHolder.TabIndex = 4
        '
        'TheShape
        '
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.White, System.Drawing.Color.White}
        CBlendItems1.iPoint = New Single() {0.0!, 1.0!}
        Me.TheShape.BarColorBlend = CBlendItems1
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
        Me.TheShape.CylonMove = 5.0!
        Me.TheShape.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TheShape.BorderWidth = CType(1, Short)
        Me.TheShape.Corners.All = CType(0, Short)
        Me.TheShape.Corners.LowerLeft = CType(0, Short)
        Me.TheShape.Corners.LowerRight = CType(0, Short)
        Me.TheShape.Corners.UpperLeft = CType(0, Short)
        Me.TheShape.Corners.UpperRight = CType(0, Short)
        Me.TheShape.CornersApply = ProgBar.ProgBarPlus.eCornersApply.Both
        Me.TheShape.FillDirection = ProgBar.ProgBarPlus.eFillDirection.Up_Right
        CFocalPoints1.CenterPoint = CType(resources.GetObject("CFocalPoints1.CenterPoint"), System.Drawing.PointF)
        CFocalPoints1.FocusScales = CType(resources.GetObject("CFocalPoints1.FocusScales"), System.Drawing.PointF)
        Me.TheShape.FocalPoints = CFocalPoints1
        Me.TheShape.CylonInterval = CType(1, Short)
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
        'lblFy
        '
        Me.lblFy.Location = New System.Drawing.Point(220, 208)
        Me.lblFy.Name = "lblFy"
        Me.lblFy.Size = New System.Drawing.Size(37, 17)
        Me.lblFy.TabIndex = 5
        Me.lblFy.Text = "0.5"
        Me.lblFy.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblFx
        '
        Me.lblFx.Location = New System.Drawing.Point(206, 230)
        Me.lblFx.Name = "lblFx"
        Me.lblFx.Size = New System.Drawing.Size(37, 17)
        Me.lblFx.TabIndex = 5
        Me.lblFx.Text = "0.5"
        Me.lblFx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCx
        '
        Me.lblCx.AutoSize = True
        Me.lblCx.Location = New System.Drawing.Point(14, 272)
        Me.lblCx.Name = "lblCx"
        Me.lblCx.Size = New System.Drawing.Size(48, 13)
        Me.lblCx.TabIndex = 6
        Me.lblCx.Text = "Center X"
        '
        'lblCy
        '
        Me.lblCy.AutoSize = True
        Me.lblCy.Location = New System.Drawing.Point(132, 272)
        Me.lblCy.Name = "lblCy"
        Me.lblCy.Size = New System.Drawing.Size(48, 13)
        Me.lblCy.TabIndex = 7
        Me.lblCy.Text = "Center Y"
        '
        'butCancel
        '
        Me.butCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(179, 301)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(67, 23)
        Me.butCancel.TabIndex = 0
        Me.butCancel.Text = "Cancel"
        '
        'dlgFocalPoints
        '
        Me.AcceptButton = Me.butApply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(258, 336)
        Me.Controls.Add(Me.butCancel)
        Me.Controls.Add(Me.butApply)
        Me.Controls.Add(Me.lblCy)
        Me.Controls.Add(Me.lblCx)
        Me.Controls.Add(Me.lblFx)
        Me.Controls.Add(Me.lblFy)
        Me.Controls.Add(Me.panShapeHolder)
        Me.Controls.Add(Me.tbarFocalY)
        Me.Controls.Add(Me.tbarFocalX)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgFocalPoints"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Adjust CenterPoint & Focus Scales"
        CType(Me.tbarFocalX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarFocalY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panShapeHolder.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents butApply As System.Windows.Forms.Button
    Friend WithEvents tbarFocalX As System.Windows.Forms.TrackBar
    Friend WithEvents tbarFocalY As System.Windows.Forms.TrackBar
    Friend WithEvents panShapeHolder As System.Windows.Forms.Panel
    Friend WithEvents lblFy As System.Windows.Forms.Label
    Friend WithEvents lblFx As System.Windows.Forms.Label
    Friend WithEvents lblCx As System.Windows.Forms.Label
    Friend WithEvents lblCy As System.Windows.Forms.Label
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents TheShape As ProgBar.ProgBarPlus

End Class
