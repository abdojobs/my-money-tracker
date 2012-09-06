'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 31/08/2012
' Time: 12:57 AM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.Xml
	
Friend Class BankItem
	
	Private m_name As String
	Private m_columns As String
	Private m_dateformat As String
	Private m_datedelim As String
	
	Public Sub New(ByVal n As String, ByVal c As String, ByVal d As String, ByVal dd As String)
		m_name = n
		m_columns = c
		m_dateformat = d
		m_datedelim = dd
	End Sub
	
	Public ReadOnly Property Name() As String
		Get
			Return m_name
		End Get
	End Property
	
	Public ReadOnly Property Columns() As String
		Get
			Return m_columns
		End Get
	End Property
	
	Public ReadOnly Property Dateformat() As String
		Get
			Return m_dateformat
		End Get
	End Property
	
	Public ReadOnly Property Datedelim() As String
		Get
			Return m_datedelim
		End Get
	End Property
	
End Class

Friend Class DataBaseItem
	
	Private m_name As String
	Private m_columns As String
	
	Public Sub New(ByVal n As String, ByVal c As String)
		m_name = n
		m_columns = c
	End Sub
	
	Public ReadOnly Property Name() As String
		Get
			Return m_name
		End Get
	End Property
	
	Public ReadOnly Property Columns() As String
		Get
			Return m_columns
		End Get
	End Property
	
End Class

Public Class WriteXMLConfig
	
	' Actual Work
	Public Sub New
		
		Dim dbs(0) As DataBaseItem
		dbs(0) = New DataBaseItem("Money", "bank,date,name,out,comment")
		
		Dim banks(3) As BankItem
		banks(0) = New BankItem("TD","date,name,out,in,total","m.d.y","/")
		banks(1) = New BankItem("AMEX-P","date,name,out","d.m.y","/")
		banks(2) = New BankItem("AMEX-C","date,ref,out,name,ref2,fluff","m.d.y","/")
		banks(3) = New BankItem("CIBC","date,name,out,in","y.m.d","-")
		
		' Create XmlWriterSettings
		Dim settings As XmlWriterSettings = New XmlWriterSettings()
		settings.Indent = True
		
		' Create XmlWriter
		Using writer As XmlWriter = XmlWriter.Create("resources.xml", settings)
			
			' Begin writing
			writer.WriteStartDocument()
			
			' Root
			writer.WriteStartElement("configuration") 
			
			' Sub Root
			writer.WriteStartElement("banks") 
			
			' Loop over banks in array
			For Each bank As BankItem In banks
				writer.WriteStartElement("bank")
				writer.WriteElementString("name", bank.Name)
				writer.WriteElementString("columns", bank.Columns)
				writer.WriteElementString("dateformat", bank.Dateformat)
				writer.WriteElementString("datedelim", bank.Datedelim)
				writer.WriteEndElement()
			Next
			
			' End sub root
			writer.WriteEndElement()
			
			' Sub Root
			writer.WriteStartElement("databases") 
			
			' Loop over banks in array
			For Each db As DataBaseItem In dbs
				writer.WriteStartElement("database")
				writer.WriteElementString("name", db.Name)
				writer.WriteElementString("columns", db.Columns)
				writer.WriteEndElement()
			Next
			
			' End sub root
			writer.WriteEndElement()
			
			' End root
			writer.WriteEndElement()
			
			' End document
			writer.WriteEndDocument()
			
		End Using
		
	End Sub
	
End Class
