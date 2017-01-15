Imports SimWorldLib.Localization

Public Module PublicFunctions

    Friend Const WINDOWSMENUOFFSET As Integer = 4 + 1
    Friend Const MAXLASTOPENENTRY As Integer = 3

    Friend MyWorld As SimWorldLib.World = Nothing

    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias _
           "GetPrivateProfileStringA" (ByVal lpApplicationName As String,
               ByVal lpKeyName As String,
               ByVal lpDefault As String,
               ByVal lpReturnedString As System.Text.StringBuilder,
               ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Public Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
                Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String,
                ByVal lpKeyName As String, ByVal lpString As String,
                ByVal lpFileName As String) As Int32

    ''' <summary>
    ''' The date and time on which the version was released.
    ''' </summary>
    ''' <param name="Version"> Convert generated <paramref name="Version"/> numbers into the date and time of release.
    ''' </param>
    Public Function BuildTime(Optional ByVal Version As Version = Nothing) As String
        'Dim Build As Integer = DateTime.Today.Subtract(New DateTime(2000, 1, 1)).Days
        'Dim Revision As Integer = CInt(DateTime.Now.Subtract(DateTime.Today).TotalSeconds) / 2
        Dim Ver As Version
        If Version Is Nothing Then
            Ver = My.Application.Info.Version
        Else
            Ver = Version
        End If
        Dim BuildTimeDT As DateTime = New DateTime(2000, 1, 1, 0, 0, 0)
        BuildTimeDT = BuildTimeDT.AddDays(Ver.Build)
        BuildTimeDT = BuildTimeDT.AddSeconds(Ver.Revision * 2)
        Return BuildTimeDT.ToString
    End Function

End Module

'Dim a As New PropertyGrid.PropertyTabCollection()

'Public Class Point3D
'    Inherits System.Windows.Media.Media3D.Point3D

'    Private _vector As Windows.Media.Media3D.Vector3D

'    <Category("Value")>
'    Public Property X As Double = 0

'    <Category("Value")>
'    Public Property Y As Double = 0

'    <Category("Value")>
'    Public Property Z As Double = 0

'    <Category("Derived")>
'    Public ReadOnly Property Norm As Double
'        Get
'            Return Math.Sqrt(X ^ 2 + Y ^ 2 + Z ^ 2)
'        End Get
'    End Property

'    Public Sub New()

'    End Sub

'    Public Sub New(x As Double, y As Double, z As Double)
'        Me.X = x
'        Me.Y = y
'        Me.Z = z
'    End Sub

'End Class

'' XML Deserializer ==================
'Dim xml_serializer As New XmlSerializer(GetType(World))
'Dim xmlContent As String = My.Computer.FileSystem.ReadAllText(Path)
'Dim string_reader As New IO.StringReader(xmlContent)
'MyWorld = DirectCast(xml_serializer.Deserialize(string_reader), World)
'string_reader.Close()