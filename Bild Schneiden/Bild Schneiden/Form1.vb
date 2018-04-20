Imports Emgu.CV
Imports Emgu.CV.Structure
Imports Emgu.Util
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO

Public Class Form1

    Dim selection As Rectangle
    Dim imagebitmap As Bitmap
    Public nomb(0 To 200, 0 To 2) As String
    Dim facewh As Integer
    Dim facex As Integer
    Dim facey As Integer
    Dim gfact As Single
    Dim sas As Integer = 0
    Dim contin As Boolean
    Public lejos As Integer = 105





    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        lejos = (My.Settings.lejosface) * 3 + 90

    End Sub


    Private Sub ListBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListBox1.DragDrop

        ListBox1.Items.Clear()
        Label2.Visible = False
        Dim Files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
        For Each FileName As String In Files
            ListBox1.Items.Add(FileName)
        Next



        Dim n As Integer
        Dim cant As Integer = ListBox1.Items.Count
        Dim dirr As String
        Dim tmpFile As System.IO.FileInfo
        Dim nstr As String
        tmpFile = My.Computer.FileSystem.GetFileInfo(ListBox1.Items.Item(0))
        dirr = tmpFile.DirectoryName
        My.Computer.FileSystem.CreateDirectory(dirr & "\recortadas\")

        For n = 0 To cant - 1

            ListBox1.Items.ToString()
            nomb(n, 1) = ListBox1.Items.Item(n)
            tmpFile = My.Computer.FileSystem.GetFileInfo(ListBox1.Items.Item(n))
            dirr = tmpFile.DirectoryName
            nstr = tmpFile.Name

            nomb(n, 2) = dirr & "\recortadas\" & nstr & "_sch.jpg"

        Next

    End Sub

    Private Sub ListBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListBox1.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub


    Private Sub clearbutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearbutton.Click

        ListBox1.Items.Clear()


    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        Dim cant As Integer = ListBox1.Items.Count
        ProgressBar1.Maximum = cant
        Dim a As Integer
        Call croppp(a)
       
    End Sub


    Sub croppp(ByVal a As Integer)
        Dim cant As Integer = ListBox1.Items.Count
        ProgressBar1.Maximum = cant
        For a = 0 To cant - 1


            Call Facedetect(a)

            PictureBox3.ImageLocation = nomb(a, 1)
            PictureBox3.Load()

            System.Threading.Thread.Sleep(1000)
            gfact = lejos / facewh
            facex = facex * gfact
            facey = facey * gfact
            Dim w As Integer = ((230 * gfact) * (PictureBox3.Image.Width)) / (PictureBox3.Image.Height)
            Dim h As Integer = 230 * gfact
            Dim bmp = New Bitmap(w, h)
            Dim sel As Rectangle
            Dim posx As Integer = (91 - facex)
            Dim posy As Integer = (161 - (facey + facewh))

            Dim g As Graphics = Graphics.FromImage(bmp)
            g.DrawImage(PictureBox3.Image, posx, posy, bmp.Width, bmp.Height)



            sel = New Rectangle(New Point(0, 0), New Size(182, 230))
            Dim crp As Bitmap = bmp.Clone(sel, bmp.PixelFormat)

            PictureBox2.Image = crp

            crp.Save(nomb(a, 2), Drawing.Imaging.ImageFormat.Jpeg)
            crp = New Bitmap(10, 10)
            bmp = New Bitmap(10, 10)

            ProgressBar1.Value = a + 1

            crp.Dispose()
            bmp.Dispose()
            g.Flush()
            g.Dispose()


        Next

    End Sub

    Sub Facedetect(ByVal d As Integer)
        Dim pic As New Bitmap(640, 480)
        Dim rectz As Drawing.Rectangle

        Dim img As New Image(Of Bgr, Byte)(nomb(d, 1))
        Dim faceDetector As New HaarCascade("C:\Emgu\emgucv-windows-x86 2.4.0.1717\bin\haarcascade_frontalface_default.xml")
        Dim imgGray As Image(Of Gray, Byte) = img.Convert(Of Gray, Byte)()

        For Each face As MCvAvgComp In faceDetector.Detect(imgGray, 1.1, 10, CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, New Size(20, 20), Size.Empty)
            img.Draw(face.rect, New Bgr(Color.White), 4)
            rectz = face.rect
        Next

        pic = img.Bitmap()
        Dim gfx As Graphics = Graphics.FromImage(pic)

        PictureBox8.Image = pic
        facewh = rectz.Width * 230 / img.Height
        facex = ((rectz.Location.X + (rectz.Width / 2)) * ((230) * (img.Width)) / (img.Height)) / (img.Width)
        facey = ((rectz.Location.Y + (rectz.Height / 2)) * 230) / img.Height

        gfx.Flush()
        gfx.Dispose()

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Form2.ShowDialog()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form3.ShowDialog()
    End Sub
End Class