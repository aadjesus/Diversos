' ==============================================================
' Programmer: S.Serpooshan, Jan 2007
' --------------------------------------------------------------
' Need References to: 
'   System, System.Windows.Forms, System.Design, System.Drawing
' ==============================================================

Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design




'/////////////////////////////////////////////////////////////////////////
'	SmartTagControlDesigner : ControlDesigner
'/////////////////////////////////////////////////////////////////////////
'
' Use this for Control Designers.
'		1- Define your DesignerActionList class by inheriting from XControlDesignerActionList.
'		2- Add this attribute to the class which defines your component:
'		     <Designer( GetType( SmartTagControlDesigner(Of Your_DesignerActionList_Class) ) )
'
<System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
Public Class SmartTagControlDesigner(Of DesignerActionList_Class As DesignerActionList)
	Inherits ControlDesigner

	Private m_ActionListCreator As New DesignerActionListCreator(Of DesignerActionList_Class)	'Cache our action list collection for better performance

	Public Overrides ReadOnly Property ActionLists() As DesignerActionListCollection
		Get
			Return m_ActionListCreator.GetActionListCollection(Me.Component)
		End Get
	End Property

End Class




'/////////////////////////////////////////////////////////////////////////
'	SmartTagComponentDesigner : ComponentDesigner
'/////////////////////////////////////////////////////////////////////////
'
' This is similar to SmartTagControlDesigner except that this class inherits from ComponentDesigner (instead of ControlDesigner).
' Use this for Component Designers
'
<System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
Public Class SmartTagComponentDesigner(Of DesignerActionList_Class As DesignerActionList)
	Inherits ComponentDesigner

	Private m_ActionListCreator As New DesignerActionListCreator(Of DesignerActionList_Class)	'Cache our action list collection for better performance

	Public Overrides ReadOnly Property ActionLists() As DesignerActionListCollection
		Get
			Return m_ActionListCreator.GetActionListCollection(Me.Component)
		End Get
	End Property

End Class




