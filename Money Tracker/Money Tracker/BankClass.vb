'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 11/09/2012
' Time: 1:06 AM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Data
Imports System.Data.Linq
Imports LumenWorks.Framework.IO.Csv

Public Class BankClass
		
	Private m_name As String
	Private m_columns As Dictionary(Of String, Integer)
	Private m_paymentcolumn As String
	Private m_dateformat As String
	Private m_datedelim As String
	Private m_identifiers As Dictionary(Of String, String)
	
	Public Sub New(ByRef element As XElement)
		
		m_name = element.<name>.Value
		
		' Deal with the columns
		Dim temp() As String = Split(element.<columns>.Value, ",")
		Dim counter As Integer = -1
		
		m_columns = New Dictionary(Of String, Integer)
		
		' Set the payment column
		m_paymentcolumn = "in"
		
		' Setup the date information
		m_dateformat = element.<dateformat>.Value
		m_datedelim = element.<datedelim>.Value
		
		For Each col As String In temp
			counter += 1
			
			' We need to figure out where the payment column is.  
			'	For most it will be a -0.00 $ in the out column
			'	some is a 0.00 in the in column
			If col = "out/in" Then
				
				' Set the payment column
				m_paymentcolumn = "out"
				
				' Reset the col variable
				col = "out"
				
			End If
			
			m_columns.Add(col, counter)
			
		Next
		
		' Get the dictionary pairs of how to identify a certain file
		m_identifiers = New Dictionary(Of String, String)
		
		For Each result As XElement In element.<identifier>
			m_identifiers.Add(result.@id, result.Value)
	    Next
		
	End Sub
	
	Public Function Check(ByVal record As CsvReader) As Boolean
		
		For Each pair As KeyValuePair(Of String, String) In m_identifiers
			
			Select Case pair.Key
					
				' Check the columns count
				Case "columns"
					
					' Ok we need to look at the column count
					If Not CInt(pair.Value) = record.FieldCount Then
						Return False
					End If
					
				' Check the date format
				Case "dateformat"
					
					' Split the date up so we can check each value
					Dim external() As String = Split(record(0), m_datedelim)
					
					' Do the check
					If Not CInt(external(CInt(pair.Value) - 1)) > 1900 Then
						Return False
					End If
					
				' Check the date delimiter
				Case "datedelim"
					
					' Ok lets see if the date carries the date delim
					If Not record(0).Contains(pair.Value) Or Not Split(record(0), pair.Value).Length = 3 Then
						Return False
					End If
					
			End Select
			
		Next
		
		Return True
		
	End Function
	
	Public ReadOnly Property Name() As String
		Get
			Return m_name
		End Get
	End Property
	
	Public ReadOnly Property OutgoingColumn() As Integer
		Get
			Return m_columns.Item("out")
		End Get
	End Property
	
	Public ReadOnly Property Column(x As String) As Integer
		Get
			Return m_columns.Item(x)
		End Get
	End Property
	
	Public ReadOnly Property PaymentColumn() As String
		Get
			Return m_paymentcolumn
		End Get
	End Property
	
	Public Property DateFormat() As String
		Get
			Return m_dateformat
		End Get
		Set
			m_dateformat = value
		End Set
	End Property
	
	Public ReadOnly Property DateDelim() As String
		Get
			Return m_datedelim
		End Get
	End Property
	
End Class
