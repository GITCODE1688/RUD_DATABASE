Imports System.IO
Imports System.Text

Public Class LogUtils
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory & "\Log\" & Format(Now(), "yyyyMM")

    Public Sub setup()

        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If

        With My.Application.Log.DefaultFileLogWriter
            .BaseFileName = "Log"    'Log檔的檔名39666666--6-6
            .CustomLocation = path   '自訂Log檔的存放路徑
            .AutoFlush = True        'Log檔寫完後自動清除緩衝
            .LogFileCreationSchedule = Logging.LogFileCreationScheduleOption.Daily '設定一天產生一個Log檔
        End With

    End Sub

    'Debug
    Public Sub d(ByVal message As String)
        My.Application.Log.WriteEntry(Now & "    " & message, TraceEventType.Error, 20)
        'Console.WriteLine(message)
    End Sub

    'Information
    Public Sub i(ByVal message As String)
        My.Application.Log.WriteEntry(Now & "    " & message, TraceEventType.Information, 10)
        'Console.WriteLine(message)
    End Sub

    Public Sub i(ByVal hashtable As Hashtable)
        Dim sb As New StringBuilder

        For Each table As Object In hashtable
            sb.Append("{" & table.Key & ":" & table.Value & "}")
        Next

        My.Application.Log.WriteEntry(Now & "    " & sb.ToString, TraceEventType.Information, 10)
        'Console.WriteLine(sb.ToString)
    End Sub

    Public Sub d(ByVal hashtable As Hashtable)
        Dim sb As New StringBuilder

        For Each table As Object In hashtable
            sb.Append("{" & table.Key & ":" & table.Value & "}")
        Next

        My.Application.Log.WriteEntry(Now & "    " & sb.ToString, TraceEventType.Error, 20)
        'Console.WriteLine(sb.ToString)
    End Sub

    'Exception
    Public Sub e(ByVal exception As Exception)
        My.Application.Log.WriteException(exception)
        'Console.WriteLine(exception.ToString)
    End Sub



End Class
