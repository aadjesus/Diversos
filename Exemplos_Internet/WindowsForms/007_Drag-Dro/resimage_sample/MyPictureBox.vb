' ==============================================================================
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'
' ©2004 Palo Mraz, LaMarvin. All Rights Reserved.
' ==============================================================================

Namespace LaMarvin.Windows.Forms

  '<summary>Our simple PictureBox descendant, which overrides the BackgroundImage
  ' property and gives it the ResourceImageEditor as an type editor.
  '</summary>
  Public Class MyPictureBox
    Inherits PictureBox

    <System.ComponentModel.Editor( _
      GetType(Design.ResourceImageEditor), _
      GetType(System.Drawing.Design.UITypeEditor))> _
    Public Shadows Property Image() As System.Drawing.Image
      Get
        Return MyBase.Image
      End Get
      Set(ByVal Value As System.Drawing.Image)
        MyBase.Image = Value
      End Set
    End Property

  End Class
End Namespace
