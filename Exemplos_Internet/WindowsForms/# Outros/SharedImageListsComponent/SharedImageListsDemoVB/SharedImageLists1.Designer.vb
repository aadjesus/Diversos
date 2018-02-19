Partial Class SharedImageLists1
  Inherits ForwardSoftware.Windows.Forms.SharedImageLists

  '<System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New(ByVal Container As System.ComponentModel.IContainer)
    MyClass.New()

    'Required for Windows.Forms Class Composition Designer support
    Container.Add(Me)

  End Sub

  '<System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    'This call is required by the Component Designer.
    InitializeComponent()

  End Sub

    'Component overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SharedImageLists1))
    Me.LargeImageList = New System.Windows.Forms.ImageList(Me.components)
    Me.SmallImageList = New System.Windows.Forms.ImageList(Me.components)
    '
    'LargeImageList
    '
    Me.LargeImageList.ImageStream = CType(resources.GetObject("LargeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.LargeImageList.TransparentColor = System.Drawing.Color.Transparent
    Me.LargeImageList.Images.SetKeyName(0, "users.ico")
    Me.LargeImageList.Images.SetKeyName(1, "install.ico")
    '
    'SmallImageList
    '
    Me.SmallImageList.ImageStream = CType(resources.GetObject("SmallImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.SmallImageList.TransparentColor = System.Drawing.Color.Transparent
    Me.SmallImageList.Images.SetKeyName(0, "printer.ico")
    Me.SmallImageList.Images.SetKeyName(1, "camera.ico")

  End Sub


  <System.Diagnostics.DebuggerNonUserCode()> _
  Shared Sub New()
    SharedImageLists1 = New SharedImageLists1
  End Sub

  'This Method returns the SharedImageLists component that is used to share
  'the ImageLists.
  <System.Diagnostics.DebuggerNonUserCode()> _
 Public Overrides Function GetSharedImageLists() As ForwardSoftware.Windows.Forms.SharedImageLists
    Return SharedImageLists1
  End Function

  Private Shared SharedImageLists1 As SharedImageLists1
  Public WithEvents LargeImageList As System.Windows.Forms.ImageList
  Public WithEvents SmallImageList As System.Windows.Forms.ImageList

End Class
