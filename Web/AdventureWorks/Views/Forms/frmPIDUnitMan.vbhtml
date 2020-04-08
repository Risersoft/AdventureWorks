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
@ModelType frmPIDUnitManModel

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

<style type="text/css">
    .ui-datepicker {
        z-index:99!important;
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
                <div class="col-md-10">
                    <Label Class="control-label labeltext"></Label>
                    <h5>{{title}} Project</h5>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-5">
                    <Label Class="control-label labeltext">Customer <span class="red">*</span></Label>
                    <input type="text" name="cust" class="form-control" ng-model="WorkItem.CustomerMan" required ng-class="{true: 'error'}[submitted() && userform.cust.$invalid]" />
                </div>
                <div class="col-md-5">
                    <Label Class="control-label labeltext">Project <span class="red">*</span></Label>
                    <input type="text" name="proj" class="form-control" ng-model="WorkItem.ProjectNameMan" required ng-class="{true: 'error'}[submitted() && userform.proj.$invalid]" />
                </div>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>
                <div Class="col-md-5">
                    <Label Class="control-label labeltext">Order Date <span class="red">*</span></Label><br />
                    <div class="input-group">
                        <input id="orderdt" name="ordt" type="text" ng-model="WorkItem.OrderDateMan" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.ordt.$invalid]" />
                        <label class="input-group-addon btn" for="date" style="position: absolute; margin-left: 240px; z-index:9">
                            <span class="fa fa-calendar open-datetimepicker"></span>
                        </label>
                    </div>
                    @*<div class="input-group">
                        <input type="text" name="ordt" datetime="yyyy-mm-dd" ng-model="WorkItem.OrderDateMan" required Class="form-control my-datepicker" ng-class="{true: 'error'}[submitted() && userform.ordt.$invalid]" readonly />
                        <label class="input-group-addon btn" for="date" style="position: absolute; margin-left: 240px; z-index:9">
                            <span class="fa fa-calendar open-datetimepicker"></span>
                        </label>
                    </div>*@
                </div>
                <div Class="col-md-5">
                    <Label Class="control-label labeltext">Order No. <span class="red">*</span></Label>
                    <input type="text" name="odnoc" class="form-control" ng-model="WorkItem.OrderNumMan" required ng-class="{true: 'error'}[submitted() && userform.odnoc.$invalid]" />
                </div>
            </div>
            <div Class="row" style="margin-bottom: 40px;">
                <div Class="col-md-1"></div>
                <div class="col-md-5">
                    <label class="control-label labeltext">Work Order No. <span class="red">*</span></label>
                    <input type="text" name="wodnoc" ng-model="WorkItem.WOInfo" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.wodnoc.$invalid]" />
                </div>
                <div class="col-md-5">
                    <label class="control-label labeltext">Division</label>
                    <select name="camp" ng-model="WorkItem.DivisionIDManSelected" ng-options="biz.DivisionCode for biz in dsCombo.Division track by biz.Divisionid " Class="form-control"></select>
                </div>
            </div>
            <div Class="form-horizontal">
                <ul class="nav nav-tabs">
                    <li class="labeltext"><a href="#tab" class="nav-link active labeltext" data-toggle="tab">General</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="tab">
                        <div class="row" style="margin-top: 40px;">
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                                <Label Class="control-label labeltext">File No.</Label>
                                <input type="text" class="form-control" ng-model="WorkItem.FileNum" />
                            </div>
                            <div class="col-md-5">
                                <Label Class="control-label labeltext">File Vol.</Label>
                                <input type="text" class="form-control" ng-model="WorkItem.FileVol" />
                            </div>
                        </div>
                        <div Class="row">
                            <div Class="col-md-1"></div>
                            <div Class="col-md-5">
                                <Label Class="control-label labeltext">Design Date</Label><br />
                                <div class="input-group">
                                    <input id="designDate" type="text" ng-model="WorkItem.DesignDate" class="form-control" />
                                    <label class="input-group-addon btn" for="date" style="position: absolute; margin-left: 240px; z-index:9">
                                        <span class="fa fa-calendar open-datetimepicker"></span>
                                    </label>
                                </div>
                                @*<div class="input-group">
                                    <input type="text" datetime="yyyy-mm-dd" ng-model="WorkItem.DesignDate" Class="form-control my-datepicker" readonly />
                                    <label class="input-group-addon btn" for="date" style="position: absolute; margin-left: 240px; z-index:9">
                                        <span class="fa fa-calendar open-datetimepicker"></span>
                                    </label>
                                </div>*@
                            </div>
                            <div Class="col-md-5">
                                <Label Class="control-label labeltext">Scope of Work <span class="red">*</span></Label>
                                <input type="text" name="wdfl" class="form-control" ng-model="WorkItem.DescripWO" required ng-class="{true: 'error'}[submitted() && userform.wdfl.$invalid]" />
                            </div>
                        </div>
                        <div Class="row">
                            <div Class="col-md-1"></div>
                            <div Class="col-md-10">
                                <Label Class="control-label labeltext">Remarks</Label>
                                <textarea rows="8" class="form-control" ng-model="WorkItem.Remarks" style="max-width:795px;"></textarea>
                            </div>
                        </div>
                        <div Class="row">
                            <div Class="col-md-1"></div>
                            <div Class="col-md-10">
                                <Label Class="control-label labeltext">Mark this Work Order as complete</Label>
                                <input type="checkbox" ng-model="WorkItem.IsCompleted" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row"><hr /></div>
            @Html.Partial("_SavePanel")
        </div>
    </form>
