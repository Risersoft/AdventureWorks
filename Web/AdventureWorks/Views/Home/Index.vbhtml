@imports risersoft.shared.cloud.common
@Code
    ViewData("Title") = "Home Page"
    Dim baseUrl As String = Request.Url.Scheme & "://" & Request.Url.Authority & Request.ApplicationPath.TrimEnd("/")
    Dim banner As String = CommonModuleBase.GetConfigSetting("banner")
    If String.IsNullOrEmpty(banner) Then banner = "/Content/images/Projects-banner.jpg" Else banner = baseUrl & banner

End Code
<div class="row">
    <div class="col-md-8">
        <img src="@banner" style="width:650px;" class="img-responsive banner-img">
    </div>
    <div class="col-md-3 gst-banner-text">
        <h1 class="gst-banner-text-title">ProjectsNirvana</h1>
        <p class="lead gst-banner-text-para">Effective Project Management.</p>
        <p><a href="/app" class="btn btn-primary btn-lg">Start Now &raquo;</a></p>
    </div>
</div>
<div class="row clsimgb">
    <div class="col-md-3 footer-nav footer-text">
        <div class="clsfoot">
            <h2>Planning</h2>
            <p>
                Simple and Productive Tool for Gantt Charts, Task Progress and Task Grid.
            </p>
        </div>
    </div>
    <div class="col-md-3 footer-nav footer-text">
        <div class="clsfoot">
            <h2>Accounting</h2>
            <p>
                Get accurate accounting information including inventory reports.
            </p>
        </div>
    </div>
    <div class="col-md-3 footer-nav footer-text">
        <div class="clsfoot">
            <h2>Collaborate</h2>
            <p>
                Create and track work items and events. Assign owners and discuss progress.
            </p>
        </div>
    </div>
    <div class="col-md-3 footer-text">
        <div class="clsfoot">
            <h2>Multi Platform</h2>
            <p>
                Securely access your cloud hosted data from desktop, web & mobile.
            </p>
        </div>
    </div>
</div>
