@imports Syncfusion.EJ2
<link href="https://ej2.syncfusion.com/aspnetmvc/Content/styles/bootstrap4.css" rel="stylesheet" />
<script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js"></script>

@*<script type="text/javascript">
    var defaultRTE; var defaultRTEFN;
    function created() {
        defaultRTE = this
    };

    function createdfn() {
        defaultRTEFN = this
    };
</script>*@

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
<div Class="row">
    <div Class="col-md-1"></div>
    <div Class="col-md-10">
        <Label Class="control-label labeltext">Comment <span class="red">*</span></Label><span ng-show="!isShowErrMsg" style="margin-left:30px" class="red">{{errorMsg}}</span>
        <div class="control-section">
            <div class="control-wrapper">
                <div class="control-section">
                    @Html.EJS().RichTextEditor("defaultfn").Value("").Created("createdfn").Render()
                </div>
            </div>
        </div>
        @Html.EJS().ScriptManager()
    </div>
</div>
