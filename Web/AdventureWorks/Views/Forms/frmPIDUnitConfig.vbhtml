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
@ModelType frmPIDUnitConfigModel

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
<script src="~/Scripts/bootstrap-datepicker.min.js"></script>
@*<link rel="stylesheet" href="https://res.cloudinary.com/dxfq3iotg/raw/upload/v1569006288/BBBootstrap/choices.min.css?version=7.0.0" />
    <script src="https://res.cloudinary.com/dxfq3iotg/raw/upload/v1569006273/BBBootstrap/choices.min.js?version=7.0.0"></script>*@

<div class="container cbackground">
    <form action="" method="post" name="userform" id="userform" ng-controller="FormController">
        <input type="hidden" id="model_json" value='@Html.Raw(modelJson)' />
        @Html.AntiForgeryToken()

        <div Class="form-horizontal">
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <label class="control-label"></label>
                    <h5>{{ title }} Project Config</h5>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-1"></div>
                <p>
                    <Label Class="control-label labeltext" style="width:100px;">Party</Label>
                    <div class="col-md-8">
                        <input type="text" class="form-control" ng-model="WorkItem.Customer" ng-readonly="true" style="max-width:795px" />
                    </div>
                </p>
            </div>

            <div class="row">
                <div class="col-md-1"></div>
                <p>
                    <Label Class="control-label labeltext" style="width:100px;">Order No.</Label>
                    <div class="col-md-8">
                        <input type="text" class="form-control" ng-model="WorkItem.ordernum" ng-readonly="true" style="max-width:795px" />
                    </div>
                </p>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <p>
                    <Label Class="control-label labeltext" style="width:100px;">Project</Label>
                    <div class="col-md-8">
                        <input type="text" class="form-control" ng-model="WorkItem.projectname" ng-readonly="true" style="max-width:795px" />
                    </div>
                </p>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <p>
                    <Label Class="control-label labeltext" style="width:100px;">Description</Label>
                    <div class="col-md-8">
                        <input type="text" class="form-control" ng-model="WorkItem.DescripWO" ng-readonly="true" style="max-width:795px" />
                    </div>
                </p>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <p>
                    <Label Class="control-label labeltext" style="width:100px;">File Number</Label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" ng-model="WorkItem.filenum" ng-readonly="true" style="max-width:795px" />
                    </div>
                </p>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>
                <div Class="col-md-10">

                    <input type="checkbox" ng-model="k" />
                    <Label Class="control-label labeltext">All WorkItem Events will be Visible to all Users</Label>
                </div>
            </div>

            <div class="row" style="margin:0px;">
                <ul class="nav nav-tabs" id="myTab" role="tablist" style="width:100%">
                    <li class="nav-item">
                        <a class="nav-link active" id="desdocgrp-tab" data-toggle="tab" href="#desdocgrp" role="tab" aria-controls="usersgrp" aria-selected="true">Users</a>
                    </li>
                </ul>
                <div class="tab-content" id="myTabContent" style="width:99%;">
                    <div class="tab-pane fade show active" id="usersgrp" role="tabpanel" aria-labelledby="usersgrp-tab">
                        <div class="clearfix" style="margin-top:20px;"></div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <a href="" class="btn btn-primary btnf" ng-click="Addrow()" title="Add Des Doc Grp" style="margin-top: -65px;">Add</a>
                            </div>
                        </div>
                        <div class="row" ng-show="userRepeat.length == 0">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="alert alert-info">
                                    <i class="fa fa-info-circle" aria-hidden="true"></i> <b>Please click on "Add" Button to add records.</b>
                                </div>
                            </div>
                        </div>
                        <div class="row" ng-show="userRepeat.length > 0" style="margin-left:0px;">
                            <div class="col-md-1">
                                <label class="control-label labeltext"></label>
                            </div>
                            <div class="col-md-4">
                                <label class="control-label labeltext">Users</label>
                            </div>
                            <div class="col-md-4">
                                <label class="control-label labeltext">Roles</label>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label labeltext"></label>
                            </div>
                        </div>
                        <div ng-repeat="row1 in userRepeat">
                            <div class="row" style="margin-left:0px;">
                                <div class="col-md-1">{{$index+1}}.</div>
                                <div class="col-md-4">
                                    <select name="ddlUsers{{$index}}" class="form-control" ng-model="row1.UserIDSelected" ng-options="row as row.UserName for row in dsCombo.users track by row.UserId" ng-change="removeItem(row1.UserIDSelected)"></select>
                                    @*{{row1.UserID}}*@
                                </div>
                                <div class="col-md-4">
                                    <select name="ddlRoles{{$index}}" class="form-control" ng-model="row1.RoleCodeSelected" ng-options="biz as biz.Descrip for biz in Roles.dsLookup.Table0 track by biz.Code"></select>
                                    @*{{row1.AssignUserIDSelected}}*@
                                </div>
                                <div class="col-md-2">
                                    <a href="#" ng-click="delete($index,'userRepeat')"><i class="fa fa-2x fa-trash-o"></i></a>
                                </div>
                            </div>
                            <div class="clearfix" style="margin-top:10px;"></div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row"><hr /></div>
            <div class="row"><hr /></div>
            @Html.Partial("_SavePanel")
            <div id="dialogFilterfile">
                <div ng-repeat="row in model.dsCombo.users">
                    <input type="checkbox" ng-model="row.Entityname" /> {{row.UserName}}
                </div>
            </div>
        </div>
    </form>
