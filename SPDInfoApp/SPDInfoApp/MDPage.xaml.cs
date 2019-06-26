using Acr.UserDialogs;
using SPDInfoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDPage : MasterDetailPage
    {
        public MDPage(string loginmode)
        {
            InitializeComponent();
            IsPresented = false;
            switch (loginmode)
            {
                case "Admin":
                    Detail = new NavigationPage(new HomePageAdmin()); 
                    break;
                case "Student":
                    Detail = new NavigationPage(new HomePageStudent()); //new NavigationPage(new TabPageStudentInfo());//(new EntryPage())
                    break;
                case "Alumni":
                    Detail = new NavigationPage(new HomePageAlumni());
                    break;
                default:
                    break;
            }
            
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MDPageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Close App.\n Do you really want to exit?", "Yes", "No");

                if (result)
                {
                    try
                    {

                       // UserDialogs.Instance.Toast("Closing App..");
                       // await LogoutAsync();
                        System.Environment.Exit(0);
                    }
                    catch
                    {
                        await DisplayAlert("Alert", "Something went wrong..", "Ok");
                    }


                }
            });
            return true;
        }
    }
}