''' <summary>
''' Multiple-entry input form dialog
''' </summary>
Public Class EntryDialog

    Private NewDialog As Form = New Form()
    Private ToolTextBoxList As List(Of TextBox) = New List(Of TextBox)
    Private Table As TableLayoutPanel = Nothing
    Public Property EntryNum As Integer = 0
    Public Property Confirmed As Boolean = False
    Public Property Results As List(Of String) = New List(Of String)

    Public Sub New(ByVal EntryNum As Integer, ByVal Title As String, ByVal Summary As String, ParamArray ByVal Prompt() As String)
        If EntryNum < 0 Then
            Throw New ArgumentException("EntryNum must be positive. ")
        End If
        Dim ToolLabel As Label = Nothing
        Me.EntryNum = EntryNum
        Table = New TableLayoutPanel()
        Table.AutoSize = True
        Table.RowCount = EntryNum + 2
        ToolLabel = New Label()
        ToolLabel.Text = Summary
        ToolLabel.AutoSize = True
        ToolLabel.Padding = New Padding(0, 10, 0, 10)
        Table.Controls.Add(ToolLabel, 0, 0)
        Table.SetColumnSpan(ToolLabel, 2)
        Table.Padding = New Padding(10)
        Table.ColumnCount = 2
        'Dim TableRowStyles As TableLayoutRowStyleCollection = Table.RowStyles
        'For Each TableRowStyle As RowStyle In TableRowStyles
        '    TableRowStyle.SizeType = SizeType.AutoSize
        'Next
        Dim ToolTextbox As TextBox = Nothing
        For i As Integer = 0 To Me.EntryNum - 1 Step 1
            ToolTextbox = New TextBox()
            If i < Prompt.Count Then
                ToolLabel = New Label()
                ToolLabel.Text = Prompt(i)
                ToolLabel.AutoSize = True
                Table.Controls.Add(ToolLabel, 0, i + 1)
                Table.Controls.Add(ToolTextbox, 1, i + 1)
            Else
                ToolTextbox.Width = Table.Width
                Table.Controls.Add(ToolTextbox, 0, i + 1)
                Table.SetColumnSpan(ToolTextbox, 2)
            End If
            ToolTextBoxList.Add(ToolTextbox)
        Next
        Dim ConfirmButton As Button = New Button()
        ConfirmButton.Text = "Confirm"
        ConfirmButton.Padding = New Padding(10, 3, 10, 3)
        ConfirmButton.AutoSize = True
        AddHandler ConfirmButton.Click, AddressOf Confirm_Click
        Table.Controls.Add(ConfirmButton, 0, EntryNum + 1)
        Dim CancelButton As Button = New Button()
        CancelButton.Text = "Cancel"
        'CancelButton.Anchor = AnchorStyles.Top + AnchorStyles.Right + AnchorStyles.Left + AnchorStyles.Bottom
        CancelButton.Padding = New Padding(10, 3, 10, 3)
        CancelButton.AutoSize = True
        AddHandler CancelButton.Click, AddressOf Cancel_Click
        Table.Controls.Add(CancelButton, 1, EntryNum + 1)
        With NewDialog
            .Text = "SimWorld"
            .ControlBox = False
            .AcceptButton = ConfirmButton
            .CancelButton = CancelButton
            .Controls.Add(Table)
            .StartPosition = FormStartPosition.CenterParent
            .ClientSize = Table.Size
            .FormBorderStyle = FormBorderStyle.FixedDialog
        End With
    End Sub

    ''' <summary>
    ''' Set default values to the input fields
    ''' </summary>
    ''' <param name="IniVal">Default entry values</param>
    Public Sub SetInitialText(ParamArray ByVal IniVal() As String)
        For i As Integer = 1 To Math.Min(EntryNum, IniVal.Count) Step 1
            ToolTextBoxList(i - 1).Text = IniVal(i - 1)
        Next
    End Sub

    Private Sub Confirm_Click(ByVal sender As Object, ByVal e As EventArgs)
        For i As Integer = 0 To EntryNum - 1 Step 1
            Results.Add(ToolTextBoxList(i).Text)
        Next
        Confirmed = True
        NewDialog.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Confirmed = False
        NewDialog.Close()
    End Sub

    ''' <summary>
    ''' Show the entry dialog. 
    ''' </summary>
    ''' <param name="Owner">The form that calls the dialog. </param>
    Public Sub Show(ByRef Owner As Form)
        NewDialog.ShowDialog(Owner)
    End Sub

End Class