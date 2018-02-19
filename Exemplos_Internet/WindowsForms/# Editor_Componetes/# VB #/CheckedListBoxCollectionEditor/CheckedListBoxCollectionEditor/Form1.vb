''' <summary>
''' Sample form with a property grid whose selected object is the custom listbox editor
''' </summary>
''' <remarks></remarks>
Public Class Form1

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'create the custom checked listbox editor
        Dim c As New My.PropertyGridControls.CheckedListBoxEditor

        'bind it to the property grid
        Me.PropertyGrid1.SelectedObject = c
    End Sub
End Class
