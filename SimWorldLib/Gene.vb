Imports System.Runtime.Serialization
Imports MathNet.Numerics
Imports SimWorldLib.ErrMsg

<Serializable>
<DataContract>
Public Class Gene

#Region "Properties"

    <DataMember>
    Public Property Phenotype As String

    <DataMember>
    Private _Model As MathModels = MathModels.UNKNOWN
    Public Property Model As MathModels
        Get
            Return _Model
        End Get
        Set(ByVal value As MathModels)
            If _Model <> value Then
                _ModelParameters.Clear()
                Select Case value
                    Case MathModels.BINARY
                        _ValidParameterNum = 1
                        _ModelParameters.Add(0)
                    Case MathModels.UNIFORM
                        _ValidParameterNum = 0
                    Case MathModels.NORMAL
                        _ValidParameterNum = 2
                        _ModelParameters.Add(0)
                        _ModelParameters.Add(1)
                    Case MathModels.EXPONENTIAL
                        '_ValidParameterNum = 1
                        '_ModelParameters.Add(0)
                    Case Else
                        'do nothing
                End Select
                _Model = value
            End If
        End Set
    End Property
    Public Enum MathModels As Byte
        UNKNOWN = 0         ' ModelParameters
        BINARY = 1          ' p(True)
        UNIFORM = 2
        NORMAL = 3          ' Mean: Mu, Standard Deviation: Sigma
        EXPONENTIAL = 4     ' TBD
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
                _ModelParameters(Index) = value
            End If
        End Set
    End Property

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

    Public Sub New()

    End Sub

    Public Sub New(ByVal Phenotype As String, Optional ByVal MathModel As MathModels = MathModels.UNKNOWN)
        Me.Phenotype = Phenotype
        Me.Model = MathModel
    End Sub

    Public Sub SetParameters(ParamArray ByVal Parameters() As Double)
        For i As Integer = 0 To Math.Min(Parameters.Count, _ValidParameterNum) - 1 Step 1
            _ModelParameters(i) = Parameters(i)
        Next i
    End Sub

    Public Function ShowPhenotype() As Object
        Select Case Me.Model
            Case MathModels.BINARY
                Return 0 'Distributions.Bernoulli.Sample(_ModelParameters(0))
            Case MathModels.UNIFORM
                Return 0
            Case MathModels.NORMAL
                Return Distributions.Normal.Sample(_ModelParameters(0), _ModelParameters(1))
            Case MathModels.EXPONENTIAL
                Return 0
            Case Else
                'Distributions.Normal.
                Return 0 'do nothing 
        End Select
    End Function

    Public Overloads Function ToString() As String
        Return Phenotype
    End Function

End Class