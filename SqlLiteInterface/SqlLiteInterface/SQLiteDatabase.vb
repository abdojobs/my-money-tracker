'
' Created by SharpDevelop.
' User: Cpl Jeff Walsh
' Date: 11/3/2011
' Time: 7:57 AM
'
' Completed for Mr. Roamer's PO 415
'
Imports System
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Data
Imports System.Data.Linq
Imports System.Data.SQLite
Imports System.Windows.Forms
Imports System.Drawing

Public Class SQLiteDatabase
	
	' Debug info
	Private bDoDebug As Boolean
	Private aDebugWheres() As String
	
	' The connection to the SQLite DB
	Private pConnection As SQLiteConnection
	
	' Old not sure if this setup is working...
	Private plDataGridView As System.Windows.Forms.DataGridView
	
	' Variable to track which da / ds
	Private piTracker As Integer
	
	' The global Data Adapter
	Private plDataAdapter() As SQLiteDataAdapter
	
	' Global dataset
	Private plDataSet() As DataSet
	
	''' <summary>
	'''     Single Param Constructor for specifying the DB file.
	''' </summary>
	''' <param name="inputFile">The File containing the DB</param>
	Public Sub New()
		
		' Initialize debug info
		Me.Initialize()
		
		pConnection = New SQLiteConnection
		
	End Sub
	
	''' <summary>
	'''     Single Param Constructor for specifying the DB file.
	''' </summary>
	''' <param name="inputFile">The File containing the DB.  Empty will get you an open file dialog, name will get that file created.</param>
	Public Sub New(ByVal inputFile As String, Optional create As Boolean = False)
		
		' Initialize debug info
		Me.Initialize()
		
		' I need to ask them for the filename
		If inputFile.Length < 1 Then
			inputFile = Me.OpenDatabaseFile()
			create = True
			
		End If
		
		If Not System.IO.File.Exists(inputFile) Then
			create = True
		End If
		
		pConnection = New SQLiteConnection
		Me.Source(inputFile)
		
		If create Then
			Me.ExecuteCreate()
		End If
		
		Me.Connect()
		
	End Sub

	''' <summary>
	'''     Single Param Constructor for specifying advanced connection options.
	''' </summary>
	''' <param name="connectionOpts">A Criteria Collection containing all desired options and their values</param>
	Public Sub New(ByVal connectionOpts As CriteriaCollection)	
		
		' Initialize debug info
		Me.Initialize()
		
		Dim str as String = ""
		
		For Each pair In connectionOpts
			
			str = str + String.Format("{0}={1}; ", pair.Name, pair.Value)
			
		Next

		str = str.Trim().Substring(0, str.Length - 1)
		
		pConnection = New SQLiteConnection
		pConnection.ConnectionString = str
		
	End Sub
	
	''' <summary>
	''' Sets the source
	''' </summary>
	''' <param name="inputFile">The source file</param>
	Public Sub Source(ByVal inputFile As String)
		
		' Write Debug Info
		WriteDebug("Source - " + inputFile)
		
		pConnection.ConnectionString = String.Format("Data Source={0}", inputFile)
		
	End Sub
	
	''' <summary>
	''' This will open a db
	''' </summary>
	''' <returns>If it is open or not</returns>
	Public Function Connect() As Boolean
		
		' Check to make sure the connection string is valid
		If String.IsNullOrEmpty(pConnection.ConnectionString) Then
			Throw New Exception("You must initialize the source before a connection")
		End If
		
		Try
			' Write Debug Info
			WriteDebug("Connect - " + pConnection.ConnectionString)
			
			pConnection.Open()
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in Connect!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return IsConnected()
		
	End Function
	
	''' <summary>
	''' Closes the DB
	''' </summary>
	Public Sub Disconnect()
		
		' Write Debug Info
		WriteDebug("Disconnect")
		
		pConnection.Close()
		pConnection.Dispose()
		
	End Sub
	
	''' <summary>
	''' Creates a new DB
	''' </summary>
	''' <returns>If it was or was not created</returns>
	Public Function ExecuteCreate() As Boolean
		
		' Write Debug Info
		WriteDebug("ExecuteCreate")
		
		If Me.IsConnected Then
				
			Me.Disconnect()
			Me.Connect()
			Me.Disconnect()
			Return True
			
		Else
			Me.Connect()
			Me.Disconnect()
			Return True
			
		End If
		
		Return False
		
	End Function

	''' <summary>
	'''     Allows the programmer to run a query against the Database.
	''' </summary>
	''' <param name="sql">The SQL to run</param>
	''' <returns>A DataTable containing the result set.</returns>
	Public Function GetDataTable(ByVal sql As String) As DataTable
		
		' Write Debug Info
		WriteDebug("GetDataTable - " + sql)
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Dim dt As DataTable = New DataTable()
		
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			mycommand.CommandText = sql
			
			Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
			dt.Load(reader)
			reader.Close()
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in GetDataTable!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return Nothing
			
		End Try
		
		Return dt
		
	End Function

	''' <summary>
	'''     Allows the programmer to run a query against the Database.
	''' </summary>
	''' <param name="table">The table to get complete</param>
	''' <returns>A DataTable containing the result set.</returns>
	Public Function GetFullDataTable(ByVal table As String) As DataTable
		
		' Write Debug Info
		WriteDebug("GetFullDataTable - " + table)
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Dim dt As DataTable = New DataTable()
		
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			mycommand.CommandText = String.Format("SELECT * FROM {0}", table)
			
			' Write Debug Info
			WriteDebug("GetFullDataTable - " + mycommand.CommandText)
			
			Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
			dt.Load(reader)
			reader.Close()
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in GetFullDataTable!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return Nothing
			
		End Try
		
		Return dt
		
	End Function

	''' <summary>
	'''     Allows the programmer to run a query against the Database.
	''' </summary>
	''' <param name="sql">The SQL to run</param>
	''' <returns>A DataRow containing the single line result set.</returns>
	Public Function GetDataRow(ByVal sql As String) As DataRow
		
		' Write Debug Info
		WriteDebug("GetDataRow - " + sql)
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Dim dt As DataTable = New DataTable()
		
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			mycommand.CommandText = sql
			
			Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
			dt.Load(reader)
			reader.Close()
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in GetDataRow!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return Nothing
			
		End Try
		
		If dt.Rows.Count > 0 Then 
			Return dt.Rows(0)
		End If
		
		Return Nothing
	
	End Function
        
	''' <summary>
	'''     Allows the programmer to interact with the database for purposes other than a query.
	''' </summary>
	''' <param name="sql">The SQL to be run.</param>
	''' <returns>An Integer containing the number of rows updated.</returns>
	Public Function ExecuteNonQuery(ByVal sql As String) As Integer
		
		' Write Debug Info
		WriteDebug("ExecuteNonQuery - " + sql)
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If

		Dim rowsUpdated As Integer = -1
		Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
		mycommand.CommandText = sql
		rowsUpdated = mycommand.ExecuteNonQuery()
		ExecuteNonQuery = rowsUpdated

	End Function

	''' <summary>
	'''     Allows the programmer to retrieve single items from the DB.
	''' </summary>
	''' <param name="sql">The query to run.</param>
	''' <returns>A string.</returns>
	Public Function ExecuteScalar(ByVal sql As String) As String
		
		' Write Debug Info
		WriteDebug("ExecuteScalar - " + sql)
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Dim value As Object = Nothing
		Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
		mycommand.CommandText = sql
		value = mycommand.ExecuteScalar()
		
		If Not IsNothing(value) Then
			Return value.ToString()
		End If
		
		Return Nothing

	End Function

	''' <summary>
	'''     Allows the programmer to easily update rows in the DB.
	''' </summary>
	''' <param name="tableName">The table to update.</param>
	''' <param name="valuestr">A string containing Column names and their new values in Criteria Format: Name=>Value|Name=>Value</param>
	''' <param name="wherestr">The where string for the update statement.</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Update(ByVal tableName As String, ByVal valuestr As String, ByVal wherestr As String) As Boolean
		
		Dim values As String = ""
		
		Dim data As CriteriaCollection = Me.CreateCriteriaList(valuestr)
		If data.Count >= 1 Then
			
			For Each pair In data
				values += String.Format(" {0} = '{1}',", pair.Name, pair.Value)
			Next

			values = values.Substring(0, values.Length - 1)

		End If
		
		Dim wheres As String = ""
		
		Dim where As CriteriaCollection = Me.CreateCriteriaList(wherestr)
		If where.Count >= 1 Then
			
			For Each pair In where
				wheres += String.Format(" {0} = '{1}',", pair.Name, pair.Value)
			Next

			wheres = wheres.Substring(0, wheres.Length - 1)

		End If
		
		Try
			Dim query = String.Format("UPDATE {0} SET {1} WHERE {2};", tableName, values, wheres)
			
			' Write Debug Info
			WriteDebug("Update1 Query: " + query)
			
			Me.ExecuteNonQuery(query)

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in Update1!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True

	End Function

	''' <summary>
	'''     Allows the programmer to easily update rows in the DB.
	''' </summary>
	''' <param name="tableName">The table to update.</param>
	''' <param name="value">A Criteria List containing column names and their values</param>
	''' <param name="where">A Criteria List containing where names and their values</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Update(ByVal tableName As String, ByVal value As CriteriaCollection, ByVal where As CriteriaCollection) As Boolean

		Dim values As String = ""
		
		If value.Count >= 1 Then
			
			For Each pair In value
				values += String.Format(" {0} = '{1}',", pair.Name, pair.Value)
			Next

			values = values.Substring(0, values.Length - 1)

		End If
		
		Dim wheres As String = ""
		
		If where.Count >= 1 Then
			
			For Each pair In where
				wheres += String.Format(" {0} = '{1}',", pair.Name, pair.Value)
			Next

			wheres = wheres.Substring(0, wheres.Length - 1)

		End If
		
		Try			
			Dim query = String.Format("UPDATE {0} SET {1} WHERE {2};", tableName, values, wheres)
			
			' Write Debug Info
			WriteDebug("Update2 Query: " + query)
			
			Me.ExecuteNonQuery(query)

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in Update2!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True

	End Function

	''' <summary>
	'''     Allows the programmer to easily update/install a rows in the DB.
	''' </summary>
	''' <param name="tableName">The table to update.</param>
	''' <param name="dstr">A string containing Column names and their new values in Criteria Format: Name=>Value|Name=>Value</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Replace(ByVal tableName As String, ByVal dstr As String) As Boolean

		Dim whats As String = ""
		Dim withs As String = ""
		
		Dim data As CriteriaCollection = Me.CreateCriteriaList(dstr)
		If data.Count >= 1 Then
			
			For Each pair In data
				whats += String.Format("{0},", pair.Name)
				withs += String.Format("'{0}',", pair.Value)
			Next

			whats = whats.Substring(0, whats.Length - 1)
			withs = withs.Substring(0, withs.Length - 1)

		End If
		
		Try
			Dim query = String.Format("REPLACE INTO {0} ({1}) VALUES ({2});", tableName, whats, withs)
			
			' Write Debug Info
			WriteDebug("Replace1 Query: " + query)
			
			Me.ExecuteNonQuery(query)

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in Replace1!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True

	End Function

	''' <summary>
	'''     Allows the programmer to easily update/install a rows in the DB.
	''' </summary>
	''' <param name="tableName">The table to update.</param>
	''' <param name="data">A Criteria List containing Column names and their new values</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Replace(ByVal tableName As String, ByVal data As CriteriaCollection) As Boolean

		Dim whats As String = ""
		Dim withs As String = ""
		
		If data.Count >= 1 Then
			
			For Each pair In data
				whats += String.Format("{0},", pair.Name)
				withs += String.Format("'{0}',", pair.Value)
			Next

			whats = whats.Substring(0, whats.Length - 1)
			withs = withs.Substring(0, withs.Length - 1)

		End If
		
		Try
			Dim query = String.Format("REPLACE INTO {0} ({1}) VALUES ({2});", tableName, whats, withs)
			
			' Write Debug Info
			WriteDebug("Replace2 Query: " + query)
			
			Me.ExecuteNonQuery(query)

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in Replace2!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True

	End Function

	''' <summary>
	'''     Allows the programmer to easily delete rows from the DB.
	''' </summary>
	''' <param name="tableName">The table from which to delete.</param>
	''' <param name="where">The where clause for the delete.</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Delete(ByVal tableName As String, ByVal where As String) As Boolean
	
		Try
			Dim query = String.Format("DELETE FROM {0} WHERE {1};", tableName, where)
			
			' Write Debug Info
			WriteDebug("Delete Query: " + query)
			
			Me.ExecuteNonQuery(query)
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in Delete!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True
		
	End Function

	''' <summary>
	'''     Allows the programmer to easily insert into the DB
	''' </summary>
	''' <param name="tableName">The table into which we insert the data.</param>
	''' <param name="data">A Criteria Collection containing the column names and data for the insert.</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Insert(ByVal tableName As String, ByVal data As CriteriaCollection) As Boolean
		
		Dim columns As String = ""
		Dim values As String = ""
		
		For Each pair In data
			
			columns = columns + String.Format(" {0},", pair.Name)
			values = values + String.Format(" '{0}',", pair.Value)
			
		Next
		
		columns = columns.Substring(0, columns.Length - 1)
		values = values.Substring(0, values.Length - 1)
	
		Try
			Dim query = String.Format("INSERT INTO {0}({1}) VALUES({2});", tableName, columns, values)
			
			' Write Debug Info
			WriteDebug("Insert1 Query: " + query)
			
			Me.ExecuteNonQuery(query)
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in Insert1!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
	
		End Try
		
		Return True
		
	End Function

	''' <summary>
	'''     Allows the programmer to easily insert into the DB
	''' </summary>
	''' <param name="tableName">The table into which we insert the data.</param>
	''' <param name="str">A String containing the column names and data for the insert in Criteria Collection</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Insert(ByVal tableName As String, ByVal str As String) As Boolean
		
		Dim columns As String = ""
		Dim values As String = ""
		Dim data As CriteriaCollection = Me.CreateCriteriaList(str)
		
		For Each pair In data
			
			columns = columns + String.Format(" {0},", pair.Name)
			values = values + String.Format(" '{0}',", pair.Value)
			
		Next
		
		columns = columns.Substring(0, columns.Length - 1)
		values = values.Substring(0, values.Length - 1)
	
		Try
			Dim query = String.Format("INSERT INTO {0}({1}) VALUES({2});", tableName, columns, values)
			
			' Write Debug Info
			WriteDebug("Insert2 Query: " + query)
			
			Me.ExecuteNonQuery(query)
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in Insert2!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
	
		End Try
		
		Return True
		
	End Function
	
	''' <summary>
	'''     Allows the programmer to easily search the DB.
	''' </summary>
	''' <param name="tableName">The table to search</param>
	''' <param name="searchcriteria">A Criteria Collection containing Value and then Column Names (Reversed from Normal)</param>
	''' <param name="where">The where group of variables</param>
	''' <returns>The item searched for</returns>
	Public Function SearchForItem(ByVal getwhat As String, ByVal tableName As String, ByVal searchcriteria As CriteriaCollection, Optional exact As Boolean = True, Optional andor As String = "OR") As String
		
		Dim query As String = ""
		
		If searchcriteria.Count >= 1 Then
			
			For Each pair In searchcriteria
				If exact Then
					query += String.Format(" {0} = '{1}' {2}", pair.Name, pair.Value, andor)
				Else
					query += String.Format(" {0} LIKE '%{1}%' {2}", pair.Name, pair.Value, andor)
				End If
			Next

			query = query.Substring(0, query.Length - 3)

		End If
		
		Try
			' Write Debug Info
			WriteDebug("SearchForItem1")
			
			Return Me.ExecuteScalar(String.Format("SELECT {0} FROM {1} WHERE {2} LIMIT 1;", getwhat, tableName, query))

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SearchForItem1!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return Nothing

	End Function
	
	''' <summary>
	'''     Allows the programmer to easily search the DB.
	''' </summary>
	''' <param name="tableName">The table to search</param>
	''' <param name="searchcriteria">A String containing Value and then Column Names in Criteria Collection Format</param>
	''' <param name="where">The where group of variables</param>
	''' <returns>The item searched for</returns>
	Public Function SearchForItem(ByVal getwhat As String, ByVal tableName As String, ByVal searchcriteria As String, Optional exact As Boolean = True, Optional andor As String = "OR") As String
		
		Dim query As String = ""
		Dim data As CriteriaCollection = Me.CreateCriteriaList(searchcriteria)
		
		If data.Count >= 1 Then
			
			For Each pair In data
				If exact Then
					query += String.Format(" {0} = '{1}' {2}", pair.Name, pair.Value, andor)
				Else
					query += String.Format(" {0} LIKE '%{1}%' {2}", pair.Name, pair.Value, andor)
				End If
			Next

			query = query.Substring(0, query.Length - 3)

		End If
		
		Try
			' Write Debug Info
			WriteDebug("SearchForItem2")
			
			Return Me.ExecuteScalar(String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, query))

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SearchForItem2!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return Nothing

	End Function
	
	''' <summary>
	'''     Allows the programmer to easily search the DB.
	''' </summary>
	''' <param name="tableName">The table to search for a specific row</param>
	''' <param name="getwhat">The items to find</param>
	''' <param name="searchcriteria">A Criteria Collection containing Value and then Column Names</param>
	''' <returns>A datarow containing the data</returns>
	Public Function SearchForRow( ByVal getwhat As String, ByVal tableName As String,ByVal searchcriteria As CriteriaCollection, Optional exact As Boolean = True, Optional andor As String = "OR") As DataRow
		
		Dim wheres As String = ""
		
		If searchcriteria.Count >= 1 Then
			
			For Each pair In searchcriteria
				If exact Then
					wheres += String.Format(" {0} = '{1}' {2}", pair.Name, pair.Value, andor)
				Else
					wheres += String.Format(" {0} LIKE '%{1}%' {2}", pair.Name, pair.Value, andor)
				End If
			Next

			wheres = wheres.Substring(0, wheres.Length - 3)

		End If
		
		Try
			Dim query = String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, wheres)
			
			' Write Debug Info
			WriteDebug("SearchForRow1 Query: " + query)
			
			Return Me.GetDataRow(query)

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SearchForRow1!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return Nothing

	End Function
	
	''' <summary>
	'''     Allows the programmer to easily search the DB.
	''' </summary>
	''' <param name="tableName">The table to search for a specific row</param>
	''' <param name="getwhat">The items to find</param>
	''' <param name="str">A string in the Criteria Collection format</param>
	''' <returns>A datarow containing the data</returns>
	Public Function SearchForRow(ByVal getwhat As String, ByVal tableName As String, ByVal searchcriteria As String, Optional exact As Boolean = True, Optional andor As String = "OR") As DataRow
		
		Dim wheres As String = ""
		Dim data As CriteriaCollection = Me.CreateCriteriaList(searchcriteria)
		
		If data.Count >= 1 Then
			
			For Each pair In data
				If exact Then
					wheres += String.Format(" {0} = '{1}' {2}", pair.Name, pair.Value, andor)
				Else
					wheres += String.Format(" {0} LIKE '%{1}%' {2}", pair.Name, pair.Value, andor)
				End If
			Next

			wheres = wheres.Substring(0, wheres.Length - 3)

		End If
		
		Try
			Dim query = String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, wheres)
			
			' Write Debug Info
			WriteDebug("SearchForRow2 Query: " + query)
			
			Return Me.GetDataRow(query)

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SearchForRow2!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return Nothing

	End Function
	
	''' <summary>
	'''     Allows the programmer to easily search the DB.
	''' </summary>
	''' <param name="tableName">The table to search</param>
	''' <param name="searchcriteria">A Criteria Collection containing Value and then Column Names (Reversed from Normal)</param>
	''' <param name="where">The where group of variables</param>
	''' <returns>The datatable searched for</returns>
	Public Function SearchForTable(ByVal getwhat As String, ByVal tableName As String, ByVal searchcriteria As CriteriaCollection, Optional exact As Boolean = True, Optional andor As String = "OR") As DataTable
		
		Dim query As String = ""
		
		If searchcriteria.Count >= 1 Then
			
			For Each pair In searchcriteria
				If exact Then
					query += String.Format(" {0} = '{1}' {2}", pair.Name, pair.Value, andor)
				Else
					query += String.Format(" {0} LIKE '%{1}%' {2}", pair.Name, pair.Value, andor)
				End If
			Next

			query = query.Substring(0, query.Length - 3)

		End If
		
		Try
			Dim temp = String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, query)
			
			' Write Debug Info
			WriteDebug("SearchForTable1 Query: " + temp)
			
			Return Me.GetDataTable(temp)

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SearchForTable1!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return Nothing

	End Function
	
	''' <summary>
	'''     Allows the programmer to easily search the DB.
	''' </summary>
	''' <param name="tableName">The table to search</param>
	''' <param name="searchcriteria">A String containing Value and then Column Names in Criteria Collection fromat</param>
	''' <param name="where">The where group of variables</param>
	''' <returns>The datatable searched for</returns>
	Public Function SearchForTable(ByVal getwhat As String, ByVal tableName As String, ByVal searchcriteria As String, Optional exact As Boolean = True, Optional andor As String = "OR") As DataTable
		
		Dim query As String = ""
		Dim data As CriteriaCollection = Me.CreateCriteriaList(searchcriteria)
		
		If data.Count >= 1 Then
			
			For Each pair In data
				If exact Then
					query += String.Format(" {0} = '{1}' {2}", pair.Name, pair.Value, andor)
				Else
					query += String.Format(" {0} LIKE '%{1}%' {2}", pair.Name, pair.Value, andor)
				End If
			Next

			query = query.Substring(0, query.Length - 3)

		End If
		
		Try
			Dim temp = String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, query)
			
			' Write Debug Info
			WriteDebug("SearchForTable2 Query: " + temp)
			
			Return Me.GetDataTable(temp)

		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SearchForTable2!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return Nothing

	End Function
	
	''' <summary>
	''' Returns the last id from the last insert
	''' </summary>
	''' <param name="table">The table to check</param>
	''' <returns>The id of the last insert</returns>
	Public Function LastInsertID(ByVal table As String) As Integer
		
		return CInt(Me.ExecuteScalar(String.Format("SELECT last_insert_rowid() FROM {0}", table)))
		
	End Function
	
	''' <summary>
	''' Gets the rows in a given table
	''' </summary>
	''' <param name="table">The table to query</param>
	''' <returns>The count of rows</returns>
	Public Function RowCount(ByVal table As String) As Integer
		
		Return CInt(Me.ExecuteScalar(String.Format("SELECT count(*) FROM {0}", table)))
		
	End Function
	
	''' <summary>
	''' Creates a Criteria Collection that can be used in with the DB
	''' </summary>
	''' <param name="str">Must be in the following form: Name=>Value|Name=>Value</param>
	''' <returns>The Criteria Collection to use</returns>
	Public Function CreateCriteriaList(ByVal str As String) As CriteriaCollection
		
		' The 'array' should look like this:
		'	name=>value|name=>value
		Dim criters As CriteriaCollection = New CriteriaCollection()
		Dim splits() As String = str.Split(New Char() {"|"c}, StringSplitOptions.RemoveEmptyEntries)
		
		' At this point splits should have
		'	name=>value types
		For Each item In splits
			
			' get rid of the > to leave only the =
			item = item.Replace(">", "")
			
			' Now we can split the pairs to add to the Criteria Collection
			Dim pair() As String = item.Split("=>")'(New Char() {"="c})
			
			criters.Add(New Criteria(pair(0).TrimStart(" ").TrimEnd(" "), pair(1).TrimStart(" ").TrimEnd(" ")))
			
		Next
		
		Return criters
		
	End Function

	''' <summary>
	'''     Allows the programmer to easily delete all data from the DB.
	''' </summary>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function ClearDB() As Boolean
			
		Dim tables as DataTable
		
		Try
			tables = Me.GetDataTable("SELECT NAME FROM SQLITE_MASTER WHERE TYPE='table' ORDER BY NAME;")
			
			For Each table as DataRow In tables.Rows
				Me.ClearTable(table("NAME").ToString())
			Next
	
			Return True
		
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in ClearDB!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return False
		
	End Function

	''' <summary>
	'''     Allows the user to easily clear all data from a specific table.
	''' </summary>
	''' <param name="table">The name of the table to clear.</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function ClearTable(ByVal table as String) As Boolean
		
		' Write Debug Info
		WriteDebug("ClearTable: " + table)
		
		Try
			Me.ExecuteNonQuery(String.Format("delete from {0};", table))
			Return True
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in ClearTable!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return False
	
	End Function

	''' <summary>
	''' Allows the programmer to bind a Database to a Something
	''' </summary>
	''' <param name="sql">The SQL command to run</param>
	''' <param name="bs">The binding source to bind to</param>
	''' <returns>A True or Flase depending on success.</returns>
	Public Function BindingDatabase(ByVal sql as String, ByRef bs As BindingSource, Optional tracker As Integer = -1) As Integer
		
		' Write Debug Info
		WriteDebug("BindingDatabase: " + sql)
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Dim old As Integer
		
		If tracker = -1 Then
			
			If piTracker < 0 Then
				piTracker = 0
			End If
			
			piTracker += 1
			old = piTracker
			ReDim Preserve plDataAdapter(piTracker)
			ReDim Preserve plDataSet(piTracker)
			
		Else
			old = piTracker
			piTracker = tracker
		
		End If
		
		Try
			plDataAdapter(piTracker) = New SQLiteDataAdapter(sql, pConnection)
			plDataSet(piTracker) = New DataSet()
			plDataAdapter(piTracker).Fill(plDataSet(piTracker))
			
			If bs Is Nothing Then
				bs = New BindingSource()
			End If
			
			bs.DataSource = plDataSet(piTracker).Tables(0)
			
			Return piTracker
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in BindingDatabase!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
			' Reset backwards
			If Not piTracker = old Then
				piTracker = old
			End If
			
			plDataAdapter(piTracker) = Nothing
			plDataSet(piTracker) = Nothing
			piTracker -= 1
			ReDim Preserve plDataAdapter(piTracker)
			ReDim Preserve plDataSet(piTracker)
			
		End Try
		
		Return -1
		
	End Function

	''' <summary>
	''' Setup an insert command for a bound table
	''' </summary>
	''' <param name="sql"></param>
	''' <returns></returns>
	Public Function SetupBoundInsert(ByVal tracker As Integer, ByVal sql As String, ByRef param() As SQLiteParameter) As Boolean
		
		Try
			' Write Debug Info
			WriteDebug("SetupBoundInsert: " + sql)
			
			plDataAdapter(tracker).InsertCommand = New SQLiteCommand(sql, pConnection)
			
			For Each item As SQLiteParameter In param
				
				' Check to make sure that item is not null
				If item IsNot Nothing Then
					
					' Command
					plDataAdapter(tracker).InsertCommand.Parameters.Add(item)
					
					' Write Debug Info
					WriteDebug(String.Format("     Name: {0} Type: {1}", item.ParameterName, item.DbType.ToString))
					
				End If
			Next
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SetupBoundInsert!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True
	
	End Function

	''' <summary>
	''' Setup an update command for a bound table
	''' </summary>
	''' <param name="sql"></param>
	''' <returns></returns>
	Public Function SetupBoundUpdate(ByVal tracker As Integer, ByVal sql As String, ByVal param() As SQLiteParameter) As Boolean
		
		Try
			' Write Debug Info
			WriteDebug("SetupBoundUpdate: " + sql)
			
			plDataAdapter(tracker).UpdateCommand = New SQLiteCommand(sql, pConnection)
			
			For Each item As SQLiteParameter In param
				
				' Check to make sure that item is not null
				If item IsNot Nothing Then
					
					' Command
					plDataAdapter(tracker).UpdateCommand.Parameters.Add(item)
					
					' Write Debug Info
					WriteDebug(String.Format("     Name: {0} Type: {1}", item.ParameterName, item.DbType.ToString))
					
				End If
			Next
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SetupBoundUpdate!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True
	
	End Function

	''' <summary>
	''' Setup a delete command for a bound table
	''' </summary>
	''' <param name="sql"></param>
	''' <returns></returns>
	Public Function SetupBoundDelete(ByVal tracker As Integer, ByVal sql As String, ByVal param() As SQLiteParameter) As Boolean
		
		Try
			' Write Debug Info
			WriteDebug("SetupBoundUpdate: " + sql)
				
			plDataAdapter(tracker).DeleteCommand = New SQLiteCommand(sql, pConnection)
			
			For Each item As SQLiteParameter In param
				
				' Check to make sure that item is not null
				If item IsNot Nothing Then
					
					' Command
					plDataAdapter(tracker).DeleteCommand.Parameters.Add(item)
					
					' Write Debug Info
					WriteDebug(String.Format("     Name: {0} Type: {1}", item.ParameterName, item.DbType.ToString))
					
				End If
			Next
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in SetupBoundDelete!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True
	
	End Function
	
	''' <summary>
	''' Allows the programmer to send a table to the Database.
	''' </summary>
	''' <returns>Good or Bad</returns>
	Public Function BoundTableCallUpdate(ByVal tracker As Integer) As Boolean
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Try
			' Write Debug Info
			WriteDebug("BoundTableCallUpdate")
			
			plDataAdapter(tracker).Update(plDataSet(tracker))
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in BoundTableCallUpdate!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
			
		End Try
		
		Return True
	
	End Function
	
	''' <summary>
	''' This will get a filename from a path
	''' </summary>
	''' <param name="path">The path eg: c:\temp\file.dat</param>
	''' <returns>String representing the filename</returns>
	Private Function FileNameFromPath(ByVal path As String) As String
		
		Return Microsoft.VisualBasic.Strings.Right(path, path.Length - (path.LastIndexOf("\", path.Length) + 1))
		
	End Function
	
	''' <summary>
	''' This will get a filename from a path
	''' </summary>
	''' <param name="path">The path eg: c:\temp\file.dat</param>
	''' <returns>String representing the filename</returns>
	Private Function FileNameWithoutExt(ByVal path As String) As String
		
		path = FileNameFromPath(path)
		Return Microsoft.VisualBasic.Strings.Left(path, path.Length - (path.LastIndexOf(".", path.Length) - 1))
		
	End Function
	
	''' <summary>
	''' This will get a path without the file name at the end
	''' </summary>
	''' <param name="path">The path eg: c:\temp\file.dat</param>
	''' <returns>String Array of 2 String(0) Filename, String(1) Truncated Path</returns>
	Private Function PathWithoutFileName(ByVal path As String) As String
		
		Return path.Substring(0, path.LastIndexOf("\"))
		
	End Function
	
	''' <summary>
	''' This function will fix the canadian date system to the fucked up yank type
	''' </summary>
	''' <param name="dte">The date in Canadian MM/DD/YYY</param>
	''' <returns>The date fixed to Yank DD/MM/YYYY</returns>
	Public Function FixDate(ByVal dte As String) As Date
		
		' DD/MM/YYY
		' 0: MM
		' 1: DD
		' 2: YYYY
		Dim broken() As String = dte.Split(CChar("/"))
		FixDate = New DateTime(CInt(broken(2)), CInt(broken(0)), CInt(broken(1)))
		
	End Function	
	
	''' <summary>
	''' This will insert a file into the database
	''' </summary>
	''' <param name="imageFileName">The file name</param>
	''' <returns>If it was done or not</returns>
	Public Function InsertFile(ByRef table As String, ByRef column As String, ByVal id as Integer, ByRef str As String) As Boolean
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			
			With mycommand
				
				.CommandText = String.Format("INSERT INTO {0} ({1}) VALUES(@string)", table, column)
				.Parameters.Add(StringToBlob("@string", str))
				.ExecuteNonQuery()
				.Dispose()
				
			End With
			
			Return True
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in InsertFile!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return False
		
	End Function
	
	''' <summary>
	''' This will read a file from the database
	''' </summary>
	''' <param name="imageFileName">The file name</param>
	''' <param name="id">The id of the file</param>
	''' <returns>If it was done or not</returns>
	Public Function ReadFile(ByRef table As String, ByRef column As String, ByVal id as Integer, ByRef filename As String) As String
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
			
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			mycommand.CommandText = String.Format("SELECT {0} FROM {1} WHERE id = '{2}';", column, table, CStr(id))
			Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
			
			While reader.Read()
				
				ReadFile = BlobToString(CType(reader(column), Byte()))
				
			End While
			
			mycommand.Dispose()
			reader.Dispose()
			
			Return ReadFile
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in ReadFile!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return Nothing
		
	End Function
	
	''' <summary>
	''' This will convert a blob to an actual file
	''' </summary>
	''' <param name="blob">The blob to convert to an image</param>
	''' <returns>An image</returns>
	Private Function BlobToString(ByVal blob() As Byte) as String
		
        Dim pData() As Byte = DirectCast(blob, Byte())
        BlobToString = System.Text.Encoding.UTF8.GetString(pData)
        
    End Function

	''' <summary>
	''' This will convert a file to a blob
	''' </summary>
	''' <param name="id">The id string of the file</param>
	''' <param name="filePath">Where is the file</param>
	''' <returns>An sql parameter to input into the DB</returns>
	Private Function StringToBlob(ByVal idstr As String, ByVal str As String) As SQLiteParameter
		
		Dim insertBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(str)
        StringToBlob = New SQLiteParameter(idstr, insertBytes)
        StringToBlob.DbType = DbType.Binary
        StringToBlob.Value = insertBytes
        
    End Function
	
	''' <summary>
	''' This will insert an image into the database
	''' </summary>
	''' <param name="imageFileName">The image file name</param>
	''' <returns>If it was done or not</returns>
	Public Function InsertImage(ByRef table As String, ByRef column As String, ByRef imageFileName As String) As Boolean
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Try
			Dim filename as String = FileNameWithoutExt(imageFileName)
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			
			With mycommand
				
				.CommandText = String.Format("INSERT INTO {0} ({1}) VALUES(@{2})", table, column, filename)
				.Parameters.Add(ImageToBlob("@" + filename, imageFileName))
				.ExecuteNonQuery()
				.Dispose()
				
			End With
			
			Return True
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in InsertImage!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return False
		
	End Function
		
	''' <summary>
	''' Reads an image from the database
	''' </summary>
	''' <param name="imageBox"></param>
	''' <returns></returns>
	Public Function ReadImage(ByRef table As String, ByRef column As String, ByRef imageBox As PictureBox, ByVal id as Integer) As Boolean
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
			
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			mycommand.CommandText = String.Format("SELECT {0} FROM {1} WHERE id = '{2}';", column, table, CStr(id))
			Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
			
			While reader.Read()
				
				imageBox.Image = BlobToImage(CType(reader("Picture"), Byte()))
				
			End While
			
			mycommand.Dispose()
			reader.Dispose()
			
			Return True
			
		Catch e As Exception
			MessageBox.Show("Error: " + e.Message, "Error in InsertImage!", MessageBoxButtons.OK, MessageBoxIcon.Error)
			
		End Try
		
		Return False
		
	End Function
	
	''' <summary>
	''' Calls the open file dialog for databases
	''' </summary>
	''' <returns>The file we want or empty string if canceled</returns>
	Public Function OpenDatabaseFile() As String
		
		Return OpenDialog("Open Database", "Project Database (*.db)|*.db|Project Database 2 (*.sdb)|*.sdb|Project Database 3 (*.s3db)|*.s3db|All Files (*.*)|*.*", ".db")
		
	End Function
	
	''' <summary>
	''' This is a generic open file dialog to make life easier
	''' </summary>
	''' <param name="filter">The filter to use</param>
	''' <returns>The file we want or empty string if canceled</returns>
	Private Function OpenDialog(ByVal title as String, ByVal filter as String, ByVal ext as String) As String
		
		OpenDialog = ""
		
		Dim ofd as OpenFileDialog = new OpenFileDialog
		
		With ofd
			
			.Filter = filter
			.DefaultExt = ext
			.FilterIndex = 1
			.Title = title
			.InitialDirectory = CurDir
			
		End With
		
		If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
			
			Try
				'Set the config file for future project openings
				OpenDialog = ofd.FileName
				
			Catch ex As Exception
				MessageBox.Show(ex.Message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
				
			End Try
			
		End If
		
	End Function
	
	''' <summary>
	''' This will convert a blob to an actual image
	''' </summary>
	''' <param name="blob">The blob to convert to an image</param>
	''' <returns>An image</returns>
	Private Function BlobToImage(ByVal blob() As Byte) as Bitmap
		
        Dim mStream As New System.IO.MemoryStream
        Dim pData() As Byte = DirectCast(blob, Byte())
        mStream.Write(pData, 0, Convert.ToInt32(pData.Length))
        BlobToImage = New Bitmap(mStream, False)
        mStream.Dispose()
        
    End Function

	''' <summary>
	''' This will convert an image to a blob
	''' </summary>
	''' <param name="id">The id string of the image</param>
	''' <param name="filePath">Where is the image file</param>
	''' <returns>An sql parameter to input into the DB</returns>
    Private Function ImageToBlob(ByVal id As String, ByVal filePath As String) as SQLiteParameter
    	
        Dim photo() As Byte = My.Computer.FileSystem.ReadAllBytes(filePath)
        ImageToBlob = New SQLiteParameter(id, photo)
        ImageToBlob.DbType = DbType.Binary
        ImageToBlob.Value = photo
        
    End Function

	''' <summary>
	''' This will write out the debug as needed
	''' </summary>
	''' <param name="str">The output to the debug system</param>
	''' <returns>True if good call false if fails</returns>
    Private Function WriteDebug(str As String) As Boolean
    	
    	' Have we set debug on?
    	If bDoDebug Then
    		
    		' Loop through the wheres
	    	For Each item As String In aDebugWheres
	    		
	    		' Print it where it is suppose to go
	    		Select Case item.ToLower
	    			Case "console"
	    				Console.WriteLine("SQLInterface: " + str)
	    			Case "debug"
	    				Debug.WriteLine("SQLInterface: " + str)
	    			Case "file"
	    				
	    				' Try to open and append file
	    				Try
	    					' The log directory
	    					Dim logdirectory As String = "logs"
	    					
	    					' Check to see that the directory exists and or create it
	    					If Not Directory.Exists(logdirectory) Then
	    						Directory.CreateDirectory(logdirectory)
	    					End If
	    					
	    					' Get todays date for the log file
	    					Dim logfilename As String = logdirectory + "/sqlite-" + Today.ToShortDateString.Replace("/",".") + ".log"
	    					
	    					' Append the data to the file
	    					Dim sw As StreamWriter

					        If Not File.Exists(logfilename) Then 
					        	
					            ' Create a file to write to.
					            sw = File.CreateText(logfilename)
					            
					        Else
					        	
					        	' Append to the file
					        	sw = File.AppendText(logfilename)
					            
					        End If
					        
					        ' Write the data
					        sw.WriteLine(Today.ToLongTimeString & ": " & str)
					        
					        ' Flush and close
					        sw.Flush()
					        sw.Close()
							
						Catch ex As Exception
							
							'Error trapping
							Throw New FileIO.MalformedLineException(ex.ToString())
							Return False
							
						End Try
						
	    		End Select
	    		
	    	Next
	    	
    	End If
    	
    	Return True
    	
    End Function
    
    Private Sub Initialize()
    	
    	' Check to see about debug by first seeing if the config file is present
		Try
			' Check to see if the resources file is there
			If Not FileIO.FileSystem.FileExists("sqlconfig.xml") Then
				
				' Need to create the file
				Dim resource = _
					<configuration>
						<debug>False</debug>
						<where>File,Console,Debug</where>
						<dbfilename>test.db3</dbfilename>
					</configuration>
			
				' Create the config.xml
				Dim settings As New XmlWriterSettings
				Dim sw = New StringWriter()
				Dim w = XmlWriter.Create(sw, settings)
				resource.Save(w)
				w.Close()
				
				'save to file
				resource.Save("sqlconfig.xml")
				
			End If
			
			' Now to load the config
			Dim doc As XDocument = XDocument.Load("sqlconfig.xml")
			
			' Get the debug stuff
			bDoDebug = CBool(doc.Root.Element("debug").Value)
			
			' Get where the debug is going
			Dim count As Integer = CInt(Split(doc.Root.Element("where").Value, ",").Length)
			ReDim aDebugWheres(count)
			aDebugWheres = Split(doc.Root.Element("where").Value, ",")
			
		Catch ex As Exception
			
			'Error trapping
			Debug.Write(ex.ToString())
			
		End Try
		
    End Sub
	
	''' <summary>
	''' Tells if we are open
	''' </summary>
	''' <returns>Yes or No</returns>
	Private Function IsConnected() As Boolean
		
		If pConnection.State = System.Data.ConnectionState.Open Then
			IsConnected = True
			
		Else
			IsConnected = False
			
		End If
		
	End Function

End Class