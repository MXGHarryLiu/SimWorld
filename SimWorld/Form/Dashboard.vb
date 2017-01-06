Option Explicit On

Public Class Dashboard

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
        PropertyGrid1.Dock = DockStyle.Fill
        Me.Icon = My.Resources.ResourceManager.GetObject("SimWorld")
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

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class