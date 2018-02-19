Public Class Form1
  Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'Form overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
  Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
  Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
  Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
  Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
  Friend WithEvents ScanPageCS As System.Windows.Forms.MenuItem
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ScanPageVB As System.Windows.Forms.MenuItem
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
    Me.MenuItem1 = New System.Windows.Forms.MenuItem
    Me.MenuItem2 = New System.Windows.Forms.MenuItem
    Me.MenuItem3 = New System.Windows.Forms.MenuItem
    Me.MenuItem4 = New System.Windows.Forms.MenuItem
    Me.ScanPageCS = New System.Windows.Forms.MenuItem
    Me.ScanPageVB = New System.Windows.Forms.MenuItem
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'MainMenu1
    '
    Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem4})
    '
    'MenuItem1
    '
    Me.MenuItem1.Index = 0
    Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.MenuItem3})
    Me.MenuItem1.Text = "&File"
    '
    'MenuItem2
    '
    Me.MenuItem2.Index = 0
    Me.MenuItem2.Text = "&Load"
    '
    'MenuItem3
    '
    Me.MenuItem3.Index = 1
    Me.MenuItem3.Text = "E&xit"
    '
    'MenuItem4
    '
    Me.MenuItem4.Index = 1
    Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ScanPageCS, Me.ScanPageVB})
    Me.MenuItem4.Text = "Barcode &Imaging"
    '
    'ScanPageCS
    '
    Me.ScanPageCS.Index = 0
    Me.ScanPageCS.Text = "Scan Page (C# - BarcodeImaging class)"
    '
    'ScanPageVB
    '
    Me.ScanPageVB.Index = 1
    Me.ScanPageVB.Text = "Scan Page (VB.Net - BarcodeScanner class)"
    '
    'PictureBox1
    '
    Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(412, 269)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.PictureBox1.TabIndex = 0
    Me.PictureBox1.TabStop = False
    '
    'Form1
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(412, 269)
    Me.Controls.Add(Me.PictureBox1)
    Me.Menu = Me.MainMenu1
    Me.Name = "Form1"
    Me.Text = "Barcode TestApp"
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
    Me.Dispose()
  End Sub

  Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
    Dim openDlg As New OpenFileDialog

    ' display an open file dialog
    openDlg.ShowDialog()

    Try
      If openDlg.FileName <> vbNullString Then
        ' try to open the file
        Me.PictureBox1.Image = Bitmap.FromFile(openDlg.FileName)
      End If
    Catch ex As Exception
      MsgBox("Failed to open the file. The specified file may not be a vaild image.", MsgBoxStyle.Exclamation, "File Open Error")
    End Try
  End Sub

  ''' <summary>
  ''' The ScanPageCS menu option demonstrates the C# version of the barcode
  ''' detection code (BarcodeImaging class).
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ScanPageCS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanPageCS.Click
    If Not Me.PictureBox1.Image Is Nothing Then
      Dim barcodes As New System.Collections.ArrayList
      Dim dtStart As DateTime = DateTime.Now
      Dim iScans As Integer = 100

      ' For maximum performance in Code39 scanning you could use this configuration,
      ' would make scanning of just Code39 about twice as fast as the default:
      'BarcodeImaging.UseBarcodeZones = False
      'BarcodeImaging.FullScanBarcodeTypes = BarcodeImaging.BarcodeType.Code39

      BarcodeImaging.FullScanPage(barcodes, Me.PictureBox1.Image, iScans)

      ' Show the results in a message box
      ShowResults(dtStart, iScans, barcodes)
    End If
  End Sub

  ''' <summary>
  ''' The ScanPageVB menu option demonstrates the VB.Net version of the barcode 
  ''' detection code (BarcodeScanner class).
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ScanPageVB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanPageVB.Click
    If Not Me.PictureBox1.Image Is Nothing Then
      Dim barcodes As New System.Collections.ArrayList
      Dim dtStart As DateTime = DateTime.Now
      Dim iScans As Integer = 100

      BarcodeScanner.FullScanPage(barcodes, Me.PictureBox1.Image, iScans, BarcodeScanner.BarcodeType.All)

      ' Show the results in a message box
      ShowResults(dtStart, iScans, barcodes)
    End If
  End Sub

  ''' <summary>
  ''' Show the reults of the barcode scan in a message box.
  ''' </summary>
  ''' <param name="dtStart">Start time of scan</param>
  ''' <param name="iScans">Number of scans done</param>
  ''' <param name="barcodes">List of barcodes found</param>
  Private Sub ShowResults(ByVal dtStart As DateTime, ByVal iScans As Integer, ByRef barcodes As System.Collections.ArrayList)
    Dim Message As String = "Completed " + iScans.ToString() + " scans in " _
                          + DateTime.Now.Subtract(dtStart).TotalSeconds.ToString("#0.00") + " seconds." + vbNewLine
    If barcodes.Count > 0 Then
      Message += "Found barcodes:" & vbNewLine
      Dim bc As Object
      For Each bc In barcodes
        Message += bc & vbNewLine
      Next
    Else
      Message += "Failed to find a barcode."
    End If
    MsgBox(Message)
    Me.Refresh()
  End Sub
End Class
