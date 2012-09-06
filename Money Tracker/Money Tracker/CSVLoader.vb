'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 14/07/2012
' Time: 1:46 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System
Imports System.IO
Imports System.Xml
Imports System.Globalization
Imports LumenWorks.Framework.IO.Csv
Imports MyInterface.SQLite

Public Partial Class CSVLoader
	Inherits System.Windows.Forms.UserControl
	
	' Private variable for the Progress Loader Bar
	Private csvLoaderProgress As CSVLoaderProgress
	
	' Private variable for when I am changing the check boxes
	Private changingChecks As Boolean
	
	' Private variable containing the KeyPairValues
	Private banks As New List(Of Tuple(Of String, String, String, String))
	
	' Private variable to track what the temporary database uses
	Private tempdb As New Dictionary(Of String, String)
	
	' Private variable for where I got my last file
	Private curwd As String
	
	Public Sub New()
		
		' Private variables
		changingChecks = False
		curwd = Directory.GetCurrentDirectory()
		
		' Temp string array for the bank names to be loaded
		Dim tempbanknames As New List(Of String)
		
		' Load the resources XML
		Try
			
			' Check to see if the resources file is there
			If Not FileIO.FileSystem.FileExists("resources.xml") Then
				
				' Need to create the file
				Dim xmlwriter As New WriteXMLConfig
				
			End If
			
			Dim xmldoc As New XmlDocument
			Dim xmlnodelist As XmlNodeList
			xmldoc.Load("resources.xml")
			
			' Load up the XML with the banks info in it
			xmlnodelist = xmldoc.SelectNodes("//banks/bank")
			
			For Each xmlnode As XmlNode In xmlnodelist
				
				' Add the name to the temp array
				tempbanknames.Add(xmlnode.ChildNodes.Item(0).InnerText.Trim)
				
				' Add all information to the banks tuple
				banks.Add(Tuple.Create(xmlnode.ChildNodes.Item(0).InnerText.Trim, _
								xmlnode.ChildNodes.Item(1).InnerText.Trim, _
								xmlnode.ChildNodes.Item(2).InnerText.Trim, _
								xmlnode.ChildNodes.Item(3).InnerText.Trim))
				
			Next
			
			' Load up the XML with the temporary database info in it
			xmlnodelist = xmldoc.SelectNodes("//databases/database")
			
			For Each xmlnode As XmlNode In xmlnodelist
				
				tempdb.Add(xmlnode.ChildNodes.Item(0).InnerText.Trim, xmlnode.ChildNodes.Item(1).InnerText.Trim)
				
			Next
			
		Catch ex As Exception
			
			'Error trapping
			Debug.Write(ex.ToString())
			
		End Try
		
		' Initialize
		Me.InitializeComponent()
		
		' Fix for the loss of CSVLoaderProgress Bar
		Me.SuspendLayout
		Me.csvLoaderProgress = New CSVLoaderProgress()
		Me.csvLoaderProgress.Enabled = false
		Me.csvLoaderProgress.Location = New System.Drawing.Point(98, 30)
		Me.csvLoaderProgress.Name = "csvLoaderProgress"
		Me.csvLoaderProgress.Size = New System.Drawing.Size(211, 52)
		Me.csvLoaderProgress.TabIndex = 5
		Me.csvLoaderProgress.Visible = False
		Me.Controls.Add(Me.csvLoaderProgress)
		Me.ResumeLayout
		
		' Assign the bank names to the combobox
		For Each item As String In tempbanknames
			comboBoxBankType.Items.Add(item)
		Next
		
		' Set the combo box to first item
		comboBoxBankType.SelectedIndex = 1
		
	End Sub
	
	' Click on the ... button
	Sub ButtonSelectClick(sender As Object, e As EventArgs)
		
		' This will be a simple open the file dialog
		openFileDialog.FileName = ""
		openFileDialog.InitialDirectory = curwd
		openFileDialog.ShowDialog() 
		
	End Sub
	
	' Handles the openFileDialog
	Private Sub openFileDialog_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles openFileDialog.FileOk          
		
		If Not e.Cancel Then
			textBoxFileName.Text = openFileDialog.FileName.ToString()
			curwd = Path.GetDirectoryName(openFileDialog.FileName)
			buttonLoad.Enabled = True
		Else
			textBoxFileName.Text = ""
			curwd = Directory.GetCurrentDirectory()
			buttonLoad.Enabled = False
		End If
         
	End Sub
	
	' When I click this one I want to disable other one
	Sub CheckBoxDeleteCheckedChanged(sender As Object, e As EventArgs)
		
		If Not changingChecks Then
			changingChecks = True
			checkBoxMarked.Checked = False
		End If
		
		changingChecks = False
		
	End Sub
	
	' When I click this one I want to disable other one
	Sub CheckBoxMarkedCheckStateChanged(sender As Object, e As EventArgs)
		
		If Not changingChecks Then
			changingChecks = True
			checkBoxDelete.Checked = False
		End If
		
		changingChecks = False
		
	End Sub
	
	' We are going to load the file into the database
	Sub ButtonLoadClick(sender As Object, e As EventArgs)
		
		' Make the form disapear
		For Each cControl As System.Windows.Forms.Control In Me.Controls
			cControl.Visible = False
			cControl.Enabled = False
		Next
		
		' Now show the progress bar
		csvLoaderProgress.Visible = True
		csvLoaderProgress.Enabled = True
		
		' Call the loading portion of the control
		If performReading() Then
			
			' Check to see if we mark or delete this file
			If checkBoxMarked.Checked Then
				
				' Ok we want to load this and then add INPUT to the first line
				Dim reader As TextReader = New StreamReader(openFileDialog.FileName)
				Dim all As String = reader.ReadToEnd
				
				
				reader.Close
				reader = Nothing
				
				Dim writer As TextWriter = New StreamWriter(openFileDialog.FileName)
				writer.WriteLine("INPUT")
				writer.Write(all)
				writer.Close
				writer = Nothing
				
			Else If checkBoxDelete.Checked
				
				' Ok we want to delete this file
				File.Delete(openFileDialog.FileName)
				
			End If
			
		End If
		
		' Now make the main visable again		
		For Each cControl As System.Windows.Forms.Control In Me.Controls
			cControl.Visible = True
			cControl.Enabled = True
		Next
		
		' Wait for user to say ok then make progress invisable and disabled again
		csvLoaderProgress.Visible = False
		csvLoaderProgress.Enabled = False
		
		' Reset the filename box to not be able to edit
		textBoxFileName.Enabled = False
		
		' Now clear the text box
		textBoxFileName.Text = ""
		
		' Disable the Load button
		buttonLoad.Enabled = False
		
		' Check box the mark read
		checkBoxMarked.Checked = True
		
	End Sub
	
	Private Function performReading() As Boolean
		
		' Open the file to get the number of lines
		Dim reader As TextReader = New StreamReader(openFileDialog.FileName)
		
		' Check the VERY first line and see if this data is already there
		Dim first As String = reader.ReadLine.Trim.Replace(vbLf, "")
		
		If first = "INPUT" Then
			MessageBox.Show(String.Format("{0} has already been input into database!", Path.GetFileName(openFileDialog.FileName))) 
			Return False
		End If
		
		' Ok its loaded so lets set some stuff in the progress bar info
		csvLoaderProgress.progressBar.Minimum = 0
		csvLoaderProgress.progressBar.Maximum = Split(reader.ReadToEnd, vbLf).Length
		csvLoaderProgress.progressBar.Value = 0
		csvLoaderProgress.progressBar.Step = 1
		
		reader.Close
		reader = Nothing
		
		' This is the banking information that will be used to create the sql query
		Dim bank As String = Nothing
		Dim columns As String() = Nothing
		Dim columnstouse As String() = Nothing
		Dim dateformat As String = Nothing
		Dim datedelim As String = Nothing
		
		'****TODO, NEED TO FIGURE OUT HOW TO ELIMINATE ANY ENTRIES WHERE I RETURNED MY ITEMS
				
		' Find out which bank we are using and set it up
		For Each tuple As Tuple(Of String, String, String, String) In banks
			
			' Check to see if they match
			If comboBoxBankType.SelectedItem.ToString = tuple.Item1 Then
				
				' Assign the bank
				bank = tuple.Item1
				
				' Now setup the columns
				columns = tuple.Item2.Split(New Char() {","c})
				
				' Set the date format
				dateformat = tuple.Item3
				
				' Set the delimiter
				datedelim = tuple.Item4
				
				' Get out of the loop
				Exit For
				
			End If
			
		Next
		
		' Find out which columns we want in the temporary database
		For Each kvp As KeyValuePair(Of String, String) In tempdb
			
			' Check for the temp database
			If kvp.Key = g_sql.TableName Then
				
				' Now setup the columns
				columnstouse = kvp.Value.Split(New Char() {","c})
				
				' Get out of the loop
				Exit For
				
			End If
			
		Next
		
		' The string holder for the data to input into DB
		Dim data As String = String.Format("bank=>{0}|", bank)
		
		' Read the file and get the rows
		reader = New StreamReader(openFileDialog.FileName)
		Dim csvReader As New CsvReader(reader, False)
		
		' The field count
		Dim fieldCount As Integer = csvReader.FieldCount
		
		' Loop through and read all the records
		While csvReader.ReadNextRecord
			
			' Step the progress bar
			csvLoaderProgress.progressBar.PerformStep
						
			' Read the data and send it to the database Name=>Value|Name=>Value
			For x = 0 To fieldCount
				
				' Check to see if we are going to use this field
				For Each item As String In columnstouse
					
					If columns(x) = item Then
						
						Dim temp As String = csvReader(x).Trim
						
						' Check to see if the length is 0
						If temp.Length < 1 Then
							'Get outa here
							GoTo SkipLoop
						End If
						
						' Check to see if its an out and its in the negative, because this is a payment and don't want it
						If columns(x) = "out" And temp.Contains("-") Then
							'Get outa here
							GoTo SkipLoop
						End If
						
						' Check to see if this is a date
						If columns(x) = "date" And Not bank = "CIBC" Then
							
							temp = FixDate(temp, dateformat, datedelim)
							
						End If
						
						data += String.Format("{0}=>{1}|", columns(x), temp)
						
						'Get outa here
						Exit For
						
					End If
					
				Next
				
			Next
			
			' Clear the last | off the dataline
			data = data.Substring(0, data.Length-1)
			data.Trim
			
			' Clean up some of the stuff that will fuck up an sql
			data = CleanEscape(data)
			
			' Dump the data into the SQL
			If Not g_sql.Sql.Insert(g_sql.TableName, data) Then
				Throw New ApplicationException("Error With SQL")
				Return False
			End If
			
			SkipLoop:
			
			' Reset data
			data = String.Format("bank=>{0}|", bank)
			
		End While
		
		reader.Close
		reader = Nothing
		
		Return True
		
	End Function
	
	' Fixes the date to this YYYY-MM-DD
	Private Function FixDate(ByVal tofix As String, ByVal format As String, ByVal delim As String) As String
		
		Dim splitfix As String() = tofix.Split(delim.Chars(0))
		Dim splitformat As String() = format.Split(New Char() {"."c})
		
		Dim d As String = Nothing
		Dim m As String = Nothing
		Dim y As String = Nothing
		Dim count As Integer = -1
		
		For Each item As String In splitformat
			
			count += 1
			
			Select Case item
					
				Case "d"
					d = splitfix(count)
					
			    Case "m"
			    	m = splitfix(count)
			    	
			    Case "y"
			    	y = splitfix(count)
			    	
			End Select
			
		Next
		
		Return String.Format("{0}-{1}-{2}", y, m, d)
		
	End Function
	
	Private Function CleanEscape(ByVal str As String) As String
		
		Dim esc() As String = {"\", "~", "!", "{", "%", "}", "^", "'", "&", "(", ")", "`"}
		
		For Each ch As String In esc
			
			If str.Contains(ch) Then
				str = str.Replace(ch, "")
			End If
			
		Next
		
		Return str
		
	End Function
	
End Class