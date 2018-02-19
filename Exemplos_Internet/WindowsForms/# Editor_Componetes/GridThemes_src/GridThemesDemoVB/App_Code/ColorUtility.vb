Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Drawing

Public Class ColorUtility

    Public Shared Function BlendColors(ByVal color1 As Color, ByVal color2 As Color) As Color
        ' return the average between both colors



        Dim r As Integer = (CType(color1.R, Integer) + CType(color2.R, Integer)) / 2
        Dim b As Integer = (CType(color1.B, Integer) + CType(color2.B, Integer)) / 2
        Dim g As Integer = (CType(color1.G, Integer) + CType(color2.G, Integer)) / 2

        Return System.Drawing.Color.FromArgb(r, g, b)
        
    End Function


    Public Shared Function BlendColors(ByVal color1 As Color, ByVal sColor2 As String) As Color
        ' return the average between both colors; the second is supplied as a string
        Dim color2 As Color = System.Drawing.ColorTranslator.FromHtml(sColor2)
        Return ColorUtility.BlendColors(color1, color2)

    End Function


End Class
