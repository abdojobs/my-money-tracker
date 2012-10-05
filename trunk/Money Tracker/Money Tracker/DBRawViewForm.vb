'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 29/08/2012
' Time: 12:21 AM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.Data
Imports System.Data.SQLite
Imports MyInterface.SQLite

Public Partial Class DBRawViewForm
	
	Private tracker As Integer
	
	Public Sub New()
		
		' Private variables
		
		Me.InitializeComponent()
		
		' Create the data connection
		Dim bs As New BindingSource
		tracker = g_sql.Sql.BindingDatabase(String.Format("SELECT * FROM {0}", g_sql.TableName), bs)
		dataGridView.DataSource = bs
		
	End Sub
	
	Public Sub Reset()
		
		' Get rid of data in the data grid view
		dataGridView.DataSource = Nothing
		
		' Create the data connection
		Dim bs As New BindingSource
		g_sql.Sql.BindingDatabase(String.Format("SELECT * FROM {0}", g_sql.TableName), bs, tracker)
		dataGridView.DataSource = bs
		
	End Sub
	
End Class
