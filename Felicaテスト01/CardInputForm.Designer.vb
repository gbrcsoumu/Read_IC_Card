<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CardInputForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TimeTimer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TimeLabel = New System.Windows.Forms.Label()
        Me.DayLabel = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BeginButton = New System.Windows.Forms.Button()
        Me.OutButton = New System.Windows.Forms.Button()
        Me.InButton = New System.Windows.Forms.Button()
        Me.FinishButton = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.AfterInputTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ModeChangeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.OverTimeButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.EarlyTimeButton = New System.Windows.Forms.Button()
        Me.PrivateRadioButton = New System.Windows.Forms.RadioButton()
        Me.OfficialRadioButton = New System.Windows.Forms.RadioButton()
        Me.ModeChangeTimer2 = New System.Windows.Forms.Timer(Me.components)
        Me.DataBaseTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DicReNewTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TimeTimer1
        '
        '
        'TimeLabel
        '
        Me.TimeLabel.AutoSize = True
        Me.TimeLabel.Font = New System.Drawing.Font("MS UI Gothic", 60.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TimeLabel.ForeColor = System.Drawing.Color.White
        Me.TimeLabel.Location = New System.Drawing.Point(1197, 21)
        Me.TimeLabel.Name = "TimeLabel"
        Me.TimeLabel.Size = New System.Drawing.Size(442, 100)
        Me.TimeLabel.TabIndex = 0
        Me.TimeLabel.Text = "24：00：00"
        '
        'DayLabel
        '
        Me.DayLabel.AutoSize = True
        Me.DayLabel.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DayLabel.ForeColor = System.Drawing.Color.White
        Me.DayLabel.Location = New System.Drawing.Point(73, 21)
        Me.DayLabel.Name = "DayLabel"
        Me.DayLabel.Size = New System.Drawing.Size(858, 80)
        Me.DayLabel.TabIndex = 0
        Me.DayLabel.Text = "2019年12月05日 金曜日"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.DayLabel)
        Me.Panel1.Controls.Add(Me.TimeLabel)
        Me.Panel1.Location = New System.Drawing.Point(19, 8)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1752, 131)
        Me.Panel1.TabIndex = 1
        '
        'BeginButton
        '
        Me.BeginButton.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BeginButton.Location = New System.Drawing.Point(9, 28)
        Me.BeginButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BeginButton.Name = "BeginButton"
        Me.BeginButton.Size = New System.Drawing.Size(332, 178)
        Me.BeginButton.TabIndex = 2
        Me.BeginButton.Text = "出勤"
        Me.BeginButton.UseVisualStyleBackColor = True
        '
        'OutButton
        '
        Me.OutButton.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.OutButton.Location = New System.Drawing.Point(403, 28)
        Me.OutButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.OutButton.Name = "OutButton"
        Me.OutButton.Size = New System.Drawing.Size(332, 178)
        Me.OutButton.TabIndex = 2
        Me.OutButton.Text = "外出"
        Me.OutButton.UseVisualStyleBackColor = True
        '
        'InButton
        '
        Me.InButton.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.InButton.Location = New System.Drawing.Point(796, 28)
        Me.InButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.InButton.Name = "InButton"
        Me.InButton.Size = New System.Drawing.Size(332, 178)
        Me.InButton.TabIndex = 2
        Me.InButton.Text = "戻り"
        Me.InButton.UseVisualStyleBackColor = True
        '
        'FinishButton
        '
        Me.FinishButton.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FinishButton.Location = New System.Drawing.Point(1189, 28)
        Me.FinishButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FinishButton.Name = "FinishButton"
        Me.FinishButton.Size = New System.Drawing.Size(332, 178)
        Me.FinishButton.TabIndex = 2
        Me.FinishButton.Text = "退勤"
        Me.FinishButton.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Black
        Me.TextBox1.Font = New System.Drawing.Font("MS UI Gothic", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(19, 670)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(1712, 144)
        Me.TextBox1.TabIndex = 4
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'AfterInputTimer
        '
        '
        'ModeChangeTimer
        '
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.Controls.Add(Me.FinishButton)
        Me.Panel2.Controls.Add(Me.InButton)
        Me.Panel2.Controls.Add(Me.OutButton)
        Me.Panel2.Controls.Add(Me.BeginButton)
        Me.Panel2.Location = New System.Drawing.Point(140, 159)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1532, 234)
        Me.Panel2.TabIndex = 5
        '
        'OverTimeButton
        '
        Me.OverTimeButton.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.OverTimeButton.Location = New System.Drawing.Point(191, 431)
        Me.OverTimeButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.OverTimeButton.Name = "OverTimeButton"
        Me.OverTimeButton.Size = New System.Drawing.Size(459, 211)
        Me.OverTimeButton.TabIndex = 6
        Me.OverTimeButton.Text = "残業時間"
        Me.OverTimeButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(1128, 502)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(189, 60)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Label1"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(1599, 850)
        Me.Button6.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(133, 30)
        Me.Button6.TabIndex = 9
        Me.Button6.Text = "設定"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'EarlyTimeButton
        '
        Me.EarlyTimeButton.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.EarlyTimeButton.Location = New System.Drawing.Point(157, 431)
        Me.EarlyTimeButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.EarlyTimeButton.Name = "EarlyTimeButton"
        Me.EarlyTimeButton.Size = New System.Drawing.Size(469, 211)
        Me.EarlyTimeButton.TabIndex = 6
        Me.EarlyTimeButton.Text = "早出時間"
        Me.EarlyTimeButton.UseVisualStyleBackColor = True
        '
        'PrivateRadioButton
        '
        Me.PrivateRadioButton.Appearance = System.Windows.Forms.Appearance.Button
        Me.PrivateRadioButton.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PrivateRadioButton.Location = New System.Drawing.Point(713, 550)
        Me.PrivateRadioButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PrivateRadioButton.Name = "PrivateRadioButton"
        Me.PrivateRadioButton.Size = New System.Drawing.Size(321, 109)
        Me.PrivateRadioButton.TabIndex = 10
        Me.PrivateRadioButton.Text = "私用外出"
        Me.PrivateRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.PrivateRadioButton.UseVisualStyleBackColor = True
        '
        'OfficialRadioButton
        '
        Me.OfficialRadioButton.Appearance = System.Windows.Forms.Appearance.Button
        Me.OfficialRadioButton.Checked = True
        Me.OfficialRadioButton.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.OfficialRadioButton.Location = New System.Drawing.Point(713, 416)
        Me.OfficialRadioButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.OfficialRadioButton.Name = "OfficialRadioButton"
        Me.OfficialRadioButton.Size = New System.Drawing.Size(321, 118)
        Me.OfficialRadioButton.TabIndex = 10
        Me.OfficialRadioButton.TabStop = True
        Me.OfficialRadioButton.Text = "公用外出"
        Me.OfficialRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.OfficialRadioButton.UseVisualStyleBackColor = True
        '
        'ModeChangeTimer2
        '
        '
        'DataBaseTimer
        '
        '
        'DicReNewTimer
        '
        '
        'CardInputForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1500, 721)
        Me.ControlBox = False
        Me.Controls.Add(Me.OfficialRadioButton)
        Me.Controls.Add(Me.PrivateRadioButton)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.EarlyTimeButton)
        Me.Controls.Add(Me.OverTimeButton)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "CardInputForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GBRC職員カード読み取りシステム ver.2.0"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimeTimer1 As System.Windows.Forms.Timer
    Friend WithEvents TimeLabel As System.Windows.Forms.Label
    Friend WithEvents DayLabel As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BeginButton As System.Windows.Forms.Button
    Friend WithEvents OutButton As System.Windows.Forms.Button
    Friend WithEvents InButton As System.Windows.Forms.Button
    Friend WithEvents FinishButton As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents AfterInputTimer As System.Windows.Forms.Timer
    Friend WithEvents ModeChangeTimer As System.Windows.Forms.Timer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents OverTimeButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents EarlyTimeButton As Button
    Friend WithEvents PrivateRadioButton As RadioButton
    Friend WithEvents OfficialRadioButton As RadioButton
    Friend WithEvents ModeChangeTimer2 As Timer
    Friend WithEvents DataBaseTimer As Timer
    Friend WithEvents DicReNewTimer As Timer
End Class
