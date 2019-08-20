Public Class HtmlNode
    Inherits BasicNode

    Public filename As String

    Public boxSize As Integer = 32
    Public boxMargin As Integer = 0
    Public colorFormat As Short = 2
    Public identificate As Boolean = False
    Public gZip As Boolean = False

    Private dialog As New frmHtmlDialog(Me)

    Public Sub New(title As String, color As Color)
        MyBase.New(title, color)

        hasInput = True
        hasOutput = False
        isExport = True
    End Sub

    Public Overrides Sub defalutFunction()
        dialog.Text = title
        dialog.txtFilename.Text = filename
        dialog.txtSize.Value = boxSize
        dialog.txtMargin.Value = boxMargin
        dialog.cmbColorFormat.SelectedIndex = colorFormat
        dialog.chkIdentificateShapes.Checked = identificate

        If dialog.ShowDialog = DialogResult.OK Then
            filename = dialog.txtFilename.Text
            boxSize = dialog.txtSize.Value
            boxMargin = dialog.txtMargin.Value
            colorFormat = dialog.cmbColorFormat.SelectedIndex
            identificate = dialog.chkIdentificateShapes.Checked
        End If
    End Sub

    Friend Overrides Sub BasicNote_Paint(sender As Object, e As PaintEventArgs)
        MyBase.BasicNote_Paint(sender, e)
        e.Graphics.DrawString(filename, Me.Font, Brushes.WhiteSmoke, New Rectangle(16, 16, Me.Width - 32, Me.Height - 24), frmMain.centerStrinfFormat)
    End Sub

End Class
