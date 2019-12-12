using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
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
            layoutStudentAdmNo.IsVisible = this._loginMode == "Student";
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            //if (UserName.Text == null) { await DisplayAlert("User Name", "Must Provide your Name", "Ok"); return; };
            //if (UserName.Text.Trim() == "" || string.IsNullOrEmpty(UserName.Text.Trim()) || string.IsNullOrWhiteSpace(UserName.Text.Trim()))
            //{
            //    await DisplayAlert("Required..", "Must Provide Your Name..", "Ok");
            //    return;
            //}
            LoginInfo loginInfo = new LoginInfo();
            if (_loginMode == "Student")
            {
                Application.Current.Properties["AuthMPIN_Student"] = "";
                Application.Current.Properties["StudentName"] = UserName.Text;
                Application.Current.Properties["StudentMobile"] = UserMobile.Text;
                Application.Current.Properties["StudentEmail"] = UserEmail.Text;
                Application.Current.Properties["StudentApplicationID"] = UserApplicationID.Text;//.Text;


                loginInfo.UserName = UserName.Text;
                loginInfo.Email = UserEmail.Text;
                loginInfo.Mobile = UserMobile.Text;
                loginInfo.ApplicationId = UserApplicationID.Text;
                loginInfo.Password = "";
                loginInfo.LoginType = "Std";


            }
            else
            {
                Application.Current.Properties["AuthMPIN_Alumni"] = "";
                Application.Current.Properties["AlumniName"] = UserName.Text;
                Application.Current.Properties["AlumniMobile"] = UserMobile.Text;
                Application.Current.Properties["AlumniEmail"] = UserEmail.Text;
                // LoginInfo loginInfo = new LoginInfo();
                loginInfo.UserName = UserName.Text;
                loginInfo.Email = UserEmail.Text;
                loginInfo.Mobile = UserMobile.Text;
                loginInfo.ApplicationId = "";
                loginInfo.Password = "";
                loginInfo.LoginType = "Alm";

            }
            // Application.Current.Properties["LoginInfo"] = null;

            var cuurentLoginInfo = Application.Current.Properties["LoginInfo"];
            if (cuurentLoginInfo == null)
            {
                List<LoginInfo> loginInfos = new List<LoginInfo>();
                loginInfos.Add(loginInfo);
                var jsonstring = Utils.SerializeToJson(loginInfos);

                Application.Current.Properties["LoginInfo"] = jsonstring;// Utils.SerializeToJson(loginInfos);
                //cuurentLoginInfo = Application.Current.Properties["LoginInfo"];
            }
            else
            {
                // var xx = cuurentLoginInfo;
                var loginInfos = Utils.DeserializeFromJson<List<LoginInfo>>(cuurentLoginInfo.ToString());// xx.ToString());// LoginInfo.FromJson(cuurentLoginInfo.ToString());// Utils.DeserializeFromJson<LoginInfo>(xx.ToString());
                loginInfos.Add(loginInfo);
                var jsonstring = Utils.SerializeToJson(loginInfos);

                Application.Current.Properties["LoginInfo"] = jsonstring;
                // Utils.SerializeToJson(loginInfos);

            }


            await Application.Current.SavePropertiesAsync();
            cuurentLoginInfo = Application.Current.Properties["LoginInfo"];

            Application.Current.MainPage = new PinPage(_loginMode);



        }

        private class LoginInfo1
        {
            public string LoginType { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }

            public int MPIN { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string ApplicationId { get; set; }

        }


        private async Task<bool> ValidateEntries()
        {
            if (UserName.Text == null) { await DisplayAlert("Validation", "Must provide your name.", "Ok"); return false; };
            if (UserName.Text.Trim() == "" || string.IsNullOrEmpty(UserName.Text.Trim()) || string.IsNullOrWhiteSpace(UserName.Text.Trim()))
            {
                await DisplayAlert("Required..", "Must provide your name..", "Ok");
                return false;
            }
            if (_loginMode.Equals("Student"))
            {
                if (UserApplicationID.Text == null) { await DisplayAlert("Validation", "Must provide your application id.", "Ok"); return false; };
                if (UserApplicationID.Text.Trim() == "" || string.IsNullOrEmpty(UserApplicationID.Text.Trim()) || string.IsNullOrWhiteSpace(UserApplicationID.Text.Trim()))
                {
                    await DisplayAlert("Required..", "Must provide your application id..", "Ok");
                    return false;
                }


            }
            else
            {
                if (UserEmail.Text == null) { await DisplayAlert("Validation", "Must provide your email id.", "Ok"); return false; };
                if (UserEmail.Text.Trim() == "" || string.IsNullOrEmpty(UserEmail.Text.Trim()) || string.IsNullOrWhiteSpace(UserEmail.Text.Trim()))
                {
                    await DisplayAlert("Required..", "Must provide your email id..", "Ok");
                    return false;
                }

                if (UserMobile.Text == null) { await DisplayAlert("Validation", "Must provide your mobile number.", "Ok"); return false; };
                if (UserMobile.Text.Trim() == "" || string.IsNullOrEmpty(UserMobile.Text.Trim()) || string.IsNullOrWhiteSpace(UserMobile.Text.Trim()))
                {
                    await DisplayAlert("Required..", "Must provide your mobile number..", "Ok");
                    return false;
                }


            }

            return true;
        }

        private async void BtnCancle_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("??", "Sure to Cancle?", "Yes", "No"))
            {
                Application.Current.MainPage = new LoginMenu();
            }
        }

        private void UserApplicationID_Unfocused(object sender, FocusEventArgs e)
        {
            var cuurentLoginInfo = Application.Current.Properties["LoginInfo"];

            if (cuurentLoginInfo != null)
            {
                var loginInfos = Utils.DeserializeFromJson<List<LoginInfo>>(cuurentLoginInfo.ToString());
                var MatchedLogin = loginInfos.Where(w => w.ApplicationId == UserApplicationID.Text.Trim()).FirstOrDefault();
                if (MatchedLogin == null) return;
                UserName.Text = MatchedLogin.UserName;
                UserEmail.Text = MatchedLogin.Email;
                UserMobile.Text = MatchedLogin.Mobile;
                BtnSubmit.Focus();
            }
            else
            {

            }


        }
    }
}