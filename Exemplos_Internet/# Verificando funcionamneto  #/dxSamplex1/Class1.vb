Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.ListControls
Imports System.Data

Namespace WindowsApplication1
	<UserRepositoryItem("Register")> _
	Public Class RepositoryItemMyButtonEdit
		Inherits RepositoryItemButtonEdit
		Private dataSource_Renamed As Object
		Shared Sub New()
			Register()
		End Sub

		Public Property DataSource() As Object
			Get
				Return dataSource_Renamed
			End Get
			Set(ByVal value As Object)
				dataSource_Renamed = value
			End Set
		End Property

		Friend Const EditorName As String = "MyButtonEdit"

		Public Shared Sub Register()
            EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(EditorName, GetType(MyButtonEdit), GetType(RepositoryItemMyButtonEdit), GetType(DevExpress.XtraEditors.ViewInfo.ButtonEditViewInfo), New DevExpress.XtraEditors.Drawing.ButtonEditPainter(), True, CType(Nothing, Image), GetType(DevExpress.Accessibility.ButtonEditAccessible)))
		End Sub
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return EditorName
			End Get
		End Property

		Public Overrides Function CreateEditor() As BaseEdit
			Dim edit As MyButtonEdit = TryCast(MyBase.CreateEditor(), MyButtonEdit)
			edit.DataSource = DataSource
			Return edit
		End Function
	End Class
	''' <summary>
	''' MyButtonEdit is a descendant from ButtonEdit.
	''' It displays a dialog form below the text box when the edit button is clicked.
	''' </summary>
	Public Class MyButtonEdit
		Inherits ButtonEdit
		Shared Sub New()
			RepositoryItemMyButtonEdit.Register()
		End Sub

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return RepositoryItemMyButtonEdit.EditorName
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As RepositoryItemMyButtonEdit
			Get
				Return TryCast(MyBase.Properties, RepositoryItemMyButtonEdit)
			End Get
		End Property

		Private _DataSource As Object

		<AttributeProvider(GetType(IListSource))> _
		Public Property DataSource() As Object
			Get
				Return _DataSource
			End Get
			Set(ByVal value As Object)
				_DataSource = value
			End Set
		End Property

		Public ReadOnly Property DataSourcePosition() As Integer
			Get
				Return BindingContext(DataSource).Position
			End Get
		End Property

		Private Function ArrayToStr(ByVal array() As Object) As String
			Dim s As String = String.Empty

			For Each obj As Object In array
				s &= obj.ToString() & " ; "
			Next obj
			Return s & Environment.NewLine
		End Function


		Protected Overrides Sub OnClickButton(ByVal buttonInfo As DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs)
			ShowPopupForm()
			MyBase.OnClickButton(buttonInfo)
		End Sub
		Protected Overridable Sub ShowPopupForm()
			Using form As New Form()
			'	form.StartPosition = FormStartPosition.Manual;
			'	form.Location = this.PointToScreen(new Point(0, Height));
				Dim label As New Label()
				label.Parent = form
				label.ForeColor = Color.Red
				label.Dock = DockStyle.Fill
				If TypeOf DataSource Is DataTable Then
					label.Text = ArrayToStr((TryCast(DataSource, DataTable)).Rows(DataSourcePosition).ItemArray)
				End If
				form.ShowDialog()
			End Using
		End Sub
	End Class
End Namespace
