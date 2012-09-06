'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 05/09/2012
' Time: 3:32 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports MyInterface.SQLite

Public Class SQLiteDBInterfaceClass
	
	' The Database we are using
	Private m_sql As SQLiteDatabase
	
	' Private variable for the database file name
	Private databaseFileName As String = "MoneyDataBase.s3db"
	
	' Private variable for the main table being used
	Private m_tableName As String = "Money"
	
	' Private variable for the categories table being used
	Private m_catTableName As String = "Categories"
	
	' Private variable for the sub categories table being used
	Private subCatTableName As String = "SubCategories"
	
	Public Sub New()
		
		' Check to see if the databas is there
		If Not FileIO.FileSystem.FileExists(databaseFileName) Then
			
			' Need to create the file
			m_sql = New SQLiteDatabase(databaseFileName, True)
		
			' Need to do this with money as well once I have it developed
			m_sql.ExecuteNonQuery("CREATE TABLE " + m_tableName + " (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,bank TEXT NULL,name TEXT NULL,out TEXT NULL,in TEXT NULL,date DATE NULL,comment TEXT NULL,categories TEXT NULL,subcategories TEXT NULL);")
			m_sql.ExecuteNonQuery("CREATE TABLE " + m_catTableName + " (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,name TEXT NULL);")
			m_sql.ExecuteNonQuery("CREATE TABLE " + subCatTableName + " (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,name TEXT NULL,owner INTEGER NULL);")
			
		Else
			
			' Need to just open the file
			m_sql = New SQLiteDatabase(databaseFileName)
			
		End If
		
	End Sub
	
	Public ReadOnly Property Sql() As SQLiteDatabase
		Get
			Return m_sql
		End Get
	End Property
	
	Public ReadOnly Property TableName() As String
		Get
			Return m_tableName
		End Get
	End Property
	
	Public ReadOnly Property CatTableName() As String
		Get
			Return m_catTableName
		End Get
	End Property
	
End Class
