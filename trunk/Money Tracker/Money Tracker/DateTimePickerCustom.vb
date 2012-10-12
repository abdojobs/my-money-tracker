'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 11/10/2012
' Time: 10:20 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class DateTimePickerCustom
	Inherits DateTimePicker
	
	' Some constants to help me pull up ONLY the montly calendar
	Const DTM_FIRST As Integer = 4096
	Const DTM_GETMONTHCAL As Integer = (DTM_FIRST + 8)
	Const MCM_FIRST As Integer = 4096
	Const MCM_SETCURRENTVIEW As Integer = (MCM_FIRST + 32)
	
	' The SendMessage Function Declaration
	Private Declare Auto Function SendMessage Lib "user32.dll" ( _
	        ByVal hWnd As IntPtr, _
	        ByVal Msg As Int32, _
	        ByVal wParam As IntPtr, _
	        ByVal lParam As IntPtr _
        ) As IntPtr
	
	' This is in case we are using the daily calendar instead of the monthly
	Dim m_monthly As Boolean
	
	' New obviously!
	Public Sub New()
		
		MyBase.New()
		m_monthly = False
		
	End Sub
	
	' Property to set the monthly variable
	Public Property Monthly() As Boolean
		Get
			Return m_monthly
		End Get
		Set
			m_monthly = value
		End Set
	End Property
	
	' Overides the ValueChanged
	Public Sub TimePicker_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.ValueChanged
		
		If m_monthly Then
			SendKeys.SendWait("%{UP}")
		End If
		
    End Sub
    
    ' Overides the DropDown
	Protected Overloads Overrides Sub OnDropDown(ByVal e As EventArgs)
		
		If m_monthly Then
			SendMessage(SendMessage(Me.Handle, DTM_GETMONTHCAL, New IntPtr(0), New IntPtr(0)), MCM_SETCURRENTVIEW, New IntPtr(0), New IntPtr(1))
		End If
		
        MyBase.OnDropDown(e)
        
	End Sub
	
End Class
