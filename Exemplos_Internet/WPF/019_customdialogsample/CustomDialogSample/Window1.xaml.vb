' Interaction logic for Window1.xaml
Partial Public Class Window1
    Inherits System.Windows.Window

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btnOne_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnOne.Click
        Dim obj As New CommonDialog.CustomDialog
        obj.AdditionalDetailsText = "These are additional details"
        obj.Buttons = CommonDialog.CustomDialogButtons.OKCancel
        obj.ButtonsDisabledDelay = 5
        obj.Caption = "The buttons are disabled for 5 seconds"
        obj.DefaultButton = CommonDialog.CustomDialogResults.OK
        obj.FooterIcon = CommonDialog.CustomDialogIcons.Information
        obj.FooterText = "This is the footer text"
        obj.InstructionHeading = "This is the instruction heading"
        obj.InstructionIcon = CommonDialog.CustomDialogIcons.Question
        obj.InstructionText = "Do you want to proceed with the next task?"
        Dim objResults As CommonDialog.CustomDialogResults = obj.Show

        Me.tbResults.Text = String.Format("Last dialog result was {0}", objResults.ToString)

    End Sub

    Private Sub btnTwo_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnTwo.Click
        Dim obj As New CommonDialog.CustomDialog
        obj.Buttons = CommonDialog.CustomDialogButtons.YesNoCancel
        obj.Caption = "Standard Dialog - default button No"
        obj.DefaultButton = CommonDialog.CustomDialogResults.No
        obj.InstructionHeading = "This is the instruction heading"
        obj.InstructionIcon = CommonDialog.CustomDialogIcons.Question
        obj.InstructionText = "Do you want to proceed with the next task?"
        Dim objResults As CommonDialog.CustomDialogResults = obj.Show

        Me.tbResults.Text = String.Format("Last dialog result was {0}", objResults.ToString)
    End Sub

    Private Sub btnThree_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnThree.Click
        Dim obj As New CommonDialog.CustomDialog
        obj.AdditionalDetailsText = "You are using a very cool WPF dialog box that works with both Vista and XP"
        obj.Buttons = CommonDialog.CustomDialogButtons.OK
        obj.Caption = "Works with Vista and XP"
        obj.DefaultButton = CommonDialog.CustomDialogResults.OK
        obj.FooterIcon = CommonDialog.CustomDialogIcons.Shield
        obj.FooterText = "This is a secure program"
        obj.InstructionHeading = "Big Brother Is Watching"
        obj.InstructionIcon = CommonDialog.CustomDialogIcons.Warning
        obj.InstructionText = "Do you want to proceed with the next task?"
        Dim objResults As CommonDialog.CustomDialogResults = obj.Show

        Me.tbResults.Text = String.Format("Last dialog result was {0}", objResults.ToString)
    End Sub
End Class
