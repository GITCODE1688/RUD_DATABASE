Imports Oracle.ManagedDataAccess.Client
Imports Utils
Public Class DatabaseConnectOracle
    Dim Connect As OracleConnection
    Dim DataAdapter As OracleDataAdapter = New OracleDataAdapter
    Dim Commmand As OracleCommand
    Dim Trans As OracleTransaction
    Dim log As New LogUtils

    Sub New()

    End Sub

    Function getConnectString(ByVal dbName As String) As String
        Dim Host As String = "SBDB"
        Dim Port As String = "1521"
        Dim OracleSID As String = dbName
        Dim Username As String = "USERNAME"
        Dim Password As String = "PASSWORD"

        Dim ConnectString As String = "Data Source= (DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST= " & Host & ")(PORT= " & Port & ")))" &
                                      "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME= " & OracleSID & "))) ; " &
                                      "User Id=" & Username & " ; " &
                                      "Password= " & Password & " ;  "
        Return ConnectString
    End Function

    Private Sub ConnectOPEN(ByVal connectString As String)
        If Connect.State = ConnectionState.Open Then
        Else
            Connect = New OracleConnection(connectString)
            Connect.Open()
        End If
    End Sub

    Private Sub ConnectCLOSE(ByVal connectString As String)
        If Connect.State = ConnectionState.Closed Then
        Else
            Connect = New OracleConnection(connectString)
            Connect.Close()
        End If
    End Sub

    Function ExecuteNonQuery(ByVal sql As String, ByVal dbName As String) As Boolean
        Dim connectString As String = getConnectString(dbName)
        Connect = New OracleConnection(connectString)
        Try
            ConnectOPEN(connectString)
            Commmand = New OracleCommand(sql, Connect)
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
        Connect = New OracleConnection(connectString)
        Dim dt As DataTable = New DataTable
        Try
            Commmand = New OracleCommand(sql, Connect)
            DataAdapter.SelectCommand = Commmand
            DataAdapter.Fill(dt)
        Catch ex As Exception
            Throw
        End Try
        Return dt
    End Function
End Class
