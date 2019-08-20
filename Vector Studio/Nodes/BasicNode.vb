Public Class BasicNode
    Const titleHeight As Integer = 14
    Const linkArea As Integer = 16

    Shared centerStringFormat As New StringFormat()
    Shared selectPen As Pen = New Pen(color.FromArgb(128, 255, 255, 255), 1)

    Public title As String
    Public color As Color

    Public link As BasicNode
    Public hasInput As Boolean
    Public hasOutput As Boolean

    Public isExport As Boolean
    Public isImport As Boolean

    Private isMoveOn As Boolean
    Private isLinkIn As Boolean
    Private isLinkOut As Boolean

    Friend value As Short = 100
    Friend max As Short = 100
    Friend min As Short = 0

    Friend thumbnail As Image
    Friend isHover As Boolean

    Public Sub New(title As String, color As Color)
        centerStringFormat.Alignment = StringAlignment.Center
        centerStringFormat.LineAlignment = StringAlignment.Center

        selectPen.DashStyle = Drawing2D.DashStyle.Dash

        Me.title = title
        Me.color = color

        InitializeComponent()
    End Sub

    Public Overridable Function apply(b As Image) As Image
        Return b
    End Function

    Public Overridable Sub updateThumbnail(Optional checked As List(Of Integer) = Nothing)
        If isExport Then Exit Sub

        If (hasInput And link IsNot Nothing) AndAlso (link.thumbnail IsNot Nothing) Then
            thumbnail = apply(link.thumbnail)
        Else
            'TODO: show preview if is null
        End If

        If checked Is Nothing Then checked = New List(Of Integer)
        checked.Add(Me.GetHashCode)

        If Not (hasInput AndAlso (link Is Nothing)) Then
            For Each node As BasicNode In frmMain.nodes
                If node.link Is Nothing Then Continue For
                If checked.Contains(node.GetHashCode) Then Continue For
                If node.link.GetHashCode = Me.GetHashCode Then node.updateThumbnail(checked)
            Next
        End If

        Me.Invalidate()
    End Sub

    Public Overridable Sub defalutFunction()
        Using frmValue As New frmSingleValue(Me)
            frmValue.trkValue.Minimum = min
            frmValue.trkValue.Maximum = max
            frmValue.trkValue.Value = value

            Dim temp As Short = value

            If frmValue.ShowDialog = DialogResult.OK Then
                value = frmValue.trkValue.Value
                Me.updateThumbnail()
            Else
                value = temp
            End If
        End Using
    End Sub

    Public Function src() As Image
        If Me.link Is Nothing Then Throw New Exception("Unlinked node")

        Dim path As New List(Of BasicNode)
        Dim current As BasicNode

        current = Me
        Do Until current Is Nothing
            path.Add(current)
            current = current.link
        Loop

        Dim result As Image = Nothing
        For i As Integer = path.Count - 1 To 0 Step -1
            If (result IsNot Nothing) OrElse (path(i).isImport) Then
                result = path(i).apply(result)
            End If
        Next

        Return result
    End Function

    Public Sub disconnect()
        link = Nothing

        For Each node As BasicNode In frmMain.nodes
            If node.link Is Me Then node.link = Nothing
        Next

        frmMain.pnlContainer.Invalidate()
    End Sub

    Public Sub delete()
        disconnect()
        frmMain.pnlContainer.Controls.Remove(Me)
        frmMain.nodes.Remove(Me)
    End Sub


    Private Sub BasicNode_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        If thumbnail IsNot Nothing Then thumbnail.Dispose()
    End Sub

    Private Sub BasicNote_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        Me.Invalidate()
    End Sub
    Private Sub BasicNote_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Me.Invalidate()
    End Sub

    Private Sub BasicNote_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        isHover = True
        Me.Invalidate()
    End Sub
    Private Sub BasicNote_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        isHover = False
        Me.Invalidate()
    End Sub

    Friend mp, pp As New Point
    Private Sub BasicNote_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Me.BringToFront()
        mp = Cursor.Position
        pp = Me.Location

        If (hasInput AndAlso e.X <= linkArea) Then
            isLinkIn = True
            frmMain.isLinkDraw = True
        ElseIf (hasOutput AndAlso e.X >= Me.Width - linkArea) Then
            isLinkOut = True
            frmMain.isLinkDraw = True
        Else
            isMoveOn = True
        End If
    End Sub

    Private Sub BasicNote_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.X <= linkArea AndAlso hasInput Then
            Me.Cursor = Cursors.UpArrow
        ElseIf e.X >= Me.Width - linkArea AndAlso hasOutput Then
            Me.Cursor = Cursors.UpArrow
        Else
            Me.Cursor = Cursors.Default
        End If

        If isMoveOn AndAlso e.Button = MouseButtons.Left Then
            Dim P As Point = New Point(pp.X + (System.Windows.Forms.Cursor.Position.X - mp.X), pp.Y + (System.Windows.Forms.Cursor.Position.Y - mp.Y))
            If P.X < 0 Then P.X = 0
            If P.Y < 0 Then P.Y = 0
            Me.Location = P

        ElseIf (isLinkIn OrElse isLinkOut) AndAlso e.Button = MouseButtons.Left Then
            Dim rp1 As Point
            Dim rp2 As Point

            rp1 = New Point(Math.Min(frmMain.linkPoint1.X, frmMain.linkPoint2.X) - 4, Math.Min(frmMain.linkPoint1.Y, frmMain.linkPoint2.Y) - 4)
            rp2 = New Point(Math.Max(frmMain.linkPoint1.X, frmMain.linkPoint2.X) + 4, Math.Max(frmMain.linkPoint1.Y, frmMain.linkPoint2.Y) + 4)
            frmMain.pnlContainer.Invalidate(New Rectangle(rp1.X, rp1.Y, rp2.X - rp1.X, rp2.Y - rp1.Y))

            If isLinkIn Then
                Dim inPoint As New Point(Me.Left, Me.Top + 44)
                frmMain.linkPoint1 = inPoint

            ElseIf isLinkOut Then
                Dim outPoint As New Point(Me.Left + Me.Width, Me.Top + 44)
                frmMain.linkPoint1 = outPoint
            End If

            frmMain.linkPoint2 = New Point(Me.Left + e.X, Me.Top + e.Y)

            rp1 = New Point(Math.Min(frmMain.linkPoint1.X, frmMain.linkPoint2.X) - 4, Math.Min(frmMain.linkPoint1.Y, frmMain.linkPoint2.Y) - 4)
            rp2 = New Point(Math.Max(frmMain.linkPoint1.X, frmMain.linkPoint2.X) + 4, Math.Max(frmMain.linkPoint1.Y, frmMain.linkPoint2.Y) + 4)
            frmMain.pnlContainer.Invalidate(New Rectangle(rp1.X, rp1.Y, rp2.X - rp1.X, rp2.Y - rp1.Y))
        End If

    End Sub

    Private Sub BasicNote_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        frmMain.isLinkDraw = False

        If isLinkIn Then
            Dim x As Integer = Me.Left + e.X
            Dim y As Integer = Me.Top + e.Y

            For Each note In frmMain.nodes
                If note Is Me Then Continue For

                If x > note.Left AndAlso x < note.Left + note.Width AndAlso _
                   y > note.Top AndAlso y < note.Top + note.Height Then
                    If note.hasOutput Then
                        Me.link = note
                        Me.updateThumbnail()
                        Exit For
                    End If
                End If
            Next

        ElseIf isLinkOut Then
            Dim x As Integer = Me.Left + e.X
            Dim y As Integer = Me.Top + e.Y

            For Each note As BasicNode In frmMain.nodes
                If note Is Me Then Continue For

                If x > note.Left AndAlso x < note.Left + note.Width AndAlso _
                   y > note.Top AndAlso y < note.Top + note.Height Then
                    If note.hasInput Then
                        note.link = Me
                        note.updateThumbnail()
                        Exit For
                    End If
                End If
            Next

        End If

        isMoveOn = False
        isLinkIn = False
        isLinkOut = False

        frmMain.pnlContainer.Invalidate()
    End Sub

    Private Sub BasicNote_Move(sender As Object, e As EventArgs) Handles Me.Move
        frmMain.pnlContainer.Invalidate()
    End Sub

    Private Sub BasicNote_DoubleClick(sender As Object, e As System.EventArgs) Handles Me.DoubleClick
        defalutFunction()
    End Sub
    Private Sub BasicNote_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = 13 Then defalutFunction()
    End Sub


    Friend Overridable Sub BasicNote_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.FillRectangle(New SolidBrush(color.FromArgb(64, color)), 0, 0, Me.Width, titleHeight)
        e.Graphics.DrawString(title, Me.Font, New SolidBrush(Me.ForeColor), New Rectangle(0, 0, Me.Width, titleHeight), centerStringFormat)

        If Me.Focused Then
            e.Graphics.DrawRectangle(selectPen, 1, 1, Me.Width - 3, Me.Height - 3)
        End If

        If hasInput Then
            e.Graphics.FillRectangle(New SolidBrush(color.FromArgb(64, 0, 0, 0)), -1, 34, 8, Me.Height - 57)
            e.Graphics.DrawRectangle(New Pen(New SolidBrush(color.FromArgb(32, 32, 32))), -1, 34, 8, Me.Height - 57)
        End If

        If hasOutput Then
            e.Graphics.FillRectangle(New SolidBrush(color.FromArgb(64, 0, 0, 0)), Me.Width - 8, 34, 8, Me.Height - 57)
            e.Graphics.DrawRectangle(New Pen(New SolidBrush(color.FromArgb(32, 32, 32))), Me.Width - 8, 34, 8, Me.Height - 57)
        End If

    End Sub

    Private Sub ParametersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParametersToolStripMenuItem.Click
        defalutFunction()
    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisconnectToolStripMenuItem.Click
        disconnect()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        delete()
    End Sub
End Class
