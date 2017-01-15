Option Explicit On
Imports SimWorldLib

Public Class Dashboard

    Private LogListCache() As ListViewItem = Nothing
    Private FirstLog As Integer = 0

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        If My.Settings.DashboardFormSize.IsEmpty = False Then
            Me.Size = My.Settings.DashboardFormSize
        End If
        If My.Settings.DashboardFormPos.IsEmpty = False Then
            Me.Location = My.Settings.DashboardFormPos
        End If
        TabControl1.Dock = DockStyle.Fill
        Me.Icon = My.Resources.ResourceManager.GetObject("SimWorld")
        With LogListView
            .Columns.Add("#", 30)
            .Columns.Add("Time", 80)
            .Columns.Add("SimTime", 40)
            .Columns.Add("Subject", 60)
            .Columns.Add("Description", 100)
            .VirtualMode = True
            .VirtualListSize = 0
        End With
    End Sub

    Private Sub Dashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainForm.CurrentDashboard = Nothing
        MainForm.FormList.RemoveAt(Me.Tag - 1)
        MainForm.WindowsMI.DropDownItems.RemoveAt(Me.Tag - 1 + WINDOWSMENUOFFSET)
        Call MainForm.WindowsMI_DropDownOpening(sender, e)
        My.Settings.DashboardFormSize = Me.Size
        My.Settings.DashboardFormPos = Me.Location
        My.Settings.Save()
    End Sub

    Public Sub PropertyGrid1_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
        Call MainForm.RefreshView(MainForm.CurrentStage)
    End Sub

    Private Sub PropertyGrid1_SelectedObjectsChanged(sender As Object, e As EventArgs) Handles PropertyGrid1.SelectedObjectsChanged
        If TypeOf PropertyGrid1.SelectedObject Is SimWorldLib.World Then
            Call RefreshLogList(MyWorld)
        Else
            TabControl1.SelectedTab = TabPage1
        End If
    End Sub

    Public Sub RefreshContent()
        Call PropertyGrid1.Refresh()
        Call RefreshLogList(MyWorld)
    End Sub

    Private Sub RefreshLogList(ByRef Subject As SimWorldLib.World)
        If LogListView.VirtualListSize <> Subject.WorldLog.EntryCount Then
            LogListView.VirtualListSize = Subject.WorldLog.EntryCount
        End If
    End Sub

    Private Sub LogListView_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles LogListView.RetrieveVirtualItem
        If Not (LogListCache Is Nothing) AndAlso e.ItemIndex >= FirstLog AndAlso e.ItemIndex < FirstLog + LogListCache.Length Then
            e.Item = LogListCache(e.ItemIndex - FirstLog)
        Else
            Dim NewRow As ListViewItem = New ListViewItem(e.ItemIndex + 1)
            NewRow.SubItems.Add(MyWorld.WorldLog.Entries(e.ItemIndex, 0))
            NewRow.SubItems.Add(MyWorld.WorldLog.Entries(e.ItemIndex, 1))
            NewRow.SubItems.Add(MyWorld.WorldLog.Entries(e.ItemIndex, 2))
            NewRow.SubItems.Add(MyWorld.WorldLog.Entries(e.ItemIndex, 3))
            e.Item = NewRow
        End If
    End Sub

    Private Sub PropertyGrid1_SelectedGridItemChanged(sender As Object, e As SelectedGridItemChangedEventArgs) Handles PropertyGrid1.SelectedGridItemChanged
        PropertyGrid1.ContextMenuStrip = Nothing
        If e.NewSelection.GridItemType = GridItemType.Category Then
            Exit Sub
        End If
        If e.NewSelection.PropertyDescriptor.PropertyType Is GetType(List(Of Creature)) Then
            PropertyGrid1.ContextMenuStrip = ContextMenuStrip1
        Else

        End If
    End Sub
End Class