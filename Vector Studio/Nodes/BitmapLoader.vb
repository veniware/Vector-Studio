Public Class BitmapLoader
    Inherits BasicNode

    Private filename As String
    Private image As Image

    Public Sub New(title As String, color As Color)
        MyBase.New(title, color)

        hasInput = False
        hasOutput = True
        isImport = True
    End Sub

    Private Sub BitmapLoader_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If image IsNot Nothing Then image.Dispose()
    End Sub


    Public Overrides Sub defalutFunction()
        Using frmOpen As New OpenFileDialog
            frmOpen.Filter = "All Picture Files(*.bmp;*.dib;*.gif;*.jpg;*.jpe;*.jpeg;*.jfif;*.png;*.tif;*.tiff;*.wmf;*.emf)|*.bmp;*.dib;*.gif;*.jpg;*.jpe;*.jpeg;*.jfif;*.png;*.tif;*.tiff;*.wmf;*.emf|" & _
                             "Bitmap (*.bmp;*.dib)|*.bmp;*.dib|" & _
                             "PNG (*.png)|*.png|" & _
                             "GIF (*.gif)|*.gif|" & _
                             "JPEG (*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|" & _
                             "TIFF (*.tif;*.tiff)|*.tif;*.tiff|" & _
                             "WMF (*.wmf)|*.wmf|" & _
                             "EMF (*.emf)|*.emf"

            If frmOpen.ShowDialog = Windows.Forms.DialogResult.OK Then

                Try
                    If image IsNot Nothing Then image.Dispose()
                    If thumbnail IsNot Nothing Then thumbnail.Dispose()

                    image = image.FromFile(frmOpen.FileName)
                    filename = frmOpen.FileName

                    If image.Width / (Me.Width - 6) < image.Height / (Me.Height - 18) Then
                        thumbnail = New Bitmap((Me.Height - 18) * image.Width \ image.Height, Me.Height - 18)
                    Else
                        thumbnail = New Bitmap(Me.Width - 6, (Me.Width - 6) * image.Height \ image.Width)
                    End If
                    Using G As Graphics = Graphics.FromImage(thumbnail)
                        G.DrawImage(image, 0, 0, thumbnail.Width, thumbnail.Height)
                    End Using
                    Me.updateThumbnail()

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If
        End Using
    End Sub

    Friend Overrides Sub BasicNote_Paint(sender As Object, e As PaintEventArgs)
        MyBase.BasicNote_Paint(sender, e)
        If thumbnail IsNot Nothing Then e.Graphics.DrawImage(thumbnail, (Me.Width - thumbnail.Width) \ 2, 15 + (Me.Height - 18 - thumbnail.Height) \ 2)
    End Sub

    Public Overrides Function apply(b As Image) As Image
        Return image
    End Function


End Class
