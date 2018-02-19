<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MyUserControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
    Me.SharedImageLists11 = New WindowsApplication2.SharedImageLists1(Me.components)
    Me.LargeImageList = Me.SharedImageLists11.NewImageList(Me.components, CType(Me.SharedImageLists11.GetSharedImageLists, WindowsApplication2.SharedImageLists1).LargeImageList)
    Me.SuspendLayout()
    '
    'MyUserControl
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Name = "MyUserControl"
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents SharedImageLists11 As WindowsApplication2.SharedImageLists1
  Protected Friend WithEvents LargeImageList As System.Windows.Forms.ImageList

End Class
