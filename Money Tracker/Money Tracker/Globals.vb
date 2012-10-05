'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 05/09/2012
' Time: 3:16 PM
' Jeff Walsh - Game Associated Programming®
' 
'

Public Module Globals
	
	' Global for the config file (must be before the SQLiteDBInterfaceClass)
	Public g_config As XMLConfig
	
	' Global for the sql database stuff
	Public g_sql As SQLiteDBInterfaceClass
	
	Public Sub Initialize()
		g_config = New XMLConfig
		g_sql = New SQLiteDBInterfaceClass
	End Sub
	
End Module
