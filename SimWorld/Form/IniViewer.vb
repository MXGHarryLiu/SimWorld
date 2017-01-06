Public Class IniViewer

    Private Property CurrentPath As String

    Private LastSelection As Integer = -1
    Private History As List(Of String()) = New List(Of String())
    Private EntryState() As EntryStateStruct
    Private Enum EntryStateStruct As Byte
        UNCHANGED = &B0
        KEYCHANGED = &B1
        VALUECHANGED = &B10
        ALLCHANGED = &B11
        'DELETED = &B100
    End Enum
    Private EventChain As List(Of String()) = New List(Of String())
    Private KeyChangedWarning As Boolean = True
    Private ValueTypeChangedWarning As Boolean = True
    Private EntryRemoved As Boolean = False
    Private EntrySwaped As Boolean = False

#Region "Load and Unload"

    Public Sub New(Optional ByVal Path As String = "")
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        CurrentPath = Path
    End Sub

    Private Sub IniViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ResourceManager.GetObject("SimWorld")
        Me.MinimizeBox = False
        MenuStrip1.AllowMerge = False
        With SplitContainer1
            .Dock = DockStyle.Fill
            .Panel2MinSize = 200
            .SplitterDistance = Me.Width * 0.618
            .FixedPanel = FixedPanel.Panel2
        End With
        With ListView1
            .ContextMenuStrip = ContextMenuStrip1
            .Dock = DockStyle.Fill
            .Columns.Add("Key", 100)
            .Columns.Add("Values", SplitContainer1.Panel1.Width - 100)
        End With
        With DataTypeImg
            .SizeMode = PictureBoxSizeMode.StretchImage
            .Width = 32
            .Height = 32
        End With
        With ToolTip1
            .SetToolTip(MoveDown, "Move selected entry one row downwards. ")
            .SetToolTip(MoveUp, "Move selected entry one row upwards. ")
            .SetToolTip(DeleteEntry, "Delete selected entry. ")
        End With

        '========turn off features========
        MoveDownMI.Enabled = False
        MoveUpMI.Enabled = False
        DeleteEntryMI.Enabled = False
        UndoMI.Enabled = False
        RedoMI.Enabled = False
        ReloadMI.Enabled = False
        SaveAsMI.Enabled = False
        SaveMI.Enabled = False
        ComboRoot.Enabled = False
        KeyText.Enabled = False
        ValueText.Enabled = False

        If CurrentPath = "" Then Exit Sub 'empty form

        SaveAsMI.Enabled = True
        ReloadMI.Enabled = True
        ComboRoot.Enabled = True
        KeyText.Enabled = True
        Me.Text = IO.Path.GetFileName(CurrentPath) & " - Data Viewer"
        Call LoadFile()
        MainForm.SimStatusLabel.Text = "Done loading file!"
    End Sub

    Private Sub IniViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If RequireSaving() = False Then
            Exit Sub
        End If
        Dim MsgAns As MsgBoxResult = MsgBox("A file is open. " & vbCrLf & "Do you want to save the current file? ",
              MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton3, "SimWorld - Database Viewer - Exit")
        If MsgAns = MsgBoxResult.Cancel Then
            e.Cancel = True
        ElseIf MsgAns = MsgBoxResult.Yes Then
            SaveMI_Click(sender, e)
        End If
    End Sub

    Private Sub IniViewer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainForm.FormList.RemoveAt(Me.Tag - 1)
        MainForm.WindowsMI.DropDownItems.RemoveAt(Me.Tag - 1 + WINDOWSMENUOFFSET)
        Call MainForm.WindowsMI_DropDownOpening(sender, e)
    End Sub

