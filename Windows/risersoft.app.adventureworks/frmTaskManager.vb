Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinSchedule
Imports Infragistics.Win.UltraWinToolbars
Imports Infragistics.Win.UltraWinSchedule.TaskUI
Imports Infragistics.Win.UltraWinGanttView
Imports Infragistics.Win.UltraMessageBox
Imports Infragistics.Shared
Imports Infragistics.Win.Printing
Imports risersoft.shared.win
Imports System.Reflection
Imports risersoft.app2.shared
Imports risersoft.shared
Imports risersoft.app.mxform

Partial Public Class frmTaskManager
    Inherits frmMax2


    Private cellActivationRecursionFlag As Boolean = False
    Private currentThemeIndex As Integer = -1, themePaths As String()

#Region "Constructor"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="frmTaskManager"/> class.
    ''' </summary>
    Public Sub New()
        rm = Resources.ResourceManager

        ' Minimize the initialization time by loading the style library before the InitializeComponent().
        ' Otherwise, all the metrics are recalculated again after the theme changes
        Me.themePaths = Utilities.GetStyleLibraryResourceNames()
        'For i As Integer = 0 To Me.themePaths.Length - 1
        '    If Me.themePaths(i).Contains("02") Then
        '        Me.currentThemeIndex = i
        '        Exit For
        '    End If
        'Next

        'Infragistics.Win.AppStyling.StyleManager.Load(Utilities.GetEmbeddedResourceStream(Me.themePaths(Me.currentThemeIndex)))

        InitializeComponent()
    End Sub
#End Region

#Region "Base Class Overrides"

#Region "Dispose"

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            ' Unhook the StyleChanged event handler
            RemoveHandler Infragistics.Win.AppStyling.StyleManager.StyleChanged, AddressOf Application_OnStyleChanged

            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

#Region "OnLoad"

    ''' <summary>
    ''' Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
    ''' </summary>
    ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    Protected Overrides Sub OnLoad(e As EventArgs)
        Me.ChangeIcon()

        MyBase.OnLoad(e)

        ' Retrieves the sample data from the provided XML file
        Me.SetupEnv()

        'Initialize the controls on the form
        Me.ColorizeImages()
        Me.InitializeUI()

        AddHandler Infragistics.Win.AppStyling.StyleManager.StyleChanged, AddressOf Application_OnStyleChanged
    End Sub
