Imports System.Runtime.Serialization

<Serializable>
<DataContract>
Public Class Grid

    <DataMember>
    Public Property XMax As Integer = 0

    <DataMember>
    Public Property YMax As Integer = 0

    <DataMember>
    Private GridSize As Double = 0

    Private Grids(,)() As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByRef thisWorld As World)
        Me.GridSize = thisWorld.GridSize
        XMax = Fix(thisWorld.Size.X / Me.GridSize)
        YMax = Fix(thisWorld.Size.Y / Me.GridSize)
        ReDim Grids(XMax, YMax)
    End Sub

    Public Overloads Function ToString() As String
        Return String.Format("({0}x{1})", Me.XMax.ToString(), Me.YMax.ToString())
    End Function

    Public Overloads Function FindNearestNeighbor(ByRef Creatures As List(Of Creature), ByRef thisCreature As Creature,
                                        ByVal GridDist As Integer) As Integer
        Dim MyX As Integer = Fix(thisCreature.Position.X / GridSize)
        Dim MyY As Integer = Fix(thisCreature.Position.Y / GridSize)
        Dim MinDist As Double = 10000000000.0
        Dim MinIdx As Integer = -1
        Dim Link As Windows.Media.Media3D.Vector3D = Nothing
        ' GridDist = 0
        Debug.Print("{0},{1}", MyX, MyY)
        If Grids(MyX, MyY).Count > 1 Then  'right upper
            For Each Idx As Integer In Grids(MyX, MyY)
                Link = Creatures(Idx).Position - thisCreature.Position
                If Link.Length > 0 Then     ' eliminate thisCreature
                    If Link.Length < MinDist Then
                        MinDist = Link.Length
                        MinIdx = Idx
                    End If
                End If
            Next
            If MinIdx <> -1 Then
                Return MinIdx
            End If
        End If
        ' GridDist = 1
        For Layer As Integer = 1 To GridDist Step 1
            For T As Integer = -Layer To Layer - 1 Step 1 'Chessboard dist
                Try     ' Prevent Indexes out of bound
                    Debug.Print("{0},{1}", MyX + Layer, MyY + T)
                    For Each Idx As Integer In Grids(MyX + Layer, MyY + T)  'right upper
                        Link = Creatures(Idx).Position - thisCreature.Position
                        If Link.Length < MinDist Then
                            MinDist = Link.Length
                            MinIdx = Idx
                        End If
                    Next
                Catch ex As Exception
                    ' ignore and continue
                End Try
                Try
                    Debug.Print("{0},{1}", MyX + T, MyY - Layer)
                    For Each Idx As Integer In Grids(MyX + T, MyY - Layer) 'upper left
                        Link = Creatures(Idx).Position - thisCreature.Position
                        If Link.Length < MinDist Then
                            MinDist = Link.Length
                            MinIdx = Idx
                        End If
                    Next
                Catch ex As Exception
                    ' ignore and continue
                End Try
                Try
                    Debug.Print("{0},{1}", MyX - T, MyY + Layer)
                    For Each Idx As Integer In Grids(MyX - T, MyY + Layer)  'lower right
                        Link = Creatures(Idx).Position - thisCreature.Position
                        If Link.Length < MinDist Then
                            MinDist = Link.Length
                            MinIdx = Idx
                        End If
                    Next
                Catch ex As Exception
                    ' ignore and continue
                End Try
                Try
                    Debug.Print("{0},{1}", MyX - Layer, MyY - T)
                    For Each Idx As Integer In Grids(MyX - Layer, MyY - T)  'left lower
                        Link = Creatures(Idx).Position - thisCreature.Position
                        If Link.Length < MinDist Then
                            MinDist = Link.Length
                            MinIdx = Idx
                        End If
                    Next
                Catch ex As Exception
                    ' ignore and continue
                End Try
            Next T
            If MinIdx <> -1 Then
                Return MinIdx
            End If
        Next Layer
        Return -1 ' Nothing, No other neighbor 
    End Function

    Public Overloads Function FindNearestNeighbor(ByRef Creatures As List(Of Creature), ByRef thisCreature As Creature,
                                        ByVal TrueDist As Double) As Integer
        Dim MyX As Integer = Fix(thisCreature.Position.X / GridSize)
        Dim MyY As Integer = Fix(thisCreature.Position.Y / GridSize)
        Dim MinDist As Double = 10000000000.0
        Dim MinIdx As Integer = -1
        Dim Link As Windows.Media.Media3D.Vector3D = Nothing
        Dim GridDist As Integer = Math.Ceiling(TrueDist / GridSize) ' GridDist>=1
        ' GridDist = 0
        Debug.Print("{0},{1}", MyX, MyY)
        If Grids(MyX, MyY).Count > 1 Then  'right upper
            For Each Idx As Integer In Grids(MyX, MyY)
                Link = Creatures(Idx).Position - thisCreature.Position
                If Link.Length > 0 Then     ' eliminate thisCreature
                    If Link.Length < MinDist Then
                        MinDist = Link.Length
                        MinIdx = Idx
                    End If
                End If
            Next
            If MinIdx <> -1 And MinDist <= TrueDist Then
                Return MinIdx
            End If
        End If
        ' GridDist = 1
        For Layer As Integer = 1 To GridDist Step 1
            For T As Integer = -Layer To Layer - 1 Step 1 'Chessboard dist
                Try     ' Prevent Indexes out of bound
                    Debug.Print("{0},{1}", MyX + Layer, MyY + T)
                    For Each Idx As Integer In Grids(MyX + Layer, MyY + T)  'right upper
                        Link = Creatures(Idx).Position - thisCreature.Position
                        If Link.Length < MinDist Then
                            MinDist = Link.Length
                            MinIdx = Idx
                        End If
                    Next
                Catch ex As Exception
                    ' ignore and continue
                End Try
                Try
                    Debug.Print("{0},{1}", MyX + T, MyY - Layer)
                    For Each Idx As Integer In Grids(MyX + T, MyY - Layer) 'upper left
                        Link = Creatures(Idx).Position - thisCreature.Position
                        If Link.Length < MinDist Then
                            MinDist = Link.Length
                            MinIdx = Idx
                        End If
                    Next
                Catch ex As Exception
                    ' ignore and continue
                End Try
                Try
                    Debug.Print("{0},{1}", MyX - T, MyY + Layer)
                    For Each Idx As Integer In Grids(MyX - T, MyY + Layer)  'lower right
                        Link = Creatures(Idx).Position - thisCreature.Position
                        If Link.Length < MinDist Then
                            MinDist = Link.Length
                            MinIdx = Idx
                        End If
                    Next
                Catch ex As Exception
                    ' ignore and continue
                End Try
                Try
                    Debug.Print("{0},{1}", MyX - Layer, MyY - T)
                    For Each Idx As Integer In Grids(MyX - Layer, MyY - T)  'left lower
                        Link = Creatures(Idx).Position - thisCreature.Position
                        If Link.Length < MinDist Then
                            MinDist = Link.Length
                            MinIdx = Idx
                        End If
                    Next
                Catch ex As Exception
                    ' ignore and continue
                End Try
            Next T
            If MinIdx <> -1 And MinDist <= TrueDist Then
                Return MinIdx
            End If
        Next Layer
        Return -1 ' Nothing, No other neighbor 
    End Function

    ' Public FindAllNeighbor()

    Public Sub UpdateGrid(ByRef Creatures As List(Of Creature))
        ReDim Grids(XMax, YMax)
        Dim X As Integer = 0
        Dim Y As Integer = 0
        For i As Integer = 0 To Creatures.Count() - 1 Step 1
            X = Fix(Creatures(i).Position.X / GridSize)
            Y = Fix(Creatures(i).Position.Y / GridSize)
            If Grids(X, Y) Is Nothing Then
                Grids(X, Y) = New Integer() {i}
            Else
                ReDim Preserve Grids(X, Y)(UBound(Grids(X, Y)) + 1)
                Grids(X, Y)(UBound(Grids(X, Y))) = i
            End If
        Next i
    End Sub

End Class