Imports System.ComponentModel

Public Class XTextBoxA
	Inherits TextBox

	Private m_myProperty As String = ""

	'This property uses our custom UITypeEditor: myListBoxPropertyEditor
	<DefaultValue(""), EditorAttribute(GetType(myListBoxPropertyEditor), GetType(System.Drawing.Design.UITypeEditor))> _
	Public Property myProperty() As String
		Get
			Return m_myProperty
		End Get
		Set(ByVal value As String)
			m_myProperty = value
		End Set
	End Property

End Class



' /////////////////////////////////////////////////////////////////////////
'   myListBoxPropertyEditor => using a ListBox as EditControl
' /////////////////////////////////////////////////////////////////////////
'
Public Class myListBoxPropertyEditor
	Inherits PropertyEditorBase

	Private WithEvents myListBox As New ListBox	'this is the control to be used in design time DropDown editor

	Protected Overrides Function GetEditControl(ByVal PropertyName As String, ByVal CurrentValue As Object) As System.Windows.Forms.Control
		Dim s As String
		myListBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		'Creating ListBox items... 
		'Note that as this is executed in design mode, performance is not important and there is no need to cache these items if they can change each time.
		myListBox.Items.Clear()	'clear previous items if any
		Dim AllDrives As System.IO.DriveInfo() = System.IO.DriveInfo.GetDrives() 'get all drives
		For Each d As System.IO.DriveInfo In AllDrives
			s = d.Name.Substring(0, 2)
			If d.DriveType = System.IO.DriveType.Fixed Then	'Because the info of Removable or CD drives cannot be retrived quickly, we get them only for local hard disk drives
				s += " (" & d.VolumeLabel & ": " & (d.AvailableFreeSpace / &H40000000&).ToString("0.0") & " GB)"
			End If
			myListBox.Items.Add(s)
		Next
		myListBox.SelectedIndex = myListBox.FindString(CurrentValue) 'Select current item based on CurrentValue of the property
		myListBox.Height = myListBox.PreferredHeight
		Return myListBox
	End Function

	Protected Overrides Function GetEditedValue(ByVal EditControl As System.Windows.Forms.Control, ByVal PropertyName As String, ByVal OldValue As Object) As Object
		Return myListBox.Text.Substring(0, 2) 'return new value for property
	End Function

	Private Sub myTreeView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles myListBox.Click
		Me.CloseDropDownWindow() 'when user clicks on an item, the edit process is done.
	End Sub

End Class


