Public Class StartPage

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.BackColor = Color.Transparent
        VerLabel.Text = My.Application.Info.Version.ToString()
        Me.Icon = My.Resources.ResourceManager.GetObject("SimWorld")
        'VerLabel.Parent = Panel1
        'TitleLabel.Parent = Panel1
    End Sub

    Private Sub StartPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LinkLabel1.Text = ""
        LinkLabel2.Text = ""
        LinkLabel3.Text = ""
        If My.Settings.LastOpen IsNot Nothing Then
            Select Case My.Settings.LastOpen.Count
                Case 0
                Case 1
                    LinkLabel1.Tag = My.Settings.LastOpen(0)
                Case 2
                    LinkLabel1.Tag = My.Settings.LastOpen(1)
                    LinkLabel2.Tag = My.Settings.LastOpen(0)
                Case Else
                    LinkLabel1.Tag = My.Settings.LastOpen(2)
                    LinkLabel2.Tag = My.Settings.LastOpen(1)
                    LinkLabel3.Tag = My.Settings.LastOpen(0)
            End Select
            LinkLabel1.Text = IO.Path.GetFileName(LinkLabel1.Tag)
            LinkLabel2.Text = IO.Path.GetFileName(LinkLabel2.Tag)
            LinkLabel3.Text = IO.Path.GetFileName(LinkLabel3.Tag)
            ToolTip1.SetToolTip(LinkLabel1, LinkLabel1.Tag)
            ToolTip1.SetToolTip(LinkLabel2, LinkLabel2.Tag)
            ToolTip1.SetToolTip(LinkLabel3, LinkLabel3.Tag)
        End If
        LinkLabel1.AutoSize = True
        LinkLabel2.AutoSize = True
        LinkLabel3.AutoSize = True
    End Sub

    Private Sub LinkLabels_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked, LinkLabel2.LinkClicked, LinkLabel3.LinkClicked
        Call MainForm.OpenMI_Click(sender, e, TryCast(sender, LinkLabel).Tag)
        Me.Close()
    End Sub

    Private Sub StartPage_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Me.Close()
    End Sub

    Private Sub NewLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles NewLabel.LinkClicked
        Call MainForm.NewWorldMI_Click(sender, e)
        Me.Close()
    End Sub
End Class