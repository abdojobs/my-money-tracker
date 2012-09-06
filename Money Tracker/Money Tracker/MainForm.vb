'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 14/07/2012
' Time: 12:09 PM
' Jeff Walsh - Game Associated Programming®
' 
'

Public Partial Class MainForm
	
	' Initializes the main form
	Public Sub New()
		Me.InitializeComponent()
		Me.Text += My.Application.Info.Version.ToString
	End Sub
	
	' Shows the loader form
	Sub tsmiFileCSVLoadClick(sender As Object, e As EventArgs)
		Dim child As New CSVLoaderForm
		child.MdiParent = Me
		child.Show
	End Sub
	
	' Loads the main loader	
	Sub tsmiFileMainLoadClick(sender As Object, e As EventArgs)
		Dim child As New DBUpdaterForm
		child.MdiParent = Me
		child.Show
	End Sub
	
	Sub RawDataToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim child As New DBRawViewForm
		child.MdiParent = Me
		child.Show
	End Sub
	
	Sub SearchToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim child As New DBSearchForm
		child.MdiParent = Me
		child.Show
	End Sub
	
End Class
