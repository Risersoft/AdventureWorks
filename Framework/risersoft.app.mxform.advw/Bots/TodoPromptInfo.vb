Imports risersoft.shared.portable.Proxies

Public Class TodoPromptInfo
    Public PIDUnitID As Integer
    Public Title As String
    Public AssignUsers As New List(Of RSUser)
    Public DueOn As DateTime
End Class



'KakaBot:
'o   / todo @xxx task ABC for this
'o   / todo list
'o	List And Task creation will send multiple messages With edit/delete buttons For suggested actions
'o   / todo: Use form input With validation For add/edit todo
'o	Bot will check If dialog Is active: Take all messages As input, Else only / ones
