@imports AuthorizationFramework.Proxies
@imports risersoft.shared.portable
@imports risersoft.app.mxform
@imports risersoft.app.mxform.prj
@imports risersoft.shared.portable.Proxies
@imports risersoft.shared.web.Extensions
@imports Newtonsoft.Json
@imports Controllers.Controllers
@imports risersoft.shared
@imports risersoft.shared.web
@imports risersoft.shared.web.Extensions
@imports System.Diagnostics
@imports Syncfusion.EJ2
@imports EJ2MVCSampleBrowser.Models
@ModelType frmPIDUnitTimeLogModel

@Code
    ViewData("Title") = ""
    Layout = "~/Views/Shared/_FrameworkLayout.vbhtml"

    Dim modelJson = Model.SerializeJson()
End Code
<link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
<script src="~/Scripts/jquery-ui-1.12.1.min.js"></script>
<link href="~/Content/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="~/Content/font-awesome.css" rel="stylesheet" />
<script src="~/Scripts/bootstrap-datepicker.min.js"></script>
<script src="https://momentjs.com/downloads/moment.js"></script>

<script type="text/javascript">
    var defaultRTE; var defaultRTEFN;
    function created() {
        defaultRTE = this
    };

    function createdfn() {
        defaultRTEFN = this
    };
</script>
<script src="~/Scripts/jQueryUI-datetimepicker.js"></script>
<div class="container cbackground">
    <form action="" method="post" name="userform" ng-controller="FormController">
        <input type="hidden" id="model_json" value='@Html.Raw(modelJson)' />
        @Html.AntiForgeryToken()
        <div Class="form-horizontal">
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <Label Class="control-label labeltext"></Label>
                    <h5>{{title}} Time Log</h5>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <Label Class="control-label labeltext">Custom Description <span class="red">*</span></Label>
                    <input name="custDesc" type="text" class="form-control" ng-model="WorkItem.CustomDescrip" style="max-width:795px" required ng-class="{true: 'error'}[submitted() && userform.custDesc.$invalid]" />
                </div>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>
                <div Class="col-md-3">
                    <Label Class="control-label labeltext">Date and Time <span class="red">*</span></Label><br />
                    <div class="input-group">
                        <input type="text" id="fdtn" datetime="yyyy-mm-dd" ng-model="WorkItem.SDate" Class="form-control" required ng-class="{true: 'error'}[submitted() && userform.fdate.$invalid]" />
                        <label class="input-group-addon btn" for="date" style="position: absolute; margin-left: 205px; z-index:9">
                            <span class="fa fa-calendar open-datetimepicker"></span>
                        </label>
                    </div>
                </div>
                <div Class="col-md-3">
                    <Label Class="control-label labeltext">Billing Status <span class="red">*</span></Label>
                    <select class="form-control" name="wtfl" ng-model="WorkItem.IsBillableSelected" ng-options="itemtn.DisplayText for itemtn in ValueLists.Billable.ValueListItems track by itemtn.DataValue" style="max-width: 794px;" required ng-class="{true: 'error'}[submitted() && userform.wtfl.$invalid]"></select>
                </div>
                <div Class="col-md-3">
                    <Label Class="control-label labeltext">Status <span class="red">*</span></Label>
                    <select class="form-control" name="wtfl" ng-model="WorkItem.StatusSelected" ng-options="itemtn.descriptot for itemtn in dsCombo.Status track by itemtn.codeword" style="max-width: 794px;" required ng-class="{true: 'error'}[submitted() && userform.wtfl.$invalid]"></select>
                </div>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>
                <div class="col-md-3"><label class="control-label labeltext">Duration (hours) <span class="red">*</span></label><input type="text" name="wtdh" ng-model="WorkItem.DurationHours" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.wtdh.$invalid]" /></div>
                <div class="col-md-3"><label class="control-label labeltext">Duration (minutes)</label><input type="text" ng-model="WorkItem.DurationMinutes" class="form-control" /></div>
                <div Class="col-md-3">
                    <Label Class="control-label labeltext">Assigned To User</Label>
                    <select name="camp" ng-model="WorkItem.AssignUserIDSelected" ng-options="biz.UserName for biz in dsCombo.User track by biz.UserID " Class="form-control"></select>
                </div>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>
                <div Class="col-md-10">
                    <Label Class="control-label labeltext">Notes</Label>
                    @*<textarea rows="8" class="form-control" ng-model="WorkItem.Notes" style="max-width:795px;"></textarea>*@
                    @Html.EJS().RichTextEditor("default").Value("").Created("created").Render()
                    @Html.EJS().ScriptManager()
                </div>
            </div>
            <div class="row"><hr /></div>
            @Html.Partial("_SavePanel")
            <div id="dialogFilterfile">
                <div ng-repeat="row in dsCombo.User">
                    {{row.UserName}}<input type="checkbox" ng-model="row.Entityname" />
                </div>
            </div>
            <div id="dialogFilterfilen">
                <div ng-repeat="row in dsCombo.User">
                    {{row.UserName}}<input type="checkbox" ng-model="row.Entityname" />
                </div>
            </div>
        </div>
        <script type="text/ng-template" id="myModalSuccessContent.html">
            <div class="modal-header">
                <h3 class="modal-title" id="modal-title">Success</h3>
            </div>
            <div class="modal-body" id="modal-body">
                Done <i class="fa fa-2x fa-check"></i>
            </div>
            <div class="modal-footer">
                <button class="btn btn-info" type="button" ng-click="cancel()">OK</button>
            </div>
        </script>
    </form>
