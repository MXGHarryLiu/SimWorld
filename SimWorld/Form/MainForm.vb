Imports System.Drawing.Drawing2D
Imports System.Net
Imports System.Runtime.Serialization
Imports MathNet.Numerics
Imports SimWorldLib

Public Class MainForm

    Friend FormList As List(Of Form) = New List(Of Form)
    Friend CurrentStage As PictureBox = Nothing
    Friend Canvas As GraphicsPath = New GraphicsPath()
    Friend CurrentDashboard As Dashboard = Nothing
    Friend ToolStripPanelTop As ToolStripPanel = New ToolStripPanel()

#Region "Constructor and Destructor"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        If My.Settings.MainFormSize.IsEmpty = False Then
            Me.Size = My.Settings.MainFormSize
        End If
        For Each C As Windows.Forms.Control In Me.Controls
            If TypeOf C Is MdiClient Then
                C.BackColor = Color.AliceBlue
                Exit For
            End If
        Next
        Me.Text = "SimWorld - " & My.Application.Info.Version.ToString()
        Me.Icon = My.Resources.ResourceManager.GetObject("SimWorld")
        SimStatusLabel.Text = "Welcome! "
        ToolStripPanelTop.Dock = DockStyle.Top
        ToolStripPanelTop.Join(MenuStrip1)
        Me.Controls.Add(ToolStripPanelTop)
        If My.Settings.CultureName <> "" Then
            Culture = Globalization.CultureInfo.CreateSpecificCulture(My.Settings.CultureName)
        End If
        If Culture Is Nothing Then
            Culture = Globalization.CultureInfo.CreateSpecificCulture("en-US")
        End If
        Select Case My.Settings.CultureName
            Case "en-US"
                EnUSMI.Checked = True
            Case "zh-CN"
                ZhCNMI.Checked = True
            Case Else
                MsgBox("Language is not supported!", MsgBoxStyle.Critical, "SimWorld - Language")
        End Select
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call UnloadWorld()
        If Environment.GetCommandLineArgs().Length > 1 Then
            OpenMI_Click(sender, e)
        End If
        Call ShowStartPage()
        Call OneclickRunMI_Click(sender, e) 'Debug use!!!!!!!!!!!!!!!!!!!!!!!!!
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.MainFormSize = Me.Size
        My.Settings.Save()
        If MyWorld Is Nothing Then
            Exit Sub
        End If
        Dim MsgAns As MsgBoxResult = MsgBox("A simulation is open. " & vbCrLf & "Do you want to save the current simulation? ",
                      MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton3, "SimWorld - Exit")
        If MsgAns = MsgBoxResult.Cancel Then
            e.Cancel = True
        ElseIf MsgAns = MsgBoxResult.Yes Then
            If SaveTheWorldMI.Enabled = True Then
                SaveTheWorldMI_Click(sender, e)
            Else
                SaveAsMI_Click(sender, e)
            End If
        End If
    End Sub

#End Region

#Region "Menu Action and Formatting"

