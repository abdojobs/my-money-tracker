'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 31/08/2012
' Time: 12:57 AM
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

Public Class XMLConfig
	
	Private doc As XDocument
	Private settings As XmlWriterSettings
	Private databases() As XElement
	Private standard As XElement
	
	' Actual Work
	Public Sub New
		
		settings = New XmlWriterSettings()
		settings.Indent = True
		
		Try
			' Check to see if the resources file is there
			If Not FileIO.FileSystem.FileExists("config.xml") Then
				
				' Need to create the file
				Me.WriteConfigXML()
				
			End If
			
			' Now to load the unifier ( we are actually only loading the info in banks )
			Dim doc As XDocument = XDocument.Load("config.xml")
			
			' Get the standard stuff
			standard = doc.Root.Element("standard")
			
			' Get the database stuff
			databases = doc.Root.Element("databases").Elements("database").ToArray
			
		Catch ex As Exception
			
			'Error trapping
			Debug.Write(ex.ToString())
			
		End Try
		
	End Sub
	
	Public Function GetStandard() As XElement
		
		Return standard
		
	End Function
	
	Public Function GetDatabases() As XElement()
		
		Return databases
		
	End Function
	
	Private Sub WriteConfigXML()
		
		Dim resource = _
			<configuration>
				<standard>
					<columns>dates,name,value</columns>
					<dateformat>y.m.d</dateformat>
					<datedelim>/</datedelim>
				</standard>
				<databases>
					<database>
						<name>Money</name>
						<columns>bank,dates,name,value,comment</columns>
						<create>CREATE TABLE Money (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,bank TEXT NULL,name TEXT NULL,value TEXT NULL,dates DATE NULL,comment TEXT NULL,categories TEXT NULL,subcategories TEXT NULL);</create>
					</database>
					<database>
						<name>Categories</name>
						<columns>categoryname</columns>
						<create>CREATE TABLE Categories (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,categoryname TEXT NULL);</create>
					</database>
					<database>
						<name>SubCategories</name>
						<columns>categoryname,owner</columns>
						<create>CREATE TABLE SubCategories (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,categoryname TEXT NULL,owner INTEGER NULL);</create>
					</database>
				</databases>
			</configuration>
		
		' Create the config.xml
		Try
			Dim sw = New StringWriter()
			Dim w = XmlWriter.Create(sw, settings)
			resource.Save(w)
			w.Close()
			
			'save to file
			resource.Save("config.xml")
			
		Catch ex As Exception
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
	End Sub
	
End Class
