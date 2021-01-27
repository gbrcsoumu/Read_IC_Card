Imports System
Imports System.Threading
Imports System.Net
Imports System.Collections
Imports Microsoft.VisualBasic.Devices

Public Class CardInputForm

    Private Mode As Integer
    'Private CardMasterKeyString As String
    Private No As String
    Private Idm As String
    Private th As Thread, th2 As Thread
    Private hostname As String, IP As String
    Private Old_Sql As String, Old_time As DateTime
    Private First_flag As Boolean, mode_chage_flag As Boolean
    Private Read_flag As Boolean
    Private Dic1 As New Dictionary(Of String, String)
    Private Dic2 As New Dictionary(Of String, String)
    Private Dic3 As New Dictionary(Of String, String)
    Private Dic4 As New Dictionary(Of String, String)
    Private Busy As Boolean

    Private Sql_Command As String, Sql_Command2 As String, Sql_Command3 As String
    Private Sql_Command_Last As String, Sql_Command2_Last As String, Sql_Command3_Last As String
    Private OutTime As DateTime
    Private Sql1 As New Queue()
    Private Sql2 As New Queue()
    Private Sql3 As New Queue()

    Private Display1 As New Display
    Delegate Sub txtMessage_Text_Delegate(ByVal value As String, ByVal Idm As String)
    Delegate Sub Timer3_Enable_Delegate()
    Const message1 As String = "出勤／退勤等を指定してICカードをかざしてください。"
    Const TimeLag As Double = 2.0

    'Private player As System.Media.SoundPlayer = Nothing

    Public Shared Function CountChar(ByVal s As String, ByVal c As Char) As Integer
        Return s.Length - s.Replace(c.ToString(), "").Length
    End Function

    Private Sub CardInputForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim n As Integer, address As String

        Me.Width = 1366
        Me.Height = 768
        Me.StartPosition = FormStartPosition.CenterScreen

        'Me.CardMasterKeyString = "GBRC 2020"
        TimeTimer1.Interval = 1000      ' 時計表示タイマー
        TimeTimer1.Enabled = True
        If TimeSpan.Compare(DateTime.Now.TimeOfDay, New TimeSpan(12, 0, 0)) = -1 Then
            Me.Mode = 1
        Else
            Me.Mode = 4
        End If
        'Me.Mode = 1
        Button_change(Me.Mode)
        'Timer2.Interval = 500
        'Timer2.Enabled = True
        AfterInputTimer.Interval = 3000
        AfterInputTimer.Enabled = False
        ModeChangeTimer.Interval = 10000
        ModeChangeTimer.Enabled = False
        ModeChangeTimer2.Interval = 60000
        ModeChangeTimer2.Enabled = True
        'DataBaseTimer.Interval = 8333
        'DataBaseTimer.Enabled = True
        First_flag = True   ' カードの読み取りが初回の場合はTrue
        mode_chage_flag = False
        hostname = System.Net.Dns.GetHostName
        System.Diagnostics.Debug.WriteLine(hostname)
        IP = ""
        Dim hostname1 As IPHostEntry = Dns.GetHostEntry(hostname)
        For Each ipAddr In hostname1.AddressList
            address = ipAddr.ToString()
            n = CountChar(address, ".")
            If n = 3 Then
                System.Diagnostics.Debug.WriteLine(address)
                If IP <> "" Then
                    IP += ":"
                End If
                IP += address
            End If
            'Console.WriteLine(ipAddr)
        Next

        Old_Sql = ""
        Old_time = DateTime.Now()
        Me.TextBox1.Text = message1
        Me.TextBox1.Select(Me.TextBox1.Text.Length, 0)
        Me.Label1.Text = ""
        Read_flag = False

        Sql_Command_Last = ""
        Sql_Command2_Last = ""
        Sql_Command3_Last = ""

        SetDic1()
        ' 定期的に職員名簿を修正する
        DicReNewTimer.Interval = 3600000 * 1
        DicReNewTimer.Enabled = True

        DataBaseTimer.Interval = 8333
        DataBaseTimer.Enabled = False
        ReadCard()



    End Sub

    Private Sub SetDic1()
        Dim db As New OdbcDbIf
        Dim tb As DataTable
        Dim Sql_Command As String, n1 As String, n2 As String, n3 As String
        Dim i As Integer
        Dim n As Integer
        'Dim Dic1 As New Dictionary(Of String, String)
        Busy = True
        Do
            n = 0
            Try
                If db.Connect() = 0 Then

                    Sql_Command = "SELECT * FROM """ + CodeTable1 + """"
                    tb = db.ExecuteSql(Sql_Command)
                    n = tb.Rows.Count
                    If n > 0 Then
                        If Dic1.Count > 0 Then
                            Dic1.Clear()
                        End If
                        For i = 0 To n - 1
                            n1 = tb.Rows(i).Item("項目").ToString()
                            n2 = tb.Rows(i).Item("コード").ToString()
                            Dic1.Add(n1, n2)
                        Next
                    End If
                    'Console.WriteLine(Dic1("早出0:45"))

                    Sql_Command = "SELECT * FROM """ + CodeTable2 + """"
                    tb = db.ExecuteSql(Sql_Command)
                    n = tb.Rows.Count
                    If n > 0 Then
                        If Dic2.Count > 0 Then
                            Dic2.Clear()
                        End If
                        For i = 0 To n - 1
                            n1 = tb.Rows(i).Item("項目").ToString()
                            n2 = tb.Rows(i).Item("コード").ToString()
                            Dic2.Add(n1, n2)
                        Next
                    End If

                    Sql_Command = "SELECT * FROM """ + MemberNameTable + """"
                    tb = db.ExecuteSql(Sql_Command)
                    n = tb.Rows.Count
                    If n > 0 Then
                        If Dic3.Count > 0 Then
                            Dic3.Clear()
                        End If
                        If Dic3.Count > 0 Then
                            Dic3.Clear()
                        End If
                        If Dic4.Count > 0 Then
                            Dic4.Clear()
                        End If
                        For i = 0 To n - 1
                            n1 = tb.Rows(i).Item("職員番号").ToString()
                            n2 = tb.Rows(i).Item("氏名").ToString()
                            n3 = tb.Rows(i).Item("Idm").ToString()
                            Dic3.Add(n1, n2)
                            Dic4.Add(n1, n3)
                        Next
                    End If

                    db.Disconnect()
                End If
            Catch ex As Exception

            End Try
            If n > 0 Then Exit Do
            System.Threading.Thread.Sleep(2000)
        Loop
        Busy = False

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimeTimer1.Tick
        TimeLabel.Text = DateTime.Now.ToString("HH：mm：ss")
        DayLabel.Text = DateTime.Now.ToString("yyyy年MM月dd日") + " " + DateTime.Now.ToString("dddd")
        If TimeLabel.Text = ListReadTime Then
            TimeTimer1.Enabled = False
            SetDic1()
            TimeTimer1.Enabled = True
        End If
    End Sub

    Private Sub Button_change(ByVal Mode1 As Integer)
        ModeChangeTimer.Enabled = False
        'Timer2.Enabled = False
        Select Case Mode1
            Case 1      ' 出勤モード
                Me.BeginButton.BackColor = Color.White
                Me.BeginButton.ForeColor = Color.Red
                Me.OutButton.BackColor = Color.Black
                Me.OutButton.ForeColor = Color.Gray
                Me.InButton.BackColor = Color.Black
                Me.InButton.ForeColor = Color.Gray
                Me.FinishButton.BackColor = Color.Black
                Me.FinishButton.ForeColor = Color.Gray
                Me.BeginButton.Focus()
                Me.OverTimeButton.Visible = False
                Me.EarlyTimeButton.Visible = True
                Me.OfficialRadioButton.Visible = False
                Me.PrivateRadioButton.Visible = False
                'Me.Label1.Text = ""
            Case 2      ' 外出モード
                Me.BeginButton.BackColor = Color.Black
                Me.BeginButton.ForeColor = Color.Gray
                Me.OutButton.BackColor = Color.White
                Me.OutButton.ForeColor = Color.Red
                Me.InButton.BackColor = Color.Black
                Me.InButton.ForeColor = Color.Gray
                Me.FinishButton.BackColor = Color.Black
                Me.FinishButton.ForeColor = Color.Gray
                Me.OutButton.Focus()
                Me.OverTimeButton.Visible = False
                Me.EarlyTimeButton.Visible = False
                Me.OfficialRadioButton.Visible = True
                Me.OfficialRadioButton.Checked = True
                Me.PrivateRadioButton.Visible = True
                Me.PrivateRadioButton.Checked = False
                'Me.Label1.Text = "公用外出"
            Case 3      ' 戻りモード
                Me.BeginButton.BackColor = Color.Black
                Me.BeginButton.ForeColor = Color.Gray
                Me.OutButton.BackColor = Color.Black
                Me.OutButton.ForeColor = Color.Gray
                Me.InButton.BackColor = Color.White
                Me.InButton.ForeColor = Color.Red
                Me.FinishButton.BackColor = Color.Black
                Me.FinishButton.ForeColor = Color.Gray
                Me.InButton.Focus()
                Me.OverTimeButton.Visible = False
                Me.EarlyTimeButton.Visible = False
                Me.OfficialRadioButton.Visible = False
                Me.PrivateRadioButton.Visible = False
                'Me.Label1.Text = ""
            Case 4      ' 退勤モード
                Me.BeginButton.BackColor = Color.Black
                Me.BeginButton.ForeColor = Color.Gray
                Me.OutButton.BackColor = Color.Black
                Me.OutButton.ForeColor = Color.Gray
                Me.InButton.BackColor = Color.Black
                Me.InButton.ForeColor = Color.Gray
                Me.FinishButton.BackColor = Color.White
                Me.FinishButton.ForeColor = Color.Red
                Me.FinishButton.Focus()
                Me.OverTimeButton.Visible = True
                Me.EarlyTimeButton.Visible = False
                Me.OfficialRadioButton.Visible = False
                Me.PrivateRadioButton.Visible = False
                'Me.Label1.Text = ""

        End Select
        'Timer4.Interval = 10000
        ModeChangeTimer.Enabled = True
        'Timer2.Enabled = True
        ReadCard()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BeginButton.Click
        'player.Play()
        System.Console.Beep()
        Me.Mode = 1
        Button_change(Me.Mode)
        Me.Label1.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles OutButton.Click
        'player.Play()
        System.Console.Beep()
        Me.Mode = 2
        Button_change(Me.Mode)
        Me.Label1.Text = "公用外出"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles InButton.Click
        'player.Play()
        System.Console.Beep()
        Me.Mode = 3
        Button_change(Me.Mode)
        Me.Label1.Text = ""
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles FinishButton.Click
        'player.Play()
        System.Console.Beep()
        Me.Mode = 4
        Button_change(Me.Mode)
        Me.Label1.Text = ""
    End Sub

    '===============================
    ' txtMessage にメッセージを追加
    '===============================
    Private Sub TxtMessage_Text(ByVal value As String)

        Dim db As New OdbcDbIf
        Dim tb As DataTable, tb2 As DataTable, tb3 As DataTable
        Dim t1 As String, D1 As String, n1 As String, S1 As String, t2 As String, t3 As String
        'Dim Sql_Command2 As String
        'Dim Sql_Command3 As String
        Dim n As Integer, n2 As Integer
        Dim A As String
        Dim timenow As DateTime
        Dim ts As TimeSpan
        Dim st1 As String, scode As Integer, ed1 As String, ecode As Integer, D2 As String, go1 As String
        Dim time_st As DateTime, time_ed As DateTime, time_go As DateTime
        value = value.Replace(vbCrLf, "")

        If value <> "" Then
            timenow = DateTime.Now()
            t1 = timenow.ToString("HH:mm:ss")
            D1 = DateTime.Now.ToString("yyyy-MM-dd")
            D2 = DateTime.Now.ToString("yyyy/MM/dd")
            A = "職員番号：" + value
            t2 = Me.Label1.Text


            If db.Connect() = 0 Then

                'Sql_Command = "SELECT ""氏名"" FROM ""職員一覧"" WHERE ""職員番号"" = '" & value & "'"
                Sql_Command = "SELECT ""氏名"" FROM """ + MemberNameTable + """ WHERE ""職員番号"" = '" & value & "'"
                tb = db.ExecuteSql(Sql_Command)
                n = tb.Rows.Count

                'Sql_Command2 = "SELECT ""職員番号"" FROM """ + DateLogTable + """ WHERE (""職員番号"" = '" & value & "' AND ""日付"" = DATE '" + D1 + " ')"
                Sql_Command2 = "SELECT * FROM """ + DateLogTable + """ WHERE (""職員番号"" = '" & value & "' AND ""日付"" = DATE '" + D1 + " ')"
                tb2 = db.ExecuteSql(Sql_Command2)
                n2 = tb2.Rows.Count

                If n2 > 0 Then
                    st1 = tb2.Rows(0).Item("出勤時刻").ToString()
                    time_st = DateTime.Parse(D2 + " " + st1)
                    scode = tb2.Rows(0).Item("出勤コード")
                    ed1 = tb2.Rows(0).Item("退勤時刻").ToString()
                    time_ed = DateTime.Parse(D2 + " " + ed1)
                    ecode = tb2.Rows(0).Item("退勤コード")
                    go1 = tb2.Rows(0).Item("外出時刻").ToString()

                    If go1 <> "" Then
                        time_go = DateTime.Parse(D2 + " " + go1)
                        t3 = time_go.ToString("HH:mm:ss")
                    Else
                        t3 = ""
                    End If
                Else
                    st1 = ""
                    time_st = timenow
                    scode = 0
                    ed1 = ""
                    time_ed = timenow
                    ecode = 0

                    t3 = ""
                End If


                S1 = ""
                If n > 0 Then
                    n1 = tb.Rows(0).Item("氏名").ToString()
                    A += "、氏名：" + n1

                    Select Case Me.Mode
                        Case 1
                            A += "、出勤"
                            S1 = "出勤"
                        Case 2
                            A += "、外出"
                            S1 = "外出"
                        Case 3
                            A += "、戻り"
                            S1 = "戻り"
                        Case 4
                            A += "、退勤"
                            S1 = "退勤"
                    End Select
                    A += "時刻：" + t1
                    If t2 <> "" Then
                        Select Case S1
                            Case "出勤"
                                If t2.Substring(0, 2) = "早出" Then
                                    A += "、" + t2
                                End If
                            Case "退勤"
                                If t2.Substring(0, 2) = "残業" Then
                                    A += "、" + t2
                                End If
                            Case "外出"
                                If t2 = "公用外出" Or t2 = "私用外出" Then
                                    A += "、" + t2
                                End If
                            Case Else
                                t2 = ""
                        End Select

                        'If (S1 = "出勤" And t2.Substring(0, 2) = "早出") Or (S1 = "退勤" And t2.Substring(0, 2) = "残業") Then
                        '    A += "、" + t2
                        'Else
                        '    t2 = ""
                        'End If
                    End If


                    Sql_Command = "INSERT INTO """ + MemberLogTable + """ (""職員番号"",""職員名"",""種別"",""日付"",""時刻"",""特記事項"",""ホスト名"",""アドレス"")"
                    Sql_Command += " VALUES ('" + value + "','" + n1 + "','" + S1 + "',DATE '" + D1 + "',TIME '"
                    Sql_Command += t1 + "','" + t2 + "','" + hostname + "','" + IP + "')"

                    Dim code1 As String
                    Sql_Command2 = ""
                    Sql_Command3 = ""
                    If n2 = 0 Then
                        Select Case Me.Mode
                            Case 1
                                If t2 <> "" Then
                                    code1 = Dic1(t2)
                                Else
                                    code1 = "0"
                                End If
                                Sql_Command2 = "INSERT INTO """ + DateLogTable + """ (""職員番号"",""日付"",""出勤時刻"",""出勤コード"")"
                                Sql_Command2 += " VALUES ('" + value + "', DATE '" + D1 + "', TIME '"
                                Sql_Command2 += t1 + "'," + code1 + ")"
                            Case 2
                            'Dim OutKind As String
                            'If Me.OfficialRadioButton.Checked Then
                            '    OutKind = "公用"
                            'Else
                            '    OutKind = "私用"
                            'End If
                            'Sql_Command2 = "INSERT INTO """ + GooutTable + """ (""職員番号"",""日付"",""時刻"",""種類"")"
                            'Sql_Command2 += " VALUES ('" + value + "', DATE '" + D1 + "', TIME '"
                            'Sql_Command2 += t1 + "','" + OutKind + "')"
                            Case 3
                            '    Sql_Command2 = "INSERT INTO """ + ReturnTable + """ (""職員番号"",""日付"",""時刻"")"
                            '    Sql_Command2 += " VALUES ('" + value + "', DATE '" + D1 + "', TIME '"
                            '    Sql_Command2 += t1 + "')"
                            'Sql_Command2 = "UPDATE """ + GooutTable + """ SET ""戻り時刻"" = TIME '" + t1 + "'"
                            'Sql_Command2 += "  WHERE ""職員番号"" = '" + value + "' AND ""日付"" = DATE '" + D1 + "' AND ""時刻"" = TIME '" + t3 + "'"
                            Case 4
                                If t2 <> "" Then
                                    code1 = Dic2(t2)
                                Else
                                    code1 = "0"
                                End If
                                Sql_Command2 = "INSERT INTO """ + DateLogTable + """ (""職員番号"",""日付"",""退勤時刻"",""退勤コード"")"
                                Sql_Command2 += " VALUES ('" + value + "', DATE '" + D1 + "', TIME '"
                                Sql_Command2 += t1 + "'," + code1 + ")"
                        End Select

                    Else
                        Select Case Me.Mode
                         'Sql_Command = "UPDATE """ + MemberNameTable + """ SET IDm = '" + Me.IDm(data_n) + "' WHERE ""職員番号"" = '" + Me.No(data_n) + "'"
                            Case 1
                                If t2 <> "" Then
                                    code1 = Dic1(t2)
                                Else
                                    code1 = "0"
                                End If
                                Sql_Command2 = "UPDATE """ + DateLogTable + """ SET ""出勤時刻"" = TIME '" + t1 + "' ,""出勤コード"" = " + code1
                                Sql_Command2 += "  WHERE ""職員番号"" = '" + value + "' AND ""日付"" = DATE '" + D1 + "'"

                            Case 2
                                Sql_Command2 = "UPDATE """ + DateLogTable + """ SET ""外出時刻"" = TIME '" + t1 + "'"
                                Sql_Command2 += "  WHERE ""職員番号"" = '" + value + "' AND ""日付"" = DATE '" + D1 + "'"

                                Dim OutKind As String
                                If Me.OfficialRadioButton.Checked Then
                                    OutKind = "公外"
                                Else
                                    OutKind = "私外"
                                End If
                                Sql_Command3 = "INSERT INTO """ + GooutTable + """ (""職員番号"",""日付"",""時刻"",""種類"")"
                                Sql_Command3 += " VALUES ('" + value + "', DATE '" + D1 + "', TIME '"
                                Sql_Command3 += t1 + "','" + OutKind + "')"
                            Case 3
                                'Sql_Command2 = "INSERT INTO """ + ReturnTable + """ (""職員番号"",""日付"",""時刻"")"
                                'Sql_Command2 += " VALUES ('" + value + "', DATE '" + D1 + "', TIME '"
                                'Sql_Command2 += t1 + "')"
                                Sql_Command3 = "UPDATE """ + GooutTable + """ SET ""戻り時刻"" = TIME '" + t1 + "'"
                                Sql_Command3 += "  WHERE ""職員番号"" = '" + value + "' AND ""日付"" = DATE '" + D1 + "' AND ""時刻"" = TIME '" + t3 + "'"

                            Case 4
                                If t2 <> "" Then
                                    code1 = Dic2(t2)
                                Else
                                    code1 = "0"
                                End If
                                Sql_Command2 = "UPDATE """ + DateLogTable + """ SET ""退勤時刻"" = TIME '" + t1 + "' ,""退勤コード"" = " + code1
                                Sql_Command2 += "  WHERE ""職員番号"" = '" + value + "' AND ""日付"" = DATE '" + D1 + "'"

                        End Select

                    End If



                    ts = timenow - Old_time
                    If ts.TotalSeconds > TimeLag Then
                        tb = db.ExecuteSql(Sql_Command)
                        System.Diagnostics.Debug.WriteLine(Sql_Command)
                        If Sql_Command2 <> "" Then
                            tb2 = db.ExecuteSql(Sql_Command2)
                            System.Diagnostics.Debug.WriteLine(Sql_Command2)
                        End If
                        If Sql_Command3 <> "" Then
                            tb3 = db.ExecuteSql(Sql_Command3)
                            System.Diagnostics.Debug.WriteLine(Sql_Command3)
                        End If
                        Old_time = timenow
                    End If

                    Display1.On1()

                    Read_flag = False
                Else
                    A = "カードが読み取れません"
                End If
                Me.TextBox1.Text = A
                db.Disconnect()
            End If
        End If
    End Sub

    '===============================
    ' txtMessage にメッセージを追加
    '===============================
    Private Sub TxtMessage_Text2(ByVal value As String, ByVal Idm As String)

        Dim db As New OdbcDbIf

        Dim t1 As String, D1 As String, n1 As String, S1 As String, t2 As String, t3 As String
        Dim Idm2 As String
        Dim A As String
        Dim timenow As DateTime

        value = value.Replace(vbCrLf, "")

        If value <> "" And Dic3.ContainsKey(value) And Dic4.ContainsKey(value) Then

            Idm2 = Dic4(value)

            If Idm2 = Idm Then
                Busy = True
                DataBaseTimer.Enabled = False
                timenow = TruncSecond(DateTime.Now())
                t1 = timenow.ToString("HH:mm:ss")
                t3 = timenow.ToString("HH:mm")
                D1 = DateTime.Now.ToString("yyyy-MM-dd")

                A = "職員番号：" + value
                t2 = Me.Label1.Text

                S1 = ""

                n1 = Dic3(value)
                A += "、氏名：" + n1 + "さん"

                Select Case Me.Mode
                    Case 1
                        A += "、出勤"
                        S1 = "出勤"
                    Case 2
                        A += "、外出"
                        S1 = "外出"
                    Case 3
                        A += "、戻り"
                        S1 = "戻り"
                    Case 4
                        A += "、退勤"
                        S1 = "退勤"
                End Select
                A += "時刻：" + t3
                If t2 <> "" Then
                    Select Case S1
                        Case "出勤"
                            If t2.Substring(0, 2) = "早出" Then
                                A += "、" + t2
                            End If
                        Case "退勤"
                            If t2.Substring(0, 2) = "残業" Then
                                A += "、" + t2
                            End If
                        Case "外出"
                            If t2 = "公用外出" Or t2 = "私用外出" Then
                                A += "、" + t2
                            End If
                        Case Else
                            t2 = ""
                    End Select

                End If


                Sql_Command = "INSERT INTO """ + MemberLogTable + """ (""職員番号"",""職員名"",""Idm"",""種別"",""日付"",""時刻"",""特記事項"",""ホスト名"",""アドレス"")"
                Sql_Command += " VALUES ('" + value + "','" + n1 + "','" + Idm + "','" + S1 + "',DATE '" + D1 + "',TIME '"
                Sql_Command += t1 + "','" + t2 + "','" + hostname + "','" + IP + "')"

                If Sql_Command <> Sql_Command_Last Then
                    Sql1.Enqueue(Sql_Command)
                    Sql_Command_Last = Sql_Command
                End If

                Display1.On1()

                Read_flag = False

                Me.TextBox1.Text = A

                Busy = False
                DataBaseTimer.Enabled = True

            Else
                Read_flag = False
                Me.TextBox1.Text = "職員番号とカード固有番号が一致しません。"
                Busy = False
                DataBaseTimer.Enabled = True
            End If
        Else
            Read_flag = False
            Me.TextBox1.Text = "このカードは登録されていません"
            Busy = False
            DataBaseTimer.Enabled = True
        End If
    End Sub

    Public Function TruncSecond(source As DateTime) As DateTime
        Dim d = New DateTime(source.Year, source.Month, source.Day, source.Hour, source.Minute, 0, 0)
        Return d
    End Function

    Private Sub DataBaseExecute()
        Dim db As New OdbcDbIf
        Dim tb As DataTable

        Dim com1 As String
        DataBaseTimer.Enabled = False
        If Busy = False Then
            Dim n As Integer = Sql1.Count
            If n > 0 Then
                If db.Connect() = 0 Then
                    For i As Integer = 0 To n - 1
                        com1 = Sql1.Dequeue()

                        tb = db.ExecuteSql(com1)
                        System.Diagnostics.Debug.WriteLine(com1)

                    Next
                    db.Disconnect()
                End If
            End If
            End If
        DataBaseTimer.Enabled = False

    End Sub




    Private Sub AfterInputTimer_True()

        Me.AfterInputTimer.Enabled = True

    End Sub

    Private Sub Thread_PCSC()
        '
        '   IC カードの読み取りルーチン
        '
        '       ①ICカードから職員番号を読み取り、データベースへのSQLコマンドを作成する。
        '       ②次の読み取りのためのタイマーを起動する。
        '
        '----------
        ' 変数定義
        '----------
        Dim pcsc As New ClsWinSCard(CardMasterKeyString)    ' ICカード操作用オブジェクトの作成
        Dim msg As String = ""
        Dim flag1 As Boolean

        '----------------
        ' 別スレッドからサブルーチンを呼び出すためのDelegateの作成
        '----------------
        '  データベースへ送信するSQLコマンドの作成サブルーチン
        Dim msg_txt As New txtMessage_Text_Delegate(AddressOf TxtMessage_Text2)
        '  次のICカード読み取りまでのタイマーの起動サブルーチン
        Dim AfterInputtimerDelegate As New Timer3_Enable_Delegate(AddressOf AfterInputTimer_True)

        '----------------
        ' ボタンを無効化
        '----------------
        'Me.Invoke(btn_enable, New Object() {False})

        '------------------------------
        ' タイムアウトする時間(ミリ秒)
        '------------------------------
        'pcsc.Timeout_MilliSecond = 3000

        '------------------------------------
        ' FelicaのIDm,PMM、MifareのUIDを取得
        '------------------------------------
        Do
            ' カードを読み取るまで無限に繰り返す。

            If pcsc.GetNoWithMac_A() Then   ' 暗号付きのカードの読み取り
                If pcsc.IsFelica Then   ' フェリカ以外のカードは読み取らない。
                    Me.No = pcsc.S_PAD0.Trim()
                    Me.Idm = pcsc.IDm.Trim()
                    System.Media.SystemSounds.Beep.Play()
                    My.Computer.Keyboard.SendKeys(" ", True)

                End If
                If Me.No > 0 Then
                    Me.Invoke(msg_txt, New Object() {Me.No, Me.Idm})    ' 職員番号でSQLコマンドを作成
                    First_flag = False                          ' 初回フラグをFalseにする。
                    flag1 = True
                End If

            Else

                'Me.Invoke(msg_txt, New Object() {msg + vbNewLine})
                flag1 = False
            End If
            If (First_flag = False And flag1 = True) Then Exit Do
        Loop

        Me.Invoke(AfterInputtimerDelegate, New Object() {})  ' 次の読み取りのためのタイマーを起動する。

    End Sub

    '======================
    ' ボタン・クリック処理
    '======================
    ' 実行中、画面が固まってしまうので実際の処理は別スレッドで実行します


    Private Sub ReadCard()
        '
        ' ICカードの読み取り
        '　　実行中、画面が固まってしまうので実際の処理は別スレッドでバックグラウンド実行する
        '
        If Read_flag = False Then   ' すでに読み取り中（Read_flag = True）の場合は実行しない
            'Me.TextBox1.Text = ""
            Me.Label1.Text = ""
            'PC/SC通信を別スレッドで実行
            th = New Thread(New ThreadStart(AddressOf Thread_PCSC))
            'バックグランドで実行
            th.IsBackground = True
            'スレッド開始
            th.Start()
            Read_flag = True
            'System.Diagnostics.Debug.WriteLine("card read")
            'Display1.On1()
        End If
    End Sub


    Private Sub AfterInputTimer_Tick(sender As Object, e As EventArgs) Handles AfterInputTimer.Tick
        '
        '   カード読み取り後の再読み込み
        '
        '
        If TimeSpan.Compare(DateTime.Now.TimeOfDay, New TimeSpan(12, 0, 0)) = -1 Then
            Me.Mode = 1     ' 午前中は出勤モード
        Else
            Me.Mode = 4     ' 午後は退勤モード
        End If
        'Me.Mode = 1
        Button_change(Me.Mode)
        Me.TextBox1.Text = message1
        Me.Label1.Text = ""
        'If First_flag Then
        '    First_flag = False
        '    Timer2.Interval = int1
        '    'Timer4.Enabled = True
        'End If
        'Timer2.Enabled = True

        AfterInputTimer.Enabled = False
        ReadCard()

        System.Diagnostics.Debug.WriteLine("timer 3")
    End Sub

    Private Sub ModeChangeTimer_Tick(sender As Object, e As EventArgs) Handles ModeChangeTimer.Tick
        'Timer3.Enabled = False
        If TimeSpan.Compare(DateTime.Now.TimeOfDay, New TimeSpan(12, 0, 0)) = -1 Then
            Me.Mode = 1
        Else
            Me.Mode = 4
        End If
        'Me.Mode = 1
        Button_change(Me.Mode)
        Me.Label1.Text = ""
        ModeChangeTimer.Enabled = False
        System.Diagnostics.Debug.WriteLine("timer 4")
    End Sub

    Private Sub Mode_change2()
        If TimeSpan.Compare(DateTime.Now.TimeOfDay, New TimeSpan(12, 0, 0)) = -1 Then
            Me.Mode = 1
        Else
            Me.Mode = 4
        End If
        'Me.Mode = 1
        Button_change(Me.Mode)
        Me.Label1.Text = ""
        ModeChangeTimer.Enabled = False
    End Sub

    Private Sub DicReNewTimer_Tick(sender As Object, e As EventArgs) Handles DicReNewTimer.Tick
        If Busy = False Then
            SetDic1()
        End If
    End Sub

    Private Sub OfficialRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles OfficialRadioButton.Click
        'If Me.OfficialRadioButton.Checked = False Then
        'player.Play()
        System.Console.Beep()
        Me.OfficialRadioButton.Checked = True
        Me.PrivateRadioButton.Checked = False
        Me.Label1.Text = "公用外出"
        'End If
    End Sub

    Private Sub PrivateRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles PrivateRadioButton.Click
        'If PrivateRadioButton.Checked = False Then
        'player.Play()
        System.Console.Beep()
        Me.OfficialRadioButton.Checked = False
        Me.PrivateRadioButton.Checked = True
        Me.Label1.Text = "私用外出"
        'End If
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) 
        System.Console.Beep()
        'Microsoft.VisualBasic.Beep()
    End Sub

    Private Sub ModeChangeTimer2_Tick(sender As Object, e As EventArgs) Handles ModeChangeTimer2.Tick
        If ModeChangeTimer.Enabled = False Then
            If TimeSpan.Compare(DateTime.Now.TimeOfDay, New TimeSpan(12, 0, 0)) = -1 Then
                Me.Mode = 1
            Else
                Me.Mode = 4
            End If
            'Me.Mode = 1
            Button_change(Me.Mode)
            Me.Label1.Text = ""
        End If
        'ModeChangeTimer.Enabled = False
    End Sub

    Private Sub DataBaseTimer_Tick(sender As Object, e As EventArgs) Handles DataBaseTimer.Tick

        'th2 = New Thread(New ThreadStart(AddressOf DataBaseExecute))
        ''バックグランドで実行
        'th2.IsBackground = True
        ''スレッド開始
        'th2.Start()
        DataBaseExecute()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles OverTimeButton.Click
        Dim OverTimeForm1 As New OverTimeForm
        'player.Play()
        System.Console.Beep()
        OverTimeForm1.Kind = "残業"
        Me.ModeChangeTimer.Enabled = False
        'Me.Timer2.Enabled = False
        'OverTimeForm1.Show()
        'モーダル開き、戻り値を受け取る
        OverTimeForm1.StartPosition = FormStartPosition.CenterParent
        If OverTimeForm1.ShowDialog = DialogResult.OK Then         '値を受け取る
            Label1.Text = OverTimeForm1.GetValue
        End If
        OverTimeForm1.Dispose()
        'Timer4.Interval = 10000
        Me.ModeChangeTimer.Enabled = True
        'Me.Timer2.Enabled = True
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles EarlyTimeButton.Click
        Dim OverTimeForm1 As New OverTimeForm
        'player.Play()
        System.Console.Beep()
        OverTimeForm1.Kind = "早出"
        Me.ModeChangeTimer.Enabled = False
        'Me.Timer2.Enabled = False
        'OverTimeForm1.Show()
        'モーダル開き、戻り値を受け取る
        OverTimeForm1.StartPosition = FormStartPosition.CenterParent
        If OverTimeForm1.ShowDialog = DialogResult.OK Then         '値を受け取る
            Label1.Text = OverTimeForm1.GetValue
        End If
        OverTimeForm1.Dispose()
        'Timer4.Interval = 10000
        Me.ModeChangeTimer.Enabled = True
        'Me.Timer2.Enabled = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim inputText As String
        inputText = InputBox("パスワードを入力")
        If inputText = ClosePassWord Then
            th.Abort()  ' スレッドの停止
            th.Join()   ' スレッド終了待機
            Me.Close()
            Me.Dispose()
        End If

    End Sub
End Class