Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports System.Reflection
Imports Newtonsoft.Json

Imports risersoft.shared.cloud
Imports risersoft.app.mxengg

<DataContract>
Public Class frmTaskManagerModel
    Inherits clsFormDataModel
    <DataMember>
    Public Property gModel As clsGanttModel
    <DataMember>
    Public Property JsonData As Object

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String
        FormPrepared = False

        If prepMode = EnumfrmMode.acEditM Then
            Me.strT = "pidu"
            sql = "Select pidunitid, descripwo, pidinfo, woinfo, wotype, wodate, filenumcomp, customer from deslistpidunittot() where PIDUnitID = " & prepIDX
            Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)
            Me.BindDataTable(myUtils.cValTN(prepIDX))

            Dim conf As New clsGanttConf() With {.FinishTimeField = "finishtime", .StartTimeField = "starttime", .IsCompletedField = "Iscompleted"}
            conf.Levels.Add(New clsGanttConfLevel With {.IDField = "WBSElementID", .pIDField = "pWBSElementID", .NameField = "Description"})
            gModel = conf.GenerateGanttModel(myContext, "Tasks", Me.dsForm)
            For Each tsk In gModel.Tasks
                tsk.ListObject = Nothing
            Next

            Dim sql2 = "select * from prjlistusercomment() where WBSElementID = " & prepIDX & " and IsDeleted IS NULL order by Dated desc"
            Me.AddDataSet("comment", sql2)

            FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal PidUnitID As Integer)
        Dim ds As DataSet, Sql As String

        Sql = " Select * from WBSElement where PIDUnitID = " & PidUnitID
        Sql = Sql & "; Select * from WBSRelation where wbselementid in (select WBSElementid from WBSElement where PIDUnitID = " & PidUnitID & ")"
        Sql = Sql & "; Select * from prjListPIDUnitUser() where PIDUnitID = " & PidUnitID
        Sql = Sql & "; Select * from userset where usersetid in (select assignusersetid from wbselement where PIDUnitID = " & PidUnitID & ")"
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "elem", 0)
        myUtils.AddTable(Me.dsForm, ds, "relation", 1)
        myUtils.AddTable(Me.dsForm, ds, "resource", 2)
        myUtils.AddTable(Me.dsForm, ds, "userset", 3)
        myContext.Tables.SetAuto(Me.dsForm.Tables("elem"), Nothing, "wbselementid")
    End Sub

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            If Me.CanSave Then
                Dim TaskDescrip As String = myUtils.cStrTN(myRow("DescripWO")) & " Tasks"
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PIDUnitID", frmIDX)
                    myUtils.ChangeAll(dsForm.Tables("elem").Select, "pidunitid=" & myRow("PidUnitID"))
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("elem"), "Select * from WBSElement where 0=0")

                    For Each rWBS In dsForm.Tables("elem").Select
                        For Each r2 As DataRow In dsForm.Tables("elem").Select("TaskID = '" & rWBS("ptaskid").ToString & "'")
                            rWBS("pWBSElementID") = r2("WBSElementID")
                        Next

                    Next
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("elem"), "Select wbselementid, pwbselementid from wbselement")

                    frmMode = EnumfrmMode.acEditM

                    frmIDX = myRow("PidUnitID")
                    myContext.Provider.dbConn.CommitTransaction(TaskDescrip, frmIDX)
                    Me.BindDataTable(frmIDX)    'Get the resources and other cleaned up data back
                    VSave = True

                Catch ex As Exception
                    myContext.Provider.dbConn.RollBackTransaction(TaskDescrip, ex.Message)
                    Me.AddError("", ex.Message)
                End Try
            End If
        End If
    End Function

    Public Function TaskID(nr As DataRow) As String
        Return "WBSElementID" & nr("wbselementid")
    End Function

    Public Overrides Sub OnAdoptModel(NewModel As clsFormDataModel)

        Dim NewModel2 = CType(NewModel, frmTaskManagerModel)
        If Not myUtils.NullNot(NewModel2.JsonData) Then
            gModel = New clsGanttModel()
            gModel.Tasks = JsonConvert.DeserializeObject(Of List(Of clsProjectTask))(NewModel2.JsonData.ToString)
            gModel.SetProjectTimes()

            Dim dt1 As DataTable = Me.dsForm.Tables("elem")
            dt1.Columns.Add("TaskID", GetType(String))
            dt1.Columns.Add("pTaskID", GetType(String))
            For Each r1 As DataRow In dt1.Select
                r1("taskid") = Me.TaskID(r1)
            Next

            Dim lst = dt1.Rows.Cast(Of DataRow).Where(Function(nr)
                                                          Return gModel.Tasks.Where(Function(y) myUtils.IsInList(y.TaskID, nr("taskid"))).Count = 0
                                                      End Function).ToList
            myUtils.DeleteRows(lst.ToArray)
            For Each tsk In gModel.Tasks
                Dim nr = dt1.Rows.Cast(Of DataRow).Where(Function(r1) myUtils.IsInList(tsk.TaskID, r1("taskid"))).FirstOrDefault
                If nr Is Nothing Then nr = myContext.Tables.AddNewRow(dt1)
                nr("taskid") = tsk.TaskID
                nr("starttime") = tsk.TaskStartTime
                nr("finishtime") = tsk.TaskEndTime
                nr("percentcomplete") = tsk.PercentComplete
                nr("description") = tsk.TaskName
                nr("ptaskid") = tsk.ParentTaskID
                nr("assignuserid") = DBNull.Value
                nr("assignusersetid") = DBNull.Value
                If tsk.Resources.Count = 1 Then
                    nr("assignuserid") = tsk.Resources(0)
                ElseIf tsk.Resources.Count > 1 Then
                    nr("assignusersetid") = myFuncs.GetUserSetID(myContext, tsk.Resources, gModel.Resources)
                End If

            Next

        End If

    End Sub

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Dim sql2 = String.Empty
            Dim WBSElementID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ParentID", Params))

            Select Case dataKey.Trim.ToLower
                Case "save"
                    'Dim WBSElementID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@wbselementid", Params))
                    Dim CommentPlain = BucketUtility.TryBase64Decode(myUtils.cStrTN(myContext.SQL.ParamValue("@commentplain", Params)))
                    Dim CommentHTML = BucketUtility.TryBase64Decode(myUtils.cStrTN(myContext.SQL.ParamValue("@commenthtml", Params)))

                    sql2 = "select * from usercomment where 0=1"
                    Dim dt2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)
                    Dim nr As DataRow = myContext.Tables.AddNewRow(dt2)
                    nr("pidfield") = "WBSElementID"
                    nr("pidvalue") = WBSElementID
                    nr("comment") = CommentPlain
                    nr("commenthtml") = CommentHTML
                    nr("dated") = TaskProviderFactory.FindLocalTime
                    myContext.Provider.objSQLHelper.SaveResults(dt2, sql2)

                Case "delete"
                    Dim UserCommentID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@usercommentid", Params))
                    sql2 = "Update usercomment set IsDeleted = 1 where UserCommentID=" & myUtils.cValTN(UserCommentID)
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql2)

                    'Case "get"
                    '    'Dim WBSElementID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@wbselementid", Params))
                    '    sql2 = "select * from prjlistusercomment() where WBSElementID = " & WBSElementID
                    '    oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2)
            End Select

            If WBSElementID <> 0 Then
                sql2 = "select * from prjlistusercomment() where WBSElementID = " & WBSElementID & " and IsDeleted IS NULL order by Dated desc"
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2)
            End If

        End If
        Return oRet
    End Function

End Class

