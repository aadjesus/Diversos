Imports System.ComponentModel
Imports ProgBar

Public Class Form1
    Private BWorker As BackgroundWorker
    Dim Max As Int32 = 500

#Region "Button Clicks"

    Private Sub butText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butText.Click
        With ProgBarPlusText
            .ResetBar()
            For i As Int32 = .Min To .Max
                .Increment()
                System.Threading.Thread.Sleep(5)
            Next
        End With
    End Sub

    Private Sub butFormatStrPercent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butFormatStrPercent.Click
        With ProgBarPlusFormat
            .ResetBar()
            For i As Int32 = .Min To .Max
                .Increment()
                System.Threading.Thread.Sleep(5)
            Next
        End With
    End Sub
    Private Sub butPercOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butPercOnly.Click
        With ProgBarPlusPerc
            .ResetBar()
            For i As Int32 = .Min To .Max
                .Increment()
                System.Threading.Thread.Sleep(5)
            Next
        End With
    End Sub

    Private Sub butTexture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butTexture.Click
        With ProgBarPlusTexture
            .ResetBar()
            For i As Int32 = .Min To .Max
                .Increment()
                System.Threading.Thread.Sleep(5)
            Next
        End With
    End Sub

    Private Sub butHatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butHatch.Click
        With ProgBarPlusHatch
            .ResetBar(False)
            For i As Int32 = .Min To .Max
                .Decrement()
                System.Threading.Thread.Sleep(5)
            Next
        End With
    End Sub

    Private Sub ProgBarPlusFixedBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butFixedBar.Click
        With ProgBarPlusFixedBar
            .ResetBar()
            .ForeColor = Color.Crimson
            For i As Int32 = .Min To .Max
                .Increment()
                System.Threading.Thread.Sleep(5)
            Next
            .ForeColor = Color.ForestGreen
        End With

    End Sub

    Private Sub chkT1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkT1.CheckedChanged
        With ProgBarPlusGrid1
            If chkT1.Checked Then
                chkT1.Text = "Stop"
                If .BarType <> ProgBarPlus.eBarType.Bar Then
                    .CylonRun = True
                Else
                    Timer1.Start()
                End If
            Else
                chkT1.Text = "Start"
                If .BarType <> ProgBarPlus.eBarType.Bar Then
                    .CylonRun = False
                End If
                Timer1.Stop()
            End If
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgBarPlusGrid1.Increment()
        If ProgBarPlusGrid1.Value = ProgBarPlusGrid1.Max Then
            Timer1.Stop()
            Delay(DateInterval.Second, 1)
            ProgBarPlusGrid1.Value = ProgBarPlusGrid1.Min
            If chkT1.Checked Then Timer1.Start()
        End If
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Timer1.Interval = TrackBar1.Value
    End Sub

    Private Sub chkT2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkT2.CheckedChanged
        With ProgBarPlusGrid2
            If chkT2.Checked Then
                chkT2.Text = "Stop"
                If .BarType <> ProgBarPlus.eBarType.Bar Then
                    .CylonRun = True
                Else
                    Timer2.Start()
                End If
            Else
                chkT2.Text = "Start"
                If .BarType <> ProgBarPlus.eBarType.Bar Then
                    .CylonRun = False
                End If
                Timer2.Stop()
            End If
        End With
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        ProgBarPlusGrid2.Increment()
        If ProgBarPlusGrid2.Value = ProgBarPlusGrid2.Max Then
            Timer2.Stop()
            Delay(DateInterval.Second, 1)
            ProgBarPlusGrid2.Value = ProgBarPlusGrid2.Min
            If chkT2.Checked Then Timer2.Start()
        End If
    End Sub

    Sub Delay(ByVal DateCylonInterval As DateInterval, ByVal CylonInterval As Double)
        Dim tmr As Date = Now
        Do Until Now > DateAdd(DateCylonInterval, CylonInterval, tmr)
            Application.DoEvents()
        Loop

    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        Timer2.Interval = TrackBar2.Value
    End Sub

    Private Sub ProgBarPlusRunAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgBarPlusRunAll.Click
        Max = 0
        If ProgBarPlusFormat.Max > Max Then Max = ProgBarPlusFormat.Max
        If ProgBarPlusPerc.Max > Max Then Max = ProgBarPlusPerc.Max
        If ProgBarPlusTexture.Max > Max Then Max = ProgBarPlusTexture.Max
        If ProgBarPlusHatch.Max > Max Then Max = ProgBarPlusHatch.Max
        If ProgBarPlusGrid1.Max > Max Then Max = ProgBarPlusGrid1.Max
        If ProgBarPlusGrid2.Max > Max Then Max = ProgBarPlusGrid2.Max
        If ProgBarPlusRunAll.Max > Max Then Max = ProgBarPlusRunAll.Max
        If ProgBarPlusFixedBar.Max > Max Then Max = ProgBarPlusFixedBar.Max
        If ProgBarPlusText.Max > Max Then Max = ProgBarPlusText.Max

        ProgBarPlusFormat.ResetBar()
        ProgBarPlusPerc.ResetBar()
        ProgBarPlusTexture.ResetBar()
        ProgBarPlusHatch.ResetBar(False)
        ProgBarPlusGrid1.ResetBar()
        ProgBarPlusGrid2.ResetBar()
        ProgBarPlusRunAll.ResetBar()
        ProgBarPlusText.ResetBar()
        ProgBarPlusFixedBar.ResetBar()
        ProgBarPlusFixedBar.ForeColor = Color.Crimson
        ProgBarPlusCylon.CylonRun = True

        StartBarThread()
    End Sub
