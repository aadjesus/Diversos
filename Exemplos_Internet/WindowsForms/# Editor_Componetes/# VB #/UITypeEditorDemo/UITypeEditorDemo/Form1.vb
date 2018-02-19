Public Class Form1

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PropertyGrid1.CollapseAllGridItems()

        ExpandGridItem("Shape", PropertyGrid1)
        ExpandGridItem("Shape", PropertyGrid1)

    End Sub

    ' Find the GridItem
    Private Sub ExpandGridItem(ByVal Search_grid_item As String, ByVal pg As PropertyGrid)
        ' Find the GridItem root.
        Dim root As Object
        root = pg.SelectedGridItem
        Do Until root.Parent Is Nothing
            root = root.Parent
        Loop

        ' Search the grid.
        Dim childgriditem As New Collection
        childgriditem.Add(root)
        Do Until childgriditem.Count = 0
            Dim test As GridItem = childgriditem(1)
            childgriditem.Remove(1)
            If test.Label = Search_grid_item Then test.Expanded = True

            For Each obj As Object In test.GridItems
                childgriditem.Add(obj)
            Next obj
        Loop
    End Sub

End Class