</div>

@section BotScripts
    <script type="text/javascript">
    $(document).ready(function () {
        setInterval(function () {
            $(".my-datepicker").removeClass("my-datepicker").each(function () {
                $(this).datepicker({ dateFormat: 'yy-mm-dd HH:mm' });
            });
        }, 10);

        $("#fdtn").datetimepicker({
            dateFormat: 'yy-mm-dd',
            timeFormat: 'HH:mm:ss',
        }).attr('readonly', 'readonly');

        setInterval(function () {
            $('#ui-datepicker-div').hide();
        }, 1);
    });

    rsApp.controller('FormController', function ($scope, $http) {
        $scope.model = JSON.parse($("#model_json").val());
        $scope.status = 'loaded';
        var loadmodel = function (result) {
            $scope.model = result;

            $('#fdtn').datepicker({ dateFormat: 'yy-mm-dd HH:mm' });
            $scope.WorkItem = $scope.model.dsForm.DT[0];
            defaultRTE.value = $scope.WorkItem.NotesHtml == undefined ? "" : $scope.WorkItem.NotesHtml;
            $scope.WorkItem.SDate = new moment($scope.WorkItem.SDate).format("YYYY-MM-D HH:mm");
            $scope.dsCombo = $scope.model.dsCombo;
            $scope.ValueLists = $scope.model.ValueLists;
            //$scope.itemvis = true;
            $scope.title = ($scope.model.frmMode == 2 ? "Add" : $scope.model.frmMode == 1 ? "Edit" : "");
            $scope.visdel = ($scope.model.frmMode == 2 ? false : true);
            $scope.visdelete = false;

            //$scope.itemed = function () {
            //    $scope.itemvis = false;
            //}
             @Html.RenderLookup("WorkItem", Model, Model.dsForm.Tables(0))

            $scope.item = function () {

            };
            $scope.delete = function (index) {

            };

            $scope.typchange = function (typch) {

            };

            $scope.partyfn = function (code) {

            };

            $scope.pitem = function () {
                $('#dialogFilterfile').dialog({
                    autoOpen: false,
                    resizable: false,
                    height: "auto",
                    width: 400,
                    modal: true,
                    buttons: [
                        {
                            text: "Save",
                            click: function () {
                                var string = '';
                                $scope.model.GridViews.Team.MainGrid.myDS.dt = [];
                                $.each($scope.dsCombo.User, function (index, row) {
                                    if (row.Entityname == true) {
                                        $scope.model.GridViews.Team.MainGrid.myDS.dt.push(row);
                                        string = string + row.UserName + ',';
                                    }
                                    $scope.EventInfo.AssignUserSet = string;
                                }); $scope.$apply();

                                $(this).dialog("close");
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
            }

            $scope.piten = function () {
                $('#dialogFilterfilen').dialog({
                    autoOpen: false,
                    resizable: false,
                    height: "auto",
                    width: 400,
                    modal: true,
                    buttons: [
                        {
                            text: "Save",
                            click: function () {
                                var string = '';
                                $scope.model.GridViews.Team.MainGrid.myDS.dt = [];
                                $.each($scope.dsCombo.User, function (index, row) {
                                    if (row.Entityname == true) {
                                        $scope.model.GridViews.Team.MainGrid.myDS.dt.push(row);

                                        string = string + row.UserName + ',';
                                    }
                                    $scope.EventInfo.AssignUserSet = string;
                                }); $scope.$apply();

                                $(this).dialog("close");
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
            }

            window.document.title = ($scope.model.frmMode == 2 ? "Add Unit Time Log" : $scope.model.frmMode == 1 ? "Edit Unit Time Log" : "");
        };

        loadmodel($scope.model);

        $scope.calculate = function (row) {
        }

        $scope.hsnfc = function (row) {
        }

        $scope.GenerateDelPayload = function () {
            var payload = { EntityKey: 'PIDUnitTimeLog', ID: $scope.model.dsForm.DT[0].PIDUnitTimeLogID, AcceptWarning: false };
            return payload;
        };

        $scope.calculateAll = function () {
            $scope.WorkItem.NotesHtml = base64EncodeUnicode(defaultRTE.getHtml());
            $scope.WorkItem.Notes = base64EncodeUnicode(defaultRTE.getText());
            //$scope.InvoicePurch.VAL = parseFloat(totalamount.toFixed(2));
        };

        $scope.calculateAll();
        $scope.cleanupmodel = function (datamodel) {

            //empty function
        };

        $scope.calculatemodel = function () {
            $scope.calculateAll();
        };

        @Html.RenderFile("~/scripts/rsforms.js")
    })
    </script>
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    @Scripts.Render("~/bundles/jqueryui")
end section

