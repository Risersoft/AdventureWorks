Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.shared.cloud
Imports risersoft.app.mxengg

<DataContract>
Public Class frmPostModel
    Inherits clsFormDataModel


    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Notify")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()

        Dim Sql As String = myFuncsBase.CodeWordSQL("Flag", "VisibleFlag", "")
        Me.AddLookupField("VisibleFlag", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "VisibleFlag").TableName)

        Sql = "Select UserID, UserName,Entityname from genlistuser() where indesignlist=1 and isdeleted=0 order by fullname"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "User")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from ForumPost Where ForumPostID = " & prepIDX
        Me.InitData(sql, "PIDUnitID", oView, prepMode, prepIDX, strXML)

        sql = myFuncs.GenerateUserSetIdSql(myContext, myUtils.cValTN(myRow("NotifyUserSetID")))
        myView.MainGrid.QuickConf(sql, True, "1", True)
        Me.myView.MainGrid.PrepEdit("<BAND TABLE=""Users"" IDFIELD=""UserID"" INDEX=""0""><COL KEY=""UserName""/></BAND>", EnumEditType.acCommandOnly)

        If prepIDX <> 0 Then
            Dim sql2 = "select * from prjlistusercomment() where ForumPostID = " & prepIDX & " and IsDeleted IS NULL order by Dated desc"
            Me.AddDataSet("comment", sql2)
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("VisibleFlag") Is Nothing Then Me.AddError("VisibleFlag", "Please Select VisibleFlag")
        If myUtils.cStrTN(myRow("Title")).Trim.Length = 0 Then Me.AddError("Title", "Enter Title")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then

            myRow("contenthtml") = BucketUtility.TryBase64Decode(myUtils.cStrTN(myRow("contenthtml")))
            myRow("content") = BucketUtility.TryBase64Decode(myUtils.cStrTN(myRow("content")))

            If frmMode = EnumfrmMode.acEditM Then
                Dim sql As String = "select ForumPostID from ForumPost where dbo.fncWorkItemUser('" & myContext.Police.UniqueUserID & "', '00000000-0000-0000-0000-000000000000', ForumPost.CreatedByID, '00000000-0000-0000-0000-000000000000', PIDUnitID, '00000000-0000-0000-0000-000000000000', NULL)= 1"
                Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count = 0 Then Me.AddError("", "Don't Have Permission to Save")
            End If

            If Me.CanSave Then
                Dim Sql As String = "Select PidUnitID, PIDInfo, DescripWO from Pidunit where PidUnitID = " & myRow("PidUnitID")
                Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                Dim PostDescrip As String = " Post: " & myUtils.cStrTN(myRow("Title")) & " for: " & myUtils.cStrTN(ds.Tables(0).Rows(0)("PidInfo")) & ", " & myUtils.cStrTN(ds.Tables(0).Rows(0)("DescripWO"))
                Try
                    Dim UserSetID = myFuncs.GetUserSetID(myContext, myView.MainGrid.myDS.Tables(0))
                    If UserSetID.HasValue Then
                        myRow("NotifyUserSetID") = UserSetID.Value
                    Else
                        myRow("NotifyUserSetID") = DBNull.Value
                    End If

                    Dim UserIDCSV As String = myUtils.cStrTN(myFuncs.GetUserIDCSV(myContext, 0, ds.Tables(""), myView.MainGrid.myDS.Tables(0)))
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "ForumPostID", frmIDX, UserIDCSV)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                    myView.MainGrid.SaveChanges(, "ForumPostID=" & frmIDX)

                    frmMode = EnumfrmMode.acEditM

                    frmIDX = myRow("ForumPostID")
                    myContext.Provider.dbConn.CommitTransaction(PostDescrip, frmIDX)
                    VSave = True
                Catch ex As Exception
                    myContext.Provider.dbConn.RollBackTransaction(PostDescrip, ex.Message)
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
            Dim sql = String.Empty
            Dim postId As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ParentID", Params))

            Select Case dataKey.Trim.ToLower
                Case "save"
                    Dim CommentPlain = myUtils.cStrTN(myContext.SQL.ParamValue("@commentplain", Params))
                    Dim CommentHTML = myUtils.cStrTN(myContext.SQL.ParamValue("@commenthtml", Params))

                    sql = "select * from usercomment where 0=1"
                    Dim dt2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                    Dim nr As DataRow = myContext.Tables.AddNewRow(dt2)
                    nr("pidfield") = "ForumPostID"
                    nr("pidvalue") = postId
                    nr("comment") = BucketUtility.TryBase64Decode(CommentPlain)
                    nr("commenthtml") = BucketUtility.TryBase64Decode(CommentHTML)
                    nr("dated") = TaskProviderFactory.FindLocalTime
                    myContext.Provider.objSQLHelper.SaveResults(dt2, sql)

                Case "delete"
                    Dim UserCommentID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@usercommentid", Params))
                    sql = "Update usercomment set IsDeleted = 1 where UserCommentID=" & myUtils.cValTN(UserCommentID)
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql)
            End Select

            sql = "select * from prjlistusercomment() where ForumPostID = " & postId & " and IsDeleted IS NULL order by Dated desc"
            oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)

        End If
        Return oRet
    End Function

    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            If AcceptWarning Then
                Select Case EntityKey.Trim.ToLower
                    Case "forumpost"
                        Dim sql As String = "Select * from ForumPost Where ForumPostID =" & ID
                        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                        If dt.Rows.Count > 0 Then
                            Dim sql1 As String = "delete from ForumPost where ForumPostID =" & ID
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
