<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.AbsTimeLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PopulationLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LayerStatus = New System.Windows.Forms.ToolStripDropDownButton()
        Me.NoneLayerML = New System.Windows.Forms.ToolStripMenuItem()
        Me.SoilLayerMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SimStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.CurrentGraphicObj = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewWorldMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartPageMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveAsMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveTheWorldMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseViewerMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DashboardMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.AssociateFilesMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowsMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.TileVerticallyMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.TileHorizontallyMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.CascadeMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseAllMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparatorWindows = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.TutorialMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.LanguageMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.EnUSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZhCNMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutUsMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbsTimeLabel, Me.PopulationLabel, Me.LayerStatus, Me.SimStatusLabel, Me.CurrentGraphicObj})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 903)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 13, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1501, 26)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'AbsTimeLabel
        '
        Me.AbsTimeLabel.Name = "AbsTimeLabel"
        Me.AbsTimeLabel.Size = New System.Drawing.Size(112, 21)
        Me.AbsTimeLabel.Text = "AbsTimeLabel"
        '
        'PopulationLabel
        '
        Me.PopulationLabel.Name = "PopulationLabel"
        Me.PopulationLabel.Size = New System.Drawing.Size(88, 21)
        Me.PopulationLabel.Text = "Population"
        '
        'LayerStatus
        '
        Me.LayerStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.LayerStatus.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NoneLayerML, Me.SoilLayerMI})
        Me.LayerStatus.Image = CType(resources.GetObject("LayerStatus.Image"), System.Drawing.Image)
        Me.LayerStatus.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LayerStatus.Name = "LayerStatus"
        Me.LayerStatus.Size = New System.Drawing.Size(62, 24)
        Me.LayerStatus.Text = "Layer"
        '
        'NoneLayerML
        '
        Me.NoneLayerML.Name = "NoneLayerML"
        Me.NoneLayerML.Size = New System.Drawing.Size(124, 26)
        Me.NoneLayerML.Text = "None"
        '
        'SoilLayerMI
        '
        Me.SoilLayerMI.Name = "SoilLayerMI"
        Me.SoilLayerMI.Size = New System.Drawing.Size(124, 26)
        Me.SoilLayerMI.Text = "Soil"
        '
        'SimStatusLabel
        '
        Me.SimStatusLabel.Name = "SimStatusLabel"
        Me.SimStatusLabel.Size = New System.Drawing.Size(1079, 21)
        Me.SimStatusLabel.Spring = True
        Me.SimStatusLabel.Text = "SimStatus"
        '
        'CurrentGraphicObj
        '
        Me.CurrentGraphicObj.Name = "CurrentGraphicObj"
        Me.CurrentGraphicObj.Size = New System.Drawing.Size(146, 21)
        Me.CurrentGraphicObj.Text = "CurrentGraphicObj"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMI, Me.ToolsMI, Me.WindowsMI, Me.HelpMI, Me.DebugMI})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1501, 28)
        Me.MenuStrip1.TabIndex = 4
        '
        'FileMI
        '
        Me.FileMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewMI, Me.OpenMI, Me.StartPageMI, Me.ToolStripSeparator4, Me.SaveAsMI, Me.SaveTheWorldMI, Me.ToolStripSeparator2, Me.ExitMI})
        Me.FileMI.Name = "FileMI"
        Me.FileMI.Size = New System.Drawing.Size(46, 24)
        Me.FileMI.Text = "&File"
        '
        'NewMI
        '
        Me.NewMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewWorldMI})
        Me.NewMI.Image = Global.SimWorld.My.Resources.Resources.Create_256x
        Me.NewMI.Name = "NewMI"
        Me.NewMI.Size = New System.Drawing.Size(262, 26)
        Me.NewMI.Text = "&New"
        '
        'NewWorldMI
        '
        Me.NewWorldMI.Image = Global.SimWorld.My.Resources.Resources.World_256x
        Me.NewWorldMI.Name = "NewWorldMI"
        Me.NewWorldMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewWorldMI.Size = New System.Drawing.Size(187, 26)
        Me.NewWorldMI.Text = "&World"
        '
        'OpenMI
        '
        Me.OpenMI.Image = Global.SimWorld.My.Resources.Resources.OpenFolder_256x
        Me.OpenMI.Name = "OpenMI"
        Me.OpenMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenMI.Size = New System.Drawing.Size(262, 26)
        Me.OpenMI.Text = "&Open..."
        '
        'StartPageMI
        '
        Me.StartPageMI.Name = "StartPageMI"
        Me.StartPageMI.Size = New System.Drawing.Size(262, 26)
        Me.StartPageMI.Text = "Start Page"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(259, 6)
        '
        'SaveAsMI
        '
        Me.SaveAsMI.Image = Global.SimWorld.My.Resources.Resources.Save_256x
        Me.SaveAsMI.Name = "SaveAsMI"
        Me.SaveAsMI.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveAsMI.Size = New System.Drawing.Size(262, 26)
        Me.SaveAsMI.Text = "Save &As..."
        '
        'SaveTheWorldMI
        '
        Me.SaveTheWorldMI.Name = "SaveTheWorldMI"
        Me.SaveTheWorldMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveTheWorldMI.Size = New System.Drawing.Size(262, 26)
        Me.SaveTheWorldMI.Text = "&Save the World..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(259, 6)
        '
        'ExitMI
        '
        Me.ExitMI.Image = Global.SimWorld.My.Resources.Resources.CloseSolution_256x
        Me.ExitMI.Name = "ExitMI"
        Me.ExitMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitMI.Size = New System.Drawing.Size(262, 26)
        Me.ExitMI.Text = "&Exit"
        '
        'ToolsMI
        '
        Me.ToolsMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseViewerMI, Me.DashboardMI, Me.ToolStripSeparator6, Me.AssociateFilesMI})
        Me.ToolsMI.Name = "ToolsMI"
        Me.ToolsMI.Size = New System.Drawing.Size(61, 24)
        Me.ToolsMI.Text = "&Tools"
        '
        'DatabaseViewerMI
        '
        Me.DatabaseViewerMI.Image = Global.SimWorld.My.Resources.Resources.Datalist_256x
        Me.DatabaseViewerMI.Name = "DatabaseViewerMI"
        Me.DatabaseViewerMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.DatabaseViewerMI.Size = New System.Drawing.Size(274, 26)
        Me.DatabaseViewerMI.Text = "Database &Viewer..."
        '
        'DashboardMI
        '
        Me.DashboardMI.Image = Global.SimWorld.My.Resources.Resources.displayconfiguration_256x
        Me.DashboardMI.Name = "DashboardMI"
        Me.DashboardMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.DashboardMI.Size = New System.Drawing.Size(274, 26)
        Me.DashboardMI.Text = "&Dashboard..."
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(271, 6)
        '
        'AssociateFilesMI
        '
        Me.AssociateFilesMI.Name = "AssociateFilesMI"
        Me.AssociateFilesMI.Size = New System.Drawing.Size(274, 26)
        Me.AssociateFilesMI.Text = "Associate &Files"
        '
        'WindowsMI
        '
        Me.WindowsMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TileVerticallyMI, Me.TileHorizontallyMI, Me.CascadeMI, Me.CloseAllMI, Me.ToolStripSeparatorWindows})
        Me.WindowsMI.Name = "WindowsMI"
        Me.WindowsMI.Size = New System.Drawing.Size(88, 24)
        Me.WindowsMI.Text = "&Windows"
        '
        'TileVerticallyMI
        '
        Me.TileVerticallyMI.Name = "TileVerticallyMI"
        Me.TileVerticallyMI.Size = New System.Drawing.Size(202, 26)
        Me.TileVerticallyMI.Text = "Tile &Vertically"
        '
        'TileHorizontallyMI
        '
        Me.TileHorizontallyMI.Name = "TileHorizontallyMI"
        Me.TileHorizontallyMI.Size = New System.Drawing.Size(202, 26)
        Me.TileHorizontallyMI.Text = "Tile &Horizontally"
        '
        'CascadeMI
        '
        Me.CascadeMI.Name = "CascadeMI"
        Me.CascadeMI.Size = New System.Drawing.Size(202, 26)
        Me.CascadeMI.Text = "&Cascade"
        '
        'CloseAllMI
        '
        Me.CloseAllMI.Image = Global.SimWorld.My.Resources.Resources.CloseGroup_256x
        Me.CloseAllMI.Name = "CloseAllMI"
        Me.CloseAllMI.Size = New System.Drawing.Size(202, 26)
        Me.CloseAllMI.Text = "&Close All"
        '
        'ToolStripSeparatorWindows
        '
        Me.ToolStripSeparatorWindows.Name = "ToolStripSeparatorWindows"
        Me.ToolStripSeparatorWindows.Size = New System.Drawing.Size(199, 6)
        '
        'HelpMI
        '
        Me.HelpMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TutorialMI, Me.ToolStripSeparator3, Me.LanguageMI, Me.UpdateMI, Me.ToolStripSeparator5, Me.AboutUsMI})
        Me.HelpMI.Name = "HelpMI"
        Me.HelpMI.Size = New System.Drawing.Size(56, 24)
        Me.HelpMI.Text = "&Help"
        '
        'TutorialMI
        '
        Me.TutorialMI.Enabled = False
        Me.TutorialMI.Name = "TutorialMI"
        Me.TutorialMI.Size = New System.Drawing.Size(164, 26)
        Me.TutorialMI.Text = "Tutorial"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(161, 6)
        '
        'LanguageMI
        '
        Me.LanguageMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnUSMI, Me.ZhCNMI})
        Me.LanguageMI.Image = Global.SimWorld.My.Resources.Resources.Language_256x
        Me.LanguageMI.Name = "LanguageMI"
        Me.LanguageMI.Size = New System.Drawing.Size(164, 26)
        Me.LanguageMI.Text = "Language"
        '
        'EnUSMI
        '
        Me.EnUSMI.Name = "EnUSMI"
        Me.EnUSMI.Size = New System.Drawing.Size(128, 26)
        Me.EnUSMI.Text = "en-US"
        '
        'ZhCNMI
        '
        Me.ZhCNMI.Name = "ZhCNMI"
        Me.ZhCNMI.Size = New System.Drawing.Size(128, 26)
        Me.ZhCNMI.Text = "zh-CN"
        '
        'UpdateMI
        '
        Me.UpdateMI.Image = Global.SimWorld.My.Resources.Resources.Refresh_grey_256x
        Me.UpdateMI.Name = "UpdateMI"
        Me.UpdateMI.Size = New System.Drawing.Size(164, 26)
        Me.UpdateMI.Text = "&Update..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(161, 6)
        '
        'AboutUsMI
        '
        Me.AboutUsMI.Image = Global.SimWorld.My.Resources.Resources.StatusHelp_cyan_256x
        Me.AboutUsMI.Name = "AboutUsMI"
        Me.AboutUsMI.Size = New System.Drawing.Size(164, 26)
        Me.AboutUsMI.Text = "&About Us..."
        '
        'DebugMI
        '
        Me.DebugMI.Name = "DebugMI"
        Me.DebugMI.Size = New System.Drawing.Size(70, 24)
        Me.DebugMI.Text = "Debug"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1501, 929)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormMain"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents AbsTimeLabel As ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents PopulationLabel As ToolStripStatusLabel
    Friend WithEvents SimStatusLabel As ToolStripStatusLabel
    Friend WithEvents FileMI As ToolStripMenuItem
    Friend WithEvents SaveTheWorldMI As ToolStripMenuItem
    Friend WithEvents OpenMI As ToolStripMenuItem
    Friend WithEvents SaveAsMI As ToolStripMenuItem
    Friend WithEvents ExitMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents NewMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents WindowsMI As ToolStripMenuItem
    Friend WithEvents ToolsMI As ToolStripMenuItem
    Friend WithEvents DatabaseViewerMI As ToolStripMenuItem
    Friend WithEvents CloseAllMI As ToolStripMenuItem
    Friend WithEvents NewWorldMI As ToolStripMenuItem
    Friend WithEvents CurrentGraphicObj As ToolStripStatusLabel
    Friend WithEvents ToolStripSeparatorWindows As ToolStripSeparator
    Friend WithEvents TileVerticallyMI As ToolStripMenuItem
    Friend WithEvents TileHorizontallyMI As ToolStripMenuItem
    Friend WithEvents DashboardMI As ToolStripMenuItem
    Friend WithEvents HelpMI As ToolStripMenuItem
    Friend WithEvents AboutUsMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents AssociateFilesMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents UpdateMI As ToolStripMenuItem
    Friend WithEvents CascadeMI As ToolStripMenuItem
    Friend WithEvents StartPageMI As ToolStripMenuItem
    Friend WithEvents DebugMI As ToolStripMenuItem
    Friend WithEvents LanguageMI As ToolStripMenuItem
    Friend WithEvents EnUSMI As ToolStripMenuItem
    Friend WithEvents ZhCNMI As ToolStripMenuItem
    Friend WithEvents LayerStatus As ToolStripDropDownButton
    Friend WithEvents NoneLayerML As ToolStripMenuItem
    Friend WithEvents SoilLayerMI As ToolStripMenuItem
    Friend WithEvents TutorialMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
End Class
