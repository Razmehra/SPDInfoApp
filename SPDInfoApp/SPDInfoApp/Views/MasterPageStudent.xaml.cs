using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPageStudent : ContentPage
	{
        public ListView ListView;
        public MasterPageStudent ()
		{
			InitializeComponent ();

            BindingContext = new MDPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MDPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MDPageMenuItem> MenuItems { get; set; }

            public MDPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MDPageMenuItem>(new[]
                {
                    new MDPageMenuItem { Id = 0, Title = "Profile" },
                    new MDPageMenuItem { Id = 1, Title = "Feedback" },
                    new MDPageMenuItem { Id = 2, Title = "Gallery" },
                    new MDPageMenuItem { Id = 3, Title = "About us" },
                    new MDPageMenuItem { Id = 4, Title = "Exit" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private void MenuItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MDPageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            //Detail = new NavigationPage(page);
            //IsPresented = false;

            //MasterPage.ListView.SelectedItem = null;
            switch (item.Id)
            {
                case 0:
                    Navigation.PushModalAsync(new EntryPage());//new NavigationPage(new EntryPage("Student")));
                    break;
                case 1:
                    Navigation.PushModalAsync(new NavigationPage(new StudentFeedback("Student")));
                    break;
                case 2:

                    break;
                case 3:

                    break;
                default:
                    break;
            }




        }
    }
}