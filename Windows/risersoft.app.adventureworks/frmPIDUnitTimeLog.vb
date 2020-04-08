Imports risersoft.app.mxform
Imports risersoft.shared.Extensions

Public Class frmPIDUnitTimeLog
    Inherits frmMax

    Private Sub InitForm()
        myWinForms.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPIDUnitTimeLogModel = Me.InitData("frmPIDUnitTimeLogModel", oView, prepMode, prepIDX, strXML)
        If Me.BindModel(objModel, oView) Then
            myWinSQL.AssignCmb(Me.dsCombo, "User", "", Me.cmb_AssignUserID)
            myWinSQL.AssignCmb(Me.dsCombo, "Status", "", Me.cmb_Status)
            Me.cmb_isBillable.ValueList = Me.Model.ValueLists("Billable").ComboList
            myWinSQL.AssignCmb(Me.dsCombo, "Parent", "", Me.cmb_PIDValue,,, 1)
            Me.cmb_PIDField.ValueList = Me.Model.ValueLists("PIDField").ComboList

            Me.CtlRTFNotes.HTMLText = myUtils.cStrTN(myRow("Noteshtml"))

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If Me.ValidateData() Then
            cm.EndCurrentEdit()
            myRow("Noteshtml") = Me.CtlRTFNotes.HTMLText
            myRow("Notes") = Me.CtlRTFNotes.PlainText
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub btnAssignUser_Click(sender As Object, e As EventArgs) Handles btnAssignUser.Click
        Dim Params As New List(Of clsSQLParam), rr() As DataRow
        If myUtils.NullNot(myRow("AssignUserID")) Then
            Dim str1 As String = "00000000-0000-0000-0000-000000000000"
            Params.Add(New clsSQLParam("@AssignUserID", str1, GetType(Guid), True))
        Else
            Params.Add(New clsSQLParam("@AssignUserID", myUtils.cStrTN(cmb_AssignUserID.Value), GetType(Guid), True))
        End If
        rr = Me.AdvancedSelect("assignuser", Params)
        If (Not rr Is Nothing) AndAlso rr.Length > 0 Then
            cmb_AssignUserID.Value = Guid.Parse(myUtils.cStrTN(rr(0)("UserID")))
        End If
    End Sub

    Private Sub btnClearUser_Click(sender As Object, e As EventArgs) Handles btnClearUser.Click
        myRow("AssignUserID") = DBNull.Value
        cmb_AssignUserID.Value = ""
    End Sub

    Private Sub btnDeleteTimeLog_Click(sender As Object, e As EventArgs) Handles btnDeleteTimeLog.Click
        If MsgBox("Are you sure you want to delete this Event", MsgBoxStyle.YesNo, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
            myRow("isDeleted") = 1
            myWinForms.ButtonAction(Me, "btnOK")
        End If
    End Sub

End Class
