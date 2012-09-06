'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 15/07/2012
' Time: 11:40 AM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class CSVLoaderForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.csvLoader1 = New CSVLoader()
		Me.SuspendLayout
		'
		'csvLoader1
		'
		Me.csvLoader1.Location = New System.Drawing.Point(12, 12)
		Me.csvLoader1.Name = "csvLoader1"
		Me.csvLoader1.Size = New System.Drawing.Size(394, 97)
		Me.csvLoader1.TabIndex = 0
		'
		'CSVLoaderForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(417, 121)
		Me.Controls.Add(Me.csvLoader1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "CSVLoaderForm"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.Text = "CSV Loader"
		Me.TopMost = true
		Me.ResumeLayout(false)
	End Sub
	Private csvLoader1 As CSVLoader
End Class
