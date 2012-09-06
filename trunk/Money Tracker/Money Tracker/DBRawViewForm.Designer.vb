'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 29/08/2012
' Time: 12:21 AM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class DBRawViewForm
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
		Me.dataGridView = New System.Windows.Forms.DataGridView()
		CType(Me.dataGridView,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'dataGridView
		'
		Me.dataGridView.AllowUserToAddRows = false
		Me.dataGridView.AllowUserToDeleteRows = false
		Me.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
		Me.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.dataGridView.Location = New System.Drawing.Point(0, 0)
		Me.dataGridView.Name = "dataGridView"
		Me.dataGridView.Size = New System.Drawing.Size(758, 414)
		Me.dataGridView.TabIndex = 1
		'
		'DBRawViewForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(758, 414)
		Me.Controls.Add(Me.dataGridView)
		Me.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.Name = "DBRawViewForm"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Data Base Raw View"
		CType(Me.dataGridView,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
	End Sub
	Private dataGridView As System.Windows.Forms.DataGridView
End Class
