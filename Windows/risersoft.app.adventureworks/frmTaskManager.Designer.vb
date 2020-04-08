Partial Class frmTaskManager
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTaskManager))
        Dim Appearance48 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance49 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance50 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool4 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Exit")
        Dim buttonTool18 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Print")
        Dim buttonTool14 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Close")
        Dim popupMenuTool2 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Theme")
        Dim popupControlContainerTool1 As Infragistics.Win.UltraWinToolbars.PopupControlContainerTool = New Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("About")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim RibbonTab1 As Infragistics.Win.UltraWinToolbars.RibbonTab = New Infragistics.Win.UltraWinToolbars.RibbonTab("Ribbon_Task")
        Dim RibbonGroup1 As Infragistics.Win.UltraWinToolbars.RibbonGroup = New Infragistics.Win.UltraWinToolbars.RibbonGroup("RibbonGrp_Font")
        Dim fontListTool1 As Infragistics.Win.UltraWinToolbars.FontListTool = New Infragistics.Win.UltraWinToolbars.FontListTool("FontList")
        Dim comboBoxTool1 As Infragistics.Win.UltraWinToolbars.ComboBoxTool = New Infragistics.Win.UltraWinToolbars.ComboBoxTool("FontSize")
        Dim stateButtonTool1 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("Font_Bold", "")
        Dim stateButtonTool3 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("Font_Italic", "")
        Dim stateButtonTool5 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("Font_Underline", "")
        Dim popupColorPickerTool1 As Infragistics.Win.UltraWinToolbars.PopupColorPickerTool = New Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("Font_BackColor")
        Dim popupColorPickerTool2 As Infragistics.Win.UltraWinToolbars.PopupColorPickerTool = New Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("Font_ForeColor")
        Dim RibbonGroup2 As Infragistics.Win.UltraWinToolbars.RibbonGroup = New Infragistics.Win.UltraWinToolbars.RibbonGroup("RibbonGrp_Tasks")
        Dim buttonTool1 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_0")
        Dim buttonTool19 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_25")
        Dim buttonTool20 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_50")
        Dim buttonTool2 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_75")
        Dim buttonTool21 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_100")
        Dim buttonTool22 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_MoveLeft")
        Dim buttonTool23 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_MoveRight")
        Dim popupMenuTool1 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tasks_MoveTask")
        Dim buttonTool11 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_Delete")
        Dim RibbonGroup3 As Infragistics.Win.UltraWinToolbars.RibbonGroup = New Infragistics.Win.UltraWinToolbars.RibbonGroup("RibbonGrp_Insert")
        Dim popupMenuTool9 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Insert_Task")
        Dim buttonTool36 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Insert_Milestone")
        Dim RibbonGroup4 As Infragistics.Win.UltraWinToolbars.RibbonGroup = New Infragistics.Win.UltraWinToolbars.RibbonGroup("Properties")
        Dim buttonTool6 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Properties_TaskInformation")
        Dim buttonTool8 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Properties_Notes")
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim fontListTool2 As Infragistics.Win.UltraWinToolbars.FontListTool = New Infragistics.Win.UltraWinToolbars.FontListTool("FontList")
        Dim comboBoxTool2 As Infragistics.Win.UltraWinToolbars.ComboBoxTool = New Infragistics.Win.UltraWinToolbars.ComboBoxTool("FontSize")
        Dim ValueList1 As Infragistics.Win.ValueList = New Infragistics.Win.ValueList(0)
        Dim popupColorPickerTool3 As Infragistics.Win.UltraWinToolbars.PopupColorPickerTool = New Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("Font_BackColor")
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim popupColorPickerTool4 As Infragistics.Win.UltraWinToolbars.PopupColorPickerTool = New Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("Font_ForeColor")
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool5 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_0")
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool24 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_25")
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool25 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_50")
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool26 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_100")
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool27 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_MoveLeft")
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool28 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_MoveRight")
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim popupMenuTool6 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tasks_MoveTask")
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim labelTool1 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Schedule_MoveTask_MoveTaskForward")
        Dim buttonTool47 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_1Day")
        Dim buttonTool48 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_1Week")
        Dim buttonTool49 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_4Weeks")
        Dim labelTool2 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Schedule_MoveTask_MoveTaskBackward")
        Dim buttonTool53 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_MoveTaskBackwards1Day")
        Dim buttonTool54 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_MoveTaskBackwards1Week")
        Dim buttonTool55 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_MoveTaskBackwards4Weeks")
        Dim popupMenuTool10 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Insert_Task")
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool59 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Insert_Task_TaskAtSelectedRow")
        Dim buttonTool13 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Insert_Task_Task")
        Dim buttonTool37 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Insert_Summary")
        Dim buttonTool38 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Insert_Milestone")
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim stateButtonTool2 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("Font_Bold", "")
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim stateButtonTool4 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("Font_Italic", "")
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim stateButtonTool6 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("Font_Underline", "")
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool16 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Insert_Task_Task")
        Dim buttonTool50 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_1Day")
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool51 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_1Week")
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool52 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_4Weeks")
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim labelTool3 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Schedule_MoveTask_MoveTaskForward")
        Dim labelTool4 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Schedule_MoveTask_MoveTaskBackward")
        Dim buttonTool56 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_MoveTaskBackwards1Day")
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool57 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_MoveTaskBackwards1Week")
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool58 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Schedule_MoveTask_MoveTaskBackwards4Weeks")
        Dim Appearance39 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool60 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Insert_Task_TaskAtSelectedRow")
        Dim buttonTool3 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_PercentComplete_75")
        Dim Appearance40 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance41 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool9 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Properties_TaskInformation")
        Dim Appearance42 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance43 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool10 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Properties_Notes")
        Dim Appearance44 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance45 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim buttonTool7 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Exit")
        Dim buttonTool12 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("ButtonTool1")
        Dim popupMenuTool3 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Theme")
        Dim labelTool5 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Select a theme:")
        Dim listTool1 As Infragistics.Win.UltraWinToolbars.ListTool = New Infragistics.Win.UltraWinToolbars.ListTool("ThemeList")
        Dim labelTool9 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Touch Mode:")
        Dim listTool5 As Infragistics.Win.UltraWinToolbars.ListTool = New Infragistics.Win.UltraWinToolbars.ListTool("TouchMode")
        Dim listTool2 As Infragistics.Win.UltraWinToolbars.ListTool = New Infragistics.Win.UltraWinToolbars.ListTool("ThemeList")
        Dim buttonTool15 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Close")
        Dim labelTool6 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Select a theme:")
        Dim labelTool10 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Touch Mode:")
        Dim listTool6 As Infragistics.Win.UltraWinToolbars.ListTool = New Infragistics.Win.UltraWinToolbars.ListTool("TouchMode")
        Dim ListToolItem1 As Infragistics.Win.UltraWinToolbars.ListToolItem = New Infragistics.Win.UltraWinToolbars.ListToolItem("Mouse")
        Dim ListToolItem2 As Infragistics.Win.UltraWinToolbars.ListToolItem = New Infragistics.Win.UltraWinToolbars.ListToolItem("Touch")
        Dim buttonTool29 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Print")
        Dim popupControlContainerTool2 As Infragistics.Win.UltraWinToolbars.PopupControlContainerTool = New Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("About")
        Dim buttonTool17 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tasks_Delete")
        Dim Appearance46 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance47 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ultraGanttView1 = New Infragistics.Win.UltraWinGanttView.UltraGanttView()
        Me.ultraCalendarInfo1 = New Infragistics.Win.UltraWinSchedule.UltraCalendarInfo(Me.components)
        Me.Form1_Fill_Panel = New System.Windows.Forms.Panel()
        Me._ProjectManagerForm_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me.ultraToolbarsManager1 = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
        Me.ilDefaultImagesLarge = New System.Windows.Forms.ImageList(Me.components)
        Me.ilDefaultImagesSmall = New System.Windows.Forms.ImageList(Me.components)
        Me.ilColorizedImagesLarge = New System.Windows.Forms.ImageList(Me.components)
        Me.ilColorizedImagesSmall = New System.Windows.Forms.ImageList(Me.components)
        Me._ProjectManagerForm_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._ProjectManagerForm_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me.ultraTouchProvider1 = New Infragistics.Win.Touch.UltraTouchProvider(Me.components)
        Me.ultraGanttViewPrintDocument1 = New Infragistics.Win.UltraWinGanttView.UltraGanttViewPrintDocument(Me.components)
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ultraGanttView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Form1_Fill_Panel.SuspendLayout()
        CType(Me.ultraToolbarsManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ultraTouchProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ultraGanttView1
        '
        Appearance1.FontData.Name = resources.GetString("resource.Name")
        Me.ultraGanttView1.Appearance = Appearance1
        Me.ultraGanttView1.CalendarInfo = Me.ultraCalendarInfo1
        resources.ApplyResources(Me.ultraGanttView1, "ultraGanttView1")
        Me.ultraGanttView1.GridAreaWidth = 328
        Appearance48.FontData.SizeInPoints = CType(resources.GetObject("resource.SizeInPoints4"), Single)
        Me.ultraGanttView1.GridSettings.ColumnHeaderAppearance = Appearance48
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Constraint").VisiblePosition = 6
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("ConstraintDateTime").VisiblePosition = 7
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Dependencies").Visible = Infragistics.Win.DefaultableBoolean.[False]
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Dependencies").VisiblePosition = 4
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Deadline").VisiblePosition = 8
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Duration").VisiblePosition = 1
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("EndDateTime").VisiblePosition = 3
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Milestone").VisiblePosition = 9
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Name").VisiblePosition = 0
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Notes").VisiblePosition = 10
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("PercentComplete").VisiblePosition = 11
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Resources").Visible = Infragistics.Win.DefaultableBoolean.[False]
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("Resources").VisiblePosition = 5
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("StartDateTime").VisiblePosition = 2
        Me.ultraGanttView1.GridSettings.ColumnSettings.GetValue("RowNumber").VisiblePosition = 12
        Appearance49.FontData.SizeInPoints = CType(resources.GetObject("resource.SizeInPoints5"), Single)
        resources.ApplyResources(Appearance49, "Appearance49")
        Me.ultraGanttView1.GridSettings.RowAppearance = Appearance49
        Me.ultraGanttView1.Name = "ultraGanttView1"
        Appearance50.FontData.SizeInPoints = CType(resources.GetObject("resource.SizeInPoints6"), Single)
        resources.ApplyResources(Appearance50, "Appearance50")
        Me.ultraGanttView1.TimelineSettings.BarSettings.BarTextAppearance = Appearance50
        Me.ultraGanttView1.VerticalSplitterMinimumResizeWidth = 10
        '
        'ultraCalendarInfo1
        '
        Me.ultraCalendarInfo1.DataBindingsForAppointments.BindingContextControl = Me
        Me.ultraCalendarInfo1.DataBindingsForOwners.BindingContextControl = Me
        '
        'Form1_Fill_Panel
        '
        Me.Form1_Fill_Panel.Controls.Add(Me.ultraGanttView1)
        Me.Form1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Form1_Fill_Panel, "Form1_Fill_Panel")
        Me.Form1_Fill_Panel.Name = "Form1_Fill_Panel"
        '
        '_ProjectManagerForm_Toolbars_Dock_Area_Left
        '
        Me._ProjectManagerForm_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._ProjectManagerForm_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._ProjectManagerForm_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left
        Me._ProjectManagerForm_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ProjectManagerForm_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 1
        resources.ApplyResources(Me._ProjectManagerForm_Toolbars_Dock_Area_Left, "_ProjectManagerForm_Toolbars_Dock_Area_Left")
        Me._ProjectManagerForm_Toolbars_Dock_Area_Left.Name = "_ProjectManagerForm_Toolbars_Dock_Area_Left"
        Me._ProjectManagerForm_Toolbars_Dock_Area_Left.ToolbarsManager = Me.ultraToolbarsManager1
        '
        'ultraToolbarsManager1
        '
        Me.ultraToolbarsManager1.DesignerFlags = 1
        Me.ultraToolbarsManager1.DockWithinContainer = Me
        Me.ultraToolbarsManager1.DockWithinContainerBaseType = GetType(System.Windows.Forms.Form)
        Me.ultraToolbarsManager1.ImageListLarge = Me.ilDefaultImagesLarge
        Me.ultraToolbarsManager1.ImageListSmall = Me.ilDefaultImagesSmall
        Me.ultraToolbarsManager1.Office2007UICompatibility = False
        Me.ultraToolbarsManager1.Ribbon.ApplicationMenu.ToolAreaLeft.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {buttonTool4})
        popupMenuTool2.InstanceProps.IsFirstInGroup = True
        Me.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.NavigationMenu.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {buttonTool18, buttonTool14, popupMenuTool2, popupControlContainerTool1})
        Appearance2.FontData.Name = resources.GetString("resource.Name1")
        Appearance2.FontData.SizeInPoints = CType(resources.GetObject("resource.SizeInPoints"), Single)
        resources.ApplyResources(Appearance2, "Appearance2")
        Me.ultraToolbarsManager1.Ribbon.CaptionAreaAppearance = Appearance2
        Appearance3.FontData.Name = resources.GetString("resource.Name2")
        Appearance3.FontData.SizeInPoints = CType(resources.GetObject("resource.SizeInPoints1"), Single)
        Me.ultraToolbarsManager1.Ribbon.FileMenuButtonAppearance = Appearance3
        Me.ultraToolbarsManager1.Ribbon.FileMenuButtonCaption = "FILE"
        Me.ultraToolbarsManager1.Ribbon.FileMenuStyle = Infragistics.Win.UltraWinToolbars.FileMenuStyle.ApplicationMenu2010
        Appearance4.AlphaLevel = CType(128, Short)
        Appearance4.FontData.SizeInPoints = CType(resources.GetObject("resource.SizeInPoints2"), Single)
        Appearance4.ForegroundAlpha = Infragistics.Win.Alpha.UseAlphaLevel
        Me.ultraToolbarsManager1.Ribbon.GroupSettings.CaptionAppearance = Appearance4
        resources.ApplyResources(RibbonTab1, "RibbonTab1")
        resources.ApplyResources(RibbonGroup1, "RibbonGroup1")
        RibbonGroup1.LayoutDirection = Infragistics.Win.UltraWinToolbars.RibbonGroupToolLayoutDirection.Horizontal
        fontListTool1.InstanceProps.Width = 193
        comboBoxTool1.InstanceProps.ButtonGroup = "FontSize"
        comboBoxTool1.InstanceProps.Width = 50
        stateButtonTool1.InstanceProps.ButtonGroup = "TextStyle"
        stateButtonTool3.InstanceProps.ButtonGroup = "TextStyle"
        stateButtonTool5.InstanceProps.ButtonGroup = "TextStyle"
        popupColorPickerTool1.InstanceProps.ButtonGroup = "Colors"
        popupColorPickerTool1.InstanceProps.IsFirstInGroup = True
        popupColorPickerTool2.InstanceProps.ButtonGroup = "Colors"
        popupColorPickerTool2.InstanceProps.IsFirstInGroup = True
        RibbonGroup1.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {fontListTool1, comboBoxTool1, stateButtonTool1, stateButtonTool3, stateButtonTool5, popupColorPickerTool1, popupColorPickerTool2})
        resources.ApplyResources(RibbonGroup2, "RibbonGroup2")
        buttonTool1.InstanceProps.IsFirstInGroup = True
        buttonTool1.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool1.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool19.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool19.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool20.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool20.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool2.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool2.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool21.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool21.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool22.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool22.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Normal
        buttonTool23.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        popupMenuTool1.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool11.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool11.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        RibbonGroup2.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {buttonTool1, buttonTool19, buttonTool20, buttonTool2, buttonTool21, buttonTool22, buttonTool23, popupMenuTool1, buttonTool11})
        resources.ApplyResources(RibbonGroup3, "RibbonGroup3")
        popupMenuTool9.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool36.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        RibbonGroup3.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {popupMenuTool9, buttonTool36})
        resources.ApplyResources(RibbonGroup4, "RibbonGroup4")
        buttonTool6.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        buttonTool8.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large
        RibbonGroup4.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {buttonTool6, buttonTool8})
        RibbonTab1.Groups.AddRange(New Infragistics.Win.UltraWinToolbars.RibbonGroup() {RibbonGroup1, RibbonGroup2, RibbonGroup3, RibbonGroup4})
        Me.ultraToolbarsManager1.Ribbon.NonInheritedRibbonTabs.AddRange(New Infragistics.Win.UltraWinToolbars.RibbonTab() {RibbonTab1})
        Me.ultraToolbarsManager1.Ribbon.QuickAccessToolbar.Visible = False
        Appearance5.FontData.Name = resources.GetString("resource.Name3")
        Appearance5.FontData.SizeInPoints = CType(resources.GetObject("resource.SizeInPoints3"), Single)
        Me.ultraToolbarsManager1.Ribbon.TabSettings.Appearance = Appearance5
        Me.ultraToolbarsManager1.Ribbon.Visible = True
        Me.ultraToolbarsManager1.ShowFullMenusDelay = 500
        Me.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013
        comboBoxTool2.ValueList = ValueList1
        popupColorPickerTool3.ReplaceableColor = System.Drawing.Color.Yellow
        Appearance6.Image = "Font_BackColor_Normal"
        popupColorPickerTool3.SharedPropsInternal.AppearancesSmall.Appearance = Appearance6
        Appearance7.Image = "Font_BackColor_Active"
        popupColorPickerTool3.SharedPropsInternal.AppearancesSmall.HotTrackAppearance = Appearance7
        resources.ApplyResources(popupColorPickerTool3.SharedPropsInternal, "popupColorPickerTool3.SharedPropsInternal")
        popupColorPickerTool3.ForceApplyResources = "SharedPropsInternal"
        popupColorPickerTool4.ReplaceableColor = System.Drawing.Color.Yellow
        Appearance8.Image = "Font_ForeColor_Normal"
        popupColorPickerTool4.SharedPropsInternal.AppearancesSmall.Appearance = Appearance8
        Appearance9.Image = "Font_ForeColor_Active"
        popupColorPickerTool4.SharedPropsInternal.AppearancesSmall.HotTrackAppearance = Appearance9
        resources.ApplyResources(popupColorPickerTool4.SharedPropsInternal, "popupColorPickerTool4.SharedPropsInternal")
        popupColorPickerTool4.ForceApplyResources = "SharedPropsInternal"
        Appearance10.Image = "Tasks_PercentComplete_0_Normal"
        buttonTool5.SharedPropsInternal.AppearancesLarge.Appearance = Appearance10
        Appearance11.Image = "Tasks_PercentComplete_0_Active"
        buttonTool5.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance11
        resources.ApplyResources(buttonTool5.SharedPropsInternal, "buttonTool5.SharedPropsInternal")
        buttonTool5.ForceApplyResources = "SharedPropsInternal"
        Appearance12.Image = "Tasks_PercentComplete_25_Normal"
        buttonTool24.SharedPropsInternal.AppearancesLarge.Appearance = Appearance12
        Appearance13.Image = "Tasks_PercentComplete_25_Active"
        buttonTool24.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance13
        resources.ApplyResources(buttonTool24.SharedPropsInternal, "buttonTool24.SharedPropsInternal")
        buttonTool24.ForceApplyResources = "SharedPropsInternal"
        Appearance14.Image = "Tasks_PercentComplete_50_Normal"
        buttonTool25.SharedPropsInternal.AppearancesLarge.Appearance = Appearance14
        Appearance15.Image = "Tasks_PercentComplete_50_Active"
        buttonTool25.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance15
        resources.ApplyResources(buttonTool25.SharedPropsInternal, "buttonTool25.SharedPropsInternal")
        buttonTool25.ForceApplyResources = "SharedPropsInternal"
        Appearance16.Image = "Tasks_PercentComplete_100_Normal"
        buttonTool26.SharedPropsInternal.AppearancesLarge.Appearance = Appearance16
        Appearance17.Image = "Tasks_PercentComplete_100_Active"
        buttonTool26.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance17
        resources.ApplyResources(buttonTool26.SharedPropsInternal, "buttonTool26.SharedPropsInternal")
        buttonTool26.ForceApplyResources = "SharedPropsInternal"
        Appearance18.Image = "Tasks_MoveLeft_Normal"
        buttonTool27.SharedPropsInternal.AppearancesLarge.Appearance = Appearance18
        Appearance19.Image = "Tasks_MoveLeft_Active"
        buttonTool27.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance19
        resources.ApplyResources(buttonTool27.SharedPropsInternal, "buttonTool27.SharedPropsInternal")
        buttonTool27.ForceApplyResources = "SharedPropsInternal"
        Appearance20.Image = "Tasks_MoveRight_Normal"
        buttonTool28.SharedPropsInternal.AppearancesLarge.Appearance = Appearance20
        Appearance21.Image = "Tasks_MoveRight_Active"
        buttonTool28.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance21
        resources.ApplyResources(buttonTool28.SharedPropsInternal, "buttonTool28.SharedPropsInternal")
        buttonTool28.ForceApplyResources = "SharedPropsInternal"
        Appearance22.Image = "Tasks_MoveTask_Normal"
        popupMenuTool6.SharedPropsInternal.AppearancesLarge.Appearance = Appearance22
        Appearance23.Image = "Tasks_MoveTask_Active"
        popupMenuTool6.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance23
        resources.ApplyResources(popupMenuTool6.SharedPropsInternal, "popupMenuTool6.SharedPropsInternal")
        buttonTool47.InstanceProps.ButtonGroup = "Colors"
        popupMenuTool6.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {labelTool1, buttonTool47, buttonTool48, buttonTool49, labelTool2, buttonTool53, buttonTool54, buttonTool55})
        popupMenuTool6.ForceApplyResources = "SharedPropsInternal"
        Appearance24.Image = "Insert_Task_Normal"
        popupMenuTool10.SharedPropsInternal.AppearancesLarge.Appearance = Appearance24
        Appearance25.Image = "Insert_Task_Active"
        popupMenuTool10.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance25
        resources.ApplyResources(popupMenuTool10.SharedPropsInternal, "popupMenuTool10.SharedPropsInternal")
        popupMenuTool10.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {buttonTool59, buttonTool13})
        popupMenuTool10.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(buttonTool37.SharedPropsInternal, "buttonTool37.SharedPropsInternal")
        buttonTool37.ForceApplyResources = "SharedPropsInternal"
        Appearance26.Image = "Insert_Milestone_Normal"
        buttonTool38.SharedPropsInternal.AppearancesLarge.Appearance = Appearance26
        Appearance27.Image = "Insert_Milestone_Active"
        buttonTool38.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance27
        resources.ApplyResources(buttonTool38.SharedPropsInternal, "buttonTool38.SharedPropsInternal")
        buttonTool38.ForceApplyResources = "SharedPropsInternal"
        Appearance28.Image = "Font_Bold_Normal"
        stateButtonTool2.SharedPropsInternal.AppearancesSmall.Appearance = Appearance28
        Appearance29.Image = "Font_Bold_Active"
        stateButtonTool2.SharedPropsInternal.AppearancesSmall.HotTrackAppearance = Appearance29
        resources.ApplyResources(stateButtonTool2.SharedPropsInternal, "stateButtonTool2.SharedPropsInternal")
        stateButtonTool2.ForceApplyResources = "SharedPropsInternal"
        Appearance30.Image = "Font_Italic_Normal"
        stateButtonTool4.SharedPropsInternal.AppearancesSmall.Appearance = Appearance30
        Appearance31.Image = "Font_Italic_Active"
        stateButtonTool4.SharedPropsInternal.AppearancesSmall.HotTrackAppearance = Appearance31
        resources.ApplyResources(stateButtonTool4.SharedPropsInternal, "stateButtonTool4.SharedPropsInternal")
        stateButtonTool4.ForceApplyResources = "SharedPropsInternal"
        Appearance32.Image = "Font_Underline_Normal"
        stateButtonTool6.SharedPropsInternal.AppearancesSmall.Appearance = Appearance32
        Appearance33.Image = "Font_Underline_Active"
        stateButtonTool6.SharedPropsInternal.AppearancesSmall.HotTrackAppearance = Appearance33
        resources.ApplyResources(stateButtonTool6.SharedPropsInternal, "stateButtonTool6.SharedPropsInternal")
        stateButtonTool6.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(buttonTool16.SharedPropsInternal, "buttonTool16.SharedPropsInternal")
        buttonTool16.ForceApplyResources = "SharedPropsInternal"
        Appearance34.Image = "Tasks_MoveForward_Day_Normal"
        buttonTool50.SharedPropsInternal.AppearancesSmall.Appearance = Appearance34
        resources.ApplyResources(buttonTool50.SharedPropsInternal, "buttonTool50.SharedPropsInternal")
        buttonTool50.ForceApplyResources = "SharedPropsInternal"
        Appearance35.Image = "Tasks_MoveForward_Week_Normal"
        buttonTool51.SharedPropsInternal.AppearancesSmall.Appearance = Appearance35
        resources.ApplyResources(buttonTool51.SharedPropsInternal, "buttonTool51.SharedPropsInternal")
        buttonTool51.ForceApplyResources = "SharedPropsInternal"
        Appearance36.Image = "Tasks_MoveForward_Month_Normal"
        buttonTool52.SharedPropsInternal.AppearancesSmall.Appearance = Appearance36
        resources.ApplyResources(buttonTool52.SharedPropsInternal, "buttonTool52.SharedPropsInternal")
        buttonTool52.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(labelTool3.SharedPropsInternal, "labelTool3.SharedPropsInternal")
        labelTool3.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(labelTool4.SharedPropsInternal, "labelTool4.SharedPropsInternal")
        labelTool4.ForceApplyResources = "SharedPropsInternal"
        Appearance37.Image = "Tasks_MoveBackward_Day_Normal"
        buttonTool56.SharedPropsInternal.AppearancesSmall.Appearance = Appearance37
        resources.ApplyResources(buttonTool56.SharedPropsInternal, "buttonTool56.SharedPropsInternal")
        buttonTool56.ForceApplyResources = "SharedPropsInternal"
        Appearance38.Image = "Tasks_MoveBackward_Week_Normal"
        buttonTool57.SharedPropsInternal.AppearancesSmall.Appearance = Appearance38
        resources.ApplyResources(buttonTool57.SharedPropsInternal, "buttonTool57.SharedPropsInternal")
        buttonTool57.ForceApplyResources = "SharedPropsInternal"
        Appearance39.Image = "Tasks_MoveBackward_Month_Normal"
        buttonTool58.SharedPropsInternal.AppearancesSmall.Appearance = Appearance39
        resources.ApplyResources(buttonTool58.SharedPropsInternal, "buttonTool58.SharedPropsInternal")
        buttonTool58.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(buttonTool60.SharedPropsInternal, "buttonTool60.SharedPropsInternal")
        buttonTool60.ForceApplyResources = "SharedPropsInternal"
        Appearance40.Image = "Tasks_PercentComplete_75_Normal"
        buttonTool3.SharedPropsInternal.AppearancesLarge.Appearance = Appearance40
        Appearance41.Image = "Tasks_PercentComplete_75_Active"
        buttonTool3.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance41
        resources.ApplyResources(buttonTool3.SharedPropsInternal, "buttonTool3.SharedPropsInternal")
        buttonTool3.ForceApplyResources = "SharedPropsInternal"
        Appearance42.Image = "Properties_TaskInformation_Normal"
        buttonTool9.SharedPropsInternal.AppearancesLarge.Appearance = Appearance42
        Appearance43.Image = "Properties_TaskInformation_Active"
        buttonTool9.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance43
        resources.ApplyResources(buttonTool9.SharedPropsInternal, "buttonTool9.SharedPropsInternal")
        buttonTool9.ForceApplyResources = "SharedPropsInternal"
        Appearance44.Image = "Properties_Notes_Normal"
        buttonTool10.SharedPropsInternal.AppearancesLarge.Appearance = Appearance44
        Appearance45.Image = "Properties_Notes_Active"
        buttonTool10.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance45
        resources.ApplyResources(buttonTool10.SharedPropsInternal, "buttonTool10.SharedPropsInternal")
        buttonTool10.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(buttonTool7.SharedPropsInternal, "buttonTool7.SharedPropsInternal")
        buttonTool7.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(buttonTool12.SharedPropsInternal, "buttonTool12.SharedPropsInternal")
        buttonTool12.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(popupMenuTool3.SharedPropsInternal, "popupMenuTool3.SharedPropsInternal")
        labelTool5.InstanceProps.IsFirstInGroup = True
        listTool1.InstanceProps.IsFirstInGroup = False
        popupMenuTool3.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {labelTool5, listTool1, labelTool9, listTool5})
        popupMenuTool3.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(listTool2.SharedPropsInternal, "listTool2.SharedPropsInternal")
        listTool2.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(buttonTool15.SharedPropsInternal, "buttonTool15.SharedPropsInternal")
        buttonTool15.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(labelTool6.SharedPropsInternal, "labelTool6.SharedPropsInternal")
        labelTool6.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(labelTool10.SharedPropsInternal, "labelTool10.SharedPropsInternal")
        labelTool10.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(ListToolItem1, "ListToolItem1")
        ListToolItem1.Key = "Mouse"
        resources.ApplyResources(ListToolItem2, "ListToolItem2")
        ListToolItem2.Key = "Touch"
        listTool6.ListToolItemsInternal.Add(ListToolItem1)
        listTool6.ListToolItemsInternal.Add(ListToolItem2)
        resources.ApplyResources(listTool6.SharedPropsInternal, "listTool6.SharedPropsInternal")
        listTool6.ForceApplyResources = "SharedPropsInternal"
        resources.ApplyResources(buttonTool29.SharedPropsInternal, "buttonTool29.SharedPropsInternal")
        buttonTool29.ForceApplyResources = "SharedPropsInternal"
        popupControlContainerTool2.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Standard
        resources.ApplyResources(popupControlContainerTool2.SharedPropsInternal, "popupControlContainerTool2.SharedPropsInternal")
        popupControlContainerTool2.ForceApplyResources = "SharedPropsInternal"
        Appearance46.Image = "Tasks_Delete_Normal"
        buttonTool17.SharedPropsInternal.AppearancesLarge.Appearance = Appearance46
        Appearance47.Image = "Tasks_Delete_Active"
        buttonTool17.SharedPropsInternal.AppearancesLarge.HotTrackAppearance = Appearance47
        resources.ApplyResources(buttonTool17.SharedPropsInternal, "buttonTool17.SharedPropsInternal")
        buttonTool17.ForceApplyResources = "SharedPropsInternal"
        Me.ultraToolbarsManager1.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {fontListTool2, comboBoxTool2, popupColorPickerTool3, popupColorPickerTool4, buttonTool5, buttonTool24, buttonTool25, buttonTool26, buttonTool27, buttonTool28, popupMenuTool6, popupMenuTool10, buttonTool37, buttonTool38, stateButtonTool2, stateButtonTool4, stateButtonTool6, buttonTool16, buttonTool50, buttonTool51, buttonTool52, labelTool3, labelTool4, buttonTool56, buttonTool57, buttonTool58, buttonTool60, buttonTool3, buttonTool9, buttonTool10, buttonTool7, buttonTool12, popupMenuTool3, listTool2, buttonTool15, labelTool6, labelTool10, listTool6, buttonTool29, popupControlContainerTool2, buttonTool17})
        '
        'ilDefaultImagesLarge
        '
        Me.ilDefaultImagesLarge.ImageStream = CType(resources.GetObject("ilDefaultImagesLarge.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilDefaultImagesLarge.TransparentColor = System.Drawing.Color.Transparent
        Me.ilDefaultImagesLarge.Images.SetKeyName(0, "Properties_Notes_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(1, "Properties_Notes_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(2, "Properties_Notes_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(3, "Tasks_MoveLeft_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(4, "Tasks_MoveLeft_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(5, "Tasks_MoveLeft_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(6, "Tasks_MoveRight_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(7, "Tasks_MoveRight_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(8, "Tasks_MoveRight_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(9, "Tasks_MoveTask_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(10, "Tasks_MoveTask_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(11, "Tasks_MoveTask_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(12, "Tasks_PercentComplete_0_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(13, "Tasks_PercentComplete_0_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(14, "Tasks_PercentComplete_0_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(15, "Tasks_PercentComplete_25_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(16, "Tasks_PercentComplete_25_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(17, "Tasks_PercentComplete_25_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(18, "Tasks_PercentComplete_50_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(19, "Tasks_PercentComplete_50_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(20, "Tasks_PercentComplete_50_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(21, "Tasks_PercentComplete_75_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(22, "Tasks_PercentComplete_75_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(23, "Tasks_PercentComplete_75_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(24, "Tasks_PercentComplete_100_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(25, "Tasks_PercentComplete_100_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(26, "Tasks_PercentComplete_100_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(27, "Insert_Milestone_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(28, "Insert_Milestone_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(29, "Insert_Milestone_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(30, "Properties_TaskInformation_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(31, "Properties_TaskInformation_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(32, "Properties_TaskInformation_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(33, "Tasks_Delete_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(34, "Tasks_Delete_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(35, "Tasks_Delete_Disabled")
        Me.ilDefaultImagesLarge.Images.SetKeyName(36, "Insert_Task_Normal")
        Me.ilDefaultImagesLarge.Images.SetKeyName(37, "Insert_Task_Active")
        Me.ilDefaultImagesLarge.Images.SetKeyName(38, "Insert_Task_Disabled")
        '
        'ilDefaultImagesSmall
        '
        Me.ilDefaultImagesSmall.ImageStream = CType(resources.GetObject("ilDefaultImagesSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilDefaultImagesSmall.TransparentColor = System.Drawing.Color.Transparent
        Me.ilDefaultImagesSmall.Images.SetKeyName(0, "Font_Bold_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(1, "Font_Bold_Active")
        Me.ilDefaultImagesSmall.Images.SetKeyName(2, "Font_Bold_Disabled")
        Me.ilDefaultImagesSmall.Images.SetKeyName(3, "Font_Italic_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(4, "Font_Italic_Active")
        Me.ilDefaultImagesSmall.Images.SetKeyName(5, "Font_Italic_Disabled")
        Me.ilDefaultImagesSmall.Images.SetKeyName(6, "Font_Underline_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(7, "Font_Underline_Active")
        Me.ilDefaultImagesSmall.Images.SetKeyName(8, "Font_Underline_Disabled")
        Me.ilDefaultImagesSmall.Images.SetKeyName(9, "Font_ForeColor_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(10, "Font_ForeColor_Active")
        Me.ilDefaultImagesSmall.Images.SetKeyName(11, "Font_ForeColor_Disabled")
        Me.ilDefaultImagesSmall.Images.SetKeyName(12, "Font_BackColor_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(13, "Font_BackColor_Active")
        Me.ilDefaultImagesSmall.Images.SetKeyName(14, "Font_BackColor_Disabled")
        Me.ilDefaultImagesSmall.Images.SetKeyName(15, "Tasks_MoveBackward_Day_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(16, "Tasks_MoveBackward_Month_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(17, "Tasks_MoveBackward_Week_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(18, "Tasks_MoveForward_Day_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(19, "Tasks_MoveForward_Month_Normal")
        Me.ilDefaultImagesSmall.Images.SetKeyName(20, "Tasks_MoveForward_Week_Normal")
        '
        'ilColorizedImagesLarge
        '
        Me.ilColorizedImagesLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.ilColorizedImagesLarge, "ilColorizedImagesLarge")
        Me.ilColorizedImagesLarge.TransparentColor = System.Drawing.Color.Transparent
        '
        'ilColorizedImagesSmall
        '
        Me.ilColorizedImagesSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.ilColorizedImagesSmall, "ilColorizedImagesSmall")
        Me.ilColorizedImagesSmall.TransparentColor = System.Drawing.Color.Transparent
        '
        '_ProjectManagerForm_Toolbars_Dock_Area_Right
        '
        Me._ProjectManagerForm_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._ProjectManagerForm_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._ProjectManagerForm_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right
        Me._ProjectManagerForm_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ProjectManagerForm_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 1
        resources.ApplyResources(Me._ProjectManagerForm_Toolbars_Dock_Area_Right, "_ProjectManagerForm_Toolbars_Dock_Area_Right")
        Me._ProjectManagerForm_Toolbars_Dock_Area_Right.Name = "_ProjectManagerForm_Toolbars_Dock_Area_Right"
        Me._ProjectManagerForm_Toolbars_Dock_Area_Right.ToolbarsManager = Me.ultraToolbarsManager1
        '
        '_ProjectManagerForm_Toolbars_Dock_Area_Top
        '
        Me._ProjectManagerForm_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._ProjectManagerForm_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._ProjectManagerForm_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
        Me._ProjectManagerForm_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
        resources.ApplyResources(Me._ProjectManagerForm_Toolbars_Dock_Area_Top, "_ProjectManagerForm_Toolbars_Dock_Area_Top")
        Me._ProjectManagerForm_Toolbars_Dock_Area_Top.Name = "_ProjectManagerForm_Toolbars_Dock_Area_Top"
        Me._ProjectManagerForm_Toolbars_Dock_Area_Top.ToolbarsManager = Me.ultraToolbarsManager1
        '
        '_ProjectManagerForm_Toolbars_Dock_Area_Bottom
        '
        Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
        Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom.InitialResizeAreaExtent = 1
        resources.ApplyResources(Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom, "_ProjectManagerForm_Toolbars_Dock_Area_Bottom")
        Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom.Name = "_ProjectManagerForm_Toolbars_Dock_Area_Bottom"
        Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.ultraToolbarsManager1
        '
        'ultraTouchProvider1
        '
        Me.ultraTouchProvider1.ContainingControl = Me
        Me.ultraTouchProvider1.Enabled = False
        '
        'ultraGanttViewPrintDocument1
        '
        Me.ultraGanttViewPrintDocument1.GanttView = Me.ultraGanttView1
        '
        'frmTaskManager
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Caption = "PROJECT MANAGER"
        Me.Controls.Add(Me.Form1_Fill_Panel)
        Me.Controls.Add(Me._ProjectManagerForm_Toolbars_Dock_Area_Left)
        Me.Controls.Add(Me._ProjectManagerForm_Toolbars_Dock_Area_Right)
        Me.Controls.Add(Me._ProjectManagerForm_Toolbars_Dock_Area_Bottom)
        Me.Controls.Add(Me._ProjectManagerForm_Toolbars_Dock_Area_Top)
        Me.Name = "frmTaskManager"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ultraGanttView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Form1_Fill_Panel.ResumeLayout(False)
        CType(Me.ultraToolbarsManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ultraTouchProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents ultraGanttView1 As Infragistics.Win.UltraWinGanttView.UltraGanttView
    Private WithEvents ultraToolbarsManager1 As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    Private Form1_Fill_Panel As System.Windows.Forms.Panel
	Private _ProjectManagerForm_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
	Private _ProjectManagerForm_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
	Private _ProjectManagerForm_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
	Private _ProjectManagerForm_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Private WithEvents ultraCalendarInfo1 As Infragistics.Win.UltraWinSchedule.UltraCalendarInfo
    Private WithEvents ultraTouchProvider1 As Infragistics.Win.Touch.UltraTouchProvider
    Private ilDefaultImagesLarge As System.Windows.Forms.ImageList
	Private ilColorizedImagesLarge As System.Windows.Forms.ImageList
	Private ilColorizedImagesSmall As System.Windows.Forms.ImageList
	Private ilDefaultImagesSmall As System.Windows.Forms.ImageList
	Private ultraGanttViewPrintDocument1 As Infragistics.Win.UltraWinGanttView.UltraGanttViewPrintDocument
End Class

