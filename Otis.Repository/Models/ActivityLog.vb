Public Class ActivityLog
    Property ActivityLogId As Integer
    Property UserId As String
    Property Activity As String
    Property ActivityDate As DateTime
    Property IsActive As Boolean

    ' Navigation Properties
    Overridable Property User As User
End Class
