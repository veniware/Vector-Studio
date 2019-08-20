Public Class frmHtmlDialog

    Private exporter As HtmlNode
    Private lastParams As String = ""

    Public Sub New(exporter As HtmlNode)
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
                Case "HTML Mosaics" : pnlView.BackgroundImage = Html.previewMosaics(exporter, txtSize.Value, txtSize.Value - txtMargin.Value)
                Case Else : Throw New Exception("Render HTML error")
            End Select

            If temp IsNot Nothing Then temp.Dispose()
        Catch : End Try
    End Sub

    Private Sub cmdFilename_Click(sender As Object, e As EventArgs) Handles cmdFilename.Click
        Using frmSave As New SaveFileDialog

            frmSave.FileName = txtFilename.Text
            frmSave.Filter = "Hypertext Markup Language File (*.htm;*.html)|*.htm;*.html"

            If frmSave.ShowDialog = Windows.Forms.DialogResult.OK Then
                txtFilename.Text = frmSave.FileName
            End If

        End Using
    End Sub

    Private Sub txtSize_ValueChanged(sender As Object, e As EventArgs) Handles txtSize.ValueChanged
        txtMargin.Maximum = txtSize.Value - 1
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