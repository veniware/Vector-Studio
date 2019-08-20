Public Class ExportBitmap
    Inherits BasicNode

    Public filename As String
    Public format As Imaging.ImageFormat

    Public Sub New(title As String, color As Color)
        MyBase.New(title, color)

        hasInput = True
        hasOutput = False
        isExport = True
    End Sub

    Public Overrides Sub defalutFunction()
        Using frmSave As New SaveFileDialog
            frmSave.Filter = "Bitmap (*.bmp;*.dib)|*.bmp;*.dib|" & _
                             "PNG (*.png)|*.png|" & _
                             "GIF (*.gif)|*.gif|" & _
                             "JPEG (*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|" & _
                             "TIFF (*.tif;*.tiff)|*.tif;*.tiff|" & _
                             "WMF (*.wmf)|*.wmf|" & _
                             "EMF (*.emf)|*.emf"

            frmSave.FilterIndex = 2
            frmSave.FileName = filename

            If frmSave.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim file As New IO.FileInfo(frmSave.FileName)

                filename = frmSave.FileName

                Select Case file.Extension.ToLower
                    Case ".bmp", ".dib" : format = Imaging.ImageFormat.Bmp
                    Case ".png" : format = Imaging.ImageFormat.Png
                    Case ".gif" : format = Imaging.ImageFormat.Gif
                    Case ".jpg", ".jpeg", ".jpe", ".jfif" : format = Imaging.ImageFormat.Jpeg
                    Case ".tif", ".tiff" : format = Imaging.ImageFormat.Tiff
                    Case ".wmf" : format = Imaging.ImageFormat.Wmf
                    Case ".emf" : format = Imaging.ImageFormat.Emf
                End Select
            End If

        End Using
    End Sub

    Friend Overrides Sub BasicNote_Paint(sender As Object, e As PaintEventArgs)
        MyBase.BasicNote_Paint(sender, e)
        e.Graphics.DrawString(filename, Me.Font, Brushes.WhiteSmoke, New Rectangle(16, 16, Me.Width - 32, Me.Height - 24), frmMain.centerStrinfFormat)
    End Sub

End Class
