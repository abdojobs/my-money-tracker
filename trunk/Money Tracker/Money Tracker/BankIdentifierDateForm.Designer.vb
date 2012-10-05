'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 18/09/2012
' Time: 5:42 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class BankIdentifierDateForm
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
		Me.labelExample = New System.Windows.Forms.Label()
		Me.comboBox1 = New System.Windows.Forms.ComboBox()
		Me.comboBox2 = New System.Windows.Forms.ComboBox()
		Me.comboBox3 = New System.Windows.Forms.ComboBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.textBoxDelim = New System.Windows.Forms.TextBox()
		Me.buttonOk = New System.Windows.Forms.Button()
		Me.SuspendLayout
		'
		'labelExample
		'
		Me.labelExample.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.labelExample.Location = New System.Drawing.Point(9, 9)
		Me.labelExample.Name = "labelExample"
		Me.labelExample.Size = New System.Drawing.Size(138, 21)
		Me.labelExample.TabIndex = 0
		Me.labelExample.Text = "2012/12/31"
		Me.labelExample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'comboBox1
		'
		Me.comboBox1.FormattingEnabled = true
		Me.comboBox1.Items.AddRange(New Object() {"y", "m", "d"})
		Me.comboBox1.Location = New System.Drawing.Point(9, 40)
		Me.comboBox1.Name = "comboBox1"
		Me.comboBox1.Size = New System.Drawing.Size(42, 21)
		Me.comboBox1.TabIndex = 1
		'
		'comboBox2
		'
		Me.comboBox2.FormattingEnabled = true
		Me.comboBox2.Items.AddRange(New Object() {"y", "m", "d"})
		Me.comboBox2.Location = New System.Drawing.Point(57, 40)
		Me.comboBox2.Name = "comboBox2"
		Me.comboBox2.Size = New System.Drawing.Size(42, 21)
		Me.comboBox2.TabIndex = 2
		'
		'comboBox3
		'
		Me.comboBox3.FormattingEnabled = true
		Me.comboBox3.Items.AddRange(New Object() {"y", "m", "d"})
		Me.comboBox3.Location = New System.Drawing.Point(105, 40)
		Me.comboBox3.Name = "comboBox3"
		Me.comboBox3.Size = New System.Drawing.Size(42, 21)
		Me.comboBox3.TabIndex = 3
		'
		'label1
		'
		Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.label1.Location = New System.Drawing.Point(153, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(74, 21)
		Me.label1.TabIndex = 4
		Me.label1.Text = "Delimitator?"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'textBoxDelim
		'
		Me.textBoxDelim.Location = New System.Drawing.Point(154, 41)
		Me.textBoxDelim.Name = "textBoxDelim"
		Me.textBoxDelim.Size = New System.Drawing.Size(73, 20)
		Me.textBoxDelim.TabIndex = 5
		'
		'buttonOk
		'
		Me.buttonOk.Location = New System.Drawing.Point(180, 67)
		Me.buttonOk.Name = "buttonOk"
		Me.buttonOk.Size = New System.Drawing.Size(47, 23)
		Me.buttonOk.TabIndex = 6
		Me.buttonOk.Text = "Ok"
		Me.buttonOk.UseVisualStyleBackColor = true
		AddHandler Me.buttonOk.Click, AddressOf Me.ButtonOkClick
		'
		'BankIdentifierDateForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(237, 98)
		Me.Controls.Add(Me.buttonOk)
		Me.Controls.Add(Me.textBoxDelim)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.comboBox3)
		Me.Controls.Add(Me.comboBox2)
		Me.Controls.Add(Me.comboBox1)
		Me.Controls.Add(Me.labelExample)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "BankIdentifierDateForm"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Date Identifier"
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private buttonOk As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
	Private textBoxDelim As System.Windows.Forms.TextBox
	Private comboBox3 As System.Windows.Forms.ComboBox
	Private comboBox2 As System.Windows.Forms.ComboBox
	Private comboBox1 As System.Windows.Forms.ComboBox
	Private labelExample As System.Windows.Forms.Label
End Class
