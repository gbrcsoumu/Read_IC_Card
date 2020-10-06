Module Module1
    Public LoginID As String                                ' FileMakerのログインID
    Public LoginPassWord As String                          ' FileMakerのログインパスワード
    Public Const DataBaseName As String = "退勤管理test01"  ' FileMakerのデータベース名
    Public Const MemberNameTable As String = "職員一覧"     ' 職員名簿のテーブル名
    Public Const MemberLogTable As String = "出退勤記録"      ' 退勤管理のテーブル名
    Public Const DateLogTable As String = "出退勤一覧"      ' 出退勤一覧のテーブル名
    Public Const CodeTable1 As String = "出勤コード一覧"     ' 出退コード一覧のテーブル名
    Public Const CodeTable2 As String = "退勤コード一覧"     ' 退勤コード一覧のテーブル名
    Public Const GooutTable As String = "外出記録"          ' 外出記録のテーブル名
    Public Const ReturnTable As String = "戻り記録"          ' 戻り記録のテーブル名
    Public Const CardMasterKeyString = "GBRC 2020"          ' Felicaカードの暗号化のキー
    Public Const ClosePassWord As String = "exit"
    Public Const ListReadTime As String = "03：00：00"            ' 職員一覧を読み込む時刻

End Module
