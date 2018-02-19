' ==============================================================================
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'
' © 2003 LaMarvin. All Rights Reserved.
'
' FMI: http://www.vbinfozine.com/a_default.shtml
' ==============================================================================

Imports System.Drawing
Imports System.Windows.Forms.Design
Imports System.ComponentModel

Namespace LaMarvin.Windows.Forms

  <DefaultProperty("Color"), DefaultEvent("ColorChanged")> _
  Public Class ColorPicker
    Inherits Control

    ' The CheckBox which is used to render the required button-like appearance
    ' of the control.
    Private WithEvents _CheckBox As CheckBox

    ' Should the control display the color's name?
    Private _TextDisplayed As Boolean = True

    ' The IWindowsFormsEditorService implementation - the meat of this code;-)
    Private _EditorService As EditorService

    ' The event is raised when the Color property changes.
    Public Event ColorChanged As EventHandler

    Private Const DefaultColorName As String = "Black"

    Public Sub New(ByVal c As Color)
      MyBase.New()

      ' Init the CheckBox to have the correct button-like appearance.
      _CheckBox = New CheckBox
      _CheckBox.Appearance = Appearance.Button
      _CheckBox.Dock = DockStyle.Fill
      _CheckBox.TextAlign = ContentAlignment.MiddleCenter
      Me.SetColor(c)
      Me.Controls.Add(_CheckBox)

      _EditorService = New EditorService(Me)
    End Sub

    Public Sub New()
      Me.New(System.Drawing.Color.FromName(DefaultColorName))
    End Sub


    <Description("The currently selected color."), Category("Appearance"), _
    DefaultValue(GetType(Color), DefaultColorName)> _
    Public Property Color() As Color
      Get
        Return _CheckBox.BackColor
      End Get
      Set(ByVal Value As Color)
        Me.SetColor(Value)
        RaiseEvent ColorChanged(Me, EventArgs.Empty)
      End Set
    End Property


    <Description("True meanse the control displays the currently selected color's name, False otherwise."), _
    Category("Appearance"), DefaultValue(True)> _
    Public Property TextDisplayed() As Boolean
      Get
        Return _TextDisplayed
      End Get
      Set(ByVal Value As Boolean)
        _TextDisplayed = Value
        Me.SetColor(Me.Color)
      End Set
    End Property


    ' Sets the associated CheckBox color and Text according to the TextDisplayed property value.
    Private Sub SetColor(ByVal c As Color)
      _CheckBox.BackColor = c
      _CheckBox.ForeColor = Me.GetInvertedColor(c)
      If _TextDisplayed Then
        _CheckBox.Text = c.Name
      Else
        _CheckBox.Text = String.Empty
      End If
    End Sub


    ' Primitive color inversion.
    Private Function GetInvertedColor(ByVal c As Color) As Color
      If (CInt(c.R) + CInt(c.G) + CInt(c.B)) > ((255I * 3I) \ 2I) Then
        Return Color.Black
      Else
        Return Color.White
      End If
    End Function


    ' If the associated CheckBox is checked, the drop-down UI is displayed.
    ' Otherwise it is closed.
    Private Sub OnCheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _CheckBox.CheckStateChanged
      If _CheckBox.CheckState = CheckState.Checked Then
        Me.ShowDropDown()
      Else
        Me.CloseDropDown()
      End If
    End Sub


    Private Sub ShowDropDown()
      Try
        ' This is the Color type editor - it displays the drop-down UI calling
        ' our IWindowsFormsEditorService implementation.
        Dim Editor As New System.Drawing.Design.ColorEditor

        ' Display the UI.
        Dim C As Color = Me.Color
        Dim NewValue As Object = Editor.EditValue(_EditorService, C)

        ' If the user didn't cancel the selection, remember the new color.
        If (Not NewValue Is Nothing) AndAlso (Not _EditorService.Canceled) Then
          Me.Color = CType(NewValue, Color)
        End If

        ' Finally, "pop-up" the associated CheckBox.
        _CheckBox.CheckState = CheckState.Unchecked

      Catch ex As Exception
        Trace.WriteLine(ex.ToString())
      End Try
    End Sub


    Private Sub CloseDropDown()
      _EditorService.CloseDropDown()
    End Sub


    ' This is a simple Form descendant that hosts the drop-down control provided
    ' by the ColorEditor class (in the call to DropDownControl).
    Private Class DropDownForm
      Inherits Form

      Private _Canceled As Boolean ' did the user cancel the color selection?
      Private _CloseDropDownCalled As Boolean ' was the form closed by calling the CloseDropDown method?

      Public Sub New()
        MyBase.New()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.ShowInTaskbar = False
        Me.KeyPreview = True  ' to catch the ESC key
        Me.StartPosition = FormStartPosition.Manual

        ' The ColorUI control is hosted by a Panel, which provides the simple border frame
        ' not available for Forms.
        Dim P As New Panel
        P.BorderStyle = BorderStyle.FixedSingle
        P.Dock = DockStyle.Fill
        Me.Controls.Add(P)
      End Sub


      Public Sub SetControl(ByVal ctl As Control)
        DirectCast(Me.Controls(0), Panel).Controls.Add(ctl)
      End Sub


      Public ReadOnly Property Canceled() As Boolean
        Get
          Return _Canceled
        End Get
      End Property


      Public Sub CloseDropDown()
        _CloseDropDownCalled = True
        Me.Hide()
      End Sub


      Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        If (e.Modifiers = 0) AndAlso (e.KeyCode = Keys.Escape) Then
          Me.Hide()
        End If
      End Sub


      Protected Overrides Sub OnDeactivate(ByVal e As System.EventArgs)
        ' We set the Owner to Nothing BEFORE calling the base class.
        ' If we wouldn't do it, the Picker form would lose focus after 
        ' the dropdown is closed.
        Me.Owner = Nothing

        MyBase.OnDeactivate(e)

        ' If the form was closed by any other means as the CloseDropDown call, it is because
        ' the user clicked outside the form, or pressed the ESC key.
        If Not _CloseDropDownCalled Then
          _Canceled = True
        End If
        Me.Hide()
      End Sub

    End Class


    ' This class actually hosts the ColorEditor.ColorUI by implementing the 
    ' IWindowsFormsEditorService.
    Private Class EditorService
      Implements IWindowsFormsEditorService
      Implements IServiceProvider

      ' The associated color picker control.
      Private _Picker As ColorPicker

      ' Reference to the drop down, which hosts the ColorUI control.
      Private _DropDownHolder As DropDownForm

      ' Cached _DropDownHolder.Canceled flag in order to allow it to be inspected
      ' by the owning ColorPicker control.
      Private _Canceled As Boolean


      Public Sub New(ByVal owner As ColorPicker)
        _Picker = owner
      End Sub


      Public ReadOnly Property Canceled() As Boolean
        Get
          Return _Canceled
        End Get
      End Property


      Public Sub CloseDropDown() Implements IWindowsFormsEditorService.CloseDropDown
        If Not _DropDownHolder Is Nothing Then
          _DropDownHolder.CloseDropDown()
        End If
      End Sub


      Public Sub DropDownControl(ByVal control As Control) Implements IWindowsFormsEditorService.DropDownControl
        _Canceled = False

        ' Initialize the hosting form for the control.
        _DropDownHolder = New DropDownForm
        _DropDownHolder.Bounds = control.Bounds
        _DropDownHolder.SetControl(control)

        ' Lookup a parent form for the Picker control and make the dropdown form to be owned
        ' by it. This prevents to show the dropdown form icon when the user presses the At+Tab system 
        ' key while the dropdown form is displayed.
        Dim PickerForm As control = Me.GetParentForm(_Picker)
        If (Not PickerForm Is Nothing) AndAlso (TypeOf PickerForm Is Form) Then
          _DropDownHolder.Owner = DirectCast(PickerForm, Form)
        End If

        ' Ensure the whole drop-down UI is displayed on the screen and show it.
        Me.PositionDropDownHolder()
        _DropDownHolder.Show() ' ShowDialog would disable clicking outside the dropdown area!

        ' Wait for the user to select a new color (in which case the ColorUI calls our CloseDropDown
        ' method) or cancel the selection (no CloseDropDown called, the Cancel flag is set to True).
        Me.DoModalLoop()

        ' Remember the cancel flag and get rid of the drop down form.
        _Canceled = _DropDownHolder.Canceled
        _DropDownHolder.Dispose()
        _DropDownHolder = Nothing
      End Sub


      Public Function ShowDialog(ByVal dialog As Form) As DialogResult Implements IWindowsFormsEditorService.ShowDialog
        Throw New NotSupportedException
      End Function


      Public Function GetService(ByVal serviceType As System.Type) As Object Implements System.IServiceProvider.GetService
        If serviceType.Equals(GetType(IWindowsFormsEditorService)) Then
          Return Me
        End If
      End Function


      Private Sub DoModalLoop()
        Debug.Assert(Not _DropDownHolder Is Nothing)
        Do While _DropDownHolder.Visible
          Application.DoEvents()
          ' The sollowing is the undocumented User32 call. For more information
          ' see the accompanying article at http://www.vbinfozine.com/a_colorpicker.shtml
          Me.MsgWaitForMultipleObjects(1, IntPtr.Zero, 1, 5, 255)
        Loop
      End Sub


      ' Don't forget (as I did:-) to declare the DllImport methods as Shared!
      ' Otherwise you'll get an exception *at runtime*!
      <System.Runtime.InteropServices.DllImport("User32", SetLastError:=True)> _
      Private Shared Function MsgWaitForMultipleObjects( _
        ByVal nCount As Int32, _
        ByVal pHandles As IntPtr, _
        ByVal bWaitAll As Int16, _
        ByVal dwMilliseconds As Int32, _
        ByVal dwWakeMask As Int32) As Int32
      End Function


      Private Sub PositionDropDownHolder()
        ' Convert _Picker location to screen coordinates.
        Dim Loc As Point = _Picker.Parent.PointToScreen(_Picker.Location)

        Dim ScreenRect As Rectangle = Screen.PrimaryScreen.WorkingArea

        ' Position the dropdown X coordinate in order to be displayed in its entirety.
        If Loc.X < ScreenRect.X Then
          Loc.X = ScreenRect.X
        ElseIf (Loc.X + _DropDownHolder.Width) > ScreenRect.Right Then
          Loc.X = ScreenRect.Right - _DropDownHolder.Width
        End If

        ' Do the same for the Y coordinate.
        If (Loc.Y + _Picker.Height + _DropDownHolder.Height) > ScreenRect.Bottom Then
          Loc.Offset(0, -_DropDownHolder.Height)  ' dropdown will be above the picker control
        Else
          Loc.Offset(0, _Picker.Height) ' dropdown will be below the picker
        End If

        _DropDownHolder.Location = Loc
      End Sub


      Private Function GetParentForm(ByVal ctl As Control) As Control
        Do
          If ctl.Parent Is Nothing Then
            Return ctl
          Else
            ctl = ctl.Parent
          End If
        Loop
      End Function

    End Class

    ' No need to display ForeColor and BackColor and Text in the property browser:

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Overrides Property ForeColor() As System.Drawing.Color
      Get
        Return MyBase.ForeColor
      End Get
      Set(ByVal Value As System.Drawing.Color)
        MyBase.ForeColor = Value
      End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Overrides Property BackColor() As System.Drawing.Color
      Get
        Return MyBase.BackColor
      End Get
      Set(ByVal Value As System.Drawing.Color)
        MyBase.BackColor = Value
      End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Overrides Property Text() As String
      Get
        Return MyBase.Text
      End Get
      Set(ByVal Value As String)
        MyBase.Text = Value
      End Set
    End Property
  End Class

End Namespace
