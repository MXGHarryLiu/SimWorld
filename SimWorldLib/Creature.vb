Imports SimWorldLib.ErrMsg
Imports SimWorldLib.Localization
Imports System.Runtime.Serialization
Imports System.Windows.Media.Media3D
Imports System.Drawing

''' <summary>
''' SimWorld Creature Class
''' </summary>
<Serializable>
<DataContract>
<System.ComponentModel.DefaultProperty(NameOf(Creature.ID))>
Public Class Creature

    Private Class DefaultValueAttribute
        Inherits System.ComponentModel.DefaultValueAttribute

        Public Sub New(ByVal item As String)
            MyBase.New(DefaultValue(item))
        End Sub
    End Class

#Region "Static or Shared"

    Private RNG As System.Random = NewRNG()

    Private PregnantAge As Double = 0

    Private Decisioners As List(Of Decisioner) = New List(Of Decisioner)

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
            Case NameOf(MarkerColor)
                Return Color.Red
            Case Else
                Return Nothing
        End Select
    End Function

    Public Enum DeathReasons As Byte
        UNKNOWN = &B0
        HUNGER = &B1
        AGE = &B10
        MATERNAL = &B11
    End Enum

    'Public Shared Widening Operator CType(ByVal thisCreature As CreatureFromFile) As Creature
    '    Return
    'End Operator

#End Region

    <ComponentModel.BrowsableAttribute(False)>
    Public Property thisWorld As World = Nothing

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

    <DataMember>
    <Category("Graphics")>
    <Description(NameOf(MarkerColor))>
    <DefaultValueAttribute(NameOf(MarkerColor))>
    Public Property MarkerColor As Color = Color.Red

#End Region

#Region "Activity Properties"

    Private _MovingVector As Vector3D = New Vector3D()   ' Normalized Vector ????????
    <DataMember>
    <Category("Activity")>
    <Description(NameOf(MovingVector))>
    <System.ComponentModel.TypeConverter(GetType(StructureConverter(Of Vector3D)))>
    Public Property MovingVector As Vector3D
        Get
            Return _MovingVector
        End Get
        Set(value As Vector3D)
            _MovingVector = value
        End Set
    End Property

    Private _Position As Point3D = New Point3D()
    <DataMember>
    <Category("Activity")>
    <Description(NameOf(Position))>
    <System.ComponentModel.TypeConverter(GetType(StructureConverter(Of Point3D)))>
    Public Property Position As Point3D
        Get
            Return _Position
        End Get
        Set(ByVal value As Point3D)
            If value.X < 0 Then
                ShowErrMsg(NameOf(Position.X), ErrType.NOTNEGATIVE)
            ElseIf value.Y < 0 Then
                ShowErrMsg(NameOf(Position.Y), ErrType.NOTNEGATIVE)
            ElseIf value.Z < 0 Then
                ShowErrMsg(NameOf(Position.Z), ErrType.NOTNEGATIVE)
            Else
                _Position = value
            End If
        End Set
    End Property


    'facing direction

    <Serializable>
    <DataContract>
    <System.ComponentModel.DefaultProperty(NameOf(States.State))>
    Public Class States

        <DataMember>
        <Description(NameOf(State))>
        Public Property State As Boolean = True

        <DataMember>
        <Description(NameOf(EnergyExpense))>
        Public Property EnergyExpense As Double = 0

        Public Sub New()

        End Sub

        Public Sub New(ByVal thisState As Boolean)
            Me.State = thisState
        End Sub

        Public Shared Widening Operator CType(ByVal thisState As States) As Boolean
            Return thisState.State
        End Operator

        Public Shared Narrowing Operator CType(ByVal thisState As Boolean) As States
            Return New States(thisState)
        End Operator

        Public Shared Operator =(ByVal a As Boolean, ByVal thisState As States) As Boolean
            Return (thisState.State = a)
        End Operator

        Public Shared Operator <>(ByVal a As Boolean, ByVal thisState As States) As Boolean
            Return (thisState.State <> a)
        End Operator

        Public Overloads Function ToString() As String
            Return Me.State.ToString()
        End Function

        Public Function E() As Double
            If Me.State = True Then
                Return Me.EnergyExpense
            Else
                Return 0
            End If
        End Function

    End Class

    <DataMember>
    <Category("Activity")>
    <Description(NameOf(Alive))>
    <System.ComponentModel.TypeConverter(GetType(StatesConverter))>
    Public Property Alive As States = True

    <DataMember>
    <Category("Activity")>
    <Description(NameOf(Move))>
    <System.ComponentModel.TypeConverter(GetType(StatesConverter))>
    Public Property Move As States = False

    Friend DefaultStates As States = New States(False)

    <DataMember>
    <Category("Activity")>
    <Description(NameOf(Sleep))>
    <System.ComponentModel.TypeConverter(GetType(StatesConverter))>
    Public Property Sleep As States = False

    <DataMember>
    <Category("Activity")>
    <Description(NameOf(Pregnant))>
    <System.ComponentModel.TypeConverter(GetType(StatesConverter))>
    Public Property Pregnant As States = False

    <DataMember>
    <Category("Activity")>
    <Description(NameOf(Photosynthesize))>
    <System.ComponentModel.TypeConverter(GetType(StatesConverter))>
    Public Property Photosynthesize As States = True

    <DataMember>
    <Category("Activity")>
    <Description(NameOf(Eating))>
    <System.ComponentModel.TypeConverter(GetType(StatesConverter))>
    Public Property Eating As States = False

#End Region

#Region "Energy Properties"

    Private _BornEnergy As Double = 0   'J
    <DataMember>
    <Category("Energy")>
    <Description(NameOf(BornEnergy))>
    <GeneticApplicable(True)>
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
    <System.ComponentModel.DefaultValueAttribute(Gender.ASEXUAL)>
    <GeneticApplicable(True)>
    Public Property Sex As Gender = Gender.ASEXUAL
    Public Enum Gender As Byte
        MALE = 0
        FEMALE = 1
        ASEXUAL = 2
        HERMAPHRODITE = 3
    End Enum

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
    <GeneticApplicable(True)>
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

    Public Property Generation As Integer = 0

    Private _Lifespan As Double = Double.PositiveInfinity           'sec
    <DataMember>
    <Category("Reproduction")>
    <Description(NameOf(Lifespan))>
    <GeneticApplicable(True)>
    Public Property Lifespan As Double
        Get
            Return _Lifespan
        End Get
        Set(ByVal value As Double)
            If value <= 0 Then
                ShowErrMsg(NameOf(Lifespan), ErrType.MUSTPOSITIVE)
            Else
                _Lifespan = value
            End If
        End Set
    End Property

#End Region

#Region "Behavior Properties"

    Private _VisionDepth As Double
    <DataMember>
    <Category("Behavior")>
    <Description(NameOf(VisionDepth))>
    <DefaultValueAttribute(NameOf(VisionDepth))>
    Public Property VisionDepth As Double
        Get
            Return _VisionDepth
        End Get
        Set(value As Double)
            If value < 0 Then
                ShowErrMsg(NameOf(VisionDepth), ErrType.NOTNEGATIVE)
            Else
                _VisionDepth = value
            End If
        End Set
    End Property

    Private _Script As String = ""
    <DataMember>
    <Category("Behavior")>
    <Description(NameOf(Script))>
    <System.ComponentModel.DefaultValueAttribute("")>
    <System.ComponentModel.Editor(GetType(ComponentModel.Design.MultilineStringEditor), GetType(Design.UITypeEditor))>
    Public Property Script As String
        Get
            Return _Script
        End Get
        Set(ByVal value As String)
            Dim Commands As String() = value.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
            Dim thisDecisioner As Decisioner = Nothing
            If Decisioners IsNot Nothing Then Decisioners.Clear()
            _Script = ""
            For i As Integer = 0 To Commands.Count - 1 Step 1
                thisDecisioner = New Decisioner(Me, Me.thisWorld, Commands(i))
                If thisDecisioner.Valid = False Then
                    Continue For
                Else
                    Decisioners.Add(thisDecisioner)
                    If i = 0 Then
                        _Script = Commands(i)
                    Else
                        _Script = _Script & vbNewLine & Commands(i)
                    End If
                End If
            Next i
        End Set
    End Property

#End Region

#Region "Genetics"

    <DataMember>
    <Category("Genetics")>
    <Description(NameOf(Genome))>
    <System.ComponentModel.ReadOnlyAttribute(True)>
    <System.ComponentModel.Editor(GetType(GenomeUIEditor), GetType(Design.UITypeEditor))>
    <System.ComponentModel.TypeConverter(GetType(GenomeConverter))>
    Public Property Genome As List(Of Gene) = New List(Of Gene)

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
        Alive.EnergyExpense = 2
        Photosynthesize.EnergyExpense = -6
        EtoWRate = 1 / 100
        WtoERate = 100
        Joule = 500 * RNG.NextDouble() + 100
        Weight = 1
        BornWeight = 1
        MinMateAge = 1000
        VisionDepth = 50
        Me.Move = True
        ' Genome
        Dim CustomAttributes() As Attribute
        Dim propGeneticApplicableAttribute As GeneticApplicableAttribute = Nothing
        For Each propinfo As Reflection.PropertyInfo In GetType(Creature).GetProperties
            CustomAttributes = propinfo.GetCustomAttributes(GetType(GeneticApplicableAttribute), False)
            If CustomAttributes.Count > 0 Then
                propGeneticApplicableAttribute = CustomAttributes(0)
                If propGeneticApplicableAttribute IsNot Nothing AndAlso propGeneticApplicableAttribute.Applicable = True Then
                    Genome.Add(CreateSpeciesTemplate(propinfo.Name))
                End If
            End If
        Next
        ' New Genetics
        Lifespan = ShowPhenotype(NameOf(Lifespan))
        Sex = ShowPhenotype(NameOf(Sex))
        BornEnergy = ShowPhenotype(NameOf(BornEnergy))
        LitterSize = ShowPhenotype(NameOf(LitterSize))
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' Clone the exact copy of the Creature (including its ID). 
    ''' </summary>
    Public Function Clone() As Creature          'serialize and deserialize
        Dim ser As DataContractSerializer = New DataContractSerializer(GetType(Creature))
        Dim NewCreature As Creature = Nothing
        Using fs As New IO.MemoryStream(), xw = Xml.XmlWriter.Create(fs)
            ser.WriteObject(xw, Me)
            xw.Flush()
            fs.Seek(0, IO.SeekOrigin.Begin)
            NewCreature = CType(ser.ReadObject(fs), Creature)
        End Using
        NewCreature.RNG = NewRNG()
        Return NewCreature
    End Function

    ''' <summary>
    ''' Clone the Creature and perform necessary update of property ID. 
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