''' <summary>Cache the action list collection for better performance. Usually, this class will not be used directly by you.</summary>
Public Class DesignerActionListCreator(Of DesignerActionList_Class As DesignerActionList)

	Private m_ActionLists As DesignerActionListCollection	'Cache our action list collection for better performance

	Public Function GetActionListCollection(ByVal component As IComponent) As DesignerActionListCollection
		If m_ActionLists Is Nothing Then
			Try
				m_ActionLists = New DesignerActionListCollection()
				Dim Obj As Object = Activator.CreateInstance(GetType(DesignerActionList_Class), component)
				m_ActionLists.Add(DirectCast(Obj, DesignerActionList_Class))
			Catch
			End Try
		End If
		Return m_ActionLists
	End Function

End Class



'/////////////////////////////////////////////////////////////////////////
'	SmartTagActionListBase : DesignerActionList
'/////////////////////////////////////////////////////////////////////////
'
Public MustInherit Class SmartTagActionListBase
	Inherits DesignerActionList

	Private m_DesignerService As DesignerActionUIService
	Private m_ActionList As DesignerActionItemCollection
	Private m_Control As Control

	Public Sub New(ByVal component As IComponent)
		MyBase.New(component)
		m_DesignerService = CType(Me.GetService(GetType(DesignerActionUIService)), DesignerActionUIService)
		m_ActionList = New DesignerActionItemCollection
		If TypeOf component Is Control Then
			m_Control = CType(component, Control)
		Else
			m_Control = Nothing
		End If
	End Sub

	''' <summary>
	''' Gets access to the Designer Action UI Service which manages the user interface (UI) for the smart tag panel.
	''' </summary>
	Public ReadOnly Property DesignerService() As DesignerActionUIService
		Get
			Return m_DesignerService
		End Get
	End Property

	''' <summary>
	''' Gets the internal DesignerActionItemCollection of this class which represents a collection of Action Items to display on the smart tag panel.
	''' </summary>
	Public ReadOnly Property ActionList() As DesignerActionItemCollection
		Get
			Return m_ActionList
		End Get
	End Property

	''' <summary>Gets the container which sites this Component or Control.</summary>
	Public ReadOnly Property Container() As IContainer
		Get
			Return Me.Component.Site.Container
		End Get
	End Property

	''' <summary>Gets this Control (if associated Component is a Control).</summary>
	Public ReadOnly Property Control() As Control
		Get
			Return m_Control
		End Get
	End Property

	''' <summary>
	''' Refreshes the contents of designer smart tag panel.
	''' </summary>
	Public Sub RefreshDesigner()
		If m_DesignerService IsNot Nothing Then m_DesignerService.Refresh(Me.Component)
	End Sub

	''' <summary>
	''' Hides the smart tag panel of this component.
	''' </summary>
	Public Sub HideDesigner()
		If m_DesignerService IsNot Nothing Then m_DesignerService.HideUI(Me.Component)
	End Sub

	''' <summary>
	''' Sets the specified property of specified control to the given value. 
	''' Note: by means of Reflection methods, "Undo" and "Menu Updates" in IDE work properly! (donot modify control properties directly through the code, but use this method)
	''' </summary>
	Public Sub SetPropertyByName(ByVal ComponentObject As Object, ByVal propName As String, ByVal value As Object)
		If ComponentObject Is Nothing Then Exit Sub
		Dim prop As PropertyDescriptor = TypeDescriptor.GetProperties(ComponentObject).Item(propName)
		If prop IsNot Nothing Then
			Try
				prop.SetValue(ComponentObject, value)
			Catch ex As Exception
				Dim sValue As String
				If value IsNot Nothing Then sValue = value.ToString() Else sValue = ""
				MessageBox.Show("SmartTagActionList: Cannot set " & prop.Name & " property to the specified value: " & sValue, "Error")
			End Try
		End If
	End Sub

	Public Property Name() As String
		Get
			Return Me.Component.Site.Name 'if it is a Control, this statement is equal to: m_Control.Name 
		End Get
		Set(ByVal value As String)
			If value <> Me.Name Then
				If m_Control IsNot Nothing Then
					SetPropertyByName(m_Control, "Name", value)	'undo is available
				Else
					Me.Component.Site.Name = value 'undo will not be available in this case
				End If
			End If
		End Set
	End Property

	''' <summary>Note: This property is valid only for Controls (Not Components)</summary>
	Public Property RightToLeft() As RightToLeft
		Get
			If m_Control IsNot Nothing Then Return m_Control.RightToLeft Else Return Nothing
		End Get
		Set(ByVal value As RightToLeft)
			SetPropertyByName(m_Control, "RightToLeft", value)
		End Set
	End Property

	''' <summary>Note: This property is valid only for Controls (Not Components)</summary>
	Public Property Font() As Font
		Get
			If m_Control IsNot Nothing Then Return m_Control.Font Else Return Nothing
		End Get
		Set(ByVal value As Font)
			SetPropertyByName(m_Control, "Font", value)
		End Set
	End Property

	''' <summary>Note: This property is valid only for Controls (Not Components)</summary>
	Public Property Text() As String
		Get
			If m_Control IsNot Nothing Then Return m_Control.Text Else Return Nothing
		End Get
		Set(ByVal value As String)
			SetPropertyByName(m_Control, "Text", value)
		End Set
	End Property

	''' <summary>Clears the text of the related Control. Note: This is valid only for Controls (Not Components)</summary>
	Public Sub ClearControlText() 'clear the text
		If m_Control IsNot Nothing AndAlso Me.Text <> "" Then Me.Text = "" : RefreshDesigner()
	End Sub

	''' <summary>Clears all action items.</summary>
	Public Sub ClearActionList()
		m_ActionList.Clear()
	End Sub

	''' <summary>Adds a static Text item to ActionItems. Call it in GetSortedActionItems() function.</summary>
	Public Sub AddActionText(ByVal displayName As String, ByVal category As String)
		m_ActionList.Add(New DesignerActionTextItem(displayName, category))
	End Sub

	''' <summary>Adds a Property item to ActionItems. Call it in GetSortedActionItems() function.</summary>
	''' <param name="memberName">The property name defined in your DesignerActionList (Not your control property!)</param>
	Public Sub AddActionProperty(ByVal memberName As String, ByVal displayName As String, ByVal category As String, ByVal description As String)
		m_ActionList.Add(New DesignerActionPropertyItem(memberName, displayName, category, description))
	End Sub

	''' <summary>Adds a Method item to ActionItems. Call it in GetSortedActionItems() function.</summary>
	''' <param name="memberName">The property name defined in your DesignerActionList (Not your control property!)</param>
	Public Sub AddActionMethod(ByVal memberName As String, ByVal displayName As String, ByVal category As String, ByVal description As String, ByVal includeAsDesignerVerb As Boolean)
		m_ActionList.Add(New DesignerActionMethodItem(Me, memberName, displayName, category, description, includeAsDesignerVerb))
	End Sub

	''' <summary>Adds a Header (Category) item to ActionItems. Call it in GetSortedActionItems() function.</summary>
	Public Sub AddActionHeader(ByVal displayName As String)
		m_ActionList.Add(New DesignerActionHeaderItem(displayName))
	End Sub

	''' <summary>Usually you should override DefineSortedActionItems() method to define your action items. But if you want, You can override this function to specify your smart tag action items. Call AddActionInit() and then some AddAction*() methods to add your items to ActionItems property. Finally you should return ActionItems.</summary>
	Public NotOverridable Overrides Function GetSortedActionItems() As DesignerActionItemCollection
		Me.ClearActionList()
		Me.AddActionItems()	'add user defined items to the ActionItems collection.
		Return m_ActionList
	End Function

	''' <summary>You should Override this method to specify your smart tag action items. Call AddAction*() methods to define your action items in this method.</summary>
	Public MustOverride Sub AddActionItems()

End Class

