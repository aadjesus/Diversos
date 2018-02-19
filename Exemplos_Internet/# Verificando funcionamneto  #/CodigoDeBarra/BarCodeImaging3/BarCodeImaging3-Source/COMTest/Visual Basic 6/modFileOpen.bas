Attribute VB_Name = "modFileOpen"
Option Explicit

Private Declare Function GetOpenFileName Lib "comdlg32.dll" Alias _
  "GetOpenFileNameA" (pOpenfilename As OPENFILENAME) As Long

Private Type OPENFILENAME
  lStructSize As Long
  hwndOwner As Long
  hInstance As Long
  lpstrFilter As String
  lpstrCustomFilter As String
  nMaxCustFilter As Long
  nFilterIndex As Long
  lpstrFile As String
  nMaxFile As Long
  lpstrFileTitle As String
  nMaxFileTitle As Long
  lpstrInitialDir As String
  lpstrTitle As String
  flags As Long
  nFileOffset As Integer
  nFileExtension As Integer
  lpstrDefExt As String
  lCustData As Long
  lpfnHook As Long
  lpTemplateName As String
End Type

Public Function GetFilename() As String
  Dim OpenFile As OPENFILENAME
  Dim lReturn As Long
  Dim sFilter As String
  OpenFile.lStructSize = Len(OpenFile)
  OpenFile.hwndOwner = Form1.hWnd
  OpenFile.hInstance = App.hInstance
  OpenFile.lpstrFilter = "Supported Image file types" & vbNullChar & "*.BMP;*.JPG;*.JPEG" & vbNullChar & vbNullChar
  OpenFile.nFilterIndex = 1
  OpenFile.lpstrFile = String(257, 0)
  OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
  OpenFile.lpstrFileTitle = OpenFile.lpstrFile
  OpenFile.nMaxFileTitle = OpenFile.nMaxFile
  OpenFile.lpstrTitle = "Select an image"
  OpenFile.flags = 0
  lReturn = GetOpenFileName(OpenFile)
  If lReturn <> 0 Then
    GetFilename = Trim$(OpenFile.lpstrFile)
  End If
End Function


