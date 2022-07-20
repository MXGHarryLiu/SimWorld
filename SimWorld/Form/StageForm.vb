Imports System.Drawing.Drawing2D
Imports System.Runtime.Serialization
Imports SimWorldLib

Public Class StageForm

    Private Enum MouseStates As Byte
        NORMAL = &B0
        DRAGCANVAS = &B1
        POPULATE = &B10
        DEPOPULATE = &B100
    End Enum
    Private MouseState As MouseStates = MouseStates.NORMAL
    Private MouseOrigin As Point
    Private LastScrollPos As Point
    Private CtrlIsDown As Boolean = False
    Private Enum ZoomModeStruct As Byte
        ZOOMIN = &B0
        ZOOMOUT = &B1
        ZOOMRESET = &B10
        ZOOMTOFIT = &B11
    End Enum
    Private Const ZOOMMINRATE As Single = 0.5
    Private Const ZOOMSTEP As Single = 0.1
    Private ZoomRate As Single = 1.0
    Private MouseHoverObj As Integer = 0
    Public Property OriginalSize As Size
    Private TempCreature As Creature = Nothing
    Private PopulateCreatureIDs As List(Of String) = New List(Of String)

#Region "Load and Unload"

    Public Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        With Stage
            .Image = My.Resources.ResourceManager.GetObject("icon")     'debug use
            .Width = Panel1.Width
            .Height = Panel1.Height
            OriginalSize = New Size(.Size)
        End With
    End Sub

    Public Sub New(ByRef thisWorld As World, ByRef CurrentStage As PictureBox)
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        CurrentStage = Me.Stage
        With Stage
            .Image = New Bitmap(CInt(thisWorld.Size.X), CInt(thisWorld.Size.Y))
            .Height = .Image.Height
            .Width = .Image.Width
            OriginalSize = New Size(.Size)
        End With
    End Sub

    Private Sub StageForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.StageFormSize.IsEmpty = False Then
            Me.Size = My.Settings.StageFormSize
        End If
        If My.Settings.StageFormPos.IsEmpty = False Then
            Me.Location = My.Settings.StageFormPos
        End If
        Me.Icon = My.Resources.ResourceManager.GetObject("SimWorld")
        Panel1.Dock = DockStyle.Fill
        FinishPopulatingMI.ShortcutKeyDisplayString = "Enter"
        CancelPopulatingMI.ShortcutKeyDisplayString = "Esc"
        FinishPopulatingMI.Enabled = False
        CancelPopulatingMI.Enabled = False
        MainForm.ToolStripPanelTop.Join(ToolStrip1, MainForm.ToolStripPanelTop.Rows.Count)
        '=====disable all features=====
        With Stage
            .Top = 0
            .Left = 0
            .SizeMode = PictureBoxSizeMode.Zoom
            .BorderStyle = BorderStyle.FixedSingle
            .BackColor = Color.Transparent
            If My.Settings.StageCheckboard = True Then
                .BackgroundImage = My.Resources.ResourceManager.GetObject("checkboardbg")
                CheckboardMI.Checked = True
            Else
                .BackgroundImage = Nothing
                CheckboardMI.Checked = False
            End If
        End With
    End Sub

    Private Sub StageForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MyWorld IsNot Nothing Then
            Dim MsgAns As MsgBoxResult = MsgBox("Are you sure you want to unload the world? ",
                            MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "SimWorld - Unload World")
            If MsgAns = MsgBoxResult.No Then
                e.Cancel = True
            Else
                Call MainForm.UnloadWorld()
            End If
        End If
    End Sub

    Private Sub StageForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainForm.CurrentStage = Nothing
        MainForm.FormList.RemoveAt(Me.Tag - 1)
        MainForm.WindowsMI.DropDownItems.RemoveAt(Me.Tag - 1 + WINDOWSMENUOFFSET)
        Call MainForm.WindowsMI_DropDownOpening(sender, e)
        For Each C As Control In MainForm.ToolStripPanelTop.Controls
            If C Is ToolStrip1 Then
                C.Dispose()
            End If
        Next
        My.Settings.StageFormSize = Me.Size
        My.Settings.StageFormPos = Me.Location
        My.Settings.Save()
    End Sub

#End Region

