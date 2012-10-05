'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 18/09/2012
' Time: 5:42 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System.Collections.Specialized

Public Partial Class BankIdentifierDateForm
	
	' For doing a check on this and seeing if there is an automatic way I can do it
	Private m_initial As Boolean
	
	Public Sub New(str As String)
		
		Me.InitializeComponent()
		
		' Set the first item in the combo boxes
		comboBox1.SelectedIndex = 0
		comboBox2.SelectedIndex = 0
		comboBox3.SelectedIndex = 0
		
		' Set the example
		labelExample.Text = str.Trim
		
	End Sub
	
	Public Overloads Function ShowDialog() As DialogResult
		
		' Set the probably delim
		GuessDelim(labelExample.Text)
		
		' Guess at the dates
		GuessDate(labelExample.Text)
		
		' Set the m_initial
		m_initial = True
		
		' Try to see if the app identified the date and delim without user intervention
		ButtonOkClick(Me, Nothing)
		
		' UnSet the m_initial
		m_initial = False
		
		If Me.DialogResult = DialogResult.OK Then
			
			Return Me.DialogResult
			
		End If
		
		Return MyBase.ShowDialog()
		
	End Function
	
	Public Function GetDateFormat() As String
		
		Dim str As String = comboBox1.SelectedItem.ToString + "." + comboBox2.SelectedItem.ToString + "." + comboBox3.SelectedItem.ToString
		Return comboBox1.SelectedItem.ToString + "." + comboBox2.SelectedItem.ToString + "." + comboBox3.SelectedItem.ToString
		
	End Function
	
	Public Function GetDateDelim() As String
		
		Dim str As String = textBoxDelim.Text.Trim
		Return textBoxDelim.Text.Trim
		
	End Function
	
	Sub ButtonOkClick(sender As Object, e As EventArgs)
		
		' Check to make sure there is something in the delim
		If Not textBoxDelim.Text.Length > 0 Then
			
			If Not m_initial Then
				MessageBox.Show("Ensure you have a delimitator, it's the character between the dates that seperate them!", "Choose Please!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
			End If
			
			Exit Sub
				
		End If
		
		Dim ct() As Integer = {0,0,0}
		Dim cb() As String = { comboBox1.SelectedItem.ToString,comboBox2.SelectedItem.ToString,comboBox3.SelectedItem.ToString}
		
		For Each item As String In cb
			
			Select Case item.Trim
				Case "y"
					ct(0) += 1
				Case "m"
					ct(1) += 1
				Case "d"
					ct(2) += 1
			End Select
			
		Next
		
		If ct(0) > 1 Or ct(1) > 1 Or ct(2) > 1 Then
			
			If Not m_initial Then
				MessageBox.Show("Ensure you have selected a proper date format!", "Choose Please!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
			End If
			
			Exit Sub
			
		End If
		
		Me.DialogResult = DialogResult.OK
		
	End Sub
	
	Private Sub GuessDelim(str As String)
		
		For Each item As Char In str
			
			If Not Char.IsDigit(item) Then
				textBoxDelim.Text = CStr(item)
				Exit Sub
			End If
			
		Next
		
		textBoxDelim.Text = ""
		
	End Sub
	
	Private Sub GuessDate(str As String)
		
		Dim guess As String = textBoxDelim.Text
		
		If guess = "" Then
			Exit Sub
		End If
		
		Dim counter As Integer = -1
		
		For Each item As String In Split(Str, guess)
			
			counter += 1
			
			Select Case counter
					
				Case 0
					comboBox1.SelectedIndex = comboBox1.Items.IndexOf(DoGuess(item))
				Case 1
					comboBox2.SelectedIndex = comboBox2.Items.IndexOf(DoGuess(item))
				Case 2
					comboBox3.SelectedIndex = comboBox3.Items.IndexOf(DoGuess(item))
					
			End Select
						
		Next
		
	End Sub
	
	Private Function DoGuess(str As String) As String
		
		If CInt(str) >= 1900 Then
			Return "y"
			
		ElseIf CInt(str) >= 12 Then
			Return "d"
			
		Else
			Return "m"
			
		End If
		
		Return "y"
		
	End Function
	
End Class
