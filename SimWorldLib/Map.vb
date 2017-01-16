Imports System.Drawing
Imports System.Runtime.Serialization
Imports SimWorldLib.Localization

''' <summary>
''' 2D Array to store value of the World
''' </summary>
<Serializable>
<DataContract>
<System.ComponentModel.DefaultProperty("Preview")>
Public Class Map

#Region "Internal Data Properties"

    Private DArray As Single()

    <DataMember(Order:=0)>
    <System.ComponentModel.BrowsableAttribute(False)>
    Public Property Width As Integer = 1

    <DataMember(Order:=0)>
    <System.ComponentModel.BrowsableAttribute(False)>
    Public Property Height As Integer = 1

    ''' <summary>
    ''' Used by serializer and deserializer to store data array to bitmap. 
    ''' </summary>
    <DataMember(Order:=1)>
    <System.ComponentModel.BrowsableAttribute(False)>
    Public Property ArrayConvertor As Bitmap
        Get
            Dim Result As Bitmap = New Bitmap(Me.Width, Me.Height)
            Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
            Dim bmpData As Imaging.BitmapData = Result.LockBits(rect, Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format32bppArgb)
            Dim ptr As IntPtr = bmpData.Scan0
            Runtime.InteropServices.Marshal.Copy(DArray, 0, ptr, Me.Width * Me.Height)
            Result.UnlockBits(bmpData)
            Return Result
        End Get
        Private Set(ByVal value As Bitmap)
            If value.Width <> Me.Width Or value.Height <> Me.Height Then
                Throw New Exception("Bitmap size doesn't match the record. ")
                Exit Property
            End If
            Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
            Dim bmpData As Imaging.BitmapData = value.LockBits(rect, Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb)
            Dim ptr As IntPtr = bmpData.Scan0
            ReDim DArray(Width * Height - 1)
            Runtime.InteropServices.Marshal.Copy(ptr, DArray, 0, Me.Width * Me.Height)
            value.UnlockBits(bmpData)
        End Set
    End Property

    <System.ComponentModel.BrowsableAttribute(False)>
    Public Property Pixel(ByVal X As Integer, ByVal Y As Integer) As Single
        Get
            Return DArray(X * Me.Height + Y)
        End Get
        Set(value As Single)
            DArray(X * Me.Height + Y) = value
        End Set
    End Property

#End Region

#Region "Derived Properties"

    ''' <summary>
    ''' Get the sum of value in each pixel. 
    ''' </summary>
    <Description(NameOf(Sum))>
    Public ReadOnly Property Sum As Single
        Get
            Dim Ans As Single = 0
            For i As Integer = 0 To Me.Width * Me.Height - 1 Step 1
                Ans = Ans + DArray(i)
            Next
            Return Ans
        End Get
    End Property

    ''' <summary>
    ''' Get the average of value in each pixel. 
    ''' </summary>
    <Description(NameOf(Avg))>
    Public ReadOnly Property Avg As Single
        Get
            Return Me.Sum / Me.Width / Me.Height
        End Get
    End Property

#End Region

#Region "Graphics Properties"

    <DataMember>
    <Category("Graphics")>
    <Description(NameOf(Visible))>
    <System.ComponentModel.DefaultValueAttribute(True)>
    Public Property Visible As Boolean = True

    <DataMember>
    <Category("Graphics")>
    <Description(NameOf(ThemeColor))>
    Public Property ThemeColor As Color = Color.Blue

    <DataMember>
    <Category("Graphics")>
    <System.ComponentModel.DefaultValueAttribute(255)>
    <Description(NameOf(Alpha))>
    Public Property Alpha As Byte = 127