#Region "Mouse Response"

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown, Stage.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                If MainForm.CurrentDashboard Is Nothing Then
                    Call MainForm.LoadDashboard(MyWorld)
                End If
                If MouseHoverObj = 0 Then       'not hover over a creature
                    MouseState = (MouseState Or MouseStates.DRAGCANVAS)
                    MouseOrigin = e.Location
                    LastScrollPos = Panel1.AutoScrollPosition
                    Me.Cursor = Cursors.SizeAll
                    MainForm.CurrentDashboard.PropertyGrid1.SelectedObject = MyWorld
                Else                              'hovering over a creature
                    MainForm.CurrentDashboard.PropertyGrid1.SelectedObject = MyWorld.Creatures(MouseHoverObj - 1)
                    If (MouseState And MouseStates.POPULATE) = MouseStates.POPULATE Then
                        If TempCreature Is Nothing Then
                            Dim MsgAns As MsgBoxResult = MsgBox("Do you want to make this creature (" & vbNewLine &
    MyWorld.Creatures(MouseHoverObj - 1).ID & ")" & vbNewLine &
    "as the template? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton1, "SimWorld - Populate")
                            If MsgAns = MsgBoxResult.Yes Then
                                TempCreature = MyWorld.Creatures(MouseHoverObj - 1)
                                MyWorld.Creatures(MouseHoverObj - 1).Marked = True
                                Call MainForm.RefreshView(Stage)
                            End If
                        End If
                    Else
                        MainForm.CurrentDashboard.BringToFront()
                    End If
                End If
            Case MouseButtons.Right

        End Select
    End Sub

    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseMove, Stage.MouseMove
        If (MouseState And MouseStates.DRAGCANVAS) = MouseStates.DRAGCANVAS Then
            If sender Is Panel1 Then
                Panel1.AutoScrollPosition = New Point(Math.Abs(LastScrollPos.X) + MouseOrigin.X - e.X,
                    Math.Abs(LastScrollPos.Y) + MouseOrigin.Y - e.Y)
            Else
                Panel1.AutoScrollPosition = New Point(Math.Abs(LastScrollPos.X) + (MouseOrigin.X - e.X),
                    Math.Abs(LastScrollPos.Y) + (MouseOrigin.Y - e.Y))
            End If
        Else
            MainForm.CurrentGraphicObj.Text = CInt(e.X / ZoomRate) & "," & CInt(MyWorld.Size.Y - e.Y / ZoomRate) &
                                           "(x" & CInt(ZoomRate * 100) / 100 & ")"
            If MainForm.Canvas IsNot Nothing Then
                Dim GraphicsPaths As GraphicsPathIterator = New GraphicsPathIterator(MainForm.Canvas)
                Dim Subpath As New GraphicsPath()
                GraphicsPaths.Rewind()
                For i = 0 To GraphicsPaths.SubpathCount - 1 Step 1
                    GraphicsPaths.NextMarker(Subpath)
                    If Subpath.IsVisible(e.X / ZoomRate, e.Y / ZoomRate) Then
                        MouseHoverObj = i
                    End If
                Next i
                Dim MouseInfo As String = ""
                Select Case MouseHoverObj
                    Case 0          'background
                        Me.Cursor = Cursors.Default
                        Stage.ContextMenuStrip = ContextMenuStripBg
                        MouseInfo = "Ambient: " & MyWorld.SunPowerRatio
                    Case Else       'foreground
                        Me.Cursor = Cursors.Hand
                        Stage.ContextMenuStrip = ContextMenuStripFg
                        MouseInfo = "Creature: #" & MouseHoverObj
                        CurrentObjCMFgTitle.Text = "Creature: #" & MouseHoverObj
                End Select
                If MainForm.SoilLayerMI.Checked = True Then
                    If CInt(e.X / ZoomRate) < MyWorld.Size.X And CInt(e.Y / ZoomRate) < MyWorld.Size.Y Then
                        MouseInfo = MouseInfo & ", " &
                            "Soil: " & MyWorld.Soil.Pixel(CInt(e.X / ZoomRate), CInt(e.Y / ZoomRate))
                        MainForm.SimStatusLabel.Text = MouseInfo
                        'ToolTip1.Show(MouseInfo, Me, e.X + 20, e.Y + 60)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseUp, Stage.MouseUp
        If (MouseState And MouseStates.DRAGCANVAS) = MouseStates.DRAGCANVAS Then
            MouseState = (MouseState And Not (MouseStates.DRAGCANVAS))
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Form_MouseWheel(sender As Object, e As MouseEventArgs) Handles Stage.MouseWheel, Panel1.MouseWheel
        If CtrlIsDown = True Then
            Select Case Math.Sign(e.Delta)
                Case 1
                    Zooming(ZoomModeStruct.ZOOMIN, ZOOMSTEP / 2)
                Case -1
                    Zooming(ZoomModeStruct.ZOOMOUT, ZOOMSTEP / 2)
            End Select
        End If
    End Sub

#End Region

#Region "View Menu"

#Region "Zoom"

    Private Sub Zooming(ByVal ZoomMode As ZoomModeStruct, Optional ByVal ZoomStep As Single = ZOOMSTEP)
        With Stage
            Select Case ZoomMode
                Case ZoomModeStruct.ZOOMIN
                    ZoomRate = ZoomRate + ZoomStep
                    ZoomOutMI.Enabled = True
                Case ZoomModeStruct.ZOOMOUT
                    If ZoomRate > ZOOMMINRATE Then
                        ZoomRate = ZoomRate - ZoomStep
                    Else
                        ZoomRate = ZOOMMINRATE
                    End If
                    If ZoomRate <= ZOOMMINRATE Then
                        ZoomOutMI.Enabled = False
                    End If
                Case ZoomModeStruct.ZOOMRESET
                    If ZoomRate <> 1 Then
                        ZoomRate = 1
                    End If
                Case ZoomModeStruct.ZOOMTOFIT
                    Dim Ratio As Double = Panel1.Height / Panel1.Width
                    If OriginalSize.Height / OriginalSize.Width > Ratio Then
                        ZoomRate = Panel1.Height / OriginalSize.Height
                    ElseIf OriginalSize.Height / OriginalSize.Width < Ratio Then
                        ZoomRate = Panel1.Width / OriginalSize.Width
                    End If
            End Select
            .Height = OriginalSize.Height * ZoomRate
            .Width = OriginalSize.Width * ZoomRate
        End With
    End Sub

    Private Sub ZoomInMI_Click(sender As Object, e As EventArgs) Handles ZoomInMI.Click
        Zooming(ZoomModeStruct.ZOOMIN)
    End Sub

    Private Sub ZoomOutMI_Click(sender As Object, e As EventArgs) Handles ZoomOutMI.Click
        Zooming(ZoomModeStruct.ZOOMOUT)
    End Sub

    Private Sub ZoomResetMI_Click(sender As Object, e As EventArgs) Handles ZoomResetMI.Click
        Zooming(ZoomModeStruct.ZOOMRESET)
    End Sub

    Private Sub ZoomFitMI_Click(sender As Object, e As EventArgs) Handles ZoomFitMI.Click
        Zooming(ZoomModeStruct.ZOOMTOFIT)
    End Sub

#End Region

    Private Sub CheckboardMI_Click(sender As Object, e As EventArgs) Handles CheckboardMI.Click
        CheckboardMI.Checked = Not CheckboardMI.Checked
        My.Settings.StageCheckboard = CheckboardMI.Checked
        If CheckboardMI.Checked = True Then
            Stage.BackgroundImage = My.Resources.ResourceManager.GetObject("checkboardbg")
        Else
            Stage.BackgroundImage = Nothing
        End If
        My.Settings.Save()
    End Sub

#End Region

    Private Sub StageForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown, Stage.KeyDown, Panel1.KeyDown
        CtrlIsDown = e.Control
        If (MouseState And MouseStates.POPULATE) = MouseStates.POPULATE Then
            If e.KeyCode = Keys.Enter Then
                If TempCreature IsNot Nothing Then
                    MsgBox("Done populating. ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Populate")
                    MouseState = (MouseState And Not (MouseStates.POPULATE))
                    For i = 0 To PopulateCreatureIDs.Count - 1 Step 1
                        Dim ii As Integer = i
                        MyWorld.Creatures.Find(Function(x) x.ID = PopulateCreatureIDs(ii)).Marked = False
                        MyWorld.LogUserAction(String.Format("Add Creature {0} according to Creature {1}", PopulateCreatureIDs(i), TempCreature.ID))
                    Next
                    PopulateCreatureIDs.Clear()
                    TempCreature.Marked = False
                    Call MainForm.RefreshView(Stage)
                    PopulateFromExistingCreatureMI.Checked = False
                    PopulateFromFileMI.Checked = False
                    If MainForm.CurrentDashboard IsNot Nothing Then
                        Call MainForm.CurrentDashboard.RefreshContent()
                    End If
                Else
                    MsgBox("Please select a template Creature to continue... ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Populate")
                End If
            ElseIf e.KeyCode = Keys.Escape Then
                MsgBox("Populating cancelled. ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Populate")
                MouseState = (MouseState And Not (MouseStates.POPULATE))
                PopulateFromExistingCreatureMI.Checked = False
                PopulateFromFileMI.Checked = False
                If TempCreature IsNot Nothing Then
                    For i = 0 To PopulateCreatureIDs.Count - 1 Step 1
                        Dim ii As Integer = i
                        MyWorld.Creatures.Remove(MyWorld.Creatures.Find(Function(x) x.ID = PopulateCreatureIDs(ii)))
                    Next
                    PopulateCreatureIDs.Clear()
                    TempCreature.Marked = False
                    Call MainForm.RefreshView(Stage)
                End If
            End If
        End If
    End Sub

    Private Sub StageForm_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp, Stage.KeyUp, Panel1.KeyUp
        CtrlIsDown = e.Control
    End Sub

    Private Sub MarkInTheFieldCMI_Click(sender As Object, e As EventArgs) Handles MarkInTheFieldCMI.Click
        MarkInTheFieldCMI.Checked = Not MarkInTheFieldCMI.Checked
        MyWorld.Creatures(MouseHoverObj - 1).Marked = MarkInTheFieldCMI.Checked
        Call MainForm.RefreshView(Stage)
    End Sub

    Private Sub ContextMenuStripFg_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripFg.Opening
        If MouseHoverObj = 0 Or MouseHoverObj > MyWorld.CreatureCount Then 'impossible
            Exit Sub
        End If
        MarkInTheFieldCMI.Checked = MyWorld.Creatures(MouseHoverObj - 1).Marked
    End Sub

    Private Sub RefreshMI_Click(sender As Object, e As EventArgs) Handles RefreshMI.Click, RefreshCMI.Click, RefreshMB.Click
        Call MainForm.RefreshView(Stage)
    End Sub

#Region "Export Menu"

    Private Sub DesolateWorldMI_Click(sender As Object, e As EventArgs) Handles DesolateWorldMI.Click
        Dim DesolateWorld As World = MyWorld.Copy()
        DesolateWorld.Creatures.Clear()
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.Filter = "SimWorld World Files|*.smw"
        SaveFileDialog1.FileName = "Untitled Desolate World"
        If DesolateWorld.WorldFileDir = "" Then   ' Save New
            SaveFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory()
        Else                                      ' Save as copy
            SaveFileDialog1.InitialDirectory = DesolateWorld.WorldFileDir
        End If
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim Path As String = SaveFileDialog1.FileName
            Try
                MainForm.SaveState(DesolateWorld, IO.Path.GetDirectoryName(Path), IO.Path.GetFileName(Path))
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Export World")
                Exit Sub
            End Try
            MainForm.SimStatusLabel.Text = "Desolate World Saved! "
        End If
    End Sub

    Private Sub ExportMI_DropDownOpening(sender As Object, e As EventArgs) Handles ExportMI.DropDownOpening
        If MainForm.CurrentDashboard IsNot Nothing AndAlso TypeOf MainForm.CurrentDashboard.PropertyGrid1.SelectedObject Is Creature Then
            ExportCurrentCreatureMI.Enabled = True
        Else
            ExportCurrentCreatureMI.Enabled = False
        End If
    End Sub

    Private Sub ExportCurrentCreatureMI_Click(sender As Object, e As EventArgs) Handles ExportCurrentCreatureMI.Click, ExportCurrentCreatureCMI.Click
        Dim CurrentCreature As Creature
        If sender Is ExportCurrentCreatureMI Then
            CurrentCreature = TryCast(MainForm.CurrentDashboard.PropertyGrid1.SelectedObject, Creature)
        Else 'ExportCurrentCreatureCMI
            If MouseHoverObj > 0 Then
                CurrentCreature = MyWorld.Creatures(MouseHoverObj - 1)
            Else
                CurrentCreature = Nothing
            End If
        End If
        If CurrentCreature Is Nothing Then 'Extra precaution, impossible
            Exit Sub
        End If
        Dim CreatureToSave As CreatureFromFile = New CreatureFromFile(CurrentCreature)
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.Filter = "SimWorld Creature Files|*.smc"
        SaveFileDialog1.FileName = "Creature " & CreatureToSave.ID
        If MyWorld.WorldFileDir = "" Then
            SaveFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory()
        Else
            SaveFileDialog1.InitialDirectory = MyWorld.WorldFileDir
        End If
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim Path As String = SaveFileDialog1.FileName
            Try
                If System.IO.File.Exists(Path) Then
                    Kill(Path)
                End If
                Dim ser = New DataContractSerializer(GetType(CreatureFromFile))
                Using fs As New IO.FileStream(Path, IO.FileMode.CreateNew), xw = Xml.XmlWriter.Create(fs)
                    ser.WriteObject(xw, CreatureToSave)
                End Using
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Export World")
                Exit Sub
            End Try
            MainForm.SimStatusLabel.Text = "Creature Exported! "
        End If
    End Sub

#End Region

#Region "Action Menu"

    Private Sub Simulate_Click(sender As Object, e As EventArgs) Handles SimulateMI.Click, SimulateMB.Click
        If SimulateMI.Checked = False Then
            SimulateMI.Checked = True
            MainForm.NewMI.Enabled = False
        Else
            SimulateMI.Checked = False
            MainForm.NewMI.Enabled = True
            Exit Sub
        End If
        MainForm.SimStatusLabel.Text = "Running... "
        Dim t As Integer = 0
        Dim ElapsedTimeList As List(Of Long) = New List(Of Long)
        Dim MovingAvgLength As Integer = 10
        Dim FPS As Integer = 0
        Dim Watch As Stopwatch = New Stopwatch()
        Watch.Start()
        While (SimulateMI.Checked = True) 'Or preassigned time
            t = t + 1
            Call MyWorld.Passage()
            If t Mod MyWorld.RefreshRate = 0 Then
                ElapsedTimeList.Add(Watch.ElapsedMilliseconds)
                Watch.Restart()
                If ElapsedTimeList.Count > MovingAvgLength Then
                    ElapsedTimeList.RemoveAt(0)
                End If
                Try
                    FPS = CInt(1000 / (ElapsedTimeList.Sum / ElapsedTimeList.Count))
                Catch ex As Exception
                    FPS = 0
                End Try
                If MyWorld.CreatureCount = 0 Then
                    Call Simulate_Click(sender, e)
                End If
                Call MainForm.RefreshView(Stage)
                If MainForm.CurrentDashboard IsNot Nothing Then
                    Call MainForm.CurrentDashboard.RefreshContent()
                End If
            End If
            MainForm.AbsTimeLabel.Text = String.Format("{0} s ({1} fps)", CInt(MyWorld.T).ToString(), FPS.ToString())
            MainForm.PopulationLabel.Text = MyWorld.CreatureCount
            Application.DoEvents()
        End While
        MainForm.SimStatusLabel.Text = String.Format("{0} {1} {2} ({3} s)", "Finished", t, "timesteps", t * MyWorld.DT)
    End Sub

    Private Sub SimulateMI_CheckedChanged(sender As Object, e As EventArgs) Handles SimulateMI.CheckedChanged
        SimulateMB.Checked = SimulateMI.Checked
        If SimulateMI.Checked = True Then
            SimulateMI.Image = My.Resources.ResourceManager.GetObject("Stop_256x")
        Else
            SimulateMI.Image = My.Resources.ResourceManager.GetObject("Run_256x")
        End If
        SimulateMB.Image = SimulateMI.Image
    End Sub

    Private Sub SimulateMI_EnabledChanged(sender As Object, e As EventArgs) Handles SimulateMI.EnabledChanged
        SimulateMB.Enabled = SimulateMI.Enabled
    End Sub

    Private Sub RevertWorldMI_Click(sender As Object, e As EventArgs) Handles RevertWorldMI.Click, RevertWorldMB.Click
        If MyWorld IsNot Nothing Then
            Dim MsgAns As MsgBoxResult = MsgBox("Do you want to revert the simulation? ",
                    MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SimWorld - Revert World")
            If MsgAns = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        Dim FullPath As String = MyWorld.WorldFileDir & "\" & MyWorld.WorldFile
        If IO.File.Exists(FullPath) = False Then
            MsgBox("No saved check point can be found. ",
                   MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "SimWorld - Revert World")
            Exit Sub
        End If
        Try
            Dim ser = New DataContractSerializer(GetType(World))
            Dim xmlContent As String = My.Computer.FileSystem.ReadAllText(FullPath)
            Using string_reader As New IO.StringReader(xmlContent),
                ms As New IO.MemoryStream(System.Text.Encoding.Default.GetBytes(string_reader.ReadToEnd))
                MyWorld = CType(ser.ReadObject(ms), World)
            End Using
            MyWorld.WorldFileDir = IO.Path.GetDirectoryName(FullPath)
            MyWorld.WorldFile = IO.Path.GetFileName(FullPath)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Revert World")
            Exit Sub
        End Try
        Call MainForm.LoadWorld(MyWorld)
        MainForm.SimStatusLabel.Text = "World reverted! "
    End Sub

    Private Sub RevertWorldMI_EnabledChanged(sender As Object, e As EventArgs) Handles RevertWorldMI.EnabledChanged
        RevertWorldMB.Enabled = RevertWorldMI.Enabled
    End Sub

    Private Sub ExportStageImageMI_Click(sender As Object, e As EventArgs) Handles ExportStageImageMI.Click
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.Filter = "PNG|*.png|JPG|*.jpg;*.jpeg|TIFF|*.tif;*.tiff|Bitmap|*.bmp|" &
            "GIF|*.gif|All supported image files|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff|All files|*.*"
        SaveFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory()
        SaveFileDialog1.FileName = "Untitled Snapshot"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Select Case IO.Path.GetExtension(SaveFileDialog1.FileName).ToLower()
                Case ".png"
                    Stage.Image.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
                Case ".jpg", ".jpeg"
                    Stage.Image.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                Case ".tif", ".tiff"
                    Stage.Image.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Tiff)
                Case ".bmp"
                    Stage.Image.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
                Case ".gif"
                    Stage.Image.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Gif)
                Case Else
                    'extension not supported
            End Select
        End If
    End Sub

    Private Sub CopyImageMI_Click(sender As Object, e As EventArgs) Handles CopyImageCMI.Click, CopyImageMI.Click, CopyImageMB.Click
        Clipboard.SetImage(Stage.Image)
    End Sub

