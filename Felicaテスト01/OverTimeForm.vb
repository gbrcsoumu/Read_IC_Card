Public Class OverTimeForm

    Private Const BUTTON_WIDTH = 240
    Private Const BUTTON_HEITH = 120
    Private Const GAP_X = 5
    Private Const GAP_Y = 5
    Private Button1() As System.Windows.Forms.Button
    Private Button_NEXT As System.Windows.Forms.Button
    Private Button_BEFORE As System.Windows.Forms.Button
    Private Button_NEXT2 As System.Windows.Forms.Button
    Private Button_BEFORE2 As System.Windows.Forms.Button
    Private Button_CANCEL As System.Windows.Forms.Button
    Private button_n As Integer
    Private button_xn As Integer

    Private anser As String

    Private Timer1 As Timer

    Public Kind As String

    'Private player As System.Media.SoundPlayer = Nothing

    Private Sub OverTimeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = Kind + "時間選択"
        'Me.Width = 1000
        'Me.Height = 600
        button_n = 16
        button_xn = 4
        ReDim Button1(button_n - 1)
        Make_Button(1)
        Timer1 = New Timer()
        AddHandler Timer1.Tick, New EventHandler(AddressOf Timer1_Tick)
        Timer1.Interval = 10000
        Timer1.Enabled = True
        'player = New System.Media.SoundPlayer("C:\Windows\Media\Windows Information Bar.wav")
        'player.Play()
        'System.Media.SystemSounds.Beep.Play()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        Timer1.Enabled = False
        anser = ""
        'MsgBox(a)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub

    Private Sub Make_Button(ByVal Page As Integer)
        Dim kind2 As String
        If Kind = "" Then
            kind2 = "残業"
        Else
            kind2 = Kind
        End If
        'button_n = 16
        'button_xn = 4
        Me.Width = BUTTON_WIDTH * button_xn + GAP_X * (button_xn + 1) + 17
        Me.Height = (BUTTON_HEITH + GAP_Y) * (Int(button_n / button_xn) + 2) + 22
        Dim bx As Single, by As Single, t As Integer, a As String, t0 As Integer
        'ReDim Button1(button_n - 1)

        If Page = 1 Then
            t0 = 0
        ElseIf Page = 2 Then
            t0 = 240
        ElseIf Page = 3 Then
            t0 = 480
        End If
        For i As Integer = 0 To button_n - 1
            bx = (i Mod button_xn) * (BUTTON_WIDTH + GAP_X) + GAP_X
            by = Int(i / button_xn) * (BUTTON_HEITH + GAP_Y) + GAP_Y
            Button1(i) = New System.Windows.Forms.Button()
            'Buttonコントロールのプロパティを設定する
            Me.Button1(i).Name = "Button" + i.ToString("00")
            t = (i + 1) * 15 + t0
            a = kind2 + Int(t / 60).ToString("0") + ":" + (t Mod 60).ToString("00")
            Me.Button1(i).Text = a
            'サイズと位置を設定する
            Me.Button1(i).Location = New Point(bx, by)
            Me.Button1(i).Size = New System.Drawing.Size(BUTTON_WIDTH, BUTTON_HEITH)
            Me.Button1(i).Font = New Font("MS UI Gothic", 32, FontStyle.Regular)
            Me.Button1(i).BackColor = Color.Black
            Me.Button1(i).ForeColor = Color.White
            'Clickイベントハンドラを追加する
            AddHandler Me.Button1(i).Click, AddressOf Button1_Click
            'フォームに追加する
            Me.Controls.Add(Me.Button1(i))
        Next

        If Page = 1 Then
            bx = 1.5 * (BUTTON_WIDTH + GAP_X) + GAP_X
            by = (Int(button_n / button_xn)) * (BUTTON_HEITH + GAP_Y) + GAP_Y
            Button_NEXT = New System.Windows.Forms.Button()
            Me.Button_NEXT.Name = "ButtonNEXT"
            a = "次を表示"
            Me.Button_NEXT.Text = a
            'サイズと位置を設定する
            Me.Button_NEXT.Location = New Point(bx, by)
            Me.Button_NEXT.Size = New System.Drawing.Size(BUTTON_WIDTH, BUTTON_HEITH)
            Me.Button_NEXT.Font = New Font("MS UI Gothic", 32, FontStyle.Regular)
            'Clickイベントハンドラを追加する
            AddHandler Me.Button_NEXT.Click, AddressOf ButtonNEXT_Click
            'フォームに追加する
            Me.Controls.Add(Me.Button_NEXT)
        ElseIf Page = 2 Then

            bx = 1.5 * (BUTTON_WIDTH + GAP_X) + GAP_X
            by = (Int(button_n / button_xn)) * (BUTTON_HEITH + GAP_Y) + GAP_Y
            Button_NEXT2 = New System.Windows.Forms.Button()
            Me.Button_NEXT.Name = "ButtonNEXT"
            a = "次を表示"
            Me.Button_NEXT2.Text = a
            'サイズと位置を設定する
            Me.Button_NEXT2.Location = New Point(bx, by)
            Me.Button_NEXT2.Size = New System.Drawing.Size(BUTTON_WIDTH, BUTTON_HEITH)
            Me.Button_NEXT2.Font = New Font("MS UI Gothic", 32, FontStyle.Regular)
            'Clickイベントハンドラを追加する
            AddHandler Me.Button_NEXT2.Click, AddressOf ButtonNEXT2_Click
            'フォームに追加する
            Me.Controls.Add(Me.Button_NEXT2)




            bx = 0.5 * (BUTTON_WIDTH + GAP_X) + GAP_X
            by = (Int(button_n / button_xn)) * (BUTTON_HEITH + GAP_Y) + GAP_Y
            Button_BEFORE = New System.Windows.Forms.Button()
            Me.Button_BEFORE.Name = "ButtonBEFORE"
            a = "前へ戻る"
            Me.Button_BEFORE.Text = a
            'サイズと位置を設定する
            Me.Button_BEFORE.Location = New Point(bx, by)
            Me.Button_BEFORE.Size = New System.Drawing.Size(BUTTON_WIDTH, BUTTON_HEITH)
            Me.Button_BEFORE.Font = New Font("MS UI Gothic", 32, FontStyle.Regular)
            'Clickイベントハンドラを追加する
            AddHandler Me.Button_BEFORE.Click, AddressOf ButtonBRFORE_Click
            'フォームに追加する
            Me.Controls.Add(Me.Button_BEFORE)

        ElseIf Page = 3 Then





            bx = 0.5 * (BUTTON_WIDTH + GAP_X) + GAP_X
            by = (Int(button_n / button_xn)) * (BUTTON_HEITH + GAP_Y) + GAP_Y
            Button_BEFORE2 = New System.Windows.Forms.Button()
            Me.Button_BEFORE2.Name = "ButtonBEFORE"
            a = "前へ戻る"
            Me.Button_BEFORE2.Text = a
            'サイズと位置を設定する
            Me.Button_BEFORE2.Location = New Point(bx, by)
            Me.Button_BEFORE2.Size = New System.Drawing.Size(BUTTON_WIDTH, BUTTON_HEITH)
            Me.Button_BEFORE2.Font = New Font("MS UI Gothic", 32, FontStyle.Regular)
            'Clickイベントハンドラを追加する
            AddHandler Me.Button_BEFORE2.Click, AddressOf ButtonBRFORE2_Click
            'フォームに追加する
            Me.Controls.Add(Me.Button_BEFORE2)


        End If

        bx = 2.5 * (BUTTON_WIDTH + GAP_X) + GAP_X
        by = (Int(button_n / button_xn)) * (BUTTON_HEITH + GAP_Y) + GAP_Y
        Button_CANCEL = New System.Windows.Forms.Button()
        Me.Button_CANCEL.Name = "ButtonCANCEL"
        a = "CANCEL"
        Me.Button_CANCEL.Text = a
        'サイズと位置を設定する
        Me.Button_CANCEL.Location = New Point(bx, by)
        Me.Button_CANCEL.Size = New System.Drawing.Size(BUTTON_WIDTH, BUTTON_HEITH)
        Me.Button_CANCEL.Font = New Font("MS UI Gothic", 32, FontStyle.Regular)
        'Clickイベントハンドラを追加する
        AddHandler Me.Button_CANCEL.Click, AddressOf ButtonCANCEL_Click
        'フォームに追加する
        Me.Controls.Add(Me.Button_CANCEL)

    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Dim a As String
        'player.Play()
        System.Console.Beep()
        Timer1.Enabled = False
        anser = sender.text
        'MsgBox(a)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ButtonCANCEL_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Dim a As String
        'player.Play()
        System.Console.Beep()
        anser = ""
        'MsgBox(a)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ButtonNEXT_Click(ByVal sender As Object, ByVal e As EventArgs)
        'player.Play()
        System.Console.Beep()
        Timer1.Enabled = False
        For i As Integer = 0 To Me.button_n - 1
            Me.Button1(i).Dispose()
        Next
        Me.Button_NEXT.Dispose()
        Me.Button_CANCEL.Dispose()
        Make_Button(2)
        Timer1.Enabled = True

    End Sub

    Private Sub ButtonNEXT2_Click(ByVal sender As Object, ByVal e As EventArgs)
        'player.Play()
        System.Console.Beep()
        Timer1.Enabled = False
        For i As Integer = 0 To Me.button_n - 1
            Me.Button1(i).Dispose()
        Next
        Me.Button_NEXT2.Dispose()
        Me.Button_BEFORE.Dispose()
        Me.Button_CANCEL.Dispose()
        Make_Button(3)
        Timer1.Enabled = True

    End Sub

    Private Sub ButtonBRFORE_Click(ByVal sender As Object, ByVal e As EventArgs)
        'player.Play()
        System.Console.Beep()
        Timer1.Enabled = False
        For i As Integer = 0 To Me.button_n - 1
            Me.Button1(i).Dispose()
        Next
        Me.Button_BEFORE.Dispose()
        Me.Button_NEXT2.Dispose()
        Me.Button_CANCEL.Dispose()
        Make_Button(1)
        Timer1.Enabled = True

    End Sub

    Private Sub ButtonBRFORE2_Click(ByVal sender As Object, ByVal e As EventArgs)
        'player.Play()
        System.Console.Beep()
        Timer1.Enabled = False
        For i As Integer = 0 To Me.button_n - 1
            Me.Button1(i).Dispose()
        Next
        Me.Button_BEFORE2.Dispose()
        Me.Button_CANCEL.Dispose()
        Make_Button(2)
        Timer1.Enabled = True

    End Sub
    '値を渡すメソッド
    Public Function GetValue() As String
        Return anser
    End Function

End Class