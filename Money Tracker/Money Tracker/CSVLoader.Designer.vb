'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 14/07/2012
' Time: 1:46 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class CSVLoader
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
		Me.buttonSelect = New System.Windows.Forms.Button()
		Me.label1 = New System.Windows.Forms.Label()
		Me.textBoxFileName = New System.Windows.Forms.TextBox()
		Me.buttonLoad = New System.Windows.Forms.Button()
		Me.checkBoxDelete = New System.Windows.Forms.CheckBox()
		Me.checkBoxMarked = New System.Windows.Forms.CheckBox()
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
		Me.checkBoxReset = New System.Windows.Forms.CheckBox()
		Me.SuspendLayout
		'
		'buttonSelect
		'
		Me.buttonSelect.Location = New System.Drawing.Point(357, 36)
		Me.buttonSelect.Name = "buttonSelect"
		Me.buttonSelect.Size = New System.Drawing.Size(30, 20)
		Me.buttonSelect.TabIndex = 1
		Me.buttonSelect.Text = "..."
		Me.buttonSelect.UseVisualStyleBackColor = true
		AddHandler Me.buttonSelect.Click, AddressOf Me.ButtonSelectClick
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(13, 10)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(338, 23)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Select CSV (Comma Seperated Variable) File:"
		'
		'textBoxFileName
		'
		Me.textBoxFileName.Enabled = false
		Me.textBoxFileName.Location = New System.Drawing.Point(13, 36)
		Me.textBoxFileName.Name = "textBoxFileName"
		Me.textBoxFileName.Size = New System.Drawing.Size(338, 20)
		Me.textBoxFileName.TabIndex = 0
		'
		'buttonLoad
		'
		Me.buttonLoad.Enabled = false
		Me.buttonLoad.Location = New System.Drawing.Point(280, 62)
		Me.buttonLoad.Name = "buttonLoad"
		Me.buttonLoad.Size = New System.Drawing.Size(71, 20)
		Me.buttonLoad.TabIndex = 5
		Me.buttonLoad.Text = "Load"
		Me.buttonLoad.UseVisualStyleBackColor = true
		AddHandler Me.buttonLoad.Click, AddressOf Me.ButtonLoadClick
		'
		'checkBoxDelete
		'
		Me.checkBoxDelete.Location = New System.Drawing.Point(105, 63)
		Me.checkBoxDelete.Name = "checkBoxDelete"
		Me.checkBoxDelete.Size = New System.Drawing.Size(86, 20)
		Me.checkBoxDelete.TabIndex = 3
		Me.checkBoxDelete.Text = "Delete"
		Me.checkBoxDelete.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxDelete.CheckedChanged, AddressOf Me.CheckBoxDeleteCheckedChanged
		'
		'checkBoxMarked
		'
		Me.checkBoxMarked.Checked = true
		Me.checkBoxMarked.CheckState = System.Windows.Forms.CheckState.Checked
		Me.checkBoxMarked.Location = New System.Drawing.Point(13, 63)
		Me.checkBoxMarked.Name = "checkBoxMarked"
		Me.checkBoxMarked.Size = New System.Drawing.Size(86, 20)
		Me.checkBoxMarked.TabIndex = 2
		Me.checkBoxMarked.Text = "Mark Read"
		Me.checkBoxMarked.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxMarked.CheckStateChanged, AddressOf Me.CheckBoxMarkedCheckStateChanged
		'
		'openFileDialog
		'
		Me.openFileDialog.DefaultExt = "csv"
		Me.openFileDialog.Filter = "CSV|*.csv|*.*|*.*"
		Me.openFileDialog.Title = "Please Select a CSV File"
		'
		'checkBoxReset
		'
		Me.checkBoxReset.Location = New System.Drawing.Point(197, 62)
		Me.checkBoxReset.Name = "checkBoxReset"
		Me.checkBoxReset.Size = New System.Drawing.Size(67, 20)
		Me.checkBoxReset.TabIndex = 4
		Me.checkBoxReset.Text = "Reset"
		Me.checkBoxReset.UseVisualStyleBackColor = true
		'
		'CSVLoader
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.checkBoxReset)
		Me.Controls.Add(Me.checkBoxMarked)
		Me.Controls.Add(Me.checkBoxDelete)
		Me.Controls.Add(Me.buttonLoad)
		Me.Controls.Add(Me.textBoxFileName)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.buttonSelect)
		Me.Name = "CSVLoader"
		Me.Size = New System.Drawing.Size(396, 96)
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private checkBoxReset As System.Windows.Forms.CheckBox
	Private WithEvents openFileDialog As System.Windows.Forms.OpenFileDialog
	Private checkBoxMarked As System.Windows.Forms.CheckBox
	Private checkBoxDelete As System.Windows.Forms.CheckBox
	Private buttonLoad As System.Windows.Forms.Button
	Private textBoxFileName As System.Windows.Forms.TextBox
	Private label1 As System.Windows.Forms.Label
	Private buttonSelect As System.Windows.Forms.Button
End Class
