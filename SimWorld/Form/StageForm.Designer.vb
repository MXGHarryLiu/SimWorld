<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StageForm
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ContextMenuStripBg = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyImageCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.Stage = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportStageImageMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportCurrentCreatureMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesolateWorldMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SimulateMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevertWorldMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionMISeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyImageMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.PopulateMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.PopulateFromRNGMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.PopulateFromFileMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.PopulateFromExistingCreatureMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.FinishPopulatingMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelPopulatingMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DepopulateMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.RandomMassacreMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DepopulateManuallyPointMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DepopulateScreeningMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManipulateMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomInMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomOutMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomResetMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomFitMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BackgroundMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckboardMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripFg = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CurrentObjCMFgTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.MarkInTheFieldCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportCurrentCreatureCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeparatorCMFg = New System.Windows.Forms.ToolStripSeparator()
        Me.PopulateFromItCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.KillItCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.SimulateMB = New System.Windows.Forms.ToolStripButton()
        Me.RevertWorldMB = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyImageMB = New System.Windows.Forms.ToolStripButton()
        Me.RefreshMB = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.PopulateMB = New System.Windows.Forms.ToolStripSplitButton()
        Me.PopulateFromRNGMB = New System.Windows.Forms.ToolStripMenuItem()
        Me.PopulateFromFileMB = New System.Windows.Forms.ToolStripMenuItem()
        Me.PopulateFromExistingCreatureMB = New System.Windows.Forms.ToolStripMenuItem()
        Me.DepopulateMB = New System.Windows.Forms.ToolStripSplitButton()
        Me.RandomMassacreMB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.DepopulateManuallyPointMB = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStripBg.SuspendLayout()
        CType(Me.Stage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStripFg.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.ContextMenuStrip = Me.ContextMenuStripBg
        Me.Panel1.Controls.Add(Me.Stage)
        Me.Panel1.Location = New System.Drawing.Point(18, 28)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 199)
        Me.Panel1.TabIndex = 0
        '
        'ContextMenuStripBg
        '
        Me.ContextMenuStripBg.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStripBg.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyImageCMI, Me.RefreshCMI})
        Me.ContextMenuStripBg.Name = "ContextMenuStrip1"
        Me.ContextMenuStripBg.Size = New System.Drawing.Size(197, 56)
        '
        'CopyImageCMI
        '
        Me.CopyImageCMI.Image = Global.SimWorld.My.Resources.Resources.CopyToClipboard_256x
        Me.CopyImageCMI.Name = "CopyImageCMI"
        Me.CopyImageCMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyImageCMI.Size = New System.Drawing.Size(196, 26)
        Me.CopyImageCMI.Text = "&Copy Image"
        '
        'RefreshCMI
        '
        Me.RefreshCMI.Image = Global.SimWorld.My.Resources.Resources.Refresh_grey_256x
        Me.RefreshCMI.Name = "RefreshCMI"
        Me.RefreshCMI.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshCMI.Size = New System.Drawing.Size(196, 26)
        Me.RefreshCMI.Text = "&Refresh"
        '
        'Stage
        '
        Me.Stage.ContextMenuStrip = Me.ContextMenuStripBg
        Me.Stage.Location = New System.Drawing.Point(17, 15)
        Me.Stage.Margin = New System.Windows.Forms.Padding(2)
        Me.Stage.Name = "Stage"
        Me.Stage.Size = New System.Drawing.Size(135, 123)
        Me.Stage.TabIndex = 0
        Me.Stage.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMI, Me.ActionMI, Me.ViewMI})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(820, 25)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Visible = False
        '
        'FileMI
        '
        Me.FileMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportMI})
        Me.FileMI.Name = "FileMI"
        Me.FileMI.Size = New System.Drawing.Size(39, 21)
        Me.FileMI.Text = "&File"
        '
        'ExportMI
        '
        Me.ExportMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportStageImageMI, Me.ExportCurrentCreatureMI, Me.DesolateWorldMI})
        Me.ExportMI.Image = Global.SimWorld.My.Resources.Resources.Forward_256x
        Me.ExportMI.Name = "ExportMI"
        Me.ExportMI.Size = New System.Drawing.Size(114, 22)
        Me.ExportMI.Text = "&Export"
        '
        'ExportStageImageMI
        '
        Me.ExportStageImageMI.Image = Global.SimWorld.My.Resources.Resources.Image_256x
        Me.ExportStageImageMI.Name = "ExportStageImageMI"
        Me.ExportStageImageMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.ExportStageImageMI.Size = New System.Drawing.Size(200, 22)
        Me.ExportStageImageMI.Text = "Stage &Image..."
        '
        'ExportCurrentCreatureMI
        '
        Me.ExportCurrentCreatureMI.Image = Global.SimWorld.My.Resources.Resources.Member_256x
        Me.ExportCurrentCreatureMI.Name = "ExportCurrentCreatureMI"
        Me.ExportCurrentCreatureMI.Size = New System.Drawing.Size(200, 22)
        Me.ExportCurrentCreatureMI.Text = "Current &Creature..."
        '
        'DesolateWorldMI
        '
        Me.DesolateWorldMI.Image = Global.SimWorld.My.Resources.Resources.ObjectFile_256x
        Me.DesolateWorldMI.Name = "DesolateWorldMI"
        Me.DesolateWorldMI.Size = New System.Drawing.Size(200, 22)
        Me.DesolateWorldMI.Text = "Desolate &World..."
        '
        'ActionMI
        '
        Me.ActionMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SimulateMI, Me.RevertWorldMI, Me.ActionMISeparator1, Me.CopyImageMI, Me.RefreshMI, Me.ToolStripSeparator2, Me.PopulateMI, Me.DepopulateMI, Me.ManipulateMI})
        Me.ActionMI.Name = "ActionMI"
        Me.ActionMI.Size = New System.Drawing.Size(56, 21)
        Me.ActionMI.Text = "&Action"
        '
        'SimulateMI
        '
        Me.SimulateMI.Image = Global.SimWorld.My.Resources.Resources.Run_256x
        Me.SimulateMI.Name = "SimulateMI"
        Me.SimulateMI.ShortcutKeys = System.Windows.Forms.Keys.F12
        Me.SimulateMI.Size = New System.Drawing.Size(199, 26)
        Me.SimulateMI.Text = "&Simulate"
        '
        'RevertWorldMI
        '
        Me.RevertWorldMI.Image = Global.SimWorld.My.Resources.Resources.Undo_grey_256x
        Me.RevertWorldMI.Name = "RevertWorldMI"
        Me.RevertWorldMI.Size = New System.Drawing.Size(199, 26)
        Me.RevertWorldMI.Text = "Revert to Last Saved"
        '
        'ActionMISeparator1
        '
        Me.ActionMISeparator1.Name = "ActionMISeparator1"
        Me.ActionMISeparator1.Size = New System.Drawing.Size(196, 6)
        '
        'CopyImageMI
        '
        Me.CopyImageMI.Image = Global.SimWorld.My.Resources.Resources.CopyToClipboard_256x
        Me.CopyImageMI.Name = "CopyImageMI"
        Me.CopyImageMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyImageMI.Size = New System.Drawing.Size(199, 26)
        Me.CopyImageMI.Text = "&Copy Image"
        '
        'RefreshMI
        '
        Me.RefreshMI.Image = Global.SimWorld.My.Resources.Resources.Refresh_grey_256x
        Me.RefreshMI.Name = "RefreshMI"
        Me.RefreshMI.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshMI.Size = New System.Drawing.Size(199, 26)
        Me.RefreshMI.Text = "&Refresh"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(196, 6)
        '
        'PopulateMI
        '
        Me.PopulateMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PopulateFromRNGMI, Me.PopulateFromFileMI, Me.PopulateFromExistingCreatureMI, Me.ToolStripSeparator3, Me.FinishPopulatingMI, Me.CancelPopulatingMI})
        Me.PopulateMI.Image = Global.SimWorld.My.Resources.Resources.AddMember_256x
        Me.PopulateMI.Name = "PopulateMI"
        Me.PopulateMI.Size = New System.Drawing.Size(199, 26)
        Me.PopulateMI.Text = "&Populate"
        '
        'PopulateFromRNGMI
        '
        Me.PopulateFromRNGMI.Name = "PopulateFromRNGMI"
        Me.PopulateFromRNGMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PopulateFromRNGMI.Size = New System.Drawing.Size(208, 22)
        Me.PopulateFromRNGMI.Text = "From &RNG..."
        '
        'PopulateFromFileMI
        '
        Me.PopulateFromFileMI.Name = "PopulateFromFileMI"
        Me.PopulateFromFileMI.Size = New System.Drawing.Size(208, 22)
        Me.PopulateFromFileMI.Text = "From &File..."
        '
        'PopulateFromExistingCreatureMI
        '
        Me.PopulateFromExistingCreatureMI.Name = "PopulateFromExistingCreatureMI"
        Me.PopulateFromExistingCreatureMI.Size = New System.Drawing.Size(208, 22)
        Me.PopulateFromExistingCreatureMI.Text = "From &Existing Creature"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(205, 6)
        '
        'FinishPopulatingMI
        '
        Me.FinishPopulatingMI.Image = Global.SimWorld.My.Resources.Resources.StatusOK_256x
        Me.FinishPopulatingMI.Name = "FinishPopulatingMI"
        Me.FinishPopulatingMI.Size = New System.Drawing.Size(208, 22)
        Me.FinishPopulatingMI.Text = "&Finish Populating"
        '
        'CancelPopulatingMI
        '
        Me.CancelPopulatingMI.Image = Global.SimWorld.My.Resources.Resources.StatusNo_cyan_256x
        Me.CancelPopulatingMI.Name = "CancelPopulatingMI"
        Me.CancelPopulatingMI.Size = New System.Drawing.Size(208, 22)
        Me.CancelPopulatingMI.Text = "&Cancel Populating"
        '
        'DepopulateMI
        '
        Me.DepopulateMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RandomMassacreMI, Me.DepopulateManuallyPointMI, Me.DepopulateScreeningMI})
        Me.DepopulateMI.Image = Global.SimWorld.My.Resources.Resources.Death_256x
        Me.DepopulateMI.Name = "DepopulateMI"
        Me.DepopulateMI.Size = New System.Drawing.Size(199, 26)
        Me.DepopulateMI.Text = "Depopulate"
        '
        'RandomMassacreMI
        '
        Me.RandomMassacreMI.Name = "RandomMassacreMI"
        Me.RandomMassacreMI.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.RandomMassacreMI.Size = New System.Drawing.Size(272, 22)
        Me.RandomMassacreMI.Text = "Random Massacre..."
        '
        'DepopulateManuallyPointMI
        '
        Me.DepopulateManuallyPointMI.Name = "DepopulateManuallyPointMI"
        Me.DepopulateManuallyPointMI.Size = New System.Drawing.Size(272, 22)
        Me.DepopulateManuallyPointMI.Text = "Manually Point"
        '
        'DepopulateScreeningMI
        '
        Me.DepopulateScreeningMI.Name = "DepopulateScreeningMI"
        Me.DepopulateScreeningMI.Size = New System.Drawing.Size(272, 22)
        Me.DepopulateScreeningMI.Text = "Screening..."
        '
        'ManipulateMI
        '
        Me.ManipulateMI.Image = Global.SimWorld.My.Resources.Resources.DataMining_256x
        Me.ManipulateMI.Name = "ManipulateMI"
        Me.ManipulateMI.Size = New System.Drawing.Size(199, 26)
        Me.ManipulateMI.Text = "Manipulate"
        '
        'ViewMI
        '
        Me.ViewMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZoomInMI, Me.ZoomOutMI, Me.ZoomResetMI, Me.ZoomFitMI, Me.ToolStripSeparator1, Me.BackgroundMI})
        Me.ViewMI.Name = "ViewMI"
        Me.ViewMI.Size = New System.Drawing.Size(47, 21)
        Me.ViewMI.Text = "&View"
        '
        'ZoomInMI
        '
        Me.ZoomInMI.Image = Global.SimWorld.My.Resources.Resources.ZoomIn_256x
        Me.ZoomInMI.Name = "ZoomInMI"
        Me.ZoomInMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Oemplus), System.Windows.Forms.Keys)
        Me.ZoomInMI.Size = New System.Drawing.Size(235, 22)
        Me.ZoomInMI.Text = "Zoom &In"
        '
        'ZoomOutMI
        '
        Me.ZoomOutMI.Image = Global.SimWorld.My.Resources.Resources.ZoomOut_32x
        Me.ZoomOutMI.Name = "ZoomOutMI"
        Me.ZoomOutMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.OemMinus), System.Windows.Forms.Keys)
        Me.ZoomOutMI.Size = New System.Drawing.Size(235, 22)
        Me.ZoomOutMI.Text = "Zoom &Out"
        '
        'ZoomResetMI
        '
        Me.ZoomResetMI.Image = Global.SimWorld.My.Resources.Resources.originalsize_256x
        Me.ZoomResetMI.Name = "ZoomResetMI"
        Me.ZoomResetMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.ZoomResetMI.Size = New System.Drawing.Size(235, 22)
        Me.ZoomResetMI.Text = "Zoom &Reset"
        '
        'ZoomFitMI
        '
        Me.ZoomFitMI.Image = Global.SimWorld.My.Resources.Resources.ZoomToFit_32x
        Me.ZoomFitMI.Name = "ZoomFitMI"
        Me.ZoomFitMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.ZoomFitMI.Size = New System.Drawing.Size(235, 22)
        Me.ZoomFitMI.Text = "Zoom &Fit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(232, 6)
        '
        'BackgroundMI
        '
        Me.BackgroundMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CheckboardMI})
        Me.BackgroundMI.Name = "BackgroundMI"
        Me.BackgroundMI.Size = New System.Drawing.Size(235, 22)
        Me.BackgroundMI.Text = "Background"
        '
        'CheckboardMI
        '
        Me.CheckboardMI.Name = "CheckboardMI"
        Me.CheckboardMI.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CheckboardMI.Size = New System.Drawing.Size(226, 22)
        Me.CheckboardMI.Text = "Checkboard"
        '
        'ContextMenuStripFg
        '
        Me.ContextMenuStripFg.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStripFg.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentObjCMFgTitle, Me.MarkInTheFieldCMI, Me.ExportCurrentCreatureCMI, Me.SeparatorCMFg, Me.PopulateFromItCMI, Me.KillItCMI})
        Me.ContextMenuStripFg.Name = "ContextMenuStripFg"
        Me.ContextMenuStripFg.Size = New System.Drawing.Size(225, 140)
        '
        'CurrentObjCMFgTitle
        '
        Me.CurrentObjCMFgTitle.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CurrentObjCMFgTitle.Name = "CurrentObjCMFgTitle"
        Me.CurrentObjCMFgTitle.Size = New System.Drawing.Size(224, 26)
        Me.CurrentObjCMFgTitle.Text = "CurrentObj"
        '
        'MarkInTheFieldCMI
        '
        Me.MarkInTheFieldCMI.Name = "MarkInTheFieldCMI"
        Me.MarkInTheFieldCMI.Size = New System.Drawing.Size(224, 26)
        Me.MarkInTheFieldCMI.Text = "&Mark in the Field"
        '
        'ExportCurrentCreatureCMI
        '
        Me.ExportCurrentCreatureCMI.Name = "ExportCurrentCreatureCMI"
        Me.ExportCurrentCreatureCMI.Size = New System.Drawing.Size(224, 26)
        Me.ExportCurrentCreatureCMI.Text = "&Export Current Creature..."
        '
        'SeparatorCMFg
        '
        Me.SeparatorCMFg.Name = "SeparatorCMFg"
        Me.SeparatorCMFg.Size = New System.Drawing.Size(221, 6)
        '
        'PopulateFromItCMI
        '
        Me.PopulateFromItCMI.Name = "PopulateFromItCMI"
        Me.PopulateFromItCMI.Size = New System.Drawing.Size(224, 26)
        Me.PopulateFromItCMI.Text = "Populate from it"
        '
        'KillItCMI
        '
        Me.KillItCMI.Name = "KillItCMI"
        Me.KillItCMI.Size = New System.Drawing.Size(224, 26)
        Me.KillItCMI.Text = "Kill it"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SimulateMB, Me.RevertWorldMB, Me.ToolStripSeparator4, Me.CopyImageMB, Me.RefreshMB, Me.ToolStripSeparator5, Me.PopulateMB, Me.DepopulateMB, Me.ToolStripSeparator6})
        Me.ToolStrip1.Location = New System.Drawing.Point(305, 28)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(198, 27)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'SimulateMB
        '
        Me.SimulateMB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SimulateMB.Image = Global.SimWorld.My.Resources.Resources.Run_256x
        Me.SimulateMB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SimulateMB.Name = "SimulateMB"
        Me.SimulateMB.Size = New System.Drawing.Size(24, 24)
        Me.SimulateMB.Text = "Simulate"
        '
        'RevertWorldMB
        '
        Me.RevertWorldMB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RevertWorldMB.Image = Global.SimWorld.My.Resources.Resources.Undo_grey_256x
        Me.RevertWorldMB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RevertWorldMB.Name = "RevertWorldMB"
        Me.RevertWorldMB.Size = New System.Drawing.Size(24, 24)
        Me.RevertWorldMB.Text = "Revert to Last Saved"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 27)
        '
        'CopyImageMB
        '
        Me.CopyImageMB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CopyImageMB.Image = Global.SimWorld.My.Resources.Resources.CopyToClipboard_256x
        Me.CopyImageMB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyImageMB.Name = "CopyImageMB"
        Me.CopyImageMB.Size = New System.Drawing.Size(24, 24)
        Me.CopyImageMB.Text = "Copy Image"
        '
        'RefreshMB
        '
        Me.RefreshMB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RefreshMB.Image = Global.SimWorld.My.Resources.Resources.Refresh_grey_256x
        Me.RefreshMB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RefreshMB.Name = "RefreshMB"
        Me.RefreshMB.Size = New System.Drawing.Size(24, 24)
        Me.RefreshMB.Text = "Refresh"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 27)
        '
        'PopulateMB
        '
        Me.PopulateMB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PopulateMB.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PopulateFromRNGMB, Me.PopulateFromFileMB, Me.PopulateFromExistingCreatureMB})
        Me.PopulateMB.Image = Global.SimWorld.My.Resources.Resources.AddMember_256x
        Me.PopulateMB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PopulateMB.Name = "PopulateMB"
        Me.PopulateMB.Size = New System.Drawing.Size(36, 24)
        Me.PopulateMB.Text = "Populate"
        '
        'PopulateFromRNGMB
        '
        Me.PopulateFromRNGMB.Name = "PopulateFromRNGMB"
        Me.PopulateFromRNGMB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PopulateFromRNGMB.Size = New System.Drawing.Size(208, 22)
        Me.PopulateFromRNGMB.Text = "From &RNG..."
        '
        'PopulateFromFileMB
        '
        Me.PopulateFromFileMB.Name = "PopulateFromFileMB"
        Me.PopulateFromFileMB.Size = New System.Drawing.Size(208, 22)
        Me.PopulateFromFileMB.Text = "From &File..."
        '
        'PopulateFromExistingCreatureMB
        '
        Me.PopulateFromExistingCreatureMB.Name = "PopulateFromExistingCreatureMB"
        Me.PopulateFromExistingCreatureMB.Size = New System.Drawing.Size(208, 22)
        Me.PopulateFromExistingCreatureMB.Text = "From &Existing Creature"
        '
        'DepopulateMB
        '
        Me.DepopulateMB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DepopulateMB.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RandomMassacreMB, Me.DepopulateManuallyPointMB})
        Me.DepopulateMB.Image = Global.SimWorld.My.Resources.Resources.Death_256x
        Me.DepopulateMB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DepopulateMB.Name = "DepopulateMB"
        Me.DepopulateMB.Size = New System.Drawing.Size(36, 24)
        Me.DepopulateMB.Text = "ToolStripSplitButton1"
        '
        'RandomMassacreMB
        '
        Me.RandomMassacreMB.Name = "RandomMassacreMB"
        Me.RandomMassacreMB.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.RandomMassacreMB.Size = New System.Drawing.Size(272, 22)
        Me.RandomMassacreMB.Text = "Random Massacre..."
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 27)
        '
        'DepopulateManuallyPointMB
        '
        Me.DepopulateManuallyPointMB.Name = "DepopulateManuallyPointMB"
        Me.DepopulateManuallyPointMB.Size = New System.Drawing.Size(272, 22)
        Me.DepopulateManuallyPointMB.Text = "Manually Point"
        '
        'StageForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(820, 531)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "StageForm"
        Me.Text = "StageForm"
        Me.Panel1.ResumeLayout(False)
        Me.ContextMenuStripBg.ResumeLayout(False)
        CType(Me.Stage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStripFg.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Stage As PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ViewMI As ToolStripMenuItem
    Friend WithEvents ZoomInMI As ToolStripMenuItem
    Friend WithEvents ZoomOutMI As ToolStripMenuItem
    Friend WithEvents ZoomResetMI As ToolStripMenuItem
    Friend WithEvents ZoomFitMI As ToolStripMenuItem
    Friend WithEvents FileMI As ToolStripMenuItem
    Friend WithEvents ExportMI As ToolStripMenuItem
    Friend WithEvents ContextMenuStripBg As ContextMenuStrip
    Friend WithEvents CopyImageCMI As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ContextMenuStripFg As ContextMenuStrip
    Friend WithEvents CurrentObjCMFgTitle As ToolStripMenuItem
    Friend WithEvents MarkInTheFieldCMI As ToolStripMenuItem
    Friend WithEvents ActionMI As ToolStripMenuItem
    Friend WithEvents CopyImageMI As ToolStripMenuItem
    Friend WithEvents ActionMISeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents BackgroundMI As ToolStripMenuItem
    Friend WithEvents CheckboardMI As ToolStripMenuItem
    Friend WithEvents RefreshMI As ToolStripMenuItem
    Friend WithEvents RefreshCMI As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ExportStageImageMI As ToolStripMenuItem
    Friend WithEvents ExportCurrentCreatureMI As ToolStripMenuItem
    Friend WithEvents DesolateWorldMI As ToolStripMenuItem
    Friend WithEvents ExportCurrentCreatureCMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents PopulateMI As ToolStripMenuItem
    Friend WithEvents PopulateFromExistingCreatureMI As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents RefreshMB As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents FinishPopulatingMI As ToolStripMenuItem
    Friend WithEvents CancelPopulatingMI As ToolStripMenuItem
    Friend WithEvents PopulateFromFileMI As ToolStripMenuItem
    Friend WithEvents PopulateFromRNGMI As ToolStripMenuItem
    Friend WithEvents CopyImageMB As ToolStripButton
    Friend WithEvents RevertWorldMI As ToolStripMenuItem
    Friend WithEvents RevertWorldMB As ToolStripButton
    Friend WithEvents SimulateMB As ToolStripButton
    Friend WithEvents SimulateMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents PopulateMB As ToolStripSplitButton
    Friend WithEvents PopulateFromRNGMB As ToolStripMenuItem
    Friend WithEvents PopulateFromFileMB As ToolStripMenuItem
    Friend WithEvents PopulateFromExistingCreatureMB As ToolStripMenuItem
    Friend WithEvents PopulateFromItCMI As ToolStripMenuItem
    Friend WithEvents ManipulateMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents DepopulateMI As ToolStripMenuItem
    Friend WithEvents RandomMassacreMI As ToolStripMenuItem
    Friend WithEvents DepopulateManuallyPointMI As ToolStripMenuItem
    Friend WithEvents DepopulateScreeningMI As ToolStripMenuItem
    Friend WithEvents SeparatorCMFg As ToolStripSeparator
    Friend WithEvents KillItCMI As ToolStripMenuItem
    Friend WithEvents DepopulateMB As ToolStripSplitButton
    Friend WithEvents RandomMassacreMB As ToolStripMenuItem
    Friend WithEvents DepopulateManuallyPointMB As ToolStripMenuItem
End Class
