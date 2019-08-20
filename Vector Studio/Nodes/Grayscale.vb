Public Class Grayscale
    Inherits BasicNode

    Public Sub New(label As String, color As Color)
        MyBase.New(label, color)

        hasInput = True
        hasOutput = True

        value = 100
        max = 100
        min = 1
    End Sub

    Private Sub Grayscale_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If Not isHover Then Exit Sub

        If e.Delta > 0 Then
            If value < max Then value += 1
            updateThumbnail()
        ElseIf e.Delta < 0 Then
            If value > min Then value -= 1
            updateThumbnail()
        End If

    End Sub

    Friend Overrides Sub BasicNote_Paint(sender As Object, e As PaintEventArgs)
        MyBase.BasicNote_Paint(sender, e)
        If thumbnail IsNot Nothing Then e.Graphics.DrawImage(thumbnail, (Me.Width - thumbnail.Width) \ 2, 15 + (Me.Height - 18 - thumbnail.Height) \ 2)

        If isHover Then
            e.Graphics.DrawString(value & " %", Me.Font, Brushes.WhiteSmoke, Me.Width - 12, 24, frmMain.rightStringFormat)
        End If
    End Sub

    Public Overrides Function apply(b As Image) As Image
        Return Filters.Grayscale(b, value)
    End Function

End Class
