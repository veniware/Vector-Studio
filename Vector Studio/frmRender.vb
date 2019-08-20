Public Class frmRender
    Public thread As Threading.Thread
    Public logList As New List(Of logItem)

    Private selectBrush As New SolidBrush(Color.FromArgb(128, 128, 128))
    Private lightBrush As New SolidBrush(Color.FromArgb(72, 72, 72))
    Private darkBrush As New SolidBrush(Color.FromArgb(96, 96, 96))

    Enum logType
        none = 0
        err = 1
        warning = 2
        apply = 3
    End Enum

    Structure logItem
        Dim Label As String
        Dim Type As logType
    End Structure

    Public Sub addLog(Label As String, Type As Byte)
        Dim newShit As New logItem
        newShit.Label = Label
        newShit.Type = Type
        logList.Add(newShit)
        lstProgress.Items.Add(Label)

        lstProgress.SelectedIndex = lstProgress.Items.Count - 1
    End Sub


    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        If thread IsNot Nothing Then If thread.IsAlive Then thread.Abort()
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub lstProgress_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstProgress.DrawItem
        If e.Index = -1 Then Exit Sub

        If e.Index Mod 2 = 0 Then
            e.Graphics.FillRectangle(LightBrush, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height)
        Else
            e.Graphics.FillRectangle(DarkBrush, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height)
        End If

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then e.Graphics.FillRectangle(selectBrush, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height)

        Select Case logList(e.Index).Type
            Case logType.err
                e.Graphics.DrawImage(My.Resources._Error, e.Bounds.X + 8, e.Bounds.Y + 2, 20, 20)

            Case logType.warning
                e.Graphics.DrawImage(My.Resources.Warning, e.Bounds.X + 8, e.Bounds.Y + 2, 20, 20)

                'Case logType.apply
                'e.Graphics.DrawImage(My.Resources.Apply, e.Bounds.X + 8, e.Bounds.Y + 2, 20, 20)
        End Select

        e.Graphics.DrawString(logList(e.Index).Label, Me.Font, Brushes.White, e.Bounds.X + 32, e.Bounds.Y + 4)

    End Sub

End Class