#End Region

    Private Sub ReloadMI_Click(sender As Object, e As EventArgs) Handles ReloadMI.Click, ReloadCMI.Click
        If RequireSaving() = True Then
            Dim MsgAns As MsgBoxResult = MsgBox("Data have been edited but not saved. " & vbCrLf &
                           "Do you want to continue to reload from: " & vbCrLf & """" & CurrentPath & """? ",
                           MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2,
                           "SimWorld - Database Viewer - Reload File")
            If MsgAns = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        MainForm.SimStatusLabel.Text = "Reloading " & IO.Path.GetFileName(CurrentPath) & " ... "
        Call LoadFile()
        MainForm.SimStatusLabel.Text = "Done reloading file! "
    End Sub

    Private Sub LoadFile()
        ListView1.Items.Clear()
        ComboRoot.Items.Clear()
        ReDim EntryState(0)
        Dim Temp As String = ""
        Dim LCount As Integer = 0
        Application.DoEvents()
        Using r As System.IO.StreamReader = New System.IO.StreamReader(CurrentPath)
            Do While r.EndOfStream = False
                Temp = r.ReadLine()
                Dim thisEntry As String()
                ReDim thisEntry(1)
                If Strings.Left(Temp, 1) = "[" Then 'Root
                    ListView1.Items.Add(Temp).SubItems.Add("")
                    ComboRoot.Items.Add(Temp)
                    thisEntry(0) = Temp
                    thisEntry(1) = ""
                Else                                'entry
                    thisEntry(0) = Strings.Left(Temp, InStr(Temp, "=") - 1)
                    thisEntry(1) = Strings.Right(Temp, Temp.Length - InStr(Temp, "="))
                    ListView1.Items.Add(thisEntry(0)).SubItems.Add(thisEntry(1))
                End If
                ListView1.Items(LCount).UseItemStyleForSubItems = False
                History.Add(thisEntry)
                ReDim Preserve EntryState(LCount)
                EntryState(LCount) = EntryStateStruct.UNCHANGED
                LCount = LCount + 1
            Loop
        End Using
    End Sub

    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        ComboRoot.Width = SplitContainer1.Panel2.Width - LabelRoot.Width - LabelRoot.Left * 2
        ValueText.Width = ComboRoot.Width
        KeyText.Width = ComboRoot.Width
    End Sub

    Private Sub IniViewer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ValueText.Height = SplitContainer1.Panel2.Height - ValueText.Top - DataTypeImg.Height - 20
        DataTypeImg.Top = ValueText.Height + ValueText.Top + 10
    End Sub

    Private Sub RefreshListViewFormat()
        For Each SItems As ListViewItem In ListView1.Items
            If SItems.Selected = True Then
                SItems.BackColor = Color.Gold
                SItems.SubItems(1).BackColor = Color.Gold
            Else
                SItems.BackColor = Color.White
                SItems.SubItems(1).BackColor = Color.White
            End If
            If (EntryState(SItems.Index) And EntryStateStruct.VALUECHANGED) = EntryStateStruct.VALUECHANGED Then
                SItems.SubItems(1).ForeColor = Color.Red
            Else
                SItems.SubItems(1).ForeColor = Color.Black
            End If
            If (EntryState(SItems.Index) And EntryStateStruct.KEYCHANGED) = EntryStateStruct.KEYCHANGED Then
                SItems.SubItems(0).ForeColor = Color.Red
            Else
                SItems.SubItems(0).ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Call RefreshListViewFormat()
        Dim SItem As ListViewItem = ListView1.SelectedItems(0)
        If Strings.Left(SItem.Text, 1) = "[" Then 'root
            MoveDownMI.Enabled = False
            MoveUpMI.Enabled = False
            ComboRoot.SelectedIndex = ComboRoot.FindStringExact(SItem.Text)
            ComboRoot.Text = ComboRoot.Items(ComboRoot.SelectedIndex)
            KeyText.Enabled = False
            ValueText.Enabled = False
            DeleteEntryMI.Enabled = False
            KeyText.Text = ""
            ValueText.Text = ""
            DataTypeImg.Image = Nothing
        Else
            If SItem.Index <= 1 Then
                MoveDownMI.Enabled = True
                MoveUpMI.Enabled = False
            ElseIf SItem.Index = ListView1.Items.Count - 1 Then
                MoveUpMI.Enabled = True
                MoveDownMI.Enabled = False
            Else
                MoveDownMI.Enabled = True
                MoveUpMI.Enabled = True
            End If
            DeleteEntryMI.Enabled = True
            KeyText.Enabled = True
            ValueText.Enabled = True
            KeyText.Text = SItem.Text
            ValueText.Text = SItem.SubItems(1).Text
            Dim Temp As String                 'find entry root
            For i = SItem.Index To 0 Step -1
                Temp = ListView1.Items(i).Text
                If Strings.Left(Temp, 1) = "[" Then
                    ComboRoot.SelectedIndex = ComboRoot.FindStringExact(Temp)
                    ComboRoot.Text = ComboRoot.Items(ComboRoot.SelectedIndex)
                    Exit For
                End If
            Next i
            If IsNumeric(ValueText.Text) = True Then
                DataTypeImg.Image = My.Resources.ResourceManager.GetObject("Numeric_256x")
                DataTypeImg.Tag = "Numeric"
                ToolTip1.SetToolTip(DataTypeImg, "Value data type: Numeric. ")
            Else
                If ValueText.Text = "True" Or ValueText.Text = "False" Then
                    DataTypeImg.Image = My.Resources.ResourceManager.GetObject("Contrast_256x")
                    DataTypeImg.Tag = "Boolean"
                    ToolTip1.SetToolTip(DataTypeImg, "Value data type: Boolean. ")
                Else
                    DataTypeImg.Image = My.Resources.ResourceManager.GetObject("String_256x")
                    DataTypeImg.Tag = "String"
                    ToolTip1.SetToolTip(DataTypeImg, "Value data type: String. ")
                End If
            End If
        End If
        LastSelection = SItem.Index
    End Sub

    Private Sub MoveUp_Click(sender As Object, e As EventArgs) Handles MoveUpMI.Click, MoveUpCMI.Click, MoveUp.Click
        If ListView1.SelectedItems.Count = 0 Then
            Exit Sub 'no selection
        End If
        Dim SItem As ListViewItem = ListView1.SelectedItems(0)
        Dim SIdx As Integer = SItem.Index
        Dim Key As String = SItem.Text
        Dim Value As String = SItem.SubItems(1).Text
        If SIdx <= 1 Then
            Exit Sub 'at the top
        End If
        If Strings.Left(Key, 1) = "[" Then 'current is a root
            Exit Sub
        End If
        Dim PrevItemText As String = ListView1.Items(SIdx - 1).Text
        If Strings.Left(PrevItemText, 1) = "[" Then 'previews is a diff root
            Dim MsgAns As MsgBoxResult = MsgBox("Are you sure you want to assign the entry to a new root? ",
                                MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SimWorld - Database Viewer")
            If MsgAns = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        RowUp(SIdx, Key, Value)
        Dim thisEvent As String()
        ReDim thisEvent(4)
        thisEvent(0) = "U"
        thisEvent(1) = SIdx
        thisEvent(2) = Key
        thisEvent(3) = Value
        thisEvent(4) = True
        EventForward(thisEvent)
    End Sub

    Private Sub MoveDown_Click(sender As Object, e As EventArgs) Handles MoveDownMI.Click, MoveDownCMI.Click, MoveDown.Click
        If ListView1.SelectedItems.Count = 0 Then
            Exit Sub 'no selection
        End If
        Dim SItem As ListViewItem = ListView1.SelectedItems(0)
        Dim SIdx As Integer = SItem.Index
        Dim Key As String = SItem.Text
        Dim Value As String = SItem.SubItems(1).Text
        If SIdx = ListView1.Items.Count - 1 Then
            Exit Sub 'at the bottom
        End If
        If Strings.Left(Key, 1) = "[" Then 'current is a root
            Exit Sub
        End If
        Dim NextItemText As String = ListView1.Items(SIdx + 1).Text
        If Strings.Left(NextItemText, 1) = "[" Then 'next is a diff root
            Dim MsgAns As MsgBoxResult = MsgBox("Are you sure you want to assign the entry to a new root? ",
                                MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SimWorld - Database Viewer")
            If MsgAns = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        RowDown(SIdx, Key, Value)
        Dim thisEvent As String()
        ReDim thisEvent(4)
        thisEvent(0) = "D"
        thisEvent(1) = SIdx
        thisEvent(2) = Key
        thisEvent(3) = Value
        thisEvent(4) = True
        EventForward(thisEvent)
    End Sub

    Private Sub DeleteEntry_Click(sender As Object, e As EventArgs) Handles DeleteEntryMI.Click, DeleteEntryCMI.Click, DeleteEntry.Click
        If ListView1.SelectedItems.Count = 0 Then
            Exit Sub 'no selection
        End If
        Dim SItem As ListViewItem = ListView1.SelectedItems(0)
        Dim SIdx As Integer = SItem.Index
        Dim Key As String = SItem.Text
        Dim Value As String = SItem.SubItems(1).Text
        If Strings.Left(Key, 1) = "[" Then 'root
            Exit Sub
        End If
        Dim MsgAns As MsgBoxResult = MsgBox("Are you sure you want to delete the current entry? ",
                           MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SimWorld - Database Viewer")
        If MsgAns = MsgBoxResult.No Then
            Exit Sub
        End If
        RowDelete(SIdx)
        Dim thisEvent As String()
        ReDim thisEvent(4)
        thisEvent(0) = "R"
        thisEvent(1) = SIdx
        thisEvent(2) = Key
        thisEvent(3) = Value
        thisEvent(4) = True
        EventForward(thisEvent)
    End Sub

    Private Sub RowUp(ByVal SIdx As Integer, ByVal Key As String, ByVal Value As String)
        ListView1.Items.RemoveAt(SIdx)
        ListView1.Items.Insert(SIdx - 1, Key).SubItems.Add(Value)
        ListView1.Items(SIdx - 1).UseItemStyleForSubItems = False
        Dim TempHis As String()
        TempHis = History(SIdx - 1)
        History(SIdx - 1) = History(SIdx)
        History(SIdx) = TempHis
        Dim TempDiff As EntryStateStruct
        TempDiff = EntryState(SIdx - 1)
        EntryState(SIdx - 1) = EntryState(SIdx)
        EntryState(SIdx) = TempDiff
        ListView1.Items(SIdx - 1).Selected = True
        EntrySwaped = True
    End Sub

    Private Sub RowDown(ByVal SIdx As Integer, ByVal Key As String, ByVal Value As String)
        ListView1.Items.RemoveAt(SIdx)
        ListView1.Items.Insert(SIdx + 1, Key).SubItems.Add(Value)
        ListView1.Items(SIdx + 1).UseItemStyleForSubItems = False
        Dim TempHis As String()
        TempHis = History(SIdx + 1)
        History(SIdx + 1) = History(SIdx)
        History(SIdx) = TempHis
        Dim TempDiff As EntryStateStruct
        TempDiff = EntryState(SIdx + 1)
        EntryState(SIdx + 1) = EntryState(SIdx)
        EntryState(SIdx) = TempDiff
        ListView1.Items(SIdx + 1).Selected = True
        EntrySwaped = True
    End Sub

    Private Sub RowDelete(ByVal SIdx As Integer)
        ListView1.Items.RemoveAt(SIdx)
        For i = SIdx To UBound(EntryState) - 1 Step 1
            EntryState(i) = EntryState(i + 1)
        Next
        ReDim Preserve EntryState(UBound(EntryState) - 1)
        History.RemoveAt(SIdx)
        RefreshListViewFormat()
        EntryRemoved = True
    End Sub

    Private Sub RowAdd(ByVal SIdx As Integer, ByVal Key As String, ByVal Value As String)
        ListView1.Items.Insert(SIdx, Key).SubItems.Add(Value)
        ListView1.Items(SIdx).UseItemStyleForSubItems = False
        ReDim Preserve EntryState(EntryState.Count)
        For i = UBound(EntryState) To SIdx + 1 Step -1
            EntryState(i) = EntryState(i - 1)
        Next
        EntryState(SIdx) = True
        Dim thisEntry As String()
        ReDim thisEntry(1)
        thisEntry(0) = Key
        thisEntry(1) = Value
        History.Insert(SIdx, thisEntry)
        ListView1.Items(SIdx).Selected = True
        EntryRemoved = True
    End Sub

    Private Sub EventForward(ByRef NextEvent As String())
        If EventChain.Count = 0 Then 'first use
            EventChain.Add(NextEvent)
        Else
            Dim DelIdx As Integer = -1
            For i = 0 To EventChain.Count - 1 Step 1
                If EventChain(i)(4) = False Then
                    DelIdx = i
                    Exit For
                End If
            Next i
            If DelIdx <> -1 Then
                EventChain.RemoveRange(DelIdx, EventChain.Count - DelIdx)
            End If
            EventChain.Add(NextEvent)
        End If
        UndoMI.Enabled = True
        RedoMI.Enabled = False
    End Sub

    Private Function EventForward() As String()
        If EventChain.Count = 0 Then
            Return Nothing
        Else
            Dim FirstBIdx As Integer = -1
            For i = 0 To EventChain.Count - 1 Step 1
                If EventChain(i)(4) = False Then
                    FirstBIdx = i
                    Exit For
                End If
            Next i
            If FirstBIdx = -1 Then
                Return Nothing
            Else
                EventChain(FirstBIdx)(4) = True
                If FirstBIdx = EventChain.Count - 1 Then
                    RedoMI.Enabled = False
                End If
                UndoMI.Enabled = True
                Return EventChain(FirstBIdx)
            End If
        End If
    End Function

    Private Function EventBackward() As String()
        If EventChain.Count = 0 Then
            Return Nothing
        Else
            Dim LastFIdx As Integer = EventChain.Count
            For i = EventChain.Count - 1 To 0 Step -1
                If EventChain(i)(4) = True Then
                    LastFIdx = i
                    Exit For
                End If
            Next i
            If LastFIdx = EventChain.Count Then
                Return Nothing
            Else
                EventChain(LastFIdx)(4) = False
                If LastFIdx = 0 Then
                    UndoMI.Enabled = False
                End If
                RedoMI.Enabled = True
                Return EventChain(LastFIdx)
            End If
        End If
    End Function

    Private Sub ValueText_LostFocus(sender As Object, e As EventArgs) Handles ValueText.LostFocus
        If ValueText.Text <> ListView1.Items(LastSelection).SubItems(1).Text Then
            Dim CurrentType As String = ""
            If IsNumeric(ValueText.Text) = True Then
                CurrentType = "Numeric"
            Else
                If ValueText.Text = "True" Or ValueText.Text = "False" Then
                    CurrentType = "Boolean"
                Else
                    CurrentType = "String"
                End If
            End If
            If CurrentType <> DataTypeImg.Tag Then
                If ValueTypeChangedWarning = True Then
                    Dim MsgAns As MsgBoxResult = MsgBox("Change the type of the value might render the entry unusable. " _
                    & vbCrLf & "Do you want to modify the value? " & vbCrLf & "(If ""YES"", your action will be remembered.)",
                    MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "SimWorld - Database Viewer")
                    If MsgAns = MsgBoxResult.Yes Then
                        ValueTypeChangedWarning = False
                    Else
                        Exit Sub
                    End If
                End If
            End If
            EntryState(LastSelection) = (EntryState(LastSelection) And (Not EntryStateStruct.VALUECHANGED)) Or
                ((ValueText.Text <> History(LastSelection)(1)) And EntryStateStruct.VALUECHANGED)
            Dim thisEvent As String()
            ReDim thisEvent(4)
            thisEvent(0) = "V"
            thisEvent(1) = LastSelection
            thisEvent(2) = ListView1.Items(LastSelection).SubItems(1).Text
            thisEvent(3) = ValueText.Text
            thisEvent(4) = True
            EventForward(thisEvent)
            ListView1.Items(LastSelection).SubItems(1).Text = ValueText.Text
        End If
    End Sub

    Private Sub KeyText_LostFocus(sender As Object, e As EventArgs) Handles KeyText.LostFocus
        If KeyChangedWarning = True Then Exit Sub
        If KeyText.Text <> ListView1.Items(LastSelection).SubItems(0).Text Then
            EntryState(LastSelection) = (EntryState(LastSelection) And (Not EntryStateStruct.KEYCHANGED)) Or
                ((KeyText.Text <> History(LastSelection)(0)) And EntryStateStruct.KEYCHANGED)
            Dim thisEvent As String()
            ReDim thisEvent(4)
            thisEvent(0) = "K"
            thisEvent(1) = LastSelection
            thisEvent(2) = ListView1.Items(LastSelection).SubItems(0).Text
            thisEvent(3) = KeyText.Text
            thisEvent(4) = True
            EventForward(thisEvent)
            ListView1.Items(LastSelection).SubItems(0).Text = KeyText.Text
        End If
    End Sub

    Private Sub Undo_Click(sender As Object, e As EventArgs) Handles UndoMI.Click, UndoCMI.Click
        Dim UndoEvent As String() = EventBackward()
        If UndoEvent Is Nothing Then Exit Sub
        Select Case UndoEvent(0)
            Case "V"
                Dim SIdx As Integer = UndoEvent(1)
                ListView1.Items(SIdx).SubItems(1).Text = UndoEvent(2)
                EntryState(SIdx) = (EntryState(SIdx) And (Not EntryStateStruct.VALUECHANGED)) Or
                ((UndoEvent(2) <> History(SIdx)(1)) And EntryStateStruct.VALUECHANGED)
                ValueText.Text = UndoEvent(2)
                ListView1.Items(SIdx).Selected = True
            Case "K"
                Dim SIdx As Integer = UndoEvent(1)
                ListView1.Items(SIdx).SubItems(0).Text = UndoEvent(2)
                EntryState(SIdx) = (EntryState(SIdx) And (Not EntryStateStruct.KEYCHANGED)) Or
                ((UndoEvent(2) <> History(SIdx)(0)) And EntryStateStruct.KEYCHANGED)
                KeyText.Text = UndoEvent(2)
                ListView1.Items(SIdx).Selected = True
            Case "U"
                RowDown(UndoEvent(1) - 1, UndoEvent(2), UndoEvent(3))
            Case "D"
                RowUp(UndoEvent(1) + 1, UndoEvent(2), UndoEvent(3))
            Case "R"
                RowAdd(UndoEvent(1), UndoEvent(2), UndoEvent(3))
        End Select
    End Sub

    Private Sub Redo_Click(sender As Object, e As EventArgs) Handles RedoMI.Click, RedoCMI.Click
        Dim RedoEvent As String() = EventForward()
        If RedoEvent Is Nothing Then Exit Sub
        Select Case RedoEvent(0)
            Case "V"
                Dim SIdx As Integer = RedoEvent(1)
                ListView1.Items(SIdx).SubItems(1).Text = RedoEvent(3)
                EntryState(SIdx) = (EntryState(SIdx) And (Not EntryStateStruct.VALUECHANGED)) Or
                ((RedoEvent(3) <> History(SIdx)(1)) And EntryStateStruct.VALUECHANGED)
                ValueText.Text = RedoEvent(3)
                ListView1.Items(SIdx).Selected = True
            Case "K"
                Dim SIdx As Integer = RedoEvent(1)
                ListView1.Items(SIdx).SubItems(0).Text = RedoEvent(3)
                EntryState(SIdx) = (EntryState(SIdx) And (Not EntryStateStruct.KEYCHANGED)) Or
                ((RedoEvent(3) <> History(SIdx)(0)) And EntryStateStruct.KEYCHANGED)
                KeyText.Text = RedoEvent(3)
                ListView1.Items(SIdx).Selected = True
            Case "U"
                RowUp(RedoEvent(1), RedoEvent(2), RedoEvent(3))
            Case "D"
                RowDown(RedoEvent(1), RedoEvent(2), RedoEvent(3))
            Case "R"
                RowDelete(RedoEvent(1))
        End Select
    End Sub

    Private Sub KeyText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KeyText.KeyPress
        If KeyChangedWarning = True Then
            Dim MsgAns As MsgBoxResult = MsgBox("Change the keys of the file might render the entry unusable. " _
                    & vbCrLf & "Do you want to modify the key? " & vbCrLf & "(If ""YES"", Your action will be remembered.)",
                    MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "SimWorld - Database Viewer")
            If MsgAns = MsgBoxResult.Yes Then
                KeyChangedWarning = False
            Else
                e.KeyChar = ChrW(Keys.None)
            End If
        End If
    End Sub

    Private Function RequireSaving() As Boolean
        If EntryRemoved = True Or EntrySwaped = True Then
            Return True
        End If
        If CurrentPath = "" Then
            Return False
        End If
        For i = 0 To EntryState.Count - 1 Step 1
            If EntryState(i) <> EntryStateStruct.UNCHANGED Then
                Return True
            End If
        Next i
        Return False
    End Function

    Private Sub FileMI_DropDownOpening(sender As Object, e As EventArgs) Handles FileMI.DropDownOpening
        SaveMI.Enabled = RequireSaving()
    End Sub

    Private Function ReadIni(ByVal Section As String, ByVal Key As String, ByVal Deflt As String) As String
        Dim sb = New System.Text.StringBuilder(600)
        Dim Path As String = CurrentPath
        GetPrivateProfileString(Section, Key, Deflt, sb, sb.Capacity, Path)
        ReadIni = sb.ToString
    End Function

    Private Sub WriteIni(ByVal Section As String, ByVal Key As String, ByVal Value As String)
        WritePrivateProfileString(Section, Key, Value, CurrentPath)
    End Sub

    Private Sub SaveIni()
        'If EntryRemoved = True Then
        Try
            IO.File.Delete(CurrentPath)
        Catch e As Exception
            MsgBox("Error: " & e.Message, MsgBoxStyle.Critical, "SimWorld")
            Exit Sub
        End Try
        Dim CurrentRoot As String = ""
        Dim TempKey As String = ""
        '====method 1 =====
        For i = 0 To ListView1.Items.Count - 1 Step 1
            TempKey = ListView1.Items(i).SubItems(0).Text
            If Strings.Left(TempKey, 1) = "[" Then
                CurrentRoot = Strings.Mid(TempKey, 2, TempKey.Length - 2)
            Else
                WriteIni(CurrentRoot, TempKey, ListView1.Items(i).SubItems(1).Text)
            End If
        Next i
        '====method 2: write to file====
        'to be finished

        EntryRemoved = False
        EntrySwaped = False
        For i = 0 To EntryState.Count - 1 Step 1
            EntryState(i) = EntryStateStruct.UNCHANGED
        Next i
    End Sub

    Private Sub SaveMI_Click(sender As Object, e As EventArgs) Handles SaveMI.Click
        SaveIni()
        RefreshListViewFormat()
        SaveMI.Enabled = False
    End Sub

    Private Sub SaveAsMI_Click(sender As Object, e As EventArgs) Handles SaveAsMI.Click
        SaveFileDialog1.Filter = "All Supported Files|*.smw;*.smc;*.ini|SimWorld World Files|*.smw|SimWorld Creature Files|*.smc|Ini Configuration Files|*.ini"
        SaveFileDialog1.InitialDirectory = IO.Path.GetDirectoryName(CurrentPath)
        SaveFileDialog1.FileName = CurrentPath
        Dim CurrentExtension As String = IO.Path.GetExtension(CurrentPath)
        Dim ConfirmedExtension As String = False
        While ConfirmedExtension = False
            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                If IO.Path.GetExtension(SaveFileDialog1.FileName) <> CurrentExtension Then
                    Dim MsgAns As MsgBoxResult = MsgBox("You will be saving the file as a different format. " & vbCrLf &
                           "Do you want to continue to save? ",
                           MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SimWorld - Database Viewer - Save File")
                    If MsgAns = MsgBoxResult.No Then
                        SaveFileDialog1.FileName = CurrentPath
                        ConfirmedExtension = False
                    Else
                        ConfirmedExtension = True
                    End If
                Else
                    ConfirmedExtension = True
                End If
            Else
                Exit Sub
            End If
        End While
        CurrentPath = SaveFileDialog1.FileName
        SaveFileDialog1.FileName = ""
        SaveIni()
    End Sub

    Private Sub CloseMI_Click(sender As Object, e As EventArgs) Handles CloseMI.Click
        Me.Close()
    End Sub

    Private Sub MoveDownMI_EnabledChanged(sender As Object, e As EventArgs) Handles MoveDownMI.EnabledChanged
        MoveDown.Enabled = MoveDownMI.Enabled
        MoveDownCMI.Enabled = MoveDownMI.Enabled
    End Sub

    Private Sub MoveUpMI_EnabledChanged(sender As Object, e As EventArgs) Handles MoveUpMI.EnabledChanged
        MoveUp.Enabled = MoveUpMI.Enabled
        MoveUpCMI.Enabled = MoveUpMI.Enabled
    End Sub

    Private Sub DeleteEntryMI_EnabledChanged(sender As Object, e As EventArgs) Handles DeleteEntryMI.EnabledChanged
        DeleteEntry.Enabled = DeleteEntryMI.Enabled
        DeleteEntryCMI.Enabled = DeleteEntryMI.Enabled
    End Sub

    Private Sub UndoMI_EnabledChanged(sender As Object, e As EventArgs) Handles UndoMI.EnabledChanged
        UndoCMI.Enabled = UndoMI.Enabled
    End Sub

    Private Sub RedoMI_EnabledChanged(sender As Object, e As EventArgs) Handles RedoMI.EnabledChanged
        RedoCMI.Enabled = RedoMI.Enabled
    End Sub

    Private Sub ReloadMI_EnabledChanged(sender As Object, e As EventArgs) Handles ReloadMI.EnabledChanged
        ReloadCMI.Enabled = ReloadMI.Enabled
    End Sub

End Class