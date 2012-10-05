'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 12/09/2012
' Time: 7:05 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.IO
Imports System.Collections
Imports System.Collections.Specialized
Imports LumenWorks.Framework.IO.Csv

Public Class CSVUnificationClass
	
	' The variable to hold all the bank information we need
	Private bank As BankIdentifierClass
	
	Public Sub New()
		
		' Figure out what bank
		bank = New BankIdentifierClass
		
	End Sub
	
	Public Function CleanLines(name As String) As String
		
		Dim outfilename As String = Path.GetDirectoryName(name) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(name) + ".mtcsv"
		
		' Now that we have the banks and the unified data loaded lets check to see if this
		'	file contains and version of the already know types of banks
		Dim ret As Integer = bank.FindBank(name)
		
		Select Case ret
				
			Case 0
				
				' Failed
				Return Nothing 
				
			Case 2
				
				' Need to reload the banks
				bank = New BankIdentifierClass
				bank.FindBank(name)
				
		End Select
			
		
		Dim current As BankClass = bank.CurrentBank
		
		Try
			' Tracking counter
			Dim counter As Integer = -1
			
			' Text reader
			Dim reader As TextReader = New StreamReader(name)
			
			' Read the file and get the rows
			Dim csvReader As New CsvReader(reader, False)
			
			' Temp value for the writer
			Dim collect As New StringCollection
			Dim checks As New Collection
			
			While csvReader.ReadNextRecord
				
				Dim value As String = Nothing
				Dim data As String = Nothing
				Dim temp As String = Nothing
				
				' Look and see if this is an in(coming) or a negative in the out(going)
				' We have something in this column so we want to save it to the checks
				If csvReader(current.Column(current.PaymentColumn)).Trim.Length > 0 Then
					
					' Counter check
					counter += 1
					
					' Reset the temp
					temp = current.Name + ","
					
					For Each stdcol As String In bank.StdColumns
						
						value = csvReader(current.Column(stdcol)).Trim
						
						If stdcol = "dates" Then
							value = FixDate(value, current.DateFormat, current.DateDelim)
							
						Else If stdcol = "name" Then
							value = Chr(34) + CleanEscape(value) + Chr(34)
							
						Else If stdcol = "out" Then
							
							' Get the value for further evaluation
							value = csvReader(current.Column(current.PaymentColumn)).Trim
							data = Chr(34) + CleanEscape(csvReader(current.Column("name"))).Trim + Chr(34)
							
							' Check to see if the payment column is an "in"
							If current.PaymentColumn = "in" Then
								
								' Check to see if value has a - and remove it
								If Not value.Contains("-") Then
									value = value.Insert(0, "-")
								End If
								
							End If
							
						End If
						
						' Get the column we want
						temp += value  + ","
						
					Next
				
					' Clear out the last comma
					temp = temp.Substring(0, temp.Length - 1)
					
					' The name and the new value
					checks.Add(New Tuple(Of String, String, String)(data, value, temp), counter.ToString)
					
				Else
					
					' Reset the temp
					temp = current.Name + ","
					
					For Each stdcol As String In bank.StdColumns
						
						value = csvReader(current.Column(stdcol)).Trim
						
						If stdcol = "dates" Then
							value = FixDate(value, current.DateFormat, current.DateDelim)
							
						Else If stdcol = "name" Then
							value = Chr(34) + CleanEscape(value) + Chr(34)
							
						End If
						
						' Get the column we want
						temp += value  + ","
						
					Next
				
					' Clear out the last comma
					temp = temp.Substring(0, temp.Length - 1)
					
					' Now to write this one to collection
					collect.Add(temp)
					
				End If
				
			End While
			
			' Close the text reader
			reader.Close
			
			' Empty data
			reader = Nothing
			
			' Reset the counter
			counter = -1
			
			' Now we have them all in a collection we can see if there are any that are the same up to the name
			For Each tup As Tuple(Of String, String, String) In checks
				
				' Increment counter
				counter += 1
				
				Dim location As Integer = -1
				
				' Check for the substring
				location = ContainsSubstring(collect, tup.Item1)
				
				' Check to see if the key is in the collection
				If location >= 0 Then
					
					' Now check to see if there are multiples
					Dim temp As String = collect.Item(location)
					
					' Check to see if the values are the same
					Dim value As String = temp.Substring(temp.LastIndexOf(",") + 1)
					
					If (CDbl(value) + CDbl(tup.Item2) = 0) Then
						
						collect.RemoveAt(location)
						checks.Remove(counter.ToString)
						
					End If
					
				End If
				
			Next
			
			' Text writer
			Dim writer As TextWriter = New StreamWriter(outfilename)
			
			' Now we have the FINAL list so we need to write it out to the writer
			For Each str As String In collect
				
				writer.WriteLine(str)
				writer.Flush()
				
			Next
			
			For Each tup As Tuple(Of String, String, String) In checks
				
				writer.WriteLine(tup.Item3)
				writer.Flush()
				
		    Next
			
			' Clear the reader
			csvReader = Nothing
			
			' Close the text writer
			writer.Close
			
			' Empty data
			writer = Nothing
			
		Catch ex As Exception
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return outfilename

	End Function
	
	Private Function ContainsSubstring(ByVal strings As StringCollection, ByVal substring As String) As Integer
		
		Dim counter As Integer = -1
		
		For Each str As String In strings
			counter += 1
	    	If str.Contains(substring) Then 
	    		Return counter
	        End If
	    Next
	    
	    Return -1
	    
	End Function
	
	' Fixes the date to this YYYY-MM-DD
	Private Function FixDate(ByVal tofix As String, ByVal format As String, ByVal delim As String) As String
		
		Dim splitfix As String() = Split(tofix, delim)
		Dim splitformat As String() = Split(format, ".")
		
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
		
		str = Replace(str, "  ", " ")
		
		Return str
		
	End Function
	
End Class
