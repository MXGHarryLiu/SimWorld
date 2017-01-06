Imports SimWorldLib.ErrMsg
Imports SimWorldLib.Localization
Imports System.Runtime.Serialization
Imports System.Windows.Media.Media3D

''' <summary>
''' SimWorld Creature Class
''' </summary>
<Serializable>
<DataContract>
<System.ComponentModel.DefaultProperty("ID")>
Public Class Creature

    Private Class DefaultValueAttribute
        Inherits System.ComponentModel.DefaultValueAttribute

        Public Sub New(ByVal item As String)
            MyBase.New(DefaultValue(item))
        End Sub
    End Class

#Region "Static or Shared"

    '<Category("Utility")>
    Private RNG As Random = New Random()

    ''' <param name="item">Name of the property. </param>
    ''' <remarks>
    ''' Return the default value for the property named <paramref name="item"/>. 
    ''' </remarks>
    Private Shared Function DefaultValue(ByVal item As String) As Object
        Select Case item
            Case NameOf(Species)
                Return CByte(0)
            Case NameOf(Marked)
                Return False
            Case NameOf(MarkerSize)
                Return 10.0F
            Case NameOf(MaleRatio)
                Return 0.5R
            Case Else
                Return Nothing
        End Select
    End Function

    Public Enum DeathReasons As Byte
        UNKNOWN = &B0
        HUNGER = &B1
    End Enum

    'Public Shared Widening Operator CType(ByVal thisCreature As CreatureFromFile) As Creature
    '    Return
    'End Operator

#End Region

#Region "Identity Properties"

    <DataMember>
    <Category("Identity")>
    <Description(NameOf(Species))>
    <DefaultValueAttribute(NameOf(Species))>
    Public Property Species As Byte = DefaultValue(NameOf(Species))

    Private _ID As String = Guid.NewGuid().ToString()
    <DataMember>
    <Category("Identity")>
    <Description(NameOf(ID))>
    Public Property ID As String
        Get
            Return _ID
        End Get
        Private Set(ByVal value As String)
            _ID = value
        End Set
    End Property

#End Region

#Region "Graphics Properties"

    <DataMember>
    <Category("Graphics")>
    <Description(NameOf(Marked))>
    <DefaultValueAttribute(NameOf(Marked))>
    Public Property Marked As Boolean = DefaultValue(NameOf(Marked))

    Private _MarkerSize As Single = DefaultValue(NameOf(MarkerSize))
    <DataMember>
    <Category("Graphics")>
    <Description(NameOf(MarkerSize))>
    <DefaultValueAttribute(NameOf(MarkerSize))>
    Public Property MarkerSize As Single
        Get
            Return _MarkerSize
        End Get
        Set(ByVal value As Single)
            If value <= 0 Then
                ShowErrMsg(NameOf(MarkerSize), ErrType.MUSTPOSITIVE)
            Else
                _MarkerSize = value
            End If
        End Set
    End Property

#End Region

#Region "State Properties"

    <DataMember>
    <Category("State")>
    <Description(NameOf(MovingVector))>
    <System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>
    Public Property MovingVector As Vector3D = New Vector3D()  ' Normalized Vector ????????

    <DataMember>
    <Category("State")>
    <Description(NameOf(Position))>
    <System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>
    Public Property Position As Point3D = New Point3D()

    <DataMember>
    <Category("State")>
    <Description(NameOf(ActState))>
    <System.ComponentModel.TypeConverter(GetType(ActStatesConverter))>
    Public Property ActState As ActStates = New ActStates()

    <DataContract>
    <Serializable>
    <System.ComponentModel.DefaultProperty("Alive")>
    Public Class ActStates 'eating|photosynthesis|Pregnant|Sleep|Move|Alive

        <DataMember>
        <Description(NameOf(Alive))>
        <System.ComponentModel.DefaultValueAttribute(True)>
        Public Property Alive As Boolean = True

        <DataMember>
        <Description(NameOf(Move))>
        <System.ComponentModel.DefaultValueAttribute(False)>
        Public Property Move As Boolean = False

        <DataMember>
        <Description(NameOf(Sleep))>
        <System.ComponentModel.DefaultValueAttribute(False)>
        Public Property Sleep As Boolean = False

        <DataMember>
        <Description(NameOf(Pregnant))>
        <System.ComponentModel.DefaultValueAttribute(False)>
        Public Property Pregnant As Boolean = False

        <DataMember>
        <Description(NameOf(Photosynthesize))>
        <System.ComponentModel.DefaultValueAttribute(False)>
        Public Property Photosynthesize As Boolean = True

        <DataMember>
        <Description(NameOf(Eating))>
        <System.ComponentModel.DefaultValueAttribute(False)>
        Public Property Eating As Boolean = False

        Public Sub New()
            ' Necessary for serialization
        End Sub

    End Class

#End Region

#Region "Energy Properties"

    Private _BornEnergy As Double = 0   'J  as a function of weight later
    <DataMember>
    <Category("Energy")>
    <Description(NameOf(BornEnergy))>
    Public Property BornEnergy As Double
        Get
            Return _BornEnergy
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(BornEnergy), ErrType.NOTNEGATIVE)
            Else
                _BornEnergy = value
            End If
        End Set
    End Property

    Private _MaxEnergyStorage As Double = 0     'J  as a function of weight later
    <DataMember>
    <Category("Energy")>
    <Description(NameOf(MaxEnergyStorage))>
    Public Property MaxEnergyStorage As Double
        Get
            Return _MaxEnergyStorage
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(MaxEnergyStorage), ErrType.NOTNEGATIVE)
            Else
                _MaxEnergyStorage = value
            End If
        End Set
    End Property

    Private _BaseEExpend As Double = 0    'J/s as a function of weight later
    <DataMember>
    <Category("Energy")>
    <Description(NameOf(BaseEExpend))>
    Public Property BaseEExpend As Double
        Get
            Return _BaseEExpend
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(BaseEExpend), ErrType.NOTNEGATIVE)
            Else
                _BaseEExpend = value
            End If
        End Set
    End Property

    Private _PhotoSynRate As Double = 0     'J/s/brightness as a function of weight later
    <DataMember>
    <Category("Energy")>
    <Description(NameOf(PhotoSynRate))>
    Public Property PhotoSynRate As Double
        Get
            Return _PhotoSynRate
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(PhotoSynRate), ErrType.NOTNEGATIVE)
            Else
                _PhotoSynRate = value
            End If
        End Set
    End Property

    Private _EtoWRate As Double = 0       'g/J
    <DataMember>
    <Category("Energy")>
    <Description(NameOf(EtoWRate))>
    Public Property EtoWRate As Double
        Get
            Return _EtoWRate
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(EtoWRate), ErrType.NOTNEGATIVE)
            Else
                _EtoWRate = value
            End If
        End Set
    End Property

    Private _WtoERate As Double = 0           'J/g
    <DataMember>
    <Category("Energy")>
    <Description(NameOf(WtoERate))>
    Public Property WtoERate As Double
        Get
            Return _WtoERate
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(WtoERate), ErrType.NOTNEGATIVE)
            Else
                _WtoERate = value
            End If
        End Set
    End Property

    'fat storage etc.

    '=====================================================

    Private _Joule As Double                     'J
    <DataMember>
    <Category("Energy")>
    <Description(NameOf(Joule))>
    Public Property Joule As Double
        Get
            Return _Joule
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(Joule), ErrType.NOTNEGATIVE)
            ElseIf value > Me.MaxEnergyStorage And Me.MaxEnergyStorage <> 0 Then
                ShowErrMsg(NameOf(Joule), ErrType.UPPERBOUNDED, Me.MaxEnergyStorage)
            Else
                _Joule = value
            End If
        End Set
    End Property

#End Region

#Region "Reproduction Properties"

    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(Sex))>
    <System.ComponentModel.DefaultValueAttribute(True)>
    Public Property Sex As Boolean = True    'Male = True

    Private _MaleRatio As Double = DefaultValue(NameOf(MaleRatio))     '0 to 1
    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(MaleRatio))>
    <DefaultValueAttribute(NameOf(MaleRatio))>
    Public Property MaleRatio As Double
        Get
            Return _MaleRatio
        End Get
        Set(ByVal value As Double)
            If value < 0 Or value > 1 Then
                ShowErrMsg(NameOf(MaleRatio), ErrType.PROBABILITY)
            Else
                _MaleRatio = value
            End If
        End Set
    End Property

    Private _Weight As Double = 0
    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(Weight))>
    Public Property Weight As Double          'gram
        Get
            Return _Weight
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(Weight), ErrType.NOTNEGATIVE)
            Else
                _Weight = value
            End If
        End Set
    End Property

    Private _BornWeight As Double = 0           'gram     for offsprings
    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(BornWeight))>
    Public Property BornWeight As Double
        Get
            Return _BornWeight
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(BornWeight), ErrType.NOTNEGATIVE)
            Else
                _BornWeight = value
            End If
        End Set
    End Property

    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(MateReady))>
    <System.ComponentModel.DefaultValueAttribute(False)>
    Public Property MateReady As Boolean = False

    Private _MinMateAge As Double = 0.0R      'sec
    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(MinMateAge))>
    <System.ComponentModel.DefaultValueAttribute(0.0R)>
    Public Property MinMateAge As Double
        Get
            Return _MinMateAge
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(MinMateAge), ErrType.NOTNEGATIVE)
            Else
                _MinMateAge = value
            End If
        End Set
    End Property

    Private _LitterSize As Integer = 1
    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(LitterSize))>
    <System.ComponentModel.DefaultValueAttribute(1)>
    Public Property LitterSize As Integer
        Get
            Return _LitterSize
        End Get
        Set(ByVal value As Integer)
            If value <= 0 Then
                ShowErrMsg(NameOf(LitterSize), ErrType.MUSTPOSITIVE)
            Else
                _LitterSize = value
            End If
        End Set
    End Property

    Private _Age As Double = 0.0R           'sec
    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(Age))>
    <System.ComponentModel.DefaultValueAttribute(0.0R)>
    Public Property Age As Double
        Get
            Return _Age
        End Get
        Set(ByVal value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(Age), ErrType.NOTNEGATIVE)
            Else
                _Age = value
            End If
        End Set
    End Property

#End Region

#Region "Constructors, Destructor, and Clone"

    Public Sub New()
        ' Necessary for serialization
    End Sub

    Public Sub New(ByRef thisWorld As World)
        ' State Variables
        MovingVector = New Vector3D(4 * (RNG.NextDouble() - 0.5), 4 * (RNG.NextDouble() - 0.5), 0)
        Position = New Point3D(RNG.NextDouble() * thisWorld.Size.X, RNG.NextDouble() * thisWorld.Size.Y, 0)
        ' Genetic Variables

        MaxEnergyStorage = 1500
        BaseEExpend = 2
        PhotoSynRate = 0.01
        EtoWRate = 1 / 100
        WtoERate = 100
        Joule = 500 * RNG.NextDouble() + 100
        BornEnergy = 500 * RNG.NextDouble() + 100

        Weight = 1
        BornWeight = 1
        Sex = (RNG.NextDouble() > MaleRatio)
        MinMateAge = 1000
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' Clone the exact copy of the Creature (including its ID). 
    ''' </summary>
    Public Function Clone() As Creature          'serialize and deserialize
        Dim ser = New DataContractSerializer(GetType(Creature))
        Using fs As New IO.MemoryStream(), xw = Xml.XmlWriter.Create(fs)
            ser.WriteObject(xw, Me)
            xw.Flush()
            fs.Seek(0, IO.SeekOrigin.Begin)
            Return CType(ser.ReadObject(fs), Creature)
        End Using
    End Function

    ''' <summary>
    ''' Clone the world and perform necessary update of property ID. 
    ''' </summary>
    Public Function Copy() As Creature
        Dim NewCreature As Creature = Me.Clone()
        NewCreature.ID = Guid.NewGuid.ToString()
        Return NewCreature
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf (obj) Is Creature Then
            If Me.ID = TryCast(obj, Creature).ID Then
                Return True
            Else
                Return False
            End If
        End If
        Return MyBase.Equals(obj)
    End Function

    'Public Overrides Function GetHashCode() As Integer
    '    Return MyBase.GetHashCode()
    'End Function

#End Region

    ' ============= Methods =============

    Private Sub Rove(ByRef thisWorld As World)
        If ActState.Move = True Then 'rove
            ' Kalman filter!!!
            Position = Position + MovingVector * thisWorld.DT
            If Position.X < 0 Then
                Position = New Point3D(0, Position.Y, Position.Z)
            ElseIf Position.X > thisWorld.Size.X Then
                Position = New Point3D(thisWorld.Size.X, Position.Y, Position.Z)
            End If
            If Position.Y < 0 Then
                Position = New Point3D(Position.X, 0, Position.Z)
            ElseIf Position.Y > thisWorld.Size.Y Then
                Position = New Point3D(Position.X, thisWorld.Size.Y, Position.Z)
            End If
            'If Position(2) < 0 Then
            '   Position(2) = 0
            'ElseIf Position(2) > thisWorld.getZ Then
            '   Position(2) = thisWorld.getZ
            'End If
            ' Kalman filter!!!
            MovingVector = New Vector3D(4 * (RNG.NextDouble() - 0.5), 4 * (RNG.NextDouble() - 0.5), 0)
        End If
    End Sub

    Private Sub EnergyRefresh(ByRef thisWorld As World)

        If ActState.Photosynthesize = True Then
            _Joule = _Joule + thisWorld.DayColor(True) * thisWorld.DT * PhotoSynRate
        End If
        If ActState.Move = True Then

        End If

        'baseline
        _Joule = _Joule - BaseEExpend * thisWorld.DT
        If _Joule < 0 Then
            _Joule = 0
            ActState.Alive = False          'DEATH!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            thisWorld.CreatureDeath(Me, DeathReasons.HUNGER)
            Exit Sub
        ElseIf _Joule > MaxEnergyStorage Then
            Dim ESurplus As Double
            ESurplus = Joule - MaxEnergyStorage
            Weight = Weight + ESurplus * EtoWRate
            Joule = MaxEnergyStorage
        End If

    End Sub

    Private Sub ReproRefresh(ByRef thisWorld As World)

        Age = Age + thisWorld.DT

        Return
        If Age > MinMateAge Then
            MateReady = True
            MarkerSize = 15               'Debug use
        End If
        If MateReady = True Then
            'ActState = ActState + ActState.PREGNANT

            If Weight > 2 * BornWeight Then
                Dim NewBorn As Creature
                Dim NewBornPos(2) As Double
                Dim i As UInteger = 1
                For i = 1 To LitterSize Step 1
                    NewBorn = New Creature(thisWorld)
                    '.Weight = .Weight - NewBorn.Repro.BornWeight
                    'Joule = Joule - NewBorn.BornEnergy
                    'NewBornPos(0) = Position(0) + Math.Cos(RNG.NextDouble() * 2 * Math.PI) * (.Size + NewBorn.Size) / 2
                    'NewBornPos(1) = Position(1) + Math.Sin(RNG.NextDouble() * 2 * Math.PI) * (.Size + NewBorn.Size) / 2
                    'NewBornPos(2) = 0
                    'NewBorn.Pos = NewBornPos
                    thisWorld.AddCreature(NewBorn)

                Next i

            End If

        End If
    End Sub

    Public Sub LiveDT(ByRef thisWorld As World)
        Call Rove(thisWorld)
        Call EnergyRefresh(thisWorld)
        If ActState.Alive = False Then Exit Sub
        Call ReproRefresh(thisWorld)
        If ActState.Alive = False Then Exit Sub
    End Sub

End Class


''' <summary>
''' SimWorld Creature Derived Class for File Import/Export
''' </summary>
<Serializable>
<DataContract>
Public Class CreatureFromFile
    Inherits Creature

    <DataMember>
    <Category("Utility")>
    <Description(NameOf(CreatureVersion))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property CreatureVersion As Byte = SimWorldLib.CREATUREVERSION

    <DataMember>
    <Category("Utility")>
    <Description(NameOf(CreationTime))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property CreationTime As String = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")

    <DataMember>
    <Category("Utility")>
    <Description(NameOf(SimWorldVer))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    Public Property SimWorldVer As String = My.Application.Info.Version.ToString

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal thisCreature As Creature)
        MyBase.New()
        For Each inProp As Reflection.PropertyInfo In GetType(Creature).GetProperties()
            inProp.SetValue(Me, inProp.GetValue(thisCreature))
        Next
    End Sub

    Public Sub New(ByVal FilePath As String)
        MyBase.New()
        Dim thisCreature As CreatureFromFile = Nothing
        Dim ser = New DataContractSerializer(GetType(CreatureFromFile))
        Dim xmlContent As String = My.Computer.FileSystem.ReadAllText(FilePath)
        Using string_reader As New IO.StringReader(xmlContent),
            ms As New IO.MemoryStream(System.Text.Encoding.Default.GetBytes(string_reader.ReadToEnd))
            thisCreature = CType(ser.ReadObject(ms), CreatureFromFile)
        End Using
        For Each inProp As Reflection.PropertyInfo In GetType(Creature).GetProperties()
            inProp.SetValue(Me, inProp.GetValue(thisCreature))
        Next
        Me.CreatureVersion = thisCreature.CreatureVersion
        Me.CreationTime = thisCreature.CreationTime
        Me.SimWorldVer = thisCreature.SimWorldVer
    End Sub

    Public Function ToCreature() As Creature
        Dim returnedCreature As Creature = New Creature()
        For Each inProp As Reflection.PropertyInfo In GetType(Creature).GetProperties()
            inProp.SetValue(returnedCreature, inProp.GetValue(Me))
        Next
        Return returnedCreature
    End Function

End Class