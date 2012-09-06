'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 20/11/2011
' Time: 8:27 PM
' 
'
Public Class CriteriaCollection
	Implements IEnumerable, IEnumerator
	
	Private iIndex As Integer   		'index of the array
    Private iCount As Integer   		'count of the elements
    Private listofiles() As Criteria 	'array of Criteria objects
	
	Public Sub New()
		
		iCount = -1
		iIndex = -1
		
	End Sub
	
	Public Sub Add(ByVal obj As Criteria)
		
		iCount += 1
		ReDim Preserve listofiles(iCount)
		listofiles(iCount) = obj
		
	End Sub
	
	Public Sub Insert(ByVal index As Integer, ByVal ln As Criteria)
			
		' Read the list from x to iCount into a temp array
		Dim counter as Integer = -1
		Dim amount as Integer = iCount - index
		Dim templist(amount) As Criteria
		
		For x = index To iCount
			counter += 1
			templist(counter) = listofiles(x)
		Next
		
		' Now merge the two
		counter = -1
		iCount += 1
		ReDim listofiles(iCount)
		
		For x = (index + 1) To iCount
			counter += 1
			listofiles(x) = templist(counter)
		Next
		
		listofiles(index) = ln
		
	End Sub
	
	Default Public Property Item(ByVal index As Integer) As Criteria
		Get
			If index < 0 Or index >= listofiles.Length Then
				Exit Property
			End If
			
			Return listofiles(index)
		End Get
		Set(ByVal value As Criteria)
			listofiles(index) = value
		End Set
	End Property
	
    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return CType(Me, IEnumerator)
    End Function
    
    Public ReadOnly Property Current() As Object Implements IEnumerator.Current
        Get
            Return listofiles(iIndex)
        End Get
    End Property
    
    Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
        If iIndex < iCount Then
            iIndex += 1
            MoveNext = True
        Else
            MoveNext = False
        End If
    End Function
    
    Public Sub Reset() Implements IEnumerator.Reset
        iIndex = -1
    End Sub
    
	Public ReadOnly Property Count() As Integer
		Get
			Return iCount + 1
		End Get
	End Property
	
End Class
