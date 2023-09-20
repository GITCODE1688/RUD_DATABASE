Module Module1

    Sub Main()
        Dim dbName As String = "mydb.db3"
        Dim SQL As String = "SELECT * FROM BGG_PACK ORDER BY sn_ DESC LIMIT 5"
        Dim RUD As RUD_DATABASE.DatabaseConnectSQLite = New RUD_DATABASE.DatabaseConnectSQLite
        Dim dt As DataTable = RUD.ExecuteQuery(SQL, dbName)
        Dim outputString As String = ""
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                outputString = ""
                For j As Integer = 0 To 5
                    outputString += dt.Rows(i).Item(j).ToString() & " / "
                Next
                Console.WriteLine(outputString)
            Next
        Else
            Console.WriteLine("no data")
        End If

        Console.WriteLine("db connect test OK")
        Console.WriteLine("press any key to exit.")
        Console.ReadKey()
    End Sub

End Module
