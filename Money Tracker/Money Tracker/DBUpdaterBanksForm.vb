'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 28/08/2012
' Time: 11:26 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports MyInterface.SQLite

Public Partial Class DBUpdaterBanksForm
	
	' The rich text box of the parent
	Private m_result As String
	
	Public Sub New()
		
		Me.InitializeComponent()
		
		' Private variables
		m_result = Nothing
		
		' Create the data connection
		dataGridView.DataSource = g_sql.Sql.GetDataTable(String.Format("SELECT DISTINCT bank FROM {0}", g_sql.TableName))
		
		' Sort them
		dataGridView.Sort(dataGridView.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
		
	End Sub
	
	Sub DataGridViewSelection(sender As Object, e As DataGridViewCellEventArgs)
		
		If e.ColumnIndex = 0 Then
			
			'When we click on the cell twice we will tell the parent what we want
			m_result = dataGridView.Rows(e.RowIndex).Cells(0).Value.ToString
			Me.DialogResult = DialogResult.OK
			
		End If
		
	End Sub
	
	
	Sub DataGridViewRowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
		
		'When we click on the cell twice we will tell the parent what we want
		m_result = dataGridView.Rows(e.RowIndex).Cells(0).Value.ToString
		Me.DialogResult = DialogResult.OK
		
	End Sub
	
	Sub DBUpdaterCategoriesFormFormClosing(sender As Object, e As FormClosingEventArgs)
		
		If e.CloseReason = CloseReason.UserClosing Then
			
			' Set the dialog result to fail
			Me.DialogResult = DialogResult.Cancel
			
		End If
		
	End Sub
	
	Public ReadOnly Property Result() As String
		
		Get
			Return m_result
		End Get
		
	End Property
	
End Class