#End Region

    Public Sub New()
        ' Necessary for serialization
    End Sub

    Public Sub New(ByVal Width As Integer, ByVal Height As Integer, Optional ByVal IniVal As Single = 0)
        Me.Width = Width
        Me.Height = Height
        ReDim DArray(Me.Width * Me.Height - 1)
        For X As Integer = 0 To Me.Width - 1 Step 1
            For Y As Integer = 0 To Me.Height - 1 Step 1
                DArray(X * Me.Height + Y) = IniVal
            Next
        Next
    End Sub

    ''' <summary>
    ''' Convert <c>Map</c> into gray scale image. 
    ''' </summary>
    ''' <param name="WhiteVal">Value (and above) to be considered white. </param>
    ''' <returns>Converted gray scale image: 0 as black, WhiteVal as white. </returns>
    Public Function GrayScaleImg(Optional ByVal WhiteVal As Single = 1.0F) As Bitmap
        Dim Result As Bitmap = New Bitmap(Me.Width, Me.Height)
        Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
        Dim bmpData As Imaging.BitmapData
        ', Optional ByVal GrayScale16 As Boolean = False
        'If GrayScale16 = True Then
        'bmpData = Result.LockBits(rect, Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format16bppGrayScale)
        'Else
        bmpData = Result.LockBits(rect, Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format32bppArgb)
        'End If
        Dim ptr As IntPtr = bmpData.Scan0
        Dim TransDArray As Byte()
        ReDim TransDArray(Me.Width * Me.Height * 4 - 1)
        Dim TempByte As Byte = 0
        For i As Integer = 0 To (Me.Width * Me.Height - 1) Step 1
            If WhiteVal <= DArray(i) Then
                TempByte = 255
            Else  ' Be careful to check max value of DArray to avoid overflow
                TempByte = CByte(255 / WhiteVal * DArray(i))
            End If
            TransDArray(4 * i) = TempByte
            TransDArray(4 * i + 1) = TempByte
            TransDArray(4 * i + 2) = TempByte
            TransDArray(4 * i + 3) = Me.Alpha
        Next i
        Runtime.InteropServices.Marshal.Copy(TransDArray, 0, ptr, Me.Width * Me.Height * 4)
        Result.UnlockBits(bmpData)
        Return Result
    End Function

    ''' <summary>
    ''' Convert <c>Map</c> into colored image. 
    ''' </summary>
    ''' <param name="SatVal">Value (and above) to be considered saturated. </param>
    ''' <returns>Converted gray scale image: SatVal as <para>ColorTheme</para>, 0 as white. </returns>
    Public Function ColorImg(Optional ByVal SatVal As Single = 1.0F) As Bitmap
        Dim Result As Bitmap = New Bitmap(Me.Width, Me.Height)
        Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
        Dim bmpData As Imaging.BitmapData
        bmpData = Result.LockBits(rect, Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format32bppArgb)
        Dim ptr As IntPtr = bmpData.Scan0
        Dim TransDArray As Byte()
        ReDim TransDArray(Me.Width * Me.Height * 4 - 1)
        For i As Integer = 0 To (Me.Width * Me.Height - 1) Step 1
            If SatVal <= DArray(i) Then
                TransDArray(4 * i) = 255        'B
                TransDArray(4 * i + 1) = 255    'G
                TransDArray(4 * i + 2) = 255    'R
            Else  ' Be careful to check max value of DArray to avoid overflow
                TransDArray(4 * i) = Me.ThemeColor.B + (255 - Me.ThemeColor.B) / SatVal * DArray(i)
                TransDArray(4 * i + 1) = Me.ThemeColor.G + (255 - Me.ThemeColor.G) / SatVal * DArray(i)
                TransDArray(4 * i + 2) = Me.ThemeColor.R + (255 - Me.ThemeColor.R) / SatVal * DArray(i)
            End If
            TransDArray(4 * i + 3) = Me.Alpha
        Next i
        Runtime.InteropServices.Marshal.Copy(TransDArray, 0, ptr, Me.Width * Me.Height * 4)
        Result.UnlockBits(bmpData)
        Return Result
    End Function

End Class
