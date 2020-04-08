@imports AuthorizationFramework.Proxies
@imports risersoft.shared.portable
@imports risersoft.app.mxform
@imports risersoft.app.mxform.prj
@imports risersoft.shared.portable.Proxies
@imports risersoft.shared.web.Extensions
@imports Newtonsoft.Json
@imports Controllers.Controllers
@imports Syncfusion.JavaScript
@imports Syncfusion.JavaScript.Models
@imports Syncfusion.MVC.EJ
@imports risersoft.shared
@imports risersoft.shared.web
@imports System.Diagnostics
@imports  Syncfusion.EJ2
@imports EJ2MVCSampleBrowser.Models
@ModelType frmTaskManagerModel

@Code
    Layout = "~/Views/Shared/_FrameworkLayout.vbhtml"

    Dim ctx = CType(ViewContext.Controller, IWebController).myWebController
    Dim gModel = Model.gModel
    Dim sDate = Microsoft.VisualBasic.Format(gModel.ProjectStartTime, "dd MMM yyyy ")
    If (gModel.ProjectFinishTime - gModel.ProjectStartTime).TotalDays < 30 Then gModel.ProjectFinishTime = gModel.ProjectStartTime.AddMonths(1)
    Dim eDate = Microsoft.VisualBasic.Format(gModel.ProjectFinishTime, "dd MMM yyyy ")
    Dim modelJson = Model.SerializeJson()
End Code

<link href="~/Content/font-awesome.css" rel="stylesheet" />
<link href="~/Content/ej2/bootstrap4.css" rel="stylesheet" />
@Scripts.Render("~/bundles/jqueryui")
<script src="~/Scripts/ej2/ej2.min.js"></script>

<script type="text/javascript">
    var defaultRTE;
    function created() {
        defaultRTE = this
    };
</script>
<style>
    .e-code-mirror::before {
        content: '\e345';
    }

    .e-html-preview::before {
        content: '\e350';
    }

    .CodeMirror-linenumber,
    .CodeMirror-gutters {
        display: none;
    }

    .sb-header {
        z-index: 100;
    }

    .sb-content.e-view.hide-header {
        top: 0 !important;
    }

    .sb-header.e-view.hide-header {
        display: none;
    }

    body, html {
        height: 100% !important;
        padding: 0;
        margin: 0;
    }

    #ganttsample {
        position: absolute;
    }

    #GanttContainer_dialog {
        width: 850px !important;
    }
</style>

<form action="" method="post">
    <h2>Task Manager</h2>
    <div name="userform" ng-controller="FormController">
        <input type="hidden" id="model_json" value='@Html.Raw(modelJson)' />
        @Html.AntiForgeryToken()

        <div id="dialogFilterfilen">
            <script type="text/javascript">
                var defaultRTE;
                function created() {
                    defaultRTE = this
                };
            </script>
            <div style="margin-top:15px;z-index:10000000;">
                @Html.EJS().RichTextEditor("default").Created("created").Render()
            </div>
            @Html.EJS().ScriptManager()
        </div>

        @(Html.EJS.Gantt("GanttContainer").DataSource(gModel.Tasks).
                                                            TaskFields(Sub(x)
                                                                           x.Id("TaskID").
                                                   Name("TaskName").
                                                   StartDate("TaskStartTime").
                                                   EndDate("TaskEndTime").
                                                   Duration("Duration").
                                                   Progress("PercentComplete").
                                                   Dependency("Predecessor").
                                                   Child("SubTasks").
                                                   ParentID("ParentTaskID").
                                                   Notes("Notes").
                                                   ResourceInfo("Key")
                                                                       End Sub).EnableContextMenu(True).
                                                                        EditSettings(Sub(edit)
                                                                                         edit.AllowEditing(True).
                                                         AllowAdding(True).
                                                         AllowDeleting(True).
                                                         AllowTaskbarEditing(True).
                                                         Mode(Syncfusion.EJ2.Gantt.EditMode.Auto).
                                                         ShowDeleteConfirmDialog(True)
                                                                                     End Sub).Toolbar(New List(Of String)(
                                                            New String() {
                                                            "Add", "Cancel", "CollapseAll",
                                                            "Delete", "Edit", "ExpandAll",
                                                            "PrevTimeSpan", "NextTimeSpan",
                                                            "Search", "Update"})).AllowSelection(True).
                                                            GridLines(Syncfusion.EJ2.Gantt.GridLine.Both).Height("490px").Width("100%").
                                                            TreeColumnIndex(1).
                                                            ResourceNameMapping("Name").
                                                            ResourceIDMapping("EmailAddress").
                                                            Resources(gModel.Resources).
                                                            HighlightWeekends(True).EditDialogFields(
                                                            Sub(edf)

                                                                edf.Type(Syncfusion.EJ2.Gantt.DialogFieldType.General).HeaderText("General").Add()
                                                                edf.Type(Syncfusion.EJ2.Gantt.DialogFieldType.Dependency).HeaderText("Predecessors").Add()
                                                                edf.Type(Syncfusion.EJ2.Gantt.DialogFieldType.Resources).HeaderText("Resources").Add()
                                                                edf.Type(Syncfusion.EJ2.Gantt.DialogFieldType.Notes).HeaderText("Notes").Add()
                                                            End Sub).
                                                            AddDialogFields(Sub(adf)
                                                                                adf.Type(Syncfusion.EJ2.Gantt.DialogFieldType.General).HeaderText("General").Add()
                                                                                adf.Type(Syncfusion.EJ2.Gantt.DialogFieldType.Dependency).HeaderText("Predecessors").Add()
                                                                                adf.Type(Syncfusion.EJ2.Gantt.DialogFieldType.Resources).HeaderText("Resources").Add()
                                                                                adf.Type(Syncfusion.EJ2.Gantt.DialogFieldType.Notes).HeaderText("Notes").Add()
                                                                            End Sub).
                                    DateFormat("d/M/yyyy").Render()
        )

        @(Html.EJS().ScriptManager())

        @Html.Partial("_SavePanel")

        <div Class="row">
            <div Class="col-md-1"></div>
            <div Class="col-md-10">
                <Label Class="control-label labeltextcn">Comments List</Label><hr /><br />
                <div class="card" ng-repeat="row in comment" style="margin-bottom:10px;">
                    <div class="card-body img-wrap">
                        <a href="#" class="close" ng-click="btnDeleteComment(row.UserCommentID, row.pIDValue)"><i class="fa fa-trash-o"></i> </a>
                        <h6 class="card-subtitle mb-2 text-muted">
                            <i class="fa fa-user"></i> {{row.FullName}} | {{row.Dated | date : "dd MMM, yyyy HH:mm"}}
                        </h6>
                        <p class="card-text" ng-bind-html="row.CommentHTML"></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>

