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

        public HomePageStudent()
        {
            InitializeComponent();
            //var htmlSource = new HtmlWebViewSource();
            //htmlSource.Html = @"<html><body><FONT color='#1E0029'><marquee behavior='scroll' direction='left' scrollamount=5>"
            //      + "Special Offer of  Plumber+Electrician Services for 1 year..  Only for upto Rs. 2000.  Tap here for Free Registration.." + "</marquee></FONT></body></html>";

            //webView.Source = htmlSource;
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

            // var TargetList = mydata.Where(w => w.IsScroll && (w.MsgMode != 2)).Select(s => s.MsgAudience).ToList();
            var MyMessages = mydata.Where(w => w.IsScroll && (w.MsgMode != 2)).ToList();
            string Msgs = "";
            var applicationID = int.Parse(Application.Current.Properties["StudentApplicationID"].ToString());
            foreach (var msg in MyMessages)
            {
                switch (msg.MsgMode)
                {
                    case 1:

                        if (msg.MsgAudience != null)
                        {
                            var IsInMsgAudience = (Utils.DeserializeFromJson<List<MessageTarget>>(msg.MsgAudience)).Where(w => w.ApplicationID == applicationID).Count() > 0;
                           if(IsInMsgAudience) Msgs = Msgs + " || " + msg.MsgMessage;
                        }

                        break;
                    case 2:

                        break;
                    case 3:
                        Msgs = Msgs + " || " + msg.MsgMessage;
                        break;

                    default:
                        break;
                }


            }
            if (Msgs.Length > 0)
            {
                var htmlSource = new HtmlWebViewSource();
                //MARQUEE LOOP=2 BEHAVIOR=SLIDE
                htmlSource.Html = @"<html><body><FONT color='#1E0029'><marquee  behavior='scroll' direction='left' scrollamount=5>"
                    +  Msgs + "</marquee></FONT></body></html>";
                
                webView.Source = null;
                webView.Source = htmlSource;
            }

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
            await Navigation.PushModalAsync(new NavigationPage(new TabPageStudentInfo()));
        }
    }
}