﻿Imports System.Data.SqlClient
Imports Utils
Public Class DatabaseConnectMSSql
    Dim Connect As SqlConnection
    Dim DataAdapter As SqlDataAdapter = New SqlDataAdapter
    Dim Commmand As SqlCommand
    Dim Trans As SqlTransaction
    Dim log As New LogUtils
    Sub New()
        log.setup()
    End Sub

    Function getConnectString(ByVal dbName As String) As String
        Dim Server As String = "192.168.1.11\SQLEXPRESS"
        Dim Username As String = "username"
        Dim Password As String = "password"
        Dim Database As String = dbName

        Dim ConnectString As String = "Server=" & Server &
                                      ";Database=" & Database &
                                      ";uid=" & Username &
                                      ";pwd=" & Password & ";"
        Return ConnectString
    End Function

    Private Sub ConnectOPEN(ByVal connectString As String)
        If Connect.State = ConnectionState.Open Then
        Else
            Connect = New SqlConnection(connectString)
            Connect.Open()
        End If
    End Sub

    Private Sub ConnectCLOSE(ByVal connectString As String)
        If Connect.State = ConnectionState.Closed Then
        Else
            Connect = New SqlConnection(connectString)
            Connect.Close()
        End If
    End Sub

    Function ExecuteNonQuery(ByVal sql As String, ByVal dbName As String) As Boolean
        Dim connectString As String = getConnectString(dbName)
        Connect = New SqlConnection(connectString)
        Try
            ConnectOPEN(connectString)
            Commmand = New SqlCommand(sql, Connect)
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
        Connect = New SqlConnection(connectString)
        Dim dt As DataTable = New DataTable
        Try
            Commmand = New SqlCommand(sql, Connect)
            DataAdapter.SelectCommand = Commmand
            DataAdapter.Fill(dt)
        Catch ex As Exception
            log.e(ex)
        End Try
        Return dt
    End Function
End Class
