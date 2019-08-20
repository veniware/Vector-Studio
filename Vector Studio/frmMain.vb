'
' sorry, no comments .l.
'

Public Class frmMain

    Public Shared linePen As New Pen(Brushes.Black, 3)
    Public Shared linePenLight As New Pen(New SolidBrush(Color.FromArgb(128, Color.Black)), 3)

    Public Shared isLinkDraw As Boolean = False
    Public Shared linkPoint1 As Point
    Public Shared linkPoint2 As Point

    Public Shared centerStrinfFormat As New StringFormat
    Public Shared leftStringFormat As New StringFormat
    Public Shared rightStringFormat As New StringFormat

    Public Shared nodes As New List(Of BasicNode)

    Public WithEvents frmGhost As New Form
    Sub frmGhost_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles frmGhost.Paint
        e.Graphics.DrawString(frmGhost.Text, New Font("Segoe UI", 8.25!), Brushes.White, frmGhost.Width \ 2, 12, centerStrinfFormat)
    End Sub

    Public Sub New()
        InitializeComponent()

        frmGhost.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        frmGhost.BackColor = Color.DimGray
        frmGhost.TopMost = True
        frmGhost.ShowIcon = False
        frmGhost.ShowInTaskbar = False
        frmGhost.BackgroundImageLayout = ImageLayout.Stretch

        centerStrinfFormat.Alignment = StringAlignment.Center
        centerStrinfFormat.LineAlignment = StringAlignment.Center

        leftStringFormat.Alignment = StringAlignment.Near
        leftStringFormat.LineAlignment = StringAlignment.Center

        rightStringFormat.Alignment = StringAlignment.Far
        rightStringFormat.LineAlignment = StringAlignment.Center

        lstTools.AddItem(New NodeButton("Bitmap", Color.FromArgb(0, 64, 172)))
        'lstTools.AddItem(New NodeButton("Raw bitmap", Color.FromArgb(0, 64, 172)))
        lstTools.AddItem(New NodeButton("Solid color", Color.FromArgb(0, 64, 172)))

        lstTools.AddSeparator()
        lstTools.AddItem(New NodeButton("Export bitmap", Color.FromArgb(64, 172, 0)))
        'lstTools.AddItem(New NodeButton("Export raw bitmap", Color.FromArgb(64, 172, 0)))

        lstTools.AddSeparator()
        lstTools.AddItem(New NodeButton("SVG Mosaics", Color.FromArgb(255, 172, 0)))
        lstTools.AddItem(New NodeButton("SVG Dots", Color.FromArgb(255, 172, 0)))
        lstTools.AddItem(New NodeButton("SVG Triangles", Color.FromArgb(255, 172, 0)))
        lstTools.AddItem(New NodeButton("SVG Polygons", Color.FromArgb(255, 172, 0)))
        lstTools.AddSeparator()
        lstTools.AddItem(New NodeButton("HTML Mosaics", Color.FromArgb(255, 172, 0)))

        lstTools.AddSeparator()
        lstTools.AddItem(New NodeButton("Resize", Color.FromArgb(32, 32, 32)))
        lstTools.AddItem(New NodeButton("Opacity", Color.FromArgb(32, 32, 32)))
        lstTools.AddItem(New NodeButton("Grayscale", Color.FromArgb(32, 32, 32)))
        lstTools.AddItem(New NodeButton("Invert", Color.FromArgb(32, 32, 32)))
        lstTools.AddItem(New NodeButton("Scale colors", Color.FromArgb(32, 32, 32)))
        lstTools.AddItem(New NodeButton("Brightness", Color.FromArgb(32, 32, 32)))
        lstTools.AddItem(New NodeButton("Contrast", Color.FromArgb(32, 32, 32)))
        lstTools.AddItem(New NodeButton("Gamma", Color.FromArgb(32, 32, 32)))
        lstTools.AddItem(New NodeButton("Normalize", Color.FromArgb(32, 32, 32)))

        lstTools.AddSeparator()
        lstTools.AddItem(New NodeButton("Channel Alpha", Color.FromArgb(255, 64, 0)))
        lstTools.AddItem(New NodeButton("Channel Red", Color.FromArgb(255, 64, 0)))
        lstTools.AddItem(New NodeButton("Channel Green", Color.FromArgb(255, 64, 0)))
        lstTools.AddItem(New NodeButton("Channel Blue", Color.FromArgb(255, 64, 0)))
        lstTools.AddSeparator()
        lstTools.AddItem(New NodeButton("Channel Cyan", Color.FromArgb(255, 64, 0)))
        lstTools.AddItem(New NodeButton("Channel Magenta", Color.FromArgb(255, 64, 0)))
        lstTools.AddItem(New NodeButton("Channel Yellow", Color.FromArgb(255, 64, 0)))
        lstTools.AddItem(New NodeButton("Channel Key (Black)", Color.FromArgb(255, 64, 0)), True)

        'TODO:  fix that
        'for adding logs to frmRender
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub render()

        Dim thread As New Threading.Thread(AddressOf Vector_Studio.Render.render)

        Using frmR As New frmRender
            frmR.thread = thread
            thread.Start({nodes, frmR, thread})
            frmR.ShowDialog()
        End Using
    End Sub

    Dim mp As Point
    Dim pp As Point
    Private Sub pnlContainer_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlContainer.MouseDown
        mp = Windows.Forms.Cursor.Position
        pp = pnlContainer.Location
    End Sub

    Private Sub pnlContainer_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlContainer.MouseMove
        If e.Button = MouseButtons.Middle Then
            Dim newX As Integer = pp.X + (Windows.Forms.Cursor.Position.X - mp.X)
            Dim newY As Integer = pp.Y + (Windows.Forms.Cursor.Position.Y - mp.Y)
            If newX > 0 Then newX = 0
            If newY > 0 Then newY = 0
            pnlContainer.Location = New Point(newX, newY)
        End If
    End Sub

    Private Sub pnlContainer_Paint(sender As Object, e As PaintEventArgs) Handles pnlContainer.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        For Each node As BasicNode In nodes
            If node.hasInput AndAlso node.link IsNot Nothing Then

                Dim inPoint As New Point(node.Left, node.Top + 44)
                Dim outPoint As New Point(node.link.Left + node.Width, node.link.Top + 44)

                Dim pt1, pt2, pt3, pt4 As New Point
                Dim d As Integer = (outPoint.X - inPoint.X) \ 5
                Dim d2 As Integer = (inPoint.Y - outPoint.Y) \ 10

                pt1 = inPoint
                pt4 = outPoint
                pt2 = New Point((pt1.X + pt4.X) \ 2 - d, pt1.Y - d2)
                pt3 = New Point((pt1.X + pt4.X) \ 2 + d, pt4.Y + d2)

                Dim points() As Point = {pt1, pt2, pt3, pt4}

                e.Graphics.DrawCurve(linePen, points, 0.5)
            End If
        Next

        If isLinkDraw Then
            Dim pt1, pt2, pt3, pt4 As New Point
            Dim d As Integer = (linkPoint2.X - linkPoint1.X) \ 5
            Dim d2 As Integer = (linkPoint1.Y - linkPoint2.Y) \ 10

            pt1 = linkPoint1
            pt4 = linkPoint2
            pt2 = New Point((pt1.X + pt4.X) \ 2 - d, pt1.Y - d2)
            pt3 = New Point((pt1.X + pt4.X) \ 2 + d, pt4.Y + d2)

            Dim points() As Point = {pt1, pt2, pt3, pt4}

            e.Graphics.DrawCurve(linePenLight, points, 0.5)
        End If

    End Sub

    Private Sub RenderToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RenderToolStripMenuItem1.Click
        render()
    End Sub
End Class
