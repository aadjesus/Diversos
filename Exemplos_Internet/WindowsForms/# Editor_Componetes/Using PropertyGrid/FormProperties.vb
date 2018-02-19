Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Globalization
<DefaultPropertyAttribute("Name")> _
Public Class FormProperties

    Private _windowSize As size = New size(100, 100)
    Private _windowFont As Font = New Font("Arial", 8, FontStyle.Regular)
    Private _toolbarColor As Color = SystemColors.Control
    Private _DataBindings_Advanced
    Private _DataBindings_Tag As String = ""
    Private _DataBindings_Text As String = ""
    Private _DynamicProperties_Advanced
    Private _Name As String
    Private _DrawGrid As Boolean
    Private _GridSize As Point
    Private _Locked As Boolean
    Private _SnapToGrid As Boolean
    Private _CausesValidation As Boolean
    Private _AutoScale As Boolean
    Private _AutoScroll As Boolean
    Private _AutoScrollMargin As System.Drawing.Size
    Private _AutoScrollMinSize As System.Drawing.Size
    Private _DockPadding As System.Windows.Forms.DockStyle
    Private _Location As Point
    Private _MaximumSize As System.Drawing.Size
    Private _MaximizeBox As Boolean
    Private _MinimizeBox As Boolean
    Private _MinimumSize As System.Drawing.Size
    Private _size As System.Drawing.Size
    Private _StartPosition As System.Windows.Forms.FormStartPosition
    Private _WindowState As System.Windows.Forms.FormWindowState
    Private _AcceptButton
    Private _CancelButton
    Private _KeyPreview As Boolean
    Private _Language
    Private _Localizable As Boolean
    Private _ControlBox As Boolean
    Private _HelpButton As Boolean
    Private _Icon As System.Drawing.Icon
    Private _IsMdiContainer As Boolean
    Private _Menu As System.Windows.Forms.MainMenu
    Private _Opacity As Short
    Private _ShowInTaskbar As Boolean
    Private _SizeGripStyle As System.Windows.Forms.SizeGripStyle
    Private _TopMost As Boolean
    Private _TransparencyKey As System.Drawing.Color
    Private _BackColor As Color
    Private _BackGroundImage As System.Drawing.Image
    Private _Cursor As System.Windows.Forms.Cursor
    Private _ForeColor As Color
    Private _Font As System.Drawing.Font
    Private _FormBorderStyle As System.Windows.Forms.FormBorderStyle
    Private _RightToLeft As System.Windows.Forms.RightToLeft
    Private _Text As String
    Private _Person As New Person()
    '''

#Region "Data Bindings"

    <CategoryAttribute("Data Bindings"), DescriptionAttribute("Advanced bindings allow you to bind properties of the control.")> _
    Public Property Advanced()
        Get
            Return _DataBindings_Advanced
        End Get
        Set(ByVal Value)
            _DataBindings_Advanced = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Data Bindings"), DescriptionAttribute("")> _
    Public Property DataBindings_Tag() As String
        Get
            Return _DataBindings_Tag
        End Get
        Set(ByVal Value As String)
            _DataBindings_Tag = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Data Bindings"), DescriptionAttribute("")> _
    Public Property DataBindings_Text() As String
        Get
            Return _DataBindings_Text
        End Get
        Set(ByVal Value As String)
            _DataBindings_Text = Value
        End Set
    End Property
    '''
#End Region
    '''
#Region "Dynamic Properties"
    <CategoryAttribute("DynamicProperties"), DescriptionAttribute("")> _
       Public Property DynamicProperties_Advanced()
        Get
            Return _DynamicProperties_Advanced
        End Get
        Set(ByVal Value)
            _DynamicProperties_Advanced = Value
        End Set
    End Property
    '''
#End Region
    '''
#Region "Design"
    <CategoryAttribute("Design"), DescriptionAttribute("")> _
       Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Design"), DescriptionAttribute("")> _
       Public Property DrawGrid() As Boolean
        Get
            Return _DrawGrid
        End Get
        Set(ByVal Value As Boolean)
            _DrawGrid = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Design"), DescriptionAttribute("")> _
       Public Property GridSize() As Point
        Get
            Return _GridSize
        End Get
        Set(ByVal Value As Point)
            _GridSize = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Design"), DescriptionAttribute("")> _
           Public Property Locked() As Boolean
        Get
            Return _Locked
        End Get
        Set(ByVal Value As Boolean)
            _Locked = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Design"), DescriptionAttribute("")> _
       Public Property SnapToGrid() As Boolean
        Get
            Return _SnapToGrid
        End Get
        Set(ByVal Value As Boolean)
            _SnapToGrid = Value
        End Set
    End Property
    '''
#End Region
    '''
#Region "Focus"
    '''
    <CategoryAttribute("Focus"), DescriptionAttribute("Indicates whether this control causesand raises validation events")> _
       Public Property CausesValidation() As Boolean
        Get
            Return _CausesValidation
        End Get
        Set(ByVal Value As Boolean)
            _CausesValidation = Value
        End Set
    End Property