</div>

@section BotScripts
    <script type="text/javascript">
    //$(document).ready(function () {
    //    //setInterval(function () {
    //    //    $(".my-datepicker").removeClass("my-datepicker").each(function () {
    //    //        $(this).datepicker({ dateFormat: 'yy-mm-dd' });
    //    //    });
    //    //}, 10);        
    //});

    rsApp.controller('FormController', function ($scope, $http) {
        $scope.model = JSON.parse($("#model_json").val());
        $scope.status = 'loaded';
        var loadmodel = function (result) {

            $("#orderdt").datetimepicker({
                format: 'Y/m/d',
                timepicker: false,
            }).attr('readonly', 'readonly');

            $("#designDate").datetimepicker({
                format: 'Y/m/d',
                timepicker: false,
            }).attr('readonly', 'readonly');        

            $scope.model = result;
            $scope.WorkItem = $scope.model.dsForm.DT[0];  

            $scope.WorkItem.OrderDateMan = $scope.WorkItem.OrderDateMan == undefined ? "" : new moment($scope.WorkItem.OrderDateMan).format("YYYY/MM/D");
            $scope.WorkItem.DesignDate = $scope.WorkItem.DesignDate == undefined ? "" : new moment($scope.WorkItem.DesignDate).format("YYYY/MM/D");
            
            $scope.dsCombo = $scope.model.dsCombo;
            $scope.ValueLists = $scope.model.ValueLists;
            $scope.title = ($scope.model.frmMode == 2 ? "Add" : $scope.model.frmMode == 1 ? "Edit" : "");
            window.document.title = ($scope.model.frmMode == 2 ? "Add Project" : $scope.model.frmMode == 1 ? "Edit Project" : "");
            $scope.visdel = ($scope.model.frmMode == 2 ? false : true);
            $scope.visdelete = false;

            @Html.RenderLookup("WorkItem", Model, Model.dsForm.Tables(0))
        };

        loadmodel($scope.model);

        $scope.calculate = function (row) {
        }

        $scope.hsnfc = function (row) {
        }

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
        @Html.RenderFile("~/scripts/rsforms.js")
    })
    </script>
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    @Scripts.Render("~/bundles/jqueryui")
end section

