<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartPage
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
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.VerLabel = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GetStartLabel = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.NewLabel = New System.Windows.Forms.LinkLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.BackColor = System.Drawing.Color.Transparent
        Me.TitleLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.ForeColor = System.Drawing.Color.OrangeRed
        Me.TitleLabel.Location = New System.Drawing.Point(235, 28)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(392, 91)
        Me.TitleLabel.TabIndex = 1
        Me.TitleLabel.Text = "SimWorld"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'VerLabel
        '
        Me.VerLabel.AutoSize = True
        Me.VerLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VerLabel.Location = New System.Drawing.Point(277, 108)
        Me.VerLabel.Name = "VerLabel"
        Me.VerLabel.Size = New System.Drawing.Size(148, 46)
        Me.VerLabel.TabIndex = 2
        Me.VerLabel.Text = "0.0.0.0"
        Me.VerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.SimWorld.My.Resources.Resources.icon
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(288, 350)
        Me.Panel1.TabIndex = 2
        '
        'GetStartLabel
        '
        Me.GetStartLabel.AutoSize = True
        Me.GetStartLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GetStartLabel.Location = New System.Drawing.Point(259, 154)
        Me.GetStartLabel.Name = "GetStartLabel"
        Me.GetStartLabel.Size = New System.Drawing.Size(302, 25)
        Me.GetStartLabel.TabIndex = 0
        Me.GetStartLabel.Text = "A vibrant world with artificial lives. "
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(260, 193)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(322, 27)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "LinkLabel1"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(433, 297)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(194, 22)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = "Don't show this on start. "
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.Location = New System.Drawing.Point(260, 220)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(322, 28)
        Me.LinkLabel2.TabIndex = 5
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "LinkLabel2"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LinkLabel3
        '
        Me.LinkLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel3.Location = New System.Drawing.Point(260, 248)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(322, 28)
        Me.LinkLabel3.TabIndex = 6
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "LinkLabel3"
        Me.LinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NewLabel
        '
        Me.NewLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewLabel.LinkColor = System.Drawing.Color.ForestGreen
        Me.NewLabel.Location = New System.Drawing.Point(260, 295)
        Me.NewLabel.Name = "NewLabel"
        Me.NewLabel.Size = New System.Drawing.Size(137, 47)
        Me.NewLabel.TabIndex = 7
        Me.NewLabel.TabStop = True
        Me.NewLabel.Text = "New World"
        '
        'StartPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 350)
        Me.Controls.Add(Me.NewLabel)
        Me.Controls.Add(Me.LinkLabel3)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.GetStartLabel)
        Me.Controls.Add(Me.VerLabel)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "StartPage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "StartPage"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TitleLabel As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents VerLabel As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents GetStartLabel As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents LinkLabel3 As LinkLabel
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents NewLabel As LinkLabel
    Friend WithEvents ToolTip1 As ToolTip
End Class
