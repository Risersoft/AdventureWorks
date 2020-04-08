@imports Controllers.Controllers
@imports Syncfusion.JavaScript
@imports Syncfusion.JavaScript.Models
@imports Syncfusion.MVC.EJ
@Code
    Layout = Nothing
End Code



<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title><%: Page.Title %> - My ASP.NET Application</title>
    <link href="~/Content/ej.widgets.core.min.css" rel="stylesheet" />
    <link href="~/Content/ej.theme.min.css" rel="stylesheet" />
    <script src="~/Scripts/jquery-2.2.3.min.js"></script>
    
    <script src="~/Scripts/jsrender.min.js"></script>
    <script src="~/Scripts/ej.web.all.min.js"></script>


    <style>
        body, html {
            height: 100% !important;
            padding: 0;
            margin: 0;
        }

        #ganttsample {
            position: absolute;
        }
    </style>

</head>
<body>
    <div id="GanttContainer"></div>
    @(Html.EJ().Gantt("GanttContainer").PredecessorMapping("Predecessor").SizeSettings(Function(size) size.Height("350px").Width("1100px")).ResourceNameMapping("ResourceName").ResourceIdMapping("ResourceID").ResourceInfoMapping("ResourceID").ToolbarSettings(Function(ts)
                                                                                                                                                                                                                                                                      ts.ShowToolbar(True)
                                                                                                                                                                                                                                                                      ts.ToolbarItems(New List(Of GanttToolBarItems)() From {
                                                                                                                                                                                                                                                                          GanttToolBarItems.Add,
                                                                                                                                                                                                                                                                          GanttToolBarItems.Edit,
                                                                                                                                                                                                                                                                          GanttToolBarItems.Delete,
                                                                                                                                                                                                                                                                          GanttToolBarItems.Update,
                                                                                                                                                                                                                                                                          GanttToolBarItems.Cancel,
                                                                                                                                                                                                                                                                          GanttToolBarItems.Indent,
                                                                                                                                                                                                                                                                          GanttToolBarItems.Outdent,
                                                                                                                                                                                                                                                                          GanttToolBarItems.ExpandAll,
                                                                                                                                                                                                                                                                          GanttToolBarItems.CollapseAll,
                                                                                                                                                                                                                                                                          GanttToolBarItems.Search
                                                                                                                                                                                                                                                                      })
                                                                                                                                                                                                                                                                  End Function).EditSettings(Function(edit)
                                                                                                                                                                                                                                                                                                 edit.AllowEditing(True)
                                                                                                                                                                                                                                                                                                 edit.AllowAdding(True)
                                                                                                                                                                                                                                                                                                 edit.AllowDeleting(True)
                                                                                                                                                                                                                                                                                                 edit.EditMode("cellEditing")
                                                                                                                                                                                                                                                                                                 edit.AllowIndent(True)

                                                                                                                                                                                                                                                                                             End Function).TaskIdMapping("TaskID").TaskNameMapping("TaskName").StartDateMapping("StartDate").DurationMapping("Duration").ProgressMapping("Progress").ScheduleStartDate("02/01/2014").ScheduleEndDate("03/14/2014").ChildMapping("SubTasks").TreeColumnIndex(1).Resources(ViewBag.resources).Datasource(ViewBag.datasource))
    @(Html.EJ().ScriptManager())

</body>

</html>

