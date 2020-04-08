using risersoft.shared;
using risersoft.shared.portable.Models.Nav;
using Risersoft.Framework.Pages.Framework;
using Risersoft.Framework.XFView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrjNirvana.XF.Pages
{
    public partial class frmEvent : BasePage
    {
        public frmEvent()
        {
            InitializeComponent();
        }
        public override async Task<bool> PrepForm(clsXamView oView, EnumfrmMode prepMode, string prepIDX, string strXML = "")
        {
            this.FormPrepared = false;
            var objModel = await this.InitData("frmEventModel", oView, prepMode, prepIDX, strXML);
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
          await VSave();
        }
    }
}