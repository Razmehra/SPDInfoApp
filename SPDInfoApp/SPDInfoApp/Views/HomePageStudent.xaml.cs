using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePageStudent : ContentPage
	{
        private PHPServices _phpService = new PHPServices();

        public HomePageStudent ()
		{
			InitializeComponent ();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body><FONT color='#1E0029'><marquee behavior='scroll' direction='left' scrollamount=5>"
                  + "Special Offer of  Plumber+Electrician Services for 1 year..  Only for upto Rs. 2000.  Tap here for Free Registration.." + "</marquee></FONT></body></html>";

            webView.Source = htmlSource;
            FetchMassages();
        }

        private async void FetchMassages()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("No Internet", "Network Error:No Internet connection available.\n !Turn ON your data connection or connect using wifi then try again.", "Ok");
                return;
            }
            //PHPServices
            var data = await _phpService.FetchMessages(new string[] { "0" });
            var mydata = Utils.DeserializeFromJson<ObservableCollection<MessageModel>>(data);//List<MessageModel>

            string Msgs = "";

            foreach (var msg in mydata)
            {
                Msgs = Msgs + " || " + msg.MsgMessage;
            }

            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body><FONT color='#1E0029'><marquee behavior='scroll' direction='left' scrollamount=5>"
                  + Msgs + "</marquee></FONT></body></html>";
            webView.Source = null;
            webView.Source = htmlSource;



            //  MessagesList = mydata;
            // MessagesList.OrderByDescending(w => w.MsgID);
            //LVMessages.BindingContext = this;
            //LVMessages.ItemsSource = null;
            //LVMessages.ItemsSource = MessagesList.OrderByDescending(w => w.MsgID);


        }

        private void BtnBrowser_Clicked(object sender, EventArgs e)
        {

        }

        private void WebView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (btnBrowser.Width != webView.Width) { btnBrowser.WidthRequest = webView.Width; }

            }
            catch (Exception)
            {
            }

        }

        private async void BtnFillInformation_Clicked(object sender, EventArgs e)
        {
          await Navigation.PushModalAsync(  new NavigationPage(new TabPageStudentInfo()));
        }
    }
}