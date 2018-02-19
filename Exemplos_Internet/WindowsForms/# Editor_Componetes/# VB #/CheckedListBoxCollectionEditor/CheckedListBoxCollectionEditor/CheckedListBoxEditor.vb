Imports System
Imports System.ComponentModel
Imports System.Windows.Forms.Design
Imports System.Windows.Forms
Imports System.Collections.Specialized

Namespace My.PropertyGridControls
	
	#Region "CheckedListBoxEditor"
	''' <summary>
	''' The control displayed in the property grid
	''' </summary>
	''' <remarks></remarks>
	Public Class CheckedListBoxEditor
		
		''' <summary>
		''' The default text displayed in the property grid "value" column
		''' </summary>
		''' <remarks></remarks>
		Private _strValue As String = "(Collection)"
		
		''' <summary>
		''' Creates a custom property editor using the CheckedListBoxEditor class as the UITypeEditor
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("This property contains the checked listbox collection."), _
			EditorAttribute(GetType(CheckedListBoxUITypeEditor), GetType(System.Drawing.Design.UITypeEditor))> _
			Public Property CheckedListBoxCollectionProperty() As String
			Get
				Return _strValue
			End Get
			Set(ByVal value As String)
				_strValue = "(Collection)"
			End Set
		End Property
		
	End Class
	#End Region
	
	#Region "CheckedListBoxUITypeEditor"
	
	''' <summary>
	''' Custom, editable checked listbox control
	''' </summary>
	''' <remarks>This demo loads a comma-delimited string collection of Urls and boolean values to set the checked state for each item.</remarks>
	Public Class CheckedListBoxUITypeEditor
		Inherits System.Drawing.Design.UITypeEditor
		Public WithEvents cbx As New CheckedListBox
		Private es As IWindowsFormsEditorService
		
		''' <summary>
		''' Override the UITypeEditorEditStyle to return the editor style: drop-down, modal, or none
		''' </summary>
		''' <param name="context"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) _
				As System.Drawing.Design.UITypeEditorEditStyle
			
			'returns the editor style:  drop-down, modal, or none
			Return System.Drawing.Design.UITypeEditorEditStyle.DropDown
			
		End Function
		
		''' <summary>
		''' Override whether or not the listbox control should be resizable
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Overloads Overrides ReadOnly Property IsDropDownResizable() As Boolean
			Get
				'if set to true, adds a grip to the lower-left portion of the listbox,
				'which makes the listbox resizeable as run time
				Return True
				
			End Get
		End Property
		
		''' <summary>
		''' Override the default method for editing values in the listbox
		''' </summary>
		''' <param name="context"></param>
		''' <param name="provider"></param>
		''' <param name="value"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
				ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
			
			'instantiate the custom property editor service provider
			es = DirectCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
			
			If es IsNot Nothing Then
				
				'load the listbox items
				LoadListBoxItems()
				
				'sort the items
				cbx.Sorted = True
				
				'show the control
				es.DropDownControl(cbx)
				
			End If
			
			'ensure function returns a value on all code paths
			Return Nothing
			
		End Function
		
		''' <summary>
		''' Save the listbox key/value pairs to My.Settings.UrlList
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub bx_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbx.Leave
			'clear the old list
			My.Settings.UrlsList.Clear()
			
			With cbx
				'load the listbox key/value pairs
				For i As Integer = 0 To .Items.Count - 1
					
					Dim txt As String = .Items(i).ToString
					Dim chk As String = .GetItemChecked(i).ToString
					
					'concatenate the key/value pair
					Dim combined As String = LCase(txt) & "," & LCase(chk)
					
					If .Items(i).ToString IsNot "" Then
						
						'add the concatenated string to the "UrlsList" string collection
						My.Settings.UrlsList.Add(combined)
						
					End If
				Next
			End With
			'save the config file
			My.Settings.Save()
		End Sub
		
		''' <summary>
		''' Loads My.Settings.UrlList comma-delimited string collection into the custom collection editor.
		''' </summary>
		''' <remarks></remarks>
		Private Sub LoadListBoxItems()
			'create an array list
			Dim a As New ArrayList
			
			'load the config file "UrlsList" string collection into the array
			For Each s As String In My.Settings.UrlsList
				
				'split the url from the checked value
				a.Add(Split(s, ","))
				
			Next
			
			'create a hashtable, so we can refer to the items in a key/value pair format
			Dim h As New Hashtable
			
			'load the array into the hashtable
			For i As Integer = 0 To a.Count - 1
				
				'add the first array item as the key, the second as the value
				h.Add(CType(a.Item(i), Array).GetValue(0).ToString, CType(a.Item(i), Array).GetValue(1).ToString)
				
			Next
			
			'dispose of the array list
			a = Nothing
			
			'clear the listbox items
			cbx.Items.Clear()
			
			'index the hashtable
			For Each de As DictionaryEntry In h
				
				'add the key/value pairs to the listbox
				cbx.Items.Add(de.Key, CBool(de.Value))
				
			Next
			
			'dispose of the collection
			h = Nothing
			
		End Sub
		
	End Class
	#End Region
	
End Namespace