Imports risersoft.app.mxform
Imports risersoft.shared.Extensions

Public Class frmComment
    Inherits frmMax

    Private Sub InitForm()
        myWinForms.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmCommentModel = Me.InitData("frmCommentModel", oView, prepMode, prepIDX, strXML)
        If Me.BindModel(objModel, oView) Then

            myWinSQL.AssignCmb(Me.dsCombo, "Parent", "", Me.cmb_PIDValue,,, 1)
            Me.cmb_PIDField.ValueList = Me.Model.ValueLists("PIDField").ComboList

            Me.CtlRTFComment.HTMLText = myUtils.cStrTN(myRow("Commenthtml"))

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
            myRow("Commenthtml") = Me.CtlRTFComment.HTMLText
            myRow("Comment") = Me.CtlRTFComment.PlainText
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub btnDelComment_Click(sender As Object, e As EventArgs) Handles btnDelComment.Click
        If MsgBox("Are you sure you want to delete this Comment", MsgBoxStyle.YesNo, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
            myRow("isDeleted") = 1
            myWinForms.ButtonAction(Me, "btnOK")
        End If
    End Sub
End Class