#Region "File Menu"

    Public Sub NewWorldMI_Click(sender As Object, e As EventArgs) Handles NewWorldMI.Click
        If MyWorld IsNot Nothing Then
            Dim MsgAns As MsgBoxResult = MsgBox("A World has been loaded. " & vbCrLf &
                            "Do you want to initialize and discard all the results? ",
                            MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SimWorld - Initialize World")
            If MsgAns = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        Dim InputWidth As Double = 600.0R
        Dim InputHeight As Double = 400.0R
        Dim ValueChecked As Boolean = False
        Dim EntryDiag As EntryDialog = New EntryDialog(2, "SimWorld - New World", "Set the World dimension: ", "Width", "Height")
        EntryDiag.SetInitialText(InputWidth.ToString(), InputHeight.ToString())
        While ValueChecked = False
            EntryDiag.Show(Me)
            If EntryDiag.Confirmed = False Then
                Exit Sub
            End If
            If Double.TryParse(EntryDiag.Results(0), InputWidth) = True And
                Double.TryParse(EntryDiag.Results(1), InputHeight) = True And InputWidth >= 1 And InputHeight >= 1 Then
                ValueChecked = True
            Else
                MsgBox("World width and height must be positive numbers. ",
                       MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "SimWorld - New World")
            End If
        End While
        Call HideStartPage()
        MyWorld = New World(InputWidth, InputHeight, 0)
        MyWorld.WorldFile = "Untitled World.smw"
        Call LoadWorld(MyWorld)
        SimStatusLabel.Text = MyWorld.LogUserAction(String.Format("Initialize World ({0}x{1})", InputWidth, InputHeight))
    End Sub

    Public Sub OpenMI_Click(sender As Object, e As EventArgs, Optional ByVal inPath As String = Nothing) Handles OpenMI.Click
        Dim Path As String = ""
        Dim OpenFileDialog1 As New OpenFileDialog
        If sender Is OpenMI Then  'open from MainForm menu
            OpenFileDialog1.Filter = "All Supported Files|*.smw;*.smc;*.ini|SimWorld World Files|*.smw|SimWorld Creature Files|*.smc|Ini Configuration Files|*.ini|All files|*.*"
            OpenFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory()
            If OpenFileDialog1.ShowDialog() <> DialogResult.OK Then
                Exit Sub
            Else
                Path = OpenFileDialog1.FileName
            End If
        ElseIf inPath IsNot Nothing Then    ' open from startpage
            Path = inPath
        Else                                ' open from argument
            Path = Environment.GetCommandLineArgs(1)
        End If
        Select Case IO.Path.GetExtension(Path)
            Case ".smw"
                If MyWorld IsNot Nothing Then
                    Dim MsgAns As MsgBoxResult = MsgBox("A World has been loaded. " & vbCrLf &
                            "Do you want to open another World and discard all the results? ",
                            MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SimWorld - Open World")
                    If MsgAns = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
                Try
                    ' DataContract Deserializer ====================
                    Dim ser = New DataContractSerializer(GetType(World))
                    Dim xmlContent As String = My.Computer.FileSystem.ReadAllText(Path)
                    Using string_reader As New IO.StringReader(xmlContent),
                        ms As New IO.MemoryStream(System.Text.Encoding.Default.GetBytes(string_reader.ReadToEnd))
                        MyWorld = CType(ser.ReadObject(ms), World)
                    End Using
                    MyWorld.WorldFileDir = IO.Path.GetDirectoryName(Path)
                    MyWorld.WorldFile = IO.Path.GetFileName(Path)
                    ' Check World file version ======================
                    Dim FileVersion As Byte = MyWorld.WorldVersion
                    If FileVersion <> WORLDVERSION Then
                        Dim MsgAns As MsgBoxResult
                        If FileVersion < WORLDVERSION Then
                            MsgAns = MsgBox("The loading file is a previous version of World. " & vbCrLf &
                            "Current version of the SimWorld may not able to handle it. " & vbCrLf &
                            "You may lose features supported in the SimWorld. " & vbCrLf &
                            "Do you want to continue (NOT RECOMMENDED)? ",
                            MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "SimWorld - Load World")
                        Else
                            MsgAns = MsgBox("The loading file is a future version of World. " & vbCrLf &
                            "Current version of the SimWorld may not able to handle it. " & vbCrLf &
                            "Please upgrade to the latest version of SimWorld. " & vbCrLf &
                            "Do you want to continue using the World (NOT RECOMMENDED)? ",
                            MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "SimWorld - Load World")
                        End If
                        If MsgAns = MsgBoxResult.No Then
                            Throw New SimErr.WorldVersionDifferentException("Loading terminated by the user due to different World version. ")
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Load World")
                    Exit Sub
                End Try
                Call LoadWorld(MyWorld)
                SimStatusLabel.Text = "World file loaded! "
            Case ".smc"
                SimStatusLabel.Text = "Loading smc file ... "
                MsgBox("View and edit a single Creature file is under development. ") '!!!!!!!!!!!!
                SimStatusLabel.Text = "Creature file loaded! "
            Case ".ini"
                SimStatusLabel.Text = "Loading ini file ... "
                Call LoadIniViewer(Path)
                SimStatusLabel.Text = "Ini file loaded! "
            Case Else
                MsgBox("Sorry but SimWorld doesn't know how to deal with the file. ",
                      MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "SimWorld")
                Exit Sub
        End Select
        ' Update LastOpen Setting =====================================
        If My.Settings.LastOpen Is Nothing Then
            My.Settings.LastOpen = New Specialized.StringCollection()
        End If
        For i As Integer = 0 To My.Settings.LastOpen.Count() - 1 Step 1
            If Path = My.Settings.LastOpen(i) Then ' Move forward same entry
                My.Settings.LastOpen.RemoveAt(i)
                Exit For
            End If
        Next i
        My.Settings.LastOpen.Add(Path)
        If My.Settings.LastOpen.Count > MAXLASTOPENENTRY Then
            My.Settings.LastOpen.RemoveAt(0)
        End If
        My.Settings.Save()
        Call HideStartPage()
    End Sub

    Private Sub StartPageMI_Click(sender As Object, e As EventArgs) Handles StartPageMI.Click
        For Each ChildForm As Form In Me.MdiChildren
            If TypeOf ChildForm Is StartPage Then
                Exit Sub
            End If
        Next
        Call ShowStartPage()
    End Sub

    Private Sub ShowStartPage()
        Dim ClientHeight As Double = 0
        For Each C As Windows.Forms.Control In Me.Controls
            If TypeOf C Is MdiClient Then
                C.BackColor = Color.AliceBlue
                ClientHeight = C.Height
                Exit For
            End If
        Next
        Dim StartPageF As StartPage = New StartPage() With {.MdiParent = Me}
        StartPageF.Location = New Point((Me.Width - StartPageF.Width) / 2,
                                        (ClientHeight - StartPageF.Height) / 2)
        StartPageF.Show()
    End Sub

    Private Sub HideStartPage()
        For Each ChildForm As Form In Me.MdiChildren
            If TypeOf ChildForm Is StartPage Then
                ChildForm.Close()
            End If
        Next
    End Sub

    Private Sub SaveAsMI_Click(sender As Object, e As EventArgs) Handles SaveAsMI.Click
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.Filter = "SimWorld World Files|*.smw|All files|*.*"
        If MyWorld IsNot Nothing Then
            If MyWorld.WorldFileDir = "" Then   ' Save New
                SaveFileDialog1.FileName = "Untitled World"
                SaveFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory()
            Else                                ' Save as copy
                SaveFileDialog1.FileName = IO.Path.GetFileNameWithoutExtension(MyWorld.WorldFile) & " - copy"
                SaveFileDialog1.InitialDirectory = MyWorld.WorldFileDir
            End If
        End If
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim Path As String = SaveFileDialog1.FileName
            Try
                SaveState(MyWorld, IO.Path.GetDirectoryName(Path), IO.Path.GetFileName(Path))
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Save World")
                Exit Sub
            End Try
            SaveTheWorldMI.Enabled = True
            If CurrentStage IsNot Nothing Then
                TryCast(CurrentStage.Parent.Parent, StageForm).Text = MyWorld.WorldFile & " - Stage"
            End If
            If CurrentDashboard IsNot Nothing Then
                TryCast(CurrentDashboard, Dashboard).Text = MyWorld.WorldFile & " - Dashboard"
            End If
            SimStatusLabel.Text = "State Saved! "
        End If
    End Sub

    Private Sub SaveTheWorldMI_Click(sender As Object, e As EventArgs) Handles SaveTheWorldMI.Click
        Try
            Call SaveState(MyWorld)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Save World")
            Exit Sub
        End Try
        SimStatusLabel.Text = "State Saved! "
    End Sub

    Private Sub ExitMI_Click(sender As Object, e As EventArgs) Handles ExitMI.Click
        Application.Exit()
    End Sub

#End Region

#Region "Tools Menu"

    Private Sub DataViewerMI_Click(sender As Object, e As EventArgs) Handles DatabaseViewerMI.Click
        Call LoadIniViewer()
    End Sub

    Private Sub LoadIniViewer(Optional ByVal Path As String = "")
        Dim FormName As String
        If Path = "" Then
            FormName = "[Empty]"
        Else
            FormName = IO.Path.GetFileName(Path)
        End If
        Dim FormID As Integer = FormList.Count + 1
        Dim IniViewerF As IniViewer = New IniViewer(Path) With
            {.MdiParent = Me, .Tag = FormID, .Text = FormName & " - Data Viewer"}
        Dim ChildFormMenu As ToolStripMenuItem = New ToolStripMenuItem() With
             {.Text = FormID.ToString() & " " & IniViewerF.Text, .Tag = FormID}
        AddHandler ChildFormMenu.Click, AddressOf ChildFormMenu_Click
        WindowsMI.DropDownItems.Add(ChildFormMenu)
        FormList.Add(IniViewerF)
        IniViewerF.Show()
    End Sub

    Private Sub DashboardMI_Click(sender As Object, e As EventArgs) Handles DashboardMI.Click
        If MyWorld IsNot Nothing Then
            Call LoadDashboard(MyWorld)
        End If
    End Sub

    Public Sub LoadDashboard(Optional ByRef thisWorld As World = Nothing)
        If CurrentDashboard IsNot Nothing Then 'prevent multiple instantiation
            CurrentDashboard.BringToFront()
            Exit Sub
        End If
        Dim FormName As String = ""
        Dim FormID As Integer = FormList.Count + 1
        Dim DashboardF As Dashboard
        If thisWorld Is Nothing Then 'nothing is not allowed except debugging
            FormName = "[Empty]"
            DashboardF = New Dashboard() With
             {.MdiParent = Me, .Tag = FormID, .Text = FormName & " - Dashboard"}
        Else
            FormName = thisWorld.WorldFile
            DashboardF = New Dashboard() With
             {.MdiParent = Me, .Tag = FormID, .Text = FormName & " - Dashboard"}
        End If
        Dim ChildFormMenu As ToolStripMenuItem = New ToolStripMenuItem() With
             {.Text = FormID.ToString() & " " & DashboardF.Text, .Tag = FormID, .Name = FormName}
        AddHandler ChildFormMenu.Click, AddressOf ChildFormMenu_Click
        WindowsMI.DropDownItems.Add(ChildFormMenu)
        FormList.Add(DashboardF)
        CurrentDashboard = DashboardF
        CurrentDashboard.PropertyGrid1.SelectedObject = thisWorld
        DashboardF.Show()
    End Sub

    Private Sub AssociateFilesMI_Click(sender As Object, e As EventArgs) Handles AssociateFilesMI.Click
        Try
            If My.Computer.Registry.ClassesRoot.OpenSubKey("SimWorld.World", True) Is Nothing Then
                Dim IconPath As String
                '=====smw=====
                My.Computer.Registry.ClassesRoot.CreateSubKey(".smw").SetValue("",
                         "SimWorld.World", Microsoft.Win32.RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("SimWorld.World").SetValue("",
                     "SimWorld World File", Microsoft.Win32.RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("SimWorld.World\shell\open\command").SetValue("",
                     Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
                IconPath = IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Resources\SimWorldSMW.ico"
                My.Computer.Registry.ClassesRoot.CreateSubKey("SimWorld.World\DefaultIcon").SetValue("", IconPath)
                '=====smc=====
                My.Computer.Registry.ClassesRoot.CreateSubKey(".smc").SetValue("",
                         "SimWorld.Creature", Microsoft.Win32.RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("SimWorld.Creature").SetValue("",
                     "SimWorld Creature File", Microsoft.Win32.RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("SimWorld.Creature\shell\open\command").SetValue("",
                     Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
                IconPath = IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Resources\SimWorldSMC.ico"
                My.Computer.Registry.ClassesRoot.CreateSubKey("SimWorld.Creature\DefaultIcon").SetValue("", IconPath)
                SimStatusLabel.Text = "File extensions are successfully associated. "
            Else
                SimStatusLabel.Text = "File extensions have already been associated. "
            End If
        Catch
            MsgBox("Please run the program as administrator. ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - File Association")
        End Try
    End Sub

#End Region

#Region "Windows Menu"

    Public Sub WindowsMI_DropDownOpening(sender As Object, e As EventArgs) Handles WindowsMI.DropDownOpening
        If FormList.Count = 0 Then
            CloseAllMI.Enabled = False
            ToolStripSeparatorWindows.Visible = False
            TileVerticallyMI.Enabled = False
            TileHorizontallyMI.Enabled = False
            CascadeMI.Enabled = False
        Else
            CloseAllMI.Enabled = True
            ToolStripSeparatorWindows.Visible = True
            TileVerticallyMI.Enabled = True
            TileHorizontallyMI.Enabled = True
            CascadeMI.Enabled = True
            Dim CurrentSubMenu As ToolStripMenuItem = Nothing
            For i = 1 To FormList.Count Step 1
                CurrentSubMenu = TryCast(WindowsMI.DropDownItems(i - 1 + WINDOWSMENUOFFSET),
                    ToolStripMenuItem)
                CurrentSubMenu.Checked = (i = Me.ActiveMdiChild.Tag)
                CurrentSubMenu.Text = i & " " & FormList(i - 1).Text
                CurrentSubMenu.Tag = i
                FormList(i - 1).Tag = i
            Next
        End If
    End Sub

    Private Sub TileVerticallyMI_Click(sender As Object, e As EventArgs) Handles TileVerticallyMI.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontallyMI_Click(sender As Object, e As EventArgs) Handles TileHorizontallyMI.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub CascadeMI_Click(sender As Object, e As EventArgs) Handles CascadeMI.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub CloseAllMI_Click(sender As Object, e As EventArgs) Handles CloseAllMI.Click
        For i = FormList.Count - 1 To 0 Step -1
            FormList(i).Close()
        Next i
    End Sub

    Private Sub ChildFormMenu_Click(sender As Object, e As EventArgs)
        Dim thisMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        FormList(CInt(thisMenu.Tag) - 1).BringToFront()
    End Sub

#End Region

#Region "Help Menu"

    Private Sub LanguageSubMI_Click(sender As Object, e As EventArgs) Handles EnUSMI.Click， ZhCNMI.Click
        EnUSMI.Checked = False
        ZhCNMI.Checked = False
        If sender Is EnUSMI Then
            EnUSMI.Checked = True
        Else 'ZhCNMI
            ZhCNMI.Checked = True
        End If
        My.Settings.CultureName = TryCast(sender, ToolStripMenuItem).Text
        My.Settings.Save()
        Culture = Globalization.CultureInfo.CreateSpecificCulture(My.Settings.CultureName)
    End Sub

    Private Sub UpdateMI_Click(sender As Object, e As EventArgs) Handles UpdateMI.Click
        Dim request As HttpWebRequest = HttpWebRequest.Create("http://harry.liu.web.rice.edu/internal/SimWorldVer.ini")
        request.Timeout = 5000
        Dim response As HttpWebResponse
        Try
            response = request.GetResponse()
        Catch ex As Exception
            MsgBox("Please check internet connection and try again. " & vbNewLine &
                     ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld - Update")
            Exit Sub
        End Try
        Dim sr As IO.StreamReader = New IO.StreamReader(response.GetResponseStream())
        Dim OnlineVersionString As String = sr.ReadLine()
        Dim UpgradeURL As String = sr.ReadLine()
        sr.Close()
        Dim OnlineVersion As Version = New Version(OnlineVersionString)
        Select Case My.Application.Info.Version.CompareTo(OnlineVersion)
            Case Is < 0
                MsgBox("There are new version available!" & vbNewLine &
                       OnlineVersion.ToString & "(Released time: " & BuildTime(OnlineVersion) & ")" & vbNewLine &
                       "You can download it at: " & vbNewLine &
                       UpgradeURL,
                       MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Update")
            Case 0
                MsgBox("Your copy of the SimWorld is the latest version. " & vbNewLine &
                       "Released Time: " & BuildTime(),
                       MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Update")
            Case Else
                MsgBox("You must be the developer! " & vbNewLine &
                       "You may push the current version: " & vbNewLine &
                       My.Application.Info.Version.ToString() & " (released time: " & BuildTime() & ")" & vbNewLine &
                       "to replace the online version: " & vbNewLine &
                       OnlineVersion.ToString & " (released time: " & BuildTime(OnlineVersion) & "). ",
                       MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SimWorld - Update")
        End Select
    End Sub

    Private Sub AboutUsMI_Click(sender As Object, e As EventArgs) Handles AboutUsMI.Click
        MsgBox(Me.Text & vbNewLine _
            & vbNewLine _
            & "--English Version (beta)" & vbNewLine _
            & "A vibrant world with artificial lives" & vbNewLine _
            & "lzhchild@hotmail.com" & vbNewLine _
            & vbNewLine _
            & "© 2016 - 2017 MXG Flying Studio", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "About Us")
    End Sub

#End Region

#End Region

    Private Sub MainForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' reposition startpage
        Dim ClientHeight As Double = 0
        For Each ChildForm As Form In Me.MdiChildren
            If TypeOf ChildForm Is StartPage Then
                For Each C As Windows.Forms.Control In Me.Controls
                    If TypeOf C Is MdiClient Then
                        ClientHeight = C.Height
                        Exit For
                    End If
                Next
                ChildForm.Location = New Point((Me.Width - ChildForm.Width) / 2,
                                        (ClientHeight - ChildForm.Height) / 2)
                Exit For
            End If
        Next
    End Sub

    Private Sub MainForm_MdiChildActivate(sender As Object, e As EventArgs) Handles Me.MdiChildActivate
        ToolStripManager.RevertMerge(Me.MenuStrip1)
        If TypeOf Me.ActiveMdiChild Is StageForm Then   ' Merge menus
            Dim StageFormF As StageForm = CType(Me.ActiveMdiChild, StageForm)
            With StageFormF
                .FileMI.MergeAction = MergeAction.MatchOnly
                .ExportMI.MergeAction = MergeAction.Insert
                .ExportMI.MergeIndex = 4
                .ActionMI.MergeAction = MergeAction.Insert
                .ActionMI.MergeIndex = 1
                .ViewMI.MergeAction = MergeAction.Insert
                .ViewMI.MergeIndex = 2
                ToolStripManager.Merge(.MenuStrip1, Me.MenuStrip1)
            End With
        Else
            'ToolStripManager.Merge(StageFormF.MenuStrip1, Me.MenuStrip1)
            ' Dim ExportMI As ToolStripItem
            ' StageFormF.ExportMI.MergeAction = MergeAction.Insert
        End If
    End Sub

    Public Sub RefreshView(ByRef Container As PictureBox)
        'Dim target As Graphics = Nothing
        'target = Graphics.FromImage(Container.Image)
        'target.FillRectangle(New SolidBrush(MyWorld.DayColor()),
        '                     New System.Drawing.Rectangle(0, 0, MyWorld.X, MyWorld.Y))
        'Dim Pos() As Double = {0, 0, 0}
        'Dim Size As Single
        'For i = 0 To MyWorld.CreatureCount - 1 Step 1
        '    Array.Copy(MyWorld.Animals(i).Pos, Pos, 3)
        '    Size = MyWorld.Animals(i).Size
        '    If MyWorld.Animals(i).Sex = True Then
        '        target.FillEllipse(New SolidBrush(System.Drawing.Color.LightBlue),
        '                           CSng(Pos(0)), CSng(MyWorld.Y - Pos(1)), Size, Size)
        '    Else
        '        target.FillEllipse(New SolidBrush(System.Drawing.Color.Pink),
        '                           CSng(Pos(0)), CSng(MyWorld.Y - Pos(1)), Size, Size)
        '    End If
        'Next i
        'target.Dispose()
        'Container.Refresh()
        '------------------------------------------------------------------------------
        Canvas.Reset()
        Canvas.ClearMarkers()
        Dim Target As Graphics = Graphics.FromImage(Container.Image)
        Canvas.AddRectangle(New Rectangle(0, 0, MyWorld.Size.X, MyWorld.Size.Y))
        Canvas.SetMarkers()
        Dim Pos As Windows.Media.Media3D.Vector3D = Nothing
        Dim Size As Single = 0
        For i = 0 To MyWorld.CreatureCount - 1 Step 1
            Pos = MyWorld.Creatures(i).Position
            Size = MyWorld.Creatures(i).MarkerSize
            Canvas.AddEllipse(CSng(Pos.X) - Size / 2, CSng(MyWorld.Size.Y - Pos.Y) - Size / 2, Size, Size)
            Canvas.SetMarkers()
        Next i
        Dim GraphicsPaths As GraphicsPathIterator = New GraphicsPathIterator(Canvas)
        Dim Subpath As New GraphicsPath()
        GraphicsPaths.Rewind()
        GraphicsPaths.NextMarker(Subpath)
        Target.FillPath(New SolidBrush(MyWorld.DayColor()), Subpath)
        Dim MarkerThickness As Integer = 2
        For i = 0 To MyWorld.CreatureCount - 1 Step 1
            GraphicsPaths.NextMarker(Subpath)
            If MyWorld.Creatures(i).Marked = True Then
                Size = MyWorld.Creatures(i).MarkerSize
                Pos = MyWorld.Creatures(i).Position
                Target.FillEllipse(New SolidBrush(MyWorld.Creatures(i).MarkerColor),
                                   CSng(Pos.X) - MarkerThickness - Size / 2,
                                   CSng(MyWorld.Size.Y - Pos.Y) - MarkerThickness - Size / 2,
                                   Size + 2 * MarkerThickness, Size + 2 * MarkerThickness)
            End If
            If MyWorld.Creatures(i).Sex = True Then
                'Target.DrawPath(Pens.Black, Canvas)
                Target.FillPath(New SolidBrush(Color.LightBlue), Subpath)
            Else
                Target.FillPath(New SolidBrush(Color.Pink), Subpath)
            End If
        Next i
        If True Then        'Show MapGrid on the Canvas
            Dim DashPen As Pen = New Pen(Color.Gray)
            DashPen.DashStyle = DashStyle.Dash
            For X As Integer = 1 To MyWorld.MapGrid.XMax Step 1
                Target.DrawLine(DashPen, CSng(X * MyWorld.GridSize), 0.0F, CSng(X * MyWorld.GridSize), CSng(MyWorld.Size.Y))
            Next X
            For Y As Single = 1 To MyWorld.MapGrid.YMax Step 1
                Target.DrawLine(DashPen, 0.0F, CSng(Y * MyWorld.GridSize), CSng(MyWorld.Size.X), CSng(Y * MyWorld.GridSize))
            Next Y
        End If
        Target.Dispose()
        Container.Refresh()
    End Sub

    Public Sub SaveState(ByRef thisWorld As World, Optional ByVal Path As String = "", Optional ByVal File As String = "")
        If thisWorld Is Nothing Then
            Throw New Exception("No World is loaded. ", New System.Exception())
            Exit Sub
        End If
        If Path <> "" Then
            thisWorld.WorldFileDir = Path
        End If
        If File <> "" Then
            thisWorld.WorldFile = File
        End If
        If thisWorld.WorldFile = "" Or thisWorld.WorldFileDir = "" Then
            Throw New Exception("Empty path or filename. ", New System.ArgumentException())
            Exit Sub
        End If
        Dim FullPath As String = thisWorld.WorldFileDir & "\" & thisWorld.WorldFile
        If System.IO.File.Exists(FullPath) Then
            Kill(FullPath)
        End If
        '' XML serializer
        'Dim xml_serializer As New XmlSerializer(GetType(World))
        'Dim string_writer As New IO.StringWriter
        'xml_serializer.Serialize(string_writer, Me)
        'My.Computer.FileSystem.WriteAllText(FullPath, string_writer.ToString(), False)
        'string_writer.Close()
        Dim ser = New DataContractSerializer(GetType(World))
        Using fs As New IO.FileStream(FullPath, IO.FileMode.CreateNew),
            xw = Xml.XmlWriter.Create(fs)
            ser.WriteObject(xw, thisWorld)
        End Using
    End Sub

    Public Sub UnloadWorld()
        MyWorld = Nothing
        CurrentStage = Nothing
        If CurrentDashboard IsNot Nothing Then
            CurrentDashboard.Close()
        End If
        SaveAsMI.Enabled = False
        DashboardMI.Enabled = False
        SaveTheWorldMI.Enabled = False
    End Sub

    Public Sub LoadWorld(Optional ByRef thisWorld As World = Nothing)
        If CurrentStage IsNot Nothing Then
            RefreshView(CurrentStage)
            TryCast(CurrentStage.Parent.Parent, Form).Text = thisWorld.WorldFile & " - Stage"
        Else
            Dim FormName As String = ""
            Dim FormID As Integer = FormList.Count + 1
            Dim StageFormF As StageForm
            If thisWorld Is Nothing Then 'nothing is not allowed except debugging
                FormName = "[Empty]"
                StageFormF = New StageForm() With
                 {.MdiParent = Me, .Tag = FormID, .Text = FormName & " - Stage"}
            Else
                FormName = thisWorld.WorldFile
                StageFormF = New StageForm(thisWorld, CurrentStage) With
                 {.MdiParent = Me, .Tag = FormID, .Text = FormName & " - Stage"}
            End If
            Dim ChildFormMenu As ToolStripMenuItem = New ToolStripMenuItem() With
                 {.Text = FormID.ToString() & " " & StageFormF.Text, .Tag = FormID}
            AddHandler ChildFormMenu.Click, AddressOf ChildFormMenu_Click
            WindowsMI.DropDownItems.Add(ChildFormMenu)
            FormList.Add(StageFormF)
            StageFormF.Show()
            If thisWorld IsNot Nothing Then
                RefreshView(CurrentStage)
            End If
        End If
        If CurrentDashboard IsNot Nothing Then
            CurrentDashboard.PropertyGrid1.SelectedObject = thisWorld
        End If
        AbsTimeLabel.Text = CInt(MyWorld.T).ToString() & " s"
        PopulationLabel.Text = MyWorld.CreatureCount
        SaveAsMI.Enabled = True
        DashboardMI.Enabled = True
        If thisWorld.WorldFileDir = "" Then                  'haven't saved
            SaveTheWorldMI.Enabled = False
        Else
            SaveTheWorldMI.Enabled = True
        End If
    End Sub

    Private Sub SoilLayerMI_Click(sender As Object, e As EventArgs) Handles SoilLayerMI.Click
        SoilLayerMI.Checked = Not SoilLayerMI.Checked
        'Dim Target As Graphics = Graphics.FromImage(CurrentStage.Image)
        'Target.DrawImage(MyWorld.Soil.ColorImg, New Point(0, 0))
        ''CurrentStage.Image = 
        'Target.Dispose()
        'CurrentStage.Refresh()

        Return

        If MyWorld IsNot Nothing AndAlso CurrentStage IsNot Nothing Then
            Call RefreshView(CurrentStage)
        End If
    End Sub

    Private Sub OneclickRunMI_Click(sender As Object, e As EventArgs) Handles OneclickRunMI.Click
        'MsgBox(Distributions.Normal.CDF(0, 2, 0))
        MyWorld = New World(600, 400, 0)
        MyWorld.WorldFile = "Untitled World.smw"
        Call LoadWorld(MyWorld)
        Dim NewCreature As Creature = Nothing
        For i = 0 To 10 - 1 Step 1
            NewCreature = New Creature(MyWorld)
            MyWorld.AddCreature(NewCreature)
            Application.DoEvents()
        Next i
        Call RefreshView(CurrentStage)
    End Sub

    Private Sub Debug2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Debug2ToolStripMenuItem.Click
        'MyWorld.MapGrid.UpdateGrid(MyWorld.Creatures)
        'Dim a As Integer = InputBox("Center Creature")
        'Dim TrueDist As Double = InputBox("GridDist",, "1")
        'MsgBox(MyWorld.MapGrid.FindNearestNeighbor(MyWorld.Creatures, MyWorld.Creatures(a), TrueDist))
        'For Each CreatureProp As Reflection.PropertyInfo In GetType(Creature).GetProperties
        '    ToolStripComboBox1.Items.Add(CreatureProp.Name)
        'Next

    End Sub
End Class
