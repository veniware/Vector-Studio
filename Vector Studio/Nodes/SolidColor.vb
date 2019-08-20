Public Class SolidColor
    Inherits BasicNode

    Private colorValue As Color = Drawing.Color.Black

    Public Sub New(title As String, color As Color)
        MyBase.New(title, color)

        hasInput = False
        hasOutput = True
        isImport = True

        thumbnail = New Bitmap(48, 48, Imaging.PixelFormat.Format24bppRgb)
        Using G As Graphics = Graphics.FromImage(thumbnail)
            G.Clear(colorValue)
        End Using

        Me.updateThumbnail()
    End Sub

    Public Overrides Sub defalutFunction()
        Using frmColor As New ColorDialog
            frmColor.Color = Me.colorValue

            If frmColor.ShowDialog = Windows.Forms.DialogResult.OK Then
                colorValue = frmColor.Color

                If thumbnail IsNot Nothing Then thumbnail.Dispose()
                thumbnail = New Bitmap(48, 48, Imaging.PixelFormat.Format24bppRgb)
                Using G As Graphics = Graphics.FromImage(thumbnail)
                    G.Clear(colorValue)
                End Using

                Me.updateThumbnail()

            End If
        End Using
    End Sub

    Friend Overrides Sub BasicNote_Paint(sender As Object, e As PaintEventArgs)
        MyBase.BasicNote_Paint(sender, e)
        e.Graphics.FillRectangle(New SolidBrush(colorValue), (Me.Width - 48) \ 2, 20, 48, 48)
    End Sub

    Public Overrides Function apply(b As Image) As Image
        Dim image As New Bitmap(256, 256, Imaging.PixelFormat.Format24bppRgb)
        Using G As Graphics = Graphics.FromImage(thumbnail)
            G.Clear(color)
        End Using
        Return image
    End Function

End Class
