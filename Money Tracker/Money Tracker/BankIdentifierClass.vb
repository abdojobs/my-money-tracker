'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 11/09/2012
' Time: 12:59 AM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Data
Imports System.Data.Linq
Imports System.Collections
Imports System.Collections.Specialized
Imports LumenWorks.Framework.IO.Csv

Public Class BankIdentifierClass
	
	' The standard class stuff
	Private m_stdColumns As StringCollection
	Private stdDateformat As String
	Private stdDatedelim As String
	Private m_bank As Integer
	Private m_ids() As BankClass
	
	Public Sub New()
		
		' Set the bank used to 0 to start
		m_bank = -1
		
		Try
			' Check to see if the resources file is there
			If Not FileIO.FileSystem.FileExists("banks.xml") Then
				
				' Need to create the file
				Me.WriteConfigBanks()
				
			End If
			
			' Count tracker for the number of already identified types of files
			Dim counter As Integer = 0
			
			' Now to load the unifier ( we are actually only loading the info in banks )
			Dim element As XElement = g_config.GetStandard
			
			' Load the standard stuff first
			m_stdColumns = New StringCollection
			m_stdColumns.AddRange(Split(element.<columns>.Value, ","))
			stdDateformat = element.<dateformat>.Value
			stdDatedelim = element.<datedelim>.Value
			
			' Fix the value column to be the out column
			m_stdColumns.Item(m_stdColumns.IndexOf("value")) = "out"
			
			Dim doc = XDocument.Load("banks.xml")
				
			' Create a query we can root through
			For Each result As XElement In doc.<banks>.<bank>
				
				' Redim to the new count
				ReDim Preserve m_ids(counter)
				
				' Now assign this id
				m_ids(counter) = New BankClass(result)
				
				' Increment
				counter += 1
				
		    Next
			
		Catch ex As Exception
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
	End Sub
	
	Public Function FindBank(filename As String) As Integer
		
		' Counter for tracking which bank it is
		Dim counter As Integer = -1
		Dim reset As Boolean = False
			
		Try
				
			' Text reader
			Dim reader As TextReader = New StreamReader(filename)
			
			' Read the file and get the rows
			Dim csvReader As New CsvReader(reader, False)
				
			' Read the next record
			csvReader.ReadNextRecord
			
			' Check to see we have some banks
			If m_ids IsNot Nothing Then
				
				' Now lets loop through the ids and find out if this file meets all the criteria
				For Each bank As BankClass In m_ids
					
					counter += 1
					
					If bank.Check(csvReader) Then
						
						m_bank = counter
						Exit For
						
					End If
					
				Next
				
			End If
			
			' We need to do an identification on this bank
			If m_bank = -1 Then
				
				Dim bankidform As New BankIdentifierForm(csvReader)
				If bankidform.ShowDialog() = DialogResult.OK Then
					
					' Create an xml entry based on this type we have identified
					Dim nm As String = bankidform.GetName()
					Dim cols As String = bankidform.GetCols()
					Dim dte As String = bankidform.GetDate()
					Dim delim As String = bankidform.GetDelim()
					
					' Load the doc and add the new information
					Dim doc = XDocument.Load("banks.xml")
					
					Dim element = _
						<bank>
							<name><%= nm %></name>
							<columns><%= cols %></columns>
							<dateformat><%= dte %></dateformat>
							<datedelim><%= delim %></datedelim>
							<identifier id="columns"><%= Split(cols, ",").Length %></identifier>
							<identifier id="dateformat"><%= Me.FindYear(dte) %></identifier>
							<identifier id="datedelim"><%= delim %></identifier>
						</bank>
					
					doc.Root.Add(element)
					doc.Save("banks.xml")
					
					reset = True
					
				Else
					Return 0
					
				End If
				
				bankidform = Nothing
				
			End If
			
			' Clear the reader
			csvReader = Nothing
			
			' Close the text reader
			reader.Close
			
			' Empty data
			reader = Nothing
			
		Catch ex As Exception
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return 0
			
		End Try
		
		If reset Then
			Return 2
		End If
		
		Return 1
		
	End Function
	
	Public ReadOnly Property CurrentBank() As BankClass
		Get
			Return m_ids(m_bank)
		End Get
	End Property
	
	Public ReadOnly Property StdColumns() As StringCollection
		Get
			Return m_stdColumns
		End Get
	End Property
	
	Private Function FindYear(str As String) As Integer
		
		Dim splits() As String = Split(str, ".")
		
		For i = 0 To splits.Length
			
			If splits(i) = "y" Then
				Return i + 1
			End If
			
		Next i
		
		Return Nothing
		
	End Function
	
	Private Sub WriteConfigBanks()
		
		Dim resource = _
			<banks>
			</banks>
		
		' Create the config.xml
		Try
			Dim sw = New StringWriter()
			Dim w = XmlWriter.Create(sw, New XmlWriterSettings())
			resource.Save(w)
			w.Close()
			
			'save to file
			resource.Save("banks.xml")
			
		Catch ex As Exception
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
	End Sub
	
End Class