</div>

@section BotScripts
    <script type="text/javascript">
        rsApp.controller('FormController', function ($scope, $http) {
        $scope.model = JSON.parse($("#model_json").val());
        $scope.status = 'loaded';
        var loadmodel = function (result) {
            //debugger
            $scope.model = result;
            $scope.WorkItem = $scope.model.dsForm.DT[0];
            $scope.Roles = $scope.model.GridViews.Roles.MainGrid;
            $scope.dsCombo = $scope.model.dsCombo;
            //$scope.itemvis = fa;
            $scope.title = ($scope.model.frmMode == 2 ? "Add" : $scope.model.frmMode == 1 ? "Edit" : "");
            $scope.userRepeat= $scope.model.GridViews.Roles.MainGrid.myDS.dt;
           @Html.RenderLookup("WorkItem", Model, Model.dsForm.Tables(0))
            $.each($scope.userRepeat, function (index, row) {
                row.UserIDSelected = $.grep($scope.model.dsCombo.users, function (item, index) { return item.UserId === row.UserID })[0];
                row.RoleCodeSelected = $.grep($scope.Roles.dsLookup.Table0, function (item, index) { return item.Code === row.RoleCode})[0];
            });

            window.document.title = ($scope.model.frmMode == 2 ? "Add Unit Config" : $scope.model.frmMode == 1 ? "Edit Unit Config" : "");
        };

            loadmodel($scope.model);

        $scope.calculate = function (row) {
        };

        $scope.hsnfc = function (row) {
        };

        //Add New Row
        $scope.Addrow = function () {
            var dscomblen = $scope.dsCombo.users.length;
            var userreplen = $scope.userRepeat.length;
            if (userreplen < dscomblen) {
                $scope.userRepeat.push({});
            }
        };

        //Delete row from ng-repeat
        $scope.delete = function (index, val) {
            $scope.userRepeat.splice(index, 1);
        };

        //$scope.removeItem = function (item) {
        //    debugger;
        //    for (var i = 0; i < $scope.dsCombo.users.length; i++) {
        //        if ($scope.dsCombo.users[i].UserId === item.UserId) {
        //            $scope.dsCombo.users.splice(i, 1);
        //        }
        //    }
        //    console.log($scope.dsCombo.users);
        //}

        $scope.GenerateDelPayload = function () {
        };

        $scope.calculateAll = function () {
            //$scope.InvoicePurch.VAL = parseFloat(totalamount.toFixed(2));
        };

        $scope.calculateAll();
        $scope.cleanupmodel = function (datamodel) {
            //empty function
        };
        $scope.calculatemodel = function () {
            $scope.calculateAll();
        };

        $scope.visdel = ($scope.model.frmMode == 2 ? false : true);
        $scope.visdelete = false;
        @Html.RenderFile("~/scripts/rsforms.js")
    })
    </script>
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    @Scripts.Render("~/bundles/jqueryui")
end section
