'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 17/09/2012
' Time: 7:05 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Data
Imports System.Data.Linq
Imports System.Collections
Imports System.Collections.Specialized
Imports LumenWorks.Framework.IO.Csv

Public Partial Class BankIdentifierForm
	
	Private record As CsvReader
	Private dte As String
	Private delim As String
	
	Public Sub New(ByVal rec As CsvReader)
		
		record = rec
		
		Me.InitializeComponent()
		
		' Draw out the window
		Me.DrawInitial()
		
	End Sub
	
	Sub ComboBoxSelectedIndexChanged(sender As Object, e As EventArgs)
		
		Dim temp As ComboBox = DirectCast(sender, ComboBox)
		
		' Handle an input of a type
		If temp.SelectedItem.ToString.Trim = "<other>" Then
			Dim str As String = InputBox("Input Type", "What type?")
			
			If str.Length > 0 Then
				
				temp.Items.Insert(temp.SelectedIndex, str)
				temp.SelectedIndex = temp.SelectedIndex - 1
				
			End If
			
		' Handle a date
		ElseIf temp.SelectedItem.ToString.Trim = "dates" Then
			
			' Need to get the item number
			Dim num As String = temp.Name.Substring(temp.Name.Length - 1)
			
			' Get the textual date
			Dim thedate As String = DirectCast(Me.Controls.Item(String.Format("label{0}", num)), Label).Text.Trim
			Dim wind As New BankIdentifierDateForm(thedate)
			
			If wind.ShowDialog = DialogResult.OK Then
				
				dte = wind.GetDateFormat()
				delim = wind.GetDateDelim()
				
			Else
				dte = Nothing
				delim = Nothing
				temp.SelectedIndex = 0
				
			End If
			
			wind = Nothing
			
		End If
		
	End Sub
	
	Sub ButtonOkClick(sender As Object, e As EventArgs)
		
		' Check to make sure the bank is filled out
		If textBoxBankName.Text.Length <= 0 Then
					
			MessageBox.Show("Ensure you have selected A bank name!", "Choose Please!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
			Exit Sub
				
		End If
		
		For Each item As ComboBox In Me.Controls.OfType(Of ComboBox)
			
			For Each item2 As ComboBox In Me.Controls.OfType(Of ComboBox)
				
				If Not item.Name = item2.Name Then
					
					If item.Text = item2.Text Then
				
						MessageBox.Show("Ensure you have selected Different Values for each box!", "Choose Please!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
						Exit Sub
				
					End If
					
				End If
			
			Next
			
		Next
		
		Me.DialogResult = DialogResult.OK
		
	End Sub
	
	Sub ButtonNextClick(sender As Object, e As EventArgs)
		
		' Get next record
		record.ReadNextRecord
		
		If record.EndOfStream Then
			Me.Dispose(True)
			Exit Sub
		End If
		
		' Draw again
		Me.ReDrawLabels()
		
	End Sub
	
	Public Function GetName() As String
		
		Return textBoxBankName.Text.Trim
		
	End Function
	
	Public Function GetCols() As String
		
		Dim str As String = Nothing
		
		For Each item As ComboBox In Me.Controls.OfType(Of ComboBox)
			
			str += item.SelectedItem.ToString.Trim + ","
			
		Next 
		
		Return str.Substring(0, str.Length - 1)
		
	End Function
	
	Public Function GetDate() As String
		
		Return dte
		
	End Function
	
	Public Function GetDelim() As String
		
		Return delim
		
	End Function
	
	Private Sub DrawInitial()
		
		' Sets
		dte = Nothing
		delim = Nothing
		
		' Counter for the tab select
		Dim counter As Integer = 0
		
		' How many records do we have
		Dim total As Integer = record.FieldCount - 1
		Dim names(total) As ComboBox
		Dim labels(total) As Label
		
		' Lets get the longest one so we can make them all the same length
		Dim len As Integer = 0
		
		' Want to loop through the record to find out the longest string for the labels to all be the same
		For x = 0 To total
			
			Dim a = record(x)
			
			If record(x).Trim.Length > len Then
				
				len = record(x).Trim.Length
				
			End If
			
		Next x
		
		' General char size
		Dim charsz As Integer = 9
		
		' Widths
		Dim combo_w As Integer = 80
		Dim label_w As Integer = len * charsz
		Dim padding As Integer = 50
		
		' Heights
		Dim h As Integer = 21
		
		' Size of the window ( combo_w + label_w + padding, temp size )
		Dim sz As New Size(combo_w + label_w + padding, 40)
		
		' The current x,y position for textbox
		Dim cbpt As New Point(10, 35)
		
		' The current x,y position for label
		Dim lblpt As New Point(combo_w + 10, 35)
		
		With Me
			
			' Start the drawing process
			.SuspendLayout
			
			' Want to loop through the record to set up all the different things
			For x = 0 To total
				
				' Create a new text box that will allow input of what the label is
				names(x) = New ComboBox
				
				With names(x)

					.FormattingEnabled = True
					.Items.AddRange(New Object() {"name", "out", "in", "out/in", "dates", "<other>"})
					.Location = cbpt
					.Name = String.Format("combobox{0}", x.ToString)
					.Size = New Size(combo_w, h)
					counter += 1
					.TabIndex = counter
					.TabStop = True
					.SelectedIndex = 0
					AddHandler .SelectedIndexChanged, AddressOf Me.ComboBoxSelectedIndexChanged
					
				End With
				
				' Create a new label, to show a value
				labels(x) = New Label
				
				With labels(x)
					
					.Name = String.Format("label{0}", x.ToString)
					.Location = lblpt
					.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
					.Size = New Size(label_w, h)
					
					If record(x).ToString.Trim.Length <= 0 Then
						.Text = "Empty"
					Else
						.Text = Replace(record(x).ToString.Trim, "  ", " ")
					End If
					
					
				End With
				
				.Controls.Add(names(x))
				.Controls.Add(labels(x))
				
				lblpt.Y = lblpt.Y + h + 5
				cbpt.Y = cbpt.Y + h + 5
				sz.Height = sz.Height + h + 5
				
			Next x
			
			' Set the text box for bank name as the start point for tab index
			textBoxBankName.TabIndex = 0
			
			' Fix the size of the Bank Name txt
			textBoxBankName.Size = New Size(combo_w, h)
			
			' Fix the size of the Bank Name lbl
			bankNameLabel.Size = New Size(combo_w, h)
			
			' Position the ok button			
			buttonOk.Location = cbpt
			
			' Set the ok as the second from end point for tab index
			buttonOk.TabIndex = counter + 1
			
			' Position the next button			
			buttonNext.Location = lblpt
			
			' Set the next as the end point for tab index
			buttonNext.TabIndex = counter + 2
			
			' Size of the everything
			.Size = New Size(sz.Width, sz.Height + h + h)
			
			' Done laying out
			.ResumeLayout(False)
			
		End With
		
	End Sub
	
	Private Sub ReDrawLabels()
		
		Dim counter As Integer = -1
		
		For Each item As Label In Me.Controls.OfType(Of Label)
			
			If Not item.Name = "bankNameLabel" Then
				counter += 1
				item.Text = Replace(record(counter).ToString.Trim, "  ", " ")
			End If
			
		Next 
		
	End Sub
	
End Class

