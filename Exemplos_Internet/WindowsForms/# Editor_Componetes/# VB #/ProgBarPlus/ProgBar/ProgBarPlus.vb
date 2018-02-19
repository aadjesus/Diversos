'ProgBarPlus February 2008
'Scott Snyder
'Added UITypeEditors and updated properties November 2008
Imports System.Reflection
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Drawing.Drawing2D
Imports System.Drawing.Design
Imports System.Windows.Forms.Design

<ToolboxItem(True), ToolboxBitmap(GetType(ProgBarPlus), "ProgBar.ProgBarPlus.bmp")> _
<Designer(GetType(ProgBarControlDesigner))> _
Public Class ProgBarPlus
    Inherits System.Windows.Forms.UserControl

    Private CylonPosition As Single = 0
    Private CylonDirection As Single = 1
    Private CylonGPosition As Single = 0.5
    Private CylonGDelta As Single = 0.001

    Structure ProgBarPath
        Dim Rect As Rectangle
        Dim Path As GraphicsPath
    End Structure

#Region "Initialize"

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                ControlStyles.DoubleBuffer Or _
                ControlStyles.ResizeRedraw Or _
                ControlStyles.SupportsTransparentBackColor Or _
                ControlStyles.UserPaint, True)
    End Sub

    'Control overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Control Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  Do not modify it
    ' using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TimerCylon = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'ProgBarPlus
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Name = "ProgBarPlus"
        Me.Size = New System.Drawing.Size(200, 20)
        Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Property Enumeration"

    Enum eShape
        Rectangle
        Ellipse
        TriangleLeft
        TriangleRight
        TriangleUp
        TriangleDown
        Text
    End Enum

    Enum eBarStyle
        Solid
        GradientLinear
        GradientPath
        Texture
        Hatch
    End Enum

    Enum eOrientation
        Horizontal
        Vertical
    End Enum

    Enum eTextPlacement
        OverBar
        OnBar
    End Enum

    Enum eCornersApply
        Both
        Border
        Bar
    End Enum

    Enum eRotateText
        None
        Left
        Right
    End Enum

    Enum eFillDirection
        Up_Right
        Down_Left
    End Enum

    Enum eTextShow
        None
        TextOnly
        PercentOnly
        FormatStrPercent
        FormatStrText
        FormatStrTextPerc
    End Enum

    Enum eBarType
        Bar
        CylonBar
        CylonGlider
    End Enum

    Enum eBarLength
        Full
        Fixed
    End Enum

#End Region 'Property Enumeration

#Region "Properties"

#Region "Corners Expandable Property"

    'Corners Property is defined in the Corners Converter Class
    'to use the ExpandableObjectConverter to simulate the BarPadding Property

    Private _Corners As CornersProperty = New CornersProperty()

    <Category("Appearance ProgBar"), _
      Description("Get or Set the Corner Radii"), _
      RefreshProperties(RefreshProperties.All), _
      DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
      Public Property Corners() As CornersProperty
        Get
            Return _Corners
        End Get
        Set(ByVal Value As CornersProperty)
            _Corners = Value
            Me.Refresh()
        End Set

    End Property
#End Region 'Corners Expandable Property

#Region "Appearance Properties"

#Region "Color and Fill"
    Private _BarBackColor As Color = Color.White
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Color behind the Bar"), _
    DefaultValue(GetType(Color), "White")> _
    Public Property BarBackColor() As Color
        Get
            Return _BarBackColor
        End Get
        Set(ByVal Value As Color)
            _BarBackColor = Value
            Me.Invalidate()
        End Set
    End Property

    Private _BarColorSolid As Color = Color.Blue
    <Description("The Solid Color to fill the Bar"), _
    Category("Appearance ProgBar")> _
    Public Property BarColorSolid() As Color
        Get
            Return _BarColorSolid
        End Get
        Set(ByVal value As Color)
            _BarColorSolid = value
            Me.Invalidate()
        End Set
    End Property

    Private _BarColorSolidB As Color = Color.White
    <Description("The Secondary Color for Hatch Style"), _
    Category("Appearance ProgBar")> _
    Public Property BarColorSolidB() As Color
        Get
            Return _BarColorSolidB
        End Get
        Set(ByVal value As Color)
            _BarColorSolidB = value
            Me.Invalidate()
        End Set
    End Property

    Private _BarColorBlend As cBlendItems = New cBlendItems(New Color() {Color.Navy, Color.Blue}, New Single() {0, 1})
    <Description("The ColorBlend to fill the shape"), _
    Category("Appearance ProgBar"), _
    Editor(GetType(BlendTypeEditor), GetType(UITypeEditor))> _
    Public Property BarColorBlend() As cBlendItems
        Get
            Return _BarColorBlend
        End Get
        Set(ByVal value As cBlendItems)
            _BarColorBlend = value
            Me.Invalidate()
        End Set
    End Property

    Private _BarStyleFill As eBarStyle = eBarStyle.Solid
    <Description("The Fill Type to apply to the Shape")> _
    <Category("Appearance ProgBar")> _
    Public Property BarStyleFill() As eBarStyle
        Get
            Return _BarStyleFill
        End Get
        Set(ByVal value As eBarStyle)
            _BarStyleFill = value
            Me.Invalidate()
        End Set
    End Property

    Private _BarStyleLinear As LinearGradientMode = LinearGradientMode.Horizontal
    <Description("The Linear Blend type"), _
    Category("Appearance ProgBar")> _
    Public Property BarStyleLinear() As LinearGradientMode
        Get
            Return _BarStyleLinear
        End Get
        Set(ByVal value As LinearGradientMode)
            _BarStyleLinear = value
            Me.Invalidate()
        End Set
    End Property

    Private _FocalPoints As cFocalPoints = New cFocalPoints(0.5, 0.5, 0, 0)

    <Editor(GetType(FocalTypeEditor), GetType(UITypeEditor)), _
    Description("The CenterPoint and FocusScales for the ColorBlend"), _
    Category("Appearance ProgBar")> _
    Public Property FocalPoints() As cFocalPoints
        Get
            Return _FocalPoints
        End Get
        Set(ByVal value As cFocalPoints)
            _FocalPoints = value
            Me.Invalidate()
        End Set
    End Property

    Private _BarStyleHatch As HatchStyle = Drawing2D.HatchStyle.SmallCheckerBoard
    <Editor(GetType(HatchStyleEditor), GetType(UITypeEditor)), _
    Category("Appearance ProgBar"), _
    Description("Get or Set the Hatch Style when the BarStyleFill is set to Hatch"), _
    DefaultValue(HatchStyle.SmallCheckerBoard)> _
    Public Property BarStyleHatch() As HatchStyle
        Get
            Return _BarStyleHatch
        End Get
        Set(ByVal Value As HatchStyle)
            _BarStyleHatch = Value
            Me.Invalidate()
        End Set
    End Property

    Private _BarStyleTexture As Image = Nothing
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Wrap Style for the Texture Image")> _
    Public Property BarStyleTexture() As Image
        Get
            Return _BarStyleTexture
        End Get
        Set(ByVal Value As Image)
            _BarStyleTexture = Value
            Me.Invalidate()
        End Set
    End Property

    Private _BarStyleWrapMode As WrapMode = WrapMode.Clamp
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Wrapmode of the Image"), _
    DefaultValue(WrapMode.Clamp)> _
    Public Property BarStyleWrapMode() As WrapMode
        Get
            Return _BarStyleWrapMode
        End Get
        Set(ByVal Value As WrapMode)
            _BarStyleWrapMode = Value
            Me.Invalidate()
        End Set
    End Property

#End Region 'Color and Fill

#Region "Text"

    Private _Shadow As Boolean = False
    <Category("Appearance ProgBar"), _
    Description("Add Shadow to text"), _
    DefaultValue(False)> _
    Public Property TextShadow() As Boolean
        Get
            Return _Shadow
        End Get
        Set(ByVal Value As Boolean)
            _Shadow = Value
            Me.Invalidate()
        End Set
    End Property

    Private _ShadowColor As Color = Color.White
    <Category("Appearance ProgBar"), _
    Description("Define the Color of the Shadow text"), _
    DefaultValue(GetType(Color), "White")> _
    Public Property TextShadowColor() As Color
        Get
            Return _ShadowColor
        End Get
        Set(ByVal Value As Color)
            _ShadowColor = Value
            Me.Invalidate()
        End Set
    End Property


    Private _TextShow As eTextShow = eTextShow.None
    <Category("Appearance ProgBar"), _
    Description("Get or Set how the Text and/or Percent is displayed"), _
    DefaultValue(eTextShow.None)> _
    Public Property TextShow() As eTextShow
        Get
            Return _TextShow
        End Get
        Set(ByVal Value As eTextShow)
            _TextShow = Value
            Me.Invalidate()
        End Set
    End Property

    Private _TextFormat As String = "Process {0}% Done"
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Format String to display Percent and or Text variables." & vbCr & _
    "FormatStrPercent = Enter {0} where you want the Percent to appear." & vbCr & _
    "FormatStrText = Enter {0} where you want the TextValue to appear." & vbCr & _
    "FormatStrTextPercent = Enter {0} where you want the TextValue to appear and" & _
    " Enter {1} where you want the Percent to appear."), _
    DefaultValue("Process {0}% Done")> _
    Public Property TextFormat() As String
        Get
            Return _TextFormat
        End Get
        Set(ByVal Value As String)
            _TextFormat = Value
            Me.Invalidate()
        End Set
    End Property

    Private _TextPlacement As eTextPlacement = eTextPlacement.OverBar
    <Category("Appearance ProgBar"), _
    Description("Where to put text. Static Over Bar or moving on the bar"), _
    DefaultValue(eTextPlacement.OverBar)> _
    Public Property TextPlacement() As eTextPlacement
        Get
            Return _TextPlacement
        End Get
        Set(ByVal Value As eTextPlacement)
            _TextPlacement = Value
            Me.Invalidate()
        End Set
    End Property

    Private _TextAlignment As StringAlignment = StringAlignment.Center
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Horizontal Alignment of the text"), _
    DefaultValue(StringAlignment.Center)> _
    Public Property TextAlignment() As StringAlignment
        Get
            Return _TextAlignment
        End Get
        Set(ByVal Value As StringAlignment)
            _TextAlignment = Value
            Me.Invalidate()
        End Set
    End Property

    Private _TextAlignmentVert As StringAlignment = StringAlignment.Center
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Vertical Alignment of he text"), _
    DefaultValue(StringAlignment.Center)> _
    Public Property TextAlignmentVert() As StringAlignment
        Get
            Return _TextAlignmentVert
        End Get
        Set(ByVal Value As StringAlignment)
            _TextAlignmentVert = Value
            Me.Invalidate()
        End Set
    End Property

    Private _TextValue As String = ""
    <Category("Appearance ProgBar"), _
    Description("Get or Set the text to appear on the Bar"), _
    DefaultValue("")> _
    Public Property TextValue() As String
        Get
            Return _TextValue
        End Get
        Set(ByVal Value As String)
            _TextValue = Value
            Me.Invalidate()
        End Set
    End Property

    Private _RotateText As eRotateText = eRotateText.None
    <Category("Appearance ProgBar"), _
    Description("Get or Set the rotation of the text"), _
    DefaultValue(eRotateText.None)> _
    Public Property TextRotate() As eRotateText
        Get
            Return _RotateText
        End Get
        Set(ByVal Value As eRotateText)
            _RotateText = Value
            Me.Invalidate()
        End Set
    End Property

    Private _TextWrap As Boolean = True
    <Category("Appearance ProgBar"), _
    Description("Get or Set if the text will wrap"), _
    DefaultValue(True)> _
    Public Property TextWrap() As Boolean
        Get
            Return _TextWrap
        End Get
        Set(ByVal Value As Boolean)
            _TextWrap = Value
            Me.Invalidate()
        End Set
    End Property

