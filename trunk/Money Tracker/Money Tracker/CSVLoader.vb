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
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Data
Imports System.Data.Linq
Imports System.Globalization
Imports System.Collections
Imports System.Collections.Specialized
Imports LumenWorks.Framework.IO.Csv
Imports MyInterface.SQLite

Public Partial Class CSVLoader
	Inherits System.Windows.Forms.UserControl
	
	' Private variable for the Progress Loader Bar
	Private loaderProgressBar As LoaderProgressBar
	
	' Private variable for when I am changing the check boxes
	Private changingChecks As Boolean
	
	' Private variable for where I got my last file
	Private curwd As String
	
	Public Sub New()
		
		' Private variables
		changingChecks = False
		curwd = Directory.GetCurrentDirectory()
		
		' Initialize
		Me.InitializeComponent()
		
		' Fix for the loss of LoaderProgressBar Bar
		Me.SuspendLayout
		Me.loaderProgressBar = New LoaderProgressBar()
		Me.loaderProgressBar.Enabled = false
		Me.loaderProgressBar.Location = New System.Drawing.Point(98, 30)
		Me.loaderProgressBar.Name = "loaderProgressBar"
		Me.loaderProgressBar.Size = New System.Drawing.Size(211, 52)
		Me.loaderProgressBar.TabIndex = 5
		Me.loaderProgressBar.Visible = False
		Me.Controls.Add(Me.loaderProgressBar)
		Me.ResumeLayout
		
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
		For Each cControl As Control In Me.Controls
			cControl.Visible = False
			cControl.Enabled = False
		Next
		
		' Now show the progress bar
		loaderProgressBar.Visible = True
		loaderProgressBar.Enabled = True
		
		' Old filename for checking
		Dim oldfilename As String = openFileDialog.FileName
		
		' Call the loading portion of the control
		If performReading() Then
			
			' Check to see if we mark or delete this file
			If checkBoxMarked.Checked Then
				
				Dim reader As TextReader = Nothing
				Dim writer As TextWriter = Nothing
				Dim all As String = Nothing
				
				' Ok we want to load this and then add INPUT to the first line
				reader = New StreamReader(oldfilename)
				all = reader.ReadToEnd
				
				reader.Close
				reader = Nothing
				
				writer = New StreamWriter(oldfilename)
				writer.WriteLine("INPUT")
				writer.Write(all)
				writer.Close
				writer = Nothing
				
			Else If checkBoxDelete.Checked
				
				' Ok we want to delete this file
				File.Delete(oldfilename)
				
			End If
			
		End If
		
		' Now make the main visable again		
		For Each cControl As System.Windows.Forms.Control In Me.Controls
			cControl.Visible = True
			cControl.Enabled = True
		Next
		
		' Wait for user to say ok then make progress invisable and disabled again
		loaderProgressBar.Visible = False
		loaderProgressBar.Enabled = False
		
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
		
		' Reader
		Dim reader As TextReader = New StreamReader(openFileDialog.FileName)
		
		' Check the VERY first line and see if this data is already there
		Dim first As String = reader.ReadLine.Trim.Replace(vbLf, "")
		
		If first = "INPUT" Then
			
			' If the reset is set then input anyway after removing the INPUT
			If checkBoxReset.Checked Then
				
				' Ok we want to load this and then add INPUT to the first line
				Dim all As String = reader.ReadToEnd
				'all = all.Remove(0, all.IndexOf(vbNewLine))
				
				reader.Close
				reader = Nothing
				
				Dim writer As New StreamWriter(openFileDialog.FileName)
				writer.Write(all)
				writer.Close
				writer = Nothing
				
			Else
				reader.Close
				reader = Nothing
				
				MessageBox.Show(String.Format("{0} has already been input into database!", Path.GetFileName(openFileDialog.FileName))) 
				Return False
				
			End If
			
		' Fix for reset and not reset
		Else
			
			' Close the reader so we can get first line again
			reader.Close
			reader = Nothing
			
		End If
		
		' Reopen
		reader = New StreamReader(openFileDialog.FileName)
			
		' Now that we have found the bank we have to clean the file
		' Now we know which bank it is lets sort the data and output it to a writer
		Dim unifier As New CSVUnificationClass()
		Dim nm As String = unifier.CleanLines(openFileDialog.FileName)
		
		If nm = Nothing Then
			MessageBox.Show("Error, nothing done!  Fill in the identification!", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error)
			Return False
		Else
			openFileDialog.FileName = nm
		End If
		
		' Ok its loaded so lets set some stuff in the progress bar info
		loaderProgressBar.progressBar.Minimum = 0
		loaderProgressBar.progressBar.Maximum = Split(reader.ReadToEnd, vbLf).Length
		loaderProgressBar.progressBar.Value = 0
		loaderProgressBar.progressBar.Step = 1
		
		reader.Close
		reader = Nothing
		
		' Counter
		Dim counter As Integer = 0
		
		' For the standard bank stuff
		Dim standard As New Dictionary(Of String, Integer)
		
		' Put bank at the begining
		standard.Add("bank", counter)
		
		' Now loop through and get the rest
		For Each item In Split(g_config.GetStandard.<columns>.Value, ",")
			counter += 1
			standard.Add(item, counter)
		Next
		
		' Read the file and get the rows
		reader = New StreamReader(openFileDialog.FileName)
		Dim csvReader As New CsvReader(reader, False)
		
		' The field count
		Dim fieldCount As Integer = csvReader.FieldCount
		
		' Loop through and read all the records
		While csvReader.ReadNextRecord
			
			' Step the progress bar
			loaderProgressBar.progressBar.PerformStep
			
			Dim data As String = Nothing
			counter = -1
			
			For Each pair As KeyValuePair(Of String, Integer) In standard
			
				data += String.Format("{0}=>{1}|", pair.Key, csvReader(pair.Value))
				
			Next
			
			' Clear the last | off the dataline
			data = data.Substring(0, data.Length-1)
			data.Trim
			
			' Dump the data into the SQL
			If Not g_sql.InsertData(data) Then
				Throw New ApplicationException("Error With SQL")
				Return False
			End If
			
		End While
		
		' Close the reader
		reader.Close
		reader = Nothing
		
		' Delete the temp file
		File.Delete(openFileDialog.FileName)
		
		Return True
		
	End Function
	
	
	
End Class

' **************************************************
'	todo
' **************************************************
' Fix curwd to use a global last directory opened