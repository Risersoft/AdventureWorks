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
@ModelType frmPostModel

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
<style type="text/css">
    .e-richtexteditor .e-rte-toolbar, .e-richtexteditor .e-rte-toolbar.e-toolbar.e-extended-toolbar {
        z-index: 9 !important;
    }

    .choices__list--dropdown {
        z-index: 9999 !important;
    }
</style>
<script type="text/javascript">
    var defaultRTE; var defaultRTEFN;
    function created() {
        defaultRTE = this
    };

    function createdfn() {
        defaultRTEFN = this
    };
</script>

<div class="container cbackground">
    <form action="" method="post" name="userform" ng-controller="FormController">
        <input type="hidden" id="model_json" value='@Html.Raw(modelJson)' />
        @Html.AntiForgeryToken()
        <div Class="form-horizontal">
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <Label Class="control-label labeltext"></Label>
                    <h5>{{title}} Forum Post</h5>
                </div>
            </div>
            <hr />
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10"></div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-6">
                    <Label Class="control-label labeltext">Title <span class="red">*</span></Label>
                    <input type="text" name="title" class="form-control" ng-model="WorkPost.Title" style="max-width:795px" required ng-class="{true: 'error'}[submitted() && userform.title.$invalid]" />
                </div>
                <div Class="col-md-3">
                    <Label Class="control-label labeltext">Visible Flag <span class="red">*</span></Label>
                    <select ng-model="WorkPost.VisibleFlagSelected"
                            name="wfln" class="form-control" ng-options="item.descriptot for item in dsCombo.VisibleFlag track by item.codeword" required ng-class="{true: 'error'}[submitted() && userform.wfln.$invalid]"></select>
                </div>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>
                <div Class="col-md-10">
                    <Label Class="control-label labeltext">Content</Label>
                    @*<textarea rows="8" class="form-control" ng-model="WorkPost.Content" style="max-width:795px;"></textarea>*@
                    @Html.EJS().RichTextEditor("default").Value("").Created("created").Render()
                    @Html.EJS().ScriptManager()
                </div>
            </div>
            <div style="display:none;">
                @*<div class="row"><div class="col-md-1"></div><div class="col-md-10"><label class="control-label">Notification</label></div></div>
                    <div Class="row">
                        <div Class="col-md-1"></div>
                        <div Class="col-md-3">
                            <Label Class="control-label labeltext">Notify To Users</Label><br />
                            <div ng-repeat="item in model.GridViews.Notify.MainGrid.myDS.dt">{{item.UserName}}</div>
                        </div>
                        <div Class="col-md-3">
                            <button type="button" style="float:right;margin-top: 15px;" Class="btn btn-primary" ng-click="pitem()">Select</button>
                        </div>
                    </div>*@
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <label class="control-label labeltext">Notify To Users <span class="red">*</span></label>
                    <select id="choices-multiple-remove-button" name="assignto" ng-model="WorkPost.NotifyUserSetIDSelected" ng-options="biz.UserName for biz in dsCombo.User track by biz.UserID " Class="form-control" placeholder="Select User" multiple required ng-class="{true: 'error'}[submitted() && userform.assignto.$invalid]"></select>
                </div>
            </div>

            <div class="row"><hr /></div>
            @Html.Partial("_SavePanel")
            <div id="dialogFilterfile">
                <input type="checkbox" ng-click="toggleAll(mult_dsCombo)" ng-model="isAllSelected"> Select all
                <hr style="margin-top: 0rem;margin-bottom: 0rem;" />
                <div ng-repeat="row in mult_dsCombo">
                    <input type="checkbox" ng-model="row.Entityname" ng-change="optionToggled(mult_dsCombo)"> {{row.UserName}}
                </div>
            </div>
            <div id="dialogFilterfilen">
                <div ng-repeat="row in dsCombo.User">
                    {{row.UserName}}<input type="checkbox" ng-model="row.Entityname" />
                </div>
            </div>
            <div id="comments" ng-show="isShowComment">
                @Html.Partial("_Comment")
                <div Class="row marb">
                    <button type="button" id="btnCommentSave" Class="btn btn-primary btnf" ng-click="btncomment(WorkPost.ForumPostID)">Save</button>
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
                $(this).datepicker({ dateFormat: 'yy-mm-dd' });
            });
        }, 10);

        var multipleCancelButton = new Choices('#choices-multiple-remove-button', {
            removeItemButton: true
        });
    });

        rsApp.controller('FormController', function ($scope, $http, usSpinnerService) {
        $scope.model = JSON.parse($("#model_json").val());
        $scope.status = 'loaded';
        var loadmodel = function (result) {
            //debugger
            $scope.model = result;
            $('#pdtn').datepicker({ dateFormat: 'yy-mm-dd' });
            $scope.WorkPost = $scope.model.dsForm.DT[0];
            $scope.dsCombo = $scope.model.dsCombo;
            defaultRTE.value = $scope.WorkPost.ContentHTML == undefined ? "" : $scope.WorkPost.ContentHTML;
            $scope.ValueLists = $scope.model.ValueLists;
            $scope.errorMsg = "";
            $scope.isShowErrMsg = false;
            $scope.deleteId = '';
            //$scope.itemvis = false;

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
            //$scope.comment = ($scope.model.frmMode == 2 ? [] : $scope.model.DatasetCollection == {} ? $scope.model.DatasetCollection.comment.dt : []);
            $scope.visdel = ($scope.model.frmMode == 2 ? false : true);
            $scope.title = ($scope.model.frmMode == 2 ? "Add" : $scope.model.frmMode == 1 ? "Edit" : "");

            $scope.visdelete = false;
            @Html.RenderLookup("WorkPost", Model, Model.dsForm.Tables(0))
            $scope.item = function () {
            };
            $scope.delete = function (index) {
            };

            $scope.typchange = function (typch) {
            };

            $scope.partyfn = function (code) {
            };

            if (Array.isArray($scope.model.GridViews.Notify.MainGrid.myDS.dt) && $scope.model.GridViews.Notify.MainGrid.myDS.dt.length) {
                $scope.WorkPost.NotifyUserSetIDSelected = $scope.model.GridViews.Notify.MainGrid.myDS.dt;
            }
            else {
                $scope.WorkPost.NotifyUserSetIDSelected = [$scope.WorkPost.NotifyUserSetIDSelected];
            }

            window.document.title = ($scope.model.frmMode == 2 ? "Add Forum Post" : $scope.model.frmMode == 1 ? "Edit Forum Post" : "");
        };

        loadmodel($scope.model);

        $scope.calculate = function (row) {
        }

        $scope.hsnfc = function (row) {
        }

        $scope.GenerateDelPayload = function () {
            var payload = { EntityKey: 'ForumPost', ID: $scope.model.dsForm.DT[0].ForumPostID, AcceptWarning: false };
            return payload;
        };

        $scope.calculateAll = function () {
            $scope.WorkPost.ContentHTML = base64EncodeUnicode(defaultRTE.getHtml());
            $scope.WorkPost.Content = base64EncodeUnicode(defaultRTE.getText());
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

            if ($scope.WorkPost.NotifyUserSetIDSelected != null && $scope.WorkPost.NotifyUserSetIDSelected != undefined) {
                $scope.model.GridViews.Notify.MainGrid.myDS.dt = [];
                angular.forEach($scope.WorkPost.NotifyUserSetIDSelected, function (row) {
                    $scope.model.GridViews.Notify.MainGrid.myDS.dt.push(row);
                });
            }
        };
        @Html.RenderFile("~/scripts/rsforms.js")
    })
    </script>
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://res.cloudinary.com/dxfq3iotg/raw/upload/v1569006288/BBBootstrap/choices.min.css?version=7.0.0" />
    <script src="https://res.cloudinary.com/dxfq3iotg/raw/upload/v1569006273/BBBootstrap/choices.min.js?version=7.0.0"></script>
    @Scripts.Render("~/bundles/jqueryui")
end section

