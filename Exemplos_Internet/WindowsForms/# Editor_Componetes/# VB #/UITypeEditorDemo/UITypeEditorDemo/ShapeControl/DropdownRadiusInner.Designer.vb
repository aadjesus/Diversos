<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DropdownRadiusInner
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.tbarRadiusInner = New System.Windows.Forms.TrackBar
        Me.lblValue = New System.Windows.Forms.Label
        Me.butApply = New System.Windows.Forms.Button
        Me.llThin = New System.Windows.Forms.LinkLabel
        Me.llNormal = New System.Windows.Forms.LinkLabel
        Me.llPentagon = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.panShapeHolder = New System.Windows.Forms.Panel
        Me.TheShape = New UITypeEditorDemo.Shape
        CType(Me.tbarRadiusInner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panShapeHolder.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbarRadiusInner
        '
        Me.tbarRadiusInner.Location = New System.Drawing.Point(81, 28)
        Me.tbarRadiusInner.Maximum = 50
        Me.tbarRadiusInner.Minimum = -100
        Me.tbarRadiusInner.Name = "tbarRadiusInner"
        Me.tbarRadiusInner.Size = New System.Drawing.Size(240, 45)
        Me.tbarRadiusInner.TabIndex = 3
        Me.tbarRadiusInner.TickFrequency = 10
        '
        'lblValue
        '
        Me.lblValue.AutoSize = True
        Me.lblValue.Location = New System.Drawing.Point(81, 60)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(39, 13)
        Me.lblValue.TabIndex = 4
        Me.lblValue.Text = "Label1"
        '
        'butApply
        '
        Me.butApply.Location = New System.Drawing.Point(280, 0)
        Me.butApply.Name = "butApply"
        Me.butApply.Size = New System.Drawing.Size(46, 23)
        Me.butApply.TabIndex = 5
        Me.butApply.Text = "Apply"
        Me.butApply.UseVisualStyleBackColor = True
        '
        'llThin
        '
        Me.llThin.AutoSize = True
        Me.llThin.Location = New System.Drawing.Point(137, 62)
        Me.llThin.Name = "llThin"
        Me.llThin.Size = New System.Drawing.Size(28, 13)
        Me.llThin.TabIndex = 7
        Me.llThin.TabStop = True
        Me.llThin.Text = "Thin"
        '
        'llNormal
        '
        Me.llNormal.AutoSize = True
        Me.llNormal.Location = New System.Drawing.Point(200, 62)
        Me.llNormal.Name = "llNormal"
        Me.llNormal.Size = New System.Drawing.Size(40, 13)
        Me.llNormal.TabIndex = 8
        Me.llNormal.TabStop = True
        Me.llNormal.Text = "Normal"
        '
        'llPentagon
        '
        Me.llPentagon.AutoSize = True
        Me.llPentagon.Location = New System.Drawing.Point(268, 62)
        Me.llPentagon.Name = "llPentagon"
        Me.llPentagon.Size = New System.Drawing.Size(53, 13)
        Me.llPentagon.TabIndex = 9
        Me.llPentagon.TabStop = True
        Me.llPentagon.Text = "Pentagon"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(90, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(171, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "This applies to the Star shape Only"
        '
        'panShapeHolder
        '
        Me.panShapeHolder.Controls.Add(Me.TheShape)
        Me.panShapeHolder.Location = New System.Drawing.Point(2, -1)
        Me.panShapeHolder.Name = "panShapeHolder"
        Me.panShapeHolder.Size = New System.Drawing.Size(80, 80)
        Me.panShapeHolder.TabIndex = 10
        '
        'TheShape
        '
        Me.TheShape.BorderColor = System.Drawing.Color.Black
        Me.TheShape.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.TheShape.BorderWidth = 2.0!
        Me.TheShape.ColorFillBlend = Nothing
        Me.TheShape.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.TheShape.Corners.All = CType(0, Short)
        Me.TheShape.Corners.LowerLeft = CType(0, Short)
        Me.TheShape.Corners.LowerRight = CType(0, Short)
        Me.TheShape.Corners.UpperLeft = CType(0, Short)
        Me.TheShape.Corners.UpperRight = CType(0, Short)
        Me.TheShape.FillType = UITypeEditorDemo.Shape.eFillType.Solid
        Me.TheShape.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.TheShape.FocalPoints = Nothing
        Me.TheShape.Location = New System.Drawing.Point(0, 0)
        Me.TheShape.Name = "TheShape"
        Me.TheShape.RadiusInner = 0.0!
        Me.TheShape.RegionClip = False
        Me.TheShape.Shape = UITypeEditorDemo.Shape.eShape.Rectangle
        Me.TheShape.Size = New System.Drawing.Size(80, 80)
        Me.TheShape.TabIndex = 1
        '
        'DropdownRadiusInner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.panShapeHolder)
        Me.Controls.Add(Me.llPentagon)
        Me.Controls.Add(Me.llNormal)
        Me.Controls.Add(Me.llThin)
        Me.Controls.Add(Me.butApply)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblValue)
        Me.Controls.Add(Me.tbarRadiusInner)
        Me.Name = "DropdownRadiusInner"
        Me.Size = New System.Drawing.Size(329, 79)
        CType(Me.tbarRadiusInner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panShapeHolder.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbarRadiusInner As System.Windows.Forms.TrackBar
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents butApply As System.Windows.Forms.Button
    Friend WithEvents llThin As System.Windows.Forms.LinkLabel
    Friend WithEvents llNormal As System.Windows.Forms.LinkLabel
    Friend WithEvents llPentagon As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panShapeHolder As System.Windows.Forms.Panel
    Friend WithEvents TheShape As UITypeEditorDemo.Shape

End Class
