'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 28/08/2012
' Time: 9:05 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports MyInterface.SQLite

Public Partial Class DBUpdaterNotesForm
	
	' The rich text box of the parent
	Private searchContext As String
	
	' The return value
	Private m_noteResult As String
	Private m_categoryResult As String
	
	Public Sub New(ByVal name As String)
		
		Me.InitializeComponent()
		
		' Private variables
		searchContext = name
		m_noteResult = ""
		m_categoryResult = ""
		
		' Do the grid view initially with the search option enabled
		DoSearch()
		
	End Sub
	
	Sub DataGridViewSelection(sender As Object, e As DataGridViewCellEventArgs)
		
		If e.ColumnIndex = 1 Then
			
			'When we click on the cell twice we will tell the parent what we want
			m_noteResult = dataGridView.Rows(e.RowIndex).Cells(1).Value.ToString
			
			' We get the category that goes with this selection
			m_categoryResult = g_sql.Sql.SearchForItem("categories", g_sql.TableName, String.Format("name=>{0}", dataGridView.Rows(e.RowIndex).Cells(0).Value.ToString))
			m_categoryResult = g_sql.Sql.SearchForItem("name", g_sql.CatTableName, String.Format("id=>{0}", m_categoryResult))
			
			If m_categoryResult Is Nothing Then
				m_categoryResult = "<new>"
			End If
			
			Me.DialogResult = DialogResult.OK
			
		End If
		
	End Sub
	
	
	Sub DataGridViewRowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
		
		'When we click on the cell twice we will tell the parent what we want
		m_noteResult = dataGridView.Rows(e.RowIndex).Cells(1).Value.ToString
			
		' We get the category that goes with this selection
		m_categoryResult = g_sql.Sql.SearchForItem("categories", g_sql.TableName, String.Format("name=>{0}", dataGridView.Rows(e.RowIndex).Cells(0).Value.ToString))
		m_categoryResult = g_sql.Sql.SearchForItem("categoryname", g_sql.CatTableName, String.Format("id=>{0}", m_categoryResult))
		
		If m_categoryResult Is Nothing Then
			m_categoryResult = "<new>"
		End If
		
		Me.DialogResult = DialogResult.OK
		
	End Sub
	
	Sub DBUpdaterNotesFormFormClosing(sender As Object, e As FormClosingEventArgs)
		
		If e.CloseReason = CloseReason.UserClosing Then
			
			' Set the dialog result to fail
			Me.DialogResult = DialogResult.Cancel
			
		End If
		
	End Sub
	
	Public ReadOnly Property NoteResult() As String
		
		Get
			Return m_noteResult
		End Get
		
	End Property
	
	Public ReadOnly Property CategoryResult() As String
		
		Get
			Return m_categoryResult
		End Get
		
	End Property
	
	Private Sub DoSearch()
		
		' Delete the DataSource in grid view
		dataGridView.DataSource = Nothing
		
		' The search to perform
		Dim search As String = ""
		
		' Check to see if we want to utilize the search
		If noSearchToolStripMenuItem.Checked Then
			
			' Setup the search to begin with
			search = "("
				
			' Split up the name a do a search for similar ones
			Dim words() As String = Split(searchContext, " ")
			
			For Each word In words
				If word.Length > 3 Then
					search += String.Format("name LIKE '%{0}%' OR ", word)
				End If
			Next
			
			' Strip the last 3 off ' OR'
			search = search.Substring(0, search.Length - 4)
			
			search += ") AND"
			
		End If
		
		' Create the data connection
		dataGridView.DataSource = g_sql.Sql.GetDataTable(String.Format("SELECT name,comment FROM {0} WHERE {1} comment IS NOT NULL AND categories IS NOT NULL", g_sql.TableName, search))
		
	End Sub
	
	
	Sub NoSearchToolStripMenuItemClick(sender As Object, e As EventArgs)
		
		' Set the check box to on
		DoSearch()
		
	End Sub
	
End Class
