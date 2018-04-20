Public Class Form3

    Public rectan As Rectangle
   
    Dim waa As Integer = Image.FromFile(Form1.nomb(0, 1)).Width
    Dim haa As Integer = Image.FromFile(Form1.nomb(0, 1)).Height
    Dim t As Integer
    Dim pic As New Bitmap(Form1.nomb(0, 1))

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Timer1.Enabled = True

        rectan = New Rectangle(20, 20, 182, 230)
        waa = waa * 437 / haa

    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim posx As Integer = MousePosition.X - (182 / 2) - PictureBox1.Location.X - Me.Location.X
        Dim posy As Integer = MousePosition.Y - (230 / 2) - PictureBox1.Location.Y - Me.Location.Y
        rectan.X = posx
        rectan.Y = posy
        rectan = New Rectangle(posx, posy, rectan.Size.Width, rectan.Size.Height)
        Dim g As Graphics
        g = PictureBox1.CreateGraphics
        Dim bmp As Bitmap = New Bitmap(waa, 437)
        Using d As Graphics = Graphics.FromImage(bmp)
            d.DrawImage(pic, 0, 0, bmp.Width, bmp.Height)
            d.Dispose()
        End Using

        Dim p As Point = New Point(0, 0)
        g.DrawImage(bmp, p)
        Dim whitepen As New Pen(Color.White, 2)
        g.DrawRectangle(whitepen, rectan)

        bmp.Dispose()
        g.Flush()
        g.Dispose()

        t = t + 1
        If t > 100 Then
            Timer1.Enabled = False
        End If

    End Sub





    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        rectan.Width = rectan.Size.Width + 10
        rectan.Height = rectan.Size.Width * 230 / 182

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        rectan.Width = rectan.Size.Width - 10
        rectan.Height = rectan.Size.Width * 230 / 182

    End Sub

    Private Sub PictureBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseClick

        Timer1.Enabled = False
        Dim wa As Integer = Image.FromFile(Form1.nomb(0, 1)).Width
        Dim ha As Integer = Image.FromFile(Form1.nomb(0, 1)).Height
        Dim w As Integer = wa * 437 / ha
        Dim h As Integer = 437
        Dim posx As Integer = rectan.Location.X
        Dim posy As Integer = rectan.Location.Y

        Dim wf As Integer = w * 182 / rectan.Size.Width
        Dim hf As Integer = ha * wf / wa

        Dim posfx As Integer = posx * wf / w
        Dim posfy As Integer = posy * hf / h
        Dim bmp = New Bitmap(wf, hf)
        Dim sel As Rectangle

        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(Image.FromFile(Form1.nomb(0, 1)), 0, 0, bmp.Width, bmp.Height)

        sel = New Rectangle(New Point(posfx, posfy), New Size(182, 230))
        Dim crp As Bitmap = bmp.Clone(sel, bmp.PixelFormat)
        crp.Save(Form1.nomb(0, 2), Drawing.Imaging.ImageFormat.Jpeg)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Timer1.Enabled = True
        t = 0
    End Sub
End Class