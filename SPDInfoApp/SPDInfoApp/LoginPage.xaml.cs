using SPDInfoApp.ViewModels;
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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            var VM = new LoginPageViewModel();
            this.BindingContext = VM;
            VM.DisplayInvalidLoginPromt += async () => await DisplayAlert("Error", "Invalid Login, Try again", "ok");

            InitializeComponent();

            UserName.Text = "";// "admin";
            Password.Text = "";// "admin1234";
            UserName.Completed += (object sender, EventArgs e) => { Password.Focus(); };
            Password.Completed += (object sender, EventArgs e) => { VM.SubmitCommand.Execute(null); };
            BtnSkipLogin.Clicked += (object sender, EventArgs e) => { VM.SkipCommand.Execute(null); };
        }

        private void BtnSkipLogin_Clicked(object sender, EventArgs e)
        {
           
        }
    }
}