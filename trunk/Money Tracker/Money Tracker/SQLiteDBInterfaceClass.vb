'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 05/09/2012
' Time: 3:32 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Data
Imports System.Data.Linq
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
		
		Dim dbElement() As XElement = g_config.GetDatabases
		
		' Check to see if the databas is there
		If Not FileIO.FileSystem.FileExists(databaseFileName) Then
			
			' Need to create the file
			m_sql = New SQLiteDatabase(databaseFileName, True)
			
			For Each item As XElement In dbElement
				m_sql.ExecuteNonQuery(item.<create>.Value)
			Next
			
		Else
			
			' Need to just open the file
			m_sql = New SQLiteDatabase(databaseFileName)
			
		End If
		
		' Setup proper names
		m_tableName = dbElement(0).<name>.Value
		m_catTableName = dbElement(1).<name>.Value
		subCatTableName = dbElement(2).<name>.Value
		
	End Sub
	
	Public Function InsertData(d As String) As Boolean
		Return m_sql.Insert(m_tableName, d)
	End Function
	
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
