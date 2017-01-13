Imports System.Runtime.Serialization

<Serializable>
<DataContract>
<System.ComponentModel.DefaultProperty("EntryCount")>
Public Class Log

    <DataMember>
    Private Property Memory As List(Of LogEntry) = New List(Of LogEntry)

    Public ReadOnly Property EntryCount As Integer
        Get
            Return Memory.Count
        End Get
    End Property

    Public ReadOnly Property Entries(ByVal Index As Integer, ByVal SubIndex As Integer) As String
        Get
            Select Case SubIndex
                Case 0
                    Return Memory(Index).Time.ToString("HH:mm:ss")
                Case 1
                    Return Memory(Index).SimTime
                Case 2
                    Return Memory(Index).Subject
                Case 3
                    Return Memory(Index).Description
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public Sub AddLog(ByVal Time As Date, ByVal SimTime As Double, ByVal Subject As String,
                      ByVal Description As String)
        Memory.Add(New LogEntry(Time, SimTime, Subject, Description))
    End Sub

    Public Sub WriteAllEntry(ByVal Path As String, Optional ClearMemory As Boolean = True)
        If Memory.Count = 0 Then
            Exit Sub
        End If
        Dim CurrentLog As LogEntry = Nothing
        Using w As IO.StreamWriter = IO.File.AppendText(Path)
            For i As Integer = 1 To Memory.Count() Step 1
                CurrentLog = Memory(i - 1)
                w.WriteLine("{0},{1},{2},{3},{4},", i.ToString, CurrentLog.Time.ToString("yyyy-MM-dd HH:mm:ss"),
                            CurrentLog.SimTime, CurrentLog.Subject, CurrentLog.Description)
            Next i
        End Using
        If ClearMemory = True Then
            Memory.Clear()
        End If
    End Sub

    <Serializable>
    <DataContract>
    Private Structure LogEntry
        <DataMember>
        Public Time As Date
        <DataMember>
        Public SimTime As Double
        <DataMember>
        Public Subject As String
        <DataMember>
        Public Description As String

        Public Sub New(ByVal Time As Date, ByVal SimTime As Double,
                       ByVal Subject As String, ByVal Description As String)
            Me.Time = Time
            Me.SimTime = SimTime
            Me.Subject = Subject
            Me.Description = Description
        End Sub
    End Structure

    Public Sub New()

    End Sub

End Class