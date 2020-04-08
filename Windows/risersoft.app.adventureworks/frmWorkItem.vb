Imports risersoft.app.mxform
Imports risersoft.shared.Extensions

Public Class frmWorkItem
    Inherits frmMax
    Dim myVueNotify As New clsWinView

    Private Sub InitForm()
        myWinForms.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(Me.UltraGridUserSet)
        myVueNotify.SetGrid(Me.UltraGridNotify)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmWorkItemModel = Me.InitData("frmWorkItemModel", oView, prepMode, prepIDX, strXML)
        If Me.BindModel(objModel, oView) Then
            myWinSQL.AssignCmb(Me.dsCombo, "User", "", Me.cmb_AssignUserID)
            myWinSQL.AssignCmb(Me.dsCombo, "Review", "", Me.cmb_ReviewUserID)
            myWinSQL.AssignCmb(Me.dsCombo, "Report", "", Me.cmb_ReporterUserID)
            myWinSQL.AssignCmb(Me.dsCombo, "VisibleFlag", "", Me.cmb_VisibleFlag)
            myWinSQL.AssignCmb(Me.dsCombo, "WorkItemType", "", Me.cmb_WorkItemType)
            myWinSQL.AssignCmb(Me.dsCombo, "Status", "", Me.cmb_Status)

            Me.CtlRTFContent.HTMLText = myUtils.cStrTN(myRow("Contenthtml"))

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Assign"),, btnDeleteUsers)
            myVueNotify.PrepEdit(Me.Model.GridViews("Notify"),, btnDelNotifyTo)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If Me.ValidateData() Then
            cm.EndCurrentEdit()
            myRow("Contenthtml") = Me.CtlRTFContent.HTMLText
            myRow("Content") = Me.CtlRTFContent.PlainText
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub btnClearUser_Click(sender As Object, e As EventArgs) Handles btnClearUser.Click
        myRow("AssignUserID") = DBNull.Value
        cmb_AssignUserID.Value = DBNull.Value
        myUtils.DeleteRows(myView.mainGrid.myDS.Tables(0).Select)
    End Sub

    Private Sub btnAddUsers_Click(sender As Object, e As EventArgs) Handles btnAddUsers.Click
        Dim Params As New List(Of clsSQLParam)
        Dim lst = myView.mainGrid.myDS.Tables(0).Select.Select(Function(r1) r1("UserID").ToString).ToList
        If Not myUtils.NullNot(myRow("AssignUserID")) Then lst.Add(myRow("AssignUserID").ToString)
        Dim str1 As String = myUtils.MakeCSV2(",", "'00000000-0000-0000-0000-000000000000'", True, lst.ToArray)
        Params.Add(New clsSQLParam("@UserIDcsv", str1, GetType(Guid), True))
        Dim rr() As DataRow = Me.AdvancedSelect("userset", Params)

        If (Not IsNothing(rr)) AndAlso rr.Length > 0 Then
            If lst.Count = 0 AndAlso rr.Length = 1 Then
                cmb_AssignUserID.Value = Guid.Parse(myUtils.cStrTN(rr(0)("UserID")))
            Else
                cmb_AssignUserID.Value = DBNull.Value
                For Each r2 As DataRow In rr
                    Dim r3 As DataRow = myUtils.CopyOneRow(r2, myView.mainGrid.myDS.Tables(0))
                Next
            End If
        End If
    End Sub

    Private Sub btnDelWorkItem_Click(sender As Object, e As EventArgs) Handles btnDelWorkItem.Click
        If MsgBox("Are you sure you want to delete this Work Item", MsgBoxStyle.YesNo, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
            myRow("isDeleted") = 1
            myWinForms.ButtonAction(Me, "btnOK")
        End If
    End Sub

    Private Sub btnReviewUser_Click(sender As Object, e As EventArgs) Handles btnReviewUser.Click
        Dim Params As New List(Of clsSQLParam), rr() As DataRow
        If myUtils.NullNot(myRow("ReviewUserID")) Then
            Dim str1 As String = "00000000-0000-0000-0000-000000000000"
            Params.Add(New clsSQLParam("@ReviewUserID", str1, GetType(Guid), True))
        Else
            Params.Add(New clsSQLParam("@ReviewUserID", myUtils.cStrTN(cmb_ReviewUserID.Value), GetType(Guid), True))
        End If
        rr = Me.AdvancedSelect("reviewuser", Params)
        If (Not rr Is Nothing) AndAlso rr.Length > 0 Then
            cmb_ReviewUserID.Value = Guid.Parse(myUtils.cStrTN(rr(0)("UserID")))
        End If
    End Sub

    Private Sub btnClearReviewUser_Click(sender As Object, e As EventArgs) Handles btnClearReviewUser.Click
        myRow("ReviewUserID") = DBNull.Value
        cmb_ReviewUserID.Value = ""
    End Sub

    Private Sub btnReporterUser_Click(sender As Object, e As EventArgs) Handles btnReporterUser.Click
        Dim Params As New List(Of clsSQLParam), rr() As DataRow
        If myUtils.NullNot(myRow("ReporterUserID")) Then
            Dim str1 As String = "00000000-0000-0000-0000-000000000000"
            Params.Add(New clsSQLParam("@ReporterUserID", str1, GetType(Guid), True))
        Else
            Params.Add(New clsSQLParam("@ReporterUserID", myUtils.cStrTN(cmb_ReporterUserID.Value), GetType(Guid), True))
        End If
        rr = Me.AdvancedSelect("reporteruser", Params)
        If (Not rr Is Nothing) AndAlso rr.Length > 0 Then
            cmb_ReporterUserID.Value = Guid.Parse(myUtils.cStrTN(rr(0)("UserID")))
        End If
    End Sub

    Private Sub btnClearReporter_Click(sender As Object, e As EventArgs) Handles btnClearReporter.Click
        myRow("ReporterUserID") = DBNull.Value
        cmb_ReporterUserID.Value = ""
    End Sub

    Private Sub btnAddNotifyTo_Click(sender As Object, e As EventArgs) Handles btnAddNotifyTo.Click
        Dim Params As New List(Of clsSQLParam)
        Dim str1 As String = myUtils.MakeCSV(myVueNotify.mainGrid.myDS.Tables(0).Select, "UserID", ",", "'00000000-0000-0000-0000-000000000000'", True)
        Params.Add(New clsSQLParam("@UserIDcsv", str1, GetType(Guid), True))
        Dim rr() As DataRow = Me.AdvancedSelect("userset", Params)

        If Not IsNothing(rr) Then
            For Each r2 As DataRow In rr
                Dim r3 As DataRow = myUtils.CopyOneRow(r2, myVueNotify.mainGrid.myDS.Tables(0))
            Next
        End If
    End Sub

End Class
