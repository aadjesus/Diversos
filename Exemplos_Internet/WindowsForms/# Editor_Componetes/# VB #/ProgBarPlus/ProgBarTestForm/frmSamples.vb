Public Class frmSamples

    Private Sub frmSamples_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    End Sub

    Private Sub frmSamples_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        tmrCPU.Stop()
        ProgBarPlus22.CylonRun = False

    End Sub

    Private Sub frmSamples_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tmrCPU.Start()
        ProgBarPlus22.CylonRun = True
    End Sub

    Private Sub tmrCPU_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCPU.Tick
        UpdatePosition()
    End Sub

    Private Sub UpdatePosition()
        Dim CpuTime As Integer = Convert.ToInt32(pfcCPU.NextValue())

        mpbCPU_H.Value = CpuTime.ToString()
        mpbCPU_V.Value = CpuTime.ToString()
    End Sub

End Class