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
@ModelType frmEventModel

@Code
    ViewData("Title") = ""
    Layout = "~/Views/Shared/_FrameworkLayout.vbhtml"
    Dim modelJson = Model.SerializeJson()
End Code

<link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
<script src="~/Scripts/jquery-ui-1.12.1.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script>
<link href="~/Content/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="~/Content/font-awesome.css" rel="stylesheet" />
<script src="~/Scripts/bootstrap-datepicker.min.js"></script>
<link href="https://ej2.syncfusion.com/aspnetmvc/Content/styles/bootstrap4.css" rel="stylesheet" />
<script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js"></script>
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
<style type="text/css">
    .choices__list--dropdown {
        z-index: 99 !important;
    }
</style>
<script src="~/Scripts/jQueryUI-datetimepicker.js"></script>

<div class="container cbackground">
    <form action="" method="post" name="userform" id="userform" ng-controller="FormController">
        <input type="hidden" id="model_json" value='@Html.Raw(modelJson)' />
        @Html.AntiForgeryToken()
        <div Class="form-horizontal">
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <label class="control-label"></label>
                    <h5>{{ title }} Calendar Event</h5>
                </div>
            </div>
            <hr />
            <div class="row" style="margin-top:15px;">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <label class="control-label labeltext">Assign To</label>
                    <select id="choices-multiple-remove-button" name="assignto" ng-model="EventInfo.AssignUserIDSelected" ng-options="biz.UserName for biz in model.dsCombo.User track by biz.UserID" Class="form-control" placeholder="Select User" multiple></select>
                    @*{{EventInfo.AssignUserIDSelected}}*@
                </div>
            </div>
            <div class="clearfix" style="margin-top:10px;"></div>
            <div class="row">
                <div class="col-md-1"></div>
                <div Class="col-md-11">
                </div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-3">
                    <label class="control-label labeltext">Subject <span class="red">*</span></label>
                    @*ng-change="EnableDisable()"*@
                    <input type="text" name="csubt" ng-model="EventInfo.Subject" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.csubt.$invalid]" />
                </div>
                <div class="col-md-3">
                    <Label Class="control-label labeltext">Date and Time<span class="red">*</span></Label>
                    <div class="input-group">
                        <input id="datec" name="cdat" type="text" ng-model="EventInfo.SDate" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.cdat.$invalid]" />
                        <label class="input-group-addon btn" for="date" style="position: absolute; margin-left: 205px; z-index:9">
                            <span class="fa fa-calendar open-datetimepicker"></span>
                        </label>
                    </div>
                </div>
                <div class="col-md-2">
                    <label class="control-label labeltext">Duration (Hours)<span class="red">*</span></label>
                    <input type="text" name="chon" ng-model="EventInfo.DurationHours" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.chon.$invalid]" />
                </div>
                <div class="col-md-2">
                    <label class="control-label labeltext">Duration (Minutes)</label>
                    <input type="text" ng-model="EventInfo.DurationMinutes" class="form-control" />
                </div>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>
                <div Class="col-md-10">
                    <Label Class="control-label labeltext">Description</Label>
                    <textarea rows="8" ng-model="EventInfo.Description" class="form-control" style="max-width:888px;"></textarea>
                </div>
            </div>
            <div Class="row" style="margin-top:10px;">
                <div Class="col-md-1"></div>
                <div Class="col-md-3">
                    <Label Class="control-label" style="font-weight:bold">Reminder</Label>
                    <input id="chkReminder" type="checkbox" ng-model="isReminder" ng-click="Reminder()" />
                </div>
            </div>
            <div ng-show="isReminder">
                <div Class="row">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-3">
                        <label class="control-label labeltext">Recurrence</label>
                        <select class="form-control" ng-model="EventInfo.ReminderTimeSelected" ng-options="itemtn.DisplayText for itemtn in ValueLists.ReminderTime.ValueListItems track by itemtn.DataValue"></select>
                        <span>before</span>
                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Email Reminder Time</Label>
                        <select class="form-control" ng-model="EventInfo.EMAILReminderTimeSelected" ng-options="itemtn.DisplayText for itemtn in ValueLists.ReminderTime.ValueListItems track by itemtn.DataValue"></select>
                        <span>before</span>
                    </div>
                    <div Class="col-md-4">
                        <Label Class="control-label labeltext">SMS Reminder Time</Label>
                        <select class="form-control" ng-model="EventInfo.SMSReminderTimeSelected" ng-options="itemt.DisplayText for itemt in ValueLists.ReminderTime.ValueListItems track by itemt.DataValue"></select>
                        <span>before</span>
                    </div>
                </div>
                <div class="row" style="margin-top:10px;">
                    <div class="col-md-1"></div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Repeat Type</Label>
                        <select name="camp" ng-model="EventInfo.RepeatTypeSelected" ng-options="item.DisplayText for item in ValueLists.RepeatType.ValueListItems track by item.DataValue" ng-change="ddlRepeatTypeChange()" Class="form-control"></select>
                        <!-- EventInfo.RepeatTypeSelected.DataValue    ng-change="partycampfn(PaymentInfo.RepeatTypeSelected.DataValue)" -->
                    </div>
                    <div Class="col-md-3" ng-show="isRepeatTypeShowHide">
                        <Label Class="control-label labeltext">Repeat Until</Label>
                        <div class="input-group">
                            @*<input type="text" id="datecn" datetime="yyyy-mm-dd" ng-model="EventInfo.RepeatUntil" class="form-control" />*@
                            <input id="datecn" name="cdatecn" type="text" ng-model="EventInfo.RepeatUntil" class="form-control" />
                            <label class="input-group-addon btn" for="date" style="position: absolute; margin-left: 205px; z-index:9">
                                <span class="fa fa-calendar open-datetimepicker"></span>
                            </label>
                        </div>
                    </div>
                    <div class="col-md-2" ng-show="isRepeatTypeShowHide">
                        <label class="control-label labeltext">Interval</label>
                        <input type="text" ng-model="EventInfo.RepeatInterval" class="form-control" />
                    </div>
                    <div class="col-md-2" ng-show="isRepeatTypeShowHide">
                        <label class="control-label labeltext">End After</label>
                        <input type="text" class="form-control" ng-model="EventInfo.RepeatCount" /><span>occurrence(s)</span>
                    </div>
                </div>
                <div class="row" style="margin-bottom:40px;" ng-show="isRepeatTypeShowHide">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <label class="control-label labeltext">Days</label><br />
                        <select id="choices-multiple-remove-button" name="weekday" ng-model="DayOfWeek" Class="form-control" ng-options="week.name for week in WeekOptions track by week.id" placeholder="Select the Day(s)" multiple></select>
                    </div>
                </div>
            </div>

            @Html.Partial("_SavePanel")
            <div id="comments" ng-show="isShowComment">
                @Html.Partial("_Comment")
                <div Class="row marb">
                    <button type="button" id="btnCommentSave" Class="btn btn-primary btnf" ng-click="btncomment(EventInfo.CalendarEventID)">Save</button>
                </div>
            </div>
        </div>
    </form>
