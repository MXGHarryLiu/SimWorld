<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Dashboard
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(345, 24)
        Me.MenuStrip1.TabIndex = 21
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Location = New System.Drawing.Point(12, 58)
        Me.PropertyGrid1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized
        Me.PropertyGrid1.Size = New System.Drawing.Size(253, 341)
        Me.PropertyGrid1.TabIndex = 22
        '
        'Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(345, 523)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Dashboard"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents PropertyGrid1 As PropertyGrid
End Class
