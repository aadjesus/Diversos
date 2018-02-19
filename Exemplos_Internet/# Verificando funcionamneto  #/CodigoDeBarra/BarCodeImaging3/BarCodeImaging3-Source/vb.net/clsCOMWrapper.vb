Imports Microsoft.VisualBasic.Compatibility
Imports System.Windows.Forms
Imports System.Drawing

''' <summary>
''' COM wrapper for BarcodeScanner class.
''' See http://www.codeproject.com/KB/graphics/BarcodeImaging3.aspx
''' 
''' Licensed under The Code Project Open License (CPOL). 
''' See http://www.codeproject.com/info/cpol10.aspx
''' </summary>
<ComVisible(True)> _
Public Class BarcodeDetection

#Region "Constructor"
  ''' <summary>
  ''' Default constructor is required for COM.
  ''' </summary>
  Public Sub BarcodeDetection()
  End Sub
#End Region

#Region "Public methods"
  ''' <summary>
  ''' FullScanPage does a full scan of the active frame in the passed bitmap. This function
  ''' will scan both vertically and horizontally. 
  ''' </summary>
  ''' <remarks>
  ''' By default FullScanPage will attempt to detect barcodes of all supported types. Assign 
  ''' a subset to FullScanBarcodeTypes if your application does not need this.
  ''' 
  ''' Use ScanPage instead of FullScanPage if you want to scan in one direction only, 
  ''' or only for specific barcode types.
  ''' </remarks>
  ''' <param name="bmp">Input bitmap. Pass the bitmap through COM as an IPictureDisp,
  ''' e.g., the Picture property of a Picture or Image control in VB6.</param>
  ''' <param name="numscans">Number of passes that must be made over the page. 
  ''' 50 - 100 usually gives a good result.</param>
  ''' <param name="types">Barcode types. Pass BarcodeType.All, or you can specify a list of types,
  ''' e.g., BarcodeType.Code39 | BarcodeType.EAN</param>
  ''' <param name="UseZones">Set UseBarcodeZones to false if you do not need this feature.
  ''' Barcode regions improve detection of multiple barcodes on one scan line,
  ''' but have a significant performance impact.</param>
  ''' <returns>Pipe-separated list of detected barcodes, empty string if none</returns>
  <ComVisible(True)> _
  Public Function FullScanPage(ByVal bmp As Object, ByVal numscans As Integer, ByVal types As BarcodeScanner.BarcodeType, ByVal UseZones As Boolean) As String
    Dim barcodes As New System.Collections.ArrayList

    BarcodeScanner.UseBarcodeZones = UseZones
    BarcodeScanner.FullScanPage(barcodes, ConvertToImage(bmp), numscans, types)
    Return String.Join("|", DirectCast(barcodes.ToArray(GetType(String)), String()))
  End Function

  ''' <summary>
  ''' Scans the active frame in the passed bitmap for barcodes.
  ''' </summary>
  ''' <param name="bmp">Input bitmap</param>
  ''' <param name="numscans">Number of passes that must be made over the page. 
  ''' 50 - 100 usually gives a good result.</param>
  ''' <param name="direction">Scan direction</param>
  ''' <param name="types">Barcode types. Pass BarcodeType.All, or you can specify a list of types,
  ''' e.g., BarcodeType.Code39 | BarcodeType.EAN</param>
  ''' <param name="UseZones">Set UseZones to false if you do not need this feature.
  ''' Barcode regions improve detection of multiple barcodes on one scan line,
  ''' but have a significant performance impact.</param>
  ''' <returns>Pipe-separated list of detected barcodes, empty string if none</returns>
  <ComVisible(True)> _
  Public Function ScanPage(ByVal bmp As Object, ByVal numscans As Integer, ByVal direction As BarcodeScanner.ScanDirection, ByVal types As BarcodeScanner.BarcodeType, ByVal UseZones As Boolean) As String
    Dim barcodes As New System.Collections.ArrayList

    BarcodeScanner.UseBarcodeZones = UseZones
    BarcodeScanner.ScanPage(barcodes, ConvertToImage(bmp), numscans, direction, types)
    Return String.Join("|", DirectCast(barcodes.ToArray(GetType(String)), String()))
  End Function

  ''' <summary>
  ''' Helper function to get image from clipboard. 
  ''' </summary>
  ''' <returns>Image from clipboard, or Nothing if the clipboard does not contain an image.</returns>
  ''' <remarks>Used in the VBA code of the MS Word sample COMTest.doc</remarks>
  <ComVisible(True)> _
  Public Function GetImageFromClipboard() As Object
    If Clipboard.ContainsImage() Then
      Return Clipboard.GetImage()
    End If
    Return Nothing
  End Function

  ''' <summary>
  ''' Attempts to convert the passed object to a .Net image object.
  ''' </summary>
  ''' <param name="oBmp">Input object</param>
  ''' <returns>Converted object</returns>
  Private Function ConvertToImage(ByVal oBmp As Object) As Object
    Dim oImg As Object = Nothing
    Try
      oImg = Microsoft.VisualBasic.Compatibility.VB6.IPictureDispToImage(oBmp)
    Catch
      Try
        oImg = Microsoft.VisualBasic.Compatibility.VB6.IPictureToImage(oBmp)
      Catch
        oImg = oBmp
      End Try
    End Try
    Return oImg
  End Function
#End Region

End Class