#End Region 'Text

#Region "Border"

    Private _Border As Boolean = False
    <Category("Appearance ProgBar"), _
    Description("Add Border around control"), _
    DefaultValue(False)> _
    Public Property Border() As Boolean
        Get
            Return _Border
        End Get
        Set(ByVal Value As Boolean)
            _Border = Value
            ShowDesignBorder = Not Value
            Me.Invalidate()
        End Set
    End Property

    Private _BorderColor As Color = Color.Black
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Border Color"), _
    DefaultValue(GetType(Color), "Black")> _
    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal Value As Color)
            _BorderColor = Value
            Me.Invalidate()
        End Set
    End Property

    Private _BorderWidth As Short = 1
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Width of the Border around control"), _
    DefaultValue(1)> _
    Public Property BorderWidth() As Short
        Get
            Return _BorderWidth
        End Get
        Set(ByVal Value As Short)
            _BorderWidth = Value
            Me.Invalidate()
        End Set
    End Property

#End Region 'Border

#Region "Shape"

    Private _CornersApply As eCornersApply = eCornersApply.Both
    <Category("Appearance ProgBar"), _
    Description("Apply corners to Bar and/or Border"), _
    DefaultValue(eCornersApply.Both)> _
    Public Property CornersApply() As eCornersApply
        Get
            Return _CornersApply
        End Get
        Set(ByVal Value As eCornersApply)
            _CornersApply = Value
            Me.Invalidate()
        End Set
    End Property

    Private _Shape As eShape = eShape.Rectangle
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Shape of the Control"), _
    RefreshProperties(RefreshProperties.All), _
    DefaultValue(eShape.Rectangle)> _
    Public Property Shape() As eShape
        Get
            Return _Shape
        End Get
        Set(ByVal Val As eShape)
            _Shape = Val
            Me.Invalidate()
        End Set
    End Property

    Private _ShapeTextFont As Font = New Font("Arial Black", 30)
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Font of the Text Shape"), _
    RefreshProperties(RefreshProperties.All), _
    DefaultValue(GetType(Font), "Arial Black")> _
    Public Property ShapeTextFont() As Font
        Get
            Return _ShapeTextFont
        End Get
        Set(ByVal Val As Font)
            _ShapeTextFont = Val
            Me.Invalidate()
        End Set
    End Property

    Private _ShapeText As String = "ProgBarPlus"
    <Category("Appearance ProgBar"), _
    Description("Get or Set the Font of the Text Shape"), _
    RefreshProperties(RefreshProperties.All), _
    DefaultValue("ProgBarPlus")> _
    Public Property ShapeText() As String
        Get
            Return _ShapeText
        End Get
        Set(ByVal Val As String)
            _ShapeText = Val
            Me.Invalidate()
        End Set
    End Property

    Private _ShapeTextRotate As eRotateText = eRotateText.None
    <Category("Appearance ProgBar"), _
    Description("Get or Set the rotation of the text shape"), _
    DefaultValue(eRotateText.None)> _
    Public Property ShapeTextRotate() As eRotateText
        Get
            Return _ShapeTextRotate
        End Get
        Set(ByVal Value As eRotateText)
            _ShapeTextRotate = Value
            Me.Invalidate()
        End Set
    End Property

#End Region 'Shape

#End Region 'Appearance Properties

#Region "Behavior Properties"

    Private _ShowDesignBorder As Boolean = True
    <Category("Behavior"), _
    Description("Show Dashed Border around control at design time"), _
    DefaultValue(True)> _
    Public Property ShowDesignBorder() As Boolean
        Get
            Return _ShowDesignBorder
        End Get
        Set(ByVal Value As Boolean)
            _ShowDesignBorder = Value
            Me.Invalidate()
        End Set
    End Property

#End Region 'Behavior Properties

#Region "Bar Cylon"

    Private _CylonRun As Boolean = False
    <Category("Bar Cylon"), _
    Description("Start and Stop the Timer in Cylon Mode"), _
    DefaultValue(False)> _
    Public Property CylonRun() As Boolean
        Get
            Return _CylonRun
        End Get
        Set(ByVal Value As Boolean)
            If BarType <> eBarType.Bar Then
                _CylonRun = Value
                TimerCylon.Enabled = Value
            Else
                _CylonRun = False
                TimerCylon.Enabled = False
            End If
        End Set
    End Property

    Private _CylonInterval As Short = 1
    <Category("Bar Cylon"), _
    Description("Get or Set the Timer CylonInterval in Cylon Mode"), _
    DefaultValue(1)> _
    Public Property CylonInterval() As Short
        Get
            Return _CylonInterval
        End Get
        Set(ByVal Value As Short)
            _CylonInterval = Value
            TimerCylon.Interval = Value
            Me.Invalidate()
        End Set
    End Property

    Private _CylonMove As Single = 5
    <Category("Bar Cylon"), _
    Description("Get or Set the Speed the bar moves back and forth"), _
    RefreshProperties(RefreshProperties.All), _
    DefaultValue(5)> _
    Public Property CylonMove() As Single
        Get
            Return _CylonMove
        End Get
        Set(ByVal Val As Single)
            _CylonMove = Val
            Me.Invalidate()
        End Set
    End Property

#End Region 'Bar Cylon

#Region "Bar Properties"

    Private _BarType As eBarType = eBarType.Bar
    <Category("Bar"), _
    Description("Get or Set the Minimum Value"), _
    RefreshProperties(RefreshProperties.All), _
    DefaultValue(eBarType.Bar)> _
    Public Property BarType() As eBarType
        Get
            Return _BarType
        End Get
        Set(ByVal Val As eBarType)
            _BarType = Val
            If Val = eBarType.Bar Then CylonRun = False
            Me.Invalidate()
        End Set
    End Property

    Private _BarLength As eBarLength = eBarLength.Full
    <Category("Bar"), _
    Description("Get or Set if the Progress Bar Expands with the Value"), _
    DefaultValue(eBarLength.Full)> _
    Public Property BarLength() As eBarLength
        Get
            Return _BarLength
        End Get
        Set(ByVal Value As eBarLength)
            _BarLength = Value
            Me.Invalidate()
        End Set
    End Property

    Private _BarLengthValue As Short = 25
    <Category("Bar"), _
    Description("Get or Set Length of the bar in Fixed BarLength mode or Cylon Mode"), _
    DefaultValue(25)> _
    Public Property BarLengthValue() As Short
        Get
            Return _BarLengthValue
        End Get
        Set(ByVal Value As Short)
            _BarLengthValue = Value
            Me.Invalidate()
        End Set
    End Property

    Private _FillDirection As eFillDirection = eFillDirection.Up_Right
    <Category("Bar"), _
    Description("Get or Set the direction the Progress Bar will fill"), _
    DefaultValue(eFillDirection.Up_Right)> _
    Public Property FillDirection() As eFillDirection
        Get
            Return _FillDirection
        End Get
        Set(ByVal Value As eFillDirection)
            _FillDirection = Value
            Me.Invalidate()
        End Set
    End Property

    Private _Orientation As eOrientation = eOrientation.Horizontal
    <Category("Bar"), _
    Description("Get or Set the Progress Bar's Orientation"), _
    DefaultValue(eOrientation.Horizontal)> _
    Public Property Orientation() As eOrientation
        Get
            Return _Orientation
        End Get
        Set(ByVal Value As eOrientation)
            _Orientation = Value
            Me.Invalidate()
        End Set
    End Property

    Private _BarPadding As Padding
    <Description("The Solid Color to fill the Bar"), _
    Category("Bar")> _
    Public Property BarPadding() As Padding
        Get
            Return _BarPadding
        End Get
        Set(ByVal value As Padding)
            _BarPadding = value
            Me.Invalidate()
        End Set
    End Property

    Private _Min As Int32 = 0
    <Category("Bar"), _
    Description("Get or Set the Minimum Value"), _
    RefreshProperties(RefreshProperties.All), _
    DefaultValue(0)> _
    Public Property Min() As Int32
        Get
            Return _Min
        End Get
        Set(ByVal Val As Int32)
            _Min = Val
            If Me.Value < Val Then Me.Value = Val
            Me.Invalidate()
        End Set
    End Property

    Private _Max As Int32 = 100
    <Category("Bar"), _
    Description("Get or Set the Maximum Value"), _
    RefreshProperties(RefreshProperties.All), _
    DefaultValue(100)> _
    Public Property Max() As Int32
        Get
            Return _Max
        End Get
        Set(ByVal Val As Int32)
            _Max = Val
            If Me.Value > Val Then Me.Value = Val
            Me.Invalidate()
        End Set
    End Property

    Private _Value As Int32 = 50
    <Category("Bar"), _
    Description("Get or Set the Bar's Value"), _
    RefreshProperties(RefreshProperties.All), _
    DefaultValue(0)> _
    Public Property Value() As Int32
        Get
            Return _Value
        End Get
        Set(ByVal Val As Int32)
            If Val > Max Then Val = Max
            If Val < Min Then Val = Min
            _Value = Val
            Me.Refresh()
        End Set
    End Property

    Public Sub Increment(Optional ByVal Inc As Integer = 1)
        If Value < Max Then Value += Inc
    End Sub

    Public Sub Decrement(Optional ByVal Inc As Integer = 1)
        If Value > Min Then Value -= Inc
    End Sub

    Public Sub ResetBar(Optional ByVal ToMinimumValue As Boolean = True)
        If ToMinimumValue Then
            Value = Min
        Else
            Value = Max
        End If
    End Sub

    <Category("Bar"), _
    Description("Percent to of value")> _
    Public ReadOnly Property ValuePercent() As Int32
        Get
            Return CInt(((Value - Min) / (Max - Min)) * 100)
        End Get

    End Property

#End Region 'Bar Properties

#Region "Hidden Properties"

    <Browsable(False)> _
    Public Overrides Property BackColor() As Color
        Get
        End Get
        Set(ByVal Value As Color)
        End Set
    End Property

    <Browsable(False)> _
    Public Shadows Property BorderStyle() As BorderStyle
        Get
        End Get
        Set(ByVal Value As BorderStyle)
        End Set
    End Property

    <Browsable(False)> _
    Public Overrides Property AllowDrop() As Boolean
        Get
        End Get
        Set(ByVal Value As Boolean)
        End Set
    End Property

    <Browsable(False)> _
    Public Overrides Property AutoValidate() As AutoValidate
        Get
        End Get
        Set(ByVal Value As AutoValidate)
        End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
        Select Case TextShow
            Case eTextShow.None
                Return ""
            Case eTextShow.PercentOnly
                Return ValuePercent() & "%"
            Case eTextShow.TextOnly
                Return TextValue
            Case eTextShow.FormatStrPercent
                Return String.Format(TextFormat, ValuePercent())
            Case eTextShow.FormatStrText
                Return String.Format(TextFormat, TextValue)
            Case eTextShow.FormatStrTextPerc
                Return String.Format(TextFormat, TextValue, ValuePercent())
            Case Else
                Return ""
        End Select
    End Function

#End Region 'Properties

