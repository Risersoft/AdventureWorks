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
@ModelType frmDivisionModel

@Code
    ViewData("Title") = ""
    Layout = "~/Views/Shared/_FrameworkLayout.vbhtml"
    Dim modelJson = Model.SerializeJson()
End Code

<link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
<script src="~/Scripts/jquery-ui-1.12.1.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script>
<link href="~/Content/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="~/Content/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="~/Content/font-awesome.css" rel="stylesheet" />

<div Class="container cbackground">
    <form action="" method="post" name="userform" id="userform" ng-controller="FormController">
        <input type="hidden" id="model_json" value='@Html.Raw(modelJson)' />
        @Html.AntiForgeryToken()
        <div Class="form-horizontal">
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <label class="control-label"></label>
                    <h5>{{title}} Division</h5>
                </div>
            </div>
            <hr />
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <label class="control-label labeltext">Division Code <span class="red">*</span></label>
                    <input type="text" name="dcode" ng-model="DivisionInfo.DivisionCode" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.dcode.$invalid]" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <label class="control-label labeltext">Division Name <span class="red">*</span></label>
                    <input type="text" name="dname" ng-model="DivisionInfo.DivisionName" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.dname.$invalid]" />
                </div>
            </div>
            <div class="clearfix" style="margin-top:20px;"></div>
            <div class="row" style="">
                <ul class="nav nav-tabs" id="myTab" role="tablist" style="width:100%">
                    <li class="nav-item">
                        <a class="nav-link active" id="desdocgrp-tab" data-toggle="tab" href="#desdocgrp" role="tab" aria-controls="desdocgrp" aria-selected="true">DesDocGrp</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="stddocgrp-tab" data-toggle="tab" href="#stddocgrp" role="tab" aria-controls="stddocgrp" aria-selected="false">StdDocGrp</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="dofilestage-tab" data-toggle="tab" href="#dofilestage" role="tab" aria-controls="dofilestage" aria-selected="false">DOFileStage</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="dosection-tab" data-toggle="tab" href="#dosection" role="tab" aria-controls="dosection" aria-selected="false">DOSection</a>
                    </li>
                </ul>
                <div class="tab-content" id="myTabContent" style="width:99%;">
                    <div class="tab-pane fade show active" id="desdocgrp" role="tabpanel" aria-labelledby="desdocgrp-tab">
                        <div class="clearfix" style="margin-top:20px;"></div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <a href="" class="btn btn-primary btnf" ng-click="Addrow('desdocgrp')" title="Add Des Doc Grp" style="margin-top: -65px;">Add</a>
                            </div>
                        </div>
                        <div class="row" ng-show="desdocgrpRows.length == 0">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="alert alert-info">
                                    <i class="fa fa-info-circle" aria-hidden="true"></i> <b>Please click on "Add" Button to add records.</b>
                                </div>
                            </div>
                        </div>
                        <div class="row" ng-show="desdocgrpRows.length > 0" style="margin-left:0px;">
                            <div class="col-md-3">
                                <label class="control-label labeltext">Des Doc Grp Name</label>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label labeltext">Nesting</label>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label labeltext">In Assembly Ext.</label>
                            </div>
                        </div>
                        <div ng-repeat="row1 in desdocgrpRows">
                            <div class="row" style="margin-left:0px;">
                                <div class="col-md-3">
                                    <input type="text" name="txtdesDocGrpName{{$index}}" ng-model="row1.DesDocGrpName" class="form-control" style="max-width:540px !important;" />
                                </div>
                                <div class="col-md-3">
                                    <select class="form-control" name="ddlonNesting{{$index}}" ng-options="bool as bool.name for bool in tfddl_IsForNesting track by bool.value" ng-model="row1.IsForNestingSelected" style="max-width:540px !important;"></select>
                                </div>
                                <div class="col-md-3">
                                    <select class="form-control" name="ddlonInAssemblyExt{{$index}}" ng-options="bool as bool.name for bool in tfddl_InAssemblyExt track by bool.value" ng-model="row1.InAssemblyExtSelected" style="max-width:540px !important;"></select>
                                </div>
                                <div class="col-md-2">
                                    <a href="#" ng-click="delete($index,'desdocgrp')"><i class="fa fa-2x fa-trash-o"></i></a>
                                </div>
                            </div>
                            <div class="clearfix" style="margin-top:10px;"></div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="stddocgrp" role="tabpanel" aria-labelledby="stddocgrp-tab">
                        <div class="clearfix" style="margin-top:20px;"></div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                @*<a href="" class="btn btne" ng-click="delete($index,'stddocgrp')" title="Delete Des Doc Grp">Delete</a>*@
                                <a href="" class="btn btn-primary btnf" ng-click="Addrow('stddocgrp')" title="Add Des Doc Grp" style="margin-top: -65px;">Add</a>
                            </div>
                        </div>
                        <div class="row" ng-show="stddocgrpRows.length == 0">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="alert alert-info">
                                    <i class="fa fa-info-circle" aria-hidden="true"></i> <b>Please click on "Add" Button to add records.</b>
                                </div>
                            </div>
                        </div>
                        <div class="row" ng-show="stddocgrpRows.length > 0" style="margin-left:0px;">
                            <div class="col-md-3">
                                <label class="control-label labeltext">Group Name</label>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label labeltext">Description</label>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label labeltext">Remark</label>
                            </div>
                        </div>
                        <div ng-repeat="row1 in stddocgrpRows">
                            <div class="row" style="margin-left:0px;">
                                <div class="col-md-3">
                                    <input type="text" name="txtGrpName{{$index}}" ng-model="row1.GroupName" class="form-control" style="max-width:540px !important;" maxlength="50" />
                                </div>
                                <div class="col-md-3">
                                    <input type="text" name="txtDescription{{$index}}" ng-model="row1.Description" class="form-control" style="max-width:540px !important;" maxlength="50" />
                                </div>
                                <div class="col-md-3">
                                    <input type="text" name="txtRemark{{$index}}" ng-model="row1.Remark" class="form-control" style="max-width:540px !important;" maxlength="50" />
                                </div>
                                <div class="col-md-2">
                                    <a href="#" ng-click="delete($index,'stddocgrp')"><i class="fa fa-2x fa-trash-o"></i></a>
                                </div>
                            </div>
                            <div class="clearfix" style="margin-top:10px;"></div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="dofilestage" role="tabpanel" aria-labelledby="dofilestage-tab">
                        <div class="clearfix" style="margin-top:20px;"></div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <a href="" class="btn btn-primary btnf" ng-click="Addrow('dofilestage')" title="Add Des Doc Grp" style="margin-top: -65px;">Add</a>
                            </div>
                        </div>
                        <div class="row" ng-show="dofilestageRows.length == 0">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="alert alert-info">
                                    <i class="fa fa-info-circle" aria-hidden="true"></i> <b>Please click on "Add" Button to add records.</b>
                                </div>
                            </div>
                        </div>
                        <div class="row" ng-show="dofilestageRows.length > 0" style="margin-left: 0px;">
                            <div class="col-md-4">
                                <label class="control-label labeltext">Stage Type</label>
                            </div>
                            <div class="col-md-4">
                                <label class="control-label labeltext">Stage Name</label>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label labeltext"></label>
                            </div>
                        </div>
                        <div ng-repeat="row1 in dofilestageRows">
                            <div class="row" style="margin-left:0px">
                                <div class="col-md-4">
                                    <input type="text" name="txtStageType{{$index}}" ng-model="row1.StageType" class="form-control" style="max-width:540px !important;" maxlength="50" />
                                </div>
                                <div class="col-md-4">
                                    <input type="text" name="txtStageName{{$index}}" ng-model="row1.StageName" class="form-control" style="max-width:540px !important;" maxlength="50" />
                                </div>
                                <div class="col-md-3">
                                    <a href="#" ng-click="delete($index,'dofilestage')"><i class="fa fa-2x fa-trash-o"></i></a>
                                </div>
                            </div>
                            <div class="clearfix" style="margin-top:10px;"></div>
                        </div>
                    </div>

                    <div class="tab-pane fade" id="dosection" role="tabpanel" aria-labelledby="dosection-tab">
                        <div class="clearfix" style="margin-top:20px;"></div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <a href="" class="btn btn-primary btnf" ng-click="Addrow('dosection')" title="Add Des Doc Grp" style="margin-top: -65px;">Add</a>
                            </div>
                        </div>
                        <div class="row" ng-show="dosectionRows.length == 0">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="alert alert-info">
                                    <i class="fa fa-info-circle" aria-hidden="true"></i> <b>Please click on "Add" Button to add records.</b>
                                </div>
                            </div>
                        </div>
                        <div class="row" ng-show="dosectionRows.length > 0" style="margin-left:0px">
                            <div class="col-md-6">
                                <label class="control-label labeltext">Section Name</label>
                            </div>
                            <div class="col-md-1">
                                <label class="control-label labeltext"></label>
                            </div>
                        </div>
                        <div ng-repeat="row1 in dosectionRows">
                            <div class="row" style="margin-left:0px">
                                <div class="col-md-6">
                                    <input type="text" name="txtSectionName{{$index}}" ng-model="row1.SectionName" class="form-control" style="max-width:540px !important;" maxlength="50" />
                                </div>
                                <div class="col-md-1">
                                    <a href="#" ng-click="delete($index,'dosection')" title="Delete Des Doc Grp"><i class="fa fa-2x fa-trash-o"></i></a>
                                </div>
                            </div>
                            <div class="clearfix" style="margin-top:10px;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            @Html.Partial("_SavePanel")
        </div>
    </form>
