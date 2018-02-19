Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Public Class DataProvider

    'Create the sample datasource for these exercises
    'this simulates a stored procedure that returns variable value columns
    Public Shared Function CreateDataSource(ByVal numValueColumns As Integer) As DataTable
        Dim t As New DataTable()

        ' create the table structure
        Dim c As New DataColumn()

        c = New DataColumn()
        c.DataType = System.Type.GetType("System.String")
        c.ColumnName = "Category"
        t.Columns.Add(c)

        Dim i As Integer
        For i = 1 To numValueColumns
            c = New DataColumn()
            c.DataType = System.Type.GetType("System.Int32")
            c.ColumnName = "Value" + i.ToString()
            t.Columns.Add(c)
        Next

        ' populate the table with some sample rows of data
        Dim rnd As New Random()
        Dim r As DataRow = t.NewRow()
        r("Category") = "North Region"
        For i = 1 To numValueColumns
            r("Value" + i.ToString()) = rnd.Next(-10, 10)
        Next
        t.Rows.Add(r)

        r = t.NewRow()
        r("Category") = "South Region"
        For i = 1 To numValueColumns
            r("Value" + i.ToString()) = rnd.Next(-10000, 10000)
        Next
        t.Rows.Add(r)

        r = t.NewRow()
        r("Category") = "East Region"
        For i = 1 To numValueColumns
            r("Value" + i.ToString()) = rnd.Next(-10000, 10000)
        Next
        t.Rows.Add(r)

        r = t.NewRow()
        r("Category") = "West Region"
        For i = 1 To numValueColumns
            r("Value" + i.ToString()) = rnd.Next(-10000, 10000)
        Next
        t.Rows.Add(r)

        Return t
    End Function


    Public Shared Function CreateCategoryDataSource(ByVal numValueColumns As Integer) As DataTable

        Dim t As New DataTable()

        ' create the table structure
        Dim c As New DataColumn()

        c = New DataColumn()
        c.DataType = System.Type.GetType("System.String")
        c.ColumnName = "Category"
        t.Columns.Add(c)

        Dim i As Integer
        For i = 1 To numValueColumns
            c = New DataColumn()
            c.DataType = System.Type.GetType("System.Int32")
            c.ColumnName = "Value" + i.ToString()
            t.Columns.Add(c)
        Next

        ' populate the table with some sample rows of data
        Dim rnd As New Random()
        Dim r As DataRow = t.NewRow()
        Dim top As Integer = rnd.Next(2, 8)
        Dim j As Integer

        For j = 0 To top
            r = t.NewRow()
            r("Category") = "North Region"
            For i = 1 To numValueColumns
                r("Value" + i.ToString()) = rnd.Next(-10, 10)
            Next
            t.Rows.Add(r)
        Next

        top = rnd.Next(2, 8)
        For j = 0 To top
            r = t.NewRow()
            r("Category") = "South Region"
            For i = 1 To numValueColumns
                r("Value" + i.ToString()) = rnd.Next(-10000, 10000)
            Next
            t.Rows.Add(r)
        Next

        top = rnd.Next(2, 8)
        For j = 0 To top
            r = t.NewRow()
            r("Category") = "East Region"
            For i = 1 To numValueColumns
                r("Value" + i.ToString()) = rnd.Next(-10000, 10000)
            Next
            t.Rows.Add(r)
        Next

        top = rnd.Next(2, 8)
        For j = 0 To top
            r = t.NewRow()
            r("Category") = "West Region"
            For i = 1 To numValueColumns
                r("Value" + i.ToString()) = rnd.Next(-10000, 10000)
            Next
            t.Rows.Add(r)
        Next

        Return t

    End Function





    Public Shared Function CreateCategoryWithSubGroupsDataSource(ByVal numValueColumns As Integer) As DataTable

        Dim t As New DataTable()

        ' create the table structure
        Dim c As New DataColumn()

        c = New DataColumn()
        c.DataType = System.Type.GetType("System.String")
        c.ColumnName = "Category"
        t.Columns.Add(c)

        c = New DataColumn()
        c.DataType = System.Type.GetType("System.String")
        c.ColumnName = "Subcategory"
        t.Columns.Add(c)

        Dim i As Integer
        For i = 1 To numValueColumns
            c = New DataColumn()
            c.DataType = System.Type.GetType("System.Int32")
            c.ColumnName = "Value" + i.ToString()
            t.Columns.Add(c)
        Next

        ' populate the table with some sample rows of data
        CreateRegionRows(t, "North Region", New String() {"Minnesota", "North Dakota"})
        CreateRegionRows(t, "South Region", New String() {"Louisiana", "Alabama", "Mississippi"})
        CreateRegionRows(t, "East Region", New String() {"New York", "Maine"})
        CreateRegionRows(t, "West Region", New String() {"California", "Nevada", "Oregon"})

        Return t
    End Function

    Private Shared Sub CreateRegionRows(ByVal t As DataTable, ByVal category As String, ByVal subCategories() As String)

        Dim rnd As Random = New Random()
        Dim r As DataRow
        Dim s As String
        Dim i As Integer
        Dim j As Integer
        Dim top As Integer

        For Each s In subCategories
            top = rnd.Next(1, 4)
            For j = 0 To top
                r = t.NewRow()
                r("Category") = category
                r("SubCategory") = s
                For i = 2 To t.Columns.Count - 1
                    r("Value" & (i - 1).ToString()) = rnd.Next(-100, 100)
                Next
                t.Rows.Add(r)
            Next
        Next

    End Sub

End Class
