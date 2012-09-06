'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 23/07/2012
' Time: 4:34 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.Data
Imports System.Data.SQLite
Imports MyInterface.SQLite

Public Partial Class DBUpdaterInfoControl
	Inherits System.Windows.Forms.UserControl
	
	' A timer for the dropbox
	Private lastClickTimer As DateTime
	
    Public Sub New()
    	
    	Me.InitializeComponent()
		
		' Private variables
		lastClickTimer = DateTime.Now
		
		' Create the data connection
		Dim bs As BindingSource = g_sql.Sql.BindingDatabase(String.Format("SELECT * FROM {0} WHERE categories IS NULL", g_sql.TableName))
		bindingNavigator.BindingSource = bs
		
		' Bind the CompanyName field to the TextBox control.
		textBoxID.DataBindings.Add(New Binding("Text", bs, "id", True))
		textBoxCategories.DataBindings.Add(New Binding("Text", bs, "categories", True))
		textBoxTransaction.DataBindings.Add(New Binding("Text", bs, "name", True))
		textBoxBank.DataBindings.Add(New Binding("Text", bs, "bank", True))
		textBoxDate.DataBindings.Add(New Binding("Text", bs, "date", True))
		textBoxOut.DataBindings.Add(New Binding("Text", bs, "out", True))
		richTextBoxNote.DataBindings.Add(New Binding("Text", bs, "comment", True))
		
		' Setup the updates
		Dim params(3) As SQLiteParameter
		params(0) = New SQLiteParameter("@id", DbType.Int32, 0, "id")
  		params(1) = New SQLiteParameter("@comment", DbType.String, 1000, "comment")
  		params(2) = New SQLiteParameter("@cat", DbType.String, 10, "categories")
  		g_sql.Sql.SetupBoundUpdate("UPDATE " + g_sql.TableName + " SET comment=@comment, categories=@cat WHERE id=@id", params)
  		g_sql.Sql.SetupBoundDelete("DELETE FROM " + g_sql.TableName + " WHERE id=@id", params)
  		
  		' We have a category
		Dim names As DataTable = g_sql.Sql.GetDataTable("SELECT name FROM " + g_sql.CatTableName)
		
		' loop and put in the names
		For Each name As DataRow In names.Rows
			dropBoxCategory.Items.Add(name(0))
		Next
		
		dropBoxCategory.Items.Add("<new>")
		
    End Sub
	
	Sub ForwardClick(sender As Object, e As EventArgs)
		
		' Update the table
		g_sql.Sql.BoundTableCallUpdate()
		
	End Sub
	
	Sub ForwardDeleteClick(sender As Object, e As EventArgs)
		
		' Delete from the database
		g_sql.Sql.BoundTableCallUpdate()
		
	End Sub
	
	Sub BindingNavigatorAddClick(sender As Object, e As EventArgs)
		' Will do this later
		' Need to allow entry of data into all the text boxes
	End Sub
	
	Private Function SplitForSearch(ByVal field As String, ByVal str As String) As CriteriaCollection
		
		' Temp where line
		Dim criters As CriteriaCollection = New CriteriaCollection()
		
		' Start with a check to see if we have a similar Name
		' Split up the text box
		Dim splits() As String = str.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
		
		' Create the where line
		For Each word In splits
			criters.Add(New Criteria(field, word))
		Next
		
		Return criters
		
	End Function
	
	Sub TextBoxIDTextChanged(sender As Object, e As EventArgs)
		
		' Check to see if there is any value in the textbox
		If textBoxCategories.Text.Length > 0 Then
		
			' This is done every time we click, mostly worried about the first one but not all that worried
			Dim category As String = g_sql.Sql.SearchForItem("name", g_sql.CatTableName, String.Format("id=>{0}", textBoxCategories.Text))
			
			' Now set the category drop box
			dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf(category)
			
		' Its not set so we goto the new
		Else
			dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf("<new>")
			
		End If
		
	End Sub
	
	Sub ButtonCategoriesClick(sender As Object, e As EventArgs)
		
		' Check to see if old category is what we want
		If dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf("<new>") Then
		
			' We want to create a new category or edit a category
			Dim name As String = InputBox("Name of Category:", "Category Setting")
			
			' Check to see something was entered
			If name.Length > 0 Then
				
				' Check it against the DB
				Dim test As String = g_sql.Sql.SearchForItem("name", g_sql.CatTableName, String.Format("name=>{0}", name), True)
				
				If String.Compare(test, name) = -1 Then
					
					' Add to the drop box
					dropBoxCategory.Items.Insert(dropBoxCategory.Items.IndexOf("<new>"), name)
					dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf(name)
					
					' Insert into DB
					If g_sql.Sql.Insert(g_sql.CatTableName, String.Format("name=>{0}", name)) Then
						textBoxCategories.Text = g_sql.Sql.LastInsertID(g_sql.CatTableName).ToString
					End If
					
				' Ok its already there so lets set the dropbox to that item
				Else
					dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf(name)
					
				End If
				
			End If
			
		' Ok we are going to edit the already present name
		Else
		
			' The words already there in case we wish to edit it
			Dim oldcategory As String = dropBoxCategory.SelectedItem.ToString
			
			' Now we get the new name
			Dim name As String = InputBox("Name of Category:", "Category Setting", oldcategory)
			
			' Check to see something was entered
			If name.Length > 0 Then
				
				' Check to see if the new name is already in the db
				Dim test As String = g_sql.Sql.SearchForItem("name", g_sql.CatTableName, String.Format("name=>{0}", name), True)
				
				' If its there we do not want to update things again
				If String.Compare(test, name) = 0  Then
					
					' So we will delete the one we were changing
					Dim index As Integer = CInt(g_sql.Sql.SearchForItem("id", g_sql.CatTableName, String.Format("name=>{0}", oldcategory), True))
				
					' Delete it from the sql
					g_sql.Sql.Delete(g_sql.CatTableName, String.Format("id={0}", index))
					
					' Fix all the items that were depending on this
					g_sql.Sql.ExecuteNonQuery(String.Format("UPDATE {0} SET categories='' WHERE categories='{1}'", g_sql.TableName, index))
				
					' Delete it from the dropbox
					dropBoxCategory.Items.RemoveAt(dropBoxCategory.Items.IndexOf(oldcategory))
					
					' Then set the index to the one we wanted to change it to
					dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf(name)
					
				Else
					
					' Fix it in the DB
					g_sql.Sql.Update(g_sql.CatTableName, String.Format("name={0}", name), String.Format("name={0}", oldcategory))
					
					' Now update the drop box
					Dim index As Integer = dropBoxCategory.Items.IndexOf(oldcategory)
					dropBoxCategory.Items.RemoveAt(index)
					dropBoxCategory.Items.Insert(index, name)
					dropBoxCategory.SelectedIndex = index
										
				End If
				
