using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using PrjNirvana.XF.Pages;
using risersoft.shared;
using risersoft.shared.portable;
using risersoft.shared.portable.Models.HR;
using risersoft.shared.portable.Proxies;
using Microsoft.AppCenter.Analytics;
using Risersoft.Framework.Entities;
using Risersoft.Framework.Global;
using Risersoft.Framework.Pages;
using Risersoft.Framework.Pages.Framework;
using Risersoft.Framework.UI;
using Risersoft.Framework.XFView;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSTNirvana
{
    public partial class App : Application
    {
        public BlankPage InitialPage;

        public App()
        {
            InitializeComponent();
            var objApp = new clsXamApp(new clsAppExtenderPrj());
            Globals.myXamApp = objApp;
            InitialPage = new BlankPage();
            InitialPage.Title = objApp.objAppExtender.ProgramName();
            this.MainPage = InitialPage;
            Globals.myXamApp.HomeNavigation += App_HomeNavigation;
            Globals.myXamApp.MenuClicked += App_MenuClicked;
            Globals.myXamApp.AccountSelected += App_AccountSelected;
            Globals.myXamApp.UserAuthenticated += App_UserAuthenticated;

        }

        public async void App_MenuClicked(object sender, clsToolModel e)
        {
            await Globals.myXamApp.HandleTool(e);
        }

        public async void App_HomeNavigation(object sender, EventArgs e)
        {
            await Globals.myXamApp.NavigateHome();
        }

        private async void App_UserAuthenticated(object sender, RSUser e)
        {
            await Globals.myXamApp.SwitchAccount((clsXamPolice)Globals.myXamApp.Controller.Police);

        }

        private async void App_AccountSelected(object sender, UserAccountProxy e)
        {
            this.MainPage = InitialPage;
            this.InitialPage.Loader.IsVisible = true;
            var oRet = await Globals.myXamApp.Init(new string[] { });
            this.InitialPage.Loader.IsVisible = false;
            if (oRet.Success)
            {
                this.MainPage = new RootPage();
                await BindMenuItems(Globals.myController());
            }
            else
            {
                await this.InitialPage.DisplayAlert("Init", oRet.Message + Environment.NewLine + oRet.StackTrace, "OK", "Cancel");
            }
        }
        public async Task<Boolean> BindMenuItems(clsXamController controller)
        {
            Globals.myXamApp.RootModel = new RootViewModel()
            {
                Title = Globals.myXamApp.objAppExtender.ProgramName(),
                UserName = Globals.myPolice().GiveUserName(),
                Items = controller.AppModel.BarManager.ToolBars[1].Items
            };
            var pg = this.MainPage as RootPage;
            pg.MenuPage.InitMenuPage(false, true, true, false);
            pg.MenuPage.BindingContext = Globals.myXamApp.RootModel;
            pg.HomePage.BindingContext = Globals.myXamApp.RootModel;
            var pView = new clsXamView(EnumViewMode.acNormalM, Globals.myController());
            //var pg2 = new frmPIDUnitMan();
            //await pg2.PrepForm(pView, EnumfrmMode.acEditM, "2");
            //var pg3 = new frmDivision();
            //await pg3.PrepForm(pView, EnumfrmMode.acEditM, "6");
            //var pg4 = new frmWorkItem();
            //await pg4.PrepForm(pView, EnumfrmMode.acEditM, "4");
            //this.MainPage = pg4;
            return await Task.FromResult(true);
        }

        protected async override void OnStart()
        {
            var result = await Globals.myController().CheckInitPolice();
        }
        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep");
        }
        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
        }

    }

}