#Region "Paint Events"

    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaintBackground(pevent)

        pevent.Graphics.Clear(Me.BarBackColor)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim MyPath As New ProgBarPath
        MyPath = GetPath(Me.DisplayRectangle, True)

        If Not Me.BackgroundImage Is Nothing Then
            e.Graphics.DrawImage(Me.BackgroundImage, Me.DisplayRectangle)
        End If

        'Call the appropriate Paint Method to draw the bar
        Select Case BarType

            Case eBarType.Bar
                If Value > 0 Then
                    PaintBar(e)
                End If

            Case eBarType.CylonBar
                PaintCylonBar(e)

            Case eBarType.CylonGlider
                PaintCylonGlider(e)

        End Select

        'Create the Border Graphicspath and Draw it
        If Border Then
            Dim MyPen As New Pen(BorderColor, BorderWidth)
            MyPen.Alignment = PenAlignment.Inset
            With MyPath
                If Shape = eShape.Text Then

                    If ShapeTextRotate <> eRotateText.None Then
                        Dim mtrx As New Matrix
                        mtrx.Rotate(GetRotateAngle(ShapeTextRotate))
                        .Path.Transform(mtrx)
                    End If

                    e.Graphics.Transform = TextMatrix(MyPath)
                End If
                e.Graphics.DrawPath(MyPen, .Path)
                e.Graphics.ResetTransform()
            End With
            MyPen.Dispose()
        End If

        'Make a Region from the Graphicspath to clip the shape
        Me.Region = Nothing
        If Border Then If BorderWidth = 1 Then MyPath = GetPath(Me.DisplayRectangle, False)
        With MyPath
            Dim mRegion As Region
            If Shape = eShape.Text Then

                If ShapeTextRotate <> eRotateText.None Then
                    Dim mtrx As New Matrix
                    mtrx.Rotate(GetRotateAngle(ShapeTextRotate))
                    .Path.Transform(mtrx)
                End If

                mRegion = New Region(.Path)
                mRegion.Transform(TextMatrix(MyPath))
            Else
                mRegion = New Region(.Path)
            End If
            Me.Region = mRegion
            mRegion.Dispose()
        End With

        'Add the Text
        If Me.TextShow <> eTextShow.None And Me.TextPlacement = eTextPlacement.OverBar Then PutText(e, Me.DisplayRectangle)

    End Sub

    Private Sub PaintBar(ByVal e As PaintEventArgs)
        Dim OrientationWidth As Integer
        Dim EndPosition As Integer
        Dim StartPosition As Integer
        Dim LengthOfBar As Integer
        Dim rect As Rectangle
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        If Orientation = eOrientation.Horizontal Then
            OrientationWidth = Me.Size.Width
        Else
            OrientationWidth = Me.Size.Height
        End If

        If BarLength = eBarLength.Full Then
            LengthOfBar = Convert.ToInt32(OrientationWidth * ((Value - Min) / (Max - Min))) + 2 '- BorderWidth
            StartPosition = 0
        Else
            EndPosition = Convert.ToInt32((OrientationWidth) * ((Value - Min) / (Max - Min)))   '- BorderWidth
            LengthOfBar = BarLengthValue
            StartPosition = EndPosition - BarLengthValue
            If StartPosition < BorderWidth Then StartPosition = 1
        End If

        If Orientation = eOrientation.Horizontal Then
            If FillDirection = eFillDirection.Down_Left Then
                rect = New Rectangle(OrientationWidth - StartPosition - LengthOfBar, 0, LengthOfBar + 1, Height - 1)
            Else
                rect = New Rectangle(StartPosition - 1, 0, LengthOfBar, Height - 1)
            End If
        Else
            If FillDirection = eFillDirection.Down_Left Then
                rect = New Rectangle(0, StartPosition - 2, Width, LengthOfBar)
            Else
                rect = New Rectangle(0, OrientationWidth - StartPosition - LengthOfBar, Width, LengthOfBar + 1)
            End If
        End If

        rect.X += BarPadding.Left
        rect.Y += BarPadding.Top
        rect.Width -= BarPadding.Horizontal
        rect.Height -= BarPadding.Vertical

        e.Graphics.FillPath(CType(GetBrush(rect), Brush), CornerPath(rect))

        If Me.TextShow <> eTextShow.None And Me.TextPlacement = eTextPlacement.OnBar Then PutText(e, rect)

    End Sub

    Private Sub PaintCylonBar(ByVal e As PaintEventArgs)
        Dim rect As Rectangle
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        If Orientation = eOrientation.Horizontal Then
            rect = New Rectangle(CInt(CylonPosition + BarPadding.Left), 0 + BarPadding.Top, BarLengthValue - 1, Me.Height - 1 - BarPadding.Vertical)
        Else
            rect = New Rectangle(0 + BarPadding.Left, CInt(CylonPosition + BarPadding.Top), Me.Width - 1 - BarPadding.Horizontal, BarLengthValue - 1)
        End If

        e.Graphics.FillPath(CType(GetBrush(rect), Brush), CornerPath(rect))

    End Sub

    Private Sub PaintCylonGlider(ByVal e As PaintEventArgs)

        Dim rect As Rectangle
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Dim br As LinearGradientBrush
        rect = New Rectangle(BarPadding.Left, BarPadding.Top, Me.Width - BarPadding.Horizontal, Me.Height - BarPadding.Vertical)
        rect.Inflate(1, 1)
        If Orientation = eOrientation.Horizontal Then
            br = New LinearGradientBrush(New Point(rect.X, rect.Y), New Point(rect.Right, rect.Y), Color.White, Color.White)
        Else
            br = New LinearGradientBrush(New Point(rect.X, rect.Bottom), New Point(rect.X, rect.Top + 1), Color.White, Color.White)
        End If
        rect.Inflate(-1, -2)

        Dim blend As ColorBlend = New ColorBlend()
        blend.Colors = Me.BarColorBlend.iColor
        blend.Positions = Me.BarColorBlend.iPoint
        If blend.Positions.Length > 2 Then
            blend.Colors = New Color() { _
                Me.BarColorBlend.iColor(0), _
                Me.BarColorBlend.iColor(0), _
                Me.BarColorBlend.iColor(1), _
                Me.BarColorBlend.iColor(2), _
                Me.BarColorBlend.iColor(2)}
        Else
            blend.Colors = New Color() { _
                Me.BarColorSolid, _
                Me.BarColorSolid, _
                Me.BarColorSolidB, _
                Me.BarColorSolid, _
                Me.BarColorSolid}

        End If
        blend.Positions = New Single() { _
            0, _
            CSng(CylonGPosition - 0.3), _
            CylonGPosition, _
            CSng(CylonGPosition + 0.3), _
            1}
        br.InterpolationColors = blend

        e.Graphics.FillPath(br, CornerPath(rect))
    End Sub
#End Region 'Paint Events

