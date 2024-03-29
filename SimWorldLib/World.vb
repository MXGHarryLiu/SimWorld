﻿Imports SimWorldLib.Creature
Imports SimWorldLib.ErrMsg
Imports SimWorldLib.Localization
Imports System.Drawing
Imports System.Runtime.Serialization
Imports System.Windows.Media.Media3D

''' <summary>
''' SimWorld World Class
''' </summary>
<Serializable>
<DataContract>
<System.ComponentModel.DefaultProperty("Creatures")>
Public Class World

    Private Class DefaultValueAttribute
        Inherits System.ComponentModel.DefaultValueAttribute

        Public Sub New(ByVal item As String)
            MyBase.New(DefaultValue(item))
        End Sub
    End Class

#Region "Static or Shared"

    Private Const ABSOLUTEZEROTEMP = -273.15

    Private Shared Function DefaultValue(ByVal item As String) As Object
        Select Case item
            Case NameOf(Size)           ' Environment
                Return New Point3D(600, 400, 0)
            Case NameOf(GridSize)
                Return 50.0R
            Case NameOf(Temperature)
                Return 25.0R
            Case NameOf(DayLen)
                Return 400.0R
            Case NameOf(NightLen)
                Return 400.0R
            Case NameOf(Twilight)
                Return 100.0R
            Case NameOf(DT)             ' Simulation 
                Return 1.0R
            Case NameOf(RefreshRate)
                Return 20
            Case Else
                Return Nothing
        End Select
    End Function

#End Region

#Region "Utility Properties"

    <DataMember>
    <Category("Utility")>
    <Description(NameOf(WorldVersion))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property WorldVersion As Byte = SimWorldLib.WORLDVERSION

    <DataMember>
    <Category("Utility")>
    <Description(NameOf(CreationTime))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property CreationTime As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

    <DataMember>
    <Category("Utility")>
    <Description(NameOf(SimWorldVer))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property SimWorldVer As String = My.Application.Info.Version.ToString

    <Category("Utility")>
    <Description(NameOf(WorldFile))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property WorldFile As String = ""

    <Category("Utility")>
    <Description(NameOf(WorldFileDir))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property WorldFileDir As String = ""

    <DataMember>
    <Category("Utility")>
    <Description(NameOf(WorldLog))>
    <System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>
    <ComponentModel.Browsable(False)>
    Public Property WorldLog As Log = New Log()

#End Region

#Region "Environment Properties"

    <DataMember>
    <Category("Environment")>
    <Description(NameOf(Size))>
    <System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>
    <DefaultValueAttribute(NameOf(Size))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property Size As Windows.Media.Media3D.Point3D = DefaultValue(NameOf(Size))

    Private _Temperature As Double = DefaultValue(NameOf(Temperature))  'Celsius
    <DataMember>
    <Category("Environment")>
    <Description(NameOf(Temperature))>
    <DefaultValueAttribute(NameOf(Temperature))>
    Public Property Temperature As Double
        Get
            Return _Temperature
        End Get
        Set(value As Double)
            If value < ABSOLUTEZEROTEMP Then
                ShowErrMsg("Temperature value cannot be below the absolute zero. ")
            Else
                _Temperature = value
            End If
        End Set
    End Property

    <Category("Environment")>
    <Description(NameOf(SunPowerRatio))>
    Public ReadOnly Property SunPowerRatio As Double
        Get
            Dim TWithinDay As Double = Me.TimeofDay
            Dim MinPassTwi As Double = 0
            Dim SunPower As Double = 0
            If TWithinDay < DayLen Then
                SunPower = 1.0R
            ElseIf TWithinDay >= DayLen And TWithinDay < DayLen + Twilight Then
                MinPassTwi = TWithinDay - DayLen
                SunPower = 1.0R - MinPassTwi / Twilight
            ElseIf TWithinDay >= DayLen + Twilight And TWithinDay < DayLen + Twilight + NightLen Then
                SunPower = 0.0R
            Else
                MinPassTwi = TWithinDay - DayLen - Twilight - NightLen
                SunPower = MinPassTwi / Twilight
            End If
            Return SunPower
        End Get
    End Property

    'sec first day and night ---\___/
    <Category("Environment")>
    <Description(NameOf(DayTotal))>
    Public ReadOnly Property DayTotal As Double
        Get
            Return Me._DayLen + Me._Twilight * 2 + Me._NightLen
        End Get
    End Property

    Private _DayLen As Double = DefaultValue(NameOf(DayLen))
    <DataMember>
    <Category("Environment")>
    <Description(NameOf(DayLen))>
    <DefaultValueAttribute(NameOf(DayLen))>
    Public Property DayLen As Double
        Get
            Return _DayLen
        End Get
        Set(value As Double)
            If value <= 0 Then
                ShowErrMsg(NameOf(DayLen), ErrType.MUSTPOSITIVE)
            Else
                _DayLen = value
            End If
        End Set
    End Property

    Private _NightLen As Double = DefaultValue(NameOf(NightLen))
    <DataMember>
    <Category("Environment")>
    <Description(NameOf(NightLen))>
    <DefaultValueAttribute(NameOf(NightLen))>
    Public Property NightLen As Double
        Get
            Return _NightLen
        End Get
        Set(value As Double)
            If value <= 0 Then
                ShowErrMsg(NameOf(NightLen), ErrType.MUSTPOSITIVE)
            Else
                _NightLen = value
            End If
        End Set
    End Property

    Private _Twilight As Double = DefaultValue(NameOf(Twilight))
    <DataMember>
    <Category("Environment")>
    <Description(NameOf(Twilight))>
    <DefaultValueAttribute(NameOf(Twilight))>
    Public Property Twilight As Double
        Get
            Return _Twilight
        End Get
        Set(value As Double)
            If value <= 0 Then
                ShowErrMsg(NameOf(Twilight), ErrType.MUSTPOSITIVE)
            Else
                _Twilight = value
            End If
        End Set
    End Property

    <DataMember>
    <Category("Environment")>
    <Description(NameOf(Soil))>
    <System.ComponentModel.TypeConverter(GetType(MapConverter))>
    <System.ComponentModel.Editor(GetType(MapUIEditor), GetType(Design.UITypeEditor))>
    Public Property Soil As Map = Nothing
    'New Map(Size.X, Size.Y, BitConverter.ToSingle(BitConverter.GetBytes(&HFFFF0000), 0))

#End Region

#Region "Life Properties"

    Private _Creatures As List(Of Creature) = New List(Of Creature)
    <DataMember>
    <Category("Life")>
    <Description(NameOf(Creatures))>
    <System.ComponentModel.Editor(GetType(CreatureCollectionEditor), GetType(Design.UITypeEditor))>
    Public Property Creatures As List(Of Creature)
        Get
            Return _Creatures
        End Get
        Set(ByVal value As List(Of Creature))
            For i As Integer = 0 To value.Count - 1 Step 1
                value(i).thisWorld = Me
            Next i
            _Creatures = value
        End Set
    End Property

    <Category("Life")>
    <Description(NameOf(CreatureCount))>
    Public ReadOnly Property CreatureCount As UInteger
        Get
            Return Creatures.Count()
        End Get
    End Property

#End Region

#Region "Simulation Properties"

    Private _DT As Double = DefaultValue(NameOf(DT))  'sec
    <DataMember>
    <Category("Simulation")>
    <Description(NameOf(DT))>
    <DefaultValueAttribute(NameOf(DT))>
    Public Property DT As Double
        Get
            Return _DT
        End Get
        Set(value As Double)
            If value <= 0 Then
                ShowErrMsg(NameOf(DT), ErrType.MUSTPOSITIVE)
            Else
                _DT = value
            End If
        End Set
    End Property

    Private _T As Double = 0                    'sec
    <DataMember>
    <Category("Simulation")>
    <Description(NameOf(T))>
    Public Property T As Double
        Get
            Return _T
        End Get
        Private Set(value As Double)
            _T = value
        End Set
    End Property

    <Category("Simulation")>
    <Description(NameOf(TimeofDay))>
    Public ReadOnly Property TimeofDay As Double
        Get
            Return T Mod DayTotal
        End Get
    End Property

    Private _RefreshRate As Integer = DefaultValue(NameOf(RefreshRate))     'multiple of dT
    <DataMember>
    <Category("Simulation")>
    <Description(NameOf(RefreshRate))>
    <DefaultValueAttribute(NameOf(RefreshRate))>
    Public Property RefreshRate As Integer
        Get
            Return _RefreshRate
        End Get
        Set(ByVal value As Integer)
            If value <= 0 Then
                ShowErrMsg(NameOf(RefreshRate), ErrType.MUSTPOSITIVE)
            Else
                _RefreshRate = value
            End If
        End Set
    End Property

    Private _GridSize As Double = DefaultValue(NameOf(GridSize))
    <DataMember>
    <Category("Simulation")>
    <Description(NameOf(GridSize))>
    <DefaultValueAttribute(NameOf(GridSize))>
    Public Property GridSize As Double
        Get
            Return _GridSize
        End Get
        Set(value As Double)
            If value <= 0 Then
                ShowErrMsg(NameOf(GridSize), ErrType.MUSTPOSITIVE)
            Else
                If value <> _GridSize Then
                    _GridSize = value
                    Me.MapGrid = New Grid(Me)
                End If
            End If
        End Set
    End Property

    <DataMember>
    <Category("Simulation")>
    <Description(NameOf(MapGrid))>
    <ComponentModel.Browsable(False)>
    Public Property MapGrid As Grid

    <Category("Simulation")>
    <Description(NameOf(GridCount))>
    Public ReadOnly Property GridCount As Integer
        Get
            Return MapGrid.XMax * MapGrid.YMax
        End Get
    End Property

#End Region

#Region "Constructors, Destructor, and Clone"

    Public Sub New()
        ' Necessary for serialization
        Soil = New Map(Size.X, Size.Y, 0.5F)
        MapGrid = New Grid(Me)
    End Sub

    Public Sub New(ByVal X As Double, ByVal Y As Double, ByVal Z As Double)
        Size = New Vector3D(X, Y, Z)
        Soil = New Map(Size.X, Size.Y, 0.5F)
        MapGrid = New Grid(Me)
    End Sub

    Protected Overrides Sub Finalize()

    End Sub

    ''' <summary>
    ''' Clone the world and perform necessary updates of the utility properties. 
    ''' </summary>
    Public Function Copy() As World          'serialize and deserialize
        Dim ser = New DataContractSerializer(GetType(World))
        Dim CopyWorld As World
        Using fs As New IO.MemoryStream(), xw = Xml.XmlWriter.Create(fs)
            ser.WriteObject(xw, Me)
            xw.Flush()
            fs.Seek(0, IO.SeekOrigin.Begin)
            CopyWorld = CType(ser.ReadObject(fs), World)
        End Using
        With CopyWorld
            .WorldFileDir = Me.WorldFileDir
            .WorldFile = Me.WorldFile
            .CreationTime = DateTime.Now.ToString("yyy-MM-dd hh:mm:ss")
            .SimWorldVer = My.Application.Info.Version.ToString
        End With
        Return CopyWorld
    End Function

