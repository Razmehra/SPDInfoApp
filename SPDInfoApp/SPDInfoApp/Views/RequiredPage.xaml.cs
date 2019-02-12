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
    public partial class RequiredPage : ContentPage
    {
        private string _loginMode;

        public RequiredPage(string loginmode)
        {
            InitializeComponent();
            this._loginMode = loginmode;
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            if (UserName.Text == null) { await DisplayAlert("User Name", "Must Provide your Name", "Ok"); return; };
            if (UserName.Text.Trim() == "" || string.IsNullOrEmpty(UserName.Text.Trim()) || string.IsNullOrWhiteSpace(UserName.Text.Trim()))
            {
                await DisplayAlert("Required..", "Must Provide Your Name..", "Ok");
                return;
            }

            if (_loginMode == "Student")
            {
                Application.Current.Properties["AuthMPIN_Student"] = "";
                Application.Current.Properties["StudentName"] = UserName.Text;
                Application.Current.Properties["StudentMobile"] = UserMobile.Text;
            }
            else
            {
                Application.Current.Properties["AuthMPIN_Alumni"] = "";
                Application.Current.Properties["AlumniName"] = UserName.Text;
                Application.Current.Properties["AlumniMobile"] = UserMobile.Text;

            }
            await Application.Current.SavePropertiesAsync();

            Application.Current.MainPage = new PinPage(_loginMode);



        }

        private async void BtnCancle_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("??", "Sure to Cancle?", "Yes", "No"))
            {
                Application.Current.MainPage = new LoginMenu();
            }
        }
    }
}