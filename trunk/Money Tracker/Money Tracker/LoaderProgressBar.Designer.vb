'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 14/07/2012
' Time: 1:49 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class LoaderProgressBar
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the control.
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
		Me.progressBar = New System.Windows.Forms.ProgressBar()
		Me.label = New System.Windows.Forms.Label()
		Me.SuspendLayout
		'
		'progressBar
		'
		Me.progressBar.Location = New System.Drawing.Point(3, 21)
		Me.progressBar.Name = "progressBar"
		Me.progressBar.Size = New System.Drawing.Size(205, 23)
		Me.progressBar.TabIndex = 0
		'
		'label
		'
		Me.label.Location = New System.Drawing.Point(3, 0)
		Me.label.Name = "label"
		Me.label.Size = New System.Drawing.Size(205, 18)
		Me.label.TabIndex = 1
		Me.label.Text = "Loading"
		'
		'LoaderProgressBar
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.label)
		Me.Controls.Add(Me.progressBar)
		Me.Name = "LoaderProgressBar"
		Me.Size = New System.Drawing.Size(211, 52)
		Me.ResumeLayout(false)
	End Sub
	Public label As System.Windows.Forms.Label
	Public progressBar As System.Windows.Forms.ProgressBar
End Class
