' Copyright (C) 2003 Michaël Willemot
' THE SOFTWARE IS PROVIDED BY THE AUTHOR "AS IS", WITHOUT WARRANTY
' OF ANY KIND, EXPRESS OR IMPLIED. IN NO EVENT SHALL THE AUTHOR BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OF THIS
' SOFTWARE.
Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.SuspendLayout()
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropertyGrid1.CommandsVisibleIfAvailable = True
        Me.PropertyGrid1.LargeButtons = False
        Me.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.PropertyGrid1.Location = New System.Drawing.Point(120, 0)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(312, 293)
        Me.PropertyGrid1.TabIndex = 0
        Me.PropertyGrid1.Text = "PropertyGrid1"
        Me.PropertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window
        Me.PropertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText
        '
        'GroupBox1
        '
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 293)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select object"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(432, 293)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Const MAXITEMS As Integer = 2 ' normally this would be a count on items in database
    Private MyOwnClassBag As New PropertyBag  ' the bag with properties that will be shown in the grid
    Private PropGridObj As MyOwnClass  ' the object that the propertygrid will change

    Private MyArray(MAXITEMS) As MyOwnClass ' my array of objects
    Friend rdb(MAXITEMS) As RadioButton ' controls to be able to select an object to see in the grid

    Private Sub rdb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If sender.checked Then
            ' make the propgridobj a reference to the obj in the array
            ' thus, whenever propgridobj changes, myarray(sender.tag) will change too
            Me.PropGridObj = MyArray(sender.tag)
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadArrayAndControls()
        Call initbag()
        rdb(0).Checked = True
    End Sub
    Private Sub MyOwnClassBag_GetValue(ByVal sender As Object, ByVal e As PropertySpecEventArgs)
        Select Case e.Property.Name
            Case "TheTitle"
                e.Value = Me.PropGridObj.Title
            Case "TheDescription"
                e.Value = Me.PropGridObj.Description
            Case "TheID"
                e.Value = Me.PropGridObj.ID
        End Select
    End Sub
    Private Sub MyOwnClassBag_SetValue(ByVal sender As Object, ByVal e As PropertySpecEventArgs)
        Select Case e.Property.Name
            ' Case "TheID" not necessary as it is a readonly property
        Case "TheTitle"
                Me.PropGridObj.Title = CType(e.Value, String)
            Case "TheDescription"
                Me.PropGridObj.Description = e.Value
        End Select
    End Sub

    Private Sub initbag()
        ' Connect the functions that will handle the set & get of the objects properties
        AddHandler MyOwnClassBag.GetValue, AddressOf MyOwnClassBag_GetValue
        AddHandler MyOwnClassBag.SetValue, AddressOf MyOwnClassBag_SetValue

        ' add the properties that should be shown in the grid to the bag

        ' First a read-only property for the ID
        Dim ps As New PropertySpec( _
                "TheID", _
                 GetType(Integer), _
                "Normal", _
                "The identifier of the object" _
                )
        Dim a() As Attribute = {System.ComponentModel.ReadOnlyAttribute.Yes}
        ps.Attributes = a
        Me.MyOwnClassBag.Properties.Add(ps)

        ' Then a string for the title
        Me.MyOwnClassBag.Properties.Add( _
            New PropertySpec( _
                "TheTitle", _
                GetType(String), _
                "Normal", _
                "This is the description of the title" _
                ) _
            )

        ' and now our customtype property for the description
        Me.MyOwnClassBag.Properties.Add( _
            New PropertySpec( _
                "TheDescription", _
                GetType(MultiLineString), _
                "Custom", _
                "This is a description of the object", _
                Nothing, _
                GetType(MultiLineStringEditor), _
                GetType(MultiLineStringConverter) _
                ) _
            )

        ' link the bag to the propertygrid
        Me.PropertyGrid1.SelectedObject = Me.MyOwnClassBag
    End Sub
    Private Sub LoadArrayAndControls()
        Dim i As Integer
        For i = 0 To MAXITEMS
            ' Fill MyArray (normally with values from database)
            MyArray(i) = New MyOwnClass(i)
            MyArray(i).Title = "Title " & i
            MyArray(i).Description = New MultiLineString("Description of" & vbCrLf & "object nr " & i)

            ' load number of controls
            ' Normally i'd use a subclassed listbox or treeview that stores 
            ' the MyOwnClass objects themselve
            ' but for simplicity i use some radiobuttons
            rdb(i) = New RadioButton
            rdb(i).Text = "Object " & i
            rdb(i).Tag = i ' Store this to be able to get a ref. to the obj. in MyArray()
            Me.GroupBox1.Controls.Add(rdb(i))
            rdb(i).Left = 20
            If i > 0 Then
                rdb(i).Top = rdb(i - 1).Top + rdb(i - 1).Height
            Else
                rdb(i).Top = 20
            End If
            rdb(i).Visible = True
            AddHandler rdb(i).CheckedChanged, AddressOf Me.rdb_CheckedChanged
        Next
    End Sub

End Class