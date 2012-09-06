'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 26/11/2011
' Time: 4:57 PM
' 
'
Imports System.Windows.Forms

Public Module Globals
	
	Public gvbDoDebug As Boolean = False
	
	''' <summary>
	''' Calls the open file dialog for databases
	''' </summary>
	''' <returns>The file we want or empty string if canceled</returns>
	Function gfOpenDatabaseFile() As String
		
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
	
End Module