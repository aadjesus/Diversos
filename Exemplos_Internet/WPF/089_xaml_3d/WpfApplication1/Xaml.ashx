<%@ WebHandler Language="VB" Class="Xaml" %>

Imports System
Imports System.Web
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Markup


Public Class Xaml : Implements IHttpHandler
    
    Private retImageStream As System.IO.Stream

    Public WithEvents MyImage As System.Windows.Controls.Page
    Private Delegate Sub PageDelegate(ByVal o As Page)
    
#Region " Properties "
    Private _context As HttpContext
    Private _Response As HttpResponse
    Private _width As Integer
    Private _height As Integer
    Private _xaml As String
    Private _encoder As String
    
    Public Property CurrentContext() As HttpContext
        Get
            Return _context
        End Get
        Set(ByVal value As HttpContext)
            _context = value
        End Set
    End Property
    
    Public Property Response() As HttpResponse
        Get
            Return _Response
        End Get

        Set(ByVal value As HttpResponse)
            _Response = value
        End Set
    End Property

    Public Property Width() As Integer
        Get
            If _width = 0 Then
                _width = 200
            End If
            Return _width
        End Get
        Set(ByVal value As Integer)
            _width = value
        End Set
    End Property

    Public Property Height() As Integer
        Get
            If _height = 0 Then
                _height = 200
            End If
            Return _height
        End Get
        Set(ByVal value As Integer)
            _height = value
        End Set
    End Property
    
    Public Property XamlDoc() As String
        Get
            Return _xaml
        End Get
        Set(ByVal value As String)
            _xaml = value
        End Set
    End Property
    
    Public Property Encoding() As String
        Get
            If _encoder = "" Then
                _encoder = "Jpeg"
            End If
            Return _encoder
        End Get
        Set(ByVal value As String)
            _encoder = value
        End Set
    End Property
    
#End Region

    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If IsNothing(context) Then
            Throw New ArgumentNullException("There is no httpContext associated with the Request.")
        End If

        If IsNothing(context.Response) Then
            Throw New ArgumentNullException("There is no httpContext.Response associated with the Request.")
        End If

        If IsNothing(context.Request("w")) Then
            Throw New ArgumentNullException("There is no width specified.")
        End If

        If IsNothing(context.Request("h")) Then
            Throw New ArgumentNullException("There is no height specified.")
        End If

        If IsNothing(context.Request("xaml")) Then
            Throw New ArgumentNullException("There is no xaml specified.")
        End If
        
        If IsNothing(context.Request("enc")) Then
            Throw New ArgumentNullException("There is no encoder specified.")
        End If
        
        CurrentContext = context
        Response = context.Response
        Width = context.Request("w")
        Height = context.Request("h")
        XamlDoc = context.Request("xaml")
        Encoding = context.Request("enc")
        
        Select Case context.Request("enc").ToLower
            Case "jpg", "png", "gif", "bmp", "tif", "wmp"
                StartThread()
            Case Else
                Throw New ArgumentException("A valid encoder was not specified, bmp = Bitmap, gif = Gif, jpg = Jpeg, png = Png, tif = Tiff, wmp = Wmp")
        End Select


    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Protected Sub GimmeMore()
        Dim sr As New System.IO.StreamReader(CurrentContext.Request.PhysicalApplicationPath + XamlDoc + ".xaml")
        Dim xaml As String = sr.ReadToEnd()
        sr.Close()
        Dim ms As New System.IO.MemoryStream(xaml.Length)
        Dim sw As New System.IO.StreamWriter(ms)
        sw.Write(xaml)
        sw.Flush()
        ms.Seek(0, System.IO.SeekOrigin.Begin)
        Dim parserContext As New ParserContext()
        parserContext.BaseUri = New Uri(CurrentContext.Request.PhysicalApplicationPath)
        MyImage = CType(System.Windows.Markup.XamlReader.Load(ms, parserContext), System.Windows.Controls.Page)

        'Dim ib As ImageBrush = MyImage.FindName("img")
        'ib.ImageSource = New BitmapImage(New Uri(CurrentContext.Request.PhysicalApplicationPath + "pesce.jpg"))

        ms.Dispose()

        Dim slt As PageDelegate
        slt = New PageDelegate(AddressOf Capture)
        MyImage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, slt, MyImage)
    End Sub
    
    Public Sub Capture(ByVal MyImage As Page)
        Dim bounds As Rect = New Rect(0, 0, Width, Height)
        MyImage.Measure(New Size(Width, Height))
        MyImage.Arrange(bounds)

        Dim rtb As RenderTargetBitmap = New RenderTargetBitmap(CInt(bounds.Width * 96 / 96.0), CInt(bounds.Height * 96 / 96.0), 96, 96, PixelFormats.Pbgra32)
        Dim dv As DrawingVisual = New DrawingVisual()
        Using ctx As DrawingContext = dv.RenderOpen()
            Dim vb As New VisualBrush(MyImage)
            ctx.DrawRectangle(vb, Nothing, New Rect(New System.Windows.Point(), bounds.Size))
        End Using
        rtb.Render(dv)
        Dim encoder As System.Windows.Media.Imaging.BitmapEncoder
        Select Case Encoding.ToLower
            Case "jpg"
                encoder = New System.Windows.Media.Imaging.JpegBitmapEncoder()
            Case "png"
                encoder = New System.Windows.Media.Imaging.PngBitmapEncoder()
            Case "gif"
                encoder = New System.Windows.Media.Imaging.GifBitmapEncoder()
            Case "bmp"
                encoder = New System.Windows.Media.Imaging.BmpBitmapEncoder()
            Case "tif"
                encoder = New System.Windows.Media.Imaging.TiffBitmapEncoder()
            Case "wmp"
                encoder = New System.Windows.Media.Imaging.WmpBitmapEncoder()
        End Select
            
        encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(rtb))
        retImageStream = New System.IO.MemoryStream()
        encoder.Save(retImageStream)
        retImageStream.Flush()
        retImageStream.Seek(0, System.IO.SeekOrigin.Begin)
        MyImage = Nothing
    End Sub
    
    Private Sub StartThread()
        Dim thread As System.Threading.Thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf GimmeMore))
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.IsBackground = True
        thread.Start()
        thread.Join()

        If Not IsNothing(retImageStream) Then
            Select Case Encoding.ToLower
                Case "jpg"
                    CurrentContext.Response.ContentType = "image/jpeg"
                Case "png"
                    CurrentContext.Response.ContentType = "image/png"
                Case "gif"
                    CurrentContext.Response.ContentType = "image/gif"
                Case "bmp"
                    CurrentContext.Response.ContentType = "image/bmp"
                Case "tif"
                    CurrentContext.Response.ContentType = "image/tiff"
                Case "wmp"
                    CurrentContext.Response.ContentType = "video/x-ms-wmp"
            End Select
            Dim br As New System.IO.BinaryReader(retImageStream)
            CurrentContext.Response.BinaryWrite(br.ReadBytes(CInt(retImageStream.Length)))
            br.Close()
            retImageStream.Close()
            CurrentContext.Response.Flush()
            thread = Nothing
            br = Nothing
            retImageStream = Nothing
            CurrentContext.Response.End()
        Else
            Throw New ArgumentNullException("No image returned.")
        End If
    End Sub
End Class