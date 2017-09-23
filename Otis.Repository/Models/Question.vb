Public Class Question
    Property QuestionId As Int32
    Property QuestionText As String
    Property CategoryId As Int32

    'Navigation Properties
    Overridable Property Category As Category
    Overridable Property Answers As ICollection(Of Answer)
End Class
