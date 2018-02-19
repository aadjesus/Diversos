Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Windows.Forms.Design
Imports System.Reflection

Friend Class DesignerView

  Public Sub New()
    MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint, True)
    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub
  Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
    MyBase.OnPaintBackground(e)
    Dim rc As Rectangle = Me.ClientRectangle
    Using backBrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
      New Point(0, 0), New Point(rc.Width, 0), SystemColors.ControlLightLight, SystemColors.Control)
      e.Graphics.FillRectangle(backBrush, rc)
    End Using
  End Sub

End Class

'<ToolboxItemFilter("Microsoft.Samples.ShapeLibrary.Shape", ToolboxItemFilterType.Require)> _
Friend Class ShapeContainerRootDesigner
  Inherits ComponentDesigner
  Implements IRootDesigner, Drawing.Design.IToolboxUser

  Private WithEvents m_view As DesignerView
  Private WithEvents m_view2 As DesignSurface

  Public Function GetView(ByVal technology As System.ComponentModel.Design.ViewTechnology) As Object Implements System.ComponentModel.Design.IRootDesigner.GetView
    If technology <> ViewTechnology.Default Then
      Throw New ArgumentException("technology")
    End If
    ' Note that we store off a single instance of the
    ' view.  Don't always create a new object here, because
    ' it is possible that someone will call this multiple times.
    '
    If m_view2 Is Nothing Then
      m_view2 = New DesignSurface(CType(Me.GetService(GetType(IServiceProvider)), IServiceProvider))
      m_view2.BeginLoad(GetType(DesignerView))
    End If
    Return m_view2.View
  End Function

  Public ReadOnly Property SupportedTechnologies() As System.ComponentModel.Design.ViewTechnology() Implements System.ComponentModel.Design.IRootDesigner.SupportedTechnologies
    Get
      Return New ViewTechnology() {ViewTechnology.Default}
    End Get
  End Property

  Public Function GetToolSupported(ByVal tool As System.Drawing.Design.ToolboxItem) As Boolean Implements System.Drawing.Design.IToolboxUser.GetToolSupported
    Return True
  End Function

  Public Sub ToolPicked(ByVal tool As System.Drawing.Design.ToolboxItem) Implements System.Drawing.Design.IToolboxUser.ToolPicked
    If tool.TypeName = "System.Windows.Forms.ImageList" Then
    End If
  End Sub

End Class


