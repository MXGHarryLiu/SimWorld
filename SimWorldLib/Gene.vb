Imports System.Runtime.Serialization
Imports MathNet.Numerics
Imports SimWorldLib.ErrMsg

<Serializable>
<DataContract>
<System.ComponentModel.DefaultProperty("Phenotype")>
Public Class Gene

#Region "Properties"

    <DataMember>
    Public Property Phenotype As String

    <DataMember>
    <System.ComponentModel.DefaultValueAttribute(True)>
    Public Property Plastic As Boolean = True

    <DataMember>
    Private _Model As MathModels = MathModels.CONSTANT
    Public Property Model As MathModels
        Get
            Return _Model
        End Get
        Set(ByVal value As MathModels)
            If _Model <> value Then
                _ModelParameters.Clear()
                Select Case value
                    Case MathModels.CONSTANT
                        _ValidParameterNum = 1
                        _ModelParameters.Add(0)     ' Const
                        _Maximum = 0
                        _Minimum = 0
                        Plastic = False
                    Case MathModels.BINARY
                        _ValidParameterNum = 1
                        _ModelParameters.Add(0.5)   ' p
                        _Maximum = 1
                        _Minimum = 0
                    Case MathModels.UNIFORM
                        _ValidParameterNum = 0
                    Case MathModels.NORMAL
                        _ValidParameterNum = 2
                        _ModelParameters.Add(0)     ' Mu
                        _ModelParameters.Add(1)     ' Sigma
                    Case MathModels.EXPONENTIAL
                        _ValidParameterNum = 1
                        _ModelParameters.Add(0)     ' Lambda
                    Case Else
                        'do nothing
                End Select
                _Model = value
            End If
        End Set
    End Property
    Public Enum MathModels As Byte  ' ModelParameters
        CONSTANT = 0                ' the constant itself
        BINARY = 1                  ' p: probability of True/1
        UNIFORM = 2
        NORMAL = 3                  ' Mean: Mu, Standard Deviation: Sigma
        EXPONENTIAL = 4             ' Lambda: Rate constant
    End Enum

    <DataMember>
    Private _ModelParameters As List(Of Double) = New List(Of Double)
    Public Property ModelParameters(ByVal Index As Integer) As Double
        Get
            If Index < 0 Then
                ShowErrMsg(NameOf(Index), ErrType.NOTNEGATIVE)
            ElseIf Index + 1 > _ValidParameterNum Then
                ShowErrMsg(NameOf(Index), ErrType.UPPERBOUNDED, _ValidParameterNum - 1)
            Else
                Return _ModelParameters(Index)
            End If
            Return 0
        End Get
        Set(ByVal value As Double)
            If Index < 0 Then
                ShowErrMsg(NameOf(Index), ErrType.NOTNEGATIVE)
            ElseIf Index + 1 > _ValidParameterNum Then
                ShowErrMsg(NameOf(Index), ErrType.UPPERBOUNDED, _ValidParameterNum - 1)
            Else
                If Me.Model = MathModels.BINARY Then
                    If Index = 0 AndAlso (value > 1 Or value < 0) Then
                        ShowErrMsg("Model parameter p", ErrType.PROBABILITY)
                        Exit Property
                    End If
                ElseIf Me.Model = MathModels.CONSTANT Then
                    _Maximum = value
                    _Minimum = value
                End If
                _ModelParameters(Index) = value
            End If
        End Set
    End Property

    <DataMember>
    Private _ValidParameterNum As Integer = 0
    Public ReadOnly Property ValidParameterNum As Integer
        Get
            Return _ValidParameterNum
        End Get
    End Property

    Private _Minimum As Double = Double.NegativeInfinity
    <DataMember>
    Public Property Minimum As Double
        Get
            Return _Minimum
        End Get
        Set(value As Double)
            If value > _Maximum Then
                ShowErrMsg(NameOf(Minimum), ErrType.UPPERBOUNDED, _Maximum)
            Else
                _Minimum = value
            End If
        End Set
    End Property

    Private _Maximum As Double = Double.PositiveInfinity
    <DataMember>
    Public Property Maximum As Double
        Get
            Return _Maximum
        End Get
        Set(value As Double)
            If value < _Minimum Then
                ShowErrMsg(NameOf(Maximum), ErrType.LOWERBOUNDED, _Minimum)
            Else
                _Maximum = value
            End If
        End Set
    End Property

#End Region

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf (obj) Is Gene Then
            If Me.Phenotype = TryCast(obj, Gene).Phenotype Then
                Return True
            Else
                Return False
            End If
        End If
        Return MyBase.Equals(obj)
    End Function

    Public Sub New()

    End Sub

    Public Sub New(ByVal Phenotype As String, Optional ByVal MathModel As MathModels = MathModels.CONSTANT)
        Me.Phenotype = Phenotype
        Me.Model = MathModel
    End Sub

    Public Sub SetParameters(ParamArray ByVal Parameters() As Double)
        For i As Integer = 0 To Math.Min(Parameters.Count, _ValidParameterNum) - 1 Step 1
            _ModelParameters(i) = Parameters(i)
        Next i
    End Sub

    Public Function ShowPhenotype() As Double
        Dim Output As Double = Double.NaN
        Dim RNG As System.Random = NewRNG()
        Do Until Output >= Minimum And Output <= Maximum
            Select Case Me.Model
                Case MathModels.CONSTANT
                    Output = _ModelParameters(0)
                Case MathModels.BINARY
                    Output = Distributions.Bernoulli.Sample(RNG, _ModelParameters(0))
                Case MathModels.UNIFORM
                    Output = Distributions.ContinuousUniform.Sample(RNG, Me.Minimum, Me.Maximum)
                Case MathModels.NORMAL
                    Output = Distributions.Normal.Sample(RNG, _ModelParameters(0), _ModelParameters(1))
                Case MathModels.EXPONENTIAL
                    Output = Distributions.Exponential.Sample(RNG, _ModelParameters(0))
                Case Else
                    Return 0 'do nothing 
            End Select
        Loop
        Return Output
    End Function

    Public Overloads Function ToString() As String
        Return Phenotype
    End Function

End Class