#End Region

#Region "Populate Menu"

    Private Sub PopulateFromExistingCreatureMI_Click(sender As Object, e As EventArgs) Handles PopulateFromExistingCreatureMI.Click, PopulateFromExistingCreatureMB.Click
        MsgBox("To start populate the World from existing Creature: " & vbNewLine &
               "1. Click the template Creature in view; " & vbNewLine &
               "2. Double-click on the location to populate; " & vbNewLine &
               "3. Press <Enter> to confirm the edit or <Esc> to revoke the edit. ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Populate")
        PopulateFromExistingCreatureMI.Checked = True
        MouseState = (MouseState Or MouseStates.POPULATE)
        TempCreature = Nothing
    End Sub

    Private Sub PopulateFromFileMI_Click(sender As Object, e As EventArgs) Handles PopulateFromFileMI.Click, PopulateFromFileMB.Click
        MsgBox("To start populate the World from Creature file: " & vbNewLine &
               "1. Select the Creature file in the file open dialog; " & vbNewLine &
               "2. Double-click on the location to populate; " & vbNewLine &
               "3. Press <Enter> to confirm the edit or <Esc> to revoke the edit. ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Populate")
        Dim OpenFileDiag As New OpenFileDialog
        OpenFileDiag.Filter = "SimWorld Creature Files|*.smc"
        If OpenFileDiag.ShowDialog() = DialogResult.OK Then
            TempCreature = New CreatureFromFile(OpenFileDiag.FileName).ToCreature().Clone()
            PopulateFromFileMI.Checked = True
            MouseState = (MouseState Or MouseStates.POPULATE)
        End If
    End Sub

    Private Sub Stage_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Stage.MouseDoubleClick
        If (MouseState And MouseStates.POPULATE) = MouseStates.POPULATE And TempCreature IsNot Nothing Then
            Dim NewCreature As Creature = TempCreature.Copy()
            PopulateCreatureIDs.Add(NewCreature.ID)
            NewCreature.Marked = True
            NewCreature.Position = New Windows.Media.Media3D.Point3D(e.X / ZoomRate, MyWorld.Size.Y - e.Y / ZoomRate, 0) 'ZZZZZZZZ is 0
            MyWorld.AddCreature(NewCreature)
            Call MainForm.RefreshView(Stage)
        End If
    End Sub

    Private Sub Populate_CheckedChanged(sender As Object, e As EventArgs) Handles _
        PopulateFromExistingCreatureMI.CheckedChanged, PopulateFromFileMI.CheckedChanged,
        PopulateFromRNGMI.CheckedChanged, RandomMassacreMI.CheckedChanged, DepopulateManuallyPointMI.CheckedChanged
        If TryCast(sender, ToolStripMenuItem).Checked = True Then 'Start populate
            SimulateMI.Enabled = False
            RevertWorldMI.Enabled = False
            '-----
            PopulateMB.Enabled = False
            PopulateFromExistingCreatureMI.Enabled = False
            PopulateFromFileMI.Enabled = False
            PopulateFromRNGMI.Enabled = False
            If sender Is PopulateFromExistingCreatureMI Or sender Is PopulateFromFileMI Then
                FinishPopulatingMI.Enabled = True
                CancelPopulatingMI.Enabled = True
            End If
            '-----
            DepopulateMB.Enabled = False                '>
            RandomMassacreMI.Enabled = False
            DepopulateManuallyPointMI.Enabled = False
            DepopulateScreeningMI.Enabled = False
        Else
            SimulateMI.Enabled = True
            RevertWorldMI.Enabled = True
            '-----
            PopulateMB.Enabled = True
            PopulateFromExistingCreatureMI.Enabled = True
            PopulateFromFileMI.Enabled = True
            PopulateFromRNGMI.Enabled = True
            FinishPopulatingMI.Enabled = False
            CancelPopulatingMI.Enabled = False
            '-----
            DepopulateMB.Enabled = True                '>
            RandomMassacreMI.Enabled = True
            DepopulateManuallyPointMI.Enabled = True
            DepopulateScreeningMI.Enabled = True
        End If
    End Sub

    Private Sub PopulateFromRNGMI_EnabledChanged(sender As Object, e As EventArgs) Handles PopulateFromRNGMI.EnabledChanged
        PopulateFromRNGMB.Enabled = PopulateFromRNGMI.Enabled
        PopulateFromRNGMB.Checked = PopulateFromRNGMI.Checked
    End Sub

    Private Sub PopulateFromFileMI_EnabledChanged(sender As Object, e As EventArgs) Handles PopulateFromFileMI.EnabledChanged
        PopulateFromFileMB.Enabled = PopulateFromFileMI.Enabled
        PopulateFromFileMB.Checked = PopulateFromFileMI.Checked
    End Sub

    Private Sub PopulateFromExistingCreatureMI_EnabledChanged(sender As Object, e As EventArgs) Handles PopulateFromExistingCreatureMI.EnabledChanged
        PopulateFromExistingCreatureMB.Enabled = PopulateFromExistingCreatureMI.Enabled
        PopulateFromExistingCreatureMB.Checked = PopulateFromExistingCreatureMI.Checked
    End Sub

    Private Sub PopulateFromRNGMI_Click(sender As Object, e As EventArgs) Handles PopulateFromRNGMI.Click, PopulateFromRNGMB.Click, PopulateMB.ButtonClick
        Dim EntryDiag As EntryDialog = New EntryDialog(1, "SimWorld - Populate", "Please enter the number of" & vbCrLf & "randomly generated Creatures: ")
        Dim ValueChecked As Boolean = False
        Dim CreatureNum As Integer = 5
        EntryDiag.SetInitialText(CreatureNum.ToString())
        While ValueChecked = False
            EntryDiag.Show(Me)
            If EntryDiag.Confirmed = False Then
                Exit Sub
            End If
            If Integer.TryParse(EntryDiag.Results(0), CreatureNum) = True And CreatureNum > 0 Then
                ValueChecked = True
            Else
                MsgBox("The number of Creatures must be a positive integer. ",
                       MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "SimWorld - Populate")
            End If
        End While
        Try
            PopulateFromRNGMI.Checked = True
            Dim NewCreature As Creature = Nothing
            For i = 0 To CreatureNum - 1 Step 1
                NewCreature = New Creature(MyWorld)
                MyWorld.AddCreature(NewCreature)
                If MainForm.CurrentDashboard IsNot Nothing Then
                    Call MainForm.CurrentDashboard.RefreshContent()
                End If
                MainForm.SimStatusLabel.Text = String.Format("Populating ({0}/{1})... ", i + 1, CreatureNum)
                MyWorld.LogUserAction("Add Creature: " & NewCreature.ID)
                Call MainForm.RefreshView(Stage)
                Application.DoEvents()
            Next i
            MainForm.SimStatusLabel.Text = String.Format("Populating finished: added {0} Creature(s). ", CreatureNum)
            PopulateFromRNGMI.Checked = False
        Catch ex As Exception
            MsgBox("Populating terminated! " & vbCrLf & ex.Message,
                MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Populate")
        End Try
    End Sub

    Private Sub PopulateFromItCMI_Click(sender As Object, e As EventArgs) Handles PopulateFromItCMI.Click
        MsgBox("To start populate the World from this Creature: " & vbNewLine &
               "1. Double-click on the location to populate; " & vbNewLine &
               "2. Press <Enter> to confirm the edit or <Esc> to revoke the edit. ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Populate")
        PopulateFromExistingCreatureMI.Checked = True
        MouseState = (MouseState Or MouseStates.POPULATE)
        TempCreature = MyWorld.Creatures(MouseHoverObj - 1)
    End Sub

    Private Sub CancelPopulatingMI_Click(sender As Object, e As EventArgs) Handles CancelPopulatingMI.Click
        Dim ee As KeyEventArgs = New KeyEventArgs(Keys.Escape)
        Call StageForm_KeyDown(Me, ee)
    End Sub

    Private Sub FinishPopulatingMI_Click(sender As Object, e As EventArgs) Handles FinishPopulatingMI.Click
        Dim ee As KeyEventArgs = New KeyEventArgs(Keys.Enter)
        Call StageForm_KeyDown(Me, ee)
    End Sub

#End Region

#Region "Depopulate Menu"

    Private Sub RandomMassacreMI_Click(sender As Object, e As EventArgs) Handles RandomMassacreMI.Click, RandomMassacreMB.Click, DepopulateMB.ButtonClick
        Dim EntryDiag As EntryDialog = New EntryDialog(1, "SimWorld - Depopulate", "Please enter the number of " & vbCrLf & "Creatures to be killed: ")
        Dim ValueChecked As Boolean = False
        Dim CreatureNum As Integer = MyWorld.CreatureCount
        EntryDiag.SetInitialText(CreatureNum.ToString())
        While ValueChecked = False
            EntryDiag.Show(Me)
            If EntryDiag.Confirmed = False Then
                Exit Sub
            End If
            If Integer.TryParse(EntryDiag.Results(0), CreatureNum) = True And CreatureNum > 0 And CreatureNum <= MyWorld.CreatureCount Then
                ValueChecked = True
            Else
                MsgBox("The number of Creatures must be a positive integer and smaller or equal to the total number of Creatures available. ",
                       MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "SimWorld - Depopulate")
            End If
        End While
        Try
            RandomMassacreMI.Checked = True
            Dim RNG As Random = NewRNG()
            Dim RemoveIdx As Integer = -1
            For i = 0 To CreatureNum - 1 Step 1
                RemoveIdx = RNG.Next(0, MyWorld.Creatures.Count - 1)
                If MainForm.CurrentDashboard IsNot Nothing Then
                    Call MainForm.CurrentDashboard.RefreshContent()
                End If
                MainForm.SimStatusLabel.Text = String.Format("Depopulating ({0}/{1})... ", i + 1, CreatureNum)
                MyWorld.LogUserAction("Remove Creature: " & MyWorld.Creatures(RemoveIdx).ID)
                MyWorld.Creatures.RemoveAt(RemoveIdx)
                Call MainForm.RefreshView(Stage)
                Application.DoEvents()
            Next i
            RandomMassacreMI.Checked = False
            MainForm.SimStatusLabel.Text = String.Format("Depopulating finished: removed {0} Creature(s). ", CreatureNum)
        Catch ex As Exception
            MsgBox("Depopulating terminated! " & vbCrLf & ex.Message,
                MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Depopulate")
        End Try
    End Sub

    Private Sub DepopulateManuallyPointMI_Click(sender As Object, e As EventArgs) Handles DepopulateManuallyPointMI.Click
        Dim EntryDiag As EntryDialog = New EntryDialog(1, "SimWorld - Depopulate", "Please enter the number of " & vbCrLf & "Creatures to be killed: ")


    End Sub

    Private Sub KillItCMI_Click(sender As Object, e As EventArgs) Handles KillItCMI.Click
        MyWorld.LogUserAction("Remove Creature: " & MyWorld.Creatures(MouseHoverObj - 1).ID)
        Call MyWorld.Creatures.RemoveAt(MouseHoverObj - 1)
        Call MainForm.RefreshView(Stage)
        If MainForm.CurrentDashboard IsNot Nothing Then
            MainForm.CurrentDashboard.PropertyGrid1.SelectedObject = MyWorld
        End If
    End Sub

    Private Sub RandomMassacreMI_EnabledChanged(sender As Object, e As EventArgs) Handles RandomMassacreMI.EnabledChanged
        RandomMassacreMB.Enabled = RandomMassacreMI.Enabled
    End Sub



#End Region

End Class