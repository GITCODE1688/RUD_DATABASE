Imports System.Data.SQLite
Imports Utils
Public Class DatabaseConnectSQLite
    Dim Connect As SQLiteConnection
    Dim DataAdapter As SQLiteDataAdapter = New SQLiteDataAdapter
    Dim Commmand As SQLiteCommand
    Dim Trans As SQLiteTransaction
    Dim log As New LogUtils

    Sub New()
        log.setup()
    End Sub
    Function getConnectString(ByVal dbName As String) As String
        Dim Database As String = dbName   'ex: localdb.db3
        Dim ConnectString As String = "Data Source=.\" & dbName & ";Version=3;New=False;Compress=True;"
        Return ConnectString
    End Function

    Private Sub ConnectOPEN(ByVal connectString As String)
        If Connect.State = ConnectionState.Open Then
        Else
            Connect = New SQLiteConnection(connectString)
            Connect.Open()
        End If
    End Sub

    Private Sub ConnectCLOSE(ByVal connectString As String)
        If Connect.State = ConnectionState.Closed Then
        Else
            Connect = New SQLiteConnection(connectString)
            Connect.Close()
        End If
    End Sub

    Function ExecuteNonQuery(ByVal sql As String, ByVal dbName As String) As Boolean
        Dim connectString As String = getConnectString(dbName)
        Connect = New SQLiteConnection(connectString)
        Try
            ConnectOPEN(connectString)
            Commmand = New SQLiteCommand(sql, Connect)
            Trans = Connect.BeginTransaction
            Commmand.Transaction = Trans
            Commmand.ExecuteNonQuery()
            Trans.Commit()
        Catch ex As Exception
            Trans.Rollback()
            log.e(ex)
            Return False
        Finally
            ConnectCLOSE(connectString)
        End Try
        Return True
    End Function

    Function ExecuteQuery(ByVal sql As String, ByVal dbName As String) As DataTable
        Dim connectString As String = getConnectString(dbName)
        Connect = New SQLiteConnection(connectString)
        Dim dt As DataTable = New DataTable
        Try
            Commmand = New SQLiteCommand(sql, Connect)
            DataAdapter.SelectCommand = Commmand
            DataAdapter.Fill(dt)
        Catch ex As Exception
            log.e(ex)
        End Try
        Return dt
    End Function
End Class
