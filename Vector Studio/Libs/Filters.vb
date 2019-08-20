Imports System.Drawing.Imaging

Module Filters

    Public Function Resize(B As Image, Val As Byte) As Bitmap
        Dim w, h As Integer
        w = B.Width * Val \ 100
        h = B.Height * Val \ 100
        If w = 0 Then w = 1
        If h = 0 Then h = 1

        Dim newB As Image = New Bitmap(w, h, B.PixelFormat)
        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, 0, 0, newB.Width, newB.Height)
        End Using
        Return newB
    End Function

    Public Function Opacity(B As Image, Val As Byte) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim cm As New ColorMatrix()
        cm.Matrix33 = Val / 100
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function Grayscale(B As Bitmap, Val As Byte) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {(1 / 3) * Val / 100 + 1 * (100 - Val) / 100, (1 / 3) * Val / 100, (1 / 3) * Val / 100, 0, 0}, _
            New Single() {(1 / 3) * Val / 100, (1 / 3) * Val / 100 + 1 * (100 - Val) / 100, (1 / 3) * Val / 100, 0, 0}, _
            New Single() {(1 / 3) * Val / 100, (1 / 3) * Val / 100, (1 / 3) * Val / 100 + 1 * (100 - Val) / 100, 0, 0}, _
            New Single() {0, 0, 0, 1, 0}, _
            New Single() {0, 0, 0, 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function InvertColors(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {-1, 0, 0, 0, 0}, _
            New Single() {0, -1, 0, 0, 0}, _
            New Single() {0, 0, -1, 0, 0}, _
            New Single() {0, 0, 0, 1, 0}, _
            New Single() {1, 1, 1, 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function ScaleColors(B As Bitmap, Levels As Byte) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v1 As Single()() = {
            New Single() {Levels / 256, 0, 0, 0, 0}, _
            New Single() {0, Levels / 256, 0, 0, 0}, _
            New Single() {0, 0, Levels / 256, 0, 0}, _
            New Single() {0, 0, 0, 1, 0}, _
            New Single() {0, 0, 0, 0, 1}}

        Dim cm1 As New ColorMatrix(v1)
        Dim ia1 As New ImageAttributes
        ia1.SetColorMatrix(cm1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia1)
        End Using

        Dim v2 As Single()() = {
            New Single() {256 / Levels, 0, 0, 0, 0}, _
            New Single() {0, 256 / Levels, 0, 0, 0}, _
            New Single() {0, 0, 256 / Levels, 0, 0}, _
            New Single() {0, 0, 0, 1, 0}, _
            New Single() {0, 0, 0, 0, 1}}

        Dim cm2 As New ColorMatrix(v2)
        Dim ia2 As New ImageAttributes
        ia2.SetColorMatrix(cm2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(newB, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia2)
        End Using

        ia1.Dispose()
        ia2.Dispose()

        Return newB
    End Function

    Public Function Brightness(B As Bitmap, Val As SByte) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {1, 0, 0, 0, 0}, _
            New Single() {0, 1, 0, 0, 0}, _
            New Single() {0, 0, 1, 0, 0}, _
            New Single() {0, 0, 0, 1, 0}, _
            New Single() {Val / 100, Val / 100, Val / 100, 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function Contrast(B As Bitmap, Val As Short) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {Val / 100, 0, 0, 0, 0}, _
            New Single() {0, Val / 100, 0, 0, 0}, _
            New Single() {0, 0, Val / 100, 0, 0}, _
            New Single() {0, 0, 0, 1, 0}, _
            New Single() {(100 - Val) / 200, (100 - Val) / 200, (100 - Val) / 200, 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function Gamma(B As Bitmap, Val As Single) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim ia As New ImageAttributes
        ia.SetGamma(Val / 100)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function Normalize(B As Bitmap) As Bitmap
        Dim newB As New Bitmap(B.Width, B.Height, B.PixelFormat)

        Dim min As Byte = 255
        Dim max As Byte = 0

        Dim sampling As Bitmap
        If B.Width > 128 AndAlso B.Height > 128 Then
            sampling = New Bitmap(128, 128, B.PixelFormat)

            Using G As Graphics = Graphics.FromImage(sampling)
                G.DrawImage(B, 0, 0, sampling.Width, sampling.Height)
            End Using
        Else
            sampling = B
        End If

        Dim dp As New DirectPixel(sampling)
        dp.lock()

        For i As Integer = 0 To sampling.Width - 1
            For j As Integer = 0 To sampling.Height - 1
                Dim C As Color = dp.getPixel(i, j)

                If C.A < 127 Then Continue For

                max = Math.Max(max, C.R)
                max = Math.Max(max, C.G)
                max = Math.Max(max, C.B)

                min = Math.Min(min, C.R)
                min = Math.Min(min, C.G)
                min = Math.Min(min, C.B)
            Next
        Next

        DP.Unlock()


        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {255 / (max - min), 0, 0, 0, 0}, _
            New Single() {0, 255 / (max - min), 0, 0, 0}, _
            New Single() {0, 0, 255 / (max - min), 0, 0}, _
            New Single() {0, 0, 0, 1, 0}, _
            New Single() {-min / (max - min), -min / (max - min), -min / (max - min), 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()
        Return newB

    End Function


    Public Function getAlpha(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {1, 1, 1, 1, 1}, _
            New Single() {0, 0, 0, 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function getRed(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {1, 1, 1, 0, 1}, _
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {0, 0, 0, 1, 1}, _
            New Single() {0, 0, 0, 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function getGreen(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {1, 1, 1, 0, 1}, _
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {0, 0, 0, 1, 1}, _
            New Single() {0, 0, 0, 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function getBlue(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim v As Single()() = {
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {0, 0, 0, 0, 1}, _
            New Single() {1, 1, 1, 0, 1}, _
            New Single() {0, 0, 0, 1, 1}, _
            New Single() {0, 0, 0, 0, 1}}

        Dim cm As New ColorMatrix(v)
        Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function getCyan(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim ia As New ImageAttributes
        ia.SetOutputChannel(ColorChannelFlag.ColorChannelC)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function getMagenta(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim ia As New ImageAttributes
        ia.SetOutputChannel(ColorChannelFlag.ColorChannelM)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function getYellow(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim ia As New ImageAttributes
        ia.SetOutputChannel(ColorChannelFlag.ColorChannelY)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

    Public Function getBlack(B As Bitmap) As Bitmap
        Dim newB As Image = New Bitmap(B.Width, B.Height, B.PixelFormat)
        Dim r As New Rectangle(0, 0, B.Width, B.Height)

        Dim ia As New ImageAttributes
        ia.SetOutputChannel(ColorChannelFlag.ColorChannelK)

        Using G As Graphics = Graphics.FromImage(newB)
            G.DrawImage(B, r, 0, 0, B.Width, B.Height, GraphicsUnit.Pixel, ia)
        End Using

        ia.Dispose()

        Return newB
    End Function

End Module
