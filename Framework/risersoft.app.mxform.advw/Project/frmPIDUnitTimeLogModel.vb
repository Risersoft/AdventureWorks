Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.shared.cloud
Imports risersoft.app.mxengg

<DataContract>
Public Class frmPIDUnitTimeLogModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select UserID, UserName from genlistuser() where indesignlist=1 and isdeleted=0 order by fullname"
        Me.AddLookupField("AssignUserID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "User").TableName)

        sql = myFuncsBase.CodeWordSQL("Status", "StatusType", "")
        Me.AddLookupField("Status", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Status").TableName)

        Dim vlist As New clsValueList
        vlist.Add(True, "Billable")
        vlist.Add(False, "Not Billable")
        Me.ValueLists.Add("Billable", vlist)
        Me.AddLookupField("isBillable", "Billable")

        Dim vlist1 As New clsValueList
        vlist1.Add("PIDUnitWorkItemID", "WorkItem")
        vlist1.Add("WBSElementID", "WBSElement")
        Me.ValueLists.Add("PIDField", vlist1)
        Me.AddLookupField("PIDField", "PIDField")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from PIDUnitTimeLog Where PIDUnitTimeLogID = " & prepIDX
        Me.InitData(sql, "PidUnitID,IDValue,IDField", oView, prepMode, prepIDX, strXML)

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
        If myUtils.cStrTN(myRow("CustomDescrip")).Trim.Length = 0 Then Me.AddError("CustomDescrip", "Enter Custom Descrip")
        If myUtils.NullNot(myRow("SDate")) Then Me.AddError("SDate", "Please Select Start Date")
        If myUtils.cStrTN(myRow("DurationHours")).Trim.Length = 0 Then Me.AddError("DurationHours", "Enter Duration")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then

            myRow("noteshtml") = BucketUtility.TryBase64Decode(myUtils.cStrTN(myRow("noteshtml")))
            myRow("notes") = BucketUtility.TryBase64Decode(myUtils.cStrTN(myRow("notes")))

            If frmMode = EnumfrmMode.acEditM Then
                Dim sql As String = "select PIDUnitTimeLogID from PIDUnitTimeLog where dbo.fncWorkItemUser('" & myContext.Police.UniqueUserID & "', AssignUserID, PIDUnitTimeLog.CreatedByID, '00000000-0000-0000-0000-000000000000', PidUnitID, '00000000-0000-0000-0000-000000000000', Null)= 1"
                Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count = 0 Then Me.AddError("", "Don't Have Permission to Save")
            End If

            myRow("Edate") = CDate(myRow("Sdate")).AddHours(myUtils.cValTN(myRow("DurationHours"))).AddMinutes(myUtils.cValTN(myRow("DurationMinutes")))

            If Me.CanSave Then
                Dim TimeLogDescrip As String = myUtils.cStrTN(myRow("CustomDescrip"))
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PIDUnitTimeLogID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                    frmMode = EnumfrmMode.acEditM

                    frmIDX = myRow("PIDUnitTimeLogID")
                    myContext.Provider.dbConn.CommitTransaction(TimeLogDescrip, frmIDX)
                    VSave = True
                Catch ex As Exception
                    myContext.Provider.dbConn.RollBackTransaction(TimeLogDescrip, ex.Message)
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

            End Select
        End If
        Return Model
    End Function

    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            If AcceptWarning Then
                Select Case EntityKey.Trim.ToLower
                    Case "pidunittimelog"
                        Dim sql As String = "Select * from PIDUnitTimeLog Where PIDUnitTimeLogID =" & ID
                        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                        If dt.Rows.Count > 0 Then
                            Dim sql1 As String = "delete from PIDUnitTimeLog Where PIDUnitTimeLogID =" & ID
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
