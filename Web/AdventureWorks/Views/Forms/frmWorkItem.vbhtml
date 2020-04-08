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
@ModelType frmWorkItemModel

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

    select[multiple], select[size] {
        height: auto;
    }

    .btn-group.open .btn.dropdown-toggle {
        background-color: #e6e6e6;
    }

    .choices__list {
        z-index: 999;
    }

    .choices__list--dropdown {
        z-index: 9999 !important;
    }
</style>
<script src="~/Scripts/jQueryUI-datetimepicker.js"></script>

<div class="container cbackground">
    <form action="" method="post" name="userform" ng-controller="FormController">
        <input type="hidden" id="model_json" value='@Html.Raw(modelJson)' />
        @Html.AntiForgeryToken()
        <div Class="form-horizontal">
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-7">
                    <Label Class="control-label labeltext"></Label>
                    <h5 id="">{{title}} Work Item</h5>
                </div>
                <div class="col-md-3">
                    <button type="button" style="float:right; margin-top:15px;" ng-click="itemed()" class="btn btn-default">Edit</button>
                </div>
            </div>
            <hr />
            <div class="clearfix" style="margin-top:15px;"></div>

            <!-- Work Item in View Mode -->
            <div ng-show="itemvis">
                <div class="row" style="line-height: 15px;">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <Label Class="control-label labeltext">Title</Label>
                        <div>{{WorkItem.Title}}</div>
                    </div>
                </div>
                <div Class="row" style="margin-top: 15px;line-height: 15px;">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Work Item Type</Label>
                        <div>{{WorkItem.WorkItemTypeSelected.descriptot}}</div>
                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Review By</Label>
                        <div>{{WorkItem.ReviewUserIDSelected.UserName}}</div>
                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Reporter</Label>
                        <div>{{WorkItem.ReporterUserIDSelected.UserName}}</div>
                    </div>
                </div>
                <div Class="row" style="margin-top: 15px;line-height: 15px;">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Due On Date</Label>
                        <div>{{WorkItem.DueOn| date : "yyyy-MM-dd"}}</div>

                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Status</Label>
                        <div>{{WorkItem.StatusSelected.descriptot}}</div>

                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Visible Flag</Label>
                        <div>{{WorkItem.VisibleFlagSelected.codeword}}</div>
                    </div>
                </div>
                <div Class="row" style="margin-top: 15px;">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-10">
                        <Label Class="control-label labeltext">Content</Label>
                        <div ng-bind-html="myHTML"></div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-5">
                        <Label Class="control-label labeltext">Assigned To</Label>
                        <div ng-repeat="row in model.GridViews.Assign.MainGrid.myDS.dt" ng-show="model.GridViews.Assign.MainGrid.myDS.dt.length > 0">{{row.UserName}}</div>
                        <div ng-show="isSingleShow">{{model.GridViews.Team.MainGrid.myDS.dt.UserName}}</div>
                    </div>
                    <div Class="col-md-5">
                        <Label Class="control-label labeltext">Notify To Users</Label><br />
                        <div ng-repeat="row in model.GridViews.Notify.MainGrid.myDS.dt">{{row.UserName}}</div>
                    </div>
                </div>
            </div>
            <div class="clearfix" style="margin-top:10px"></div>

            <!-- Work Item in ADD and EDIT Mode -->
            <div ng-hide="itemvis">
                <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <Label Class="control-label labeltext">Title <span class="red">*</span></Label>
                        <input type="text" name="wftitle" class="form-control" ng-model="WorkItem.Title" style="max-width:795px" required ng-class="{true: 'error'}[submitted() && userform.wftitle.$invalid]" />
                    </div>
                </div>
                <div Class="row">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Work Item Type <span class="red">*</span></Label>
                        <select ng-model="WorkItem.WorkItemTypeSelected"
                                name="wfln" class="form-control" ng-options="item.descriptot for item in dsCombo.WorkItemType track by item.codeword" required ng-class="{true: 'error'}[submitted() && userform.wfln.$invalid]"></select>
                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Review By</Label>
                        <select class="form-control" ng-model="WorkItem.ReviewUserIDSelected" ng-options="itemtn.UserName for itemtn in dsCombo.Review track by itemtn.UserID" style="max-width: 794px;"></select>
                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Reporter</Label>
                        <select class="form-control" ng-model="WorkItem.ReporterUserIDSelected" ng-options="itemtn.UserName for itemtn in dsCombo.Report track by itemtn.UserID" style="max-width: 794px;"></select>
                    </div>
                </div>
                <div Class="row">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Due On Date <span class="red">*</span></Label><br />
                        <div class="input-group">
                            <input id="pdtn" name="pdtn" type="text" ng-model="WorkItem.DueOn" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.pdtn.$invalid]" />
                            <label class="input-group-addon btn" for="date" style="position: absolute; margin-left: 205px; z-index:9">
                                <span class="fa fa-calendar open-datetimepicker"></span>
                            </label>
                        </div>
                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Status <span class="red">*</span></Label>
                        <select class="form-control" name="wtfl" ng-model="WorkItem.StatusSelected" ng-options="itemtn.descriptot for itemtn in dsCombo.Status track by itemtn.codeword" style="max-width: 794px;" required ng-class="{true: 'error'}[submitted() && userform.wtfl.$invalid]"></select>
                    </div>
                    <div Class="col-md-3">
                        <Label Class="control-label labeltext">Visible Flag <span class="red">*</span></Label>
                        <select class="form-control" name="cfln" ng-model="WorkItem.VisibleFlagSelected" ng-options="itemtn.codeword for itemtn in dsCombo.VisibleFlag track by itemtn.codeword" style="max-width: 794px;" required ng-class="{true: 'error'}[submitted() && userform.cfln.$invalid]"></select>
                    </div>
                </div>
                <div Class="row">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-10">
                        <Label Class="control-label labeltext">Content</Label>
                        <div class="control-section">
                            <div class="control-wrapper">
                                <div class="control-section">
                                    @Html.EJS().RichTextEditor("default").Value("").Created("created").Render()
                                </div>
                            </div>
                        </div>
                        @Html.EJS().ScriptManager()
                    </div>
                </div>
                <div Class="row">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-10">
                        <Label Class="control-label labeltext">Assign To <span class="red">*</span></Label>
                        <select id="choices-multiple-remove-button" name="assignto" ng-model="WorkItem.AssignUserIDSelected" ng-options="biz.UserName for biz in UserCombo track by biz.UserID " Class="form-control" placeholder="Select User" multiple required ng-class="{true: 'error'}[submitted() && userform.assignto.$invalid]"></select>
                    </div>
                </div>
                <div Class="row">
                    <div Class="col-md-1"></div>
                    <div Class="col-md-10">
                        <Label Class="control-label labeltext">Notify To Users</Label><br />
                        <select id="choices-multiple-remove-button" name="camp" ng-model="WorkItem.NotifyUserIDSelected" ng-options="biz.UserName for biz in NotifyCombo track by biz.UserID " Class="form-control chosenmulti" placeholder="Select User" multiple></select>
                    </div>
                </div>
            </div>
            @Html.Partial("_SavePanel")

            <!-- Comment Section -->
            <div id="comment" ng-show="isShowComment">
                @Html.Partial("_Comment")
                <div Class="row marb">
                    <button type="button" id="btnCommentSave" Class="btn btn-primary btnf" ng-click="btncomment(WorkItem.PIDUnitWorkItemID)">Save</button>
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
        var multipleCancelButton = new Choices('#choices-multiple-remove-button', {
            removeItemButton: true,
            //searchResultLimit: 20,
            //maxItemCount: 100,
            //renderChoiceLimit: 5
        });
    });

        rsApp.controller('FormController', function ($scope, $http, usSpinnerService) {
        $scope.model = JSON.parse($("#model_json").val());
        $scope.status = 'loaded';
        var loadmodel = function (result) {
            //debugger;
            $("#pdtn").datetimepicker({
                format: 'Y/m/d',
                timepicker: false,
                defaultDate: new Date(),
            }).attr('readonly', 'readonly');

            $scope.model = result;
            $scope.WorkItem = $scope.model.dsForm.DT[0];
            $scope.WorkItem.DueOn = $scope.WorkItem.DueOn == undefined ? "" : new moment($scope.WorkItem.DueOn).format("YYYY/MM/D");
            $scope.dsCombo = $scope.model.dsCombo;

            $scope.UserCombo = $scope.dsCombo.User;
            $scope.NotifyCombo = $scope.dsCombo.User;

            $scope.ValueLists = $scope.model.ValueLists;
            $scope.myHTML = defaultRTE.value = $scope.WorkItem.ContentHTML == undefined ? "" : $scope.WorkItem.ContentHTML;

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

            $scope.itemvis = $scope.model.frmMode == 2 ? false : true;
            $scope.errorMsg = "";
            $scope.isShowErrMsg = false;
            $scope.deleteId = '';
            $scope.visdel = ($scope.model.frmMode == 2 ? false : true);
            $scope.title = ($scope.model.frmMode == 2 ? "Add" : $scope.model.frmMode == 1 ? "Edit" : "");
            $scope.visdelete = false;
            $scope.isSingleShow = false;
            $scope.itemed = function () {
                $scope.itemvis = false;
            }

             @Html.RenderLookup("WorkItem", Model, Model.dsForm.Tables(0))

            $scope.item = function () {
            };

            $scope.delete = function (index) {
            };

            $scope.typchange = function (typch) {
            };

            $scope.partyfn = function (code) {
            };

            //Set Values for chosen multiselect box.
            if (Array.isArray($scope.model.GridViews.Assign.MainGrid.myDS.dt) && $scope.model.GridViews.Assign.MainGrid.myDS.dt.length) {
                $scope.WorkItem.AssignUserIDSelected = $scope.model.GridViews.Assign.MainGrid.myDS.dt;
            }
            else {
                $scope.isSingleShow = true;
                //Display Assigned User in View Mode
                $scope.model.GridViews.Assign.MainGrid.myDS.dt = [$scope.WorkItem.AssignUserIDSelected];
                //Selected Assigend User in Edit Mode
                $scope.WorkItem.AssignUserIDSelected = [$scope.WorkItem.AssignUserIDSelected];
            }

            if ($scope.WorkItem.NotifyUsersetID != "" && $scope.WorkItem.NotifyUsersetID != undefined) {
                $scope.WorkItem.NotifyUserIDSelected = $scope.model.GridViews.Notify.MainGrid.myDS.dt
            }

            window.document.title = ($scope.model.frmMode == 2 ? "Add Work Item" : $scope.model.frmMode == 1 ? "Edit Work Item" : "");
        };

        loadmodel($scope.model);

        $scope.calculate = function (row) {
        }

        $scope.hsnfc = function (row) {
        }

        $scope.GenerateDelPayload = function () {
            var payload = { EntityKey: 'PIDUnitWorkItem', ID: $scope.model.dsForm.DT[0].PIDUnitWorkItemID, AcceptWarning: false };
            return payload;
        };

        $scope.calculateAll = function () {
            $scope.WorkItem.ContentHTML = base64EncodeUnicode(defaultRTE.getHtml());
            $scope.WorkItem.Content = base64EncodeUnicode(defaultRTE.getText());
            //$scope.InvoicePurch.VAL = parseFloat(totalamount.toFixed(2));
        };

        $scope.calculateAll();
        $scope.cleanupmodel = function (datamodel) {
            //empty function
            if ($scope.model.frmMode == 1) {
                datamodel.DatasetCollection.comment.dt = null;
            }
        };
        $scope.calculatemodel = function () {
            $scope.calculateAll();

            if ($scope.WorkItem.AssignUserIDSelected != null && $scope.WorkItem.AssignUserIDSelected != undefined) {
                var auLength = $scope.WorkItem.AssignUserIDSelected.length;
                if (auLength > 1) {
                    $scope.model.GridViews.Assign.MainGrid.myDS.dt = [];
                    angular.forEach($scope.WorkItem.AssignUserIDSelected, function (row) {
                        $scope.model.GridViews.Assign.MainGrid.myDS.dt.push(row);
                    });

                    $scope.WorkItem.AssignUserID = null;
                    $scope.WorkItem.AssignUserIDSelected = null;
                    $scope.model.dsForm.DT[0].AssignUserIDSelected = null;
                }
                else {
                    $scope.model.dsForm.DT[0].AssignUserIDSelected = $scope.WorkItem.AssignUserIDSelected[0];
                    $scope.model.GridViews.Assign.MainGrid.myDS.dt = [];
                    $scope.model.dsForm.DT[0].AssignUserSetID = null
                }
            }

            $scope.model.GridViews.Notify.MainGrid.myDS.dt = [];
            angular.forEach($scope.WorkItem.NotifyUserIDSelected, function (row) {
                $scope.model.GridViews.Notify.MainGrid.myDS.dt.push(row);
            });
        };

        @Html.RenderFile("~/scripts/rsforms.js")
    })
    </script>
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://res.cloudinary.com/dxfq3iotg/raw/upload/v1569006288/BBBootstrap/choices.min.css?version=7.0.0" />
    <script src="https://res.cloudinary.com/dxfq3iotg/raw/upload/v1569006273/BBBootstrap/choices.min.js?version=7.0.0"></script>
    @Scripts.Render("~/bundles/jqueryui")
end section