<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IniViewer
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveUpMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveDownMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteEntryMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.UndoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReloadMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MoveUpCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveDownCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteEntryCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.UndoCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedoCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReloadCMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.DataTypeImg = New System.Windows.Forms.PictureBox()
        Me.DeleteEntry = New System.Windows.Forms.Button()
        Me.MoveUp = New System.Windows.Forms.Button()
        Me.MoveDown = New System.Windows.Forms.Button()
        Me.ValueText = New System.Windows.Forms.TextBox()
        Me.ComboRoot = New System.Windows.Forms.ComboBox()
        Me.KeyText = New System.Windows.Forms.TextBox()
        Me.LabelValue = New System.Windows.Forms.Label()
        Me.LabelRoot = New System.Windows.Forms.Label()
        Me.LabelKey = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataTypeImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMI, Me.EditMI})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(621, 28)
        Me.MenuStrip1.TabIndex = 0
        '
        'FileMI
        '
        Me.FileMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.SaveMI, Me.SaveAsMI, Me.ToolStripSeparator4, Me.CloseMI})
        Me.FileMI.Name = "FileMI"
        Me.FileMI.Size = New System.Drawing.Size(46, 24)
        Me.FileMI.Text = "&File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Enabled = False
        Me.NewToolStripMenuItem.Image = Global.SimWorld.My.Resources.Resources.Create_256x
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(173, 26)
        Me.NewToolStripMenuItem.Text = "&New"
        '
        'SaveMI
        '
        Me.SaveMI.Image = Global.SimWorld.My.Resources.Resources.Save_256x
        Me.SaveMI.Name = "SaveMI"
        Me.SaveMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveMI.Size = New System.Drawing.Size(173, 26)
        Me.SaveMI.Text = "&Save"
        '
        'SaveAsMI
        '
        Me.SaveAsMI.Name = "SaveAsMI"
        Me.SaveAsMI.Size = New System.Drawing.Size(173, 26)
        Me.SaveAsMI.Text = "Save &As..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(170, 6)
        '
        'CloseMI
        '
        Me.CloseMI.Image = Global.SimWorld.My.Resources.Resources.CloseSolution_256x
        Me.CloseMI.Name = "CloseMI"
        Me.CloseMI.Size = New System.Drawing.Size(173, 26)
        Me.CloseMI.Text = "&Close"
        '
        'EditMI
        '
        Me.EditMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoveUpMI, Me.MoveDownMI, Me.DeleteEntryMI, Me.ToolStripSeparator1, Me.UndoMI, Me.RedoMI, Me.ToolStripSeparator3, Me.ReloadMI})
        Me.EditMI.Name = "EditMI"
        Me.EditMI.Size = New System.Drawing.Size(49, 24)
        Me.EditMI.Text = "&Edit"
        '
        'MoveUpMI
        '
        Me.MoveUpMI.Image = Global.SimWorld.My.Resources.Resources.Upload_gray_256x
        Me.MoveUpMI.Name = "MoveUpMI"
        Me.MoveUpMI.Size = New System.Drawing.Size(179, 26)
        Me.MoveUpMI.Text = "Move &Up"
        '
        'MoveDownMI
        '
        Me.MoveDownMI.Image = Global.SimWorld.My.Resources.Resources.Download_grey_256x
        Me.MoveDownMI.Name = "MoveDownMI"
        Me.MoveDownMI.Size = New System.Drawing.Size(179, 26)
        Me.MoveDownMI.Text = "Move &Down"
        '
        'DeleteEntryMI
        '
        Me.DeleteEntryMI.Image = Global.SimWorld.My.Resources.Resources.Cancel_256x
        Me.DeleteEntryMI.Name = "DeleteEntryMI"
        Me.DeleteEntryMI.Size = New System.Drawing.Size(179, 26)
        Me.DeleteEntryMI.Text = "Delete &Entry"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(176, 6)
        '
        'UndoMI
        '
        Me.UndoMI.Image = Global.SimWorld.My.Resources.Resources.Undo_grey_256x
        Me.UndoMI.Name = "UndoMI"
        Me.UndoMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoMI.Size = New System.Drawing.Size(179, 26)
        Me.UndoMI.Text = "&Undo"
        '
        'RedoMI
        '
        Me.RedoMI.Image = Global.SimWorld.My.Resources.Resources.Redo_grey_256x
        Me.RedoMI.Name = "RedoMI"
        Me.RedoMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.RedoMI.Size = New System.Drawing.Size(179, 26)
        Me.RedoMI.Text = "&Redo"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(176, 6)
        '
        'ReloadMI
        '
        Me.ReloadMI.Image = Global.SimWorld.My.Resources.Resources.Refresh_grey_256x
        Me.ReloadMI.Name = "ReloadMI"
        Me.ReloadMI.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.ReloadMI.Size = New System.Drawing.Size(179, 26)
        Me.ReloadMI.Text = "Re&load"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoveUpCMI, Me.MoveDownCMI, Me.DeleteEntryCMI, Me.ToolStripSeparator2, Me.UndoCMI, Me.RedoCMI, Me.ToolStripSeparator5, Me.ReloadCMI})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(180, 172)
        '
        'MoveUpCMI
        '
        Me.MoveUpCMI.Image = Global.SimWorld.My.Resources.Resources.Upload_gray_256x
        Me.MoveUpCMI.Name = "MoveUpCMI"
        Me.MoveUpCMI.Size = New System.Drawing.Size(179, 26)
        Me.MoveUpCMI.Text = "Move &Up"
        '
        'MoveDownCMI
        '
        Me.MoveDownCMI.Image = Global.SimWorld.My.Resources.Resources.Download_grey_256x
        Me.MoveDownCMI.Name = "MoveDownCMI"
        Me.MoveDownCMI.Size = New System.Drawing.Size(179, 26)
        Me.MoveDownCMI.Text = "Move &Down"
        '
        'DeleteEntryCMI
        '
        Me.DeleteEntryCMI.Image = Global.SimWorld.My.Resources.Resources.Cancel_256x
        Me.DeleteEntryCMI.Name = "DeleteEntryCMI"
        Me.DeleteEntryCMI.Size = New System.Drawing.Size(179, 26)
        Me.DeleteEntryCMI.Text = "Delete &Entry"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(176, 6)
        '
        'UndoCMI
        '
        Me.UndoCMI.Image = Global.SimWorld.My.Resources.Resources.Undo_grey_256x
        Me.UndoCMI.Name = "UndoCMI"
        Me.UndoCMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoCMI.Size = New System.Drawing.Size(179, 26)
        Me.UndoCMI.Text = "&Undo"
        '
        'RedoCMI
        '
        Me.RedoCMI.Image = Global.SimWorld.My.Resources.Resources.Redo_grey_256x
        Me.RedoCMI.Name = "RedoCMI"
        Me.RedoCMI.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.RedoCMI.Size = New System.Drawing.Size(179, 26)
        Me.RedoCMI.Text = "&Redo"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(176, 6)
        '
        'ReloadCMI
        '
        Me.ReloadCMI.Name = "ReloadCMI"
        Me.ReloadCMI.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.ReloadCMI.Size = New System.Drawing.Size(179, 26)
        Me.ReloadCMI.Text = "&Reload"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(19, 52)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataTypeImg)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DeleteEntry)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MoveUp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MoveDown)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ValueText)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ComboRoot)
        Me.SplitContainer1.Panel2.Controls.Add(Me.KeyText)
        Me.SplitContainer1.Panel2.Controls.Add(Me.LabelValue)
        Me.SplitContainer1.Panel2.Controls.Add(Me.LabelRoot)
        Me.SplitContainer1.Panel2.Controls.Add(Me.LabelKey)
        Me.SplitContainer1.Size = New System.Drawing.Size(450, 213)
        Me.SplitContainer1.SplitterDistance = 163
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 5
        '
        'ListView1
        '
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListView1.Location = New System.Drawing.Point(15, 11)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(2)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(128, 152)
        Me.ListView1.TabIndex = 2
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'DataTypeImg
        '
        Me.DataTypeImg.Location = New System.Drawing.Point(56, 184)
        Me.DataTypeImg.Margin = New System.Windows.Forms.Padding(2)
        Me.DataTypeImg.Name = "DataTypeImg"
        Me.DataTypeImg.Size = New System.Drawing.Size(26, 27)
        Me.DataTypeImg.TabIndex = 21
        Me.DataTypeImg.TabStop = False
        '
        'DeleteEntry
        '
        Me.DeleteEntry.BackgroundImage = Global.SimWorld.My.Resources.Resources.Cancel_256x
        Me.DeleteEntry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteEntry.Location = New System.Drawing.Point(10, 169)
        Me.DeleteEntry.Margin = New System.Windows.Forms.Padding(2)
        Me.DeleteEntry.Name = "DeleteEntry"
        Me.DeleteEntry.Size = New System.Drawing.Size(30, 32)
        Me.DeleteEntry.TabIndex = 20
        Me.DeleteEntry.UseVisualStyleBackColor = True
        '
        'MoveUp
        '
        Me.MoveUp.BackgroundImage = Global.SimWorld.My.Resources.Resources.Upload_gray_256x
        Me.MoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MoveUp.Location = New System.Drawing.Point(10, 93)
        Me.MoveUp.Margin = New System.Windows.Forms.Padding(2)
        Me.MoveUp.Name = "MoveUp"
        Me.MoveUp.Size = New System.Drawing.Size(30, 32)
        Me.MoveUp.TabIndex = 19
        Me.MoveUp.UseVisualStyleBackColor = True
        '
        'MoveDown
        '
        Me.MoveDown.BackgroundImage = Global.SimWorld.My.Resources.Resources.Download_grey_256x
        Me.MoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MoveDown.Location = New System.Drawing.Point(10, 130)
        Me.MoveDown.Margin = New System.Windows.Forms.Padding(2)
        Me.MoveDown.Name = "MoveDown"
        Me.MoveDown.Size = New System.Drawing.Size(30, 32)
        Me.MoveDown.TabIndex = 18
        Me.MoveDown.UseVisualStyleBackColor = True
        '
        'ValueText
        '
        Me.ValueText.Location = New System.Drawing.Point(56, 58)
        Me.ValueText.Margin = New System.Windows.Forms.Padding(2)
        Me.ValueText.Multiline = True
        Me.ValueText.Name = "ValueText"
        Me.ValueText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ValueText.Size = New System.Drawing.Size(216, 112)
        Me.ValueText.TabIndex = 17
        '
        'ComboRoot
        '
        Me.ComboRoot.FormattingEnabled = True
        Me.ComboRoot.Location = New System.Drawing.Point(56, 11)
        Me.ComboRoot.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboRoot.Name = "ComboRoot"
        Me.ComboRoot.Size = New System.Drawing.Size(216, 21)
        Me.ComboRoot.TabIndex = 16
        '
        'KeyText
        '
        Me.KeyText.Location = New System.Drawing.Point(56, 36)
        Me.KeyText.Margin = New System.Windows.Forms.Padding(2)
        Me.KeyText.Name = "KeyText"
        Me.KeyText.Size = New System.Drawing.Size(216, 20)
        Me.KeyText.TabIndex = 15
        '
        'LabelValue
        '
        Me.LabelValue.AutoSize = True
        Me.LabelValue.Location = New System.Drawing.Point(8, 61)
        Me.LabelValue.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelValue.Name = "LabelValue"
        Me.LabelValue.Size = New System.Drawing.Size(41, 15)
        Me.LabelValue.TabIndex = 14
        Me.LabelValue.Text = "Value:"
        '
        'LabelRoot
        '
        Me.LabelRoot.AutoSize = True
        Me.LabelRoot.Location = New System.Drawing.Point(8, 11)
        Me.LabelRoot.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelRoot.Name = "LabelRoot"
        Me.LabelRoot.Size = New System.Drawing.Size(51, 15)
        Me.LabelRoot.TabIndex = 13
        Me.LabelRoot.Text = "Section:"
        '
        'LabelKey
        '
        Me.LabelKey.AutoSize = True
        Me.LabelKey.Location = New System.Drawing.Point(8, 37)
        Me.LabelKey.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelKey.Name = "LabelKey"
        Me.LabelKey.Size = New System.Drawing.Size(30, 15)
        Me.LabelKey.TabIndex = 12
        Me.LabelKey.Text = "Key:"
        '
        'IniViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(621, 428)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "IniViewer"
        Me.Text = "IniViewer"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataTypeImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents EditMI As ToolStripMenuItem
    Friend WithEvents MoveUpMI As ToolStripMenuItem
    Friend WithEvents MoveDownMI As ToolStripMenuItem
    Friend WithEvents DeleteEntryMI As ToolStripMenuItem
    Friend WithEvents UndoMI As ToolStripMenuItem
    Friend WithEvents RedoMI As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents MoveUpCMI As ToolStripMenuItem
    Friend WithEvents MoveDownCMI As ToolStripMenuItem
    Friend WithEvents DeleteEntryCMI As ToolStripMenuItem
    Friend WithEvents UndoCMI As ToolStripMenuItem
    Friend WithEvents RedoCMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents FileMI As ToolStripMenuItem
    Friend WithEvents SaveMI As ToolStripMenuItem
    Friend WithEvents SaveAsMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents CloseMI As ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReloadMI As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ReloadCMI As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ListView1 As ListView
    Friend WithEvents DataTypeImg As PictureBox
    Friend WithEvents DeleteEntry As Button
    Friend WithEvents MoveUp As Button
    Friend WithEvents MoveDown As Button
    Friend WithEvents ValueText As TextBox
    Friend WithEvents ComboRoot As ComboBox
    Friend WithEvents KeyText As TextBox
    Friend WithEvents LabelValue As Label
    Friend WithEvents LabelRoot As Label
    Friend WithEvents LabelKey As Label
End Class
