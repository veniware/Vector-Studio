Public Class Normalize
    Inherits BasicNode

    Public Sub New(label As String, color As Color)
        MyBase.New(label, color)

        hasInput = True
        hasOutput = True

        ParametersToolStripMenuItem.Visible = False
        ToolStripMenuItem1.Visible = False
    End Sub

    Public Overrides Sub defalutFunction()
    End Sub

    Friend Overrides Sub BasicNote_Paint(sender As Object, e As PaintEventArgs)
        MyBase.BasicNote_Paint(sender, e)
        If thumbnail IsNot Nothing Then e.Graphics.DrawImage(thumbnail, (Me.Width - thumbnail.Width) \ 2, 15 + (Me.Height - 18 - thumbnail.Height) \ 2)
    End Sub

    Public Overrides Function apply(b As Image) As Image
        Return Filters.Normalize(b)
    End Function

End Class
