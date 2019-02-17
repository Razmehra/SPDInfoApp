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
            switch (loginmode)
            {
                case "Admin":
                    Detail = new NavigationPage(new HomePageAdmin()); 
                    break;
                case "Student":
                    Detail = new NavigationPage(new EntryPage());
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
    }
}