Imports System.ComponentModel


<Designer(GetType(SmartTagComponentDesigner(Of mySmartTagActionList2)))> _
Public Class XTooltip
	Inherits ToolTip

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



Public Class mySmartTagActionList2
	Inherits SmartTagActionListBase

	Sub New(ByVal component As IComponent)
		MyBase.New(component)
	End Sub

	Public Overrides Sub AddActionItems()
		AddActionProperty("Name", "Name:", "", "")
	End Sub

End Class
