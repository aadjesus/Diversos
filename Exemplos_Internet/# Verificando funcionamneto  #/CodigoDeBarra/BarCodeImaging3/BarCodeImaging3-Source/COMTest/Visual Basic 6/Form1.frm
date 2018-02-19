VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   6255
   ClientLeft      =   165
   ClientTop       =   735
   ClientWidth     =   10485
   LinkTopic       =   "Form1"
   ScaleHeight     =   6255
   ScaleWidth      =   10485
   StartUpPosition =   3  'Windows Default
   Begin VB.Image Image1 
      Height          =   6015
      Left            =   120
      Stretch         =   -1  'True
      Top             =   120
      Width           =   10215
   End
   Begin VB.Menu mnuFile 
      Caption         =   "File"
      Begin VB.Menu mnuLoad 
         Caption         =   "Load"
      End
      Begin VB.Menu mnuExit 
         Caption         =   "Exit"
      End
   End
   Begin VB.Menu mnuBarcodeImage 
      Caption         =   "Barcode Image"
      Enabled         =   0   'False
      Begin VB.Menu mnuScanPageVertical 
         Caption         =   "Scan Page Vertical"
      End
      Begin VB.Menu mnuScanPageHorizontal 
         Caption         =   "Scan Page Horizontal"
      End
      Begin VB.Menu mnuFullPageScan 
         Caption         =   "Full Scan Page"
      End
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public sFileName As String
' Used to specify what barcode type(s) to detect.
Public Enum BarcodeType
  None = 0    ' Not specified
  Code39 = 1  ' Code39
  EAN = 2     ' EAN/UPC
  Code128 = 4 ' Code128
  All = 7     ' All: Code39 Or EAN Or Code128
End Enum

' Used to specify whether to scan a page in vertical direction,
' horizontally, or both.
Public Enum ScanDirection
  Vertical = 1   ' Scan top-to-bottom
  Horizontal = 2 ' Scan left-to-right
End Enum

Private Sub Form_Resize()
  On Error Resume Next
  Image1.Width = Me.ScaleWidth - 2 * Image1.Left
  Image1.Height = Me.ScaleHeight - 2 * Image1.Top
End Sub

Private Sub mnuExit_Click()
  Unload Me
End Sub

Private Sub mnuFullPageScan_Click()
  Dim sLetti As String
  Dim oBarcodeDetection As Object
  
  Set oBarcodeDetection = CreateObject("BarcodeDetection")
  sLetti = oBarcodeDetection.FullScanPage(Image1.Picture, 100, BarcodeType.All, True)
  If sLetti <> "" Then
    MsgBox Replace(sLetti, "|", vbCrLf)
  Else
    MsgBox "No barcodes found"
  End If
  Set oBarcodeDetection = Nothing
End Sub

Private Sub mnuLoad_Click()
  sFileName = GetFilename()
  If LenB(sFileName) > 0 Then
    Image1.Picture = LoadPicture(sFileName)
    mnuBarcodeImage.Enabled = True
  End If
End Sub

Private Sub mnuScanPageVertical_Click()
  Dim sLetti As String
  Dim oBarcodeDetection As Object
  
  Set oBarcodeDetection = CreateObject("BarcodeDetection")
  sLetti = oBarcodeDetection.ScanPage(Image1.Picture, 100, ScanDirection.Vertical, BarcodeType.All, True)
  If sLetti <> "" Then
    MsgBox Replace(sLetti, "|", vbCrLf)
  Else
    MsgBox "No barcodes found"
  End If
  Set oBarcodeDetection = Nothing
End Sub

Private Sub mnuScanPageHorizontal_Click()
  Dim sLetti As String
  Dim oBarcodeDetection As Object
  
  Set oBarcodeDetection = CreateObject("BarcodeDetection")
  sLetti = oBarcodeDetection.ScanPage(Image1.Picture, 100, ScanDirection.Horizontal, BarcodeType.All, True)
  If sLetti <> "" Then
    MsgBox Replace(sLetti, "|", vbCrLf)
  Else
    MsgBox "No barcodes found"
  End If
  Set oBarcodeDetection = Nothing
End Sub
