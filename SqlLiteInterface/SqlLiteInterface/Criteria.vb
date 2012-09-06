'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 20/11/2011
' Time: 8:26 PM
' 
'
Public Class Criteria
	
	Private m_Name As String
	Private m_Value As String
	
	Public Sub New(ByVal name As String, ByVal value As String)
		
		Me.Name = name
		Me.Value = value
		
	End Sub
	
	Public Property Name() As String
		Get
			Return m_Name
		End Get
		Set
			m_Name = value
		End Set
	End Property
	
	Public Property Value() As String
		Get
			Return m_Value
		End Get
		Set
			m_Value = value
		End Set
	End Property
	
End Class