#Region "Paint Helpers"

    Private Function CornerPath(ByVal rect As Rectangle) As GraphicsPath
        Dim gp As New GraphicsPath
        If Me.Shape = eShape.Rectangle Then
            Select Case CornersApply
                Case eCornersApply.Bar, eCornersApply.Both
                    gp = GetRoundedRectPath(rect, Corners)
                Case eCornersApply.Border
                    gp.AddRectangle(rect)
            End Select
        Else
            gp.AddRectangle(rect)
        End If
        Return gp
    End Function

    Public Function TextMatrix(ByVal mp As ProgBarPath) As Matrix
        'Scale the Path to fit the Rectangle
        With mp
            Dim text_rectf As RectangleF = .Path.GetBounds()
            Dim target_pts() As PointF = { _
                New PointF(.Rect.Left, .Rect.Top), _
                New PointF(.Rect.Right, .Rect.Top), _
                New PointF(.Rect.Left, .Rect.Bottom)}

            Return New Matrix(text_rectf, target_pts)
        End With
    End Function

    Public Function GetPath(ByVal PathRect As Rectangle, ByVal IsBorder As Boolean, Optional ByVal ShowDotBorder As Short = 0) As ProgBarPath

        Dim pPath As New ProgBarPath
        With pPath
            .Path = New GraphicsPath
            Select Case Shape
                Case eShape.Rectangle
                    If IsBorder Then
                        .Rect = New Rectangle(PathRect.X, PathRect.Y, PathRect.Width - 1, PathRect.Height - 1)
                    Else
                        .Rect = New Rectangle(PathRect.X, PathRect.Y, PathRect.Width - ShowDotBorder, PathRect.Height - ShowDotBorder)
                    End If
                    If CornersApply = eCornersApply.Bar Then
                        .Path.AddRectangle(.Rect)
                    Else
                        .Path = GetRoundedRectPath(.Rect, Corners)
                    End If

                Case eShape.Ellipse
                    If IsBorder Then
                        .Rect = New Rectangle(1, 1, PathRect.Width - 2, PathRect.Height - 2)
                    Else
                        .Rect = New Rectangle(PathRect.X + ShowDotBorder, PathRect.Y + ShowDotBorder, PathRect.Width - ShowDotBorder * 2, PathRect.Height - ShowDotBorder * 2)
                    End If
                    .Path.AddEllipse(.Rect)

                Case eShape.TriangleLeft
                    .Rect = New Rectangle(PathRect.X, PathRect.Y, PathRect.Width, PathRect.Height)
                    Dim myArray() As Point
                    If IsBorder Then
                        myArray = New Point() {New Point(.Rect.Left, CInt(.Rect.Height / 2)), New Point(.Rect.Right, 1), New Point(.Rect.Right, .Rect.Bottom - 1)}
                    Else
                        myArray = New Point() {New Point(.Rect.Left + ShowDotBorder, CInt(.Rect.Height / 2)), _
                                               New Point(CInt(.Rect.Right - ShowDotBorder / 2), 0 + ShowDotBorder), _
                                               New Point(CInt(.Rect.Right - ShowDotBorder / 2), .Rect.Bottom - ShowDotBorder)}
                    End If
                    .Path.AddPolygon(myArray)

                Case eShape.TriangleRight
                    .Rect = New Rectangle(PathRect.X, PathRect.Y, PathRect.Width, PathRect.Height)
                    Dim myArray() As Point
                    If IsBorder Then
                        myArray = New Point() {New Point(0, 1), New Point(0, .Rect.Bottom - 1), New Point(.Rect.Right, CInt(.Rect.Height / 2))}
                    Else
                        myArray = New Point() {New Point(0, 0 + ShowDotBorder), New Point(0, .Rect.Bottom - ShowDotBorder), New Point(.Rect.Right - ShowDotBorder, CInt(.Rect.Height / 2))}
                    End If
                    .Path.AddPolygon(myArray)

                Case eShape.TriangleUp
                    .Rect = New Rectangle(PathRect.X, PathRect.Y, PathRect.Width, PathRect.Height)
                    Dim myArray() As Point
                    If IsBorder Then
                        myArray = New Point() {New Point(CInt(.Rect.Width / 2), .Rect.Top), New Point(.Rect.Left + 2, .Rect.Bottom - 1), New Point(.Rect.Right - 2, .Rect.Bottom - 1)}
                    Else
                        myArray = New Point() {New Point(CInt(.Rect.Width / 2), CInt(.Rect.Top + ShowDotBorder / 2)), New Point(.Rect.Left + ShowDotBorder * 2, .Rect.Bottom - ShowDotBorder), New Point(.Rect.Right - ShowDotBorder * 2, .Rect.Bottom - ShowDotBorder)}
                    End If
                    .Path.AddPolygon(myArray)

                Case eShape.TriangleDown
                    .Rect = New Rectangle(PathRect.X, PathRect.Y, PathRect.Width, PathRect.Height)
                    Dim myArray() As Point
                    If IsBorder Then
                        myArray = New Point() {New Point(CInt(.Rect.Width / 2), .Rect.Bottom), New Point(.Rect.Left + 1, 0), New Point(.Rect.Right - 1, 0)}
                    Else
                        myArray = New Point() {New Point(CInt(.Rect.Width / 2), CInt(.Rect.Bottom - ShowDotBorder / 2)), New Point(.Rect.Left + ShowDotBorder, 0), New Point(.Rect.Right - ShowDotBorder, 0)}
                    End If
                    .Path.AddPolygon(myArray)

                Case eShape.Text

                    If IsBorder Then
                        .Rect = New Rectangle(1, 1, PathRect.Width - 2, PathRect.Height - 2)
                    Else
                        .Rect = New Rectangle(PathRect.X + 1, PathRect.Y + 1, CInt(PathRect.Width - 2 - ShowDotBorder / 2), PathRect.Height - 2 - ShowDotBorder)
                    End If

                    ' Make the StringFormat.
                    Dim sf As New StringFormat
                    sf.Alignment = StringAlignment.Center
                    sf.LineAlignment = StringAlignment.Center

                    ' Add the text to the GraphicsPath.
                    .Path.AddString(ShapeText, _
                        ShapeTextFont.FontFamily, CInt(FontStyle.Bold), _
                        .Rect.Height, New PointF(0, 0), sf)

                    sf.Dispose()
            End Select

        End With
        Return pPath
    End Function

    Sub PutText(ByVal e As PaintEventArgs, ByVal TextRect As Rectangle)
        Dim sf As New StringFormat
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        sf.Alignment = TextAlignment
        sf.LineAlignment = TextAlignmentVert
        If Not TextWrap Then sf.FormatFlags = StringFormatFlags.NoWrap
        If TextShadow Then
            Dim ShadowRect As Rectangle = TextRect
            ShadowRect.Offset(1, 1)
            If TextRotate <> eRotateText.None Then ShadowRect = RotateRect(e, ShadowRect, TextRotate)
            e.Graphics.DrawString(Me.ToString, Me.Font, New SolidBrush(TextShadowColor), ShadowRect, sf)
            e.Graphics.ResetTransform()
        End If

        If TextRotate <> eRotateText.None Then TextRect = RotateRect(e, TextRect, TextRotate)
        e.Graphics.DrawString(Me.ToString, Me.Font, New SolidBrush(Me.ForeColor), TextRect, sf)
        e.Graphics.ResetTransform()

    End Sub

    Private Function RotateRect(ByVal e As System.Windows.Forms.PaintEventArgs, ByVal TabRect As Rectangle, ByVal Rotate As eRotateText) As Rectangle

        Dim cp As New PointF(TabRect.Left + (TabRect.Width \ 2), TabRect.Top + (TabRect.Height \ 2))
        e.Graphics.TranslateTransform(cp.X, cp.Y)
        e.Graphics.RotateTransform(GetRotateAngle(Rotate))
        Return New Rectangle(-(TabRect.Height \ 2), -(TabRect.Width \ 2), TabRect.Height, TabRect.Width)

    End Function

    Public Function GetRotateAngle(ByVal Rotate As eRotateText) As Short
        Dim RotateAngle As Single = 0
        Select Case Rotate
            Case eRotateText.Left
                RotateAngle = 270
            Case eRotateText.Right
                RotateAngle = 90
        End Select
        Return CShort(RotateAngle)
    End Function

    Private Function GetBrush(ByRef rect As Rectangle) As Brush
        Try

            Select Case BarStyleFill
                Case eBarStyle.Solid
                    Return New SolidBrush(BarColorSolid)
                Case eBarStyle.Hatch
                    Return New HatchBrush(BarStyleHatch, BarColorSolid, BarColorSolidB)
                Case eBarStyle.GradientLinear
                    If Me.Orientation = eOrientation.Horizontal Then
                        rect.Inflate(-2, 0)
                    Else
                        rect.Inflate(0, -2)
                    End If
                    Dim br As LinearGradientBrush = New LinearGradientBrush(rect, Color.White, Color.White, BarStyleLinear)
                    Dim cb As New ColorBlend
                    cb.Colors = Me.BarColorBlend.iColor()
                    cb.Positions = Me.BarColorBlend.iPoint
                    br.InterpolationColors = cb
                    Return br

                Case eBarStyle.GradientPath
                    Dim OffsetX As Single = 0
                    Dim OffsetY As Single = 0
                    Dim CylonOffsetX As Single = 0
                    Dim CylonOffsetY As Single = 0
                    If Me.BarType = eBarType.CylonBar Then
                        If Me.Orientation = eOrientation.Horizontal Then
                            CylonOffsetX = CylonPosition
                        Else
                            CylonOffsetY = CylonPosition
                        End If
                    Else
                        If Me.Orientation = eOrientation.Horizontal Then
                            OffsetX = rect.X
                        Else
                            OffsetY = rect.Y
                        End If
                    End If
                    Dim br As PathGradientBrush = New PathGradientBrush(GetPath(rect, False).Path)
                    Dim cb As New ColorBlend
                    cb.Colors = Me.BarColorBlend.iColor
                    cb.Positions = Me.BarColorBlend.iPoint
                    br.FocusScales = Me.FocalPoints.FocusScales
                    br.CenterPoint = New PointF( _
                        Me.FocalPoints.CenterPoint.X * CSng(rect.Width) + OffsetX + Me.BarPadding.Left + CylonOffsetX, _
                        Me.FocalPoints.CenterPoint.Y * CSng(rect.Height) + OffsetY + Me.BarPadding.Top + CylonOffsetY)
                    br.InterpolationColors = cb
                    Return br

                Case eBarStyle.Texture
                    Dim br As TextureBrush
                    br = New TextureBrush(BarStyleTexture)
                    br.WrapMode = BarStyleWrapMode
                    Return br
            End Select
        Catch ex As Exception
            Return New SolidBrush(BarColorSolid)
        End Try
        Return New SolidBrush(BarColorSolid)
    End Function

    Public Function GetRoundedRectPath(ByVal BaseRect As RectangleF, ByVal rCorners As CornersProperty) As GraphicsPath

        Dim ArcRect As RectangleF
        Dim MyPath As New Drawing2D.GraphicsPath()
        If rCorners.All = -1 Then
            With MyPath
                ' top left arc
                If rCorners.UpperLeft = 0 Then
                    .AddLine(BaseRect.X, BaseRect.Y, BaseRect.X, BaseRect.Y)
                Else
                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.UpperLeft * 2, rCorners.UpperLeft * 2))
                    .AddArc(ArcRect, 180, 90)
                End If

                ' top right arc
                If rCorners.UpperRight = 0 Then
                    .AddLine(BaseRect.X + (rCorners.UpperLeft), BaseRect.Y, BaseRect.Right - (rCorners.UpperRight), BaseRect.Top)
                Else
                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.UpperRight * 2, rCorners.UpperRight * 2))
                    ArcRect.X = BaseRect.Right - (rCorners.UpperRight * 2)
                    .AddArc(ArcRect, 270, 90)
                End If

                ' bottom right arc
                If rCorners.LowerRight = 0 Then
                    .AddLine(BaseRect.Right, BaseRect.Top + (rCorners.UpperRight), BaseRect.Right, BaseRect.Bottom - (rCorners.LowerRight))
                Else
                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.LowerRight * 2, rCorners.LowerRight * 2))
                    ArcRect.Y = BaseRect.Bottom - (rCorners.LowerRight * 2)
                    ArcRect.X = BaseRect.Right - (rCorners.LowerRight * 2)
                    .AddArc(ArcRect, 0, 90)
                End If

                ' bottom left arc
                If rCorners.LowerLeft = 0 Then
                    .AddLine(BaseRect.Right - (rCorners.LowerRight), BaseRect.Bottom, BaseRect.X - (rCorners.LowerLeft), BaseRect.Bottom)
                Else
                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.LowerLeft * 2, rCorners.LowerLeft * 2))
                    ArcRect.Y = BaseRect.Bottom - (rCorners.LowerLeft * 2)
                    .AddArc(ArcRect, 90, 90)
                End If

                .CloseFigure()
            End With
        Else
            With MyPath
                If rCorners.All = 0 Then
                    .AddRectangle(BaseRect)
                Else

                    ArcRect = New RectangleF(BaseRect.Location, _
                        New SizeF(rCorners.All * 2, rCorners.All * 2))
                    ' top left arc
                    .AddArc(ArcRect, 180, 90)

                    ' top right arc
                    ArcRect.X = BaseRect.Right - (rCorners.All * 2)
                    .AddArc(ArcRect, 270, 90)

                    ' bottom right arc
                    ArcRect.Y = BaseRect.Bottom - (rCorners.All * 2)
                    .AddArc(ArcRect, 0, 90)

                    ' bottom left arc
                    ArcRect.X = BaseRect.Left
                    .AddArc(ArcRect, 90, 90)

                End If
                .CloseFigure()
            End With
        End If
        Return MyPath
    End Function
#End Region 'Paint Helpers

#Region "Cylon"

    Private Sub TimerCylon_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerCylon.Tick
        Select Case BarType
            Case eBarType.CylonBar
                If Orientation = eOrientation.Horizontal Then
                    If CylonPosition + BarLengthValue >= Me.Width Then CylonDirection = -(_CylonMove)
                    If CylonPosition <= 0 Then CylonDirection = _CylonMove
                Else
                    If CylonPosition + BarLengthValue >= Me.Height Then CylonDirection = -(_CylonMove)
                    If CylonPosition <= 0 Then CylonDirection = _CylonMove
                End If

                CylonPosition += CylonDirection

            Case eBarType.CylonGlider
                CylonGPosition += CylonGDelta * _CylonMove
                If (CylonGPosition > 1) OrElse (CylonGPosition < 0) Then CylonGDelta = -CylonGDelta
        End Select

        Me.Refresh()
    End Sub

    Friend WithEvents TimerCylon As System.Windows.Forms.Timer

#End Region 'Cylon

End Class

#Region "Dropdown Editors"

#Region "HatchStyleEditor"

Public Class HatchStyleEditor
    Inherits UITypeEditor

    ' Indicate that we display a dropdown.
    Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    ' Edit a line style
    Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        ' Get an IWindowsFormsEditorService object.
        Dim editor_service As IWindowsFormsEditorService = _
            CType(provider.GetService(GetType(IWindowsFormsEditorService)), _
            IWindowsFormsEditorService)
        If editor_service Is Nothing Then
            Return MyBase.EditValue(context, provider, value)
        End If

        ' Pass the UI editor the current property value
        Dim Instance As New ProgBarPlus
        If context.Instance.GetType Is GetType(ProgBarPlus) Then
            Instance = CType(context.Instance, ProgBarPlus)
        Else
            Instance = CType(context.Instance, ProgBarPlusActionList).CurrProgBarPlus
        End If

        ' Convert the value into a BorderStyles value.
        Dim hatch_style As HatchStyle = DirectCast(value, HatchStyle)

        ' Make the editing control.
        Dim editor_control As New HatchStyleListBox(hatch_style.ToString, Instance.BarColorSolid, Instance.BarColorSolidB, editor_service)
        ' Display the editing control.
        editor_service.DropDownControl(editor_control)

        ' Save the new results.
        Return CType(System.Enum.Parse(GetType(HatchStyle), editor_control.Text, True), HatchStyle)
    End Function

    Public Overrides ReadOnly Property IsDropDownResizable() As Boolean
        Get
            Return MyBase.IsDropDownResizable
        End Get
    End Property

    Private SmartContext As ITypeDescriptorContext 'SmartTag Workaround
    Public Overrides Function GetPaintValueSupported(ByVal context As ITypeDescriptorContext) As Boolean
        SmartContext = context 'store reference for use in PaintValue
        Return True
    End Function

    Public Overrides Sub PaintValue(ByVal e As PaintValueEventArgs)
        Dim hatch As HatchStyle = CType(e.Value, HatchStyle)
        ' Pass the UI editor the current property value
        Dim Instance As New ProgBarPlus
        'e.context only works properly in the Propertygrid.
        'When comming from the SmartTag e.context becomes null and 
        'will cause a fatal crash of the IDE.
        'So to get around the null value error I captured a reference to the context
        'in the SmartContext variable in the GetPaintValueSupported function 
        If e.Context IsNot Nothing Then
            Instance = CType(e.Context.Instance, ProgBarPlus)
        Else
            Instance = CType(SmartContext.Instance, ProgBarPlusActionList).CurrProgBarPlus
        End If

        Using br As Brush = New HatchBrush(hatch, Instance.BarColorSolid, Instance.BarColorSolidB)
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using

    End Sub
