Module Svg

    Function mosaics(exporter As SvgNode) As String
        Dim step_ As Integer = exporter.boxSize
        Dim size As Integer = exporter.boxSize - exporter.boxMargin

        Dim bitmap As Bitmap = exporter.src

        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim b As New System.Text.StringBuilder
        b.AppendLine("<!-- Generator: Vector Studio 1.0 -->")

        b.AppendLine("<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"" width=""" & bitmap.Width & "px"" height=""" & bitmap.Height & "px"">")

        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                If dp.getPixel(i, j).A = 0 Then Continue For

                b.Append("<polyline ")

                If exporter.identificate Then b.Append("id=""i" & (j \ step_) & "_" & (i \ step_) & """ ")

                Select Case exporter.colorFormat
                    Case 0
                        b.Append("fill=""" & colorToHex(dp.getPixel(i, j)) & """ ")
                    Case 1 'rgb
                        b.Append("fill=""" & colorToRgb(dp.getPixel(i, j)) & """ ")
                    Case 2 'rgba
                        b.Append("fill=""" & colorToRgba(dp.getPixel(i, j)) & """ ")
                End Select

                b.Append("points=""" & _
                         i & "," & j & " " & _
                         i + size & "," & j & " " & _
                         i + size & "," & j + size & " " & _
                         i & "," & j + size & "" & _
                         """")

                b.Append("/>")
            Next
        Next

        b.AppendLine()
        b.Append("</svg>")

        dp.unlock()
        Return b.ToString
    End Function

    Function dots(exporter As SvgNode) As String
        Dim step_ As Integer = exporter.boxSize
        Dim size As Integer = (exporter.boxSize - exporter.boxMargin) / 2

        Dim bitmap As Bitmap = exporter.src

        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim b As New System.Text.StringBuilder
        b.AppendLine("<!-- Generator: Vector Studio 1.0 -->")

        b.AppendLine("<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"" width=""" & bitmap.Width & "px"" height=""" & bitmap.Height & "px"">")

        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                If dp.getPixel(i, j).A = 0 Then Continue For

                b.Append("<circle ")
                If exporter.identificate Then b.Append("id=""i" & (j \ step_) & "_" & (i \ step_) & """ ")
                b.Append("cx=""" & i + size / 2 & """ cy=""" & j + size / 2 & """ ")
                b.Append("r=""" & Math.Round(size * dp.getPixel(i, j).A / 255) & """ ")

                Select Case exporter.colorFormat
                    Case 0 : b.Append("fill=""" & colorToHex(dp.getPixel(i, j)) & """ ")
                    Case 1 : b.Append("fill=""" & colorToRgb(dp.getPixel(i, j)) & """ ")
                    Case 2 : b.Append("fill=""" & colorToRgba(dp.getPixel(i, j)) & """ ")
                End Select

                b.AppendLine("/>")
            Next
        Next

        b.AppendLine()
        b.Append("</svg>")

        dp.unlock()
        Return b.ToString
    End Function

    Function triangles(exporter As SvgNode) As String
        Dim step_ As Integer = exporter.boxSize
        Dim size As Integer = exporter.boxSize - exporter.boxMargin

        Dim bitmap As Bitmap = exporter.src

        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim b As New System.Text.StringBuilder
        b.AppendLine("<!-- Generator: Vector Studio 1.0 -->")

        b.AppendLine("<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"" width=""" & bitmap.Width & "px"" height=""" & bitmap.Height & "px"">")

        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                If dp.getPixel(i, j).A = 0 Then Continue For

                If exporter.identificate Then b.Append("id=""i" & (j \ step_) & "_" & (i \ step_) & """ ")

                b.Append("<polyline ")

                Select Case exporter.colorFormat
                    Case 0 : b.Append("fill=""" & colorToHex(dp.getPixel(i, j)) & """ ")
                    Case 1 : b.Append("fill=""" & colorToRgb(dp.getPixel(i, j)) & """ ")
                    Case 2 : b.Append("fill=""" & colorToRgba(dp.getPixel(i, j)) & """ ")
                End Select

                If (i / step_ + ((j / step_) Mod 2)) Mod 2 = 0 Then
                    b.Append("points=""" & _
                             i - size / 2 - 1 & "," & j & " " & _
                             i + size * 1.5 + 1 & "," & j & " " & _
                             i + size / 2 & "," & j + size & "" & _
                             """")
                Else
                    b.Append("points=""" & _
                             i - size / 2 - 1 & "," & j + size & " " & _
                             i + size * 1.5 + 1 & "," & j + size & " " & _
                             i + size / 2 & "," & j & "" & _
                             """")
                End If



                b.Append("/>")
            Next
        Next

        b.AppendLine()
        b.Append("</svg>")

        dp.unlock()
        Return b.ToString
    End Function

    Function polygons(exporter As SvgNode) As String
        Dim step_ As Integer = exporter.boxSize
        Dim size As Integer = exporter.boxSize - exporter.boxMargin

        Dim bitmap As Bitmap = exporter.src

        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim b As New System.Text.StringBuilder
        b.AppendLine("<!-- Generator: Vector Studio 1.0 -->")

        b.AppendLine("<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"" width=""" & bitmap.Width & "px"" height=""" & bitmap.Height & "px"">")

        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                If dp.getPixel(i, j).A = 0 Then Continue For
                'TODO:
            Next
        Next

        b.AppendLine()
        b.Append("</svg>")

        dp.unlock()
        Return b.ToString
    End Function

    Function colorToHex(c As Color) As String
        Dim R, G, B As String
        R = Convert.ToString(c.R, 16)
        G = Convert.ToString(c.G, 16)
        B = Convert.ToString(c.B, 16)

        If R.Length = 1 Then R = "0" & R
        If G.Length = 1 Then R = "0" & G
        If B.Length = 1 Then R = "0" & B

        Return "#" & R & G & B
    End Function
    Function colorToRgb(c As Color) As String
        Return "rgb(" & c.R & "," & c.G & "," & c.B & ")"
    End Function
    Function colorToRgba(c As Color) As String
        Dim a As String = Math.Round(c.A / 255, 3)
        If a.Length > 1 AndAlso a.StartsWith("0") Then a = a.Substring(1, a.Length - 1)
        Return "rgba(" & c.R & "," & c.G & "," & c.B & "," & a & ")"
    End Function


    Function previewMosaics(exporter As SvgNode, step_ As Integer, size As Integer) As Image
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

    Function previewDots(exporter As SvgNode, step_ As Integer, size As Integer) As Image
        Dim bitmap As Bitmap = exporter.src
        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim result As Image = New Bitmap(bitmap.Width, bitmap.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(result)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                If dp.getPixel(i, j).A = 0 Then Continue For
                Dim r As Integer = size * dp.getPixel(i, j).A / 255
                g.FillEllipse(New SolidBrush(dp.getPixel(i, j)), i + (size - r) \ 2, j + (size - r) \ 2, r, r)
            Next
        Next

        g.Dispose()
        dp.unlock()
        Return result
    End Function

    Function previewTrinagles(exporter As SvgNode, step_ As Integer, size As Integer) As Image
        Dim bitmap As Bitmap = exporter.src
        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim result As Image = New Bitmap(bitmap.Width, bitmap.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(result)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                If dp.getPixel(i, j).A = 0 Then Continue For

                If (i / step_ + ((j / step_) Mod 2)) Mod 2 = 0 Then
                    Dim points() As Point = {New Point(i - size / 2 - 1, j), New Point(i + size * 1.5 + 1, j), New Point(i + size / 2, j + size)}
                    g.FillPolygon(New SolidBrush(dp.getPixel(i, j)), points)
                Else
                    Dim points() As Point = {New Point(i - size / 2 - 1, j + size), New Point(i + size * 1.5 + 1, j + size), New Point(i + size / 2, j)}
                    g.FillPolygon(New SolidBrush(dp.getPixel(i, j)), points)
                End If

            Next
        Next

        g.Dispose()
        dp.unlock()
        Return result
    End Function

    Function previewPolygons(exporter As SvgNode, step_ As Integer, size As Integer) As Image
        Dim bitmap As Bitmap = exporter.src
        Dim dp As New DirectPixel(bitmap)
        dp.lock()

        Dim result As Image = New Bitmap(bitmap.Width, bitmap.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(result)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        For j As Integer = 0 To bitmap.Height - 1 Step step_
            For i As Integer = 0 To bitmap.Width - 1 Step step_
                'TODO:
            Next
        Next

        g.Dispose()
        dp.unlock()
        Return result
    End Function

End Module