#End Region 'Button Clicks

#Region "Form Events"

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        BW_ProgressBar.CancelAsync()
        While BW_ProgressBar.IsBusy
            Application.DoEvents()
        End While

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PropertyGrid1.CollapseAllGridItems()

        ExpandGridItem("Appearance ProgBar", PropertyGrid1)
        ExpandGridItem("Bar", PropertyGrid1)
        ExpandGridItem("Bar Cylon", PropertyGrid1)

        ProgBarPlusRunAll.Value = 0
        BW_ProgressBar.WorkerReportsProgress = True
        BW_ProgressBar.WorkerSupportsCancellation = True

        Dim cbItem As New ToolStripControlHost(ProgBarPlus_StatusBar)
        StatusStrip1.Items.Insert(0, cbItem)
        StatusStrip1.Items(0).Margin = New System.Windows.Forms.Padding(2, 3, 2, 1)
        StatusStrip1.Items(0).AutoSize = False
        StatusStrip1.Items(0).Size = New Size(500, 19)
        ProgBarPlus_StatusBar.CylonRun = True
    End Sub
#End Region 'Form Events

#Region "Helpers"

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
#End Region 'Helpers

#Region "Component Background Worker for Cylon"
    'Running a process in the component background worker with the Cylon Bar"


    Private m_NewValue As Integer = 0
    Private m_BackThread_Stop As Boolean = False
    Private m_PB_Maximum As Integer = 100

    Private Delegate Sub UpdateProgressbarHandler(ByVal NewValue As Integer)

    Private Sub BW_ProgressBar_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW_ProgressBar.DoWork
        Dim i As Integer = 0
        Try
            For m_NewValue = 1 To 100
                System.Threading.Thread.Sleep(100)
                If BW_ProgressBar.CancellationPending Then
                    e.Cancel = True
                    Return
                End If

                BW_ProgressBar.ReportProgress(i, New Object() {m_NewValue})
            Next

        Catch ex As Exception
            BW_ProgressBar.CancelAsync()
            BW_ProgressBar.Dispose()
        End Try
    End Sub

    Private Sub BW_ProgressBar_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BW_ProgressBar.ProgressChanged
        If m_BackThread_Stop = False Then
            Invoke(New UpdateProgressbarHandler(AddressOf SetProgressbarValue), New Object() {m_NewValue})
        End If
    End Sub

    Private Sub BW_ProgressBar_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_ProgressBar.RunWorkerCompleted
        Try
            If e.Cancelled = False Then
                BW_ProgressBar.Dispose()
                chkCylon.Checked = False
                If m_BackThread_Stop = False Then
                    BW_ProgressBar.RunWorkerAsync()
                End If
            End If
        Catch ex As Exception
            BW_ProgressBar.Dispose()
            BW_ProgressBar.CancelAsync()
        End Try
    End Sub

    Private Sub SetProgressbarValue(ByVal value As Integer)
        chkCylon.Text = "Count " & value
        My.Application.DoEvents()

    End Sub

    Public Sub Cylon_Start()
        Try
            If BW_ProgressBar.IsBusy = False Then
                m_BackThread_Stop = False
                BW_ProgressBar.RunWorkerAsync()
            End If
        Catch ex As Exception
            BW_ProgressBar.CancelAsync()
            BW_ProgressBar.Dispose()
        End Try
    End Sub

    Public Sub Cylon_Stop()
        m_BackThread_Stop = True
        BW_ProgressBar.CancelAsync()
    End Sub

    Private Sub chkCylon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCylon.CheckedChanged
        If chkCylon.Checked = True Then
            ProgBarPlusCylon.CylonRun = True
            Cylon_Start()
        Else
            Cylon_Stop()
            ' Wait for the BackgroundWorker to finish the download.

            ProgBarPlusCylon.CylonRun = False

            chkCylon.Text = "Cylon --->"
        End If

    End Sub


