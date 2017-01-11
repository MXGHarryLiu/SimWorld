'Imports MathNet.Numerics

Public Module PublicFunctions

    Public Const WORLDVERSION As Byte = 1
    Public Const CREATUREVERSION As Byte = 1

    Public Culture As Globalization.CultureInfo = Nothing

End Module

Public Class Mutator

    Public Sub New()
        'MsgBox(Distributions.Normal.CDF(0, 2, 0))
    End Sub

    Public Shared Sub Mutate(ByRef thisWorld As World, ByRef Subject As Creature, ByVal CreatureProperty As Reflection.PropertyInfo, Optional ByRef Parent As Creature = Nothing)
        Dim RNG As System.Random = New Random()
        Select Case CreatureProperty
            Case GetType(Creature).GetProperty(NameOf(Creature.Position))
                Dim Heading As Double = RNG.NextDouble * 2 * Math.PI
                Dim NX As Double = Subject.Position.X + Subject.MarkerSize * Math.Cos(Heading)
                Dim NY As Double = Subject.Position.Y + Subject.MarkerSize * Math.Sin(Heading)
                Dim NZ As Double = Subject.Position.Z
                Subject.Position = New Windows.Media.Media3D.Point3D(NX, NY, NZ)
                Creature.BoundPosition(thisWorld, Subject.Position)
            Case GetType(Creature).GetProperty(NameOf(Creature.MovingVector))

            Case GetType(Creature).GetProperty(NameOf(Creature.Sex))
                Subject.Sex = (RNG.NextDouble() > Subject.MaleRatio)
            Case Else
                MsgBox("Property Not Found!")
        End Select
    End Sub

End Class



