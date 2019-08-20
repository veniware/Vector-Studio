Module Html

    Function mosaics(exporter As HtmlNode) As String
        Dim step_ As Integer = exporter.boxSize
        Dim size As Integer = exporter.boxSize - exporter.boxMargin

        Dim bitmap As Bitmap = exporter.src

        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim b As New System.Text.StringBuilder
        b.AppendLine("<!-- Generator: Vector Studio 1.0 -->")

        b.AppendLine("<html>")

        b.AppendLine("<head>")
        b.AppendLine("<style>")

        b.Append("#container {")
        b.Append("position:absolute;")
        b.Append("width:" & bitmap.Width & "px;")
        b.Append("height:" & bitmap.Height & "px;")
        b.Append("overflow:hidden;")
        b.AppendLine("}")

        b.Append("#container div {")
        b.Append("position:absolute;")
        b.Append("width:" & size & "px;")
        b.Append("height:" & size & "px;")
        b.AppendLine("}")

        b.AppendLine("</style>")

        b.AppendLine("</head>")

        b.AppendLine("<body>")
        b.AppendLine("<div id=""container"">")
        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                If dp.getPixel(i, j).A = 0 Then Continue For

                b.Append("<div ")
                If exporter.identificate Then b.Append("id=""i" & (j \ step_) & "_" & (i \ step_) & """ ")
                b.Append("style=""")
                b.Append("left:" & i & "px;")
                b.Append("top:" & j & "px;")
                Select Case exporter.colorFormat
                    Case 0 'hex
                        b.Append("background-color:" & colorToHex(dp.getPixel(i, j)) & ";")
                    Case 1 'rgb
                        b.Append("background-color:" & colorToRgb(dp.getPixel(i, j)) & ";")
                    Case 2 'rgba
                        b.Append("background-color:" & colorToRgba(dp.getPixel(i, j)) & ";")
                End Select
                b.Append(""">")
                b.AppendLine("</div>")
            Next
        Next
        b.AppendLine("</div>")
        b.AppendLine("</body>")

        b.AppendLine("</html>")

        dp.unlock()
        Return b.ToString
    End Function

    Function previewMosaics(exporter As HtmlNode, step_ As Integer, size As Integer) As Image
        Dim bitmap As Bitmap = exporter.src
        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim result As Image = New Bitmap(bitmap.Width, bitmap.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(result)
        'g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                If dp.getPixel(i, j).A = 0 Then Continue For
                g.FillRectangle(New SolidBrush(dp.getPixel(i, j)), i, j, size, size)
            Next
        Next

        g.Dispose()
        dp.unlock()
        Return result
    End Function

End Module
