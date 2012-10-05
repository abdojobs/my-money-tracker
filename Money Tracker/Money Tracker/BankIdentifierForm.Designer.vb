'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 17/09/2012
' Time: 7:05 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class BankIdentifierForm
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
		Me.buttonOk = New System.Windows.Forms.Button()
		Me.bankNameLabel = New System.Windows.Forms.Label()
		Me.textBoxBankName = New System.Windows.Forms.TextBox()
		Me.buttonNext = New System.Windows.Forms.Button()
		Me.SuspendLayout
		'
		'buttonOk
		'
		Me.buttonOk.Location = New System.Drawing.Point(12, 109)
		Me.buttonOk.Name = "buttonOk"
		Me.buttonOk.Size = New System.Drawing.Size(50, 20)
		Me.buttonOk.TabIndex = 0
		Me.buttonOk.Text = "Ok"
		Me.buttonOk.UseVisualStyleBackColor = true
		AddHandler Me.buttonOk.Click, AddressOf Me.ButtonOkClick
		'
		'bankNameLabel
		'
		Me.bankNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.bankNameLabel.Location = New System.Drawing.Point(92, 9)
		Me.bankNameLabel.Name = "bankNameLabel"
		Me.bankNameLabel.Size = New System.Drawing.Size(66, 20)
		Me.bankNameLabel.TabIndex = 1
		Me.bankNameLabel.Text = "Bank Name"
		Me.bankNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'textBoxBankName
		'
		Me.textBoxBankName.Location = New System.Drawing.Point(10, 10)
		Me.textBoxBankName.Name = "textBoxBankName"
		Me.textBoxBankName.Size = New System.Drawing.Size(73, 20)
		Me.textBoxBankName.TabIndex = 2
		'
		'buttonNext
		'
		Me.buttonNext.Location = New System.Drawing.Point(68, 109)
		Me.buttonNext.Name = "buttonNext"
		Me.buttonNext.Size = New System.Drawing.Size(50, 20)
		Me.buttonNext.TabIndex = 3
		Me.buttonNext.Text = "Next"
		Me.buttonNext.UseVisualStyleBackColor = true
		AddHandler Me.buttonNext.Click, AddressOf Me.ButtonNextClick
		'
		'BankIdentifierForm
		'
		Me.AcceptButton = Me.buttonOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(170, 141)
		Me.Controls.Add(Me.buttonNext)
		Me.Controls.Add(Me.textBoxBankName)
		Me.Controls.Add(Me.bankNameLabel)
		Me.Controls.Add(Me.buttonOk)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "BankIdentifierForm"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Bank Identification"
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private buttonNext As System.Windows.Forms.Button
	Private textBoxBankName As System.Windows.Forms.TextBox
	Private bankNameLabel As System.Windows.Forms.Label
	Private buttonOk As System.Windows.Forms.Button
End Class
