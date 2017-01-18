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
                                        ByVal GridDist As Integer, ByRef NearestDist As Double) As Integer
        Dim MyX As Integer = Fix(thisCreature.Position.X / GridSize)
        Dim MyY As Integer = Fix(thisCreature.Position.Y / GridSize)
        Dim MinDist As Double = Double.PositiveInfinity
        Dim MinIdx As Integer = -1
        Dim Link As Windows.Media.Media3D.Vector3D = Nothing
        ' GridDist = 0
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
                NearestDist = MinDist
                Return MinIdx
            End If
        End If
        ' GridDist = 1
        For Layer As Integer = 1 To GridDist Step 1
            For T As Integer = -Layer To Layer - 1 Step 1 'Chessboard dist
                If MyX + Layer <= XMax And MyY + T <= YMax And 0 <= MyY + T Then    ' Prevent Indexes out of bound
                    If Grids(MyX + Layer, MyY + T) IsNot Nothing Then
                        For Each Idx As Integer In Grids(MyX + Layer, MyY + T)  'right upper
                            Link = Creatures(Idx).Position - thisCreature.Position
                            If Link.Length < MinDist Then
                                MinDist = Link.Length
                                MinIdx = Idx
                            End If
                        Next
                    End If
                End If
                If 0 <= MyX + T And MyX + T <= XMax And MyY - Layer >= 0 Then
                    If Grids(MyX + T, MyY - Layer) IsNot Nothing Then
                        For Each Idx As Integer In Grids(MyX + T, MyY - Layer) 'upper left
                            Link = Creatures(Idx).Position - thisCreature.Position
                            If Link.Length < MinDist Then
                                MinDist = Link.Length
                                MinIdx = Idx
                            End If
                        Next
                    End If
                End If
                If 0 <= MyX - T And MyX - T <= XMax And MyY + Layer <= YMax Then
                    If Grids(MyX - T, MyY + Layer) IsNot Nothing Then
                        For Each Idx As Integer In Grids(MyX - T, MyY + Layer)  'lower right
                            Link = Creatures(Idx).Position - thisCreature.Position
                            If Link.Length < MinDist Then
                                MinDist = Link.Length
                                MinIdx = Idx
                            End If
                        Next
                    End If
                End If
                If MyX - Layer >= 0 And 0 <= MyY - T And MyY - T <= YMax Then
                    If Grids(MyX - Layer, MyY - T) IsNot Nothing Then
                        For Each Idx As Integer In Grids(MyX - Layer, MyY - T)  'left lower
                            Link = Creatures(Idx).Position - thisCreature.Position
                            If Link.Length < MinDist Then
                                MinDist = Link.Length
                                MinIdx = Idx
                            End If
                        Next
                    End If
                End If
            Next T
            If MinIdx <> -1 Then
                NearestDist = MinDist
                Return MinIdx
            End If
        Next Layer
        NearestDist = Double.PositiveInfinity ' or gridsize * griddist
        Return -1 ' Nothing, No other neighbor 
    End Function

    Public Overloads Function FindNearestNeighbor(ByRef Creatures As List(Of Creature), ByRef thisCreature As Creature,
                                        ByVal TrueDist As Double, ByRef NearestDist As Double) As Integer
        Dim MyX As Integer = Fix(thisCreature.Position.X / GridSize)
        Dim MyY As Integer = Fix(thisCreature.Position.Y / GridSize)
        Dim MinDist As Double = Double.PositiveInfinity
        Dim MinIdx As Integer = -1
        Dim Link As Windows.Media.Media3D.Vector3D = Nothing
        Dim GridDist As Integer = Math.Ceiling(TrueDist / GridSize) ' GridDist>=1
        ' GridDist = 0
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
                NearestDist = MinDist
                Return MinIdx
            End If
        End If
        ' GridDist = 1
        For Layer As Integer = 1 To GridDist Step 1
            For T As Integer = -Layer To Layer - 1 Step 1 'Chessboard dist
                If MyX + Layer <= XMax And MyY + T <= YMax And 0 <= MyY + T Then    ' Prevent Indexes out of bound
                    If Grids(MyX + Layer, MyY + T) IsNot Nothing Then
                        For Each Idx As Integer In Grids(MyX + Layer, MyY + T)  'right upper
                            Link = Creatures(Idx).Position - thisCreature.Position
                            If Link.Length < MinDist Then
                                MinDist = Link.Length
                                MinIdx = Idx
                            End If
                        Next
                    End If
                End If
                If 0 <= MyX + T And MyX + T <= XMax And MyY - Layer >= 0 Then
                    If Grids(MyX + T, MyY - Layer) IsNot Nothing Then
                        For Each Idx As Integer In Grids(MyX + T, MyY - Layer) 'upper left
                            Link = Creatures(Idx).Position - thisCreature.Position
                            If Link.Length < MinDist Then
                                MinDist = Link.Length
                                MinIdx = Idx
                            End If
                        Next
                    End If
                End If
                If 0 <= MyX - T And MyX - T <= XMax And MyY + Layer <= YMax Then
                    If Grids(MyX - T, MyY + Layer) IsNot Nothing Then
                        For Each Idx As Integer In Grids(MyX - T, MyY + Layer)  'lower right
                            Link = Creatures(Idx).Position - thisCreature.Position
                            If Link.Length < MinDist Then
                                MinDist = Link.Length
                                MinIdx = Idx
                            End If
                        Next
                    End If
                End If
                If MyX - Layer >= 0 And 0 <= MyY - T And MyY - T <= YMax Then
                    If Grids(MyX - Layer, MyY - T) IsNot Nothing Then
                        For Each Idx As Integer In Grids(MyX - Layer, MyY - T)  'left lower
                            Link = Creatures(Idx).Position - thisCreature.Position
                            If Link.Length < MinDist Then
                                MinDist = Link.Length
                                MinIdx = Idx
                            End If
                        Next
                    End If
                End If
            Next T
            If MinIdx <> -1 And MinDist <= TrueDist Then
                NearestDist = MinDist
                Return MinIdx
            End If
        Next Layer
        NearestDist = Double.PositiveInfinity
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