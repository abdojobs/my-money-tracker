'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 29/08/2012
' Time: 11:41 AM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class DBSearchForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DBSearchForm))
		Me.groupBox2 = New System.Windows.Forms.GroupBox()
		Me.dataGridView = New System.Windows.Forms.DataGridView()
		Me.label1 = New System.Windows.Forms.Label()
		Me.textBoxTransaction = New System.Windows.Forms.TextBox()
		Me.label3 = New System.Windows.Forms.Label()
		Me.textBoxBank = New System.Windows.Forms.TextBox()
		Me.label4 = New System.Windows.Forms.Label()
		Me.textBoxCategory = New System.Windows.Forms.TextBox()
		Me.buttonClear = New System.Windows.Forms.Button()
		Me.buttonSearch = New System.Windows.Forms.Button()
		Me.datePickerStart = New System.Windows.Forms.DateTimePicker()
		Me.label5 = New System.Windows.Forms.Label()
		Me.datePickerEnd = New System.Windows.Forms.DateTimePicker()
		Me.label6 = New System.Windows.Forms.Label()
		Me.groupBox3 = New System.Windows.Forms.GroupBox()
		Me.radioButtonLike = New System.Windows.Forms.RadioButton()
		Me.radioButtonExact = New System.Windows.Forms.RadioButton()
		Me.groupBox4 = New System.Windows.Forms.GroupBox()
		Me.radioButtonOr = New System.Windows.Forms.RadioButton()
		Me.radioButtonAnd = New System.Windows.Forms.RadioButton()
		Me.groupBox5 = New System.Windows.Forms.GroupBox()
		Me.checkedListBoxVars = New System.Windows.Forms.CheckedListBox()
		Me.groupBox6 = New System.Windows.Forms.GroupBox()
		Me.textBoxTotal = New System.Windows.Forms.TextBox()
		Me.groupBox1 = New System.Windows.Forms.GroupBox()
		Me.groupBox2.SuspendLayout
		CType(Me.dataGridView,System.ComponentModel.ISupportInitialize).BeginInit
		Me.groupBox3.SuspendLayout
		Me.groupBox4.SuspendLayout
		Me.groupBox5.SuspendLayout
		Me.groupBox6.SuspendLayout
		Me.groupBox1.SuspendLayout
		Me.SuspendLayout
		'
		'groupBox2
		'
		Me.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.groupBox2.Controls.Add(Me.dataGridView)
		Me.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.groupBox2.Location = New System.Drawing.Point(0, 170)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(502, 367)
		Me.groupBox2.TabIndex = 1
		Me.groupBox2.TabStop = false
		Me.groupBox2.Text = "Output"
		'
		'dataGridView
		'
		Me.dataGridView.AllowUserToAddRows = false
		Me.dataGridView.AllowUserToDeleteRows = false
		Me.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
		Me.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.dataGridView.Location = New System.Drawing.Point(3, 16)
		Me.dataGridView.Name = "dataGridView"
		Me.dataGridView.ReadOnly = true
		Me.dataGridView.Size = New System.Drawing.Size(496, 348)
		Me.dataGridView.TabIndex = 12
		AddHandler Me.dataGridView.CellMouseClick, AddressOf Me.DataGridViewCellMouseClick
		AddHandler Me.dataGridView.CellMouseUp, AddressOf Me.DataGridViewCellMouseClick
		AddHandler Me.dataGridView.ColumnHeaderMouseClick, AddressOf Me.DataGridViewColumnHeaderMouseClick
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(6, 20)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(66, 20)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Transaction"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'textBoxTransaction
		'
		Me.textBoxTransaction.Location = New System.Drawing.Point(78, 21)
		Me.textBoxTransaction.Name = "textBoxTransaction"
		Me.textBoxTransaction.Size = New System.Drawing.Size(154, 20)
		Me.textBoxTransaction.TabIndex = 1
		'
		'label3
		'
		Me.label3.Location = New System.Drawing.Point(33, 46)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(39, 20)
		Me.label3.TabIndex = 4
		Me.label3.Text = "Bank"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'textBoxBank
		'
		Me.textBoxBank.Location = New System.Drawing.Point(78, 47)
		Me.textBoxBank.Name = "textBoxBank"
		Me.textBoxBank.Size = New System.Drawing.Size(154, 20)
		Me.textBoxBank.TabIndex = 2
		'
		'label4
		'
		Me.label4.Location = New System.Drawing.Point(21, 72)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(51, 20)
		Me.label4.TabIndex = 6
		Me.label4.Text = "Category"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'textBoxCategory
		'
		Me.textBoxCategory.Location = New System.Drawing.Point(78, 73)
		Me.textBoxCategory.Name = "textBoxCategory"
		Me.textBoxCategory.Size = New System.Drawing.Size(154, 20)
		Me.textBoxCategory.TabIndex = 3
		AddHandler Me.textBoxCategory.MouseDoubleClick, AddressOf Me.TextBoxCategoryMouseDoubleClick
		'
		'buttonClear
		'
		Me.buttonClear.Location = New System.Drawing.Point(253, 116)
		Me.buttonClear.Name = "buttonClear"
		Me.buttonClear.Size = New System.Drawing.Size(61, 20)
		Me.buttonClear.TabIndex = 8
		Me.buttonClear.Text = "Clear"
		Me.buttonClear.UseVisualStyleBackColor = true
		AddHandler Me.buttonClear.Click, AddressOf Me.ButtonClearClick
		'
		'buttonSearch
		'
		Me.buttonSearch.Location = New System.Drawing.Point(253, 142)
		Me.buttonSearch.Name = "buttonSearch"
		Me.buttonSearch.Size = New System.Drawing.Size(61, 20)
		Me.buttonSearch.TabIndex = 7
		Me.buttonSearch.Text = "Search"
		Me.buttonSearch.UseVisualStyleBackColor = true
		AddHandler Me.buttonSearch.Click, AddressOf Me.ButtonSearchClick
		'
		'datePickerStart
		'
		Me.datePickerStart.CustomFormat = ""
		Me.datePickerStart.Location = New System.Drawing.Point(78, 99)
		Me.datePickerStart.Name = "datePickerStart"
		Me.datePickerStart.Size = New System.Drawing.Size(154, 20)
		Me.datePickerStart.TabIndex = 4
		Me.datePickerStart.Value = New Date(2012, 1, 1, 0, 0, 0, 0)
		AddHandler Me.datePickerStart.ValueChanged, AddressOf Me.DatePickerStartValueChanged
		'
		'label5
		'
		Me.label5.Location = New System.Drawing.Point(21, 98)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(51, 20)
		Me.label5.TabIndex = 11
		Me.label5.Text = "Start"
		Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'datePickerEnd
		'
		Me.datePickerEnd.CustomFormat = ""
		Me.datePickerEnd.Location = New System.Drawing.Point(78, 128)
		Me.datePickerEnd.Name = "datePickerEnd"
		Me.datePickerEnd.Size = New System.Drawing.Size(154, 20)
		Me.datePickerEnd.TabIndex = 5
		Me.datePickerEnd.Value = New Date(2012, 8, 29, 0, 0, 0, 0)
		AddHandler Me.datePickerEnd.ValueChanged, AddressOf Me.DatePickerEndValueChanged
		'
		'label6
		'
		Me.label6.Location = New System.Drawing.Point(21, 127)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(51, 20)
		Me.label6.TabIndex = 13
		Me.label6.Text = "End"
		Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'groupBox3
		'
		Me.groupBox3.BackColor = System.Drawing.SystemColors.Control
		Me.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.groupBox3.Controls.Add(Me.radioButtonLike)
		Me.groupBox3.Controls.Add(Me.radioButtonExact)
		Me.groupBox3.ForeColor = System.Drawing.Color.Black
		Me.groupBox3.Location = New System.Drawing.Point(238, 12)
		Me.groupBox3.Name = "groupBox3"
		Me.groupBox3.Size = New System.Drawing.Size(96, 48)
		Me.groupBox3.TabIndex = 20
		Me.groupBox3.TabStop = false
		Me.groupBox3.Text = "Type of Search"
		'
		'radioButtonLike
		'
		Me.radioButtonLike.Location = New System.Drawing.Point(6, 28)
		Me.radioButtonLike.Name = "radioButtonLike"
		Me.radioButtonLike.Size = New System.Drawing.Size(81, 16)
		Me.radioButtonLike.TabIndex = 1
		Me.radioButtonLike.TabStop = true
		Me.radioButtonLike.Text = "Like"
		Me.radioButtonLike.UseVisualStyleBackColor = true
		AddHandler Me.radioButtonLike.Click, AddressOf Me.RadioButtonTypeClicked
		'
		'radioButtonExact
		'
		Me.radioButtonExact.Location = New System.Drawing.Point(6, 15)
		Me.radioButtonExact.Name = "radioButtonExact"
		Me.radioButtonExact.Size = New System.Drawing.Size(81, 16)
		Me.radioButtonExact.TabIndex = 0
		Me.radioButtonExact.TabStop = true
		Me.radioButtonExact.Text = "Exact"
		Me.radioButtonExact.UseVisualStyleBackColor = true
		AddHandler Me.radioButtonExact.Click, AddressOf Me.RadioButtonTypeClicked
		'
		'groupBox4
		'
		Me.groupBox4.BackColor = System.Drawing.SystemColors.Control
		Me.groupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.groupBox4.Controls.Add(Me.radioButtonOr)
		Me.groupBox4.Controls.Add(Me.radioButtonAnd)
		Me.groupBox4.ForeColor = System.Drawing.Color.Black
		Me.groupBox4.Location = New System.Drawing.Point(238, 62)
		Me.groupBox4.Name = "groupBox4"
		Me.groupBox4.Size = New System.Drawing.Size(96, 48)
		Me.groupBox4.TabIndex = 21
		Me.groupBox4.TabStop = false
		Me.groupBox4.Text = "And / Or"
		'
		'radioButtonOr
		'
		Me.radioButtonOr.Location = New System.Drawing.Point(6, 28)
		Me.radioButtonOr.Name = "radioButtonOr"
		Me.radioButtonOr.Size = New System.Drawing.Size(81, 16)
		Me.radioButtonOr.TabIndex = 1
		Me.radioButtonOr.TabStop = true
		Me.radioButtonOr.Text = "Or"
		Me.radioButtonOr.UseVisualStyleBackColor = true
		AddHandler Me.radioButtonOr.Click, AddressOf Me.RadioButtonAndOrClicked
		'
		'radioButtonAnd
		'
		Me.radioButtonAnd.Location = New System.Drawing.Point(6, 15)
		Me.radioButtonAnd.Name = "radioButtonAnd"
		Me.radioButtonAnd.Size = New System.Drawing.Size(81, 16)
		Me.radioButtonAnd.TabIndex = 0
		Me.radioButtonAnd.TabStop = true
		Me.radioButtonAnd.Text = "And"
		Me.radioButtonAnd.UseVisualStyleBackColor = true
		AddHandler Me.radioButtonAnd.Click, AddressOf Me.RadioButtonAndOrClicked
		'
		'groupBox5
		'
		Me.groupBox5.Controls.Add(Me.checkedListBoxVars)
		Me.groupBox5.Location = New System.Drawing.Point(340, 12)
		Me.groupBox5.Name = "groupBox5"
		Me.groupBox5.Size = New System.Drawing.Size(154, 98)
		Me.groupBox5.TabIndex = 22
		Me.groupBox5.TabStop = false
		Me.groupBox5.Text = "View Variables"
		'
		'checkedListBoxVars
		'
		Me.checkedListBoxVars.BackColor = System.Drawing.SystemColors.Control
		Me.checkedListBoxVars.CheckOnClick = true
		Me.checkedListBoxVars.FormattingEnabled = true
		Me.checkedListBoxVars.Items.AddRange(New Object() {"Bank", "Date", "Comment", "Categories"})
		Me.checkedListBoxVars.Location = New System.Drawing.Point(6, 19)
		Me.checkedListBoxVars.Name = "checkedListBoxVars"
		Me.checkedListBoxVars.Size = New System.Drawing.Size(142, 64)
		Me.checkedListBoxVars.TabIndex = 16
		Me.checkedListBoxVars.ThreeDCheckBoxes = true
		'
		'groupBox6
		'
		Me.groupBox6.Controls.Add(Me.textBoxTotal)
		Me.groupBox6.Location = New System.Drawing.Point(342, 116)
		Me.groupBox6.Name = "groupBox6"
		Me.groupBox6.Size = New System.Drawing.Size(154, 46)
		Me.groupBox6.TabIndex = 25
		Me.groupBox6.TabStop = false
		Me.groupBox6.Text = "Selected Total $"
		'
		'textBoxTotal
		'
		Me.textBoxTotal.Location = New System.Drawing.Point(6, 19)
		Me.textBoxTotal.Name = "textBoxTotal"
		Me.textBoxTotal.ReadOnly = true
		Me.textBoxTotal.Size = New System.Drawing.Size(142, 20)
		Me.textBoxTotal.TabIndex = 20
		'
		'groupBox1
		'
		Me.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.groupBox1.Controls.Add(Me.groupBox6)
		Me.groupBox1.Controls.Add(Me.groupBox5)
		Me.groupBox1.Controls.Add(Me.groupBox4)
		Me.groupBox1.Controls.Add(Me.groupBox3)
		Me.groupBox1.Controls.Add(Me.label6)
		Me.groupBox1.Controls.Add(Me.datePickerEnd)
		Me.groupBox1.Controls.Add(Me.label5)
		Me.groupBox1.Controls.Add(Me.datePickerStart)
		Me.groupBox1.Controls.Add(Me.buttonSearch)
		Me.groupBox1.Controls.Add(Me.buttonClear)
		Me.groupBox1.Controls.Add(Me.textBoxCategory)
		Me.groupBox1.Controls.Add(Me.label4)
		Me.groupBox1.Controls.Add(Me.textBoxBank)
		Me.groupBox1.Controls.Add(Me.label3)
		Me.groupBox1.Controls.Add(Me.textBoxTransaction)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Top
		Me.groupBox1.Location = New System.Drawing.Point(0, 0)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(502, 170)
		Me.groupBox1.TabIndex = 0
		Me.groupBox1.TabStop = false
		Me.groupBox1.Text = "Search Criteria"
		'
		'DBSearchForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(502, 537)
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.groupBox1)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.Name = "DBSearchForm"
		Me.ShowInTaskbar = false
		Me.Text = "Search DB"
		Me.groupBox2.ResumeLayout(false)
		CType(Me.dataGridView,System.ComponentModel.ISupportInitialize).EndInit
		Me.groupBox3.ResumeLayout(false)
		Me.groupBox4.ResumeLayout(false)
		Me.groupBox5.ResumeLayout(false)
		Me.groupBox6.ResumeLayout(false)
		Me.groupBox6.PerformLayout
		Me.groupBox1.ResumeLayout(false)
		Me.groupBox1.PerformLayout
		Me.ResumeLayout(false)
	End Sub
	Private groupBox6 As System.Windows.Forms.GroupBox
	Private radioButtonAnd As System.Windows.Forms.RadioButton
	Private radioButtonOr As System.Windows.Forms.RadioButton
	Private groupBox4 As System.Windows.Forms.GroupBox
	Private groupBox5 As System.Windows.Forms.GroupBox
	Private radioButtonExact As System.Windows.Forms.RadioButton
	Private radioButtonLike As System.Windows.Forms.RadioButton
	Private groupBox3 As System.Windows.Forms.GroupBox
	Private textBoxTotal As System.Windows.Forms.TextBox
	Private checkedListBoxVars As System.Windows.Forms.CheckedListBox
	Private dataGridView As System.Windows.Forms.DataGridView
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private datePickerStart As System.Windows.Forms.DateTimePicker
	Private label5 As System.Windows.Forms.Label
	Private datePickerEnd As System.Windows.Forms.DateTimePicker
	Private label6 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private textBoxBank As System.Windows.Forms.TextBox
	Private label4 As System.Windows.Forms.Label
	Private textBoxCategory As System.Windows.Forms.TextBox
	Private buttonClear As System.Windows.Forms.Button
	Private buttonSearch As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
	Private textBoxTransaction As System.Windows.Forms.TextBox
	Private groupBox1 As System.Windows.Forms.GroupBox
End Class
