Imports System.ComponentModel


<Designer(GetType(SmartTagControlDesigner(Of mySmartTagActionList1)))> _
Public Class XTextBox
	Inherits TextBox

	Private m_myProperty As String

	Public Property myProperty() As String
		Get
			Return m_myProperty
		End Get
		Set(ByVal value As String)
			m_myProperty = value
		End Set
	End Property

End Class



Public Class mySmartTagActionList1
	Inherits SmartTagActionListBase

	Private m_Control As XTextBox

	Sub New(ByVal component As IComponent)
		MyBase.New(component)
		m_Control = CType(component, XTextBox)
	End Sub

	Public Property BackColor() As Color
		Get
			Return m_Control.BackColor
		End Get
		Set(ByVal value As Color)
			Me.SetPropertyByName(m_Control, "BackColor", value)
		End Set
	End Property

	Public Property ForeColor() As Color
		Get
			Return m_Control.ForeColor
		End Get
		Set(ByVal value As Color)
			Me.SetPropertyByName(m_Control, "ForeColor", value)
		End Set
	End Property

	Public Property IsMultiline() As Boolean
		Get
			Return m_Control.Multiline
		End Get
		Set(ByVal value As Boolean)
			Me.SetPropertyByName(m_Control, "Multiline", value)
		End Set
	End Property

	Public Sub SwapColors()
		Dim c As Color = Me.ForeColor
		Me.ForeColor = Me.BackColor
		Me.BackColor = c
		RefreshDesigner()
	End Sub

	Public Overrides Sub AddActionItems()
		'These properties are already defined in base (SmartTagActionListBase) class:
		'  => Name, Text, Font, RightToLeft
		'Other properties/Methods should be defined in current class
		AddActionHeader("Main")
		AddActionProperty("Name", "Name:", "Main", "")
		AddActionProperty("Text", "Text:", "Main", "")
		AddActionProperty("Font", "Font:", "Main", "")
		AddActionProperty("IsMultiline", "Multiline:", "", "")
		AddActionHeader("Colors")
		AddActionProperty("ForeColor", "ForeColor:", "Colors", "Sets the ForeColor")
		AddActionProperty("BackColor", "BackColor:", "Colors", "Sets the BackColor")
		AddActionText("This is my info...", "Colors")
		AddActionMethod("SwapColors", "Swap Colors", "Colors", "Swap ForeColor and BackColor", True)
	End Sub

End Class