Public Class EntryDialog : Implements IDisposable

    Private Responsed As Boolean = False
    Private EntryBar As ToolStrip = Nothing
    Private disposed As Boolean = False
    Private ToolTextBoxList As List(Of ToolStripTextBox) = New List(Of ToolStripTextBox)
    Public Property EntryNum As Integer = 0
    Public Property Confirmed As Boolean = False
    Public Property Results As List(Of String) = New List(Of String)

    Public Sub New(ByVal EntryNum As Integer, ParamArray ByVal Prompt() As String)
        If EntryNum < 0 Then
            Throw New ArgumentException("EntryNum must be positive. ")
        End If
        Me.EntryNum = EntryNum
        EntryBar = New ToolStrip()
        Dim ToolLabel As ToolStripLabel = Nothing
        Dim ToolTextbox As ToolStripTextBox = Nothing
        For i As Integer = 1 To Me.EntryNum Step 1
            If i <= Prompt.Count Then
                ToolLabel = New ToolStripLabel(Prompt(i - 1))
                EntryBar.Items.Add(ToolLabel)
            End If
            ToolTextbox = New ToolStripTextBox()
            If ToolLabel IsNot Nothing Then
                ToolTextbox.ToolTipText = ToolLabel.Text
            End If
            EntryBar.Items.Add(ToolTextbox)
            ToolTextBoxList.Add(ToolTextbox)
            ToolLabel = Nothing
        Next
        Dim ConfirmImg As Image = My.Resources.ResourceManager.GetObject("StatusOK_256x")
        Dim ConfirmButton As ToolStripButton = New ToolStripButton("Confirm", ConfirmImg)
        AddHandler ConfirmButton.Click, AddressOf Confirm_Click
        EntryBar.Items.Add(ConfirmButton)
        Dim CancelImg As Image = My.Resources.ResourceManager.GetObject("StatusNo_cyan_256x")
        Dim CancelButton As ToolStripButton = New ToolStripButton("Cancel", CancelImg)
        AddHandler CancelButton.Click, AddressOf Cancel_Click
        EntryBar.Items.Add(CancelButton)
    End Sub

    Public Sub SetInitialText(ParamArray ByVal Prompt() As String)
        For i As Integer = 1 To Math.Min(EntryNum, Prompt.Count) Step 1
            ToolTextBoxList(i - 1).Text = Prompt(i - 1)
        Next
    End Sub

    'Private Sub ToolTextbox_Leave(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim thisToolTextBox As ToolStripTextBox = TryCast(sender, ToolStripTextBox)
    '    If thisToolTextBox.Text = "" Then
    '        thisToolTextBox.Focus()
    '    End If
    'End Sub

    Private Sub Confirm_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each C As ToolStripItem In EntryBar.Items
            If TypeOf C Is ToolStripTextBox Then
                Results.Add(TryCast(C, ToolStripTextBox).Text)
            End If
        Next
        Confirmed = True
        Responsed = True
    End Sub

    Private Sub Cancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Confirmed = False
        Responsed = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If disposed Then Return
        If disposing Then
            EntryBar.Dispose() ' Free any other managed objects here.
        End If
        ' Free any unmanaged objects here.
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Sub ShowAndWaitInput(ByRef Container As ToolStripPanel)
        Container.Join(EntryBar, Container.Rows.Count - 1)
        While Responsed = False
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

End Class