End Class

#Region "HatchStyleListBox Custom Control"

<ToolboxItem(False)> _
Public Class HatchStyleListBox
    Inherits ListBox

    ' The editor service displaying this control.
    Private m_EditorService As IWindowsFormsEditorService

    Public Sub New(ByVal hatch_style As String, _
      ByVal ColorFore As Color, _
      ByVal ColorBack As Color, _
      ByVal editor_service As IWindowsFormsEditorService)
        MyBase.New()

        m_EditorService = editor_service
        ' Make items for each LineStyles value.
        Me.Items.Clear()
        Dim hatchNames As String() = System.Enum.GetNames(GetType(HatchStyle))
        Array.Sort(hatchNames)
        For Each hs As String In hatchNames
            Me.Items.Add(hs)
        Next
        Me.SelectedIndex = Me.FindStringExact(hatch_style)
        Me.ColorFore = ColorFore
        Me.ColorBack = ColorBack
        Me.DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ItemHeight = 21
        Me.Height = 200
        Me.Width = 200
    End Sub

    Private _ColorFore As Color
    Public Property ColorFore() As Color
        Get
            Return _ColorFore
        End Get
        Set(ByVal value As Color)
            _ColorFore = value
        End Set
    End Property

    Private _ColorBack As Color
    Public Property ColorBack() As Color
        Get
            Return _ColorBack
        End Get
        Set(ByVal value As Color)
            _ColorBack = value
        End Set
    End Property

    ' When the user selects an item, close the dropdown.
    Private Sub HatchStyleListBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        If m_EditorService IsNot Nothing Then
            m_EditorService.CloseDropDown()
        End If
    End Sub

    ' Draw a menu item.
    Private Sub HatchStyleListBox_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
        e.DrawBackground()
        If e.Index <> -1 And Me.Items.Count > 0 Then
            Dim g As Graphics = e.Graphics
            Dim sample As Rectangle = e.Bounds
            Dim sampletext As Rectangle = e.Bounds

            sample.Width = 40
            sample.Inflate(0, -3)
            sampletext.Width = sampletext.Width - sample.Width - 2
            sampletext.X = sample.Right + 2

            Dim displayText As String = Me.Items(e.Index).ToString()
            Dim hs As HatchStyle = CType(System.Enum.Parse(GetType(HatchStyle), displayText, True), HatchStyle)
            Dim hb As HatchBrush = New HatchBrush(hs, ColorFore, ColorBack)

            Dim sf As StringFormat = New StringFormat()
            sf.Alignment = StringAlignment.Near
            sf.LineAlignment = StringAlignment.Center
            sf.FormatFlags = StringFormatFlags.NoWrap
            If (e.State And DrawItemState.Focus) = 0 Then
                g.FillRectangle(New SolidBrush(SystemColors.Window), sampletext)
                g.DrawString(displayText, Me.Font, New SolidBrush(SystemColors.WindowText), sampletext, sf)
            Else
                g.FillRectangle(New SolidBrush(SystemColors.Highlight), sampletext)
                g.DrawString(displayText, Me.Font, New SolidBrush(SystemColors.HighlightText), sampletext, sf)
            End If
            g.FillRectangle(hb, sample)
            g.DrawRectangle(New Pen(Color.Black, 1), sample)
        End If
        e.DrawFocusRectangle()

    End Sub
End Class

#End Region 'HatchStyleListBox Custom Control

#End Region 'HatchStyleEditor

#Region "BlendTypeEditor - UITypeEditor"

#Region "cBlendItems"

Public Class cBlendItems

    Sub New()

    End Sub

    Sub New(ByVal Color As Color(), ByVal Pt As Single())
        iColor = Color
        iPoint = Pt
    End Sub

    Private _iColor As Color()
    <Description("The Color for the Point"), _
       Category("Appearance")> _
    Public Property iColor() As Color()
        Get
            Return _iColor
        End Get
        Set(ByVal value As Color())
            _iColor = value
        End Set
    End Property

    Private _iPoint As Single()
    <Description("The Color for the Point"), _
       Category("Appearance")> _
    Public Property iPoint() As Single()
        Get
            Return _iPoint
        End Get
        Set(ByVal value As Single())
            _iPoint = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return "BlendItems"
    End Function

End Class

#End Region 'cBlendItems

Public Class BlendTypeEditor
    Inherits UITypeEditor

    Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If Not context Is Nothing Then
            Return UITypeEditorEditStyle.DropDown
        End If
        Return (MyBase.GetEditStyle(context))
    End Function

    Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (Not context Is Nothing) And (Not provider Is Nothing) Then
            ' Access the property browser's UI display service, IWindowsFormsEditorService
            Dim editorService As IWindowsFormsEditorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If Not editorService Is Nothing Then
                ' Create an instance of the UI editor, passing a reference to the editor service
                Dim dropDownEditor As DropdownColorBlender = New DropdownColorBlender(editorService)

                ' Pass the UI editor the current property value
                Dim Instance As New ProgBarPlus
                If context.Instance.GetType Is GetType(ProgBarPlus) Then
                    Instance = CType(context.Instance, ProgBarPlus)
                Else
                    Instance = CType(context.Instance, ProgBarPlusActionList).CurrProgBarPlus
                End If

                With dropDownEditor

                    Select Case Instance.Shape
                        Case ProgBarPlus.eShape.Ellipse
                            .BlendPathShape = DropdownColorBlender.eBlendPathShape.Ellipse
                        Case ProgBarPlus.eShape.Rectangle, ProgBarPlus.eShape.Text
                            .BlendPathShape = DropdownColorBlender.eBlendPathShape.Rectangle
                        Case Else
                            .BlendPathShape = DropdownColorBlender.eBlendPathShape.Triangle
                    End Select

                    If Instance.BarStyleFill = ProgBarPlus.eBarStyle.GradientPath Then
                        .BlendGradientType = DropdownColorBlender.eBlendGradientType.Path
                        .FocalPoints = New cFocalPoints( _
                                New PointF( _
                                    Instance.FocalPoints.CenterPoint.X, _
                                    Instance.FocalPoints.CenterPoint.Y), _
                                Instance.FocalPoints.FocusScales)
                    Else
                        .BlendGradientType = DropdownColorBlender.eBlendGradientType.Linear
                        .BlendGradientMode = Instance.BarStyleLinear
                    End If


                    .LoadABlend(Instance.BarColorBlend)
                End With

                ' Display the UI editor
                editorService.DropDownControl(dropDownEditor)

                ' Return the new property value from the editor
                Return New cBlendItems(dropDownEditor.BlendColors, dropDownEditor.BlendPositions)
            End If
        End If
        Return MyBase.EditValue(context, provider, value)
    End Function

    ' Indicate that we draw values in the Properties window.
    Public Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    ' Draw a BorderStyles value.
    Public Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        ' Erase the area.
        e.Graphics.FillRectangle(Brushes.White, e.Bounds)

        ' Draw the sample.
        Dim cblnd As cBlendItems = DirectCast(e.Value, cBlendItems)
        Using br As LinearGradientBrush = New LinearGradientBrush(e.Bounds, Color.Black, Color.Black, LinearGradientMode.Horizontal)
            Dim cb As New ColorBlend
            cb.Colors = cblnd.iColor
            cb.Positions = cblnd.iPoint
            br.InterpolationColors = cb
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using
    End Sub
End Class
#End Region 'BlendTypeEditor - UITypeEditor

#End Region 'Dropdown Editors

#Region "Modal Form Editors"

#Region "FocalTypeEditor"

#Region "cFocalPoints"

Public Class cFocalPoints

    Private _CenterPoint As PointF = New PointF(0.5, 0.5)
    Public Property CenterPoint() As PointF
        Get
            Return _CenterPoint
        End Get
        Set(ByVal value As PointF)
            If value.X < 0 Then value.X = 0
            If value.X > 1 Then value.X = 1
            If value.Y < 0 Then value.Y = 0
            If value.Y > 1 Then value.Y = 1
            _CenterPoint = value
        End Set
    End Property

    Private _FocusScales As PointF = New PointF(0, 0)
    Public Property FocusScales() As PointF
        Get
            Return _FocusScales
        End Get
        Set(ByVal value As PointF)
            If value.X < 0 Then value.X = 0
            If value.X > 1 Then value.X = 1
            If value.Y < 0 Then value.Y = 0
            If value.Y > 1 Then value.Y = 1
            _FocusScales = value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByVal Cx As Single, ByVal Cy As Single, ByVal Fx As Single, ByVal Fy As Single)
        Me.CenterPoint = New PointF(Cx, Cy)
        Me.FocusScales = New PointF(Fx, Fy)
    End Sub

    Sub New(ByVal ptC As PointF, ByVal ptF As PointF)
        Me.CenterPoint = ptC
        Me.FocusScales = ptF
    End Sub

    Public Overrides Function ToString() As String
        Return "CP=" & _CenterPoint.ToString & ", FP=" & _FocusScales.ToString
    End Function

End Class

#End Region 'cFocalPoints

Public Class FocalTypeEditor
    Inherits UITypeEditor

    ' Indicate that we display a modal dialog.
    Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.Modal
    End Function

    ' Edit a Selected value.
    Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
        ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        ' Get the editor service.
        Dim editor_service As IWindowsFormsEditorService = _
            CType(provider.GetService(GetType(IWindowsFormsEditorService)), _
                IWindowsFormsEditorService)
        If editor_service Is Nothing Then Return value

        Dim Instance As New ProgBarPlus
        If context.Instance.GetType Is GetType(ProgBarPlus) Then
            Instance = CType(context.Instance, ProgBarPlus)
        Else
            Instance = CType(context.Instance, ProgBarPlusActionList).CurrProgBarPlus
        End If

        Dim dlg As dlgFocalPoints = New dlgFocalPoints

        ' Prepare the editing dialog.
        With dlg
            Dim ratio As Single
            Dim BarWidth As Integer = 0
            Dim BarHeight As Integer = 0
            If Instance.BarType = ProgBarPlus.eBarType.CylonBar Then
                If Instance.Orientation = ProgBarPlus.eOrientation.Horizontal Then
                    BarWidth = Instance.BarLengthValue
                    BarHeight = Instance.Height
                Else
                    BarWidth = Instance.Width
                    BarHeight = Instance.BarLengthValue
                End If
            Else
                BarWidth = Instance.Width
                BarHeight = Instance.Height
            End If


            If BarWidth > BarHeight Then
                .TheShape.Height = CInt(.TheShape.Width * (BarHeight / BarWidth))
                .TheShape.Top = CInt((.panShapeHolder.Height - .TheShape.Height) / 2)
                ratio = CSng(.TheShape.Height / BarHeight)
            Else
                .TheShape.Width = CInt(.TheShape.Height * (BarWidth / BarHeight))
                .TheShape.Left = CInt((.panShapeHolder.Width - .TheShape.Width) / 2)
                ratio = CSng(.TheShape.Width / BarWidth)
            End If

            .TheShape.Shape = Instance.Shape
            .TheShape.BarStyleFill = Instance.BarStyleFill
            .TheShape.BarStyleLinear = Instance.BarStyleLinear
            .TheShape.BarColorSolid = Instance.BarColorSolid
            .TheShape.BorderWidth = Instance.BorderWidth
            .TheShape.BorderColor = Instance.BorderColor
            .TheShape.BorderStyle = Instance.BorderStyle
            .TheShape.BarColorBlend = Instance.BarColorBlend
            .TheShape.Corners = New CornersProperty( _
                            CShort(Instance.Corners.LowerLeft * ratio), _
                            CShort(Instance.Corners.LowerRight * ratio), _
                            CShort(Instance.Corners.UpperLeft * ratio), _
                            CShort(Instance.Corners.UpperRight * ratio))
            .TheShape.FocalPoints = New cFocalPoints( _
                            Instance.FocalPoints.CenterPoint, _
                            Instance.FocalPoints.FocusScales)
        End With

        ' Display the dialog.
        editor_service.ShowDialog(dlg)
        Instance.Refresh()
        ' Return the new value.
        Return dlg.TheShape.FocalPoints
    End Function
