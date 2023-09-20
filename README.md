# RUD_DATABASE
only read、update、delete database

## database type 
 ### DatabaseConnectMDB RUD access database (undone)    
 ### DatabaseConnectMSSql  RUD MSSql database         
 ### DatabaseConnectOracle  RUD Oracle database      
    reference  Oracle.ManagedDataAccess.dll 
### DatabaseConnectPostgresql  RUD Postgresql database    
### DatabaseConnectSQLite  RUD SQLite database    
    reference  System.Data.SQLite.dll 
## change connect string
``` vb.net
ex:mssql
        Dim Server As String = "192.168.1.11\SQLEXPRESS"
        Dim Username As String = "username"
        Dim Password As String = "password"
        Dim Database As String = dbName
```
