Public Class Decisioner

    Private ArgList As List(Of Object) = New List(Of Object)
    Private ArgTypeList As List(Of Integer) = New List(Of Integer)
    ' 1 = Creature.Gender, 2 = Double, 3 = Boolean
    Private dataT As DataTable = New DataTable()

    Public Property Valid As Boolean = False

    Public Sub New(ByRef Subject As Object, ByRef thisWorld As World, ByVal Expression As String)
        Dim PropertyName As String = ""
        Dim CurrentProp As Reflection.PropertyInfo = Nothing
        ' https://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx
        ' regex for output property
        Dim r1 As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("^#\w+\s*=\s*")
        Dim m1 As System.Text.RegularExpressions.MatchCollection = r1.Matches(Expression)
        If m1.Count = 0 Then
            MsgBox("Expression must contain an output! ")
            Exit Sub
        End If
        Dim TrimLen As Integer = m1(0).Value.Length + 1
        r1 = New System.Text.RegularExpressions.Regex("^#\w+")
        m1 = r1.Matches(Expression)
        Expression = Strings.Mid(Expression, TrimLen)
        PropertyName = Strings.Mid(m1(0).Value, 2)
        CurrentProp = Subject.GetType().GetProperty(PropertyName)
        If CurrentProp Is Nothing Then
            MsgBox("Fail to find property """ & PropertyName & """! ", MsgBoxStyle.Critical, "SimWorld - Decision")
            Exit Sub
        Else
            Call UpdateArgLists(Subject, CurrentProp)
            Expression = Strings.Replace(Expression, m1(0).Value, PropertyName)
        End If
        ' regex for input property
        Dim r2 As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("#\w+")
        Dim m2 As System.Text.RegularExpressions.MatchCollection = r2.Matches(Expression)
        Dim NewExpression As String = Expression
        For i As Integer = 0 To m2.Count - 1 Step 1
            PropertyName = Strings.Mid(m2(i).Value, 2)
            If dataT.Columns.Contains(PropertyName) Then
                Continue For
            End If
            CurrentProp = Subject.GetType().GetProperty(PropertyName)
            If CurrentProp Is Nothing Then
                CurrentProp = thisWorld.GetType().GetProperty(PropertyName)
                If CurrentProp Is Nothing Then
                    MsgBox("Fail to find property """ & PropertyName & """! ", MsgBoxStyle.Critical, "SimWorld - Decision")
                    dataT.Columns.Add(PropertyName, GetType(String))
                    Exit Sub
                Else
                    Call UpdateArgLists(thisWorld, CurrentProp)
                End If
            Else
                Call UpdateArgLists(Subject, CurrentProp)
            End If
            NewExpression = Strings.Replace(NewExpression, m2(i).Value, PropertyName)
        Next i
        dataT.Columns.Add("E", GetType(Object), NewExpression)
        dataT.Rows.Add(dataT.NewRow())
        Valid = True
    End Sub

    Private Sub UpdateArgLists(ByRef Subject As Object, ByRef CurrentProp As Reflection.PropertyInfo)
        Select Case CurrentProp.PropertyType
            Case GetType(Creature.Gender)
                Dim Prop As PropertyHook(Of Creature.Gender) = New PropertyHook(Of Creature.Gender)(Subject, CurrentProp)
                dataT.Columns.Add(CurrentProp.Name, GetType(String))
                ArgList.Add(Prop)
                ArgTypeList.Add(1)
            Case GetType(Double)
                Dim Prop As PropertyHook(Of Double) = New PropertyHook(Of Double)(Subject, CurrentProp)
                dataT.Columns.Add(CurrentProp.Name, GetType(Double))
                ArgList.Add(Prop)
                ArgTypeList.Add(2)
            Case GetType(Boolean)
                Dim Prop As PropertyHook(Of Boolean) = New PropertyHook(Of Boolean)(Subject, CurrentProp)
                dataT.Columns.Add(CurrentProp.Name, GetType(Boolean))
                ArgList.Add(Prop)
                ArgTypeList.Add(3)
            Case Else
                ' Do nothing
        End Select
    End Sub

    Public Function Decide(Optional ByVal SuppressWrite As Boolean = False) As Object
        Dim Output As Object = Nothing
        If dataT.Rows.Count <> 0 Then    'has input from property
            For i As Integer = 0 To ArgList.Count - 1 Step 1
                Select Case ArgTypeList(i)
                    Case 1
                        Dim Prop As PropertyHook(Of Creature.Gender) = CType(ArgList(i), PropertyHook(Of Creature.Gender))
                        dataT.Rows(0).SetField(Prop.Name, Prop.Value.ToString)
                    Case 2
                        Dim Prop As PropertyHook(Of Double) = CType(ArgList(i), PropertyHook(Of Double))
                        dataT.Rows(0).SetField(Prop.Name, Prop.Value)
                    Case 3
                        Dim Prop As PropertyHook(Of Boolean) = CType(ArgList(i), PropertyHook(Of Boolean))
                        dataT.Rows(0).SetField(Prop.Name, Prop.Value)
                    Case Else
                        ' reserved
                End Select
            Next i
            Output = dataT.Rows(0).Field(Of Object)("E")
        End If
        If SuppressWrite = False Then       'Edit the output property
            Select Case ArgTypeList(0)
                Case 1
                    Dim Prop As PropertyHook(Of Creature.Gender) = CType(ArgList(0), PropertyHook(Of Creature.Gender))
                    Prop.Value = CType(Output, Creature.Gender)
                Case 2
                    Dim Prop As PropertyHook(Of Double) = CType(ArgList(0), PropertyHook(Of Double))
                    Prop.Value = CType(Output, Double)
                Case 3
                    Dim Prop As PropertyHook(Of Boolean) = CType(ArgList(0), PropertyHook(Of Boolean))
                    Prop.Value = CType(Output, Boolean)
                Case Else
                    ' reserved
            End Select
        End If
        Return Output
    End Function

    Private Class PropertyHook(Of T)

        Private Getter As Func(Of T)
        Private Setter As Action(Of T) = Nothing

        Private _Name As String = ""
        Public ReadOnly Property Name As String
            Get
                Return _Name
            End Get
        End Property

        Public Property Value As T
            Get
                Return Getter()
            End Get
            Set(ByVal value As T)
                If Setter IsNot Nothing Then
                    Setter(value)
                End If
            End Set
        End Property

        Public Sub New(ByRef Subject As Object, ByRef Prop As Reflection.PropertyInfo)
            _Name = Prop.Name
            Getter = [Delegate].CreateDelegate(GetType(Func(Of T)), Subject, Prop.GetGetMethod())
            If Prop.CanWrite = True Then
                Setter = [Delegate].CreateDelegate(GetType(Action(Of T)), Subject, Prop.GetSetMethod())
            End If
        End Sub

    End Class

End Class