End Class

#End Region 'FocalTypeEditor

#End Region 'Modal Form Editors

#Region "Expandable Border Corners Property Class"

<TypeConverter(GetType(CornerConverter)), Serializable()> _
Public Class CornersProperty

    Private _All As Short = -1
    Private _UpperLeft As Short = 0
    Private _UpperRight As Short = 0
    Private _LowerLeft As Short = 0
    Private _LowerRight As Short = 0

    Public Sub New(ByVal LowerLeft As Short, ByVal LowerRight As Short, _
      ByVal UpperLeft As Short, ByVal UpperRight As Short)
        Me.LowerLeft = LowerLeft
        Me.LowerRight = LowerRight
        Me.UpperLeft = UpperLeft
        Me.UpperRight = UpperRight
    End Sub

    Public Sub New()
        Me.LowerLeft = 0
        Me.LowerRight = 0
        Me.UpperLeft = 0
        Me.UpperRight = 0
    End Sub

    Private Sub CheckForAll(ByVal val As Short)
        If val = LowerLeft AndAlso _
           val = LowerRight AndAlso _
           val = UpperLeft AndAlso _
           val = UpperRight Then
            If All <> val Then All = val
        Else
            If All <> -1 Then All = -1
        End If
    End Sub

    <DescriptionAttribute("Set the Radius of the All four Corners the same"), _
    NotifyParentProperty(True), _
    RefreshProperties(RefreshProperties.All)> _
    Public Property All() As Short
        Get
            Return _All
        End Get
        Set(ByVal Value As Short)
            _All = Value
            If Value > -1 Then
                Me.LowerLeft = Value
                Me.LowerRight = Value
                Me.UpperLeft = Value
                Me.UpperRight = Value
            End If
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Upper Left Corner"), _
    RefreshProperties(RefreshProperties.All)> _
    Public Property UpperLeft() As Short
        Get
            Return _UpperLeft
        End Get
        Set(ByVal Value As Short)
            _UpperLeft = Value
            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Upper Right Corner"), _
    RefreshProperties(RefreshProperties.All)> _
    Public Property UpperRight() As Short
        Get
            Return _UpperRight
        End Get
        Set(ByVal Value As Short)
            _UpperRight = Value
            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Lower Left Corner"), _
    RefreshProperties(RefreshProperties.All)> _
    Public Property LowerLeft() As Short
        Get
            Return _LowerLeft
        End Get
        Set(ByVal Value As Short)
            _LowerLeft = Value
            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Lower Right Corner"), _
    RefreshProperties(RefreshProperties.All)> _
    Public Property LowerRight() As Short
        Get
            Return _LowerRight
        End Get
        Set(ByVal Value As Short)
            _LowerRight = Value
            CheckForAll(Value)
        End Set
    End Property

End Class 'Corners Properties

Friend Class CornerConverter : Inherits ExpandableObjectConverter

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim s As String = CType(value, String)
                Dim cornerParts(4) As String
                cornerParts = Split(s, ",")
                If Not IsNothing(cornerParts) Then
                    If IsNothing(cornerParts(0)) Then cornerParts(0) = CStr(0)
                    If IsNothing(cornerParts(1)) Then cornerParts(1) = CStr(0)
                    If IsNothing(cornerParts(2)) Then cornerParts(2) = CStr(0)
                    If IsNothing(cornerParts(3)) Then cornerParts(3) = CStr(0)
                    Return New CornersProperty(CShort(cornerParts(0)), CShort(cornerParts(1)), CShort(cornerParts(2)), CShort(cornerParts(3)))
                End If
            Catch ex As Exception
                Throw New ArgumentException("Can not convert '" & CStr(value) & "' to type Corners")
            End Try
        Else
            Return New CornersProperty()
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, _
      ByVal value As Object, ByVal destinationType As System.Type) As Object

        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is CornersProperty) Then
            Dim _Corners As CornersProperty = CType(value, CornersProperty)

            ' build the string as "UpperLeft,UpperRight,LowerLeft,LowerRight" 
            Return String.Format("{0},{1},{2},{3}", _Corners.LowerLeft, _Corners.LowerRight, _Corners.UpperLeft, _Corners.UpperRight)
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)

    End Function

End Class 'CornerConverter Code

#End Region 'Expandable Border Corners Property Class

#Region "Control Designer"
'
'You have to directly add the System.Design Reference to the Project
'
Public Class ProgBarControlDesigner
    Inherits ControlDesigner
    Private _ProgBarPlus As ProgBarPlus = Nothing
    Private _Lists As DesignerActionListCollection


    Public Overrides Sub Initialize(ByVal component As IComponent)
        MyBase.Initialize(component)

        _ProgBarPlus = CType(component, ProgBarPlus)
    End Sub

#Region "OnPaintAdornments"

    Protected Overrides Sub OnPaintAdornments(ByVal e As PaintEventArgs)
        If _ProgBarPlus.ShowDesignBorder Then
            Dim g As Graphics = e.Graphics
            Dim myPen As Pen = New Pen(Color.Gray, 1)
            Dim rect As New Rectangle(0, 0, _ProgBarPlus.Width - 1, _ProgBarPlus.Height - 1)
            myPen.DashStyle = Drawing2D.DashStyle.Dash
            If _ProgBarPlus.CornersApply = ProgBarPlus.eCornersApply.Bar Then
                g.DrawRectangle(myPen, rect)
            Else
                Dim MyPath As New ProgBarPlus.ProgBarPath
                MyPath = _ProgBarPlus.GetPath(_ProgBarPlus.DisplayRectangle, False, 2)
                With MyPath
                    If _ProgBarPlus.Shape = ProgBarPlus.eShape.Text Then

                        If _ProgBarPlus.ShapeTextRotate <> ProgBarPlus.eRotateText.None Then
                            Dim mtrx As New Matrix
                            mtrx.Rotate(_ProgBarPlus.GetRotateAngle(_ProgBarPlus.ShapeTextRotate))
                            .Path.Transform(mtrx)
                        End If

                        g.Transform = _ProgBarPlus.TextMatrix(MyPath)
                        g.DrawPath(myPen, .Path)
                        g.ResetTransform()
                    Else
                        e.Graphics.DrawPath(myPen, .Path)

                    End If
                End With
            End If

            myPen.Dispose()
        End If
    End Sub
#End Region 'OnPaintAdornments

#Region "ActionLists"

    Public Overrides ReadOnly Property ActionLists() As System.ComponentModel.Design.DesignerActionListCollection
        Get
            If _Lists Is Nothing Then
                _Lists = New DesignerActionListCollection
                _Lists.Add(New ProgBarPlusActionList(Me.Component))
            End If
            Return _Lists
        End Get
    End Property

#End Region 'ActionLists

End Class

#Region "ProgBarPlusActionList"

Public Class ProgBarPlusActionList
    Inherits DesignerActionList

    Private _ProgBarPlusSelector As ProgBarPlus
    Private _DesignerService As DesignerActionUIService = Nothing

    Public Sub New(ByVal component As IComponent)
        MyBase.New(component)

        ' Save a reference to the control we are designing.
        _ProgBarPlusSelector = DirectCast(component, ProgBarPlus)

        ' Save a reference to the DesignerActionUIService
        _DesignerService = _
            CType(GetService(GetType(DesignerActionUIService)), _
            DesignerActionUIService)

        'Makes the Smart Tags open automatically 
        Me.AutoShow = True
    End Sub

#Region "Smart Tag Items"

