'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 23/07/2012
' Time: 4:37 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class DBUpdaterForm
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
		Me.components = New System.ComponentModel.Container()
		Me.dbUpdaterInfoControl = New MoneyTracker.DBUpdaterInfoControl()
		Me.SuspendLayout
		'
		'dbUpdaterInfoControl2
		'
		Me.dbUpdaterInfoControl.Location = New System.Drawing.Point(6, 12)
		Me.dbUpdaterInfoControl.Name = "dbUpdaterInfoControl2"
		Me.dbUpdaterInfoControl.Size = New System.Drawing.Size(487, 299)
		Me.dbUpdaterInfoControl.TabIndex = 0
		'
		'DBUpdaterForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(500, 314)
		Me.Controls.Add(Me.dbUpdaterInfoControl)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "DBUpdaterForm"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.Text = "Updater Form"
		Me.TopMost = true
		Me.ResumeLayout(false)
	End Sub
	Private dbUpdaterInfoControl As MoneyTracker.DBUpdaterInfoControl
End Class
