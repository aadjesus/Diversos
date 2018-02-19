Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace WindowsApplication1
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		Private colColumn1 As DevExpress.XtraGrid.Columns.GridColumn
		Private repositoryItemMyButtonEdit1 As WindowsApplication1.RepositoryItemMyButtonEdit
		Private myButtonEdit1 As WindowsApplication1.MyButtonEdit
		Private gridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()
			gridControl1.DataSource = CreateTable(20)
			repositoryItemMyButtonEdit1.DataSource = gridControl1.DataSource
			myButtonEdit1.DataSource = gridControl1.DataSource

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.myButtonEdit1 = New WindowsApplication1.MyButtonEdit()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.colColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.repositoryItemMyButtonEdit1 = New WindowsApplication1.RepositoryItemMyButtonEdit()
			Me.gridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
			CType(Me.myButtonEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.repositoryItemMyButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' myButtonEdit1
			' 
			Me.myButtonEdit1.EditValue = "myButtonEdit1"
			Me.myButtonEdit1.Location = New System.Drawing.Point(13, 7)
			Me.myButtonEdit1.Name = "myButtonEdit1"
			Me.myButtonEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton()})
			Me.myButtonEdit1.Size = New System.Drawing.Size(120, 20)
			Me.myButtonEdit1.TabIndex = 0
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.gridControl1.Location = New System.Drawing.Point(0, 51)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() { Me.repositoryItemMyButtonEdit1})
			Me.gridControl1.Size = New System.Drawing.Size(400, 208)
			Me.gridControl1.TabIndex = 1
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' gridView1
			' 
			Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.colColumn1, Me.gridColumn1})
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
			Me.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
			' 
			' colColumn1
			' 
			Me.colColumn1.Caption = "Column1"
			Me.colColumn1.ColumnEdit = Me.repositoryItemMyButtonEdit1
			Me.colColumn1.FieldName = "Name"
			Me.colColumn1.Name = "colColumn1"
			Me.colColumn1.Visible = True
			Me.colColumn1.VisibleIndex = 0
			' 
			' repositoryItemMyButtonEdit1
			' 
			Me.repositoryItemMyButtonEdit1.AutoHeight = False
			Me.repositoryItemMyButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton()})
			Me.repositoryItemMyButtonEdit1.Name = "repositoryItemMyButtonEdit1"
			' 
			' gridColumn1
			' 
			Me.gridColumn1.Caption = "gridColumn1"
			Me.gridColumn1.FieldName = "ID"
			Me.gridColumn1.Name = "gridColumn1"
			Me.gridColumn1.Visible = True
			Me.gridColumn1.VisibleIndex = 1
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(400, 259)
			Me.Controls.Add(Me.gridControl1)
			Me.Controls.Add(Me.myButtonEdit1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			CType(Me.myButtonEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.repositoryItemMyButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub
		Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) })
			Next i
			Return tbl

		End Function
	End Class
End Namespace