<script type="text/javascript">
    var selectedid;
    rsApp.controller('FormController', function ($scope, $rootScope, $http, $modal, usSpinnerService) {
        $scope.userform = {};
        $scope.model = JSON.parse($("#model_json").val());
        $scope.status = 'loaded';

        var loadmodel = function (result) {
            $scope.model = result;
            $scope.WorkItem = $scope.model.dsForm.pidu[0];
            $scope.comment = $scope.model.DatasetCollection.comment.dt;
            if ($scope.WorkItem != undefined) {
                var pidunitid = selectedid = $scope.WorkItem.pidunitid;
            }

            $scope.fncomn = function () {
                defaultRTE.value = '';
                $('#dialogFilterfilen').dialog({
                    autoOpen: false,
                    resizable: false,
                    height: "auto",
                    width: 700,
                    title: "Comments List",
                    top:205,
                    modal: true,
                    buttons: [
                        {
                            text: "Save",
                            click: function () {
                                if (defaultRTE.getText() != "") {
                                    $scope.errorMsg = "";
                                    var payload = { commentplain: defaultRTE.getText(), ParentID: selectedid, commenthtml: window.btoa(defaultRTE.getHtml()) };
                                    var payload = JSON.stringify(payload);
                                    $.post('/frmTaskManager/ParamsOutput/' + location.search, {
                                        key: 'save',
                                        Params: payload,
                                        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                                    }, function (result) {
                                        if (result.success) {
                                            $scope.comment = result.Data.Data.Table;
                                            $scope.message = '';
                                            defaultRTE.value = '';
                                        }
                                        else {
                                            if (result.message === '') { result.message = 'Unknown Error!' };
                                            $scope.message = result.message;
                                        }
                                        $scope.$apply();
                                        $('#dialogFilterfilen').dialog("close");
                                        return;
                                    });
                                }
                                else {
                                    $scope.errorMsg = "Please enter comment.";
                                    return false;
                                }
                            }
                        },
                        {
                            text: "Cancel",
                            click: function () {
                                $(this).dialog("close");
                            }
                        }
                    ]
                }).dialog("open");
            };

            $scope.open = function () {
                $scope.fncomn();
            };

            window.document.title = ($scope.model.frmMode == 2 ? "Add Task Manager" : $scope.model.frmMode == 1 ? "Edit Task Manager" : "");
        };

        loadmodel($scope.model);
        $scope.calculatemodel = function () {
            var ganttObj = document.getElementById('GanttContainer').ej2_instances[0];
            ganttObj.dataSource[0].Notes = base64EncodeUnicode(ganttObj.dataSource[0].Notes);
            $scope.model.JsonData = JSON.stringify(ganttObj.dataSource);
        };

        $scope.cleanupmodel = function (datamodel) {
            //empty function
            datamodel.dsForm.resource = null;
            datamodel.DatasetCollection.comment.dt = null;
        };

        @Html.RenderFile("~/scripts/rsforms.js")
        $scope.viscom = true;
    });

    $(document).ready(function () {
        $('.e-treegridrows').on('click', function () {
            selectedid = $(this).find('td:first').html();
        });

        setInterval(function () {
            $('#GanttContainerResourcesTabContainercolGroup > col:eq(1)').css('width', '275');
            $('#content-GanttContainerResourcesTabContainercolGroup > col:eq(1)').css('width', '275');
        }, 1);
    });
</script>