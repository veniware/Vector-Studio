Imports System.Runtime.InteropServices

Public Class DirectPixel
    Private bitmap As Bitmap
    Private bitmapData As Imaging.BitmapData
    Private bitLength As Byte

    Public bytes() As Byte

    Private isLocked As Boolean = False

    Public Sub New(bitmap As Bitmap)
        Me.bitmap = bitmap

        If bitmap.PixelFormat = Imaging.PixelFormat.Format16bppArgb1555 OrElse _
           bitmap.PixelFormat = Imaging.PixelFormat.Format32bppArgb OrElse _
           bitmap.PixelFormat = Imaging.PixelFormat.Format32bppPArgb OrElse _
           bitmap.PixelFormat = Imaging.PixelFormat.Format64bppArgb OrElse _
           bitmap.PixelFormat = Imaging.PixelFormat.Format64bppPArgb Then
            bitLength = 4
        Else
            bitLength = 3
        End If

    End Sub

    Public Sub lock()
        If isLocked Then Exit Sub

        bitmapData = bitmap.LockBits(New Rectangle(0, 0, bitmap.Width, bitmap.Height), Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat)

        ReDim bytes(bitmap.Width * bitmap.Height * bitLength - 1)
        Marshal.Copy(bitmapData.Scan0, bytes, 0, bytes.Length)

        isLocked = True
    End Sub

    Public Sub unlock()
        If Not isLocked Then Exit Sub

        Marshal.Copy(bytes, 0, bitmapData.Scan0, bytes.Length)
        bitmap.UnlockBits(bitmapData)
        isLocked = False
    End Sub

    Public Function getPixel(ByVal location As Point) As Color
        Return Me.GetPixel(location.X, location.Y)
    End Function
    Public Function getPixel(ByVal x As Integer, ByVal y As Integer) As Color
        If Not isLocked Then Exit Function

        Dim index As Integer = (y * bitmap.Width + x) * bitLength

        Dim A As Byte = 255
        Dim R As Byte
        Dim G As Byte
        Dim B As Byte

        B = bytes(index)
        G = bytes(index + 1)
        R = bytes(index + 2)
        If bitLength = 4 Then A = bytes(index + 3)

        Return Color.FromArgb(A, R, G, B)
    End Function

    Public Sub setPixel(ByVal location As Point, ByVal colour As Color)
        Me.SetPixel(location.X, location.Y, colour)
    End Sub
    Public Sub setPixel(ByVal x As Integer, ByVal y As Integer, ByVal color As Color)
        If Not isLocked Then Exit Sub

        Dim index As Integer = (y * bitmap.Width + x) * bitLength

        bytes(index) = color.B
        bytes(index + 1) = color.G
        bytes(index + 2) = color.R
        If bitLength = 4 Then bytes(index + 3) = color.A
    End Sub

End Class
