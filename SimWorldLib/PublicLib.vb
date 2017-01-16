Public Module PublicFunctions

    Public Const WORLDVERSION As Byte = 1
    Public Const CREATUREVERSION As Byte = 1

    Public Culture As Globalization.CultureInfo = Nothing

    Public Function NewRNG() As System.Random
        Dim Seed As Integer = BitConverter.ToInt32(Guid.NewGuid.ToByteArray(), 0)
        Return New System.Random(Seed)
    End Function

End Module

Public Class Mutator

    Public Sub New()

    End Sub

    Public Shared Sub Mutate(ByRef thisWorld As World, ByRef Subject As Creature, ByVal CreatureProperty As Reflection.PropertyInfo, Optional ByRef Parent As Creature = Nothing)
        Dim RNG As System.Random = NewRNG()
        Select Case CreatureProperty
            Case GetType(Creature).GetProperty(NameOf(Creature.Position))
                Dim Heading As Double = RNG.NextDouble * 2 * Math.PI
                Dim NX As Double = Subject.Position.X + Subject.MarkerSize * Math.Cos(Heading)
                Dim NY As Double = Subject.Position.Y + Subject.MarkerSize * Math.Sin(Heading)
                Dim NZ As Double = Subject.Position.Z
                Dim TempPos As Windows.Media.Media3D.Point3D = New Windows.Media.Media3D.Point3D(NX, NY, NZ)
                Creature.BoundPosition(thisWorld, TempPos)
                Subject.Position = TempPos
            Case GetType(Creature).GetProperty(NameOf(Creature.MovingVector))

            Case Else
                MsgBox("Property Not Found!")
        End Select
    End Sub

End Class