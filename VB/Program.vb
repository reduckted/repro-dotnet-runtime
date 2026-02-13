Imports Effort.Provider
Imports System.Data.Common
Imports System.Data.Entity

Public NotInheritable Class Program

    Public Shared Function Main(args() As String) As Integer
        Using context = CreateContext()
            context.Alphas.ToList()
        End Using

        Widget.Create(Enumerable.Empty(Of Object)).ToList()

        Console.WriteLine("Completed")

        Return 0
    End Function

    Private Shared Function CreateContext() As Context
        Dim builder As EffortConnectionStringBuilder


        EffortProviderConfiguration.RegisterProvider()

        builder = New EffortConnectionStringBuilder With {
            .InstanceId = Guid.NewGuid().ToString()
        }

        Return New Context(New EffortConnection With {.ConnectionString = builder.ConnectionString})
    End Function
End Class

Public NotInheritable Class Widget

    Public Shared Function Create(Of T)(values As IEnumerable(Of T)) As IWidget(Of T)
        Return New Implementation(Of T)(values)
    End Function


    Private Class Implementation(Of T)
        Implements IWidget(Of T)

        Private ReadOnly _values As List(Of T)

        Public Sub New(values As IEnumerable(Of T))
            _values = values.ToList()
        End Sub

        Public ReadOnly Property Count As Integer Implements IReadOnlyCollection(Of T).Count
            Get
                Return _values.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(index As Integer) As T Implements IReadOnlyList(Of T).Item
            Get
                Return _values(index)
            End Get
        End Property

        Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            Return _values.GetEnumerator()
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

    End Class
End Class

Public Interface IWidget(Of T)
    Inherits IReadOnlyList(Of T)

End Interface

Public Class Context
    Inherits DbContext

    Public Sub New(connection As DbConnection)
        MyBase.New(connection, True)
    End Sub

    Public Overridable Property Alphas As DbSet(Of Alpha)

    Public Overridable Property Betas As DbSet(Of Beta)

End Class

Public Class Alpha

    Public Overridable Property ID As Integer

End Class

Public Class Beta

    Public Overridable Property ID As Integer

    Public Overridable Property Alphas As ICollection(Of Alpha)

End Class