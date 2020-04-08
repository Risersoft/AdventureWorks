using Risersoft.Framework.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrjNirvana.XF.Pages
{
    public partial class DivisionList : ContentPage
    {
        List<ListDivision> listDivisions;
        public DivisionList(ListDivision listDivision)
        {
            InitializeComponent();
            AddDivision(listDivision);
        }
        public DivisionList()
        {
            InitializeComponent();
            ListDivision listdivision = new ListDivision();
            AddDivision(listdivision);
        }
        public void AddDivision(ListDivision listdivision)
        {
            listDivisions = new List<ListDivision>();
            if (listdivision.DesDocGrpName != null)
            {
                listDivisions.Add(listdivision);
            }
            listDivision.ItemsSource = listDivisions;
            if(listDivisions.Count==0)
            {
                listDivision.IsVisible = false;
                btnAddDivision.IsVisible = true;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new frmDivision();
        }

        private void listDivision_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selectedItem = e.ItemData as ListDivision;
           // Application.Current.MainPage = new frmDivision(selectedItem);
        }
    }
    public class ListDivision
    {
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DesDocGrpName { get; set; }
        public string Nesting { get; set; }
        public string InAssemblyExt { get; set; }

    }
}