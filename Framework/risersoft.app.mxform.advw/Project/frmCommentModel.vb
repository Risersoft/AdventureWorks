Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmCommentModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()

        Dim vlist As New clsValueList
        vlist.Add("PIDUnitWorkItemID", "WorkItem")
        vlist.Add("WBSElementID", "WBSElement")
        vlist.Add("CalendarEventID", "CalendarEvent")
        vlist.Add("ForumPostID", "ForumPost")
        Me.ValueLists.Add("PIDField", vlist)
        Me.AddLookupField("PIDField", "PIDField")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from UserComment Where UserCommentID = " & prepIDX
        Me.InitData(sql, "IDValue,IDField", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("pidfield") = Me.vBag("idfield")
            myRow("pidvalue") = Me.vBag("idvalue")
        End If

        Dim rParent = myFuncs.GetParentRow(myContext, myRow("pidfield"), myRow("pidvalue"))
        myUtils.AddTable(Me.dsCombo, rParent.Table, "Parent")

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function


    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If myUtils.NullNot(myRow("Dated")) Then Me.AddError("Dated", "Please Select Comment Date")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then

            If frmMode = EnumfrmMode.acEditM Then
                Dim sql As String = "select UserCommentID from UserComment where dbo.fncWorkItemUser('" & myContext.Police.UniqueUserID & "', '00000000-0000-0000-0000-000000000000', UserComment.CreatedByID, '00000000-0000-0000-0000-000000000000', NULL, '00000000-0000-0000-0000-000000000000', NULL)= 1"
                Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count = 0 Then Me.AddError("", "Don't Have Permission to Save")
            End If

            If Me.CanSave Then
                Dim CommentDescrip As String = " Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy") & " for: " & myUtils.cStrTN(myRow("PIDField"))
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "UserCommentID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                    frmMode = EnumfrmMode.acEditM

                    frmIDX = myRow("UserCommentID")
                    myContext.Provider.dbConn.CommitTransaction(CommentDescrip, frmIDX)
                    VSave = True
                Catch ex As Exception
                    myContext.Provider.dbConn.RollBackTransaction(CommentDescrip, ex.Message)
                    Me.AddError("", ex.Message)
                End Try
            End If
        End If
    End Function

End Class