'			' An empty will delete this item from DB and list
'			Else
'				
'				' Find the index in the sql for this item
'				Dim index As Integer = CInt(g_sql.SqlSearchForItem("id", g_sql.CatTableName, String.Format("name=>{0}", oldcategory), True))
'				
'				' Delete it from the sql
'				g_sql.SqlDelete(g_sql.CatTableName, String.Format("id={0}", index))
'				
'				' Delete it from the dropbox
'				index = dropBoxCategory.Items.IndexOf(oldcategory)
'				dropBoxCategory.Items.RemoveAt(index)
'				dropBoxCategory.SelectedIndex = index
				
			End If
			
		End If
		
	End Sub
	
	Sub DropBoxCategorySelectedIndexChanged(sender As Object, e As EventArgs)
		
		' Check to see if we hit <new>
		If dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf("<new>") Or dropBoxCategory.SelectedIndex = -1 Then
			
			' Now that we have it we must set the category text box
			textBoxCategories.Text = ""
			
			' Get out
			Exit Sub
			
		End If
		
		' Ok not a new one so we want to associate this category with this db entry
		Dim category As String = dropBoxCategory.SelectedItem.ToString
		
		' Now find this one in the database
		Dim index As String = g_sql.Sql.SearchForItem("id", g_sql.CatTableName, String.Format("name=>{0}", category), True)
		
		' Now that we have it we must set the category text box
		textBoxCategories.Text = index
		
	End Sub
	
	Sub DropBoxCategoryMouseDoubleClick(sender As Object, e As MouseEventArgs)
		
		If e.Button = MouseButtons.Left Then
			
			Dim current As TimeSpan = DateTime.Now - lastClickTimer
			Dim dblClickSpan As TimeSpan = TimeSpan.FromMilliseconds(SystemInformation.DoubleClickTime)
			
			If current.TotalMilliseconds <= dblClickSpan.TotalMilliseconds Then
		
				' We want to load up all the notes and go through them to find the ones we want
				Dim frm As New DBUpdaterCategoriesForm()
				If frm.ShowDialog(Me) = DialogResult.OK Then
					dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf(frm.Result())
				End If
				
				frm.Dispose()
				
			End If
			
			lastClickTimer = DateTime.Now
			
		End If
		
	End Sub
	
	Sub ButtonNotesClick(sender As Object, e As EventArgs)
		
		' We want to load up all the notes and go through them to find the ones we want
		Dim frm As New DBUpdaterNotesForm(textBoxTransaction.Text)
		If frm.ShowDialog(Me) = DialogResult.OK Then
			richTextBoxNote.Text = frm.NoteResult()
			dropBoxCategory.SelectedIndex = dropBoxCategory.Items.IndexOf(frm.CategoryResult())
		End If
		
		frm.Dispose()
		
	End Sub
	
End Class
