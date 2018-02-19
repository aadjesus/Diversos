Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing

Public Class Form1
  
    'Defining the path where the database is located
    Dim FilePath As String = "DragAndDrop.mdb"
    Dim conString As String = "Provider=microsoft.jet.OLEDB.4.0;" & _
                   "Data Source =" & FilePath

    'Settin up the connection and the reader
    Dim conn As New OleDbConnection(conString)

    Dim selectString As String
    Dim cmd As OleDbCommand
    Dim reader As OleDbDataReader

    Private Dragging As Boolean
    Private mousex As Integer
    Private mousey As Integer

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ToolStrip1.Visible = False

        'Fill in an Id for the ToolStripButton
        Dim i As Integer
        For i = 0 To ToolStrip1.Items.Count - 1
            ToolStrip1.Items.Item(i).Tag = i
        Next

        'Reading the database when starting up
        selectString = "SELECT * FROM tblInfo"
        If conn.State = ConnectionState.Closed Then conn.Open()
        cmd = New OleDbCommand(selectString, conn)
        reader = cmd.ExecuteReader()

        While reader.Read()
            'Reading all positions in the database

            Dim MyPicturebox As New PictureBox
            'Add handlers that will move the image on the screen
            AddHandler MyPicturebox.MouseDown, AddressOf MyMouseClick
            AddHandler MyPicturebox.MouseMove, AddressOf MyMouseMove
            AddHandler MyPicturebox.MouseUp, AddressOf MyMouseUp

            'Adding an image and properties for the image
            Me.Controls.Add(MyPicturebox)
            MyPicturebox.Location = New Point(reader.Item("PosX").ToString, reader.Item("PosY").ToString)

            MyPicturebox.Tag = reader.Item("Id").ToString
            MyPicturebox.AccessibleDescription = reader.Item("Name").ToString

            MyPicturebox.BackgroundImageLayout = ImageLayout.Stretch
            MyPicturebox.BringToFront()
            MyPicturebox.Size = New System.Drawing.Size(40, 40)

            'Find the right image to use
            For i = 0 To ToolStrip1.Items.Count - 1
                If ToolStrip1.Items.Item(i).ToolTipText = reader.Item("name") Then
                    MyPicturebox.BackgroundImage = ToolStrip1.Items.Item(i).Image
                End If
            Next

        End While
        reader.Close()
        If conn.State = ConnectionState.Open Then conn.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Set to Edit mode
        ToolStrip1.Visible = True
        ToolStrip1.BringToFront()
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        'Code to insert a picture

        'If it is "End edit mode" then don't insert anything
        If Not e.ClickedItem.Tag = ToolStrip1.Items.Count - 1 Then

            Dim MyPicturebox As New PictureBox

            'Add handlers that will move the image on the screen
            AddHandler MyPicturebox.MouseDown, AddressOf MyMouseClick
            AddHandler MyPicturebox.MouseMove, AddressOf MyMouseMove
            AddHandler MyPicturebox.MouseUp, AddressOf MyMouseUp

            'Adding an image and properties for the image
            Me.Controls.Add(MyPicturebox)
            MyPicturebox.Location = New Point(40, 40)
            MyPicturebox.BringToFront()
            MyPicturebox.BackgroundImageLayout = ImageLayout.Stretch

            'Find out which button is pressed to insert the right image. 
            Dim TagId As Integer = e.ClickedItem.Tag

            MyPicturebox.BackgroundImage = sender.Items.Item(TagId).Image
            MyPicturebox.Name = sender.Items.Item(TagId).ToolTipText

            MyPicturebox.Size = New System.Drawing.Size(40, 40)

            ' Insert position and name in the database.
            selectString = "Insert into tblInfo (PosX, PosY, Name) Values (" & _
                                   "'" & 40 & "'," & _
                                    "'" & 40 & "'," & _
                                    "'" & MyPicturebox.Name & "')"

            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim cmd = New OleDbCommand(selectString, conn)
            cmd.executeNonQuery()
            If conn.State = ConnectionState.Open Then conn.Close()

            'Find the corresponding Id from the database
            MyPicturebox.Tag = FindLastId()

        Else
            'End Edit mode
            ToolStrip1.Visible = False
        End If

        sender.Invalidate()


    End Sub


    Private Function FindLastId() As String

        selectString = "Select max(id) FROM tblInfo"
        If conn.State = ConnectionState.Closed Then conn.Open()

        Dim cmd = New OleDbCommand(selectString, conn)

        Dim Id As String
        Id = cmd.ExecuteScalar()

        If conn.State = ConnectionState.Open Then conn.Close()

        Return Id
    End Function


    ' The handler for the MouseClick event
    Public Sub MyMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        ' Find out if it is in Drag and Drop mode
        If ToolStrip1.Visible = True Then

            'Prosedure to move an image from the workspace. Using Mousebutton right
            If e.Button = Windows.Forms.MouseButtons.Right Then

                Dim Response As MsgBoxResult = MsgBox("Do you want to remove this object", MsgBoxStyle.YesNo, "Id = " & sender.tag)
                If Response = MsgBoxResult.Yes Then   ' User chose Yes.
                    'Remove from workspace
                    Me.Controls.Remove(sender)
                    'Remove from database
                    If conn.State = ConnectionState.Closed Then conn.Open()
                    selectString = "DELETE FROM tblInfo WHERE(Id =" & sender.Tag & ")"
                    cmd = New OleDbCommand(selectString, conn)
                    reader = cmd.ExecuteReader()
                    reader.Close()
                    If conn.State = ConnectionState.Open Then conn.Close()

                End If

            End If

            'Prosedure to select the image
            If e.Button = Windows.Forms.MouseButtons.Left Then

                Me.Cursor = Cursors.Hand

                Dragging = True
                mousex = -e.X
                mousey = -e.Y
                Dim clipleft As Integer = Me.PointToClient(MousePosition).X - sender.Location.X
                Dim cliptop As Integer = Me.PointToClient(MousePosition).Y - sender.Location.Y
                Dim clipwidth As Integer = Me.ClientSize.Width - (sender.Width - clipleft)
                Dim clipheight As Integer = Me.ClientSize.Height - (sender.Height - cliptop)
                Windows.Forms.Cursor.Clip = Me.RectangleToScreen(New Rectangle(clipleft, cliptop, clipwidth, clipheight))
                sender.Invalidate()

            End If
        End If
    End Sub

    ' The handler for the MouseMove event
    Public Sub MyMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ' Find out if it is in Drag and Drop mode
        If ToolStrip1.Visible = True Then
            If Dragging Then
                'move control to new position
                Dim MPosition As New Point()
                MPosition = Me.PointToClient(MousePosition)
                MPosition.Offset(mousex, mousey)
                'ensure control cannot leave container
                sender.Location = MPosition
            End If
        End If
    End Sub

    ' The handler for the MouseUp event
    Private Sub MyMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        ' Find out if it is in Drag and Drop mode
        If ToolStrip1.Visible = True Then

            If Dragging Then
                'After dragging update the database with the new position X and Y
                selectString = "Update (tblInfo) Set " & _
                            "PosX=" & "'" & sender.Location.X & "'," & _
                            "PosY=" & "'" & sender.Location.Y & "'" & _
                            " WHERE Id=" & sender.tag & ""

                If conn.State = ConnectionState.Closed Then conn.Open()

                Dim cmd = New OleDbCommand(selectString, conn)
                cmd.executeNonQuery()
                If conn.State = ConnectionState.Open Then conn.Close()

                'End the dragging
                Dragging = False
                Windows.Forms.Cursor.Clip = Nothing
                sender.Invalidate()
            End If

            Me.Cursor = Cursors.Arrow
        End If
    End Sub

End Class
