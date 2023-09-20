# RUD_DATABASE
only read、update、delete database
只有讀取、更新、刪除功能

## 已完成項目
 ### DatabaseConnectMDB 讀寫 access 資料庫 (未完成)    
 ### DatabaseConnectMSSql  讀寫 MSSql 資料庫 (完成)     
 ### DatabaseConnectOracle  讀寫 Oracle 資料庫 (完成)    
    需加入 Oracle.ManagedDataAccess.dll 參考
### DatabaseConnectPostgresql  讀寫 Postgresql 資料庫 (完成)    
### DatabaseConnectSQLite.vb  讀寫 SQLite 資料庫 (完成)    
    需加入 System.Data.SQLite.dll 參考
## 使用前需變更連線資訊
``` vb.net
ex:mssql
        Dim Server As String = "192.168.1.11\SQLEXPRESS"
        Dim Username As String = "username"
        Dim Password As String = "password"
        Dim Database As String = dbName
```
