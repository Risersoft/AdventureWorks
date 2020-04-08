Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.shared.cloud
Imports risersoft.app.mxengg

<DataContract>
Public Class frmEventModel
    Inherits clsFormDataModel
    Dim myVueNotify As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Assign")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        'Entityname for Angular page
        sql = "Select UserID, UserName,Entityname from genlistuser() where indesignlist=1 and isdeleted=0 order by fullname"
        Me.AddLookupField("AssignUserID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "User").TableName)

        Dim vlist As New clsValueList
        vlist.Add("NO", "None")
        vlist.Add("1m", "1 minute") 'prior
        vlist.Add("5m", "5 minutes")
        vlist.Add("10m", "10 minutes")
        vlist.Add("15m", "15 minutes")
        vlist.Add("30m", "30 minutes")
        vlist.Add("1h", "1 hour")
        vlist.Add("2h", "2 hour")
        vlist.Add("1D", "1 day")
        vlist.Add("2D", "2 days")
        Me.ValueLists.Add("ReminderTime", vlist)
        Me.AddLookupField("ReminderTime", "ReminderTime")
        Me.AddLookupField("EmailReminderTime", "ReminderTime")
        Me.AddLookupField("SMSReminderTime", "ReminderTime")

        Dim vlist1 As New clsValueList
        vlist1.Add("NO", "None")
        vlist1.Add("D", "Daily")
        vlist1.Add("W", "Weekly")
        vlist1.Add("M", "Monthly")
        vlist1.Add("Y", "Yearly")
        Me.ValueLists.Add("RepeatType", vlist1)
        Me.AddLookupField("RepeatType", "RepeatType")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from CalendarEvent Where CalendarEventID = " & prepIDX
        Me.InitData(sql, "PidUnitID", oView, prepMode, prepIDX, strXML)

        sql = myFuncs.GenerateUserSetIdSql(myContext, myUtils.cValTN(myRow("AssignUserSetID")))
        myView.MainGrid.QuickConf(sql, True, "1", True)
        Me.myView.MainGrid.PrepEdit("<BAND INDEX=""0"" IDFIELD=""UserID""/>", EnumEditType.acCommandOnly)

        If prepIDX <> 0 Then
            Dim sql2 = "select * from prjlistusercomment() where CalendarEventID = " & prepIDX & " and IsDeleted IS NULL  order by Dated desc"
            Me.AddDataSet("comment", sql2)
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If myUtils.cStrTN(myRow("Subject")).Trim.Length = 0 Then Me.AddError("Subject", "Enter Subject")
        If myUtils.NullNot(myRow("SDate")) Then Me.AddError("SDate", "Please Select Start Date")
        If myUtils.cStrTN(myRow("DurationHours")).Trim.Length = 0 Then Me.AddError("DurationHours", "Enter Duration")
        If myUtils.cValTN(myRow("DurationHours")) > 12 Then Me.AddError("DurationHours", "Please Select Duration Hours between 1-12")
        If myUtils.cValTN(myRow("DurationMinutes")) > 60 Then Me.AddError("DurationMinutes", "Please Select Duration Minutes between 1-60")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then

            If frmMode = EnumfrmMode.acEditM Then
                Dim sql As String = "select CalendarEventID from CalendarEvent left Join userset on CalendarEvent.AssignUserSetID = Userset.UserSetID where dbo.fncWorkItemUser('" & myContext.Police.UniqueUserID & "', AssignUserID, CalendarEvent.CreatedByID, '00000000-0000-0000-0000-000000000000', PidUnitID, UserSet.UserSetList, Null)= 1"
                Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count = 0 Then Me.AddError("", "Don't Have Permission to Save")
            End If

            myRow("Edate") = CDate(myRow("Sdate")).AddHours(myUtils.cValTN(myRow("DurationHours"))).AddMinutes(myUtils.cValTN(myRow("DurationMinutes")))

            If Me.CanSave Then
                Dim EventDescrip As String = myUtils.cStrTN(myRow("Subject"))
                Try
                    Dim UserSetID = myFuncs.GetUserSetID(myContext, myView.MainGrid.myDS.Tables(0))
                    If UserSetID.HasValue Then
                        myRow("AssignUserSetID") = UserSetID.Value
                    Else
                        myRow("AssignUserSetID") = DBNull.Value
                    End If

                    Dim dt As DataTable
                    Dim UserIDCSV As String = myUtils.cStrTN(myFuncs.GetUserIDCSV(myContext, 0, myView.MainGrid.myDS.Tables(0), dt, myUtils.cStrTN(myRow("AssignUserID"))))
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "CalendarEventID", frmIDX, UserIDCSV)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)

                    frmMode = EnumfrmMode.acEditM

                    frmIDX = myRow("CalendarEventID")
                    myContext.Provider.dbConn.CommitTransaction(EventDescrip, frmIDX)
                    VSave = True
                Catch ex As Exception
                    myContext.Provider.dbConn.RollBackTransaction(EventDescrip, ex.Message)
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
            Dim eventid As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ParentID", Params))

            Select Case dataKey.Trim.ToLower
                Case "save"
                    Dim CommentPlain = myUtils.cStrTN(myContext.SQL.ParamValue("@commentplain", Params))
                    Dim CommentHTML = myUtils.cStrTN(myContext.SQL.ParamValue("@commenthtml", Params))

                    sql2 = "select * from usercomment where 0=1"
                    Dim dt2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)
                    Dim nr As DataRow = myContext.Tables.AddNewRow(dt2)
                    nr("pidfield") = "CalendarEventID"
                    nr("pidvalue") = eventid
                    nr("comment") = BucketUtility.TryBase64Decode(CommentPlain)
                    nr("commenthtml") = BucketUtility.TryBase64Decode(CommentHTML)
                    nr("dated") = TaskProviderFactory.FindLocalTime
                    myContext.Provider.objSQLHelper.SaveResults(dt2, sql2)
                Case "delete"
                    Dim UserCommentID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@usercommentid", Params))
                    sql2 = "Update usercomment set IsDeleted = 1 where UserCommentID=" & myUtils.cValTN(UserCommentID)
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql2)
            End Select

            sql2 = "select * from prjlistusercomment() where CalendarEventID = " & eventid & " and IsDeleted IS NULL order by Dated desc"
            oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2)

        End If
        Return oRet

    End Function

    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            If AcceptWarning Then
                Select Case EntityKey.Trim.ToLower
                    Case "calendarevent"
                        Dim sql As String = "Select * from CalendarEvent Where CalendarEventID =" & ID
                        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                        If dt.Rows.Count > 0 Then
                            Dim sql1 As String = "delete from CalendarEvent where CalendarEventID =" & ID
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
