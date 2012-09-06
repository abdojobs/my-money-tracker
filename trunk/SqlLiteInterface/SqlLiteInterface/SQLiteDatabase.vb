'
' Created by SharpDevelop.
' User: Cpl Jeff Walsh
' Date: 11/3/2011
' Time: 7:57 AM
'
' Completed for Mr. Roamer's PO 415
'
Imports System.Data
Imports System.Data.SQLite
Imports System.Windows.Forms
Imports System.Drawing

Public Class SQLiteDatabase

	Private pConnection As SQLiteConnection
	
	' Old not sure if this setup is working...
	Private plDataGridView As System.Windows.Forms.DataGridView
	
	' The global Data Adapter
	Private plDataAdapter As SQLiteDataAdapter
	
	' Global dataset
	Private plDataSet As DataSet
	
	' Global binding source
	Private plBindingSource As BindingSource
	
	''' <summary>
	'''     Single Param Constructor for specifying the DB file.
	''' </summary>
	''' <param name="inputFile">The File containing the DB</param>
	Public Sub New()
		
		pConnection = New SQLiteConnection
		
	End Sub
	
	''' <summary>
	'''     Single Param Constructor for specifying the DB file.
	''' </summary>
	''' <param name="inputFile">The File containing the DB.  Empty will get you an open file dialog, name will get that file created.</param>
	Public Sub New(ByVal inputFile As String, Optional create As Boolean = False)
		
		' I need to ask them for the filename
		If inputFile.Length < 1 Then
			inputFile = gfOpenDatabaseFile()
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
			pConnection.Open()
			
		Catch e As Exception
			Throw New Exception("Opening: " + e.Message)
			Connect = False
		End Try
		
		Connect = IsConnected()
		
	End Function
	
	''' <summary>
	''' Closes the DB
	''' </summary>
	Public Sub Disconnect()
		
		pConnection.Close()
		pConnection.Dispose()
		
	End Sub
	
	''' <summary>
	''' Creates a new DB
	''' </summary>
	''' <returns>If it was or was not created</returns>
	Public Function ExecuteCreate() As Boolean
		
		ExecuteCreate = False
		
		If Me.IsConnected Then
				
			Me.Disconnect()
			Me.Connect()
			Me.Disconnect()
			ExecuteCreate = True
			
		Else
			Me.Connect()
			Me.Disconnect()
			ExecuteCreate = True
			
		End If
		
	End Function

	''' <summary>
	'''     Allows the programmer to run a query against the Database.
	''' </summary>
	''' <param name="sql">The SQL to run</param>
	''' <returns>A DataTable containing the result set.</returns>
	Public Function GetDataTable(ByVal sql As String) As DataTable
		
		If gvbDoDebug Then
			Debug.WriteLine(sql)
		End If
		
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
			Throw New Exception(e.Message)
			
		End Try
		
		Return dt
		
	End Function

	''' <summary>
	'''     Allows the programmer to run a query against the Database.
	''' </summary>
	''' <param name="table">The table to get complete</param>
	''' <returns>A DataTable containing the result set.</returns>
	Public Function GetFullDataTable(ByVal table As String) As DataTable
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Dim dt As DataTable = New DataTable()
		
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			mycommand.CommandText = String.Format("SELECT * FROM {0}", table)
			
			If gvbDoDebug Then
				Debug.WriteLine(mycommand.CommandText)
			End If
			
			Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
			dt.Load(reader)
			reader.Close()
			
		Catch e As Exception
			Throw New Exception(e.Message)
			
		End Try
		
		Return dt
		
	End Function

	''' <summary>
	'''     Allows the programmer to run a query against the Database.
	''' </summary>
	''' <param name="sql">The SQL to run</param>
	''' <returns>A DataRow containing the single line result set.</returns>
	Public Function GetDataRow(ByVal sql As String) As DataRow
		
		If gvbDoDebug Then
			Debug.WriteLine(sql)
		End If
		
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
			Throw New Exception(e.Message)
			
		End Try
		
		If dt.Rows.Count > 0 Then 
			GetDataRow = dt.Rows(0)
		Else
			GetDataRow = Nothing
		End If
	
	End Function
        
	''' <summary>
	'''     Allows the programmer to interact with the database for purposes other than a query.
	''' </summary>
	''' <param name="sql">The SQL to be run.</param>
	''' <returns>An Integer containing the number of rows updated.</returns>
	Public Function ExecuteNonQuery(ByVal sql As String) As Integer
		
		If gvbDoDebug Then
			Debug.WriteLine(sql)
		End If
		
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
		
		If gvbDoDebug Then
			Debug.WriteLine(sql)
		End If
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Dim value As Object = Nothing
		Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
		mycommand.CommandText = sql
		value = mycommand.ExecuteScalar()
		
		If Not IsNothing(value) Then
			ExecuteScalar = value.ToString()
		Else
			ExecuteScalar = Nothing
		End If

	End Function

	''' <summary>
	'''     Allows the programmer to easily update rows in the DB.
	''' </summary>
	''' <param name="tableName">The table to update.</param>
	''' <param name="valuestr">A string containing Column names and their new values in Criteria Format: Name=>Value|Name=>Value</param>
	''' <param name="wherestr">The where string for the update statement.</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Update(ByVal tableName As String, ByVal valuestr As String, ByVal wherestr As String) As Boolean

		Update = True

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
			Me.ExecuteNonQuery(String.Format("UPDATE {0} SET {1} WHERE {2};", tableName, values, wheres))

		Catch e As Exception
			Update = False
			
		End Try

	End Function

	''' <summary>
	'''     Allows the programmer to easily update rows in the DB.
	''' </summary>
	''' <param name="tableName">The table to update.</param>
	''' <param name="value">A Criteria List containing column names and their values</param>
	''' <param name="where">A Criteria List containing where names and their values</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Update(ByVal tableName As String, ByVal value As CriteriaCollection, ByVal where As CriteriaCollection) As Boolean

		Update = True

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
			Me.ExecuteNonQuery(String.Format("UPDATE {0} SET {1} WHERE {2};", tableName, values, wheres))

		Catch e As Exception
			Update = False
			
		End Try

	End Function

	''' <summary>
	'''     Allows the programmer to easily update/install a rows in the DB.
	''' </summary>
	''' <param name="tableName">The table to update.</param>
	''' <param name="dstr">A string containing Column names and their new values in Criteria Format: Name=>Value|Name=>Value</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Replace(ByVal tableName As String, ByVal dstr As String) As Boolean

		Replace = True

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
			Me.ExecuteNonQuery(String.Format("REPLACE INTO {0} ({1}) VALUES ({2});", tableName, whats, withs))

		Catch e As Exception
			Replace = False
			
		End Try

	End Function
	
	

	''' <summary>
	'''     Allows the programmer to easily update/install a rows in the DB.
	''' </summary>
	''' <param name="tableName">The table to update.</param>
	''' <param name="data">A Criteria List containing Column names and their new values</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Replace(ByVal tableName As String, ByVal data As CriteriaCollection) As Boolean

		Replace = True

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
			Me.ExecuteNonQuery(String.Format("REPLACE INTO {0} ({1}) VALUES ({2});", tableName, whats, withs))

		Catch e As Exception
			Replace = False
			
		End Try

	End Function

	''' <summary>
	'''     Allows the programmer to easily delete rows from the DB.
	''' </summary>
	''' <param name="tableName">The table from which to delete.</param>
	''' <param name="where">The where clause for the delete.</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function Delete(ByVal tableName As String, ByVal where As String) As Boolean
	
		Delete = true
	
		Try
			Me.ExecuteNonQuery(String.Format("DELETE FROM {0} WHERE {1};", tableName, where))
			
		Catch e As Exception
			MessageBox.Show(e.Message)
			Delete = False
			
		End Try
		
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
		Insert = True
		
		For Each pair In data
			
			columns = columns + String.Format(" {0},", pair.Name)
			values = values + String.Format(" '{0}',", pair.Value)
			
		Next
		
		columns = columns.Substring(0, columns.Length - 1)
		values = values.Substring(0, values.Length - 1)
	
		Try
			Me.ExecuteNonQuery(String.Format("INSERT INTO {0}({1}) VALUES({2});", tableName, columns, values))
			
		Catch e As Exception
			MessageBox.Show(e.Message)
			Insert = False
	
		End Try
		
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
		
		Insert = True
		
		For Each pair In data
			
			columns = columns + String.Format(" {0},", pair.Name)
			values = values + String.Format(" '{0}',", pair.Value)
			
		Next
		
		columns = columns.Substring(0, columns.Length - 1)
		values = values.Substring(0, values.Length - 1)
	
		Try
			Me.ExecuteNonQuery(String.Format("INSERT INTO {0}({1}) VALUES({2});", tableName, columns, values))
			
		Catch e As Exception
			MessageBox.Show(e.Message)
			Insert = False
	
		End Try
		
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
			 Return Me.ExecuteScalar(String.Format("SELECT {0} FROM {1} WHERE {2} LIMIT 1;", getwhat, tableName, query))

		Catch e As Exception
			Return Nothing
			
		End Try

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
			 Return Me.ExecuteScalar(String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, query))

		Catch e As Exception
			Return Nothing
			
		End Try

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
			Return Me.GetDataRow(String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, wheres))

		Catch e As Exception
			Return Nothing
			
		End Try

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
			Return Me.GetDataRow(String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, wheres))

		Catch e As Exception
			Return Nothing
			
		End Try

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
			Return Me.GetDataTable(String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, query))

		Catch e As Exception
			Return Nothing
			
		End Try

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
			Return Me.GetDataTable(String.Format("SELECT {0} FROM {1} WHERE {2};", getwhat, tableName, query))

		Catch e As Exception
			Return Nothing
			
		End Try

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
	
			ClearDB = True
		
		Catch e As Exception
			ClearDB = false
	
		End Try
		
	End Function

	''' <summary>
	'''     Allows the user to easily clear all data from a specific table.
	''' </summary>
	''' <param name="table">The name of the table to clear.</param>
	''' <returns>A boolean true or false to signify success or failure.</returns>
	Public Function ClearTable(ByVal table as String) As Boolean
		
		Try
			Me.ExecuteNonQuery(String.Format("delete from {0};", table))
			ClearTable = True
			
		Catch e As Exception
			ClearTable = False
			
		End Try
	
	End Function

	''' <summary>
	''' Allows the programmer to bind a Database to a Something
	''' </summary>
	''' <param name="sql">The SQL command to run</param>
	''' <returns>A BindingSource containing the result set.</returns>
	Public Function BindingDatabase(ByVal sql as String) As BindingSource
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Try
			plDataAdapter = New SQLiteDataAdapter(sql, pConnection)
			plDataSet = New DataSet()
			plDataAdapter.Fill(plDataSet)
			
			plBindingSource = New BindingSource()
			plBindingSource.DataSource = plDataSet.Tables(0)
			
			Return plBindingSource
			
		Catch e As Exception
			Return Nothing
			
		End Try
		
	End Function

	''' <summary>
	''' Setup an insert command for a bound table
	''' </summary>
	''' <param name="sql"></param>
	''' <returns></returns>
	Public Function SetupBoundInsert(ByVal sql As String, ByRef param() As SQLiteParameter) As Boolean
		
		Try
			
			plDataAdapter.InsertCommand = New SQLiteCommand(sql, pConnection)
			
			For Each item As SQLiteParameter In param
				plDataAdapter.InsertCommand.Parameters.Add(item)
			Next
			
		Catch e As Exception
			Return False
			
		End Try
		
		Return True
	
	End Function

	''' <summary>
	''' Setup an update command for a bound table
	''' </summary>
	''' <param name="sql"></param>
	''' <returns></returns>
	Public Function SetupBoundUpdate(ByVal sql As String, ByVal param() As SQLiteParameter) As Boolean
		
		Try
				
			plDataAdapter.UpdateCommand = New SQLiteCommand(sql, pConnection)
			
			For Each item As SQLiteParameter In param
				plDataAdapter.UpdateCommand.Parameters.Add(item)
			Next
			
		Catch e As Exception
			Return False
			
		End Try
		
		Return True
	
	End Function

	''' <summary>
	''' Setup a delete command for a bound table
	''' </summary>
	''' <param name="sql"></param>
	''' <returns></returns>
	Public Function SetupBoundDelete(ByVal sql As String, ByVal param() As SQLiteParameter) As Boolean
		
		Try
				
			plDataAdapter.DeleteCommand = New SQLiteCommand(sql, pConnection)
			
			For Each item As SQLiteParameter In param
				plDataAdapter.DeleteCommand.Parameters.Add(item)
			Next
			
		Catch e As Exception
			Return False
			
		End Try
		
		Return True
	
	End Function
	
	''' <summary>
	''' Allows the programmer to send a table to the Database.
	''' </summary>
	''' <returns>Good or Bad</returns>
	Public Function BoundTableCallUpdate() As Boolean
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		Try
			plDataAdapter.Update(plDataSet)
			
		Catch e As Exception
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
		
		InsertFile = False
		
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			
			With mycommand
				
				.CommandText = String.Format("INSERT INTO {0} ({1}) VALUES(@string)", table, column)
				.Parameters.Add(StringToBlob("@string", str))
				.ExecuteNonQuery()
				.Dispose()
				
			End With
			
		Catch e As Exception
			Throw New Exception("InsertImage: " + e.Message)
			
		End Try
		
		InsertFile = True
		
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
		
		ReadFile = Nothing
			
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			mycommand.CommandText = String.Format("SELECT {0} FROM {1} WHERE id = '{2}';", column, table, CStr(id))
			Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
			
			While reader.Read()
				
				ReadFile = BlobToString(CType(reader(column), Byte()))
				
			End While
			
			mycommand.Dispose()
			reader.Dispose()
			
		Catch e As Exception
			Throw New Exception("ReadFile: " + e.Message)
			
		End Try
		
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
		
		InsertImage = False
		
		Try
			Dim filename as String = FileNameWithoutExt(imageFileName)
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			
			With mycommand
				
				.CommandText = String.Format("INSERT INTO {0} ({1}) VALUES(@{2})", table, column, filename)
				.Parameters.Add(ImageToBlob("@" + filename, imageFileName))
				.ExecuteNonQuery()
				.Dispose()
				
			End With
			
			InsertImage = True
			
		Catch e As Exception
			Throw New Exception("InsertImage: " + e.Message)
			
		End Try
		
	End Function
		
	''' <summary>
	''' Reads an image from the database
	''' </summary>
	''' <param name="imageBox"></param>
	''' <returns></returns>
	Public Function ReadImage(ByRef table As String, ByRef column As String, ByRef imageBox As PictureBox, ByVal id as Integer) As Image
		
		' Are we open yet?
		If Not Me.IsConnected Then
			Throw New Exception("Need to Open DB")
		End If
		
		ReadImage = Nothing
			
		Try
			Dim mycommand As SQLiteCommand = New SQLiteCommand(pConnection)
			mycommand.CommandText = String.Format("SELECT {0} FROM {1} WHERE id = '{2}';", column, table, CStr(id))
			Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
			
			While reader.Read()
				
				imageBox.Image = BlobToImage(CType(reader("Picture"), Byte()))
				
			End While
			
			mycommand.Dispose()
			reader.Dispose()
			
		Catch e As Exception
			Throw New Exception("ReadImage: " + e.Message)
			
		End Try
		
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