#End Region
#End Region
    Protected Friend Sub SetupEnv()
        Me.ultraToolbarsManager1.Tools("Exit").SharedProps.Visible = False
        Me.ultraToolbarsManager1.Tools("Close").SharedProps.Visible = False
        Dim oTool1 As New ButtonTool("OK")
        Dim oTool2 As New ButtonTool("Save")
        oTool1.SharedProps.Caption = "OK"
        oTool2.SharedProps.Caption = "Save"
        Me.ultraToolbarsManager1.Tools.AddRange(New ToolBase() {oTool1, oTool2})
        Me.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.NavigationMenu.NonInheritedTools.AddRange(New ToolBase() {oTool1, oTool2})

    End Sub
    Private Sub InitForm()
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmTaskManagerModel = Me.InitData("frmTaskManagerModel", oView, prepMode, prepIDX, strXML)
        If Me.BindModel(objModel, oView) Then
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            Dim dsTask = PanelModelUtils.CreateTasksFromWBS(Me.dsForm)
            Me.ultraGanttView1.BindProjectData(dsTask)
            'If Me.dsForm.Tables("elem").Select("starttime is not null").Length > 0 Then
            '    Me.ultraGanttView1.EnsureDateTimeVisible(Me.dsForm.Tables("elem").Compute("min(starttime)", ""), True)
            'End If
            Return True
        End If
        Return False
    End Function
    Public Function PopulateWBSData(ultraGanttView1 As UltraGanttView, dsWBS As DataSet) As Boolean
        If Not dsWBS.Tables("elem").Columns.Contains("ispresent") Then dsWBS.Tables("elem").Columns.Add("ispresent", GetType(Boolean))
        myUtils.ChangeAll(dsWBS.Tables("elem").Select, "ispresent=false")
        Dim cont = PopulateWBS(ultraGanttView1.CalendarInfo.Tasks, dsWBS.Tables("pidu").Rows(0), dsWBS)
        myUtils.DeleteRows(dsWBS.Tables("elem").Select("ispresent=false"))
        Return cont
    End Function
    Public Function PopulateWBS(tasks As TasksCollection, rPIDU As DataRow, dsWBS As DataSet) As Boolean
        For Each task In tasks
            Dim nr As DataRow
            Dim rr() As DataRow = dsWBS.Tables("elem").Select("taskid='" & task.Id.ToString & "'")
            If rr.Length > 0 Then
                nr = rr(0)
            Else
                nr = myWinApp.Controller.Tables.AddNewRow(dsWBS.Tables("elem"))
                nr("taskid") = task.Id.ToString
            End If
            nr("ispresent") = True
            nr("pidunitid") = rPIDU("pidunitid")
            nr("description") = task.Name
            nr("starttime") = task.StartDateTime
            nr("finishtime") = task.EndDateTime
            nr("durationday") = task.Duration.Days
            nr("ptaskid") = If(task.Parent Is Nothing, Nothing, task.Parent.Id)
            nr("schconstraint") = task.Constraint
            nr("percentcomplete") = task.PercentComplete
            nr("allproperties") = task.Save
            Dim lst = task.Resources.Cast(Of Owner).ToList
            If lst.Count = 1 Then
                nr("assignuserid") = lst(0).Key
                nr("assignusersetid") = DBNull.Value
            Else
                Dim UserIDCSV As String = myUtils.MakeCSV2(",", "00000000-0000-0000-0000-000000000000", False, lst.Select(Function(x) x.Key).ToArray)
                Dim UserNameCSV As String = myUtils.MakeCSV2(",", "00000000-0000-0000-0000-000000000000", False, lst.Select(Function(x) x.EmailAddress).ToArray)
                nr("assignuserid") = DBNull.Value
                'nr("assignusersetid") = mxform.myFuncs.GetUserSetID(Me.Controller, UserIDCSV, UserNameCSV)
            End If
            PopulateWBS(task.Tasks, rPIDU, dsWBS)
        Next
        Return True
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If Me.ValidateData() Then
            If Me.CanSave() Then
                Me.PopulateWBSData(Me.ultraGanttView1, Me.dsForm)
                If Me.SaveModel() Then
                    Return True
                End If
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function


    ''' <summary>
    ''' Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
    ''' </summary>
    ''' <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)

        ' Process other events before firing the event. 
        ' Otherwise, the form will not be completely rendered before the splash screen is closed
        Application.DoEvents()

    End Sub


    ''' <summary>
    ''' Handles the StyleChanged event of the Application Styling Manager
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Infragistics.Win.AppStyling.StyleChangedEventArgs"/> instance containing the event data.</param>
    Private Sub Application_OnStyleChanged(sender As Object, e As Infragistics.Win.AppStyling.StyleChangedEventArgs)
        Me.ultraGanttView1.PerformAutoSizeAllGridColumns()

        ' Colorize the images to match the current theme.
        Me.ColorizeImages()
        Me.ChangeIcon()
    End Sub


    ''' <summary>
    ''' Handles the CalendarInfoChanged event of the ultraCalendarInfo1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CalendarInfoChangedEventArgs"/> instance containing the event data.</param>
    Private Sub ultraCalendarInfo1_CalendarInfoChanged(sender As Object, e As CalendarInfoChangedEventArgs) Handles ultraCalendarInfo1.CalendarInfoChanged
        Dim activeTask As Task = Me.ultraGanttView1.ActiveTask
        If activeTask Is Nothing Then
            Return
        End If

        ' Check to see if the level of the active task changed.
        ' If so, make sure the state of the Tasks tools is verified.
        Dim propInfo As PropChangeInfo = e.PropChangeInfo.FindTrigger(activeTask)
        If propInfo IsNot Nothing AndAlso TypeOf propInfo.PropId Is TaskPropertyIds AndAlso CType(propInfo.PropId, TaskPropertyIds) = TaskPropertyIds.Level Then
            Me.UpdateTasksToolsState(activeTask)
        End If
    End Sub


    ''' <summary>
    ''' Handles the ActiveTaskChanging event of the ultraGanttView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ActiveTaskChangingEventArgs"/> instance containing the event data.</param>
    Private Sub ultraGanttView1_ActiveTaskChanging(sender As Object, e As ActiveTaskChangingEventArgs) Handles ultraGanttView1.ActiveTaskChanging
        Dim newActiveTask As Task = e.NewActiveTask
        Me.UpdateTasksToolsState(newActiveTask)
        Me.UpdateToolsRequiringActiveTask(newActiveTask IsNot Nothing)
        If newActiveTask IsNot Nothing Then Me.ultraGanttView1.EnsureDateTimeVisible(newActiveTask.StartDateTime)
    End Sub


    ''' <summary>
    ''' Handles the CellActivating event of the ultraGanttView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CellActivatingEventArgs"/> instance containing the event data.</param>
    Private Sub ultraGanttView1_CellActivating(sender As Object, e As CellActivatingEventArgs) Handles ultraGanttView1.CellActivating
        Dim originalValue As Boolean = Me.cellActivationRecursionFlag
        Me.cellActivationRecursionFlag = True
        Try
            HandleCellActivation(Me.ultraToolbarsManager1, e.TaskFieldInfo.Task, e.TaskFieldInfo.TaskField)
            Me.UpdateFontToolsState(e.TaskFieldInfo.TaskField.HasValue)
        Finally
            Me.cellActivationRecursionFlag = originalValue
        End Try

    End Sub

    ''' <summary>
    ''' Handles the CellDeactivating event of the ultraGanttView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Infragistics.Win.UltraWinGanttView.CellDeactivatingEventArgs"/> instance containing the event data.</param>
    Private Sub ultraGanttView1_CellDeactivating(sender As Object, e As Infragistics.Win.UltraWinGanttView.CellDeactivatingEventArgs) Handles ultraGanttView1.CellDeactivating
        Me.UpdateFontToolsState(False)
        Me.UpdateTasksToolsState(Nothing)
    End Sub


    ''' <summary>
    ''' Handles the TaskAdded event of the ultraGanttView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="TaskAddedEventArgs"/> instance containing the event data.</param>
    Private Sub ultraGanttView1_TaskAdded(sender As Object, e As TaskAddedEventArgs) Handles ultraGanttView1.TaskAdded
        Me.UpdateToolsRequiringActiveTask(Me.ultraGanttView1.ActiveTask IsNot Nothing)
    End Sub


    ''' <summary>
    ''' Handles the TaskDeleted event of the ultraGanttView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="TaskDeletedEventArgs"/> instance containing the event data.</param>
    Private Sub ultraGanttView1_TaskDeleted(sender As Object, e As TaskDeletedEventArgs) Handles ultraGanttView1.TaskDeleted
        Me.UpdateToolsRequiringActiveTask(Me.ultraGanttView1.ActiveTask IsNot Nothing)
    End Sub

    ''' <summary>
    ''' Handles the displaying task dialog.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="TaskDialogDisplayingEventArgs"/> instance containing the event data.</param>
    Private Sub ultraGanttView1_TaskDialogDisplaying(sender As Object, e As TaskDialogDisplayingEventArgs) Handles ultraGanttView1.TaskDialogDisplaying
        ' Show Notes page when task dialog gets launched
        e.Dialog.SelectPage(TaskDialogPage.General)
    End Sub

    ''' <summary>
    ''' Handles the PropertyChanged event of the ultraToolbarsManager1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Infragistics.Win.PropertyChangedEventArgs"/> instance containing the event data.</param>
    Private Sub ultraToolbarsManager1_PropertyChanged(sender As Object, e As Infragistics.Win.PropertyChangedEventArgs) Handles ultraToolbarsManager1.PropertyChanged
        Dim trigger As PropChangeInfo = e.ChangeInfo.FindTrigger(Nothing)
        If trigger IsNot Nothing AndAlso TypeOf trigger.Source Is SharedProps AndAlso TypeOf trigger.PropId Is Infragistics.Win.UltraWinToolbars.PropertyIds Then
            Select Case CType(trigger.PropId, Infragistics.Win.UltraWinToolbars.PropertyIds)
                Case Infragistics.Win.UltraWinToolbars.PropertyIds.Enabled

                    Dim sharedProps As SharedProps = DirectCast(trigger.Source, SharedProps)

                    Dim tool As ToolBase = If((sharedProps.ToolInstances.Count > 0), sharedProps.ToolInstances(0), sharedProps.RootTool)
                    Dim imageKey As String = String.Format("{0}_{1}", tool.Key, If(tool.EnabledResolved, "Normal", "Disabled"))
                    If Me.ilColorizedImagesLarge.Images.ContainsKey(imageKey) Then
                        sharedProps.AppearancesLarge.Appearance.Image = imageKey
                    End If
                    If Me.ilColorizedImagesSmall.Images.ContainsKey(imageKey) Then
                        sharedProps.AppearancesSmall.Appearance.Image = imageKey
                    End If

                    Exit Select
            End Select
        End If
    End Sub


    ''' <summary>
    ''' Handles the ToolClick event of the ultraToolbarsManager1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Infragistics.Win.UltraWinToolbars.ToolClickEventArgs"/> instance containing the event data.</param>
    Private Sub ultraToolbarsManager1_ToolClick(sender As Object, e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ultraToolbarsManager1.ToolClick
        Try
            Select Case e.Tool.Key
                Case "Font_Bold"
                    If cellActivationRecursionFlag = False Then
                        Me.UpdateFontProperty(FontProperties.Bold)
                    End If

                Case "Font_Italic"
                    If cellActivationRecursionFlag = False Then
                        Me.UpdateFontProperty(FontProperties.Italics)
                    End If

                Case "Font_Underline"
                    If cellActivationRecursionFlag = False Then
                        Me.UpdateFontProperty(FontProperties.Underline)
                    End If

                Case "Font_BackColor"
                    Dim fontColor As Color = DirectCast(Me.ultraToolbarsManager1.Tools("Font_BackColor"), PopupColorPickerTool).SelectedColor
                    Me.ultraGanttView1.SetTextBackColor(fontColor)

                Case "Font_ForeColor"
                    Dim fontColor As Color = DirectCast(Me.ultraToolbarsManager1.Tools("Font_ForeColor"), PopupColorPickerTool).SelectedColor
                    Me.ultraGanttView1.SetTextForeColor(fontColor)

                Case "FontList"
                    Dim fontName As String = DirectCast(Me.ultraToolbarsManager1.Tools("FontList"), FontListTool).Text
                    Me.ultraGanttView1.UpdateFontName(fontName)

                Case "FontSize"
                    Dim item As ValueListItem = DirectCast(DirectCast(Me.ultraToolbarsManager1.Tools("FontSize"), ComboBoxTool).SelectedItem, ValueListItem)
                    Me.ultraGanttView1.UpdateFontSize(item)

                Case "Insert_Task_Task"
                    Me.ultraGanttView1.AddNewTask(rm.GetString("NewTaskName"), False)

                Case "Insert_Task_TaskAtSelectedRow"
                    Me.ultraGanttView1.AddNewTask(rm.GetString("NewTaskName"), True)

                Case "Tasks_PercentComplete_0"
                    Me.ultraGanttView1.SetActiveTaskPercent(0)

                Case "Tasks_PercentComplete_25"
                    Me.ultraGanttView1.SetActiveTaskPercent(25)

                Case "Tasks_PercentComplete_50"
                    Me.ultraGanttView1.SetActiveTaskPercent(50)

                Case "Tasks_PercentComplete_75"
                    Me.ultraGanttView1.SetActiveTaskPercent(75)

                Case "Tasks_PercentComplete_100"
                    Me.ultraGanttView1.SetActiveTaskPercent(100)

                Case "Tasks_MoveLeft"
                    Me.ultraGanttView1.PerformIndentOrOutdent(GanttViewAction.OutdentTask)

                Case "Tasks_MoveRight"
                    Me.ultraGanttView1.PerformIndentOrOutdent(GanttViewAction.IndentTask)

                Case "Tasks_Delete"
                    Me.DeleteTask()

                Case "Schedule_MoveTask_1Day"
                    Me.ultraGanttView1.MoveTask(GanttViewAction.MoveTaskDateForward, TimeSpanForMoving.OneDay)

                Case "Schedule_MoveTask_1Week"
                    Me.ultraGanttView1.MoveTask(GanttViewAction.MoveTaskDateForward, TimeSpanForMoving.OneWeek)

                Case "Schedule_MoveTask_4Weeks"
                    Me.ultraGanttView1.MoveTask(GanttViewAction.MoveTaskDateForward, TimeSpanForMoving.FourWeeks)

                Case "Schedule_MoveTask_MoveTaskBackwards1Day"
                    Me.ultraGanttView1.MoveTask(GanttViewAction.MoveTaskDateBackward, TimeSpanForMoving.OneDay)

                Case "Schedule_MoveTask_MoveTaskBackwards1Week"
                    Me.ultraGanttView1.MoveTask(GanttViewAction.MoveTaskDateBackward, TimeSpanForMoving.OneWeek)

                Case "Schedule_MoveTask_MoveTaskBackwards4Weeks"
                    Me.ultraGanttView1.MoveTask(GanttViewAction.MoveTaskDateBackward, TimeSpanForMoving.FourWeeks)

                Case "Properties_TaskInformation"
                    Me.ultraGanttView1.DisplayTaskDialog(Me.ultraGanttView1.ActiveTask)

                Case "Properties_Notes"
                    AddHandler Me.ultraGanttView1.TaskDialogDisplaying, AddressOf ultraGanttView1_TaskDialogDisplaying
                    Me.ultraGanttView1.DisplayTaskDialog(Me.ultraGanttView1.ActiveTask)
                    RemoveHandler Me.ultraGanttView1.TaskDialogDisplaying, AddressOf ultraGanttView1_TaskDialogDisplaying

                Case "Insert_Milestone"
                    Me.ultraGanttView1.ActiveTask.Milestone = Not Me.ultraGanttView1.ActiveTask.Milestone

                Case "TouchMode"
                    Dim touchModeListTool As ListTool = TryCast(e.Tool, ListTool)
                    If touchModeListTool.SelectedItem Is Nothing Then
                        touchModeListTool.SelectedItemIndex = e.ListToolItem.Index
                    End If
                    Me.ultraTouchProvider1.Enabled = (e.ListToolItem.Key = "Touch")

                Case "ThemeList"
                    Dim themeListTool As ListTool = TryCast(e.Tool, ListTool)
                    If themeListTool.SelectedItem Is Nothing Then
                        themeListTool.SelectedItemIndex = e.ListToolItem.Index
                    End If

                    Dim key As String = e.ListToolItem.Key
                    If Me.currentThemeIndex < 0 OrElse Me.themePaths(Me.currentThemeIndex) <> key Then
                        Me.currentThemeIndex = e.ListToolItem.Index
                        Infragistics.Win.AppStyling.StyleManager.Load(Utilities.GetEmbeddedResourceStream(key))
                    End If
                Case "Open"
                    'Dim rr() As DataRow = Me.Controller.AdvancedSelect("<SYS ENT=""PR""/>", False)
                    'If (Not rr Is Nothing) AndAlso rr.Length > 0 Then
                    '    frmIDX = rr(0)("pidunitid")
                    '    Dim oRet = Me.GenerateIDOutput("get", frmIDX)
                    '    dsWBS = oRet.Data
                    '    Me.ultraGanttView1.BindProjectData(CreateDataFromWBS(dsWBS))
                    'End If
                Case "Save"
                    myWinForms.ButtonAction(Me, "btnsave")

                    'If dsWBS IsNot Nothing Then
                    '    Me.ultraGanttView1.PopulateWBS(dsWBS)
                    '    Dim oRet = Me.GenerateDataOutput("save", dsWBS, frmIDX)
                    'End If
                Case "OK"
                    myWinForms.ButtonAction(Me, "btnok")

                Case "Exit", "Close"
                    Application.[Exit]()
            End Select

        Catch ex As Exception
            UltraMessageBoxManager.Show(ex.Message, rm.GetString("MessageBox_Error"))
        End Try
    End Sub

    ''' <summary>
    ''' Handles the ToolValueChanged event of the ultraToolbarsManager1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ToolEventArgs"/> instance containing the event data.</param>
    Private Sub ultraToolbarsManager1_ToolValueChanged(sender As Object, e As ToolEventArgs) Handles ultraToolbarsManager1.ToolValueChanged
        Select Case e.Tool.Key
            Case "Font_BackColor"
                Dim fontColor As Color = DirectCast(Me.ultraToolbarsManager1.Tools("Font_BackColor"), PopupColorPickerTool).SelectedColor
                Me.ultraGanttView1.SetTextBackColor(fontColor)

            Case "Font_ForeColor"
                Dim fontColor As Color = DirectCast(Me.ultraToolbarsManager1.Tools("Font_ForeColor"), PopupColorPickerTool).SelectedColor
                Me.ultraGanttView1.SetTextForeColor(fontColor)

            Case "FontList"
                Dim fontName As String = DirectCast(Me.ultraToolbarsManager1.Tools("FontList"), FontListTool).Text
                Me.ultraGanttView1.UpdateFontName(fontName)

            Case "FontSize"
                Dim item As ValueListItem = DirectCast(DirectCast(Me.ultraToolbarsManager1.Tools("FontSize"), ComboBoxTool).SelectedItem, ValueListItem)
                Me.ultraGanttView1.UpdateFontSize(item)
        End Select
    End Sub

    ''' <summary>
    ''' Handles the PropertyChanged event of the ultraTouchProvider1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Infragistics.Win.PropertyChangedEventArgs"/> instance containing the event data.</param>
    Private Sub ultraTouchProvider1_PropertyChanged(sender As Object, e As Infragistics.Win.PropertyChangedEventArgs) Handles ultraTouchProvider1.PropertyChanged
        Dim propChanged As PropChangeInfo = e.ChangeInfo
        If TypeOf propChanged.PropId Is Infragistics.Win.Touch.TouchProviderPropertyIds AndAlso DirectCast(propChanged.PropId, Infragistics.Win.Touch.TouchProviderPropertyIds) = Infragistics.Win.Touch.TouchProviderPropertyIds.Enabled Then
            Me.ultraGanttView1.PerformAutoSizeAllGridColumns()
        End If
    End Sub


    Private Sub DeleteTask()
        Try
            Dim newActiveTask = Me.ultraGanttView1.DeleteActiveTask
            Me.UpdateTasksToolsState(newActiveTask)
            Me.UpdateToolsRequiringActiveTask(newActiveTask IsNot Nothing)
        Catch ex As TaskException
            UltraMessageBoxManager.Show(ex.Message, rm.GetString("MessageBox_Error"))
        End Try

    End Sub

    Private Sub ChangeIcon()
        If Me.currentThemeIndex > -1 Then
            Dim iconPath As String = Me.themePaths(Me.currentThemeIndex).Replace("Theme", "AppIcon - Theme").Replace(".isl", ".ico")

            Dim stream As System.IO.Stream = Utilities.GetEmbeddedResourceStream(iconPath)
            If stream IsNot Nothing Then
                Me.Icon = New Icon(stream)
            End If
        End If
    End Sub
    Private Sub UpdateFontProperty(propertyToUpdate As FontProperties)
        Me.ultraGanttView1.UpdateFontProperty(propertyToUpdate)
        Me.cellActivationRecursionFlag = False
    End Sub



    ''' <summary>
    ''' Initializes the UI.
    ''' </summary>
    Private Sub InitializeUI()
        ' Populate the themes list
        Dim selectedIndex As Integer = 0
        Dim themeTool As ListTool = DirectCast(Me.ultraToolbarsManager1.Tools("ThemeList"), ListTool)
        For Each resourceName As String In Me.themePaths
            Dim item As New ListToolItem(resourceName)
            Dim libraryName As String = resourceName.Replace(".isl", String.Empty)
            item.Text = libraryName.Remove(0, libraryName.LastIndexOf("."c) + 1)
            themeTool.ListToolItems.Add(item)

            If item.Text.Contains("02") Then
                selectedIndex = item.Index
            End If
        Next
        'themeTool.SelectedItemIndex = selectedIndex

        ' Select the proper touch mode list item.
        DirectCast(Me.ultraToolbarsManager1.Tools("TouchMode"), ListTool).SelectedItemIndex = 0

        ' Creates a valueList with various font sizes
        Me.PopulateFontSizeValueList()
        DirectCast(Me.ultraToolbarsManager1.Tools("FontSize"), ComboBoxTool).SelectedIndex = 0
        DirectCast(Me.ultraToolbarsManager1.Tools("FontList"), FontListTool).SelectedIndex = 0
        Me.UpdateFontToolsState(False)

        Dim control As frmMax = myWinApp.objAppExtender.AboutBox
        control.MakeDownLevel()
        control.Visible = False
        control.Parent = Me
        DirectCast(Me.ultraToolbarsManager1.Tools("About"), PopupControlContainerTool).Control = control

        ' Autosize the columns so all the data is visible.
        Me.ultraGanttView1.HorizontalResizeMode = GanttViewHorizontalResizeMode.Proportional
        Me.ultraGanttView1.PerformAutoSizeAllGridColumns()

        ' Colorize the images to match the current theme.
        Me.ColorizeImages()

        Me.ultraToolbarsManager1.Ribbon.FileMenuButtonCaption = Resources.ribbonFileTabCaption
    End Sub
    Private Sub ColorizeImages()
        Utilities.ColorizeImages(Me.ultraToolbarsManager1, Me.ilDefaultImagesSmall, Me.ilDefaultImagesLarge, Me.ilColorizedImagesSmall, Me.ilColorizedImagesLarge)
    End Sub

    ''' <summary>
    ''' Populates the list of font sizes
    ''' </summary>
    Private Sub PopulateFontSizeValueList()
        Dim fontSizeList As New List(Of Single)(New Single() {8, 9, 10, 11, 12, 14,
            16, 18, 20, 22, 24, 26,
            28, 36, 48, 72})
        For Each i As Single In fontSizeList
            DirectCast(Me.ultraToolbarsManager1.Tools("FontSize"), ComboBoxTool).ValueList.ValueListItems.Add(i)
        Next
    End Sub

    ''' <summary>
    ''' Updates the Enabled property for tools in the Font RibbonGroup 
    ''' </summary>
    Private Sub UpdateFontToolsState(enabled As Boolean)
        Utilities.SetRibbonGroupToolsEnabledState(Me.ultraToolbarsManager1.Ribbon.Tabs(0).Groups("RibbonGrp_Font"), enabled)
    End Sub


    ''' <summary>
    ''' Verifies the state of the tools in the Tasks RibbonGroup.
    ''' </summary>
    ''' <param name="activeTask">The active task.</param>
    Private Sub UpdateTasksToolsState(activeTask As Task)
        Dim group As RibbonGroup = Me.ultraToolbarsManager1.Ribbon.Tabs("Ribbon_Task").Groups("RibbonGrp_Tasks")

        If activeTask IsNot Nothing Then
            ' For summary tasks, the completion percentage is based on it's child tasks
            Utilities.SetRibbonGroupToolsEnabledState(group, Not activeTask.IsSummary)

            group.Tools("Tasks_MoveLeft").SharedProps.Enabled = activeTask.CanOutdent()
            group.Tools("Tasks_MoveRight").SharedProps.Enabled = activeTask.CanIndent()
            group.Tools("Tasks_Delete").SharedProps.Enabled = True
        Else
            Utilities.SetRibbonGroupToolsEnabledState(group, False)
        End If
    End Sub


    ''' <summary>
    '''  Verifies the state of all tools requiring an active task.
    ''' </summary>
    Private Sub UpdateToolsRequiringActiveTask(enabled As Boolean)
        Me.ultraToolbarsManager1.Tools("Tasks_Delete").SharedProps.Enabled = enabled
        Me.ultraToolbarsManager1.Tools("Insert_Milestone").SharedProps.Enabled = enabled
        Me.ultraToolbarsManager1.Tools("Properties_TaskInformation").SharedProps.Enabled = enabled
        Me.ultraToolbarsManager1.Tools("Properties_Notes").SharedProps.Enabled = enabled
        Me.ultraToolbarsManager1.Tools("Insert_Task_TaskAtSelectedRow").SharedProps.Enabled = enabled
    End Sub

    Private Sub ultraGanttView1_MouseWheel(sender As Object, e As MouseEventArgs) Handles ultraGanttView1.MouseWheel
        'https://www.infragistics.com/community/forums/f/ultimate-ui-for-windows-forms/71443/zoom-in-ultraganttview
        If (e.Delta > 0) Then
            Me.ultraGanttView1.TimelineSettings.PrimaryInterval.Interval += 1
            '            ultraGanttView1.EnsureDateTimeVisible(DateTime.Today)
        Else
            Me.ultraGanttView1.TimelineSettings.PrimaryInterval.Interval -= 1
            'ultraGanttView1.EnsureDateTimeVisible(DateTime.Today)
        End If
        If Me.ultraGanttView1.ActiveTask IsNot Nothing Then
            Me.ultraGanttView1.EnsureDateTimeVisible(Me.ultraGanttView1.ActiveTask.StartDateTime)
        End If
    End Sub

    Private Sub frmTaskManager_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.ultraGanttView1.ChartAreaWidth = Me.Width * 0.6
    End Sub

    Private Sub ultraGanttView1_VerticalSplitterDragged(sender As Object, e As VerticalSplitterDraggedEventArgs) Handles ultraGanttView1.VerticalSplitterDragged
        Me.ultraGanttView1.PerformAutoSizeAllGridColumns()
    End Sub
End Class

