Imports risersoft.app.mxform
Imports risersoft.shared.Extensions

Public Class frmPost
    Inherits frmMax
    Dim myVueNotify As New clsWinView

    Private Sub InitForm()
        myWinForms.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myVueNotify.SetGrid(Me.UltraGridNotify)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPostModel = Me.InitData("frmPostModel", oView, prepMode, prepIDX, strXML)
        If Me.BindModel(objModel, oView) Then
            myWinSQL.AssignCmb(Me.dsCombo, "VisibleFlag", "", Me.cmb_VisibleFlag)

            Me.CtlRTFContent.HTMLText = myUtils.cStrTN(myRow("Contenthtml"))

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
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

    Private Sub btnDelPost_Click(sender As Object, e As EventArgs) Handles btnDelPost.Click
        If MsgBox("Are you sure you want to delete this Post", MsgBoxStyle.YesNo, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
            myRow("isDeleted") = 1
            myWinForms.ButtonAction(Me, "btnOK")
        End If
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
