Public NotInheritable Class Program

    Public Shared Function Main(args() As String) As Integer
        Dim w As New Widget(Of Integer)
        w.ToList()
        Console.WriteLine("Completed")
        Return 0
    End Function

End Class

Public Class Widget(Of T)
    Implements IWidget(Of T)

    Public ReadOnly Property Count As Integer Implements IReadOnlyCollection(Of T).Count

    Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Return Enumerable.Empty(Of T)().GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function

End Class

Public Interface IWidget(Of T)
    Inherits IReadOnlyCollection(Of T)

End Interface
