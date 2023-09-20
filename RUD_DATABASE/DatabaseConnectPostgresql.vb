Imports System.Data.Odbc
Imports Utils
Public Class DatabaseConnectPostgresql
    Dim Connect As OdbcConnection
    Dim DataAdapter As OdbcDataAdapter = New OdbcDataAdapter
    Dim Commmand As OdbcCommand
    Dim Trans As OdbcTransaction
    Dim log As New LogUtils

    Sub New()

    End Sub

    Function getConnectString(ByVal dbName As String) As String
        Dim Dsn As String = "PostgreDBS2"
        Dim Server As String = "dbs1.YAHOO.com.tw"
        Dim Port As String = "5432"
        Dim Username As String = "USERNAME"
        Dim Password As String = "PASSWORD"
        Dim Database As String = dbName

        Dim ConnectString As String = "dsn=" & Dsn &
                                      ";Server=" & Server &
                                      ";Port=" & Port &
                                      ";Database=" & Database &
                                      ";uid=" & Username &
                                      ";pwd=" & Password & ";"
        Return ConnectString
    End Function

    Private Sub ConnectOPEN(ByVal connectString As String)
        If Connect.State = ConnectionState.Open Then
        Else
            Connect = New OdbcConnection(connectString)
            Connect.Open()
        End If
    End Sub

    Private Sub ConnectCLOSE(ByVal connectString As String)
        If Connect.State = ConnectionState.Closed Then
        Else
            Connect = New OdbcConnection(connectString)
            Connect.Close()
        End If
    End Sub

    Function ExecuteNonQuery(ByVal sql As String, ByVal dbName As String) As Boolean
        Dim connectString As String = getConnectString(dbName)
        Connect = New OdbcConnection(connectString)
        Try
            ConnectOPEN(connectString)
            Commmand = New OdbcCommand(sql, Connect)
            Trans = Connect.BeginTransaction
            Commmand.Transaction = Trans
            Commmand.ExecuteNonQuery()
            Trans.Commit()
        Catch ex As Exception
            Trans.Rollback()
            Return False
        Finally
            ConnectCLOSE(connectString)
        End Try
        Return True
    End Function

    Function ExecuteQuery(ByVal sql As String, ByVal dbName As String) As DataTable
        Dim connectString As String = getConnectString(dbName)
        Connect = New OdbcConnection(connectString)
        Dim dt As DataTable = New DataTable
        Try
            Commmand = New OdbcCommand(sql, Connect)
            DataAdapter.SelectCommand = Commmand
            DataAdapter.Fill(dt)
        Catch ex As Exception
            Throw
        End Try
        Return dt
    End Function

End Class