#End Region

    ' ============= Methods =============

    Private Function CreateSpeciesTemplate(ByVal PropertyName As String) As Gene
        Dim NewGene As Gene = New Gene(PropertyName)
        Select Case PropertyName
            Case NameOf(Lifespan)
                NewGene.Model = Gene.MathModels.NORMAL
                NewGene.Minimum = 0
                NewGene.SetParameters(3000, 100)
                NewGene.Perceptible = False
            Case NameOf(Sex)                                ' bisexual
                NewGene.Model = Gene.MathModels.BINARY
            Case NameOf(BornEnergy)
                NewGene.Model = Gene.MathModels.NORMAL
                NewGene.Minimum = 0
                NewGene.Maximum = Me.MaxEnergyStorage        '!!!!!!!!!!!!!!!!!!!
                NewGene.SetParameters(500, 50)
                NewGene.Perceptible = False
            Case NameOf(LitterSize)
                NewGene.Model = Gene.MathModels.UNIFORM
                NewGene.Minimum = 1
                NewGene.Maximum = 3
                NewGene.Perceptible = False
            Case Else
                ' Do nothing
        End Select
        Return NewGene
    End Function

    Private Function ShowPhenotype(ByVal Name As String, Optional DefaultValue As Double = 0) As Double
        Dim TargetGene As Gene = Me.Genome.Find(Function(x) x.Phenotype = Name)
        If TargetGene IsNot Nothing Then
            Return TargetGene.ShowPhenotype()
        End If
        Return DefaultValue
    End Function

    Public Shared Sub BoundPosition(ByRef thisWorld As World, ByRef Position As Point3D)
        If Position.X < 0 Then
            Position.X = 0
        ElseIf Position.X > thisWorld.Size.X Then
            Position.X = thisWorld.Size.X
        End If
        If Position.Y < 0 Then
            Position.Y = 0
        ElseIf Position.Y > thisWorld.Size.Y Then
            Position.Y = thisWorld.Size.Y
        End If
        If Position.Z < 0 Then
            Position.Z = 0
        ElseIf Position.Z > thisWorld.Size.Z Then
            Position.Z = thisWorld.Size.Z
        End If
    End Sub

    Private Sub Rove(ByRef thisWorld As World)
        If Me.Move = True Then 'rove
            ' Kalman filter!!!
            _Position = _Position + _MovingVector * thisWorld.DT
            BoundPosition(thisWorld, _Position)
            ' Kalman filter!!!
            _MovingVector = New Vector3D(4 * (Me.RNG.NextDouble() - 0.5), 4 * (Me.RNG.NextDouble() - 0.5), 0)
        End If
    End Sub

    Private Sub EnergyRefresh(ByRef thisWorld As World)

        _Joule = _Joule - thisWorld.SunPowerRatio * thisWorld.DT * Photosynthesize.E

        If Me.Move = True Then
            '_Joule = _Joule - 1 * Weight * MovingVector.Length
        End If

        _Joule = _Joule - Alive.E * thisWorld.DT

        If _Joule < 0 Then
            _Joule = 0
            Me.Alive = False
            thisWorld.CreatureDeath(Me, DeathReasons.HUNGER)
            Exit Sub
        ElseIf _Joule > MaxEnergyStorage Then
            Dim ESurplus As Double = _Joule - MaxEnergyStorage
            Weight = Weight + ESurplus * EtoWRate
            _Joule = MaxEnergyStorage
        End If

    End Sub

    Private Sub ReproRefresh(ByRef thisWorld As World)
        MarkerSize = CSng(Me.Age / 50 + 1)   'Debug use !!!!!!!!!!!!!!!!!!!!!!!!!!
        If Age >= MinMateAge Then
            MateReady = True
            Marked = True                    'Debug use !!!!!!!!!!!!!!!!!!!!!!!!!!
        End If
        If MateReady = False Then
            Exit Sub
        End If
        If Me.Pregnant = True And Me.Age - PregnantAge > 100 Then ' Magic number!!!
            If Weight >= (LitterSize + 1) * BornWeight Then     ' Deliver new born 'MAGIC NUMBER
                Me.Pregnant = False
                Dim NewBorn As Creature = Nothing
                Dim NewBornPos(2) As Double
                For i As Integer = 1 To LitterSize Step 1
                    _Joule = _Joule - BornEnergy
                    If _Joule < 0 Then
                        _Joule = 0
                        Me.Alive = False
                        thisWorld.CreatureDeath(Me, DeathReasons.MATERNAL)
                        Exit Sub
                    End If
                    NewBorn = Me.Copy() ' finally replaced by empty creature
                    Mutator.Mutate(thisWorld, NewBorn, GetType(Creature).GetProperty(NameOf(Position)))
                    Me.Weight = Me.Weight - Me.BornWeight
                    With NewBorn
                        .Age = 0
                        .Joule = Me.BornEnergy
                        .Weight = Me.BornWeight
                        .MateReady = False
                        .Marked = False          'debug use
                        '.Photosynthesize = True
                        .Lifespan = .ShowPhenotype(NameOf(Lifespan))
                        .Sex = .ShowPhenotype(NameOf(Sex))
                        .BornEnergy = .ShowPhenotype(NameOf(BornEnergy))
                        .LitterSize = .ShowPhenotype(NameOf(LitterSize)) ' A decision?????
                    End With
                    thisWorld.AddCreature(NewBorn)
                    thisWorld.WorldLog.AddLog(DateTime.Now, thisWorld.T, NewBorn.ID,
                                                  String.Format("Creature is born by {0}.", Me.ID))
                    ' Prepare for next birth
                    'BornEnergy = ShowPhenotype(NameOf(BornEnergy))      ' A decision?????
                Next i
            End If
            Exit Sub
        ElseIf Me.Pregnant = False And MateReady = True Then
            Select Case Me.Sex      'Find mate
                Case Gender.ASEXUAL
                    Exit Sub
                Case Gender.FEMALE
                    Dim NearestDist As Double = Double.PositiveInfinity
                    Dim NeigborIdx As Integer = thisWorld.MapGrid.FindNearestNeighbor(thisWorld.Creatures, Me, VisionDepth, NearestDist)
                    If NeigborIdx <> -1 And NearestDist < 2 * Me.MarkerSize Then        '!!!!!!!!!!!!!!!!!!!!
                        If thisWorld.Creatures(NeigborIdx).Sex = Gender.MALE And thisWorld.Creatures(NeigborIdx).MateReady = True Then
                            Me.Pregnant = True  'immediately pregnanted
                            PregnantAge = Me.Age
                        End If
                    End If
                Case Gender.MALE
                ' ???????????????????????
                Case Gender.HERMAPHRODITE
                    ' ???????????????????????
                Case Else
                    ' ERROR: Do nothing 
            End Select
        End If
    End Sub

    Public Sub LiveDT(ByRef thisWorld As World)
        Age = Age + thisWorld.DT
        If Age > Lifespan Then
            Me.Alive = False
            thisWorld.CreatureDeath(Me, DeathReasons.AGE)
            Exit Sub
        End If
        Call EnergyRefresh(thisWorld)
        If Me.Alive = False Then Exit Sub
        Call ReproRefresh(thisWorld)
        If Me.Alive = False Then Exit Sub
        Call Rove(thisWorld)
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
    Public Property CreationTime As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

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