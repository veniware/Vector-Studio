Public Class NodeButton

    Property label As String
    Property color As Color
    Private hovered As Boolean = False

    Public Sub New(label As String, color As Color)
        InitializeComponent()

        Me.label = label
        Me.color = color
    End Sub

    Private Sub NoteButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        hovered = True
        Me.Invalidate()
    End Sub
    Private Sub NoteButton_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        hovered = False
        Me.Invalidate()
    End Sub

    Private Sub NoteButton_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Me.Invalidate()

        If e.Button = MouseButtons.Left Then
            frmMain.frmGhost.Text = label
            frmMain.frmGhost.Show()
            frmMain.Select()
            frmMain.frmGhost.Size = Me.Size
        End If
    End Sub

    Private Sub NoteButton_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = MouseButtons.Left Then
            If e.X - frmMain.lstTools.Width < 0 Then
                Dim o As Decimal = 0.666 + (e.X - frmMain.lstTools.Width) / 64
                If o < 0.08 Then o = 0.08
                frmMain.frmGhost.Opacity = o
            Else
                frmMain.frmGhost.Opacity = 0.666
            End If

            frmMain.frmGhost.Location = New Point(System.Windows.Forms.Cursor.Position.X + 16, System.Windows.Forms.Cursor.Position.Y + 16)

        End If
    End Sub

    Private Sub NoteButton_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Me.Invalidate()

        If e.Button = MouseButtons.Left AndAlso (Not e.X - frmMain.lstTools.Width < 0) Then
            frmMain.frmGhost.Hide()


            Dim newNote As BasicNode = Nothing

            Select Case label
                Case "Bitmap" : newNote = New BitmapLoader(label, color)
                Case "Solid color" : newNote = New SolidColor(label, color)

                Case "Export bitmap" : newNote = New ExportBitmap(label, color)

                Case "SVG Mosaics" : newNote = New SvgNode(label, color)
                Case "SVG Dots" : newNote = New SvgNode(label, color)
                Case "SVG Triangles" : newNote = New SvgNode(label, color)
                Case "SVG Polygons" : newNote = New SvgNode(label, color)

                Case "HTML Mosaics" : newNote = New HtmlNode(label, color)

                Case "Resize" : newNote = New Resize(label, color)
                Case "Opacity" : newNote = New Opacity(label, color)
                Case "Grayscale" : newNote = New Grayscale(label, color)
                Case "Invert" : newNote = New Invert(label, color)
                Case "Scale colors" : newNote = New ScaleColors(label, color)
                Case "Brightness" : newNote = New Brightness(label, color)
                Case "Contrast" : newNote = New Contrast(label, color)
                Case "Gamma" : newNote = New Gamma(label, color)
                Case "Normalize" : newNote = New Normalize(label, color)

                Case "Channel Alpha" : newNote = New ChannelA(label, color)
                Case "Channel Red" : newNote = New ChannelR(label, color)
                Case "Channel Green" : newNote = New ChannelG(label, color)
                Case "Channel Blue" : newNote = New ChannelB(label, color)
                Case "Channel Cyan" : newNote = New ChannelC(label, color)
                Case "Channel Magenta" : newNote = New ChannelM(label, color)
                Case "Channel Yellow" : newNote = New ChannelY(label, color)
                Case "Channel Key (Black)" : newNote = New ChannelK(label, color)

                Case Else
                    frmMain.frmGhost.Hide()
                    Exit Sub
            End Select

            newNote.Left = e.X - frmMain.lstTools.Width - frmMain.pnlContainer.Left
            newNote.Top = e.Y + Me.Top + frmMain.lstTools.pnlMainList.Top - frmMain.pnlContainer.Top
            frmMain.pnlContainer.Controls.Add(newNote)
            frmMain.nodes.Add(newNote)
            newNote.BringToFront()

        Else
            frmMain.frmGhost.Hide()
        End If
    End Sub

    Private Sub NoteButton_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        If hovered Then
            e.Graphics.FillRectangle(New SolidBrush(color.FromArgb(32, 32, 32)), 0, 0, Me.Width, Me.Height)
        End If

        e.Graphics.FillRectangle(New SolidBrush(color), 2, 2, 4, Me.Height - 5)

        e.Graphics.DrawString(label, Me.Font, Brushes.WhiteSmoke, 8, Me.Height \ 2, frmMain.leftStringFormat)
    End Sub
End Class