#End Region
#Region "Layout"
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("If set to true,the form will automatically scale with the screen font")> _
       Public Property AutoScale() As Boolean
        Get
            Return _AutoScale
        End Get
        Set(ByVal Value As Boolean)
            _AutoScale = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("Determines whether scrollbars will automatically appear if controls are placed outside the form's client area")> _
   Public Property AutoScroll() As Boolean
        Get
            Return _AutoScroll
        End Get
        Set(ByVal Value As Boolean)
            _AutoScroll = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("Margin around controls during auto scroll")> _
       Public Property AutoScrollMargin() As size
        Get
            Return _AutoScrollMargin
        End Get
        Set(ByVal Value As size)
            _AutoScrollMargin = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("The minimum logical size for the auto scroll region")> _
       Public Property AutoScrollMinSize() As size
        Get
            Return _AutoScrollMinSize
        End Get
        Set(ByVal Value As size)
            _AutoScrollMinSize = Value
        End Set
    End Property
    '''
    <Editor(GetType(DockingTypeEditor), GetType(System.Drawing.Design.UITypeEditor))> _
          Public Property DockPadding() As System.Windows.Forms.DockStyle
        Get
            Return _DockPadding
        End Get
        Set(ByVal Value As System.Windows.Forms.DockStyle)
            _DockPadding = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("The position of the top-left corner of the control with respect to its container")> _
          Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal Value As Point)
            _Location = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("Determines whether a form has a maximize box in the upper-right corner of its caption bar")> _
      Public Property MaximumBox() As Boolean
        Get
            Return _MaximizeBox
        End Get
        Set(ByVal Value As Boolean)
            _MaximizeBox = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("The maximum size the form can be resized to")> _
      Public Property MaximumSize() As size
        Get
            Return _MaximumSize
        End Get
        Set(ByVal Value As size)
            _MaximumSize = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("Determines whether a form has a minimize box in the upper-right corner of its caption bar")> _
      Public Property MinimizeBox() As Boolean
        Get
            Return _MinimizeBox
        End Get
        Set(ByVal Value As Boolean)
            _MinimizeBox = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("The minimum size the form can be resized to")> _
      Public Property MinimumSize() As size
        Get
            Return _MinimumSize
        End Get
        Set(ByVal Value As size)
            _MinimumSize = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("The size of the controls in pixels")> _
          Public Property size() As System.Drawing.Size
        Get
            Return _size
        End Get
        Set(ByVal Value As System.Drawing.Size)
            _size = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("Determines the position of a form when it first appears")> _
      Public Property StartPosition() As System.Windows.Forms.FormStartPosition
        Get
            Return _StartPosition
        End Get
        Set(ByVal Value As System.Windows.Forms.FormStartPosition)
            _StartPosition = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Layout"), DescriptionAttribute("Determines the initial visual state of the form")> _
          Public Property WindowState() As System.Windows.Forms.FormWindowState
        Get
            Return _WindowState
        End Get
        Set(ByVal Value As System.Windows.Forms.FormWindowState)
            _WindowState = Value
        End Set
    End Property
    '''
#End Region
    '''
#Region "Misc"
    '''
    <CategoryAttribute("Misc"), DescriptionAttribute("The accept button of the form. If this is set, the button is 'clicked' when ever the user presses 'ENTER' key")> _
          Public Property AcceptButton()
        Get
            Return _AcceptButton
        End Get
        Set(ByVal Value)
            _AcceptButton = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Misc"), DescriptionAttribute("The cancel button of the form. If this is set, the button is 'clicked' when ever the user presses 'ESC' key")> _
Public Property CancelButton()
        Get
            Return (_CancelButton)
        End Get
        Set(ByVal Value)
            _CancelButton = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Misc"), DescriptionAttribute("Determines whether keyboard events for controls on the form are registered with the form")> _
Public Property KeyPreview() As Boolean
        Get
            Return _KeyPreview
        End Get
        Set(ByVal Value As Boolean)
            _KeyPreview = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Misc"), DescriptionAttribute("Indicates the current localizable language")> _
 Public Property Language()
        Get
            Return (_Language)
        End Get
        Set(ByVal Value)
            _Language = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Misc"), DescriptionAttribute("Determines if localize code will be generated for this object")> _
Public Property Localizable() As Boolean
        Get
            Return _Localizable
        End Get
        Set(ByVal value As Boolean)
            _Localizable = value
        End Set
    End Property