#End Region

    ' ============= Methods =============

    Public Function LogUserAction(ByVal Message As String) As String
        WorldLog.AddLog(DateTime.Now, Me.T, "User", Message)
        Return "User: " & Message
    End Function

    Public Sub AddCreature(Optional ByRef NewCreature As Creature = Nothing)
        If NewCreature Is Nothing Then
            NewCreature = New Creature(Me)
        End If
        NewCreature.thisWorld = Me
        Creatures.Add(NewCreature)
    End Sub

    Public Sub CreatureDeath(ByRef sender As Creature,
                             Optional ByVal DeathReason As DeathReasons = DeathReasons.UNKNOWN)
        WorldLog.AddLog(DateTime.Now, Me.T, sender.ID, "Creature death.Reason: " & DeathReason.ToString)
        Creatures.Remove(sender)
    End Sub

    Public Sub Passage()    'simulate the world for one time step
        T = T + DT
        Call MapGrid.UpdateGrid(Me.Creatures)
        Dim i As Integer = 0
        Dim LastCount As Integer = 0
        While i < Creatures.Count
            LastCount = Creatures.Count
            Creatures(i).LiveDT(Me)
            If Creatures.Count = LastCount Then ' Death can only occur once!
                i = i + 1
            Else
                Call MapGrid.UpdateGrid(Me.Creatures)
            End If
        End While
    End Sub

    Public Function DayColor() As Object
        Dim SunPower As Integer = CInt(Me.SunPowerRatio * 255)
        Return Color.FromArgb(SunPower, SunPower, SunPower)
    End Function

End Class