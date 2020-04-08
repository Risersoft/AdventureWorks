using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using risersoft.shared;
using Risersoft.Framework.Pages.Framework;
using Risersoft.Framework.XFView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrjNirvana.XF.Pages
{
    public partial class frmWorkItem : BasePage
    {
        public frmWorkItem()
        {
            InitializeComponent();
        }
        public override async Task<bool> PrepForm(clsXamView oView, EnumfrmMode prepMode, string prepIDX, string strXML = "")
        {
            this.FormPrepared = false;
            var objModel = await this.InitData("frmWorkItemModel", oView, prepMode, prepIDX, strXML);
            if (this.BindModel(objModel, oView))
            {
                this.FormPrepared = true;
            }
            return this.FormPrepared;
        }
        public override bool BindModel(clsFormDataModel NewModel, clsView oView)
        {
            if (base.BindModel(NewModel, oView))
            {
                clsDataRowBindableModel data=null;
                var bnd = this.Model.Bindable();
                this.BindingContext = bnd;
                return true;
            }
            return false;
        }
        public override async Task<bool> VSave()
        {
            bool result = false;
            this.InitError();
            if (this.ValidateData())
            {
                if (await this.SaveModel()) result = true;
            }
            else this.SetError();
            return result;
        }
        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }
        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            });
        }

        private void pickerWorkItemType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked_Save(object sender, EventArgs e)
        {
            await VSave();
        }

        private async void Button_Clicked_Cancel(object sender, EventArgs e)
        {

        }
    }
}