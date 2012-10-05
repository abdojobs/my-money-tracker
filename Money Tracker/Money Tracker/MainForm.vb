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
		Initialize()
	End Sub
	
	' Shows the loader form
	Sub tsmiFileCSVLoadClick(sender As Object, e As EventArgs)
		
		' Lets find our child if its there
		For Each item As Form In Me.MdiChildren
			
			' Check to see if this child is of this type
			If item.GetType = GetType(CSVLoaderForm) Then
				
				item.BringToFront
				Exit Sub
				
			End If
			
		Next
		
		Dim child As New CSVLoaderForm
		child.MdiParent = Me
		child.Show
		
	End Sub
	
	' Loads the main loader	
	Sub tsmiFileMainLoadClick(sender As Object, e As EventArgs)
		
		' Lets find our child if its there
		For Each item As Form In Me.MdiChildren
			
			' Check to see if this child is of this type
			If item.GetType = GetType(DBUpdaterForm) Then
				
				item.BringToFront
				Exit Sub
				
			End If
			
		Next
		
		Dim child As New DBUpdaterForm
		child.MdiParent = Me
		child.Show
		
	End Sub
	
	Sub RawDataToolStripMenuItemClick(sender As Object, e As EventArgs)
		
		' Lets find our child if its there
		For Each item As Form In Me.MdiChildren
			
			' Check to see if this child is of this type
			If item.GetType = GetType(DBRawViewForm) Then
				
				DirectCast(item, DBRawViewForm).Reset()
				item.BringToFront
				Exit Sub
				
			End If
			
		Next
		
		Dim child As New DBRawViewForm
		child.MdiParent = Me
		child.Show
		
	End Sub
	
	Sub SearchToolStripMenuItemClick(sender As Object, e As EventArgs)
		
		' Lets find our child if its there
		For Each item As Form In Me.MdiChildren
			
			' Check to see if this child is of this type
			If item.GetType = GetType(DBSearchForm) Then
				
				item.BringToFront
				Exit Sub
				
			End If
			
		Next
		
		Dim child As New DBSearchForm
		child.MdiParent = Me
		child.Show
		
	End Sub
	
	Sub ExcelToolStripMenuItemClick(sender As Object, e As EventArgs)
		
		' Lets find our child if its there
		For Each item As Form In Me.MdiChildren
			
			' Check to see if this child is of this type
			If item.GetType = GetType(ExcelFileForm) Then
				
				item.BringToFront
				Exit Sub
				
			End If
			
		Next
		
		Dim child As New ExcelFileForm
		child.MdiParent = Me
		child.Show
		
	End Sub
	
End Class
