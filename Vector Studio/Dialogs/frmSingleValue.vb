Public Class frmSingleValue

    Private exporter As BasicNode
    Private lastParams As String = ""

    Public Sub New(exporter As BasicNode)
        Me.exporter = exporter
        InitializeComponent()

        lblValue.Text = exporter.value
    End Sub

    Private Sub updatePreview()
        If exporter.link Is Nothing Then Exit Sub

        Try
            Dim temp As Image = pnlView.BackgroundImage
            pnlView.BackgroundImage = Nothing

            exporter.value = trkValue.Value
            pnlView.BackgroundImage = exporter.src

            If temp IsNot Nothing Then temp.Dispose()
        Catch : End Try
    End Sub

    Private Sub frmSingleValue_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        updatePreview()
    End Sub

    Private Sub trkValue_Scroll(sender As Object, e As EventArgs) Handles trkValue.Scroll
        lblValue.Text = trkValue.Value
        updatePreview()
    End Sub
End Class