#Region "Properties"

    Public Property FillDirection() As ProgBarPlus.eFillDirection
        Get
            Return _ProgBarPlusSelector.FillDirection
        End Get
        Set(ByVal value As ProgBarPlus.eFillDirection)
            SetControlProperty("FillDirection", value)
        End Set
    End Property

    Public Property Orientation() As ProgBarPlus.eOrientation
        Get
            Return _ProgBarPlusSelector.Orientation
        End Get
        Set(ByVal value As ProgBarPlus.eOrientation)
            SetControlProperty("Orientation", value)
        End Set
    End Property

    Public Property BarType() As ProgBarPlus.eBarType
        Get
            Return _ProgBarPlusSelector.BarType
        End Get
        Set(ByVal value As ProgBarPlus.eBarType)
            SetControlProperty("BarType", value)
            _DesignerService.ShowUI(_ProgBarPlusSelector)
        End Set
    End Property

    Public Property CylonMove() As Single
        Get
            Return _ProgBarPlusSelector.CylonMove
        End Get
        Set(ByVal value As Single)
            SetControlProperty("CylonMove", value)
        End Set
    End Property

    Public Property CylonInterval() As Short
        Get
            Return _ProgBarPlusSelector.CylonInterval
        End Get
        Set(ByVal value As Short)
            SetControlProperty("CylonInterval", value)
        End Set
    End Property

    Public Property CylonRun() As Boolean
        Get
            Return _ProgBarPlusSelector.CylonRun
        End Get
        Set(ByVal value As Boolean)
            SetControlProperty("CylonRun", value)
        End Set
    End Property

    Public Property BarLength() As ProgBarPlus.eBarLength
        Get
            Return _ProgBarPlusSelector.BarLength
        End Get
        Set(ByVal value As ProgBarPlus.eBarLength)
            SetControlProperty("BarLength", value)
            _DesignerService.ShowUI(_ProgBarPlusSelector)
        End Set
    End Property

    Public Property BarLengthValue() As Short
        Get
            Return _ProgBarPlusSelector.BarLengthValue
        End Get
        Set(ByVal value As Short)
            SetControlProperty("BarLengthValue", value)
        End Set
    End Property

    Public Property Shape() As ProgBarPlus.eShape
        Get
            Return _ProgBarPlusSelector.Shape
        End Get
        Set(ByVal value As ProgBarPlus.eShape)
            SetControlProperty("Shape", value)
            _DesignerService.ShowUI(_ProgBarPlusSelector)
        End Set
    End Property

    Public Property CornersApply() As ProgBarPlus.eCornersApply
        Get
            Return _ProgBarPlusSelector.CornersApply
        End Get
        Set(ByVal value As ProgBarPlus.eCornersApply)
            SetControlProperty("CornersApply", value)
        End Set
    End Property

    Public Property ShapeText() As String
        Get
            Return _ProgBarPlusSelector.ShapeText
        End Get
        Set(ByVal value As String)
            SetControlProperty("ShapeText", value)
        End Set
    End Property

    Public Property ShapeTextFont() As Font
        Get
            Return _ProgBarPlusSelector.ShapeTextFont
        End Get
        Set(ByVal value As Font)
            SetControlProperty("ShapeTextFont", value)
        End Set
    End Property

    Public Property ShapeTextRotate() As ProgBarPlus.eRotateText
        Get
            Return _ProgBarPlusSelector.ShapeTextRotate
        End Get
        Set(ByVal value As ProgBarPlus.eRotateText)
            SetControlProperty("ShapeTextRotate", value)
        End Set
    End Property

    Public Property BarStyleFill() As ProgBarPlus.eBarStyle
        Get
            Return _ProgBarPlusSelector.BarStyleFill
        End Get
        Set(ByVal value As ProgBarPlus.eBarStyle)
            SetControlProperty("BarStyleFill", value)
            _DesignerService.ShowUI(_ProgBarPlusSelector)
        End Set
    End Property

    Public Property BarBackColor() As Color
        Get
            Return _ProgBarPlusSelector.BarBackColor
        End Get
        Set(ByVal value As Color)
            SetControlProperty("BarBackColor", value)
        End Set
    End Property

    Public Property BarColorSolid() As Color
        Get
            Return _ProgBarPlusSelector.BarColorSolid
        End Get
        Set(ByVal value As Color)
            SetControlProperty("BarColorSolid", value)
            If _ProgBarPlusSelector.BarStyleFill = ProgBarPlus.eBarStyle.Hatch Then
                _DesignerService.Refresh(_ProgBarPlusSelector)
            End If
        End Set
    End Property

    Public Property BarColorSolidB() As Color
        Get
            Return _ProgBarPlusSelector.BarColorSolidB
        End Get
        Set(ByVal value As Color)
            SetControlProperty("BarColorSolidB", value)
            If _ProgBarPlusSelector.BarStyleFill = ProgBarPlus.eBarStyle.Hatch Then
                _DesignerService.Refresh(_ProgBarPlusSelector)
            End If
        End Set
    End Property

    <Editor(GetType(BlendTypeEditor), GetType(UITypeEditor))> _
    Public Property BarColorBlend() As cBlendItems
        Get
            Return _ProgBarPlusSelector.BarColorBlend
        End Get
        Set(ByVal value As cBlendItems)
            SetControlProperty("BarColorBlend", value)
        End Set
    End Property

    Public Property BarStyleLinear() As LinearGradientMode
        Get
            Return _ProgBarPlusSelector.BarStyleLinear
        End Get
        Set(ByVal value As LinearGradientMode)
            SetControlProperty("BarStyleLinear", value)
        End Set
    End Property

    <Editor(GetType(HatchStyleEditor), GetType(UITypeEditor))> _
     Public Property BarStyleHatch() As HatchStyle
        Get
            Return _ProgBarPlusSelector.BarStyleHatch
        End Get
        Set(ByVal value As HatchStyle)
            SetControlProperty("BarStyleHatch", value)
        End Set
    End Property

    Public Property BarStyleTexture() As Image
        Get
            Return _ProgBarPlusSelector.BarStyleTexture
        End Get
        Set(ByVal value As Image)
            SetControlProperty("BarStyleTexture", value)
        End Set
    End Property

    Public Property BarStyleWrapMode() As WrapMode
        Get
            Return _ProgBarPlusSelector.BarStyleWrapMode
        End Get
        Set(ByVal value As WrapMode)
            SetControlProperty("BarStyleWrapMode", value)
        End Set
    End Property

    <Editor(GetType(FocalTypeEditor), GetType(UITypeEditor))> _
        Public Property FocalPoints() As cFocalPoints
        Get
            Return _ProgBarPlusSelector.FocalPoints
        End Get
        Set(ByVal value As cFocalPoints)
            SetControlProperty("FocalPoints", value)
        End Set
    End Property

    Public Property Border() As Boolean
        Get
            Return _ProgBarPlusSelector.Border
        End Get
        Set(ByVal value As Boolean)
            SetControlProperty("Border", value)
            _DesignerService.ShowUI(_ProgBarPlusSelector)
        End Set
    End Property

    Public Property BorderColor() As Color
        Get
            Return _ProgBarPlusSelector.BorderColor
        End Get
        Set(ByVal value As Color)
            SetControlProperty("BorderColor", value)
        End Set
    End Property

    Public Property BorderWidth() As Integer
        Get
            Return _ProgBarPlusSelector.BorderWidth
        End Get
        Set(ByVal value As Integer)
            SetControlProperty("BorderWidth", value)
        End Set
    End Property

    Public Property TextShow() As ProgBarPlus.eTextShow
        Get
            Return _ProgBarPlusSelector.TextShow
        End Get
        Set(ByVal value As ProgBarPlus.eTextShow)
            SetControlProperty("TextShow", value)
            _DesignerService.Refresh(_ProgBarPlusSelector)
        End Set
    End Property

    Public Property ForeColor() As Color
        Get
            Return _ProgBarPlusSelector.ForeColor
        End Get
        Set(ByVal value As Color)
            SetControlProperty("ForeColor", value)
        End Set
    End Property

    Public Property TextValue() As String
        Get
            Return _ProgBarPlusSelector.TextValue
        End Get
        Set(ByVal value As String)
            SetControlProperty("TextValue", value)
        End Set
    End Property

    Public Property TextFormat() As String
        Get
            Return _ProgBarPlusSelector.TextFormat
        End Get
        Set(ByVal value As String)
            SetControlProperty("TextFormat", value)
        End Set
    End Property

    Public Property TextPlacement() As ProgBarPlus.eTextPlacement
        Get
            Return _ProgBarPlusSelector.TextPlacement
        End Get
        Set(ByVal value As ProgBarPlus.eTextPlacement)
            SetControlProperty("TextPlacement", value)
        End Set
    End Property

    Public Property TextAlignment() As StringAlignment
        Get
            Return _ProgBarPlusSelector.TextAlignment
        End Get
        Set(ByVal value As StringAlignment)
            SetControlProperty("TextAlignment", value)
        End Set
    End Property

    Public Property TextAlignmentVert() As StringAlignment
        Get
            Return _ProgBarPlusSelector.TextAlignmentVert
        End Get
        Set(ByVal value As StringAlignment)
            SetControlProperty("TextAlignmentVert", value)
        End Set
    End Property

    Public Property TextWrap() As Boolean
        Get
            Return _ProgBarPlusSelector.TextWrap
        End Get
        Set(ByVal value As Boolean)
            SetControlProperty("TextWrap", value)
        End Set
    End Property

    Public Property TextRotate() As ProgBarPlus.eRotateText
        Get
            Return _ProgBarPlusSelector.TextRotate
        End Get
        Set(ByVal value As ProgBarPlus.eRotateText)
            SetControlProperty("TextRotate", value)
        End Set
    End Property

    Public Property TextShadow() As Boolean
        Get
            Return _ProgBarPlusSelector.TextShadow
        End Get
        Set(ByVal value As Boolean)
            SetControlProperty("TextShadow", value)
            _DesignerService.ShowUI(_ProgBarPlusSelector)
        End Set
    End Property

    Public Property TextShadowColor() As Color
        Get
            Return _ProgBarPlusSelector.TextShadowColor
        End Get
        Set(ByVal value As Color)
            SetControlProperty("TextShadowColor", value)
        End Set
    End Property


    Public ReadOnly Property CurrProgBarPlus() As ProgBarPlus
        Get
            Return _ProgBarPlusSelector
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub AdjustCorners()

        'Create a new Corners Dialog and update the controls on the form
        Dim dlg As dlgCorners = New dlgCorners()

        Dim maxcorner As Integer
        Dim ratio As Single

        If _ProgBarPlusSelector.Width > _ProgBarPlusSelector.Height Then
            dlg.TheShape.Height = CInt(dlg.TheShape.Width * (_ProgBarPlusSelector.Height / _ProgBarPlusSelector.Width))
            dlg.TheShape.Top = CInt((dlg.panShapeHolder.Height - dlg.TheShape.Height) / 2)
            maxcorner = CInt(((dlg.TheShape.Height / 2) - (_ProgBarPlusSelector.BorderWidth) * 2))
            ratio = CSng(dlg.TheShape.Height / _ProgBarPlusSelector.Height)
        Else
            dlg.TheShape.Width = CInt(dlg.TheShape.Height * (_ProgBarPlusSelector.Width / _ProgBarPlusSelector.Height))
            dlg.TheShape.Left = CInt((dlg.panShapeHolder.Width - dlg.TheShape.Width) / 2)
            maxcorner = CInt(((dlg.TheShape.Width / 2) - (_ProgBarPlusSelector.BorderWidth) * 2))
            ratio = CSng(dlg.TheShape.Width / _ProgBarPlusSelector.Width)
        End If

        ' Set current Corners values
        dlg.tbarUpperLeft.Maximum = maxcorner
        dlg.tbarUpperRight.Maximum = maxcorner
        dlg.tbarLowerLeft.Maximum = maxcorner
        dlg.tbarLowerRight.Maximum = maxcorner
        dlg.tbarAll.Maximum = maxcorner
        dlg.tbarUpperLeft.TickFrequency = CInt(maxcorner / 2)
        dlg.tbarUpperRight.TickFrequency = CInt(maxcorner / 2)
        dlg.tbarLowerLeft.TickFrequency = CInt(maxcorner / 2)
        dlg.tbarLowerRight.TickFrequency = CInt(maxcorner / 2)
        dlg.tbarAll.TickFrequency = CInt(maxcorner / 2)
        If _ProgBarPlusSelector.Corners.All > -1 Then
            dlg.tbarAll.Value = CInt(Math.Min((_ProgBarPlusSelector.Corners.UpperLeft * ratio), maxcorner))
        End If
        dlg.tbarUpperLeft.Value = CInt(Math.Min((_ProgBarPlusSelector.Corners.UpperLeft * ratio), maxcorner))
        dlg.tbarUpperRight.Value = CInt(Math.Min((_ProgBarPlusSelector.Corners.UpperRight * ratio), maxcorner))
        dlg.tbarLowerLeft.Value = CInt(Math.Min((_ProgBarPlusSelector.Corners.LowerLeft * ratio), maxcorner))
        dlg.tbarLowerRight.Value = CInt(Math.Min((_ProgBarPlusSelector.Corners.LowerRight * ratio), maxcorner))

        dlg.TheShape.Shape = _ProgBarPlusSelector.Shape
        dlg.TheShape.BarStyleFill = _ProgBarPlusSelector.BarStyleFill
        dlg.TheShape.BarStyleLinear = _ProgBarPlusSelector.BarStyleLinear
        dlg.TheShape.BarLength = _ProgBarPlusSelector.BarLength
        dlg.TheShape.BarLengthValue = _ProgBarPlusSelector.BarLengthValue
        dlg.TheShape.BarBackColor = _ProgBarPlusSelector.BarBackColor
        dlg.TheShape.BarColorSolid = _ProgBarPlusSelector.BarColorSolid
        dlg.TheShape.FillDirection = _ProgBarPlusSelector.FillDirection
        dlg.TheShape.Orientation = _ProgBarPlusSelector.Orientation
        dlg.TheShape.CornersApply = _ProgBarPlusSelector.CornersApply
        dlg.TheShape.BarColorSolidB = _ProgBarPlusSelector.BarColorSolidB
        dlg.TheShape.Border = _ProgBarPlusSelector.Border
        dlg.TheShape.BorderWidth = _ProgBarPlusSelector.BorderWidth
        dlg.TheShape.BorderColor = _ProgBarPlusSelector.BorderColor
        dlg.TheShape.BarStyleHatch = _ProgBarPlusSelector.BarStyleHatch
        dlg.TheShape.BarColorBlend = New cBlendItems(_ProgBarPlusSelector.BarColorBlend.iColor, _ProgBarPlusSelector.BarColorBlend.iPoint)
        dlg.TheShape.Corners = New CornersProperty( _
                        CShort(_ProgBarPlusSelector.Corners.LowerLeft * ratio), _
                        CShort(_ProgBarPlusSelector.Corners.LowerRight * ratio), _
                        CShort(_ProgBarPlusSelector.Corners.UpperLeft * ratio), _
                        CShort(_ProgBarPlusSelector.Corners.UpperRight * ratio))
        dlg.TheShape.FocalPoints = _ProgBarPlusSelector.FocalPoints
        dlg.HSBarSample.Location = New Point(dlg.HSBarSample.Location.X, dlg.panShapeHolder.Location.Y + dlg.TheShape.Location.Y + dlg.TheShape.Height)

        ' Update new Corners values if OK button was pressed
        If dlg.ShowDialog() = DialogResult.OK Then
            Dim designerHost As IDesignerHost = CType(Me.Component.Site.GetService(GetType(IDesignerHost)), IDesignerHost)

            If designerHost IsNot Nothing Then
                Dim t As DesignerTransaction = designerHost.CreateTransaction()
                Try
                    SetControlProperty("Corners", New CornersProperty( _
                        CShort(dlg.TheShape.Corners.LowerLeft / ratio), _
                        CShort(dlg.TheShape.Corners.LowerRight / ratio), _
                        CShort(dlg.TheShape.Corners.UpperLeft / ratio), _
                        CShort(dlg.TheShape.Corners.UpperRight / ratio)))
                    t.Commit()
                Catch
                    t.Cancel()
                End Try
            End If
        End If
        _ProgBarPlusSelector.Refresh()

    End Sub

