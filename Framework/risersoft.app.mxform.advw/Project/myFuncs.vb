Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.IO
Imports risersoft.shared.cloud
Imports System.Configuration
Imports risersoft.shared.dal

Public Class myFuncs
    Inherits myFuncsBase

    Public Shared Function GetParentRow(context As IProviderContext, pIDField As String, pIDValue As Integer) As DataRow
        Dim sql As String
        Select Case pIDField.Trim.ToLower
            Case "leadid"
                sql = "select * from crmlistLead() where Leadid=" & pIDValue

            Case "salescaseid"
                sql = "select * from crmlistSalesCase() where SalesCaseid=" & pIDValue

            Case "opportunityid"
                sql = "select * from crmListOpportunity() where Opportunityid=" & pIDValue

            Case "campaignid"
                sql = "select * from crmListCampaign() where Campaignid=" & pIDValue

            Case "customerid"
                sql = "select * from slsListCustomer() where Customerid=" & pIDValue

            Case "personid"
                sql = "select * from genListPersParty() where Personid=" & pIDValue

            Case "pidunitworkitemid"
                sql = "select * from prjListPIDUnitWorkItem() where PIDUnitWorkItemID=" & pIDValue

            Case "wbselementid"
                sql = "select * from prjListWBSElement() where WBSElementID=" & pIDValue

            Case "pidunittimelogid"
                sql = "select * from prjListPIDUnitTimeLog() where PIDUnitTimeLogID=" & pIDValue

            Case "calendareventid"
                sql = "select * from prjListCalendarEvent() where CalendarEventID=" & pIDValue

            Case "forumpostid"
                sql = "select * from prjListForumPost() where ForumPostID=" & pIDValue

        End Select
        Dim dt1 As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Return dt1.Rows(0)
    End Function

    Public Shared Function GetUserSetID(context As IProviderContext, UserIDCSV As String, UserNameCSV As String) As Integer?
        If String.IsNullOrEmpty(UserIDCSV) OrElse myUtils.IsInList(UserIDCSV, "0") Then
            Return Nothing
        Else
            Dim Sql As String = "select * from UserSet where usersetlist='" & UserIDCSV & "'"
            Dim dt2 As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0)

            Dim nr As DataRow
            If dt2.Rows.Count > 0 Then
                nr = dt2.Rows(0)
            Else
                nr = context.Tables.AddNewRow(dt2)
                nr("UserSetList") = UserIDCSV
                nr("UserSetName") = UserNameCSV
                context.Provider.objSQLHelper.SaveResults(dt2, "Select UserSetID,UserSetList,UserSetName from UserSet")
            End If
            Return nr("usersetid")
        End If

    End Function
    Public Shared Function GenerateUserSetIdSql(context As IProviderContext, UserSetId As Integer) As String
        Dim dic As New clsCollecString(Of String)
        dic.Add("Team", "select * from UserSet where UserSetID = " & UserSetId & "")
        Dim ds As DataSet = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        Dim str1 = Replace(myUtils.MakeCSV(ds.Tables("Team").Select(), "UserSetList", ", ", "'00000000-0000-0000-0000-000000000000'", True), ",", "','")
        Dim Sql = "select UserID, UserName from genListUser() where indesignlist=1 and isdeleted=0 and UserID in (" & str1 & ")"
        Return Sql
    End Function
    Public Shared Function GetUserSetID(context As IProviderContext, dtUsers As DataTable) As Integer?
        dtUsers.AcceptChanges()
        Dim str1 As String = myUtils.MakeCSV(dtUsers.Select("", "UserID"), "UserID")
        Dim str2 As String = myUtils.MakeCSV(dtUsers.Select("", "UserName"), "UserName")
        Return myFuncs.GetUserSetID(context, str1, str2)
    End Function
    Public Shared Function GetUserSetID(context As IProviderContext, Selected As List(Of String), Resources As List(Of clsTaskResource)) As Integer?
        Dim str1 As String = myUtils.MakeCSV(",", Selected.ToArray)
        Dim str2 As String = myUtils.MakeCSV(",", Resources.Select(Function(x) x.Name).ToArray)
        Return myFuncs.GetUserSetID(context, str1, str2)
    End Function

    Public Shared Function GetUserIDCSV(context As IProviderContext, TeamID As Integer, dtUsers1 As DataTable, dtUsers2 As DataTable, ParamArray arrUser() As String) As String
        Dim str1 As String = If(dtUsers1 Is Nothing, "", myUtils.MakeCSV(dtUsers1.Select("", "UserID"), "UserID", ",", "00000000-0000-0000-0000-000000000000", False))
        Dim str2 As String = If(dtUsers2 Is Nothing, "", myUtils.MakeCSV(dtUsers2.Select("", "UserID"), "UserID", ",", "00000000-0000-0000-0000-000000000000", False))
        Dim str3 As String = myUtils.MakeCSV(",", arrUser)
        Dim Sql As String = "select userid from teammembership where teamid =" & TeamID
        Dim dt2 As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0)
        Dim str4 As String = myUtils.MakeCSV(dt2.Select("", "UserID"), "UserID", ",", "00000000-0000-0000-0000-000000000000", False)

        Dim result As String = myUtils.MakeCSV(",", str1, str2, str3, str4)
        Return result
    End Function

End Class