using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BCMessageCenter : ContentPage
	{
        private PHPServices _phpService = new PHPServices();
        private ObservableCollection<MessageModel> _messagesList;
        public ObservableCollection<MessageModel> MessagesList {
            get { return _messagesList; }
            set { _messagesList=value; }
        }

        public BCMessageCenter ()
		{
			InitializeComponent ();
            FetchMassages();

		}

        private async void FetchMassages()
        {
            var data = await _phpService.FetchMessages(new string[] { "0" });
            var mydata = Utils.DeserializeFromJson<ObservableCollection<MessageModel>>(data);//List<MessageModel>

            MessagesList = mydata;
            LVMessages.BindingContext = this;
            LVMessages.ItemsSource = null;
            LVMessages.ItemsSource = MessagesList;


        }

        private void BtnNew_Clicked(object sender, EventArgs e)
        {

        }

        private async void TGREdit_Tapped(object sender, EventArgs e)
        {
           await  Navigation.PushModalAsync(new BCMessages() { Title="Broadcast Message: New Message", Icon="SPDInfo.png", BackgroundColor=Color.FloralWhite} );
                
        }

        private void TGRDelete_Tapped(object sender, EventArgs e)
        {

        }

        private void TGRViewTargets_Tapped(object sender, EventArgs e)
        {

        }

    }
}