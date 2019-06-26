using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessagesPage : ContentPage
	{
		public MessagesPage ()
		{
			InitializeComponent ();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body><FONT color='#1E0029'><marquee behavior='scroll' direction='left' scrollamount=5>"
                  + "Special Offer of  Plumber+Electrician Services for 1 year..  Only for upto Rs. 2000.  Tap here for Free Registration.." + "</marquee></FONT></body></html>";

            webView.Source = htmlSource;

        }

        private void BtnBrowser_Clicked(object sender, EventArgs e)
        {

        }

        private void WebView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}