#End Region
    '''
#Region "Window Style"
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("Determines whether a form has a control/system menu box")> _
  Public Property ControlBox() As Boolean
        Get
            Return _ControlBox
        End Get
        Set(ByVal value As Boolean)
            _ControlBox = value
        End Set
    End Property
    ''''
    <CategoryAttribute("Window Style"), DescriptionAttribute("Determines whether a form has a help button on the caption bar.")> _
     Public Property HelpButton() As Boolean
        Get
            Return _HelpButton
        End Get
        Set(ByVal value As Boolean)
            _HelpButton = value
        End Set
    End Property
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("Indicates the icon for the form. This is displayed in the form's system menu box and when the form is minimized")> _
    Public Property Icon() As System.Drawing.Icon
        Get
            Return _Icon
        End Get
        Set(ByVal value As System.Drawing.Icon)
            Icon = value
        End Set
    End Property
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("Determines whether a form is an MDI container.")> _
    Public Property IsMdiContainer() As Boolean
        Get
            Return _IsMdiContainer
        End Get
        Set(ByVal value As Boolean)
            _IsMdiContainer = value
        End Set
    End Property
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("")> _
 Public Property MaximizeBox() As Boolean
        Get

        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("The main Menu of the form. This should be set to component of type Mainmenu")> _
    Public Property Menu() As System.Windows.Forms.MainMenu
        Get
            Return _Menu
        End Get
        Set(ByVal value As System.Windows.Forms.MainMenu)
            _Menu = value
        End Set
    End Property
    '''

    <CategoryAttribute("Window Style"), DescriptionAttribute("Determines how opaque or transparent the form is; )% is transparent 100% is opaque")> _
    Public Property Opacity() As Short
        Get
            Return _Opacity
        End Get
        Set(ByVal value As Short)
            _Opacity = value
        End Set
    End Property
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("Determines whether the form appears in the windows taskbar")> _
    Public Property ShowInTaskbar() As Boolean
        Get
            Return _ShowInTaskbar
        End Get
        Set(ByVal value As Boolean)
            _ShowInTaskbar = value
        End Set
    End Property
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("Determines when the sizegrip will be displayed for the form")> _
      Public Property SizeGripStyle() As System.Windows.Forms.SizeGripStyle
        Get
            Return _SizeGripStyle
        End Get
        Set(ByVal value As System.Windows.Forms.SizeGripStyle)
            _SizeGripStyle = value
        End Set
    End Property
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("Determines whether the form is above all other non-topmost forms, even when deactivated.")> _
    Public Property TopMost() As Boolean
        Get
            Return _TopMost
        End Get
        Set(ByVal value As Boolean)
            _TopMost = value
        End Set
    End Property
    '''
    <CategoryAttribute("Window Style"), DescriptionAttribute("A color which will appear transparent when painted on the form.")> _
    Public Property TransparencyKey() As System.Drawing.Color
        Get
            Return _TransparencyKey
        End Get
        Set(ByVal value As System.Drawing.Color)
            _TransparencyKey = value
        End Set
    End Property
    '''
#End Region
#Region "Appearence"
    <CategoryAttribute("Appearence"), DescriptionAttribute("The background color used to display text and graphics in the control.")> _
        Public Property BackColor() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal Value As Color)
            _BackColor = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Appearence"), DescriptionAttribute("The background image used for the control.")> _
           Public Property BackGroundImage() As System.Drawing.Image
        Get
            Return _BackGroundImage
        End Get
        Set(ByVal Value As System.Drawing.Image)
            _BackGroundImage = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Appearence"), DescriptionAttribute("The cursor that appears when the mouse passes over the control.")> _
       Public Property Cursor() As System.Windows.Forms.Cursor
        Get
            Return _Cursor
        End Get
        Set(ByVal Value As System.Windows.Forms.Cursor)
            _Cursor = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Appearence"), DescriptionAttribute("The foreground color used to display the text and graphics in the control.")> _
       Public Property ForeColor() As Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal Value As Color)
            _ForeColor = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Appearence"), DescriptionAttribute("The font used to display the text in the control")> _
       Public Property Font() As System.Drawing.Font
        Get
            Return _Font
        End Get
        Set(ByVal Value As System.Drawing.Font)
            _Font = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Appearence"), DescriptionAttribute("Controls the appearance of the border for the form. This will also affect how the caption bar will be displayed, and what buttons are allowed to appear on it.")> _
       Public Property FormBorderStyle() As System.Windows.Forms.FormBorderStyle
        Get
            Return _FormBorderStyle
        End Get
        Set(ByVal Value As System.Windows.Forms.FormBorderStyle)
            _FormBorderStyle = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Appearence"), DescriptionAttribute("Indicates whether the control should draw right-to-left for RTL Languages.")> _
       Public Property RightToLeft() As System.Windows.Forms.RightToLeft
        Get
            Return _RightToLeft
        End Get
        Set(ByVal Value As System.Windows.Forms.RightToLeft)
            _RightToLeft = Value
        End Set
    End Property
    '''
    <CategoryAttribute("Appearence"), DescriptionAttribute("The text contained in the control")> _
       Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal Value As String)
            _Text = Value
        End Set
    End Property
    '''
#End Region
End Class
'''