#End Region 'Component Background Worker for Cylon

#Region "Runtime Background Worker for Run All"
    'Create a new Background Worker for Running a process and Updateing normal Bars too with the Cylon Bar"

    Sub StartBarThread()
        BWorker = New BackgroundWorker()

        AddHandler BWorker.DoWork, New DoWorkEventHandler(AddressOf DoWorkBars)
        AddHandler BWorker.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf OnProgressDone)

        BWorker.RunWorkerAsync()
    End Sub

    Private Sub DoWorkBars(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        For i As Int32 = 0 To Max
            System.Threading.Thread.Sleep(5)
            UpdateBars()
        Next
    End Sub

    Private Sub OnProgressDone(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        ProgBarPlusRunAll.TextValue = "Done!"
        ProgBarPlusRunAll.Refresh()
        System.Threading.Thread.Sleep(500)
        ProgBarPlusRunAll.TextValue = "Run All"
        ProgBarPlusRunAll.Value = 0
        ProgBarPlusCylon.CylonRun = False

    End Sub

    Private Sub UpdateBars()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf UpdateBars))
        Else
            ProgBarPlusFormat.Increment()
            ProgBarPlusPerc.Increment()
            ProgBarPlusTexture.Increment()
            ProgBarPlusHatch.Decrement()
            ProgBarPlusGrid1.Increment()
            ProgBarPlusGrid2.Increment()
            ProgBarPlusRunAll.Increment()
            ProgBarPlusText.Increment()
            ProgBarPlusFixedBar.Increment()
            If ProgBarPlusFixedBar.Value = ProgBarPlusFixedBar.Max Then
                ProgBarPlusFixedBar.ForeColor = Color.ForestGreen
            End If
        End If
    End Sub

#End Region 'Runtime Background Worker for Run All

    Private Sub ProgBarPlusGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles ProgBarPlusGrid1.Click, ProgBarPlusGrid2.Click
        PropertyGrid1.SelectedObject = sender
        PropertyGrid1.Refresh()

    End Sub

    Private Sub butSamples_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSamples.Click
        frmSamples.Show()
    End Sub

    Private Sub rbutCylonBar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles rbutCylonBar.CheckedChanged, rbutCylonGlider.CheckedChanged
        If sender.name = "rbutCylonBar" And sender.checked Then
            ProgBarPlusCylon.BarType = ProgBarPlus.eBarType.CylonBar
            ProgBarPlusCylon.CylonMove = 5
        ElseIf sender.name = "rbutCylonGlider" And sender.checked Then
            ProgBarPlusCylon.BarType = ProgBarPlus.eBarType.CylonGlider
            ProgBarPlusCylon.CylonMove = 1
        End If
    End Sub

End Class
