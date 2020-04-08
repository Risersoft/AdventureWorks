Imports risersoft.shared
Imports risersoft.shared.dal
Imports risersoft.app.mxform
Imports risersoft.shared.win
Imports risersoft.shared.cloud
Imports System.Threading.Tasks
Imports risersoft.app.mxent
Imports System.ServiceModel
Imports risersoft.app2.shared
Imports risersoft.app.reports.erp
Imports risersoft.chrome.winforms
Imports risersoft.app.mxform.prj
Imports risersoft.shared.portable.Models.Nav
Imports System.Configuration
Imports risersoft.app.config
Imports Serilog
Imports System.IO

Public Class clsExtendAppProj
    Inherits clsExtendRsBase


    Protected Friend mFileProvider As ICloudFileProvider, mQueueProvider As IQueueProvider
    Dim dic As clsCollecString(Of Boolean)
    Public Overrides Function IsFreeLicenseApp(oApp As clsCoreApp) As Boolean
        Return True
    End Function
    Public Overrides Function GetLicProductInfo() As LicProductInfo
        Return New LicProductInfo("advdem", 2.0, "AdventureDemo.Std")
    End Function
    Public Overrides Function AboutBox() As risersoft.shared.IForm
        Return New frmAboutBox
    End Function
    Public Overrides Function StartupAppCode() As String
        Return ""
    End Function

    Public Overrides Function ProgramName() As String
        'Return "Hisaab-Kitaab"
        Return "AdventureWorks"
    End Function

    Public Overrides Function NewDBName() As String
        Return "mxnirvanadb"
    End Function


    Public Overrides Function MinDBVersion() As Decimal
        Return My.Settings.MinDBVersion
    End Function

    Public Overrides Function ProgramCode() As String
        Return "mxadvw"
    End Function

    Public Overrides Function LinkLabel() As String
        Return "http://www.risersoft.com/books"
    End Function
    Public Overrides Function dicMat() As clsCollecString(Of Boolean)
        'TODO: ETO=Inv, once this is implemented for Max @Kanohar
        If dic Is Nothing Then dic = myFuncsBase.dicMat(False, False, False, True, True, True, True, False)
        Return dic
    End Function

    Public Overrides Function FileServerRequired() As Boolean
        Return False
    End Function
    Public Overrides Sub UpdateSettings(s As risersoft.shared.myAppSettings)
        s.RelateLoginPerson = False
        's.AppLoadBehaviour = EnumLoadBehaviour.acForceXML
    End Sub


    Public Overrides Function GenerateDataTable(pView As clsViewModel, conf As clsBandConf, pdclientview As String, strFilter As String) As DataTable
        Dim oGen As New clsPDCViewGeneratorBase(pView.myContext)
        Dim dt1 As DataTable = oGen.GenerateDataTable(pView, conf, pdclientview, strFilter)
        Return dt1
    End Function
    Public Overrides Function FileProviderCode(context As IProviderContext) As String
        Dim str1 As String = ""
        If ProviderFactory.IsCloudServiceClient(context) Then
            str1 = "blob"
        Else
            str1 = MyBase.FileProviderCode(context)
        End If

        Return str1
    End Function
    Public Overrides Function FileProviderClient(context As IProviderContext, AppCode As String, ProviderCode As String) As clsFileProviderClientBase
        Dim provider As clsFileProviderClientBase
        Select Case ProviderCode.Trim.ToLower
            Case "blob"
                If mFileProvider Is Nothing Then mFileProvider = ProviderFactory.CreateFileProvider(context)
                provider = New clsBlobFileClient(context, AppCode, mFileProvider)
            Case Else
                provider = MyBase.FileProviderClient(context, AppCode, ProviderCode)
        End Select
        Return provider
    End Function
    Public Overrides Function QueueProvider(context As IProviderContext) As IQueueProvider
        Dim setting1 As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("ServiceBus")
        If (mQueueProvider Is Nothing) AndAlso (setting1 IsNot Nothing) Then mQueueProvider = New clsAzureSBQProvider(context, setting1.ConnectionString)
        Return mQueueProvider
    End Function

    Public Overrides Function CreateDataProvider(context As clsAppController, cb As IAsyncWCFCallBack) As IAppDataProvider
        Dim Provider As IAppDataProvider = ProviderFactory.CreateDataProvider(context, cb)
        Return Provider

    End Function
    Public Overrides Function oSQLHelper(provider As IAppDataProvider) As ISQLHelper2
        Dim objSQLHelper = New clsSQLHelper2(provider)
        Return objSQLHelper
    End Function

    Public Overrides Function dicFormModelTypes() As clsCollecString(Of Type)
        If dicFormModel Is Nothing Then
            dicFormModel = New clsCollecString(Of Type)
            'Me.AddTypeAssembly(dicFormModel, GetType(frmGenPersonModel).Assembly, GetType(clsFormDataModel))
            Me.AddTypeAssembly(dicFormModel, GetType(frmTaskManagerModel).Assembly, GetType(clsFormDataModel))

        End If
        Return dicFormModel
    End Function
    Public Overrides Function dicReportProviderTypes(myContext As clsAppController) As clsCollecString(Of Type)
        If dicReportModelProvider Is Nothing Then
            dicReportModelProvider = New clsCollecString(Of Type)

            'Me.AddTypeAssembly(dicReportModelProvider, GetType(accReportDataProvider).Assembly, GetType(clsReportDataProviderBase))
        End If
        Return dicReportModelProvider

    End Function
    Public Overrides Function WOTabList(oWO As clsWOInfoBase) As List(Of String)
        Dim tl As New List(Of String)
        tl.Add("params")
        tl.Add("ref")
        Return tl
    End Function

    Public Overrides Function dicTaskProviderTypes() As clsCollecString(Of Type)
        If dicTaskProvider Is Nothing Then
            dicTaskProvider = New clsCollecString(Of Type)
            Me.AddTypeAssembly(dicTaskProvider, GetType(EVTaskProvider).Assembly, GetType(clsTaskProviderBase))
        End If
        Return dicTaskProvider
    End Function
    Public Overrides Function fBrowser() As IMxBrowser
        Dim f As New BrowserForm
        f.MakeDownLevel()
        Return f
    End Function
    Public Overrides Function OnAppClose(oApp As clsCoreApp) As Boolean
        ChromeHandler.Shutdown()
        Return MyBase.OnAppClose(oApp)
    End Function
    Public Overrides Function frmDel(pView As clsView, IDX As String, sysentXML As String) As IForm
        'Dim f As frmDel = New frmDel
        'f.PrepForm(pView, EnumfrmMode.acEditM, IDX, sysentXML)
        'Return f

    End Function
    Public Overrides Function CreateLogger(context As IProviderContext) As ILogger

        Dim path2 = Path.Combine("Logs",
                If(context.Controller.CalcRLSId() = Guid.Empty, "", context.Controller.CalcRLSId().ToString()))
        Dim fileSuffix = If(context.Controller.TaskInfo.ApiTaskID = Guid.Empty, "", context.Controller.TaskInfo.ApiTaskID.ToString())

        If (Not Directory.Exists(path2)) Then Directory.CreateDirectory(path2)

        Dim filePathFormat = Path.Combine(path2, $"log{fileSuffix}.txt")

        Dim Log = New LoggerConfiguration().WriteTo.Trace().CreateLogger()
        Return Log
    End Function
End Class