#End Region 'Methods

    ' Set a control property. This method makes Undo/Redo
    ' work properly and marks the form as modified in the IDE.
    Private Sub SetControlProperty(ByVal property_name As String, ByVal value As Object)
        TypeDescriptor.GetProperties(_ProgBarPlusSelector) _
            (property_name).SetValue(_ProgBarPlusSelector, value)
    End Sub

#End Region ' Smart Tag Items

    ' Return the smart tag action items.
    Public Overrides Function GetSortedActionItems() As System.ComponentModel.Design.DesignerActionItemCollection
        Dim items As New DesignerActionItemCollection()

        items.Add(New DesignerActionHeaderItem("Behavior"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "BarType", _
                "Bar Type", _
                "Behavior", _
                "Use Standard, CylonBar, or Glider Progress Bar"))
        If _ProgBarPlusSelector.BarType = ProgBarPlus.eBarType.Bar Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "BarLength", _
                    "Bar Length Type", _
                    "Behavior", _
                    "Use Fixed or Full Length Progress Bar"))
            If _ProgBarPlusSelector.BarLength = ProgBarPlus.eBarLength.Fixed Then
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarLengthValue", _
                        "Bar Length Value", _
                        "Behavior", _
                        "Length of the Fixed Progress Bar"))
            End If
            items.Add( _
                New DesignerActionPropertyItem( _
                    "FillDirection", _
                    "Progress Direction", _
                    "Behavior", _
                    "The ProgBarPlus of the Control"))

        Else
            If _ProgBarPlusSelector.BarType = ProgBarPlus.eBarType.CylonBar Then
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarLengthValue", _
                        "Bar Length Value", _
                        "Behavior", _
                        "Length of the Fixed Progress Bar"))
            End If
            items.Add( _
                New DesignerActionPropertyItem( _
                    "CylonRun", _
                    "Cylon On", _
                    "Behavior", _
                    "Start the Cylon Timer"))
            items.Add( _
                New DesignerActionPropertyItem( _
                    "CylonMove", _
                    "Move Distance", _
                    "Behavior", _
                    "How far the bar moves"))
            items.Add( _
                New DesignerActionPropertyItem( _
                    "CylonInterval", _
                    "Timer Tick", _
                    "Behavior", _
                    "How often the bar moves"))
        End If

        items.Add( _
            New DesignerActionPropertyItem( _
                "Orientation", _
                "Bar Orientation", _
                "Behavior", _
                "The ProgBarPlus of the Control"))

        items.Add(New DesignerActionHeaderItem("Color and Fill"))

        items.Add( _
            New DesignerActionPropertyItem( _
                "BarStyleFill", _
                "Bar Fill Type", _
                "Color and Fill", _
                "Fill Solid, Gradient, Hatch, or Texture"))

        Select Case _ProgBarPlusSelector.BarStyleFill
            Case ProgBarPlus.eBarStyle.Solid
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarColorSolid", _
                        "Primary Solid Color", _
                        "Color and Fill", _
                        "The Primary Color for Solid Fill"))
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarColorSolidB", _
                        "Secondary Solid Color", _
                        "Color and Fill", _
                        "The Secondary Color for Solid Fill"))

            Case ProgBarPlus.eBarStyle.Hatch
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarColorSolid", _
                        "Primary Solid Color", _
                        "Color and Fill", _
                        "The Primary Color for Hatch Fill"))
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarColorSolidB", _
                        "Secondary Solid Color", _
                        "Color and Fill", _
                        "The Secondary Color for Hatch Fill"))
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarStyleHatch", _
                        "Hatch Style", _
                        "Color and Fill", _
                        "The Hatch Style for Fill"))

            Case ProgBarPlus.eBarStyle.GradientLinear
                items.Add( _
                     New DesignerActionPropertyItem( _
                         "BarColorBlend", _
                         "Blend Colors", _
                         "Color and Fill", _
                         "Color and Position Arrays for Color Blend"))
                items.Add( _
                     New DesignerActionPropertyItem( _
                         "BarStyleLinear", _
                         "Linear Style", _
                         "Color and Fill", _
                         "Color and Position Arrays for Color Blend"))

            Case ProgBarPlus.eBarStyle.GradientPath
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarColorBlend", _
                        "Blend Colors", _
                        "Color and Fill", _
                        "Color and Position Arrays for Color Blend"))
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "FocalPoints", _
                        "FocalPoints", _
                        "Color and Fill", _
                        "The color of the ProgBarPlus's Border"))

            Case ProgBarPlus.eBarStyle.Texture
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarStyleTexture", _
                        "Texture Image", _
                        "Color and Fill", _
                        "The Image to fill with"))
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "BarStyleWrapMode", _
                        "Texture Wrap Mode", _
                        "Color and Fill", _
                        "The Wrap Mode for texture fills"))

        End Select
        items.Add( _
            New DesignerActionPropertyItem( _
                "BarBackColor", _
                "Background Color", _
                "Color and Fill", _
                "The ProgBarPlus of the Control"))

        items.Add(New DesignerActionHeaderItem("Border"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "Border", _
                "Show Border", _
                "Border", _
                "The show or not show the border"))
        If _ProgBarPlusSelector.Border Then
            items.Add( _
                New DesignerActionPropertyItem( _
                    "BorderColor", _
                    "Border Color", _
                    "Border", _
                    "The color of the ProgBarPlus's Border"))
            items.Add( _
                New DesignerActionPropertyItem( _
                    "BorderWidth", _
                    "Border Width", _
                    "Border", _
                    "The width of the ProgBarPlus's Border"))
        End If

        items.Add(New DesignerActionHeaderItem("Shape"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "Shape", _
                "Shape", _
                "Shape", _
                "The Shape of the ProgBarPlus"))

        Select Case _ProgBarPlusSelector.Shape
            Case ProgBarPlus.eShape.Rectangle
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "CornersApply", _
                        "Apply Corners", _
                        "Shape", _
                        "Apply the Corners to what parts of the ProgBarPlus"))
                items.Add( _
                    New DesignerActionMethodItem( _
                          Me, _
                        "AdjustCorners", _
                        "Adjust Corners ", _
                        "Shape", _
                        "Adjust Corners", _
                        True))
            Case ProgBarPlus.eShape.Text
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "ShapeTextFont", _
                        "Font for Shape", _
                        "Shape", _
                        "The Font for Shape of the ProgBarPlus"))
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "ShapeText", _
                        "Text for Shape", _
                        "Shape", _
                        "The Text for Shape of the ProgBarPlus"))
                items.Add( _
                    New DesignerActionPropertyItem( _
                        "ShapeTextRotate", _
                        "Rotate Text", _
                        "Shape", _
                        "The Font for Shape of the ProgBarPlus"))
        End Select

        items.Add(New DesignerActionHeaderItem("Text"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "TextShow", _
                "Show Text", _
                "Text", _
                "The Show or not Show the Text on the ProgBarPlus"))
        If _ProgBarPlusSelector.TextShow <> ProgBarPlus.eTextShow.None Then

            items.Add( _
                    New DesignerActionPropertyItem( _
                        "ForeColor", _
                        "Color for Text", _
                        "Text", _
                        "The Color for Text on the Bar"))

            If _ProgBarPlusSelector.TextShow = ProgBarPlus.eTextShow.FormatStrText Or _
                _ProgBarPlusSelector.TextShow = ProgBarPlus.eTextShow.FormatStrTextPerc Or _
                _ProgBarPlusSelector.TextShow = ProgBarPlus.eTextShow.TextOnly Then
                items.Add( _
                        New DesignerActionPropertyItem( _
                            "TextValue", _
                            "Text for Shape", _
                            "Text", _
                            "The Text for Shape of the ProgBarPlus"))
            End If

            If _ProgBarPlusSelector.TextShow = ProgBarPlus.eTextShow.FormatStrPercent Or _
                _ProgBarPlusSelector.TextShow = ProgBarPlus.eTextShow.FormatStrText Or _
                _ProgBarPlusSelector.TextShow = ProgBarPlus.eTextShow.FormatStrTextPerc Then
                items.Add( _
                        New DesignerActionPropertyItem( _
                            "TextFormat", _
                            "Format String", _
                            "Text", _
                            "The Text for Shape of the ProgBarPlus"))

            End If

            items.Add( _
                    New DesignerActionPropertyItem( _
                        "TextPlacement", _
                        "Placement on Bar", _
                        "Text", _
                        "The Text for Shape of the ProgBarPlus"))
            items.Add( _
                    New DesignerActionPropertyItem( _
                        "TextAlignment", _
                        "Horiz Alignment", _
                        "Text", _
                        "The Text for Shape of the ProgBarPlus"))
            items.Add( _
                    New DesignerActionPropertyItem( _
                        "TextAlignmentVert", _
                        "Vert Alignment", _
                        "Text", _
                        "The Text for Shape of the ProgBarPlus"))
            items.Add( _
                    New DesignerActionPropertyItem( _
                        "TextShadow", _
                        "Text Shadow", _
                        "Text", _
                        "The Text for Shape of the ProgBarPlus"))
            If _ProgBarPlusSelector.TextShadow Then
                items.Add( _
                        New DesignerActionPropertyItem( _
                            "TextShadowColor", _
                            "Text Shadow", _
                            "Text", _
                            "The Text for Shape of the ProgBarPlus"))
            End If
            items.Add( _
                    New DesignerActionPropertyItem( _
                        "TextRotate", _
                        "Rotate", _
                        "Text", _
                        "The Text for Shape of the ProgBarPlus"))
            items.Add( _
                    New DesignerActionPropertyItem( _
                        "TextWrap", _
                        "Wrap Text", _
                        "Text", _
                        "The Text for Shape of the ProgBarPlus"))

        End If


        'items.Add(New DesignerActionHeaderItem("Information"))
        'Add Text Item - I gave it an empty Category to make 
        'it appear at the end with no Header
        items.Add( _
            New DesignerActionTextItem( _
                Space(20) & "Smart Tags Are Cool" & vbCr & Space(28) & "GonzoDiver", _
                ""))

        'Another Text item but with a header
        'Dim txt As String = "Width=" & _ProgBarPlusSelector.Width & _
        ' " Height=" & _ProgBarPlusSelector.Height
        'items.Add( _
        '    New DesignerActionTextItem( _
        '        txt, "Information"))

        Return items
    End Function

End Class

#End Region 'ProgBarPlusActionList

#End Region 'Control Designer

