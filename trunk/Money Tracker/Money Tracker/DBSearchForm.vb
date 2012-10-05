'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 29/08/2012
' Time: 11:41 AM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports MyInterface.SQLite

Public Partial Class DBSearchForm
	
	' This is the tracker for the old dates
	Private oldStartDate As Date
	Private oldEndDate As Date
	
	Public Sub New()
		
		Me.InitializeComponent()
		
		' Fix the date time thing
		datePickerStart.Format = DateTimePickerFormat.Custom
		datePickerStart.CustomFormat = "yyyy-MM-dd" 
		datePickerEnd.Format = DateTimePickerFormat.Custom
		datePickerEnd.CustomFormat = "yyyy-MM-dd" 
		
		' Set the end date to today
		datePickerEnd.Value = Date.Today
		
		' Get the old dates
		oldStartDate = datePickerStart.Value
		oldEndDate = datePickerEnd.Value
		
		' Set the and or
		radioButtonAnd.Checked = True
		
		' Set the exact
		radioButtonExact.Checked = True
		
	End Sub
	
	Sub RadioButtonTypeClicked(sender As Object, e As EventArgs)
		
		If DirectCast(sender, RadioButton).Name = "radioButtonExact" Then
			radioButtonLike.Checked = False
		Else
			radioButtonExact.Checked = False
		End If
		
	End Sub
	
	Sub RadioButtonAndOrClicked(sender As Object, e As EventArgs)
		
		If DirectCast(sender, RadioButton).Name = "radioButtonAnd" Then
			radioButtonOr.Checked = False
		Else
			radioButtonAnd.Checked = False
		End If
		
	End Sub
	
	Sub ButtonClearClick(sender As Object, e As EventArgs)
		
		For Each item As Control In groupBox1.Controls
			
			' Find the text boxes and clear them
			If TypeOf item Is TextBox Then
				item.Text = ""
			
			' Find the date time pickers and reset them
			ElseIf TypeOf item Is DateTimePicker Then
				
				' Check to see if its start or end
				If item.Name = "datePickerStart" Then
					DirectCast(item, DateTimePicker).Value = New Date(2012, 1, 1, 0, 0, 0, 0)
				Else
					DirectCast(item, DateTimePicker).Value = Date.Today
				End If
				
			' Check for the check box item
			ElseIf TypeOf item Is CheckedListBox Then
				
				Dim lb As CheckedListBox = DirectCast(item, CheckedListBox)
				
				For i As Integer = 0 To lb.Items.Count - 1 Step 1
	                lb.SetItemChecked(i, False)
	            Next i
				
			End If
			
		Next
		
		' Reset some stuff
		' Get the old dates
		oldStartDate = datePickerStart.Value
		oldEndDate = datePickerEnd.Value
		
		' Set the and or
		radioButtonAnd.Checked = True
		
		' Set the exact
		radioButtonExact.Checked = True
		
	End Sub
	
	Sub DatePickerStartValueChanged(sender As Object, e As EventArgs)
		
		' Have to make sure the date is less than the end date
		Dim result As Integer = DateTime.Compare(datePickerStart.Value, datePickerEnd.Value)
				
		If result = 0 Then
			' Throw error saying same date
			datePickerStart.Value = oldStartDate
			MessageBox.Show("Start date can not be SAME as end date!", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		ElseIf result > 0
			' Throw error saying start > end
			datePickerStart.Value = oldStartDate
			MessageBox.Show("Start date can not be GREATER than end date!", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End If
		
	End Sub
	
	Sub DatePickerEndValueChanged(sender As Object, e As EventArgs)
		
		' Have to make sure the date is greater than the end date
		Dim result As Integer = DateTime.Compare(datePickerEnd.Value, datePickerStart.Value)
		
		If result < 0 Then
			' Throw error saying start < end
			datePickerEnd.Value = oldEndDate
			MessageBox.Show("Start date can not be LESS than end date!", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		ElseIf result = 0
			' Throw error saying same date
			datePickerEnd.Value = oldEndDate
			MessageBox.Show("Start date can not be SAME as end date!", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End If
		
	End Sub
	
	Sub TextBoxCategoryMouseDoubleClick(sender As Object, e As MouseEventArgs)
		
		' We want to load up all the notes and go through them to find the ones we want
		Dim frm As New DBUpdaterCategoriesForm()
		If frm.ShowDialog(Me) = DialogResult.OK Then
			textBoxCategory.Text = frm.Result()
		End If
		
		frm.Dispose()
		
	End Sub
	
	Sub ButtonSearchClick(sender As Object, e As EventArgs)
		
		' Clear the grid view
		dataGridView.DataSource = Nothing
		
		' This is the meat of this form
		' First we create the sql statement
		Dim sqlStatement As String = "SELECT {0} FROM {1} WHERE {2}"
		
		' Now we need to create the 2 parts for this statement
		' First we will get all the variables the individual wants to see
		Dim selectWhat As String = "name,value,"
		
		For Each var In checkedListBoxVars.CheckedItems
			
			selectWhat += var.ToString + ","
			
		Next
		
		' Get rid of last comma
		selectWhat = selectWhat.Substring(0, selectWhat.Length - 1).ToLower
		
		' Now we need the wheres
		Dim selectWhere As String = ""
		
		' Check the transaction box
		If textBoxTransaction.Text.Length > 0 Then
			selectWhere += "name" + TypeOfSearch(textBoxTransaction.Text)
		End If
		
		' Check the bank box
		If textBoxBank.Text.Length > 0 Then
			selectWhere += "bank" + TypeOfSearch(textBoxBank.Text)
		End If
		
		' Check the category box
		If textBoxCategory.Text.Length > 0 Then
			Dim id As String = g_sql.Sql.SearchForItem("id", g_sql.CatTableName, String.Format("categoryname=>{0}", textBoxCategory.Text), True)
			selectWhere += "categories" + TypeOfSearch(id)
		End If
		
		'Need To fix the ending AND OR
		If selectWhere.Contains("OR") Then
			selectWhere = selectWhere.Substring(0, selectWhere.Length - 3).ToLower
			selectWhere += "AND "
		End If
		
		Dim startdate As Date = New Date("dd/MM/YYYY")
		Dim enddate As Date = New Date("dd/MM/YYYY")
		
		If datePickerEnd.Enabled And labelDateEnd.Enabled Then
			
			' Need to figure out the start date of the month selected and the end date
			startdate = DateSerial(datePickerStart.Value.Year,datePickerStart.Value.Month, datePickerStart.Value.Day)
			enddate = DateSerial(datePickerEnd.Value.Year,datePickerEnd.Value.Month, datePickerEnd.Value.Day)
			
		Else
			
			' Need to figure out the start date of the month selected and the end date
			startdate = DateSerial(datePickerStart.Value.Year,datePickerStart.Value.Month, 1)
			enddate = DateSerial(datePickerStart.Value.Year,datePickerStart.Value.Month + 1, 0)
			
		End If
		
		Dim a = startdate.ToShortDateString
	
		' Set the format
		Dim first As String = String.Format("{0}/{1}/{2}", startdate.Day, startdate.Month, startdate.Year)
		Dim last As String = String.Format("{0}/{1}/{2}", enddate.Day, enddate.Month, enddate.Year)
		
		' Now we know for sure that we will ALWAYS have dates
		selectWhere += String.Format("dates BETWEEN '{0}' AND '{1}'", first, last)
		
		' Now we combine them
		sqlStatement = String.Format(sqlStatement, selectWhat, g_sql.TableName, selectWhere)
		
		' Now that is done we call the sql and bind the results to the tableDim bs As BindingSource
		Dim bs As New BindingSource
		g_sql.Sql.BindingDatabase(sqlStatement, bs)
		dataGridView.DataSource = bs
		
	End Sub
	
	Sub DataGridViewColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
		
		' Make sure its left mouse
		If e.Button = MouseButtons.Right Then
			
			' Ensure there is a column selected
			If e.ColumnIndex >= 0 Then
				
				' Check to make sure its the money column
				If dataGridView.Columns(e.ColumnIndex).Name = "value" Then
					
					dataGridView.ClearSelection()
			
					For Each row As DataGridViewRow In dataGridView.Rows
						row.Cells(e.ColumnIndex).Selected = True
					Next
					
					dataGridView.Select()
					
					' Tally up the cells
					Tally()
					
				End If
				
			End If
			
		End If
		
	End Sub
	
	Sub DataGridViewCellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
		
		' Make sure its left mouse
		If e.Button = MouseButtons.Left Then
			Tally()
		End If
		
	End Sub
	
	Sub ButtonMonthlyOrRangeClick(sender As Object, e As EventArgs)
		
		If buttonMonthlyOrRange.Text = "Date Range" Then
			
			' Change the button
			buttonMonthlyOrRange.Text = "Monthly"
			labelDateStart.Text = "Start"
			
			' Show the End stuff
			labelDateEnd.Visible = True
			labelDateEnd.Enabled = True
			datePickerEnd.Visible = True
			datePickerEnd.Enabled = True
			
		Else
			
			' Change the button
			buttonMonthlyOrRange.Text = "Date Range"
			labelDateStart.Text = "Month"
			
			' Hide the End stuff
			labelDateEnd.Visible = False
			labelDateEnd.Enabled = False
			datePickerEnd.Visible = False
			datePickerEnd.Enabled = False
			
		End If
		
	End Sub
	
	Private Function TypeOfSearch(ByVal str As String) As String
		
		if radioButtonLike.Checked Then
			Return String.Format(" LIKE '%{0}%' {1} ", str, GetAndOr())
		End If
		
		Return String.Format(" = '{0}' {1} ", str, GetAndOr())
		
	End Function
	
	Private Function GetAndOr() As String
		
		If radioButtonAnd.Checked Then
			Return "AND"
		End If
		
		Return "OR"
		
	End Function
	
	Private Sub Tally
		
		Dim total As Double = 0.0
		Dim id As Integer = dataGridView.Columns("value").Index
		
		For Each row As DataGridViewTextBoxCell In dataGridView.SelectedCells
			
			If id = row.OwningColumn.Index Then
				total += CDbl(row.Value)
			End If
			
		Next
		
		If total > 0.0 Then
			textBoxTotal.Text = CStr(total)
		End If
		
	End Sub
	
End Class
