Public Class Form2

    Private Sub TrackBar1_Scroll(sender As System.Object, e As System.EventArgs) Handles TrackBar1.Scroll
        Dim sett As Integer
        sett = (TrackBar1.Value) * 3 + 90
        Form1.lejos = sett
        My.Settings.lejosface = TrackBar1.Value
        My.Settings.Save()


    End Sub

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim sett As Integer
        sett = My.Settings.lejosface
        TrackBar1.Value = sett
    End Sub
End Class