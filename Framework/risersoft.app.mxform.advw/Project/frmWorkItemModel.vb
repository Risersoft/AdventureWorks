Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.shared.cloud
Imports risersoft.app.mxengg

<DataContract>
Public Class frmWorkItemModel
    Inherits clsFormDataModel
    Dim myVueNotify As clsViewModel

    Protected Overrides Sub InitViews()

        myView = Me.GetViewModel("Assign")
        myVueNotify = Me.GetViewModel("Notify")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select UserID, UserName,Entityname from genlistuser() where indesignlist=1 and isdeleted=0 order by UserName"
        Me.AddLookupField("AssignUserID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "User").TableName)

        sql = "Select UserID, UserName from genlistuser() where indesignlist=1 and isdeleted=0 order by UserName"
        Me.AddLookupField("ReviewUserID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Review").TableName)

        sql = "Select UserID, UserName from genlistuser() where indesignlist=1 and isdeleted=0 order by UserName"
        Me.AddLookupField("ReporterUserID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Report").TableName)

        sql = myFuncsBase.CodeWordSQL("Flag", "VisibleFlag", "")
        Me.AddLookupField("VisibleFlag", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "VisibleFlag").TableName)

        sql = myFuncsBase.CodeWordSQL("WorkItem", "WorkItemType", "")
        Me.AddLookupField("WorkItemType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "WorkItemType").TableName)

        sql = myFuncsBase.CodeWordSQL("Status", "StatusType", "")
        Me.AddLookupField("Status", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Status").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from PIDUnitWorkItem Where PIDUnitWorkItemID = " & prepIDX
        Me.InitData(sql, "PIDUnitID", oView, prepMode, prepIDX, strXML)

        sql = myFuncs.GenerateUserSetIdSql(myContext, myUtils.cValTN(myRow("AssignUserSetID")))
        myView.MainGrid.QuickConf(sql, True, "1", True)
        Me.myView.MainGrid.PrepEdit("<BAND INDEX=""0"" IDFIELD=""UserID""/>", EnumEditType.acCommandOnly)

        sql = myFuncs.GenerateUserSetIdSql(myContext, myUtils.cValTN(myRow("NotifyUserSetID")))
        myVueNotify.MainGrid.QuickConf(sql, True, "1", True)
        Me.myVueNotify.MainGrid.PrepEdit("<BAND INDEX=""0"" IDFIELD=""UserID""/>", EnumEditType.acCommandOnly)

        If prepIDX <> 0 Then
            Dim sql2 = "select * from prjlistusercomment() where pidunitworkitemid = " & prepIDX & " and IsDeleted IS NULL order by Dated desc"
            Me.AddDataSet("comment", sql2)
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        'If Me.SelectedRow("Title") Is Nothing Then Me.AddError("Title", "Please Enter title")
        If Me.SelectedRow("VisibleFlag") Is Nothing Then Me.AddError("VisibleFlag", "Please Select VisibleFlag")
        If Me.SelectedRow("WorkItemType") Is Nothing Then Me.AddError("WorkItemType", "Please Select WorkItem Type")
        If Me.SelectedRow("Status") Is Nothing Then Me.AddError("Status", "Please Select Status")
        'If Me.SelectedRow("AssignUserID") Is Nothing Then Me.AddError("AssignUserID", "Please Select Assign User")
        Return Me.CanSave
    End Function

    Public Function SaveTodoInfo(info As TodoPromptInfo) As clsProcOutput
        Dim oView As New clsViewModel(myContext), oRet As New clsProcOutput
        If Me.PrepForm(oView, EnumfrmMode.acAddM, "") Then
            myRow("title") = info.Title
            myRow("dueon") = info.DueOn
            If info.AssignUsers.Count > 1 Then
                For Each obj In info.AssignUsers
                    Dim nr = myContext.Tables.AddNewRow(myView.MainGrid.myDS.Tables(0))
                    nr("userid") = obj.Id
                    nr("username") = obj.FullName
                Next
            Else
                myRow("assignuserid") = info.AssignUsers(0).Id
            End If
            myRow("pidunitid") = info.PIDUnitID
            myRow("visibleflag") = "INT"
            myRow("workitemtype") = "TASK"
            myRow("STATUS") = "OPEN"
            If Me.Validate Then
                If Me.VSave Then
                    oRet.Message = "Saved"
                Else
                    oRet.AddError("Cannot Save")
                End If
            Else
                oRet.AddError(Me.ErrorMessage)
            End If
        Else
            oRet.AddError("Cannot Get Model")
        End If
        Return oRet
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then

            myRow("contenthtml") = BucketUtility.TryBase64Decode(myUtils.cStrTN(myRow("contenthtml")))
            myRow("content") = BucketUtility.TryBase64Decode(myUtils.cStrTN(myRow("content")))

            If myRow("Status") = "CLOSE" Then
                If myUtils.NullNot(myRow("completedon")) Then myRow("completedon") = Now
            Else
                myRow("completedon") = DBNull.Value
            End If

            If frmMode = EnumfrmMode.acEditM Then
                Dim sql As String = "select PIDUnitWorkItemID from PIDUnitWorkItem left Join userset on PIDUnitWorkItem.AssignUserSetID = Userset.UserSetID where dbo.fncWorkItemUser('" & myContext.Police.UniqueUserID & "', AssignUserID, PIDUnitWorkItem.CreatedByID, ReviewUserID, PIDUnitID, UserSet.UserSetList, VisibleFlag)= 1"
                Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count = 0 Then Me.AddError("", "Don't Have Permission to Save")
            End If

            If Me.CanSave Then
                Dim Sql As String = "Select PidUnitID, PIDInfo, DescripWO from Pidunit where PidUnitID = " & myRow("PidUnitID")
                Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                Dim WorkItemDescrip As String = " WorkItem: " & myUtils.cStrTN(myRow("Title")) & " for: " & myUtils.cStrTN(ds.Tables(0).Rows(0)("PidInfo")) & ", " & myUtils.cStrTN(ds.Tables(0).Rows(0)("DescripWO"))
                Try
                    'Assign To User(s) List
                    Dim AssignUserSetID = myFuncs.GetUserSetID(myContext, myView.MainGrid.myDS.Tables(0))
                    If AssignUserSetID.HasValue Then
                        'myView
                        myRow("AssignUserSetID") = AssignUserSetID.Value
                    Else
                        myRow("AssignUserSetID") = DBNull.Value
                    End If

                    'Notify User(s) List
                    Dim NotifyUserSetID = myFuncs.GetUserSetID(myContext, myVueNotify.MainGrid.myDS.Tables(0))
                    If NotifyUserSetID.HasValue Then
                        myRow("NotifyUserSetID") = NotifyUserSetID.Value
                    Else
                        myRow("NotifyUserSetID") = DBNull.Value
                    End If

                    Dim UserIDCSV As String = myUtils.cStrTN(myFuncs.GetUserIDCSV(myContext, 0, myView.MainGrid.myDS.Tables(0), myVueNotify.MainGrid.myDS.Tables(0), myUtils.cStrTN(myRow("AssignUserID")), myUtils.cStrTN(myRow("ReporterUserID")), myUtils.cStrTN(myRow("ReviewUserID"))))
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PIDUnitWorkItemID", frmIDX, UserIDCSV)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)

                    frmMode = EnumfrmMode.acEditM
                    frmIDX = myRow("PIDUnitWorkItemID")
                    myContext.Provider.dbConn.CommitTransaction(WorkItemDescrip, frmIDX)
                    AzureNotifyProvider.SendNotification(myContext, WorkItemDescrip)
                    VSave = True
                Catch ex As Exception
                    myContext.Provider.dbConn.RollBackTransaction(WorkItemDescrip, ex.Message)
                    Me.AddError("", ex.Message)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower

                Case "assignuser"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("select UserID,FullName,UserName from genlistuser() where indesignlist=1 and isdeleted=0 and UserID not in ('@AssignUserID')", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Model.Mode = EnumViewMode.acSelectM : Model.MultiSelect = False
                    Model.MainGrid.QuickConf(sql, True, "1-1")
                    Model.MainGrid.PrepEdit("", EnumEditType.acEditOnly)

                Case "reviewuser"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("select UserID,FullName,UserName from genlistuser() where indesignlist=1 and isdeleted=0 and UserID not in ('@ReviewUserID')", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Model.Mode = EnumViewMode.acSelectM : Model.MultiSelect = False
                    Model.MainGrid.QuickConf(sql, True, "1-1")
                    Model.MainGrid.PrepEdit("", EnumEditType.acEditOnly)

                Case "reporteruser"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("select UserID,FullName,UserName from genlistuser() where indesignlist=1 and isdeleted=0 and UserID not in ('@ReporterUserID')", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Model.Mode = EnumViewMode.acSelectM : Model.MultiSelect = False
                    Model.MainGrid.QuickConf(sql, True, "1-1")
                    Model.MainGrid.PrepEdit("", EnumEditType.acEditOnly)

                Case "userset"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("select UserID,FullName,UserName from genlistuser() where indesignlist=1 and isdeleted=0 and UserID not in (@UserIDcsv)", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Model.Mode = EnumViewMode.acSelectM : Model.MultiSelect = True
                    Model.MainGrid.QuickConf(sql, True, "1-1")
                    Model.MainGrid.PrepEdit("", EnumEditType.acEditOnly)

            End Select
        End If
        Return Model
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput

        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Dim sql2 = String.Empty
            Dim WorkItemId As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ParentID", Params))

            Select Case dataKey.Trim.ToLower
                Case "save"
                    Dim CommentPlain = BucketUtility.TryBase64Decode(myUtils.cStrTN(myContext.SQL.ParamValue("@commentplain", Params)))
                    Dim CommentHTML = BucketUtility.TryBase64Decode(myUtils.cStrTN(myContext.SQL.ParamValue("@commenthtml", Params)))

                    sql2 = "select * from usercomment where 0=1"
                    Dim dt2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)
                    Dim nr As DataRow = myContext.Tables.AddNewRow(dt2)
                    nr("pidfield") = "pidunitworkitemid"
                    nr("pidvalue") = WorkItemId
                    nr("comment") = CommentPlain
                    nr("commenthtml") = CommentHTML
                    nr("dated") = TaskProviderFactory.FindLocalTime
                    myContext.Provider.objSQLHelper.SaveResults(dt2, sql2)

                Case "delete"
                    Dim UserCommentID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@usercommentid", Params))
                    sql2 = "Update usercomment set IsDeleted = 1 where UserCommentID=" & myUtils.cValTN(UserCommentID)
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql2)
            End Select

            sql2 = "select * from prjlistusercomment() where pidunitworkitemid = " & WorkItemId & " and IsDeleted IS NULL order by Dated desc"
            oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2)

        End If
        Return oRet
    End Function

    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            If AcceptWarning Then
                Select Case EntityKey.Trim.ToLower
                    Case "pidunitworkitem"
                        Dim sql As String = "Select * from PIDUnitWorkItem where PIDUnitWorkItemID =" & ID
                        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                        If dt.Rows.Count > 0 Then
                            Dim sql1 As String = "delete from PIDUnitWorkItem where PIDUnitWorkItemID =" & ID
                            myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql1)
                        End If

                End Select
            ElseIf oRet.WarningCount = 0 Then
                oRet.AddWarning("Are you sure ?")
            End If
        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

End Class
