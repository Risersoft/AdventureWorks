<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmComment
    Inherits frmMax

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call


        Me.InitForm()
    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ValueListItem7 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem8 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtlRTFComment = New risersoft.[shared].win.ctlRTF()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnDelComment = New Infragistics.Win.Misc.UltraButton()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.UltraPanel5 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_Dated = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.cmb_PIDField = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.cmb_PIDValue = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.UltraPanel5.ClientArea.SuspendLayout()
        Me.UltraPanel5.SuspendLayout()
        CType(Me.dt_Dated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_PIDField, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_PIDValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtlRTFComment)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(642, 172)
        '
        'CtlRTFComment
        '
        Me.CtlRTFComment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtlRTFComment.EditEnabled = True
        Me.CtlRTFComment.HTMLText = ""
        Me.CtlRTFComment.LayoutType = "Continuous"
        Me.CtlRTFComment.Location = New System.Drawing.Point(0, 0)
        Me.CtlRTFComment.Name = "CtlRTFComment"
        Me.CtlRTFComment.PlainText = ""
        Me.CtlRTFComment.RTFText = ""
        Me.CtlRTFComment.Size = New System.Drawing.Size(642, 172)
        Me.CtlRTFComment.TabIndex = 265
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnDelComment)
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 240)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(646, 39)
        Me.Panel4.TabIndex = 240
        '
        'btnDelComment
        '
        Appearance1.BackColor = System.Drawing.Color.LightCoral
        Appearance1.FontData.BoldAsString = "True"
        Me.btnDelComment.Appearance = Appearance1
        Me.btnDelComment.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelComment.Location = New System.Drawing.Point(0, 0)
        Me.btnDelComment.Name = "btnDelComment"
        Me.btnDelComment.Size = New System.Drawing.Size(88, 39)
        Me.btnDelComment.TabIndex = 5
        Me.btnDelComment.Text = "Delete Comment"
        Me.btnDelComment.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'btnSave
        '
        Appearance2.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance2
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(382, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 39)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance3.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance3
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(470, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 39)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance4.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance4
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(558, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 39)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        '
        'UltraPanel5
        '
        '
        'UltraPanel5.ClientArea
        '
        Me.UltraPanel5.ClientArea.Controls.Add(Me.UltraLabel3)
        Me.UltraPanel5.ClientArea.Controls.Add(Me.dt_Dated)
        Me.UltraPanel5.ClientArea.Controls.Add(Me.cmb_PIDField)
        Me.UltraPanel5.ClientArea.Controls.Add(Me.cmb_PIDValue)
        Me.UltraPanel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel5.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel5.Name = "UltraPanel5"
        Me.UltraPanel5.Size = New System.Drawing.Size(646, 42)
        Me.UltraPanel5.TabIndex = 253
        '
        'UltraLabel3
        '
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance5
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(388, 15)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(31, 14)
        Me.UltraLabel3.TabIndex = 243
        Me.UltraLabel3.Text = "Date:"
        '
        'dt_Dated
        '
        Appearance6.FontData.BoldAsString = "False"
        Appearance6.FontData.ItalicAsString = "False"
        Appearance6.FontData.Name = "Arial"
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.FontData.StrikeoutAsString = "False"
        Appearance6.FontData.UnderlineAsString = "False"
        Me.dt_Dated.Appearance = Appearance6
        Me.dt_Dated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dt_Dated.FormatString = "dddd dd MMM yyyy"
        Me.dt_Dated.Location = New System.Drawing.Point(429, 12)
        Me.dt_Dated.Name = "dt_Dated"
        Me.dt_Dated.NullText = "Not Defined"
        Me.dt_Dated.Size = New System.Drawing.Size(192, 21)
        Me.dt_Dated.TabIndex = 242
        '
        'cmb_PIDField
        '
        ValueListItem7.DataValue = False
        ValueListItem7.DisplayText = "Salary"
        ValueListItem8.DataValue = True
        ValueListItem8.DisplayText = "Wage"
        Me.cmb_PIDField.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem7, ValueListItem8})
        Me.cmb_PIDField.Location = New System.Drawing.Point(34, 12)
        Me.cmb_PIDField.Name = "cmb_PIDField"
        Me.cmb_PIDField.ReadOnly = True
        Me.cmb_PIDField.Size = New System.Drawing.Size(122, 21)
        Me.cmb_PIDField.TabIndex = 241
        Me.cmb_PIDField.UseAppStyling = False
        '
        'cmb_PIDValue
        '
        Me.cmb_PIDValue.DisplayMember = "IDInfo"
        Me.cmb_PIDValue.Location = New System.Drawing.Point(162, 12)
        Me.cmb_PIDValue.Name = "cmb_PIDValue"
        Me.cmb_PIDValue.ReadOnly = True
        Me.cmb_PIDValue.Size = New System.Drawing.Size(142, 22)
        Me.cmb_PIDValue.TabIndex = 240
        Me.cmb_PIDValue.ValueMember = "IDValue"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 42)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance7.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance7
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(646, 198)
        Me.UltraTabControl1.TabIndex = 259
        Me.UltraTabControl1.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab1.TabPage = Me.UltraTabPageControl2
        UltraTab1.Text = "Comment"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1})
        Me.UltraTabControl1.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(642, 172)
        '
        'frmComment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "Comment"
        Me.ClientSize = New System.Drawing.Size(646, 279)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.UltraPanel5)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "frmComment"
        Me.Text = "Comment"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.UltraPanel5.ClientArea.ResumeLayout(False)
        Me.UltraPanel5.ClientArea.PerformLayout()
        Me.UltraPanel5.ResumeLayout(False)
        CType(Me.dt_Dated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_PIDField, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_PIDValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents btnDelComment As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraPanel5 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmb_PIDField As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents cmb_PIDValue As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_Dated As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents CtlRTFComment As ctlRTF
End Class
