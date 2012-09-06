'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 23/07/2012
' Time: 4:34 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class DBUpdaterInfoControl
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DBUpdaterInfoControl))
		Me.groupBox1 = New System.Windows.Forms.GroupBox()
		Me.buttonNotes = New System.Windows.Forms.Button()
		Me.textBoxSubCategory = New System.Windows.Forms.TextBox()
		Me.textBoxCategories = New System.Windows.Forms.TextBox()
		Me.textBoxID = New System.Windows.Forms.TextBox()
		Me.richTextBoxNote = New System.Windows.Forms.RichTextBox()
		Me.label8 = New System.Windows.Forms.Label()
		Me.textBoxBank = New System.Windows.Forms.TextBox()
		Me.label7 = New System.Windows.Forms.Label()
		Me.textBoxTransaction = New System.Windows.Forms.TextBox()
		Me.label4 = New System.Windows.Forms.Label()
		Me.textBoxOut = New System.Windows.Forms.TextBox()
		Me.label6 = New System.Windows.Forms.Label()
		Me.textBoxDate = New System.Windows.Forms.TextBox()
		Me.label3 = New System.Windows.Forms.Label()
		Me.bindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
		Me.bindingNavigatorAdd = New System.Windows.Forms.ToolStripButton()
		Me.bindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
		Me.bindingNavigatorDelete = New System.Windows.Forms.ToolStripButton()
		Me.bindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
		Me.bindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
		Me.bindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
		Me.bindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
		Me.bindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.bindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
		Me.bindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
		Me.bindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
		Me.groupBox2 = New System.Windows.Forms.GroupBox()
		Me.buttonSubCategories = New System.Windows.Forms.Button()
		Me.buttonCategories = New System.Windows.Forms.Button()
		Me.label2 = New System.Windows.Forms.Label()
		Me.label1 = New System.Windows.Forms.Label()
		Me.dropBoxSubCategory = New System.Windows.Forms.ComboBox()
		Me.dropBoxCategory = New System.Windows.Forms.ComboBox()
		Me.groupBox1.SuspendLayout
		CType(Me.bindingNavigator,System.ComponentModel.ISupportInitialize).BeginInit
		Me.bindingNavigator.SuspendLayout
		Me.groupBox2.SuspendLayout
		Me.SuspendLayout
		'
		'groupBox1
		'
		Me.groupBox1.Controls.Add(Me.buttonNotes)
		Me.groupBox1.Controls.Add(Me.textBoxSubCategory)
		Me.groupBox1.Controls.Add(Me.textBoxCategories)
		Me.groupBox1.Controls.Add(Me.textBoxID)
		Me.groupBox1.Controls.Add(Me.richTextBoxNote)
		Me.groupBox1.Controls.Add(Me.label8)
		Me.groupBox1.Controls.Add(Me.textBoxBank)
		Me.groupBox1.Controls.Add(Me.label7)
		Me.groupBox1.Controls.Add(Me.textBoxTransaction)
		Me.groupBox1.Controls.Add(Me.label4)
		Me.groupBox1.Controls.Add(Me.textBoxOut)
		Me.groupBox1.Controls.Add(Me.label6)
		Me.groupBox1.Controls.Add(Me.textBoxDate)
		Me.groupBox1.Controls.Add(Me.label3)
		Me.groupBox1.Location = New System.Drawing.Point(3, 28)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(481, 183)
		Me.groupBox1.TabIndex = 16
		Me.groupBox1.TabStop = false
		Me.groupBox1.Text = "Item Specifics"
		'
		'buttonNotes
		'
		Me.buttonNotes.Location = New System.Drawing.Point(445, 123)
		Me.buttonNotes.Name = "buttonNotes"
		Me.buttonNotes.Size = New System.Drawing.Size(24, 21)
		Me.buttonNotes.TabIndex = 25
		Me.buttonNotes.Text = "..."
		Me.buttonNotes.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.buttonNotes.UseVisualStyleBackColor = true
		AddHandler Me.buttonNotes.Click, AddressOf Me.ButtonNotesClick
		'
		'textBoxSubCategory
		'
		Me.textBoxSubCategory.Enabled = false
		Me.textBoxSubCategory.Location = New System.Drawing.Point(56, 157)
		Me.textBoxSubCategory.Name = "textBoxSubCategory"
		Me.textBoxSubCategory.Size = New System.Drawing.Size(20, 20)
		Me.textBoxSubCategory.TabIndex = 24
		Me.textBoxSubCategory.TabStop = false
		'
		'textBoxCategories
		'
		Me.textBoxCategories.Enabled = false
		Me.textBoxCategories.Location = New System.Drawing.Point(30, 157)
		Me.textBoxCategories.Name = "textBoxCategories"
		Me.textBoxCategories.Size = New System.Drawing.Size(20, 20)
		Me.textBoxCategories.TabIndex = 23
		Me.textBoxCategories.TabStop = false
		'
		'textBoxID
		'
		Me.textBoxID.Enabled = false
		Me.textBoxID.Location = New System.Drawing.Point(6, 157)
		Me.textBoxID.Name = "textBoxID"
		Me.textBoxID.Size = New System.Drawing.Size(19, 20)
		Me.textBoxID.TabIndex = 0
		Me.textBoxID.TabStop = false
		AddHandler Me.textBoxID.TextChanged, AddressOf Me.TextBoxIDTextChanged
		'
		'richTextBoxNote
		'
		Me.richTextBoxNote.Location = New System.Drawing.Point(86, 123)
		Me.richTextBoxNote.MaxLength = 1000
		Me.richTextBoxNote.Name = "richTextBoxNote"
		Me.richTextBoxNote.Size = New System.Drawing.Size(353, 47)
		Me.richTextBoxNote.TabIndex = 22
		Me.richTextBoxNote.Text = ""
		'
		'label8
		'
		Me.label8.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.label8.Location = New System.Drawing.Point(38, 123)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(42, 20)
		Me.label8.TabIndex = 21
		Me.label8.Text = "Notes"
		Me.label8.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'textBoxBank
		'
		Me.textBoxBank.Enabled = false
		Me.textBoxBank.Location = New System.Drawing.Point(86, 45)
		Me.textBoxBank.Name = "textBoxBank"
		Me.textBoxBank.Size = New System.Drawing.Size(389, 20)
		Me.textBoxBank.TabIndex = 19
		Me.textBoxBank.TabStop = false
		'
		'label7
		'
		Me.label7.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.label7.Location = New System.Drawing.Point(38, 45)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(42, 20)
		Me.label7.TabIndex = 20
		Me.label7.Text = "Bank"
		Me.label7.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'textBoxTransaction
		'
		Me.textBoxTransaction.Enabled = false
		Me.textBoxTransaction.Location = New System.Drawing.Point(86, 19)
		Me.textBoxTransaction.Name = "textBoxTransaction"
		Me.textBoxTransaction.Size = New System.Drawing.Size(389, 20)
		Me.textBoxTransaction.TabIndex = 17
		Me.textBoxTransaction.TabStop = false
		'
		'label4
		'
		Me.label4.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.label4.Location = New System.Drawing.Point(6, 19)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(74, 20)
		Me.label4.TabIndex = 18
		Me.label4.Text = "Transaction"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'textBoxOut
		'
		Me.textBoxOut.Enabled = false
		Me.textBoxOut.Location = New System.Drawing.Point(86, 97)
		Me.textBoxOut.Name = "textBoxOut"
		Me.textBoxOut.Size = New System.Drawing.Size(89, 20)
		Me.textBoxOut.TabIndex = 0
		Me.textBoxOut.TabStop = false
		'
		'label6
		'
		Me.label6.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.label6.Location = New System.Drawing.Point(30, 96)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(50, 20)
		Me.label6.TabIndex = 14
		Me.label6.Text = "Outgoing"
		Me.label6.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'textBoxDate
		'
		Me.textBoxDate.Enabled = false
		Me.textBoxDate.Location = New System.Drawing.Point(86, 71)
		Me.textBoxDate.Name = "textBoxDate"
		Me.textBoxDate.Size = New System.Drawing.Size(89, 20)
		Me.textBoxDate.TabIndex = 0
		Me.textBoxDate.TabStop = false
		'
		'label3
		'
		Me.label3.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.label3.Location = New System.Drawing.Point(45, 70)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(35, 20)
		Me.label3.TabIndex = 8
		Me.label3.Text = "Date"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'bindingNavigator
		'
		Me.bindingNavigator.AddNewItem = Me.bindingNavigatorAdd
		Me.bindingNavigator.CountItem = Me.bindingNavigatorCountItem
		Me.bindingNavigator.DeleteItem = Me.bindingNavigatorDelete
		Me.bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.bindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bindingNavigatorMoveFirstItem, Me.bindingNavigatorMovePreviousItem, Me.bindingNavigatorSeparator, Me.bindingNavigatorPositionItem, Me.bindingNavigatorCountItem, Me.bindingNavigatorSeparator1, Me.bindingNavigatorMoveNextItem, Me.bindingNavigatorMoveLastItem, Me.bindingNavigatorSeparator2, Me.bindingNavigatorDelete, Me.bindingNavigatorAdd})
		Me.bindingNavigator.Location = New System.Drawing.Point(0, 0)
		Me.bindingNavigator.MoveFirstItem = Me.bindingNavigatorMoveFirstItem
		Me.bindingNavigator.MoveLastItem = Me.bindingNavigatorMoveLastItem
		Me.bindingNavigator.MoveNextItem = Me.bindingNavigatorMoveNextItem
		Me.bindingNavigator.MovePreviousItem = Me.bindingNavigatorMovePreviousItem
		Me.bindingNavigator.Name = "bindingNavigator"
		Me.bindingNavigator.PositionItem = Me.bindingNavigatorPositionItem
		Me.bindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
		Me.bindingNavigator.Size = New System.Drawing.Size(487, 25)
		Me.bindingNavigator.TabIndex = 15
		Me.bindingNavigator.Text = "bindingNavigator1"
		'
		'bindingNavigatorAdd
		'
		Me.bindingNavigatorAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.bindingNavigatorAdd.Enabled = false
		Me.bindingNavigatorAdd.Image = CType(resources.GetObject("bindingNavigatorAdd.Image"),System.Drawing.Image)
		Me.bindingNavigatorAdd.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.bindingNavigatorAdd.Name = "bindingNavigatorAdd"
		Me.bindingNavigatorAdd.Size = New System.Drawing.Size(23, 22)
		Me.bindingNavigatorAdd.Text = "Add New Entry"
		AddHandler Me.bindingNavigatorAdd.Click, AddressOf Me.BindingNavigatorAddClick
		'
		'bindingNavigatorCountItem
		'
		Me.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem"
		Me.bindingNavigatorCountItem.Size = New System.Drawing.Size(36, 22)
		Me.bindingNavigatorCountItem.Text = "of {0}"
		Me.bindingNavigatorCountItem.ToolTipText = "Total number of items"
		'
		'bindingNavigatorDelete
		'
		Me.bindingNavigatorDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.bindingNavigatorDelete.Image = CType(resources.GetObject("bindingNavigatorDelete.Image"),System.Drawing.Image)
		Me.bindingNavigatorDelete.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.bindingNavigatorDelete.Name = "bindingNavigatorDelete"
		Me.bindingNavigatorDelete.Size = New System.Drawing.Size(23, 22)
		Me.bindingNavigatorDelete.Text = "Delete Entry"
		AddHandler Me.bindingNavigatorDelete.Click, AddressOf Me.ForwardDeleteClick
		'
		'bindingNavigatorMoveFirstItem
		'
		Me.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.bindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("bindingNavigatorMoveFirstItem.Image"),System.Drawing.Image)
		Me.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem"
		Me.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true
		Me.bindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
		Me.bindingNavigatorMoveFirstItem.Text = "Move first"
		AddHandler Me.bindingNavigatorMoveFirstItem.Click, AddressOf Me.ForwardClick
		'
		'bindingNavigatorMovePreviousItem
		'
		Me.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.bindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("bindingNavigatorMovePreviousItem.Image"),System.Drawing.Image)
		Me.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem"
		Me.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true
		Me.bindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
		Me.bindingNavigatorMovePreviousItem.Text = "Move previous"
		AddHandler Me.bindingNavigatorMovePreviousItem.Click, AddressOf Me.ForwardClick
		'
		'bindingNavigatorSeparator
		'
		Me.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator"
		Me.bindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
		'
		'bindingNavigatorPositionItem
		'
		Me.bindingNavigatorPositionItem.AccessibleName = "Position"
		Me.bindingNavigatorPositionItem.AutoSize = false
		Me.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem"
		Me.bindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
		Me.bindingNavigatorPositionItem.Text = "0"
		Me.bindingNavigatorPositionItem.ToolTipText = "Current position"
		'
		'bindingNavigatorSeparator1
		'
		Me.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1"
		Me.bindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'bindingNavigatorMoveNextItem
		'
		Me.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.bindingNavigatorMoveNextItem.Image = CType(resources.GetObject("bindingNavigatorMoveNextItem.Image"),System.Drawing.Image)
		Me.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem"
		Me.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true
		Me.bindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
		Me.bindingNavigatorMoveNextItem.Text = "Move next"
		AddHandler Me.bindingNavigatorMoveNextItem.Click, AddressOf Me.ForwardClick
		'
		'bindingNavigatorMoveLastItem
		'
		Me.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.bindingNavigatorMoveLastItem.Image = CType(resources.GetObject("bindingNavigatorMoveLastItem.Image"),System.Drawing.Image)
		Me.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem"
		Me.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true
		Me.bindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
		Me.bindingNavigatorMoveLastItem.Text = "Move last"
		AddHandler Me.bindingNavigatorMoveLastItem.Click, AddressOf Me.ForwardClick
		'
		'bindingNavigatorSeparator2
		'
		Me.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2"
		Me.bindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
		'
		'groupBox2
		'
		Me.groupBox2.Controls.Add(Me.buttonSubCategories)
		Me.groupBox2.Controls.Add(Me.buttonCategories)
		Me.groupBox2.Controls.Add(Me.label2)
		Me.groupBox2.Controls.Add(Me.label1)
		Me.groupBox2.Controls.Add(Me.dropBoxSubCategory)
		Me.groupBox2.Controls.Add(Me.dropBoxCategory)
		Me.groupBox2.Location = New System.Drawing.Point(3, 217)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(481, 75)
		Me.groupBox2.TabIndex = 17
		Me.groupBox2.TabStop = false
		Me.groupBox2.Text = "Categories"
		'
		'buttonSubCategories
		'
		Me.buttonSubCategories.Enabled = false
		Me.buttonSubCategories.Location = New System.Drawing.Point(445, 43)
		Me.buttonSubCategories.Name = "buttonSubCategories"
		Me.buttonSubCategories.Size = New System.Drawing.Size(24, 21)
		Me.buttonSubCategories.TabIndex = 5
		Me.buttonSubCategories.Text = "..."
		Me.buttonSubCategories.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.buttonSubCategories.UseVisualStyleBackColor = true
		'
		'buttonCategories
		'
		Me.buttonCategories.Location = New System.Drawing.Point(445, 16)
		Me.buttonCategories.Name = "buttonCategories"
		Me.buttonCategories.Size = New System.Drawing.Size(24, 21)
		Me.buttonCategories.TabIndex = 4
		Me.buttonCategories.Text = "..."
		Me.buttonCategories.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.buttonCategories.UseVisualStyleBackColor = true
		AddHandler Me.buttonCategories.Click, AddressOf Me.ButtonCategoriesClick
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(6, 42)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(83, 21)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Sub Category"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(6, 16)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(83, 21)
		Me.label1.TabIndex = 2
		Me.label1.Text = "Main Category"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'dropBoxSubCategory
		'
		Me.dropBoxSubCategory.AutoCompleteCustomSource.AddRange(New String() {"<new>"})
		Me.dropBoxSubCategory.Enabled = false
		Me.dropBoxSubCategory.FormattingEnabled = true
		Me.dropBoxSubCategory.Location = New System.Drawing.Point(103, 43)
		Me.dropBoxSubCategory.Name = "dropBoxSubCategory"
		Me.dropBoxSubCategory.Size = New System.Drawing.Size(336, 21)
		Me.dropBoxSubCategory.TabIndex = 1
		'
		'dropBoxCategory
		'
		Me.dropBoxCategory.FormattingEnabled = true
		Me.dropBoxCategory.Location = New System.Drawing.Point(103, 16)
		Me.dropBoxCategory.MaxDropDownItems = 10
		Me.dropBoxCategory.Name = "dropBoxCategory"
		Me.dropBoxCategory.Size = New System.Drawing.Size(336, 21)
		Me.dropBoxCategory.Sorted = true
		Me.dropBoxCategory.TabIndex = 0
		AddHandler Me.dropBoxCategory.SelectedIndexChanged, AddressOf Me.DropBoxCategorySelectedIndexChanged
		AddHandler Me.dropBoxCategory.MouseDown, AddressOf Me.DropBoxCategoryMouseDoubleClick
		'
		'DBUpdaterInfoControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.bindingNavigator)
		Me.Name = "DBUpdaterInfoControl"
		Me.Size = New System.Drawing.Size(487, 299)
		Me.groupBox1.ResumeLayout(false)
		Me.groupBox1.PerformLayout
		CType(Me.bindingNavigator,System.ComponentModel.ISupportInitialize).EndInit
		Me.bindingNavigator.ResumeLayout(false)
		Me.bindingNavigator.PerformLayout
		Me.groupBox2.ResumeLayout(false)
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private bindingNavigatorAdd As System.Windows.Forms.ToolStripButton
	Private bindingNavigatorDelete As System.Windows.Forms.ToolStripButton
	Private buttonNotes As System.Windows.Forms.Button
	Private textBoxSubCategory As System.Windows.Forms.TextBox
	Private buttonCategories As System.Windows.Forms.Button
	Private buttonSubCategories As System.Windows.Forms.Button
	Private dropBoxSubCategory As System.Windows.Forms.ComboBox
	Private dropBoxCategory As System.Windows.Forms.ComboBox
	Private textBoxCategories As System.Windows.Forms.TextBox
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private textBoxID As System.Windows.Forms.TextBox
	Private bindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private bindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
	Private bindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
	Private bindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private bindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
	Private bindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
	Private bindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
	Private bindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
	Private bindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
	Private bindingNavigator As System.Windows.Forms.BindingNavigator
	Private label3 As System.Windows.Forms.Label
	Private textBoxDate As System.Windows.Forms.TextBox
	Private label6 As System.Windows.Forms.Label
	Private textBoxOut As System.Windows.Forms.TextBox
	Private label7 As System.Windows.Forms.Label
	Private textBoxBank As System.Windows.Forms.TextBox
	Private label8 As System.Windows.Forms.Label
	Private richTextBoxNote As System.Windows.Forms.RichTextBox
	Private label4 As System.Windows.Forms.Label
	Private textBoxTransaction As System.Windows.Forms.TextBox
	Private groupBox1 As System.Windows.Forms.GroupBox
End Class
