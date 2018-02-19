Imports System.ComponentModel

Public Class XTextBoxB
	Inherits TextBox

	Private m_myProperty As String = ""

	'this property uses our custom UITypeEditor: myTreeViewPropertyEditor
	<DefaultValue(""), EditorAttribute(GetType(myTreeViewPropertyEditor), GetType(System.Drawing.Design.UITypeEditor))> _
	Public Property myProperty() As String
		Get
			Return m_myProperty
		End Get
		Set(ByVal value As String)
			m_myProperty = value
		End Set
	End Property


	Private m_myProperty2 As String
	'this property uses our custom UITypeEditor: myUserControlPropertyEditor
	<DefaultValue(""), EditorAttribute(GetType(myUserControlPropertyEditor), GetType(System.Drawing.Design.UITypeEditor))> _
	Public Property myProperty2() As String
		Get
			Return m_myProperty2
		End Get
		Set(ByVal value As String)
			m_myProperty2 = value
		End Set
	End Property


	Private m_myProperty3 As String
	'this property uses our custom UITypeEditor: myFormPropertyEditor
	<DefaultValue(""), EditorAttribute(GetType(myFormPropertyEditor), GetType(System.Drawing.Design.UITypeEditor))> _
	Public Property myProperty3() As String
		Get
			Return m_myProperty3
		End Get
		Set(ByVal value As String)
			m_myProperty3 = value
		End Set
	End Property

End Class



' /////////////////////////////////////////////////////////////////////////
'   myTreeViewPropertyEditor => using a TreeView as EditControl
' /////////////////////////////////////////////////////////////////////////
'
Public Class myTreeViewPropertyEditor
	Inherits PropertyEditorBase

	Private WithEvents myTreeView As TreeView

	Protected Overrides Function GetEditControl(ByVal PropertyName As String, ByVal CurrentValue As Object) As System.Windows.Forms.Control

		If myTreeView Is Nothing Then
			myTreeView = New TreeView
			With myTreeView
				.BorderStyle = System.Windows.Forms.BorderStyle.None
				.Height = 150
				.PathSeparator = "."
				'fill the treeview with some items...
				.Nodes.Add("Root1")
				With .Nodes.Add("Root2")
					With .Nodes.Add("AAA")
						.Nodes.Add("Item1")
						.Nodes.Add("Item2")
					End With
					.Nodes.Add("BBB")
					.Nodes.Add("CCC")
				End With
			End With
		End If

		Return myTreeView

	End Function

	Protected Overrides Function GetEditedValue(ByVal EditControl As System.Windows.Forms.Control, ByVal PropertyName As String, ByVal OldValue As Object) As Object
		Dim s As String = ""
		Dim myNode As TreeNode = myTreeView.SelectedNode
		If myNode IsNot Nothing Then s = myNode.FullPath
		Return s
	End Function

	Private Sub myTreeView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles myTreeView.DoubleClick
		Me.CloseDropDownWindow()
	End Sub

End Class




' /////////////////////////////////////////////////////////////////////////
'   myUserControlPropertyEditor => using a UserControl as EditControl
' /////////////////////////////////////////////////////////////////////////
'
Public Class myUserControlPropertyEditor
	Inherits PropertyEditorBase

	Private WithEvents myControl As myUserControl 'this is the control to be used in design time DropDown editor

	Protected Overrides Function GetEditControl(ByVal PropertyName As String, ByVal CurrentValue As Object) As System.Windows.Forms.Control
		myControl = New myUserControl
		myControl.Visible = True
		myControl.TextBox1.Text = CurrentValue
		Return myControl
	End Function

	Protected Overrides Function GetEditedValue(ByVal EditControl As System.Windows.Forms.Control, ByVal PropertyName As String, ByVal OldValue As Object) As Object
		If myControl Is Nothing Then Return OldValue
		If myControl.IsCanceled Then
			Return OldValue
		Else
			Return myControl.TextBox1.Text
		End If
	End Function

	Private Sub myForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myControl.VisibleChanged
		If myControl.Visible = False Then Me.CloseDropDownWindow()
	End Sub

End Class





' /////////////////////////////////////////////////////////////////////////
'   myFormPropertyEditor => using a Form as EditControl
' /////////////////////////////////////////////////////////////////////////
'
Public Class myFormPropertyEditor
	Inherits PropertyEditorBase

	Private WithEvents myControl As myEditorForm 'this is the control to be used in design time DropDown editor

	Protected Overrides Function GetEditControl(ByVal PropertyName As String, ByVal CurrentValue As Object) As System.Windows.Forms.Control
		myControl = New myEditorForm()
		Dim s As String = ""
		If CurrentValue IsNot Nothing Then s = CurrentValue.ToString
		Dim i As Integer = s.IndexOf(" ")
		If i >= 0 Then
			myControl.TextBox1.Text = s.Substring(0, i)
			Dim d As DateTime
			Date.TryParse(s.Substring(0, i), d)
			myControl.MonthCalendar1.SelectionStart = d
			myControl.MonthCalendar1.SelectionEnd = d
			myControl.TextBox2.Text = s.Substring(i + 1)
		Else
			myControl.TextBox1.Text = CurrentValue
		End If
		Return myControl
	End Function

	Protected Overrides Function GetEditedValue(ByVal EditControl As System.Windows.Forms.Control, ByVal PropertyName As String, ByVal OldValue As Object) As Object
		If myControl Is Nothing Then Return OldValue
		If myControl.IsCanceled Then
			Return OldValue
		Else
			Return myControl.TextBox1.Text
		End If
	End Function

End Class
