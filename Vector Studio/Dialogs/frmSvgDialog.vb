Public Class frmSvgDialog

    Private exporter As SvgNode
    Private lastParams As String = ""

    Public Sub New(exporter As SvgNode)
        Me.exporter = exporter
        InitializeComponent()
        cmbColorFormat.SelectedIndex = 0
    End Sub

    Private Sub updatePreview()
        If exporter.link Is Nothing Then Exit Sub

        Try
            Dim temp As Image = pnlView.BackgroundImage
            pnlView.BackgroundImage = Nothing

            Select Case Me.Text
                Case "SVG Mosaics" : pnlView.BackgroundImage = Svg.previewMosaics(exporter, txtSize.Value, txtSize.Value - txtMargin.Value)
                Case "SVG Dots" : pnlView.BackgroundImage = Svg.previewDots(exporter, txtSize.Value, txtSize.Value - txtMargin.Value)
                Case "SVG Triangles" : pnlView.BackgroundImage = Svg.previewTrinagles(exporter, txtSize.Value, txtSize.Value - txtMargin.Value)
                Case "SVG Polygons" : pnlView.BackgroundImage = Svg.previewPolygons(exporter, txtSize.Value, txtSize.Value - txtMargin.Value)
                Case Else : Throw New Exception("Render SVG error")
            End Select

            If temp IsNot Nothing Then temp.Dispose()
        Catch : End Try
    End Sub

    Private Sub cmdFilename_Click(sender As Object, e As EventArgs) Handles cmdFilename.Click
        Using frmSave As New SaveFileDialog
            If chkGZip.Checked Then
                frmSave.Filter = "Compressed Scalable Vector Graphics (*.svgz)|*.svgz"
            Else
                frmSave.Filter = "Scalable Vector Graphics (*.svg)|*.svg"
            End If

            frmSave.FileName = txtFilename.Text

            If frmSave.ShowDialog = Windows.Forms.DialogResult.OK Then
                txtFilename.Text = frmSave.FileName
            End If

        End Using
    End Sub

    Private Sub txtSize_ValueChanged(sender As Object, e As EventArgs) Handles txtSize.ValueChanged
        txtMargin.Maximum = txtSize.Value - 1
    End Sub


    Private Sub chkGZip_CheckedChanged(sender As Object, e As EventArgs) Handles chkGZip.CheckedChanged
        If txtFilename.Text = "" Then Return

        If chkGZip.Checked Then
            If Not txtFilename.Text.ToLower.EndsWith("z") Then txtFilename.Text &= "z"
        Else
            If txtFilename.Text.ToLower.EndsWith("z") Then txtFilename.Text = txtFilename.Text.Substring(0, txtFilename.Text.Length - 1)
        End If
    End Sub

    Private Sub tmrUpdatePreview_Tick(sender As Object, e As EventArgs) Handles tmrUpdatePreview.Tick
        Dim params As String = txtSize.Value & "." & txtMargin.Value
        If Not lastParams = params Then
            updatePreview()
            lastParams = params
        End If
    End Sub

    Private Sub frmSvgDialog_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        updatePreview()
    End Sub
End Class