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
	
	Public Sub New()
		
		' Private variables
		
		Me.InitializeComponent()
		
		' Create the data connection
		Dim bs As BindingSource = g_sql.Sql.BindingDatabase(String.Format("SELECT * FROM {0}", g_sql.TableName))
		dataGridView.DataSource = bs
		
	End Sub
	
End Class