</div>
@section botscripts
    <script type="text/javascript">
    rsApp.controller('FormController', function ($scope, $http) {
        $scope.model = JSON.parse($("#model_json").val());
        $scope.status = 'loaded';
        var loadmodel = function (result) {
            $scope.model = result;
            $scope.DivisionInfo = $scope.model.dsForm.DT[0];

            $scope.desdocgrpRows = $scope.model.GridViews.DesDoc.MainGrid.myDS.dt;
            $scope.stddocgrpRows = $scope.model.GridViews.StdDrg.MainGrid.myDS.dt;
            $scope.dofilestageRows = $scope.model.GridViews.FileStage.MainGrid.myDS.dt;
            $scope.dosectionRows = $scope.model.GridViews.Section.MainGrid.myDS.dt;

            $scope.visdel = ($scope.model.frmMode == 2 ? false : true);
            $scope.title = ($scope.model.frmMode == 2 ? "Add" : $scope.model.frmMode == 1 ? "Edit" : "");

            $scope.tfddl_IsForNesting = [{ id: 1, value: true, name: 'Yes' }, { id: 2, value: false, name: 'No' }]
            $scope.tfddl_InAssemblyExt = [{ value: true, name: 'Yes' }, { value: false, name: 'No' }]

            if ($scope.desdocgrpRows.length > 0) {
                for (var i = 0; i < $scope.tfddl_IsForNesting.length; i++) {
                    if ($scope.desdocgrpRows[0].IsForNesting == $scope.model.GridViews.DesDoc.MainGrid.myDS.dt[0].IsForNesting) {
                        $scope.IsForNestingSelected = $scope.tfddl_IsForNesting[i];
                        break;
                    }
                }

                $.each($scope.tfddl_InAssemblyExt, function (index, row) {
                    if (row.InAssemblyExt == $scope.tfddl_InAssemblyExt[index].InAssemblyExt) {
                        $scope.InAssemblyExtSelected = row;
                    }
                });
            }

            $.each($scope.desdocgrpRows, function (index, row) {
                row.IsForNestingSelected = $.grep($scope.tfddl_IsForNesting, function (item, index) { return item.value == row.IsForNesting })[0];
                row.InAssemblyExtSelected = $.grep($scope.tfddl_InAssemblyExt, function (item, index) { return item.value == row.InAssemblyExt })[0];
            });

            window.document.title = ($scope.model.frmMode == 2 ? "Add Division" : $scope.model.frmMode == 1 ? "Edit Division" : "");
        };

        loadmodel($scope.model);

        $scope.calculate = function (row) { }

        $scope.GenerateDelPayload = function () {
            var payload = { EntityKey: 'Division', ID: $scope.model.dsForm.DT[0].DivisionID, AcceptWarning: false };
            return payload;
        };

        $scope.cleanupmodel = function (datamodel) {
            //empty function
        };

        $scope.calculatemodel = function () { }

        $scope.Addrow = function (val) {
            if (val == "desdocgrp") {
                $scope.desdocgrpRows.push({});
            }
            else if (val == "stddocgrp") {
                $scope.stddocgrpRows.push({});
            }
            else if (val == "dofilestage") {
                $scope.dofilestageRows.push({});
            }
            else if (val == "dosection") {
                $scope.dosectionRows.push({});
            }
        };

        //Delete row from ng-repeat
        $scope.delete = function (index, val) {
            if (val == "desdocgrp") {
                $scope.desdocgrpRows.splice(index, 1);
            }
            else if (val == "stddocgrp") {
                $scope.stddocgrpRows.splice(index, 1);
            }
            else if (val == "dofilestage") {
                $scope.dofilestageRows.splice(index, 1);
            }
            else if (val == "dosection") {
                $scope.dosectionRows.splice(index, 1);
            }
        }

        @Html.RenderFile("~/scripts/rsforms.js")
    })
    </script>
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    @Scripts.Render("~/bundles/jqueryui")
end section