</div>
@section botscripts
    <script type="text/javascript">
        $(document).ready(function () {
            var multipleCancelButton = new Choices('#choices-multiple-remove-button', {
                removeItemButton: true
            });
        });

        rsApp.controller('FormController', function ($scope, $http, usSpinnerService) {
        $scope.model = JSON.parse($("#model_json").val());
        $scope.status = 'loaded';
        var loadmodel = function (result) {
            //debugger;
            $("#datec").datetimepicker({
                dateFormat: 'yy/mm/dd',
                timeFormat: 'HH:mm:ss',
            }).attr('readonly', 'readonly');

            $('#datecn').datetimepicker({ format: 'yy-m-d',timepicker: false, }).attr('readonly', 'readonly');

            $scope.model = result;
            $scope.EventInfo = $scope.model.dsForm.DT[0];
            $scope.EventInfo.SDate = $scope.EventInfo.SDate == undefined ? "" : new moment($scope.EventInfo.SDate).format("YYYY/MM/D HH:mm");
            $scope.EventInfo.RepeatUntil = $scope.EventInfo.RepeatUntil == undefined ? "" : new moment($scope.EventInfo.RepeatUntil).format("YYYY/MM/D");
            $scope.isRepeatTypeShowHide = false;

            if ($scope.model.frmMode == 1) {
                if ($scope.EventInfo.RepeatType != undefined && $scope.EventInfo.RepeatType != "" && $scope.EventInfo.RepeatType!=null)
                    $scope.EventInfo.RepeatType.toLowerCase() != "no" ? ($scope.isRepeatTypeShowHide = true) : ($scope.isRepeatTypeShowHide = false);
            }

            $scope.dsCombo = $scope.model.dsCombo;
            $scope.title = ($scope.model.frmMode == 2 ? "Add" : $scope.model.frmMode == 1 ? "Edit" : "");
            $scope.ValueLists = $scope.model.ValueLists;

            if ($scope.model.frmMode == 2) {
                $scope.comment = $scope.model.DatasetCollection.comment;
                $scope.isShowComment = false;
            }
            if ($scope.model.frmMode == 1) {
                $scope.isShowComment = true;
                if ($scope.model.DatasetCollection.comment != undefined) {
                    if (Array.isArray($scope.model.DatasetCollection.comment.dt) && $scope.model.DatasetCollection.comment.dt.length)
                        $scope.comment = $scope.model.DatasetCollection.comment.dt;
                }
                else {
                    $scope.comment = $scope.model.DatasetCollection.comment;
                }
            }

            $scope.DayOfWeek = [];
            $scope.WeekOptions = [{ id: '1', name: 'Monday' }, { id: '2', name: 'Tuesday' }, { id: '3', name: 'Wednesday' }, { id: '4', name: 'Thursday' }, { id: '5', name: 'Friday' }, { id: '6', name: 'Saturday' }, { id: '7', name: 'Sunday' }];

            $scope.errorMsg = "";
            $scope.isShowErrMsg = false;
            $scope.deleteId = '';
            $scope.isShowComment = $scope.visdel = ($scope.model.frmMode == 2 ? false : true);

            if ($scope.EventInfo.ReminderTimeSelected || $scope.EventInfo.EMAILReminderTimeSelected || $scope.EventInfo.SMSReminderTimeSelected || $scope.EventInfo.RepeatTypeSelected || $scope.EventInfo.RepeatUntil || $scope.EventInfo.RepeatInterval || $scope.EventInfo.RepeatCount || $scope.EventInfo.RepeatDow) {
                $scope.isReminder = true;
            }

            $scope.isSingleShow = false;
            if ($scope.EventInfo.RepeatDow != undefined) {
                $scope.DayOfWeek = $.grep($scope.WeekOptions, function (item, index) { return item.name === $scope.EventInfo.RepeatDow.split(',')[index] });
            }

            @Html.RenderLookup("EventInfo", Model, Model.dsForm.Tables(0))

            if (Array.isArray($scope.model.GridViews.Assign.MainGrid.myDS.dt) && $scope.model.GridViews.Assign.MainGrid.myDS.dt.length) {
                $scope.EventInfo.AssignUserIDSelected = $scope.model.GridViews.Assign.MainGrid.myDS.dt;
            }
            else {
                $scope.EventInfo.AssignUserIDSelected = [$scope.EventInfo.AssignUserIDSelected];
            }

            $scope.ddlRepeatTypeChange = function () {
                if ($scope.EventInfo.RepeatTypeSelected.DataValue.toLowerCase() == "no")
                    $scope.isRepeatTypeShowHide = false;
                else
                    $scope.isRepeatTypeShowHide = true;
            }

            window.document.title = ($scope.model.frmMode == 2 ? "Add Event" : $scope.model.frmMode == 1 ? "Edit Event" : "");
        };

        loadmodel($scope.model);
        $scope.calculate = function (row) { }

        $scope.GenerateDelPayload = function () {
            var payload = { EntityKey: 'CalendarEvent', ID: $scope.model.dsForm.DT[0].CalendarEventID, AcceptWarning: false };
            return payload;
        };

        $scope.Reminder = function () {
            if ($scope.isReminder == true) {
                $scope.DayOfWeek = [];
                $scope.isReminder = false;
            }
        };

        $scope.cleanupmodel = function (datamodel) {
            //empty function
            if ($scope.model.frmMode == 1) {
                datamodel.DatasetCollection.comment.dt = null;
            }
        };

        $scope.calculatemodel = function () {
            $scope.EventInfo.RepeatDow = null;
            var arr1 = $scope.WeekOptions;
            var arr2 = $scope.DayOfWeek;
            var strVal = [];

            angular.forEach(arr1, function (value1, key1) {
                angular.forEach(arr2, function (value2, key2) {
                    if (value1.id === value2.id) {
                        strVal.push(value2.name);
                    }
                });
            });

            $scope.EventInfo.RepeatDow = strVal.toString();

            if ($scope.EventInfo.AssignUserIDSelected != null && $scope.EventInfo.AssignUserIDSelected != undefined) {
                var auLength = $scope.EventInfo.AssignUserIDSelected.length;
                if (auLength > 1) {
                    $scope.model.GridViews.Assign.MainGrid.myDS.dt = [];
                    angular.forEach($scope.EventInfo.AssignUserIDSelected, function (row) {
                        $scope.model.GridViews.Assign.MainGrid.myDS.dt.push(row);
                    });

                    $scope.EventInfo.AssignUserID = null;
                    $scope.EventInfo.AssignUserIDSelected = null;
                    $scope.model.dsForm.DT[0].AssignUserIDSelected = null;
                }
                else {
                    $scope.model.dsForm.DT[0].AssignUserIDSelected = $scope.EventInfo.AssignUserIDSelected[0] == undefined ? $scope.EventInfo.AssignUserIDSelected : $scope.EventInfo.AssignUserIDSelected[0];
                    $scope.model.GridViews.Assign.MainGrid.myDS.dt = [];
                    $scope.model.dsForm.DT[0].AssignUserSetID = null
                }
            }
        };

        @Html.RenderFile("~/scripts/rsforms.js")
    });
    </script>
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://res.cloudinary.com/dxfq3iotg/raw/upload/v1569006288/BBBootstrap/choices.min.css?version=7.0.0" />
    <script src="https://res.cloudinary.com/dxfq3iotg/raw/upload/v1569006273/BBBootstrap/choices.min.js?version=7.0.0"></script>
    @Scripts.Render("~/bundles/jqueryui")
end section
