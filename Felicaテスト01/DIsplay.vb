Imports System.Runtime.InteropServices

Public Class Display
    Private Declare Function SendMessageA Lib "user32" (hWnd As Integer, Msg As Integer, wParam As Integer, IParam As Integer) As Integer
    'Private Declare Function SendInput Lib "user32" (nInputs As Integer, pInputs As MOUSEINPUT, cbSize As Integer) As Integer
    <StructLayout(LayoutKind.Sequential)>
    Private Structure MOUSEINPUT
        Public dx As Integer
        Public dy As Integer
        Public mouseData As Integer
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Structure

    ' キーボードイベント(keybd_eventの引数と同様のデータ)
    <StructLayout(LayoutKind.Sequential)>
    Private Structure KEYBDINPUT
        Public wVk As Short
        Public wScan As Short
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Structure

    ' ハードウェアイベント
    <StructLayout(LayoutKind.Sequential)>
    Private Structure HARDWAREINPUT
        Public uMsg As Integer
        Public wParamL As Short
        Public wParamH As Short
    End Structure

    ' 各種イベント(SendInputの引数データ)
    <StructLayout(LayoutKind.Explicit)>
    Private Structure INPUT
        <FieldOffset(0)> Public type As Integer
        <FieldOffset(4)> Public mi As MOUSEINPUT
        <FieldOffset(4)> Public ki As KEYBDINPUT
        <FieldOffset(4)> Public hi As HARDWAREINPUT
    End Structure

    ' キー操作、マウス操作をシミュレート(擬似的に操作する)
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Sub SendInput(
        ByVal nInputs As Integer, ByRef pInputs As INPUT, ByVal cbsize As Integer)
    End Sub
    ' 仮想キーコードをスキャンコードに変換
    <DllImport("user32.dll", EntryPoint:="MapVirtualKeyA")>
    Private Shared Function MapVirtualKey(
        ByVal wCode As Integer, ByVal wMapType As Integer) As Integer
    End Function

    Private Const INPUT_MOUSE = 0                   ' マウスイベント
    Private Const INPUT_KEYBOARD = 1                ' キーボードイベント
    Private Const INPUT_HARDWARE = 2                ' ハードウェアイベント

    Private Const MOUSEEVENTF_MOVE = &H1            ' マウスを移動する
    Private Const MOUSEEVENTF_ABSOLUTE = &H8000     ' 絶対座標指定
    Private Const MOUSEEVENTF_LEFTDOWN = &H2        ' 左　ボタンを押す
    Private Const MOUSEEVENTF_LEFTUP = &H4          ' 左　ボタンを離す
    Private Const MOUSEEVENTF_RIGHTDOWN = &H8       ' 右　ボタンを押す
    Private Const MOUSEEVENTF_RIGHTUP = &H10        ' 右　ボタンを離す
    Private Const MOUSEEVENTF_MIDDLEDOWN = &H20     ' 中央ボタンを押す
    Private Const MOUSEEVENTF_MIDDLEUP = &H40       ' 中央ボタンを離す
    Private Const MOUSEEVENTF_WHEEL = &H800         ' ホイールを回転する
    Private Const WHEEL_DELTA = 120                 ' ホイール回転値

    Private Const KEYEVENTF_KEYDOWN = &H0           ' キーを押す
    Private Const KEYEVENTF_KEYUP = &H2             ' キーを離す
    Private Const KEYEVENTF_EXTENDEDKEY = &H1       ' 拡張コード
    Private Const VK_SHIFT = &H10                   ' SHIFTキー
    '    UINT SendInput(
    '  UINT nInputs,
    '  LPINPUT pInputs, 
    '  int cbSize
    '); 
    '    Structure User32.SendInput(1, inputs, inputs.bytesize)
    Public Sub On1()
        SendMessageA(-1, 274, 61808, -1)

        Const num As Integer = 3
        Dim inp As INPUT() = New INPUT(num - 1) {}

        ' (1)マウスカーソルを移動する(スクリーン座標でX座標=800ピクセル,Y=400ピクセルの位置)
        inp(0).type = INPUT_MOUSE
        inp(0).mi.dwFlags = MOUSEEVENTF_MOVE Or MOUSEEVENTF_ABSOLUTE
        inp(0).mi.dx = 800 * (65535 / Screen.PrimaryScreen.Bounds.Width)
        inp(0).mi.dy = 400 * (65535 / Screen.PrimaryScreen.Bounds.Height)
        inp(0).mi.mouseData = 0
        inp(0).mi.dwExtraInfo = 0
        inp(0).mi.time = 0

        ' (2)マウスの右ボタンを押す
        inp(1).type = INPUT_MOUSE
        inp(1).mi.dwFlags = MOUSEEVENTF_RIGHTDOWN
        inp(1).mi.dx = 0
        inp(1).mi.dy = 0
        inp(1).mi.mouseData = 0
        inp(1).mi.dwExtraInfo = 0
        inp(1).mi.time = 0

        ' (3)マウスの右ボタンを離す
        inp(2).type = INPUT_MOUSE
        inp(2).mi.dwFlags = MOUSEEVENTF_RIGHTUP
        inp(2).mi.dx = 0
        inp(2).mi.dy = 0
        inp(2).mi.mouseData = 0
        inp(2).mi.dwExtraInfo = 0
        inp(2).mi.time = 0

        ' マウス操作実行
        SendInput(num, inp(0), Marshal.SizeOf(inp(0)))

        SendMessageA(-1, 274, 61808, -1)

    End Sub
    Public Sub Off1()
        SendMessageA(-1, 274, 61808, 2)
    End Sub
End Class