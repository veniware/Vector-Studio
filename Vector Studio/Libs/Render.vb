Module Render

    Public Sub render(o() As Object)
        Dim nodes As List(Of BasicNode) = o(0)
        Dim frmRenderDialog As frmRender = o(1)
        Dim thread As Threading.Thread = o(2)

        Dim exports As New List(Of ExportBitmap)
        Dim svgs As New List(Of SvgNode)
        Dim htmls As New List(Of HtmlNode)

        For i As Integer = 0 To nodes.Count - 1
            If TypeOf nodes(i) Is ExportBitmap Then exports.Add(nodes(i))
            If TypeOf nodes(i) Is SvgNode Then svgs.Add(nodes(i))
            If TypeOf nodes(i) Is HtmlNode Then htmls.Add(nodes(i))
        Next

        If exports.Count = 0 AndAlso _
           svgs.Count = 0 AndAlso _
           htmls.Count = 0 Then
            frmRenderDialog.addLog("No exporters found", 1)
        End If

        frmRenderDialog.prgLoad.Maximum = exports.Count + svgs.Count + htmls.Count

        'exporters
        For i As Integer = 0 To exports.Count - 1
            Try
                frmRenderDialog.prgLoad.Value = i
                renderExporter(exports(i))
            Catch ex As Exception
                frmRenderDialog.addLog(ex.Message.Replace(vbNewLine, " "), 1)
            End Try
        Next

        'svgs
        For i As Integer = 0 To svgs.Count - 1
            Try
                frmRenderDialog.prgLoad.Value = exports.Count + i
                renderSvg(svgs(i))
            Catch ex As Exception
                frmRenderDialog.addLog(ex.Message.Replace(vbNewLine, " "), 1)
            End Try
        Next

        'html
        For i As Integer = 0 To htmls.Count - 1
            Try
                frmRenderDialog.prgLoad.Value = exports.Count + svgs.Count + i
                renderHtml(htmls(i))
            Catch ex As Exception
                frmRenderDialog.addLog(ex.Message.Replace(vbNewLine, " "), 1)
            End Try
        Next

        Dim hasErrors As Boolean = False
        For Each obj As frmRender.logItem In frmRenderDialog.logList
            If obj.Type = frmRender.logType.err OrElse obj.Type = frmRender.logType.warning Then
                hasErrors = True
                Exit For
            End If
        Next

        If hasErrors Then
            frmRenderDialog.addLog("Done", 0)
            frmRenderDialog.prgLoad.Value = frmRenderDialog.prgLoad.Maximum
            frmRenderDialog.cmdCancel.Text = "Close"
        Else
            frmRenderDialog.DialogResult = DialogResult.OK
        End If

    End Sub

    Public Sub renderExporter(exporter As ExportBitmap)
        If exporter.filename = Nothing OrElse exporter.filename = "" Then Throw New Exception("Export location is not specified")

        Dim image As Image = Nothing

        image = exporter.src()

        If image IsNot Nothing Then
            image.Save(exporter.filename, exporter.format)
        Else
            Throw New Exception("Bitmap is not specified")
        End If
    End Sub

    Public Sub renderSvg(exporter As SvgNode)
        If exporter.filename = Nothing OrElse exporter.filename = "" Then Throw New Exception("Export location is not specified")
        If exporter.src Is Nothing Then Throw New Exception("Bitmap is not specified")

        Dim bytes As Byte()

        Select Case exporter.title
            Case "SVG Mosaics" : bytes = System.Text.Encoding.UTF8.GetBytes(Svg.mosaics(exporter))
            Case "SVG Dots" : bytes = System.Text.Encoding.UTF8.GetBytes(Svg.dots(exporter))
            Case "SVG Triangles" : bytes = System.Text.Encoding.UTF8.GetBytes(Svg.triangles(exporter))
            Case "SVG Polygons" : bytes = System.Text.Encoding.UTF8.GetBytes(Svg.polygons(exporter))
            Case Else : Throw New Exception("Render SVG error")
        End Select

        If exporter.gZip Then bytes = gzipCompresstion(bytes)

        Try
            My.Computer.FileSystem.WriteAllBytes(exporter.filename, bytes, False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub renderHtml(exporter As HtmlNode)
        If exporter.filename = Nothing OrElse exporter.filename = "" Then Throw New Exception("Export location is not specified")
        If exporter.src Is Nothing Then Throw New Exception("Bitmap is not specified")

        Dim bytes As Byte()

        Select Case exporter.title
            Case "HTML Mosaics" : bytes = System.Text.Encoding.UTF8.GetBytes(Html.mosaics(exporter))
            Case Else : Throw New Exception("Render HTML error")
        End Select

        If exporter.gZip Then bytes = gzipCompresstion(bytes)

        Try
            My.Computer.FileSystem.WriteAllBytes(exporter.filename, bytes, False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function gzipCompresstion(ByVal b As Byte()) As Byte()
        Dim m As IO.MemoryStream = New IO.MemoryStream()
        Using gzip As IO.Compression.GZipStream = New IO.Compression.GZipStream(m, IO.Compression.CompressionMode.Compress, True)
            gzip.Write(b, 0, b.Length)
        End Using

        Return m.ToArray()
    End Function

End Module
